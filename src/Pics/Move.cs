using System;

namespace Pics {
    public class Move {
        public static Coordinates move(Direction direction, Coordinates coordinates) {
            var result = coordinates;
            switch(direction)
            {
                case Direction.UpLeft:
                    return new Coordinates(x: coordinates.x - 1, y: coordinates.y - 1);
                case Direction.Up:
                    return new Coordinates(x: coordinates.x, y: coordinates.y - 1);
                case Direction.UpRight:
                    return new Coordinates(x: coordinates.x + 1, y: coordinates.y - 1);
                case Direction.Left:
                    return new Coordinates(x: coordinates.x - 1, y: coordinates.y);
                case Direction.Stop:
                    return coordinates;
                case Direction.Right:
                    return new Coordinates(x: coordinates.x + 1, y: coordinates.y);
                case Direction.DownLeft:
                    return new Coordinates(x: coordinates.x - 1, y: coordinates.y + 1);
                case Direction.Down:
                    return new Coordinates(x: coordinates.x, y: coordinates.y + 1);
                case Direction.DownRight:
                    return new Coordinates(x: coordinates.x + 1, y: coordinates.y + 1);
                default:
                    throw new ArgumentOutOfRangeException($"unknown direction to move: {direction}");
            };
        }
    }
}