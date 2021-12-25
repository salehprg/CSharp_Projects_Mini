// Angular
import {Pipe, PipeTransform} from '@angular/core';
import { baseUrl } from 'src/polyfills';

const API_BASE_USER = baseUrl;

/**
 * Returns only first letter of string
 */
@Pipe({
  name: 'toServerPath'
})
export class ToServerPathPipe implements PipeTransform {

  /**
   * Transform
   *
   * @param value: any
   * @param args: any
   */

  transform(value: any, args?: any): any {
    if (!value) {
      return value;
    }
    const str = value.toString();
    if (typeof value === 'string' && str[0] === '~') {
      return API_BASE_USER + str.substr(2);
    }
    return value;
  }
}
