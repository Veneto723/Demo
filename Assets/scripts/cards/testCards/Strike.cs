using System.Collections.Generic;
using actions;
using characters;
using characters.buffs;

namespace cards.testCards {
    public class Strike : AbstractAttackCard {
        public Strike(AbstractCharacter owner) : base("Strike", 1, 4, 1, 0, "",
            new Dictionary<Keyword, int> {{Keyword.Deal, 4}, {Keyword.Vulnerable, 1}}, owner, CardModifier.Dash,
            CardRarity.Starter, CardTarget.Enemy) { }

        public override void OnUse(AbstractCharacter source, AbstractCharacter target) {
            // 追加连击
            base.OnUse(source, target);
            // 获取玩家能造成的总伤害
            var damage = Damage; // 卡牌伤害
            AddToBot(new DamageAction(source, target, this, damage));
            // 执行相应动画
        }

        public override void AfterUse(AbstractCharacter source, AbstractCharacter target) {
            AddToBot(new BuffAction(source, target, new Vulnerable(source, target, MagicNumber),
                this, true));
        }
    }
}