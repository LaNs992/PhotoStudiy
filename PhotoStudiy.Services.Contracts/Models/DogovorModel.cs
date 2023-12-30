using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudiy.Services.Contracts.Models
{
    public class DogovorModel
    {
        /// <summary>
        /// Индитификатор
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// клиент
        /// </summary>
        public ClientModel Client { get; set; }
        /// <summary>
        /// фотограф
        /// </summary>
        public PhotographModel Photograph { get; set; }
        /// <summary>
        /// фотосессия
        /// </summary>
        public PhotoSetModel Photoset { get; set; }
        /// <summary>
        /// продукт
        /// </summary>
        public ProductModel Product { get; set; }
        /// <summary>
        /// реквизит
        /// </summary>
        public RecvisitModel Recvisit { get; set; }
        /// <summary>
        /// Услуга
        /// </summary>
        public UslugiModel Uslugi { get; set; }

        /// <summary>
        /// Цена итог
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Дата и врремя проведения фотос
        /// </summary>
        public DateTimeOffset Date { get; set; } =DateTimeOffset.UtcNow;
    }
}
