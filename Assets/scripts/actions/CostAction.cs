using cards;
using characters;

namespace actions {
    public class CostAction : AbstractAction{
        
        private readonly int _cost;

        public CostAction(AbstractCharacter source, AbstractCharacter target, AbstractCard card, int cost) 
            : base(source, target, card) {
            _cost = cost;
        }
        
        public override void OnAct() {
            Target.CostRecover(_cost);
        }
    }
}