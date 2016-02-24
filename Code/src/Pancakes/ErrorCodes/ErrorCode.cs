namespace Pancakes.ErrorCodes
{
    public class ErrorCode
    {
        public string Identifier { get; }

        public string Description { get; }

        public ErrorCode(string identifier, string description)
        {
            Identifier = identifier;
            Description = description;
        }

        public override string ToString()
        {
            return $"{Identifier}: {Description}";
        }
    }
}
