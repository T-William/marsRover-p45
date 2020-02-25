import { environment } from './../../../environments/environment';
import { Injectable } from '@angular/core';
import { Subject, Observable } from 'rxjs';
import { RoverMovements } from 'src/app/_models/roverMovements';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class RoverMovementService {
  baseUrl = environment.apiUrl;
  roverMovSelected = new Subject<RoverMovements>();
  constructor(private http: HttpClient) {}

  changeEditState(rm: RoverMovements) {
    this.roverMovSelected.next(rm);
  }

  getRoverMovement(roverId): Observable<RoverMovements> {
    return this.http.get<RoverMovements>(
      this.baseUrl + 'RoverMovement/' + roverId
    );
  }
  createRoverMovement(roverMov: RoverMovements) {
    this.http.post(this.baseUrl + 'rovermovement/', roverMov);
  }
  calculateRoverMovement(gridId: number, roverMov: RoverMovements) {
    this.http.put(this.baseUrl + 'rovermovement/' + gridId, roverMov);
  }
}
