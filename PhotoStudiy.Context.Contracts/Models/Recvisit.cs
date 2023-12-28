using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudiy.Context.Contracts.Models
{
    /// <summary>
    /// Реквизит
    /// </summary>
    public class Recvisit : BaseAuditEntity
    {
        /// <summary>
        /// Название Реквизита
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Описние
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Колличество реквизита
        /// </summary>
        public int Amount { get; set; }

        public ICollection<Dogovor> Dogovors { get; set; }
    }
}
