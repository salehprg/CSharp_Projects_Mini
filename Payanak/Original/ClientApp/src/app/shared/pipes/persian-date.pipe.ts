// Angular
import {Pipe, PipeTransform} from '@angular/core';

const month = [
  'فروردین',
  'اردیبهشت',
  'خرداد',
  'تیر',
  'مرداد',
  'شهریور',
  'مهر',
  'آبان',
  'آذر',
  'دی',
  'بهمن',
  'اسفند',
];
const persianNumbers = [/۰/g, /۱/g, /۲/g, /۳/g, /۴/g, /۵/g, /۶/g, /۷/g, /۸/g, /۹/g];

/**
 * Returns only first letter of string
 */
@Pipe({
  name: 'persianDate'
})
export class PersianDatePipe implements PipeTransform {

  /**
   * Transform
   *
   * @param value: any
   * @param args: any
   */
  transform(value: any, args?: any): any {
    const ticks: number = +value;
    if (ticks === -1) {
      return '---';
    }
    const epochTicks = 621355968000000000;
    const ticksPerMillisecond = 10000; // there are 10000 .net ticks per millisecond

    const jsTicks = (ticks - epochTicks) / ticksPerMillisecond;
    const tickDate = new Date(jsTicks);
    const dateStr = tickDate.toLocaleDateString('fa-IR');
    const dateParts = dateStr.split('/');
    return dateParts[2] + ' ' + month[parseInt(this.fixNumbers(dateParts[1]), 10) - 1] + ' ' + dateParts[0];
  }

  fixNumbers(str) {
    if (typeof str === 'string') {
      for (let i = 0; i < 10; i++) {
        str = str.replace(persianNumbers[i], i);
      }
    }
    return str;
  }
}
