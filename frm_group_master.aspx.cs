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

public partial class frm_group_master : System.Web.UI.Page
{
    group_master gm = new group_master();
    common cm = new common();
    Class1 cls = new Class1();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["emp_id"]) == "")
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                if (!IsPostBack)
                {

                    ddlfill();
                   
                    btnnew.Enabled = false;
                    chkprev.Enabled = false;
                    txtgroupname.Enabled = false;
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
        public static T JsonDeserialize<T>(string jsonString)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T obj = (T)ser.ReadObject(ms);
            return obj;
        }
    }
    public void useless()
    {

        gm.medium_id = null;
        gm.class_id = null;
        gm.subject_id = null;
        gm.subject_name = null;
        gm.group_id = null;
        gm.group_name = null;
        gm.AYID = null;
        gm.username = null;
        gm.type = null;
        gm.prevAyid = null;


    }
    public void ddlfill()
    {
        try
        {
            ddlstandard.Enabled = false;
            btn_save.Enabled = false;
            useless();
            string urlalias = cls.urls();
            string url = @urlalias + "Common/";
            cm.type = "ddlfill";
            cm.year = Session["acdyear"].ToString();

            string jsonString = JsonHelper.JsonSerializer<common>(cm);
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (StreamWriter sw = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                sw.Write(jsonString);
                sw.Flush();
                sw.Close();
            }

            var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (StreamReader sr = new StreamReader(httpWebResponse.GetResponseStream()))
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

                DataTable dt2 = dslist.Tables[1];
                ddlprevyear.DataSource = dt2;
                ddlprevyear.DataTextField = "duration";
                ddlprevyear.DataValueField = "AYID";
                ddlprevyear.DataBind();
                ddlprevyear.Items.Insert(0, "--Select--");
                ddlprevyear.SelectedIndex = 0;


            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
        try
        {
            if (chkprev.Checked == true)
            {
                string group = "";
                foreach (GridViewRow row in gv_grp_mst.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        if (group == "")
                        {
                            group = (row.Cells[2].FindControl("lblgroup") as Label).Text;
                        }
                        else
                        {
                            group = group + "," + (row.Cells[2].FindControl("lblgroup") as Label).Text;
                        }
                    }
                }

                string urlalias = cls.urls();
                string url = @urlalias + "GroupMaster/";
                gm.type = "Insertprev";
                gm.class_id = ddlstandard.SelectedValue.ToString();
                gm.medium_id = ddlmedium.SelectedValue.ToString();
                gm.group_id = group;
                gm.prevAyid = ddlprevyear.SelectedValue;
                gm.AYID = Session["acdyear"].ToString();
                gm.username = Session["emp_id"].ToString();
                string jsonString = JsonHelper.JsonSerializer<group_master>(gm);
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (StreamWriter sw = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    sw.Write(jsonString);
                    sw.Flush();
                    sw.Close();
                }

                var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (StreamReader sr = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    string result = sr.ReadToEnd();
                    if (result.ToString().Contains("Saved") == true)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Previous year group saved successfully for current academic year', { color: '#fff', background: '#198104', blur: 0.2, delay: 0 });", true);
                        ddlprevyear.SelectedIndex = 0;
                        chkprev.Checked = false;

                        txtgroupname.Text = "";
                        txtgroupname.Enabled = false;
                        gridviewFill(ddlmedium.SelectedValue, ddlstandard.SelectedValue);
                        ddlmedium.Enabled = true;
                        ddlstandard.Enabled = true;
                        ddlprevyear.Enabled = true;
                        divprev.Attributes.Add("style","display:none");
                        
                    }
                    else if (result.ToString().Contains("NoGroup") == true)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('No group found for previous year', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
                    }
                   
                   
                }
               

            }
            else
            {


                string subject = "";
                foreach (GridViewRow row in grdsub.Rows)
                {

                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkRow = (row.Cells[0].FindControl("chkgroup") as CheckBox);
                        if (chkRow.Checked)
                        {
                            if (subject == "")
                            {
                                subject = (row.Cells[2].FindControl("lblsub") as Label).Text;
                            }
                            else
                            {

                                subject = subject + "," + (row.Cells[2].FindControl("lblsub") as Label).Text;

                            }
                        }
                    }
                }

                if (ddlmedium.SelectedIndex == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Select medium', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                }
                else if (ddlstandard.SelectedIndex == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Select standard', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                }
                else if (txtgroupname.Enabled == true && txtgroupname.Text.Trim() == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Enter group name', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                }
                else if (grdsub.Rows.Count == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Click new to load subject', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                }
                else if (subject == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Select atleast one subject', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                }
                else
                {
                    if (btn_save.Text == "Save")
                    {



                        string urlalias = cls.urls();
                        string url = @urlalias + "GroupMaster/";
                        gm.type = "Insert";
                        gm.class_id = ddlstandard.SelectedValue.ToString();
                        gm.medium_id = ddlmedium.SelectedValue.ToString();
                        gm.subject_id = subject;
                        gm.group_name = txtgroupname.Text.Trim();
                        gm.AYID = Session["acdyear"].ToString();
                        gm.username = Session["emp_id"].ToString();
                        string jsonString = JsonHelper.JsonSerializer<group_master>(gm);
                        var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                        httpWebRequest.ContentType = "application/json";
                        httpWebRequest.Method = "POST";

                        using (StreamWriter sw = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            sw.Write(jsonString);
                            sw.Flush();
                            sw.Close();
                        }

                        var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                        using (StreamReader sr = new StreamReader(httpWebResponse.GetResponseStream()))
                        {
                            string result = sr.ReadToEnd();
                            if (result.ToString().Contains("Saved") == true)
                            {
                                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Group saved successfully', { color: '#fff', background: '#198104', blur: 0.2, delay: 0 });", true);
                                
                                txtgroupname.Text = "";
                               
                                gridviewFill(ddlmedium.SelectedValue, ddlstandard.SelectedValue);
                                

                            }
                            else if (result.ToString().Contains("GroupName") == true)
                            {
                                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Group name already assigned for selected medium and standard ', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
                            }
                            else if (result.ToString().Contains("GroupIssue") == true)
                            {
                                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Error while creating group', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
                            }
                           
                            
                        }

                    }
                    else
                    {


                        string urlalias = cls.urls();
                        string url = @urlalias + "GroupMaster/";
                        gm.type = "Update";
                        gm.class_id = ddlstandard.SelectedValue.ToString();
                        gm.medium_id = ddlmedium.SelectedValue.ToString();
                        gm.subject_id = subject;
                        gm.group_id = lblgroup_id.Text;
                        gm.group_name = txtgroupname.Text.Trim();
                        gm.AYID = Session["acdyear"].ToString();
                        gm.username = Session["emp_id"].ToString();
                        string jsonString = JsonHelper.JsonSerializer<group_master>(gm);
                        var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                        httpWebRequest.ContentType = "application/json";
                        httpWebRequest.Method = "POST";

                        using (StreamWriter sw = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            sw.Write(jsonString);
                            sw.Flush();
                            sw.Close();
                        }

                        var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                        using (StreamReader sr = new StreamReader(httpWebResponse.GetResponseStream()))
                        {
                            string result = sr.ReadToEnd();
                            if (result.ToString().Contains("Saved") == true)
                            {
                                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Group updated successfully', { color: '#fff', background: '#198104', blur: 0.2, delay: 0 });", true);
                               
                                txtgroupname.Text = "";
                                txtgroupname.Enabled = false;
                                gridviewFill(ddlmedium.SelectedValue, ddlstandard.SelectedValue);
                                btn_save.Text = "Save";

                                btnnew.Enabled = true;
                            }
                            else if (result.ToString().Contains("GroupName") == true)
                            {
                                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Group name already assigned for selected medium and standard ', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
                            }
                           
                           
                        }

                    }
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
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
                        ddlstandard.DataSource = table;
                        ddlstandard.DataTextField = "std_name";
                        ddlstandard.DataValueField = "std_id";
                        ddlstandard.DataBind();
                        ddlstandard.Enabled = true;
                        ddlstandard.Items.Insert(0, "--Select--");
                        ddlstandard.SelectedIndex = 0;


                    }
                }
            }
            else if (ddlmedium.SelectedIndex == 0)
            {
                if (ddlstandard.SelectedIndex > 0)
                {
                    ddlstandard.SelectedIndex = 0;
                }
                ddlstandard.Enabled = false;
                ViewState["dt"] = null;
                txtgroupname.Text = "";
                txtgroupname.Enabled = false;
                btnnew.Enabled = false;
                

            }
            gv_grp_mst.DataSource = "";
            gv_grp_mst.DataBind();
            grdsub.DataSource = "";
            grdsub.DataBind();
            btn_save.Text = "Save";
            txtgroupname.Text = "";
            txtgroupname.Enabled = false;
            btn_save.Enabled = false;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }
    protected void ddlstandard_SelectedIndexChanged(object sender, EventArgs e)
    {
        gv_grp_mst.DataSource = "";
        gv_grp_mst.DataBind();
        grdsub.DataSource = "";
        grdsub.DataBind();
        btn_save.Text = "Save";
        //txtgroupname.Text = "";
        if (ddlstandard.SelectedIndex != 0)
        {
            gridviewFill(ddlmedium.SelectedValue, ddlstandard.SelectedValue);
            btnnew.Enabled = true;
        }
        else
        {
            btnnew.Enabled = false;
        }
      
        txtgroupname.Text = "";
        txtgroupname.Enabled = false;
       
       
    }
    protected void btnnew_Click(object sender, EventArgs e)
    {
        if (ddlstandard.SelectedIndex > 0)
        {
            gridviewsubjectFill(ddlmedium.SelectedValue, ddlstandard.SelectedValue);
        }
        else
        {
            if (ddlmedium.SelectedIndex == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Select medium ', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
            }
            else if (ddlstandard.SelectedIndex == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Select standard ', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
            }
        }
        
    }

    public void gridviewFill(string med_id, string class_id)
    {
        try
        {
            if (med_id != "" && class_id != "")
            {

                grdsub.DataSource = "";
                grdsub.DataBind();
                useless();
                ViewState["dtgrp"] = null;
                string urlalias = cls.urls();
                string url = @urlalias + "GroupMaster/";
                gm.type = "gvfill";
                gm.class_id = class_id;
                gm.AYID = Session["acdyear"].ToString();
                gm.medium_id = med_id;
                if (chkprev.Checked == true && ddlprevyear.SelectedIndex > 0)
                {
                    gm.prevAyid = ddlprevyear.SelectedValue;

                }

                string jsonString = JsonHelper.JsonSerializer<group_master>(gm);
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (StreamWriter sw = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    sw.Write(jsonString);
                    sw.Flush();
                    sw.Close();
                }

                var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (StreamReader sr = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    string result = sr.ReadToEnd();
                    DataSet ds = JsonConvert.DeserializeObject<DataSet>(result);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ViewState["dtgrp"] = ds.Tables[0];
                        gv_grp_mst.DataSource = ds.Tables[0];
                        gv_grp_mst.DataBind();
                        chkprev.Enabled = false;
                        txtgroupname.Enabled = false;
                        btnnew.Enabled=true;
                        btn_save.Enabled = false;
                        if (chkprev.Checked == true)
                        {
                            chkprev.Enabled = true;
                            //txtgroupname.Enabled = false;
                            txtgroupname.Text = "";
                            btn_save.Enabled = true;
                            btnnew.Enabled = false;
                            foreach (GridViewRow row in gv_grp_mst.Rows)
                            {
                                if (row.RowType == DataControlRowType.DataRow)
                                {
                                    LinkButton lnkrow = (row.Cells[0].FindControl("LBDelete") as LinkButton);
                                    lnkrow.OnClientClick = null;
                                }
                            }

                        }

                    }
                    else
                    {
                        gv_grp_mst.DataSource = "";
                        gv_grp_mst.DataBind();
                        ViewState["dtgrp"] = null;
                        chkprev.Enabled = true;
                        txtgroupname.Enabled = false;
                        txtgroupname.Text = "";
                        btnnew.Enabled = true;
                        btn_save.Enabled = false;
                        if (chkprev.Checked == true)
                        {
                            chkprev.Enabled = true;
                            txtgroupname.Enabled = false;
                            txtgroupname.Text = "";
                            btnnew.Enabled = false;

                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Groups not defined for selected previous year', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                        }
                        else
                        {

                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Groups not defined', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                        }
                    }
                }
            }
            else
            {
                gv_grp_mst.DataSource = "";
                gv_grp_mst.DataBind();
                ViewState["dtgrp"] = null;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);

        }
    }

    public void gridviewsubjectFill(string med_id, string class_id)
    {
        try
        {
            if (med_id != "" && class_id != "")
            {
                useless();
                ViewState["dt"] = null;
                string urlalias = cls.urls();
                string url = @urlalias + "GroupMaster/";
                gm.type = "fillsub";
                gm.class_id = class_id;
                gm.medium_id = med_id;

                string jsonString = JsonHelper.JsonSerializer<group_master>(gm);
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (StreamWriter sw = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    sw.Write(jsonString);
                    sw.Flush();
                    sw.Close();
                }

                var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (StreamReader sr = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    string result = sr.ReadToEnd();
                    DataSet ds = JsonConvert.DeserializeObject<DataSet>(result);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ViewState["dt"] = ds.Tables[0];
                        grdsub.DataSource = ds.Tables[0];
                        grdsub.DataBind();
                        gv_grp_mst.DataSource = "";
                        gv_grp_mst.DataBind();
                        btnnew.Enabled = false;
                        txtgroupname.Enabled = true;
                        btn_save.Enabled = true;
                    }
                    else
                    {
                        ViewState["dt"] = null;
                        grdsub.DataSource = "";
                        grdsub.DataBind();
                        btnnew.Enabled = true;
                        txtgroupname.Enabled = false;
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Subjects not defined', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                    }
                }
            }
            else
            {
                ViewState["dt"] = null;
                grdsub.DataSource = "";
                grdsub.DataBind();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);

        }
    }





    protected void btn_Edit_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = sender as LinkButton;
            GridViewRow gvrow = btn.NamingContainer as GridViewRow;
            GridViewRow row1 = gv_grp_mst.Rows[gvrow.RowIndex];
            DataTable dt = ViewState["dt"] as DataTable;
            int i = gv_grp_mst.EditIndex;
            if (i != -1)
            {

            }
            else
            {
                lblgroup_id.Text = ((Label)gv_grp_mst.Rows[gvrow.RowIndex].FindControl("lblgroup")).Text;
                string[] subjectid = ((Label)gv_grp_mst.Rows[gvrow.RowIndex].FindControl("lbl_subject")).Text.Split(',');
                txtgroupname.Text = ((Label)gv_grp_mst.Rows[gvrow.RowIndex].FindControl("lblgrpname")).Text;
                gridviewsubjectFill(ddlmedium.SelectedValue, ddlstandard.SelectedValue);
                for (int j = 0; j < subjectid.Length; j++)
                {
                    string lblsubj = subjectid[j];
                    foreach (GridViewRow row in grdsub.Rows)
                    {

                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            CheckBox chk = (CheckBox)row.FindControl("chkgroup");
                            Label sub_id = row.FindControl("lblsub") as Label;

                            if (lblsubj == sub_id.Text)
                            {
                                chk.Checked = true;
                            }
                        }

                    }
                }

                gv_grp_mst.DataSource = "";
                gv_grp_mst.DataBind();
                btn_save.Text = "Update";
                
                btnnew.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }
    protected void btn_clear_Click(object sender, EventArgs e)
    {
        ddlmedium.SelectedIndex = 0;
        ddlmedium_SelectedIndexChanged(sender, e);
        ddlmedium.Enabled = true;
        txtgroupname.Text = "";
        btn_save.Text = "Save";
        btnnew.Enabled = false;
        gv_grp_mst.DataSource = "";
        gv_grp_mst.DataBind();
        grdsub.DataSource = "";
        grdsub.DataBind();
        chkprev.Enabled = false;
        divprev.Attributes.Add("style", "display:none");
        chkprev.Checked = false;
        ddlprevyear.SelectedIndex = 0;
        btn_save.Enabled = false;
    }
    protected void OnDelete(object sender, EventArgs e)
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
                GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
                //DataTable dt = ViewState["dt"] as DataTable;

                string id = ((Label)gv_grp_mst.Rows[row.RowIndex].FindControl("lblgroup")).Text;

                if (id != "")
                {
                    useless();
                    string urlalias = cls.urls();
                    string url = @urlalias + "GroupMaster/";
                    gm.type = "delete";
                    gm.class_id = ddlstandard.SelectedValue.ToString();
                    gm.AYID = Session["acdyear"].ToString();
                    gm.medium_id = ddlmedium.SelectedValue.ToString();
                    gm.group_id = id.ToString();

                    string jsonString = JsonHelper.JsonSerializer<group_master>(gm);
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                    httpWebRequest.ContentType = "application/json";
                    httpWebRequest.Method = "POST";

                    using (StreamWriter sw = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        sw.Write(jsonString);
                        sw.Flush();
                        sw.Close();
                    }

                    var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                    using (StreamReader sr = new StreamReader(httpWebResponse.GetResponseStream()))
                    {
                        string result = sr.ReadToEnd();
                        if (result.ToString().Contains("Saved") == true)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Group deleted successfully', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
                            //dt.Rows.RemoveAt(row.RowIndex);
                            //ViewState["dt"] = dt;
                            //fillgridview of Group Master
                            gridviewFill(ddlmedium.SelectedValue, ddlstandard.SelectedValue);

                        }
                        else if (result.ToString().Contains("Students Exist") == true)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Student already assigned for the group', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
                        }
                        
                    }
                }
                else
                {
                    //dt.Rows.RemoveAt(row.RowIndex);
                    //ViewState["dt"] = dt;
                    //fill grid again

                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Group id not found', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
                }
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }
    protected void chkprev_CheckedChanged(object sender, EventArgs e)
    {

        if (chkprev.Checked == true)
        {
            divprev.Attributes.Add("style", "display:block");
            btnnew.Enabled = false;
            txtgroupname.Text = "";
            txtgroupname.Enabled = false;
            ddlmedium.Enabled = false;
            ddlstandard.Enabled = false;
            grdsub.DataSource = "";
            grdsub.DataBind();
           
        }
        else
        {
            divprev.Attributes.Add("style", "display:none");
            btnnew.Enabled = true;
            txtgroupname.Text = "";
            txtgroupname.Enabled = false;
            ddlmedium.Enabled = true;
            ddlstandard.Enabled = true;
            gv_grp_mst.DataSource = "";
            gv_grp_mst.DataBind();
            ddlprevyear.SelectedIndex = 0;

        }


    }

    protected void ddlprevyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlprevyear.SelectedIndex > 0)
        {
            gridviewFill(ddlmedium.SelectedValue, ddlstandard.SelectedValue);
            txtgroupname.Enabled = false;
            ddlmedium.Enabled = false;
            ddlstandard.Enabled = false;
        }
    }
}