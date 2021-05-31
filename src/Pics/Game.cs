using System;
using System.Threading.Tasks;

namespace Pics
{
    class Game
    {
        private int level;
        private Config config;

        public Game() {
            config = new Config();
        }

        public void Run()
        {
            var r = new Room(config.width, config.height);
            Console.WriteLine("Room:");
            Console.WriteLine(r);
            game_loop();
            Console.WriteLine("Done.");
        }

        private void prepare_new_game() {
            set_initial_level();
        }

        private void set_initial_level() {
            level = 1;
        }

        private void increase_level() {
            level++;
        }

        private bool is_insane_level() {
            return level > 10;
        }
        private void game_loop() {
            bool keep_playing = true;
            do {
                var result = play_level();
                switch(result) {
                    case LevelResult.next_level:
                        increase_level();
                        if(is_insane_level()) {
                            keep_playing = false;
                            Console.WriteLine("Too much.");
                        }
                        break;
                    case LevelResult.game_over:
                        prepare_new_game();
                        break;
                    case LevelResult.quit:
                        keep_playing = false;
                        break;
                }
            } while (keep_playing);
        }

        private LevelResult play_level() {
            LevelResult status;
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

        }

        private void update_brush() {
            brush.update();
        }

        private void update_mice() {
            foreach(var mouse in mice) {
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
