using PhotoStudiy.Services.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudiy.Services.Contracts.Interface
{
    public interface IRecvisitService
    {


        /// <summary>
        /// Получить список всех <see cref="RecvisitModel"/>
        /// </summary>
        /// 

        Task<IEnumerable<RecvisitModel>> GetAllAsync(CancellationToken cancellationToken);
        /// <summary>
        /// Получить <see cref="RecvisitModel"/> по идентификатору
        /// </summary>

        Task<RecvisitModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
