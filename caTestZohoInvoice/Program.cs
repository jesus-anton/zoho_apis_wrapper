using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZohoInvoiceClient;
using DataClient;
using System.Data;
using System.Reflection;

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
                object idConnection = default(object);
                BaseRepo br = new BaseRepo();
                br.ProvFactory = MySql.Data.MySqlClient.MySqlClientFactory.Instance;
                br.ConnectionString = "data source=localhost; initial catalog=ctalaiz_zohoInvoice; user id=root; password=legendary";
                using (DataTable tblCon = br.Select("Connection"))
                {
                    if (tblCon.Rows.Count > 0)
                    {
                        idConnection = tblCon.Rows[0]["idConnection"];
                        client.ApiKey = tblCon.Rows[0]["ApiKey"].ToString();
                        client.AuthToken = tblCon.Rows[0]["AuthToken"].ToString();
                        client.Organization = tblCon.Rows[0]["CompanyName"].ToString();

                        Console.WriteLine("Usando conexión {0}", tblCon.Rows[0]["Name"]);
                    }
                }
                if (idConnection == null)
                {
                    Console.WriteLine("No se han encontrado conexiones");
                    System.Environment.Exit(-1);
                }
                Console.WriteLine("Recuperando items...");
                using (DataTable tblItems = br.Structure("Item"))
                {
                    client.ReadItems().ForEach((i) => InsertRow(tblItems, i, idConnection));
                    br.Update(tblItems);
                    Console.WriteLine("Grabados {0} items en la base de datos.", tblItems.Rows.Count);
                }
                Console.WriteLine("Recuperando customers...");
                var lstCust = client.ReadCustomersDetails();
                using (DataTable tblCust = br.Structure("Customer"))
                {
                    lstCust.ForEach((c) => InsertRow(tblCust, c, idConnection));
                    br.Update(tblCust);
                    Console.WriteLine("Grabados {0} customers en la base de datos.", tblCust.Rows.Count);
                }
                using (DataTable tblCust = br.Select("Customer"))
                {
                    using (DataTable tblCustContacts = br.Structure("CustomerContacts"))
                    {
                        lstCust.ForEach((c) =>
                        {
                            c.Contacts.ForEach((cc) => InsertRow("idCustomer", tblCust.Select(string.Format("CustomerID='{0}'", c.CustomerID))[0]["IDCustomer"], tblCustContacts, cc, idConnection));
                        });
                        br.Update(tblCustContacts);
                        Console.WriteLine("Grabados {0} customer contacts en la base de datos.", tblCustContacts.Rows.Count);
                    }
                    using (DataTable tblCustCustFields = br.Structure("CustomerCustomFields"))
                    {
                        lstCust.ForEach((c) =>
                        {
                            foreach (string k in c.CustomFields.Keys)
                            {
                                DataRow dr = tblCustCustFields.NewRow();
                                dr["idCustomer"] = tblCust.Select(string.Format("CustomerID='{0}'", c.CustomerID))[0]["idCustomer"];
                                dr["Key"] = k;
                                dr["Value"] = c.CustomFields[k];
                                tblCustCustFields.Rows.Add(dr);
                            }
                        });
                        br.Update(tblCustCustFields);
                        Console.WriteLine("Grabados {0} customer custom fields en la base de datos.", tblCustCustFields.Rows.Count);
                    }
                }
                Console.WriteLine("Recuperando invoices...");
                var lstInv = client.ReadInvoiceDetails();
                using (DataTable tblInv = br.Structure("Invoice"))
                {
                    using (DataTable tblCust = br.Select("Customer"))
                    {
                        lstInv.ForEach((c) =>
                        {
                            DataRow dr = CreateAndFillRow(tblInv, c);
                            dr["idCustomer"] = tblCust.Select(string.Format("CustomerID = '{0}'", c.CustomerID))[0]["idCustomer"];
                            tblInv.Rows.Add(dr);
                        });
                    }
                    br.Update(tblInv);
                    Console.WriteLine("Grabados {0} invoices en la base de datos.", tblInv.Rows.Count);
                }
                using (DataTable tblInv = br.Select("Invoice"))
                using (DataTable tblInvItem = br.Structure("InvoiceItems"))
                using (DataTable tblItem = br.Select("Item"))
                {
                    lstInv.ForEach((c) =>
                    {
                        c.InvoiceItems.ForEach((i) =>
                        {
                            DataRow dr = CreateAndFillRow(tblInvItem, i);
                            dr["idInvoice"] = tblInv.Select(string.Format("InvoiceID = '{0}'", c.InvoiceID))[0]["idInvoice"];
                            DataRow[] drItems = tblItem.Select(string.Format("ItemID = '{0}'", i.ItemID));
                            if (drItems.Length == 0)
                            {
                                drItems = tblItem.Select(string.Format("Name = '{0}'", i.ItemName));
                            }
                            if (drItems.Length == 0)
                            {
                                DataRow drItem = tblItem.NewRow();
                                drItem["ItemID"] = i.ItemID;
                                drItem["idConnection"] = idConnection;
                                drItem["Name"] = i.ItemName;
                                drItem["Description"] = i.ItemDescription;
                                drItem["Rate"] = 0;
                                drItem["Tax1Name"] = i.Tax1Name;
                                drItem["Tax1Percentage"] = i.Tax1Percentage;
                                drItem["Tax2Name"] = i.Tax2Name;
                                drItem["Tax2Percentage"] = i.Tax2Percentage;
                                tblItem.Rows.Add(drItem);
                                br.Update(tblItem);
                                drItems = tblItem.Select(string.Format("ItemID = '{0}'", i.ItemID));
                            }
                            dr["idItem"] = drItems[0]["idItem"];
                            tblInvItem.Rows.Add(dr);
                        });
                    });
                    br.Update(tblInvItem);
                    Console.WriteLine("Grabados {0} invoice items en la base de datos.", tblInvItem.Rows.Count);
                }

                //client.ReadCustomers().ForEach((c) => Console.WriteLine("ID: {0} - Name: {1}.", c.CustomerID, c.Name));
                //client.ReadItems().ForEach((i) => Console.WriteLine("ID: {0} - Name: {1}.", i.ItemID, i.Name));
                //client.ReadInvoices().ForEach((i) => Console.WriteLine("ID: {0} - Customer: {1} - Total; {2}", i.InvoiceID, i.CustomerName, i.Total));
                //client.ReadInvoiceDetails().ForEach((i) => Console.WriteLine("ID: {0} - Customer: {1} - Total; {2} - Nº of Items: {3}", i.InvoiceID, i.CustomerName, i.Total, i.InvoiceItems.Count));
                //client.ReadCustomersDetails().ForEach((i) => { Console.WriteLine("ID: {0} - CIF: {1} - Name; {2}", i.CustomerID, i.CustomFields.ContainsKey("CIF:") ?  i.CustomFields["CIF:"] : "SIN CIF", i.Name); });
                //Console.WriteLine("{0}", client.ReadItems());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            Console.ReadKey();
        }

        private static void InsertRow<T>(string parentColumn, object parentValue, DataTable tbl, T i, object idConnection)
        {
            DataRow dr = CreateAndFillRow<T>(tbl, i);
            dr[parentColumn] = parentValue;
            if (tbl.Columns.IndexOf("idConnection") >= 0)
            {
                dr["idConnection"] = idConnection;
            }
            tbl.Rows.Add(dr);
        }

        private static DataRow CreateAndFillRow<T>(DataTable tbl, T i)
        {
            Type t = i.GetType();
            PropertyInfo pi = null;
            DataRow dr = tbl.NewRow();
            foreach (DataColumn dc in dr.Table.Columns)
            {
                pi = t.GetProperty(dc.ColumnName);
                if (pi != null)
                {
                    dr[dc] = pi.GetValue(i, null);
                }
            }
            return dr;
        }

        private static void InsertRow<T>(DataTable tbl, T i, object idConnection)
        {
            DataRow dr = CreateAndFillRow(tbl, i);
            if (tbl.Columns.IndexOf("idConnection") >= 0)
            {
                dr["idConnection"] = idConnection;
            }
            tbl.Rows.Add(dr);
        }
    }
}
