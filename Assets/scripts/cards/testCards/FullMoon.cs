using System.Collections.Generic;
using actions;
using characters;

//满月
namespace cards.testCards {
    public class FullMoon : AbstractAttackCard {

        public FullMoon(AbstractCharacter owner) : base("FullMoon", owner.Weapon.Cost, 
            owner.Weapon.Damage,0, 0, "", 
            new Dictionary<Keyword, int>{{Keyword.Deal, owner.Weapon.Damage}}, owner,
            CardModifier.Middle, CardRarity.Starter, CardTarget.Enemy) { }
        
        public override void OnUse(AbstractCharacter source, AbstractCharacter target) {
            base.OnUse(source, source);
            AddToBot(new DamageAction(source, target, this, Damage));
        }

        
    }
}