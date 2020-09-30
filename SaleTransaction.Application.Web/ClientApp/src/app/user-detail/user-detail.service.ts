import { HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiServiceService } from '../../core/services/ApiService.service'

@Injectable({
  providedIn: 'root'
})
export class UserDetailService {

constructor(private api: ApiServiceService) { }
getUserDetail(UserId): Observable<any>{
  return this.api.get('account/userdetail', JSON.stringify({UserId : UserId}));
}
getAllUser(){
    return this.api.get('account/alluser');
}
}
