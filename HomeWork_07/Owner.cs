using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_07
{
    struct Owner
    {
        /// <summary>
        /// Имя пользователя 
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Дата рождения 
        /// </summary>
        public DateTime DateOfBirth { get; }
        /// <summary>
        /// Конструктор по всем полям
        /// </summary>
        /// <param name="name">Имя пользователя</param>
        /// <param name="dateofbirth">Дата рождения</param>
        public Owner (string name,DateTime dateofbirth)  
        {
            this.Name = name;
            this.DateOfBirth = dateofbirth;
        }
        /// <summary>
        /// Перегрузка конструктора с 1 полем
        /// </summary>
        /// <param name="name">Имя пользователя </param>
        public Owner(string name) :
            this(name, new DateTime(1900, 1, 1))
            {}


    }
}
