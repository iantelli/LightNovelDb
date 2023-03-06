namespace LightNovelApi.Models;

public class Review
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int Rating { get; set; }
    
    public Reviewer Reviewer { get; set; }
    public Novel Novel { get; set; }
}