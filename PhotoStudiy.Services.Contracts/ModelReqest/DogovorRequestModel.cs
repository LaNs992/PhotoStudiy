using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudiy.Services.Contracts.ModelReqest
{   /// <summary>
    /// Модель создания Договора
    /// </summary>
    public class DogovorRequestModel
    {
        /// <summary>
        /// Индитификатор
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// клиент
        /// </summary>
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
