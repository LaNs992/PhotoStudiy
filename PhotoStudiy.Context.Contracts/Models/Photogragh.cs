using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudiy.Context.Contracts.Models
{
    /// <summary>
    /// Фотографы
    /// </summary>
    public class Photogragh : BaseAuditEntity
    {
       
        /// <summary>
        /// Иия
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Номер телфона
        /// </summary>
        public string Number { get; set; }
        public ICollection<Dogovor>? Dogovors { get; set; }
    }
}
