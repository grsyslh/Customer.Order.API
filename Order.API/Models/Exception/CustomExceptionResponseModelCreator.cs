using TechBuddy.Middlewares.ExceptionHandling;
using TechBuddy.Middlewares.ExceptionHandling.Infrastructure;

namespace Order.API.Exception
{
    public class CustomExceptionResponseModelCreator : IResponseModelCreator
    {
        public object CreateModel(ModelCreatorContext model)
        {
            return new ExceptionResponseModel()
            {
                ExceptionMessage = model.Exception.Message,
                HttpStatusCode = model.HttpStatusCode
            };
        }
    }
}
