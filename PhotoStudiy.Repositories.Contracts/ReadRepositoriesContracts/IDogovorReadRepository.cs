using PhotoStudiy.Context.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudiy.Repositories.Contracts.ReadRepositoriesContracts
{
    public interface IDogovorReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="Dogovor"/>
        /// </summary>
        Task<IReadOnlyCollection<Dogovor>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Dogovor"/> по идентификатору
        /// </summary>
        Task<Dogovor?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Проверить есть ли <see cref="Dogovor"/> в коллеции
        /// </summary>
        Task<bool> IsNotNullAsync(Guid id, CancellationToken cancellationToken);
    }
}
