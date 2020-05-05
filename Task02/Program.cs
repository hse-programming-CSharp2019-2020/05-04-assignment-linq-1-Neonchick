using System;
using System.Linq;
using System.Text.RegularExpressions;

/* В задаче не использовать циклы for, while. Все действия по обработке данных выполнять с использованием LINQ
 * 
 * На вход подается строка, состоящая из целых чисел типа int, разделенных одним или несколькими пробелами.
 * Необходимо оставить только те элементы коллекции, которые предшествуют нулю, или все, если нуля нет.
 * Дважды вывести среднее арифметическое квадратов элементов новой последовательности.
 * Вывести элементы коллекции через пробел.
 * Остальные указания см. непосредственно в коде.
 * 
 * Пример входных данных:
 * 1 2 0 4 5
 * 
 * Пример выходных:
 * 2,500
 * 2,500
 * 1 2
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
 */
namespace Task02
{
    class Program
    {
        static void Main(string[] args)
        {
            RunTesk02();
        }

        public static void RunTesk02()
        {
            // Переключаем локаль, чтоб была не точка, а запятая в вещественных числах.
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ru");

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

            var filteredCollection = arr.TakeWhile<int>(x => x != 0);
           
            try
            {
                // использовать статическую форму вызова метода подсчета среднего
                double averageUsingStaticForm = System.Linq.Enumerable.Average(filteredCollection.Select((x) => checked (x * x)));
                // использовать объектную форму вызова метода подсчета среднего
                double averageUsingInstanceForm = filteredCollection.Select((x) => x * x).Average();

                // Выведем среднее.
                Console.WriteLine($"{averageUsingStaticForm:f3}");
                Console.WriteLine($"{averageUsingInstanceForm:f3}");

                // вывести элементы коллекции в одну строку
                Console.WriteLine(filteredCollection.ToArray().Select<int, string>(x => x.ToString()).Aggregate((x, y) => x + ' ' + y));
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
        }
    }
}
