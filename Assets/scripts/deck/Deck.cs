using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cards;
using characters;
using exceptions;

namespace deck {
    public class Deck {
        public AbstractCharacter Owner { get; set; } // 持有者
        protected readonly List<AbstractCard> _deck; // 牌库数组
        private readonly Random _random = new Random(); // 用于洗牌的随机数

        /// <summary>
        /// 牌库默认构造方法，将创建一个持有者为<code>owner</code>的空牌库。
        /// </summary>
        /// <param name="owner">牌库持有者</param>
        /// <seealso cref="AbstractCharacter"/>
        public Deck(AbstractCharacter owner) {
            Owner = owner ?? throw new ArgumentNullException(nameof(owner));
            _deck = new List<AbstractCard>();
        }

        /// <summary>
        /// 牌库构造方法，将创建一个持有者为<code>owner</code>的<code>deck</code>牌库。
        /// </summary>
        /// <param name="owner">持有者</param>
        /// <param name="deck">牌库集合</param>
        /// <seealso cref="AbstractCharacter"/> 
        /// <seealso cref="AbstractCard"/>
        public Deck(AbstractCharacter owner, List<AbstractCard> deck) {
            Owner = owner ?? throw new ArgumentNullException(nameof(owner));
            _deck = deck ?? throw new ArgumentNullException(nameof(deck));
        }

        /// <summary>
        /// 从牌库顶抽一张牌。
        /// </summary>
        /// <returns>AbstractCard</returns>
        /// <exception cref="EmptyDeckException">牌库为空异常。因为并不决定让牌库绑定墓地，
        /// 所以牌库无权调用墓地的洗回牌库方法。所以要留给上层的调用方法处理异常。</exception>
        /// <seealso cref="AbstractCard"/>
        public AbstractCard Draw() {
            if (_deck.Count <= 0) throw new EmptyDeckException();
            var abstractCard = _deck[0];
            _deck.RemoveAt(0);
            return abstractCard;
        }

        /// <summary>
        /// 从牌库抽指定下标的Card。
        /// </summary>
        /// <param name="index">卡牌下标</param>
        /// <returns>AbstractCard</returns>
        /// <exception cref="EmptyDeckException">牌库为空异常。因为并不决定让牌库绑定墓地，
        /// 所以牌库无权调用墓地的洗回牌库方法。所以要留给上层的调用方法处理异常。</exception>
        /// <seealso cref="AbstractCard"/>
        public AbstractCard Draw(int index) {
            if (_deck.Count <= 0) throw new EmptyDeckException();
            var abstractCard = _deck[index];
            _deck.RemoveAt(index);
            return abstractCard;
        }

        /// <summary>
        /// 从牌库定向检索第一张名字相同的牌。
        /// </summary>
        /// <param name="abstractCard">要抽取的牌</param>
        /// <returns>AbstractCard</returns>
        /// <exception cref="EmptyDeckException">牌库为空异常。因为并不决定让牌库绑定墓地，
        /// 所以牌库无权调用墓地的洗回牌库方法。所以要留给上层的调用方法处理异常。</exception>
        /// <exception cref="ArgumentNullException">检索条件为空异常</exception>
        /// <seealso cref="AbstractCard"/>
        public AbstractCard Draw(AbstractCard abstractCard) {
            if (_deck.Count <= 0) throw new EmptyDeckException();
            var index = _deck.IndexOf(abstractCard ?? throw new ArgumentException(nameof(abstractCard)));
            _deck.RemoveAt(index);
            return abstractCard;
        }

        /// <summary>
        /// 从牌库定向检索第一张带有<code>modifiers</code>的卡牌。
        /// </summary>
        /// <param name="modifiers">卡牌修饰关键词集合</param>
        /// <param name="logic">逻辑修饰符</param>
        /// <returns>第一张符合条件卡牌</returns>
        /// <exception cref="EmptyDeckException">牌库为空异常。因为并不决定让牌库绑定墓地，
        /// 所以牌库无权调用墓地的洗回牌库方法。所以要留给上层的调用方法处理异常。</exception>
        /// <exception cref="ArgumentNullException">检索条件为空异常</exception>
        /// <seealso cref="AbstractCard"/>
        /// <seealso cref="AbstractCard.CardModifier"/>
        public AbstractCard Draw(List<AbstractCard.CardModifier> modifiers) {
            if (_deck.Count <= 0) throw new EmptyDeckException();
            foreach (var card in _deck.Where(card => card.HasModifier(modifiers))) {
                _deck.Remove(card);
                return card;
            }

            return null;
        }

        /// <summary>
        /// 从牌库定向检索第一张带有<code>types</code>的卡牌。
        /// </summary>
        /// <param name="types">卡牌类型集</param>
        /// <returns>第一张符合条件卡牌</returns>
        /// <exception cref="EmptyDeckException">牌库为空异常。因为并不决定让牌库绑定墓地，
        /// 所以牌库无权调用墓地的洗回牌库方法。所以要留给上层的调用方法处理异常。</exception>
        /// <exception cref="ArgumentNullException">检索条件为空异常</exception>
        /// <seealso cref="AbstractCard"/>
        /// <seealso cref="AbstractCard.CardType"/>
        public AbstractCard Draw(List<AbstractCard.CardType> types) {
            if (_deck.Count <= 0) throw new EmptyDeckException();
            foreach (var card in _deck.Where(card =>
                card.HasType(types ?? throw new ArgumentNullException(nameof(types))))) {
                _deck.Remove(card);
                return card;
            }

            return null;
        }

        /// <summary>
        /// 洗牌库。
        /// </summary>
        public void Shuffle() {
            for (var i = _deck.Count - 1; i > 0; i--) {
                var swapTo = _random.Next(i + 1);
                var temp = _deck[i];
                _deck[i] = _deck[swapTo];
                _deck[swapTo] = temp;
            }
        }

        /// <summary>
        /// 牌库删除指定下标卡牌。
        /// </summary>
        /// <param name="index">待删除卡牌下标</param>
        public void Discard(int index) {
            _deck.RemoveAt(index);
        }


        /// <summary>
        /// 牌库指定位置追加<code>card</code>。
        /// </summary>
        /// <param name="index">下标</param>
        /// <param name="card">待添加卡牌</param>
        /// <seealso cref="AbstractCard"/>
        public virtual void Add(int index, AbstractCard card) {
            _deck.Insert(index, card);
        }

        /// <summary>
        /// 牌库尾部追加<code>card</code>。
        /// </summary>
        /// <param name="abstractCard">待添加卡牌</param>
        /// <seealso cref="AbstractCard"/>
        public void Add(AbstractCard abstractCard) {
            Add(_deck.Count, abstractCard);
        }

        /// <summary>
        /// 牌库尾部添加多张卡牌。
        /// </summary>
        /// <param name="cards"></param>
        public void Add(List<AbstractCard> cards) {
            foreach (var card in cards ?? throw new ArgumentNullException(nameof(cards))) {
                _deck.Add(card);
            }
        }

        public override string ToString() {
            var builder = new StringBuilder($"牌库[#Card:{_deck.Count}]");
            foreach (var card in _deck) {
                builder.Append("\n").Append(card);
            }

            return builder.ToString();
        }
    }
}