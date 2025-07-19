namespace Movies.Core.Exceptions;

public class DtoBadRequestException : BadRequestException
{
    public DtoBadRequestException(string info) : base($"Provide required {info} in json body.") { }
}
