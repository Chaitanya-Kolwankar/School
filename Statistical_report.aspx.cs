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
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Statistical_report : System.Web.UI.Page
{
    common cm = new common();
    Class1 cls = new Class1();
    statistical_rpt srpt = new statistical_rpt();
    DataTable dt1;
    DataTable dt2;
    DataTable dt3;
    DataTable dt4;
    DataTable dt5;
    DataTable dt6;
    DataTable dt7;
    DataTable dt8;
    DataTable dt9;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                if (Convert.ToString(Session["emp_id"]) == "")
                {
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    //module1();
                    medium_select();
                }
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
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

    private void medium_select()
    {
        try
        {

            string type = "ddlfill";

            string urlalias = cls.urls();
            string url = @urlalias + "Common/";
            //string url = @"http://203.192.254.34/Utkarsha_api1/utkarsha_api/Common/";
            cm.type = type.ToString();

            string jsonString = JsonHelper.JsonSerializer<common>(cm);


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

                ddlmedium1.DataSource = dt1;
                ddlmedium1.DataTextField = "medium";
                ddlmedium1.DataValueField = "med_id";
                ddlmedium1.DataBind();
                ddlmedium1.Items.Insert(0, "--Select--");
                ddlmedium1.SelectedIndex = 0;

                ddlmedium2.DataSource = dt1;
                ddlmedium2.DataTextField = "medium";
                ddlmedium2.DataValueField = "med_id";
                ddlmedium2.DataBind();
                ddlmedium2.Items.Insert(0, "--Select--");
                ddlmedium2.SelectedIndex = 0;


                ddlmedium3.DataSource = dt1;
                ddlmedium3.DataTextField = "medium";
                ddlmedium3.DataValueField = "med_id";
                ddlmedium3.DataBind();
                ddlmedium3.Items.Insert(0, "--Select--");
                ddlmedium3.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void ddlmedium_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string medium = ddlmedium.SelectedValue.ToString();

            load(medium);
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    private void load(string medium)
    {
        try
        {
            string type = "datewise";
            string ayid = Session["acdyear"].ToString();


            string urlalias = cls.urls();
            string url = @urlalias + "statisticalreport/";
            //string url = @"http://localhost:9199/statisticalrpt/";
            srpt.type = type.ToString();
            srpt.medium = medium;
            srpt.ayid = ayid;
            string jsonString = JsonHelper.JsonSerializer<statistical_rpt>(srpt);


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
                DataSet datewise = JsonConvert.DeserializeObject<DataSet>(result);

                dt1 = datewise.Tables[0];
                dt2 = datewise.Tables[1];
                dt3 = datewise.Tables[2];
            }
            if (dt3.Rows.Count > 0)
            {
                lblapplicant.Text = dt1.Rows[0]["Count"].ToString();
                lbltotal.Text = dt2.Rows[0]["Count"].ToString();
                Grd_datewise.DataSource = dt3;
                Grd_datewise.DataBind();
                datecard.Visible = true;
            }
            else if (dt3.Rows.Count == 0)
            {
                datecard.Visible = false;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify( 'No data found', { color: '#fff', background: '#D9534F', blur: 0.2, delay: 0 })", true);
                return;
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }

    }

    private void grp_load(string medium)
    {
        try
        {
            string type = "sgvfill";
            string ayid = Session["acdyear"].ToString();

            string urlalias = cls.urls();
            string url = @urlalias + "statisticalreport/";
            //   string url = @"http://localhost:9199/statisticalrpt/";
            srpt.type = type.ToString();
            srpt.medium = medium;
            srpt.ayid = ayid;
            string jsonString = JsonHelper.JsonSerializer<statistical_rpt>(srpt);


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
                DataSet datewise = JsonConvert.DeserializeObject<DataSet>(result);

                dt4 = datewise.Tables[0];
                dt5 = datewise.Tables[1];
                dt6 = datewise.Tables[2];
            }
            if (dt6.Rows.Count > 0)
            {
                lblgrpapp.Text = dt4.Rows[0]["Count"].ToString();
                lblgrptotal.Text = dt5.Rows[0]["Count"].ToString();
                Grd_grpwise.DataSource = dt6;
                Grd_grpwise.DataBind();
                grpcard.Visible = true;
            }
            else if (dt6.Rows.Count == 0)
            {
                grpcard.Visible = false;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify( 'No data found', { color: '#fff', background: '#D9534F', blur: 0.2, delay: 0 })", true);
                return;
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    private void canceldate(string medium)
    {
        try
        {
            string type = "cgvfill";
            string ayid = Session["acdyear"].ToString();


            string urlalias = cls.urls();
            string url = @urlalias + "statisticalreport/";
            // string url = @"http://localhost:9199/statisticalrpt/";
            srpt.type = type.ToString();
            srpt.medium = medium;
            srpt.ayid = ayid;
            string jsonString = JsonHelper.JsonSerializer<statistical_rpt>(srpt);
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
                DataSet datewise = JsonConvert.DeserializeObject<DataSet>(result);
                dt7 = datewise.Tables[0];
                dt8 = datewise.Tables[1];

            }
            if (dt8.Rows.Count > 0)
            {
                lblcancel.Text = dt7.Rows[0]["Count"].ToString();
                gridcancel.DataSource = dt8;
                gridcancel.DataBind();
                cancard.Visible = true;
            }
            else if (dt8.Rows.Count == 0)
            {
                cancard.Visible = false;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify( 'No data found', { color: '#fff', background: '#D9534F', blur: 0.2, delay: 0 })", true);
                return;
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void ddlmedium1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string medium = ddlmedium1.SelectedValue.ToString();

            grp_load(medium);
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void ddlmedium2_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string medium = ddlmedium2.SelectedValue.ToString();

            canceldate(medium);
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void ddlmedium3_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string medium = ddlmedium3.SelectedValue.ToString();

            graph(medium);
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    [WebMethod]
    public static string graph(string ayid)
    {
      
        classWebMethods webCls = new classWebMethods();
        return webCls.graph( HttpContext.Current.Session["acdyear"].ToString());
        
    }

    
}