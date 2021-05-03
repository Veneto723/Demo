using Blade.cards.weapons;
using characters;
using utils;

namespace cards.weapons {
    public class AbstractWeapon : AbstractCard {

        private int _baseDefence;
        private int _defence;
        protected AbstractUnit Pommel;
        protected AbstractUnit Blade;
        public AbstractUnit Grip { get; protected set; }
        protected AbstractUnit Guard;

        protected int BaseDefence {
            get => _baseDefence;
            set => _baseDefence = Utils.NotNegative(value);
        }

        public int Defence {
            get => _defence;
            protected set => _defence = Utils.NotNegative(value);
        }
        
        public int CostRecovery { get; set; }

        public AbstractWeapon(string img, string description) : base(img, description) {
            
        }

        public void Init() {
            Name = GetWeaponName();
            BaseCost = GetBaseCost();
            Cost = BaseCost;
            BaseDamage = GetDamage();
            Damage = BaseDamage;
            BaseDefence = GetDefence();
            Defence = BaseDefence;
            CostRecovery = Grip.CostRecovery;
        }

        /// <summary>
        /// 计算武器总消耗。
        /// </summary>
        /// <returns>武器消耗</returns>
        protected int GetBaseCost() {
            var sumWeight = (double) Grip.Weight + Guard.Weight + Blade.Weight + Pommel.Weight;
            return (int) Utils.NotNegative(Utils.Round(sumWeight / 10));
        }

        /// <summary>
        /// 获取武器总伤害。
        /// </summary>
        /// <returns>武器伤害</returns>
        protected int GetDamage() {
            return Utils.NotNegative(Blade.Damage);
        }

        /// <summary>
        /// 获取武器总防御。
        /// </summary>
        /// <returns>武器防御</returns>
        protected int GetDefence() {
            return Grip.Defence + Blade.Defence + Pommel.Defence + Guard.Defence;
        }


        /// <summary>
        /// TODO 交换武器配件。
        /// </summary>
        /// <param name="unit">武器配件</param>
        /// <param name="index">配件下标</param>
        public void SwitchUnit(AbstractUnit unit, int index) {
            
        }

        /// <summary>
        /// TODO 通过握把和刀刃判断武器名。
        /// </summary>
        /// <returns>武器名</returns>
        public static string GetWeaponName() {
            return "";
        }
        

        /// <summary>
        /// 打出武器牌，装备武器。并将原有弃入墓地。
        /// </summary>
        /// <param name="source">发起方</param>
        /// <param name="target">受影响对象</param>
        public override void OnUse(AbstractCharacter source, AbstractCharacter target) {
            source.UnEquip(source.Weapon);
            source.Equip(this);
        }

        public override string ToString() {
            return $"#{Name}[{Rarity}] {BaseCost}C {CostRecovery}CR {Damage}Damage {Defence}Defence";
        }
    }
}