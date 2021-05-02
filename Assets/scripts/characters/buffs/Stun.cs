using utils;

namespace characters.buffs {
    public class Stun : Buff {

        public Stun() { }

        public Stun(AbstractCharacter source, AbstractCharacter target, int amount) 
            : base("Stun", "Can not move for next turn", BuffType.Debuff, amount, 0, source, target) { }

        public override void OnUse(AbstractCharacter source, AbstractCharacter target) { }

        public override void Escalate(AbstractCharacter source, AbstractCharacter target, int amount, int duration) {
            Amount += Utils.NotNegative(amount);
        }
    }
}