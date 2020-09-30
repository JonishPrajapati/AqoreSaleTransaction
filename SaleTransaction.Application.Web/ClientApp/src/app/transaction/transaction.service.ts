import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiServiceService } from '../../core/services/ApiService.service';


@Injectable({
  providedIn: 'root'
})
export class TransactionService {

constructor(private api :ApiServiceService) { }
addTransaction(json: any): Observable<any> {
  return this.api.post('transaction/transactionAdd', json);
 }
getTransaction(){
  return this.api.get('transaction/transactionAll');
}
updateTransaction(json: any):Observable<any>{
  return this.api.post('transaction/transactionUpdate', json)
}

}
