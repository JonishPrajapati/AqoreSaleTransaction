import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiServiceService } from '../../core/services/ApiService.service';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

constructor(private api: ApiServiceService) { }
getLogin(json: any): Observable<any> {

  return this.api.post('account/userlogin', json);
}
}

