using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudiy.Services.Contracts.Exception
{
    public class TimeTableEntityNotFoundException<TEntity> : TimeTableNotFoundException
    {
        public TimeTableEntityNotFoundException(Guid id)
           : base($"Сущность {typeof(TEntity)} c id = {id} не найдена.")
        {
        }
    }
}
