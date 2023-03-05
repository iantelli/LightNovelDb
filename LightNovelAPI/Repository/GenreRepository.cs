using LightNovelDb.Data;
using LightNovelDb.Interfaces;
using LightNovelDb.Models;

namespace LightNovelDb.Repository;

public class GenreRepository : IGenreRepository
{
    private DataContext _context;
    public GenreRepository(DataContext context)
    {
        _context = context;
    }
    public bool GenreExists(int id)
    {
        return _context.Genres.Any(g => g.Id == id);
    }
    public bool CreateGenre(Genre genre)
    {
        _context.Add(genre);
        return Save();
    }
    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }

    public Genre GetGenre(int id)
    {
        return _context.Genres.Where(g => g.Id == id).FirstOrDefault();
    }
    
    public ICollection<Genre> GetGenres()
    {
        return _context.Genres.ToList();
    }
    
    public ICollection<Novel> GetNovelsByGenre(int genreId)
    {
        return _context.NovelGenres.Where(g => g.GenreId == genreId).Select(n => n.Novel).ToList();
    }
}