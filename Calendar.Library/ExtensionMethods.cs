using System;
using System.Collections.Generic;
using System.Text;

namespace Calendar.Library
{
    static class ExtensionMethods
    {
        /// <summary>
        /// Fills each item an array with specified value.
        /// </summary>
        /// <param name="array">Arrya to fill values in.</param>
        /// <param name="value">Fill value.</param>
        /// <returns>Array with each value replaced by fill value.</returns>
        public static T[] Fill<T>(this T[] array, T value)
        {
            for (var i = 0; i < array.Length; i++)
            {
                array[i] = value;
            }

            return array;
        }


        /// <summary>
        /// Creates a shallow copy of a range of elements in the source array.
        /// Similar to <see cref="List{T}.GetRange(int, int)"/>
        /// </summary>
        /// <typeparam name="T">Type of the array.</typeparam>
        /// <param name="array">Array to extract elements from.</param>
        /// <param name="start">The zero-based array index at which the range starts.</param>
        /// <param name="length">The number of elements in the range.</param>
        /// <returns>A shallow copy of a range of elements in the source array.</returns>
        public static T[] GetRange<T>(this T[] array, int start, int length)
        {
            var result = new T[length];

            Array.Copy(array, start, result, 0, length);

            return result;
        }

        /// <summary>
        /// Creates a shallow copy of an array.
        /// </summary>
        /// <typeparam name="T">Type of the array.</typeparam>
        /// <param name="array">Array to create a shallow copy of.</param>
        /// <returns>A shallow copy of the array.</returns>
        public static T[] Clone<T>(this T[] array) => array.GetRange(0, array.Length);

        /// <summary>
        /// Utility wrapper for Math.Floor that returns an int
        /// </summary>
        /// <param name="value">Value to floor.</param>
        /// <returns>Floored value as an int.</returns>
        public static int Floor(this double value) => (int)Math.Floor(value);

        /// <summary>
        /// Proper modulo function given that % is remainder in C#.
        /// </summary>
        /// <param name="value">Value to mod.</param>
        /// <param name="modulo">Value to mod by.</param>
        /// <returns>Value % mod.</returns>
        public static int Modulo(this int value, int modulo) => ((value % modulo) + modulo) % modulo;

        /// <summary>
        /// Centers text in a string of specified width.
        /// </summary>
        /// <param name="text">Text to center.</param>
        /// <param name="width">Final desired width of output string.</param>
        /// <returns>String with centered text.</returns>
        public static string Center(this string text, int width)
        {
            var padSize = width - text.Length;

            var leftPad = padSize / 2;
            var rightPad = padSize / 2;

            if (leftPad + text.Length + rightPad < width)
            {
                rightPad++;
            }

            return $"{' '.Repeat(leftPad)}{text}{' '.Repeat(rightPad)}";
        }

        /// <summary>
        /// Creates a string by repeating a char a specified number of times.
        /// </summary>
        /// <param name="c">Char to repeat.</param>
        /// <param name="count">How often to repeat the char.</param>
        /// <returns>String with repeated char.</returns>
        public static string Repeat(this char c, int count)
        {
            var sb = new StringBuilder();

            for (var i = 0; i < count; i++)
            {
                sb.Append(c);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Splits an array into chunkSized chunks.
        /// </summary>
        /// <param name="array">Array to split.</param>
        /// <param name="chunkSize">Size of each chunk.</param>
        /// <returns>Chunked up array.</returns>
        public static IEnumerable<string[]> Split(this string[] array, int chunkSize)
        {
            var i = 0;
            while (i < array.Length)
            {
                var chunk = new string[chunkSize];

                for (var j = 0; j < chunkSize && i + j < array.Length; j++)
                {
                    chunk[j] = array[i + j];
                }

                i += chunkSize;

                yield return chunk;
            }
        }
    }
}
