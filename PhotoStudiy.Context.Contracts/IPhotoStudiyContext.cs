using Microsoft.EntityFrameworkCore;
using PhotoStudiy.Context.Contracts.Models;

namespace PhotoStudiy.Context.Contracts
{
    /// <summary>
    /// Контекст работы с сущностями
    /// </summary>
    public interface IPhotoStudiyContext
    {
        /// <summary>
        /// Кинотеатры
        /// </summary>
        DbSet<Client> Clients { get; }
        /// <summary>
        /// договор
        /// </summary>
        DbSet<Dogovor> Dogovors { get; }
        /// <summary>
        /// фотограф
        /// </summary>
        DbSet<Photogragh> Photograghs { get; }
        /// <summary>
        /// фотосессия
        /// </summary>
        DbSet<PhotoSet> PhotoSets{ get; }
        /// <summary>
        /// продукт
        /// </summary>
        DbSet<Product> Products { get; }
        /// <summary>
        /// реквизит
        /// </summary>
        DbSet<Recvisit> Recvisits { get; }
        /// <summary>
        /// услуги
        /// </summary>
        DbSet<Uslugi> Uslugis { get; }



    }
}