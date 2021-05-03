using System.Collections.Generic;
using actions;
using characters;
using characters.buffs;

namespace cards.testCards {
    public class CutBack : AbstractAttackCard {
        //name/base cost/base damage/basicNumber/cost/damage/magicnNum/img/discription
        public CutBack() : base("CutBack", 0, 5, 0, 0, 5,   
            0, "", "", CardModifier.Dash, CardRarity.Starter, CardTarget.Enemy) { }

        protected override void OnUse(AbstractCharacter source, AbstractCharacter target) {
            base.OnUse(source, target);
            var damage = Damage; // 卡牌伤害
            AddToBot(new DamageAction(source, target, this, damage));
            // 执行相应动画
        }

        
        }
    }