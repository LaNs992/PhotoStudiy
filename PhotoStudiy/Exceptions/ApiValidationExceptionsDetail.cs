using PhotoStudiy.General;

namespace PhotoStudiy.API.Exceptions
{
    public class ApiValidationExceptionsDetail
    {
        /// <summary>
        /// Ошибки валидации
        /// </summary>
        public IEnumerable<InvalidateItemModel> Errors { get; set; } = Array.Empty<InvalidateItemModel>();
    }
}
