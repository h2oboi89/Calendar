using System;
using System.Collections.Generic;
using System.Text;

namespace Calendar.Library
{
    static class ExtensionMethods
    {
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

        /// <summary>
        /// Fills each item an array with specified value.
        /// </summary>
        /// <param name="array">Arrya to fill values in.</param>
        /// <param name="text">Fill value.</param>
        /// <returns>Array with each value replaced by fill value.</returns>
        public static string[] Fill(this string[] array, string text)
        {
            for (var i = 0; i < array.Length; i++)
            {
                array[i] = text;
            }

            return array;
        }

        /// <summary>
        /// Increases length of each sublist in a list so that all are the same length.
        /// </summary>
        /// <param name="list">List of sublists.</param>
        /// <param name="text">Value to append to each sublist if not maximum length.</param>
        public static void Equalize(this List<List<string>> list, string text)
        {
            var maxLength = 0;

            foreach (var subList in list)
            {
                if (maxLength < subList.Count)
                {
                    maxLength = subList.Count;
                }
            }

            foreach (var subList in list)
            {
                if (subList.Count < maxLength)
                {
                    subList.Add(text);
                }
            }
        }
    }
}
