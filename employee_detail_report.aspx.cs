using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class employee_detail_report : System.Web.UI.Page
{
    employee_d_rpt edr = new employee_d_rpt();
    DataTable dt = new DataTable();
    string arr;
    Class1 cls = new Class1();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Convert.ToString(Session["emp_id"]) == "")
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                try
                {
                    emp();
                    details();
                    excel.Visible = false;
                    gcard.Visible = false;
                }
                catch (Exception ex)
                {
                    Response.Redirect("Login.aspx");
                }
            }
           

        }

    }

    public class JsonHelper
    {
        /// <summary>
        /// JSON Serialization
        /// </summary>
        public static string JsonSerializer<T>(T t)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, t);
            string jsonString = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();
            return jsonString;
        }
        /// <summary>
        /// JSON Deserialization
        /// </summary>
        public static T JsonDeserialize<T>(string jsonString)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T obj = (T)ser.ReadObject(ms);
            return obj;
        }
    }

    private void details()
    {
        try
        {
            string type = "selectempdt";

            string urlalias = cls.urls();
            string url = @urlalias + "emprptdtl/";

            //  string url = @"http://localhost:9199/emprptdtl/";
            edr.type = type.ToString();

            string jsonString = JsonHelper.JsonSerializer<employee_d_rpt>(edr);


            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {

                streamWriter.Write(jsonString);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            using (StreamReader sr = new StreamReader(httpResponse.GetResponseStream()))
            {
                string result = sr.ReadToEnd();
                DataSet dslist = JsonConvert.DeserializeObject<DataSet>(result);
                Session["DSLIST"] = dslist;
                DataTable dt1 = dslist.Tables[0];
                dt.Columns.Add("table", typeof(string));
                dt.Columns.Add("tableshow", typeof(string));
                DataRow dr1, dr2;

                for (int i = 0; i < dt1.Columns.Count; i++)
                {
                    string temp = dt1.Columns[i].ColumnName.ToString();
                    string disval = dt1.Columns[i].ColumnName.ToString();

                    dr1 = dt.NewRow();
                    //dr2 = dt.NewRow();
                    dr1[0] = temp.ToString();
                    dr1[1] = disval.ToString().Replace("_", " ");
                    //dt.Rows.Add(dr2);
                    dt.Rows.Add(dr1);
                }
                //for (int i = 0; i < dt1.Columns.Count; i++)
                //{
                //    //string temp = dt1.Columns[i].ColumnName.ToString();
                //    string disval = dt1.Columns[i].ColumnName.ToString();

                //    //dr1 = dt.NewRow();
                //    dr2 = dt.NewRow();
                //    //dr1[0] = temp.ToString();
                //    dr2[1] = disval.ToString().Replace("_", " ");
                //    dt.Rows.Add(dr2);
                //    //dt.Rows.Add(dr1);
                //}

                ddlselfield.Items.Clear();
                ddlselfield.DataTextField = "tableshow";
                ddlselfield.DataValueField = "table";
                ddlselfield.DataSource = dt;
                ddlselfield.DataBind();
                getdata.Enabled = true;

            }
        }
        catch (Exception dex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    private void emp()
    {
        try
        {

            string type = "ddlemptype";

            string urlalias = cls.urls();
            string url = @urlalias + "emprptdtl/";
            // string url = @"http://localhost:9199/emprptdtl/";
            edr.type = type.ToString();

            string jsonString = JsonHelper.JsonSerializer<employee_d_rpt>(edr);


            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {

                streamWriter.Write(jsonString);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            using (StreamReader sr = new StreamReader(httpResponse.GetResponseStream()))
            {
                string result = sr.ReadToEnd();
                DataSet dslist = JsonConvert.DeserializeObject<DataSet>(result);
                Session["DSLIST"] = dslist;
                DataTable dt1 = dslist.Tables[0];
                emptype.DataSource = dt1;
                emptype.DataTextField = "role_name";
                emptype.DataValueField = "role_id";
                emptype.DataBind();
                emptype.Items.Insert(0, "--Select--");
                emptype.SelectedIndex = 0;
            }
        }
        catch (Exception ex) {
            Response.Redirect("Login.aspx");
        }
    }

    public string load()
    {
        string ddlvalue = "";
        try
        {
            IList<string> selectedItems = new List<string>();
           

            foreach (ListItem item in ddlselfield.Items)
            {
                if (item.Selected == true)
                {

                    ddlvalue += item.Value +" "+"as"+" ["+item.Text+ "],";

                }
            }


            ddlvalue = ddlvalue.TrimEnd(',');
            return ddlvalue;
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
            return ddlvalue;
        }

    }

    public void notifys(string msg, string color)
    {
        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + msg + "', { color: '#fff', background: '" + color + "', blur: 0.2, delay: 0 });", true);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> ddlselmul();</script>", false);
    }

    protected void getdata_Click(object sender, EventArgs e)
    {
        if (ddlselfield.Text == "")
        {
            notifys("Please Select atleast one field", "#D9534F");
        }
        else
        {
            try
            {
                string list = load();

                string type = "getdata";
                string role_id = emptype.SelectedValue.ToString();

                string urlalias = cls.urls();
                string url = @urlalias + "emprptdtl/";
                //  string url = @"http://localhost:9199/emprptdtl/";
                edr.type = type.ToString();
                edr.arr = list.ToString();

                if (emptype.SelectedIndex > 0)
                {
                    edr.role_type = role_id;
                }

                string jsonString = JsonHelper.JsonSerializer<employee_d_rpt>(edr);


                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {

                    streamWriter.Write(jsonString);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (StreamReader sr = new StreamReader(httpResponse.GetResponseStream()))
                {
                    string result = sr.ReadToEnd();
                    DataSet dslist = JsonConvert.DeserializeObject<DataSet>(result);
                    DataTable dt1 = dslist.Tables[0];
                    if (dt1.Rows.Count > 0)
                    {
                        gcard.Visible = true;
                        gvdata.DataSource = dt1;
                        gvdata.DataBind();
                        //getdata.Enabled = false;
                        excel.Visible = true;
                        //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> clk();</script>", false);
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> ddlselmul(); </script>", false);
                        //getdata.Enabled = false;




                    }
                    else
                    {

                        gcard.Visible = false;
                        notifys("No Employees Found", "#D9534F");
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> ddlselmul();</script>", false);


                    }
                }
            }
            catch (Exception ex) {
                Response.Redirect("Login.aspx");
            }
        }

    }

    private void clkd()
    {

    }

    public override void VerifyRenderingInServerForm(Control control)
    {
       
    }

    protected void excel_Click(object sender, EventArgs e)
    { 
        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        string FileName = "Employee" + DateTime.Now + ".xls";
        StringWriter strwritter = new StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        gvdata.GridLines = GridLines.Both;
        gvdata.HeaderStyle.Font.Bold = true;
        gvdata.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();
        //getdata.Enabled = false;
    }

    protected void clear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }

    protected void ddlselfield_SelectedIndexChanged(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> ddlselmul(); </script>", false);
        getdata.Enabled = true;
    }

    protected void ddlselfield_TextChanged(object sender, EventArgs e)
    {

    }

    protected void emptype_SelectedIndexChanged(object sender, EventArgs e)
    {
        gcard.Visible = false;
        excel.Visible = false;
        gvdata.DataSource = "";
        gvdata.DataBind();
        ddlselfield.Items.Clear();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> ddlselmul(); </script>", false);
        details();
    }
}
