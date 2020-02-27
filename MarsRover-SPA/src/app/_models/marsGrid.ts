import { Rover } from "./rover";

export interface MarsGrid {
  id: number;
  gridName: string;  
  gridSizeX: number;
  gridSizeY: number;
  gridTotalSize: number;
  description:string;
  rovers: Rover[];
}
