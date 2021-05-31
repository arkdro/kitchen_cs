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

        private void go_on(Coordinates c) {
            coordinates = c;
        }

        private void burn() {
        }

        private void bounce() {
        }
    }
}