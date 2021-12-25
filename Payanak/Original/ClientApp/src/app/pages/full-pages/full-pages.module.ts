import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';

import {FullPagesRoutingModule} from './full-pages-routing.module';
import {ChartistModule} from 'ng-chartist';
import {AgmCoreModule} from '@agm/core';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';

import {GalleryPageComponent} from './gallery/gallery-page.component';
import {InvoicePageComponent} from './invoice/invoice-page.component';
import {HorizontalTimelinePageComponent} from './timeline/horizontal/horizontal-timeline-page.component';
import {HorizontalTimelineComponent} from './timeline/horizontal/component/horizontal-timeline.component';
import {VerticalTimelinePageComponent} from './timeline/vertical/vertical-timeline-page.component';
import {UserProfilePageComponent} from './user-profile/user-profile-page.component';
import {SearchComponent} from './search/search.component';
import {FaqComponent} from './faq/faq.component';
import {KnowledgeBaseComponent} from './knowledge-base/knowledge-base.component';
import {MatFormFieldModule} from '@angular/material';
import {PersianDatePipe} from '../../shared/pipes/persian-date.pipe';
import {PersianNumberPipe} from '../../shared/pipes/persian-number.pipe';
import {MapModule} from '../../map/map.module';
import {SharedModule} from '../../shared/shared.module';
import { ChangePasswordComponent } from './change-password/change-password.component';


@NgModule({
  imports: [
    CommonModule,
    FullPagesRoutingModule,
    FormsModule,
    ChartistModule,
    AgmCoreModule,
    NgbModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    MapModule,
    SharedModule
  ],
  declarations: [
    GalleryPageComponent,
    InvoicePageComponent,
    HorizontalTimelinePageComponent,
    HorizontalTimelineComponent,
    VerticalTimelinePageComponent,
    UserProfilePageComponent,
    SearchComponent,
    FaqComponent,
    KnowledgeBaseComponent,
    ChangePasswordComponent,
  ]
})
export class FullPagesModule {
}
