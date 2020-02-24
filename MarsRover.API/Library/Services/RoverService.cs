using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using MarsRover.API.Data.Interfaces;
using MarsRover.API.Dtos;
using MarsRover.API.Helpers;
using MarsRover.API.Library.Interfaces;
using MarsRover.API.Models;

namespace MarsRover.API.Library.Services
{
    public class RoverService : IRoverService
    {
        private readonly IMapper _mapper;
        private readonly IValidationDictionary _validation;
        private readonly IRoverRepository _repo;

        public RoverService(IMapper mapper, IValidationDictionary validation, IRoverRepository repo)
        {
            _repo = repo;
            _mapper = mapper;
            _validation = validation;
        }

        public async Task<RoverDto> GetRover(int id)
        {
            var fund = await _repo.GetRover(id);
            return _mapper.Map<RoverDto>(fund);
        }

        public async Task<IValidationDictionary> Create(RoverDto dto)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                Validate(dto, true);
                if (_validation.IsValid)
                {
                    var roverToCreate = _mapper.Map<Rover>(dto);

                    _repo.Add(roverToCreate);
                    if (!await _repo.SaveAll())
                    {
                        _validation.AddError("Rover could not be saved");
                    }
                }
                scope.Complete();
                return _validation;
            }

        }

        public async Task<IValidationDictionary> Delete(int id)
        {
            var rover = await _repo.GetRover(id);

            _repo.Delete(rover);
            if (!await _repo.SaveAll())
            {
                _validation.AddError("Rover could not be deleted.");
                return _validation;
            }
            return _validation;
        }

        public async Task<IEnumerable<RoverDto>> GetPagedList(RoverParams roverParams)
        {
            var roversToList = await _repo.GetPagedRoverList(roverParams);

            var roversToReturn = _mapper.Map<IEnumerable<RoverDto>>(roversToList);

            return roversToReturn;

        }

        public async Task<IValidationDictionary> Update(int id, RoverDto dto)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                Validate(dto, false);
                if (_validation.IsValid)
                {
                    var roverFromRepo = await _repo.GetRover(id);

                    _mapper.Map(dto, roverFromRepo);
                    await _repo.SaveAll();
                }

                scope.Complete();
                return _validation;

            }

        }
        public void ValidateRoverNameExists(string RoverName, string ImportMessage = "")
        {
            if (RoverName != "")
            {
                if (_repo.RoverNameExists(RoverName).Result)
                {
                    _validation.AddError($"{ImportMessage}The Rover Name {RoverName} already exists.");
                }
            }
        }

        public IValidationDictionary Validate(RoverDto dtoToValidate, bool isCreate, string ImportMessage = "")
        {
            //Required            
            _validation.Required(dtoToValidate.Name, $"{ImportMessage}Name is required.");


            //String Length

            _validation.MaxLength(dtoToValidate.Name, 150, $"{ImportMessage}Description: Max length of 150");



            //Custom
            if (isCreate)
                ValidateRoverNameExists(dtoToValidate.Name, ImportMessage);

            return _validation;
        }
    }
}