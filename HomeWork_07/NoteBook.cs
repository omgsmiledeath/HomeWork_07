using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace HomeWork_07
{
    struct NoteBook
    {

        /// <summary>
        /// Массив записей 
        /// </summary>
        private List<Record> Records { get; set; }
        private bool newBook { get; }

        /// <summary>
        /// Конструктор для открытия записной книги 
        /// </summary>

        public NoteBook(bool newBook)
        {
            this.Records = new List<Record>();
            this.newBook = newBook;
        }
        /// <summary>
        /// Свойство добавления записи
        /// </summary>
        /// <param name="record">входящая запись</param>
        public void Add(Record record)
        {
            Records.Add(record);
        }
        /// <summary>
        /// Свойство по изменению выбранной записи
        /// </summary>
        /// <param name="counter">номер записи</param>
        /// <param name="record">Измененная запись</param>
        public void Edit(int counter,Record record)
        {
            Records[counter]=record;
        }

        /// <summary>
        /// Свойство удаления записей
        /// </summary>
        /// <param name="counter">Указатель какую запись удалить</param>
        public void Delete(int counter)
        {
            Records.RemoveAt(counter);
        }
        /// <summary>
        /// Свойство по загрузке массва записей
        /// </summary>
        /// <param name="inputData"></param>
        public void InputData (List<Record> inputData)
        {
            foreach (var e in inputData)
                Records.Add(e);
        }
        /// <summary>
        /// Свойство для экспорта всех записей в книге
        /// </summary>
        /// <returns></returns>
        public List<Record> ExportData()
        {
            return Records;
        }
    }
}
