using System.Collections.Generic;
using actions;
using characters;
using characters.buffs;


//强袭
namespace cards.testCards {
    public class Strike : AbstractAttackCard {
        public Strike(AbstractCharacter owner) : base("Strike", 1, 4, 1, 0, "",
            new Dictionary<Keyword, int> {{Keyword.Deal, 4}, {Keyword.Vulnerable, 1}}, owner, CardModifier.Dash,
            CardRarity.Starter, CardTarget.Enemy) { }

        public override void OnUse(AbstractCharacter source, AbstractCharacter target) {
            // 追加连击
            base.OnUse(source, source);
            AddToBot(new DamageAction(source, target, this, Damage));
            // 执行相应动画
        }

        public override void AfterUse(AbstractCharacter source, AbstractCharacter target) {
            AddToBot(new BuffAction(source, target, new Vulnerable(source, target, MagicNumber),
                this, true));
        }
    }
}