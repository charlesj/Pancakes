// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationException.cs" company="Josh Charles">
//   Copyright (c) 2013 Josh Charles.  Released under the MIT license.
// </copyright>
// <summary>
//   The validation exception.  This exception is meant to be thrown when an object is encountered
//   that should be valid at that point, but is not.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Pancakes.Validation
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The validation exception.  This exception is meant to be thrown when an object is encountered
    /// that should be valid at that point, but is not.
    /// </summary>
    public class ValidationException : Exception
    {
        /// <summary>
        /// The data.
        /// </summary>
        private readonly System.Collections.IDictionary data;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class.
        /// </summary>
        public ValidationException() : this(new Result { IsValid = false })
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class.
        /// </summary>
        /// <param name="result">
        /// The result.
        /// </param>
        public ValidationException(Result result) : base("There was a problem validating the submission. See data property for messages.")
        {
            this.data = new Dictionary<string, string>();
            foreach (var message in result.FailureMessages)
            {
                this.data.Add(message.PropertyName, message.Message);
            }
        }

        /// <summary>
        /// Gets the data.
        /// </summary>
        public override System.Collections.IDictionary Data
        {
            get
            {
                return this.data;
            }
        }
    }
}