using Microsoft.EntityFrameworkCore;
using PhotoStudiy.Common.Entity.InterfaceDB;
using PhotoStudiy.Common.Entity.Repositories;
using PhotoStudiy.Context.Contracts.Models;
using PhotoStudiy.Repositories.Anchors;
using PhotoStudiy.Repositories.Contracts.ReadRepositoriesContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudiy.Repositories.ReadRepositories
{
    public class DogovorReadRepositories : IDogovorReadRepository, IRepositoryAnchor
    {
        /// <summary>
        /// Контекст для связи с бд
        /// </summary>
        private IDbRead reader;
        public DogovorReadRepositories(IDbRead reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<Dogovor>> IDogovorReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Dogovor>()
                .NotDeletedAt()
        .OrderBy(x => x.Date)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Dogovor?> IDogovorReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Dogovor>()
            .NotDeletedAt()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);
        Task<bool> IDogovorReadRepository.IsNotNullAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Dogovor>().NotDeletedAt().AnyAsync(x => x.Id == id && !x.DeletedAt.HasValue, cancellationToken);
    }
}
