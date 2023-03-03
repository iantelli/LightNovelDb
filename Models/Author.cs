namespace LightNovelDb.Models;

public class Author
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    public Country Country { get; set; }
    public ICollection<NovelAuthor> NovelAuthors { get; set; }
}