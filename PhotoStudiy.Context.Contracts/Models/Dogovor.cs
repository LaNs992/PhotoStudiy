using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudiy.Context.Contracts.Models
{ 
    /// <summary>
    /// Договор
    /// </summary>
    public class Dogovor : BaseAuditEntity
    {
        /// <summary>
        /// Индитификатор
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// клиент
        /// </summary>
        public Guid ClientId { get; set; }
        public Client Client { get; set; }
        /// <summary>
        /// фотограф
        /// </summary>
        public Guid PhotographId { get; set; }
        public Photogragh Photogragh { get; set; }
        /// <summary>
        /// фотосессия
        /// </summary>
        public Guid PhotosetId { get; set; }
        public PhotoSet PhotoSet { get; set; }

        /// <summary>
        /// продукт
        /// </summary>
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        /// <summary>
        /// реквизит
        /// </summary>
        public Guid RecvisitId { get; set; }
        public Recvisit Recvisit { get; set; }


        /// <summary>
        /// Услуга
        /// </summary>
        public Guid UslugiId { get; set; }
        public Uslugi Uslugi { get; set; }

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
