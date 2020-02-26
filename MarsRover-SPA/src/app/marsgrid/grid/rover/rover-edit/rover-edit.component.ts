import { MarsGrid } from './../../../../_models/marsGrid';
import { finalize } from 'rxjs/operators';
import { AlertifyService } from './../../../../_services/alertify.service';
import { RoverService } from './../../../_services/rover.service';
import { Rover } from 'src/app/_models/rover';
import { Component, OnInit, EventEmitter, Output, OnDestroy, Pipe, Input } from '@angular/core';
import { Subscription } from 'rxjs';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';


@Component({
   selector: 'app-rover-edit',
   templateUrl: './rover-edit.component.html',
   styleUrls: ['./rover-edit.component.css'],
})
export class RoverEditComponent implements OnInit, OnDestroy {
   opened = false;
   isNewMode = true;
   RoverId: number;
   ModalTitle = 'Create New Rover';
   roverForm: FormGroup;
   rover: Rover;
   loading = false;
   selectedGridId: number;
   gridMaxX: number;
   gridMaxY: number;

   @Output()
   dialogClose: EventEmitter<Rover> = new EventEmitter<Rover>();
   private roverEditSubscription: Subscription;
   @Input()
   gridSelected: MarsGrid;

   constructor(private roverService: RoverService, private fb: FormBuilder, private alertify: AlertifyService) {}

   ngOnInit() {
      this.createForm();
      this.roverEditSubscription = this.roverService.roverSelected.subscribe((c: Rover) => {
         if (c) {
            this.gridMaxX = this.gridSelected.gridSizeX - 1;
            this.gridMaxY = this.gridSelected.gridSizeY - 1;
            this.createForm();
            this.ModalTitle = 'Edit Rover';
            this.isNewMode = false;
            this.RoverId = c.id;
            this.roverService.getRover(c.id).subscribe((rovData: Rover) => {
               this.rover = rovData;
               this.selectedGridId = this.rover.gridId;
               this.roverForm.setValue({
                  id: this.rover.id,
                  name: this.rover.name,
                  startX: this.rover.beginX,
                  startY: this.rover.beginY,
                  beginOrientation: this.rover.beginOrientation,
                  movementInput: this.rover.movementInput,
               });
               this.opened = true;
            });
         } else {
            this.gridMaxX = this.gridSelected.gridSizeX;
            this.gridMaxY = this.gridSelected.gridSizeY;
            this.opened = true;
            this.ModalTitle = 'New Rover';
            this.isNewMode = true;
            this.createForm();
            this.roverForm.setValue({
               id: 0,
               name: '',
               startX: 0,
               startY: 0,
               startDir: '',
               beginOrientation: '',
            });
            this.loading = false;
         }
      });
   }

   ngOnDestroy() {
      // tslint:disable-next-line:comment-format
      //Called once, before the instance is destroyed.
      // tslint:disable-next-line:comment-format
      //Add 'implements OnDestroy' to the class.
      this.roverEditSubscription.unsubscribe();
   }
   createForm() {
      this.roverForm = this.fb.group({
         id: [0],
         name: ['', Validators.compose([Validators.required, Validators.maxLength(150)])],
         startX: ['', Validators.required],
         startY: ['', Validators.required],
         beginOrientation: ['', Validators.required],
         movementInput: ['', Validators.required],
      });
   }
   close() {
      this.opened = false;
      this.dialogClose.emit(null);
      this.roverForm.reset();
   }
   save() {
      this.loading = true;
      this.rover = Object.assign({}, this.roverForm.value);
      if (this.isNewMode) {
         this.roverService
            .createRover(this.rover)
            .pipe(
               finalize(() => {
                  this.loading = false;
               })
            )
            .subscribe(
               () => {
                  this.alertify.success('Successfully Created');
                  this.close();
               },
               error => {
                  this.alertify.error(error);
               }
            );
      } else {
         this.rover.id = this.RoverId;
         this.roverService
            .updateRover(this.rover.id, this.rover)
            .pipe(
               finalize(() => {
                  this.loading = false;
               })
            )
            .subscribe(
               () => {
                  this.alertify.success('Successfully Updated');
                  this.close();
               },
               error => {
                  this.alertify.error(error);
               }
            );
      }
   }
}
