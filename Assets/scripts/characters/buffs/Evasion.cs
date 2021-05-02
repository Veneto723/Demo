using utils;

namespace characters.buffs {
    public class Evasion : Buff {

        public Evasion() { }

        public Evasion(AbstractCharacter source, AbstractCharacter target, int amount) 
            : base("Evasion", "evade next attack", BuffType.Buff, amount, InfiniteDuration, source, target) { }


        public override void OnUse(AbstractCharacter source, AbstractCharacter target) { }

        public override void Escalate(AbstractCharacter source, AbstractCharacter target, int amount, int duration) {
            Amount += Utils.NotNegative(amount);
        }
    }
}