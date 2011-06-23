using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ZohoInvoiceClient
{
    public class InvoiceDetail : Invoice
    {
        public virtual string CreatedTime { get; set; }
        public virtual string LastModifiedTime { get; set; }
        public virtual string LastSyncTime { get; set; }
        public virtual string Source { get; set; }
        public virtual string ReferenceID { get; set; }
        public virtual int PaymentsDue { get; set; }
        public virtual decimal ExchangeRate { get; set; }
        public virtual string LFName { get; set; }
        public virtual decimal LateFeeAmount { get; set; }
        public virtual string Notes { get; set; }
        public virtual string Terms { get; set; }
        public virtual string CustomerID { get; set; }

        public virtual List<string> PaymentGateways { get; protected set; } // ASK - ¿De qué tipo?
        public virtual List<InvoiceItem> InvoiceItems { get; protected set; }
        public virtual List<Payment> Payments { get; protected set; }
        public virtual List<string> Comments { get; protected set; }

        internal void Init() { 
            InvoiceItems = new List<InvoiceItem>();
            Payments = new List<Payment>();
            PaymentGateways = new List<string>();
            Comments = new List<string>();
        }

        // ASK - ¿Comentarios? ¿Créditos? ¿Posts?

        public InvoiceDetail() { Init(); }
        public InvoiceDetail(string invoiceID)
            : base(invoiceID)
        {
            Init();
        }

        internal static InvoiceDetail ParseInvoiceDatail(XElement invoiceDetail)
        {
            InvoiceDetail ret = new InvoiceDetail();

            Invoice.ParseInvoice(invoiceDetail, ret);

            ret.CreatedTime = invoiceDetail.Element("CreatedTime").Value;
            ret.CustomerID = invoiceDetail.Element("CustomerID").Value;
            ret.LastModifiedTime = invoiceDetail.Element("LastModifiedTime").Value;
            ret.LastSyncTime = invoiceDetail.Element("LastSyncTime").Value;
            ret.Source = invoiceDetail.Element("Source").Value;
            ret.ReferenceID = invoiceDetail.Element("ReferenceID").Value;
            ret.PaymentsDue = Utils.ParseInt(invoiceDetail.Element("PaymentsDue").Value);
            ret.ExchangeRate = Utils.ParseDecimal(invoiceDetail.Element("ExchangeRate").Value);
            ret.LFName = invoiceDetail.Element("LFName").Value;
            ret.LateFeeAmount = Utils.ParseDecimal(invoiceDetail.Element("LateFeeAmount").Value);
            ret.Notes = invoiceDetail.Element("Notes").Value;
            ret.Terms = invoiceDetail.Element("Terms").Value;
            foreach (XElement pg in invoiceDetail.Element("PaymentGateways").Elements("PaymentGateway"))
            {
                ret.PaymentGateways.Add(pg.Element("PaymentGateway").Value);
            }
            foreach (XElement invItem in invoiceDetail.Element("InvoiceItems").Elements("InvoiceItem"))
            {
                ret.InvoiceItems.Add(InvoiceItem.ParseItem(invItem));
            }
            foreach (XElement payment in invoiceDetail.Element("Payments").Elements("Payment"))
            {
                ret.Payments.Add(Payment.ParsePayment(payment));
            }
            foreach (XElement comment in invoiceDetail.Element("Comments").Elements("Comment"))
            {
                ret.Comments.Add(comment.Element("Comment").Value);
            }

            return ret;
        }
    }
}
