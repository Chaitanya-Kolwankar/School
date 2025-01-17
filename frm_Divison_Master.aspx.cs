using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
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

public partial class frm_Divison_Master : System.Web.UI.Page
{
    Class1 cls = new Class1();
    DivisonMasterClass div = new DivisonMasterClass();

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
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }//done

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

    public void useless()
    {
        div.type = null;
        div.medium = null;
        div.classid = null;
        div.ayid = null;
        div.division_id = null;
        div.division_name = null;
        div.user_id = null;
        div.prevayid = null;
    }//done

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

                DataTable dt3 = dataSet.Tables[1];
                ddlprev.DataSource = dt3;
                ddlprev.DataTextField = "duration";
                ddlprev.DataValueField = "AYID";
                ddlprev.DataBind();
                ddlprev.Items.Insert(0, "--Select--");
                ddlprev.SelectedIndex = 0;
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
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }//done

    protected void ddlclass_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlmedium.SelectedIndex > 0)
            {
                if (ddlclass.SelectedIndex > 0)
                {
                    ViewState["dt"] = null;
                    useless();
                    grd_divison.Enabled = true;
                    string urlalias = cls.urls();
                    string url = @urlalias + "loadgrid/";
                    div.type = "LoadGrid";
                    div.medium = ddlmedium.SelectedValue.ToString();
                    div.classid = ddlclass.SelectedValue.ToString();
                    div.ayid = Session["acdyear"].ToString();
                    div.prevayid = Session["acdyear"].ToString();

                    string jsonString = JsonHelper.JsonSerializer<DivisonMasterClass>(div);
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
                        if (dataSet.Tables["gridload"].Rows.Count > 0)
                        {
                            dataSet.Tables[0].Columns.Add("action");
                            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                            {
                                dataSet.Tables[0].Rows[i]["action"] = "update";
                            }
                            ViewState["dt"] = dataSet.Tables["gridload"];
                            this.BindGrid();
                        }
                        else
                        {
                            DataTable dt = new DataTable();
                            dt.Columns.Add("division_id");
                            dt.Columns.Add("division_name");
                            dt.Columns.Add("flag");
                            dt.Columns.Add("action");
                            ViewState["dt"] = dt;
                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('No Division Found', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                            grd_divison.DataSource = null;
                            grd_divison.DataBind();
                        }
                    }
                }
                else
                {
                    grd_divison.DataSource = null;
                    grd_divison.DataBind();
                    ViewState["dt"] = null;
                }
            }
            else
            {
                ddlclass.SelectedIndex = 0;
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Select Medium', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }//done

    protected void chkprev_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (chkprev.Checked == true)
            {
                if (ddlmedium.SelectedIndex < 1 || ddlclass.SelectedIndex < 1)
                {
                    if (ddlmedium.SelectedIndex < 1)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Select the medium', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                        ddlmedium.Focus();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Select the Class', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                        ddlclass.Focus();
                    }
                    chkprev.Checked = false;
                    return;
                }
                string urlalias = cls.urls();
                string url = @urlalias + "loadgrid/";
                div.type = "LoadGrid";
                div.medium = ddlmedium.SelectedValue.ToString();
                div.classid = ddlclass.SelectedValue.ToString();
                div.ayid = Session["acdyear"].ToString();
                div.prevayid = Session["acdyear"].ToString();

                string jsonString = JsonHelper.JsonSerializer<DivisonMasterClass>(div);
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
                    if (dataSet.Tables["gridload"].Rows.Count > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Divisions already defined for current year', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                        chkprev.Checked = false;
                        return;
                    }
                }

                DataTable dt = ViewState["dt"] as DataTable;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["division_id"].ToString() == "" && dt.Rows[i]["division_name"].ToString() == "")
                    {
                        grd_divison.EditIndex = -1;
                    }
                }
                grd_divison.DataSource = null;
                grd_divison.DataBind();
                ddlprev.Enabled = true;
                ddlclass.Enabled = false;
                ddlmedium.Enabled = false;
                btnadd.Enabled = false;

            }
            else
            {
                ddlprev.SelectedIndex = 0;
                ddlprev.Enabled = false;
                ddlclass.Enabled = true;
                ddlmedium.Enabled = true;
                ddlclass.SelectedIndex = 0;
                grd_divison.DataSource = null;
                grd_divison.DataBind();
                btnadd.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }//done

    protected void ddlprev_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlprev.SelectedIndex > 0)
            {
                ViewState["dt"] = null;
                this.BindGrid();
                useless();
                string urlalias = cls.urls();
                string url = @urlalias + "loadgrid/";
                div.type = "LoadGrid";
                div.medium = ddlmedium.SelectedValue.ToString();
                div.classid = ddlclass.SelectedValue.ToString();
                div.ayid = Session["acdyear"].ToString();
                div.prevayid = ddlprev.SelectedValue.ToString();


                string jsonString = JsonHelper.JsonSerializer<DivisonMasterClass>(div);
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
                        for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                        {
                            dataSet.Tables[0].Rows[i]["division_id"] = "";
                        }
                        grd_divison.DataSource = dataSet.Tables["gridload"];
                        grd_divison.DataBind();
                        grd_divison.Enabled = false;
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('No division Found', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
                    }
                }
            }
            else
            {
                grd_divison.DataSource = null;
                grd_divison.DataBind();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }//done

    public void clear()
    {
        try
        {
            ddlmedium.SelectedIndex = 0;
            ddlclass.SelectedIndex = 0;
            ddlprev.SelectedIndex = 0;
            grd_divison.DataSource = null;
            grd_divison.DataBind();
            chkprev.Checked = false;
            ddlprev.Enabled = false;
            ddlclass.Enabled = true;
            ddlmedium.Enabled = true;
            btnadd.Enabled = true;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }//done

    protected void btnclear_Click(object sender, EventArgs e)
    {
        clear();
    }//done

    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("div");
            dt.Columns.Add("name");
            dt.Columns.Add("action");
            if (chkprev.Checked != true)
            {
                DataTable check = ViewState["dt"] as DataTable;
                for (int i = 0; i < check.Rows.Count; i++)
                {
                    if (check.Rows[i]["division_name"].ToString() == "")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Division Name cannot be empty', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
                        grd_divison.EditIndex = i;
                        this.BindGrid();
                        return;
                    }
                }
            }
            string json = "";
            bool chkup = false, chkins = false;
            DataSet ds = new DataSet();
            if (ddlmedium.SelectedIndex == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Please Select the Medium', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
                ddlmedium.Focus();
                return;
            }
            if (ddlclass.SelectedIndex == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Please Select the Class', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
                ddlclass.Focus();
                return;
            }
            else
            {
                if (chkprev.Checked == true)
                {
                    if (ddlprev.SelectedIndex == 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Please Select the Previous year', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
                        ddlprev.Focus();
                        grd_divison.DataSource = null;
                        return;
                    }

                    foreach (GridViewRow row in grd_divison.Rows)
                    {
                        if (((Label)row.FindControl("lblflag")).Text == "0" || ((Label)row.FindControl("lblflag")).Text == "")
                        {
                            DataRow dr = dt.NewRow();
                            dr[0] = ((Label)row.FindControl("lbldivid")).Text;
                            dr[1] = ((Label)row.FindControl("lbldivname")).Text.Trim();
                            dr[2] = "insert";
                            dt.Rows.Add(dr);
                        }
                    }
                    chkins = true;
                    json = DataTableToJSON(dt);

                    string urlalias = cls.urls();
                    string url = @urlalias + "loadgrid/";

                    div.type = "Save";
                    div.division_name = json.ToString();
                    div.classid = ddlclass.SelectedValue.ToString();
                    div.ayid = Session["acdyear"].ToString();
                    div.medium = ddlmedium.SelectedValue.ToString();
                    div.user_id = Session["emp_id"].ToString();

                    string jsonString = JsonHelper.JsonSerializer<DivisonMasterClass>(div);
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
                        if (result.ToString().Contains("Insert") == true)
                        {
                            chkins = true;
                        }
                    }

                }
                else
                {

                    foreach (GridViewRow row in grd_divison.Rows)
                    {
                        DataRow dr = dt.NewRow();
                        dr[0] = ((Label)row.FindControl("lbldivid")).Text;
                        dr[1] = ((Label)row.FindControl("lbldivname")).Text.Trim();
                        if (dr[0].ToString() == "" || dr[0].ToString() == null)
                        {
                            dr[2] = "insert";
                            chkins = true;
                        }
                        else
                        {
                            dr[2] = "update";
                            chkup = true;
                        }
                        dt.Rows.Add(dr);
                    }


                    json = DataTableToJSON(dt);

                    string urlalias = cls.urls();
                    string url = @urlalias + "loadgrid/";

                    div.type = "Save";
                    div.division_name = json.ToString();
                    div.classid = ddlclass.SelectedValue.ToString();
                    div.ayid = Session["acdyear"].ToString();
                    div.medium = ddlmedium.SelectedValue.ToString();
                    div.user_id = Session["emp_id"].ToString();

                    string jsonString = JsonHelper.JsonSerializer<DivisonMasterClass>(div);
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
                        if (result.ToString().Contains("Insert") == true)
                        {

                        }
                    }
                }

                if (chkup == false && chkins == true)
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Saved Successfully', { color: '#fff', background: '#198104', blur: 0.2, delay: 0 });", true);
                    clear();
                }
                else if (chkup == true && chkins == false)
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Updated Successfully', { color: '#fff', background: '#198104', blur: 0.2, delay: 0 });", true);
                    clear();
                }
                else if (chkup == true && chkins == true)
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Saved and Updated Successfully', { color: '#fff', background: '#198104', blur: 0.2, delay: 0 });", true);
                    clear();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('No Changes Made', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
                    clear();
                }

            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }//done

    protected void btnadd_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlclass.SelectedIndex != 0 && ddlmedium.SelectedIndex != 0)
            {
                int i = 0;
                bool check = false;
                DataTable dt = ViewState["dt"] as DataTable;
                if (dt.Rows.Count > 0)
                {
                    for (i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["division_name"].ToString() == "")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Division Name cannot be empty', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                            ViewState["dt"] = dt;
                            grd_divison.EditIndex = i;
                            this.BindGrid();
                            ((TextBox)grd_divison.Rows[i].FindControl("txtdivname")).Focus();
                            check = true;

                        }
                    }
                    if (check == false)
                    {
                        dt.Rows.Add("");
                        dt.Rows[i]["flag"] = "0";
                        ViewState["dt"] = dt;
                        grd_divison.EditIndex = i;
                        this.BindGrid();
                        ((TextBox)grd_divison.Rows[i].FindControl("txtdivname")).Focus();
                    }
                }
                else
                {
                    dt.Rows.Add("");
                    dt.Rows[0]["flag"] = "0";
                    ViewState["dt"] = dt;
                    grd_divison.EditIndex = i;
                    this.BindGrid();
                    ((TextBox)grd_divison.Rows[i].FindControl("txtdivname")).Focus();

                }
            }
            else
            {
                if (ddlmedium.SelectedIndex == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Select the Medium', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                }
                if (ddlclass.SelectedIndex == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Select the Standard', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                }
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }//done

    protected void BindGrid()
    {
        try
        {
            DataTable dt = ViewState["dt"] as DataTable;
            grd_divison.DataSource = dt;
            grd_divison.DataBind();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }//done

    protected void btnupdate_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
            DataTable dt = ViewState["dt"] as DataTable;
            string name = ((TextBox)grd_divison.Rows[row.RowIndex].FindControl("txtdivname")).Text.Trim();
            if (name.ToString() != "")
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string a = dt.Rows[i]["division_name"].ToString().ToUpper();
                    if (name.ToString().ToUpper() == a.ToString() && dt.Rows.Count > 1 && i != row.RowIndex)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Division cannot be same', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
                        ((TextBox)grd_divison.Rows[row.RowIndex].FindControl("txtdivname")).Text = "";
                        grd_divison.EditIndex = row.RowIndex;
                        this.BindGrid();
                        return;
                    }
                }
                dt.Rows[row.RowIndex]["division_id"] = ((Label)grd_divison.Rows[row.RowIndex].FindControl("lbldivid")).Text;
                dt.Rows[row.RowIndex]["division_name"] = ((TextBox)grd_divison.Rows[row.RowIndex].FindControl("txtdivname")).Text.Trim();
                ViewState["dt"] = dt;
                grd_divison.EditIndex = -1;
                this.BindGrid();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Division Name cannot be empty', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                ViewState["dt"] = dt;
                grd_divison.EditIndex = row.RowIndex;
                this.BindGrid();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }//done

    protected void btncancel_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
            DataTable dt = ViewState["dt"] as DataTable;
            if (((TextBox)grd_divison.Rows[row.RowIndex].FindControl("txtdivname")).Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Division Name cannot be empty', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                ViewState["dt"] = dt;
                grd_divison.EditIndex = row.RowIndex;
                this.BindGrid();
            }
            else
            {
                grd_divison.EditIndex = -1;
                this.BindGrid();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }//done

    protected void btndelete_Click1(object sender, EventArgs e)
    {
        try
        {
            string confirmValue = Request.Form["division"];
            if (confirmValue.Contains(","))
            {
                confirmValue = confirmValue.Split(',').Last();
            }
            if (confirmValue.ToString() == "OK")
            {
                GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
                if (((Label)row.FindControl("lblflag")).Text == "0")
                {
                    bool process = false;
                    DataTable dt = ViewState["dt"] as DataTable;
                    if (dt.Rows[row.RowIndex]["division_id"].ToString() == "" && dt.Rows[row.RowIndex]["division_name"].ToString() == "")
                    {
                        dt.Rows.RemoveAt(row.RowIndex);
                    }
                    else if (dt.Rows[row.RowIndex]["division_id"].ToString() == "")
                    {
                        dt.Rows.RemoveAt(row.RowIndex);
                        process = true;
                    }
                    else
                    {
                        useless();
                        string urlalias = cls.urls();
                        string url = @urlalias + "loadgrid/";
                        div.type = "Remove";
                        div.division_id = dt.Rows[row.RowIndex]["division_id"].ToString();
                        div.ayid = Session["acdyear"].ToString();


                        string jsonString = JsonHelper.JsonSerializer<DivisonMasterClass>(div);
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
                            if (result.ToString().Contains("No") == true)
                            {
                                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Students are already assigned Division Cannot be deleted..', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                            }
                            else if (result.ToString().Contains("Yes") == true)
                            {
                                dt.Rows.RemoveAt(row.RowIndex);
                                process = true;
                            }
                        }
                    }
                    ViewState["dt"] = dt;
                    this.BindGrid();
                    if (process == true)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Division Deleted Successfully', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Students already assigned', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }//done

    protected void grd_divison_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (chkprev.Checked == true)
                {
                    e.Row.Enabled = false;
                    ((LinkButton)e.Row.FindControl("btndelete")).OnClientClick = null;
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }//done

    public string DataTableToJSON(DataTable table)
    {
        string JSONString = string.Empty;
        JSONString = JsonConvert.SerializeObject(table);
        return JSONString;
    }

    protected void btnedit_Click(object sender, EventArgs e)
    {
        LinkButton btn = sender as LinkButton;
        GridViewRow gvrow = btn.NamingContainer as GridViewRow;
        GridViewRow row1 = grd_divison.Rows[gvrow.RowIndex];
        DataTable dt = ViewState["dt"] as DataTable;
        int i = grd_divison.EditIndex;
        string name;
        try
        {
            if (i != -1)
            {
                try
                {
                    name = ((TextBox)grd_divison.Rows[i].FindControl("txtdivname")).Text.Trim();
                }
                catch (Exception ex)
                {
                    name = ((Label)grd_divison.Rows[i].FindControl("lbldivname")).Text.Trim();
                }
                if (name.ToString() != "")
                {
                    grd_divison.EditIndex = row1.RowIndex;
                }
                else
                {
                    ViewState["dt"] = dt;
                    grd_divison.EditIndex = i;
                    this.BindGrid();
                }
            }
            else
            {
                ViewState["dt"] = dt;
                grd_divison.EditIndex = row1.RowIndex;
                this.BindGrid();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }
}