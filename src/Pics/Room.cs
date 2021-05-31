using System.Collections.Generic;

namespace Pics {
    public class Room {
        private Cell[,] _floor;
        private HashSet<Coordinates> _steps = default!;
        internal readonly int height;
        internal readonly int width;
        public Room() : this(4, 7) {
            _floor[1,0] = Cell.Brush;
            _floor[2,3] = Cell.SnowMouse;
        }
        public Room(int w, int h) {
            width = w;
            height = h;
            _floor = filled_floor(width, height);
            init_steps();
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
            _steps.Add(coordinates);
        }

        public Cell get(Coordinates coordinates) {
            return _floor[coordinates.x, coordinates.y];
        }

        public Cell? try_get(Coordinates coordinates) {
            if (coordinates.x < 0 || coordinates.x >= width) {
                return null;
            }
            if (coordinates.y < 0 || coordinates.y >= height) {
                return null;
            }
            return get(coordinates);
        }

        public void set(Coordinates coordinates, Cell content) {
            _floor[coordinates.x, coordinates.y] = content;
        }

        public HashSet<Coordinates> get_steps() {
            return _steps;
        }

        public void clear_steps() {
            init_steps();
        }

        private void init_steps() {
            _steps = new HashSet<Coordinates>();
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