using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZohoInvoiceClient;

namespace caTestZohoInvoice
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                InvoiceClient client = new InvoiceClient();
                client.ApiUrl = "https://invoice.zoho.com/api/";
                client.ApiKey = "YourAPIKey";
                client.AuthToken = "YourAuthToken";
                client.Organization = "YourOrYourPartnerOrganization";
                //client.ReadCustomers().ForEach((c) => Console.WriteLine("ID: {0} - Name: {1}.", c.CustomerID, c.Name));
                //client.ReadItems().ForEach((i) => Console.WriteLine("ID: {0} - Name: {1}.", i.ItemID, i.Name));
                //client.ReadInvoices().ForEach((i) => Console.WriteLine("ID: {0} - Customer: {1} - Total; {2}", i.InvoiceID, i.CustomerName, i.Total));
                client.ReadInvoiceDetails().ForEach((i) => Console.WriteLine("ID: {0} - Customer: {1} - Total; {2} - Nº of Items: {3}", i.InvoiceID, i.CustomerName, i.Total, i.InvoiceItems.Count));
                //Console.WriteLine("{0}", client.ReadItems());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }
    }
}
