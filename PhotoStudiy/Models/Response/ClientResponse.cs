namespace PhotoStudiy.API.Models.Response
{
    public class ClientResponse
    {        /// <summary>
             /// Индитификатор
             /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Иия
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; } = string.Empty;
        /// <summary>
        /// Номер телфона
        /// </summary>
        public string Number { get; set; } = string.Empty;
    }
}
