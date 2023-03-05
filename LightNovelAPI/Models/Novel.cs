namespace LightNovelDb.Models;

public class Novel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Cover { get; set; }
    public DateTime Published { get; set; }
    public ICollection<Review> Reviews { get; set; }
    public ICollection<NovelAuthor> NovelAuthors { get; set; }
    public ICollection<NovelGenre> NovelGenres { get; set; }
}