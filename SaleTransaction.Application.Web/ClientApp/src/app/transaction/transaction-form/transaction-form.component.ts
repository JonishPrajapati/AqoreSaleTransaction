import { AfterViewInit, Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CustomerService } from 'src/app/customer/customer.service';
import { MvProduct } from 'src/app/product/product';
import { ProductService } from 'src/app/product/product.service';
import { TransactionService } from '../transaction.service';
import { MvTransaction } from './../transaction.model'

@Component({
  selector: 'app-transaction-form',
  templateUrl: './transaction-form.component.html',
  styleUrls: ['./transaction-form.component.scss']
})
export class TransactionFormComponent implements OnInit, OnInit, AfterViewInit {

  transactionForm: FormGroup;
  errorMessage: null;
  action: string;
  selectedTransaction: MvTransaction = <MvTransaction>{};
  products = [];
  customers = [];



  constructor(@Inject(MAT_DIALOG_DATA) public data: any,
    private fb: FormBuilder,
    private transactionService: TransactionService,
    private productService: ProductService,
    private customerService: CustomerService,
    public dialogRef: MatDialogRef<TransactionFormComponent>) {

    dialogRef.disableClose = true
    this.action = data.action;
    this.selectedTransaction = data.data || {};
  }
  ngAfterViewInit(): void {

    this.transactionForm.updateValueAndValidity();
  }

  ngOnInit() {
    this.transactionForm = this.fb.group({
      ProductId: [this.selectedTransaction.ProductId, Validators.required],
      Quantity: [this.selectedTransaction.Quantity, Validators.required],
      CustomerId: [this.selectedTransaction.CustomerId, Validators.required]
    });
    this.getProductObject();
    this.getCustomerObject();
    console.log(this.fb,"formbuulder");
    
  }

  getProductObject() {
    this.productService.getProduct().subscribe((res) => {
      if (res && res.data) {
        
        res.data.forEach(element => {
          console.log(element);
          
          if (element.ProductId) {
            this.products.push({
              productId: element.ProductId,
              productName: element.ProductName
            });
          }
          
        
        });
      }
    });
  }
  getCustomerObject() {
    this.customerService.getCustomer().subscribe((res) => {
      if (res && res.data) {
        res.data.forEach(element => {

          if (element.CustomerId) {
            this.customers.push({
              customerId: element.CustomerId,
              firstName: element.FirstName
            });
          }
          else {
            this.customers = res.data
          }

        });
      }
    })
  }
  onClose() {
    this.dialogRef.close();
  }
  onSubmit() {
    this.dialogRef.close(this.selectedTransaction);


  }

}
