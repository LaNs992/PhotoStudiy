using PhotoStudiy.Context.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudiy.Repositories.Contracts.ReadRepositoriesContracts
{
    public interface IPhotoSetReadRepository
    {

        /// <summary>
        /// Получить список всех <see cref="PhotoSet"/>
        /// </summary>
        Task<IReadOnlyCollection<PhotoSet>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="PhotoSet"/> по идентификатору
        /// </summary>
        Task<PhotoSet?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="PhotoSet"/> по идентификаторам
        /// </summary>
        Task<Dictionary<Guid, PhotoSet>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);

        /// <summary>
        /// Проверить есть ли <see cref="PhotoSet"/> в коллеции
        /// </summary>
        Task<bool> IsNotNullAsync(Guid id, CancellationToken cancellationToken);
    }
}
