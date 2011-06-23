using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace My_Zoho.Business
{
    public class Connection
    {
        public static List<My_Zoho.Data.connection> GetConnections()
        {
            var ret = new List<My_Zoho.Data.connection>();
            using (My_Zoho.Data.ctalaiz_zohoinvoiceEntities ctx = new Data.ctalaiz_zohoinvoiceEntities())
            {
                ret.AddRange(ctx.connection);
            }
            return ret;
        }

        public static List<My_Zoho.Data.connection> GetConnectionsByUser(string userName)
        {
            return User.GetUserConnections(userName);
        }

        public static void InsertConnection(string conName, string apiKey, string authToken, string companyName = null)
        {
            using (My_Zoho.Data.ctalaiz_zohoinvoiceEntities ctx = new Data.ctalaiz_zohoinvoiceEntities())
            {
                var con = My_Zoho.Data.connection.Createconnection(-1, conName, apiKey, authToken);
                ctx.AddToconnection(con);
                ctx.SaveChanges(System.Data.Objects.SaveOptions.AcceptAllChangesAfterSave);
            }
        }

        public static void UpdateConnection(My_Zoho.Data.connection con)
        {
            using (My_Zoho.Data.ctalaiz_zohoinvoiceEntities ctx = new Data.ctalaiz_zohoinvoiceEntities())
            {
                var con1 = ctx.connection.Where((x) => x.idConnection == con.idConnection).FirstOrDefault();
                if (con1 != default(My_Zoho.Data.connection))
                {
                    con1.Name = con.Name;
                    con1.ApiKey = con.ApiKey;
                    con1.AuthToken = con.AuthToken;
                    con1.CompanyName = con.CompanyName;
                    ctx.SaveChanges();
                }
            }
        }

        public static void DelecteConnection(My_Zoho.Data.connection con)
        {
            using (My_Zoho.Data.ctalaiz_zohoinvoiceEntities ctx = new Data.ctalaiz_zohoinvoiceEntities())
            {
                var con1 = ctx.connection.Where((x) => x.idConnection == con.idConnection).FirstOrDefault();
                if (con1 != default(My_Zoho.Data.connection))
                {
                    //con1.a
                }
            }
        }
    }
}
