using System;

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
        }

        private void prepare_new_game() {
            set_initial_level();
        }

        private void game_loop() {
            while(true) {
                var result = play_level();
                switch(result) {
                    case next_level:
                        increase_level();
                        break;
                    case game_over:
                        prepare_new_game();
                        break;
                    case quit:
                        return;
                }
            }
        }

        private LevelResult play_level() {
        }

    }
}
