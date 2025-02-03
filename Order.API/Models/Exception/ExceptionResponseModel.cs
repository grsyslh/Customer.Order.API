using System.Net;

namespace Order.API.Models.Exception
{
    public class ExceptionResponseModel
    {
        public HttpStatusCode HttpStatusCode { get; set; }

        public string ExceptionMessage { get; set; }
    }
}
