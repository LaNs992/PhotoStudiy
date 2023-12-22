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
        /// <summary>
        /// Добавляет новый Реквизит
        /// </summary>
        Task<RecvisitModel> AddAsync(RecvisitModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует существующий Реквизит
        /// </summary>
        Task<RecvisitModel> EditAsync(RecvisitModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующий Реквизит
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
