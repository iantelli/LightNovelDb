﻿using LightNovelDb.Data;
using LightNovelDb.Interfaces;
using LightNovelDb.Models;
using Microsoft.EntityFrameworkCore;

namespace LightNovelDb.Repository;

public class ReviewerRepository : IReviewerRepository
{
    private readonly DataContext _context;
    public ReviewerRepository(DataContext context)
    {
        _context = context;
    }
    public ICollection<Reviewer> GetReviewers()
    {
        return _context.Reviewers.ToList();
    }

    public Reviewer GetReviewer(int reviewerId)
    {
        return _context.Reviewers.Where(r => r.Id == reviewerId).Include(e => e.Reviews).FirstOrDefault();
    }

    public ICollection<Review> GetReviewsByReviewer(int reviewerId)
    {
        return _context.Reviews.Where(r => r.Reviewer.Id == reviewerId).ToList();
    }

    public bool ReviewerExists(int reviewerId)
    {
        return _context.Reviewers.Any(r => r.Id == reviewerId);
    }
}