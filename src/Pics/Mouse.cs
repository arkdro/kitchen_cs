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