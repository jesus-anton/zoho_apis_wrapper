using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace My_Zoho.Account
{
    public partial class ChangePassword : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            lblError.Text = string.Empty;
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    string userName = Session["UserName"] as string;
                    if (!string.IsNullOrWhiteSpace(userName))
                    {
                        if (Business.User.ChangePassword(userName, txtCurrentPassword.Text, txtNewPassword.Text))
                        {
                            Response.Redirect("ChangePasswordSuccess.aspx");
                        }
                        else
                        {
                            lblError.Text = "The current password is not valid";
                        }
                    }
                    else
                    {
                        //Sesión caducada.
                        FormsAuthentication.SignOut();
                        Response.Redirect("~/");
                    }
                }
                catch (Exception ex)
                {
                    lblError.Text = ex.Message;
                }
            } 
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/");
        }
    }
}
