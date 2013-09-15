namespace Pancakes.Validation
{
    using System.Collections.Generic;

    /// <summary>
    /// The validation result.
    /// </summary>
    public class Result
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Result"/> class.
        /// </summary>
        public Result()
        {
            this.FailureMessages = new List<FailureMessage>();
            this.IsValid = false;
        }

        /// <summary>
        /// Gets or sets a value indicating whether is valid.
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// Gets or sets the failure messages.
        /// </summary>
        public List<FailureMessage> FailureMessages { get; set; }
    }
}
