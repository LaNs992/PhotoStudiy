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
    internal class PhotoSetReadRepositories : IPhotoSetReadRepository, IRepositoryAnchor
    {
        /// <summary>
        /// Контекст для связи с бд
        /// </summary>
        private IDbRead reader;

        public PhotoSetReadRepositories(IDbRead reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<PhotoSet>> IPhotoSetReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<PhotoSet>()
                .NotDeletedAt()
                .OrderBy(x => x.Name)
                .ThenBy(x => x.Description)
                .ThenBy(x => x.Price)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<PhotoSet?> IPhotoSetReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<PhotoSet>()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, PhotoSet>> IPhotoSetReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
            => reader.Read<PhotoSet>()
                .ByIds(ids)
                .OrderBy(x => x.Name)
                .ThenBy(x => x.Description)
                .ThenBy(x => x.Price)
            .ToDictionaryAsync(x => x.Id, cancellationToken);


        Task<bool> IPhotoSetReadRepository.IsNotNullAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<PhotoSet>().AnyAsync(x => x.Id == id && !x.DeletedAt.HasValue, cancellationToken);
    }
}
