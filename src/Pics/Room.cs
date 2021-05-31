using System.Collections.Generic;

namespace Pics {
    public class Room {
        private Cell[,] _floor;
        private HashSet<Coordinates> _steps = default!;
        internal readonly int height;
        internal readonly int width;
        internal Coordinates snow_top_left_point { get; private set; }
        internal Coordinates snow_bottom_right_point { get; private set; }
        public Room() : this(4, 7) {
            _floor[1,0] = Cell.Brush;
            _floor[2,3] = Cell.SnowMouse;
        }
        public Room(int w, int h) {
            width = w;
            height = h;
            _floor = filled_floor(width, height);
            add_snow();
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

        internal (int[,], int, int) get_black_white_replica() {
            int snow_color = 1;
            int ground_color = 0;
            var replica = new int[height, width];
            for(int y = 0; y < height; y++) {
                for(int x = 0; x < width; x++) {
                    if (_floor[y, x] == Cell.Snow) {
                        replica[y, x] = snow_color;
                    } else {
                        replica[y, x] = ground_color;
                    }
                }
            }
            return (replica, snow_color, ground_color);
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

        private void add_snow() {
            if (width < 7 || height < 7) {
                return;
            }
            var left_x = 2;
            var right_x = width - 3;
            var top_y = 2;
            var bottom_y = height - 3;
            snow_top_left_point = new Coordinates(x: left_x, y: top_y);
            snow_bottom_right_point = new Coordinates(x: right_x, y: bottom_y);
            for (var y = top_y; y <= bottom_y; y++) {
                for (var x = left_x; x <= right_x; x++) {
                    var coordinates = new Coordinates(x: x, y: y);
                    set(coordinates, Cell.Snow);
                }
            }
        }
    }
}