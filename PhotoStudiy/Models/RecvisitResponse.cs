namespace PhotoStudiy.API.Models
{
    public class RecvisitResponse
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
