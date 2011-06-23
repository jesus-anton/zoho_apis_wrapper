using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Data;

namespace My_Zoho.GUI
{
    public static class DataGridViewRowExtensions
    {
        /// <summary>
        /// Ejemplo de cómo tomar los valores de post para actualizar una fila cuando la estás metiendo al hacer DataBind en todos los Load
        /// </summary>
        /// <param name="dgvRequestedConections"></param>
        /// <param name="tbl"></param>
        /// <param name="columns"></param>
        public static void UpdateValues(this GridView dgvRequestedConections, DataTable tbl, string[] columns)
        {
            for (int cellIdx = 3; cellIdx <= 6; cellIdx++)
            {
                TableCell cell = dgvRequestedConections.SelectedRow.Cells[cellIdx];
                foreach (Control ctl in cell.Controls)
                {
                    if (ctl is TextBox)
                    {
                        TextBox tb = ctl as TextBox;
                        tbl.Rows[0][columns[cellIdx - 3]] = HttpContext.Current.Request.Form[tb.UniqueID] != null ? (object)HttpUtility.HtmlEncode(HttpContext.Current.Request.Form[tb.UniqueID]) : (object)DBNull.Value; ;
                    }
                }
            }
        }
    }
}