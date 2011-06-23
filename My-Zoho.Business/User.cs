using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace My_Zoho.Business
{
    public class User
    {
        /// <summary>
        /// Check if a user name is available for registration.
        /// </summary>
        /// <param name="userName">the user name to check for</param>
        /// <returns>true if available, false otherwise.</returns>
        public static bool Available(string userName)
        {
            bool ret = false;
            using (var ctx = new Data.ctalaiz_zohoinvoiceEntities())
            {
                ret = (ctx.user.Where((x) => x.userName.ToUpper() == userName.ToUpper()).FirstOrDefault() == default(Data.user));
            }
            return ret;
        }

        /// <summary>
        /// Inserts a new user into the data base
        /// </summary>
        /// <param name="userName">user name to save</param>
        /// <param name="password">password to save</param>
        /// <remarks>a data exception (duplicate key) could be thrown if an existing user name is specified.</remarks>
        public static void NewUser(string userName, string password)
        {
            using (My_Zoho.Data.ctalaiz_zohoinvoiceEntities ctx = new Data.ctalaiz_zohoinvoiceEntities())
            {
                ctx.AddTouser(new Data.user { userName = userName, password = password, signedUp = DateTime.Now });
                ctx.SaveChanges();
            }
        }

        /// <summary>
        /// Changes a user password
        /// </summary>
        /// <param name="userName">user name to change password</param>
        /// <param name="currentPassword">current password to check</param>
        /// <param name="newPassword">new password to save</param>
        public static bool ChangePassword(string userName, string currentPassword, string newPassword)
        {
            using (My_Zoho.Data.ctalaiz_zohoinvoiceEntities ctx = new Data.ctalaiz_zohoinvoiceEntities())
            {
                var user = ctx.user.Where((x) => x.userName.ToUpper() == userName.ToUpper()).FirstOrDefault();
                if (user != default(Data.user))
                {
                    if (user.password.ToUpper() == currentPassword.ToUpper())
                    {
                        user.password = newPassword;
                        ctx.SaveChanges();
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Logs in a user if the username exists and the passwords match. If the user name exists but the passwords do not match then a failed login is registered.
        /// </summary>
        /// <param name="userName">user name to login</param>
        /// <param name="password">password to login</param>
        /// <returns>true if the user could log in. false otherwise.</returns>
        public static bool LogIn(string userName, string password)
        {
            bool ret = false;
            using (var ctx = new Data.ctalaiz_zohoinvoiceEntities())
            {
                var usr = ctx.user.Where((x) => x.userName.ToUpper() == userName.ToUpper()).FirstOrDefault();
                ret = (usr != default(Data.user));
                if (ret)
                {
                    if (usr.password.ToUpper() == password.ToUpper())
                    {
                        usr.lastLogIn = DateTime.Now;
                        usr.failedTries = 0;
                        ret = true;
                    }
                    else
                    {
                        usr.failedTries++;
                        ret = false;
                    }
                    ctx.SaveChanges();
                }
            }
            return ret;
        }

        public static bool HasRole(string userName, string roleName)
        {
            bool ret = false;
            using (var ctx = new Data.ctalaiz_zohoinvoiceEntities())
            {
                var user = ctx.user.Where((x) => x.userName.ToUpper() == userName.ToUpper()).FirstOrDefault();
                if (user != default(Data.user))
                {
                    var role = user.role.Where((x) => x.roleName.ToUpper() == roleName.ToUpper()).FirstOrDefault();
                    if (role != default(Data.role))
                    {
                        ret = true;
                    }
                }
            }
            return ret;
        }

        public static bool IsAdmin(string userName)
        {
            return HasRole(userName, "admin");
        }

        public static bool IsUser(string userName)
        {
            return HasRole(userName, "user") || HasRole(userName, "admin");
        }

        public static void AddRole(string userName, string roleName)
        {
            using (var ctx = new Data.ctalaiz_zohoinvoiceEntities())
            {
                var user = ctx.user.Where((x) => x.userName.ToUpper() == userName.ToUpper()).FirstOrDefault();
                if (user != default(Data.user))
                {
                    var role = ctx.role.Where((x) => x.roleName.ToUpper() == roleName.ToUpper()).FirstOrDefault();
                    if (role != default(Data.role))
                    {
                        user.role.Add(role);
                        role.user.Add(user);
                        ctx.SaveChanges();
                    }
                }
            }
        }

        public static void DelRole(string userName, string roleName)
        {
            using (var ctx = new Data.ctalaiz_zohoinvoiceEntities())
            {
                var user = ctx.user.Where((x) => x.userName.ToUpper() == userName.ToUpper()).FirstOrDefault();
                if (user != default(Data.user))
                {
                    var role = user.role.Where((x) => x.roleName.ToUpper() == roleName.ToUpper()).FirstOrDefault();
                    if (role == default(Data.role))
                    {
                        user.role.Remove(role);
                        role.user.Remove(user);
                    }
                }
            }
        }

        public static List<My_Zoho.Data.connection> GetUserConnections(string userName)
        {
            var ret = new List<My_Zoho.Data.connection>();
            using (My_Zoho.Data.ctalaiz_zohoinvoiceEntities ctx = new Data.ctalaiz_zohoinvoiceEntities())
            {
                var user = ctx.user.Where((x) => x.userName == userName).FirstOrDefault();
                if (user != default(My_Zoho.Data.user))
                {
                    ret.AddRange(user.connection);
                }
            }
            return ret;
        }

        public static void AddConnection(string userName, string conName, string apiKey, string authToken, string companyName = null)
        {
            using (My_Zoho.Data.ctalaiz_zohoinvoiceEntities ctx = new Data.ctalaiz_zohoinvoiceEntities())
            {
                var user = ctx.user.Where((x) => x.userName.ToString() == userName.ToString()).FirstOrDefault();
                if (user != default(My_Zoho.Data.user))
                {
                    var con = user.connection.Where((x) => x.Name.ToUpper() == conName.ToUpper()).FirstOrDefault();
                    if (con == default(My_Zoho.Data.connection))
                    {
                        con = My_Zoho.Data.connection.Createconnection(-1, conName, apiKey, authToken);
                        con.user.Add(user);
                    }
                    else
                    {
                        con.ApiKey = apiKey;
                        con.AuthToken = authToken;
                    }
                    con.CompanyName = companyName;
                }
            }
        }

        /// <summary>
        /// Returns a user searching by its user id.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static My_Zoho.Data.user GetByUserName(string userName)
        {
            My_Zoho.Data.user ret;
            using (var ctx = new Data.ctalaiz_zohoinvoiceEntities())
            {
                ret = (ctx.user.Where((x) => x.userName.ToUpper() == userName.ToUpper()).FirstOrDefault());
            }
            return ret;
        }

        /// <summary>
        /// Returns a user searching by its user id.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static My_Zoho.Data.user GetByUserId(int userid)
        {
            My_Zoho.Data.user ret;
            using (var ctx = new Data.ctalaiz_zohoinvoiceEntities())
            {
                ret = (ctx.user.Where((x) => x.userid == userid)).FirstOrDefault();
            }
            return ret;
        }

        public static List<My_Zoho.Data.user_reqestedconnection> GetRequestedConnectionsOf(string userName)
        {
            List<My_Zoho.Data.user_reqestedconnection> ret = new List<My_Zoho.Data.user_reqestedconnection>();
            using (var ctx = new Data.ctalaiz_zohoinvoiceEntities())
            {
                var user = (ctx.user.Where((x) => x.userName.ToUpper() == userName.ToUpper()).FirstOrDefault());
                if (user != default(My_Zoho.Data.user))
                {
                    ret.AddRange(user.user_reqestedconnection);
                }
            }
            return ret;
        }
    }
}
