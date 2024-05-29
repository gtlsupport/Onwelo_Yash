import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { TransformedVoterModel, VoterModel } from '../../../models/voter-model';

@Component({
  selector: 'app-getallvoters',
  templateUrl: './getallvoters.component.html',
  styleUrl: './getallvoters.component.css'
})
export class GetallvotersComponent implements OnChanges, OnInit {
 
  @Input() voters: VoterModel[] = [];
  voterList : TransformedVoterModel[] = [];

  ngOnInit(): void {
    
   }
 
   ngOnChanges(changes: SimpleChanges) {
     if (changes['voters']) {
       
       this.voterList = changes['voters'].currentValue;
     }
   }

}
