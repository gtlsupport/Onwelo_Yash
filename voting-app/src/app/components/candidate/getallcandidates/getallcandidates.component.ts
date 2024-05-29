import { Component, DoCheck, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { CandidateModel, TransformedCandidateModel } from '../../../models/candidate-model';

@Component({
  selector: 'app-getallcandidates',
  templateUrl: './getallcandidates.component.html',
  styleUrl: './getallcandidates.component.css'
})
export class GetallcandidatesComponent implements  OnChanges, OnInit {
  
 
  @Input() candidates: CandidateModel[] = [];
  candidateList : TransformedCandidateModel[] = [];
  

  ngOnInit(): void {
   
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes['candidates']) {
      
      this.candidateList = changes['candidates'].currentValue;
    
    }
  }

}
