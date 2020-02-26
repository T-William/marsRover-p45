using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MarsRover.API.Data.Interfaces;
using MarsRover.API.Dtos;
using MarsRover.API.Helpers;

namespace MarsRover.API.Library.Interfaces
{
    public interface IRoverService
    {

        Task<List<RoverDto>> GetRovers(int gridId);
        Task<IValidationDictionary> Create(RoverDto dto);

        Task<RoverDto> GetRover(int id);
        Task<IValidationDictionary> Update (int id,RoverDto dto);
        Task<IValidationDictionary> Delete (int id);
        Task<IValidationDictionary> CalculateMovement(int GridId,RoverDto dto);
        IValidationDictionary Validate(RoverDto dtoToValidate, bool isCreate, string ImportMessage);
        
         
    }
}