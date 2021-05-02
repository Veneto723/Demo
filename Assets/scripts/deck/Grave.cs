using System.Collections.Generic;
using System.Text;
using cards;
using characters;

namespace deck {
    public class Grave : Deck{
        public Grave(AbstractCharacter owner) : base(owner) { }
        public Grave(AbstractCharacter owner, List<AbstractCard> deck) : base(owner, deck) { }

        /// <summary>
        /// 将墓地洗回牌库，并将牌库进行洗牌
        /// </summary>
        /// <param name="deck">牌库</param>
        public void ShuffleToDeck(Deck deck) {
            deck.Add(_deck);
            _deck.Clear();
            deck.Shuffle();
        }

        /// <summary>
        /// 墓地追加方法。并触发进墓效果。
        /// </summary>
        /// <param name="index">下标</param>
        /// <param name="card">待添加卡牌</param>
        public override void Add(int index, AbstractCard card) {
            base.Add(index, card);
            card.OnGrave(Owner, card.Target);
        }

        public override string ToString() {
            var builder = new StringBuilder($"墓地[#Card:{_deck.Count}]");
            foreach (var card in _deck) {
                builder.Append("\n").Append(card);
            }
            return builder.ToString();
        }
    }
}