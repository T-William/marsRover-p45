using System.Collections.Generic;
using System.Threading.Tasks;
using MarsRover.API.Dtos;

namespace MarsRover.API.Library.Interfaces
{
    public interface IRoverMovementService
    {
        Task<List<RoverMovementDto>> GetRoverMovements(int roverId);
        Task<RoverMovementDto> GetRoverMovement(int roverId);

        
        Task<IValidationDictionary> Create(RoverMovementDto dto);
        Task<IValidationDictionary> Update(int id, RoverMovementDto dto);
        IValidationDictionary Validate(RoverMovementDto dtoToValidate, bool isCreate, string ImportMessage ="");
        Task<IValidationDictionary> CalculateMovement(int GridId,RoverMovementDto dto);
        
         
    }
}