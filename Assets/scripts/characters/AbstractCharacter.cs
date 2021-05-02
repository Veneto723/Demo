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
        // TODO coarse class, need to be refined

        private int _hitPoint;
        private int CurrentCost;
        private const int MaxPosture = 10;
        private const int TotalCost = 10;

        public int HitPoint {
            get => _hitPoint;
            set {
                _hitPoint = Utils.NotNegative(value);
                if (_hitPoint == 0) Dying();
            }
        }

        public int Cost {
            get => CurrentCost;
            set => CurrentCost = Utils.NotNegative(value);
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
            Hand = new Hand(this);
            Deck = new Deck(this);
            Grave = new Grave(this);
            ArmoryDeck = new ArmoryDeck(this);
            ArmoryGrave = new ArmoryGrave(this);
            Buffs = new List<Buff>();
        }

        public AbstractCharacter(int hitPoint) {
            HitPoint = hitPoint;
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
            var finalDefence = Weapon.Defence > 0 ? Utils.NotNegative(Weapon.Defence - defenceIgnore) : Weapon.Defence;

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
        /// 处理角色死亡
        /// </summary>
        public virtual void Dying() {
            // TODO unfinished
        }

        public virtual void TriggerPosture(AbstractCharacter source, AbstractCard sourceCard) {
            // TODO unfinished
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