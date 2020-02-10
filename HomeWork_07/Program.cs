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
            string path=@"c:\temp\temp.csv";
            //FileInfo fi;
            //do
            //{
            //    Console.WriteLine("Введите путь по которому сохранится записная книга");
            //    path = Console.ReadLine();
            //    fi = new FileInfo(path);
            //} while (!fi.Exists);

            Menu menu = new Menu(path, true);

            menu.notebook.Add(new Record("досвидания", "не очень", "бла бла бла бла бла ", new DateTime(2011, 02, 02), "Вася"));
            menu.notebook.Add(new Record("Привет", "очень", "бла бла", new DateTime(2018, 02, 24), "Петя"));
            menu.notebook.Add(new Record("Просто запись", "важное", "бла бла  бла", new DateTime(2018, 12, 08), "Карл"));
            menu.notebook.Add(new Record("вот", "пофиг", "бла бла бла бла", new DateTime(2004, 01, 01), "123"));
            
            //menu.Import();
            menu.SaveInFile();
            // menu.notebook.Add(new Record("Привет", "очень", "удали меня", new DateTime(2020, 02, 02), "1"));
            //menu.notebook.Add(new Record("Привет", "очень", "бла бла бла", new DateTime(2020, 02, 02), "1"));
            menu.PrintNBinConsole();
            menu.SortRecords();
            //menu.EditRecord();
            ////menu.DeleteRecord();
            menu.PrintNBinConsole();
            menu.SaveInAnotherFile(@"C:\temp\save.csv");
            
            Console.ReadKey();
        }
    }
}
