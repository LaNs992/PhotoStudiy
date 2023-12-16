using PhotoStudiy.Context.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudiy.Repositories.Contracts.ReadRepositoriesContracts
{
    public interface IPhotographReadRepository
    {

        /// <summary>
        /// Получить список всех <see cref="Photogragh"/>
        /// </summary>
        Task<IReadOnlyCollection<Photogragh>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Photogragh"/> по идентификатору
        /// </summary>
        Task<Photogragh?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Photogragh"/> по идентификаторам
        /// </summary>
        Task<Dictionary<Guid, Photogragh>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);

        /// <summary>
        /// Проверить есть ли <see cref="Photogragh"/> в коллеции
        /// </summary>
        Task<bool> IsNotNullAsync(Guid id, CancellationToken cancellationToken);
    }
}
