using System;

namespace Pics {
    public enum Direction {
        UpLeft, Up, UpRight,
        Left, Stop, Right,
        DownLeft, Down, DownRight
    }
    static class DirectionExtensions {
        public static Direction flip (this Direction input) {
            switch(input) {
                case Direction.UpLeft: return Direction.DownRight;
                case Direction.Up: return Direction.Down;
                case Direction.UpRight: return Direction.DownLeft;
                case Direction.Left: return Direction.Right;
                case Direction.Right: return Direction.Left;
                case Direction.DownLeft: return Direction.UpRight;
                case Direction.Down: return Direction.Up;
                case Direction.DownRight: return Direction.UpLeft;
            }
            throw new ArgumentException($"Unknown Direction to flip: {input}");
        }

        public static Direction flip_vertical_wall (this Direction input) {
            switch(input) {
                case Direction.UpLeft: return Direction.UpRight;
                case Direction.UpRight: return Direction.UpLeft;
                case Direction.Left: return Direction.Right;
                case Direction.Right: return Direction.Left;
                case Direction.DownLeft: return Direction.DownRight;
                case Direction.DownRight: return Direction.DownLeft;
            }
            throw new ArgumentException($"Unknown Direction to flip vertical wall: {input}");
        }

        public static Direction flip_horizontal_wall (this Direction input) {
            switch(input) {
                case Direction.UpLeft: return Direction.DownLeft;
                case Direction.Up: return Direction.Down;
                case Direction.UpRight: return Direction.DownRight;
                case Direction.DownLeft: return Direction.UpLeft;
                case Direction.Down: return Direction.Up;
                case Direction.DownRight: return Direction.UpRight;
            }
            throw new ArgumentException($"Unknown Direction to flip horizontal wall: {input}");
        }

        public static Coordinates direction_to_delta(this Direction input) {
            switch(input) {
                case Direction.UpLeft: return new Coordinates(x: -1, y: -1);
                case Direction.Up: return new Coordinates(x: 0, y: -1);
                case Direction.UpRight: return new Coordinates(x: 1, y: -1);
                case Direction.Left: return new Coordinates(x: -1, y: 0);
                case Direction.Stop: return new Coordinates(x: 0, y: 0);
                case Direction.Right: return new Coordinates(x: 1, y: 0);
                case Direction.DownLeft: return new Coordinates(x: -1, y: 1);
                case Direction.Down: return new Coordinates(x: 0, y: 1);
                case Direction.DownRight: return new Coordinates(x: 1, y: 1);
            }
            throw new ArgumentException($"Unknown Direction to get delta: {input}");
        }

        public static Direction index_to_direction(this int index) {
            switch (index % 8) {
                case 0: return Direction.UpLeft;
                case 1: return Direction.Up;
                case 2: return Direction.UpRight;
                case 3: return Direction.Left;
                case 4: return Direction.Right;
                case 5: return Direction.DownLeft;
                case 6: return Direction.Down;
                case 7: return Direction.DownRight;
                default: return Direction.Stop;
            }
        }
    }
}
