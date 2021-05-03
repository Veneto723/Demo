using System.Collections.Generic;
using actions;
using characters;
using characters.buffs;

//落樱
namespace cards.testCards {
    public class CherryBlossom : AbstractAttackCard { 
        public CherryBlossom() : base("CherryBlossom", 1, 4, 1, 1, 4,
            1, "", "", CardModifier.Dash, CardRarity.Starter, CardTarget.Enemy) { }

        protected override void OnUse(AbstractCharacter source, AbstractCharacter target) {

            base.OnUse(source, target);
            var damage = source.Weapon.Damage; // 武器伤害
            AddToBot(new DamageAction(source, target, this, damage));
            // 执行相应动画
        }
        
        //Buff: 破甲
        public override void AfterUse(AbstractCharacter source, AbstractCharacter target) {
            AddToBot(new BuffAction(source, target, new DefenceDown(source, target, MagicNumber),
                this,true)); 
        }
    }
}