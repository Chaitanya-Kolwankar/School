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

public partial class frm_Exam_Master : System.Web.UI.Page
{
    exam_master exm = new exam_master();
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
                    ddlfill();}
            }
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>multi();</script>", false);
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
        cm.type = null;
        cm.year = null;
        exm.type = null;
        exm.exam_id = null;
        exm.exam_name = null;
        exm.subject_name = null;
        exm.medium_id = null;
        exm.class_id = null;
        exm.exam_type = null;
        exm.examtype_name = null;
        exm.out_of = null;
        exm.passing = null;
        exm.criteria = null;
        exm.ayid = null;
        exm.subject_id = null;
        exm.type2 = null;
        exm.std = null;
        exm.ref_id = null;
        exm.username = null;
    }

    public void ddlfill()
    {
        try
        {
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
            if (ddlmedium.SelectedIndex > 0)
            {
                ddlstandard.Enabled = true;
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
            else
            {
                if (ddlstandard.SelectedIndex > 0)
                {
                    ddlstandard.SelectedIndex = 0;
                }
                ddlstandard.Enabled = false;
            }
            if (ddlexam_name.SelectedIndex > 0)
            {
                ddlexam_name.SelectedIndex = 0;
            }
            ddlexam_name.Enabled = false;
            if (ddlexamtype.SelectedIndex > 0)
            {
                ddlexamtype.SelectedIndex = 0;
            }
            ddlexamtype.Enabled = false;
            ms_sub_name.SelectedIndex = -1;
            ms_sub_name.Items.Clear();
            gv_exam_mst.DataSource = null;
            gv_exam_mst.DataBind();
            btncreate_test.Enabled = false;
            savepanel.Visible = false;
            testname.Visible = false;
            txt_test_name.Text = "";
            txt_out_of.Text = "";
            txt_passing.Text = "";
            txt_criteria.Text = "";
            btncreate_test.Text = "Create New Test";
            lblrefid.Text = "";
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
            ms_sub_name.Items.Clear();
            gv_exam_mst.DataSource = null;
            gv_exam_mst.DataBind();
            btncreate_test.Enabled = false;
            savepanel.Visible = false;
            testname.Visible = false;
            txt_test_name.Text = "";
            txt_out_of.Text = "";
            txt_passing.Text = "";
            txt_criteria.Text = "";
            lblrefid.Text = "";
            if (ddlprevyear.Visible == true && chkprevious.Checked == true)
            {
                btncreate_test.Text = "Save";
            }
            else
            {
                btncreate_test.Text = "Create New Test";
            }
            if (ddlstandard.SelectedIndex != 0)
            {
                fillexam();
                ddlexam_name.Enabled = true;
                btncreate_test.Enabled = true;
                if (ddlprevyear.Visible == false  && chkprevious.Checked == false)
                {
                    ddlexamtype.Enabled = true;
                    fillsub("", "", "");
                    ddlexam_name.SelectedIndex = 0;
                    
                }
                else
                {
                    fillpreviousexam();
                }
            }
            else
            {
                if (ddlexam_name.SelectedIndex > 0)
                {
                    ddlexam_name.SelectedIndex = 0;
                }
                ddlexam_name.Enabled = false;
                ddlexamtype.SelectedIndex = 0;
                ddlexamtype.Enabled = false;
                ms_sub_name.SelectedIndex = -1;
                btncreate_test.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    public void fillpreviousexam()
    {
        try
        {
            string urlalias = cls.urls();
            string url = @urlalias + "ExamMaster/";
            useless();
            exm.type = "fillpreviousexam";
            exm.medium_id = ddlmedium.SelectedValue.ToString();
            exm.class_id = ddlstandard.SelectedValue.ToString();
            exm.ref_id = ddlprevyear.SelectedValue.ToString();
            exm.ayid = Session["acdyear"].ToString();


            string jsonString = JsonHelper.JsonSerializer<exam_master>(exm);
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

                DataTable dt1 = dslist.Tables[0];
                if (dt1.Rows.Count == 0)
                {
                    notifys("All exams already carried in current year", "#D44950");
                }

                ddlexam_name.DataSource = dt1;
                ddlexam_name.DataValueField = "exam_id";
                ddlexam_name.DataTextField = "exam_name";
                ddlexam_name.DataBind();
                ddlexam_name.Enabled = true;
                ddlexam_name.Items.Insert(0, "--Select--");
                ddlexam_name.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    public void fillexam()
    {
        try
        {
            string urlalias = cls.urls();
            string url = @urlalias + "ExamMaster/";
            useless();
            exm.type = "fillexam";
            exm.medium_id = ddlmedium.SelectedValue.ToString();
            exm.class_id = ddlstandard.SelectedValue.ToString();
            if (ddlprevyear.Visible == true && chkprevious.Checked == true)
            {
                exm.ayid = ddlprevyear.SelectedValue.ToString();
            }
            else
            {
                if (Session["acdyear"] != null)
                {
                    exm.ayid = Session["acdyear"].ToString();
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }

            string jsonString = JsonHelper.JsonSerializer<exam_master>(exm);
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

                DataTable dt1 = dslist.Tables[0];
                if (dt1.Rows.Count == 0)
                {
                    notifys("No exam defined for given medium, class and academic year.", "#D44950");
                }

                ddlexam_name.DataSource = dt1;
                ddlexam_name.DataValueField = "exam_id";
                ddlexam_name.DataTextField = "exam_name";
                ddlexam_name.DataBind();
                ddlexam_name.Enabled = true;
                ddlexam_name.Items.Insert(0, "--Select--");
                ddlexam_name.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    public void fillsub(string exam_id, string exam_type, string examtype)
    {
        try
        {
            string urlalias = cls.urls();
            string url = @urlalias + "ExamMaster/";
            useless();
            exm.type = "fillsub";
            exm.medium_id = ddlmedium.SelectedValue.ToString();
            exm.class_id = ddlstandard.SelectedValue.ToString();
            exm.exam_type = exam_type;

            if (ddlexam_name.SelectedIndex > 0)
            {
                exm.exam_id = exam_id;
            }
            else
            {
                exm.exam_id = "";
            }

            exm.ayid = Session["acdyear"].ToString();

            string jsonString = JsonHelper.JsonSerializer<exam_master>(exm);
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
                DataTable dt1 = dslist.Tables[0];
                ms_sub_name.DataSource = dt1;
                ms_sub_name.DataValueField = "subject_id";
                ms_sub_name.DataTextField = "subject_name";
                ms_sub_name.DataBind();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    public void fillgv(string med_id, string class_id, string exam_id, string exam_type)
    {
        try
        {
            string urlalias = cls.urls();
            string url = @urlalias + "ExamMaster/";
            useless();
            exm.type = "fillgv";
            exm.medium_id = med_id;
            exm.class_id = class_id;
            exm.exam_id = exam_id;
            exm.exam_type = exam_type;
            if (ddlprevyear.Visible == true && chkprevious.Checked == true)
            {
                exm.ayid = ddlprevyear.SelectedValue.ToString();
            }
            else
            {
                if (Session["acdyear"] != null)
                {
                    exm.ayid = Session["acdyear"].ToString();
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }

            string jsonString = JsonHelper.JsonSerializer<exam_master>(exm);
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
                DataTable dtCloned = new DataTable();
                if (dslist.Tables.Count > 0)
                {
                    DataTable dt = dslist.Tables[0];
                    ViewState["dt"] = dt;
                    gv_exam_mst.DataSource = dt;
                    gv_exam_mst.DataBind();
                }

                else
                {
                    notifys("No exam defined for the given selection.", "#D44950");
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void ddlexam_name_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlexam_name.SelectedIndex > 0)
            {
                if (ddlprevyear.Visible == false && chkprevious.Checked == false)
                { 
                    btncreate_test.Text = "Create Test";
                    ms_sub_name.SelectedIndex = -1;
                    ddlexamtype.SelectedIndex = 0;
                    ddlexamtype.Enabled = true;
                }
                fillgv(ddlmedium.SelectedValue.ToString(), ddlstandard.SelectedValue.ToString(), ddlexam_name.SelectedValue.ToString(), "");
                lblrefid.Text = findrefid();
            }
            else
            {
                ddlexamtype.SelectedIndex = 0;
                ms_sub_name.SelectedIndex = -1;
                if (ddlprevyear.Visible == false && chkprevious.Checked == false)
                {
                    btncreate_test.Text = "Create New Test";
                    fillsub("", "", "");
                }
                gv_exam_mst.DataSource = null;
                gv_exam_mst.DataBind();
                lblrefid.Text = "";
            }
            savepanel.Visible = false;
            testname.Visible = false;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void btncreate_test_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlexamtype.SelectedIndex > 0 && btncreate_test.Text!="Save")
            {
                if (ms_sub_name.SelectedIndex != -1)
                {
                    if (btncreate_test.Text.Contains("New") == true)
                    {
                        savepanel.Visible = true;
                        testname.Visible = true;
                        if (ddlexam_name.SelectedIndex > 0)
                        {
                            ddlexam_name.SelectedIndex = 0;
                            lblrefid.Text = "";
                        }
                        ddlexam_name.Enabled = false;
                    }
                    else
                    {
                        if (ddlexamtype.SelectedItem.ToString() != "Grade")
                        {
                            savepanel.Visible = true;
                            testname.Visible = false;
                        }
                        else
                        {
                            btn_save_test_Click(sender, e);
                        }
                    }
                }
                else
                {
                    notifys("Select subjects", "#D44950");
                }
            }
            else if (chkprevious.Checked == true)
            {
                if (ddlprevyear.SelectedIndex == 0)
                {
                    notifys("Select previous year", "#D44950");
                }
                else if (ddlmedium.SelectedIndex == 0)
                {
                    notifys("Select medium", "#D44950");
                }
                else if (ddlstandard.SelectedIndex == 0)
                {
                    notifys("Select standard", "#D44950");
                }
                else if (ddlexam_name.SelectedIndex == 0)
                {
                    notifys("Select exam", "#D44950");
                }
                else
                {
                    DataSet ds = saveprevious();
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            notifys("Exam saved successfully", "#198104");
                            chkprevious.Checked = false;
                            chkprevious_CheckedChanged(sender, e);
                        }
                        else
                        {
                            notifys("Data not saved", "#D44950");
                        }
                    }
                }
            }
            else
            {
                notifys("Select exam type", "#D44950");
            }
            txt_out_of.Text = "";
            txt_passing.Text = "";
            txt_criteria.Text = "";
            txt_test_name.Text = "";
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    public DataSet saveprevious()
    {
        DataSet dslist = new DataSet();
        try
        {
            string urlalias = cls.urls();
            string url = @urlalias + "ExamMaster/";
            useless();
            exm.type = "saveprevious";
            exm.medium_id = ddlmedium.SelectedValue.ToString();
            exm.class_id = ddlstandard.SelectedValue.ToString();
            exm.exam_id = ddlexam_name.SelectedValue.ToString();
            if (Session["acdyear"] != null)
            {
                exm.ayid = Session["acdyear"].ToString();
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
            exm.exam_name = ddlexam_name.SelectedItem.ToString();
            exm.ref_id = lblrefid.Text;
            exm.username= Session["emp_id"].ToString();

            string jsonString = JsonHelper.JsonSerializer<exam_master>(exm);
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
                dslist = JsonConvert.DeserializeObject<DataSet>(result);
                
            }
            return dslist;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
            return dslist;
        }
    }
    protected void ddlexamtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlexamtype.SelectedIndex != 0)
            {
                fillsub(ddlexam_name.SelectedItem.Value.ToString(), ddlexamtype.SelectedValue.ToString(), ddlexamtype.SelectedItem.Text.ToString());
                if (ddlexam_name.SelectedIndex > 0)
                {
                    fillgv(ddlmedium.SelectedValue.ToString(), ddlstandard.SelectedValue.ToString(), ddlexam_name.SelectedValue.ToString(), ddlexamtype.SelectedValue.ToString());
                }
                ms_sub_name.Enabled = true;
                if (ms_sub_name.Enabled == true && ddlexamtype.SelectedIndex > 0)
                {
                    if (ms_sub_name.Items.Count == 0 && gv_exam_mst.Rows.Count > 0)
                    {
                        notifys("All subjects already assigned to selected exam.", "#198104");
                    }
                    else if (ms_sub_name.Items.Count == 0 && gv_exam_mst.Rows.Count == 0)
                    {
                        notifys("Subject does not exist for given selection.", "#D44950");
                    }
                }
            }
            else
            {
                if (ddlstandard.SelectedIndex > 0)
                {
                    fillsub("", "", "");
                    fillgv(ddlmedium.SelectedValue.ToString(), ddlstandard.SelectedValue.ToString(), ddlexam_name.SelectedValue.ToString(), "");
                }
            }
            savepanel.Visible = false;
            testname.Visible = false;
            ddlexam_name.Enabled = true;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void BindGrid()
    {
        try
        {
            gv_exam_mst.DataSource = ViewState["dt"] as DataTable;
            gv_exam_mst.DataBind();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void btn_clear_Click(object sender, EventArgs e)
    {
        try
        {
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    public void notifys(string msg, string color)
    {
        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert12", "$.notify('" + msg + "', { color: '#fff', background: '" + color + "', blur: 0.2, delay: 0 });", true);
    }

    private string findrefid()
    {
        string str = "";
        try
        {
            string urlalias = cls.urls();
            string url = @urlalias + "ExamMaster/";
            useless();
            exm.type = "findref";
            exm.medium_id = ddlmedium.SelectedValue.ToString();
            exm.class_id = ddlstandard.SelectedValue.ToString();
            exm.exam_id = ddlexam_name.SelectedValue.ToString();

            if (ddlprevyear.Visible == true && chkprevious.Checked == true)
            {
                exm.ayid = ddlprevyear.SelectedValue.ToString();
            }
            else
            {
                if (Session["acdyear"] != null)
                {
                    exm.ayid = Session["acdyear"].ToString();
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }

            string jsonString = JsonHelper.JsonSerializer<exam_master>(exm);
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
                if (dslist.Tables.Count > 0)
                {
                    if (dslist.Tables[0].Rows.Count > 0)
                    {
                        str = dslist.Tables[0].Rows[0]["ref_id"].ToString();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
        return str;
    }

    protected void btn_save_test_Click(object sender, EventArgs e)
    {
        try
        {
            string med_id = "";
            string class_id = "";
            string exam_id = "";
            string exam_type = "";
            string passing = "";
            string out_of = "";
            string criteria = "";

            if (ddlmedium.SelectedIndex > 0 && ddlstandard.SelectedIndex > 0)
            {
                med_id = ddlmedium.SelectedValue.ToString();
                class_id = ddlstandard.SelectedValue.ToString();

                if (ddlexam_name.SelectedIndex > 0 && ddlexamtype.SelectedIndex > 0 && ms_sub_name.SelectedIndex >= 0)
                {
                    exam_id = ddlexam_name.SelectedValue.ToString();
                    exam_type = ddlexamtype.SelectedItem.Text.ToString();
                    
                }
                else
                {
                    exam_id = txt_test_name.Text.ToString();
                    if (ddlexamtype.SelectedIndex > 0)
                    {
                        exam_type = ddlexamtype.SelectedItem.Text.ToString();
                    }
                    else
                    {
                        notifys("Please select the exam type.", "#D44950");
                    }
                }

                if (ddlexamtype.SelectedItem.Text != "Grade")
                {
                    if (txt_out_of.Text != string.Empty && txt_passing.Text != string.Empty && txt_criteria.Text != string.Empty)
                    {
                        passing = txt_passing.Text.ToString();
                        out_of = txt_out_of.Text.ToString();
                        criteria = txt_criteria.Text.ToString();
                    }
                    else
                    {
                        notifys("Please enter all fields.", "#D44950");
                        return;
                    }
                    if (Convert.ToInt32(passing) > Convert.ToInt32(out_of))
                    {
                        notifys("Passing marks should be less than out of marks", "#D44950");
                        txt_criteria.Text = "";
                        txt_out_of.Text = "";
                        txt_passing.Text = "";
                        return;
                    }
                }
            }
            else
            {
                notifys("Please select all fields.", "#D44950");
            }

            string subject_list = "";
            foreach (ListItem item in ms_sub_name.Items)
            {
                if (item.Selected)
                {
                    subject_list += item.Value.ToString() + ",";
                }
            }

            if (subject_list != "")
            {
                subject_list = subject_list.Remove(subject_list.Length - 1);
            }
            else
            {
                notifys("Please select subject field.", "#D44950");
            }

            

            if (med_id != "" && class_id != "" && exam_id != "" && exam_type != "" && subject_list != "")
            {
                string url = cls.urls() + "ExamMaster/";
                useless();
                exm.type = "insert";
                exm.medium_id = med_id;
                exm.class_id = class_id;
                if (Session["acdyear"] != null)
                {
                    exm.ayid = Session["acdyear"].ToString();
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
                exm.username = Session["emp_id"].ToString();
                exm.subject_id = subject_list;
                exm.exam_type = exam_type;
                exm.passing = passing;
                exm.out_of = out_of;
                exm.criteria = criteria;
                string ename = "";
                if (ddlexam_name.SelectedIndex == 0)
                {
                    exm.exam_name = txt_test_name.Text.ToString();
                    exm.exam_id = "";
                    ename = exm.exam_name;
                }
                else
                {
                    exm.exam_name = ddlexam_name.SelectedItem.Text.ToString();
                    exm.exam_id = exam_id;
                    exm.ref_id = lblrefid.Text.ToString();
                }

                string jsonString = JsonHelper.JsonSerializer<exam_master>(exm);
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
                DataSet dslist = new DataSet();
                using (StreamReader sr = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    string result = sr.ReadToEnd();
                    dslist = JsonConvert.DeserializeObject<DataSet>(result);
                }
                if (dslist.Tables[0].Rows.Count>0)
                {
                    ViewState["dt"] = dslist.Tables[0];
                    fillexam();
                    notifys("Exam saved successfully.", "#198104");
                }
                
                foreach (ListItem item in ms_sub_name.Items)
                {
                    if (item.Selected == true)
                    {
                        item.Selected = false;
                    }
                }
                ddlexam_name.SelectedIndex = 0;
                ddlexam_name_SelectedIndexChanged(sender, e);
                lblrefid.Text = "";
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void txt_out_of_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlexamtype.SelectedItem.Text != "Grade")
            {
                if (txt_passing.Text != "" && txt_out_of.Text != "" && (Convert.ToInt32(txt_passing.Text.ToString()) < Convert.ToInt32(txt_out_of.Text.ToString())))
                {
                    double pass = Convert.ToDouble(txt_passing.Text.ToString());
                    double out_of = Convert.ToDouble(txt_out_of.Text.ToString());
                    txt_criteria.Text = ((int)Math.Round((pass / out_of) * 100)).ToString();
                    txt_passing.Focus();
                }

                else if (txt_passing.Text != "" && txt_out_of.Text != "" && (Convert.ToInt32(txt_passing.Text.ToString()) >= Convert.ToInt32(txt_out_of.Text.ToString())))
                {
                    notifys("Passing marks should be less than out of marks.", "#D44950");
                    txt_criteria.Text = "";
                    txt_passing.Text = "";
                    txt_out_of.Text = "";
                }
                else
                {
                    txt_passing.Focus();
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }

    }

    protected void txt_passing_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlexamtype.SelectedItem.Text != "Grade")
            {
                if (txt_passing.Text != "" && txt_out_of.Text != "" && (Convert.ToInt32(txt_passing.Text.ToString()) < Convert.ToInt32(txt_out_of.Text.ToString())))
                {
                    double pass = Convert.ToDouble(txt_passing.Text.ToString());
                    double out_of = Convert.ToDouble(txt_out_of.Text.ToString());
                    txt_criteria.Text = ((int)Math.Round((pass / out_of) * 100)).ToString();
                }
                else if (txt_passing.Text != "" && txt_out_of.Text != "" && (Convert.ToInt32(txt_passing.Text.ToString()) >= Convert.ToInt32(txt_out_of.Text.ToString())))
                {
                    notifys("Passing marks should be less than out of marks.", "#D44950");
                    txt_criteria.Text = "";
                    txt_passing.Text = "";
                    txt_out_of.Text = "";
                }
                else
                {

                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void LBUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
            Label exam_id = (Label)row.FindControl("lblgv_examid");
            Label subject_id = (Label)row.FindControl("lblgv_subid");
            Label exam_type = (Label)row.FindControl("lblgv_exam_type");
            TextBox out_of = (TextBox)row.FindControl("txtgv_out_of") as TextBox;
            TextBox passing = (TextBox)row.FindControl("txtgv_passing") as TextBox;


            if (out_of.Text != string.Empty && passing.Text != string.Empty)
            {
                if (Convert.ToInt32(out_of.Text.ToString()) > Convert.ToInt32(passing.Text.ToString()))
                {
                    update_grid(exam_type.Text.ToString(), passing.Text.ToString(), out_of.Text.ToString(), subject_id.Text.ToString());
                    gv_exam_mst.EditIndex = -1;
                    notifys("Successfully updated.", "#198104");
                    LinkButton btn_delete = row.FindControl("LBDelete") as LinkButton;
                    btn_delete.Enabled = true;
                }
                else
                {
                    notifys("Passing marks should be less than Out Of Marks.", "#D44950");
                }
            }
            else
            {
                notifys("Please enter all fields.", "#D44950");

            }
            this.BindGrid();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    public void update_grid(string exam_type, string passing, string out_of, string subject_id)
    {
        try
        {
            string urlalias = cls.urls();
            string url = @urlalias + "ExamMaster/";
            useless();
            exm.type = "update";
            exm.exam_id = ddlexam_name.SelectedValue.ToString();
            exm.medium_id = ddlmedium.SelectedValue.ToString();
            exm.class_id = ddlstandard.SelectedValue.ToString();
            exm.ayid = Session["acdyear"].ToString();
            exm.subject_id = subject_id;

            exm.exam_type = exam_type;
            exm.passing = passing;
            exm.out_of = out_of;

            string jsonString = JsonHelper.JsonSerializer<exam_master>(exm);
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
                ViewState["dt"] = dslist.Tables[0];
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void LBCancel_Click(object sender, EventArgs e)
    {
        try
        {
            gv_exam_mst.EditIndex = -1;
            this.BindGrid();
            GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
            LinkButton btn_delete = row.FindControl("LBDelete") as LinkButton;
            btn_delete.Enabled = true;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void gv_exam_mst_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            gv_exam_mst.EditIndex = e.NewEditIndex;
            this.BindGrid();

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void txtgv_out_of_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlexamtype.SelectedItem.Text != "Grade")
            {
                GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
                TextBox _txtgv_pass = (TextBox)row.FindControl("txtgv_passing");
                TextBox _txtgv_out = (TextBox)row.FindControl("txtgv_out_of");
                if (_txtgv_pass.Text != "" && _txtgv_out.Text != "" && (Convert.ToInt32(_txtgv_pass.Text.ToString()) < Convert.ToInt32(_txtgv_out.Text.ToString())))
                {
                    double pass = Convert.ToDouble(_txtgv_pass.Text.ToString());
                    double out_of = Convert.ToDouble(_txtgv_out.Text.ToString());
                }
                else if (_txtgv_pass.Text != "" && _txtgv_out.Text != "" && (Convert.ToInt32(_txtgv_pass.Text.ToString()) >= Convert.ToInt32(_txtgv_out.Text.ToString())))
                {
                    notifys("Passing marks should be less than out of marks.", "#D44950");
                    _txtgv_out.Text = "";
                }

                else
                {

                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void txtgv_passing_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlexamtype.SelectedItem.Text != "Grade")
            {
                GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
                TextBox _txtgv_pass = (TextBox)row.FindControl("txtgv_passing");
                TextBox _txtgv_out = (TextBox)row.FindControl("txtgv_out_of");
                if (_txtgv_pass.Text != "" && _txtgv_out.Text != "" && (Convert.ToInt32(_txtgv_pass.Text.ToString()) < Convert.ToInt32(_txtgv_out.Text.ToString())))
                {
                    double pass = Convert.ToDouble(_txtgv_pass.Text.ToString());
                    double out_of = Convert.ToDouble(_txtgv_out.Text.ToString());
                }

                else if (_txtgv_pass.Text != "" && _txtgv_out.Text != "" && (Convert.ToInt32(_txtgv_pass.Text.ToString()) >= Convert.ToInt32(_txtgv_out.Text.ToString())))
                {
                    notifys("Passing marks should be less than out of marks.", "#D44950");
                    _txtgv_pass.Text = "";
                }

                else { }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void txt_test_name_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlexam_name.Items.FindByText(txt_test_name.Text.ToString()) != null)
            {
                notifys("The exam is already created.", "#D44950");
                txt_test_name.Text = "";
            }
            else
            {
                txt_out_of.Focus();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void LBDelete_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
            Label subject_id = (Label)row.FindControl("lblgv_subid");
            Label exam_type = (Label)row.FindControl("lblgv_exam_type");
            Label count = (Label)row.FindControl("lblcount");
            if (Convert.ToInt32(count.Text) == 0)
            {
                delete_grid(subject_id.Text.ToString(), exam_type.Text.ToString());
                fillsub(ddlexam_name.SelectedItem.Value.ToString(), ddlexamtype.SelectedValue.ToString(), ddlexamtype.SelectedItem.Text.ToString());
                notifys("Subject removed successfully", "#D44950");
            }
            else
            {
                notifys("Marks entry already done for the subject", "#D44950");
            }
            this.BindGrid();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }  
    }

    public void delete_grid(string subject_id, string exam_type)
    {
        try
        {
            string urlalias = cls.urls();
            string url = @urlalias + "ExamMaster/";
            useless();
            exm.type = "delete";

            if (ddlexam_name.SelectedIndex <= 0)
            {
                notifys("Please select an exam.", "#D44950");
            }

            else
            {
                exm.exam_id = ddlexam_name.SelectedValue.ToString();
                exm.medium_id = ddlmedium.SelectedValue.ToString();
                exm.class_id = ddlstandard.SelectedValue.ToString();
                exm.ayid = Session["acdyear"].ToString();
                exm.subject_id = subject_id;
                exm.exam_type = exam_type;

                string jsonString = JsonHelper.JsonSerializer<exam_master>(exm);
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
                    ViewState["dt"] = dslist.Tables[0];
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }

    }

    protected void gv_exam_mst_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (chkprevious.Checked == true)
                {
                    e.Row.Enabled = false;
                    ((LinkButton)e.Row.FindControl("LBDelete")).OnClientClick = null;
                }
                else
                {
                    string type = ((Label)e.Row.FindControl("lblgv_exam_type")).Text;
                    if (type.ToString() == "Grade")
                    {
                        ((LinkButton)e.Row.FindControl("btn_Edit")).Enabled = false;
                        ((Label)e.Row.FindControl("lblgv_out_of")).Text = "--";
                        ((Label)e.Row.FindControl("lblgv_passing")).Text = "--";
                    }
                }
            }
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void chkprevious_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (chkprevious.Checked == true)
            {
                ddlprevyear.Visible = true;
                DataSet ds = fill();
                if(ds.Tables[0].Rows.Count>0)
                {
                    DataTable ayid = ds.Tables[0];
                    ddlprevyear.DataSource = ayid;
                    ddlprevyear.DataTextField = "duration";
                    ddlprevyear.DataValueField = "ayid";
                    ddlprevyear.DataBind();
                    ddlprevyear.Items.Insert(0, "--Select--");
                    ddlprevyear.SelectedIndex = 0;
                }
                else
                {
                    chkprevious.Checked = false;
                    chkprevious_CheckedChanged(sender, e);
                    notifys("No exams defined previously", "#D44950");
                }
            }
            else
            {
                ddlprevyear.Visible = false;
                ddlprevyear.SelectedIndex = 0;
                ddlprevyear.DataSource = null;
                ddlprevyear.DataBind();
            }
            ddlmedium.SelectedIndex = 0;
            ddlmedium_SelectedIndexChanged(sender, e);
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void ddlprevyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlmedium.SelectedIndex = 0;
            ddlmedium_SelectedIndexChanged(sender, e);
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    public DataSet fill()
    {
        DataSet dslist = new DataSet();
        useless();
        string urlalias = cls.urls();
        string url = @urlalias + "ExamMaster/";
        exm.type = "fillyear";
        exm.ayid = Session["acdyear"].ToString();

        string jsonString = JsonHelper.JsonSerializer<exam_master>(exm);
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
            dslist = JsonConvert.DeserializeObject<DataSet>(result);
        }
        return dslist;

    }
}