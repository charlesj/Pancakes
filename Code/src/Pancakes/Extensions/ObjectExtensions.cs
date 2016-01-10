using Newtonsoft.Json;

namespace Pancakes.Extensions
{
    public static class ObjectExtensions
    {
        public static string ToJson(this object obj, bool formatted = false)
        {
            if (formatted)
                return JsonConvert.SerializeObject(obj, Formatting.Indented);
            return JsonConvert.SerializeObject(obj);
        }
    }
}
