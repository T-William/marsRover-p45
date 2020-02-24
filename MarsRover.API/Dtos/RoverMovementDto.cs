using System;

namespace MarsRover.API.Dtos
{
    public class RoverMovementDto
    {

        public int Id { get; set; }        
        public int RoverId { get; set; }        
        public int BeginX { get; set; }
        public int BeginY { get; set; }
        public string BeginOrientation { get; set; }
        public string MovementInput { get; set; }
        public int? EndX { get; set; }
        public int? EndY { get; set; }
        public string EndOrientation { get; set; }        
        public DateTime MovementDate { get; set; }

    }
}