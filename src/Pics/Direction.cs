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

    }
}
