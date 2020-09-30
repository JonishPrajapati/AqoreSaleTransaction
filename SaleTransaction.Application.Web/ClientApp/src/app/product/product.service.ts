import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiServiceService } from '../../core/services/ApiService.service';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private api: ApiServiceService) { }
  addProduct(json: any): Observable<any> {
  
    return this.api.post('product/productadd', json);
  }

  getProduct(){
    return this.api.get('product/productall');
  }
  updateProduct(json: any):Observable<any>{
    return this.api.post('product/productUpdate', json)
  }
}
