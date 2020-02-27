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
        private readonly IMarsGridRepository _repoGrid;

        public RoverService(IMapper mapper, IValidationDictionary validation, IRoverRepository repo, IMarsGridRepository repoGrip)
        {
            _repo = repo;
            _repoGrid = repoGrip;
            _mapper = mapper;
            _validation = validation;
        }

        public async Task<RoverDto> GetRover(int id)
        {
            var fund = await _repo.GetRover(id);
            return _mapper.Map<RoverDto>(fund);
        }
        public async Task<IValidationDictionary> CalculateMovement(int GridId, List<RoverDto> dtos)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var currentGrid = _repoGrid.GetGrid(GridId).Result;
                foreach (var dto in dtos)
                {
                    List<PossibleMovements> movementsList = new List<PossibleMovements>();
                    var movInput = dto.MovementInput;
                    var movInputLength = dto.MovementInput.Length;
                    var currentX = dto.BeginX;
                    var currentY = dto.BeginY;
                    var currentDir = dto.BeginOrientation;
                    var maxX = currentGrid.GridSizeX - 1;
                    var maxY = currentGrid.GridSizeY - 1;


                    if (dto.BeginX <= maxX && dto.BeginY <= maxY)
                    {
                        //List for Movement
                        for (int i = 0; i <= movInputLength - 1; i++)
                        {
                            if ((movInput.Substring(i, 1)) == "M")
                                movementsList.Add(PossibleMovements.M);
                            else if ((movInput.Substring(i, 1)) == "L")
                                movementsList.Add(PossibleMovements.L);
                            else if ((movInput.Substring(i, 1)) == "R")
                                movementsList.Add(PossibleMovements.R);
                        }

                        //Calculate Movements
                        foreach (var item in movementsList)
                        {
                            switch (item)
                            {
                                case PossibleMovements.L:
                                    {
                                        if (currentDir == "N")
                                            currentDir = "W";
                                        else if (currentDir == "W")
                                            currentDir = "S";
                                        else if (currentDir == "S")
                                            currentDir = "E";
                                        else if (currentDir == "E")
                                            currentDir = "N";
                                        break;
                                    }
                                case PossibleMovements.R:
                                    {
                                        if (currentDir == "N")
                                            currentDir = "E";
                                        else if (currentDir == "W")
                                            currentDir = "N";
                                        else if (currentDir == "S")
                                            currentDir = "W";
                                        else if (currentDir == "E")
                                            currentDir = "S";
                                        break;
                                    }
                                case PossibleMovements.M:
                                    {
                                        //Rover can't go off grid - for simplicity the rover will simply just stay in the current position if it is the end of the grid.
                                        if (currentDir == "N")
                                        {
                                            if (currentY + 1 <= maxY)
                                                currentY = currentY + 1;
                                        }
                                        else if (currentDir == "W")
                                        {
                                            if (currentX - 1 >= 0)
                                                currentX = currentX - 1;
                                        }
                                        else if (currentDir == "S")
                                        {
                                            if (currentY - 1 >= 0)
                                                currentY = currentY - 1;
                                        }
                                        else if (currentDir == "E")
                                        {
                                            if (currentX + 1 <= maxX)
                                                currentX = currentX + 1;
                                        }
                                        break;
                                    }
                                default:
                                    break;
                            }
                        }

                        //Assign new Pos to Rover
                        dto.EndOrientation = currentDir;
                        dto.EndX = currentX;
                        dto.EndY = currentY;


                    }
                    Validate(dto, false);
                    if (_validation.IsValid)
                    {
                        var roverFromRepo = await _repo.GetRover(dto.Id);
                        _mapper.Map(dto, roverFromRepo);
                    }


                }
                await _repo.SaveAll();
                scope.Complete();
                return _validation;
            }

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

        public async Task<List<RoverDto>> GetRovers(int gridId)
        {
            var roversToList = await _repo.GetRoversFull(gridId);


            return _mapper.Map<List<RoverDto>>(roversToList);



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
        public enum PossibleMovements
        {
            L,
            R,
            M
        }
    }
}