import {Injectable} from '@angular/core';
import {Subject} from 'rxjs';
import {HttpClient} from '@angular/common/http';
import { baseUrl } from 'src/polyfills';

const API_BASE_USER = baseUrl;

@Injectable()
export class SearchService {
  public filterChanged: Subject<string>;
  lastFilter: string;

  constructor(private http: HttpClient) {
    this.filterChanged = new Subject<string>();
  }

  getFilter() {
    return this.lastFilter;
  }

  setFilter(str: string) {
    this.lastFilter = str;
    console.log(str);
    this.filterChanged.next(str);
  }

}
