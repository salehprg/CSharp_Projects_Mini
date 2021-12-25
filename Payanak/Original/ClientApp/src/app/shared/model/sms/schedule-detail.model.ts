import {ScheduleSmsInfoModel} from './schedule-sms-info.model';
import {ContactModel} from '../contact/contact.model';

export class ScheduleDetailModel {
  id: number;
  parent: ScheduleSmsInfoModel;
  date: number;
  updatedDate: number;
  counter: number;
  user: ContactModel;
}
