using System.Collections.Generic;
using System.Reflection;
using cards;
using dungeon;

namespace actions {
    public class ActionManager {
        private AbstractDungeon _dungeon;
        private List<AbstractAction> _actions;
        private List<AbstractCard> _cards;

        public ActionManager(AbstractDungeon dungeon) {
            _dungeon = dungeon;
            _actions = new List<AbstractAction>();
            _cards = new List<AbstractCard>();
        }

        /// <summary>
        /// 添加至行为队列尾部。
        /// </summary>
        /// <param name="action"></param>
        public void AddToBottom(AbstractAction action) {
            if (action is DamageAction && !action.SourceCard.Chain.dict.ContainsKey(AbstractCard.Keyword.Immortal)) {
                if (action.Target.CanCounter(action.SourceCard)) {
                    // TODO 进入反击阶段
                    var room = (BattleRoom) _dungeon.currentRoom;
                    if (room.IsPlayerTurn) {
                        var counterCard = (AbstractAttackCard) room.Enemy.ChooseCounterCard(action.SourceCard);
                        // 展示打出手牌
                        // counterCard.Heal
                    }
                }
            }

            action.OnAct();
        }

        /// <summary>
        /// 添加至行为队列头部。
        /// </summary>
        /// <param name="action"></param>
        public void AddToTop(AbstractAction action) {
            _actions.Insert(0, action);
        }
    }
}