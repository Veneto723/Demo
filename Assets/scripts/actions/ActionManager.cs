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
            _actions.Add(action);
        }
        
        public void AddAToTop(AbstractAction action) {
            _actions.Insert(0, action);
        }

    }
}