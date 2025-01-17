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

public partial class frm_Fee_Master : System.Web.UI.Page
{
    Class1 cls = new Class1();
    FeeMaster fm = new FeeMaster();
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
            }
            grdfee.Visible = false;
            button.Visible = false;
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
                useless();
                string urlalias = cls.urls();
                string url = @urlalias + "loadfee/";
                fm.type = "caste";

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
                        ddlcaste.DataSource = dataSet.Tables[0];
                        ddlcaste.DataValueField = "category_id";
                        ddlcaste.DataTextField = "category_name";
                        ddlcaste.DataBind();
                        ddlcaste.Items.Insert(0, "--Select--");
                        ddlcaste.Enabled = true;
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('No Caste Found', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                    }
                }
            }
            else
            {
                ddlcaste.Enabled = false;
            }
            grdfee.Visible = false;
            button.Visible = false;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlduration.Items.Clear();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>multi();</script>", false);
            if (ddltype.SelectedIndex > 0)
            {
                ddlduration.Enabled = true;
                useless();
                string urlalias = cls.urls();
                string url = @urlalias + "loadfee/";
                fm.type = "defined";
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
                        ddlduration.DataSource = dataSet.Tables[0];
                        ddlduration.DataTextField = "duration";
                        ddlduration.DataValueField = "duration_id";
                        ddlduration.DataBind();
                        ddlduration.Items.Insert(0, "--Select--");
                        ddlduration.Enabled = true;
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('No Fee Payment Duration Type Found', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                        ddltype.Enabled = false;
                    }
                }
            }
            grdfee.Visible = false;
            button.Visible = false;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void ddlcaste_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlcaste.SelectedIndex > 0)
            {
                ddltype.Enabled = true;
                useless();
                string urlalias = cls.urls();
                string url = @urlalias + "loadfee/";
                fm.type = "loadtype";
                fm.ayid = Session["acdyear"].ToString();
                fm.medium_id = ddlmedium.SelectedValue.ToString();
                fm.class_id = ddlclass.SelectedValue.ToString();

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
                        ddltype.Enabled = false;
                    }
                }
            }
            else
            {
                ddltype.Enabled = false;
            }
            grdfee.Visible = false;
            button.Visible = false;
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
            clear();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        try
        {
            btnprev.Enabled = false;
            btncaste.Enabled = false;
            DataTable dt = new DataTable();
            if (grdfee.Rows.Count == 0)
            {
                dt.Columns.Add("struct_id");
                dt.Columns.Add("struct_name");
                dt.Columns.Add("Amount");
                dt.Columns.Add("Rank");
                dt.Columns.Add("admflg");
                dt.Columns.Add("Flag");
                dt.Rows.Add("");
                dt.Rows[0]["Flag"] = "0";
                grdfee.Visible = true;
                grdfee.EditIndex = 0;
                btnadd.Enabled = false;
                btnsave.Enabled = false;
            }
            else
            {
                dt = ViewState["dt"] as DataTable;
                int i = grdfee.EditIndex;
                if (i.ToString() == "-1")
                {
                    dt.Rows.Add("");
                    dt.Rows[grdfee.Rows.Count]["Flag"] = "0";
                    grdfee.EditIndex = grdfee.Rows.Count;
                    btnadd.Enabled = false;
                    btnsave.Enabled = false;
                }
                else
                {
                    grdfee.EditIndex = i;
                    btnadd.Enabled = false;
                    btnsave.Enabled = false;
                }
            }
            ViewState["dt"] = dt;
            this.BindGrid();
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
            if (grdfee.EditIndex != -1)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('First Save all the Data of Fee Structure', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
            }
            else
            {
                DataTable dt = ViewState["dt"] as DataTable;
                try
                {
                    for (int i = 0; i < grdfee.Rows.Count; i++)
                    {
                        dt.Rows[i]["struct_id"] = ((Label)grdfee.Rows[i].FindControl("lblid")).Text;
                        dt.Rows[i]["struct_name"] = ((Label)grdfee.Rows[i].FindControl("lblname")).Text.Trim();
                        dt.Rows[i]["Amount"] = ((Label)grdfee.Rows[i].FindControl("lblamount")).Text.Trim();
                        dt.Rows[i]["Rank"] = ((TextBox)grdfee.Rows[i].FindControl("txtRank")).Text.Trim();
                        dt.Rows[i]["admflg"] = ((CheckBox)grdfee.Rows[i].FindControl("admichk")).Checked;
                        dt.Rows[i]["Flag"] = ((Label)grdfee.Rows[i].FindControl("lblFlag")).Text;
                    }
                    string json = DataTableToJSON(dt);
                    useless();
                    string urlalias = cls.urls();
                    string url = @urlalias + "loadfee/";
                    fm.type = "save";
                    fm.ayid = Session["acdyear"].ToString();
                    fm.medium_id = ddlmedium.SelectedValue.ToString();
                    fm.class_id = ddlclass.SelectedValue.ToString();
                    fm.caste = ddlcaste.SelectedValue.ToString();
                    fm.dutype = ddltype.SelectedItem.ToString();
                    fm.duration = ddlduration.SelectedItem.ToString();
                    fm.table = json.ToString();
                    fm.user=Session["emp_id"].ToString();
                    fm.duration_id = ddlduration.SelectedValue.ToString();


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
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('No Changes Made', { color: '#fff', background: '#198104', blur: 0.2, delay: 0 });", true);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void ddlduration_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlduration.SelectedIndex > 0)
            {
                ViewState["dt"] = null;
                this.BindGrid();
                button.Visible = true;
                useless();
                string urlalias = cls.urls();
                string url = @urlalias + "loadfee/";
                fm.type = "load";
                fm.ayid = Session["acdyear"].ToString();
                fm.caste = ddlcaste.SelectedValue.ToString();
                fm.class_id = ddlclass.SelectedValue.ToString();
                fm.medium_id = ddlmedium.SelectedValue.ToString();
                fm.dutype = ddltype.SelectedItem.ToString();
                fm.duration = ddlduration.SelectedItem.ToString();

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
                        grdfee.Visible = true;
                        ViewState["dt"] = dataSet.Tables[0];
                        this.BindGrid();
                        btnprev.Enabled = false;
                        btncaste.Enabled = false;
                        btnsave.Enabled = true;
                        btnadd.Enabled = true;
                    }
                    else
                    {
                        useless();
                        string urlalias1 = cls.urls();
                        string url1 = @urlalias1 + "loadfee/";
                        fm.type = "previous";
                        fm.ayid = Session["acdyear"].ToString();
                        fm.caste = ddlcaste.SelectedValue.ToString();
                        fm.class_id = ddlclass.SelectedValue.ToString();
                        fm.medium_id = ddlmedium.SelectedValue.ToString();
                        fm.dutype = ddltype.SelectedItem.ToString();
                        fm.duration_id = ddlduration.SelectedValue.ToString();

                        string jsonString1 = JsonHelper.JsonSerializer<FeeMaster>(fm);
                        var httprequest1 = (HttpWebRequest)WebRequest.Create(url1);
                        httprequest1.ContentType = "application/json";
                        httprequest1.Method = "POST";

                        using (var streamWriter = new StreamWriter(httprequest1.GetRequestStream()))
                        {
                            streamWriter.Write(jsonString1);
                            streamWriter.Flush();
                            streamWriter.Close();
                        }

                        var httpresponse1 = (HttpWebResponse)httprequest1.GetResponse();
                        using (var streamReader1 = new StreamReader(httpresponse1.GetResponseStream()))
                        {
                            string result1 = streamReader1.ReadToEnd();
                            DataSet dataSet1 = JsonConvert.DeserializeObject<DataSet>(result1);
                            if (dataSet1.Tables[0].Rows.Count > 0)
                            {
                                btnprev.Enabled = true;
                            }
                            else
                            {
                                btnprev.Enabled = false;
                            }
                            if (dataSet1.Tables[2].Rows.Count > 0)
                            {
                                btncaste.Enabled = true;
                            }
                            else
                            {
                                btncaste.Enabled = false;
                            }
                        }

                        btnadd.Enabled = true;
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('No Entry Found', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                    }
                }
                if (ddltype.SelectedValue.ToString() == "12")
                {
                    btnprev.Enabled = false;
                }
            }
            else
            {
                ViewState["dt"] = null;
                this.BindGrid();
                button.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void btnprev_Click(object sender, EventArgs e)
    {
        try
        {
            grdprev.DataSource = null;
            grdprev.DataBind();
            grdprev.Visible = false;
            lblref.Text = ddltype.SelectedItem.ToString();
            lblto.Text = ddltype.SelectedItem.ToString();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>multi();</script>", false);
            useless();
            string urlalias = cls.urls();
            string url = @urlalias + "loadfee/";
            fm.type = "previous";
            fm.ayid = Session["acdyear"].ToString();
            fm.caste = ddlcaste.SelectedValue.ToString();
            fm.class_id = ddlclass.SelectedValue.ToString();
            fm.medium_id = ddlmedium.SelectedValue.ToString();
            fm.dutype = ddltype.SelectedItem.ToString();
            fm.duration_id = ddlduration.SelectedValue.ToString();

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
                if (dataSet.Tables[0].Rows.Count > 0 && dataSet.Tables[1].Rows.Count > 0)
                {
                    ddlref.DataSource = dataSet.Tables[0];
                    ddlref.DataTextField = "duration";
                    ddlref.DataValueField = "duration_id";
                    ddlref.DataBind();
                    ddlref.Items.Insert(0, "--Select--");
                    lstmonth.DataSource = dataSet.Tables[1];
                    lstmonth.DataTextField = "duration";
                    lstmonth.DataValueField = "duration_id";
                    lstmonth.DataBind();
                }
                else
                {
                    if (dataSet.Tables[0].Rows.Count == 0)
                    {
                        ddlref.DataSource = dataSet.Tables[0];
                        ddlref.DataBind();
                        ddlref.Items.Insert(0, "--Select--");
                    }
                    if (dataSet.Tables[1].Rows.Count == 0)
                    {
                        lstmonth.DataSource = null;
                        lstmonth.DataBind();
                    }
                }
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void BindGrid()
    {
        grdfee.DataSource = ViewState["dt"] as DataTable;
        grdfee.DataBind();
    }

    protected void btnupdate_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
            DataTable dt = ViewState["dt"] as DataTable;
            string name = ((TextBox)grdfee.Rows[row.RowIndex].FindControl("txtname")).Text.Trim();
            string amount = ((TextBox)grdfee.Rows[row.RowIndex].FindControl("txtamount")).Text;
            string rank = ((TextBox)grdfee.Rows[row.RowIndex].FindControl("txtRank")).Text.Trim();
            bool dmflg = ((CheckBox)grdfee.Rows[row.RowIndex].FindControl("admichk")).Checked;
            string str = "";
            if (name.ToString() != "" || amount.ToString() != "")
            {
                if (name.ToString() != "")
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string a = dt.Rows[i]["struct_name"].ToString().ToUpper();
                        if (name.ToString().ToUpper() == a.ToString() && dt.Rows.Count > 1 && i != row.RowIndex)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Structure Name cannot be same', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
                            ((TextBox)grdfee.Rows[row.RowIndex].FindControl("txtname")).Text = "";
                            dt.Rows[row.RowIndex]["struct_name"] = "";
                            grdfee.EditIndex = row.RowIndex;
                            this.BindGrid();
                            return;
                        }
                    }
                    dt.Rows[row.RowIndex]["struct_id"] = ((TextBox)grdfee.Rows[row.RowIndex].FindControl("txtid")).Text;
                    dt.Rows[row.RowIndex]["struct_name"] = ((TextBox)grdfee.Rows[row.RowIndex].FindControl("txtname")).Text.Trim();
                    //dt.Rows[row.RowIndex]["struct_name"] = ((TextBox)grdfee.Rows[row.RowIndex].FindControl("txtname")).Text.Trim();
                }
                else
                {
                    dt.Rows[row.RowIndex]["struct_name"] = "";
                    str = "name";
                }
                if (amount.ToString() != "")
                {
                    dt.Rows[row.RowIndex]["Amount"] = ((TextBox)grdfee.Rows[row.RowIndex].FindControl("txtamount")).Text;
                }
                else
                {
                    dt.Rows[row.RowIndex]["Amount"] = "";
                    str = "amount";
                }
                if (rank.ToString() != "")
                {
                    dt.Rows[row.RowIndex]["Rank"] = ((TextBox)grdfee.Rows[row.RowIndex].FindControl("txtRank")).Text.Trim();
                }

                if (dmflg)
                {
                    dt.Rows[row.RowIndex]["admflg"] = ((CheckBox)grdfee.Rows[row.RowIndex].FindControl("admichk")).Checked;
                }
                else {
                    dt.Rows[row.RowIndex]["admflg"] =dmflg;
                }

                if (str.ToString() == "name")
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Structure Name cannot be empty', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                }
                else if (str.ToString() == "amount")
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Amount cannot be empty', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Structure Name and Amount cannot be empty', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                str = "blank";
            }
            ViewState["dt"] = dt;
            if (str.ToString() == "")
            {
                grdfee.EditIndex = -1;
                btnadd.Enabled = true;
                btnsave.Enabled = true;
            }
            else
            {
                grdfee.EditIndex = row.RowIndex;
                btnadd.Enabled = false;
                btnsave.Enabled = false;
            }
            this.BindGrid();

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void grd_fee_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string flag = ((Label)e.Row.FindControl("lblFlag")).Text;
                if (flag.ToString() == "1")
                {
                    ((LinkButton)e.Row.FindControl("btndelete")).Enabled = false;
                }
                string admflag = ((Label)e.Row.FindControl("lbladmFlag")).Text;
                if (admflag == "True")
                {
                    ((CheckBox)e.Row.FindControl("admichk")).Checked = true;
                }
                else { ((CheckBox)e.Row.FindControl("admichk")).Checked = false; }

            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void grdfee_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            DataTable dt = ViewState["dt"] as DataTable;
            int i = grdfee.EditIndex;
            string name, amount, Rank;
            try
            {
                Rank = ((TextBox)grdfee.Rows[i].FindControl("txtRank")).Text.Trim();
                if (Rank.ToString() != "")
                {
                    dt.Rows[i]["Rank"] = Rank.ToString();
                }
                if (i != -1)
                {
                    try
                    {
                        name = ((TextBox)grdfee.Rows[i].FindControl("txtname")).Text.Trim();
                        amount = ((TextBox)grdfee.Rows[i].FindControl("txtamount")).Text;
                    }
                    catch (Exception ex)
                    {
                        name = ((Label)grdfee.Rows[i].FindControl("lblname")).Text.Trim();
                        amount = ((Label)grdfee.Rows[i].FindControl("lblamount")).Text.Trim();
                    }
                    if (name.ToString() != "" && amount.ToString() != "")
                    {
                        grdfee.EditIndex = Convert.ToInt32(e.CommandArgument);
                    }
                    else
                    {
                        ViewState["dt"] = dt;
                        grdfee.EditIndex = i;
                        this.BindGrid();
                    }
                }
                else
                {
                    ViewState["dt"] = dt;
                    grdfee.EditIndex = Convert.ToInt32(e.CommandArgument);
                    this.BindGrid();
                    btnadd.Enabled = false;
                    btnsave.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
            }
        }
    }

    protected void btnedit_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = sender as LinkButton;
            GridViewRow gvrow = btn.NamingContainer as GridViewRow;
            GridViewRow row1 = grdfee.Rows[gvrow.RowIndex];
            DataTable dt = ViewState["dt"] as DataTable;
            int i = grdfee.EditIndex;
            string name, amount, Rank;
            //var admfl=false;
            
            try
            {
                
                if (i != -1)
                {
                    Rank = ((TextBox)grdfee.Rows[i].FindControl("txtRank")).Text.Trim();
                    if (Rank.ToString() != "")
                    {
                        dt.Rows[i]["Rank"] = Rank.ToString();
                    }
                    dt.Rows[i]["Rank"] = ((CheckBox)grdfee.Rows[i].FindControl("admichk")).Checked;
                    //if (admfl)
                    //{

                    //}
                    try
                    {
                        name = ((TextBox)grdfee.Rows[i].FindControl("txtname")).Text.Trim();
                        amount = ((TextBox)grdfee.Rows[i].FindControl("txtamount")).Text;
                    }
                    catch (Exception ex)
                    {
                        name = ((Label)grdfee.Rows[i].FindControl("lblname")).Text.Trim();
                        amount = ((Label)grdfee.Rows[i].FindControl("lblamount")).Text.Trim();
                    }
                    if (name.ToString() != "" && amount.ToString() != "")
                    {
                        grdfee.EditIndex = row1.RowIndex;
                    }
                    else
                    {
                        ViewState["dt"] = dt;
                        grdfee.EditIndex = i;
                        this.BindGrid();
                    }
                }
                else
                {
                    dt.Rows[gvrow.RowIndex]["admflg"] = ((CheckBox)grdfee.Rows[gvrow.RowIndex].FindControl("admichk")).Checked;
                    ViewState["dt"] = dt;
                    grdfee.EditIndex = row1.RowIndex;
                    this.BindGrid();
                    btnadd.Enabled = false;
                    btnsave.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
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
            ViewState["dt"] = null;
            this.BindGrid();
            grdfee.Visible = false;
            button.Visible = false;
            btnadd.Enabled = true;
            btnsave.Enabled = false;
            if (ddlduration.SelectedIndex > 0)
            {
                ddlduration.SelectedIndex = 0;
                ddlduration.DataSource = null;
                ddlduration.DataBind();
                ddlduration.Enabled = false;
            }
            if (ddltype.SelectedIndex > 0)
            {
                ddltype.SelectedIndex = 0;
                ddltype.DataSource = null;
                ddltype.DataBind();
                ddltype.Enabled = false;
            }
            if (ddlcaste.SelectedIndex > 0)
            {
                ddlcaste.SelectedIndex = 0;
                ddlcaste.DataSource = null;
                ddlcaste.DataBind();
                ddlcaste.Enabled = false;
            }
            if (ddlclass.SelectedIndex > 0)
            {
                ddlclass.SelectedIndex = 0;
                ddlclass.DataSource = null;
                ddlclass.DataBind();
                ddlclass.Enabled = false;
            }
            if (ddlmedium.SelectedIndex > 0)
            {
                ddlmedium.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void btndelete_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = ViewState["dt"] as DataTable;
            LinkButton btn = sender as LinkButton;
            GridViewRow gvrow = btn.NamingContainer as GridViewRow;
            GridViewRow row1 = grdfee.Rows[gvrow.RowIndex];
            string id = "";

            try
            {
                id = ((TextBox)grdfee.Rows[row1.RowIndex].FindControl("txtid")).Text;
            }
            catch (Exception ex)
            {
                id = ((Label)grdfee.Rows[row1.RowIndex].FindControl("lblid")).Text;
            }

            if (id.ToString() == "")
            {
                dt.Rows.RemoveAt(row1.RowIndex);
                btnadd.Enabled = true;
                btnsave.Enabled = true;
                grdfee.EditIndex = -1;
            }
            else
            {
                useless();
                string urlalias = cls.urls();
                string url = @urlalias + "loadfee/";
                fm.type = "delete";
                fm.ayid = Session["acdyear"].ToString();
                fm.caste = id.ToString();
                

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
                    if (result.ToString().Contains("delete") == true)
                    {
                        dt.Rows.RemoveAt(row1.RowIndex);
                        btnadd.Enabled = true;
                        btnsave.Enabled = true;
                        grdfee.EditIndex = -1;
                    }
                }
            }
            if (dt.Rows.Count == 0)
            {
                btnadd.Enabled = true;
                definedcheck();
            }

            ViewState["dt"] = dt;
            this.BindGrid();
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

    protected void ddlref_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlref.SelectedIndex > 0)
            {
                grdprev.Visible = true;
                useless();
                string urlalias = cls.urls();
                string url = @urlalias + "loadfee/";
                fm.type = "load";
                fm.ayid = Session["acdyear"].ToString();
                fm.caste = ddlcaste.SelectedValue.ToString();
                fm.class_id = ddlclass.SelectedValue.ToString();
                fm.medium_id = ddlmedium.SelectedValue.ToString();
                fm.dutype = ddltype.SelectedItem.ToString();
                fm.duration = ddlref.SelectedItem.ToString();

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
                        grdprev.DataSource = dataSet.Tables[0];
                        grdprev.DataBind();
                    }
                }
            }
            else
            {
                grdprev.DataSource = null;
                grdprev.DataBind();
            }
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>multi();</script>", false);
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void btnsaveprev_Click(object sender, EventArgs e)
    {
        try
        {
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue.ToString() != null)
            {
                string[] str = confirmValue.ToString().Split(',');
                if (str[str.Length-1].ToString() == "OK")
                {
                    try
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add("struct_id");
                        dt.Columns.Add("struct_name");
                        dt.Columns.Add("Amount");
                        dt.Columns.Add("Rank");
                        dt.Columns.Add("isadmission");
                        for (int i = 0; i < grdprev.Rows.Count; i++)
                        {
                            dt.Rows.Add("");
                            dt.Rows[i]["struct_id"] = "";
                            dt.Rows[i]["struct_name"] = ((Label)grdprev.Rows[i].FindControl("lblname")).Text.Trim();
                            dt.Rows[i]["Amount"] = ((Label)grdprev.Rows[i].FindControl("lblamount")).Text.Trim();
                            dt.Rows[i]["Rank"] = ((Label)grdprev.Rows[i].FindControl("lblrank")).Text;
                            dt.Rows[i]["isadmission"] = ((CheckBox)grdprev.Rows[i].FindControl("admichk")).Text;
                        }
                        string json = DataTableToJSON(dt);
                        useless();
                        string urlalias = cls.urls();
                        string url = @urlalias + "loadfee/";
                        fm.type = "saveprevious";
                        fm.ayid = Session["acdyear"].ToString();
                        fm.medium_id = ddlmedium.SelectedValue.ToString();
                        fm.class_id = ddlclass.SelectedValue.ToString();
                        fm.caste = ddlcaste.SelectedValue.ToString();
                        fm.dutype = ddltype.SelectedItem.ToString();
                        fm.duration = ddlduration.SelectedItem.ToString();
                        fm.table = json.ToString();
                        fm.user = Session["emp_id"].ToString();
                        fm.duration_id = Session["id"].ToString();


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
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "Pop", "$('#myModal').modal('hide');", true);
                                ddlduration.SelectedIndex = 0;
                                ViewState["dt"] = null;
                                this.BindGrid();
                                button.Visible = false;
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('No Changes Made', { color: '#fff', background: '#198104', blur: 0.2, delay: 0 });", true);
                                ddlduration.SelectedIndex = 0;
                                ViewState["dt"] = null;
                                this.BindGrid();
                                button.Visible = false;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                    }
                }
                else if (confirmValue.ToString() == "NO")
                {
                    
                }
            }
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

    [System.Web.Services.WebMethod(enableSession: true)]
    public static void SetCaste(string caste)
    {
        HttpContext.Current.Session["Caste"] = caste;
    }

    public void definedcheck()
    {
        useless();
        string urlalias1 = cls.urls();
        string url1 = @urlalias1 + "loadfee/";
        fm.type = "previous";
        fm.ayid = Session["acdyear"].ToString();
        fm.caste = ddlcaste.SelectedValue.ToString();
        fm.class_id = ddlclass.SelectedValue.ToString();
        fm.medium_id = ddlmedium.SelectedValue.ToString();
        fm.dutype = ddltype.SelectedItem.ToString();
        fm.duration_id = ddlduration.SelectedValue.ToString();

        string jsonString1 = JsonHelper.JsonSerializer<FeeMaster>(fm);
        var httprequest1 = (HttpWebRequest)WebRequest.Create(url1);
        httprequest1.ContentType = "application/json";
        httprequest1.Method = "POST";

        using (var streamWriter = new StreamWriter(httprequest1.GetRequestStream()))
        {
            streamWriter.Write(jsonString1);
            streamWriter.Flush();
            streamWriter.Close();
        }

        var httpresponse1 = (HttpWebResponse)httprequest1.GetResponse();
        using (var streamReader1 = new StreamReader(httpresponse1.GetResponseStream()))
        {
            string result1 = streamReader1.ReadToEnd();
            DataSet dataSet1 = JsonConvert.DeserializeObject<DataSet>(result1);
            if (dataSet1.Tables[0].Rows.Count > 0)
            {
                btnprev.Enabled = true;
            }
            else
            {
                btnprev.Enabled = false;
            }
            if (dataSet1.Tables[2].Rows.Count > 0)
            {
                btncaste.Enabled = true;
            }
            else
            {
                btncaste.Enabled = false;
            }
        }
    }
    protected void btncaste_Click(object sender, EventArgs e)
    {
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            GridView1.Visible = false;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>multi();</script>", false);
            useless();
            string urlalias = cls.urls();
            string url = @urlalias + "loadfee/";
            fm.type = "previouscaste";
            fm.ayid = Session["acdyear"].ToString();
            fm.duration_id = ddlduration.SelectedValue.ToString();
            fm.caste = ddlcaste.SelectedValue.ToString();
            fm.class_id = ddlclass.SelectedValue.ToString();
            fm.medium_id = ddlmedium.SelectedValue.ToString();
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
                if (dataSet.Tables[0].Rows.Count > 0 && dataSet.Tables[1].Rows.Count > 0)
                {
                    ddlcasteref.DataSource = dataSet.Tables[0];
                    ddlcasteref.DataTextField = "category_name";
                    ddlcasteref.DataValueField = "category_id";
                    ddlcasteref.DataBind();
                    ddlcasteref.Items.Insert(0, "--Select--");
                    lstcaste.DataSource = dataSet.Tables[1];
                    lstcaste.DataTextField = "category_name";
                    lstcaste.DataValueField = "category_id";
                    lstcaste.DataBind();
                }
                else
                {
                    if (dataSet.Tables[0].Rows.Count == 0)
                    {
                        ddlcasteref.DataSource = dataSet.Tables[0];
                        ddlcasteref.DataBind();
                        ddlcasteref.Items.Insert(0, "--Select--");
                    }
                    if (dataSet.Tables[1].Rows.Count == 0)
                    {
                        lstcaste.DataSource = null;
                        lstcaste.DataBind();
                    }
                }
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }
    protected void btnsavecaste_Click(object sender, EventArgs e)
    {
        try
        {
            string confirmValue = Request.Form["confirm_value1"];
            if (confirmValue.ToString() != null)
            {
                string[] str = confirmValue.ToString().Split(',');
                if (str[str.Length - 1].ToString() == "OK")
                {
                    try
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add("struct_id");
                        dt.Columns.Add("struct_name");
                        dt.Columns.Add("Amount");
                        dt.Columns.Add("Rank");
                        dt.Columns.Add("admflg");

                        for (int i = 0; i < GridView1.Rows.Count; i++)
                        {
                            dt.Rows.Add("");
                            dt.Rows[i]["struct_id"] = "";
                            dt.Rows[i]["struct_name"] = ((Label)GridView1.Rows[i].FindControl("lblname")).Text.Trim();
                            dt.Rows[i]["Amount"] = ((Label)GridView1.Rows[i].FindControl("lblamount")).Text.Trim();
                            dt.Rows[i]["Rank"] = ((Label)GridView1.Rows[i].FindControl("lblrank")).Text;
                            int chk = 0;
                            if (((CheckBox)GridView1.Rows[i].FindControl("lblflg")).Checked == true)
                            {
                                chk = 1;
                            }
                            dt.Rows[i]["admflg"] = chk;
                        }
                        string json = DataTableToJSON(dt);
                        useless();
                        string urlalias = cls.urls();
                        string url = @urlalias + "loadfee/";
                        fm.type = "savepreviouscastewise";
                        fm.ayid = Session["acdyear"].ToString();
                        fm.medium_id = ddlmedium.SelectedValue.ToString();
                        fm.class_id = ddlclass.SelectedValue.ToString();
                        fm.caste = Session["Caste"].ToString();
                        fm.dutype = ddltype.SelectedItem.ToString();
                        fm.duration = ddlduration.SelectedItem.ToString();
                        fm.table = json.ToString();
                        fm.user = Session["emp_id"].ToString();
                        fm.duration_id = ddlduration.SelectedValue.ToString();


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
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "Pop", "$('#modalcaste').modal('hide');", true);
                                ddlduration.SelectedIndex = 0;
                                ViewState["dt"] = null;
                                this.BindGrid();
                                button.Visible = false;
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "Pop", "$('#modalcaste').modal('hide');", true);
                                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('No Changes Made', { color: '#fff', background: '#198104', blur: 0.2, delay: 0 });", true);
                                ddlduration.SelectedIndex = 0;
                                ViewState["dt"] = null;
                                this.BindGrid();
                                button.Visible = false;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                    }
                }
                else if (confirmValue.ToString() == "NO")
                {

                }
            }
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>multi();</script>", false);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }
    protected void ddlcasteref_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlcasteref.SelectedIndex > 0)
            {
                GridView1.Visible = true;
                useless();
                string urlalias = cls.urls();
                string url = @urlalias + "loadfee/";
                fm.type = "load";
                fm.ayid = Session["acdyear"].ToString();
                fm.caste = ddlcasteref.SelectedValue.ToString();
                fm.class_id = ddlclass.SelectedValue.ToString();
                fm.medium_id = ddlmedium.SelectedValue.ToString();
                fm.dutype = ddltype.SelectedItem.ToString();
                fm.duration = ddlduration.SelectedItem.ToString();

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
                        GridView1.DataSource = dataSet.Tables[0];
                        GridView1.DataBind();
                    }
                }
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>multi();</script>", false);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }
}