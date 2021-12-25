// Angular
import {Pipe, PipeTransform} from '@angular/core';

/**
 * Returns only first letter of string
 */
@Pipe({
  name: 'persianNumber'
})
export class PersianNumberPipe implements PipeTransform {

  /**
   * Transform
   *
   * @param value: any
   * @param args: any
   */
  transform(value: any, args?: any): any {
    return this.fixNumbers(value);
  }

  fixNumbers(str) {
    if (typeof str === 'string') {
      const id = ['۰', '۱', '۲', '۳', '۴', '۵', '۶', '۷', '۸', '۹'];
      return str.replace(/[0-9]/g, function(w) {
        return id[+w];
      });
    } else {
      const res = str.toString();
      const id = ['۰', '۱', '۲', '۳', '۴', '۵', '۶', '۷', '۸', '۹'];
      return res.replace(/[0-9]/g, function(w) {
        return id[+w];
      });
    }
    return str;
  }
}
