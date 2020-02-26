using System.Collections.Generic;
using System.Threading.Tasks;
using MarsRover.API.Data.Interfaces;
using MarsRover.API.Models;
using Microsoft.EntityFrameworkCore;

namespace MarsRover.API.Data.Repositories
{
    public class MarsGridRepository : IMarsGridRepository
    {
        private readonly DataContext _context;
        public MarsGridRepository(DataContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<MarsGrid> GetGrid(int id)
        {
            return await _context.Grid.FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<MarsGrid> GetDefaultGrid()
        {
            var grid = await _context.Grid.FirstOrDefaultAsync();
            return grid;
        }

        public async Task<List<MarsGrid>> GetGridsFull()
        {
            return await _context.Grid.ToListAsync();
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}