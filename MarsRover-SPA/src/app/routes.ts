import { HomeComponent } from "./home/home.component";

import { Routes } from "@angular/router";

export const appRoutes: Routes = [
  { path: "", component: HomeComponent },
  {
    path: "marsGrid",
    loadChildren: () =>
      import("./marsgrid/marsgrid.module").then(mod => mod.MarsgridModule)
  },

  { path: "**", redirectTo: "", pathMatch: "full" }
];
