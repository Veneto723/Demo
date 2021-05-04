using System.Collections.Generic;
using actions;
using characters;

//流云
namespace cards.testCards {
    public class FlowingClouds: AbstractAttackCard{
        public FlowingClouds(AbstractCharacter owner):base("FlowingClouds", owner.Weapon.Cost, owner.Weapon.Damage, 1, 1, 
            "", new Dictionary<Keyword, int>{{Keyword.Deal, owner.Weapon.Damage}, {Keyword.Draw, 1}}, owner,
            CardModifier.Dash, CardRarity.Starter,  CardTarget.Enemy) { } 
        
        public override void OnUse(AbstractCharacter source, AbstractCharacter target) {
            base.OnUse(source, source);
            AddToBot(new DamageAction(source, target, this, Damage));
        }
        public override void AfterUse(AbstractCharacter source, AbstractCharacter target) {
            AddToBot(new DrawAction(source, source, this, MagicNumber)); 
        }
    }
}