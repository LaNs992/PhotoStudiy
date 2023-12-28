using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudiy.Services.Contracts.Exceptions
{
    public class PhotoStudiyInvalidOperationException : PhotoStudiyException
    {  /// <summary>
       /// Инициализирует новый экземпляр <see cref="PhotoStudiyInvalidOperationException"/>
       /// с указанием сообщения об ошибке
       /// </summary>
        public PhotoStudiyInvalidOperationException(string message)
            : base(message)
        {

        }
    }
}
