import {GroupModel} from '../group.model';
import {ContactModel} from '../contact/contact.model';
import {TemplateModel} from './template.model';
import {NumberModel} from '../number/number.model';

export class PanelModel {
  id: number;
  serial: string;
  createDate: number;
  hashId: string;
  number: string;
  group: GroupModel;
  user: ContactModel;
  version: string;
  lastActivity: number;
  name: string;
  isBlocked: boolean;
  status: number;
  template: TemplateModel;
  sendNumber: NumberModel;
}
