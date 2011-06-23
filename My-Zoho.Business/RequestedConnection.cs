using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace My_Zoho.Business
{
    public class RequestedConnection
    {
        public List<My_Zoho.Data.user_reqestedconnection> GetData(string userName)
        {
            return User.GetRequestedConnectionsOf(userName);
        }

        public static void InsertRequest(string userName, string conName, string apiKey, string authToken, string companyName = null)
        {
            using (My_Zoho.Data.ctalaiz_zohoinvoiceEntities ctx = new Data.ctalaiz_zohoinvoiceEntities())
            {
                var user = ctx.user.Where(u => u.userName.ToUpper() == userName.ToUpper()).FirstOrDefault();
                if (user != default(My_Zoho.Data.user))
                {
                    var con = My_Zoho.Data.user_reqestedconnection.Createuser_reqestedconnection(-1, user.userid, conName, apiKey, authToken);
                    con.CompanyName = companyName;
                    ctx.AddTouser_reqestedconnection(con);
                    ctx.SaveChanges(System.Data.Objects.SaveOptions.AcceptAllChangesAfterSave);
                }
            }
        }

        public static void UpdateRequest(My_Zoho.Data.user_reqestedconnection req)
        {
            using (My_Zoho.Data.ctalaiz_zohoinvoiceEntities ctx = new Data.ctalaiz_zohoinvoiceEntities())
            {
                var con = ctx.user_reqestedconnection.Where(r => r.iduser_reqestedConnection == req.iduser_reqestedConnection).FirstOrDefault();
                if (con != default(My_Zoho.Data.user_reqestedconnection))
                {
                    con.Name = req.Name;
                    con.ApiKey = req.ApiKey;
                    con.AuthToken = req.AuthToken;
                    con.CompanyName = req.CompanyName;
                    ctx.SaveChanges();
                }
            }
        }

        public static void DeleteRequest(My_Zoho.Data.user_reqestedconnection req)
        {
            using (My_Zoho.Data.ctalaiz_zohoinvoiceEntities ctx = new Data.ctalaiz_zohoinvoiceEntities())
            {
                var con = ctx.user_reqestedconnection.Where(r => r.iduser_reqestedConnection == req.iduser_reqestedConnection).FirstOrDefault();
                if (con != default(My_Zoho.Data.user_reqestedconnection))
                {
                    ctx.DeleteObject(con);
                    ctx.SaveChanges();
                }
            }
        }

        public static My_Zoho.Data.user_reqestedconnection GetById(int id)
        {
            using (My_Zoho.Data.ctalaiz_zohoinvoiceEntities ctx = new Data.ctalaiz_zohoinvoiceEntities())
            {
                var con = ctx.user_reqestedconnection.Where(r => r.iduser_reqestedConnection == id).FirstOrDefault();
                if (con != default(My_Zoho.Data.user_reqestedconnection))
                {
                    return con;
                }
            }
            return null;
        }
    }
}
