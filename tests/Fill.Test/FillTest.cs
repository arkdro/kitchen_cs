using NUnit.Framework;

using System;
using System.Threading;

using Fill;

namespace Fill.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [Test]
        public void FillTest01()
        {
            var f = new Fill();
            var data = new int[,] {{11,12}, {11,14}, {11,11}, {17,11}};
            var new_data = f.fill(data, 1, 1, 14, 21);
            var expected = new int[,] {{11,12}, {11,21}, {11,11}, {17,11}};
            int height = data.GetLength(0);
            int width = data.GetLength(1);
            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    Assert.AreEqual(expected[y, x], new_data[y, x], $"expected: {expected[y, x]}, actual: {new_data[y, x]}, x: {x}, y: {y}");
                }
            }
        }

        [Test]
        public void FillTest02_fill_small_region()
        {
            var f = new Fill();
            var data = new int[,] {
                {11, 10, 11, 11},
                {11, 10, 13, 11},
                {11, 11, 11, 11},
                {12, 17, 17, 14},
                {12, 12, 11, 11}
            };
            var new_data = f.fill(input: data, start_x: 2, start_y: 4, target_color: 11, replacement_color: 21);
            var expected = new int[,] {
                {11, 10, 11, 11},
                {11, 10, 13, 11},
                {11, 11, 11, 11},
                {12, 17, 17, 14},
                {12, 12, 21, 21}
            };
            int height = data.GetLength(0);
            int width = data.GetLength(1);
            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    Assert.AreEqual(expected[y, x], new_data[y, x], $"expected: {expected[y, x]}, actual: {new_data[y, x]}, x: {x}, y: {y}");
                }
            }
        }

        [Test]
        public void FillTest03_fill_big_region()
        {
            var f = new Fill();
            var data = new int[,] {
                {11, 10, 11, 11},
                {11, 10, 13, 11},
                {11, 11, 11, 11},
                {12, 17, 17, 14},
                {12, 12, 11, 11}
            };
            var new_data = f.fill(input: data, start_x: 2, start_y: 0, target_color: 11, replacement_color: 21);
            var expected = new int[,] {
                {21, 10, 21, 21},
                {21, 10, 13, 21},
                {21, 21, 21, 21},
                {12, 17, 17, 14},
                {12, 12, 11, 11}
            };
            int height = data.GetLength(0);
            int width = data.GetLength(1);
            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    Assert.AreEqual(expected[y, x], new_data[y, x], $"expected: {expected[y, x]}, actual: {new_data[y, x]}, x: {x}, y: {y}");
                }
            }
        }

        [Test]
        public void FillTest04_fill_region()
        {
            var f = new Fill();
            var data = new int[,] {
                {11, 10, 11, 11, 11, 11, 11, 10},
                {11, 10, 11, 13, 13, 10, 11, 11},
                {11, 10, 11, 13, 11, 11, 10, 11},
                {11, 10, 11, 13, 13, 11, 10, 11},
                {11, 10, 11, 11, 11, 11, 10, 11},
                {11, 11, 17, 12, 12, 17, 17, 11},
                {12, 11, 11, 11, 11, 11, 11, 11}
            };
            var new_data = f.fill(input: data, start_x: 2, start_y: 0, target_color: 11, replacement_color: 21);
            var expected = new int[,] {
                {21, 10, 21, 21, 21, 21, 21, 10},
                {21, 10, 21, 13, 13, 10, 21, 21},
                {21, 10, 21, 13, 21, 21, 10, 21},
                {21, 10, 21, 13, 13, 21, 10, 21},
                {21, 10, 21, 21, 21, 21, 10, 21},
                {21, 21, 17, 12, 12, 17, 17, 21},
                {12, 21, 21, 21, 21, 21, 21, 21}
            };
            int height = data.GetLength(0);
            int width = data.GetLength(1);
            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    Assert.AreEqual(expected[y, x], new_data[y, x], $"expected: {expected[y, x]}, actual: {new_data[y, x]}, x: {x}, y: {y}");
                }
            }
        }

        [Test]
        public void extract_result_test()
        {
            var f = new Fill();
            var data = new Point[,] {
                {new Point(state:State.New, color: 11), new Point(state:State.New, color: 12)},
                {new Point(state:State.New, color: 11), new Point(state:State.New, color: 14)},
                {new Point(state:State.New, color: 11), new Point(state:State.New, color: 11)},
                {new Point(state:State.New, color: 17), new Point(state:State.New, color: 11)},
            };
            var extracted = f.extract_result(data);
            var expected = new int[,] {{11,12}, {11,14}, {11,11}, {17,11}};
            int height = data.GetLength(0);
            int width = data.GetLength(1);
            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    Assert.AreEqual(expected[y, x], extracted[y, x]);
                }
            }
        }

        [Test]
        public void generate_random_data_test()
        {
            // it still results in System.OutOfMemoryException sometimes, but finishes OK.
            int number_of_packs = 25;
            multiple_packs_of_random_data(number_of_packs);
            Assert.Pass();
        }

        public void multiple_packs_of_random_data(int number_of_packs)
        {
            for (int i = 0; i < number_of_packs; i++) {
                Console.Error.WriteLine($"generate_random_data, i = {i}");
                do_bunch_random_data_test();
                if(i > 0 && ((i % 10) == 0)) {
                    Console.Error.WriteLine($"generate_random_data, sleeping, i = {i}");
                    Thread.Sleep(5000);
                }
            }
            Assert.Pass();
        }

        private void do_bunch_random_data_test() {
            int number_of_tests = 100;
            for (int i = 0; i < number_of_tests; i++) {
                do_one_random_data_test();
                if(i > 0 && i % 10 == 0) {
                    Console.Error.WriteLine($"do_bunch_random_data_test, sleeping, i = {i}");
                    Thread.Sleep(200);
                }
            }
        }

        private void do_one_random_data_test()
        {
            int height_min = 3;
            int height_max = 30;
            int width_min = 3;
            int width_max = 30;
            int min_colors = 2;
            int max_colors = 4;
            double prob_of_main_color = 0.55;
            var (area, main_color, initial_x, initial_y) = generate_area(height_min, height_max, width_min, width_max, min_colors, max_colors, prob_of_main_color);
            fill_area(area, main_color, initial_x, initial_y);
            // no exceptions means success
        }

        private void fill_area(int[,] data, int main_color, int start_x, int start_y) {
            var f = new Fill();
            int replacement_color = 127;
            var new_data = f.fill(input: data, start_x: start_x, start_y: start_y, target_color: main_color, replacement_color: replacement_color);
        }

        private (int[,], int, int, int) generate_area(int height_min, int height_max, int width_min, int width_max, int min_colors, int max_colors, double prob_of_main_color) {
            var rnd = new Random();
            int width = rnd.Next(width_min, width_max + 1);
            int height = rnd.Next(height_min, height_max + 1);
            int colors = rnd.Next(min_colors, max_colors + 1);
            int main_color = choose_main_color(rnd, colors);
            int[,] data = new int[height, width];
            int initial_x = 0;
            int initial_y = 0;
            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    int color = get_random_color(main_color, rnd, colors, prob_of_main_color);
                    data[y, x] = color;
                    if(color == main_color) {
                        initial_x = x;
                        initial_y = y;
                    }
                }
            }
            return (data, main_color, initial_x, initial_y);
        }

        private int choose_main_color(Random rnd, int colors) {
            int color = rnd.Next(0, colors);
            return color;
        }

        private int get_random_color(int main_color, Random rnd, int colors, double prob_of_main_color) {
            int color = rnd.Next(0, colors);
            double prob = rnd.NextDouble();
            if (prob < prob_of_main_color) {
                return main_color;
            } else {
                return color;
            }
        }
    }
}
