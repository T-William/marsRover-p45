import { finalize } from 'rxjs/operators';
import { MarsGrid } from './../../../_models/marsGrid';

import { AlertifyService } from './../../../_services/alertify.service';
import { RoverService } from './../../_services/rover.service';
import { Component, OnInit, Input } from '@angular/core';
import { Rover } from 'src/app/_models/rover';
import { isUndefined } from 'util';
import { Subscription } from 'rxjs';
import { error } from 'protractor';

@Component({
   selector: 'app-rover',
   templateUrl: './rover.component.html',
   styleUrls: ['./rover.component.css'],
})
export class RoverComponent implements OnInit {
   showRovers = false;
   roverToDelete: Rover;
   roverList: Rover[] = [];
   errorMessage: string;
   successMessage: string;
   theGrid: MarsGrid;
   loading = false;
   opened = false;

   @Input()
   selectedGrid: MarsGrid;

   private roverViewSubscription: Subscription;
   

   constructor(private roverService: RoverService, private alertify: AlertifyService) {}

   ngOnInit() {
      this.roverViewSubscription = this.roverService.gridSelected.subscribe((m: MarsGrid) => {
         if (m) {
            this.loading = true;
            this.selectedGrid = m;
            this.refreshRoverGrid();
            this.theGrid = this.selectedGrid;
         } else {
            this.loading = false;
         }
      });



   }

   openRoverEdit(rover: Rover) {
      this.roverService.changeEditState(rover);
   }
   closeRoverEdit(c: Rover) {
      this.roverService.getRovers(this.selectedGrid.id).subscribe(_rovers => {
         this.roverList = _rovers;
      });
   }
   openRoverDelete(rover: Rover) {
      this.roverToDelete = rover;
      this.opened = true;
   }
   deleteRover() {
      this.roverService
         .deleteRover(this.roverToDelete.id)
         .pipe(
            finalize(() => {
               this.loading = false;
            })
         )
         .subscribe(
            () => {
               this.close();
               this.alertify.success('Successfully Deleted Rover');
            },
            error => {
               this.errorMessage = error;
               this.alertify.error(this.errorMessage);
               this.refreshRoverGrid();
            }
         );
   }

   refreshRoverGrid() {
      this.roverService.getRovers(this.selectedGrid.id).subscribe(_rovers => {
         this.roverList = _rovers;
         this.loading = false;
      });
   }
   close() {
      this.opened = false;
   }
}
