import { Component, OnInit } from '@angular/core';
import { CandidateModel } from '../../../models/candidate-model';
import { SharedServiceService } from '../../../services/shared-service.service';
import { Observable, Subscription } from 'rxjs';
import { error } from 'console';

@Component({
  selector: 'app-addcandidate',
  templateUrl: './addcandidate.component.html',
  styleUrl: './addcandidate.component.css'
})
export class AddcandidateComponent implements OnInit {
  

  constructor(private sharedService : SharedServiceService) {
    this.message = '';
  }

  candidateModel : CandidateModel = new CandidateModel();
  candidateList : CandidateModel[] = [] ;
  message : string = '';
  

  ngOnInit(): void {
    this.GetCandidateList();
  }

  GetCandidateList(): void {
    this.sharedService.GetCandidatesList().subscribe({
      next: (res) => {
        this.candidateList = res.map((item:CandidateModel) => {return item});
       
      },
      error: (err) => {
        console.error('Error fetching candidates:', err);
      }
    });
  }
  
  SubmitCandidate() : void
  {
    if(this.candidateModel.CandidateName !== '')
    {
      this.message = '';
      this.sharedService.InsertCandidate(this.candidateModel).subscribe({
        next:(res)=>
        {
          if(res)
          {
            this.candidateList = res.map((item:CandidateModel) => {return item});
            alert('Candidate added');
          }
          this.candidateModel.CandidateName = '';
        },
        error:(err)=>
        {
          alert(err);
        }
      })
     
    }
    else
    {
        this.message = 'Candidate name is required';
    }
  }
}
