using System.Net.Http;
using System.Threading.Tasks;
using Pancakes.SanityChecks;

namespace Example.Console.SanityChecks
{
    public class InternetAccessSanityCheck : ICheckSanity
    {
        public async Task<bool> Probe()
        {
            var httpClient = new HttpClient();
            try
            {
                var result = await httpClient.GetAsync("http://www.google.com");
                return result.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}
