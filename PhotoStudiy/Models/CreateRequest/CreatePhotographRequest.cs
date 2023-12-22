namespace PhotoStudiy.API.Models.CreateRequest
{
    public class CreatePhotographRequest
    { /// <summary>
      /// Имя
      /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; } = string.Empty;
        /// <summary>
        /// Номер телфона
        /// </summary>
        public string Number { get; set; } = string.Empty;
    }
}
