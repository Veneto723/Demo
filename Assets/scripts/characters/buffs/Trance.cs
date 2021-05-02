using utils;

namespace characters.buffs {
    public class Trance : Buff{

        public Trance() { }

        public Trance(AbstractCharacter source, AbstractCharacter target, int duration)
            : base("Trance", "Deal 15% less damage", BuffType.Debuff, 0, duration, source, target) { }

        public override void OnUse(AbstractCharacter source, AbstractCharacter target) { }

        public override void Escalate(AbstractCharacter source, AbstractCharacter target, int amount, int duration) {
            Duration += Utils.NotNegative(duration);
        }
    }
}