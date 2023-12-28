using PhotoStudiy.API.Models.CreateRequest;

namespace PhotoStudiy.API.Models.Request
{
    public class UslugiRequest: CreateUslugiRequest
    {  /// <summary>
       /// Индитификатор
       /// </summary>
        public Guid Id { get; set; }
    }
}
