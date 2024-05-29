import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CandidateModel } from '../models/candidate-model';
import { Observable } from 'rxjs';
import { VoterModel } from '../models/voter-model';
import { CastVote } from '../components/voting/votemachine/votemachine.component';

@Injectable({
  providedIn: 'root'
})
export class SharedServiceService {

  constructor(private httpClient : HttpClient) { }
  
 InsertCandidate(candidateModel : CandidateModel) : Observable<any>
 {
    return this.httpClient.post<any>('https://localhost:7081/api/Candidate/insert-candidate', candidateModel);
 }

 GetCandidatesList() : Observable<any>
 {
  return this.httpClient.get<any>('https://localhost:7081/api/Candidate/candidates-list');
 }

 InsertVoter(voterModel : VoterModel) : Observable<any>
 {
  return this.httpClient.post<any>('https://localhost:7081/api/Voter/insert-voter', voterModel);
 }

 GetVoterList() : Observable<any>
 {
  return this.httpClient.get<any>('https://localhost:7081/api/Voter/all-voters');
 }


 GetVoteMachineData() : Observable<any>
 {
  return this.httpClient.get<any>('https://localhost:7081/api/Voting/voting-machine-date');
 }
 
 CastVote(castVote : CastVote)
 {
  return this.httpClient.post<any>('https://localhost:7081/api/Voting/caste-vote',castVote);
 }
  
}
