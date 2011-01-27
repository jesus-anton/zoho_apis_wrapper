using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ZohoInvoiceClient
{
    public class InvoiceClient: ClientBase
    {
        public InvoiceClient(string organization): base(organization) {
        
        }

        public virtual List<Invoice> ReadInvoices()
        {
            List<Invoice> ret = new List<Invoice>();
            Read("invoices", "",
            (page, perPage, total, totalPages, el) =>
            {
                XElement customers = el.Element("Invoices");
                ret.AddRange(from c in customers.Elements() select Invoice.ParseInvoice(c));
            });
            return ret;
        }

        public virtual List<Customer> ReadCustomers()
        {
            List<Customer> ret = new List<Customer>();
            Read("customers", "",
            (page, perPage, total, totalPages, el) =>
            {
                XElement customers = el.Element("Customers");
                ret.AddRange(from c in customers.Elements() select Customer.ParseCustomer(c));
            });
            return ret;
        }

        public virtual List<Item> ReadItems()
        {
            List<Item> ret = new List<Item>();
            Read("items", "",
            (page, perPage, total, totalPages, el) =>
            {
                XElement items = el.Element("Items");
                ret.AddRange(from c in items.Elements() select Item.ParseItem(c));
            });
            return ret;
        }

        public virtual List<InvoiceDetail> ReadInvoiceDetails()
        {
            List<InvoiceDetail> ret = new List<InvoiceDetail>();
            ReadInvoices().ForEach((inv) => { ret.Add(ReadInvoiceDetail(inv.InvoiceID)); });
            return ret;
        }

        public virtual InvoiceDetail ReadInvoiceDetail(string invoiceID)
        {
            InvoiceDetail ret = null;
            Read("invoices", invoiceID,
            (page, perPage, total, totalPages, el) =>
            {
                ret = InvoiceDetail.ParseInvoiceDatail(el.Element("Invoice"));
            });
            return ret;
        }
    }
}
