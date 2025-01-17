using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class frm_Academic_Year : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ConnectionString);
    Class1 cls = new Class1();
    AcademicYear acd = new AcademicYear();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["emp_id"] == null || Session["emp_id"] == "")
            {
                Response.Redirect("Login.aspx?msg=session");
            }
            else
            {
                if (Convert.ToString(Session["acdmsg"]) == "saved")
                {
                    Session["acdmsg"] = "";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Saved Successfully', { color: '#fff', background: '#198104', blur: 0.2, delay: 0, timeout: 100 });", true);
                }
                if (!IsPostBack)
                {
                    load();
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    public void load()
    {
        DataSet ds = FillGrid();
        grid.DataSource = ds;
        grid.DataBind();
    }

    public DataSet FillGrid()
    {
        DataSet dataSet = new DataSet();
        try
        {
            
            string urlalias = cls.urls();
            string url = @urlalias + "AcademicYears/";

            acd.type = "load";

            string jsonString = JsonHelper.JsonSerializer<AcademicYear>(acd);
            var httprequest = (HttpWebRequest)WebRequest.Create(url);
            httprequest.ContentType = "application/json";
            httprequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httprequest.GetRequestStream()))
            {
                streamWriter.Write(jsonString);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpresponse = (HttpWebResponse)httprequest.GetResponse();
            using (var streamReader = new StreamReader(httpresponse.GetResponseStream()))
            {
                string result = streamReader.ReadToEnd();
                dataSet = JsonConvert.DeserializeObject<DataSet>(result);
            }
            return dataSet;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
            return dataSet;
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        load();
        clearcontrols();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            Session["acdmsg"] = "";
            string json = "{" + '"' + "year" + '"' + ":" + "[";
            for (int i = 0; i < grid.Rows.Count; i++)
            {
                string type = "";
                string flag = "";
                if (((CheckBox)grid.Rows[i].FindControl("chk")).Checked == true)
                {
                    flag = "True";
                }
                else
                {
                    flag = "False";
                }
                if (((Label)grid.Rows[i].FindControl("lblID2")).Text != "")
                {
                    type = "update";
                }
                else
                {
                    type = "Insert";
                }
                json = json + "{" + '"' + "AYID" + '"' + ":" + '"' + "" + ((Label)grid.Rows[i].FindControl("lblID2")).Text.ToString() + "" + '"' + ',' + '"' + "Duration" + '"' + ":" + '"' + "" + ((Label)grid.Rows[i].FindControl("lblID1")).Text.ToString() + "" + '"' + ',' + '"' + "flag" + '"' + ':' + '"' + "" + flag.ToString() + "" + '"' + ',' + '"' + "Type" + '"' + ':' + '"' + "" + type.ToString() + "" + '"' + "}";
                if (i != grid.Rows.Count - 1)
                {
                    json = json + ",";
                }
            }
            json = json + "]}";
            string urlalias = cls.urls();
            string url = @urlalias + "AcademicYears/";

            acd.type = "save";
            acd.date = json.ToString();

            string jsonString = JsonHelper.JsonSerializer<AcademicYear>(acd);
            var httprequest = (HttpWebRequest)WebRequest.Create(url);
            httprequest.ContentType = "application/json";
            httprequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httprequest.GetRequestStream()))
            {
                streamWriter.Write(jsonString);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpresponse = (HttpWebResponse)httprequest.GetResponse();
            using (var streamReader = new StreamReader(httpresponse.GetResponseStream()))
            {
                string result = streamReader.ReadToEnd();
                if (result.ToString().Contains("Save") == true)
                {
                    Session["acdyear"] = null;
                    Response.Redirect(HttpContext.Current.Request.Url.ToString(), false);
                    Session["acdmsg"] = "saved";
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    public void notify(string msg, string color)
    {
        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + msg + "', { color: '#fff', background: '" + color + "', blur: 0.2, delay: 0 });", true);
    }

    void clearcontrols()
    {
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txt1.ClientID + "').value = '';", true);
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript2", "document.getElementById('" + txt2.ClientID + "').value = '';", true);
    }

    public void EmptyControlData(Control parent)
    {
        foreach (Control c in parent.Controls)
        {
            if ((c.GetType() == typeof(TextBox)))
            {
                ((TextBox)c).Text = "";
            }
            if (c.HasControls())
            {
                EmptyControlData(c);
            }
        }
    }

  
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            String duration = "";
            if (txt1.Text != "" && txt2.Text != "")
            {
                duration = txt1.Text + " -" + txt2.Text;
                string year = duration.Substring(6, 4) + " -" + duration.Substring(18, 4);
                string sr3 = year.Replace("/", "a");

                DataSet ds = new DataSet();
                string urlalias = cls.urls();
                string url = @urlalias + "AcademicYears/";

                acd.type = "add";
                acd.date = sr3.ToString();
                acd.start = txt1.Text.Trim().Replace("/", "a");


                string jsonString = JsonHelper.JsonSerializer<AcademicYear>(acd);
                var httprequest = (HttpWebRequest)WebRequest.Create(url);
                httprequest.ContentType = "application/json";
                httprequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httprequest.GetRequestStream()))
                {
                    streamWriter.Write(jsonString);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var httpresponse = (HttpWebResponse)httprequest.GetResponse();
                using (var streamReader = new StreamReader(httpresponse.GetResponseStream()))
                {
                    string result = streamReader.ReadToEnd();
                    ds = JsonConvert.DeserializeObject<DataSet>(result);
                }
  
                if (ds.Tables[0].Rows.Count == 0)
                {
                    if (ds.Tables[1].Rows[0]["remark"].ToString() == "Allow")
                    {
                        DataSet dt = new DataSet();
                        dt = FillGrid();
                        dt.Tables[0].Rows.Add("", txt1.Text + " - " + txt2.Text);
                        grid.DataSource = dt;
                        duration = txt1.Text + " - " + txt2.Text;
                        grid.DataBind();
                        clearcontrols();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Selected Range Already Alloted in Year', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                        return;
                    }

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Academic Year Already Exist', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                    clearcontrols();

                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Fill the details', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                clearcontrols();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void chk_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox activeCheckBox = sender as CheckBox;

            foreach (GridViewRow rw in grid.Rows)
            {
                CheckBox chkBx = (CheckBox)rw.FindControl("chk");
                if (chkBx != activeCheckBox)
                {
                    chkBx.Checked = false;
                }
                else
                {
                    chkBx.Checked = true;
                }
            }
            activeCheckBox.Focus();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }


    }

    protected void txt1_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txt1.Text != "")
            {
                DateTime dt = DateTime.ParseExact(txt1.Text.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                string b = String.Format("{0:dd/MM/yyyy}", dt);
                string[] sDate = b.Split('-');
                txt1.Text = b.Replace('-', '/');


                DateTime dt1 = dt.AddDays(-1).AddYears(1);
                string a = String.Format("{0:dd/MM/yyyy}", dt1);
                string[] sDate1 = a.Split('-');
                txt2.Text = a.Replace('-', '/');
            }
            else
            {
                txt2.Text = "";
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }

    }

    public class JsonHelper
    {
        public static string JsonSerializer<T>(T t)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, t);
            string jsonString = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();
            return jsonString;
        }
    }

    protected void grid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (((Label)e.Row.FindControl("lblflag")).Text == "True")
                {
                    ((CheckBox)e.Row.FindControl("chk")).Checked = true;
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }
}   