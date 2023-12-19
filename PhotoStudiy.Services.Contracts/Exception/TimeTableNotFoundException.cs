﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudiy.Services.Contracts.Exception
{
    public class TimeTableNotFoundException: TimeTableException
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="TimeTableNotFoundException"/> с указанием
        /// сообщения об ошибке
        /// </summary>
        public TimeTableNotFoundException(string message)
            : base(message)
        { }
    }
}
