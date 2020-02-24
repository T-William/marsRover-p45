using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MarsRover.API.Models
{
    public class Rover
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(150)]
        [Required]
        public string Name { get; set; }
        public int TotalMovements { get; set; }
        public int TotalDeployments { get; set; }
        public int? GridId { get; set; }
        public MarsGrid MarsGrid { get; set; }
        public ICollection<RoverMovement> Movements { get; set; }

    }
}