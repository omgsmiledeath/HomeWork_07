using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_07
{
    class NoteBook
    {
        /// <summary>
        /// Перечисление метода записи
        /// </summary>
        public enum WriteMode
        {
            Save,  //Сохранить в текущем файле
            SaveAll //Сохранить в новом
        }
        /// <summary>
        /// Поле показывающее были ли сохранены изменения
        /// </summary>
        private bool changes;

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
        public NoteBook()
        {
            this.Records = new List<Record>();
            this.titles = new string[0];
            this.changes = false;
            this.newBook = true;
        }

        /// <summary>
        /// Конструктор для открытия записной книги которая уже была создана
        /// </summary>
        /// <param name="path">путь до файла с записями</param>
        public NoteBook (string path):
            this()
        {
            this.newBook = false;
            this.path = path;
            this.Load();
            
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
        public void InputRecords (string path)
        {

        }
        /// <summary>
        /// Метод сохраняющий записную книгу
        /// </summary>
        /// <param name="wm">Переменная выбора способа сохранения</param>
        private void Save(WriteMode wm)
        {
            if(wm == 0)
            {
                if(this.newBook == true) //если книга была создана без загрузки из файла ,проверка чтоб указать куда сохранять данные  
                {

                }
                changes = true; 
            }
            else
            {
                changes = true;
            }
        }

        public void Add(Record record)
        {

        }

        public void Edit(uint num)
        {

        }

        public void Delete(uint num)
        {

        }
    }
}
