namespace LightNovelApi.Models;

public class NovelAuthor
{
    public int NovelId { get; set; }
    public int AuthorId { get; set; }
    public Novel Novel { get; set; }
    public Author Author { get; set; }
}