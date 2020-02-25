import { FormsModule } from '@angular/forms';
import { AppModule } from './../app.module';
import { RoverEditComponent } from './grid/rover/rover-edit/rover-edit.component';
import { RoverComponent } from './grid/rover/rover.component';
import { GridComponent } from './Grid/Grid.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MarsgridComponent } from './marsgrid.component';
import { MarsGrid } from '../_models/marsGrid';



@NgModule({
  imports: [
    CommonModule,
    AppModule,
    FormsModule   ],
  declarations: [MarsgridComponent,
  GridComponent,
RoverComponent,
RoverEditComponent],
exports:[
  MarsgridComponent,
  GridComponent,
  RoverComponent,
  RoverEditComponent
]
})
export class MarsgridModule { 
}

