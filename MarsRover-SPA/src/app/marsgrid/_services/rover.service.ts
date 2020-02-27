import { MarsGrid } from './../../_models/marsGrid';
import { Rover } from './../../_models/rover';
import { environment } from './../../../environments/environment';
import { Injectable } from '@angular/core';
import { Subject, Observable, throwError } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { map, catchError } from 'rxjs/operators';
import { error } from 'protractor';

@Injectable({
   providedIn: 'root',
})
export class RoverService {
   baseUrl = environment.apiUrl;
   roverSelected = new Subject<Rover>();
   gridSelected = new Subject<MarsGrid>();
   constructor(private http: HttpClient) {}

   changeEditState(r: Rover) {
      this.roverSelected.next(r);
   }
   changeViewState(m: MarsGrid) {
      this.gridSelected.next(m);
   }
   getRover(id): Observable<Rover> {
      return this.http.get<Rover>(this.baseUrl + 'Rover/' + id);
   }

   getRovers(gridId:number): Observable<Rover[]> {
      return this.http.get<Rover[]>(this.baseUrl + 'Rover/full/'+gridId).pipe(
         map((data: any) => data.result),
         catchError(error => {
            return throwError('Its a trap');
         })
      );
   }
   updateRover(id: number, rover: Rover) {
      return this.http.put(this.baseUrl + 'rover/' + id, rover);
   }

   createRover(rover: Rover) {
      return this.http.post(this.baseUrl + 'Rover/', rover);
   }
   deleteRover(id: number) {
      return this.http.delete(this.baseUrl + 'Rover/' + id);
   }

   calculateRoverMovement(gridId: number, rovers: Rover[]) {
      return this.http.put(this.baseUrl + 'Rover/calculate/' + gridId, rovers);
    }
}
