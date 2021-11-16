import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class TermService {

  constructor(private _http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
  }

  getTerms(filter: string) {
    return this._http.get(this.baseUrl + 'terms/' + filter).pipe(map(
      response => {
        return response;
      }));
  }

  postWeightIncrease(id: number, filter: string) {
    let content = { termId: id, input: filter };
    return this._http.post(this.baseUrl + 'weight', content)
      .pipe(map(
        response => {
          return response;
        }));
  }
}
