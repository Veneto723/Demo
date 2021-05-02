using System.Runtime.CompilerServices;
using cards;
using characters;

namespace actions {
    public class DamageAction : AbstractAction {
        private int _damage;
        private readonly int _defenceIgnore;

        public DamageAction(AbstractCharacter source, AbstractCharacter target, AbstractCard card, int damage) 
            : base(source, target, card) {
            _damage = damage;
        }

        public DamageAction(AbstractCharacter source, AbstractCharacter target, AbstractCard card, int damage, int defenceIgnore)
            : base(source, target, card) {
            _damage = damage;
            _defenceIgnore = defenceIgnore;
        }

        public override void OnAct() {
            Target.TakeDamage(_damage, _defenceIgnore);
        }

    }
}