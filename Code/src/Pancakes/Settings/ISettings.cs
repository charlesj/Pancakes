namespace Pancakes.Settings
{
    public interface ISettings
    {
        string ApplicationName { get; } 
        string EnvironmentName { get; }
    }
}