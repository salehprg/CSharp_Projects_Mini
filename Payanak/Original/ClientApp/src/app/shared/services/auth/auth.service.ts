import {Injectable} from '@angular/core';
import {FormGroup} from '@angular/forms';
import {Observable, of} from 'rxjs';
import {HttpClient} from '@angular/common/http';
import {UserModel} from '../../model/user.model';
import {QueryParamModel} from '../../model/Response/query-param.model';
import {RoleModel} from '../../model/role.model';
import {catchError} from 'rxjs/operators';
import { baseUrl } from 'src/polyfills';
import { ResponseModel } from '../../model/Response/responseModel';

const API_BASE_USER = baseUrl;

@Injectable()
export class AuthService {
  constructor(private http: HttpClient) {
  }

  login(username: string, password: string): Observable<ResponseModel> {
    return this.http.post<ResponseModel>(API_BASE_USER + 'api/auth', {username, password});
  }

  register(userModel: UserModel) {
    return this.http.post<ResponseModel>(API_BASE_USER + 'api/register', userModel);
  }

  getErrors(fg: FormGroup): string[] {
    const err: string[] = [];
    const username = fg.controls['username'];
    const password = fg.controls['password'];
    if (!username || !password) {
      return [];
    }

    const reqUsername = username.hasError('required') && (username.dirty || username.touched);
    const minUsername = username.hasError('minLength') && (username.dirty || username.touched);
    const maxUsername = username.hasError('maxLength') && (username.dirty || username.touched);
    const reqPassword = password.hasError('required') && (password.dirty || password.touched);
    const minPassword = password.hasError('minLength') && (password.dirty || password.touched);
    const maxPassword = password.hasError('maxLength') && (password.dirty || password.touched);
    if (reqPassword) {
      err.push('لطفا رمز عبور را وارد کنید.');
    }
    if (reqPassword) {
      err.push('لطفا نام کاربری را وارد کنید');
    }
    if (reqPassword) {
      err.push('حداقل طول رمز عبور 2 کاراکتر می باشد.');
    }
    if (reqPassword) {
      err.push('حداقل طول نام کاربری 2 کاراکتر می باشد.');
    }
    if (reqPassword) {
      err.push('حداکثر طول رمز عبور 100 کاراکتر می باشد.');
    }
    if (reqPassword) {
      err.push('حداکثر طول نام کاربری 100 کاراکتر می باشد.');
    }
    return err;
  }

  getUserByToken() {
    const userToken = localStorage.getItem('token');
    if (!userToken) {
      return of(null);
    }
    return this.http.get<ResponseModel>(API_BASE_USER + 'api/auth').pipe(catchError(err => {
      return of(null);
    }));
  }




  

  getAllRolesWithQuery(queryParams: QueryParamModel) {
    const qpm = JSON.stringify(queryParams);
    return this.http.get<ResponseModel>(API_BASE_USER + 'api/AdminRole?queryParam=' + qpm);
  }

  getAllRoles() {
    return this.http.get<ResponseModel>(API_BASE_USER + 'api/AdminRole');
  }

  deleteRole(RoleId: number) {
    return this.http.delete<ResponseModel>(API_BASE_USER + 'api/AdminRole/' + RoleId);
  }

  editRole(role: RoleModel) {
    return this.http.put<ResponseModel>(API_BASE_USER + 'api/AdminRole', role);
  }

  addRole(role: RoleModel) {
    return this.http.post<ResponseModel>(API_BASE_USER + 'api/AdminRole', role);
  }










  getAllPermissionsWithQuery(queryParams: QueryParamModel) {
    const qpm = JSON.stringify(queryParams);
    return this.http.get<ResponseModel>(API_BASE_USER + 'api/AdminPermission?queryParam=' + qpm);
  }

  getAllPermissions() {
    return this.http.get<ResponseModel>(API_BASE_USER + 'api/AdminPermission');
  }

  deletePermission(RoleId: number) {
    return this.http.delete<ResponseModel>(API_BASE_USER + 'api/AdminPermission/' + RoleId);
  }

  editPermission(role: RoleModel) {
    return this.http.put<ResponseModel>(API_BASE_USER + 'api/AdminPermission', role);
  }

  addPermission(role: RoleModel) {
    return this.http.post<ResponseModel>(API_BASE_USER + 'api/AdminPermission', role);
  }

}
