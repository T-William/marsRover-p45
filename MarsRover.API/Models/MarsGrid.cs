using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MarsRover.API.Dtos;

namespace MarsRover.API.Models
{
    public class MarsGrid
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(150)]
        public string GridName { get; set; }
        public int NumberOfRovers { get; set; }
        public int GridSizeX { get; set; }
        public int GridSizeY { get; set; }
        public int GridTotalSize { get; set; }
        public ICollection<Rover> Rovers { get; set; }

        
    }
}