using System.Collections.Generic;
using System.Threading.Tasks;
using MarsRover.API.Models;

namespace MarsRover.API.Data.Interfaces
{
    public interface IRoverMovementRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
        Task<RoverMovement> GetRoverMovement(int RoverId);
        Task<RoverMovement> GetRecentMovement(int roverId);
        Task<List<RoverMovement>> GetSingleRoverMovementsFull(int roverId);

    }
}