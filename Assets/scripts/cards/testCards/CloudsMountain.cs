using System.Collections.Generic;
using actions;
using characters;
using characters.buffs;

//云瀑山碎
namespace cards.testCards {
    public class CloudMountain: AbstractAttackCard{
        public CloudMountain(AbstractCharacter owner):base("CloudMountain", owner.Weapon.Cost, 2 * owner.Weapon.Damage, 1, 0, 
            "", new Dictionary<Keyword, int>{{Keyword.Deal, 2 * owner.Weapon.Damage}, {Keyword.Stun, 1}}, owner,
            CardModifier.Upper, CardRarity.Starter,  CardTarget.Enemy) { }


        public override bool CanUse(AbstractCharacter source, AbstractCharacter target)
        {
            return source.Hand.OnlyObtain(CardType.Attack);
        }
        
        //只有有攻击牌时才能使用
        public override void OnUse(AbstractCharacter source, AbstractCharacter target) {
            base.OnUse(source, source);
            AddToBot(new DamageAction(source, target, this, Damage));
        }
        
        // Buff: 眩晕
        public override void AfterUse(AbstractCharacter source, AbstractCharacter target) {
            AddToBot(new BuffAction(source, target, new Stun(source, target, MagicNumber),
                this,true)); 
        }
    }
}