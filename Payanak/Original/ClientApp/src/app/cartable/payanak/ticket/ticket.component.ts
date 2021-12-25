import {ChangeDetectorRef, Component, ElementRef, OnDestroy, OnInit, Renderer2, ViewChild} from '@angular/core';
import {ChatModel} from '../../../shared/model/chat.model';
import {TicketService} from '../../../shared/services/ticket/ticket.service';
import {QueryParamModel} from '../../../shared/model/Response/query-param.model';
import {ToastrService} from 'ngx-toastr';
import {TicketModel} from '../../../shared/model/ticket/ticket.model';
import {MatDialog} from '@angular/material';
import {GetNameComponent} from '../../../components/get-name/get-name.component';
import {ConfirmComponent} from '../../../components/confirm/confirm.component';
import {Subscription} from 'rxjs';
import {SearchService} from '../../../shared/services/search.service';

@Component({
  selector: 'app-ticket',
  templateUrl: './ticket.component.html',
  styleUrls: ['./ticket.component.scss']
})
export class TicketComponent implements OnInit, OnDestroy {
  tickets: TicketModel[] = [];
  chat: ChatModel[];
  activeChatUser: string;
  activeChatUserImg: string;
  selectedId: number;
  chatLoaded = false;
  @ViewChild('messageInput', {static: true}) messageInputRef: ElementRef;
  @ViewChild('chatSidebar', {static: true}) sidebar: ElementRef;
  @ViewChild('contentOverlay', {static: true}) overlay: ElementRef;
  @ViewChild('body', {static: false}) body: ElementRef;

  messages = [];
  item = 0;
  queryParam: QueryParamModel;

  search: string;
  private subscriptions: Subscription[] = [];

  constructor(private elRef: ElementRef,
              private renderer: Renderer2,
              private chatService: TicketService,
              public toaster: ToastrService,
              private dialog: MatDialog,
              private searchService: SearchService,
              private cdRef: ChangeDetectorRef) {
  }

  ngOnInit() {
    this.onSearch();
    const searchSubscription = this.searchService.filterChanged.subscribe(
      res => {
        this.onSearch(res);
      }
    );
    this.subscriptions.push(searchSubscription);
  }

  ngOnDestroy() {
    this.subscriptions.forEach(el => el.unsubscribe());
  }

  onSearch(query: string = '') {
    this.search = query;
    this.loadTickets();
  }

  onKeydown(event, str) {
    this.sendTicketBody(str);
  }

  SetActiveId(event, chat: TicketModel) {
    const hElement: HTMLElement = this.elRef.nativeElement;
    // now you can simply get your elements with their class name
    const allAnchors = hElement.getElementsByClassName('list-group-item');
    // do something with selected elements
    [].forEach.call(allAnchors, function(item: HTMLElement) {
      item.setAttribute('class', 'list-group-item no-border');
    });
    // set active class for selected item
    event.currentTarget.setAttribute('class', 'list-group-item bg-blue-grey bg-lighten-5 border-right-primary border-right-2');

    this.messages = [];
    this.chatLoaded = false;
    if (!chat) {
      return;
    }
    this.chatService.getUserTicketDetail(chat.id).subscribe(
      res => {
        if (res && res.Status && res.Status.length === 1 && res.Status[0].status === 200) {
          this.chat = res.Result;
          this.activeChatUser = chat.header;
          this.activeChatUserImg = chat.user.picture;
          this.selectedId = chat.id;
          this.chatLoaded = true;
          this.cdRef.detectChanges();
        } else {
          for (const itm of res.Status) {
            this.toaster.error(res.Status[0].message, 'خطا');
          }
        }
      },
      err => {
        this.toaster.error('خطا در عملیات.', 'خطا');
      }
    );
    this.cdRef.detectChanges();
  }

  onSidebarToggle() {
    this.renderer.removeClass(this.sidebar.nativeElement, 'd-none');
    this.renderer.removeClass(this.sidebar.nativeElement, 'd-sm-none');
    this.renderer.addClass(this.sidebar.nativeElement, 'd-block');
    this.renderer.addClass(this.sidebar.nativeElement, 'd-sm-block');
    this.renderer.addClass(this.overlay.nativeElement, 'show');
  }

  onContentOverlay() {
    this.renderer.removeClass(this.overlay.nativeElement, 'show');
    this.renderer.removeClass(this.sidebar.nativeElement, 'd-block');
    this.renderer.removeClass(this.sidebar.nativeElement, 'd-sm-block');
    this.renderer.addClass(this.sidebar.nativeElement, 'd-none');
    this.renderer.addClass(this.sidebar.nativeElement, 'd-sm-none');

  }

