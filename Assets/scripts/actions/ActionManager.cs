using System.Collections.Generic;
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
            if (action is DamageAction) {
                if (action.Target.CanCounter(action.SourceCard)) {
                    // TODO 进入反击阶段
                    
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