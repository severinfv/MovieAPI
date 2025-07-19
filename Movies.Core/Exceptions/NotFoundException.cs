using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Core.Exceptions;
public class BadRequestException : Exception
{
    public string Title { get; }
    public BadRequestException(string message, string title = "Bad Request") : base(message)
    {
        Title = title;
    }
}
