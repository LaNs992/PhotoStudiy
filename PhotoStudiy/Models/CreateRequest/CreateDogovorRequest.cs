using PhotoStudiy.API.Models.Response;

namespace PhotoStudiy.API.Models.CreateRequest
{
    public class CreateDogovorRequest
    {
        public Guid ClientId { get; set; }
        /// <summary>
        /// фотограф
        /// </summary>
        public Guid PhotographId { get; set; }
        /// <summary>
        /// фотосессия
        /// </summary>
        public Guid PhotosetId { get; set; }
        /// <summary>
        /// продукт
        /// </summary>
        public Guid ProductId { get; set; }
        /// <summary>
        /// реквизит
        /// </summary>
        public Guid RecvisitId { get; set; }
        /// <summary>
        /// Услуга
        /// </summary>
        public Guid UslugiId { get; set; }

        /// <summary>
        /// Цена итог
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Дата и врремя проведения фотос
        /// </summary>
        public DateTimeOffset Date { get; set; }
    }
}
