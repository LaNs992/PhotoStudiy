using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudiy.Services.Contracts.Models
{
    public class UslugiModel
    {/// <summary>
     /// Индитификатор
     /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Название услуги
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Цена за услугу
        /// </summary>
        public string Price { get; set; }

    }
}
