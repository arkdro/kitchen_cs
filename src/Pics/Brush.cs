using System.Collections.Generic;
using System.Linq;

namespace Pics {
    public class Brush : MovingThing {

        private int spare_brushes;
        private bool making_steps = false;
        private NextCellContent previous_background = NextCellContent.Ground;
        private BackgroundChange backround_change_status = BackgroundChange.None;
        public Brush() {
            coordinates = initial_coordinates();
            direction = Direction.Stop;
            spare_brushes = 5;
        }

        public void update(Direction new_direction, List<GroundMouse> ground_mice, List<SnowMouse> snow_mice, Room room) {
            direction = new_direction;
            var next_coordinates = Move.move(direction, coordinates);
            var next_content = get_content_at_next_coordinates(next_coordinates, ground_mice, snow_mice, room);
            switch(next_content) {
                case NextCellContent.Step:
                    burn(room);
                    break;
                case NextCellContent.GroundAndMouse:
                    mark_if_background_changed(next_content);
                    go_on(room, next_coordinates, snow_mice);
                    burn(room);
                    break;
                case NextCellContent.Mouse:
                    burn(room);
                    break;
                case NextCellContent.Ground:
                case NextCellContent.Snow:
                    mark_if_background_changed(next_content);
                    go_on(room, next_coordinates, snow_mice);
                    break;
                case NextCellContent.Wall:
                    stop();
                    break;
            };
        }

        public bool has_brushes_available() {
            return spare_brushes >= 0;
        }

        public Direction get_direction() {
            return direction;
        }

        private enum NextCellContent {
            Ground,
            Snow,
            Wall,
            Step,
            Mouse,
            GroundAndMouse
        }
        private NextCellContent get_content_at_next_coordinates(Coordinates next_coordinates, List<GroundMouse> ground_mice, List<SnowMouse> snow_mice, Room room) {
            if(is_next_cell_step(next_coordinates, room)) {
                return NextCellContent.Step;
            }
            if(is_next_cell_ground(next_coordinates, room) && is_next_cell_ground_mouse(next_coordinates, ground_mice)) {
                return NextCellContent.GroundAndMouse;
            }
            if(is_next_cell_mouse(next_coordinates, ground_mice, snow_mice)) {
                return NextCellContent.Mouse;
            }
            if(is_next_cell_outer_wall(next_coordinates, room)) {
                return NextCellContent.Wall;
            }
            if(is_next_cell_snow(next_coordinates, room)) {
                return NextCellContent.Snow;
            }
            return NextCellContent.Ground;
        }

        private bool is_next_cell_mouse(Coordinates next_coordinates, List<GroundMouse> ground_mice, List<SnowMouse> snow_mice) {
            return is_next_cell_ground_mouse(next_coordinates, ground_mice)
                || is_next_cell_snow_mouse(next_coordinates, snow_mice);
        }

        internal bool is_next_cell_ground_mouse(Coordinates next_coordinates, List<GroundMouse> ground_mice) {
            return is_mouse_at_coordinates(next_coordinates, ground_mice);
        }

        internal bool is_next_cell_snow_mouse(Coordinates next_coordinates, List<SnowMouse> snow_mice) {
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

        private bool is_next_cell_step(Coordinates next_coordinates, Room room) {
            return room.contains_step(next_coordinates);
        }

        private bool is_next_cell_ground(Coordinates next_coordinates, Room room) {
            var content = room.try_get(next_coordinates);
            return content == Cell.Ground;
        }

        private bool is_next_cell_snow(Coordinates next_coordinates, Room room) {
            var content = room.try_get(next_coordinates);
            return content == Cell.Snow;
        }
        private void go_on(Room room, Coordinates next_coordinates, List<SnowMouse> snow_mice) {
            if (entered_into_snow()) {
                start_making_steps();
            }
            if (exited_from_snow()) {
                add_step(room, coordinates);
                stop();
                stop_making_steps();
                finish_steps(room);
                remove_unoccupied_snow(room, snow_mice);
                room.update_stats();
            }
            if(making_steps) {
                add_step(room, coordinates);
            }
            coordinates = next_coordinates;
        }

        internal void burn(Room room) {
            coordinates = initial_coordinates();
            spare_brushes--;
            stop();
            burn_steps(room);
            previous_background = NextCellContent.Ground;
        }

        private Coordinates initial_coordinates() {
            return new Coordinates(x: 0, y: 0);
        }

        private void stop() {
            direction = Direction.Stop;
        }

        private void burn_steps(Room room) {
            stop_making_steps();
            clear_steps(room);
        }

        private void start_making_steps() {
            making_steps = true;
        }

        private void stop_making_steps() {
            making_steps = false;
        }

        private bool entered_into_snow() {
            return backround_change_status == BackgroundChange.EnteredSnow;
        }

        private bool exited_from_snow() {
            return backround_change_status == BackgroundChange.LeftSnow;
        }

        private void mark_if_background_changed(NextCellContent next_content) {
            if(previous_background == NextCellContent.Ground && next_content == NextCellContent.Snow) {
                previous_background = next_content;
                backround_change_status = BackgroundChange.EnteredSnow;
            } else if( previous_background == NextCellContent.Snow &&
                        (next_content == NextCellContent.Ground || next_content == NextCellContent.GroundAndMouse) ) {
                previous_background = next_content;
                backround_change_status = BackgroundChange.LeftSnow;
            } else {
                backround_change_status = BackgroundChange.None;
            }
        }

        private void add_step(Room room, Coordinates coordinates) {
            if(contains_snow(room, coordinates)) {
               room.add_step(coordinates);
            }
        }

        private bool contains_snow(Room room, Coordinates coordinates) {
            var content = room.get(coordinates);
            return content == Cell.Snow;
        }

        private void finish_steps(Room room) {
            foreach(var coordinate in room.get_steps()) {
                room.set(coordinate, Cell.Ground);
            }
            clear_steps(room);
        }

        private void clear_steps(Room room) {
            room.clear_steps();
        }

        private void remove_unoccupied_snow(Room room, List<SnowMouse> snow_mice) {
            Replica replica = new Replica(room);
            int keep_color = 2;
            foreach (var mouse in snow_mice) {
                var coordinates = mouse.coordinates;
                var color = replica.get(coordinates);
                if (color != keep_color) {
                    var filler = new Fill.Fill();
                    var new_data = filler.fill(replica.data, coordinates.x, coordinates.y, color, keep_color);
                    replica.data = new_data;
                }
            }
            for (int y = 0; y < room.height; y++) {
                for (int x = 0; x < room.width; x++) {
                    var coordinates = new Coordinates(x: x, y: y);
                    if (replica.get(coordinates) == replica.snow_color) {
                        replica.set(coordinates, replica.ground_color);
                    }
                }
            }
            room.copy_ground_from_replica(replica);
        }
    }
}
