using utils;

namespace characters.buffs {
    public class Bravery : Buff{

        public Bravery() { }

        public Bravery(AbstractCharacter source, AbstractCharacter target, int duration)
            : base("Bravery", "Deal 20% more damage", BuffType.Buff, 0, duration, source, target) { }

        public override void OnUse(AbstractCharacter source, AbstractCharacter target) { }

        public override void Escalate(AbstractCharacter source, AbstractCharacter target, int amount, int duration) {
            Duration += Utils.NotNegative(duration);
        }
    }
}