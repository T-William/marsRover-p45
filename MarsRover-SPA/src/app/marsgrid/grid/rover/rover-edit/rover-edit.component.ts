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
               this.selectedGridId = this.gridSelected.id;
               this.roverForm.setValue({
                  id: this.rover.id,
                  name: this.rover.name,
                  beginX: this.rover.beginX,
                  beginY: this.rover.beginY,
                  beginOrientation: this.rover.beginOrientation,
                  movementInput: this.rover.movementInput,
               });
               this.opened = true;
            });
         } else {
            this.gridMaxX = this.gridSelected.gridSizeX;
            this.gridMaxY = this.gridSelected.gridSizeY;
            this.selectedGridId = this.gridSelected.id;
            this.opened = true;
            this.ModalTitle = 'New Rover';
            this.isNewMode = true;
            this.createForm();
            this.roverForm.setValue({
               id: 0,
               name: '',
               beginX: 0,
               beginY: 0,
               beginOrientation: '',
               movementInput: '',
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
      this.roverForm = this.fb.group(
         {
            id: [0],
            name: ['', Validators.compose([Validators.required, Validators.maxLength(150)])],
            beginX: ['', Validators.required],
            beginY: ['', Validators.required],
            beginOrientation: ['', Validators.required],
            movementInput: ['', Validators.compose([Validators.required])],
         },
         { validator: [this.beginOrientationRule, this.movementInputRule] }
      );
   }
   beginOrientationRule(g: FormGroup) {
      var x = g.get('beginOrientation').value.toUpperCase();
      var check = ['N','E','S','W'];
      if (!check.includes(x)) {
         return { beginOrientationRule: true };
      }
      return null;
   }
   movementInputRule(g: FormGroup) {
      var movementInputString = g.get('movementInput').value.toUpperCase();
      var movementList = movementInputString.split('');
      var check = ['L','M','R'];
      var errorCount =0;  

      movementList.forEach(el => {
         if(!check.includes(el)){
            errorCount = 1;
         }         
      });
      if(errorCount != 0){
            return { movementInputRule: true };
      }
      return null;
   }
   close() {
      this.opened = false;
      this.dialogClose.emit(null);
      this.roverForm.reset();
   }
   save() {
      this.loading = true;
      this.rover = Object.assign({}, this.roverForm.value);
      this.rover.movementInput = this.rover.movementInput.toUpperCase();
      this.rover.gridId = this.selectedGridId;
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
