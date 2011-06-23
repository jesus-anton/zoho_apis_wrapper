using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;
using My_Zoho.Data;
using System.Configuration;

namespace My_Zoho.Admin
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Business.User.IsAdmin(Session["UserName"] as string))
            {
                Response.Redirect("~/");
            }
            BindPendingConnections();
        }

        private DataTable GetRequestedConnections()
        {
            using (MySqlDataAdapter da = new MySqlDataAdapter("select * from user_reqestedconnection", ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                DataTable tbl = new DataTable();
                da.FillSchema(tbl, SchemaType.Source);
                da.Fill(tbl);

                return tbl;
            }
        }

        private void BindPendingConnections()
        {
            //Esta comprobación ha costado una tarde entera de trabajo. Por esta estúpida condición (ausencia de ella), 
            //una tarde entera haciendo el gilipollas y persiguiendo fantasmas.
            if (dgvRequestedConections.EditIndex == -1)
            {
                DataTable tbl;
                dgvRequestedConections.DataSource = tbl = GetRequestedConnections();
                dgvRequestedConections.DataBind();
                if (tbl.Rows.Count == 0)
                {
                    lblPendingConnections.Text = "No pending connections.";
                }
                else if (tbl.Rows.Count == 1)
                {
                    lblPendingConnections.Text = "One pending connection.";
                }
                else
                {
                    lblPendingConnections.Text = string.Format("{0} pending connections.", tbl.Rows.Count);
                }
            }
        }

        protected void dgvRequestedConections_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            dgvRequestedConections.SelectedIndex = e.RowIndex;
            using (MySqlDataAdapter da = new MySqlDataAdapter("select * from user_reqestedconnection where iduser_reqestedConnection=@iduser_reqestedConnection", ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            using (MySqlCommandBuilder cbldr = new MySqlCommandBuilder(da))
            {
                da.SelectCommand.Parameters.AddWithValue("iduser_reqestedConnection", dgvRequestedConections.SelectedValue);
                da.DeleteCommand = cbldr.GetDeleteCommand();

                using (DataTable tbl = new DataTable())
                {
                    da.FillSchema(tbl, SchemaType.Source);
                    da.Fill(tbl);
                    tbl.Rows[0].Delete();
                    da.Update(tbl);
                    tbl.AcceptChanges();
                }
            }
            BindPendingConnections();
        }

        protected void dgvRequestedConections_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Pasar la conexión a permanente - habilitada para importar datos a nuestro sistema.

        }
    }
}