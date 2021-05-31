using System;

namespace Pics {
    public class Coordinates : IEquatable<Coordinates> {

        public override string ToString()
        {
            return $"Coordinates(x: {x}, y: {y})";
        }

        public override bool Equals(object? obj) {
            if (Object.ReferenceEquals(obj, null)) {
                return false;
            }
            if (this.GetType() != obj.GetType()) {
                return false;
            }
            if(obj is null) {
                return false;
            } else {
                return this.Equals(obj as Coordinates);
            }
        }

        public override int GetHashCode() {
            return x * 0x40011001 + y;
        }

        public bool Equals(Coordinates? other) {
            if (Object.ReferenceEquals(other, null)) {
                return false;
            }
            if (this.GetType() != other.GetType()) {
                return false;
            }
            return x == other.x && y == other.y;
        }

        public static bool operator ==(Coordinates p1, Coordinates p2) {
            if (Object.ReferenceEquals(p1, null)) {
                if (Object.ReferenceEquals(p2, null)) {
                    return true;
                } else {
                    return false;
                }
            } else {
                return p1.Equals(p2);
            }
        }

        public static bool operator !=(Coordinates p1, Coordinates p2) {
            return !(p1 == p2);
        }

        public Coordinates(int x, int y) {
            this.x = x;
            this.y = y;
        }
        internal readonly int x;
        internal readonly int y;
    }
}