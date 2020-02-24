using System.Collections.Generic;
using System.Threading.Tasks;
using MarsRover.API.Models;

namespace MarsRover.API.Data.Interfaces
{
    public interface IMarsGridRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
        Task<List<MarsGrid>> GetGridsFull();
        Task<MarsGrid> GetGrid(int id);

    }
}