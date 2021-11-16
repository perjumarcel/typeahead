import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { HoverClassDirective } from 'src/directives/hover-class.directive';
import { HighlightPipe } from 'src/pipes/highlight.pipe';

@NgModule({
  declarations: [
    AppComponent,
    HoverClassDirective,
    HighlightPipe
  ],
  imports: [
    BrowserModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
