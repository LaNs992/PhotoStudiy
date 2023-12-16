using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudiy.Context.Contracts.Models
{
    /// <summary>
    /// Продукты
    /// </summary>
    public class Product : BaseAuditEntity
    {

        /// <summary>
        /// Индитификатор
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Название продукта
        /// </summary>
        public string Name { get; set; }
      
        /// <summary>
        /// Цена за продукт
        /// </summary>
        public string Price { get; set; }
        /// <summary>
        /// Колличество
        /// </summary>
        public int Amount   { get; set; }
        
        public ICollection<Dogovor> Dogovors { get; set; }
    }
}
