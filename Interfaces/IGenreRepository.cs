using LightNovelDb.Models;

namespace LightNovelDb.Interfaces;

public interface IGenreRepository
{
    ICollection<Genre> GetGenres();
    Genre GetGenre(int id);
    ICollection<Novel> GetNovelsByGenre(int genreId);
    bool GenreExists(int id);

}