using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ZohoInvoiceClient
{
    public class Payment
    {
        public virtual string PaymentID { get; protected set; }
        public virtual string InvoiceID { get; set; }
        public virtual string Mode { get; set; }
        public virtual string Description { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual decimal ExchangeRate { get; set; }
        public virtual decimal Amount { get; set; }

        public Payment() { }
        public Payment(string paymentID)
        {
            PaymentID = paymentID;
        }

        public static Payment ParsePayment(XElement payment)
        {
            Payment ret = new Payment(payment.Element("PaymentID").Value);

            ret.InvoiceID = payment.Element("InvoiceID").Value;
            ret.Mode = payment.Element("Mode").Value;
            ret.Description = payment.Element("Description").Value;
            ret.Date = Utils.ParseDate(payment.Element("Date").Value);
            ret.ExchangeRate = Utils.ParseDecimal(payment.Element("ExchangeRate").Value);
            ret.Amount = Utils.ParseDecimal(payment.Element("Amount").Value);

            return ret;
        }
    }
}
