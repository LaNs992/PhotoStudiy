using PhotoStudiy.API.Models.CreateRequest;

namespace PhotoStudiy.API.Models.Request
{
    public class RecvisitRequest: CreateRecvisitRequest
    {  /// <summary>
       /// Индитификатор
       /// </summary>
        public Guid Id { get; set; }
    }
}
