import {ContactModel} from '../contact/contact.model';

export class NumberModel {
  username: string;
  id: number;
  isShared: boolean;
  password: string;
  isBlocked: boolean;
  type: number;
  owner: number;
  number: string;
  createDate: number;
  user: ContactModel;
}
