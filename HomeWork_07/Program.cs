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

            Menu menu = new Menu(path, false);


           // menu.AddRecord();
           // menu.Import();
            menu.SaveInFile();
            menu.PrintNBinConsole();
            menu.SortRecords();
            //menu.EditRecord();
            ////menu.DeleteRecord();
            menu.PrintNBinConsole();
            menu.SaveInAnotherFile(@"C:\temp\save2.csv");
            
            Console.ReadKey();
        }
    }
}
