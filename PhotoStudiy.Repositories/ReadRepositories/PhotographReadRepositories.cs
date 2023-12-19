using Microsoft.EntityFrameworkCore;
using PhotoStudiy.Common.Entity.InterfaceDB;
using PhotoStudiy.Common.Entity.Repositories;
using PhotoStudiy.Context.Contracts.Models;
using PhotoStudiy.Repositories.Anchors;
using PhotoStudiy.Repositories.Contracts.ReadRepositoriesContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudiy.Repositories.ReadRepositories
{
    internal class PhotographReadRepositories : IPhotographReadRepository, IRepositoryAnchor
    {
        /// <summary>
        /// Контекст для связи с бд
        /// </summary>
        private IDbRead reader;

        public PhotographReadRepositories(IDbRead reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<Photogragh>> IPhotographReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Photogragh>()
                .NotDeletedAt()
                .OrderBy(x => x.Number)
                .ThenBy(x => x.LastName)
                .ThenBy(x => x.Name)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Photogragh?> IPhotographReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Photogragh>()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Photogragh>> IPhotographReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
            => reader.Read<Photogragh>()
                .NotDeletedAt()
                .ByIds(ids)
                .OrderBy(x => x.Number)
                .ThenBy(x => x.LastName)
                .ThenBy(x => x.Name)
            .ToDictionaryAsync(x => x.Id, cancellationToken);
                 

        Task<bool> IPhotographReadRepository.IsNotNullAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Photogragh>().AnyAsync(x => x.Id == id && !x.DeletedAt.HasValue, cancellationToken);
    }
}
