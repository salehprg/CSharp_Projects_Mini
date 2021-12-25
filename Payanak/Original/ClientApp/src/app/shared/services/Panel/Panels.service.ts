import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {ResponseModel} from '../../model/Response/responseModel';
import {of} from 'rxjs';
import {QueryParamModel} from '../../model/Response/query-param.model';
import {PanelModel} from '../../model/sms/panel.model';
import {PanelVersionModel} from '../../model/sms/panel-version.model';
import { baseUrl } from 'src/polyfills';


const API_BASE_USER = baseUrl;

@Injectable()
export class PanelsService {
  constructor(private http: HttpClient) {
  }

  getUserPanels(queryParams: QueryParamModel) {
    const qpm = JSON.stringify(queryParams);
    return this.http.get<ResponseModel>(API_BASE_USER + 'api/UserPanel?queryParam=' + qpm);
  }

  getAllPanels(queryParams: QueryParamModel) {
    const qpm = JSON.stringify(queryParams);
    return this.http.get<ResponseModel>(API_BASE_USER + 'api/AdminPanel?queryParam=' + qpm);
  }

  getPanelVersions(queryParams: QueryParamModel) {
    const qpm = JSON.stringify(queryParams);
    return this.http.get<ResponseModel>(API_BASE_USER + 'api/AdminPanelVersion?queryParam=' + qpm);
  }

  deactivatePanel(templateId: number) {
    return this.http.delete<ResponseModel>(API_BASE_USER + 'api/UserPanel/' + templateId);
  }

  blockPanel(panel: PanelModel) {
    return this.http.put<ResponseModel>(API_BASE_USER + 'api/AdminPanel/', panel);
  }

  deletePanel(templateId: number) {
    return this.http.delete<ResponseModel>(API_BASE_USER + 'api/AdminPanel/' + templateId);
  }

  addPanel(panel: PanelModel) {
    return this.http.post<ResponseModel>(API_BASE_USER + 'api/AdminPanel', panel);
  }

  editPanel(panel: PanelModel) {
    return this.http.put<ResponseModel>(API_BASE_USER + 'api/UserPanel', panel);
  }

  getAllUsersForAssign() {
    return this.http.get<ResponseModel>(API_BASE_USER + 'api/AdminPanel/GetAllUsers');
  }

  getUserGroupsForAssign(userId: number) {
    return this.http.get<ResponseModel>(API_BASE_USER + 'api/AdminPanel/GetAllUserGroups/' + userId);
  }

  getUserNumbersForAssign(userId: number) {
    return this.http.get<ResponseModel>(API_BASE_USER + 'api/AdminPanel/GetAllUserNumbers/' + userId);
  }

  getUserTemplatesForAssign(userId: number) {
    return this.http.get<ResponseModel>(API_BASE_USER + 'api/AdminPanel/GetAllUserTemplates/' + userId);
  }

  deletePanelVersion(panelVersionId: number) {
    return this.http.delete<ResponseModel>(API_BASE_USER + 'api/AdminPanelVersion/' + panelVersionId);
  }

  addPanelVersion(model: PanelVersionModel, file: File) {
    if (!file) {
      return of(null);
    }

    // let fileToUpload = <File>files[0];
    const formData = new FormData();
    formData.append(file.name, file);

    // for (const itm in cheque) {
    formData.append('model', JSON.stringify(model));
    // }
    let headers: HttpHeaders = new HttpHeaders();
    headers = headers.append('Content-Type', 'multipart/form-data');
    return this.http.post<ResponseModel>(API_BASE_USER + 'api/AdminPanelVersion', formData);
  }
  
}
