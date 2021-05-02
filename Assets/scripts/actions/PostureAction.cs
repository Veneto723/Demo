using cards;
using characters;

namespace actions {
    public class PostureAction : AbstractAction {

        private readonly AbstractCard _sourceCard;
        private readonly int _posture;
        
        public PostureAction(AbstractCharacter source, AbstractCharacter target, AbstractCard card, int posture)
            : base(source, target, card) {
            _sourceCard = card;
            _posture = posture;
        }
        
        public override void OnAct() {
            Target.SetPosture(Source, _sourceCard, _posture);
        }
    }
}