using System.Collections.Generic;
using actions;
using characters;
using deck;

//玉碎
namespace cards.testCards {
    public class JadePieces: AbstractAttackCard{
        public JadePieces(AbstractCharacter owner):base("JadePieces", 0, 2 * owner.Weapon.Damage, 0, 0, 
            "", new Dictionary<Keyword, int>{{Keyword.Deal, 2 * owner.Weapon.Damage}}, owner,
            CardModifier.Middle, CardRarity.Starter,  CardTarget.Enemy) { } 
        
        // 手牌中只有攻击牌时才能使用
        public override bool CanUse(AbstractCharacter source, AbstractCharacter target) {
            
            return source.Hand.OnlyObtain(CardType.Attack);
        }
        
        public override void OnUse(AbstractCharacter source, AbstractCharacter target) {
            base.OnUse(source, source);
            AddToBot(new DamageAction(source, target, this, Damage));
        }
    }
}