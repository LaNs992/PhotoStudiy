using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudiy.Services.Contracts.Models
{
    public class PhotoSetModel
    {
        /// <summary>
        /// Индитификатор
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Название фотосессии
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Описние
        /// </summary>
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// Цена за фотосесисию
        /// </summary>
        public string Price { get; set; } = string.Empty;
    }
}
