using System.Collections.Generic;

namespace Pics {
    public class Brush : MovingThing {

        public void update(List<GroundMouse> ground_mice, List<SnowMouse> snow_mice, Room room) {
            var next_coordinates = Move.move(direction, coordinates);
            var next_content = get_content_at_next_coordinates(next_coordinates, ground_mice, snow_mice, room);
            switch(next_content) {
                case NextCellContent.Ground:
                case NextCellContent.Snow:
                    go_on(next_coordinates);
                    break;
                case NextCellContent.Wall:
                    bounce();
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
        private NextCellContent get_content_at_next_coordinates(Coordinates coordinates, List<GroundMouse> ground_mice, List<SnowMouse> snow_mice, Room room) {
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
            foreach(var mouse in mice) {
                if (coordinates == mouse.coordinates) {
                    return true;
                }
            }
            return false;
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
        }

        private void bounce() {
        }
    }
}