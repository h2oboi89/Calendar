using System;
using System.Collections.Generic;
using System.Text;

namespace Calendar.Library
{
    static class ExtensionMethods
    {
        public static int Floor(this double value) => (int)Math.Floor(value);

        public static int Modulo(this int value, int modulo) => ((value % modulo) + modulo) % modulo;

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

        public static string Repeat(this char c, int count)
        {
            var sb = new StringBuilder();

            for (var i = 0; i < count; i++)
            {
                sb.Append(c);
            }

            return sb.ToString();
        }

        public static IEnumerable<string[]> Split(this string[] array, int chunkSize)
        {
            var i = 0;
            while (i < array.Length)
            {
                var chunk = new string[chunkSize];
                
                for(var j = 0; j < chunkSize && i + j < array.Length; j++)
                {
                    chunk[j] = array[i + j];
                }

                i += chunkSize;

                yield return chunk;
            }
        }
        
        public static string[] Fill(this string[] array, string text)
        {
            for(var i = 0; i < array.Length; i++)
            {
                array[i] = text;
            }

            return array;
        }

        public static void Equalize(this List<List<string>> list, string text)
        {
            var maxLength = 0;

            foreach(var subList in list)
            {
                if (maxLength < subList.Count)
                {
                    maxLength = subList.Count;
                }
            }

            foreach(var subList in list)
            {
                if (subList.Count < maxLength)
                {
                    subList.Add(text);
                }
            }
        }
    }
}
