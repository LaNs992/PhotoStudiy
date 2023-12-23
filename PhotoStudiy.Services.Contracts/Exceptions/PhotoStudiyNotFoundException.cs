using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudiy.Services.Contracts.Exceptions
{
    public class PhotoStudiyNotFoundException: PhotoStudiyException
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="PhotoStudiyNotFoundException"/> с указанием
        /// сообщения об ошибке
        /// </summary>
        public PhotoStudiyNotFoundException(string message)
            : base(message)
        { }
    }
}
