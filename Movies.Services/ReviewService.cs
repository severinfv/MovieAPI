using Domain.Contracts.Repositories;
using Domain.Models.Entities;
using Domain.Models.Exceptions;
using Microsoft.EntityFrameworkCore;
using Movies.Shared.DTOs;
using Service.Contracts;

namespace Movies.Services;

public class ReviewService : IReviewService
{
    private IUnitOfWork uow;
    public ReviewService(IUnitOfWork uow)
    {
        this.uow = uow;
    }
    public async Task<bool> ReviewExistsAsync(int id) => await uow.ReviewRepository.ExistsAsync(id);

    public async Task<ReviewDto> GetReviewAsync(int id, bool trackChanges = false)
    {
        var review = await uow.ReviewRepository.GetByIdAsync(id, trackChanges);
        if (review == null) return null!;

        var dto = new ReviewDto(review.UserName, review.Comment, review.Rating);
        return dto;
    }

    public async Task<IEnumerable<ReviewDto>> GetReviewsAsync(bool trackChanges = false)
    {
        var reviews = await uow.ReviewRepository.GetAllAsync(trackChanges);
        var dtos = reviews.Select(r => new ReviewDto(r.UserName, r.Comment, r.Rating));
        return dtos;
    }

    public async Task<IEnumerable<ReviewDto>> GetReviewsFromMovieAsync(int movieId, bool trackChanges = false)
    {
        var reviews = await uow.ReviewRepository.GetReviewsByMovieIdAsync(movieId, trackChanges) ?? throw new MovieNotFoundException(movieId);
        var dtos = reviews.Select(r => new ReviewDto(r.UserName, r.Comment, r.Rating));
        return dtos;
    }



}
