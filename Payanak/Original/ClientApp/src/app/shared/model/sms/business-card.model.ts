import {ContactModel} from '../contact/contact.model';
import {GroupModel} from '../group.model';
import {TemplateModel} from './template.model';
import {NumberModel} from '../number/number.model';

export class BusinessCardModel {
  id: number;
  createDate: number;
  isBlocked: boolean;
  status: number;
  user: ContactModel;
  group: GroupModel;
  template: TemplateModel;
  number: NumberModel;
  key: string;
}
