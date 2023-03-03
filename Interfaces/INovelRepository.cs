using LightNovelDb.Models;

namespace LightNovelDb.Interfaces;

public interface INovelRepository
{
    ICollection<Novel> GetNovels();
}