namespace PhotoStudiy.API.Models.CreateRequest
{
    public class CreateUslugiRequest
    { /// <summary>
      /// Название услуги
      /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Цена за услугу
        /// </summary>
        public string Price { get; set; } = string.Empty;
    }
}
