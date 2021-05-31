using System;
using System.Collections.Generic;
using System.Threading;

namespace Pics {

    public class Level {

        public static int MAX_LEVEL = 10;
        private Config config;
        private int level_number;
        private Brush brush = default!;
        private List<GroundMouse> ground_mice = default!;
        private List<SnowMouse> snow_mice = default!;
        private Room room = default!;
        private KeyboardInput input = new KeyboardInput();

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
                var brush_new_direction = get_direction_from_input();
                update_brush(brush_new_direction);
                update_mice();
                redraw();
                status = calculate_level_result();
                delay();
            } while (status == LevelResult.continue_current_level);
            return status;
        }
        private void delay() {
            if (level_number <= MAX_LEVEL) {
                int delay = (MAX_LEVEL - level_number) * 100;
                Thread.Sleep(delay);
            }
            Thread.Sleep(5);
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

        private Coordinates snow_mouse_initial_coordinates(int total_mice, int index) {
            var placement = Placement.get_placement_parameters(room, total_mice);
            var coordinates = build_mouse_coordinates(room, index, placement);
            return coordinates;
        }

        private Coordinates build_mouse_coordinates(Room room, int index, Placement placement) {
            int start_point = placement.one_piece_volume * index;
            int end_point = Math.Min(start_point + placement.one_piece_volume, placement.placement_total);
            var rnd = new Random();
            var i = rnd.Next(start_point, end_point);
            int y = i / placement.snow_height + placement.height_gap;
            int x = i % placement.snow_width + placement.width_gap;
            return new Coordinates(x: x, y: y);
        }

        private int ground_mouse_initial_speed() {
            return level_number;
        }

        private int snow_mouse_initial_speed() {
            return level_number;
        }

        private Direction ground_mouse_initial_direction() {
            return Direction.DownLeft;
        }

        private Direction snow_mouse_initial_direction(int index) {
            return index.index_to_direction();
        }

        private void create_snow_mice() {
            if (!enough_space_for_snow_mice()) {
                return;
            }
            snow_mice = new List<SnowMouse>();
            var n = level_number;
            for(var i = 0; i < n; i++) {
                var mouse = create_snow_mouse(n, i);
                snow_mice.Add(mouse);
            }
        }

        private SnowMouse create_snow_mouse(int total, int index) {
            var coordinates = snow_mouse_initial_coordinates(total, index);
            var speed = snow_mouse_initial_speed();
            var direction = snow_mouse_initial_direction(index);
            var mouse = new SnowMouse(coordinates, speed, direction);
            return mouse;
        }

        private bool enough_space_for_snow_mice() {
            var snow_width = room.snow_bottom_right_point.x - room.snow_top_left_point.x;
            var snow_height = room.snow_bottom_right_point.y - room.snow_top_left_point.y;
            if (snow_width >= 5 && snow_height >= 5) {
                return true;
            } else {
                return false;
            }
        }

        private void create_room() {
            room = new Room(config.width, config.height);
        }
        private void create_brush() {
            brush = new Brush();
        }

        private void update_brush(Direction new_direction) {
            brush.update(new_direction, ground_mice, snow_mice, room);
        }

        private void update_mice() {
            update_ground_mice();
            update_snow_mice();
            check_brush_to_bite();
        }

        private void update_ground_mice() {
            foreach(var mouse in ground_mice) {
                mouse.update(room);
            }
        }

        private void update_snow_mice() {
            foreach(var mouse in snow_mice) {
                mouse.update(room);
            }
        }

        private void check_brush_to_bite() {
            var brush_result = check_brush();
            if (brush_result == BiteResult.Bitten) {
                brush.burn(room);
                return;
            }
            var steps_result = check_brush_steps();
            if (steps_result == BiteResult.Bitten) {
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
                    return result;
                }
            }
            return BiteResult.Missed;
        }

        private BiteResult check_brush_steps() {
            var steps = room.get_steps();
            foreach(var mouse in snow_mice) {
                var result = mouse.check_brush_steps_to_bite(steps);
                if (result == BiteResult.Bitten) {
                    return result;
                }
            }
            foreach(var mouse in ground_mice) {
                var result = mouse.check_brush_steps_to_bite(steps);
                if (result == BiteResult.Bitten) {
                    return result;
                }
            }
            return BiteResult.Missed;
        }

        private void redraw() {
            Console.WriteLine("Room redraw:");
            string acc = "";
            for(int y = 0; y < room.height; y++) {
                string line = "";
                for(int x = 0; x < room.width; x++) {
                    var coordinates = new Coordinates(x: x, y: y);
                    var symbol = choose_current_cell_symbol(coordinates);
                    line += symbol;
                }
                acc += line + "\n";
            }
            Console.WriteLine(acc);
        }

        private string choose_current_cell_symbol(Coordinates coordinates) {
            string symbol;
            var content = get_current_coordinates_content(coordinates);
            switch(content) {
                case Cell.Brush:
                    symbol = Cell.Brush.CellName();
                    break;
                case Cell.Step:
                    symbol = Cell.Step.CellName();
                    break;
                case Cell.GroundMouse:
                    symbol = Cell.GroundMouse.CellName();
                    break;
                case Cell.SnowMouse:
                    symbol = Cell.SnowMouse.CellName();
                    break;
                default:
                    var cell = room.get(coordinates);
                    symbol = cell.CellName();
                    break;
            }
            return symbol;
        }

        private Cell get_current_coordinates_content(Coordinates coordinates) {
            if(brush.coordinates == coordinates) {
                return Cell.Brush;
            } else if (room.get_steps().Contains(coordinates)) {
                return Cell.Step;
            } else if (contains_snow_mouse(coordinates)) {
                return Cell.SnowMouse;
            } else if (contains_ground_mouse(coordinates)) {
                return Cell.GroundMouse;
            } else {
                return Cell.Ground; // ignored later
            }
        }

        private bool contains_snow_mouse(Coordinates coordinates) {
            return brush.is_next_cell_snow_mouse(coordinates, snow_mice);
        }

        private bool contains_ground_mouse(Coordinates coordinates) {
            return brush.is_next_cell_ground_mouse(coordinates, ground_mice);
        }

        private LevelResult calculate_level_result() {
            if(brush.has_brushes_available()) {
                return LevelResult.continue_current_level;
            } else {
                return LevelResult.game_over;
            }
        }

        private Direction get_direction_from_input() {
            return KeyboardInput.get_direction(brush.get_direction());
        }
    }

    internal class Placement {

        internal int placement_total;
        internal int one_piece_volume;
        internal int snow_width;
        internal int snow_height;
        internal int width_gap;
        internal int height_gap;

        public static Placement get_placement_parameters(Room room, int total_mice) {
            var p = new Placement();
            init_placement_parameters(p, room, total_mice);
            return p;
        }

        private static void init_placement_parameters(Placement p, Room room, int total_mice) {
            p.width_gap = 2;
            p.height_gap = 2;
            p.snow_width = room.snow_bottom_right_point.x - room.snow_top_left_point.x;
            p.snow_height = room.snow_bottom_right_point.y - room.snow_top_left_point.y;
            var placement_width = p.snow_width - p.width_gap;
            var placement_height = p.snow_height - p.height_gap;
            p.placement_total = placement_width * placement_height;
            p.one_piece_volume = p.placement_total / total_mice;
        }
    }
}