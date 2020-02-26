import { GridService } from './../marsgrid/_services/grid.service';
import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { catchError } from 'rxjs/operators';
import { empty } from 'rxjs/internal/observable/empty';

@Injectable({
   providedIn: 'root',
})
export class MarsGridResolver implements Resolve<any> {
   constructor(private gridService: GridService) {}
   resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
      return this.gridService.getDefaultGrid().pipe(
         catchError(error => {
            return empty;
         })
      );
   }
}
