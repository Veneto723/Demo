using System;
using cards;
using characters;

namespace actions {
    public class DiscardAction : AbstractAction {
        private readonly int _discard;

        public DiscardAction(AbstractCharacter source, AbstractCharacter target, AbstractCard card, int discard) 
            : base(source, target, card) {
            _discard = discard;
        }
        
        public override void OnAct() {
            for (var i = 0; i < _discard; i++) {
                Target.Hand.RandomDiscard();
            }
        }
    }
}