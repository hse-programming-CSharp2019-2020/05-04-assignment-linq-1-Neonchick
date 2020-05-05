using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

/* В задаче не использовать циклы for, while. Все действия по обработке данных выполнять с использованием LINQ
 * 
 * На вход подается строка, состоящая из целых чисел типа int, разделенных одним или несколькими пробелами.
 * Необходимо отфильтровать полученные коллекцию, оставив только отрицательные или четные числа.
 * Дважды вывести коллекцию, разделив элементы специальным символом.
 * Остальные указания см. непосредственно в коде!
 * 
 * Пример входных данных:
 * 1 2 3 4 5
 * 
 * Пример выходных:
 * 2:4
 * 2*4
 * 
 * Обрабатывайте возможные исключения путем вывода на экран типа этого исключения 
 * (не использовать GetType(), пишите тип руками).
 * Например, 
 *          catch (SomeException)
            {
                Console.WriteLine("SomeException");
            }
 * В случае возникновения иных нештатных ситуаций (например, в случае попытки итерирования по пустой коллекции) 
 * выбрасывайте InvalidOperationException!
 * 
 */

namespace Task01
{
    class Program
    {
        static void Main(string[] args)
        {
            RunTesk01();
        }

        public static void RunTesk01()
        {
            // Присвоим arr какое-то значение, так как нормальное присваивание в try блоке.
            int[] arr = null;
            try
            {
                // Попробуйте осуществить считывание целочисленного массива, записав это ОДНИМ ВЫРАЖЕНИЕМ.
                arr = (from s in Regex.Replace(Console.ReadLine(), "[ ]+", " ").Trim().Split()
                       select int.Parse(s)).ToArray();
                
                // Проверка на пустой массив.
                if (arr.Length == 0)
                    throw new InvalidOperationException();
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

            // использовать синтаксис запросов!
            IEnumerable<int> arrQuery = from el in arr
                                        where (el < 0 || el % 2 == 0)
                                        select el;

            // использовать синтаксис методов!
            IEnumerable<int> arrMethod = arr.Where<int>(el => (el < 0 || el % 2 == 0));

            try
            {
                // Выводим два раза.
                PrintEnumerableCollection<int>(arrQuery, ":");
                PrintEnumerableCollection<int>(arrMethod, "*");
            }
            // Проверка пустоты.
            catch (InvalidOperationException)
            {
                Console.WriteLine("InvalidOperationException");
                return;
            }
        }

        // Попробуйте осуществить вывод элементов коллекции с учетом разделителя, записав это ОДНИМ ВЫРАЖЕНИЕМ.
        // P.S. Есть два способа, оставьте тот, в котором применяется LINQ...
        public static void PrintEnumerableCollection<T>(IEnumerable<T> collection, string separator)
        {
            Console.WriteLine(collection.Select<T, string>(x => x.ToString()).Aggregate((x, y) => x + separator + y));
        }
    }
}
