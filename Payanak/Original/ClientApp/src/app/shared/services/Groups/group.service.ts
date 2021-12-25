import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {ResponseModel} from '../../model/Response/responseModel';
import {QueryParamModel} from '../../model/Response/query-param.model';
import {GroupModel} from '../../model/group.model';
import { baseUrl } from 'src/polyfills';

const API_BASE_USER = baseUrl;

@Injectable()
export class GroupService {
  constructor(private http: HttpClient) {
  }

  getUserGroups(queryParams: QueryParamModel) {
    const qpm = JSON.stringify(queryParams);
    return this.http.get<ResponseModel>(API_BASE_USER + 'api/UserGroups?queryParam=' + qpm);
  }

  getUserOwnedGroups(queryParams: QueryParamModel) {
    const qpm = JSON.stringify(queryParams);
    return this.http.get<ResponseModel>(API_BASE_USER + 'api/UserOwnedGroups?queryParam=' + qpm);
  }

  addGroup(group: GroupModel) {
    return this.http.post<ResponseModel>(API_BASE_USER + 'api/UserOwnedGroups', group);
  }

  editGroup(group: GroupModel) {
    return this.http.put<ResponseModel>(API_BASE_USER + 'api/UserOwnedGroups', group);
  }

  deleteGroup(groupId: number) {
    return this.http.delete<ResponseModel>(API_BASE_USER + 'api/UserOwnedGroups/' + groupId);
  }

  getGroupById(groupId: number) {
    return this.http.get<ResponseModel>(API_BASE_USER + 'api/UserOwnedGroups/' + groupId);
  }

  getGroupContacts(groupId: number, queryParams: QueryParamModel) {
    const qpm = JSON.stringify(queryParams);
    return this.http.get<ResponseModel>(API_BASE_USER + 'api/UserGroupContacts/' + groupId + '?queryParam=' + qpm);
  }

  deleteContactFromGroup(contactId: number, groupId: number) {
    return this.http.delete<ResponseModel>(API_BASE_USER + 'api/UserGroupContacts/' + contactId + '?groupId=' + groupId);
  }

  createGroupForUser(userId: number, group: GroupModel) {
    return this.http.post<ResponseModel>(API_BASE_USER + 'api/adminGroup/CreateGroupForUser/' + userId, group);
  }
}
