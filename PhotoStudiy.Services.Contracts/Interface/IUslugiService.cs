using PhotoStudiy.Services.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudiy.Services.Contracts.Interface
{
    public interface IUslugiService
    {



        /// <summary>
        /// Получить список всех <see cref="UslugiModel"/>
        /// </summary>
        /// 

        Task<IEnumerable<UslugiModel>> GetAllAsync(CancellationToken cancellationToken);
        /// <summary>
        /// Получить <see cref="UslugiModel"/> по идентификатору
        /// </summary>

        Task<UslugiModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
