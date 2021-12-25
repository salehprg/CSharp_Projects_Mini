import {AfterViewInit, Component, ElementRef, EventEmitter, OnDestroy, OnInit, Output, ViewChild} from '@angular/core';
import {LayoutService} from '../services/layout.service';
import {fromEvent, Subscription} from 'rxjs';
import {ConfigService} from '../services/config.service';
import {TaskService} from '../services/task.service';
import {Store} from '@ngrx/store';
import {AppState} from '../reducers';
import {Logout} from '../actions/auth/auth.actions';
import {debounceTime, distinctUntilChanged, tap} from 'rxjs/operators';
import {SearchService} from '../services/search.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit, AfterViewInit, OnDestroy {
  currentLang = 'en';
  toggleClass = 'ft-maximize';
  placement = 'bottom-right';
  public isCollapsed = true;
  layoutSub: Subscription;
  @ViewChild('searchInput', {static: true}) searchInput: ElementRef;
  @Output()
  toggleHideSidebar = new EventEmitter<Object>();
  private subscriptions: Subscription[] = [];
  taskLength: number;

  public config: any = {};

  constructor( private layoutService: LayoutService,
               private configService: ConfigService,
               public taskService: TaskService,
               private searchService: SearchService,
               private store: Store<AppState>) {




    this.layoutSub = layoutService.changeEmitted$.subscribe(
      direction => {
        const dir = direction.direction;
        if (dir === 'rtl') {
          this.placement = 'bottom-left';
        } else if (dir === 'ltr') {
          this.placement = 'bottom-right';
        }
      });
  }

  ngOnInit() {
    this.config = this.configService.templateConf;
    // Filtration, bind to searchInput
    const searchSubscription = fromEvent(this.searchInput.nativeElement, 'keyup').pipe(
      // tslint:disable-next-line:max-line-length
      debounceTime(150), // The user can type quite quickly in the input box, and that could trigger a lot of server requests. With this operator, we are limiting the amount of server requests emitted to a maximum of one every 150ms
      distinctUntilChanged(), // This operator will eliminate duplicate values
      tap(() => {
        this.searchService.setFilter(this.searchInput.nativeElement.value);
      })
    )
      .subscribe();
    this.subscriptions.push(searchSubscription);
  }

  ngAfterViewInit() {
    if (this.config.layout.dir) {
      setTimeout(() => {
        const dir = this.config.layout.dir;
        if (dir === 'rtl') {
          this.placement = 'bottom-left';
        } else if (dir === 'ltr') {
          this.placement = 'bottom-right';
        }
      }, 0);

    }
  }

  ngOnDestroy() {
    if (this.layoutSub) {
      this.layoutSub.unsubscribe();
    }
    this.subscriptions.forEach(el => el.unsubscribe());
  }

  ChangeLanguage(language: string) {
    // this.translate.use(language);
  }

  ToggleClass() {
    if (this.toggleClass === 'ft-maximize') {
      this.toggleClass = 'ft-minimize';
    } else {
      this.toggleClass = 'ft-maximize';
    }
  }

  toggleNotificationSidebar() {
    this.layoutService.emitNotiSidebarChange(true);
  }

  toggleSidebar() {
    const appSidebar = document.getElementsByClassName('app-sidebar')[0];
    if (appSidebar.classList.contains('hide-sidebar')) {
      this.toggleHideSidebar.emit(false);
    } else {
      this.toggleHideSidebar.emit(true);
    }
  }

  logout() {
    this.store.dispatch(new Logout());
  }
}
