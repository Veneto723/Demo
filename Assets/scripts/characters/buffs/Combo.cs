using utils;

namespace characters.buffs {
    public class Combo : Buff {

        public Combo() : base() { }

        public Combo(int amount, AbstractCharacter source, AbstractCharacter target) : 
            base("连击", "", BuffType.Buff, amount, InfiniteDuration, source, target) { }
        public override void OnUse(AbstractCharacter source, AbstractCharacter target) { }

        public override void Escalate(AbstractCharacter source, AbstractCharacter target, int amount, int duration) {
            Amount += Utils.NotNegative(amount);
        }
    }
}