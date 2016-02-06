using System.Net.Http;
using Pancakes.SanityChecks;

namespace Example.Console.SanityChecks
{
    public class InternetAccessSanityCheck : ICheckSanity
    {
        public bool Probe()
        {
            var httpClient = new HttpClient();
            try
            {
                var result = httpClient.GetAsync("http://www.google.com").GetAwaiter().GetResult();
                return result.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}
