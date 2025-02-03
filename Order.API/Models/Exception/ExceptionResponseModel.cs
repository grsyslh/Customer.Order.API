using System.Net;

namespace Order.API.Exception
{
    public class ExceptionResponseModel
    {
        public HttpStatusCode HttpStatusCode { get; set; }

        public string ExceptionMessage { get; set; }
    }
}
