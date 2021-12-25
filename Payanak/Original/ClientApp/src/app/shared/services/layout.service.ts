import {Injectable} from '@angular/core';
import {Subject} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LayoutService {

  private emitChangeSource = new Subject<any>();
  changeEmitted$ = this.emitChangeSource.asObservable();

  private emitCustomizerSource = new Subject<any>();
  customizerChangeEmitted$ = this.emitCustomizerSource.asObservable();

  private emitCustomizerCMSource = new Subject<any>();
  customizerCMChangeEmitted$ = this.emitCustomizerCMSource.asObservable();

  private emitNotiSidebarSource = new Subject<any>();
  notiSidebarChangeEmitted$ = this.emitNotiSidebarSource.asObservable();

  emitChange(change: any) {
    this.emitChangeSource.next(change);
  }

  // Customizer
  emitCustomizerChange(change: any) {
    let sum = 0;
    for (const itm in change) {
      sum++;
    }
    if (sum >= 7) {
      localStorage.setItem('themeDetail', JSON.stringify(change));
    }
    this.emitCustomizerSource.next(change);
  }

  // customizer - compact menu
  emitCustomizerCMChange(change: any) {
    this.emitCustomizerCMSource.next(change);
  }

  // customizer - compact menu
  emitNotiSidebarChange(change: any) {
    this.emitNotiSidebarSource.next(change);
  }
}
