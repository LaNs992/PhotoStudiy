using PhotoStudiy.Context.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudiy.Repositories.Contracts.ReadRepositoriesContracts
{
    public interface IRecvisitReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="Recvisit"/>
        /// </summary>
        Task<IReadOnlyCollection<Recvisit>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Recvisit"/> по идентификатору
        /// </summary>
        Task<Recvisit?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Recvisit"/> по идентификаторам
        /// </summary>
        Task<Dictionary<Guid, Recvisit>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);

        /// <summary>
        /// Проверить есть ли <see cref="Recvisit"/> в коллеции
        /// </summary>
        Task<bool> IsNotNullAsync(Guid id, CancellationToken cancellationToken);
    }
}
