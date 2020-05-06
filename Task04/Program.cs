using System;
using System.Linq;
using System.Text.RegularExpressions;

/*
 * На вход подается строка, состоящая из целых чисел типа int, разделенных одним или несколькими пробелами.
 * На основе полученных чисел получить новое по формуле: 5 + a[0] - a[1] + a[2] - a[3] + ...
 * Это необходимо сделать двумя способами:
 * 1) с помощью встроенного LINQ метода Aggregate
 * 2) с помощью своего метода MyAggregate, сигнатура которого дана в классе MyClass
 * Вывести полученные результаты на экран (естесственно, они должны быть одинаковыми)
 * 
 * Пример входных данных:
 * 1 2 3 4 5
 * 
 * Пример выходных:
 * 8
 * 
 * 8
 * 
 * Пояснение:
 * 5 + 1 - 2 + 3 - 4 + 5 = 8
 * 
 * 
 * Обрабатывайте возможные исключения путем вывода на экран типа этого исключения 
 * (не использовать GetType(), пишите тип руками).
 * Например, 
 *          catch (SomeException)
            {
                Console.WriteLine("SomeException");
            }
 */

namespace Task04
{
    class Program
    {
        static void Main(string[] args)
        {
            RunTesk04();
        }

        public static void RunTesk04()
        {
            // Создадим массив.
            int[] arr;
            try
            {
                // Попробуйте осуществить считывание целочисленного массива, записав это ОДНИМ ВЫРАЖЕНИЕМ.
                arr = (from s in Regex.Replace(Console.ReadLine(), "[ ]+", " ").Trim().Split()
                       select int.Parse(s)).ToArray();
            }
            // Проверка формата.
            catch (FormatException)
            {
                Console.WriteLine("FormatException");
                return;
            }
            // Проверка переполнения.
            catch (OverflowException)
            {
                Console.WriteLine("OverflowException");
                return;
            }
            // Проверка пустоты.
            catch (InvalidOperationException)
            {
                Console.WriteLine("InvalidOperationException");
                return;
            }

            // использовать синтаксис методов! SQL-подобные запросы не писать!

            // Множитель.
            int mn = 1;

            int arrAggregate = arr.Aggregate(delegate (int x, int y)
            {
                mn = -mn;
                return x + y * mn;
            }) + 5;

            // Собственный класс.
            int arrMyAggregate = MyClass.MyAggregate(arr);

            Console.WriteLine(arrAggregate);
            Console.WriteLine(arrMyAggregate);

        }
    }

    /// <summary>
    /// Собственный класс для агрегации.
    /// </summary>
    static class MyClass
    {
        /// <summary>
        /// Метод для агрегации.
        /// </summary>
        /// <param name="arr">Массив.</param>
        /// <returns>Необходимое по заданию значение.</returns>
        public static int MyAggregate(int[] arr)
        {
            // Множитель.
            int mn = 1;
            // Сумма.
            int sum = 0;
            foreach (var el in arr)
            {
                sum += el * mn;
                mn = -mn;
            }
            return sum + 5;
        }
    }
}
