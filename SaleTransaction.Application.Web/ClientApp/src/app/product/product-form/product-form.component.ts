import { Component, OnInit, Inject, AfterViewInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MvProduct } from './../product'
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ProductService } from '../product.service';

@Component({
  selector: 'app-product-form',
  templateUrl: './product-form.component.html',
  styleUrls: ['./product-form.component.scss']
})
export class ProductFormComponent implements OnInit, AfterViewInit {

  productForm: FormGroup;
  errorMessage: null;
  errorMessageType : any = {
    invalidForm: 'Data Must be required'
  }
  
  action:string;
  selectedProduct: MvProduct = <MvProduct>{};

  constructor(@Inject(MAT_DIALOG_DATA) public data :any,
              private fb: FormBuilder,
              private productService: ProductService,
              public dialogRef: MatDialogRef<ProductFormComponent>) { 

                dialogRef.disableClose = true
                this.action = data.action;
                this.selectedProduct = data.data || {};
                
              }

  ngOnInit() {
      this.productForm = this.fb.group({
        ProductName: ['',Validators.required],
        ProductCategory: ['',Validators.required],
        Stock: ['',Validators.required],
        Amount: ['',Validators.required]
      })
  }


  onClose(){
    this.dialogRef.close();
  }
  onSubmit(){
    this.dialogRef.close(this.selectedProduct);
    console.log(this.selectedProduct);
    
  }
  ngAfterViewInit(): void {
    this.productForm.updateValueAndValidity();
  }

}
