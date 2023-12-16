using PhotoStudiy.Common.Entity.InterfaceDB;
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

        Task<IReadOnlyCollection<Hall>> IPhotographReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Hall>()
                .NotDeletedAt()
                .OrderBy(x => x.Number)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Hall?> IHallReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Hall>()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Hall>> IPhotographReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
            => reader.Read<Hall>()
                .NotDeletedAt()
                .ByIds(ids)
                .OrderBy(x => x.Number).ToDictionaryAsync(x => x.Id, cancellationToken);

        Task<bool> IPhotographReadRepository.IsNotNullAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Hall>().AnyAsync(x => x.Id == id && !x.DeletedAt.HasValue, cancellationToken);
    }
}
