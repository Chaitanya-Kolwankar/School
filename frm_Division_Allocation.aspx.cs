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

public partial class frm_Division_Allocation : System.Web.UI.Page
{
    Class1 cls = new Class1();
    common cm = new common();
    Allocation ac = new Allocation();
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
                    btnclear.Enabled = false;
                    btnsave.Enabled = false;
                }
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
    }//done

    public void ddlfill()
    {
        try
        {
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

    }//done

    protected void ddlmedium_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
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
                gridstd.DataSource = null;
                gridstd.DataBind();
            }
            else
            {
                ddlclass.SelectedIndex = 0;
                ddlclass.Enabled = false;
                ddldiv.SelectedIndex = 0;
                ddldiv.Enabled = false;
                gridstd.DataSource = null;
                gridstd.DataBind();
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
                gridstd.DataSource = null;
                gridstd.DataBind();
            }
            else
            {
                ddldiv.SelectedIndex = 0;
                ddldiv.Enabled = false;
                gridstd.DataSource = null;
                gridstd.DataBind();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void ddldiv_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddldiv.SelectedIndex > 0)
            {
                DataSet ds = new DataSet();
                string urlalias = cls.urls();
                string url = @urlalias + "all/";

                ac.type = "studload";
                ac.medium = ddlmedium.SelectedValue.ToString();
                ac.classid = ddlclass.SelectedValue.ToString();
                ac.div = ddldiv.SelectedValue.ToString();
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
                        ds.Tables[0].Columns.Add("flag");
                        ds.Tables[0].Columns.Add("check");
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            ds.Tables[0].Rows[i]["check"] = "0";
                            ds.Tables[0].Rows[i]["flag"] = "0";
                        }
                        ViewState["dt"] = ds.Tables[0];
                        ViewState["sort"] = "ASC";
                        this.Bindgrid();
                        btnsave.Enabled = true;
                        btnclear.Enabled = true;
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Students not found', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                    }
                }
            }
            else
            {
                gridstd.DataSource = null;
                gridstd.DataBind();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    public void Bindgrid()
    {
        gridstd.DataSource = ViewState["dt"] as DataTable;
        gridstd.DataBind();
    }

    protected void gridstd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (((TextBox)e.Row.FindControl("txtdivid")).Text != "")
                    {
                        ((CheckBox)e.Row.FindControl("chkassign")).Checked = true;
                        ((TextBox)e.Row.FindControl("txtcheck")).Text = "1";
                        e.Row.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        ((TextBox)e.Row.FindControl("txtroll")).Enabled = false;
                        ((TextBox)e.Row.FindControl("txtcheck")).Text = "0";
                    }
                }
            }
            else
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (((TextBox)e.Row.FindControl("txtdivid")).Text != "")
                    {
                        ((CheckBox)e.Row.FindControl("chkassign")).Checked = true;
                        ((TextBox)e.Row.FindControl("txtcheck")).Text = "1";
                        e.Row.ForeColor = System.Drawing.Color.Green;
                    }
                    else if (((TextBox)e.Row.FindControl("txtcheck")).Text == "1")
                    {
                        ((CheckBox)e.Row.FindControl("chkassign")).Checked = true;
                    }
                    else
                    {
                        ((TextBox)e.Row.FindControl("txtroll")).Enabled = false;
                        ((TextBox)e.Row.FindControl("txtcheck")).Text = "0";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    public void clear()
    {
        try
        {
            gridstd.DataSource = null;
            gridstd.DataBind();
            ddldiv.SelectedIndex = 0;
            ddlclass.SelectedIndex = 0;
            ddlmedium.SelectedIndex = 0;
            ddlclass.Enabled = false;
            ddldiv.Enabled = false;
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void btnclear_Click(object sender, EventArgs e)
    {
        clear();
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Student_id");
            dt.Columns.Add("Roll_no");
            dt.Columns.Add("division_id");

            for (int i = 0; i < gridstd.Rows.Count; i++)
            {
                if (((TextBox)gridstd.Rows[i].FindControl("txtflag")).Text == "1" && ((CheckBox)gridstd.Rows[i].FindControl("chkassign")).Checked == true)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = ((Label)gridstd.Rows[i].FindControl("lblstud")).Text;
                    dr[1] = ((TextBox)gridstd.Rows[i].FindControl("txtroll")).Text.Trim();
                    dr[2] = ddldiv.SelectedValue.ToString();
                    dt.Rows.Add(dr);
                }
                else if (((TextBox)gridstd.Rows[i].FindControl("txtflag")).Text == "1" && ((CheckBox)gridstd.Rows[i].FindControl("chkassign")).Checked == false)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = ((Label)gridstd.Rows[i].FindControl("lblstud")).Text;
                    dr[1] = "";
                    dr[2] = "";
                    dt.Rows.Add(dr);
                }
            }

            if (dt.Rows.Count > 0)
            {
                string json = DataTableToJSON(dt);

                string urlalias = cls.urls();
                string url = @urlalias + "all/";

                ac.type = "save";
                ac.medium = ddlmedium.SelectedValue.ToString();
                ac.classid = ddlclass.SelectedValue.ToString();
                ac.ayid = Session["acdyear"].ToString();
                ac.div = json.ToString();

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
                    if (result.ToString().Contains("Update") == true)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Saved Successfully', { color: '#fff', background: '#198104', blur: 0.2, delay: 0 });", true);
                        clear();
                    }
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('No Changes Made', { color: '#fff', background: '#198104', blur: 0.2, delay: 0 });", true);
                clear();
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    public string DataTableToJSON(DataTable table)
    {
        string JSONString = string.Empty;
        JSONString = JsonConvert.SerializeObject(table);
        return JSONString;
    }

    protected void gridstd_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            DataTable dt = ViewState["dt"] as DataTable;
            dt.Rows.Clear();
            for (int i = 0; i < gridstd.Rows.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = ((Label)gridstd.Rows[i].FindControl("lblform")).Text;
                dr[1] = ((Label)gridstd.Rows[i].FindControl("lblstud")).Text;
                dr[2] = ((Label)gridstd.Rows[i].FindControl("lblname")).Text;
                dr[3] = ((TextBox)gridstd.Rows[i].FindControl("txtroll")).Text.Trim();
                dr[4] = ((TextBox)gridstd.Rows[i].FindControl("txtdivid")).Text;
                dr[5] = ((TextBox)gridstd.Rows[i].FindControl("txtflag")).Text;
                dr[6] = ((TextBox)gridstd.Rows[i].FindControl("txtcheck")).Text;

                dt.Rows.Add(dr);
            }
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToString(ViewState["sort"]).ToUpper() == "ASC")
                {
                    dt.DefaultView.Sort = e.SortExpression + " Desc";
                    ViewState["sort"] = "DESC";
                }
                else
                {
                    dt.DefaultView.Sort = e.SortExpression + " Asc";
                    ViewState["sort"] = "ASC";
                }
            }
            ViewState["dt"] = dt;
            this.Bindgrid();

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }
}