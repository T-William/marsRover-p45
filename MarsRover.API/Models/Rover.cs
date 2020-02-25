using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarsRover.API.Models
{
    public class Rover
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(150)]
        [Required]
        public string Name { get; set; }        
        [Required]
        [ForeignKey("MarsGrid")]
        public int? GridId { get; set; }

        public MarsGrid MarsGrid { get; set; }
        public ICollection<RoverMovement> Movements { get; set; }

    }
}