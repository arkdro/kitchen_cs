using System;
using System.Threading;

namespace Pics {
    public class KeyboardInput : Input {
        public ConsoleKey? get_input() {
            ConsoleKey key = ConsoleKey.Spacebar;
            Thread.Sleep(1);
            if(Console.KeyAvailable) {
                key = Console.ReadKey(true).Key;
                return key;
            }
            return null;
        }

        public Direction get_direction(Direction old) {
            var key = get_input();
            if(key is null) {
                return old;
            }
            switch (key) {
                case ConsoleKey.UpArrow:
                    return Direction.Up;
                case ConsoleKey.DownArrow:
                    return Direction.Down;
                case ConsoleKey.LeftArrow:
                    return Direction.Left;
                case ConsoleKey.RightArrow:
                    return Direction.Right;
                default:
                    return Direction.Stop;
            }
        }
    }
}