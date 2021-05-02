using System.Collections.Generic;
using actions;
using characters;

namespace cards.testCards {
    public class Strike : AbstractAttackCard {
        public Strike() : base("Strike", 1, 4, 1, 1, 4,
            1, "", "", CardModifier.Middle, CardRarity.Default, CardTarget.Enemy) { }

        protected override void OnUse(AbstractCharacter source, AbstractCharacter target) {
            base.OnUse(source, target);
            // 获取玩家能造成的总伤害
            // TODO 伤害增益百分比
            // TODO 重写行为逻辑
            var damage = source.Weapon.Damage + Damage; // 角色攻击力+卡牌伤害
            AddToBot(new DamageAction(source, target, this, damage));
            // 执行相应动画
        }
    }
}