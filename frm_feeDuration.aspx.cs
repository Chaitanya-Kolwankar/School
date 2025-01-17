using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frm_feeDuration : System.Web.UI.Page
{
    Class1 cls = new Class1();
    FeeMaster fm = new FeeMaster();
    public List<String> list = new List<String>();
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
                if (!IsPostBack)
                {
                    ddlfill();
                    Session["sel1"] = null;
                    Session["sel2"] = null;
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    public void useless()
    {
        fm.ayid = null;
        fm.caste = null;
        fm.class_id = null;
        fm.duration = null;
        fm.medium_id = null;
        fm.type = null;
        fm.dutype = null;
        fm.table = null;
        fm.user = null;
        fm.duration_id = null;

    }

    public void ddlfill()
    {
        try
        {
            common cm = new common();
            DataSet dataSet = new DataSet();

            string urlalias = cls.urls();
            string url = @urlalias + "Common/";

            cm.type = "ddlfill";
            cm.year = Session["acdyear"].ToString();

            string jsonString = JsonHelper.JsonSerializer<common>(cm);
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
                Session["DSLIST"] = dataSet;
                ddlmedium.DataSource = dataSet.Tables[0];
                ddlmedium.DataTextField = "medium";
                ddlmedium.DataValueField = "med_id";
                ddlmedium.Items.Insert(0, "--Select--");
                ddlmedium.DataBind();
                ddlmedium.Items.Insert(0, "--Select--");
                ddlmedium.SelectedIndex = 0;
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

    protected void ddlmedium_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            grduration.DataSource = null;
            grduration.DataBind();
            if (ddlmedium.SelectedIndex > 0)
            {
                DataSet ds = (DataSet)Session["DSLIST"];
                foreach (DataTable table in ds.Tables)
                {
                    if (table.TableName.Equals(ddlmedium.SelectedValue.ToString()))
                    {
                        ddlclass.DataSource = table;
                        ddlclass.DataTextField = "std_name";
                        ddlclass.DataValueField = "std_id";
                        ddlclass.DataBind();
                        ddlclass.Enabled = true;
                        ddlclass.Items.Insert(0, "--SELECT--");
                        ddlclass.SelectedIndex = 0;
                    }

                }
                ddlclass.Enabled = true;
            }
            else
            {
                ddlclass.Enabled = false;
                if (ddlclass.SelectedIndex > 0)
                {
                    ddlclass.SelectedIndex = 0;
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>multi();</script>", false);
    }

    protected void ddlclass_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            grduration.DataSource = null;
            grduration.DataBind();
            if (ddlclass.SelectedIndex > 0)
            {
                ddltype.Enabled = true;
                useless();
                string urlalias = cls.urls();
                string url = @urlalias + "loadfee/";
                fm.type = "loadtype";

                string jsonString = JsonHelper.JsonSerializer<FeeMaster>(fm);
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
                    DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(result);
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        ddltype.DataSource = dataSet.Tables[0];
                        ddltype.DataTextField = "type";
                        ddltype.DataValueField = "value";
                        ddltype.DataBind();
                        ddltype.Items.Insert(0, "--Select--");
                        ddltype.Enabled = true;
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('No Fee Payment Duration Type Found', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                    }
                }
            }
            else
            {
                ddltype.Enabled = false;
                if (ddltype.SelectedIndex > 0)
                {
                    ddltype.SelectedIndex = 0;
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>multi();</script>", false);
    }

    public void month()
    {
        if (ddltype.SelectedIndex > 0)
        {
            lstmonth.Items.Clear();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>multi();</script>", false);
            lstmonth.Items.Add("January");
            lstmonth.Items.Add("February");
            lstmonth.Items.Add("March");
            lstmonth.Items.Add("April");
            lstmonth.Items.Add("May");
            lstmonth.Items.Add("June");
            lstmonth.Items.Add("July");
            lstmonth.Items.Add("August");
            lstmonth.Items.Add("September");
            lstmonth.Items.Add("October");
            lstmonth.Items.Add("November");
            lstmonth.Items.Add("December");
            lstmonth.Enabled = true;
        }
    }

    protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            load();
            lstmonth.Enabled = true;
            if (ddltype.SelectedValue.ToString() == "6" || ddltype.SelectedValue.ToString() == "12")
            {
                txtname.Enabled = true;
                multi.Style.Add("display","none");
                text.Style.Add("display", "block");
                if (ddltype.SelectedValue.ToString() == "6")
                {
                    lblname.Text = "Term";
                    txtname.Text = "";
                }
                else if(ddltype.SelectedValue.ToString() == "12")
                {
                    lblname.Text = "Year";
                    string[] str= Session["year"].ToString().Split('/');
                    string[] str2 = str[2].ToString().Split('-');
                    txtname.Text = "Academic Year "+str2[0]+'-'+str[4];
                    txtname.Enabled = false;
                }
            }
            else if (ddltype.SelectedValue.ToString() == "3" || ddltype.SelectedValue.ToString() == "1")
            {
                multi.Style.Add("display", "block");
                text.Style.Add("display", "none");
            }
            else
            {
                multi.Style.Add("display", "none");
                text.Style.Add("display", "block");
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlmedium.SelectedIndex > 0 && ddlclass.SelectedIndex > 0 && ddltype.SelectedIndex > 0)
            {
                bool flag = false;
                DataTable dt = new DataTable();
                dt.Columns.Add("duration");
                string[] str = new string[12];
                if (ddltype.SelectedValue.ToString() != "12" && ddltype.SelectedValue.ToString() != "6")
                {
                    str = Session["id"].ToString().Split(',');
                }

                if (str.Length!= 0)
                {
                    if (ddltype.SelectedValue.ToString() == "1")
                    {
                        for (int i = 0; i < str.Length; i++)
                        {
                            dt.Rows.Add("");
                            dt.Rows[i]["duration"] = str[i];
                        }
                        useless();
                        string urlalias = cls.urls();
                        string url = @urlalias + "loadfee/";
                        fm.type = "checkexist";
                        fm.medium_id = ddlmedium.SelectedValue.ToString();
                        fm.class_id = ddlclass.SelectedValue.ToString();
                        fm.dutype = ddltype.SelectedItem.ToString();
                        fm.ayid = Session["acdyear"].ToString();
                        fm.caste = ddltype.SelectedValue.ToString();
                        fm.table = DataTableToJSON(dt);

                        string jsonString = JsonHelper.JsonSerializer<FeeMaster>(fm);
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
                            string months = "";
                            string result = streamReader.ReadToEnd();
                            DataTable dataSet = JsonConvert.DeserializeObject<DataTable>(result);
                            if (dataSet.Rows.Count > 0)
                            {
                                for (int j = 0; j < dataSet.Rows.Count; j++)
                                {
                                    if (months == "")
                                    {
                                        months = dataSet.Rows[j]["duration"].ToString();
                                    }
                                    else
                                    {
                                        months = months + ',' + dataSet.Rows[j]["duration"].ToString();
                                    }
                                }
                                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Month " + months + " already defined', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                                flag = true;
                            }
                        }
                    }
                    else if (ddltype.SelectedValue.ToString() == "3")
                    {
                        dt.Rows.Add("");
                        dt.Rows[0]["duration"] = Session["id"].ToString();
                        useless();
                        string urlalias = cls.urls();
                        string url = @urlalias + "loadfee/";
                        fm.type = "checkexist";
                        fm.medium_id = ddlmedium.SelectedValue.ToString();
                        fm.class_id = ddlclass.SelectedValue.ToString();
                        fm.dutype = ddltype.SelectedItem.ToString();
                        fm.ayid = Session["acdyear"].ToString();
                        fm.caste = ddltype.SelectedValue.ToString();
                        fm.table = Session["id"].ToString();

                        string jsonString = JsonHelper.JsonSerializer<FeeMaster>(fm);
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
                            if (result.ToString().Contains("found") == true)
                            {
                                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Month already exist in another Quarter', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                                flag = true;
                            }
                        }
                    }
                    else if (ddltype.SelectedValue.ToString() == "6")
                    {
                        if (txtname.Text != "")
                        {
                            dt.Rows.Add("");
                            dt.Rows[0]["duration"] = txtname.Text;
                            useless();
                            string urlalias = cls.urls();
                            string url = @urlalias + "loadfee/";
                            fm.type = "checkexist";
                            fm.medium_id = ddlmedium.SelectedValue.ToString();
                            fm.class_id = ddlclass.SelectedValue.ToString();
                            fm.dutype = ddltype.SelectedItem.ToString();
                            fm.ayid = Session["acdyear"].ToString();
                            fm.caste = ddltype.SelectedValue.ToString();
                            fm.table = txtname.Text.ToString().Trim();  

                            string jsonString = JsonHelper.JsonSerializer<FeeMaster>(fm);
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
                                if (result.ToString().Contains("found") == true)
                                {
                                    flag = true;
                                    txtname.Text = "";
                                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Term with Same name already exist', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                                }
                                else if (result.ToString().Contains("exist") == true)
                                {
                                    flag = true;
                                    txtname.Text = "";
                                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Two terms are already defined', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                                }
                            }
                        }
                    }
                    else if (ddltype.SelectedValue.ToString() == "12")
                    {
                        dt.Rows.Add("");
                        dt.Rows[0]["duration"] = txtname.Text;
                        useless();
                        string urlalias = cls.urls();
                        string url = @urlalias + "loadfee/";
                        fm.type = "checkexist";
                        fm.medium_id = ddlmedium.SelectedValue.ToString();
                        fm.class_id = ddlclass.SelectedValue.ToString();
                        fm.dutype = ddltype.SelectedItem.ToString();
                        fm.ayid = Session["acdyear"].ToString();
                        fm.caste = ddltype.SelectedValue.ToString();

                        string jsonString = JsonHelper.JsonSerializer<FeeMaster>(fm);
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
                            if (result.ToString().Contains("found") == true)
                            {
                                flag = true;
                                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Year Already Defined', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                            }
                        }
                    }
                    if (flag == false)
                    {
                        useless();
                        string urlalias = cls.urls();
                        string url = @urlalias + "loadfee/";
                        fm.type = "saveduration";
                        fm.medium_id = ddlmedium.SelectedValue.ToString();
                        fm.class_id = ddlclass.SelectedValue.ToString();
                        fm.dutype = ddltype.SelectedItem.ToString();
                        fm.ayid = Session["acdyear"].ToString();
                        fm.user = Session["emp_id"].ToString();
                        fm.caste = ddltype.SelectedValue.ToString();
                        fm.table = DataTableToJSON(dt);

                        string jsonString = JsonHelper.JsonSerializer<FeeMaster>(fm);
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
                            if (result.ToString().Contains("success") == true)
                            {
                                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Saved Successfully', { color: '#fff', background: '#198104', blur: 0.2, delay: 0 });", true);
                                txtname.Text = "";
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('No changes made', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                                txtname.Text = "";
                            }
                        }
                    }
                    load();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Select Duration to Save', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                }
            }
            else
            {
                
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void btnclear_Click(object sender, EventArgs e)
    {
        try
        {
            ddlmedium.SelectedIndex = 0;
            ddlclass.SelectedIndex = 0;
            ddltype.SelectedIndex = 0;
            ddlclass.Enabled = false;
            ddltype.Enabled = false;
            lstmonth.Items.Clear();
            lstmonth.Enabled = false;
            grduration.DataSource = null;
            grduration.DataBind();
            multi.Style.Add("display", "block");
            text.Style.Add("display", "none");
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>multi();</script>", false);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }   

    [System.Web.Services.WebMethod(enableSession: true)]
    public static void Setsession(string id)
    {
        HttpContext.Current.Session["id"] = id;
    }

    public string DataTableToJSON(DataTable table)
    {
        string JSONString = string.Empty;
        JSONString = JsonConvert.SerializeObject(table);
        return JSONString;
    }

    protected void grduration_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string str = ((Label)e.Row.FindControl("lblflag")).Text;
                if (str == "1")
                {
                    ((LinkButton)e.Row.FindControl("btndelete")).Enabled = false;
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
        try
        {
            month();
            useless();
            string urlalias = cls.urls();
            string url = @urlalias + "loadfee/";
            fm.type = "gridload";
            fm.ayid = Session["acdyear"].ToString();
            fm.medium_id = ddlmedium.SelectedValue.ToString();
            fm.class_id = ddlclass.SelectedValue.ToString();
            fm.dutype = ddltype.SelectedItem.ToString();

            string jsonString = JsonHelper.JsonSerializer<FeeMaster>(fm);
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
                DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(result);
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    grduration.DataSource = dataSet.Tables[0];
                    grduration.DataBind();
                }
                else
                {
                    grduration.DataSource = null;
                    grduration.DataBind();
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('No fee duration defined', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void grduration_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            GridViewRow gr = (GridViewRow)grduration.Rows[e.RowIndex];
            string id = ((Label)gr.FindControl("lblid")).Text;
            string count = ((Label)gr.FindControl("lblflag")).Text;
            if (count == "1")
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Fees already assigned', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>multi();</script>", false);
            }
            else
            {
                useless();
                string urlalias = cls.urls();
                string url = @urlalias + "loadfee/";
                fm.type = "deletedur";
                fm.table = id.ToString();

                string jsonString = JsonHelper.JsonSerializer<FeeMaster>(fm);
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
                    if (result.ToString().Contains("success") == true)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Deleted Successfully', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('No changes made', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                    }
                    load();
                }
            }
           
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }
}