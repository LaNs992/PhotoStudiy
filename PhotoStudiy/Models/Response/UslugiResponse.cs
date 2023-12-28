namespace PhotoStudiy.API.Models.Response
{
    public class UslugiResponse
    {
        /// <summary>
        /// Индитификатор
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Название услуги
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Цена за услугу
        /// </summary>
        public string Price { get; set; } = string.Empty;
    }
}
