import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {FullLayoutComponent} from './layouts/full/full-layout.component';
import {ContentLayoutComponent} from './layouts/content/content-layout.component';
import {CONTENT_ROUTES} from './shared/routes/content-layout.routes';
import {Full_ROUTES} from './shared/routes/full-layout.routes';
import {AuthGuard, AuthGuardNeg} from './shared/services/auth-gurd.service';


const routes: Routes = [
  {
    path: '',
    redirectTo: 'pages/login',
    pathMatch: 'full',
    canActivate: [AuthGuardNeg]
  },
  {
    path: '',
    component: FullLayoutComponent,
    data: {title: 'full Views'},
    children: Full_ROUTES,
    // canActivate: [AuthGuard]
  },
  {
    path: '',
    component: ContentLayoutComponent,
    data: {title: 'content Views'},
    children: CONTENT_ROUTES,
    // canActivate: [AuthGuard]
  },
  {
    path: '**',
    redirectTo: 'pages/error'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
