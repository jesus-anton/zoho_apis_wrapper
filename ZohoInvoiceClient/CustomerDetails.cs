using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ZohoInvoiceClient
{
    public class CustomerDetails : Customer
    {
        public class CustomerContact
        {
            public virtual string ContactID { get; protected set; }
            public virtual string Salutation { get; set; }
            public virtual string FirstName { get; set; }
            public virtual string LastName { get; set; }
            public virtual string EMail { get; set; }
            public virtual string Phone { get; set; }
            public virtual string Mobile { get; set; }

            public CustomerContact() { }
            public CustomerContact(string contactID)
            {
                ContactID = contactID;
            }

            internal static CustomerContact ParseContact(XElement contact)
            {
                CustomerContact ret = new CustomerContact();

                ret.ContactID = contact.Element("ContactID").Value;
                ret.Salutation = contact.Element("Salutation").Value;
                ret.FirstName = contact.Element("FirstName").Value;
                ret.LastName = contact.Element("LastName").Value;
                ret.EMail = contact.Element("EMail").Value;
                ret.Phone = contact.Element("Phone").Value;
                ret.Mobile = contact.Element("Mobile").Value;

                return ret;
            }
        }

        public virtual string BillingAddress { get; set; }
        public virtual string BillingCity { get; set; }
        public virtual string BillingState { get; set; }
        public virtual string BillingZip { get; set; }
        public virtual string BillingCountry { get; set; }
        public virtual string BillingFax { get; set; }

        public virtual string ShippingAddress { get; set; }
        public virtual string ShippingCity { get; set; }
        public virtual string ShippingState { get; set; }
        public virtual string ShippingZip { get; set; }
        public virtual string ShippingCountry { get; set; }
        public virtual string ShippingFax { get; set; }

        public virtual List<CustomerContact> Contacts { get; protected set; }
        public virtual Dictionary<string, string> CustomFields { get; protected set; }

        public virtual string Notes { get; set; }

        protected virtual void InitCustomerDetails()
        {
            Contacts = new List<CustomerContact>();
            CustomFields = new Dictionary<string, string>();
        }

        public CustomerDetails()
            : base()
        {
            InitCustomerDetails();
        }

        public CustomerDetails(string customerID)
            : base(customerID)
        {
            InitCustomerDetails();
        }


        internal static CustomerDetails ParseCustomerDetail(XElement customer)
        {
            CustomerDetails ret = new CustomerDetails();

            Customer.ParseCustomer(customer, ret);

            ret.BillingAddress = customer.Element("BillingAddress").Value;
            ret.BillingCity = customer.Element("BillingCity").Value;
            ret.BillingState = customer.Element("BillingState").Value;
            ret.BillingZip = customer.Element("BillingZip").Value;
            ret.BillingCountry = customer.Element("BillingCountry").Value;
            ret.BillingFax = customer.Element("BillingFax").Value;

            ret.ShippingAddress = customer.Element("ShippingAddress").Value;
            ret.ShippingCity = customer.Element("ShippingCity").Value;
            ret.ShippingState = customer.Element("ShippingState").Value;
            ret.ShippingZip = customer.Element("ShippingZip").Value;
            ret.ShippingCountry = customer.Element("ShippingCountry").Value;
            ret.ShippingFax = customer.Element("ShippingFax").Value;

            foreach (XElement contact in customer.Element("Contacts").Elements())
            {
                CustomerContact cc = CustomerContact.ParseContact(contact);
                if (!string.IsNullOrEmpty(cc.ContactID))
                {
                    ret.Contacts.Add(cc);
                }
            }

            XElement customFields = customer.Element("CustomFields");
            if (!string.IsNullOrEmpty(customFields.Element("CustomFieldLabel1").Value))
            {
                ret.CustomFields.Add(customFields.Element("CustomFieldLabel1").Value, customFields.Element("CustomFieldValue1").Value);
            }

            if (!string.IsNullOrEmpty(customFields.Element("CustomFieldLabel2").Value))
            {
                ret.CustomFields.Add(customFields.Element("CustomFieldLabel2").Value, customFields.Element("CustomFieldValue2").Value);
            }

            if (!string.IsNullOrEmpty(customFields.Element("CustomFieldLabel3").Value))
            {
                ret.CustomFields.Add(customFields.Element("CustomFieldLabel3").Value, customFields.Element("CustomFieldValue3").Value);
            }

            return ret;
        }
    }
}
