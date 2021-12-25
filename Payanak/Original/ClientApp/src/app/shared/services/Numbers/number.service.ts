import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {ResponseModel} from '../../model/Response/responseModel';
import {QueryParamModel} from '../../model/Response/query-param.model';
import {GroupModel} from '../../model/group.model';
import {NumberModel} from '../../model/number/number.model';
import { baseUrl } from 'src/polyfills';

const API_BASE_USER = baseUrl;

@Injectable()
export class NumberService {
  constructor(private http: HttpClient) {
  }

  getAllNumbers(queryParams: QueryParamModel) {
    const qpm = JSON.stringify(queryParams);
    return this.http.get<ResponseModel>(API_BASE_USER + 'api/AdminNumber?queryParam=' + qpm);
  }
  getUserNumbers(queryParams: QueryParamModel) {
    const qpm = JSON.stringify(queryParams);
    return this.http.get<ResponseModel>(API_BASE_USER + 'api/Numbers?queryParam=' + qpm);
  }

  getAllUsersForAssign() {
    return this.http.get<ResponseModel>(API_BASE_USER + 'api/AdminNumber/GetAllUsers');
  }


  getUserOwnedGroups(queryParams: QueryParamModel) {
    const qpm = JSON.stringify(queryParams);
    return this.http.get<ResponseModel>(API_BASE_USER + 'api/UserOwnedGroups?queryParam=' + qpm);
  }

  addNumber(number: NumberModel) {
    return this.http.post<ResponseModel>(API_BASE_USER + 'api/AdminNumber', number);
  }

  deleteNumber(numberId: number) {
    return this.http.delete<ResponseModel>(API_BASE_USER + 'api/AdminNumber/' + numberId);
  }
  toggleBlockNumber(number: NumberModel) {
    const tmpNumber = {...number};
    tmpNumber.isBlocked = !number.isBlocked;
    return this.http.put<ResponseModel>(API_BASE_USER + 'api/AdminNumber', tmpNumber);
  }

}
