using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudiy.Services.Contracts.Exceptions
{
    public class PhotoStudiyEntityNotFoundException<TEntity> : PhotoStudiyNotFoundException
    {
        public PhotoStudiyEntityNotFoundException(Guid id)
           : base($"Сущность {typeof(TEntity)} c id = {id} не найдена.")
        {
        }
    }
}
