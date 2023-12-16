using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudiy.Services.Contracts.Models
{
    public class ProductModel
    {
        /// <summary>
        /// Индитификатор
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Название продукта
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Цена за продукт
        /// </summary>
        public string Price { get; set; } = string.Empty;
        /// <summary>
        /// Колличество
        /// </summary>
        public int Amount { get; set; }
    }
}
