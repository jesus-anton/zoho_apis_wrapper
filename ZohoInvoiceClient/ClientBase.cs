using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Net;
using System.IO;
using System.Net.Cache;
using System.Globalization;
using System.Xml.Linq;

namespace ZohoInvoiceClient
{
    public class ClientBase
    {
        public delegate void Action<T1, T2, T3, T4, T5>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5);

        public ClientBase(string organization)
        {
            ApiUrl = Properties.Settings.Default.API_URL;
            AuthToken = Properties.Settings.Default.AUTHTOKEN;
            ApiKey = Properties.Settings.Default.API_KEY;
            Organization = organization;
        }

        protected virtual string ApiUrl { get; set; }
        protected virtual string AuthToken { get; set; }
        protected virtual string ApiKey { get; set; }
        protected virtual string Organization { get; set; }

        protected virtual String ConstructURL(String url, String additionalParams)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(ApiUrl);
            sb.Append(url);
            sb.Append("?authtoken=");
            sb.Append(AuthToken);
            sb.Append("&scope=invoiceapi");
            sb.Append("&apikey=");
            sb.Append(ApiKey);

            if (!string.IsNullOrEmpty(Organization))
            {
                //CompanyName is mandatory if you belong to more than one organization
                sb.Append("&CompanyName=");
                sb.Append(HttpUtility.UrlEncode(Organization, Encoding.UTF8)); // URLEncoder.encode("Surya Test", "UTF-8"));
            }

            sb.Append(additionalParams);
            //System.out.println(sb.toString());
            return sb.ToString();
        }

        protected virtual String EncodeAndPrependParam(String param, String str)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(param);
            sb.Append(HttpUtility.UrlEncode(str, Encoding.UTF8));
            return sb.ToString();
        }

        protected virtual String EscapeXml(String str)
        {
            str = str.Replace("&", "&amp;");
            str = str.Replace("<", "&lt;");
            str = str.Replace(">", "&gt;");
            return str;
        }

        protected virtual byte[] readFromStream(Stream s)
        {
            byte[] ret = new byte[0];
            byte[] buff = new byte[512];
            int read = 0;

            while ((read = s.Read(buff, 0, buff.Length)) > 0)
            {
                byte[] temp = new byte[ret.Length + read];
                Array.Copy(ret, temp, ret.Length);
                Array.Copy(buff, 0, temp, ret.Length, read);
                ret = temp;
            }

            return ret;
        }

        protected virtual String SendRequest(String xml, String requestMethod, String urlString)
        {
            byte[] reqContents;
            HttpWebRequest wrq = (HttpWebRequest)HttpWebRequest.Create(urlString);
            wrq.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
            wrq.Method = requestMethod;
            wrq.ContentType = "application/x-www-form-urlencoded";
            if (!"GET".Equals(requestMethod) && !string.IsNullOrEmpty(xml))
            {
                reqContents = Encoding.UTF8.GetBytes(EscapeXml(xml));
                wrq.GetRequestStream().Write(reqContents, 0, reqContents.Length);
                wrq.GetRequestStream().Flush();
                wrq.GetRequestStream().Close();
            }
            HttpWebResponse resp = (HttpWebResponse)wrq.GetResponse();
            if (((int)resp.StatusCode) >= 300)
            {
                throw new Exception(resp.StatusDescription);
            }
            reqContents = readFromStream(wrq.GetResponse().GetResponseStream());
            return Encoding.UTF8.GetString(reqContents);
        }


        protected virtual void Read(string url, string extraParams, Action<int, int, int, int, XElement> code)
        {
            string tmp = SendRequest("", "GET", ConstructURL(url, extraParams));
            XDocument doc = XDocument.Parse(tmp);
            if (doc.Root.HasElements)
            {
                int page = 0, totalPages = 0, total = 0, perPage = 0;
                XElement pc = doc.Root.Element("PageContext");
                if (pc != null)
                {
                    int.TryParse(pc.Attribute("Page").Value, out page);
                    int.TryParse(pc.Attribute("Per_Page").Value, out perPage);
                    int.TryParse(pc.Attribute("Total").Value, out total);
                    int.TryParse(pc.Attribute("Total_Pages").Value, out totalPages);
                }
                code(page, perPage, total, totalPages, doc.Root);
            }
        }

    }
}
