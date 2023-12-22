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
    internal class UslugiReadRepositories : IUslugiReadRepository, IRepositoryAnchor
    {
        /// <summary>
        /// Контекст для связи с бд
        /// </summary>
        private IDbRead reader;

        public UslugiReadRepositories(IDbRead reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<Uslugi>> IUslugiReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Uslugi>()
                .NotDeletedAt()
                .OrderBy(x => x.Name)
                .ThenBy(x => x.Price)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Uslugi?> IUslugiReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Uslugi>()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Uslugi>> IUslugiReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
            => reader.Read<Uslugi>()
                .ByIds(ids)
                .OrderBy(x => x.Name)
                .ThenBy(x => x.Price)
                .ToDictionaryAsync(x => x.Id, cancellationToken);


        Task<bool> IUslugiReadRepository.IsNotNullAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Uslugi>().AnyAsync(x => x.Id == id && !x.DeletedAt.HasValue, cancellationToken);
    }
}
