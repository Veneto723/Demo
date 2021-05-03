using System;

namespace utils {
    public class Utils {

        public static int NotNegative(int number) {
            return number < 0 ? 0 : number;
        }

        public static double NotNegative(double number) {
            return number < 0 ? 0 : number;
        }

        public static int Below(int num, int max) {
            return Math.Min(num, max);
        }

        public static double Floor(double number) {
            return Math.Floor(number);
        }

        public static double Round(double number) {
            return Math.Round(number);
        }
    }
}