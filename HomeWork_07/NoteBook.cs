using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HomeWork_07
{
    struct NoteBook
    {

        private bool newBook;

        /// <summary>
        /// Массив записей 
        /// </summary>
        private List<Record> Records { get; set; }
        /// <summary>
        /// Путь до файла с записями
        /// </summary>
        private string path;

        /// <summary>
        /// Заголовки для вывода на экран
        /// </summary>
        private string[] titles;

        /// <summary>
        /// Конструктор для создания новой записной книги
        /// </summary>

        /// <summary>
        /// Конструктор для открытия записной книги которая уже была создана
        /// </summary>
        /// <param name="path">путь до файла с записями</param>
        public NoteBook (string path)
        {
            this.Records = new List<Record>();
            this.titles = new string[0];
            this.path = path;
            this.Load();
        }
        public NoteBook(bool newBook)
        {
            this.Records = new List<Record>();
            this.titles = new string[0];
            this.path = String.Empty;
            this.newBook = newBook;
        }

        /// <summary>
        /// Метод который загружает записи из файла 
        /// </summary>
        private void Load()
        {

        }

        /// <summary>
        /// Метод для добавления в существующую книгу записей из файла
        /// </summary>
        /// <param name="path"></param>
        public void InputRecords(string path)
        {

        }
    }
}
