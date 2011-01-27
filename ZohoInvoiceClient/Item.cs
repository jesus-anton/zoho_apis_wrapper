using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ZohoInvoiceClient
{
    public class Item
    {
        public virtual string ItemID { get; protected set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual decimal Rate { get; set; }
        public virtual string Tax1Name { get; set; }
        public virtual decimal Tax1Percentage { get; set; }
        public virtual string Tax2Name { get; set; }
        public virtual decimal Tax2Percentage { get; set; }

        public Item() { }

        public Item(string itemId)
        {
            ItemID = itemId;
        }

        internal static Item ParseItem(XElement item)
        {
            Item ret = new Item(item.Element("ItemID").Value);

            ret.Name = item.Element("Name").Value;
            ret.Description = item.Element("Description").Value;
            ret.Rate = Utils.ParseDecimal(item.Element("Rate").Value);
            ret.Tax1Name = item.Element("Tax1Name").Value;
            ret.Tax1Percentage = Utils.ParseDecimal(item.Element("Tax1Percentage").Value);
            ret.Tax2Name = item.Element("Tax2Name").Value;
            ret.Tax2Percentage = Utils.ParseDecimal(item.Element("Tax2Percentage").Value);

            return ret;
        }
    }
}
