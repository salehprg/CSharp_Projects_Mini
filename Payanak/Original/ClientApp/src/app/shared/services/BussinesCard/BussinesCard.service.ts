import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {ResponseModel} from '../../model/Response/responseModel';
import {QueryParamModel} from '../../model/Response/query-param.model';
import {BusinessCardModel} from '../../model/sms/business-card.model';
import { baseUrl } from 'src/polyfills';

const API_BASE_USER = baseUrl;

@Injectable()
export class BussinesCardService {
  constructor(private http: HttpClient) {
  }

  getAllBusinessCards(queryParams: QueryParamModel) {
    const qpm = JSON.stringify(queryParams);
    return this.http.get<ResponseModel>(API_BASE_USER + 'api/AdminBusinessCard?queryParam=' + qpm);
  }

  getUserBusinessCards(queryParams: QueryParamModel) {
    const qpm = JSON.stringify(queryParams);
    return this.http.get<ResponseModel>(API_BASE_USER + 'api/UserBusinessCard?queryParam=' + qpm);
  }

  addBusinessCard(card: BusinessCardModel) {
    return this.http.post<ResponseModel>(API_BASE_USER + 'api/AdminBusinessCard', card);
  }

  deleteBusinessCard(businessCardId: number) {
    return this.http.delete<ResponseModel>(API_BASE_USER + 'api/AdminBusinessCard/' + businessCardId);
  }

  editBusinessCard(card: BusinessCardModel) {
    return this.http.put<ResponseModel>(API_BASE_USER + 'api/UserBusinessCard', card);
  }

  blockBusinessCard(card: BusinessCardModel) {
    return this.http.put<ResponseModel>(API_BASE_USER + 'api/AdminBusinessCard/', card);
  }

  deactivateBusinessCard(businessCardId: number) {
    return this.http.delete<ResponseModel>(API_BASE_USER + 'api/UserBusinessCard/' + businessCardId);
  }
}
