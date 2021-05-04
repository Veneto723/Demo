using System.Collections.Generic;
using actions;
using characters;

// 浮光
namespace cards.testCards {
    public class FloatingLight : AbstractAttackCard {

        public FloatingLight(AbstractCharacter owner) : base("FloatingLight", owner.Weapon.Cost, 
            2 * owner.Weapon.Damage,0, 2, "", 
            new Dictionary<Keyword, int>{{Keyword.Deal, 2 * owner.Weapon.Damage}}, owner,
            CardModifier.Lower, CardRarity.Starter, CardTarget.Enemy) { }
        
        public override void OnUse(AbstractCharacter source, AbstractCharacter target) {
            base.OnUse(source, source);
            AddToBot(new DamageAction(source, target, this, Damage));
        }

        
    }
}