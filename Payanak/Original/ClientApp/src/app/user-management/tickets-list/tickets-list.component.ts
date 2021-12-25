import {ChangeDetectorRef, Component, ElementRef, OnDestroy, OnInit, Renderer2, ViewChild} from '@angular/core';
import {TicketModel} from '../../shared/model/ticket/ticket.model';
import {ChatModel} from '../../shared/model/chat.model';
import {QueryParamModel} from '../../shared/model/Response/query-param.model';
import {TicketService} from '../../shared/services/ticket/ticket.service';
import {ToastrService} from 'ngx-toastr';
import {MatDialog} from '@angular/material';
import {GetNameComponent} from '../../components/get-name/get-name.component';
import {ConfirmComponent} from '../../components/confirm/confirm.component';
import {Subscription} from 'rxjs';
import {SearchService} from '../../shared/services/search.service';

@Component({
  selector: 'app-tickets-list',
  templateUrl: './tickets-list.component.html',
  styleUrls: ['./tickets-list.component.scss']
})
export class TicketsListComponent implements OnInit, OnDestroy {
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
    this.chatService.getAdminTicketDetail(chat.id).subscribe(
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


  loadTickets() {
    this.queryParam = {
      filter: '',
      pageNumber: 1,
      pageSize: 1000,
      sortField: '',
      sortOrder: ''
    };
    this.chatService.getTicketsList(this.queryParam).subscribe(
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

  deactive() {
    const dialogTitle = 'تایید عملیات';
    const dialogBody = 'آیا اطمینان دارید؟';
    const dialogRef = this.dialog.open(ConfirmComponent, {
      height: 'auto',
      data: {title: dialogTitle, body: dialogBody, hasValue: false, value: ''}
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.chatService.CompleteAdminTicket(this.selectedId).subscribe(
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
    this.chatService.addNewTicketDetailAdmin(td).subscribe(
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

}
