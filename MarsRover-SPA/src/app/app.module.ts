import { RoverEditComponent } from './marsgrid/grid/rover/rover-edit/rover-edit.component';
import { RoverComponent } from './marsgrid/grid/rover/rover.component';
import { GridComponent } from './marsgrid/grid/grid.component';

import { NgModule } from '@angular/core';

import { ButtonsModule } from '@progress/kendo-angular-buttons';
import { InputsModule } from '@progress/kendo-angular-inputs';
import { RippleModule } from '@progress/kendo-angular-ripple';
import { LayoutModule } from '@progress/kendo-angular-layout';
import { DialogsModule } from '@progress/kendo-angular-dialog';
import { UploadModule } from '@progress/kendo-angular-upload';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DropDownsModule } from '@progress/kendo-angular-dropdowns';
import { GridModule } from '@progress/kendo-angular-grid';

import { TooltipModule } from '@progress/kendo-angular-tooltip';
import { DateInputsModule } from '@progress/kendo-angular-dateinputs';
import { PopupModule } from '@progress/kendo-angular-popup';
import { NotificationModule } from '@progress/kendo-angular-notification';
import { RouterTestingModule } from '@angular/router/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule, HttpClient } from '@angular/common/http';

import { AppComponent } from './app.component';

import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';

@NgModule({
   imports: [
      FormsModule,
      ReactiveFormsModule,
      BrowserModule,
      CommonModule,
      ButtonsModule,
      InputsModule,
      RippleModule,
      LayoutModule,
      DialogsModule,
      UploadModule,
      DropDownsModule,
      GridModule,
      PopupModule,
      TooltipModule,
      DateInputsModule,
      NotificationModule,
      RouterTestingModule,
      BrowserAnimationsModule,
      HttpClientModule,
   ],
   declarations: [AppComponent, GridComponent, RoverComponent, RoverEditComponent],
   providers: [],
   exports: [
      NotificationModule,
      ButtonsModule,
      InputsModule,
      RippleModule,
      LayoutModule,
      DialogsModule,
      UploadModule,
      DropDownsModule,
      GridModule,
      TooltipModule,
      DateInputsModule,
      HttpClientModule,
   ],
   bootstrap: [AppComponent],
})
export class AppModule {}
