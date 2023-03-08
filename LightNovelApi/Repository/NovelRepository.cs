using LightNovelApi.Data;
using LightNovelApi.Interfaces;
using LightNovelApi.Models;

namespace LightNovelApi.Repository;

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
    public bool AddNovel(int authorId, int genreId, Novel novel)
    {
        var novelAuthorEntity = _context.Authors.Where(a => a.Id == authorId).FirstOrDefault();
        var novelGenreEntity = _context.Genres.Where(g => g.Id == genreId).FirstOrDefault();

        var novelAuthor = new NovelAuthor()
        {
            Author = novelAuthorEntity,
            Novel = novel,
        };

        var novelGenre = new NovelGenre()
        {
            Genre = novelGenreEntity,
            Novel = novel,
        };
        
        _context.Add(novelAuthor);
        _context.Add(novelGenre);
        _context.Add(novel);
        
        return Save();
    }
    public bool UpdateNovel(int authorId, int genreId, Novel novel)
    {
        var authorEntity = _context.Authors.Where(a => a.Id == authorId).FirstOrDefault();
        var genreEntity = _context.Genres.Where(g => g.Id == genreId).FirstOrDefault();
        var novelAuthorEntity = _context.NovelAuthors.Where(na => na.NovelId == novel.Id).FirstOrDefault();
        var novelGenreEntity = _context.NovelGenres.Where(ng => ng.NovelId == novel.Id).FirstOrDefault();

        if (novelAuthorEntity != null)
        {
            novelAuthorEntity.Author = authorEntity;
            _context.Update(novelAuthorEntity);
        }
        else
        {
            var novelAuthor = new NovelAuthor()
            {
                Author = authorEntity,
                Novel = novel,
            };
            _context.Add(novelAuthor);
        }
        
        if (novelGenreEntity != null)
        {
            novelGenreEntity.Genre = genreEntity;
            _context.Update(novelGenreEntity);
        }
        else
        {
            var novelGenre = new NovelGenre()
            {
                Genre = genreEntity,
                Novel = novel,
            };
            _context.Add(novelGenre);
        }
        
        _context.Update(novel);
        return Save();
    }
    public bool DeleteNovel(Novel novel)
    {
        _context.Remove(novel);
        return Save();
    }
    public bool Save()
    {
        return _context.SaveChanges() > 0 ? true : false;
    }
}