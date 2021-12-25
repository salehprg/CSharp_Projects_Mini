import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {ResponseModel} from '../../model/Response/responseModel';
import {QueryParamModel} from '../../model/Response/query-param.model';
import {GroupModel} from '../../model/group.model';
import {UserModel} from '../../model/user.model';
import {CreditModel} from '../../model/contact/credit.model';
import { baseUrl } from 'src/polyfills';

const API_BASE_USER = baseUrl;

@Injectable()
export class UserService {
  constructor(private http: HttpClient) {
  }

  getAllUsers(queryParams: QueryParamModel) {
    const qpm = JSON.stringify(queryParams);
    return this.http.get<ResponseModel>(API_BASE_USER + 'api/AdminContact?queryParam=' + qpm);
  }

  getUserById(userId: number) {
    return this.http.get<ResponseModel>(API_BASE_USER + 'api/User/' + userId);
  }

  editUser(userModel: UserModel) {
    return this.http.put<ResponseModel>(API_BASE_USER + 'api/User', userModel);
  }

  editUserRole(roles: number[], userId: number) {
    const userModel: UserModel = {
      accountInfo: {
        id: userId,
        picture: null,
        businessPhone: null,
        homePhone: null,
        mobilePhone: null,
        email: null,
        username: null,
        lastLogin: -1,
        createDate: -1,
        password: null,
        formId: null
      },
      addressInfo: null,
      additionalInfo: null,
      personalInfo: null,
      roles
    };
    return this.http.put<ResponseModel>(API_BASE_USER + 'api/UserRoles', userModel);
  }

  getUserRoles(userId: number) {
    return this.http.get<ResponseModel>(API_BASE_USER + 'api/UserRoles/' + userId);
  }

  getUserCredit(userId: number) {
    return this.http.get<ResponseModel>(API_BASE_USER + 'api/AdminUserCredit/' + userId);
  }

  editUserCredit(credit: CreditModel, userId: number) {
    return this.http.put<ResponseModel>(API_BASE_USER + 'api/AdminUserCredit/' + userId, credit);
  }

  getUserCompletionForm(guid: string) {
    return this.http.get<ResponseModel>(API_BASE_USER + 'api/UserForm?fi=' + guid);
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
