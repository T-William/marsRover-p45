import { MarsGrid } from './../../../_models/marsGrid';

import { AlertifyService } from './../../../_services/alertify.service';
import { RoverService } from './../../_services/rover.service';
import { Component, OnInit, Input } from '@angular/core';
import { Rover } from 'src/app/_models/rover';
import { isUndefined } from 'util';
import { Subscription } from 'rxjs';

@Component({
   selector: 'app-rover',
   templateUrl: './rover.component.html',
   styleUrls: ['./rover.component.css'],
})
export class RoverComponent implements OnInit {
   showRovers = false;
   roverList: Rover[] = [];
   theGrid: MarsGrid;
   loaded = false;

   @Input()
   selectedGrid: MarsGrid;

   private roverViewSubscription: Subscription;

   constructor(private roverService: RoverService, private alertify: AlertifyService) {}

   ngOnInit() {
      this.updateRovers();
      this.theGrid = this.selectedGrid;
   }
   updateRovers() {
      this.roverViewSubscription = this.roverService.gridSelected.subscribe((m: MarsGrid) => {
         if (m) {
            this.loaded = true;
            this.selectedGrid = m;
            this.roverService.getRovers(this.selectedGrid.id).subscribe(_rovers => {
               this.roverList = _rovers;
            });
         } else {
            this.loaded = false;
         }
      });
   }

   openRoverEdit(rover: Rover) {
      this.roverService.changeEditState(rover);
   }
   closeRoverEdit(c: Rover) {
      this.updateRovers();
   }
}
