
import { Movements } from './../../_models/movements';
import { Rover } from 'src/app/_models/rover';

import { RoverService } from './../_services/rover.service';
import { AlertifyService } from './../../_services/alertify.service';
import { GridService } from './../_services/grid.service';
import { MarsGrid } from './../../_models/marsGrid';
import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { finalize } from 'rxjs/operators';
import { Validators, FormGroup, FormBuilder, FormArray } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

import { Subscription } from 'rxjs';

@Component({
   selector: 'app-grid',
   templateUrl: './grid.component.html',
   styleUrls: ['./grid.component.css'],
})
export class GridComponent implements OnInit {
   loading = false;
   colour: string;
   currentDir: string;
   currentX: number;
   currentY: number;
   gridForm: FormGroup;
   gridx = 0;
   beginx;
   beginy;
   endx;
   endy;
   gridy = 0;
   gridxArray: number[];
   gridyArray: number[];
   marsGridName: string;
   gridtotal = 0;
   gridToCreate: MarsGrid;
   gridSelected: MarsGrid;
   marsGridList: MarsGrid[] = [];
   roverList: Rover[];
   calcRover: Rover;
   roverMovements: Movements[] = [];
   lastMovement: Movements;
   rovSplitMoveList: string[];
   @Output()
   refreshRoverGrid: EventEmitter<Rover> = new EventEmitter<Rover>();
   private roverGridSubscription: Subscription;
   // tslint:disable-next-line: max-line-length
   constructor(
      private gridService: GridService,
      private roverService: RoverService,
      private alertify: AlertifyService,
      private fb: FormBuilder,
      private activatedRoute: ActivatedRoute
   ) {}

   ngOnInit() {
      this.activatedRoute.data.subscribe((data: { defaultGrid: MarsGrid }) => {
         this.gridSelected = data.defaultGrid;
      });
      this.loading = false;
      this.createForm();
      this.gridForm.setValue({
         gridName: '',
         gridSizeX: 0,
         gridSizeY: 0,
         gridTotalSize: 0,
         description: '',
         rovers: [],
      });
      this.gridService.getMarsGrids().subscribe(_marsGrids => {
         this.marsGridList = _marsGrids;
         // this.gridSelected = this.marsGridList[0];
         // this.gridtotal = this.gridSelected.gridTotalSize;
         // this.updateMarsGrid(this.gridSelected);
      });
   }

   gridChange(grid: MarsGrid) {
      this.loading = false;
      this.gridSelected = grid;
      this.updateMarsGrid(this.gridSelected);
      this.gridForm.reset();
      this.roverService.changeViewState(this.gridSelected);
   }
   save() {
      if (this.gridForm.valid) {
         this.loading = true;
         this.gridToCreate = Object.assign({}, this.gridForm.value);

         this.gridToCreate.gridTotalSize = this.gridx * this.gridy;

         this.gridService
            .createMarsGrid(this.gridToCreate)
            .pipe(
               finalize(() => {
                  this.loading = false;
               })
            )
            .subscribe(
               () => {
                  this.alertify.success('Successfully Created');
                  this.gridService.getMarsGrids().subscribe(_marsGrids => {
                     this.marsGridList = _marsGrids;
                  });
               },
               error => {
                  this.alertify.error(error);
               }
            );
      } else {
      }
   }

   createForm() {
      this.gridForm = this.fb.group({
         gridName: ['', Validators.compose([Validators.required, Validators.maxLength(150)])],
         gridSizeX: [0],
         gridSizeY: [0],
         gridTotalSize: [0],
         description: [''],
         rovers: this.fb.array([]),
      });
   }
   updateMarsGrid(g: MarsGrid) {
      this.loading = true;
      this.gridxArray = Array.from({ length: g.gridSizeX }, (_, k) => k);
      this.gridyArray = Array.from({ length: g.gridSizeY }, (_, d) => d).reverse();
      this.gridtotal = g.gridTotalSize;
      this.roverList = [];
      this.roverMovements = [];
   }
   getRandomColor() {
      var color = Math.floor(0x1000000 * Math.random()).toString(16);
      return '#' + ('000000' + color).slice(-6);
   }

   deployRovers() {
      this.roverList = [];
      this.roverMovements = [];
      this.roverService.getRovers(this.gridSelected.id).subscribe(r => {
         this.roverList = r;
         this.roverService
            .calculateRoverMovement(this.gridSelected.id, r)
            .pipe(finalize(() => {}))
            .subscribe(() => {
               this.roverService.getRovers(this.gridSelected.id).subscribe(_rovers => {
                  this.roverList = _rovers;
                  // Compile rover deployment list
                  this.roverList.forEach(rov => {
                     this.rovSplitMoveList = rov.movementInput.split('');
                     // Get Random Colour for This rover
                     this.colour = this.getRandomColor();
                     // rovermovements :[] = [{x:'',y:'',d:'',Colour:''}];
                     this.currentDir = rov.beginOrientation;
                     this.currentX = rov.beginX;
                     this.currentY = rov.beginY;
                     // Now Create movementsList - First Begin coords
                     // tslint:disable-next-line: max-line-length
                     this.roverMovements.push({
                        roverName: rov.name,
                        roverId: rov.id,
                        origin: 'Start Point',
                        x: rov.beginX,
                        y: rov.beginY,
                        d: rov.beginOrientation,
                        colour: this.colour,
                     });
                     // Add new move if movement input is M
                     this.rovSplitMoveList.forEach(move => {
                        this.oneStepForRover(move);
                        if (move == 'M') {
                           // tslint:disable-next-line: max-line-length
                           this.roverMovements.push({
                              roverName: rov.name,
                              roverId: rov.id,
                              origin: '',
                              x: this.currentX,
                              y: this.currentY,
                              d: this.currentDir,
                              colour: this.colour,
                           });
                        }
                     });
                     // Pop last Movement To assign checkpoint to it.
                     this.lastMovement = this.roverMovements.pop();
                     this.lastMovement.origin = 'End Point';
                     this.roverMovements.push(this.lastMovement);
                  });
               });
               
               this.alertify.success('Rover Deployed');
            });
      });
   }

   
   oneStepForRover(movement: string) {
      if (movement === 'L') {
         if (this.currentDir == 'N') {
            this.currentDir = 'W';
         } else if (this.currentDir == 'W') {
            this.currentDir = 'S';
         } else if (this.currentDir == 'S') {
            this.currentDir = 'E';
         } else if (this.currentDir == 'E') {
            this.currentDir = 'N';
         }
      } else if (movement === 'R') {
         if (this.currentDir == 'N') {
            this.currentDir = 'E';
         } else if (this.currentDir == 'W') {
            this.currentDir = 'N';
         } else if (this.currentDir == 'S') {
            this.currentDir = 'W';
         } else if (this.currentDir == 'E') {
            this.currentDir = 'S';
         }
      } else if (movement === 'M') {
         if (this.currentDir == 'N') {
            if (this.currentY + 1 <= this.gridSelected.gridSizeY) {
               this.currentY = this.currentY + 1;
            }
         } else if (this.currentDir == 'W') {
            if (this.currentX - 1 >= 0) {
               this.currentX = this.currentX - 1;
            }
         } else if (this.currentDir == 'S') {
            if (this.currentY - 1 >= 0) {
               this.currentY = this.currentY - 1;
            }
         } else if (this.currentDir == 'E') {
            if (this.currentX + 1 <= this.gridSelected.gridSizeY) {
               this.currentX = this.currentX + 1;
            }
         }
      }
   }
}
