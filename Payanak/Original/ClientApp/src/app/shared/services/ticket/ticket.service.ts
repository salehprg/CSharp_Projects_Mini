import {Injectable} from '@angular/core';
import {ChatModel} from '../../model/chat.model';
import {QueryParamModel} from '../../model/Response/query-param.model';
import {ResponseModel} from '../../model/Response/responseModel';
import {HttpClient} from '@angular/common/http';
import {TicketModel} from '../../model/ticket/ticket.model';
import { baseUrl } from 'src/polyfills';

const API_BASE_USER = baseUrl;

@Injectable()
export class TicketService {

  constructor(private http: HttpClient) {
  }

  public chat1: ChatModel[] = [
    new ChatModel(
      'right',
      'chat',
      'assets/img/portrait/small/avatar-s-1.png',
      '',
      [
        'How can we help? We are here for you!'
      ],
      'text'),
    new ChatModel(
      'left',
      'chat chat-left',
      'assets/img/portrait/small/avatar-s-3.png',
      '1 hour ago',
      [
        'Hey John, I am looking for the best admin template.',
        'Could you please help me to find it out?',
        'It should be angular 5 bootstrap compatible.'
      ]
      , 'text'),
    new ChatModel(
      'right',
      'chat',
      'assets/img/portrait/small/avatar-s-1.png',
      '30 minutes ago',
      [
        'Absolutely!',
        'Apex admin is the responsive angular 5 bootstrap admin template.'
      ]
      , 'text'),
    new ChatModel(
      'left',
      'chat chat-left',
      'assets/img/portrait/small/avatar-s-3.png',
      '20 minutes ago',
      [
        'Can you provide me audio link?'
      ]
      , 'text'),
    new ChatModel(
      'right',
      'chat',
      'assets/img/portrait/small/avatar-s-1.png',
      '',
      [
        'http://static.videogular.com/assets/audios/videogular.mp3'
      ]
      , 'audio'),
    new ChatModel(
      'left',
      'chat chat-left',
      'assets/img/portrait/small/avatar-s-3.png',
      '10 minutes ago',
      [
        'Can you provide me video link?'
      ]
      , 'text'),
    new ChatModel(
      'right',
      'chat',
      'assets/img/portrait/small/avatar-s-1.png',
      '',
      [
        'http://static.videogular.com/assets/videos/videogular.mp4'
      ]
      , 'video'),
    new ChatModel(
      'left',
      'chat chat-left',
      'assets/img/portrait/small/avatar-s-3.png',
      'just now',
      [
        'Looks clean and fresh UI.',
        'It is perfect for my next project.',
        'How can I purchase it?'
      ]
      , 'text'),
    new ChatModel(
      'right',
      'chat',
      'assets/img/portrait/small/avatar-s-1.png',
      '',
      [
        'Thanks, from ThemeForest.'
      ]
      , 'text'),
    new ChatModel(
      'left',
      'chat chat-left',
      'assets/img/portrait/small/avatar-s-3.png',
      '',
      [
        'I will purchase it for sure.',
        'Thanks.'
      ]
      , 'text'),
  ];
  public chat2: ChatModel[] = [
    new ChatModel(
      'right',
      'chat',
      'assets/img/portrait/small/avatar-s-1.png',
      '',
      [
        'How can we help'
      ]
      , 'text'),
    new ChatModel(
      'left',
      'chat chat-left',
      'assets/img/portrait/small/avatar-s-7.png',
      '1 hours ago',
      [
        'Hi, I spoke with a representative yesterday.',
        'he gave me some steps to fix my problem',
        'but they didn’t help.'
      ]
      , 'text'),
    new ChatModel(
      'right',
      'chat',
      'assets/img/portrait/small/avatar-s-1.png',
      '20 minutes ago',
      [
        'Can you provide me audio link of your conversation?'
      ]
      , 'text'),
    new ChatModel(
      'left',
      'chat chat-left',
      'assets/img/portrait/small/avatar-s-7.png',
      '',
      [
        'http://static.videogular.com/assets/audios/videogular.mp3'
      ]
      , 'audio'),
    new ChatModel(
      'right',
      'chat',
      'assets/img/portrait/small/avatar-s-1.png',
      '10 minutes ago',
      [
        'Can you provide me video link of your issue?'
      ]
      , 'text'),
    new ChatModel(
      'left',
      'chat chat-left',
      'assets/img/portrait/small/avatar-s-7.png',
      '',
      [
        'http://static.videogular.com/assets/videos/videogular.mp4'
      ]
      , 'video'),
    new ChatModel(
      'right',
      'chat',
      'assets/img/portrait/small/avatar-s-1.png',
      '',
      [
        'I’m sorry to hear that',
        'Can I ask which model of projector you own?',
        'What steps did he suggest you to take?',
        'What sort of issue are you having?'
      ]
      , 'text'),
    new ChatModel(
      'left',
      'chat chat-left',
      'assets/img/portrait/small/avatar-s-7.png',
      '',
      [
        'An issue with the power.'
      ]
      , 'text'),
    new ChatModel(
      'right',
      'chat',
      'assets/img/portrait/small/avatar-s-1.png',
      '',
      [
        'Did you make sure the outlet you plugged it into was functional.'
      ]
      , 'text'),
    new ChatModel(
      'left',
      'chat chat-left',
      'assets/img/portrait/small/avatar-s-7.png',
      '',
      [
        'Yes'
      ]
      , 'text'),
  ];
  public chat3: ChatModel[] = [
    new ChatModel(
      'right',
      'chat',
      'assets/img/portrait/small/avatar-s-1.png',
      '',
      [
        'How can we help'
      ]
      , 'text'),
    new ChatModel(
      'left',
      'chat chat-left',
      'assets/img/portrait/small/avatar-s-8.png',
      '1 hours ago',
      [
        'Hey John, I am looking for the best admin template.',
        'Could you please help me to find it out?',
        'It should be angular 5 bootstrap compatible.'
      ]
      , 'text'),
    new ChatModel(
      'right',
      'chat',
      'assets/img/portrait/small/avatar-s-1.png',
      '',
      [
        'Absolutely!',
        'Apex admin is the responsive angular 5 bootstrap admin template.'
      ]
      , 'text'),
    new ChatModel(
      'left',
      'chat chat-left',
      'assets/img/portrait/small/avatar-s-8.png',
      '20 minutes ago',
      [
        'Can you provide me audio link?'
      ]
      , 'text'),
    new ChatModel(
      'right',
      'chat',
      'assets/img/portrait/small/avatar-s-1.png',
      '',
      [
        'http://static.videogular.com/assets/audios/videogular.mp3'
      ]
      , 'audio'),
    new ChatModel(
      'left',
      'chat chat-left',
      'assets/img/portrait/small/avatar-s-8.png',
      '10 minutes ago',
      [
        'Can you provide me video link?'
      ]
      , 'text'),
    new ChatModel(
      'right',
      'chat',
      'assets/img/portrait/small/avatar-s-1.png',
      '',
      [
        'http://static.videogular.com/assets/videos/videogular.mp4'
      ]
      , 'video'),
    new ChatModel(
      'left',
      'chat chat-left',
      'assets/img/portrait/small/avatar-s-8.png',
      '',
      [
        'Looks clean and fresh UI.',
        'It is perfect for my next project.',
        'How can I purchase it?'
      ]
      , 'text'),
    new ChatModel(
      'right',
      'chat',
      'assets/img/portrait/small/avatar-s-1.png',
      '',
      [
        'Thanks, from ThemeForest.'
      ]
      , 'text'),
    new ChatModel(
      'left',
      'chat chat-left',
      'assets/img/portrait/small/avatar-s-8.png',
      '',
      [
        'I will purchase it for sure.',
        'Thanks.'
      ]
      , 'text'),
  ];
  public chat4: ChatModel[] = [
    new ChatModel(
      'right',
      'chat',
      'assets/img/portrait/small/avatar-s-1.png',
      '',
      [
        'How can we help'
      ]
      , 'text'),
    new ChatModel(
      'left',
      'chat chat-left',
      'assets/img/portrait/small/avatar-s-5.png',
      '1 hours ago',
      [
        'Hi, I spoke with a representative yesterday.',
        'he gave me some steps to fix my problem',
        'but they didn’t help.'
      ]
      , 'text'),
    new ChatModel(
      'right',
      'chat',
      'assets/img/portrait/small/avatar-s-1.png',
      '20 minutes ago',
      [
        'Can you provide me audio link of your conversation?'
      ]
      , 'text'),
    new ChatModel(
      'left',
      'chat chat-left',
      'assets/img/portrait/small/avatar-s-5.png',
      '',
      [
        'http://static.videogular.com/assets/audios/videogular.mp3'
      ]
      , 'audio'),
    new ChatModel(
      'right',
      'chat',
      'assets/img/portrait/small/avatar-s-1.png',
      '10 minutes ago',
      [
        'Can you provide me video link of your issue?'
      ]
      , 'text'),
    new ChatModel(
      'left',
      'chat chat-left',
      'assets/img/portrait/small/avatar-s-5.png',
      '',
      [
        'http://static.videogular.com/assets/videos/videogular.mp4'
      ]
      , 'video'),
    new ChatModel(
      'right',
      'chat',
      'assets/img/portrait/small/avatar-s-1.png',
      '',
      [
        'I’m sorry to hear that',
        'Can I ask which model of projector you own?',
        'What steps did he suggest you to take?',
        'What sort of issue are you having?'
      ]
      , 'text'),
    new ChatModel(
      'left',
      'chat chat-left',
      'assets/img/portrait/small/avatar-s-5.png',
      '',
      [
        'An issue with the power.'
      ]
      , 'text'),
    new ChatModel(
      'right',
      'chat',
      'assets/img/portrait/small/avatar-s-1.png',
      '',
      [
        'Did you make sure the outlet you plugged it into was functional.'
      ]
      , 'text'),
    new ChatModel(
      'left',
      'chat chat-left',
      'assets/img/portrait/small/avatar-s-5.png',
      '',
      [
        'Yes'
      ]
      , 'text'),
  ];
  public chat5: ChatModel[] = [
    new ChatModel(
      'right',
      'chat',
      'assets/img/portrait/small/avatar-s-1.png',
      '',
      [
        'How can we help'
      ]
      , 'text'),
    new ChatModel(
      'left',
      'chat chat-left',
      'assets/img/portrait/small/avatar-s-9.png',
      '1 hours ago',
      [
        'Hey John, I am looking for the best admin template.',
        'Could you please help me to find it out?',
        'It should be angular 5 bootstrap compatible.'
      ]
      , 'text'),
    new ChatModel(
      'right',
      'chat',
      'assets/img/portrait/small/avatar-s-1.png',
      '',
      [
        'Absolutely!',
        'Apex admin is the responsive angular 5 bootstrap admin template.'
      ]
      , 'text'),
    new ChatModel(
      'left',
      'chat chat-left',
      'assets/img/portrait/small/avatar-s-9.png',
      '20 minutes ago',
      [
        'Can you provide me audio link?'
      ]
      , 'text'),
    new ChatModel(
      'right',
      'chat',
      'assets/img/portrait/small/avatar-s-1.png',
      '',
      [
        'http://static.videogular.com/assets/audios/videogular.mp3'
      ]
      , 'audio'),
    new ChatModel(
      'left',
      'chat chat-left',
      'assets/img/portrait/small/avatar-s-9.png',
      '10 minutes ago',
      [
        'Can you provide me video link?'
      ]
      , 'text'),
    new ChatModel(
      'right',
      'chat',
      'assets/img/portrait/small/avatar-s-1.png',
      '',
      [
        'http://static.videogular.com/assets/videos/videogular.mp4'
      ]
      , 'video'),
    new ChatModel(
      'left',
      'chat chat-left',
      'assets/img/portrait/small/avatar-s-9.png',
      '',
      [
        'Looks clean and fresh UI.',
        'It is perfect for my next project.',
        'How can I purchase it?'
      ]
      , 'text'),
    new ChatModel(
      'right',
      'chat',
      'assets/img/portrait/small/avatar-s-1.png',
      '',
      [
        'Thanks, from ThemeForest.'
      ]
      , 'text'),
    new ChatModel(
      'left',
      'chat chat-left',
      'assets/img/portrait/small/avatar-s-9.png',
      '',
      [
        'I will purchase it for sure.',
        'Thanks.'
      ]
      , 'text'),
  ];
  public chat6: ChatModel[] = [
    new ChatModel(
      'right',
      'chat',
      'assets/img/portrait/small/avatar-s-1.png',
      '',
      [
        'How can we help'
      ]
      , 'text'),
    new ChatModel(
      'left',
      'chat chat-left',
      'assets/img/portrait/small/avatar-s-4.png',
      '1 hours ago',
      [
        'Hi, I spoke with a representative yesterday.',
        'he gave me some steps to fix my problem',
        'but they didn’t help.'
      ]
      , 'text'),
    new ChatModel(
      'right',
      'chat',
      'assets/img/portrait/small/avatar-s-1.png',
      '20 minutes ago',
      [
        'Can you provide me audio link of your conversation?'
      ]
      , 'text'),
    new ChatModel(
      'left',
      'chat chat-left',
      'assets/img/portrait/small/avatar-s-4.png',
      '',
      [
        'http://static.videogular.com/assets/audios/videogular.mp3'
      ]
      , 'audio'),
    new ChatModel(
      'right',
      'chat',
      'assets/img/portrait/small/avatar-s-1.png',
      '10 minutes ago',
      [
        'Can you provide me video link of your issue?'
      ]
      , 'text'),
    new ChatModel(
      'left',
      'chat chat-left',
      'assets/img/portrait/small/avatar-s-4.png',
      '',
      [
        'http://static.videogular.com/assets/videos/videogular.mp4'
      ]
      , 'video'),
    new ChatModel(
      'right',
      'chat',
      'assets/img/portrait/small/avatar-s-1.png',
      '',
      [
        'I’m sorry to hear that',
        'Can I ask which model of projector you own?',
        'What steps did he suggest you to take?',
        'What sort of issue are you having?'
      ]
      , 'text'),
    new ChatModel(
      'left',
      'chat chat-left',
      'assets/img/portrait/small/avatar-s-4.png',
      '',
      [
        'An issue with the power.'
      ]
      , 'text'),
    new ChatModel(
      'right',
      'chat',
      'assets/img/portrait/small/avatar-s-1.png',
      '',
      [
        'Did you make sure the outlet you plugged it into was functional.'
      ]
      , 'text'),
    new ChatModel(
      'left',
      'chat chat-left',
      'assets/img/portrait/small/avatar-s-4.png',
      '',
      [
        'Yes'
      ]
      , 'text'),
  ];
  public chat7: ChatModel[] = [
    new ChatModel(
      'right',
      'chat',
      'assets/img/portrait/small/avatar-s-1.png',
      '',
      [
        'How can we help'
      ]
      , 'text'),
    new ChatModel(
      'left',
      'chat chat-left',
      'assets/img/portrait/small/avatar-s-14.png',
      '1 hours ago',
      [
        'Hey John, I am looking for the best admin template.',
        'Could you please help me to find it out?',
        'It should be angular 4 bootstrap compatible.'
      ]
      , 'text'),
    new ChatModel(
      'right',
      'chat',
      'assets/img/portrait/small/avatar-s-1.png',
      '',
      [
        'Absolutely!',
        'Apex admin is the responsive angular 4 bootstrap admin template.'
      ]
      , 'text'),
    new ChatModel(
      'left',
      'chat chat-left',
      'assets/img/portrait/small/avatar-s-14.png',
      '20 minutes ago',
      [
        'Can you provide me audio link?'
      ]
      , 'text'),
    new ChatModel(
      'right',
      'chat',
      'assets/img/portrait/small/avatar-s-1.png',
      '',
      [
        'http://static.videogular.com/assets/audios/videogular.mp3'
      ]
      , 'audio'),
    new ChatModel(
      'left',
      'chat chat-left',
      'assets/img/portrait/small/avatar-s-14.png',
      '10 minutes ago',
      [
        'Can you provide me video link?'
      ]
      , 'text'),
    new ChatModel(
      'right',
      'chat',
      'assets/img/portrait/small/avatar-s-1.png',
      '',
      [
        'http://static.videogular.com/assets/videos/videogular.mp4'
      ]
      , 'video'),
    new ChatModel(
      'left',
      'chat chat-left',
      'assets/img/portrait/small/avatar-s-14.png',
      '',
      [
        'Looks clean and fresh UI.',
        'It is perfect for my next project.',
        'How can I purchase it?'
      ]
      , 'text'),
    new ChatModel(
      'right',
      'chat',
      'assets/img/portrait/small/avatar-s-1.png',
      '',
      [
        'Thanks, from ThemeForest.'
      ]
      , 'text'),
    new ChatModel(
      'left',
      'chat chat-left',
      'assets/img/portrait/small/avatar-s-14.png',
      '',
      [
        'I will purchase it for sure.',
        'Thanks.'
      ]
      , 'text'),
  ];

