using PhotoStudiy.Context.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudiy.Repositories.Contracts.ReadRepositoriesContracts
{
    public interface IUslugiReadRepository
    {

        /// <summary>
        /// Получить список всех <see cref="Uslugi"/>
        /// </summary>
        Task<IReadOnlyCollection<Uslugi>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Uslugi"/> по идентификатору
        /// </summary>
        Task<Uslugi?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Uslugi"/> по идентификаторам
        /// </summary>
        Task<Dictionary<Guid, Uslugi>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);

        /// <summary>
        /// Проверить есть ли <see cref="Uslugi"/> в коллеции
        /// </summary>
        Task<bool> IsNotNullAsync(Guid id, CancellationToken cancellationToken);
    }
}
