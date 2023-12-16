using PhotoStudiy.Services.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudiy.Services.Contracts.Interface
{
    public interface IPhotoSetService
    {

        /// <summary>
        /// Получить список всех <see cref="PhotoSetModel"/>
        /// </summary>
        /// 

        Task<IEnumerable<PhotoSetModel>> GetAllAsync(CancellationToken cancellationToken);
        /// <summary>
        /// Получить <see cref="PhotoSetModel"/> по идентификатору
        /// </summary>

        Task<PhotoSetModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
