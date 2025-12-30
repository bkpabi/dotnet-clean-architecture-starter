using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationException = CleanArch.Domain.Exceptions.ApplicationException;


namespace CleanArch.ApplicationCore.ApplicationExceptions
{
    public class ValidationException : ApplicationException
    {

        public ValidationException(IReadOnlyDictionary<string, string[]> errorsDictionary)
                : base("Validation Failure", "One or more validation errors occurred")
                => ErrorsDictionary = errorsDictionary;

        public IReadOnlyDictionary<string, string[]> ErrorsDictionary { get; }
    }
}
