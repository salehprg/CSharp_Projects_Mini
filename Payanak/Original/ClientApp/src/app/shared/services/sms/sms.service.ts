import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {QueryParamModel} from '../../model/Response/query-param.model';
import {NumberModel} from '../../model/number/number.model';
import {ComposeSMSModel} from '../../model/sms/compose-sms.model';
import {ResponseModel} from '../../model/Response/responseModel';
import {ScheduleSmsInfoModel} from '../../model/sms/schedule-sms-info.model';
import {ScheduleDetailModel} from '../../model/sms/schedule-detail.model';
import { baseUrl } from 'src/polyfills';

const API_BASE_USER = baseUrl;

@Injectable()
export class SmsService {
  constructor(private http: HttpClient) {
  }

  getAllNumbers(queryParams: QueryParamModel) {
    const qpm = JSON.stringify(queryParams);
    return this.http.get<ResponseModel>(API_BASE_USER + 'api/Numbers?queryParam=' + qpm);
  }

  ComposeSms(data: ComposeSMSModel) {
    return this.http.post<ResponseModel>(API_BASE_USER + 'api/sendSms', data);
  }


  


  getSSDForUser(userId, ssiId) {
    return this.http.get<ResponseModel>(API_BASE_USER + 'api/UserScheduleDetail?userId=' + userId + '&ssiId=' + ssiId);
  }

  addSSD(schedule: ScheduleDetailModel) {
    return this.http.post<ResponseModel>(API_BASE_USER + 'api/UserScheduleDetail', schedule);
  }

  deleteSSD(ssdId: number) {
    return this.http.delete<ResponseModel>(API_BASE_USER + 'api/UserScheduleDetail/' + ssdId);
  }

  addSSI(schedule: ScheduleSmsInfoModel) {
    return this.http.post<ResponseModel>(API_BASE_USER + 'api/UserScheduledSms', schedule);
  }

  getUserSSI(queryParams: QueryParamModel) {
    const qpm = JSON.stringify(queryParams);
    return this.http.get<ResponseModel>(API_BASE_USER + 'api/UserScheduledSms?queryParam=' + qpm);
  }

  editSSI(schedule: ScheduleSmsInfoModel) {
    return this.http.put<ResponseModel>(API_BASE_USER + 'api/UserScheduledSms', schedule);
  }

  deactivateSSI(schedule: ScheduleSmsInfoModel) {
    const ssi = {...schedule};
    ssi.status = 1 - ssi.status;
    return this.http.put<ResponseModel>(API_BASE_USER + 'api/UserScheduledSms', ssi);
  }

  deleteSSI(ssiId: number) {
    return this.http.delete<ResponseModel>(API_BASE_USER + 'api/UserScheduledSms/' + ssiId);
  }

  

  getUserSentSms(queryParams: QueryParamModel) {
    const qpm = JSON.stringify(queryParams);
    return this.http.get<ResponseModel>(API_BASE_USER + 'api/SendSms?queryParam=' + qpm);
  }

  getUserReceivedSms(queryParams: QueryParamModel) {
    const qpm = JSON.stringify(queryParams);
    return this.http.get<ResponseModel>(API_BASE_USER + 'api/ReceiveSms?queryParam=' + qpm);
  }

  

  getBankInfo() {
    return this.http.get<ResponseModel>(API_BASE_USER + 'api/BankInfo');
  }

  
}
