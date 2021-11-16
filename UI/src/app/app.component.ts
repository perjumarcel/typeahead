import { Component } from '@angular/core';
import { Term } from 'src/models/term';
import { TermService } from 'src/services/term.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  public terms: Term[];

  constructor(private _termService: TermService) {
  }

  getTerms(filter: string) {
    if(filter.length < 3) {
      return;
    }

    this._termService.getTerms(filter).subscribe(
      (data: Term[]) => this.terms = data
    );
  }

  weightIncrease(term: Term) {
    this._termService.postWeightIncrease(term.id, 'mic').subscribe((data: any) => console.log(data));
  }
}
