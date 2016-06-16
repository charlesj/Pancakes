using System.Threading.Tasks;

namespace Pancakes.Commands
{
    public interface ICommand
    {
        Task<bool> AuthorizeAsync();
        Task<bool> ValidateAsync();
        Task<object> ExecuteAsync();
    }
}