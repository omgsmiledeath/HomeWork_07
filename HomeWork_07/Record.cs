using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_07
{
    struct Record
    {
        /// <summary>
        /// Заголовок записи
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Важность
        /// </summary>
        public string Relevance { get; set; }
        /// <summary>
        /// Текст записи
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Дата записи
        /// </summary>
        public DateTime WriteDate { get; set; }

        /// <summary>
        /// Пользователь оставивший запись 
        /// </summary>
        public Owner RecordOwner { get;}

        /// <summary>
        /// Конструктор создающий запись
        /// </summary>
        /// <param name="title">Заголовок записи</param>
        /// <param name="relevance">Важность записи</param>
        /// <param name="text">Текст записи</param>
        /// <param name="writeDate">Дата записи</param>
        /// <param name="recordOwner">Пользователь оставивший</param>
        public Record (string title, string relevance, string text, DateTime writeDate, Owner recordOwner )
        {
            this.Title = title;
            this.Relevance = relevance;
            this.Text = text;
            this.WriteDate = writeDate;
            this.RecordOwner = recordOwner;
        }
    }
}
