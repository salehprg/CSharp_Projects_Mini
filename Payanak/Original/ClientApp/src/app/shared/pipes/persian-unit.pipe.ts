// Angular
import {Pipe, PipeTransform} from '@angular/core';

/**
 * Returns only first letter of string
 */
@Pipe({
  name: 'persianUnit'
})
export class PersianUnitPipe implements PipeTransform {

  /**
   * Transform
   *
   * @param value: any
   * @param args: any
   */
  transform(value: any, args?: any): any {
    return this.fixNumbers(value) + ' ' + args;
  }

  fixNumbers(str) {
    if (typeof str === 'string') {
      const id = ['۰', '۱', '۲', '۳', '۴', '۵', '۶', '۷', '۸', '۹'];
      return str.replace(/[0-9]/g, function(w) {
        return id[+w];
      });
    }
    return str;
  }
}
