using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Globalization;

namespace ZohoInvoiceClient
{
    public class Invoice
    {
        public virtual string InvoiceID { get; internal protected set; }
        public virtual string CustomerName { get; set; }
        public virtual int Status { get; set; }
        public virtual string InvoiceNumber { get; set; }
        public virtual string PONumber { get; set; }
        public virtual DateTime InvoiceDate { get; set; }
        public virtual DateTime DueDate { get; set; }
        public virtual int DueByDays { get; set; }
        public virtual string CurrencyCode { get; set; }
        public virtual decimal Total { get; set; }
        public virtual decimal Balance { get; set; }
        public virtual long CreatedTimeInMillis { get; set; }

        public Invoice() { }
        public Invoice(string invoiceID)
        {
            InvoiceID = invoiceID;
        }

        internal static Invoice ParseInvoice(XElement invoice)
        {
            Invoice ret = new Invoice();

            ParseInvoice(invoice, ret);

            return ret;
        }

        internal static void ParseInvoice(XElement invoice, Invoice ret)
        {
            ret.InvoiceID = invoice.Element("InvoiceID").Value;
            ret.CustomerName = invoice.Element("CustomerName").Value;
            ret.Status = Utils.ParseInt(invoice.Element("Status").Value);
            ret.InvoiceNumber = invoice.Element("InvoiceNumber").Value;
            ret.PONumber = invoice.Element("PONumber").Value;
            ret.InvoiceDate = Utils.ParseDate(invoice.Element("InvoiceDate").Value);
            ret.DueDate = Utils.ParseDate(invoice.Element("DueDate").Value);
            ret.DueByDays = Utils.ParseInt(invoice.Element("DueByDays").Value);
            ret.CurrencyCode = invoice.Element("CurrencyCode").Value;
            ret.Total = Utils.ParseDecimal(invoice.Element("Total").Value);
            ret.Balance = Utils.ParseDecimal(invoice.Element("Balance").Value);
            if (invoice.Element("CreatedTimeInMillis") != null)
            {
                ret.CreatedTimeInMillis = Utils.ParseLong(invoice.Element("CreatedTimeInMillis").Value);
            }
        }
    }
}
