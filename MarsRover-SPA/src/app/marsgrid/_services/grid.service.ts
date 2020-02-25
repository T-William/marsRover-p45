import { MarsgridComponent } from './../marsgrid.component';
import { MarsGrid } from './../../_models/marsGrid';
import { environment } from './../../../environments/environment';

import { Injectable } from '@angular/core';
import { Subject, Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class GridService {
  baseUrl = environment.apiUrl;
  gridSelected = new Subject<MarsGrid>();
  constructor(private http: HttpClient) {}

  changeEditState(g: MarsGrid) {
    this.gridSelected.next(g);
  }

  getMarsGrid(id): Observable<MarsGrid> {
    return this.http.get<MarsGrid>(this.baseUrl + 'MarsGrid/' + id);
  }

  getMarsGrids(): Observable<MarsGrid[]> {
    return this.http.get<MarsGrid[]>(this.baseUrl + 'MarsGrid/full');
  }

  createMarsGrid(marsGrid: MarsGrid) {
    return this.http.post(this.baseUrl + 'MarsGrid/', marsGrid);
  }

  deleteMarsGrid(id: number) {
    return this.http.delete(this.baseUrl + 'MarsGrid/' + id);
  }

  updateMarsGrid(id: number, marsGrid: MarsGrid) {
    return this.http.put(this.baseUrl + 'MarsGrid/' + id, marsGrid);
  }
}
