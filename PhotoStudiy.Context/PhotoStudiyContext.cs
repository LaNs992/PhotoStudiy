using Microsoft.EntityFrameworkCore;
using PhotoStudiy.Common.Entity.InterfaceDB;
using PhotoStudiy.Context.Contracts;
using PhotoStudiy.Context.Contracts.Configution.Configuration;
using PhotoStudiy.Context.Contracts.Models;


namespace PhotoStudiyContext
{
    public class PhotoStudiyContext : DbContext, IPhotoStudiyContext, IDbRead, IDbWriter, IUnitOfWork
    {
        //private readonly List<Client> clients;
        //private readonly List<Dogovor> dogovors;
        //private readonly List<Photogragh> photograghs;
        //private readonly List<PhotoSet> photoSets;
        //private readonly List<Product> products;
        //private readonly List<Recvisit> recvisits;
        //private readonly List<Uslugi> uslugis;


        //public PhotoStudiyContext()
        //{
        //    clients = new List<Client>()
        //    {
        //        new Client()
        //        {
        //            Id = Guid.NewGuid(),
        //            Name = "Кирилл",
        //            LastName = "Бажин",
        //            Number = "89123753406"
        //        }
        //    };

        //    photograghs= new List<Photogragh>()
        //    {
        //        new Photogragh()
        //        {
        //            Id = Guid.NewGuid(),
        //            Name = "Эльвина",
        //            LastName = "Коршткова",
        //            Number = "89123753606"
        //        }

        //    };

        //    photoSets = new List<PhotoSet>()
        //    {
        //        new PhotoSet(){
        //        Id = Guid.NewGuid(),
        //        Name = "Фотосессия в лесу бляьб",
        //        Description = "фотоссеия в лесу где человека щаводят в лес и жестко фотографируют",
        //        Price="5000"
        //        }
        //    };
        //    products = new List<Product>()
        //    {
        //        new Product()
        //        {
        //            Id= Guid.NewGuid(),
        //            Name="магнитик",
        //            Price="200",
        //            Amount= 45
        //        }
        //    };
        //    recvisits = new List<Recvisit>()
        //    {
        //        new Recvisit()
        //        {
        //            Id = Guid.NewGuid(),
        //            Name="Коженный костюм",
        //            Description="Для жесткой фотосесии",
        //            Amount=1
        //        }
        //    };
        //    uslugis = new List<Uslugi>
        //    {
        //        new Uslugi()
        //        {
        //            Id= Guid.NewGuid(),
        //            Name="Напечатать фотогрфию",
        //            Price="300"
        //        }
        //    };
        //    dogovors = new List<Dogovor>()
        //   {
        //       new Dogovor()
        //       {
        //           Id = Guid.NewGuid(),
        //           ClientId=clients.First().Id,
        //           PhotographId=clients.First().Id,
        //           Date=DateTime.Now,
        //           PhotosetId=clients.First().Id,
        //           ProductId=clients.First().Id,
        //           RecvisitId=clients.First().Id,
        //           UslugiId=clients.First().Id,
        //           Price=54444
        //       }
        //   };
        //}

        public DbSet<Client> Clients { get; set; }

        public DbSet<Dogovor> Dogovors { get; set; }

        public DbSet<Photogragh> Photograghs { get; set; }

        public DbSet<PhotoSet> PhotoSets { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Recvisit> Recvisits { get; set; }

        public DbSet<Uslugi> Uslugis { get; set; }
        public PhotoStudiyContext(DbContextOptions<PhotoStudiyContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClientEntityTypeConfiguration).Assembly);
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