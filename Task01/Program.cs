using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
            int[] arr = null;
            try
            {
                // Попробуйте осуществить считывание целочисленного массива, записав это ОДНИМ ВЫРАЖЕНИЕМ.
                arr = (from s in Regex.Replace(Console.ReadLine(), "[ ]+", " ").Trim().Split()
                       select int.Parse(s)).ToArray();
                
                if (arr.Length == 0)
                    throw new InvalidOperationException();
            }
            catch (FormatException)
            {
                Console.WriteLine("FormatException");
                return;
            }
            catch (OverflowException)
            {
                Console.WriteLine("OverflowException");
                return;
            }
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
                PrintEnumerableCollection<int>(arrQuery, ":");
                PrintEnumerableCollection<int>(arrMethod, "*");
            }
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
            Console.WriteLine(Array.ConvertAll<T, string>(collection.ToArray(), (x) => x.ToString()).Aggregate((x, y) => x + separator + y));
        }
    }
}
