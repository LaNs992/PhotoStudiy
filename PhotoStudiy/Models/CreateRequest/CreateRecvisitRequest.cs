namespace PhotoStudiy.API.Models.CreateRequest
{
    public class CreateRecvisitRequest
    {   /// <summary>
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
