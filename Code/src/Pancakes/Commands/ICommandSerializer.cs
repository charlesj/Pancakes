using Newtonsoft.Json;
using Pancakes.Extensions;

namespace Pancakes.Commands
{
    public interface ICommandSerializer
    {
        string Serialize(ICommand command);

        void DeserializeInto(string json, ICommand target);
    }

    public class CommandSerializer : ICommandSerializer
    {
        public string Serialize(ICommand command)
        {
            return command.ToJson();
        }

        public void DeserializeInto(string json, ICommand target)
        {
            JsonConvert.PopulateObject(json, target);
        }
    }
}