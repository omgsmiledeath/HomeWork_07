

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
        private NoteBook notebook { get; }
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
            this.Titles = null;
            this.changes = true;
            this.newBook = newBook;
            this.path = path;
            this.notebook = new NoteBook(this.newBook);
        }

        public void SaveInFile()
        {

        }
        
        public List<Record> inputRecords ()
        {
            
            return null;
        }

    }
}