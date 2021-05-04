using System.Collections.Generic;
using actions;
using characters;

namespace cards.testCards {
    //切反
    public class CutBack : AbstractAttackCard {
        public CutBack(AbstractCharacter owner) : base("CutBack", 0, 5,0, 0, 
            "", new Dictionary<Keyword, int>{{Keyword.Deal, 5}}, owner,
            CardModifier.Upper, CardRarity.Starter, CardTarget.Enemy) { }
        
        public override void OnUse(AbstractCharacter source, AbstractCharacter target) {
            base.OnUse(source, source);
            AddToBot(new DamageAction(source, target, this, Damage));
            // 执行相应动画
        }

        
        }
    }