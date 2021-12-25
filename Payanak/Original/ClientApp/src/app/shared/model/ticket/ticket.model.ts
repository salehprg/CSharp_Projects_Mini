import {ContactModel} from '../contact/contact.model';

export class TicketModel {
  id: number;
  createDate: number;
  user: ContactModel;
  responder: ContactModel;
  status: number;
  header: string;
  lastMessage: string;
  unread: number;
}
