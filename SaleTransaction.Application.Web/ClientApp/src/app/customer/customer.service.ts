import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiServiceService } from '../../core/services/ApiService.service';


@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  constructor(private api: ApiServiceService) { }
  addcustomer(json: any): Observable<any> {
  
    return this.api.post('customer/customerAdd', json);
  }

  getCustomer(){
    return this.api.get('customer/getdetail');
  }
  updateCustomer(json: any):Observable<any>{
    return this.api.post('customer/customerUpdate', json)
  }

}
