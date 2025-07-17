namespace Domain.Models.Exceptions;

public class MovieNotFoundException : NotFoundException
    {
    public MovieNotFoundException(int id) : base($"Movie with id {id} was not found") { }
    }
