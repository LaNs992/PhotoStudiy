using PhotoStudiy.API.Models.CreateRequest;

namespace PhotoStudiy.API.Models.Request
{
    public class PhotoSetRequest: CreatePhotoSetRequest
    {  /// <summary>
       /// Индитификатор
       /// </summary>
        public Guid Id { get; set; }
    }
}
