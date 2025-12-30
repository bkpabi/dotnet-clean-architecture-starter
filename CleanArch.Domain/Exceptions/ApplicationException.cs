using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Domain.Exceptions;

public class ApplicationException : Exception
{
    protected ApplicationException(string title, string message)
           : base(message) =>
           Title = title;

    public string Title { get; }
}
