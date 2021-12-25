import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {SidebarComponent} from './sidebar/sidebar.component';
import {SidebarDirective} from './directives/sidebar.directive';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import {RouterModule} from '@angular/router';
import {PerfectScrollbarModule} from 'ngx-perfect-scrollbar';
import {SidebarLinkDirective} from './directives/sidebarlink.directive';
import {SidebarListDirective} from './directives/sidebarlist.directive';
import {SidebarAnchorToggleDirective} from './directives/sidebaranchortoggle.directive';
import {SidebarToggleDirective} from './directives/sidebartoggle.directive';
import {CustomizerComponent} from './customizer/customizer.component';
import {NotificationSidebarComponent} from './notification-sidebar/notification-sidebar.component';
import {NavbarComponent} from './navbar/navbar.component';
import { OsmViewComponent } from './osm-view/osm-view.component';
import {TaskComponentModule} from '../components/task-component/task-component.module';
import {ToServerPathPipe} from './pipes/to-server-path.pipe';
import {PersianUnitPipe} from './pipes/persian-unit.pipe';
import {ToggleFullscreenDirective} from './directives/toggle-fullscreen.directive';
import {PersianDatePipe} from './pipes/persian-date.pipe';
import {PersianNumberPipe} from './pipes/persian-number.pipe';

@NgModule({
  declarations: [
    // FooterComponent,
    NavbarComponent,
    SidebarComponent,
    CustomizerComponent,
    NotificationSidebarComponent,
    // ToggleFullscreenDirective,
    SidebarDirective,
    SidebarLinkDirective,
    SidebarListDirective,
    SidebarAnchorToggleDirective,
    SidebarToggleDirective,
    OsmViewComponent,
    ToServerPathPipe,
    PersianUnitPipe,
    PersianDatePipe,
    PersianNumberPipe,
    ToggleFullscreenDirective
  ],
  imports: [
    RouterModule,
    CommonModule,
    NgbModule,

    // TranslateModule,
    PerfectScrollbarModule,
    TaskComponentModule
  ],
  exports: [
    CommonModule,
    // FooterComponent,
    NavbarComponent,
    SidebarComponent,
    CustomizerComponent,
    NotificationSidebarComponent,
    ToggleFullscreenDirective,
    SidebarDirective,
    NgbModule,
    OsmViewComponent,
    ToServerPathPipe,
    PersianUnitPipe,
    PersianDatePipe,
    PersianNumberPipe
    // TranslateModule
  ]
})
export class SharedModule { }
