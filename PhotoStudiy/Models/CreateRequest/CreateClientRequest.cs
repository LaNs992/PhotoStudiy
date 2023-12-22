namespace PhotoStudiy.API.Models.CreateRequest
{
    public class CreateClientRequest
    {
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
