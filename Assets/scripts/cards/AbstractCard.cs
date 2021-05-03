using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using actions;
using characters;
using dungeon;
using utils;

namespace cards {
    public abstract class AbstractCard {

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

        public int BonusCost { get; }

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
        public Dictionary<Keyword, int> Keywords { get; set; }
        public CardType Type { get; }
        public CardModifier Modifier { get; }
        public CardRarity Rarity { get; }
        public CardTarget Target { get; }
        public Chain Chain { get; set; }
        public AbstractCharacter Owner { get; }


        /// <summary>
        /// 攻击类卡牌使用构造函数
        /// </summary>
        protected AbstractCard(string name, int baseCost, int bonusCost, int baseDamage, int baseMagicNumber,
            string img, Dictionary<Keyword, int> keywords, AbstractCharacter owner,
            CardType type, CardModifier modifier, CardRarity rarity, CardTarget target) {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            BaseCost = baseCost;
            BonusCost = bonusCost;
            BaseDamage = baseDamage;
            BaseMagicNumber = baseMagicNumber;
            Cost = BaseCost + BonusCost;
            Damage = BaseDamage;
            MagicNumber = BaseMagicNumber;
            Img = img ?? throw new ArgumentNullException(nameof(img));
            keywords = keywords ?? throw new ArgumentNullException(nameof(keywords));
            Description = DescriptionConstruct(keywords);
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
        public virtual void OnDiscard(AbstractCharacter source, AbstractCharacter target) { }

        /// <summary>
        /// 卡牌使用触发效果，主要用于处理攻击和攻击前施加buff
        /// </summary>
        /// <param name="source">发起方</param>
        /// <param name="target">受影响对象</param>
        public abstract void OnUse(AbstractCharacter source, AbstractCharacter target);

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

        public static string DescriptionConstruct(Dictionary<Keyword, int> dict) {
            var builder = new StringBuilder();
            foreach (var key in dict.Keys) {
                switch (key) {
                    case Keyword.Deal:
                        builder.Append($"造成{dict[key]}点HP。");
                        break;
                    case Keyword.Heal:
                        builder.Append($"恢复{dict[key]}点HP。");
                        break;
                    case Keyword.Bleeding:
                        builder.Append($"施加{dict[key]}层流血。");
                        break;
                    case Keyword.Bravery:
                        builder.Append($"施加{dict[key]}层英勇。");
                        break;
                    case Keyword.Discard:
                        builder.Append($"弃{dict[key]}张牌。");
                        break;
                    case Keyword.Draw:
                        builder.Append($"抽{dict[key]}张牌。");
                        break;
                    case Keyword.Evasion:
                        builder.Append($"施加{dict[key]}层闪避。");
                        break;
                    case Keyword.Immortal:
                        builder.Append("无法被反击。");
                        break;
                    case Keyword.Posture:
                        builder.Append(dict[key] > 0 ? $"额外造成{dict[key]}层躯干值。" : $"额外回复{dict[key]}层躯干值。");
                        break;
                    case Keyword.Stun:
                        builder.Append($"施加{dict[key]}层眩晕。");
                        break;
                    case Keyword.Trance:
                        builder.Append($"施加{dict[key]}层恍惚。");
                        break;
                    case Keyword.Vulnerable:
                        builder.Append($"施加{dict[key]}层脆弱。");
                        break;
                    case Keyword.DefenceDown:
                        builder.Append($"减少{dict[key]}层防御。");
                        break;
                    case Keyword.OpponentCost:
                        builder.Append(dict[key] > 0 ? $"回复对方{dict[key]}点精力。" : $"减少对方{dict[key]}点精力");
                        break;
                    case Keyword.SelfCost:
                        builder.Append(dict[key] > 0 ? $"回复自己{dict[key]}点精力。" : $"减少自己{dict[key]}点精力");
                        break;
                    case Keyword.Unbalanced:
                        builder.Append($"施加{dict[key]}层失衡。");
                        break;
                    case Keyword.Hard:
                        builder.Append($"施加{dict[key]}层坚硬。");
                        break;
                    case Keyword.Penetrate:
                        builder.Append($"贯穿。");
                        break;
                }
            }

            return builder.ToString();
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
            Deal,
            Heal,
            Quick,
            Draw,
            Discard,
            OpponentDiscard,
            SelfCost,
            OpponentCost,
            Posture,
            Immortal,
            Bravery,
            Trance,
            Bleeding,
            Combo,
            DefenceDown,
            Evasion,
            Stun,
            Vulnerable,
            Unbalanced,
            Hard,
            Penetrate
        }
    }
}