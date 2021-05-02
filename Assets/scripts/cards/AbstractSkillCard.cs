using System.Collections.Generic;
using Blade.cards;
using Blade.characters;
using Blade.characters.buffs;
using cards;
using characters;
using characters.buffs;

namespace ConsoleApplication.cards {
    public class AbstractSkillCard : AbstractCard{
        
        public AbstractSkillCard(string name, int baseCost, int baseDamage, int baseMagicNumber, int cost, int damage, int magicNumber, string img, string description, CardType type, CardModifier modifier, CardRarity rarity, CardTarget target) : base(name, baseCost, baseDamage, baseMagicNumber, cost, damage, magicNumber, img, description, type, modifier, rarity, target) { }
        
        protected override void OnUse(AbstractCharacter source, AbstractCharacter target) {
            source.DispelBuff(new Combo());
        }
}
}