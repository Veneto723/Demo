using cards;
using characters;

namespace actions {
    public class DrawWeaponAction : AbstractAction {
        private readonly int _draw;

        public DrawWeaponAction(AbstractCharacter source, AbstractCharacter target, AbstractCard card,
            int draw) : base(source, target, card) {
            _draw = draw;
        }
        
        public override void OnAct() {
            for (var i = 0; i < _draw; i++) {
                Target.Hand.Draw(Target.ArmoryDeck.Draw());
            }
        }
    }
}