using LightNovelDb.Models;
using Microsoft.EntityFrameworkCore;

namespace LightNovelDb.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {   
    }
    
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Novel> Novels { get; set; }
    public DbSet<NovelAuthor> NovelAuthors { get; set; }
    public DbSet<NovelGenre> NovelGenres { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Reviewer> Reviewers { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<NovelGenre>()
            .HasKey(ng => new { ng.NovelId, ng.GenreId });
        modelBuilder.Entity<NovelGenre>()
            .HasOne(n => n.Novel)
            .WithMany(ng => ng.NovelGenres)
            .HasForeignKey(n => n.NovelId);
        modelBuilder.Entity<NovelGenre>()
            .HasOne(g => g.Genre)
            .WithMany(ng => ng.NovelGenres)
            .HasForeignKey(g => g.GenreId);
        
        modelBuilder.Entity<NovelAuthor>()
            .HasKey(na => new { na.NovelId, na.AuthorId });
        modelBuilder.Entity<NovelAuthor>()
            .HasOne(n => n.Novel)
            .WithMany(na => na.NovelAuthors)
            .HasForeignKey(n => n.NovelId);
        modelBuilder.Entity<NovelAuthor>()
            .HasOne(a => a.Author)
            .WithMany(na => na.NovelAuthors)
            .HasForeignKey(a => a.AuthorId);
    }
    
}