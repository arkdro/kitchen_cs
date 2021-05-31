namespace Pics {
    public class Room {
        private Cell[,] _floor;
        public Room() {
            _floor = filled_floor(4, 7);
        }
        private Cell[,] filled_floor(int width, int height) {
            var array = new Cell[width,height];
            for(int y = 0; y < height; y++) {
                for(int x = 0; x < width; x++) {
                    array[x, y] = Cell.Ground;
                }
            }
            return array;
        }
    }
}