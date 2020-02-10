

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HomeWork_07
{
    struct Menu
    {
        /// <summary>
        /// Массив заголовков для пунктов записной книги
        /// </summary>
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
        public string path { get; private set; }
        /// <summary>
        /// поле показывающая созданна эта книга или загруженна
        /// </summary>
        private bool newBook { get; set; }

        
        /// <summary>
        /// Конструктор структуры
        /// </summary>
        /// <param name="path">Путь до файла где будут хранится записи</param>
        /// <param name="newBook">Новая книга или нет</param>
        public Menu(string path, bool newBook)
        {
            this.newBook = newBook;
            if (newBook)
            {
                this.Titles = new string[] { "Заголовок", "Важность", "Текст записки", "Дата записи", "Владелец записи" };
                this.changes = true;
                this.path = path;
                this.notebook = new NoteBook(this.newBook);
                SaveInFile();
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

        /// <summary>
        /// Свойство сохраняет записи по актуальному пути
        /// </summary>
        public void SaveInFile()
        {
            using (StreamWriter sw = new StreamWriter(path, false,Encoding.UTF32))
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

        /// <summary>
        /// Свойство которое проверяет условия для записи по указанному пути с возможность перезаписи
        /// </summary>
        /// <param name="path">Путь до места где будут сохранен файл</param>
        /// <param name="apend">Параметр указывающий будет ли перезапись конечного файла</param>
        public void SaveInAnotherFile(string path,bool apend=false)
        {
            while (true)
                if (File.Exists(path))
                {
                    Console.WriteLine(@"По указанному пути есть файл , перезаписать(да)  дописать(нет)?");
                    string temp = Console.ReadLine();
                    if (temp == "да")
                    {
                        createDir(path);
                        break;
                    }
                    else if(temp == "нет")
                    {
                        createDir(path,true);

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
        /// <summary>
        /// Свойство создающее директорию и файл
        /// </summary>
        /// <param name="path">Путь до файла</param>
        /// <param name="append">Возможно ли перезаписать</param>
        private void createDir(string path,bool append = false)
        {
            this.path = path;
            FileInfo fi = new FileInfo(path);
            DirectoryInfo di = new DirectoryInfo(fi.DirectoryName);
            di.Create();
            if (append == false)
                using (StreamWriter sr = fi.CreateText()) ;
            else
                using (StreamWriter sr = fi.AppendText()) ;
            SaveInFile();
        }

        /// <summary>
        /// Свойство для проверки возможнсти импорта записей
        /// </summary>
        /// <param name="met">1 или 2 (1 простой импорт) 2 (импорт с условием по дате)</param>
        /// <param name="min">Минимально допустимая дата для импорта с условием</param>
        /// <param name="max">Максимально допустимая дата дял импорта с условием</param>
        public void Import(int met,DateTime? min = null  ,DateTime? max = null)
        {
            string path = String.Empty;
            while (true)
            {
                Console.WriteLine("Введите путь до файла с записями:");
                path = Console.ReadLine();
                Console.WriteLine(File.Exists(path));
                if (File.Exists(path))
                {
                    if (met==1)
                    notebook.InputData(inputRecords(path));
                    else notebook.InputData(inputRecordsWithOption(path,min,max));
                    break;
                }
                else
                {
                    Console.WriteLine("По выбранному пути невозможно найти файл");
                }

            }

        }



        /// <summary>
        /// Свойство импортирущее записи по указанному пути с опцией по дате создания
        /// </summary>
        /// <param name="path">путь до файла</param>
        /// <param name="min">Минимально допустимая дата для импорта с условием</param>
        /// <param name="max">Максимально допустимая дата дял импорта с условием</param>
        /// <returns>Массив конкретных записей из файла</returns>
        private List<Record> inputRecordsWithOption(string path,DateTime? min ,DateTime? max)
        {
            List<Record> result = new List<Record>();
            using (StreamReader sr = new StreamReader(path))
            {
                Titles = sr.ReadLine().Split(';');
                while (!sr.EndOfStream)
                {
                    string[] temp = sr.ReadLine().Split(';');
                    if ((Convert.ToDateTime(temp[3]) >= min) && (Convert.ToDateTime(temp[3]) <= max))
                    result.Add(new Record(temp[0], Int32.Parse(temp[1]), temp[2], Convert.ToDateTime(temp[3]), temp[4]));
                }
            }
            return result;
        }

        /// <summary>
        /// Свойство которое импортирует записи по указанному пути
        /// </summary>
        /// <param name="path">Путь до файла</param>
        /// <returns>Все записи из файла</returns>
        private List<Record> inputRecords(string path)
        {
            List<Record> result = new List<Record>();
            using (StreamReader sr = new StreamReader(path))
            {
                Titles = sr.ReadLine().Split(';');
                while (!sr.EndOfStream)
                {
                    string[] temp = sr.ReadLine().Split(';');

                    result.Add(new Record(temp[0],Int32.Parse(temp[1]), temp[2], Convert.ToDateTime(temp[3]), temp[4]));
                }
            }
            return result;
        }

        /// <summary>
        /// Свойство которое добавляет запись в книгу
        /// </summary>
        public void AddRecord()
        {
            string tempTitle = String.Empty;
            int tempRel = 0;
            string tempText = String.Empty;
            DateTime tempDateTime;
            string owner = String.Empty;

            Console.Write("Введите заголовок записи: ");
            tempTitle = Console.ReadLine();

            Console.Write("Введите важность записи от 0 до 10: ");
            Int32.TryParse(Console.ReadLine(), out tempRel);

            Console.Write("Введите текст записи: ");
            tempText = Console.ReadLine();

            Console.Write("Введите дату записи(пример 1900.01.01): ");
            tempDateTime = Convert.ToDateTime(Console.ReadLine());

            Console.Write("Введие имя: ");
            owner = Console.ReadLine();

            notebook.Add(new Record(tempTitle, tempRel, tempText, tempDateTime, owner));

;        }

        /// <summary>
        /// Свойство выводящее на экран всю записную книгу
        /// </summary>
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

        /// <summary>
        /// Свойство выводящее на экран только выбранную запись из книги
        /// </summary>
        /// <param name="count">Номер записи</param>
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

        /// <summary>
        /// Свойство удаляющее запись из книги
        /// </summary>
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
        /// <summary>
        /// Свойство для редактирования записи в книге
        /// </summary>
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
                                            tempRecord.Relevance = Int32.Parse(Console.ReadLine());
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

        /// <summary>
        /// Свойство для сортировки книги по выбранному пункту
        /// </summary>
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