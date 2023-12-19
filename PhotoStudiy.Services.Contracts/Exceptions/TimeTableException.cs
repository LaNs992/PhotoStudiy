using System;
using System.Exception;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 using global::System.Threading;


namespace PhotoStudiy.Services.Contracts.Exceptions
{
    public abstract class TimeTableException : Exception
    {

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="TimeTableException"/> без параметров
        /// </summary>
        protected TimeTableException() { }

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="TimeTableException"/> с указанием
        /// сообщения об ошибке
        /// </summary>
        protected TimeTableException(string message)
            : base(message) { }
    }
}
