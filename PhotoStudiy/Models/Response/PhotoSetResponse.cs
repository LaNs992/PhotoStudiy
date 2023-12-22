namespace PhotoStudiy.API.Models.Response
{
    public class PhotoSetResponse
    {
        /// <summary>
        /// Индитификатор
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
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
