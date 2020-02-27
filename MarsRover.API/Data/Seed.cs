using MarsRover.API.Data.Interfaces;
using MarsRover.API.Models;

namespace MarsRover.API.Data
{
    public class Seed
    {
        private readonly IMarsGridRepository _repoGrid;
        private readonly IRoverRepository _repoRover;
        public Seed(IMarsGridRepository repoGrid,
        IRoverRepository repoRover)
        {
            _repoGrid = repoGrid;
            _repoRover = repoRover;
        }
        public void SeedAll()
        {
            SeedGrid();
            SeedRovers();
        }

        public void SeedGrid()
        {
            var gridList = _repoGrid.GetGridsFull().Result;
            if (gridList.Count == 0)
            {
                var grid1 = new MarsGrid();                
                grid1.GridSizeX = 33;
                grid1.GridSizeY = 25;
                grid1.GridTotalSize= 825;
                grid1.GridName = "MaximumEffort";                
                _repoGrid.Add(grid1);
                _repoGrid.SaveAll().Wait();
            }
        }
        public void SeedRovers()
        {
            var roverList = _repoRover.GetRoversFull(0).Result;
            if (roverList.Count == 0)
            {
                var rover1 = new Rover();
                rover1.Name = "M";
                rover1.GridId = 2;
                rover1.BeginOrientation = "S";
                rover1.BeginX = 1;
                rover1.BeginY = 24;
                rover1.MovementInput = "MMMMMMLMMMLMMMRRMMMLMMMLMMMMMM";

                var rover2 = new Rover();
                rover2.Name = "A";
                rover2.GridId = 2;
                rover2.BeginOrientation = "S";
                rover2.BeginX = 10;
                rover2.BeginY = 24;
                rover2.MovementInput = "MMMMMM";

                var rover3 = new Rover();
                rover3.Name = "X";
                rover3.GridId = 2;
                rover3.BeginOrientation = "N";
                rover3.BeginX = 10;
                rover3.BeginY = 21;
                rover3.MovementInput = "MMMRMMMLLMMMMMM";
                var rover4 = new Rover();
                rover4.Name = "I";
                rover4.GridId = 2;
                rover4.BeginOrientation = "W";
                rover4.BeginX = 18;
                rover4.BeginY = 24;
                rover4.MovementInput = "MMLMMMLMMRRMMLMMMLMM";

                var rover5 = new Rover();
                rover5.Name = ".M";
                rover5.GridId = 2;
                rover5.BeginOrientation = "N";
                rover5.BeginX = 21;
                rover5.BeginY = 18;
                rover5.MovementInput = "MMMMMMRMMMRMMMMMM";

                var rover6 = new Rover();
                rover6.Name = "U";
                rover6.GridId = 2;
                rover6.BeginOrientation = "S";
                rover6.BeginX = 4;
                rover6.BeginY = 15;
                rover6.MovementInput = "MMMMMMLMMLMRMLMMMMLMRMLM";
                var rover7 = new Rover();
                rover7.Name = "M.";
                rover7.GridId = 2;
                rover7.BeginOrientation = "E";
                rover7.BeginX = 11;
                rover7.BeginY = 15;
                rover7.MovementInput = "MMRMLMRMMMMRMLMRMMRMLMRMMMMRM";
                var rover8 = new Rover();
                rover8.Name = "E";
                rover8.GridId = 2;
                rover8.BeginOrientation = "E";
                rover8.BeginX = 19;
                rover8.BeginY = 15;
                rover8.MovementInput = "MMMMLLMMLMMMMMMLMMLLMMMM";
                var rover9 = new Rover();
                rover9.Name = "F";
                rover9.GridId = 2;
                rover9.BeginOrientation = "W";
                rover9.BeginX = 5;
                rover9.BeginY = 7;
                rover9.MovementInput = "MMMLMMMLMMMRMMMRMMM";
                var rover10 = new Rover();
                rover10.Name = "F.";
                rover10.GridId = 2;
                rover10.BeginOrientation = "E";
                rover10.BeginX = 8;
                rover10.BeginY = 7;
                rover10.MovementInput = "MMMMLLMMLMMMMMM";
                var rover11 = new Rover();
                rover11.Name = "O";
                rover11.GridId = 2;
                rover11.BeginOrientation = "N";
                rover11.BeginX = 15;
                rover11.BeginY = 1;
                rover11.MovementInput = "MMMMMRMLMRMMRMLMRMMMMMLLMMMLMMM";
                var rover12 = new Rover();
                rover12.Name = "R";
                rover12.GridId = 2;
                rover12.BeginOrientation = "N";
                rover12.BeginX = 22;
                rover12.BeginY = 1;
                rover12.MovementInput = "MMMMMMRMMMRMMMRMMRRMRMMM";
                var rover13 = new Rover();
                rover13.Name = "T";
                rover13.GridId = 2;
                rover13.BeginOrientation = "W";
                rover13.BeginX = 32;
                rover13.BeginY = 7;
                rover13.MovementInput = "MMLMMMMMMLLMMMMMMLMM";


                _repoRover.Add(rover1);
                _repoRover.Add(rover2);
                _repoRover.Add(rover3);
                _repoRover.Add(rover4);
                _repoRover.Add(rover5);
                _repoRover.Add(rover6);
                _repoRover.Add(rover7);
                _repoRover.Add(rover8);
                _repoRover.Add(rover9);
                _repoRover.Add(rover10);
                _repoRover.Add(rover11);
                _repoRover.Add(rover12);
                _repoRover.Add(rover13);
                _repoRover.SaveAll().Wait();
            }
        }
    }
}