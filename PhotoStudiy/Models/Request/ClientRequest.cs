using PhotoStudiy.API.Models.CreateRequest;

namespace PhotoStudiy.API.Models.Request
{
    public class ClientRequest: CreateClientRequest
    {
        /// <summary>
        /// Индитификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}
