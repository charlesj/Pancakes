using System.Threading.Tasks;

namespace Pancakes.SanityChecks
{
    public interface ICheckSanity
    {
        Task<bool> Probe();
    }
}