using utils;

namespace characters.buffs {
    public class Unbalanced : Buff{

        public Unbalanced() { }

        public Unbalanced(AbstractCharacter source, AbstractCharacter target, int amount) 
            : base("Unbalanced", "recover certain cost less next turn", BuffType.Debuff, amount, 0, source, target) { }

        public override void OnUse(AbstractCharacter source, AbstractCharacter target) {
        }

        public override void Escalate(AbstractCharacter source, AbstractCharacter target, int amount, int duration) {
            Amount += Utils.NotNegative(amount);
        }
    }
}