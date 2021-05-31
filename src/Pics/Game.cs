using System;

namespace Pics
{
    class Game
    {
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
        private LevelResult play_level() {
        }

    }
}
