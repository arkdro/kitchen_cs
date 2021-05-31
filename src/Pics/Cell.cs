using System;

namespace Pics
{
    public enum Cell {
        Ground,
        Snow,
        Brush,
        Step,
        SnowMouse,
        GroundMouse
    }
    static class Extensions {
        public static string CellName(this Cell input) {
            switch(input) {
                case Cell.Ground: return ".";
                case Cell.Snow: return "#";
                case Cell.Brush: return "o";
                case Cell.Step: return "+";
                case Cell.SnowMouse: return "X";
                case Cell.GroundMouse: return "Y";
            }
            throw new ArgumentException($"Unknown Cell value: {input}");
        }

    }
}