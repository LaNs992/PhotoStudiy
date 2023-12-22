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
        /// <summary>
        /// Добавляет новый Фотограф
        /// </summary>
        Task<PhotographModel> AddAsync(PhotographModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует существующий Фотограф
        /// </summary>
        Task<PhotographModel> EditAsync(PhotographModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующий Фотограф
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
