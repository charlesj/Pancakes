using System.Net.Http;
using Pancakes.Commands;

namespace Example.Console.Commands
{
    public class DownloadUrlCommand : ICommand
    {
        public string Url { get; set; }

        public bool Authorize()
        {
            return true;
        }

        public bool Validate()
        {
            return !string.IsNullOrWhiteSpace(this.Url);
        }

        public async void Execute()
        {
            using (var client = new HttpClient())
            {
                var html = await client.GetStringAsync(this.Url);
                System.Console.WriteLine(html);
            }
        }
    }
}
