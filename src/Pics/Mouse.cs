using System.Collections.Generic;

namespace Pics {
    public abstract class Mouse : MovingThing {
        private readonly Cell preferred_ground;
        public Mouse(Ground ground) {
            switch(ground) {
                case Ground.Ground:
                    preferred_ground = Cell.Ground;
                    break;
                case Ground.Snow:
                    preferred_ground = Cell.Snow;
                    break;
            }
        }

        public void update(Room room) {
            if(direction == Direction.Stop || speed == 0) {
                return;
            }
            var next_cell = get_next_cell_content(room);
            switch(next_cell) {
                case NextCell.Free:
                    coordinates = Move.move(direction, coordinates);
                    break;
                case NextCell.Corner:
                    bounce_corner(room);
                    break;
                case NextCell.VerticallWall:
                    bounce_vertical_wall(room);
                    break;
                case NextCell.HorisontalWall:
                    bounce_horisontal_wall();
                    break;
            }
        }

        public BiteResult check_brush_to_bite(Brush brush) {
            if (coordinates == brush.coordinates) {
                return BiteResult.Bitten;
            } else {
                return BiteResult.Missed;
            }
        }

        internal BiteResult check_brush_steps_to_bite(HashSet<Coordinates> steps) {
            if (steps.Contains(coordinates)) {
                return BiteResult.Bitten;
            } else {
                return BiteResult.Missed;
            }
        }

        private void bounce_corner(Room room) {
            if(can_bounce_back(room)) {
                direction.flip();
                coordinates = Move.move(direction, coordinates);
            } else {
                stop();
            }
        }

        private void bounce_vertical_wall(Room room) {
            if (can_bounce_vertical_wall(room)) {
            } else {
                bounce_corner(room);
            }
        }

        private NextCell get_next_cell_content(Room room) {
            var next_coordinates = Move.move(direction, coordinates);
            var next_cell = room.get(next_coordinates);
            if (next_cell == preferred_ground) {
                return NextCell.Free;
            }
            var delta = direction.direction_to_delta();
            var vertically_changed_coordinates = new Coordinates(x: next_coordinates.x, y: coordinates.y + delta.y);
            var horizontally_changed_coordinates = new Coordinates(x: next_coordinates.x + delta.x, y: coordinates.y);
            var vertical_cell = room.get(vertically_changed_coordinates);
            var horizontal_cell = room.get(horizontally_changed_coordinates);
            if( vertical_cell != preferred_ground && horizontal_cell != preferred_ground) {
                return NextCell.Corner;
            } else if( vertical_cell != preferred_ground) {
                return NextCell.VerticallWall;
            } else {
                return NextCell.HorisontalWall;
            }
        }

        private bool can_bounce_back(Room room) {
            var backward_direction = direction.flip();
            var backward_coordinates = Move.move(backward_direction, coordinates);
            var backward_cell = room.get(backward_coordinates);
            return backward_cell == preferred_ground;
        }

        private bool can_bounce_vertical_wall(Room room) {
            var new_direction = direction.flip_vertical_wall();
            var new_coordinates = Move.move(new_direction, coordinates);
            var new_cell = room.get(new_coordinates);
            return new_cell == preferred_ground;
        }

        private void stop() {
            direction = Direction.Stop;
            speed = 0;
        }
    }
}