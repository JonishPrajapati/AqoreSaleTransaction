  import { SelectionModel } from '@angular/cdk/collections';
  import { Component, OnInit } from '@angular/core';
  import { MatDialog } from '@angular/material/dialog';
  import { MatSnackBar } from '@angular/material/snack-bar';
  import { InvoiceFormComponent } from './invoice-form/invoice-form.component';
  import { MvInvoice, MvInvoiceDetail } from './invoice.model';
  import { InvoiceService } from './invoice.service';

  @Component({
    selector: 'app-invoice',
    templateUrl: './invoice.component.html',
    styleUrls: ['./invoice.component.scss']
  })
  export class InvoiceComponent implements OnInit {
    errorMessage: string = null;
    displayedColumns: string[];
    dataSource: MvInvoice[] = [];
    selectedInvoice: MvInvoice = <MvInvoice>{};
    selectedInvoiceDetail: MvInvoiceDetail[]=[];
    selection = new SelectionModel<MvInvoice>(false, []);
    
    constructor(private invoiceService: InvoiceService,
      private dialog: MatDialog,
      private snacbar: MatSnackBar) { }

    ngOnInit() {
      this.displayedColumns = ['InvoiceId','InvoiceDate', 'FirstName','LastName','InvoiceAmount','InvoiceDiscount'];
      this.getInvoices();
      
    }
    getInvoices(){
      this.invoiceService.getInvoice().subscribe((response: any) =>{
        
        if(response && response[0].details){
          this.dataSource = response[0].details;
          console.log(this.dataSource);
          
        }else{
        this.openSnackBar("no data","");
        }
      })
    }

    getInvoiceDetail(){
      this.openDialog();
    }

    openDialog(){
      if(this.selection.hasValue())
      {
        this.invoiceService.getInvoiceDetails(this.selectedInvoice.InvoiceId).subscribe(response =>{
          console.log(response.Details,"ths.selected");
          console.log(this.selectedInvoice,"response");
          
          this.selectedInvoiceDetail=response.Details
          console.log("hello world",response);
            const dialogRef = this.dialog.open(InvoiceFormComponent,{
              
              
          data:{
                invoice: this.selectedInvoice,
                invoiceDetail: this.selectedInvoiceDetail
          }
        });
        dialogRef.afterClosed().subscribe(action =>{
          if(action === 'print'){
            console.log(action);
            
            this.openSnackBar("Invoice has been printed Successfully","");
          }else if(action === 'close'){
            this.openSnackBar("Proceed Canceled","");
          }
        })
          
     
          // console.log(response,"message details");
          
          
          // if(response && response.data){
          //   this.selectedInvoice = response.data;
          //   console.log(this.selectedInvoice,"fgnjdgjfdgdf");
            
          // }
        })
      
           
      }else{
        this.openSnackBar("Please select a row to generate Invoice","");
      }
    }

    openSnackBar(message,action){
      this.snacbar.open(message, action,{
        duration:3000,
        panelClass: ['login-snackbar'],
        verticalPosition: 'top',
        horizontalPosition: 'right',
      })
    }
    rowClick(e: any, row: MvInvoice){
      this.selectedInvoice = {...row};
      this.selection.toggle(row);
      console.log("cliecked bait", row);
      
    }

  }
