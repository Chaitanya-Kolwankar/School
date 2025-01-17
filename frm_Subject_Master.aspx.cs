using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frm_Subject_Master : System.Web.UI.Page
{
    subject_master sm = new subject_master();
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
                    btnSubmit.Visible = false;
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
        sm.medium_id = null;
        sm.class_id = null;
        sm.subject_id = null;
        sm.subject_name = null;
        sm.criteria = null;
        sm.AYID = null;
        sm.username = null;
        sm.table = null;
        sm.type = null;
        cm.type = null;
        cm.year = null;
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

    public string DataTableToJSON(DataTable table)
    {
        string JSONString = string.Empty;
        JSONString = JsonConvert.SerializeObject(table);
        return JSONString;
    }

    public void ddlfill()
    {
        try
        {
            ddlstandard.Enabled = false;
            
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
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('"+ex.Message.ToString()+"', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    public void gridviewFill(string ayid)
    {
        try
        {
            useless();
            ViewState["dt"] = null;
            string urlalias = cls.urls();
            string url = @urlalias + "Subject_Master/";
            sm.type = "gvfill";
            sm.class_id = ddlstandard.SelectedValue.ToString();
            sm.AYID = ayid;
            sm.medium_id = ddlmedium.SelectedValue.ToString();

            string jsonString = JsonHelper.JsonSerializer<subject_master>(sm);
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
                    this.BindGrid();
                    btnSubmit.Visible = true;
                }
                else
                {
                    ViewState["dt"] = null;
                    this.BindGrid();
                    btnSubmit.Visible = false;
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Subjects not defined', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
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
        gv_sub_mst.DataSource = ViewState["dt"] as DataTable;
        gv_sub_mst.DataBind();
    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        try
        {
            gv_sub_mst.EditIndex = -1;
            this.BindGrid();
            txtaddsname.Text = "";
            rd_marks.Checked = false;
            rd_grade.Checked = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal('modalcol');", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void btnaddsub_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtaddsname.Text != "")
            {
                DataTable dt = ViewState["dt"] as DataTable;
                if (dt==null)
                {
                    DataTable dtnew = new DataTable();
                    dtnew.Columns.Add("rank");
                    dtnew.Columns.Add("subject_id");
                    dtnew.Columns.Add("subject_name");
                    dtnew.Columns.Add("criteria");
                    dtnew.Columns.Add("count");
                    dtnew.Columns.Add("count_group");
                    dtnew.Columns.Add("update");
                    dt = dtnew.Copy();
                }
                else if (dt.Rows.Count > 0)
                {
                    foreach (DataRow drow in dt.Rows)
                    {
                        if (drow["subject_name"].ToString().ToUpper() == txtaddsname.Text.ToString().ToUpper())
                        {
                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Subject already exist', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                            return;
                        }
                    }
                }
                DataRow dr = dt.NewRow();
                dr[0] = "";
                dr[1] = "";
                dr[2] = txtaddsname.Text;
                if (rd_grade.Checked == true)
                {
                    dr[3] = rd_grade.Text;
                }
                else if (rd_marks.Checked == true)
                {
                    dr[3] = rd_marks.Text;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Select Criteria', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                    return;
                }
                dr[4] = "0";
                dr[5] = "0";
                dr[6] = "1";
                dt.Rows.Add(dr);
                ViewState["dt"] = dt;
                this.BindGrid();
                btnSubmit.Visible = true;
                btnSubmit.Enabled = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModal('modalcol');", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Add subject name', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                return;
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
            gv_sub_mst.EditIndex = -1;
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
                        gv_sub_mst.DataSource = null;
                        gv_sub_mst.DataBind();
                       
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
                this.BindGrid();
             
                btnSubmit.Visible = false;
                btnadd.Enabled = false;
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
            gv_sub_mst.EditIndex = -1;
            if (ddlstandard.SelectedIndex > 0)
            {
                btnadd.Enabled = true;
                
                gridviewFill(Session["acdyear"].ToString());
            }
            else if (ddlstandard.SelectedIndex == 0)
            {
                ViewState["dt"] = null;
                this.BindGrid();
                btnSubmit.Visible = false;
                btnadd.Enabled = false;
               
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
            GridViewRow row1 = gv_sub_mst.Rows[gvrow.RowIndex];
            DataTable dt = ViewState["dt"] as DataTable;
            int i = gv_sub_mst.EditIndex;
            if (i != -1)
            {

            }
            else
            {
                if (((Label)gv_sub_mst.Rows[gvrow.RowIndex].FindControl("countflag")).Text == "0")
                {
                    string criteria=((Label)gv_sub_mst.Rows[gvrow.RowIndex].FindControl("lbl_gv_criteria")).Text;
                    gv_sub_mst.EditIndex = gvrow.RowIndex;
                    this.BindGrid();
                    ((DropDownList)gv_sub_mst.Rows[gvrow.RowIndex].FindControl("ddl_criteria")).SelectedIndex = ((DropDownList)gv_sub_mst.Rows[gvrow.RowIndex].FindControl("ddl_criteria")).Items.IndexOf(((DropDownList)gv_sub_mst.Rows[gvrow.RowIndex].FindControl("ddl_criteria")).Items.FindByText(criteria));
                    btnSubmit.Enabled = false;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Subject already assigned in exam', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                    return;
                    btnSubmit.Enabled = true;
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void OnCancel(object sender, EventArgs e)
    {
        try
        {
            gv_sub_mst.EditIndex = -1;
            this.BindGrid();
            btnSubmit.Enabled = true;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void OnUpdate(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
            DataTable dt = ViewState["dt"] as DataTable;
            string name = ((TextBox)gv_sub_mst.Rows[row.RowIndex].FindControl("txtgv_subname")).Text;
            string criteria = ((DropDownList)gv_sub_mst.Rows[row.RowIndex].FindControl("ddl_criteria")).Text;
            if (name.ToString() != "")
            {
                int i = 0;
                foreach (DataRow drow in dt.Rows)
                {
                    if (drow["subject_name"].ToString().ToUpper() == name.ToString().ToUpper() && i != row.RowIndex)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Subject Name cannot be same', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                        ViewState["dt"] = dt;
                        gv_sub_mst.EditIndex = row.RowIndex;
                        this.BindGrid();
                        return;
                    }
                    i++;
                }
                dt.Rows[row.RowIndex]["subject_id"] = ((Label)gv_sub_mst.Rows[row.RowIndex].FindControl("lblgv_subid")).Text;
                dt.Rows[row.RowIndex]["subject_name"] = ((TextBox)gv_sub_mst.Rows[row.RowIndex].FindControl("txtgv_subname")).Text;
                dt.Rows[row.RowIndex]["criteria"] = ((DropDownList)gv_sub_mst.Rows[row.RowIndex].FindControl("ddl_criteria")).Text;
                dt.Rows[row.RowIndex]["update"] = "1";
                ViewState["dt"] = dt;
                gv_sub_mst.EditIndex = -1;
                this.BindGrid();
                btnSubmit.Enabled = true;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Subject Name cannot be empty', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                ViewState["dt"] = dt;
                gv_sub_mst.EditIndex = row.RowIndex;
                this.BindGrid();
                btnSubmit.Enabled = false;
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
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
                DataTable dt = ViewState["dt"] as DataTable;
                string count = ((Label)gv_sub_mst.Rows[row.RowIndex].FindControl("countflag")).Text;
                string groupcount = ((Label)gv_sub_mst.Rows[row.RowIndex].FindControl("countgroup")).Text;
                string id = ((Label)gv_sub_mst.Rows[row.RowIndex].FindControl("lblgv_subid")).Text;
                if (Convert.ToInt32(count) > 0 && Convert.ToInt32(groupcount) > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Subject already assigned in a Group and Exam also created for same', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                    return;
                }
                else if (Convert.ToInt32(count) > 0 && Convert.ToInt32(groupcount) == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Exam already defined for the Subject', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                    return;
                }
                else if (Convert.ToInt32(count) == 0 && Convert.ToInt32(groupcount) > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Subject already assigned in the Group', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                    return;
                }
                else
                {
                    if (id != "")
                    {
                        useless();
                        string urlalias = cls.urls();
                        string url = @urlalias + "Subject_Master/";
                        sm.type = "delete";
                        sm.class_id = ddlstandard.SelectedValue.ToString();
                        sm.AYID = Session["acdyear"].ToString();
                        sm.medium_id = ddlmedium.SelectedValue.ToString();
                        sm.subject_id = id.ToString();

                        string jsonString = JsonHelper.JsonSerializer<subject_master>(sm);
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
                                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Subject Deleted successfully', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
                                dt.Rows.RemoveAt(row.RowIndex);
                                ViewState["dt"] = dt;
                                this.BindGrid();
                                if (dt.Rows.Count == 0)
                                {
                                    btnSubmit.Visible = false;
                                }
                            }
                            else if (result.ToString().Contains("Subject Exist") == true)
                            {
                                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Subject already assigned in a Group and Exam also created for same', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
                            }
                            else if (result.ToString().Contains("Exam Exist") == true)
                            {
                                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Exam already defined for the Subject', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
                            }
                            else if (result.ToString().Contains("Group Exist") == true)
                            {
                                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Subject already assigned in the Group', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
                            }
                            else if (result.ToString().Contains("Error") == true)
                            {
                                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Error occurred', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
                            }
                        }
                    }
                    else
                    {
                        dt.Rows.RemoveAt(row.RowIndex);
                        ViewState["dt"] = dt;
                        this.BindGrid();
                        if (dt.Rows.Count == 0) { btnSubmit.Visible = false; }
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Subject Deleted successfully', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = ViewState["dt"] as DataTable;
            useless();
            string urlalias = cls.urls();
            string url = @urlalias + "Subject_Master/";
            sm.type = "save";
            sm.class_id = ddlstandard.SelectedValue.ToString();
            sm.AYID = Session["acdyear"].ToString();
            sm.medium_id = ddlmedium.SelectedValue.ToString();
            sm.username = Session["emp_id"].ToString();

           
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["update"].ToString() != "1")
                    {
                        dt.Rows.RemoveAt(i);
                        i--;
                    }
                }
          

            if (dt.Rows.Count > 0)
            {
                string json = DataTableToJSON(dt);
                sm.table = json.ToString();

                string jsonString = JsonHelper.JsonSerializer<subject_master>(sm);
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
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Subject Saved successfully', { color: '#fff', background: '#198104', blur: 0.2, delay: 0 });", true);
                        ddlmedium.SelectedIndex = 0;
                        ddlmedium_SelectedIndexChanged(sender, e);
                        ddlmedium.Enabled = true;
                    }
                    else if (result.ToString().Contains("Error") == true)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Error occurred', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
                    }
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Subject Saved successfully', { color: '#fff', background: '#198104', blur: 0.2, delay: 0 });", true);
                ddlmedium.SelectedIndex = 0;
                ddlmedium_SelectedIndexChanged(sender, e);
                ddlmedium.Enabled = true;
            }
            
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void txtrank_TextChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;
            DataTable dt = ViewState["dt"] as DataTable;
            string rank = ((TextBox)gv_sub_mst.Rows[row.RowIndex].FindControl("txtrank")).Text;
            if (rank.ToString() != "")
            {
                if (Convert.ToInt32(rank) != 0)
                { 
                    int i = 0;
                    foreach (DataRow drow in dt.Rows)
                    {
                        if (drow["rank"].ToString() != "")
                        {
                            if (Convert.ToInt32(drow["rank"].ToString()) == Convert.ToInt32(rank) && i != row.RowIndex)
                            {
                                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Rank cannot be same', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                                ViewState["dt"] = dt;
                                this.BindGrid();
                                return;
                            }
                        }
                        i++;
                    }
                    dt.Rows[row.RowIndex]["rank"] = rank.ToString();
                    dt.Rows[row.RowIndex]["update"] = "1";
                    ViewState["dt"] = dt;
                    this.BindGrid();
                    btnSubmit.Enabled = true;
                }
                else
                {
                    ((TextBox)gv_sub_mst.Rows[row.RowIndex].FindControl("txtrank")).Text = "";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Rank cannot be 0', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                }
            }
            else
            {
                dt.Rows[row.RowIndex]["rank"] = null;
                dt.Rows[row.RowIndex]["update"] = "1";
                ViewState["dt"] = dt;
                this.BindGrid();
                btnSubmit.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }
}