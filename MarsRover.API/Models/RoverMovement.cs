using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarsRover.API.Models
{
    public class RoverMovement
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Rover")]
        public int RoverId { get; set; }        
        public Rover Rover { get; set; }
        public int BeginX { get; set; }
        public int BeginY { get; set; }
        public string BeginOrientation { get; set; }
        public string MovementInput { get; set; }
        public int? EndX { get; set; }
        public int? EndY { get; set; }
        public string EndOrientation { get; set; }
        [Required]
        public DateTime MovementDate { get; set; }
    }
}