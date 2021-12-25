import {ContactModel} from '../contact/contact.model';
import {TemplateModel} from './template.model';
import {NumberModel} from '../number/number.model';

export class ScheduleSmsInfoModel {
  id: number;
  name: string;
  code: number;
  addedYear: number;
  addedMonth: number;
  addedDay: number;
  owner: ContactModel;
  template: TemplateModel;
  number: NumberModel;
  status: number;
}
