using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using cards;

namespace characters {
    public class AbstractMonster : AbstractCharacter {
        // TODO unfinished

        /// <summary>
        /// 选择进行Counter的卡牌。
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public AbstractCard ChooseCounterCard(AbstractCard card) {
            var cards = Hand.ReturnCounterCards(card);
            var returnCard = cards[0];
            foreach (var current in cards.Where(current => current.Damage < returnCard.Damage)) {
                returnCard = current;
            }

            return returnCard;
        }

    }
}