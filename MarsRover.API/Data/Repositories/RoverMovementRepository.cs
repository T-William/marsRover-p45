using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarsRover.API.Data.Interfaces;
using MarsRover.API.Models;
using Microsoft.EntityFrameworkCore;

namespace MarsRover.API.Data.Repositories
{
    public class RoverMovementRepository : IRoverMovementRepository
    {
        
        private readonly DataContext _context;
        public RoverMovementRepository(DataContext context)
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

        public async Task<RoverMovement> GetRecentMovement(int roverId)
        {
            return await _context.RoverMovement.OrderByDescending(x=>x.MovementDate).FirstOrDefaultAsync(r=>r.RoverId== roverId);
        }

        public async Task<RoverMovement> GetRoverMovement(int roverId)
        {
            return await _context.RoverMovement.FirstOrDefaultAsync(r=>r.RoverId == roverId);
        }

        public async Task<List<RoverMovement>> GetSingleRoverMovementsFull(int roverId)
        {
            return await _context.RoverMovement.Where(x=>x.RoverId ==roverId).OrderByDescending(r=>r.MovementDate).ToListAsync();
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() >0;
        }
    }
}