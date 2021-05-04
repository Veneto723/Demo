using System.Collections.Generic;
using actions;
using characters;

//残月
namespace cards.testCards {
    public class WaningMoon : AbstractAttackCard {

        public WaningMoon(AbstractCharacter owner) : base("WaningMoon", owner.Weapon.Cost, 
            owner.Weapon.Damage,0, 0, "", 
            new Dictionary<Keyword, int>{{Keyword.Deal, owner.Weapon.Damage}}, owner,
            CardModifier.Dash, CardRarity.Starter, CardTarget.Enemy) { }
        
        public override void OnUse(AbstractCharacter source, AbstractCharacter target) {
            base.OnUse(source, source);
            AddToBot(new DamageAction(source, target, this, Damage));
        }

        
    }
}