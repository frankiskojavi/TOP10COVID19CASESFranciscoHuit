using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace TOP10COVID19CASESFranciscoHuit.Views
{
    public partial class WpgReport : System.Web.UI.Page
    {
        TOP10COVID19CASESFranciscoHuit.Controllers.COVID19ReportController covidController = new TOP10COVID19CASESFranciscoHuit.Controllers.COVID19ReportController();
        Thread fileThread;
        DataTable tableData;
        String reportType;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    FillDpdRegions();
                }catch(Exception ex)
                {
                    lblMessage.Text = ex.Message;
                }
            }else
            {
                lblMessage.Text = "There are not registers to download";
                dpdRegions.DataSource = null;
                dpdRegions.DataBind();
            }
        }        
        protected void Button5_Click(object sender, EventArgs e)
        {
            String province = dpdRegions.SelectedItem.Text;
            try {
                if (province.Equals("Regions"))
                {
                    tableData = covidController.getReportForRegions();
                }
                else
                {
                    tableData = covidController.getReportForProvinces(province);
                }
                grdView2.DataSource = tableData;
                grdView2.DataBind();
                ViewState["Data"] = tableData;
            } catch(Exception ex)
            {
                lblMessage.Text = ex.Message;
                grdView2.DataSource = null;
                grdView2.DataBind();
            }

        }

        protected void btnXML_Click(object sender, EventArgs e)
        {
            reportType = "XML";
            startReport();
        }

        protected void btnJSON_Click(object sender, EventArgs e)
        {
            reportType = "JSON";
            startReport();
        }

        protected void btnCSV_Click(object sender, EventArgs e)
        {

            reportType = "CSV";
            startReport();
        }

        public void FillDpdRegions()
        {
            dpdRegions.DataSource = covidController.getRegions();
            dpdRegions.DataTextField = "name";
            dpdRegions.DataValueField = "iso";
            dpdRegions.DataBind();
        }

        private void startReport()
        {
            try
            {
                if (ViewState["Data"] != null)
                {
                    DataTable dtxml = (DataTable)ViewState["Data"];
                    if (dtxml.Rows.Count > 0)
                    {
                        fileThread = new Thread(new ThreadStart(downloadReport));
                        fileThread.ApartmentState = ApartmentState.STA;
                        fileThread.Start();
                    }
                }
            }catch(Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
            
        }
        private void downloadReport()
        {
            try
            {
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.FilterIndex = 2;
                dialog.RestoreDirectory = true;
                dialog.InitialDirectory = "C:\\";

                switch (reportType)
                {
                    case "XML":
                        dialog.Filter = "Xml files (*.xml)|*.xml";
                        dialog.FileName = "Report_XML";
                        dialog.Title = "XML Export";
                        break;
                    case "CSV":
                        dialog.Filter = "|*.csv";
                        dialog.FileName = "Report_CSV";
                        dialog.Title = "CSV Export";
                        break;
                    case "JSON":
                        dialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                        dialog.FileName = "Report_JSON";
                        dialog.Title = "JSON Export";
                        break;

                }

                DataSet ds = new DataSet();
                DataTable dtxml = (DataTable)ViewState["Data"];
                string JSONString = string.Empty;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    StreamWriter sw = new StreamWriter(dialog.FileName);
                    switch (reportType)
                    {
                        case "XML":
                            ds.Tables.Add(dtxml);
                            ds.WriteXml(sw);
                            break;
                        case "CSV":
                            WriteDataTable(dtxml, sw, true);
                            break;
                        case "JSON":
                            JSONString = JsonConvert.SerializeObject(dtxml);
                            sw.WriteLine(JSONString);
                            break;
                    }
                    sw.Flush();
                    sw.Close();
                }
                
            }catch(Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
            fileThread.Abort();
        }
        public static void WriteDataTable(DataTable sourceTable, TextWriter writer, bool includeHeaders)
        {
            if (includeHeaders)
            {
                List<string> headerValues = new List<string>();
                foreach (DataColumn column in sourceTable.Columns)
                {
                    headerValues.Add(column.ColumnName);
                }

                writer.WriteLine(String.Join(",", headerValues.ToArray()));
            }
            string[] items = null;
            foreach (DataRow row in sourceTable.Rows)
            {
                items = row.ItemArray.Select(o => o.ToString()).ToArray();
                writer.WriteLine(String.Join(",", items));
            }
            writer.Flush();
        }
    }
}