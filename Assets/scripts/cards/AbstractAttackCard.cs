using System.Collections.Generic;
using Blade.characters.buffs;
using characters;
using characters.buffs;

namespace cards {
    public class AbstractAttackCard : AbstractCard {
        public AbstractAttackCard(string name, int baseCost, int baseDamage, int baseMagicNumber, int cost, int damage,
            int magicNumber, string img, string description, CardModifier modifier, CardRarity rarity,
            CardTarget target)
            : base(name, baseCost, baseDamage, baseMagicNumber, cost, damage, magicNumber, img, description,
                CardType.Attack, modifier, rarity, target) { }

        protected override void OnUse(AbstractCharacter source, AbstractCharacter target) {
            source.TakeBuff(new Combo(1, source, target));
        }
    }
}