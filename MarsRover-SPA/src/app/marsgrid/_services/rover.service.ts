import { Rover } from './../../_models/rover';
import { environment } from './../../../environments/environment';
import { Injectable } from '@angular/core';
import { Subject, Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class RoverService {
  baseUrl = environment.apiUrl;
  roverSelected = new Subject<Rover>();
  constructor(private http: HttpClient) {}
  changeEditState(r: Rover) {
    this.roverSelected.next(r);
  }
  getRover(id): Observable<Rover> {
    return this.http.get<Rover>(this.baseUrl + 'Rover/' + id);
  }

  getRovers(): Observable<Rover[]> {
    return this.http.get<Rover[]>(this.baseUrl + 'Rover/full');
  }

  createRover(rover: Rover) {
    return this.http.post(this.baseUrl + 'Rover/', rover);
  }
  deleteRover(id: number) {
    return this.http.delete(this.baseUrl + 'Rover/' + id);
  }
}
