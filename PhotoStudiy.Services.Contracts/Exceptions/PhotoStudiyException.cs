using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 using global::System.Threading;


namespace PhotoStudiy.Services.Contracts.Exceptions
{
    public abstract class PhotoStudiyException : Exception
    {

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="PhotoStudiyException"/> без параметров
        /// </summary>
        protected PhotoStudiyException() { }

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="PhotoStudiyException"/> с указанием
        /// сообщения об ошибке
        /// </summary>
        protected PhotoStudiyException(string message)
            : base(message) { }
    }
}