  AddTicket() {
    const dialogTitle = 'موضوع تیکت';
    const dialogBody = 'لطفا نامی مرتبط با موضوع وارد نمایید.';
    const dialogRef = this.dialog.open(GetNameComponent, {
      height: 'auto',
      data: {title: dialogTitle, body: dialogBody, hasValue: true, value: ''}
    });

    dialogRef.afterClosed().subscribe(
      (result) => {
        if (result) {
          const ticket: TicketModel = {
            header: result.value,
            createDate: -1,
            responder: null,
            id: -1,
            status: 1,
            user: null,
            lastMessage: null,
            unread: 0
          };
          this.chatService.addNewTicket(ticket).subscribe(
            res => {
              if (res && res.Status && res.Status.length === 1 && res.Status[0].status === 200) {
                this.onSearch(this.search);
              } else {
                for (const itm of res.Status) {
                  this.toaster.error(res.Status[0].message, 'خطا');
                }
              }
            },
            err => {
              this.toaster.error('خطا در عملیات.', 'خطا');
            }
          );
        }
      },
      error => {
      }
    );

  }

  loadTickets() {
    this.queryParam = {
      filter: this.search,
      pageNumber: 1,
      pageSize: 100,
      sortField: '',
      sortOrder: ''
    };
    this.chatService.getAllTickets(this.queryParam).subscribe(
      res => {
        if (res && res.Status && res.Status.length === 1 && res.Status[0].status === 200) {
          this.tickets = res.Result;
        } else {
          for (const itm of res.Status) {
            this.toaster.error(res.Status[0].message, 'خطا');
          }
        }
      },
      err => {
        this.toaster.error('خطا در عملیات.', 'خطا');
      }
    );
  }

  sendTicketBody(str: string) {
    const td: ChatModel = {
      ticketId: this.selectedId,
      avatar: '',
      chatClass: '',
      imagePath: '',
      messages: [str],
      messageType: '',
      time: ''
    };
    this.chatService.addNewTicketDetail(td).subscribe(
      res => {
        if (res && res.Status && res.Status.length === 1 && res.Status[0].status === 200) {
          this.chat.push(res.Result);
        } else {
          for (const itm of res.Status) {
            this.toaster.error(res.Status[0].message, 'خطا');
          }
        }
      },
      err => {
        this.toaster.error('خطا در عملیات.', 'خطا');
      }
    );
    this.body.nativeElement.value = '';
  }

  deactive() {
    const dialogTitle = 'تایید عملیات';
    const dialogBody = 'آیا اطمینان دارید؟';
    const dialogRef = this.dialog.open(ConfirmComponent, {
      height: 'auto',
      data: {title: dialogTitle, body: dialogBody, hasValue: false, value: ''}
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.chatService.CompleteUserTicket(this.selectedId).subscribe(
          res => {
            if (res && res.Status && res.Status.length === 1 && res.Status[0].status === 200) {
              this.chatLoaded = false;
              this.loadTickets();

            } else {
              for (const itm of res.Status) {
                this.toaster.error(res.Status[0].message, 'خطا');
              }
            }
          },
          err => {
            this.toaster.error('خطا در عملیات.', 'خطا');
          }
        );
      }
    });
  }

  delete() {
    const dialogTitle = 'تایید عملیات';
    const dialogBody = 'آیا اطمینان دارید؟';
    const dialogRef = this.dialog.open(ConfirmComponent, {
      height: 'auto',
      data: {title: dialogTitle, body: dialogBody, hasValue: false, value: ''}
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.chatService.DeleteUserTicket(this.selectedId).subscribe(
          res => {
            if (res && res.Status && res.Status.length === 1 && res.Status[0].status === 200) {
              this.chatLoaded = false;
              this.onSearch(this.search);

            } else {
              for (const itm of res.Status) {
                this.toaster.error(res.Status[0].message, 'خطا');
              }
            }
          },
          err => {
            this.toaster.error('خطا در عملیات.', 'خطا');
          }
        );
      }
    });
  }

  isDeactive() {
    const index = this.tickets.findIndex((a) => {
      return a.id === this.selectedId;
    });
    if (index >= 0) {
      return this.tickets[index].status !== 1;
    }
    return false;
  }

}
