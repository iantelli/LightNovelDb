using LightNovelDb.Models;

namespace LightNovelDb.Interfaces;

public interface INovelRepository
{
    ICollection<Novel> GetNovels();
    Novel GetNovel(int id);
    Novel GetNovel(string title);
    decimal GetNovelRating(int id);
    bool NovelExists(int novelId);
}