import { HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiServiceService } from '../../core/services/ApiService.service';


@Injectable({
  providedIn: 'root'
})
export class InvoiceService {

  constructor(private api: ApiServiceService) { }

  generateInvoice(json:any): Observable<any> {
    console.log("json",json);
    
    return this.api.post('Invoice/generateInvoice',json);
  }
  getInvoice(){
    return this.api.get('invoice/getInvoiceDetail');
  }
  getInvoiceDetails(InvoiceId): Observable<any>{
    return this.api.get('invoice/generateSingleInvoice', JSON.stringify({InvoiceId : InvoiceId}));
  }
}
