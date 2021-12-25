import {AfterViewInit, Component, ElementRef, OnDestroy, OnInit, Renderer2, ViewChild} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {customAnimations} from '../animations/custom-animations';
import {ConfigService} from '../services/config.service';
import {LayoutService} from '../services/layout.service';
import {Subscription} from 'rxjs';
import {MenuService} from '../services/menu.service';
import {ResponseModel} from '../model/Response/responseModel';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  animations: customAnimations
})
export class SidebarComponent implements OnInit, AfterViewInit, OnDestroy {

  @ViewChild('toggleIcon', {static: false}) toggleIcon: ElementRef;
  public menuItems: any[];
  depth: number;
  activeTitle: string;
  activeTitles: string[] = [];
  expanded: boolean;
  nav_collapsed_open = false;
  logoUrl = 'assets/img/Payanak.svg';
  public config: any = {};
  layoutSub: Subscription;


  constructor(
    private elementRef: ElementRef,
    private renderer: Renderer2,
    private router: Router,
    private menuService: MenuService,
    private route: ActivatedRoute,
    private configService: ConfigService,
    private layoutService: LayoutService
  ) {
    if (this.depth === undefined) {
      this.depth = 0;
      this.expanded = true;
    }

    this.layoutSub = layoutService.customizerChangeEmitted$.subscribe(
      options => {
        if (options) {
          if (options.bgColor) {
            if (options.bgColor === 'white') {
              this.logoUrl = 'assets/img/Payanak.svg';
            } else {
              this.logoUrl = 'assets/img/Payanak2.svg';
            }
          }

          if (options.compactMenu === true) {
            this.expanded = false;
            this.renderer.addClass(this.toggleIcon.nativeElement, 'ft-toggle-left');
            this.renderer.removeClass(this.toggleIcon.nativeElement, 'ft-toggle-right');
            this.nav_collapsed_open = true;
          } else if (options.compactMenu === false) {
            this.expanded = true;
            this.renderer.removeClass(this.toggleIcon.nativeElement, 'ft-toggle-left');
            this.renderer.addClass(this.toggleIcon.nativeElement, 'ft-toggle-right');
            this.nav_collapsed_open = false;
          }

        }
      });

  }


  ngOnInit() {
    this.config = this.configService.templateConf;
    // this.menuItems = ROUTES;
    this.menuService.getMenu().subscribe(
      res => {
        console.log(res);
        this.menuItems = res.Result;
        if (res.status.length === 1 && res.status[0].status === 200) {
          this.menuItems = res.Result;
        }
      }
    );

    if (this.config.layout.sidebar.backgroundColor === 'white') {
      this.logoUrl = 'assets/img/Payanak.svg';
    } else {
      this.logoUrl = 'assets/img/Payanak2.svg';
    }


  }

  ngAfterViewInit() {

    setTimeout(() => {
      if (this.config.layout.sidebar.collapsed !== undefined) {
        if (this.config.layout.sidebar.collapsed === true) {
          this.expanded = false;
          this.renderer.addClass(this.toggleIcon.nativeElement, 'ft-toggle-left');
          this.renderer.removeClass(this.toggleIcon.nativeElement, 'ft-toggle-right');
          this.nav_collapsed_open = true;
        } else if (this.config.layout.sidebar.collapsed === false) {
          this.expanded = true;
          this.renderer.removeClass(this.toggleIcon.nativeElement, 'ft-toggle-left');
          this.renderer.addClass(this.toggleIcon.nativeElement, 'ft-toggle-right');
          this.nav_collapsed_open = false;
        }
      }
    }, 0);


  }

  ngOnDestroy() {
    if (this.layoutSub) {
      this.layoutSub.unsubscribe();
    }
  }

  toggleSlideInOut() {
    this.expanded = !this.expanded;
  }

  handleToggle(titles) {
    this.activeTitles = titles;
  }

  // NGX Wizard - skip url change
  ngxWizardFunction(path: string) {
    if (path.indexOf('forms/ngx') !== -1) {
      this.router.navigate(['forms/ngx/wizard'], {skipLocationChange: false});
    }
  }
}
