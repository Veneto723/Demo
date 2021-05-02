using System.Collections.Generic;
using dungeon;

namespace actions {
    public class ActionManager {

        private AbstractDungeon _dungeon;
        private List<AbstractAction> _actions;
        
        public ActionManager(AbstractDungeon dungeon) {
            _dungeon = dungeon;
        }

        public void AddToBottom(AbstractAction action) {
            if (action is DamageAction) {
                if (action.Target.CanCounter(action.SourceCard)) {
                    // TODO 进入反击阶段
                }
            }
            action.OnAct();
        }
        
        public void AddToTop(AbstractAction action) {
            _actions.Insert(0, action);
        }

    }
}