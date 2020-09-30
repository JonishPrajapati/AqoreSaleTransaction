import { SelectionModel } from '@angular/cdk/collections';
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MvTransaction } from './transaction.model';
import { TransactionService } from './transaction.service';
import { TransactionFormComponent } from './transaction-form/transaction-form.component'
import { InvoiceService } from '../invoice/invoice.service';
import { MvInvoice } from '../invoice/invoice.model';

@Component({
  selector: 'app-transaction',
  templateUrl: './transaction.component.html',
  styleUrls: ['./transaction.component.scss']
})
export class TransactionComponent implements OnInit {

  displayedColumns: string[];
  dataSource: MvTransaction[] = [];
  selectedTransaction: MvTransaction = <MvTransaction>{};
  selection = new SelectionModel<MvTransaction>(false, []);
  selectionBox = new SelectionModel<MvTransaction>(true, []);
  invoice = []
  constructor(private transactionService: TransactionService,
    private invoiceService: InvoiceService,
    private dialog: MatDialog,
    private snacbar: MatSnackBar) { }


  ngOnInit() {
    this.displayedColumns = ['select', 'TransactId', 'FirstName',
      'ProductName', 'Quantity', 'Rate', 'Discount',
      'TotalAmount', 'InvoiceId'];
    this.getAllTransaction();
  }
  /** Whether the number of selected elements matches the total number of rows. */
  isAllSelected() {
    const numSelected = this.selectionBox.selected.length;
    const numRows = this.dataSource.length;
    return numSelected === numRows;
  }


  /** Selects all rows if they are not all selected; otherwise clear selection. */
  masterToggle() {
    this.isAllSelected() ?
      this.selectionBox.clear() :
      this.dataSource.forEach(row => this.selectionBox.select(row));
  }


  getAllTransaction() {
    this.transactionService.getTransaction().subscribe((response) => {
      if (response && response.data) {
        this.dataSource = response.data;
      } else {
        this.openSnackBar("No Data Available here", '');
      }
    })
  }
  transactionAdd() {
    this.selection.clear();
    this.selectedTransaction = <MvTransaction>{};
    this.openDialog('Add');
  }
  transactionEdit() {
    this.openDialog('Edit');
  }

  generateInvoice() {
    if (!this.selectionBox.hasValue()) {
      this.openSnackBar("you haven't select any transaction to further proceed", "");
      return;
    } else {

      if (this.checkInvoiceId(this.selectionBox.selected)) {
        this.openSnackBar("Invoice already created", "");
        return;
      } else if (!this.checkCustomer(this.selectionBox.selected)) {
        this.openSnackBar("Customer must be same", "");
        return;
      }
      else {  

        this.invoiceService.generateInvoice(this.selectionBox.selected).subscribe(res => {
          this.openSnackBar("generated successfully", "");
          this.getAllTransaction();
        });
      }
    }

  }

  openDialog(action: string) {
    if (action === 'Edit' && !this.selection.hasValue()) {
      this.openSnackBar('Row has not been selected', "");
      return;
    }
    const dialogRef = this.dialog.open(TransactionFormComponent, {
      data: {
        action: action,
        data: this.selectedTransaction
      }
    });
  

    dialogRef.afterClosed().subscribe((requestedRow) => {
 

      if (requestedRow) {
        if (action === 'Edit') {
          this.transactionService.updateTransaction(requestedRow).subscribe((updated) => {
            this.openSnackBar('Transaction Successfully Updated', "");
            this.getAllTransaction();
          })

        } else {
          this.transactionService.addTransaction(requestedRow).subscribe(added => {
            this.openSnackBar('Transaction Successfully Addedd ', '');
            this.getAllTransaction();
          })
        }
      }
    })
  }

  checkInvoiceId(array): boolean {
    let value = false;
    array.forEach(checkRow => {
      if (checkRow.InvoiceId) {
        value = true;
        return;
      }
    });
    return value;
  }
  checkCustomer(array): boolean {
    const initialCustomer = array[0].CustomerId;
    return array.every(transaction => transaction.CustomerId === initialCustomer);
  }

  




  rowClick(e: any, row: MvTransaction) {
    this.selectedTransaction = { ...row };
    this.selection.toggle(row);
    this.selectionBox.toggle(row);
  }


  openSnackBar(message, action) {
    this.snacbar.open(message, action, {
      duration: 3000,
      panelClass: ['login-snackbar'],
      verticalPosition: 'top',
      horizontalPosition: 'right',
    })
  }



}




