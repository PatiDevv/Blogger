﻿using Domain.Entity;
using Domain.Interface;
using Infrastructure.Data;
using Infrastructure.ExtensionMethods;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly BloggerContext _context;

        public PostRepository(BloggerContext context)
        {
            _context = context;
        }

        public IQueryable<Post> GetAll()
        {
            return _context.Posts.AsQueryable();
        }

        public async Task<IEnumerable<Post>> GetAllAsync(int pageNumber, int pageSize, string sortField, bool ascending, string filterBy)
        {
            return await _context.Posts
                .Where(x => x.Title.ToLower().Contains(filterBy.ToLower()) || x.Content.ToLower().Contains(filterBy.ToLower()))
                .OrderByPropertyName(sortField, ascending)
                .Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<int> GetAllCountAsync(string filterBy)
        {
            return await _context.Posts
                .Where(x => x.Title.ToLower().Contains(filterBy.ToLower()) || x.Content.ToLower().Contains(filterBy.ToLower()))
                .CountAsync();
        }

        public async Task<Post> GetByIdAsync(int id)
        {
            return await _context.Posts.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Post> AddAsync(Post post)
        {

            var creadtedPost = await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
            return creadtedPost.Entity;
        }

        public async Task UpdateAsync(Post post)
        {

            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Post post)
        {
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            await Task.CompletedTask;

        }
    }
}
