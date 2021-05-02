using characters;

namespace actions {
    public class DrawAction : AbstractAction {
        private readonly int _draw;

        public DrawAction(AbstractCharacter source, AbstractCharacter target, int draw) : base(source, target) {
            _draw = draw;
        }
        
        public override void OnAct() {
            for (var i = 0; i < _draw; i++) {
                Target.Hand.Add(Target.Deck.Draw());
            }
        }
    }
}