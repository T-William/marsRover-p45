import { Rover } from 'src/app/_models/rover';

import { RoverService } from './../_services/rover.service';
import { AlertifyService } from './../../_services/alertify.service';
import { GridService } from './../_services/grid.service';
import { MarsGrid } from './../../_models/marsGrid';
import { Component, OnInit } from '@angular/core';
import { finalize } from 'rxjs/operators';
import { Validators, FormGroup, FormBuilder, FormArray } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

@Component({
   selector: 'app-grid',
   templateUrl: './grid.component.html',
   styleUrls: ['./grid.component.css'],
})
export class GridComponent implements OnInit {
   loading = false;
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
   successMessage: string;
   gridSelected: MarsGrid;
   marsGridList: MarsGrid[] = [];
   roverList: Rover[];
   rovers: FormArray;
   calcRover: Rover;
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
   }

   deployRovers() {
      this.roverService.getRovers(this.gridSelected.id).subscribe(r => {
         this.roverList = r;
         this.roverList.forEach(x => {
            this.roverService
               .calculateRoverMovement(this.gridSelected.id, x)
               .pipe(finalize(() => {}))
               .subscribe(() => {
                  this.roverService.getRover(x.id).subscribe(_rover=>{
                     this.beginx =_rover.beginX;
                     this.endx = _rover.endX;
                     this.beginy=_rover.beginY;
                     this.endy=_rover.endY;
                  })
                  this.alertify.success('Rover Deployed');
                  
               });
         });
      });
   }
}
