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
            MainPage(menu);

        }
        /// <summary>
        /// Начальный выбор 
        /// </summary>
        /// <param name="newBook">Параметр для определения новая книга или нет</param>
        /// <returns>Путь к файлу</returns>
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
        /// <summary>
        /// Метод который выводит на экран главное меню
        /// </summary>
        /// <param name="menu">экемлпяр структуры которая хранит в себе все записи</param>
        static void MainPage(Menu menu)
        {
            
            int choise;
            bool exit = false;
            while(exit==false)
            {
                Console.Clear();
                Console.WriteLine($"Выберите пункт меню:\n 1.Работа с записями\n 2.Загрузка записей\n 3.Сохранение записей\n 4.Сортировка записей\n 5.Вывод всей записной книги на экран\n   0.Выход");

                Int32.TryParse(Console.ReadLine(), out choise);

                switch(choise)
                {
                    case 1:
                        RecordEditPage(menu);
                        break;
                    case 2:
                        LoadRecordPage(menu);
                        break;
                    case 3:
                        SaveRecordPage(menu);
                        break;
                    case 4:
                        menu.SortRecords();
                        break;
                    case 5:
                        menu.PrintNBinConsole();
                        Console.WriteLine("Нажмите любую клавишу для продолжения");
                        Console.ReadKey();
                        break;
                    case 0:
                        exit = true;
                        break;

                    default:
                        
                        break;

                }

            }
        }
        /// <summary>
        /// Метод вывода на экран меню для действий с записями
        /// </summary>
        /// <param name="menu">экемлпяр структуры которая хранит в себе все записи</param>
        static void RecordEditPage(Menu menu)
        {
            Console.Clear();
            int choise;
            Console.WriteLine("Ваша записная книга :");
            menu.PrintNBinConsole();
          
            bool flag = false;
            while (flag == false)
            {
                Console.WriteLine("Выберите пункт взаимодействия с записями:\n 1.Добавить запись\n 2.Удалить запись\n 3.Редактировать запись\n 4.Вернуться в меню");
                Int32.TryParse(Console.ReadLine(), out choise);

                switch (choise)
                {
                    case 1:
                        menu.AddRecord();
                        
                        break;
                    case 2:
                        menu.DeleteRecord();
                        break;
                    case 3:
                        menu.EditRecord();
                        break;
                    case 4:
                        flag = true;
                        break;
                    default:
                        Console.WriteLine("Выбор не определен");
                        break;
                }
            }
        }
        /// <summary>
        /// Метод выводящий на экран меню для загрузки данных
        /// </summary>
        /// <param name="menu">экемлпяр структуры которая хранит в себе все записи</param>
        static void LoadRecordPage(Menu menu)
        {
            int choise;
           
            bool flag = false;
            while (flag == false)
            {
                Console.WriteLine("Выберите пункт загрузки записей:\n 1.Загрузить другую книгу\n 2.Загрузить и добавить записи к текущей книге\n 3.Загрузить записи по по выбранному диапозону дат\n 4.Вернуться в меню");
                Int32.TryParse(Console.ReadLine(), out choise);

                switch (choise)
                {
                    case 1:
                        menu.notebook.ClearRecord();
                        menu.Import(1);
                        break;
                    case 2:
                        menu.Import(1);
                        break;
                    case 3:
                        Console.WriteLine("Введите минимальную дату для импорта записей");
                        DateTime? min = Convert.ToDateTime(Console.ReadLine());
                        Console.WriteLine("Введите максимальную дату для импорта записей");
                        DateTime? max = Convert.ToDateTime(Console.ReadLine());
                        menu.Import(2, min, max);
                        break;
                    case 4:
                        flag = true;
                        break;
                    default:
                        Console.WriteLine("Выбор не определен");
                        break;
                }
            }
        }
        /// <summary>
        /// Метод выводящий на экран меню для сохранения данных
        /// </summary>
        /// <param name="menu">экемлпяр структуры которая хранит в себе все записи</param>
        static void SaveRecordPage(Menu menu)
        {
            int choise;
            string path = menu.path;
            bool flag = false;
            while (flag == false)
            {
                Console.WriteLine($"Выберите пункт сохранения записей:\n 1.Сохранить в текущем файле({path})\n 2.Сохранить в другом файле \n 4.Вернуться в меню");
                Int32.TryParse(Console.ReadLine(), out choise);

                switch (choise)
                {
                    case 1:
                        menu.SaveInFile();
                        break;
                    case 2:
                        Console.WriteLine("Введите путь до файла");
                        path = Console.ReadLine();
                        menu.SaveInAnotherFile(path);
                        break;
                    
                    case 4:
                        flag = true;
                        break;
                    default:
                        Console.WriteLine("Выбор не определен");
                        break;
                }
            }
        }
    }
}
