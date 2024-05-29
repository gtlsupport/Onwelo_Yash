import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddcandidateComponent } from './components/candidate/addcandidate/addcandidate.component';
import { AddvoterComponent } from './components/voter/addvoter/addvoter.component';
import { VotemachineComponent } from './components/voting/votemachine/votemachine.component';

const routes: Routes = [
  {path:'candidate', component:AddcandidateComponent},
  {path:'voter', component:AddvoterComponent},
  {path:'voting',component:VotemachineComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
