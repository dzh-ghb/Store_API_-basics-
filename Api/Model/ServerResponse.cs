using System.Net;

namespace Api.Model
{
    // описание собирательной модели ответа сервера
    public class ServerResponse
    {
        public ServerResponse()
        {
            this.IsSuccess = true;
            this.ErrorMessages = new();
        }

        public bool IsSuccess { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public List<string> ErrorMessages { get; set; }
        public object Result { get; set; }
    }
}