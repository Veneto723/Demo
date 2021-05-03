using System.Collections.Generic;
using characters;

namespace dungeon {
    public class AbstractRoom {

        public RoomType Type { get; }

        public AbstractCharacter Player { get; set; }
        public AbstractMonster Enemy { get; set; }

        public AbstractRoom(RoomType type) {
            Type = type;
        }

        public enum RoomType {
            Battle, Event,
        }
    }
}