using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Fill.Test")]
namespace Fill {

    internal enum State {
        Old,
        New
    }

    internal readonly struct Point {
        internal readonly State state;
        internal readonly int color;
        internal Point(State state, int color) {
            this.state = state;
            this.color = color;
        }
    }

    internal readonly struct QueuePoint {
        internal readonly State state;
        internal readonly int x;
        internal readonly int y;
        internal QueuePoint(State state, int x, int y) {
            this.state = state;
            this.x = x;
            this.y = y;
        }
    }

    public class Fill {
        public int[,] fill(int[,] input, int start_x, int start_y, int target_color, int replacement_color) {
            var work = copy_input_data(input, target_color);
            filler(work, start_x, start_y, target_color, replacement_color);
            var result = extract_result(work);
            return result;
        }

        private void filler(Point[,] data, int start_x, int start_y, int target_color, int replacement_color) {
            if (!inside(data, start_x, start_y, target_color, replacement_color)) {
                return;
            }
            var queue = new Queue<QueuePoint>();
            queue.Enqueue(new QueuePoint(State.Old, start_x, start_y));
            while(queue.Count > 0) {
                process_one_item(data, queue, target_color, replacement_color);
            }
        }

        private void process_one_item(Point[,] data, Queue<QueuePoint> queue, int target_color, int replacement_color) {
            var item = queue.Dequeue();
            Console.WriteLine($"process_one_item, x: {item.x}, y: {item.y}, st: {item.state}");
            var left_x = item.x;
            while(left_x > 0 && inside(data, left_x - 1, item.y, target_color, replacement_color)) {
                Console.WriteLine($"process_one_item, left, left_x: {left_x}, y: {item.y}");
                set(data, left_x - 1, item.y, target_color, replacement_color);
                left_x--;
            }
            var width = data.GetLength(1);
            var right_x = item.x;
            while(right_x < width && inside(data, right_x, item.y, target_color, replacement_color)) {
                Console.WriteLine($"process_one_item, right, right_x: {right_x}, y: {item.y}");
                set(data, right_x, item.y, target_color, replacement_color);
                right_x++;
            }
            Console.WriteLine($"process_one_item, scan up, y: {item.y + 1}");
            scan(data, left_x, right_x - 1, item.y + 1, queue, target_color, replacement_color);
            Console.WriteLine($"process_one_item, scan down, y: {item.y - 1}");
            scan(data, left_x, right_x - 1, item.y - 1, queue, target_color, replacement_color);
        }

        private bool inside(Point[,] data, int x, int y, int target_color, int replacement_color) {
            Console.WriteLine($"inside, get, x: {x}, y: {y}");
            var point = data[y, x];
            if (point.state == State.Old && point.color == target_color) {
                Console.WriteLine($"inside, x: {x}, y: {y}, c: {point.color}, true");
                return true;
            } else {
                Console.WriteLine($"inside, x: {x}, y: {y}, c: {point.color}, false");
                return false;
            }
        }

        private void set(Point[,] data, int x, int y, int target_color, int replacement_color) {
            var point = data[y, x];
            var new_point = new Point(State.New, replacement_color);
            Console.WriteLine($"set, x: {x}, y: {y}, c: {point.color}");
            data[y, x] = new_point;
        }

        private void scan(Point[,] data, int left, int right, int y, Queue<QueuePoint> queue, int target_color, int replacement_color) {
            int height = data.GetLength(0);
            if (y < 0 || y >= height) {
                return;
            }
            bool added = false;
            for (int x = left; x <= right; x++) {
                if(!inside(data, x, y, target_color, replacement_color)) {
                    added = false;
                } else if (!added) {
                    Console.WriteLine($"scan, enqueue, x: {x}, y: {y}");
                    queue.Enqueue(new QueuePoint(State.Old, x, y));
                    added = true;
                }
            }
        }

        private Point[,] copy_input_data(int[,] input, int target_color) {
            int height = input.GetLength(0);
            int width = input.GetLength(1);
            Point[,] work = new Point[height, width];
            for (int y = 0; y < height; y++) {
                Console.Write($"copy_input_data, y: {y}, ");
                for (int x = 0; x < width; x++) {
                    Console.Write($"x: {x}, ");
                    var point = new Point(State.Old, input[y, x]);
                    Console.Write($"c: {point.color}; ");
                    work[y, x] = point;
                }
                Console.WriteLine("");
            }
            return work;
        }

        internal int[,] extract_result(Point[,] data) {
            int height = data.GetLength(0);
            int width = data.GetLength(1);
            int[,] work = new int[height, width];
            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    var point = data[y, x];
                    work[y, x] = point.color;
                }
            }
            return work;
        }
    }
}
