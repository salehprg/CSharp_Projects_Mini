// Angular
import {Injectable} from '@angular/core';
import {HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpResponse} from '@angular/common/http';
// RxJS
import {Observable} from 'rxjs';
import {tap} from 'rxjs/operators';
import {debug} from 'util';
import {ActivatedRoute, Router} from '@angular/router';

@Injectable()
export class InterceptService implements HttpInterceptor {
  // intercept request and add token
  constructor(private activatedRoute: ActivatedRoute,
              private router: Router) {
  }

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    // tslint:disable-next-line:no-debugger
    // modify request
    request = request.clone({
      setHeaders: {
        Authorization: `Bearer ${localStorage.getItem('token')}`
      }
    });

    return next.handle(request).pipe(
      tap(
        event => {
          if (event instanceof HttpResponse) {

          }
        },
        error => {
          // http response status code
          // console.log('----response----');
          // console.error('status code:');
          // tslint:disable-next-line:no-debugger
          // if (error.status === 401 && this.router.url.indexOf('/login') === -1) {
          //   localStorage.clear();
          //   // this.router.navigate(['/login']);
          //   window.location.href = window.location.origin + '/login';
          //   return;
          // }
          // console.log('--- end of response---');
        }
      )
    );
  }
}
