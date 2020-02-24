using System.Collections.Generic;
using MarsRover.API.Models;

namespace MarsRover.API.Dtos
{
    public class MarsGridDto
    {
        public int Id { get; set; }
        public string GridName { get; set; }
        public int NumberOfRovers { get; set; }
        
        public int GridSizeX { get; set; }
        public int GridSizeY { get; set; }
        public int GridTotalSize { get; set; }
        public ICollection<Rover> Rovers { get; set; }
    }
}