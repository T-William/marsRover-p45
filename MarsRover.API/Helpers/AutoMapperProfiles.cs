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

             //MarsGrid
            CreateMap<MarsGrid,MarsGridDto>()
            .ForMember(dest => dest.Description, opt =>
                {
                    opt.MapFrom(src => src.GridName + " Size- "+ "(" + src.GridTotalSize + ")");
                });
            CreateMap<MarsGridDto,MarsGrid>();


                
        }
    }
}