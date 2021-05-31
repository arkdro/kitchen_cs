namespace Pics {
    public class Replica {
        public int[,] data { get; internal set; }
        public int snow_color { get; private set; }
        public int ground_color {get; private set; }

        public Replica(Room room) {
            (int[,] replica, int snow_color, int ground_color) = room.get_black_white_replica();
            this.data = replica;
            this.snow_color = snow_color;
            this.ground_color = ground_color;
        }

        public int get (Coordinates coordinates) {
            return data[coordinates.y, coordinates.x];
        }

        public void set (Coordinates coordinates, int color) {
            data[coordinates.y, coordinates.x] = color;
        }
    }
}
