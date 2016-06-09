using System.Net.Http;
using System.Threading.Tasks;
using Pancakes.Commands;

namespace Example.Console.Commands
{
    public class DownloadUrlCommand : ICommand
    {
        public string Url { get; set; }

        public Task<bool> AuthorizeAsync()
        {
            return Task.FromResult(true);
        }

        public Task<bool> ValidateAsync()
        {
            var validation = !string.IsNullOrWhiteSpace(this.Url);
            return Task.FromResult(validation);
        }

        public async Task ExecuteAsync()
        {
            using (var client = new HttpClient())
            {
                var html = await client.GetStringAsync(this.Url);
                System.Console.WriteLine(html);
            }
        }
    }
}
