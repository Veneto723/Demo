using System.Collections.Generic;

namespace cards.weapons {
    public class AbstractUnit {
        public int Damage { get; }
        public int Defence { get; }

        public int CostRecovery { get; }
        public int Weight { get; }
        protected WeaponUnit Unit { get; }
        protected List<AbstractCard.CardModifier> ModifierRange { get; }
        protected List<AbstractCard.CardTarget> TargetRange { get; }
        protected AbstractCard.CardRarity Rarity { get; }

        public AbstractUnit(int damage, int defence, int weight, WeaponUnit weaponUnit,
            AbstractCard.CardRarity rarity) {
            Damage = damage;
            Defence = defence;
            Weight = weight;
            Unit = weaponUnit;
            ModifierRange = new List<AbstractCard.CardModifier>();
            TargetRange = new List<AbstractCard.CardTarget>();
            Rarity = rarity;
        }
        
        public AbstractUnit(int damage, int defence, int costRecovery, int weight, WeaponUnit weaponUnit,
            AbstractCard.CardRarity rarity) {
            Damage = damage;
            Defence = defence;
            CostRecovery = costRecovery;
            Weight = weight;
            Unit = weaponUnit;
            ModifierRange = new List<AbstractCard.CardModifier>();
            TargetRange = new List<AbstractCard.CardTarget>();
            Rarity = rarity;
        }

        public AbstractUnit(int damage, int defence, int weight, WeaponUnit weaponUnit,
            List<AbstractCard.CardModifier> modifierRange, List<AbstractCard.CardTarget> targetRange,
            AbstractCard.CardRarity rarity)
            : this(damage, defence, weight, weaponUnit, rarity) {
            ModifierRange = modifierRange;
            TargetRange = targetRange;
        }

        public enum WeaponUnit {
            Pommel, // 配重 补伤害
            Grip, // 握把 回费
            Blade, // 刀刃 面板伤害
            Guard // 护手 防御
        }
    }
}