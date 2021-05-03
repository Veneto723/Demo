using System.Collections.Generic;
using characters;
using characters.buffs;

namespace cards {
    public class AbstractAttackCard : AbstractCard {
        public AbstractAttackCard(string name, int baseCost, int baseDamage, int baseMagicNumber, int bonusCost,
            string img, Dictionary<Keyword, int> keywords, CardModifier modifier, CardRarity rarity,
            CardTarget target)
            : base(name, baseCost, bonusCost, baseDamage, baseMagicNumber, img, keywords,
                CardType.Attack, modifier, rarity, target) { }

        public override void OnUse(AbstractCharacter source, AbstractCharacter target) {
            source.TakeBuff(new Combo(1, source, target));
        }
    }
}