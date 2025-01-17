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

public partial class Studentdetailed_report : System.Web.UI.Page
{
    DataTable dt = new DataTable();
    Allocation ac = new Allocation();
    Class1 cls = new Class1();
    common sbm = new common();
    studentdetrpt sd = new studentdetrpt();
    string display;

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

    private void useless()
    {
        sd.med_id = null;
        sd.std_id = null;
        sd.div_id = null;
        sd.fields = null;
        sd.AYID = null;

    }

    private void medium_select()
    {
        try
        {
            string type = "ddlfill";
            string urlalias = cls.urls();
            string url = @urlalias + "Common/";


            sbm.type = type.ToString();

            string jsonString = JsonHelper.JsonSerializer<common>(sbm);


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

                ddlmedium.DataSource = dt1;
                ddlmedium.DataTextField = "medium";
                ddlmedium.DataValueField = "med_id";
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

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.btn_excel);
        if (!Page.IsPostBack)
        {
            medium_select();
        }

        if (grid1.Rows.Count > 0)
        {
            grid1.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

    }

    protected void ddlmedium_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            btn_excel.Enabled = false;
            grid1.DataSource = null;
            grid1.DataBind();
            ddlclass.DataSource = "";
            ddlclass.DataBind();
            ddldiv.DataSource = "";
            ddldiv.DataBind();
            foreach (ListItem item in ddlfield.Items)
            {
                item.Selected = false;  
            }

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

            }
            else
            {
                ddlclass.DataSource = "";
                ddlclass.DataBind();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void ddlclass_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            btn_excel.Enabled = false;
            grid1.DataSource = null;
            grid1.DataBind();
            ddldiv.DataSource = "";
            ddldiv.DataBind();
            foreach (ListItem item in ddlfield.Items)
            {
                item.Selected = false;
            }
            if (ddlclass.SelectedIndex > 0)
            {
                DataSet ds = new DataSet();
                string urlalias = cls.urls();
                string url = @urlalias + "all/";

                ac.type = "divload";
                ac.medium = ddlmedium.SelectedValue.ToString();
                ac.classid = ddlclass.SelectedValue.ToString();
                ac.ayid = Session["acdyear"].ToString();

                string jsonString = JsonHelper.JsonSerializer<Allocation>(ac);
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
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddldiv.DataSource = ds.Tables[0];
                        ddldiv.DataTextField = "division_name";
                        ddldiv.DataValueField = "division_id";
                        ddldiv.DataBind();
                        ddldiv.Items.Insert(0, "--Select--");
                        ddldiv.SelectedIndex = 0;
                        ddldiv.Enabled = true;
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('No Divison Found', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                        if (ddldiv.SelectedIndex > 0)
                        {
                            ddldiv.SelectedIndex = 0;
                        }
                        ddldiv.Enabled = false;
                    }
                }
                grid1.DataSource = null;
                grid1.DataBind();
            }
            else
            {
                ddldiv.SelectedIndex = 0;
                ddldiv.Enabled = false;
                grid1.DataSource = null;
                grid1.DataBind();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }
    public void fillgrd()
    {
        btn_excel.Enabled = false;
        useless();
        try
        {
            string strfields = "";
            foreach(ListItem item in  ddlfield.Items)
            {
                if (item.Selected)
                {
                    
                        strfields += ","+ item.Value;
                    
                }
            }

            if (ddlmedium.SelectedIndex > 0)
            {
                sd.med_id = ddlmedium.SelectedValue;
            }
            if (ddlclass.SelectedIndex > 0)
            {
                sd.std_id = ddlclass.SelectedValue;
            }
            if (ddldiv.SelectedIndex > 0)
            {
                sd.div_id = ddldiv.SelectedValue;
            }
            if(strfields!="")
            {
                sd.fields = strfields;
            }
           

            string urlalias = cls.urls();
            string url = @urlalias + "studentdetailrpt/";

            sd.AYID = Session["acdyear"].ToString();


            string jsonString = JsonHelper.JsonSerializer<studentdetrpt>(sd);


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
                DataSet ds = JsonConvert.DeserializeObject<DataSet>(result);
                dt = ds.Tables[0];

                grid1.DataSource =dt;
                grid1.DataBind();
                dt = ds.Tables[0];
                grid_card.Visible = true;
                btn_excel.Enabled = true;
            }
            if (dt.Rows.Count == 0)
            {
                btn_excel.Enabled = false;
                grid_card.Visible = false;
                display = "No data found for this Medium and Class ";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify( '" + display + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                return;
            }
            else if (display == "no")
            {
                btn_excel.Enabled = false;
                grid_card.Visible = false;
                display = "No data found for this Medium and Class select another";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify( '" + display + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                return;
            }
        }




        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }




    protected void btn_get_Click(object sender, EventArgs e)
    {
        fillgrd();
        if (grid1.Rows.Count > 0)
        {
            grid1.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    protected void btn_excel_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        string subname = "";
        if (ddlmedium.SelectedIndex > 0)
        {
            subname = ddlmedium.SelectedItem.Text;
        }
        if (ddlclass.SelectedIndex > 0)
        {
            subname += " " + ddlclass.SelectedItem.Text;
        }
        if (ddldiv.SelectedIndex > 0)
        {
            subname += " " + ddldiv.SelectedItem.Text;
        }
        string FileName = "Student Detailed Report " + subname +" "+ DateTime.Now + ".xls";
        StringWriter strwritter = new StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        grid1.GridLines = GridLines.Both;
        grid1.HeaderStyle.Font.Bold = true;
        for (int i = 0; i < grid1.Rows.Count; i++)
        {
            GridViewRow row = grid1.Rows[i];
            //Apply text style to each Row
            row.Attributes.Add("class", "textmode");
        }
        grid1.RenderControl(htmltextwrtter);
        string style = @"<style> td { mso-number-format:\@; } </style>";
        Response.Write(style);
        Response.Write(strwritter.ToString());
        Response.End();
        
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }


    protected void ddldiv_SelectedIndexChanged(object sender, EventArgs e)
    {
        btn_excel.Enabled = false;
        grid1.DataSource = null;
        grid1.DataBind();
        foreach (ListItem item in ddlfield.Items)
        {
            item.Selected = false;
        }
    }

    protected void btn_clear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }



    protected void ddlfield_SelectedIndexChanged(object sender, EventArgs e)
    {
        btn_excel.Enabled = false;
        grid1.DataSource = null;
        grid1.DataBind();
        foreach (ListItem item in ddlfield.Items)
        {
            item.Selected = false;
        }
    }
}