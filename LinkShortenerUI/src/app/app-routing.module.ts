import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

// Imported Component
import { DashboardComponent } from './dashboard/dashboard.component';

const routes: Routes = [
  {
    path : '',
    redirectTo : 'home',
    pathMatch : 'full'
  },
  {
    path : 'home',
    component : DashboardComponent ,
  },

  {
    path : '**',
    redirectTo : ''
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
