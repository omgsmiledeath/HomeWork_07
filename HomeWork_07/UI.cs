

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HomeWork_07
{
    struct UI
    {
        private NoteBook notebook;

        /// <summary>
        /// Поле показывающее были ли сохранены изменения
        /// </summary>
        private bool changes;

        private bool newBook;

        public UI()
        {
            this.changes = true;
            this.newBook = true;
            this.notebook = new NoteBook();
        }
        public UI(string path):this()
        {
            this.changes = true;
            this.notebook = new NoteBook(path);
        }


        
        /// <summary>
        /// Метод сохраняющий записную книгу
        /// </summary>
        /// <param name="wm">Переменная выбора способа сохранения</param>
        private void Save(Library.WriteMode wm)
        {
            if (wm == 0)
            {
                if (this.newBook == true) //если книга была создана без загрузки из файла ,проверка чтоб указать куда сохранять данные  
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