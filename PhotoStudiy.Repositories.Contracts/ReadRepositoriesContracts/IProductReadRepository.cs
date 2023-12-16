using PhotoStudiy.Context.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudiy.Repositories.Contracts.ReadRepositoriesContracts
{
    public interface IProductReadRepository
    {

        /// <summary>
        /// Получить список всех <see cref="Product"/>
        /// </summary>
        Task<IReadOnlyCollection<Product>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Product"/> по идентификатору
        /// </summary>
        Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Product"/> по идентификаторам
        /// </summary>
        Task<Dictionary<Guid, Product>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);

        /// <summary>
        /// Проверить есть ли <see cref="Product"/> в коллеции
        /// </summary>
        Task<bool> IsNotNullAsync(Guid id, CancellationToken cancellationToken);
    }
}
