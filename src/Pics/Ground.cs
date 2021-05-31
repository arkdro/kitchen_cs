using System;

namespace Pics {
    public enum Ground {
        Ground,
        Snow
    }
    static class GroundExtensions {
        public static string GroundName(this Ground input) {
            switch(input) {
                case Ground.Ground: return ".";
                case Ground.Snow: return "#";
            }
            throw new ArgumentException($"Unknown Ground value: {input}");
        }
    }
}