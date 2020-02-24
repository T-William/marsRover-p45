using System.Collections.Generic;
using System.Threading.Tasks;
using MarsRover.API.Dtos;

namespace MarsRover.API.Library.Interfaces
{
    public interface IMarsGridService
    {
        Task<List<MarsGridDto>> GetMarsGridFull();
        Task<MarsGridDto> GetMarsGrid(int id);
        Task<IValidationDictionary> Create(MarsGridDto dto);
        Task<IValidationDictionary> Update(int id, MarsGridDto dto);
        Task<IValidationDictionary> Delete(int id);

        IValidationDictionary Validate(MarsGridDto dtoToValidate, bool IsCreate, string ImportMessage = "");

    }
}