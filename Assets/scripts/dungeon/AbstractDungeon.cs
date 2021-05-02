using actions;

namespace dungeon {
    public class AbstractDungeon {

        public static ActionManager ActionManager;
        public AbstractRoom currentRoom;

        public AbstractDungeon() {
            ActionManager = new ActionManager(this);
            currentRoom = new BattleRoom(null);
        }

    }
}