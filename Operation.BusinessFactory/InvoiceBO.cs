using System;
using System.Collections.Generic;
using Operation.Contract;
using Operation.DataFactory;

namespace Operation.BusinessFactory
{
    public class InvoiceBO
    {
        private InvoiceDAL invoiceDAL;
        public InvoiceBO()
        {
            invoiceDAL = new InvoiceDAL();
        }

        public List<Invoice> GetList()
        {
            return invoiceDAL.GetList();
        }

        public bool SaveInvoice(Invoice newItem)
        {

            return invoiceDAL.Save(newItem);
        }

        public bool DeleteInvoice(Invoice item)
        {
            return invoiceDAL.Delete(item);
        }

        public Invoice GetInvoice(Invoice item)
        {
            return (Invoice)invoiceDAL.GetItem<Invoice>(item);
        }
        
        public Invoice GetInvoiceByBookingNo(string bookingNo)
        {
            return (Invoice)invoiceDAL.GetInvoiceByBookingNo(bookingNo);
        }
    }
}
