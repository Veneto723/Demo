using System;
using utils;

namespace characters.buffs {
    public abstract class Buff {

        public const int InfiniteDuration = -1;
        
        public String Name { get; } // Buff名
        public String Description { get; } // Buff描述
        public BuffType Type { get; } // Buff类型
        protected int Amount; // Buff层数
        protected int Duration; // Buff持续时间
        public AbstractCharacter Source { get; } // Buff给予者
        public AbstractCharacter Target { get; } // Buff接受者

        public int GetAmount() {
            return Amount;
        }

        public int GetDuration() {
            return Duration;
        }

        /// <summary>
        /// Default 构造函数
        /// </summary>
        protected Buff() { }

        /// <summary>
        /// Buff构造方法，供子类使用。
        /// </summary>
        /// <param name="name">Buff名</param>
        /// <param name="description">Buff描述</param>
        /// <param name="type">Buff类型</param>
        /// <param name="amount">层数</param>
        /// <param name="duration">持续时间</param>
        /// <param name="source">授予者</param>
        /// <param name="target">接受者</param>
        /// <exception cref="ArgumentNullException"></exception>
        protected Buff(string name, string description, BuffType type, int amount, int duration,
            AbstractCharacter source, AbstractCharacter target) {
            Name = name;
            Description = description;
            Type = type;
            Amount = Utils.NotNegative(amount);
            Duration = duration;
            Source = source ?? throw new ArgumentNullException(nameof(source));
            Target = target ?? throw new ArgumentNullException(nameof(target));
        }

        /// <summary>
        /// Buff触发方法。
        /// </summary>
        /// <param name="source">授予者</param>
        /// <param name="target">接受者</param>
        public abstract void OnUse(AbstractCharacter source, AbstractCharacter target);

        /// <summary>
        /// Buff减少层数方法。
        /// </summary>
        /// <param name="source">授予者</param>
        /// <param name="target">接受者</param>
        /// <param name="amount">减少层数</param>
        public virtual void Alleviate(AbstractCharacter source, AbstractCharacter target, int amount) {
            Amount = Utils.NotNegative(Amount - amount);
            if (Amount == 0) {
                Purge(target);
            }
        }

        /// <summary>
        /// Buff增加层数方法。
        /// </summary>
        /// <param name="source">授予者</param>
        /// <param name="target">接受者</param>
        /// <param name="amount">增加层数</param>
        /// <param name="duration">增加时间</param>
        public abstract void Escalate(AbstractCharacter source, AbstractCharacter target, int amount, int duration);

        /// <summary>
        /// Buff倒计时。
        /// </summary>
        public virtual void CountDown() {
            Duration = Utils.NotNegative(Duration - 1);
            if (Duration == 0) {
                OnUse(Source, Target);
            }
        }

        /// <summary>
        /// 从<code>target</code>身上清除该Buff。
        /// </summary>
        /// <param name="target">Buff持有者</param>
        public void Purge(AbstractCharacter target) {
            target.DispelBuff(this);
        }

        public override bool Equals(object obj) {
            if (obj is Buff otherBuff) {
                return Name.Equals(otherBuff.Name);
            }

            return false;
        }

        public override string ToString() {
            return $"{Name} Duration: {Duration}, Amount: {Amount}";
        }

        public enum BuffType {
            Buff,
            Debuff,
            Stance // 姿态
        }
    }
}