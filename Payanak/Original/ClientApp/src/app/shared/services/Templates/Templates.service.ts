import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {ResponseModel} from '../../model/Response/responseModel';
import {QueryParamModel} from '../../model/Response/query-param.model';
import {TemplateModel} from '../../model/sms/template.model';
import { baseUrl } from 'src/polyfills';


const API_BASE_USER = baseUrl;

@Injectable()
export class TemplatesService {
  constructor(private http: HttpClient) {
  }

  addTemplate(template: TemplateModel) {
    return this.http.post<ResponseModel>(API_BASE_USER + 'api/UserTemplate', template);
  }

  editTemplate(template: TemplateModel) {
    return this.http.put<ResponseModel>(API_BASE_USER + 'api/UserTemplate', template);
  }

  createTemplateForUser(userId: number, template: TemplateModel) {
    return this.http.post<ResponseModel>(API_BASE_USER + 'api/AdminPanel/CreateTemplateForUser/' + userId, template);
  }

  getUserTemplates(queryParams: QueryParamModel) {
    const qpm = JSON.stringify(queryParams);
    return this.http.get<ResponseModel>(API_BASE_USER + 'api/UserTemplate?queryParam=' + qpm);
  }

  deleteTemplate(templateId: number) {
    return this.http.delete<ResponseModel>(API_BASE_USER + 'api/UserTemplate/' + templateId);
  }
}
