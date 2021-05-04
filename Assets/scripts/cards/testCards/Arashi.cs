using System.Collections.Generic;
using actions;
using characters;

//岚
namespace cards.testCards {
    public class Arashi: AbstractAttackCard{
        public Arashi(AbstractCharacter owner):base("岚", owner.Weapon.Cost, owner.Weapon.Damage, 1, 1, 
            "", new Dictionary<Keyword, int>{{Keyword.Deal, owner.Weapon.Damage}, {Keyword.OpponentDiscard, 1}}, owner,
            CardModifier.Lower, CardRarity.Starter,  CardTarget.Enemy) { } 
        
        public override void OnUse(AbstractCharacter source, AbstractCharacter target) {
            base.OnUse(source, source);
            AddToBot(new DamageAction(source, target, this, Damage));
        }
        
        //弃掉对手一张手牌
        public override void AfterUse(AbstractCharacter source, AbstractCharacter target) {
            AddToBot(new DiscardAction(source, target,this, MagicNumber)); 
        }
    }
}