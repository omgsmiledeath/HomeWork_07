

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
            this.Titles = null;
            this.changes = true;
            this.newBook = newBook;
            this.path = path;
            this.notebook = new NoteBook(this.newBook);
        }

        public void SaveInFile()
        {
            using (StreamWriter sw = new StreamWriter(path,false,Encoding.UTF32))
            {
                foreach(var e in notebook.ExportData())
                {
                    string temp = String.Format("{0},{1},{2},{3},{4}",
                        e.Title,
                     e.Relevance,
                     e.Text,
                     e.WriteDate,
                     e.RecordOwner);
                    sw.WriteLine(temp);
                }
            }
        }
        
        public List<Record> inputRecords ()
        {
            
            return null;
        }

    }
}