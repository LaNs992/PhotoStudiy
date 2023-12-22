using PhotoStudiy.Services.Contracts.ModelReqest;
using PhotoStudiy.Services.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudiy.Services.Contracts.Interface
{
    public interface IDogovorService
    {
         /// <summary>
        /// Получить список всех <see cref="DogovorModel"/>
        /// </summary>
        /// 

        Task<IEnumerable<DogovorModel>> GetAllAsync(CancellationToken cancellationToken);
        /// <summary>
        /// Получить <see cref="DogovorModel"/> по идентификатору
        /// </summary>

        Task<DogovorModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет новый Договор
        /// </summary>
        Task<DogovorModel> AddAsync(DogovorRequestModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует существующий Договор
        /// </summary>
        Task<DogovorModel> EditAsync(DogovorRequestModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующий Договор
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
