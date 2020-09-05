using System;

namespace Zaac.CSNaturalMouseMotion.Tests.Util
{
    public static class ArrayExtensions
    {
        public static void Fill<T>(this T[] destinationArray, T value)
        {
            if (destinationArray == null)
            {
                throw new ArgumentNullException(nameof(destinationArray));
            }

            destinationArray[0] = value;
            FillInternal(destinationArray, 1);
        }

        public static void Fill<T>(this T[] destinationArray, T[] values)
        {
            if (destinationArray == null)
            {
                throw new ArgumentNullException(nameof(destinationArray));
            }

            var copyLength = values.Length;
            var destinationArrayLength = destinationArray.Length;

            if (copyLength == 0)
            {
                throw new ArgumentException("Параметр должен содержать хотя бы одно значение.", nameof(values));
            }

            if (copyLength > destinationArrayLength)
            {
                // значение для копирования длиннее, чем место назначения,
                // поэтому заполните место назначения первой частью значения
                Array.Copy(values, destinationArray, destinationArrayLength);
                return;
            }

            Array.Copy(values, destinationArray, copyLength);

            FillInternal(destinationArray, copyLength);
        }

        private static void FillInternal<T>(this T[] destinationArray, int copyLength)
        {
            var destinationArrayLength = destinationArray.Length;
            var destinationArrayHalfLength = destinationArrayLength / 2;

            // циклическое копирование от начала массива до текущей позиции,
            // удваивающее длину копии с каждым проходом
            for (; copyLength < destinationArrayHalfLength; copyLength *= 2)
            {
                Array.Copy(
                    sourceArray: destinationArray,
                    sourceIndex: 0,
                    destinationArray: destinationArray,
                    destinationIndex: copyLength,
                    length: copyLength);
            }

            // мы прошли половину пути, это означает, что остается только одна копия,
            // точно заполняющая остаток массива
            Array.Copy(
                sourceArray: destinationArray,
                sourceIndex: 0,
                destinationArray: destinationArray,
                destinationIndex: copyLength,
                length: destinationArrayLength - copyLength);
        }
    }
}
