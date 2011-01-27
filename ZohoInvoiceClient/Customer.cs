using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ZohoInvoiceClient
{
    public class Customer
    {
        public virtual string CustomerID { get; protected set; }
        public virtual string Name { get; set; }
        public virtual int PaymentsDue { get; set; }
        public virtual string CurrencyCode { get; set; }
        public virtual decimal AvailableCredit { get; set; }
        public virtual decimal OutstandingBalance { get; set; }

        public Customer() { }
        public Customer(string customerID)
        {
            CustomerID = customerID;
        }

        internal static Customer ParseCustomer(XElement customer)
        {
            Customer cust;

            XElement e = customer.Element("CustomerID");
            cust = new Customer(e.Value);
            e = customer.Element("Name");
            cust.Name = e.Value;
            e = customer.Element("PaymentsDue");
            cust.PaymentsDue = Utils.ParseInt(e.Value);
            e = customer.Element("CurrencyCode");
            cust.CurrencyCode = e.Value;
            e = customer.Element("AvailableCredit");
            cust.AvailableCredit = Utils.ParseDecimal(e.Value);
            e = customer.Element("OutstandingBalance");
            cust.OutstandingBalance = Utils.ParseDecimal(e.Value);

            return cust;
        }
    }
}
