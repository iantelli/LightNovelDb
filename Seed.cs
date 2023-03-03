using LightNovelDb.Data;
using LightNovelDb.Models;

namespace LightNovelDb;

public class Seed
{
        private readonly DataContext dataContext;
        public Seed(DataContext context)
        {
            this.dataContext = context;
        }
        public void SeedDataContext()
        {
            if (!dataContext.NovelAuthors.Any())
            {
                var novelAuthors = new List<NovelAuthor>()
                {
                    new NovelAuthor()
                    {
                        Novel = new Novel()
                        {
                            Title = "Ascendance of a Bookworm",
                            Published = new DateTime(2015,11,1),
                            NovelGenres = new List<NovelGenre>()
                            {
                                new NovelGenre { Genre = new Genre() { Name = "Fantasy"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review { Title="Ascendance of a Bookworm", Content = "My favourite light novel of all time", Rating = 5,
                                Reviewer = new Reviewer(){ Name = "Ian"} },
                                new Review { Title="Ascendance of a Bookworm", Content = "Great read", Rating = 5,
                                Reviewer = new Reviewer(){ Name = "Bradley" } },
                                new Review { Title="Ascendance of a Bookworm",Content = "boring", Rating = 1,
                                Reviewer = new Reviewer(){ Name = "Stacy" } },
                            }
                        },
                        Author = new Author()
                        {
                            FirstName = "Miya",
                            LastName = "Kazuki",
                            Country = new Country()
                            {
                                Name = "Japan"
                            }
                        }
                    },
                    new NovelAuthor()
                    {
                        Novel = new Novel()
                        {
                            Title = "Solo Leveling",
                            Published = new DateTime(2014,2,14),
                            NovelGenres = new List<NovelGenre>()
                            {
                                new NovelGenre { Genre = new Genre() { Name = "Action"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review { Title="Solo Leveling",Content = "What a good read", Rating = 5,
                                Reviewer = new Reviewer(){ Name = "Ian" } },
                                new Review { Title="Solo Leveling",Content = "would read again", Rating = 5,
                                Reviewer = new Reviewer(){ Name = "Bradley" } },
                                new Review { Title="Solo Leveling",Content = "boring boring", Rating = 1,
                                Reviewer = new Reviewer(){ Name = "Stacy" } },
                            }
                        },
                        Author = new Author()
                        {
                            FirstName = "Chu",
                            LastName = "Gong",
                            Country = new Country()
                            {
                                Name = "Korea"
                            }
                        }
                    }
                };
                dataContext.NovelAuthors.AddRange(novelAuthors);
                dataContext.SaveChanges();
            }
        }  
}