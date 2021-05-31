using System.Collections.Generic;

namespace Pics {
    public class Room {
        private Cell[,] _floor;
        private HashSet<Coordinates> steps;
        internal readonly int height;
        internal readonly int width;
        public Room() : this(4, 7) {
            _floor[1,0] = Cell.Food;
            _floor[2,3] = Cell.Animal;
        }
        public Room(int w, int h) {
            width = w;
            height = h;
            _floor = filled_floor(width, height);
            steps = new HashSet<Coordinates>();
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

        public void add_step(Coordinates coordinates) {
            steps.Add(coordinates);
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