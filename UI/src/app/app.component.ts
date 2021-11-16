import { Component } from '@angular/core';
import { Term } from 'src/models/term';
import { TermService } from 'src/services/term.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  public terms: Term[];
  public filter: string;
  public selected: Term;
  public isListVisible: Boolean;

  constructor(private _termService: TermService) {
  }

  getTerms(filter: string) {
    if (filter.length < 3) {
      return;
    }

    this._termService.getTerms(filter).subscribe(
      (data: Term[]) => {
        this.terms = data;
        this.selected = null;
        this.setListVisibility(true);
      },
      (error: any) => {
        this.setListVisibility(false);
        this.clearTerms();
      }
    );
  }

  weightIncrease(term: Term) {
    this._termService.postWeightIncrease(term.id, this.filter).subscribe((data: any) => console.log(data));
  }

  onKeyUp(event: any) {
    if (this.filter == event.target.value) {
      return;
    }

    this.filter = event.target.value;
    if (this.filter.length > 2) {
      this.getTerms(this.filter);
    } else {
      this.clearTerms();
    }
  }

  onLostFocus(event: any) {
    this.setListVisibility(false);
  }

  onFocus(event: any) {
    this.setListVisibility(true);
  }

  setListVisibility(isVisible: boolean) {
    this.isListVisible = isVisible;
  }

  clearTerms() {
    this.terms = [];
    this.selected = null;
  }

  selectTerm(term: Term) {
    this.selected = term;
    this.weightIncrease(term);
    this.filter = term.name;
  }
}
