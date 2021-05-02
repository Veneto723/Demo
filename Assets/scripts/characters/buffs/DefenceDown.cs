using utils;

namespace characters.buffs {
    public class DefenceDown : Buff {
        public DefenceDown() { }

        public DefenceDown(AbstractCharacter source, AbstractCharacter target, int amount)
            : base("DefenceDown", "lose certain defence", BuffType.Debuff, amount, 0, source, target) { }

        public override void OnUse(AbstractCharacter source, AbstractCharacter target) { }

        public override void Escalate(AbstractCharacter source, AbstractCharacter target, int amount, int duration) {
            Amount += Utils.NotNegative(amount);
            target.Defence -= amount;
        }
    }
}