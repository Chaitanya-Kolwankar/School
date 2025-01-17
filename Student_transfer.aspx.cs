using Newtonsoft.Json;
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

public partial class Student_transfer : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ConnectionString);
    Class1 cls = new Class1();
    common cm = new common();
    Stud_transfer st = new Stud_transfer();
    DataTable dt = new DataTable();
    DataTable dt1 = new DataTable();
    string display;



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
                    string ayid = Session["acdyear"].ToString();
                    filltoyear(ayid);

                    medium_select();
                    btnsave.Enabled = false;
                    btndetain.Enabled = false;
                    grid_card.Visible = false;

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





    public void filltoyear(string ayid)
    {
        grid1.DataSource = null;
        grid1.DataBind();
        grid_card.Visible = false;
        try
        {
            string type = "toyear";

            string urlalias = cls.urls();
            string url = @urlalias + "studtransfer/";

            st.type = type.ToString();
            st.ayid = ayid;
            string jsonString = JsonHelper.JsonSerializer<Stud_transfer>(st);


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
                DataSet dsyear = JsonConvert.DeserializeObject<DataSet>(result);
                Session["toyear"] = dsyear;
                DataTable dt1 = dsyear.Tables[0];
                ddltoyear.DataSource = dt1;
                ddltoyear.DataTextField = "duration";
                ddltoyear.DataValueField = "ayid";
                ddltoyear.DataBind();
                ddltoyear.Items.Insert(0, "--Select--");
                ddltoyear.SelectedIndex = 0;


            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }



    private void medium_select()
    {
        try
        {
            string type = "ddlfill";

            string urlalias = cls.urls();
            string url = @urlalias + "Common/";

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
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void ddlmedium_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            btnsave.Enabled = false;
            btndetain.Enabled = false;
            grid1.DataSource = null;
            grid1.DataBind();
            grid_card.Visible = false;
            ddlclass.DataSource = "";
            ddlclass.DataBind();
            transferyear.Text = "";
            ddltoyear.SelectedIndex = 0;
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
                        ddlclass.Items.Insert(0, "--Select--");
                        ddlclass.SelectedIndex = 0;
                        Session["tab"] = table;
                    }
                }

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
            btnsave.Enabled = false;
            btndetain.Enabled = false;
            grid1.DataSource = null;
            grid1.DataBind();
            grid_card.Visible = false;
            transferyear.Text = "";
            ddltoyear.SelectedIndex = 0;
            
            if (ddlclass.SelectedIndex == 0)
            {

                transferyear.Text = "";
                canceltransfer.Enabled = false;
                canceltransfer.Checked = false;
            }
            else
            {
                canceltransfer.Enabled = true;
               
                int next = Convert.ToInt32(ddlclass.SelectedIndex) + 1;
                if (ddlclass.Items.Count <= next)
                {
                    transferyear.Text = "";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify( 'Define Next Class to Assign Student', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                    return;
                }
                else
                {
                    transferyear.Text = ddlclass.Items[next].Text;
                }
                class_sel();
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    public void class_sel()
    {
        DataTable assigneddt;
        try
        {
            if (ddlmedium.SelectedIndex > 0 && ddlclass.SelectedIndex > 0)
            {
                int next = Convert.ToInt32(ddlclass.SelectedIndex) + 1;


                string medium = ddlmedium.SelectedValue;
                string cls1 = ddlclass.SelectedValue;
                string ayid = Session["acdyear"].ToString();

                string urlalias = cls.urls();
                string url = @urlalias + "studtransfer/";

                st.medium = medium;
                st.standard = cls1;
                st.ayid = ayid;
                string nextclass = " select std_id from mst_standard_tbl where std_name = '" + transferyear.Text + "' and med_id = '" + ddlmedium.SelectedValue + "' and del_flag = 0;";
                DataTable nextclassdt = cls.filldatatable(nextclass);
                st.nextclass = nextclassdt.Rows[0][0].ToString();
                st.type = "fgvfill";

                string jsonString = JsonHelper.JsonSerializer<Stud_transfer>(st);


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

                    //string divresult = sr.ReadToEnd();
                    //DataTable divdt = JsonConvert.DeserializeObject<DataTable>(divresult);

                    //if (divdt.Rows.Count == 0)
                    //{
                    //    display = "Division Not Forwded To Current Year";
                    //}

                    //else
                    
                        string result = sr.ReadToEnd();
                        DataSet ds = JsonConvert.DeserializeObject<DataSet>(result);
                        assigneddt = ds.Tables[1];
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            dt.Columns.Add("Student_id", typeof(string));
                            dt.Columns.Add("Student_Name", typeof(string));
                            dt.Columns.Add("Division", typeof(string));

                            dt.Columns.Add("division_id", typeof(string));
                            dt.Columns.Add("GroupName", typeof(string));
                           dt.Columns.Add("Groupid", typeof(string));
                           dt.Columns.Add("type_flag", typeof(string));
                            DataRow dr1;
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                string type = ds.Tables[0].Rows[i]["type"].ToString();
                                if (type == "0")
                                {


                                    string stud_id = ds.Tables[0].Rows[i]["Student_id"].ToString();
                                    string name = ds.Tables[0].Rows[i]["Student_Name"].ToString();
                                    string division = ds.Tables[0].Rows[i]["Division"].ToString();
                                    string div_id = ds.Tables[0].Rows[i]["division_id"].ToString();
                                    string group_name = ds.Tables[0].Rows[i]["GroupName"].ToString();
                                    string groupid = ds.Tables[0].Rows[i]["Groupid"].ToString();
                                    string type_flag = ds.Tables[0].Rows[i]["type"].ToString();


                                    dr1 = dt.NewRow();
                                    dr1[0] = stud_id.ToString();
                                    dr1[1] = name.ToString();
                                    dr1[2] = division.ToString();
                                    dr1[3] = div_id.ToString();
                                    dr1[4] = group_name.ToString();
                                dr1[5] = groupid.ToString();
                                dt.Rows.Add(dr1);

                                }
                                else
                                {
                                    //  add each of the data rows to the table

                                    string stud_id = ds.Tables[0].Rows[i]["Student_id"].ToString();
                                    string name = ds.Tables[0].Rows[i]["Student_Name"].ToString();
                                    string division = ds.Tables[0].Rows[i]["Division"].ToString();
                                    string group_name = ds.Tables[0].Rows[i]["GroupName"].ToString();
                                    string Groupid = ds.Tables[0].Rows[i]["Groupid"].ToString();

                                    string type_flag = ds.Tables[0].Rows[i]["type"].ToString();


                                    dr1 = dt.NewRow();
                                    dr1[0] = stud_id.ToString();
                                    dr1[1] = name.ToString();
                                    dt.Rows.Add(dr1);
                                }
                            }
                        }

                        //here taken
                      
                    

                  
                }

                if (dt.Rows.Count == 0)
                {
                    grid_card.Visible = false;
                    if (assigneddt.Rows.Count > 0)
                    {
                        display = "Student are already promoted or not available for selected class";

                    }
                    else
                    {
                        display = "Students not defined for selected Class";
                    }
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify( '" + display + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                    return;
                }
                else if (display == "no")
                {
                    grid_card.Visible = false;
                    display = "No data found for this standard and medium select another";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify( '" + display + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                    return;
                }

                if (dt.Rows.Count > 0)
                {
                    btnsave.Enabled = true;
                    btndetain.Enabled = true;
                    ViewState["dta"] = dt;
                    grid1.DataSource = dt;
                    grid1.DataBind();
                    grid1.Visible = true;
                    grid1.HeaderRow.TableSection = TableRowSection.TableHeader;

                    btnsave.Enabled = true;
                    if (canceltransfer.Checked == true)
                    {
                        grid_card.Visible = true;
                    }
                    else
                    {
                        grid_card.Visible = true;
                    }


                }
                else
                {
                    btnsave.Enabled = false;
                    btndetain.Enabled = false;
                    grid1.DataSource = dt;
                    grid1.DataBind();
                    grid1.Visible = false;
                    grid_card.Visible = false;
                }


                if (ddlclass.Items.Count <= next)
                {
                    transferyear.Text = "";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify( 'Define Next Class to Assign Student', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                    return;
                }
                else
                {
                    transferyear.Text = ddlclass.Items[next].Text;
                }
            }
            else
            {

                if (ddlmedium.SelectedIndex == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify( 'Select Medium', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);

                }

            }
        }

        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);

        }
    }

    private void ld()
    {
        try
        {

            DataTable dt = (DataTable)ViewState["dta"];
            foreach (GridViewRow row in grid1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkcheck = (CheckBox)row.FindControl("chksel");
                    chkcheck.Checked = true;
                    (row.FindControl("lblchk") as Label).Text = "1";
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void chkall_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in grid1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chk = (CheckBox)grid1.HeaderRow.FindControl("chkall");

                    if (chk.Checked == true)
                    {
                        CheckBox chkcheck = (CheckBox)row.FindControl("chksel");
                        chkcheck.Checked = true;
                        (row.FindControl("lblchk") as Label).Text = "1";

                    }
                    else
                    {
                        CheckBox chkcheck = (CheckBox)row.FindControl("chksel");
                        chkcheck.Checked = false;
                        (row.FindControl("lblchk") as Label).Text = "0";

                    }
                }
            }
            grid_card.Visible = true;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    private void chkload()
    {
        try
        {
            foreach (GridViewRow row in grid1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chk = (CheckBox)grid1.HeaderRow.FindControl("chkall");

                    if (chk.Checked == true)
                    {
                        CheckBox chkcheck = (CheckBox)row.FindControl("chksel");
                        chkcheck.Checked = true;
                        (row.FindControl("lblchk") as Label).Text = "1";
                    }
                    else
                    {
                        CheckBox chkcheck = (CheckBox)row.FindControl("chksel");
                        chkcheck.Checked = false;
                        (row.FindControl("lblchk") as Label).Text = "0";
                    }
                }
            }
            grid_card.Visible = true;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void canceltransfer_CheckedChanged(object sender, EventArgs e)
    {
        try
        {

            if (ddlmedium.SelectedIndex > 0 && ddlclass.SelectedIndex > 0 && ddlclass.SelectedItem.Text != "")
            {
                canceltransfer.Enabled = true;
                if (canceltransfer.Checked == true)
                {
                    ddltoyear.SelectedIndex = 0;
                    ddltoyear.Enabled = false;
                    int next = Convert.ToInt32(ddlclass.SelectedIndex) + 1;

                    transferyear.Text = "";
                    btndetain.Enabled = false;
                    btnsave.Text = "Cancel Transfer";

                    string ayid = Session["acdyear"].ToString();
                    if (ddlclass.Items.Count <= next)
                    {
                        transferyear.Text = "";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify( 'Define Next Class to Assign Student', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                        return;
                    }
                    else
                    {
                        transferyear.Text = ddlclass.Items[next].Text;
                    }
                    if (grid1.Rows.Count == 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify( 'No Students Found for this Class', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
         
                        return;
                    }

                    dt1 = GetDataTable(grid1);

                    st.json = DataTableToJSON(dt1);

                    string html = string.Empty;

                    string urlalias = cls.urls();
                    string url = @urlalias + "studtransfer/";


                    st.ayid = ayid;
                    st.medium = ddlmedium.SelectedValue;
                    st.standard = ddlclass.SelectedValue;

                    st.type = "selectcancel";
                    string jsonString = JsonHelper.JsonSerializer<Stud_transfer>(st);


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
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        string result = streamReader.ReadToEnd();
                        DataSet ds = JsonConvert.DeserializeObject<DataSet>(result);
                        dt = ds.Tables[0];
                        dt1 = ds.Tables[1];
                        if (dt.Rows.Count > 0)
                        {
                            grid1.Columns[4].Visible = false;
                            grid1.DataSource = dt;
                            grid1.DataBind();

                            grid_card.Visible = true;
                            chkload();
                        }
                        else
                        {
                            btnsave.Text = "Promote";
                            btnsave.Enabled = false;
                            btndetain.Enabled = false;
                            grid1.DataSource = null;
                            grid1.DataBind();
                            grid_card.Visible = false;
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify('No Students Found for this Class', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                            canceltransfer.Checked = false;
                            ddltoyear.Enabled = true;
                            filltoyear(ayid);
                             grid1.Columns[4].Visible = false;
                            //Response.Redirect(Request.RawUrl);
                        }

                        if (dt1.Rows.Count > 0)
                        {

                            grid2.DataSource = dt1;
                            grid2.DataBind();

                        }
                    }

                }
                else
                {
                    ddltoyear.SelectedIndex = 0;
                    ddltoyear.Enabled = true;
                    ddlclass.SelectedIndex = 0;
                    ddlmedium.SelectedIndex = 0;
                    grid1.Columns[4].Visible = true;
                    grid1.DataSource = null;
                    grid1.DataBind();

                    grid_card.Visible = false;
                    transferyear.Text = "";
                    btndetain.Enabled = false;
                    btnsave.Enabled = false;
                    btnsave.Text = "Promote";
                    ddlclass.DataSource = "";
                    ddlclass.DataBind();


                }

            }
            else
            {

                canceltransfer.Enabled = false;
                canceltransfer.Checked = false;
                grid_card.Visible = false;

                if (ddlmedium.SelectedIndex == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify( 'Select Appropriate Medium ', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                    return;
                }
                else if (ddlclass.SelectedIndex == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify( 'Select Appropriate Standard', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);

                    Response.Redirect(Request.RawUrl);
                    return;
                }


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

    DataTable GetDataTable(GridView dtg)
    {

        DataTable dt = new DataTable();

        // add the columns to the datatable            
        dt.Columns.Add("STUDENT ID", typeof(string));
       
        dt.Columns.Add("CHECK", typeof(string));
     
        dt.Columns.Add("DIVISION", typeof(string));
        dt.Columns.Add("Division Id", typeof(string));
        dt.Columns.Add("Group Name", typeof(string));
        dt.Columns.Add("Group Id", typeof(string));
        //  add each of the data rows to the table
        foreach (GridViewRow row in dtg.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {

                string stud_id = (row.FindControl("lblstud_id") as Label).Text;
            
                string check = (row.FindControl("lblchk") as Label).Text;
                string division = (row.FindControl("lbldiv") as Label).Text;
                string division_id = (row.FindControl("lbldivid") as Label).Text;
                string group_name = (row.FindControl("lblgrp") as Label).Text;
                string groupid = (row.FindControl("lblgroupid") as Label).Text;

                if (check == "1")
                {
                    DataRow dr1;
                    dr1 = dt.NewRow();
                    dr1[0] = stud_id.ToString();
                    dr1[1] = check.ToString();
                    dr1[2] = division.ToString();
                    dr1[3] = division_id.ToString();
                    dr1[4] = group_name.ToString();
                    dr1[5] = groupid.ToString();

                    dt.Rows.Add(dr1);
                }
            }
        }
        return dt;
    }



    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {

            if (btnsave.Text == "Promote")
            {
                if (ddltoyear.SelectedIndex > 0 && ddlmedium.SelectedIndex > 0 && ddlclass.SelectedIndex > 0 && ddlclass.SelectedItem.Text != "")
                {
                    int next = Convert.ToInt32(ddlclass.SelectedIndex) + 1;
                    if (ddlclass.Items.Count <= next)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify( 'Define Next Class to Assign Student', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                        return;
                    }
                    else
                    {
                        if (grid1.Rows.Count == 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify( 'No Students Found for this Class', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                            return;
                        }
                        DataTable dt1 = GetDataTable(grid1);
                        if (dt1.Rows.Count == 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify( 'No Students Selected for Promoting', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                            return;
                        }
                        st.json = DataTableToJSON(dt1);

                        string promotedivyear = ddltoyear.SelectedValue;
                        st.promotedivyear = promotedivyear;

                        transferyear.Text = ddlclass.Items[next].Text;
                        st.standard = transferyear.Text;

                        string html = string.Empty;

                        string urlalias = cls.urls();
                        string url = @urlalias + "studtransfer/";


                        st.ayid = ddltoyear.SelectedValue;

                        st.standard = ddlclass.Items[next].Value;
                        st.medium = ddlmedium.SelectedItem.Value.ToString();
                        st.type = "insert";
                        string jsonString = JsonHelper.JsonSerializer<Stud_transfer>(st);


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
                        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                        {
                            string result = streamReader.ReadToEnd();
                            DataSet ds = JsonConvert.DeserializeObject<DataSet>(result);
                            dt = ds.Tables[0];
                            if (dt.Rows.Count > 0)
                            {

                                if (dt.Rows[0]["1"].ToString() == "Red")
                                {
                                    display = dt.Rows[0]["msg"].ToString();

                                    canceltransfer.Checked = false;
                                    string y = " Class " + ddlclass.Items[next].Text + "  for Academic Year " + ddltoyear.SelectedItem.Text;
                                    int iv = ddlclass.SelectedIndex + 1;
                                    string it = ddlclass.Items[iv].ToString();
                                    btnsave.Enabled = false;
                                    clear();
                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify( '" + display + "  " + y + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0  })", true);
                                    return;
                                }

                                else if (dt.Rows[0]["0"].ToString() == "Green")
                                {
                                    display = dt.Rows[0]["msg"].ToString();

                                    canceltransfer.Checked = false;
                                    string y = " Class " + ddlclass.Items[next].Text + "  for Academic Year " + ddltoyear.SelectedItem.Text;
                                    int iv = ddlclass.SelectedIndex + 1;
                                    string it = ddlclass.Items[iv].ToString();
                                    btnsave.Enabled = false;
                                    clear();
                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify( '" + display + "  " + y + "', { color: '#fff', background: '#168029', blur: 0.2, delay: 0 })", true);
                                    return;
                                }




                               
                            }




                        }
                    }
                }
                else
                {
                   
                     if (ddlmedium.SelectedIndex == 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify( 'Select Appropriate Medium ', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                        ddlmedium.Focus();
                        return;
                    }
                    else if (ddlclass.SelectedIndex == 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify( 'Select Appropriate Class', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                        ddlclass.Focus();
                        return;
                    }
                    else if (ddltoyear.SelectedIndex == 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify( 'Select Appropriate Year to Transfer', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                        ddltoyear.Focus();
                        return;
                    }
                }
            }
            else
            {
                if (ddlmedium.SelectedIndex > 0 && ddlclass.SelectedIndex > 0 && ddlclass.SelectedItem.Text != "")
                {
                    if (grid1.Rows.Count == 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify( 'No Students Found for this Class', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                        return;
                    }
                    DataTable dt1 = GetDataTable(grid1);
                    if (dt1.Rows.Count == 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify( 'No Students Selected for Cancelling', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                        return;
                    }
                    st.json = DataTableToJSON(dt1);
                    string ayid = Session["acdyear"].ToString();
                    string html = string.Empty;
                    string urlalias = cls.urls();
                    string url = @urlalias + "studtransfer/";

                    st.ayid = ayid;

                    st.currstd = ddlclass.SelectedItem.Value.ToString();


                    st.type = "update";
                    string jsonString = JsonHelper.JsonSerializer<Stud_transfer>(st);


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
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        string result = streamReader.ReadToEnd();
                        DataSet ds = JsonConvert.DeserializeObject<DataSet>(result);
                        dt = ds.Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            display = dt.Rows[0]["msg"].ToString();
                            clear();
                            canceltransfer.Checked = false;
                            ddltoyear.Enabled = true;
                            btnsave.Enabled = false;
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify( '" + display + "', { color: '#fff', background: '#168029', blur: 0.2, delay: 0 })", true);
                            return;
                        }

                    }
                }
                else
                {

                    if (ddlmedium.SelectedIndex == 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify( 'Select Appropriate Medium ', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                        ddlmedium.Focus();
                        return;
                    }
                    else if (ddlclass.SelectedIndex == 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify( 'Select Appropriate Class', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                        ddlclass.Focus();
                        return;
                    }
                }

            }

            clear();
            if (dt.Rows.Count == 0)
            {

                grid_card.Visible = false;
                display = "No data found for this standard and medium ";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify( '" + display + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                return;
            }
            else if (display == "no")
            {
                grid_card.Visible = false;
                display = "No data found for this standard and medium select another";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify( '" + display + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                return;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    private string msg1()
    {
        string msg;
        DataTable dt = (DataTable)Session["dtyear"];

        string ayid = Session["acdyear"].ToString();

        string ndt = dt.Rows[0]["AYID"].ToString();
        if (ndt == ayid)
        {
            msg = "Student Already Assigned for next year";
        }
        else
        {
            msg = "No Data Found for this standard";
        }
        return msg;

    }

    private void clear()
    {
        ddlmedium.SelectedIndex = 0;
        ddlclass.SelectedIndex = 0;
        ddltoyear.SelectedIndex = 0;
        transferyear.Text = "";
        btn_error.Visible = false;
        badge.InnerText = "";
        grid_card.Visible = false;
        btnsave.Enabled = false;
        btndetain.Enabled = false;
        btnsave.Text = "Promote";
    }
    protected void Clear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }
    protected void chksel_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow Row = ((GridViewRow)((Control)sender).Parent.Parent);

            CheckBox chk = (CheckBox)grid1.HeaderRow.FindControl("chkall");


            if ((Row.FindControl("chksel") as CheckBox).Checked == true)
            {
                chk.Checked = false;

                (Row.FindControl("lblchk") as Label).Text = "1";
            }
            else
            {
                chk.Checked = false;

                (Row.FindControl("lblchk") as Label).Text = "0";
            }


            grid_card.Visible = true;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void btndetain_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddltoyear.SelectedIndex > 0 && ddlmedium.SelectedIndex > 0 && ddlclass.SelectedIndex > 0 && ddlclass.SelectedItem.Text != "")
            {

                if (grid1.Rows.Count == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify( 'No Students Found for this Class', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                    return;
                }
                DataTable dt1 = GetDataTable(grid1);
                if (dt1.Rows.Count == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify( 'No Students Selected for Detaining', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                    return;
                }
                st.json = DataTableToJSON(dt1);

                string html = string.Empty;

                string urlalias = cls.urls();
                string url = @urlalias + "studtransfer/";


                st.ayid = ddltoyear.SelectedValue;
                string promotedivyear = ddltoyear.SelectedValue;
                st.promotedivyear = promotedivyear;
                st.standard = ddlclass.SelectedValue;
                st.medium = ddlmedium.SelectedItem.Value.ToString();
                st.type = "insert";
                string jsonString = JsonHelper.JsonSerializer<Stud_transfer>(st);


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
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    string result = streamReader.ReadToEnd();
                    DataSet ds = JsonConvert.DeserializeObject<DataSet>(result);
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        display = dt.Rows[0]["msg"].ToString();

                        canceltransfer.Checked = false;
                        string y = " Class " + ddlclass.SelectedItem.Text + "  for Academic Year " + ddltoyear.SelectedItem.Text;
                        int iv = ddlclass.SelectedIndex + 1;
                        string it = ddlclass.Items[iv].ToString();
                        btnsave.Enabled = false;
                        clear();
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify( '" + display + "  " + y + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                        return;
                    }

                }

            }
            else
            {
                if (ddltoyear.SelectedIndex == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify( 'Select Appropriate To Year', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                    return;
                }
                else if (ddlmedium.SelectedIndex == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify( 'Select Appropriate Medium ', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                    return;
                }
                else if (ddlclass.SelectedIndex == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify( 'Select Appropriate Standard', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }


    protected void ddltoyear_SelectedIndexChanged(object sender, EventArgs e)
    {



        string urlalias = cls.urls();
        string url = @urlalias + "studtransfer/";

        st.type = "divcheck";

        string medium;
        medium = ddlmedium.SelectedValue;

        string promotedivyear = ddltoyear.SelectedValue;
        st.promotedivyear = promotedivyear;
        int next = Convert.ToInt32(ddlclass.SelectedIndex) + 1;
        transferyear.Text = ddlclass.Items[next].Text;

        st.standard = transferyear.Text;
        string jsonString = JsonHelper.JsonSerializer<Stud_transfer>(st);


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


            if (ds.Tables[0].Rows.Count == 0)

            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify( 'Division Not Forwarded For Current Year', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);

                btnsave.Enabled = false;
            }

            else
            {
                btnsave.Enabled = true;
            }

        }

        st.type = "grpcheck";
        string jsonString1 = JsonHelper.JsonSerializer<Stud_transfer>(st);
        var httpWebRequest1 = (HttpWebRequest)WebRequest.Create(url);
        httpWebRequest1.ContentType = "application/json";
        httpWebRequest1.Method = "POST";

        using (var streamWriter1 = new StreamWriter(httpWebRequest1.GetRequestStream()))
        {

            streamWriter1.Write(jsonString1);
            streamWriter1.Flush();
            streamWriter1.Close();
        }

        var httpResponse1 = (HttpWebResponse)httpWebRequest1.GetResponse();

        using (StreamReader sr1 = new StreamReader(httpResponse1.GetResponseStream()))
        {

            string result1 = sr1.ReadToEnd();
            DataSet ds = JsonConvert.DeserializeObject<DataSet>(result1);

            if (  st.standard == "9" || st.standard == "10"  || st.standard == "९ वी" || st.standard == "१० वी")
            {


                if (ds.Tables[0].Rows.Count == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify( 'Group Not Forwarded For Current Year', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);

                    btnsave.Enabled = false;
                }


                else
                {
                    btnsave.Enabled = true;
                }
            }


            //DataTable dt = new DataTable();
            //dt = ds.Tables["table"];




        }


    }
    
}