using PhotoStudiy.API.Models.CreateRequest;

namespace PhotoStudiy.API.Models.Request
{
    public class PhotographRequest:CreatePhotographRequest
    {  /// <summary>
       /// Индитификатор
       /// </summary>
        public Guid Id { get; set; }
    }
}
