using System.Collections.Generic;
using actions;
using characters;
using characters.buffs;

//锋芒
namespace cards.testCards {
    public class Edge: AbstractAttackCard{
        public Edge(AbstractCharacter owner):base("Edge", owner.Weapon.Cost, owner.Weapon.Damage, 
            owner.Weapon.Damage, 1, "", 
            new Dictionary<Keyword, int>{{Keyword.Deal, owner.Weapon.Damage}, {Keyword.Bleeding, owner.Weapon.Damage}}, 
            owner, CardModifier.Dash, CardRarity.Starter,  CardTarget.Enemy) { } 
        
        public override void OnUse(AbstractCharacter source, AbstractCharacter target) {
            base.OnUse(source, source);
            AddToBot(new DamageAction(source, target, this, Damage));
        }
        
        //流血Buff
        public override void AfterUse(AbstractCharacter source, AbstractCharacter target) {
            AddToBot(new BuffAction(source, target, new Bleeding(source, target, MagicNumber), this,true));  
        }
    }
}