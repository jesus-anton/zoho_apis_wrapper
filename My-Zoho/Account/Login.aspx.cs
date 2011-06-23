using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace My_Zoho.Account
{
    public partial class Login : System.Web.UI.Page
    {
        private string ReturnUrl
        {
            get
            {
                string ret = ViewState["ReturnUrl"] as string;
                if (String.IsNullOrEmpty(ret))
                {
                    ret = "~/";
                }
                return ret;
            }
            set
            {
                ViewState["ReturnUrl"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ReturnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            RegisterHyperLink.NavigateUrl = "Register.aspx?ReturnUrl=" + ReturnUrl;
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text;
            string password = txtPassword.Text;

            if (My_Zoho.Business.User.LogIn(userName, password))
            {
                FormsAuthentication.SetAuthCookie(userName, false /* createPersistentCookie */);
                Session["UserName"] = userName;
                Response.Redirect(HttpUtility.UrlDecode(ReturnUrl));
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(HttpUtility.UrlDecode(ReturnUrl));
        }
    }
}
