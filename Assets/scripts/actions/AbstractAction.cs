using cards;
using characters;

namespace actions {
    public abstract class AbstractAction {
        public AbstractCharacter Source { get; }
        public AbstractCharacter Target { get; }
        public AbstractCard.CardTarget CardTarget;
        public AbstractCard SourceCard { get; }


        public AbstractAction(AbstractCharacter source, AbstractCharacter target, AbstractCard sourceCard) {
            Source = source;
            Target = target;
            SourceCard = sourceCard;
        }

        public AbstractAction(AbstractCharacter source, AbstractCard.CardTarget target) {
            Source = source;
            CardTarget = target;
        }

        public abstract void OnAct();

        public virtual void AfterAct() {
            SourceCard.AfterUse(Source, Target);
        }


    }
}