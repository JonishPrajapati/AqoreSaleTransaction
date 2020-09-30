export interface MvInvoice {
    InvoiceId: number,
    InvoiceDate: Date,
    FirstName: string,
    LastName: string,
    InvoiceAmount: number,
    InvoiceDiscount: number
}

export interface MvInvoiceDetail {
    TransactId: number;
    ProductName: string;
    Quantity: number;
    Rate: number;
    TotalAmount: number;
}