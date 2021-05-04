using System;
using System.Collections.Generic;
using System.Linq;
using cards;
using cards.weapons;
using characters;


namespace deck {
    public class Hand : Deck {


        public Hand(AbstractCharacter owner) : base(owner) { }

        public Hand(AbstractCharacter owner, List<AbstractCard> deck) : base(owner, deck) { }

        /// <summary>
        /// 往手牌中添加手牌。多余手牌上限将弃牌。
        /// </summary>
        /// <param name="index"></param>
        /// <param name="card"></param>
        public override void Add(int index, AbstractCard card) {
            if (_deck.Count >= 10) {
                if (card is AbstractWeapon) {
                    Owner.ArmoryGrave.Add(card);
                }
                else {
                    Owner.Grave.Add(card);
                }
            } 
            else {
                base.Add(index, card);
                card.OnDraw(Owner, card.Target);
            }
        }

        /// <summary>
        /// 判断手牌中是否有统一Modifier的卡牌
        /// </summary>
        /// <param name="modifier">Modifier</param>
        /// <returns></returns>
        public bool Obtain(AbstractCard.CardModifier modifier) {
            return _deck.Any(card => card.HasModifier(modifier));
        }

        /// <summary>
        /// 判断手牌是否只含有<value>type</value>的卡牌
        /// </summary>
        /// <param name="type">卡牌类型</param>
        /// <returns></returns>
        public bool OnlyObtain(AbstractCard.CardType type) {
            return _deck.All(card => card.Type == type);
        }

        /// <summary>
        /// 判断手牌不含有<value>type</value>的卡牌
        /// </summary>
        /// <param name="type">卡牌类型</param>
        /// <returns></returns>
        public bool NotObtain(AbstractCard.CardType type) {
            return _deck.All(card => !card.HasType(type));
        }

        /// <summary>
        /// 返回手牌数量
        /// </summary>
        /// <returns></returns>
        public int Count() {
            return _deck.Count;
        }

        /// <summary>
        /// 返回可以进行反击的卡
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public List<AbstractCard> ReturnCounterCards(AbstractCard card) {
            return _deck.Where(temp => temp.Damage > card.Damage && temp.HasModifier(card.Modifier)).ToList();
        }

        /// <summary>
        /// 根据上一张打出的牌，改变手牌所有的卡效
        /// </summary>
        /// <param name="card"></param>
        public void Chain(AbstractCard card) {
            
        }

        /// <summary>
        /// 弃掉所有手牌。
        /// </summary>
        public void DiscardAll() {
            _deck.Clear();
        }

        public void RandomDiscard()
        {
            var random = new Random();
            var discardInd = random.Next(_deck.Count);
            Discard(discardInd);
        }
    }
}