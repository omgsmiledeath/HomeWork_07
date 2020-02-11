using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;


namespace HomeWork_07
{
    struct NoteBook
    {

        /// <summary>
        /// Массив записей 
        /// </summary>
        private List<Record> Records { get; set; }

        /// <summary>
        /// Поле указывающее новая записная книга или нет
        /// </summary>
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
        public void InputData (List<Record> input)
        {
            foreach (var e in input)
            {
                Records.Add(e);
            }
                
        }
        /// <summary>
        /// Свойство для очистки записей в текущей книге
        /// </summary>
        public void ClearRecord()
        {
            Records.Clear();
        }
        /// <summary>
        /// Свойство для экспорта всех записей в книге
        /// </summary>
        /// <returns></returns>
        public List<Record> ExportData()
        {
            List<Record> result = new List<Record>();
            result.AddRange(Records.ToArray());
            return result;
        }

        /// <summary>
        /// Свойство которое выдает нужную запись
        /// </summary>
        /// <param name="counter">Номер записи</param>
        /// <returns>Запись соответствующую номеру</returns>
        public Record getRecord(int counter)
        {
            return Records[counter];
        }

        public void SortByTitle()
        {
            var temp= Records.OrderBy(a => a.Title).ToList();
            ClearRecord();
            Records.AddRange(temp.ToArray());
            
        }

        public void SortByRelevance()
        {
            var temp = Records.OrderBy(a => a.Relevance).ToList();
            ClearRecord();
            Records.AddRange(temp.ToArray());
        }

        public void SortByText()
        {
            var temp = Records.OrderBy(a => a.Text).ToList();
            ClearRecord();
            Records.AddRange(temp.ToArray());
        }

        public void SortByDate()
        {
            var temp = Records.OrderBy(a => a.WriteDate).ToList();
            ClearRecord();
            Records.AddRange(temp.ToArray());
        }

        public void SortByOwner()
        {
            var temp = Records.OrderBy(a => a.RecordOwner).ToList();
            ClearRecord();
            Records.AddRange(temp.ToArray());
        }
    }
}
