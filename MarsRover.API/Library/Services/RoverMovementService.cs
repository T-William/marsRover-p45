using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using MarsRover.API.Data;
using MarsRover.API.Data.Interfaces;
using MarsRover.API.Dtos;
using MarsRover.API.Library.Interfaces;

namespace MarsRover.API.Library.Services
{
    public class RoverMovementService : IRoverMovementService
    {
        private readonly IMapper _mapper;
        private readonly IRoverMovementRepository _repo;
        private readonly IMarsGridRepository _repoGrid;
        private readonly IValidationDictionary _validation;


        public RoverMovementService(IMapper mapper,
        IValidationDictionary validation,
        IRoverMovementRepository repo)
        {
            _mapper = mapper;
            _validation = validation;
            _repo = repo;
        }

        public async Task<IValidationDictionary> CalculateMovement(int GridId,RoverMovementDto dto)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var currentGrid = _repoGrid.GetGrid(GridId).Result;
                List<PossibleMovements> movementsList = new List<PossibleMovements>();
                var movInput = dto.MovementInput;
                var movInputLength = dto.MovementInput.Length;
                var currentX = dto.BeginX;
                var currentY = dto.BeginY;
                var currentDir = dto.BeginOrientation;


                if (dto.BeginX! > currentGrid.GridSizeX && dto.BeginY! > currentGrid.GridSizeY)
                {
                    //List for Movement
                    for (int i = 0; i <= movInputLength; i++)
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
                                        if (currentY + 1 <= currentGrid.GridSizeY)
                                            currentY = currentY + 1;
                                        else if (currentDir == "W")
                                            if (currentX - 1 >= 0)
                                                currentX = currentX + 1;
                                            else if (currentDir == "S")
                                                if (currentY - 1 >= 0)
                                                    currentY = currentY + 1;
                                                else if (currentDir == "E")
                                                    if (currentX + 1 <= currentGrid.GridSizeX)
                                                        currentX = currentX + 1;
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
                    var movementFromRepo = await _repo.GetRecentMovement(dto.RoverId);

                    _mapper.Map(dto, movementFromRepo);
                    await _repo.SaveAll();
                }

                scope.Complete();
                return _validation;
            }
        }

        public async Task<IValidationDictionary> Create(RoverMovementDto dto)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                Validate(dto, true);
                if (_validation.IsValid)
                {
                    var movementToCreate = _mapper.Map<RoverMovementDto>(dto);

                    _repo.Add(movementToCreate);
                    if (!await _repo.SaveAll())
                    {
                        _validation.AddError("Movement could not be saved");
                    }
                }
                scope.Complete();
                return _validation;
            }
        }


        public async Task<RoverMovementDto> GetRoverMovement(int roverId)
        {
            var movement = await _repo.GetRoverMovement(roverId);
            return _mapper.Map<RoverMovementDto>(movement);

        }

        public async Task<List<RoverMovementDto>> GetRoverMovements(int roverId)
        {
            var movements = await _repo.GetSingleRoverMovementsFull(roverId);
            return _mapper.Map<List<RoverMovementDto>>(movements);
        }

        public async Task<IValidationDictionary> Update(int roverId, RoverMovementDto dto)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                Validate(dto, false);
                if (_validation.IsValid)
                {
                    var movementFromRepo = await _repo.GetRoverMovement(roverId);

                    _mapper.Map(dto, movementFromRepo);
                    await _repo.SaveAll();
                }

                scope.Complete();
                return _validation;

            }
        }

        public IValidationDictionary Validate(RoverMovementDto dtoToValidate, bool isCreate, string ImportMessage = "")
        {
            //Required            
            _validation.Required(dtoToValidate.BeginX.ToString(), $"{ImportMessage}Beginning X coordinate is required.");
            _validation.Required(dtoToValidate.BeginY.ToString(), $"{ImportMessage}Beginning Y coordinate is required.");
            _validation.Required(dtoToValidate.BeginOrientation.ToString(), $"{ImportMessage}Beginning Orientation is required.");
            _validation.Required(dtoToValidate.MovementInput, $"{ImportMessage}Movement Input is required.");

            //String Length

            //Custom           

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