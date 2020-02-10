

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
                this.Titles = new string[]{"Заголовок","Важность","Текст записки","Дата записи","Владелец записи"};
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
                sw.WriteLine($"{Titles[0]};{Titles[1]};{Titles[2]};{Titles[3]:yyyy MM dd};{Titles[4]}");
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


        public void SaveInAnotherFile(string path)
        {            
            while(true)
            if(File.Exists(path))
                {
                Console.WriteLine("По указанному пути есть файл , хотите перезаписать?");
                    if(Console.ReadLine() =="да")
                    {
                        createDir(path);
                        break;
                    }
                    else
                        {
                        Console.WriteLine("Ввод не опознан");
                        }
                }
            else
            {
                    createDir(path);
                    break;
            }
        }

        private void createDir(string path)
        {
            this.path = path;
            FileInfo fi = new FileInfo(this.path);
            DirectoryInfo di = new DirectoryInfo(fi.DirectoryName);
            di.Create();
            using(StreamWriter sr = fi.CreateText());
            SaveInFile();
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


        public void PrintNBinConsole()
        {
            int count = 1;
            foreach(var e in this.notebook.ExportData())
            {    
                Console.WriteLine($"Запись номер {count++}");
                Console.Write($"{Titles[0]}: {e.Title} \n");
                Console.Write($"{Titles[1]}: {e.Relevance} \n");
                Console.Write($"{Titles[2]}: {e.Text} \n");
                Console.Write($"{Titles[3]}: {e.WriteDate} \n");
                Console.Write($"{Titles[4]}: {e.RecordOwner} \n");
                Console.WriteLine("-------------------------------------------");
            }
        }
    }
}