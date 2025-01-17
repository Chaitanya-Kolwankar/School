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
using ClosedXML;
using System.Drawing;

public partial class GR_report : System.Web.UI.Page
{
    Class1 cls = new Class1();
    common cm = new common();
    GR_rpt grp = new GR_rpt();
    DataTable dt = new DataTable();
    string display;
    string msg1;
    DataSet ds;

    string date;
    string medium1;
    string clss;

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
                    medium_select();
                    gridcard3.Visible = false;
                    grid_card.Visible = false;
                  
                }
            }

            if (grid1.Rows.Count > 0)
            {
                grid1.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            if (grid2.Rows.Count > 0)
            {
                grid2.HeaderRow.TableSection = TableRowSection.TableHeader;
            }

            if (GridView1.Rows.Count > 0)
            {
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
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
                // string url = @"http://203.192.254.34/Utkarsha_api1/utkarsha_api/Common/";
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
            if (ddlmedium.SelectedItem.Text != "Select")
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
                        Session["tab"] = table;
                    }
                }

            }
            gridcard3.Visible = false;
            grid_card.Visible = false;
            grid1.DataSource = null;
            grid1.DataBind();
            grid2.DataSource = null;
            grid2.DataBind();
            GridView1.DataSource = null;
            GridView1.DataBind();
            GridView2.DataSource = null;
            GridView2.DataBind();
            exportexcel.Visible = false;
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    public void class_sel()
    {
        try
        {

            if (ddlmedium.SelectedItem.Text != "--Select--" && ddlclass.SelectedItem.Text != "--Select--")
            {
                string medium = ddlmedium.SelectedValue;
                string cls1 = ddlclass.SelectedValue;
                string ayid = Session["acdyear"].ToString();

                string urlalias = cls.urls();
                string url = @urlalias + "grrpt/";
                // string url = @"http://localhost:9199/grrpt/";
                grp.medium = medium;
                grp.standard = cls1;
                grp.ayid = ayid;
                grp.type = "getexl";

                string jsonString = JsonHelper.JsonSerializer<GR_rpt>(grp);


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
                    ds = JsonConvert.DeserializeObject<DataSet>(result);
                    dt = ds.Tables[0];
                    date = Convert.ToString(DateTime.Today.ToString("dd-MM-yyyy"));
                    Session["date"] = date;
                    medium1 = ddlmedium.SelectedItem.Text;
                    Session["medium1"] = medium1;
                    clss = ddlclass.SelectedItem.Text;
                    Session["clss"] = clss;
                }




                if (dt.Rows.Count > 0)
                {
                    //ViewState["dta"] = dt;
                    grid1.DataSource = dt;
                    grid1.DataBind();
                    grid2.DataSource = dt;
                    grid2.DataBind();
                    GridView2.DataSource = null;
                    GridView2.DataBind();
                    gridcard3.Visible = false;
                    grid_card.Visible = true;
                    exportexcel.Visible = true;
                    
                }


                if (dt.Rows.Count == 0)
                {
                    grid_card.Visible = false;
                    display = "No data found for this standard";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify( '" + display + "', { color: '#fff', background: '#FF2900', blur: 0.2, delay: 0 })", true);
                    return;
                }
                else if (display == "no")
                {
                    grid_card.Visible = false;
                    display = "No data found for this standard and medium select another";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify( '" + display + "', { color: '#fff', background: '#FF2900', blur: 0.2, delay: 0 })", true);
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    public void id_sel()
    {
        try
        {
            if (ddlmedium.SelectedItem.Text != "--Select--" && ddlclass.SelectedItem.Text != "--Select--")
            {
                string medium = ddlmedium.SelectedValue;
                string cls1 = ddlclass.SelectedValue;
                string ayid = Session["acdyear"].ToString();

                string urlalias = cls.urls();
                string url = @urlalias + "grrpt/";
                //string url = @"http://localhost:9199/grrpt/";
                grp.medium = medium;
                grp.standard = cls1;
                grp.ayid = ayid;
                grp.type = "getidexl";

                string jsonString = JsonHelper.JsonSerializer<GR_rpt>(grp);


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
                    ds = JsonConvert.DeserializeObject<DataSet>(result);
                    dt = ds.Tables[0];
                    date = Convert.ToString(DateTime.Today.ToString("dd-MM-yyyy"));
                    Session["date"] = date;
                    medium1 = ddlmedium.SelectedItem.Text;
                    Session["medium1"] = medium1;
                    clss = ddlclass.SelectedItem.Text;
                    Session["clss"] = clss;
                }


                if (dt.Rows.Count > 0)
                {
                    ViewState["dta"] = dt;
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    GridView2.DataSource = dt;
                    GridView2.DataBind();
                    grid2.DataSource = null;
                    grid2.DataBind();
                    grid_card.Visible = false;
                    gridcard3.Visible = true;
                    exportexcel.Visible = true;
                  
                }



                if (dt.Rows.Count == 0)
                {
                    grid_card.Visible = false;
                    display = "No data found for this standard";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify( '" + display + "', { color: '#fff', background: '#FF2900', blur: 0.2, delay: 0 })", true);
                    return;
                }
                else if (display == "no")
                {
                    grid_card.Visible = false;
                    display = "No data found for this standard and medium select another";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify( '" + display + "', { color: '#fff', background: '#FF2900', blur: 0.2, delay: 0 })", true);
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }
  
    protected void getexcel_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlmedium.SelectedItem.Text == "--Select--")
            {
                display = "Please Select medium";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify( '" + display + "', { color: '#fff', background: '#FF2900', blur: 0.2, delay: 0 })", true);
                return;
            }
            if (ddlclass.SelectedItem.Text == "--SELECT--")
            {
                display = "Please Select Class";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify( '" + display + "', { color: '#fff', background: '#FF2900', blur: 0.2, delay: 0 })", true);
                return;
            }
            else
            {
                class_sel();
                if (grid1.Rows.Count > 0)
                {
                    grid1.HeaderRow.TableSection = TableRowSection.TableHeader;
                }

                if (grid2.Rows.Count > 0)
                {
                    grid2.HeaderRow.TableSection = TableRowSection.TableHeader;
                }


                
                exportexcel.Enabled = true;
                //display = "Select Standard and medium";
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify( '" + display + "', { color: '#fff', background: '#FF2900', blur: 0.2, delay: 0 })", true);
                //return;
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void getidexcel_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlmedium.SelectedItem.Text == "--Select--")
            {
                display = "Please Select medium";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify( '" + display + "', { color: '#fff', background: '#FF2900', blur: 0.2, delay: 0 })", true);
                return;
            }
            if (ddlclass.SelectedItem.Text == "--SELECT--")
            {
                display = "Please Select Class";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify( '" + display + "', { color: '#fff', background: '#FF2900', blur: 0.2, delay: 0 })", true);
                return;
            }
            else
            {
                id_sel();
                if (grid1.Rows.Count > 0)
                {
                    grid1.HeaderRow.TableSection = TableRowSection.TableHeader;
                }

                if (grid2.Rows.Count > 0)
                {
                    grid2.HeaderRow.TableSection = TableRowSection.TableHeader;
                }


                if (GridView1.Rows.Count > 0)
                {
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                }


                exportexcel.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
      
    }

    protected void exportexcel_Click(object sender, EventArgs e)
    {

        getexcel.Enabled = false;
        excl();
    }
       

    private void excl()
    {
       
            if (grid2.Rows.Count > 0)
            {
                Response.Clear();
                Response.Buffer = true;
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Charset = "";
                string FileName = "GR Report" + DateTime.Now + ".xls";
                StringWriter strwritter = new StringWriter();
                HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);

                grid2.GridLines = GridLines.Both;
                grid2.HeaderStyle.Font.Bold = true;
                for (int i = 0; i < grid2.Rows.Count; i++)
                {
                    GridViewRow row = grid2.Rows[i];
                    //Apply text style to each Row
                    row.Attributes.Add("class", "textmode");
                }
                grid2.RenderControl(htmltextwrtter);
                string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                Response.Write(style);
                Response.Write(strwritter.ToString());
                Response.End();
            }
            else if (GridView2.Rows.Count > 0)
            {
                Response.Clear();
                Response.Buffer = true;
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Charset = "";
                string FileName = "ID Card Data" + DateTime.Now + ".xls";
                StringWriter strwritter = new StringWriter();
                HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
                GridView2.GridLines = GridLines.Both;
                GridView2.HeaderStyle.Font.Bold = true;
                for (int i = 0; i < GridView2.Rows.Count; i++)
                {
                    GridViewRow row = GridView2.Rows[i];
                    //Apply text style to each Row
                    row.Attributes.Add("class", "textmode");
                }
                GridView2.RenderControl(htmltextwrtter);
                string style = @"<style> .textmode { mso-number-format:0; } </style>";
                Response.Write(style);
                Response.Write(strwritter.ToString());
                Response.End();
            }
            else
            {
                display = "get data by clicking Get Data or Get ID Card Data";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify( '" + display + "', { color: '#fff', background: '#FF2900', blur: 0.2, delay: 0 })", true);
                return;
            }
        
       



    }
       
    protected void grid2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            Label dtee = (e.Row.FindControl("dte") as Label);
            if (dtee != null)
            {
                dtee.Text = Session["date"].ToString();
            }

            Label medd = (e.Row.FindControl("med") as Label);
            if (medd != null)
            {
                medd.Text = Session["medium1"].ToString();
            }

            Label cslss = (e.Row.FindControl("cls") as Label);
            if (cslss != null)
            {
                cslss.Text = Session["clss"].ToString();
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            Label dtee = (e.Row.FindControl("dte") as Label);
            if (dtee != null)
            {
                dtee.Text = Session["date"].ToString();
            }

            Label medd = (e.Row.FindControl("med") as Label);
            if (medd != null)
            {
                medd.Text = Session["medium1"].ToString();
            }

            Label cslss = (e.Row.FindControl("cls") as Label);
            if (cslss != null)
            {
                cslss.Text = Session["clss"].ToString();
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void ddlclass_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            gridcard3.Visible = false;
            grid_card.Visible = false;
            grid1.DataSource = null;
            grid1.DataBind();
            grid2.DataSource = null;
            grid2.DataBind();
            GridView1.DataSource = null;
            GridView1.DataBind();
            GridView2.DataSource = null;
            GridView2.DataBind();
            exportexcel.Visible = false;
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void clear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }
}