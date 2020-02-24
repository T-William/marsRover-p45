using System;
using System.Collections.Generic;
using System.Linq;
using MarsRover.API.Dtos;
using MarsRover.API.Models;
using AutoMapper;

namespace MarsRover.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {

            //Rover
            CreateMap<Rover,RoverDto>();
            CreateMap<RoverDto,Rover>();

            //RoverMovement
            CreateMap<RoverMovement,RoverMovementDto>();
            CreateMap<RoverMovementDto,RoverMovement>();

            //MarsGrid
            CreateMap<MarsGrid,MarsGridDto>();
            CreateMap<MarsGridDto,MarsGrid>();


                
        }
    }
}