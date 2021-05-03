using System.Collections.Generic;
using actions;
using characters;
using characters.buffs;

namespace cards.testCards {
    public class WaterMoon: AbstractAttackCard{
        public WaterMoon():base("WaterMoon", 1, 4, 1, 1, 0,
            1, "", "", CardModifier.Dash, CardRarity.Starter, CardTarget.Enemy) { }
        protected override void OnUse(AbstractCharacter source, AbstractCharacter target) {

            base.OnUse(source, target);
            var damage = Damage;
            AddToBot(new DamageAction(source, target, this, damage));
        }
    }
}