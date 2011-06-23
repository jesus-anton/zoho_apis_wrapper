using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;

namespace My_Zoho.Account
{
    /// <summary>
    /// Summary description for SecurityService
    /// </summary>
    [WebService()]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [ScriptService()]
    public class SecurityService : System.Web.Services.WebService
    {
        [WebMethod]
        [ScriptMethod(XmlSerializeString = false, UseHttpGet=false, ResponseFormat = ResponseFormat.Json)]
        public bool UserNameAvailable(string userName)
        {
            return My_Zoho.Business.User.Available(userName);
        }
    }
}
