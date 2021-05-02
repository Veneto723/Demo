using System.Collections.Generic;
using Blade.cards.weapons;
using cards;
using cards.weapons;
using characters;

namespace deck {
    public class Hand : Deck {
        public Hand(AbstractCharacter owner) : base(owner) { }
        public Hand(AbstractCharacter owner, List<AbstractCard> deck) : base(owner, deck) { }

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
    }
}