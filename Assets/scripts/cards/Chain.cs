using System.Collections.Generic;
using actions;
using characters;
using characters.buffs;
using UnityEditor.Animations;

namespace cards {
    public class Chain {
        public Dictionary<AbstractCard.Keyword, int> dict = new Dictionary<AbstractCard.Keyword, int>();

        public static void DoChain(AbstractCard prevCard, AbstractCard thisCard) {
            var chain = new Chain();
            if (prevCard.HasModifier(AbstractCard.CardModifier.Upper)) {
                if (thisCard.HasModifier(AbstractCard.CardModifier.Upper)) {
                    chain.dict.Add(AbstractCard.Keyword.Bravery, 1);
                }

                if (thisCard.HasModifier(AbstractCard.CardModifier.Middle)) {
                    chain.dict.Add(AbstractCard.Keyword.DefenceDown, 1);
                }

                if (thisCard.HasModifier(AbstractCard.CardModifier.Lower)) {
                    chain.dict.Add(AbstractCard.Keyword.Unbalanced, 1);
                }

                if (thisCard.HasModifier(AbstractCard.CardModifier.Dash)) {
                    chain.dict.Add(AbstractCard.Keyword.Bleeding, 1);
                }
            }
            else if (prevCard.HasModifier(AbstractCard.CardModifier.Middle)) {
                if (thisCard.HasModifier(AbstractCard.CardModifier.Upper)) {
                    chain.dict.Add(AbstractCard.Keyword.Hard, 1);
                }

                if (thisCard.HasModifier(AbstractCard.CardModifier.Middle)) {
                    chain.dict.Add(AbstractCard.Keyword.Trance, 1);
                }

                if (thisCard.HasModifier(AbstractCard.CardModifier.Lower)) {
                    chain.dict.Add(AbstractCard.Keyword.Posture, 1);
                }

                if (thisCard.HasModifier(AbstractCard.CardModifier.Dash)) {
                    chain.dict.Add(AbstractCard.Keyword.Draw, 1);
                }
            }
            else if (prevCard.HasModifier(AbstractCard.CardModifier.Lower)) {
                if (thisCard.HasModifier(AbstractCard.CardModifier.Upper)) {
                    chain.dict.Add(AbstractCard.Keyword.Unbalanced, 1);
                }

                if (thisCard.HasModifier(AbstractCard.CardModifier.Middle)) {
                    chain.dict.Add(AbstractCard.Keyword.SelfCost, 1);
                }

                if (thisCard.HasModifier(AbstractCard.CardModifier.Lower)) {
                    chain.dict.Add(AbstractCard.Keyword.Immortal, 1);
                }

                if (thisCard.HasModifier(AbstractCard.CardModifier.Dash)) {
                    chain.dict.Add(AbstractCard.Keyword.OpponentDiscard, 1);
                }
            }
            else if (prevCard.HasModifier(AbstractCard.CardModifier.Dash)) {
                if (thisCard.HasModifier(AbstractCard.CardModifier.Upper)) { 
                    chain.dict.Add(AbstractCard.Keyword.Vulnerable, 1);
                    
                }

                if (thisCard.HasModifier(AbstractCard.CardModifier.Middle)) { 
                    chain.dict.Add(AbstractCard.Keyword.Posture, -2);
                }

                if (thisCard.HasModifier(AbstractCard.CardModifier.Lower)) {
                    chain.dict.Add(AbstractCard.Keyword.Evasion, 1);
                }

                if (thisCard.HasModifier(AbstractCard.CardModifier.Dash)) { 
                    chain.dict.Add(AbstractCard.Keyword.Penetrate, 0);
                }
            }

            thisCard.Chain = chain;
        }

        public static AbstractAction GetActionByChain(AbstractCharacter source, AbstractCharacter target, AbstractCard card, Dictionary<AbstractCard.Keyword, int> dict) {
            foreach (var key in dict.Keys) {
                switch (key) {
                    case AbstractCard.Keyword.Draw:
                        return new DrawAction(source, target, card, dict[key]);
                    case AbstractCard.Keyword.Posture:
                        return dict[key] > 0 ? new PostureAction(source, target, card, dict[key]) : new PostureAction(source, source, card, -dict[key]);
                    case AbstractCard.Keyword.SelfCost:
                        return new CostAction(source, source, card, dict[key]);
                    case AbstractCard.Keyword.Discard:
                        // return 
                        break;
                    case AbstractCard.Keyword.Bleeding:
                        return new BuffAction(source, target, new Bleeding(source, target, dict[key], dict[key]), card, true);
                    case AbstractCard.Keyword.Bravery:
                        return new BuffAction(source, target, new Bravery(source, target, dict[key]), card, true);
                    case AbstractCard.Keyword.DefenceDown:
                        return new BuffAction(source, target, new DefenceDown(source, target, dict[key]), card, true);
                    case AbstractCard.Keyword.Unbalanced:
                        return new BuffAction(source, target, new DefenceDown(source, target, dict[key]), card, true);
                    case AbstractCard.Keyword.Hard:
                        return new BuffAction(source, target, new Hard(source, target, dict[key]), card, true);
                    case AbstractCard.Keyword.Trance:
                        return new BuffAction(source, target, new Trance(source, target, dict[key]), card, true);
                    case AbstractCard.Keyword.Vulnerable:
                        return new BuffAction(source, target, new Vulnerable(source, target, dict[key]), card, true);
                    case AbstractCard.Keyword.Evasion:
                        return new BuffAction(source, target, new Evasion(source, target, dict[key]), card, true);
                    case AbstractCard.Keyword.OpponentDiscard:
                        return null;
                    case AbstractCard.Keyword.OpponentCost:
                        return new CostAction(source, target, card, dict[key]);
                    case AbstractCard.Keyword.Stun:
                        return new BuffAction(source, target, new Stun(source, target, dict[key]), card, true);
                        break;
                    default:
                        break;
                }
            }

            return null;
        }
    }
}