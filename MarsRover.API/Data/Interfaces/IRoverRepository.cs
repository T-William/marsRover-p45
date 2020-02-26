using System.Collections.Generic;
using System.Threading.Tasks;
using MarsRover.API.Helpers;
using MarsRover.API.Models;

namespace MarsRover.API.Data.Interfaces
{
    public interface IRoverRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
        Task<List<Rover>> GetRoversFull(int gridId);

        Task<PagedList<Rover>> GetPagedRoverList(RoverParams roverParams);

        Task<Rover> GetRover(int Id);
        Task<bool> RoverNameExists(string roverName);

    }
}