using System.Collections.Generic;
using System.Linq;
using actions;
using characters;
using characters.buffs;


//掠影
namespace cards.testCards {
    public class Glimpse : AbstractAttackCard {
        public Glimpse(AbstractCharacter owner) : base("Glimpse", 1, 6, 1, 0, "",
            new Dictionary<Keyword, int> {{Keyword.Deal, 6}, {Keyword.SelfCost, 1}}, owner, CardModifier.Lower,
            CardRarity.Starter, CardTarget.Enemy) { }

        public override void OnUse(AbstractCharacter source, AbstractCharacter target) {
            base.OnUse(source, source);
            AddToBot(new DamageAction(source, target, this, Damage));
        }

        public override void AfterUse(AbstractCharacter source, AbstractCharacter target) {
            var num = target.Buffs.Count(buff => buff.Type == Buff.BuffType.Debuff);
            AddToBot(new CostAction(source, source, this, num));
        }
    }
}