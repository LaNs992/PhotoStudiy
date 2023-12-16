using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudiy.Services.Contracts.Models
{
    public class UslugiModel
    {
        /// <summary>
        /// Индитификатор
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Название Реквизита
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Описние
        /// </summary>
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// Колличество реквизита
        /// </summary>
        public int Amount { get; set; }
    }
}
