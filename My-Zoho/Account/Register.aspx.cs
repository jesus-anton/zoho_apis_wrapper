using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace My_Zoho.Account
{
    public partial class Register : System.Web.UI.Page
    {
        private string ReturnUrl { get { return ViewState["ReturnUrl"] as string; } set { ViewState["ReturnUrl"] = value; } }
        private string UserName { get { return Session["UserName"] as string;  } set { Session["UserName"] = value; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            lblError.Text = "";
            if (!IsPostBack)
            {
                ReturnUrl = Request.QueryString["ReturnUrl"];
            }
        }

        protected void RegisterUser_CreatedUser(object sender, EventArgs e)
        {
            FormsAuthentication.SetAuthCookie(UserName, false /* createPersistentCookie */);

            ReturnToOrigin();
        }

        private void ReturnToOrigin()
        {
            string continueUrl = ReturnUrl;
            if (String.IsNullOrEmpty(continueUrl))
            {
                continueUrl = "~/";
            }
            Response.Redirect(HttpUtility.UrlDecode(continueUrl));
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    My_Zoho.Business.User.NewUser(txtUserName.Text, txtPassword.Text);
                    lblError.Text = "Usuario creado correctamente.";
                    UserName = txtUserName.Text;
                    RegisterUser_CreatedUser(this, EventArgs.Empty);
                }
                catch (Exception ex)
                {
                    lblError.Text = ex.ToString();
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ReturnToOrigin();
        }
    }
}
