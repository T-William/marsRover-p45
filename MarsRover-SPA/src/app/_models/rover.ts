import { RoverMovements } from './roverMovements';

export interface Rover {
  id: number;
  name: string;
  totalMovements: string;
  totalDeployments: number;
  movements: RoverMovements[];
}
