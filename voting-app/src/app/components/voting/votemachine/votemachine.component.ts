import { Component, OnInit } from '@angular/core';
import { TransformedCandidateModel } from '../../../models/candidate-model';
import { TransformedVoterModel } from '../../../models/voter-model';
import { SharedServiceService } from '../../../services/shared-service.service';

class VotingMachine
{
  CandidateList : TransformedCandidateModel[] = [];
  voterList : TransformedVoterModel[] = [];
}

export class CastVote{
  voterId : number = 0;
  candidateId : number = 0;
}

@Component({
  selector: 'app-votemachine',
  templateUrl: './votemachine.component.html',
  styleUrl: './votemachine.component.css'
})
export class VotemachineComponent implements OnInit {
  

  votingMachine : VotingMachine = new VotingMachine();
  selectedVoterValue: number = 0;
  selectedCandidateValue: number = 0;
  castVote : CastVote = new CastVote();
  message : string = '';
  constructor(private sharedService : SharedServiceService) {}

  ngOnInit(): void {
    this.FetchDropdownData();
  }
  private FetchDropdownData(): void {
    this.sharedService.GetVoteMachineData().subscribe(data => {
    
     this.votingMachine.voterList = data.voterList;
     this.votingMachine.CandidateList = data.candidateList;

    });
  }


  HandleSubmit() : void
  {
    if(this.selectedVoterValue > 0 && this.selectedCandidateValue > 0)
    {
      this.message = '';
     this.castVote.candidateId = this.selectedCandidateValue;
     this.castVote.voterId = this.selectedVoterValue; 
     this.sharedService.CastVote(this.castVote).subscribe(data => alert(data.message));

     this.selectedVoterValue = 0;
     this.selectedCandidateValue = 0;
     this.FetchDropdownData();
    }
    else
    {
      this.message = 'Select your name and candidate name to cast vote';
    }
   
  }

}
