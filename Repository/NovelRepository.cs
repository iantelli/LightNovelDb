using LightNovelDb.Data;
using LightNovelDb.Interfaces;
using LightNovelDb.Models;

namespace LightNovelDb.Repository;

public class NovelRepository : INovelRepository
{
    private readonly DataContext _context;
    
    public NovelRepository(DataContext context)
    {
        _context = context;
    }

    public ICollection<Novel> GetNovels()
    {
        return _context.Novels.OrderBy(n => n.Id).ToList();
    }

    public Novel GetNovel(int id)
    {
        return _context.Novels.Where(n => n.Id == id).FirstOrDefault();
    }

    public Novel GetNovel(string title)
    {
        return _context.Novels.Where(n => n.Title == title).FirstOrDefault();
    }

    public decimal GetNovelRating(int id)
    {
        var review = _context.Reviews.Where(n => n.Id == id);

        if (review.Count() <= 0)
            return 0;

        return ((decimal)review.Sum(r => r.Rating) / review.Count());
    }

    public bool NovelExists(int novelId)
    {
        return _context.Novels.Any(n => n.Id == novelId);
    }
}