using System;
using System.Collections.Generic;

namespace PostalService.Helpers
{
    public static class Utils
    {

        // формирование случайных целых чисел в заданном диапазоне (lo, hi),
        // исключая указанное параметром exclude число
        public static int GetRandomExclude(int lo, int hi, int exclude)
        {
            int number = 0;
            do
                number = Random.Next(lo, hi);
            while (number == exclude);

            return number;
        } // GetRandomExclude

        // объект для получения случайных чисел
        public static readonly Random Random = new Random(Environment.TickCount);

        // Получение случайного числа
        // краткая форма записи метода - это не лямбда выражение
        public static int GetRandom(int lo, int hi) => Random.Next(lo, hi + 1);
        public static double GetRandom(double lo, double hi) => lo + (hi - lo) * Random.NextDouble();

        



    }
}
