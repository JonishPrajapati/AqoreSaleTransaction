import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MvCustomer } from './customer.model';
import { CustomerService } from './customer.service';
import { SelectionModel } from '@angular/cdk/collections';
import { CustomerFormComponent } from './customer-form/customer-form.component';


@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.scss']
})
export class CustomerComponent implements OnInit {
  errorMessage: string = null;
  displayedColumns: string[];
  dataSource: MvCustomer[] = [];
  selectedCustomer: MvCustomer = <MvCustomer>{};
  selection = new SelectionModel<MvCustomer>(false, []);

  constructor(private customerService: CustomerService,
                private dialog: MatDialog,
                private snacbar: MatSnackBar) { }

  ngOnInit() {
    this.displayedColumns = ['CustomerId', 'FirstName','MiddleName','LastName','Email','Gender'];
    this.getAllCustomer();
  }

  getAllCustomer(){
    this.customerService.getCustomer().subscribe((response: any) =>{
      if(response && response.data){
        this.dataSource = response.data;
        console.log(this.dataSource);
        
      }else{
        this.errorMessage = "No Data Available"
      }
    })
  }

  customerAdd(){
    this.selection.clear();
    this.selectedCustomer = <MvCustomer>{};
    this.openDialog('Add');
  }
  customerEdit(){
    this.openDialog('Edit');
  }

  openDialog(action: string) {
    if(action === 'Edit' && !this.selection.hasValue()){
      this.openSnackBar('Row has not been selected',"");
      return;
    }const dialogRef = this.dialog.open(CustomerFormComponent,{
      data:{
        action:action,
        data: this.selectedCustomer
      }
    });
    dialogRef.afterClosed().subscribe((requestedRow) =>{
      if(requestedRow){
        this.selectedCustomer = requestedRow;
        if(action === 'Edit'){
            this.customerService.updateCustomer(requestedRow).subscribe((updated)=>{
              console.log("editable data ", requestedRow);
              
              if(updated){
                console.log(this.selectedCustomer);
                
                this.getAllCustomer();
                this.openSnackBar('Customer Successfully Updated',"");
              }
              else{
                this.getAllCustomer();
                this.openSnackBar("Customer couldn't be updated","")
              }
            })
        }else{
          this.customerService.addcustomer(requestedRow).subscribe(added =>{
            this.getAllCustomer();
            this.openSnackBar('Customer Successfully Addedd ','');
          })
        }
      }
    })
  }
  openSnackBar(message,action){
    this.snacbar.open(message, action,{
      duration:3000,
      panelClass: ['login-snackbar'],
      verticalPosition: 'top',
      horizontalPosition: 'right',
    })
  }
  rowClick(e: any, row: MvCustomer){
    this.selectedCustomer = {...row};
    this.selection.toggle(row);
    console.log("cliecked bait", row);
    
  }
  


}

