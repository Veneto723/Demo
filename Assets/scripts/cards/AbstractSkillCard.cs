using System.Collections.Generic;
using cards;
using characters;
using characters.buffs;

namespace ConsoleApplication.cards {
    public class AbstractSkillCard : AbstractCard {
        public AbstractSkillCard(string name, int baseCost, int baseDamage, int baseMagicNumber, int bonusCost,
            string img, Dictionary<Keyword, int> keywords, CardType type, CardModifier modifier, CardRarity rarity,
            CardTarget target) : base(name, baseCost, bonusCost, baseDamage, baseMagicNumber, img, keywords,
            CardType.Attack, modifier, rarity, target) { }

        public override void OnUse(AbstractCharacter source, AbstractCharacter target) {
            source.DispelBuff(new Combo());
        }
    }
}