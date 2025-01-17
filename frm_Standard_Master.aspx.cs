using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Runtime.Serialization.Json;
using System.Text;


public partial class frm_Standard_Master : System.Web.UI.Page
{
    StandardMaster std = new StandardMaster();
    Class1 cls = new Class1();
    int add_flag = 0;

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
                    ddlfillstd();
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }//done

    public void uselessstd()
    {
        std.med = null;
        std.rank = null;
        std.stdid = null;
        std.type = null;
        std.user = null;
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


        public static T JsonDeserialize<T>(string jsonString)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T obj = (T)ser.ReadObject(ms);
            return obj;
        }
    }

    public void ddlfillstd()
    {
        try
        {
            uselessstd();
            btnclearstd.Enabled = false;
            btnsavestd.Enabled = false;
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
                ddlmediumstd.DataSource = dataSet.Tables[0];
                ddlmediumstd.DataTextField = "medium";
                ddlmediumstd.DataValueField = "med_id";
                ddlmediumstd.Items.Insert(0, "--Select--");
                ddlmediumstd.DataBind();
                ddlmediumstd.Items.Insert(0, "--Select--");
                ddlmediumstd.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }//done

    protected void ddlmediumstd_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            load();
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }//done

    protected void btnclearstd_Click(object sender, EventArgs e)
    {
        try
        {
            Clearstd();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
}//done

    protected void btnsavestd_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlmediumstd.SelectedIndex > 0)
            {
                DataTable dt1 = ViewState["dt"] as DataTable;
                for (int j = 0; j < dt1.Rows.Count; j++)
                {
                    if (dt1.Rows[j]["std_name"].ToString() == "")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Standard Name cannot be empty', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
                        return;
                    }
                }
                int i = 1;

                DataTable dt = new DataTable();
                dt.Columns.Add("std_id");
                dt.Columns.Add("std_name");
                dt.Columns.Add("action");
                dt.Columns.Add("rank");
                foreach (GridViewRow row in gridstd.Rows)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = ((Label)row.FindControl("lblstdid")).Text;
                    dr[1] = ((Label)row.FindControl("lblstd")).Text.Trim();
                    if (dr[0].ToString() == "" || dr[0].ToString() == null)
                    {
                        dr[2] = "insert";
                    }
                    else
                    {
                        dr[2] = "exist";
                    }
                    dr[3] = i.ToString();
                    dt.Rows.Add(dr);
                    i++;
                }

                string json = DataTableToJSON(dt);
                refresh(json);
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Saved Successfully', { color: '#fff', background: '#198104', blur: 0.2, delay: 0 });", true);
                ViewState["dt"] = null;
                this.data();
                ddlmediumstd.SelectedIndex = 0;
                btnclearstd.Enabled = false;
                btnsavestd.Enabled = false;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Select the medium', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
                return;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void btnaddstd_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = ViewState["dt"] as DataTable;

            for (int k = 0; k < gridstd.Rows.Count; k++)
            {
                try
                {
                    if (((Label)gridstd.Rows[k].FindControl("lblstd")).Text.Trim() == "")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Standard Name can not be empty', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    if (((TextBox)gridstd.Rows[k].FindControl("txtstd")).Text.Trim() == "")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Standard Name can not be empty', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
                        gridstd.Rows[k].FindControl("txtstd").Focus();
                        return;
                    }
                }

            }
            if (add_flag == 0)
            {
                Session["row"] = "";
                LinkButton btn = sender as LinkButton;
                GridViewRow gvrow = btn.NamingContainer as GridViewRow;
                GridViewRow row1 = gridstd.Rows[gvrow.RowIndex];
                Session["row"] = row1.RowIndex.ToString();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#myModal').modal('show');</script>", false);
            }
            else if (add_flag == 1 && Session["row"].ToString() != "")
            {

                DataRow dr = dt.NewRow();
                for (int i = 0; i < gridstd.Rows.Count; i++)
                {
                    dr = dt.NewRow();
                    try
                    {
                        dr[1] = ((Label)gridstd.Rows[i].FindControl("lblstd")).Text.Trim();
                    }
                    catch (Exception ex)
                    {
                        dr[1] = ((TextBox)gridstd.Rows[i].FindControl("txtstd")).Text.Trim();
                    }

                }
                if (Session["row"].ToString() == "0")
                {
                    DataRow r = dt.NewRow();
                    dt.Rows.InsertAt(r, 0);
                    dt.Rows[0]["flag"] = "0";
                    dt.Rows[0]["action"] = "insert";
                }
                else if (Convert.ToInt32(Session["row"].ToString()) > 0)
                {
                    DataRow r = dt.NewRow();
                    dt.Rows.InsertAt(r, Convert.ToInt32(Session["row"].ToString()));
                    dt.Rows[Convert.ToInt32(Session["row"].ToString())]["flag"] = "0";
                    dt.Rows[Convert.ToInt32(Session["row"].ToString())]["action"] = "insert";
                }

                ViewState["dt"] = dt;
                gridstd.EditIndex = Convert.ToInt32(Session["row"].ToString());
                this.data();
                gridstd.Rows[Convert.ToInt32(Session["row"].ToString())].FindControl("txtstd").Focus();
                add_flag = 0;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#myModal').modal('hide');</script>", false);
            }
            else if (add_flag == 2 && Session["row"].ToString() != "")
            {
                int cal = Convert.ToInt32(Session["row"].ToString()) + 1;
                DataRow r = dt.NewRow();
                dt.Rows.InsertAt(r, cal);
                dt.Rows[cal]["flag"] = "0";
                dt.Rows[cal]["action"] = "insert";
                gridstd.EditIndex = cal;
                this.data();
                gridstd.Rows[cal].FindControl("txtstd").Focus();
                add_flag = 0;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#myModal').modal('hide');</script>", false);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }

    }//done

    protected void btnabovestd_Click(object sender, EventArgs e)
    {
        try
        {
            add_flag = 1;
            btnaddstd_Click(sender, e);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }//done

    protected void btnbelowstd_Click(object sender, EventArgs e)
    {
        try
        {
            add_flag = 2;
            btnaddstd_Click(sender, e);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }//done

    protected void txtstd_TextChanged(object sender, EventArgs e)
    {
        try
        {
            bool action = false;
            TextBox txt = sender as TextBox;
            GridViewRow gvrow = txt.NamingContainer as GridViewRow;
            GridViewRow row1 = gridstd.Rows[gvrow.RowIndex];
            DataTable dt = ViewState["dt"] as DataTable;

            string name = ((TextBox)gridstd.Rows[row1.RowIndex].FindControl("txtstd")).Text.Trim();

            DataRow dr = dt.NewRow();

            for (int i = 0; i < gridstd.Rows.Count; i++)
            {
                dr = dt.NewRow();
                try
                {
                    if (row1.RowIndex == i)
                    {
                        dr[1] = ((TextBox)gridstd.Rows[i].FindControl("txtstd")).Text.Trim();
                    }
                    else
                    {
                        dr[1] = ((Label)gridstd.Rows[i].FindControl("lblstd")).Text.Trim();
                    }
                }
                catch (Exception ex)
                {
                    dr[1] = ((TextBox)gridstd.Rows[i].FindControl("txtstd")).Text.Trim();
                }

                if (name.ToString().ToUpper() == dr[1].ToString().ToUpper() && gridstd.Rows.Count > 1 && i != row1.RowIndex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Standard Cannot be same', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
                    ((TextBox)gridstd.Rows[row1.RowIndex].FindControl("txtstd")).Text = "";
                    gridstd.Rows[row1.RowIndex].FindControl("txtstd").Focus();
                    action = true;
                    return;
                }
            }
            if (action == false)
            {
                dt.Rows[row1.RowIndex]["std_name"] = name.ToString();
                ViewState["dt"] = dt;
                gridstd.EditIndex = -1;
                this.data();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }//done

    protected void btndeletestd_Click(object sender, EventArgs e)
    {
        try
        {
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue.Contains(","))
            {
                confirmValue = confirmValue.Split(',').Last();
            }
            if (confirmValue.ToString() == "OK")
            {
                LinkButton lnkbtn = sender as LinkButton;
                GridViewRow gvrow1 = lnkbtn.NamingContainer as GridViewRow;
                GridViewRow row = gridstd.Rows[gvrow1.RowIndex];
                if (((Label)row.FindControl("lblflag")).Text == "0")
                {
                    DataTable dt = ViewState["dt"] as DataTable;

                    string id = ((Label)gridstd.Rows[gvrow1.RowIndex].FindControl("lblstdid")).Text;
                    string name = "";
                    try
                    {
                        name = ((Label)gridstd.Rows[gvrow1.RowIndex].FindControl("lblstd")).Text.Trim();
                    }
                    catch (Exception ex)
                    {
                        name = ((TextBox)gridstd.Rows[gvrow1.RowIndex].FindControl("txtstd")).Text.Trim();
                    }
                    if (id == "")
                    {
                        bool flag = false;
                        dt.Rows.RemoveAt(row.RowIndex);
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Standard deleted successfully', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
                        if (dt.Rows.Count == 0)
                        {
                            dt.Rows.Add("");
                            flag = true;
                        }
                        ViewState["dt"] = dt;
                        if (flag == true)
                        {
                            gridstd.EditIndex = 0;
                        }
                        else
                        {
                            gridstd.EditIndex = -1;
                        }
                        this.data();
                    }
                    else if (id.ToString() != "")
                    {
                        uselessstd();
                        string urlalias = cls.urls();
                        string url = @urlalias + "std/";

                        std.type = "delete";
                        std.med = ddlmediumstd.SelectedValue.ToString();
                        std.stdid = id.ToString();

                        string jsonString = JsonHelper.JsonSerializer<StandardMaster>(std);
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
                            if (result.ToString().Contains("Delete") == true)
                            {
                                dt.Rows.RemoveAt(row.RowIndex);
                                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Standard deleted successfully', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
                                ViewState["dt"] = dt;
                                this.data();
                            }
                        }
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
    }

    protected void gridstd_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try { }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }//done

    protected void data()
    {
        try
        {
            DataTable dt= ViewState["dt"] as DataTable;
            gridstd.DataSource = dt;
            gridstd.DataBind();
            if (dt != null)
            {
                if (dt.Rows.Count == 0)
                {
                    ddlmediumstd.SelectedIndex = 0;
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }//done

    public void refresh(string str)
    {
        try
        {
            uselessstd();
            string urlalias = cls.urls();
            string url = @urlalias + "std/";

            std.type = "save";
            std.med = ddlmediumstd.SelectedValue.ToString();
            std.rank = str.ToString();
            std.user = Session["emp_id"].ToString();

            string jsonString = JsonHelper.JsonSerializer<StandardMaster>(std);
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
        try
        {
            if (ddlmediumstd.SelectedIndex > 0)
            {
                btnclearstd.Enabled = true;
                btnsavestd.Enabled = true;
                ViewState["dt"] = null;
                this.data();
                DataSet ds = new DataSet();
                if (ddlmediumstd.SelectedIndex > 0)
                {
                    uselessstd();
                    string urlalias = cls.urls();
                    string url = @urlalias + "std/";

                    std.type = "loadstd";
                    std.med = ddlmediumstd.SelectedValue.ToString();

                    string jsonString = JsonHelper.JsonSerializer<StandardMaster>(std);
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
                        ViewState["dt"] = ds.Tables[0];
                    }
                }

                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.data();
                }
                else
                {
                    ds.Tables[0].Columns.Add("std_id");
                    ds.Tables[0].Columns.Add("std_name");
                    ds.Tables[0].Columns.Add("flag");
                    ds.Tables[0].Columns.Add("action");
                    ds.Tables[0].Rows.Add("");
                    ds.Tables[0].Rows[0]["flag"] = "0";
                    ds.Tables[0].Rows[0]["action"] = "insert";
                    ViewState["dt"] = ds.Tables[0];
                    gridstd.EditIndex = 0;
                    this.data();
                    gridstd.Rows[0].FindControl("txtstd").Focus();

                }
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i]["std_id"].ToString() != "" && ds.Tables[0].Rows[i]["std_name"].ToString() != "")
                    {
                        gridstd.EditIndex = -1;
                    }
                    this.data();
                }
            }
            else
            {
                btnclearstd.Enabled = false;
                btnsavestd.Enabled = false;
                ViewState["dt"] = null;
                this.data();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }//done

    public void Clearstd()
    {
        try
        {
            string confirmValue = Request.Form["trigger"];
            if (confirmValue.Contains(","))
            {
                confirmValue = confirmValue.Split(',').Last();
            }
            if (confirmValue.ToString() == "OK")
            {
                ddlmediumstd.SelectedIndex = 0;
                ViewState["dt"] = null;
                this.data();
                btnclearstd.Enabled = false;
                btnsavestd.Enabled = false;
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
}