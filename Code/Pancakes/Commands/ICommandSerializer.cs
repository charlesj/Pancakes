namespace Pancakes.Commands
{
    public interface ICommandSerializer
    {
        string Serialize(ICommand command);

        void DeserializeInto(string json, ICommand target);
    }
}