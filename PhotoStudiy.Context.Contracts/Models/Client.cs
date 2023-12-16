

using System.Net.Sockets;

namespace PhotoStudiy.Context.Contracts.Models
{
    /// <summary>
    /// Клиент
    /// </summary>
    public class Client : BaseAuditEntity
    {
        /// <summary>
        /// Индитификатор
        /// </summary>
        public Guid Id { get; set; }
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

        public ICollection<Dogovor> Dogovors { get; set; }
    }
}
