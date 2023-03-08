using LightNovelApi.Models;

namespace LightNovelApi.Interfaces;

public interface IAuthorRepository
{
    ICollection<Author> GetAuthors();
    Author GetAuthor(int id);
    ICollection<Author> GetAuthorsOfANovel(int novelId);
    ICollection<Novel> GetNovelsByAuthor(int authorId);
    bool AuthorExists(int id);
    bool AddAuthor(Author author);
    bool UpdateAuthor(Author author);
    bool DeleteAuthor(Author author);
    bool Save();
}