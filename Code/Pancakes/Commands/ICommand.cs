namespace Pancakes.Commands
{
    public interface ICommand
    {
        bool Authorize();
        bool Validate();
        void Execute();
    }
}