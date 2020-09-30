import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MvProduct } from './product';
import { ProductFormComponent } from './product-form/product-form.component';
import { ProductService } from './product.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { SelectionModel } from '@angular/cdk/collections';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss']
})
export class ProductComponent implements OnInit {

  errorMessage: string = null;
  displayedColumns: string[];
  dataSource: MvProduct[] = [];
  selectedProduct: MvProduct = <MvProduct>{};
  selection = new SelectionModel<MvProduct>(false, []);
  
  constructor( 
                private productService: ProductService,
                private dialog: MatDialog,
                private snacbar: MatSnackBar) { 
  }

  ngOnInit() {
    this.displayedColumns = ['ProductId', 'ProductName','ProductCategory','Stock', 'Amount'];
    this.getAllProducts();
  }

  getAllProducts(){
    this.productService.getProduct().subscribe((response: any) =>{
      if(response && response.data){
        this.dataSource = response.data;
      }else{
        this.errorMessage = "No Data Available"
      }
    })
  }
  productAdd(){
    this.selection.clear();
    this.selectedProduct = <MvProduct>{};
    this.openDialog('Add');
  }
  productEdit(){
    this.openDialog('Edit');
  }

  //creating dialog box for add and edit
  openDialog(action: string){
      if(action === 'Edit' && !this.selection.hasValue()){
        this.openSnackBar('Row has not been selected',"");
        return;
      }
      const dialogRef = this.dialog.open(ProductFormComponent,{
        
        data:{
          action:action,
          data: this.selectedProduct
        }
        
        
      });
      
      dialogRef.afterClosed().subscribe((requestedRow) =>{
        if(requestedRow){
          this.selectedProduct = requestedRow;
          console.log("requested row", requestedRow);
          
          if(action === 'Edit'){
              this.productService.updateProduct(requestedRow).subscribe((updated)=>{
                if(updated){
                  console.log("updatedresult", updated);
                  
                  console.log("this.select",this.selectedProduct);
                  
                  this.getAllProducts();
                  this.openSnackBar('Product Successfully Updated',"");
                }
                else{
                  this.getAllProducts();
                  this.openSnackBar("Product couldn't be updated","")
                }
              })
          }else{
            this.productService.addProduct(requestedRow).subscribe(added =>{
              this.getAllProducts();
              this.openSnackBar('Product Successfully Addedd ','');
            })
          }
        }
      })
  }
  rowClick(e: any, row: MvProduct){
    this.selectedProduct = {...row};
    this.selection.toggle(row);
    console.log("selected row",row);
    console.log("selected row", this.selectedProduct);
  }
 
  
  
  
    openSnackBar(message,action){
      this.snacbar.open(message, action,{
        duration:3000,
        panelClass: ['login-snackbar'],
        verticalPosition: 'top',
        horizontalPosition: 'right',
      })
    }
}

