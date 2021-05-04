using System.Collections.Generic;
using actions;
using characters;

//居合
namespace cards.testCards {
    public class Laido: AbstractAttackCard{
        public Laido(AbstractCharacter owner):base("Laido", 2, owner.Weapon.Damage, 1, 0, 
            "", new Dictionary<Keyword, int>{{Keyword.Deal, owner.Weapon.Damage}, {Keyword.Draw, 1}}, owner,
            CardModifier.Middle, CardRarity.Starter,  CardTarget.Enemy) { } 
        
        
        // TODO 装备手牌中武器的伤害再打出去 Unfinished
        public override void OnUse(AbstractCharacter source, AbstractCharacter target) {
            // AddToBot(new DrawWeaponAction(source, source, this, MagicNumber));
            //
            // base.OnUse(source, source);
            // AddToBot(new DamageAction(source, target, this, Damage));
        }
    }
}