using characters;
using characters.buffs;
using utils;

namespace Blade.characters.buffs {
    public class Poison : Buff {

        public Poison() : base() { }

        public Poison(AbstractCharacter source, AbstractCharacter target, int amount)
            : base("Poison", "", BuffType.Debuff, amount, 3, source, target) { }

        public override void OnUse(AbstractCharacter source, AbstractCharacter target) {
            target.TakeRealDamage(Amount * 3);
        }

        public override void Escalate(AbstractCharacter source, AbstractCharacter target, int amount, int duration) {
            Amount += Utils.NotNegative(amount);
        }
    }
}