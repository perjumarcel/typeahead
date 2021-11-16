import { Pipe, PipeTransform } from '@angular/core';
@Pipe({
  name: 'highlight'
})

export class HighlightPipe implements PipeTransform {
  transform(value: string, searchText: string): string {
    if (!searchText) { return value; }

    const re = new RegExp(searchText, 'gi');
    return value.replace(re, '<mark>$&</mark>');
  }
}
