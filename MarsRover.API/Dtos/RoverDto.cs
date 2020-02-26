using System.Collections.Generic;

namespace MarsRover.API.Dtos
{
    public class RoverDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int GridId {get;set;}
        public int BeginX { get; set; }
        public int BeginY { get; set; }
        public string BeginOrientation { get; set; }
        public string MovementInput { get; set; }
        public int? EndX { get; set; }
        public int? EndY { get; set; }
        public string EndOrientation { get; set; }        
    }
}