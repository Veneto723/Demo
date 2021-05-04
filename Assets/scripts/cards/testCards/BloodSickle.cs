using System.Collections.Generic;
using actions;
using characters;

//血镰
namespace cards.testCards {
    public class BloodSickle: AbstractAttackCard{
        public BloodSickle(AbstractCharacter owner):base("BloodSickle", owner.Weapon.Cost, owner.Weapon.Damage, 
            owner.Weapon.Damage, 1, "", 
            new Dictionary<Keyword, int>{{Keyword.Deal, owner.Weapon.Damage}, {Keyword.Heal, owner.Weapon.Damage}}, 
            owner, CardModifier.Middle, CardRarity.Starter,  CardTarget.Enemy) { } 
        
        public override void OnUse(AbstractCharacter source, AbstractCharacter target) {
            base.OnUse(source, source);
            AddToBot(new DamageAction(source, target, this, Damage));
        }

        public override void AfterUse(AbstractCharacter source, AbstractCharacter target) {
            AddToBot(new HealAction(source, source, this, MagicNumber));
        }
    }
}