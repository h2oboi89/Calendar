using System;
using System.Collections.Generic;

namespace Calendar.Tests
{
    static class ExtensionMethods
    {
        public static T[] Concat<T>(this T[] array, T[] other)
        {
            var combined = new T[array.Length + other.Length];

            Array.Copy(array, 0, combined, 0, array.Length);
            Array.Copy(other, 0, combined, array.Length, other.Length);

            return combined;
        }

        public static T[] GetRange<T>(this T[] array, int start, int length)
        {
            var result = new T[length];

            Array.Copy(array, start, result, 0, length);

            return result;
        }

        public static T[] Fill<T>(this T[] array, T value)
        {
            for (var i = 0; i < array.Length; i++)
            {
                array[i] = value;
            }

            return array;
        }

        public static Queue<T> ToQueue<T>(this T[] array)
        {
            return new Queue<T>(array);
        }
    }
}
