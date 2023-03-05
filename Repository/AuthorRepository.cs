﻿using LightNovelDb.Data;
using LightNovelDb.Interfaces;
using LightNovelDb.Models;

namespace LightNovelDb.Repository;

public class AuthorRepository : IAuthorRepository
{
    private readonly DataContext _context;
    public AuthorRepository(DataContext context)
    {
        _context = context;
    }
    public ICollection<Author> GetAuthors()
    {
        return _context.Authors.ToList();
    }

    public Author GetAuthor(int id)
    {
        return _context.Authors.Where(a => a.Id == id).FirstOrDefault();
    }

    public ICollection<Author> GetAuthorsOfANovel(int novelId)
    {
        return _context.NovelAuthors.Where(n => n.Novel.Id == novelId).Select(a => a.Author).ToList();
    }

    public ICollection<Novel> GetNovelsByAuthor(int authorId)
    {
        return _context.NovelAuthors.Where(a => a.Author.Id == authorId).Select(n => n.Novel).ToList();
    }

    public bool AuthorExists(int id)
    {
        return _context.Authors.Any(a => a.Id == id);
    }
    public bool AddAuthor(Author author)
    {
        _context.Add(author);
        return Save();
    }
    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved >= 0 ? true : false;
    }
}