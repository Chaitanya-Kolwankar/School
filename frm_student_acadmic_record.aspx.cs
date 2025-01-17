using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frm_student_acadmic_record : System.Web.UI.Page
{
    DataTable firstTable1;
    std st = new std();
    Class1 cls = new Class1();
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
    protected void Page_Load(object sender, EventArgs e)
    {

        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "MyFunction()", true);
        try
        {
            if (Session["emp_id"].ToString() == "")
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                   
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "MyFunction()", true);
                }
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void id_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (id.Text == "")
            {
                notifys("please enter id", "#D9534F");
            }
            else
            {
                name.Text = "";
                std st = new std();
                st.type = "load";
                string idd = id.Text;
                st.id = idd;
                string urlalias = cls.urls();
                string url = @urlalias + "loaddata/";
                //string url = "http://localhost:9199/loaddata/";
                string jsonString = JsonHelper.JsonSerializer<std>(st);
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
                    DataTable firstTable = dataSet.Tables[0];
                    GridView2.DataSource = dataSet.Tables["adm_student_master"];
                    GridView2.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void name_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (name.Text == "")
            {
                notifys("please enter name", "#D9534F");
            }
            else
            {
                id.Text = "";
                st.type = "load";
                string[] str = name.Text.Trim().Split(' ');
                if (str[0].Trim() != "")
                {
                    st.name = str[0];
                }
                if (str.Length > 1)
                {
                    if (str[1].Trim() != "")
                    {
                        st.firstname = str[1];
                    }
                }

                string urlalias = cls.urls();
                string url = @urlalias + "loaddata/";
                //string url = "http://localhost:9199/loaddata/";
                string jsonString = JsonHelper.JsonSerializer<std>(st);
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
                    firstTable1 = dataSet.Tables[0];
                    GridView2.DataSource = dataSet.Tables["adm_student_master"];
                    GridView2.DataBind();
                    if (dataSet.Tables[0].Rows.Count != 0)
                    {
                        GridView2.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                }

            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    public void notifys(string msg, string color)
    {
        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + msg + "', { color: '#fff', background: '" + color + "', blur: 0.2, delay: 0 });", true);
    }

    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "select")
            {
                int RowIndex = Convert.ToInt32(e.CommandArgument.ToString());
                GridViewRow row = GridView2.Rows[RowIndex];
                st.type = "load1";
                string idd = row.Cells[0].Text.ToString();
                st.id = idd;

                string urlalias = cls.urls();
                string url = @urlalias + "loaddata/";
                //  string url = "http://localhost:9199/loaddata/";
                string jsonString = JsonHelper.JsonSerializer<std>(st);
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
                    DataTable firstTable = dataSet.Tables[0];
                    Label1.Text = row.Cells[0].Text.ToString();
                    lbl1.Text = row.Cells[1].Text.ToString();
                    Label2.Text = firstTable.Rows[0][1].ToString();
                    grd_report.DataSource = dataSet.Tables["view_student_academic_records"];
                    grd_report.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }

    }

}