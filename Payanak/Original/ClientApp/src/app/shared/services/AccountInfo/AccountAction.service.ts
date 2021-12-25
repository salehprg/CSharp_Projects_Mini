import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {catchError} from 'rxjs/operators';
import { baseUrl } from 'src/polyfills';

const API_BASE_USER = baseUrl;

@Injectable({
  providedIn: 'root'
})
export class AccountActionService {

  constructor(private http : HttpClient) { }

  ChangePassword(OldPassword: string, NewPassword: string) {
    return this.http.post<any>(API_BASE_USER + 'api/ChangePassword', { OldPassword, NewPassword });
  }

}
