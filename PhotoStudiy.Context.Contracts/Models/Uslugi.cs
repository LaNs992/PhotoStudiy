using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudiy.Context.Contracts.Models
{
    /// <summary>
    /// Услуги
    /// </summary>
    public class Uslugi : BaseAuditEntity
    {
        
        /// <summary>
        /// Название услуги
        /// </summary>
        public string Name { get; set; }
      
        /// <summary>
        /// Цена за услугу
        /// </summary>
        public string Price { get; set; }

        public ICollection<Dogovor> Dogovors { get; set; }
    }
}
