using cards;
using characters;

namespace actions {
    public class HealAction : AbstractAction{
        public int Heal { get;}

        public HealAction(AbstractCharacter source, AbstractCharacter target, AbstractCard card, int heal) 
            : base(source, target, card) {
            Heal = heal;
        }

        public override void OnAct() {
            Target.Heal(Heal);
        }

    }
}