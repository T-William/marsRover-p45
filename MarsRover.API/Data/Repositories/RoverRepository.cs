using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarsRover.API.Data.Interfaces;
using MarsRover.API.Helpers;
using MarsRover.API.Models;
using Microsoft.EntityFrameworkCore;

namespace MarsRover.API.Data.Repositories
{
    public class RoverRepository : IRoverRepository
    {
        private readonly DataContext _context;
        public RoverRepository(DataContext context)
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

        public async Task<Rover> GetRover(int Id)
        {
            var rover = await _context.Rover.FirstOrDefaultAsync(r => r.Id == Id);
            return rover;

        }

        public async Task<List<Rover>> GetRoversFull(int gridId)
        {
            return await _context.Rover.Where(x=>x.GridId == gridId).ToListAsync();
        }

        public async Task<PagedList<Rover>> GetPagedRoverList(RoverParams roverParams){
            var rovers = _context.Rover.OrderBy(r=>r.Id).AsQueryable();

            if (!string.IsNullOrEmpty(roverParams.OrderBy))
            {
                switch (roverParams.OrderBy)
                {
                    case "name":
                        rovers = rovers.OrderByDescending(u => u.Name);
                        break;
                    default:
                        rovers = rovers.OrderByDescending(u => u.Id);
                        break;
                }
            }

            return await PagedList<Rover>.CreateAsync(rovers,roverParams.PageNumber,roverParams.PageSize);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() >0;
        }

        public async Task<bool> RoverNameExists(string roverName)
        {
            return await _context.Rover.AnyAsync(r=>r.Name== roverName);
        }
    }
}