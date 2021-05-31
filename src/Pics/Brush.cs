using System.Collections.Generic;
using System.Linq;

namespace Pics {
    public class Brush : MovingThing {

        private int spare_brushes;
        public Brush() {
            coordinates = new Coordinates(0, 0);
            direction = Direction.Stop;
            spare_brushes = 5;
        }

        public void update(List<GroundMouse> ground_mice, List<SnowMouse> snow_mice, Room room) {
            var next_coordinates = Move.move(direction, coordinates);
            var next_content = get_content_at_next_coordinates(next_coordinates, ground_mice, snow_mice, room);
            switch(next_content) {
                case NextCellContent.Ground:
                case NextCellContent.Snow:
                    go_on(next_coordinates);
                    break;
                case NextCellContent.Wall:
                    stop();
                    break;
                case NextCellContent.Mouse:
                    burn();
                    break;
            };
        }

        private enum NextCellContent {
            Ground,
            Snow,
            Wall,
            Mouse
        }
        private NextCellContent get_content_at_next_coordinates(Coordinates next_coordinates, List<GroundMouse> ground_mice, List<SnowMouse> snow_mice, Room room) {
            if(is_next_cell_mouse(next_coordinates, ground_mice, snow_mice)) {
                return NextCellContent.Mouse;
            }
            if(is_next_cell_outer_wall(next_coordinates, room)) {
                return NextCellContent.Wall;
            }
            return NextCellContent.Ground;
        }

        private bool is_next_cell_mouse(Coordinates next_coordinates, List<GroundMouse> ground_mice, List<SnowMouse> snow_mice) {
            return is_next_cell_ground_mouse(next_coordinates, ground_mice)
                || is_next_cell_snow_mouse(next_coordinates, snow_mice);
        }

        private bool is_next_cell_ground_mouse(Coordinates next_coordinates, List<GroundMouse> ground_mice) {
            return is_mouse_at_coordinates(next_coordinates, ground_mice);
        }

        private bool is_next_cell_snow_mouse(Coordinates next_coordinates, List<SnowMouse> snow_mice) {
            return is_mouse_at_coordinates(next_coordinates, snow_mice);
        }

        private bool is_mouse_at_coordinates(Coordinates coordinates, IEnumerable<Mouse> mice) {
            return mice.Any(mouse => coordinates == mouse.coordinates);
        }

        private bool is_next_cell_outer_wall(Coordinates next_coordinates, Room room) {
            if(next_coordinates.x >= room.width) {
                return true;
            }
            if(next_coordinates.x < 0) {
                return true;
            }
            if(next_coordinates.y >= room.height) {
                return true;
            }
            if(next_coordinates.y < 0) {
                return true;
            }
            return false;
        }

        private void go_on(Coordinates c) {
            coordinates = c;
        }

        private void burn() {
            coordinates = initial_coordinates();
            spare_brushes--;
        }

        private Coordinates initial_coordinates() {
            return new Coordinates(0, 0);
        }

        private void stop() {
            direction = Direction.Stop;
        }
    }
}