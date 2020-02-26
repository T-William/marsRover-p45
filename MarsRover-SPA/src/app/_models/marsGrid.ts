import { Rover } from "./rover";

export interface MarsGrid {
  id: number;
  gridName: string;
  numberOfRovers: number;
  gridSizeX: number;
  gridSizeY: number;
  gridTotalSize: number;
  description:string;
  rovers: Rover[];
}