  getAllTickets(queryParams: QueryParamModel) {
    const qpm = JSON.stringify(queryParams);
    return this.http.get<ResponseModel>(API_BASE_USER + 'api/UserTicket?queryParam=' + qpm);
  }

  getTicketsList(queryParams: QueryParamModel) {
    const qpm = JSON.stringify(queryParams);
    return this.http.get<ResponseModel>(API_BASE_USER + 'api/AdminTicket?queryParam=' + qpm);
  }

  addNewTicket(ticket: TicketModel) {
    return this.http.post<ResponseModel>(API_BASE_USER + 'api/UserTicket', ticket);
  }

  getUserTicketDetail(tdId: number) {
    return this.http.get<ResponseModel>(API_BASE_USER + 'api/UserTicket/' + tdId);
  }

  getAdminTicketDetail(tdId: number) {
    return this.http.get<ResponseModel>(API_BASE_USER + 'api/AdminTicket/' + tdId);
  }

  CompleteAdminTicket(tdId: number) {
    return this.http.delete<ResponseModel>(API_BASE_USER + 'api/AdminTicket/' + tdId);
  }

  CompleteUserTicket(tdId: number) {
    return this.http.delete<ResponseModel>(API_BASE_USER + 'api/UserTicket/Deactive/' + tdId);
  }

  DeleteUserTicket(tdId: number) {
    return this.http.delete<ResponseModel>(API_BASE_USER + 'api/UserTicket/DeleteTicket/' + tdId);
  }

  addNewTicketDetail(chat: ChatModel) {
    return this.http.put<ResponseModel>(API_BASE_USER + 'api/UserTicket', chat);
  }

  addNewTicketDetailAdmin(chat: ChatModel) {
    return this.http.put<ResponseModel>(API_BASE_USER + 'api/AdminTicket', chat);
  }
}
