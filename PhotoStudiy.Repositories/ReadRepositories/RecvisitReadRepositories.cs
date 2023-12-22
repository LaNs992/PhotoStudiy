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
    internal class RecvisitReadRepositories : IRecvisitReadRepository, IRepositoryAnchor
    {
        /// <summary>
        /// Контекст для связи с бд
        /// </summary>
        private IDbRead reader;

        public RecvisitReadRepositories(IDbRead reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<Recvisit>> IRecvisitReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Recvisit>()
                .NotDeletedAt()
                .OrderBy(x => x.Name)
                .ThenBy(x => x.Description)
                .ThenBy(x => x.Amount)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Recvisit?> IRecvisitReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Recvisit>()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Recvisit>> IRecvisitReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
            => reader.Read<Recvisit>()
                .ByIds(ids)
                .OrderBy(x => x.Name)
                .ThenBy(x => x.Description)
                .ThenBy(x => x.Amount)
            .ToDictionaryAsync(x => x.Id, cancellationToken);


        Task<bool> IRecvisitReadRepository.IsNotNullAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Recvisit>().AnyAsync(x => x.Id == id && !x.DeletedAt.HasValue, cancellationToken);
    }
}
