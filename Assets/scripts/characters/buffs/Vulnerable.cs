using utils;

namespace characters.buffs {
    public class Vulnerable : Buff {
        public Vulnerable() { }

        public Vulnerable(AbstractCharacter source, AbstractCharacter target, int amount)
            : base("Vulnerable", "Take 15% more damage", BuffType.Debuff, amount, 0, source, target) { }

        public override void OnUse(AbstractCharacter source, AbstractCharacter target) {
        }

        public override void Escalate(AbstractCharacter source, AbstractCharacter target, int amount, int duration) {
            Amount += Utils.NotNegative(amount);
        }
    }
}