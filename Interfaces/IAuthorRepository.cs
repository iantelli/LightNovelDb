using LightNovelDb.Models;

namespace LightNovelDb.Interfaces;

public interface IAuthorRepository
{
    ICollection<Author> GetAuthors();
    Author GetAuthor(int id);
    ICollection<Author> GetAuthorsOfANovel(int novelId);
    ICollection<Novel> GetNovelsByAuthor(int authorId);
    bool AuthorExists(int id);
    bool AddAuthor(Author author);
    bool Save();
}