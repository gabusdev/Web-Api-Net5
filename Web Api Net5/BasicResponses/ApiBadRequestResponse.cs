using System;
using System.Collections.Generic;
using System.Linq;

namespace BasicResponses
{
    public class ApiBadRequestResponse : ApiResponse

    {
        public ApiBadRequestResponse(FluentValidation.Results.ValidationResult results)
            : base(400)
        {
            if (results.IsValid)
            {
                throw new ArgumentException("ModelState must be invalid", nameof(results));
            }

            Errors = results.Errors.Select(e => $"Property { e.PropertyName} is not valid. { e.ErrorMessage }");
        }

        public IEnumerable<string> Errors { get; set; }
    }
}