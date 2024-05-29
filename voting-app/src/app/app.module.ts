import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AddcandidateComponent } from './components/candidate/addcandidate/addcandidate.component';
import { GetallcandidatesComponent } from './components/candidate/getallcandidates/getallcandidates.component';
import { AddvoterComponent } from './components/voter/addvoter/addvoter.component';
import { GetallvotersComponent } from './components/voter/getallvoters/getallvoters.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { VotemachineComponent } from './components/voting/votemachine/votemachine.component';
import { Custompipe } from './custompipe';

@NgModule({
  declarations: [
    AppComponent,
    AddcandidateComponent,
    GetallcandidatesComponent,
    AddvoterComponent,
    GetallvotersComponent,
    VotemachineComponent,
    Custompipe
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [
    provideClientHydration(),
    provideAnimationsAsync()
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
