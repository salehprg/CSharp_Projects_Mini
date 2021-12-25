import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot} from '@angular/router';
import {Injectable} from '@angular/core';
import {AuthService} from './auth/auth.service';

@Injectable()
export class AuthGuard implements CanActivate {

  constructor(private authService: AuthService) {
  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    const token = localStorage.getItem('token');
    if (!token) {
      return false;
    }
    this.authService.getUserByToken().subscribe(
      res => {
        return true;
      }
    );
  }
}

@Injectable()
export class AuthGuardNeg implements CanActivate {

  constructor(private authService: AuthService,
              private  router: Router) {
  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    const token = localStorage.getItem('token');
    if (!token) {
      return true;
    }
    this.authService.getUserByToken().subscribe(
      res => {
        this.router.navigate(['/dashboard/dashboard1']);
        return false;
      }
    );
  }
}
