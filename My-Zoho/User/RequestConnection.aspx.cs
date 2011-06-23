using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using System.Data.Common;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;

namespace My_Zoho.User
{
    public partial class RequestConnection : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindCurrentConnections();
            BindPendingConnections();
        }

        private void BindCurrentConnections()
        {
            DataTable tbl = GetCurrentConnections();
            dgvCurrentConnections.DataSource = tbl;
            dgvCurrentConnections.DataBind();
            if (tbl.Rows.Count == 0)
            {
                lblCurrentConnections.Text = "No active connections.";
            }
            else if (tbl.Rows.Count == 1)
            {
                lblCurrentConnections.Text = "One active connection.";
            }
            else
            {
                lblCurrentConnections.Text = string.Format("{0} active connections.", tbl.Rows.Count);
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

        private DataTable GetRequestedConnections()
        {
            using (MySqlDataAdapter da = new MySqlDataAdapter("select * from user_reqestedconnection where userid in (select userid from user where userName = @userName)", ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                da.SelectCommand.Parameters.AddWithValue("userName", Session["userName"]);
                DataTable tbl = new DataTable();
                da.FillSchema(tbl, SchemaType.Source);
                da.Fill(tbl);

                return tbl;
            }
        }

        private DataTable GetCurrentConnections()
        {
            using (MySqlDataAdapter da = new MySqlDataAdapter("select * from connection where idconnection in (select idconnection from user_connection where iduser in (select userid from user where userName = @userName))", ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                da.SelectCommand.Parameters.AddWithValue("userName", Session["userName"]);
                DataTable tbl = new DataTable();
                da.FillSchema(tbl, SchemaType.Source);
                da.Fill(tbl);

                return tbl;
            }
        }

        protected void dgvRequestedConections_RowEditing(object sender, GridViewEditEventArgs e)
        {
            dgvRequestedConections.SelectedIndex = e.NewEditIndex;
            DecodeRow(dgvRequestedConections.SelectedValue);
            dgvRequestedConections.EditIndex = e.NewEditIndex;
            dgvRequestedConections.DataBind();

        }

        private void DecodeRow(object iduser_reqestedConnection)
        {
            using (MySqlDataAdapter da = new MySqlDataAdapter("select * from user_reqestedconnection where iduser_reqestedConnection=@iduser_reqestedConnection", ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            using (MySqlCommandBuilder cbldr = new MySqlCommandBuilder(da))
            {
                da.SelectCommand.Parameters.AddWithValue("iduser_reqestedConnection", iduser_reqestedConnection);
                da.UpdateCommand = cbldr.GetUpdateCommand();

                using (DataTable tbl = new DataTable())
                {
                    da.FillSchema(tbl, SchemaType.Source);
                    da.Fill(tbl);
                    foreach (DataColumn dc in tbl.Columns)
                    {
                        if (dc.DataType == typeof(string))
                        {
                            tbl.Rows[0][dc] = tbl.Rows[0][dc] is DBNull ? (object)DBNull.Value : (object)HttpUtility.HtmlDecode(tbl.Rows[0][dc].ToString());
                        }
                    }
                    da.Update(tbl);
                    tbl.AcceptChanges();
                }
            }
            BindPendingConnections();
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

        protected void dgvRequestedConections_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            dgvRequestedConections.SelectedIndex = -1;
            dgvRequestedConections.EditIndex = -1;
            BindPendingConnections();
        }

        protected void dgvRequestedConections_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                using (MySqlDataAdapter da = new MySqlDataAdapter("select * from user_reqestedconnection where iduser_reqestedConnection=@iduser_reqestedConnection", ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                using (MySqlCommandBuilder cbldr = new MySqlCommandBuilder(da))
                {
                    da.SelectCommand.Parameters.AddWithValue("iduser_reqestedConnection", dgvRequestedConections.SelectedValue);
                    da.UpdateCommand = cbldr.GetUpdateCommand();

                    using (DataTable tbl = new DataTable())
                    {
                        da.FillSchema(tbl, SchemaType.Source);
                        da.Fill(tbl);
                        foreach (var kv in e.NewValues.Keys)
                        {
                            tbl.Rows[0][kv.ToString()] = e.NewValues[kv] != null ? (object)HttpUtility.HtmlEncode(e.NewValues[kv].ToString()) : (object)DBNull.Value;
                        }
                        da.Update(tbl);
                        tbl.AcceptChanges();
                    }
                }
                dgvRequestedConections.SelectedIndex = -1;
                dgvRequestedConections.EditIndex = -1;
                BindPendingConnections();
            }
            catch
            {
                
            }
        }

        protected void btnAddRequestedConnection_Click(object sender, EventArgs e)
        {
            //Business.RequestedConnection.InsertRequest(Session["userName"] as string, "[Enter value]", "[Enter value]", "[Enter value]");
            DataTable tbl = AddRequestedConnections();
            dgvRequestedConections.DataSource = tbl;
            dgvRequestedConections.DataBind();
            dgvRequestedConections.PageIndex = dgvRequestedConections.PageCount - 1;
            dgvRequestedConections.SelectedIndex = dgvRequestedConections.Rows.Count - 1;
            BindPendingConnections();
            dgvRequestedConections.EditIndex = dgvRequestedConections.SelectedIndex;
            dgvRequestedConections.DataBind();
        }

        private DataTable AddRequestedConnections()
        {
            using (MySqlDataAdapter da = new MySqlDataAdapter("select * from user_reqestedconnection where userid in (select userid from user where userName = @userName)", ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            using (MySqlCommandBuilder cbldr = new MySqlCommandBuilder(da))
            {
                da.SelectCommand.Parameters.AddWithValue("userName", Session["userName"]);
                da.InsertCommand = cbldr.GetInsertCommand();
                DataTable tbl = new DataTable();

                da.FillSchema(tbl, SchemaType.Source);
                da.Fill(tbl);
                DataRow row = tbl.NewRow();
                row["userid"] = Business.User.GetByUserName(Session["userName"] as string).userid;
                row["Name"] = "[Enter value]";
                row["ApiKey"] = "[Enter value]";
                row["AuthToken"] = "[Enter value]";
                tbl.Rows.Add(row);
                da.Update(tbl.GetChanges());
                tbl.Clear();
                da.FillSchema(tbl, SchemaType.Source);
                da.Fill(tbl);

                return tbl;
            }
        }
    }
}