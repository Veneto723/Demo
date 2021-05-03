using System;
using System.Collections.Generic;
using System.Linq;
using cards;
using cards.weapons;
using characters.buffs;
using deck;
using utils;

namespace characters {
    public abstract class AbstractCharacter {

        private int _hitPoint;
        private int _currentCost;
        private const int MaxPosture = 10;
        private const int TotalCost = 10;

        public int HitPoint {
            get => _hitPoint;
            set {
                _hitPoint = Utils.NotNegative(value);
                if (_hitPoint == 0) Dying();
            }
        }

        public int Defence { get; set; }

        public int Cost {
            get => _currentCost;
            set => _currentCost = Utils.NotNegative(value);
        }

        public int Posture { get; private set; }

        public void SetPosture(AbstractCharacter source, AbstractCard sourceCard, int amount) {
            Posture = Utils.NotNegative(amount + Posture);
            if (Posture >= MaxPosture) {
                TriggerPosture(source, sourceCard);
                Posture = 0;
            }
        }

        public string Img { get; set; }

        public Hand Hand { get; }
        public Deck Deck { get; }
        public Grave Grave { get; }
        public ArmoryDeck ArmoryDeck { get; }
        public ArmoryGrave ArmoryGrave { get; }
        public AbstractWeapon Weapon { get; set; }
        public List<Buff> Buffs { get; }


        public AbstractCharacter() {
            HitPoint = 100;
            Defence = 3;
            Hand = new Hand(this);
            Deck = new Deck(this);
            Grave = new Grave(this);
            ArmoryDeck = new ArmoryDeck(this);
            ArmoryGrave = new ArmoryGrave(this);
            Buffs = new List<Buff>();
        }

        public AbstractCharacter(int hitPoint) {
            HitPoint = hitPoint;
            Defence = 3;
            Hand = new Hand(this);
            Deck = new Deck(this);
            Grave = new Grave(this);
            ArmoryDeck = new ArmoryDeck(this);
            ArmoryGrave = new ArmoryGrave(this);
            Buffs = new List<Buff>();
        }


        /// <summary>
        /// 添加Buff。
        /// </summary>
        /// <param name="buff">授予Buff</param>
        public virtual void TakeBuff(Buff buff) {
            var hasBuff = false;
            var isStance = buff.Type == Buff.BuffType.Stance;
            if (isStance) {
                foreach (var currentBuff in Buffs.Where(currentBuff => currentBuff.Type == Buff.BuffType.Stance)) {
                    DispelBuff(currentBuff);
                    break;
                }
            }
            else {
                foreach (var currentBuff in Buffs.Where(currentBuff => currentBuff.Equals(buff))) {
                    hasBuff = true;
                    currentBuff.Escalate(buff.Source, this, buff.GetAmount(), buff.GetDuration());
                    break;
                }
            }

            if (!hasBuff) {
                Buffs.Add(buff);
                if (buff is DefenceDown) {
                    Defence -= buff.GetAmount();
                }
            }
        }

        /// <summary>
        /// 移除Buff。
        /// </summary>
        /// <param name="buff">待移除Buff</param>
        public virtual void DispelBuff(Buff buff) {
            foreach (var currentBuff in Buffs.Where(currentBuff => currentBuff.Equals(buff))) {
                Buffs.Remove(currentBuff);
            }
        }

        /// <summary>
        /// 角色受击方法。内会检测伤害数值。
        /// </summary>
        /// <param name="damage">将要受击数值</param>
        /// <param name="defenceIgnore">护甲忽视</param>
        public virtual void TakeDamage(int damage, int defenceIgnore) {
            // 受击动画
            // 扣血 TODO 伤害减益百分比
            // 防御力为正，则扣除破甲数值。防御力为负，则无视破甲。
            var tempDefence = Weapon.Defence + Defence;
            var finalDefence = tempDefence > 0 ? Utils.NotNegative(tempDefence - defenceIgnore) : tempDefence;
            HitPoint -= Utils.NotNegative(damage - finalDefence);
        }

        /// <summary>
        /// 角色承受真实伤害，忽视护甲。内会检测伤害数值是否。
        /// </summary>
        /// <param name="damage">将要受击数值</param>
        public virtual void TakeRealDamage(int damage) {
            // 受击动画
            // 扣血 
            HitPoint -= Utils.NotNegative(damage);
        }

        /// <summary>
        /// 计算造成伤害百分比。
        /// </summary>
        /// <returns>增幅百分比</returns>
        public virtual int DealDamagePercent() {
            var percent = 0;
            foreach (var currentBuff in Buffs) {
                switch (currentBuff) {
                    case Bravery _:
                        percent += 20;
                        break;
                    case Trance _:
                        percent -= 15;
                        break;
                }
            }

            return percent;
        }
        
        /// <summary>
        /// 计算受到伤害百分比。
        /// </summary>
        /// <returns>增幅百分比</returns>
        public virtual int TakeDamagePercent() {
            var percent = 0;
            foreach (var currentBuff in Buffs) {
                switch (currentBuff) {
                    case Vulnerable _:
                        percent += 15;
                        break;
                }
            }

            return percent;
        }

        /// <summary>
        /// 开局回费。
        /// </summary>
        public virtual void CostRecover() {
            if (Weapon == null) return;
            _currentCost += Weapon.Grip.CostRecovery;
            _currentCost = Utils.Below(_currentCost, TotalCost);
        }

        /// <summary>
        /// 判断角色是否可操作
        /// </summary>
        /// <returns></returns>
        public bool CanMove() {
            return !Buffs.OfType<Stun>().Any();
        }


        /// <summary>
        /// 处理角色死亡
        /// </summary>
        public virtual bool Dying() {
            return HitPoint <= 0;
        }

        /// <summary>
        /// 根据触发的卡牌类型，决定触发效果。
        /// </summary>
        /// <param name="source">卡牌来源</param>
        /// <param name="sourceCard">打出的卡牌</param>
        public virtual void TriggerPosture(AbstractCharacter source, AbstractCard sourceCard) {
            _currentCost -= 2;
            Hand.DiscardAll();
        }

        /// <summary>
        /// 判断是否可以进行反击。
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public bool CanCounter(AbstractCard card) {
            return Hand.Obtain(card.Modifier);
        }

        /// <summary>
        /// 装备武器。
        /// </summary>
        /// <param name="weapon">武器</param>
        /// <exception cref="ArgumentNullException">武器变量为空</exception>
        public void Equip(AbstractWeapon weapon) {
            Weapon = weapon ?? throw new ArgumentNullException();
        }

        /// <summary>
        /// 卸下武器。
        /// </summary>
        /// <param name="weapon">武器</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void UnEquip(AbstractWeapon weapon) {
            ArmoryGrave.Add(weapon ?? throw new ArgumentNullException());
        }
    }
}