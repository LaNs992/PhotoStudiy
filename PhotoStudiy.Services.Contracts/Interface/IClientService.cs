using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoStudiy.Services.Contracts.Models;

namespace PhotoStudiy.Services.Contracts.Interface
{
    public interface IClientService
    {
        /// <summary>
        /// Получить список всех <see cref="ClientModel"/>
        /// </summary>
        /// 

        Task<IEnumerable<ClientModel>> GetAllAsync(CancellationToken cancellationToken);
        /// <summary>
        /// Получить <see cref="ClientModel"/> по идентификатору
        /// </summary>

        Task<ClientModel?>GetByIdAsync(Guid id,CancellationToken cancellationToken);
    }
}
