import {Injectable} from '@angular/core';
import {FormGroup} from '@angular/forms';
import {Observable, of} from 'rxjs';
import {HttpClient} from '@angular/common/http';
import {ResponseModel} from '../model/Response/responseModel';
import { baseUrl } from 'src/polyfills';

const API_BASE_USER = baseUrl;

@Injectable()
export class MenuService {
  constructor(private http: HttpClient) {
  }

  getMenu() {
    return this.http.get<any>(API_BASE_USER + 'api/router');
  }

}
