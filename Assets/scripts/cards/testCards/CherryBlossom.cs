using System.Collections.Generic;
using actions;
using characters;
using characters.buffs;


//落樱
namespace cards.testCards {
    public class CherryBlossom : AbstractAttackCard {
        public CherryBlossom(AbstractCharacter owner) : base("CherryBlossom", owner.Weapon.Cost, owner.Weapon.Damage,
            owner.Weapon.Damage, 1,
            "", new Dictionary<Keyword, int> {{Keyword.Deal, owner.Weapon.Damage},
                {Keyword.DefenceDown, owner.Weapon.Damage}}, owner,
            CardModifier.Upper, CardRarity.Starter, CardTarget.Enemy) { }

        public override void OnUse(AbstractCharacter source, AbstractCharacter target) {
            base.OnUse(source, source);
            AddToBot(new DamageAction(source, target, this, Damage));
        }

        //Buff: 破甲
        public override void AfterUse(AbstractCharacter source, AbstractCharacter target) {
            AddToBot(new BuffAction(source, target, new DefenceDown(source, target, MagicNumber),
                this, true));
        }
    }
}