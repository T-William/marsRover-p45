using System.Collections.Generic;

namespace MarsRover.API.Dtos
{
    public class RoverDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TotalMovements { get; set; }
        public int TotalDeployments { get; set; }
        public ICollection<RoverMovementDto> Movements { get; set; }
    }
}