using PhotoStudiy.Services.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudiy.Services.Contracts.Interface
{
    public interface IPhotographService
    {

        /// <summary>
        /// Получить список всех <see cref="PhotographModel"/>
        /// </summary>
        /// 

        Task<IEnumerable<PhotographModel>> GetAllAsync(CancellationToken cancellationToken);
        /// <summary>
        /// Получить <see cref="PhotographModel"/> по идентификатору
        /// </summary>

        Task<PhotographModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
