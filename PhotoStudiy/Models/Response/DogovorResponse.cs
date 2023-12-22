using PhotoStudiy.Context.Contracts.Models;

namespace PhotoStudiy.API.Models.Response
{
    public class DogovorResponse
    {
        /// <summary>
        /// Индитификатор
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// клиент
        /// </summary>
        public ClientResponse? Client { get; set; }
        /// <summary>
        /// фотограф
        /// </summary>
        public PhotographResponse? Photograph { get; set; }
        /// <summary>
        /// фотосессия
        /// </summary>
        public PhotoSetResponse? Photoset { get; set; }
        /// <summary>
        /// продукт
        /// </summary>
        public ProductResponse? Product { get; set; }
        /// <summary>
        /// реквизит
        /// </summary>
        public RecvisitResponse? Recvisit { get; set; }
        /// <summary>
        /// Услуга
        /// </summary>
        public UslugiResponse? Uslugi { get; set; }

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
