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
}