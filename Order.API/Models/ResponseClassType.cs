using System.Reflection;

namespace OfferSale.Service.Validators
{
    public static class ResponseClassType
    {
        public static List<Type> GetReponseClassType()
        {
            var q = from t in Assembly.GetExecutingAssembly().GetTypes()
                    where t.IsClass && (t.Name.Contains("QueryResponse") || t.Name.Contains("CommandResponse"))
                    select t;
            return q.ToList();
        }
    }
}
