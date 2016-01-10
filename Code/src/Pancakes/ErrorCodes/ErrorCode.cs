namespace Pancakes.ErrorCodes
{
    public class ErrorCode
    {
        public string Identifier { get; private set; }

        public string Description { get; private set; }

        public ErrorCode(string identifier, string description)
        {
            this.Identifier = identifier;
            this.Description = description;
        }
    }
}
