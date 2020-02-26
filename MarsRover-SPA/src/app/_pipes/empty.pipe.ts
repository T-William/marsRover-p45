import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'empty'
})
export class EmptyPipe implements PipeTransform {
  transform(value: any, args?: any): any {
    if (value === null || value === '' || value === undefined) {
      return '-';
    } else {
      return value;
    }
  }
}
