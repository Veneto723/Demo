using utils;

namespace characters.buffs {
    public class Bleeding : Buff{

        public Bleeding() { }

        public Bleeding(AbstractCharacter source, AbstractCharacter target, int amount, int duration)
            : base("Bleeding", "Lose certain hp per turn", BuffType.Debuff, amount, duration, source, target) { }

        public override void OnUse(AbstractCharacter source, AbstractCharacter target) {
            target.TakeRealDamage(Amount);
        }

        public override void Escalate(AbstractCharacter source, AbstractCharacter target, int amount, int duration) {
            Amount += Utils.NotNegative(amount);
            Duration += Utils.NotNegative(duration);
        }
    }
}