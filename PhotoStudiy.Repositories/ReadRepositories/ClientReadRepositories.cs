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
    public class ClientReadRepositories : IClientReadRepository, IRepositoryAnchor
    {
        /// <summary>
        /// Контекст для связи с бд
        /// </summary>
        private IDbRead reader;

        public ClientReadRepositories(IDbRead reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<Client>> IClientReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Client>()
                .NotDeletedAt()
                .OrderBy(x => x.Number)
                .ThenBy(x => x.LastName)
                .ThenBy(x => x.Name)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Client?> IClientReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Client>()
            .NotDeletedAt()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Client>> IClientReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
            => reader.Read<Client>()
                .NotDeletedAt()
                .ByIds(ids)
                .OrderBy(x => x.Number)
                .ThenBy(x => x.LastName)
                .ThenBy(x => x.Name)
            .ToDictionaryAsync(x => x.Id, cancellationToken);


        Task<bool> IClientReadRepository.IsNotNullAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Client>().NotDeletedAt().AnyAsync(x => x.Id == id && !x.DeletedAt.HasValue, cancellationToken);
    }
}
