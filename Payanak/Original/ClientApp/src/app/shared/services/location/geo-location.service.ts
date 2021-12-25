import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';


const GEOLOCATION_ERRORS = {
  'errors.location.unsupportedBrowser': 'مرورگر شما سرویس مکان را پشتیبانی نمیکند.',
  'errors.location.permissionDenied': 'شما دسترسی به مکان را رد کردید.لطفا دسترسی را فعال نمایید.',
  'errors.location.positionUnavailable': 'امکان ردیابی مکان شما میسر نیست.',
  'errors.location.timeout': 'سرویس در حال حاضر پاسخگو نیست.'
};

@Injectable()
export class GeoLocationService {

  public getLocation(geoLocationOptions?: any): Observable<any> {
    geoLocationOptions = geoLocationOptions || { timeout: 5000 };

    return Observable.create(observer => {

      if (window.navigator && window.navigator.geolocation) {
        window.navigator.geolocation.getCurrentPosition(
          (position) => {
            observer.next(position);
            observer.complete();
          },
          (error) => {
            switch (error.code) {
              case 1:
                observer.error(GEOLOCATION_ERRORS['errors.location.permissionDenied']);
                break;
              case 2:
                observer.error(GEOLOCATION_ERRORS['errors.location.positionUnavailable']);
                break;
              case 3:
                observer.error(GEOLOCATION_ERRORS['errors.location.timeout']);
                break;
            }
          },
          geoLocationOptions);
      } else {
        observer.error(GEOLOCATION_ERRORS['errors.location.unsupportedBrowser']);
      }

    });



  }
}

export let geolocationServiceInjectables: Array<any> = [
  {provide: GeoLocationService, useClass: GeoLocationService }
];
