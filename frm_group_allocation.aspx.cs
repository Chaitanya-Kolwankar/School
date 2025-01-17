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

public partial class frm_group_allocation : System.Web.UI.Page
{
    grpallocation gm = new grpallocation();
    Class1 cls = new Class1();
    common cm = new common();
    Allocation ac = new Allocation();
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
        gm.group_id = null;
        gm.stud_id = null;
        gm.AYID = null;
        gm.division = null;
        gm.type = null;



    }
    public void ddlfill()
    {
        try
        {
            ViewState["dtgrp"] = "";
            ddl_division.Enabled = false;
            ddl_group.Enabled = false;
            ddlstandard.Enabled = false;
            btn_save.Enabled = false;
            btn_edit.Enabled = false;
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
            btn_save.Text = "Save";
            btn_save.Enabled = false;
            btn_edit.Enabled = false;
            ddl_group.DataSource = "";
            ddl_group.DataBind();
            ddl_group.Enabled = false;
            ddl_division.DataSource = "";
            ddl_division.DataBind();
            ddl_division.Enabled = false;
            rdoname.Checked = true;
            rdorolldiv.Checked = false;
            ViewState["dtgrp"] = null;

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
            }
          
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }
    protected void ddlstandard_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            gv_grp_mst.DataSource = "";
            gv_grp_mst.DataBind();
            btn_save.Text = "Save";
            btn_save.Enabled = false;
            ddl_group.DataSource = "";
            ddl_group.DataBind();
            ddl_group.Enabled = false;
            ddl_division.DataSource = "";
            ddl_division.DataBind();
            ddl_division.Enabled = false;
            rdoname.Checked = true;
            rdorolldiv.Checked = false;
            ViewState["dtgrp"] = null;
            btn_edit.Enabled = false;

            if (ddlstandard.SelectedIndex != 0)
            {
                groupFill(ddlmedium.SelectedValue, ddlstandard.SelectedValue);
                gridviewFill(ddlmedium.SelectedValue, ddlstandard.SelectedValue, "", ddl_group.SelectedValue, "");
                DataSet ds = new DataSet();
                string urlalias = cls.urls();
                string url = @urlalias + "all/";

                ac.type = "divload";
                ac.medium = ddlmedium.SelectedValue.ToString();
                ac.classid = ddlstandard.SelectedValue.ToString();
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
                        ddl_division.DataSource = ds.Tables[0];
                        ddl_division.DataTextField = "division_name";
                        ddl_division.DataValueField = "division_id";
                        ddl_division.DataBind();
                        ddl_division.Items.Insert(0, "--Select--");
                        ddl_division.SelectedIndex = 0;
                        ddl_division.Enabled = true;
                        if (Session["msg"].ToString() != "")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + Session["msg"].ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                        }
                    }
                    else
                    {
                        ddl_division.DataSource = "";
                        ddl_division.DataBind();
                        ddl_division.Enabled = false;
                        if (Session["msg"].ToString() != "")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + Session["msg"].ToString() + " & No division assigned for current academic year  ', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                        }
                        else
                        {

                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('No division assigned for current academic year', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                        }
                    }
                }


            }
            else
            {

            }
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

  
    protected void btn_save_Click(object sender, EventArgs e)
    {
        try
        {
            string stud_id = "";
            if (ddlmedium.SelectedIndex == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Select medium', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
            }
            else if (ddlstandard.SelectedIndex == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Select standard', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
            }
            else if (ddl_group.SelectedIndex == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Select group', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
            }
            else
            {
                foreach (GridViewRow row in gv_grp_mst.Rows)
                {

                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkRow = (row.FindControl("chkgroup") as CheckBox);
                        if (btn_save.Text == "Save")
                        {
                            if (chkRow.Checked==true)
                            {
                                if (stud_id == "")
                                {
                                    stud_id = (row.FindControl("lbl_studid") as Label).Text;
                                }
                                else
                                {
                                    stud_id = stud_id + "," + (row.FindControl("lbl_studid") as Label).Text;
                                }
                            }
                        }
                        else
                        {
                            if (chkRow.Checked==false)
                            {
                                if (stud_id == "")
                                {
                                    stud_id = (row.FindControl("lbl_studid") as Label).Text;
                                }
                                else
                                {
                                    stud_id = stud_id + "," + (row.FindControl("lbl_studid") as Label).Text;
                                }
                            }
                        }
                    }
                }
                if (stud_id == "")
                {
                    if (btn_save.Text == "Save")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Select atleast one student to assign in group', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Uncheck students to remove from group', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                    }

                }
                else
                {
                    useless();
                    ViewState["dtgrp"] = null;
                    string urlalias = cls.urls();
                    string url = @urlalias + "GroupAllocation/";
                    if(btn_save.Text=="Save")
                    {
                    gm.type = "Insert";
                    }
                    else
                    {
                         gm.type = "Delete";
                    }
                    gm.class_id = ddlstandard.SelectedValue;
                    gm.AYID = Session["acdyear"].ToString();
                    gm.medium_id = ddlmedium.SelectedValue;
                    gm.group_id = ddl_group.SelectedValue;
                    gm.stud_id = stud_id;
                    if (ddl_division.SelectedIndex == 0)
                    {
                        gm.division = null;
                    }
                    else
                    {
                        gm.division = ddl_division.SelectedValue;
                    }
                    string jsonString = JsonHelper.JsonSerializer<grpallocation>(gm);
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
                            if (btn_save.Text == "Save")
                            {
                                
                                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Student assigned to group successfully', { color: '#fff', background: '#198104', blur: 0.2, delay: 0 });", true);
                                ddl_group_SelectedIndexChanged(sender, e);

                            }
                            else
                            {
                               
                                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Student group removed successfully', { color: '#fff', background: '#198104', blur: 0.2, delay: 0 });", true);
                                btn_edit_Click(sender, e);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + result.ToString() + "', { color: '#fff', background: '#198104', blur: 0.2, delay: 0 });", true);
                        }
                    }
                }
            }
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" +ex.Message + "', { color: '#fff', background: '#198104', blur: 0.2, delay: 0 });", true);
        }
    }
    protected void btn_clear_Click(object sender, EventArgs e)
    {
        ddlmedium.SelectedIndex = 0;

        ddlstandard.DataSource = "";
        ddlstandard.DataBind();
        ddlstandard.Enabled = false;
        ddl_group.DataSource = "";
        ddl_group.DataBind();
        ddl_group.Enabled = false;
        ddl_division.DataSource = "";
        ddl_division.DataBind();
        ddl_division.Enabled = false;
        btn_save.Enabled = false;
        btn_edit.Enabled = false;
        gv_grp_mst.DataSource = "";
        gv_grp_mst.DataBind();
        rdoname.Checked = true;
        rdorolldiv.Checked = false;
        ViewState["dtgrp"] = null;
    }

    public void gridviewFill(string med_id, string class_id,string division,string group_id,string grptype)
    {
        try
        {
            Session["msg"] = "";
            if (med_id != "" && class_id != "")
            {
                useless();
                ViewState["dtgrp"] = null;
                string urlalias = cls.urls();
                string url = @urlalias + "GroupAllocation/";
                gm.type = "gvfill";
                gm.class_id = class_id;
                gm.AYID = Session["acdyear"].ToString();
                gm.medium_id = med_id;
                gm.group_id = group_id;
                gm.grouptype = grptype;
                if (division != "")
                {
                    gm.division = ddl_division.SelectedValue;
                }
                else
                {
                    gm.division = null;
                }
                if(rdoname.Checked==true)
                {
                    gm.orderby = "name";
                }
                else
                {
                    gm.orderby = "divroll";
                }
                string jsonString = JsonHelper.JsonSerializer<grpallocation>(gm);
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
                        if (grptype == "edit")
                        {
                            btn_save.Text = "Remove";
                            
                            CheckBox ChkBoxHeader = (CheckBox)gv_grp_mst.HeaderRow.FindControl("chkallgroup");
                            ChkBoxHeader.Checked = true;
                            foreach (GridViewRow row in gv_grp_mst.Rows)
                            {

                                if (row.RowType == DataControlRowType.DataRow)
                                {
                                    CheckBox chkRow = (row.FindControl("chkgroup") as CheckBox);
                                    chkRow.Checked = true;
                                }
                            }

                        }
                        else
                        {
                            btn_save.Text = "Save";
                        }
                        btn_save.Enabled = true;
                        

                       
                    }
                    else
                    {
                        gv_grp_mst.DataSource = "";
                        gv_grp_mst.DataBind();
                        ViewState["dtgrp"] = null;
                        if (grptype == "edit")
                        {
                            //btn_save.Text = "Save";
                            ddl_group.SelectedIndex = 0;
                        }
                        else
                        {
                            //btn_save.Text = "Save";
                        }
                        btn_save.Text = "Save";
                        btn_save.Enabled = false;
                        
                        if (ds.Tables[1].Rows.Count > 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Students are Already Assigned to Groups', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('No students are available for allocating group', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                        }
                        
                    }
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        btn_edit.Enabled = true;
                    }
                    else
                    {
                        btn_edit.Enabled = false;
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

     public void groupFill(string med_id, string class_id)
     {
        try
        {

            if (med_id != "" && class_id != "")
            {
                useless();
                string urlalias = cls.urls();
                string url = @urlalias + "GroupAllocation/";
                gm.type = "fillgroup";
                gm.class_id = class_id;
                gm.AYID = Session["acdyear"].ToString();
                gm.medium_id = med_id;

                string jsonString = JsonHelper.JsonSerializer<grpallocation>(gm);
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
                        ddl_group.DataSource = ds.Tables[0];
                        ddl_group.DataTextField = "group_name";
                        ddl_group.DataValueField = "group_id";
                        ddl_group.DataBind();
                        ddl_group.Items.Insert(0, "--Select--");
                        ddl_group.SelectedIndex = 0;
                        ddl_group.Enabled = true;
                        Session["msg"] = "";
                    }
                    else
                    {
                        ddl_group.DataSource = "";
                        ddl_group.Enabled = false;
                        ddl_group.DataBind();
                        Session["msg"] = "Groups not defined for selected medium and standard";
                       
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
     

    protected void ddl_division_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if(ddl_group.SelectedIndex==0)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Select Group to Load', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
            }
            else if (ddl_division.SelectedIndex == 0)
            {
                if (btn_save.Text == "Save")
                {
                    gridviewFill(ddlmedium.SelectedValue, ddlstandard.SelectedValue, "", ddl_group.SelectedValue, "");
                }
                else
                {
                    gridviewFill(ddlmedium.SelectedValue, ddlstandard.SelectedValue, "", ddl_group.SelectedValue, "edit");
                }
            }
            else
            {
                if (btn_save.Text == "Save")
                {
                    gridviewFill(ddlmedium.SelectedValue, ddlstandard.SelectedValue, ddl_division.SelectedValue, ddl_group.SelectedValue, "");
                }
                else
                {
                    gridviewFill(ddlmedium.SelectedValue, ddlstandard.SelectedValue, ddl_division.SelectedValue, ddl_group.SelectedValue, "edit");
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void chkallgroup_CheckedChanged(object sender, EventArgs e)
    {
        try
        {

            CheckBox ChkBoxHeader = (CheckBox)gv_grp_mst.HeaderRow.FindControl("chkallgroup");
            foreach (GridViewRow row in gv_grp_mst.Rows)
            {
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkgroup");
                if (ChkBoxHeader.Checked == true)
                {
                    ChkBoxRows.Checked = true;
                }
                else
                {
                    ChkBoxRows.Checked = false;
                }
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }

        
    }
    protected void btn_edit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlmedium.SelectedIndex == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Select Medium', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
            }
            else if (ddlstandard.SelectedIndex == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Select Standard', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
            }
            else if (ddl_group.SelectedIndex == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Select Group to edit', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);

            }
            else
            {
                if (ddl_division.SelectedIndex == 0)
                {
                    gridviewFill(ddlmedium.SelectedValue, ddlstandard.SelectedValue, "", ddl_group.SelectedValue, "edit");
                }
                else
                {
                    gridviewFill(ddlmedium.SelectedValue, ddlstandard.SelectedValue, ddl_division.SelectedValue, ddl_group.SelectedValue, "edit");
                }
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }
    protected void ddl_group_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            gv_grp_mst.DataSource = "";
            gv_grp_mst.DataBind();
            ViewState["dtgrp"] = null;
            btn_edit.Enabled = false;
            if (ddl_division.Items.Count > 0)
            {
                ddl_division.SelectedIndex = 0;
            }
            if (ddlmedium.SelectedIndex == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Select Medium.', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
            }
            else if (ddlstandard.SelectedIndex == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Select Standard.', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
            }
            else if(ddl_group.SelectedIndex==0)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Select Group.', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
            }
            else
            {
                gridviewFill(ddlmedium.SelectedValue, ddlstandard.SelectedValue, "", ddl_group.SelectedValue, "");
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }
    protected void rdoname_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = ViewState["dtgrp"] as DataTable;
            if (dt != null)
            {
                DataView dv = new DataView(dt);
                dv.Sort = "Name ASC";
                gv_grp_mst.DataSource = dv;
                gv_grp_mst.DataBind();

                if (btn_save.Text != "Save")
                {
                    CheckBox ChkBoxHeader = (CheckBox)gv_grp_mst.HeaderRow.FindControl("chkallgroup");
                    ChkBoxHeader.Checked = true;
                    chkallgroup_CheckedChanged(sender, e);
                }
              

            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }
    protected void rdorolldiv_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = ViewState["dtgrp"] as DataTable;
            if (dt != null)
            {
                DataView dv = new DataView(dt);
                dv.Sort = "Roll_no,division_name ASC";
                gv_grp_mst.DataSource = dv;
                gv_grp_mst.DataBind();

                if (btn_save.Text != "Save")
                {
                    CheckBox ChkBoxHeader = (CheckBox)gv_grp_mst.HeaderRow.FindControl("chkallgroup");
                    ChkBoxHeader.Checked = true;
                    chkallgroup_CheckedChanged(sender, e);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }
    
}