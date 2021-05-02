using System.Collections.Generic;
using characters;

namespace dungeon {
    public class BattleRoom : AbstractRoom {
        private List<AbstractCharacter> Enemies { get; }

        public BattleRoom(List<AbstractCharacter> enemies) : base(RoomType.Battle) {
            Enemies = enemies;
        }
    }
}