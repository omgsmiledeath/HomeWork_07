using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_07
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Задание
            /// Разработать ежедневник.
            /// В ежедневнике реализовать возможность 
            /// - создания
            /// - удаления
            /// - реактирования 
            /// записей
            /// 
            /// В отдельной записи должно быть не менее пяти полей
            /// 
            /// Реализовать возможность 
            /// - Загрузки даннах из файла
            /// - Выгрузки даннах в файл
            /// - Добавления данных в текущий ежедневник из выбранного файла
            /// - Импорт записей по выбранному диапазону дат
            /// - Упорядочивания записей ежедневника по выбранному полю
            #endregion
            Console.WriteLine("Вас приветствует записная книжка v0.123 (beta) ранний доступ");

            Console.WriteLine($"Выберите режим запуска книги:\n 1.Новая книга (по умолчанию хранится в"+ @"(c:\temp\NoteBook3000.csv)"+"\n 2.Готовая книга (укажите полный путь до нее)");
            int choise;
            bool newBook;

            string path = firstPage(out newBook);
            Menu menu = new Menu(path, newBook);

            bool exit = false;
            while(exit==false)
            {
                Console.Clear();
                Console.WriteLine($"Выберите пункт меню:\n 1.Работа с записями\n 2.Загрузка записей\n 3.Сохранение записей\n 4.Сортировка записей\n 5.Вывод всей записной книги на экран\n   0.Выход");

                Int32.TryParse(Console.ReadLine(), out choise);

                switch(choise)
                {
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 0:
                        exit = true;
                        break;

                    default:
                        
                        break;

                }

            }



           //// menu.AddRecord();
           //// menu.Import();
           // menu.SaveInFile();
           // menu.PrintNBinConsole();
           // menu.SortRecords();
           // //menu.EditRecord();
           // ////menu.DeleteRecord();
           // menu.PrintNBinConsole();
           // menu.SaveInAnotherFile(@"C:\temp\save2.csv");
            
            Console.ReadKey();
        }

        static string firstPage(out bool newBook)
        {
            int choise;
            string path;
            newBook = true;
            while (true)
            {
                Int32.TryParse(Console.ReadLine(), out choise);
                if (choise == 1)
                {
                    path = @"c:\temp\NoteBook3000.csv";
                    break;
                }
                if (choise == 2)
                {
                    Console.Write("Введите путь до файла:");
                    path = Console.ReadLine();
                    newBook = false;
                    break;
                }
                else Console.WriteLine("Ввод не опознан");
            }
            return path;
        }

        static void MainPage(Menu menu)
        {

        }
    }
}
