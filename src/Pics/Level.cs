using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pics {

    public class Level {
        private Config config;
        private int level_number;
        private Brush brush = default!;
        private List<GroundMouse> ground_mice = default!;
        private List<SnowMouse> snow_mice = default!;
        private Room room = default!;

        public Level(Config config, int level_number) {
            this.config = config;
            this.level_number = level_number;
            create_room();
            create_brush();
            create_mice();
        }
        public LevelResult play_level() {
            LevelResult status;
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
            var mouse = create_ground_mouse();
            ground_mice.Add(mouse);
        }

        private GroundMouse create_ground_mouse() {
            var coordinates = ground_mouse_initial_coordinates();
            var speed = ground_mouse_initial_speed();
            var direction = ground_mouse_initial_direction();
            var mouse = new GroundMouse(coordinates, speed, direction);
            return mouse;
        }

        private Coordinates ground_mouse_initial_coordinates() {
            var y = 0;
            var x = 0;
            if (config.width % 2 == 0) {
                x = config.width / 2;
            } else {
                x = config.width / 2 + 1;
            }
            return new Coordinates(x, y);
        }

        private int ground_mouse_initial_speed() {
            return level_number;
        }

        private Direction ground_mouse_initial_direction() {
            return Direction.DownLeft;
        }

        private void create_snow_mice() {
            snow_mice = new List<SnowMouse>();
            var n = level_number + 1;
            for(var i = 0; i < n; i++) {
                var mouse = new SnowMouse();
                snow_mice.Add(mouse);
            }
        }

        private void create_room() {
            room = new Room(config.width, config.height);
        }
        private void create_brush() {
            brush = new Brush();
        }

        private void update_brush() {
            brush.update(ground_mice, snow_mice, room);
        }

        private void update_mice() {
            update_ground_mice();
            update_snow_mice();
            check_brush_to_bite();
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

        private void check_brush_to_bite() {
            var brush_result = check_brush();
            if (brush_result == BiteResult.Bitten) {
                brush.burn(room);
                return;
            }
        }

        private BiteResult check_brush() {
            foreach(var mouse in snow_mice) {
                var result = mouse.check_brush_to_bite(brush);
                if (result == BiteResult.Bitten) {
                    return result;
                }
            }
            foreach(var mouse in ground_mice) {
                var result = mouse.check_brush_to_bite(brush);
                if (result == BiteResult.Bitten) {
                    return;
                }
            }
        }

        private void redraw() {
            Console.WriteLine("Room:");
            Console.WriteLine(room);
        }
        private LevelResult calculate_level_result() {
            if(brush.has_brushes_available()) {
                return LevelResult.next_level;
            } else {
                return LevelResult.game_over;
            }
        }
    }
}