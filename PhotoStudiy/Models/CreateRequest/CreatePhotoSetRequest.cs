namespace PhotoStudiy.API.Models.CreateRequest
{
    public class CreatePhotoSetRequest
    {  /// <summary>
       /// Название фотосессии
       /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Описние
        /// </summary>
        public string Description { get; set; } = string.Empty;
        /// <summary>   
        /// Цена за фотосесисию
        /// </summary>
        public string Price { get; set; } = string.Empty;
    }
}
