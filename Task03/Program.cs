using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*Все действия по обработке данных выполнять с использованием LINQ
 * 
 * Объявите перечисление Manufacturer, состоящее из элементов
 * Dell (код производителя - 0), Asus (1), Apple (2), Microsoft (3).
 * 
 * Обратите внимание на класс ComputerInfo, он содержит поле типа Manufacturer
 * 
 * На вход подается число N.
 * На следующих N строках через пробел записана информация о компьютере: 
 * фамилия владельца, код производителя (от 0 до 3) и год выпуска (в диапазоне 1970-2020).
 * Затем с помощью средств LINQ двумя разными способами (как запрос или через методы)
 * отсортируйте коллекцию следующим образом:
 * 1. Первоочередно объекты ComputerInfo сортируются по фамилии владельца в убывающем порядке
 * 2. Для объектов, у которых фамилии владельцев сопадают, 
 * сортировка идет по названию компании производителя (НЕ по коду) в возрастающем порядке.
 * 3. Если совпадают и фамилия, и имя производителя, то сортировать по году выпуска в порядке возрастания.
 * 
 * Выведите элементы каждой коллекции на экран в формате:
 * <Фамилия_владельца>: <Имя_производителя> [<Год_производства>]
 * 
 * Пример ввода:
 * 3
 * Ivanov 1970 0
 * Ivanov 1971 0
 * Ivanov 1970 1
 * 
 * Пример вывода:
 * Ivanov: Asus [1970]
 * Ivanov: Dell [1971]
 * Ivanov: Dell [1970]
 * 
 * Ivanov: Asus [1970]
 * Ivanov: Dell [1971]
 * Ivanov: Dell [1970]
 * 
 * 
 *  * Обрабатывайте возможные исключения путем вывода на экран типа этого исключения 
 * (не использовать GetType(), пишите тип руками).
 * Например, 
 *          catch (SomeException)
            {
                Console.WriteLine("SomeException");
            }
 * При некорректных входных данных (не связанных с созданием объекта) выбрасывайте FormatException
 * При невозможности создать объект класса ComputerInfo выбрасывайте ArgumentException!
 */
namespace Task03
{
    class Program
    {
        static void Main(string[] args)
        {
            // Сменим кодировку.
            System.Console.OutputEncoding = Encoding.UTF8;
            System.Console.InputEncoding = Encoding.UTF8;

            int N;
            // Создадим список.
            List<ComputerInfo> computerInfoList = new List<ComputerInfo>();
            try
            {
                // Считаем колво элементов.
                N = int.Parse(Console.ReadLine());

                // Считаем элементы.
                for (int i = 0; i < N; i++)
                {
                    var str = Console.ReadLine().Split();
                    // Проверка кол-ва полей.
                    if (str.Length!=3)
                        throw new ArgumentException();
                    computerInfoList.Add(new ComputerInfo()
                    {
                        Owner = str[0],
                        Year = int.Parse(str[1]),
                        ComputerManufacturer = (Manufacturer)int.Parse(str[2])
                    });
                    // Проверка коректности полей.
                    if (computerInfoList.Last().Year < 1970 || computerInfoList.Last().Year > 2020)
                        throw new ArgumentException();
                    if ((int)computerInfoList.Last().ComputerManufacturer < 0 || (int)computerInfoList.Last().ComputerManufacturer > 4)
                        throw new ArgumentException();
                }
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
            catch (ArgumentException)
            {
                Console.WriteLine("ArgumentException");
            }

            // выполните сортировку одним выражением
            var computerInfoQuery = from el in computerInfoList
                                    orderby el.Owner descending,
                                    el.ComputerManufacturer.ToString(),
                                    el.Year descending
                                    select el;

            // Вывод колекции.
            PrintCollectionInOneLine(computerInfoQuery);

            Console.WriteLine();

            // Ыыполните сортировку одним выражением.
            var computerInfoMethods = computerInfoList.OrderByDescending(x => x.Owner)
                .ThenBy(x => x.ComputerManufacturer.ToString()).ThenByDescending(x => x.Year);

            PrintCollectionInOneLine(computerInfoMethods);

        }

        // Выведите элементы коллекции на экран с помощью кода, состоящего из одной линии (должна быть одна точка с запятой)
        public static void PrintCollectionInOneLine(IEnumerable<ComputerInfo> collection)
        {
            Console.WriteLine(collection.ToArray().Select<ComputerInfo, string>(x => $"{x.Owner}: {x.ComputerManufacturer.ToString()} [{x.Year}]")
                .Aggregate((x, y) => x + "\n" + y));
        }
    }

    /// <summary>
    /// Перечисление мануфктур.
    /// </summary>
    enum Manufacturer
    {
        Dell,
        Asus,
        Apple,
        Microsoft
    }

    /// <summary>
    /// Класс информации о компьютере.
    /// </summary>
    class ComputerInfo
    {
        /// <summary>
        /// Владелец.
        /// </summary>
        public string Owner { get; set; }
        /// <summary>
        /// Мануфактура.
        /// </summary>
        public Manufacturer ComputerManufacturer { get; set; }
        /// <summary>
        /// Год выпуска.
        /// </summary>
        public int Year { get; set; }
    }
}
