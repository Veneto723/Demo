namespace characters.buffs {
    public class Suspension : Buff {
        public Suspension(AbstractCharacter source, AbstractCharacter target, int amount) 
            : base("Suspension", "", BuffType.Buff, amount, 1, source, target) { }

        public override void OnUse(AbstractCharacter source, AbstractCharacter target) {
            throw new System.NotImplementedException();
        }

        public override void Escalate(AbstractCharacter source, AbstractCharacter target, int amount, int duration) {
            throw new System.NotImplementedException();
        }
    }
}