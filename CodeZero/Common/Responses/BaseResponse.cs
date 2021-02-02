using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;

namespace CodeZero.Common.Responses
{
    public class BaseResponse
    {
        public BaseResponse()
        {
            IsSuccess = false;
        }

        public BaseResponse(IEnumerable<ValidationFailure> failures)
        {
            IsSuccess = false;

            ValidationIssues = new List<ValidationIssue>();

            var propertyNames = failures
                .Select(e => e.PropertyName)
                .Distinct();

            foreach (var propertyName in propertyNames)
            {
                // Each PropertyName get's an array of failures associated with it:
                var PropertyFailures = failures
                    .Where(e => e.PropertyName == propertyName)
                    .Select(e => e.ErrorMessage)
                    .ToArray();

                var propertyFailure = new ValidationIssue { PropertyName = propertyName, PropertyFailures = PropertyFailures.ToList() };
                ValidationIssues.Add(propertyFailure);
            }
        }

        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        //public Object Object { get; set; }

        public IList<ValidationIssue> ValidationIssues;
    }
}
