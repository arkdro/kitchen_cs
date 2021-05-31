using System;

namespace Pics
{
    public enum Cell {
        Ground,
        Food,
        Animal
    }
    static class Extensions {
        public static string CellName(this Cell input) {
            switch(input) {
                case Cell.Ground: return ".";
                case Cell.Food: return "o";
                case Cell.Animal: return "X";
            }
            throw new ArgumentException($"Unknown Cell value: {input}");
        }

    }
}