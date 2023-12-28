using Microsoft.EntityFrameworkCore;
using PhotoStudiy.Common.Entity.InterfaceDB;
using PhotoStudiy.Context.Contracts;
using PhotoStudiy.Context.Contracts.Configution.Configuration;
using PhotoStudiy.Context.Contracts.Models;


namespace PhotoStudiy.Context
{
    public class PhotoStudiyContext : DbContext, IPhotoStudiyContext, IDbRead, IDbWriter, IUnitOfWork
    {
        
        public DbSet<Client> Clients { get; set; }

        public DbSet<Dogovor> Dogovors { get; set; }

        public DbSet<Photogragh> Photograghs { get; set; }

        public DbSet<PhotoSet> PhotoSets { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Recvisit> Recvisits { get; set; }

        public DbSet<Uslugi> Uslugs { get; set; }
        public PhotoStudiyContext(DbContextOptions<PhotoStudiyContext> options) : base(options)
        {

        }
      

        async Task<int> IUnitOfWork.SaveChangesAsync(CancellationToken cancellationToken)
        {
            var count = await base.SaveChangesAsync(cancellationToken);
            foreach (var entry in base.ChangeTracker.Entries().ToArray())
            {
                entry.State = EntityState.Detached;
            }
            return count;
        }

        /// <summary>
        /// Чтение сущностей из БД
        /// </summary>
        IQueryable<TEntity> IDbRead.Read<TEntity>()
            => base.Set<TEntity>()
                .AsNoTracking()
                .AsQueryable();

        /// <summary>
        /// Запись сущности в БД
        /// </summary>
        void IDbWriter.Add<TEntity>(TEntity entity)
            => base.Entry(entity).State = EntityState.Added;

        /// <summary>
        /// Обновление сущностей
        /// </summary>
        void IDbWriter.Update<TEntity>(TEntity entity)
            => base.Entry(entity).State = EntityState.Modified;

        /// <summary>
        /// Удаление сущности из БД
        /// </summary>
        void IDbWriter.Delete<TEntity>(TEntity entity)
            => base.Entry(entity).State = EntityState.Deleted;
    }
}