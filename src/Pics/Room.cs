namespace Pics {
    public class Room {
        private Cell[,] _floor;
        public Room() {
            _floor = filled_floor(4, 7);
        }
        public Room(int w, int h) {
            width = w;
            height = h;
            _floor = filled_floor(width, height);
        }
        override public string ToString() {
            string acc = "";
            for(int y = 0; y < height; y++) {
                string line = "";
                for(int x = 0; x < width; x++) {
                    var cell = _floor[x, y];
                    var symbol = cell.CellName();
                    line += symbol;
                }
                acc += line + "\n";
            }
            return acc;
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