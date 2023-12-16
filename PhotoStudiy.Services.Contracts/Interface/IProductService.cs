using PhotoStudiy.Services.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudiy.Services.Contracts.Interface
{
    public interface IProductService
    {


        /// <summary>
        /// Получить список всех <see cref="ProductModel"/>
        /// </summary>
        /// 

        Task<IEnumerable<ProductModel>> GetAllAsync(CancellationToken cancellationToken);
        /// <summary>
        /// Получить <see cref="ProductModel"/> по идентификатору
        /// </summary>

        Task<ProductModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
