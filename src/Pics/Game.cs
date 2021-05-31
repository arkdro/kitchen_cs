using System;

namespace Pics
{
    class Game
    {
        public void Run()
        {
            var config = new Config();
            var r = new Room(config.width, config.height);
            Console.WriteLine("Room:");
            Console.WriteLine(r);
        }
    }
}
