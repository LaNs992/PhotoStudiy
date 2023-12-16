namespace PhotoStudiy.API.Models
{
    public class ProductResponse
    {
        /// <summary>
        /// Индитификатор
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Название продукта
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Цена за продукт
        /// </summary>
        public string Price { get; set; } = string.Empty;
        /// <summary>
        /// Колличество
        /// </summary>
        public int Amount { get; set; } 
    }
}
