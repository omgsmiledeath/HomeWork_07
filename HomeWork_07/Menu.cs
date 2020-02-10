

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

        public Menu(string path, bool newBook)
        {
            this.newBook = newBook;
            if (newBook)
            {
                this.Titles = new string[] { "Заголовок", "Важность", "Текст записки", "Дата записи", "Владелец записи" };
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
            while (true)
                if (File.Exists(path))
                {
                    Console.WriteLine(@"По указанному пути есть файл , хотите перезаписать (да\нет)?");
                    string temp = Console.ReadLine();
                    if (temp == "да")
                    {
                        createDir(path);
                        break;
                    }
                    else if(temp == "нет")
                    {
                        Console.WriteLine("Файл не сохранен");
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
            using (StreamWriter sr = fi.CreateText()) ;
            SaveInFile();
        }

        public void Import()
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

        private List<Record> inputRecords(string path)
        {
            List<Record> result = new List<Record>();
            using (StreamReader sr = new StreamReader(path))
            {
                Titles = sr.ReadLine().Split(';');
                while (!sr.EndOfStream)
                {
                    string[] temp = sr.ReadLine().Split(';');

                    result.Add(new Record(temp[0], temp[1], temp[2], Convert.ToDateTime(temp[3]), temp[4]));
                }
            }
            return result;
        }


        public void PrintNBinConsole()
        {
            int count = 1;
            foreach (var e in this.notebook.ExportData())
            {
                Console.WriteLine($"Запись номер {count++}");
                Console.Write($"1.{Titles[0]}: {e.Title} \n");
                Console.Write($"2.{Titles[1]}: {e.Relevance} \n");
                Console.Write($"3.{Titles[2]}: {e.Text} \n");
                Console.Write($"4.{Titles[3]}: {e.WriteDate} \n");
                Console.Write($"5.{Titles[4]}: {e.RecordOwner} \n");
                Console.WriteLine("-------------------------------------------");
            }
        }

        public void PrintRecordinConsole(int count)
        {
            Record temp = notebook.getRecord(count);
            Console.WriteLine($"Выделенна запись номер {count+1}");
            Console.Write($"1.{Titles[0]}: {temp.Title} \n");
            Console.Write($"2.{Titles[1]}: {temp.Relevance} \n");
            Console.Write($"3.{Titles[2]}: {temp.Text} \n");
            Console.Write($"4.{Titles[3]}: {temp.WriteDate} \n");
            Console.Write($"5.{Titles[4]}: {temp.RecordOwner} \n");
            Console.WriteLine("-------------------------------------------");
        }

        public void DeleteRecord()
        {
            int count;
            while (true)
            {
                Console.Write("Введите номер записи который хотите удалить:");
                if (Int32.TryParse(Console.ReadLine(), out count))
                {
                    if (count-1 > notebook.ExportData().Count)
                    {
                        Console.WriteLine($"записи {count} не существует");
                    }
                    else
                    {
                        notebook.Delete(count - 1);
                        break;
                    }
                }
            }
        }

        public void EditRecord()
        {
            int count;
            while (true)
            {
                Console.Write("Введите номер записи который хотите отредактировать:");
                if (Int32.TryParse(Console.ReadLine(), out count))
                {
                    if (count-1 > notebook.ExportData().Count)
                    {
                        Console.WriteLine($"записи {count} не существует");
                    }
                    else
                    {
                        int choise=100;
                        PrintRecordinConsole(count-1);
                        Record tempRecord = notebook.getRecord(count - 1);
                        while (true)
                        {
                            Console.Write("Введите номер пункта записи который хотите изменить(1-5) либо введите 9 если редактирование завершено:");
                            if (Int32.TryParse(Console.ReadLine(), out choise))
                            {
                                if (choise == 9)
                                {
                                    notebook.Edit(count - 1, tempRecord);
                                    notebook.getRecord(count - 1);
                                    break;

                                }
                                else
                                {
                                    switch (choise)
                                    {
                                        case 1:
                                            Console.WriteLine("Введите новый заголовок:");
                                            tempRecord.Title = Console.ReadLine();
                                            break;
                                        case 2:
                                            Console.WriteLine("Введите новую важность для записи:");
                                            tempRecord.Relevance = Console.ReadLine();
                                            break;
                                        case 3:
                                            Console.WriteLine("Введите новый текст записи:");
                                            tempRecord.Text = Console.ReadLine();
                                            break;
                                        case 4:
                                            Console.WriteLine("Введите новую дату записи записи:");
                                            tempRecord.WriteDate = Convert.ToDateTime(Console.ReadLine());
                                            break;
                                        case 5:
                                            Console.WriteLine("Введите имя:");
                                            tempRecord.RecordOwner = Console.ReadLine();
                                            break;
                                    }
                                }
                            }                        
                        }
                        break;
                    }
                }
            }
        }

        public void SortRecords()
        {
            List<Record> temp = new List<Record>();
            temp = notebook.ExportData();
            notebook.ClearRecord();
            bool flag = false;
            while (flag == false)
            {
                Console.WriteLine("Введите номера пункта у записи по которому хотите сортировать");
                int count;
                Int32.TryParse(Console.ReadLine(), out count);
                switch (count)
                {
                    case 1:
                        temp.Sort((a, b) => a.Title.CompareTo(b.Title));
                        notebook.InputData(temp);
                        flag = true;
                        break;
                    case 2:
                        temp.Sort((a, b) => a.Relevance.CompareTo(b.Relevance));
                        notebook.InputData(temp);
                        flag = true;
                        break;
                    case 3:
                        temp.Sort((a, b) => a.Text.CompareTo(b.Text));
                        notebook.InputData(temp);
                        flag = true;
                        break;
                    case 4:
                        temp.Sort((a, b) => a.WriteDate.CompareTo(b.WriteDate));
                        notebook.InputData(temp);
                        flag = true;
                        break;
                    case 5:
                        temp.Sort((a, b) => a.RecordOwner.CompareTo(b.RecordOwner));
                        notebook.InputData(temp);
                        flag = true;
                        break;
                    default:
                        Console.WriteLine("Значение не распознанно");
                        break;

                }   
            }
        }
    }
}