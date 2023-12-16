using PhotoStudiy.Common.Entity.EntityInterface;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudiy.Repositories.Contracts.WriteRepositoriesContracts
{

    /// <summary>
    /// Интерфейс для работы с БД
    /// </summary>
    /// <typeparam name="TEntity"> Сущность из БД</typeparam>
    public interface IRepositoryWriter<in TEntity> where TEntity : class, IEntity
    {
        /// <summary>
        /// Добавить новую запись
        /// </summary>
        void Add([NotNull] TEntity entity);

        /// <summary>
        /// Изменить запись
        /// </summary>
        void Update([NotNull] TEntity entity);

        /// <summary>
        /// Удалить запись
        /// </summary>
        void Delete([NotNull] TEntity entity);
    }
}
