using PhotoStudiy.API.Models.CreateRequest;

namespace PhotoStudiy.API.Models.Request
{
    public class ProductRequest:CreateProductRequest
    {  /// <summary>
       /// Индитификатор
       /// </summary>
        public Guid Id { get; set; }
    }
}
