using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Operation.Contract;

namespace Operation.Contract
{
	public class Invoice:IContract
	{
		public Invoice() { }


		public string InvoiceNo { get; set; }


		public string TripID { get; set; }


		public DateTime  InvoiceDate { get; set; }


		public decimal  TripAmount { get; set; }


		public decimal  TaxAmount { get; set; }


		public decimal  TotalAmount { get; set; }


		public Int16  PaymentType { get; set; }


		public decimal  PaidAmount { get; set; }


		public DateTime  CreatedOn { get; set; }


		public bool  IsMailSent { get; set; }

        public string BookingNo { get; set; }


	}
}




