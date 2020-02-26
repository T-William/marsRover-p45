using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using MarsRover.API.Data.Interfaces;
using MarsRover.API.Dtos;
using MarsRover.API.Library.Interfaces;
using MarsRover.API.Models;

namespace MarsRover.API.Library.Services
{
    public class MarsGridService : IMarsGridService
    {
        private readonly IMapper _mapper;
        private readonly IValidationDictionary _validation;
        private readonly IMarsGridRepository _repo;

        public MarsGridService(IMapper mapper, IValidationDictionary validation, IMarsGridRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
            _validation = validation;
        }
        public async Task<IValidationDictionary> Create(MarsGridDto dto)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                Validate(dto, true);
                if (_validation.IsValid)
                {
                    var fundToCreate = _mapper.Map<MarsGrid>(dto);

                    _repo.Add(fundToCreate);
                    if (!await _repo.SaveAll())
                    {
                        _validation.AddError("Mars Grid could not be saved");
                    }
                }
                scope.Complete();
                return _validation;
            }
        }

        public async Task<IValidationDictionary> Delete(int id)
        {
            var assetType = await _repo.GetGrid(id);

            _repo.Delete(assetType);

            if (!await _repo.SaveAll())
            {
                _validation.AddError("Mars Grid could not be deleted.");
            }
            return _validation;
        }

        public async Task<MarsGridDto> GetMarsGrid(int id)
        {
            var marsGrid = await _repo.GetGrid(id);
            return _mapper.Map<MarsGridDto>(marsGrid);
        }
        public async Task<MarsGridDto> GetDefaultGrid()
        {
            var marsGrid = await _repo.GetDefaultGrid();
            return _mapper.Map<MarsGridDto>(marsGrid);
        }

        public async Task<List<MarsGridDto>> GetMarsGridFull()
        {
            var marsGrids = await _repo.GetGridsFull();
            return _mapper.Map<List<MarsGridDto>>(marsGrids);
        }

        public async Task<IValidationDictionary> Update(int id, MarsGridDto dto)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                Validate(dto, false);
                if (_validation.IsValid)
                {
                    var gridFromRepo = await _repo.GetGrid(id);

                    _mapper.Map(dto, gridFromRepo);
                    await _repo.SaveAll();
                }

                scope.Complete();
                return _validation;

            }
        }

        public IValidationDictionary Validate(MarsGridDto dtoToValidate, bool IsCreate, string ImportMessage = "")
        {
            //Required            
            _validation.Required(dtoToValidate.GridName, $"{ImportMessage}Grid Name is required.");


            //String Length

            _validation.MaxLength(dtoToValidate.GridName, 150, $"{ImportMessage}Grid Name: Max length of 150");






            return _validation;
        }
    }
}