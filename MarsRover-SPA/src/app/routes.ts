import { GridComponent } from './marsgrid/grid/grid.component';
import { MarsGridResolver } from './_resolvers/marsGridResolver';

import { Routes } from '@angular/router';

export const appRoutes: Routes = [
   { path: 'grid', resolve: { defaultGrid: MarsGridResolver }, component: GridComponent },
   ,
   { path: '**',resolve: { defaultGrid: MarsGridResolver }, redirectTo: '' },
];
