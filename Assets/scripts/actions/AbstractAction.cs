using cards;
using characters;

namespace actions {
    public abstract class AbstractAction {
        protected AbstractCharacter Source { get; }
        protected AbstractCharacter Target { get; }
        public AbstractCard.CardTarget CardTarget;


        public AbstractAction(AbstractCharacter source, AbstractCharacter target) {
            Source = source;
            Target = target;
        }

        public AbstractAction(AbstractCharacter source, AbstractCard.CardTarget target) {
            Source = source;
            CardTarget = target;
        }

        public abstract void OnAct();
        

    }
}