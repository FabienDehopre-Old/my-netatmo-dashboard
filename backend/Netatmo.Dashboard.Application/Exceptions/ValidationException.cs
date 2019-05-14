using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security;

namespace Netatmo.Dashboard.Application.Exceptions
{
    [Serializable]
    public class ValidationException : Exception
    {
        public ValidationException()
            : base("One or more validation failures have occurred.")
        {
            Failures = new Dictionary<string, string[]>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            var propertyNames = failures
                .Select(e => e.PropertyName)
                .Distinct();
            foreach (var propertyName in propertyNames)
            {
                var propertyFailures = failures
                    .Where(e => e.PropertyName == propertyName)
                    .Select(e => e.ErrorMessage)
                    .ToArray();
                Failures.Add(propertyName, propertyFailures);
            }
        }

        protected ValidationException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
            Failures = info.GetValue(nameof(Failures), typeof(Dictionary<string, string[]>)) as Dictionary<string, string[]>;
        }

        public IDictionary<string, string[]> Failures { get; }

        [SecurityCritical]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Failures), Failures);
            base.GetObjectData(info, context);
        }
    }
}
