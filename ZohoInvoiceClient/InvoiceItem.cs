using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ZohoInvoiceClient
{
    public class InvoiceItem 
    {
        //TODO - Añadir los campos 
        public virtual string ProductID { get; set; }
        public virtual string ItemID { get; protected set; }
        public virtual string ItemName { get; set; }
        public virtual string ItemDescription { get; set; }
        public virtual decimal Price { get; set; }
        public virtual decimal Quantity { get; set; }
        public virtual decimal Discount { get; set; }
        public virtual string Tax1Name { get; set; }
        public virtual string Tax1Type { get; set; }
        public virtual decimal Tax1Percentage { get; set; }
        public virtual string Tax2Name { get; set; }
        public virtual string Tax2Type { get; set; }
        public virtual decimal Tax2Percentage { get; set; }
        public virtual decimal ItemTotal { get; set; }

        public InvoiceItem() { }

        public InvoiceItem(string itemId)
        {
            ItemID = itemId;
        }

        internal static InvoiceItem ParseItem(XElement item)
        {
            InvoiceItem ret = null;

            if (item != null)
            {
                ret = new InvoiceItem(item.Element("ItemID").Value);

                ret.ProductID = item.Element("ProductID").Value;
                ret.ItemID = item.Element("ItemID").Value;
                ret.ItemName = item.Element("ItemName").Value;
                ret.ItemDescription = item.Element("ItemDescription").Value;
                ret.Price = Utils.ParseDecimal(item.Element("Price").Value);
                ret.Quantity = Utils.ParseDecimal(item.Element("Quantity").Value);
                ret.Discount = Utils.ParseDecimal(item.Element("Discount").Value);
                ret.Tax1Name = item.Element("Tax1Name").Value;
                ret.Tax1Type = item.Element("Tax1Type").Value;
                ret.Tax1Percentage = Utils.ParseDecimal(item.Element("Tax1Percentage").Value);
                ret.Tax2Name = item.Element("Tax2Name").Value;
                ret.Tax2Type = item.Element("Tax2Type").Value;
                ret.Tax2Percentage = Utils.ParseDecimal(item.Element("Tax1Percentage").Value);
                ret.ItemTotal = Utils.ParseDecimal (item.Element("ItemTotal").Value);
            }
            return ret;
        }
    }
}
