namespace Pancakes.Commands
{
    public interface ICommandResult
    {
        CommandResultType ResultTypeType { get; }
        int ExecutionTime { get; }
    }
}