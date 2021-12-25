import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {ResponseModel} from '../model/Response/responseModel';
import {QueryParamModel} from '../model/Response/query-param.model';
import {GroupModel} from '../model/group.model';
import {filter} from 'lodash';
import {Subject} from 'rxjs';
import { baseUrl } from 'src/polyfills';


const API_BASE_USER = baseUrl;

@Injectable()
export class TaskService {
  taskIds: string[] = [];
  Tasks: Subject<string[]>;
  TaskAdded: Subject<string>;
  public TaskReset: Subject<boolean>;

  constructor(private http: HttpClient) {
    this.Tasks = new Subject<string[]>();
    this.TaskAdded = new Subject<string>();
    this.TaskReset = new Subject<boolean>();
  }

  addNewTaskId(guid: string) {
    this.taskIds.push(guid);
    this.Tasks.next(this.taskIds);
    this.TaskAdded.next(guid);
  }

  getTaskResult(guid: string) {
    return this.http.get<ResponseModel>(API_BASE_USER + 'api/Task?guid=' + guid);
  }

  RemoveTaskId(guid: string) {
    this.taskIds = filter(this.taskIds, function(ids) {
      return ids !== guid;
    });
    this.Tasks.next(this.taskIds);
  }

  getAllTaskIds() {
    return this.taskIds;
  }

  getDashboardInfo() {
    return this.http.get<ResponseModel>(API_BASE_USER + 'api/dashboard');
  }

  getUserOwnedGroups(queryParams: QueryParamModel) {
    const qpm = JSON.stringify(queryParams);
    return this.http.get<ResponseModel>(API_BASE_USER + 'api/UserOwnedGroups?queryParam=' + qpm);
  }

  addGroup(group: GroupModel) {
    return this.http.post<ResponseModel>(API_BASE_USER + 'api/UserOwnedGroups', group);
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
}
