using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudiy.Context.Contracts.Models
{
    /// <summary>
    /// Фотосессия 
    /// </summary>
    public class PhotoSet : BaseAuditEntity
    {

        /// <summary>
        /// Индитификатор
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Название фотосессии
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Описние
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Цена за фотосесисию
        /// </summary>
        public string Price { get; set; }

        public ICollection<Dogovor> Dogovors { get; set; }
    }
}
