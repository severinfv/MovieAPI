using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Core.Exceptions;
public class BusinessRuleException : Exception
{
    public string Title { get; }
    public BusinessRuleException(string message, string title = "Business Rule Violation") : base(message)
    {
        Title = title;
    }
}
