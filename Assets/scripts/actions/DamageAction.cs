using System.Runtime.CompilerServices;
using cards;
using characters;

namespace actions {
    public class DamageAction : AbstractAction {
        public int Damage { get;}
        private readonly int _defenceIgnore;

        public DamageAction(AbstractCharacter source, AbstractCharacter target, AbstractCard card, int damage) 
            : base(source, target, card) {
            var percent = 100 + source.DealDamagePercent() + target.TakeDamagePercent();
            Damage = percent * damage;
        }

        public DamageAction(AbstractCharacter source, AbstractCharacter target, AbstractCard card, int damage, int defenceIgnore)
            : base(source, target, card) {
            var percent = 100 + source.DealDamagePercent() + target.TakeDamagePercent();
            Damage = percent * damage;
            _defenceIgnore = defenceIgnore;
        }

        public override void OnAct() {
            Target.TakeDamage(Damage, _defenceIgnore);
        }

    }
}