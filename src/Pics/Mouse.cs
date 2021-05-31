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
    }
}