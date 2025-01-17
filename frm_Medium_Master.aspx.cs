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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public partial class frm_Medium_Master : System.Web.UI.Page
{
    mediumclass med = new mediumclass();
    Class1 cls = new Class1();
    string flag = "False";
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
                    BindGrid();
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
        med.med_id = 0;
        med.medium = null;
        med.user_id = null;
        med.curr_date = null;
        med.mod_date = null;
        med.extra1 = null;
        med.extra2 = null;
        med.extra3 = null;
        med.del_flag = 0;
        med.form_code = null;
    }

    protected void BindGrid()
    {
        try
        {
            mediumclass cls2 = new mediumclass();
            string html = string.Empty;
            string result1 = "";

            string urlalias = cls.urls();
            string url = @urlalias + "medium/";
            flag = "False";
            clear();
            med.form_code = "select";

            string jsonString = JsonHelper.JsonSerializer<mediumclass>(med);

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
                result1 = streamReader.ReadToEnd();
            }

            DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(result1);
            DataTable dataTable = dataSet.Tables["Table"];
            GridView1.DataSource = dataTable;
            GridView1.DataBind();

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
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try { }
        catch(Exception ex)
        { ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true); }
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtName.Text.ToString().Trim() != "")
            {
                string html = string.Empty;
                string urlalias = cls.urls();
                string url = @urlalias + "medium/";

                med.med_id = 0;
                med.medium = txtName.Text;

                med.user_id = Session["emp_id"].ToString();
                med.del_flag = 0;
                med.form_code = "insert";

                string jsonString = JsonHelper.JsonSerializer<mediumclass>(med);
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
                    if (result.ToString().Contains("insert") == true)
                    {
                        BindGrid();
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Saved Successfully', { color: '#fff', background: '#198104', blur: 0.2, delay: 0 });", true);
                        txtName.Text = "";
                    }
                    else if (result.ToString().Contains("Exist") == true)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Medium Already Exist', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('No Changes Made', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
                    }
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Enter Medium Name', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "DELETE")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[rowIndex];
                HiddenField lblid = (HiddenField)row.FindControl("lbl_id");
                Label lblflag = (Label)row.FindControl("lblflag");
                if (lblflag.Text == "1")
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Standard Already Assigned To This Medium', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
                    BindGrid();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "open", "Confirm();", true);
                    string confirmValue = Request.Form["confirm_value"];
                    if (confirmValue.Contains(","))
                    {
                        confirmValue = confirmValue.Split(',').Last();
                    }
                    if (confirmValue.ToString() == "Yes")
                    {
                        string html = string.Empty;

                        string urlalias = cls.urls();
                        string url = @urlalias + "medium/";

                        med.med_id = Convert.ToInt32(lblid.Value.ToString());

                        med.del_flag = 0;
                        med.form_code = "delete";

                        string jsonString = JsonHelper.JsonSerializer<mediumclass>(med);
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
                            if (result.ToString().Contains("delete") == true)
                            {
                                BindGrid();
                                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Deleted Successfully', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
                            }
                        }
                    }

                }
            }
            if (e.CommandName == "Edit")
            {
                BindGrid();
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[rowIndex];
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    TextBox txt_partname = (TextBox)GridView1.Rows[i].FindControl("txt_med_name");
                    txt_partname.Visible = true;
                    Label lbl_med = (Label)GridView1.Rows[i].FindControl("lbl_med");
                    LinkButton btn_edit = (LinkButton)GridView1.Rows[i].FindControl("edit_btn");
                    HiddenField lblid = (HiddenField)GridView1.Rows[i].FindControl("lbl_id");
                    LinkButton btn_update = (LinkButton)GridView1.Rows[i].FindControl("updt_btn");
                    LinkButton cancel_btn = (LinkButton)GridView1.Rows[i].FindControl("btn_cancel");
                    Label lblflag = (Label)GridView1.Rows[i].FindControl("lblflag");

                    if (rowIndex == i)
                    {
                        if (lblflag.Text == "1")
                        {

                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Standard Already Assigned To This Medium', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
                            BindGrid();
                        }
                        else
                        {
                            lbl_med.Visible = false;
                            txt_partname.Enabled = true;
                            btn_update.Visible = true;
                            cancel_btn.Visible = true;
                            btn_edit.Visible = false;
                        }
                    }
                    else
                    {
                        txt_partname.Visible = false;
                        lbl_med.Visible = true;
                        txt_partname.Enabled = false;
                        btn_update.Visible = false;
                        cancel_btn.Visible = false;
                        btn_edit.Visible = true;
                    }


                }
            }

            if (e.CommandName == "Update")
            {
                if (flag == "True")
                {
                    int rowIndex1 = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = GridView1.Rows[rowIndex1];
                    TextBox txtmedname = (TextBox)row.FindControl("txt_med_name");
                    HiddenField lblmed = (HiddenField)row.FindControl("lbl_id");
                    Label lblflag = (Label)row.FindControl("lblflag");

                    if (lblflag.Text == "1")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Standard Already Assigned To This Medium', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
                        BindGrid();
                    }
                    else
                    {
                        if (txtmedname.Text.ToString().Trim() != "")
                        {
                            int rowIndex = Convert.ToInt32(e.CommandArgument);
                            GridViewRow row1 = GridView1.Rows[rowIndex];

                            for (int i = 0; i < GridView1.Rows.Count; i++)
                            {
                                TextBox txt_partname = (TextBox)GridView1.Rows[i].FindControl("txt_med_name");
                                txt_partname.Visible = false;

                                Label lbl_med = (Label)GridView1.Rows[i].FindControl("lbl_med");
                                LinkButton btn_update = (LinkButton)GridView1.Rows[i].FindControl("updt_btn");
                                HiddenField lblid = (HiddenField)GridView1.Rows[i].FindControl("lbl_id");
                                if (rowIndex == i)
                                {

                                    if (btn_update.CommandName == "Update")
                                    {
                                        string urlalias = cls.urls();
                                        string url = @urlalias + "medium/";

                                        med.med_id = Convert.ToInt32(lblid.Value.ToString());
                                        med.medium = txt_partname.Text.ToString();
                                        med.del_flag = 0;
                                        med.form_code = "update";

                                        string jsonString = JsonHelper.JsonSerializer<mediumclass>(med);

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
                                            if (result.ToString().Contains("update") == true)
                                            {
                                                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Updated Successfully', { color: '#fff', background: '#198104', blur: 0.2, delay: 0 });", true);
                                                BindGrid();
                                            }
                                            else if (result.ToString().Contains("Exist") == true)
                                            {
                                                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Medium Already Exist', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
                                                BindGrid();
                                            }
                                            else
                                            {
                                                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('No Changes Made', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
                                                BindGrid();
                                            }
                                        }
                                        lbl_med.Visible = true;
                                        btn_update.Visible = false;
                                    }
                                }
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Enter Medium Name', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
                            BindGrid();
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('No Changes Made', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
                    BindGrid();
                }


            }

            if (e.CommandName == "Cancel")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row1 = GridView1.Rows[rowIndex];
                DataTable dt = ViewState["dt"] as DataTable;
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    TextBox txt_partname = (TextBox)GridView1.Rows[i].FindControl("txt_med_name");
                    txt_partname.Visible = false;

                    Label lbl_med = (Label)GridView1.Rows[i].FindControl("lbl_med");
                    LinkButton btn_cncl = (LinkButton)GridView1.Rows[i].FindControl("btn_cancel");
                    HiddenField lblid = (HiddenField)GridView1.Rows[i].FindControl("lbl_id");
                    LinkButton btn_edit = (LinkButton)GridView1.Rows[i].FindControl("edit_btn");
                    LinkButton btn_update = (LinkButton)GridView1.Rows[i].FindControl("updt_btn");
                    if (rowIndex == i)
                    {

                        lbl_med.Visible = true;

                        txt_partname.Visible = false;
                        btn_cncl.Visible = false;
                        btn_edit.Visible = true;
                        btn_update.Visible = false;
                        ViewState["dt"] = dt;
                        GridView1.EditIndex = i;
                        this.BindGrid();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        { }
        catch(Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        { }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        { }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }
    protected void txt_med_name_TextChanged(object sender, EventArgs e)
    {
        try
        {
            flag = "True";
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
        
    }
    protected void del_btn_Click(object sender, EventArgs e)
    {
        try
        { }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }
}