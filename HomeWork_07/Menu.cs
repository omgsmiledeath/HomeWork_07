

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HomeWork_07
{
    struct Menu
    {

        private string[] Titles;

        /// <summary>
        /// Поле хранящее экземлпяр записной книги
        /// </summary>
        public NoteBook notebook { get; }
        /// <summary>
        /// Поле показывающее были ли сохранены изменения
        /// </summary>
        private bool changes { get; set; }
        /// <summary>
        /// Путь к файлу
        /// </summary>
        private string path { get; set; }
        /// <summary>
        /// поле показывающая созданна эта книга или загруженна
        /// </summary>
        private bool newBook { get; set; }

        public Menu(string path,bool newBook)
        {
            this.newBook = newBook;
            if (newBook)
            {
                this.Titles = new string[]{ "Заголовок","Важность","Текст записки","Дата записи","Владелец записи"};
                this.changes = true;
                this.path = path;
                this.notebook = new NoteBook(this.newBook);
            }
            else
            {
                this.Titles = new string[0];
                this.changes = true;
                this.path = path;
                this.notebook = new NoteBook(this.newBook);
                this.notebook.InputData(inputRecords(path));
            }
            
        }

        public void SaveInFile()
        {            
                using (StreamWriter sw = new StreamWriter(path, false, Encoding.UTF8))
                {
                sw.WriteLine($"{Titles[0],7};{Titles[1],8};{Titles[2],15};{Titles[3]:yyyy MM dd,8};{Titles[4]}");
                    foreach (var e in notebook.ExportData())
                    {
                        string temp = String.Format("{0};{1};{2};{3};{4};",
                            e.Title,
                         e.Relevance,
                         e.Text,
                         e.WriteDate,
                         e.RecordOwner);
                        sw.WriteLine(temp);
                    }
                }
        }
        



        public void Import ()
        {
            string path = String.Empty;
            while (true)
            {
                Console.WriteLine("Введите путь до файла с записями:");
                path = Console.ReadLine();
                Console.WriteLine(File.Exists(path));
                if (File.Exists(path))
                {
                    notebook.InputData(inputRecords(path));
                    break;
                }
                else
                {
                    Console.WriteLine("По выбранному пути невозможно найти файл");
                }
                    
            }
            
        }

        private List<Record> inputRecords (string path)
        {
            List<Record> result = new List<Record>();
            using (StreamReader sr = new StreamReader(path))
            {
                Titles = sr.ReadLine().Split(';');
                while (!sr.EndOfStream)
                {
                    string[] temp = sr.ReadLine().Split(';');

                    result.Add(new Record(temp[0],temp[1],temp[2],Convert.ToDateTime(temp[3]),temp[4]));
                }
            }
                return result;
        }

    }
}