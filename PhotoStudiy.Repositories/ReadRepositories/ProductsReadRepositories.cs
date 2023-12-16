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
    internal class ProductsReadRepositories : IProductReadRepository, IRepositoryAnchor
    {
        /// <summary>
        /// Контекст для связи с бд
        /// </summary>
        private IDbRead reader;

        public ProductsReadRepositories(IDbRead reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<Product>> IProductReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Product>()
                .NotDeletedAt()
                .OrderBy(x => x.Name)
                .ThenBy(x => x.Price)
                .ThenBy(x => x.Amount)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Product?> IProductReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Product>()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Product>> IProductReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
            => reader.Read<Product>()
                .ByIds(ids)
                .OrderBy(x => x.Name)
                .ThenBy(x => x.Price)
                .ThenBy(x => x.Amount)
            .ToDictionaryAsync(x => x.Id, cancellationToken);


        Task<bool> IProductReadRepository.IsNotNullAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Product>().AnyAsync(x => x.Id == id && !x.DeletedAt.HasValue, cancellationToken);
    }
}
