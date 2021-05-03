using System;
using System.Collections.Generic;
using System.Linq;
using actions;
using characters;
using dungeon;
using utils;

namespace cards {
    public abstract class AbstractCard {

        protected const int Depends = -1; 

        private int _baseCost;
        private int _baseDamage;
        private int _baseHeal;
        private int _cost;
        private int _damage;
        private int _heal;

        public string Name { get; set; }

        public int BaseCost {
            get => _baseCost;
            set => _baseCost = Utils.NotNegative(value);
        }

        public int BaseDamage {
            get => _baseDamage;
            set => _baseDamage = Utils.NotNegative(value);
        }

        public int BaseHeal {
            get => _baseHeal;
            set => _baseHeal = Utils.NotNegative(value);
        }

        public int BaseMagicNumber { get; }

        public int Cost {
            get => _cost;
            set => _cost = Utils.NotNegative(value);
        }

        public int Damage {
            get => _damage;
            set => _damage = Utils.NotNegative(value);
        }

        public int Heal {
            get => _heal;
            set => _heal = Utils.NotNegative(value);
        }

        public int MagicNumber { get; }
        public string Img { get; }
        public string Description { get; }
        public CardType Type { get; }
        public CardModifier Modifier { get; }
        public CardRarity Rarity { get; }
        public CardTarget Target { get; }


        /// <summary>
        /// 攻击类卡牌使用构造函数
        /// </summary>
        protected AbstractCard(string name, int baseCost, int baseDamage, int baseMagicNumber, int cost,
            int damage, int magicNumber, string img, string description, CardType type, CardModifier modifier,
            CardRarity rarity, CardTarget target) {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            BaseCost = baseCost;
            BaseDamage = baseDamage;
            BaseMagicNumber = baseMagicNumber;
            Cost = cost;
            Damage = damage;
            MagicNumber = magicNumber;
            Img = img ?? throw new ArgumentNullException(nameof(img));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Type = type;
            Modifier = modifier;
            Rarity = rarity;
            Target = target;
        }

        /// <summary>
        /// 武器类卡牌使用构造函数
        /// </summary>
        protected AbstractCard(string img, string description) {
            Img = img ?? throw new ArgumentNullException(nameof(img));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Type = CardType.Weapon;
        }


        /// <summary>
        /// 卡牌抽上触发效果
        /// </summary>
        /// <param name="source">发起方</param>
        /// <param name="target">受影响对象</param>
        public virtual void OnDraw(AbstractCharacter source, CardTarget target) { }

        /// <summary>
        /// 弃牌触发效果
        /// </summary>
        /// <param name="source">发起方</param>
        /// <param name="target">受影响对象</param>
        protected virtual void OnDiscard(AbstractCharacter source, AbstractCharacter target) { }

        /// <summary>
        /// 卡牌使用触发效果，主要用于处理攻击和攻击前施加buff
        /// </summary>
        /// <param name="source">发起方</param>
        /// <param name="target">受影响对象</param>
        protected abstract void OnUse(AbstractCharacter source, AbstractCharacter target);

        /// <summary>
        /// 卡牌使用触后发效果，用于处理卡牌造成伤害后效果，例如抽牌、施加debuff
        /// </summary>
        /// <param name="source">发起方</param>
        /// <param name="target">受影响对象</param>
        public virtual void AfterUse(AbstractCharacter source, AbstractCharacter target) { }

        /// <summary>
        /// 卡牌送墓触发效果
        /// </summary>
        /// <param name="source">发起方</param>
        /// <param name="target">受影响对象</param>
        public virtual void OnGrave(AbstractCharacter source, CardTarget target) { }

        /// <summary>
        /// 判断这张卡是否可以打出
        /// </summary>
        /// <param name="source">发起方</param>
        /// <param name="target">受影响对象</param>
        /// <returns></returns>
        public virtual bool CanUse(AbstractCharacter source, AbstractCharacter target) {
            return Cost <= source.Cost && source.CanMove();
        }

        public bool HasModifier(IEnumerable<CardModifier> modifiers) {
            return modifiers.Any(modifier => modifier == Modifier);
        }

        /// <summary>
        /// 判断卡牌是否含有该Modifier
        /// </summary>
        /// <param name="modifier">modifier</param>
        /// <returns></returns>
        public bool HasModifier(CardModifier modifier) {
            return Modifier == modifier;
        }

        /// <summary>
        /// 判断该卡牌是否包含<code>types</code>中的相应类型。
        /// </summary>
        /// <param name="types">卡牌类型集</param>
        /// <returns>是否包含</returns>
        public bool HasType(IEnumerable<CardType> types) {
            return types.Any(type => type == Type);
        }

        /// <summary>
        /// 添加行为至ActionManager队列尾。
        /// </summary>
        /// <param name="action">行为</param>
        protected void AddToBot(AbstractAction action) {
            AbstractDungeon.ActionManager.AddToBottom(action);
        }

        /// <summary>
        /// 添加行为至ActionManager队列首。
        /// </summary>
        /// <param name="action">行为</param>
        protected void AddToTop(AbstractAction action) {
            AbstractDungeon.ActionManager.AddToTop(action);
        }

        public override bool Equals(object obj) {
            if (obj is AbstractCard otherCard) {
                return Name.Equals(otherCard.Name);
            }

            return false;
        }

        public override string ToString() {
            return $"#{Name}[{Rarity}] {BaseCost}C => {Description}\n";
        }
        
        
        public string DescriptionConstruct(Dictionary<Keyword, int> dict) {
            
            return "";
        }


        public enum CardType {
            Attack,
            Defence,
            Heal,
            Weapon

            // TODO 其余分类
        }

        public enum CardModifier {
            // TODO 其他修饰
            Upper,
            Middle,
            Lower,
            Dash
        }

        public enum CardRarity {
            Starter,
            Default,
            Normal,
            Rare,
            UltraRare
        }

        public enum CardTarget {
            Self, // 自己
            Both, // 自己&敌人
            Enemy, // 单个敌人
            Multi, // 多敌人
            All // 全体敌人
        }

        public enum Keyword {
            
        }
    }
}