using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pics {

    public class Level {
        private int level_number;
        private Brush brush;
        private List<GroundMouse> ground_mice;
        private List<SnowMouse> snow_mice;

        public Level(int level_number) {
            this.level_number = level_number;
        }
        public LevelResult play_level() {
            LevelResult status;
            create_brush();
            create_mice();
            redraw();
            delay(); // ???
            do {
                update_brush();
                update_mice();
                redraw();
                status = calculate_level_result();
                delay();
            } while (status == LevelResult.continue_current_level);
            return status;
        }
        private void delay() {
            // FIXME delay should depend on a level
            Task.Delay(10);
        }

        private void create_mice() {
            create_ground_mice();
            create_snow_mice();
        }

        private void create_ground_mice() {
            ground_mice = new List<GroundMouse>();
        }

        private void create_snow_mice() {
            snow_mice = new List<SnowMouse>();
        }

        private void create_brush() {
            brush = new Brush();
        }

        private void update_brush() {
            brush.update();
        }

        private void update_mice() {
            update_ground_mice();
            update_snow_mice();
        }

        private void update_ground_mice() {
            foreach(var mouse in ground_mice) {
                mouse.update();
            }
        }

        private void update_snow_mice() {
            foreach(var mouse in snow_mice) {
                mouse.update();
            }
        }
        private void redraw() {
        }
        private LevelResult calculate_level_result() {
            return LevelResult.next_level;
        }
    }
}