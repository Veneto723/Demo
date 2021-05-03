using utils;

namespace characters.buffs {
    public class Hard : Buff{
        public Hard() { }

        public Hard(AbstractCharacter source, AbstractCharacter target, int amount)
            : base("Hard", "receive 20% less damage", BuffType.Buff, amount, 
                0, source, target) { }

        public override void OnUse(AbstractCharacter source, AbstractCharacter target) { }

        public override void Escalate(AbstractCharacter source, AbstractCharacter target, int amount, int duration) {
            Amount += Utils.NotNegative(amount);
        }
    }
}