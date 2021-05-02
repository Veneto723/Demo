namespace dungeon {
    public class AbstractRoom {

        public RoomType Type { get; }

        public AbstractRoom(RoomType type) {
            Type = type;
        }

        public enum RoomType {
            Battle, Event,
        }
    }
}