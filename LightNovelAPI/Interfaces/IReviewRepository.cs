using LightNovelDb.Models;

namespace LightNovelDb.Interfaces;

public interface IReviewRepository
{
    ICollection<Review> GetReviews();
    Review GetReview(int reviewId);
    ICollection<Review> GetReviewsOfANovel(int novelId);
    bool ReviewExists(int reviewId);
}