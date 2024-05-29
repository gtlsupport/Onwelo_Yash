import { Component, OnInit } from '@angular/core';
import { VoterModel } from '../../../models/voter-model';
import { SharedServiceService } from '../../../services/shared-service.service';

@Component({
  selector: 'app-addvoter',
  templateUrl: './addvoter.component.html',
  styleUrl: './addvoter.component.css'
})
export class AddvoterComponent implements OnInit {

  voterModel : VoterModel = new VoterModel();
  voterList : VoterModel[] = [] ;
  message : string = '';
 
  constructor(private sharedService : SharedServiceService) {} 

  ngOnInit(): void {
    this.GetAllVoterList();
    this.message = '';
  }

  private GetAllVoterList() : void
  {
    this.sharedService.GetVoterList().subscribe({
      next: (res) => {
        this.voterList = res.map((item:VoterModel) => {return item});   
      },
      error: (err) => {
        console.error('Error fetching candidates:', err);
      }
    });
  }

  HandleInsertVoter() : void
  {
     if(this.voterModel.VoterName !== '')
     {
      this.message = '';
     
      this.sharedService.InsertVoter(this.voterModel).subscribe({
        next:(res) =>
        {
          this.voterList = res.map((item:VoterModel) => {return item});
          alert('Voter added');
          this.voterModel.VoterName = '';
        },
        error:(err) =>
        {
          console.log(err)
        }
      })
     }
     else
     {
      this.message = 'Voter name is required';
     }
  }
}
