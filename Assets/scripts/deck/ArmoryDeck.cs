using System;
using System.Collections.Generic;
using Blade.cards.weapons;
using cards.weapons;
using characters;

namespace deck {
    public class ArmoryDeck : Deck {

        protected readonly List<AbstractWeapon> _armoryDeck;
        
        public ArmoryDeck(AbstractCharacter owner) : base(owner) { }

        public ArmoryDeck(AbstractCharacter owner, List<AbstractWeapon> armoryDeck) : base(owner) {
            _armoryDeck = armoryDeck;
        }

        /// <summary>
        /// 牌库尾部添加多张卡牌。
        /// </summary>
        /// <param name="cards">卡牌</param>
        public void Add(List<AbstractWeapon> cards) {
            _armoryDeck.AddRange(cards ?? throw new ArgumentNullException(nameof(cards)));
        }
    }
}