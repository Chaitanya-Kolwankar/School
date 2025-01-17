using ClosedXML.Excel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frm_Marks_Entry : System.Web.UI.Page
{
    subject_master sm = new subject_master();
    exam_master exm = new exam_master();
    marks_entry me = new marks_entry();
    Class1 cls = new Class1();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlfill();
       
        }
        else
        {
            if (btn_edit.Enabled == true && btn_edit.Visible == true && btn_edit.Text == "Edit")
            {
               
            }
            else
            {
                
            }
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
        sm.medium_id = null;
        sm.class_id = null;
        sm.subject_id = null;
        sm.subject_name = null;
        sm.criteria = null;
        sm.AYID = null;
        sm.username = null;
        sm.table = null;
        sm.type = null;
        exm.class_id = null;
        exm.criteria = null;
        exm.exam_id = null;
        exm.exam_name = null;
        exm.exam_type = null;
        exm.ayid = null;
        exm.subject_id = null;
        exm.medium_id = null;
        exm.out_of = null;
        exm.passing = null;
        exm.subject_name = null;
        exm.type = null;
        me.ayid = null;
        me.division_id = null;
        me.exam_id = null;
        me.extra1 = null;
        me.extra2 = null;
        me.medium = null;
        me.standard = null;
        me.subject_id = null;
        me.type = null;
        me.student_id = null;
        me.theory_marks = null;
        me.practical_marks = null;
    }

    //fill the medium
    public void ddlfill()
    {
        string html = string.Empty;

        string urlalias = cls.urls();
        string url = @urlalias + "Subject_Master/";
        //string url = @"http://localhost:9199//Subject_Master";
        useless();
        //sm.col1 = "ddlfill";

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
            DataSet dslist = JsonConvert.DeserializeObject<DataSet>(result);

            //To use it later to assign standard
            Session["DSLIST"] = dslist;
            DataTable dt1 = dslist.Tables["Table1"];
            ddlmedium.DataSource = dt1;
            ddlmedium.DataTextField = "medium";
            ddlmedium.DataValueField = "med_id";
            ddlmedium.Items.Insert(0, "Select");
            ddlmedium.DataBind();
            ddlmedium.Items.Insert(0, "----SELECT----");
            ddlmedium.SelectedIndex = 0;

       
            btn_save.Visible = false;
            btn_import_cancel.Visible = false;
               
        }
    }   

    protected void ddlmedium_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlmedium.SelectedIndex != 0)
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
                    ddlstandard.Items.Insert(0, "----SELECT----");
                    ddlstandard.SelectedIndex = 0;
                }
            }
        }

        else
        {
            //std
            ddlstandard.SelectedIndex = 0;
            ddlstandard.Enabled = false;
            
            //exam
            ddlexam.SelectedIndex = 0;
            ddlexam.Enabled = false;    
            
            //grid
            gv_marks_entry.DataSource = null;
            gv_marks_entry.DataBind();
            
            //subject
            ddlsubject.SelectedIndex = 0;
            ddlsubject.Enabled = false;

            //division
            ddldivision.SelectedIndex = 0;
            ddldivision.Enabled = false;

            //criteria
            marks_criteria.Visible = false;

            //buttons
            btn_export.Visible = false;
            btn_cancel.Visible = false;
            btn_edit.Visible = false;

        }
    }
    
   //to fill exam name ddl
    public void fillexam(string med_id, string class_id)
    {
        string html = string.Empty;
        string urlalias = cls.urls();
        string url = @urlalias + "ExamMaster/";
       // string url = @"http://localhost:9199//ExamMaster";
        useless();
        exm.type = "fillexam";
        exm.medium_id = med_id;
        exm.class_id = class_id;

        if (Session["acdyear"] == null)
        {
            notifys("Please select academic year.", "#D44950");
            return;
        }
        else if (Session["acdyear"].ToString() == "----SELECT----")
        {
            notifys("Please select academic year.", "#D44950");
            return;
        }
        else
        {
            exm.ayid = Session["acdyear"].ToString();
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

            DataTable dt1 = dslist.Tables["Table1"];
            if (dt1.Rows.Count == 0)
            {
                notifys("No exam defined for given medium, class and academic year.", "#D44950");
            }

            ddlexam.DataSource = dt1;
            ddlexam.DataValueField = "exam_id";
            ddlexam.DataTextField = "exam_name";
            ddlexam.DataBind();
            ddlexam.Enabled = true;
            ddlexam.Items.Insert(0, "----SELECT----");
            ddlexam.SelectedIndex = 0;
        }
    }

    //to fill subject mulitselect
    public void fillsub(string med_id, string class_id, string exam_id, string ayid)
    {
        string html = string.Empty;
        string urlalias = cls.urls();
        string url = @urlalias + "ExamMaster/";
      // string url = @"http://localhost:9199//ExamMaster";
        useless();
        exm.type = "fillsub";
        exm.medium_id = med_id;
        exm.class_id = class_id;
        exm.exam_id = exam_id;
        exm.ayid = ayid;        

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
            DataTable dt1 = dslist.Tables["Table1"];
            ddlsubject.DataSource = dt1;
            ddlsubject.DataValueField = "subid";
            ddlsubject.DataTextField = "subject_name";
            ddlsubject.DataBind();
            ddlsubject.Items.Insert(0, "----SELECT----");
            ddlsubject.SelectedIndex = 0;
            ddlsubject.Enabled = true;
        }
    }

    protected void ddlstandard_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlstandard.SelectedIndex != 0)
        {
            ddlexam.Enabled = true;
            fillexam(ddlmedium.SelectedValue.ToString(), ddlstandard.SelectedValue.ToString());            
            fillsub(ddlmedium.SelectedValue.ToString(), ddlstandard.SelectedValue.ToString(), "", "");          
            ddlexam.SelectedIndex = 0;
            gv_marks_entry.DataSource = null;
            gv_marks_entry.DataBind();        
        }

        else
        {           
            //exam
            ddlexam.SelectedIndex = 0;
            ddlexam.Enabled = false;

            //grid
            gv_marks_entry.DataSource = null;
            gv_marks_entry.DataBind();

            //subject
            ddlsubject.SelectedIndex = 0;
            ddlsubject.Enabled = false;

            //division
            ddldivision.SelectedIndex = 0;
            ddldivision.Enabled = false;

            //criteria
            marks_criteria.Visible = false;

            //buttons
            btn_export.Visible = false;
            btn_cancel.Visible = false;
            btn_edit.Visible = false;
        }
    }

    protected void ddlexam_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlexam.SelectedIndex > 0)
        {
            if (Session["acdyear"] == null)
            {
                notifys("Please first select academic year.", "#D44950");
                return;
            }
            else
            {
                fillsub(ddlmedium.SelectedValue.ToString(), ddlstandard.SelectedValue.ToString(), ddlexam.SelectedValue.ToString(), Session["acdyear"].ToString());
            }
        }
        else
        {
            //subject
            ddlsubject.SelectedIndex = 0;
            ddlsubject.Enabled = false;


            //grid
            gv_marks_entry.DataSource = null;
            gv_marks_entry.DataBind();

           
            //division
            ddldivision.SelectedIndex = 0;
            ddldivision.Enabled = false;

            //criteria
            marks_criteria.Visible = false;

            //buttons
            btn_export.Visible = false;
            btn_cancel.Visible = false;
            btn_edit.Visible = false;
        }
    }

    public void filldiv(string ayid, string medium, string std)
    {
        string html = string.Empty;

        string urlalias = cls.urls();
        string url = @urlalias + "MarksEntry/";
      //  string url = @"http://localhost:9199/MarksEntry";
        useless();
        me.ayid = ayid;
        me.medium = medium;
        me.standard = std;
        me.type = "filldiv";

        string jsonString = JsonHelper.JsonSerializer<marks_entry>(me);
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

            //To use it later to assign standard
            Session["DSLIST"] = dslist;
            DataTable dt1 = dslist.Tables["Table1"];
            ddldivision.DataSource = dt1;
            ddldivision.DataTextField = "division_name";
            ddldivision.DataValueField = "division_id";       
            ddldivision.DataBind();
            ddldivision.Items.Insert(0, "----SELECT----");
            ddldivision.SelectedIndex = 0;
            ddldivision.Enabled = true;
        }
    }

    public void fill_marks_criteria(string medium, string ayid, string subject_id, string standard, string exam_id)
    {
        string column_removal = "";
        string html = string.Empty;
        string urlalias = cls.urls();
        string url = @urlalias + "MarksEntry/";
        //string url = @"http://localhost:9199/MarksEntry";
        useless();
        me.ayid = ayid;
        me.medium = medium;
        me.standard = standard;
        me.type = "fill_marks_criteria";
        me.subject_id = subject_id;
        me.exam_id = exam_id;

        string jsonString = JsonHelper.JsonSerializer<marks_entry>(me);
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
               string grade = "";

            //To use it later to assign standard        
            DataTable dt1 = dslist.Tables["Table1"];
            if (dt1.Rows.Count <= 3)
            {
                txttabio.Text = "";
                txttabip.Text = "";
                txttabpo.Text = "";
                txttabpp.Text = "";
                txttabto.Text = "";
                txttabtp.Text = "";
             
                foreach (DataRow row in dt1.Rows)
                {
                    string exam_type = row["exam_type"].ToString();
                    if (exam_type == "0")
                    {
                        string pass = row["passing"].ToString();
                        txttabtp.Text = pass.ToString();

                        string out_of = row["out_of"].ToString();
                        txttabto.Text = out_of.ToString();
                    }

                    else if (exam_type == "1")
                    {
                        string pass = row["passing"].ToString();
                        txttabip.Text = pass.ToString();

                        string out_of = row["out_of"].ToString();
                        txttabio.Text = out_of.ToString();
                    }

                    else if (exam_type == "2")
                    {
                        string pass = row["passing"].ToString();
                        txttabpp.Text = pass.ToString();

                        string out_of = row["out_of"].ToString();
                        txttabpo.Text = out_of.ToString();
                    }
                    else if (exam_type == "3")
                    {
                        grade = "Grade";
                    }

                    else
                    {

                    }
                }
            }

            

            if (txttabto.Text == "")
            {
                txttabto.Visible = false;
                txttabtp.Visible = false;
                lbltabto.Visible = false;
                lbltabtp.Visible = false;
                lbltab_thtemp.Visible = true;
                column_removal += "theory,";
              

            }
            else
            {
                txttabto.Visible = true;
                txttabtp.Visible = true;
                lbltabto.Visible = true;
                lbltabtp.Visible = true;
                lbltab_thtemp.Visible = false;
                
            }

            if (txttabpo.Text == "")
            {
                txttabpo.Visible = false;
                txttabpp.Visible = false;
                lbltabpo.Visible = false;
                lbltabpp.Visible = false;
                lbltab_prtemp.Visible = true;
                column_removal += "practical,";
               
            }
            else
            {
                txttabpo.Visible = true;
                txttabpp.Visible = true;
                lbltabpo.Visible = true;
                lbltabpp.Visible = true;
                lbltab_prtemp.Visible = false;
            }

            if(txttabio.Text == "")
            {
                txttabio.Visible = false;
                txttabip.Visible = false;
                lbltabio.Visible = false;
                lbltabip.Visible = false;
                lbltab_intemp.Visible = true;
                column_removal += "internal,";
                

            }
            else
            {
                txttabio.Visible = true;
                txttabip.Visible = true;
                lbltabio.Visible = true;
                lbltabip.Visible = true;
                lbltab_intemp.Visible = false;
            }
            if (grade.ToString() == "")
            {
                column_removal += "Grade,";
            }
        }

        if (column_removal != "")
        {
            column_removal = column_removal.Remove(column_removal.Length - 1);
            Session["column_removal"] = column_removal;
        }
        else
        {
            Session["column_removal"] = null;
        }
        
    }

    protected void ddlsubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlsubject.SelectedIndex > 0)
        {
            if (Session["acdyear"] == null)
            {
                notifys("Please first select academic year.", "#D44950");
                return;
            }
            else
            {
                filldiv(Session["acdyear"].ToString(), ddlmedium.SelectedValue.ToString(), ddlstandard.SelectedValue.ToString());
                fill_marks_criteria(ddlmedium.SelectedValue.ToString(), Session["acdyear"].ToString(), ddlsubject.SelectedValue.ToString(), ddlstandard.SelectedValue.ToString(), ddlexam.SelectedValue.ToString());
                gv_marks_entry.DataSource = null;
                gv_marks_entry.DataBind();
            }

        }
        else
        {

            //division
            ddldivision.SelectedIndex = 0;
            ddldivision.Enabled = false;

            //grid
            gv_marks_entry.DataSource = null;
            gv_marks_entry.DataBind();

            //criteria
            marks_criteria.Visible = false;

            //buttons
            btn_export.Visible = false;
            btn_cancel.Visible = false;
            btn_edit.Visible = false;
        }
    }

    public void notifys(string msg, string color)
    {
        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert12", "$.notify('" + msg + "', { color: '#fff', background: '" + color + "', blur: 0.2, delay: 0 });", true);
    }

    protected void BindGrid()
    {
        DataTable dt = (DataTable)ViewState["dt"];
        gv_marks_entry.DataSource = ViewState["dt"] as DataTable;
        gv_marks_entry.DataBind();
    }

    public void compare_DT()
    {
        DataTable dt = ViewState["dt"] as DataTable;
        DataTable spareDT = ViewState["spareDT"] as DataTable;

        if (dt.Rows.Count == spareDT.Rows.Count) {
                        
        
        }

    }
    
    //fill the grid
    public void fillgrid(string ayid, string med, string std, string exam, string sub, string div)
    {
        string html = string.Empty;
        string urlalias = cls.urls();
        string url = @urlalias + "MarksEntry/";
      //  string url = @"http://localhost:9199//MarksEntry";
        useless();
        me.type = "fillgrid";
        me.ayid = ayid;
        me.division_id = div;
        me.exam_id = exam;
        me.medium = med;
        me.standard = std;
        me.subject_id = sub;
              
        string jsonString = JsonHelper.JsonSerializer<marks_entry>(me);
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
      
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        using (StreamReader sr = new StreamReader(httpWebResponse.GetResponseStream()))
        {
            string result = sr.ReadToEnd();
            ds = JsonConvert.DeserializeObject<DataSet>(result);
            
        }

        dt = ds.Tables["Table1"];
        DataTable spareDT = dt;
        DataColumn newColumn = new System.Data.DataColumn("data_flag", typeof(System.String));
        newColumn.DefaultValue = "old";
        spareDT.Columns.Add(newColumn);
       

        if (dt.Rows.Count > 0)
        {
            ViewState["dt"] = dt;
            ViewState["spareDT"] = spareDT;
          
            gv_marks_entry.DataSource = dt;
            gv_marks_entry.DataBind();

            if (Session["column_removal"] != null)
            {
                string[] col_list = Session["column_removal"].ToString().Split(new char[] { ',' });
                gv_marks_entry.Columns[3].Visible = true;
                tbl_theory.Visible = true;
                gv_marks_entry.Columns[4].Visible = true;
                tbl_internal.Visible = true;
                gv_marks_entry.Columns[5].Visible = true;
                tbl_practical.Visible = true;
                gv_marks_entry.Columns[6].Visible = true;
                tbl_grade.Visible = true;

                foreach (string item in col_list)
                {
                    if (item == "theory")
                    {
                        gv_marks_entry.Columns[3].Visible = false;
                        tbl_theory.Visible = false;
                    }
                    else if (item == "internal")
                    {
                        gv_marks_entry.Columns[4].Visible = false;
                        tbl_internal.Visible = false;
                    }
                    else if (item == "practical")
                    {
                        gv_marks_entry.Columns[5].Visible = false;
                        tbl_practical.Visible = false;
                    }
                    else if (item == "Grade")
                    {
                        gv_marks_entry.Columns[6].Visible = false;
                        tbl_grade.Visible = false;
                    }
                }
            }

          
        }
        else
        {
            notifys("Student Data is not available for given selection.", "#D44950");
            gv_marks_entry.DataSource = null;
            gv_marks_entry.DataBind();
        }
    }

    //fill the grid in this event.
    protected void ddldivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldivision.SelectedIndex > 0)
        {
            if (Session["acdyear"] == null)
            {
                notifys("Please first select academic year.", "#D44950");
                return;
            }
            else
            {
                marks_criteria.Visible = true;
                fillgrid(Session["acdyear"].ToString(), ddlmedium.SelectedValue.ToString(), ddlstandard.SelectedValue.ToString(), ddlexam.SelectedValue.ToString(), ddlsubject.SelectedValue.ToString(), ddldivision.SelectedValue.ToString());
                btn_export.Visible = true;
                btn_display_import.Visible = true;
                if (btn_edit.Text == "Update")
                {
                    btn_cancel.Visible = false;
                    btn_edit.Text ="Edit";
                }

                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "make_visible()", true);
                btn_edit.Visible = true;
            }
        }
        else
        {
            
            //grid
            gv_marks_entry.DataSource = null;
            gv_marks_entry.DataBind();            

            //buttons
            btn_export.Visible = false;
            btn_cancel.Visible = false;
            btn_edit.Visible = false;
        }
    }   

    //called to insert and update on edit event
    protected void btn_edit_Click(object sender, EventArgs e)
    {
      
        if (btn_edit.Text == "Edit")
        {
            ViewState["datatable"] = gv_marks_entry.DataSource as DataTable;
            DataTable spareDT = ViewState["spareDT"] as DataTable;
            ViewState["previous_spare_dt"] = spareDT;

            if (gv_marks_entry.Rows.Count > 0)
            {

            }
            else
            {
                notifys("Cannot edit because table does not contain data.", "#D44950");
                return;
            }

            foreach (GridViewRow row in gv_marks_entry.Rows)
            {
                TextBox txt_theory = row.FindControl("lblgv_theory") as TextBox;
                TextBox txt_internal = row.FindControl("lblgv_internal") as TextBox;
                TextBox txt_practical = row.FindControl("lblgv_practical") as TextBox;
                TextBox txt_grade = row.FindControl("lblgv_grade") as TextBox;
                txt_theory.Enabled = true;
                txt_internal.Enabled = true;
                txt_practical.Enabled = true;
                txt_grade.Enabled = true;
            }          

            btn_edit.Text = "Update";
            btn_cancel.Visible = true;
            btn_export.Visible = false;
         
            return;
        }

        if (btn_edit.Text == "Update")
        {
            string th_type = "", int_type = "", pr_type = "", grd_type = "";
            DataTable spareDT = ViewState["spareDT"] as DataTable;
           
            if (gv_marks_entry.Rows.Count > 0)
            {

            }
            else
            {
                notifys("Marks Entry table doesnot contain data.", "#D44950");
                return;
            }


            foreach (GridViewRow row in gv_marks_entry.Rows)
            {
                TextBox txt_theory = row.FindControl("lblgv_theory") as TextBox;
                TextBox txt_internal = row.FindControl("lblgv_internal") as TextBox;
                TextBox txt_practical = row.FindControl("lblgv_practical") as TextBox;
                TextBox txt_grade = row.FindControl("lblgv_grade") as TextBox;
                Label student_id = row.FindControl("lblgv_std_id") as Label;

                int index = row.RowIndex;

                string pr_student_id = spareDT.Rows[index].Field<string>("Student_id");
                Int64 pr_theory = spareDT.Rows[index].Field<Int64>("theory");
                Int64 pr_internal = spareDT.Rows[index].Field<Int64>("internal");
                Int64 pr_practical = spareDT.Rows[index].Field<Int64>("practical");
                string pr_grade = spareDT.Rows[index].Field<string>("Grade");
                

                if (student_id.Text.ToString() == pr_student_id)
                {
                    if (txt_theory.Text == "")
                    {
                        txt_theory.Text = "0";
                    }
                    if (txt_internal.Text == "")
                    {
                        txt_internal.Text = "0";
                    }
                    if (txt_practical.Text == "")
                    {
                        txt_practical.Text = "0";
                    }
                    if (txt_grade.Text == "")
                    {
                        txt_grade.Text = "";
                    }



                    if (Convert.ToInt64(txt_theory.Text.ToString()) != pr_theory || Convert.ToInt64(txt_internal.Text.ToString()) != pr_internal || Convert.ToInt64(txt_practical.Text.ToString()) != pr_practical || txt_grade.Text.ToString() != pr_grade.ToString())                    
                    {
                        if (pr_theory == 0 && pr_internal == 0 && pr_practical == 0 && pr_grade =="")
                        {
                            spareDT.Rows[index]["data_flag"] = "insert";
                            if (tbl_theory.Visible == true)
                            {
                                spareDT.Rows[index]["theory"] = Convert.ToInt64(txt_theory.Text.ToString());
                                th_type = "Theroy";
                            }
                            if (tbl_internal.Visible == true)
                            {
                                spareDT.Rows[index]["internal"] = Convert.ToInt64(txt_internal.Text.ToString());
                                int_type = "Internal";
                            }
                            if (tbl_practical.Visible == true)
                            {
                                spareDT.Rows[index]["practical"] = Convert.ToInt64(txt_practical.Text.ToString());
                                pr_type = "Practical";
                            }
                            if (tbl_grade.Visible == true)
                            {
                                spareDT.Rows[index]["Grade"] =txt_grade.Text.ToString();
                                grd_type="Grade";
                            }

                        }

                        else
                        {
                            spareDT.Rows[index]["data_flag"] = "update";
                            if (tbl_theory.Visible == true)
                            {
                                spareDT.Rows[index]["theory"] = Convert.ToInt64(txt_theory.Text.ToString());
                                th_type = "Theroy";
                            }
                            if (tbl_internal.Visible == true)
                            {
                                spareDT.Rows[index]["internal"] = Convert.ToInt64(txt_internal.Text.ToString());
                                int_type = "Internal";
                            }
                            if (tbl_practical.Visible == true)
                            {
                                spareDT.Rows[index]["practical"] = Convert.ToInt64(txt_practical.Text.ToString());
                                pr_type = "Practical";
                            }
                            if (tbl_grade.Visible == true)
                            {
                                spareDT.Rows[index]["Grade"] =txt_grade.Text.ToString();
                                grd_type = "Grade";
                            }
                           
                        }                       
                    }
                }

                txt_theory.Enabled = false;
                txt_internal.Enabled = false;
                txt_practical.Enabled = false;
                txt_grade.Enabled = false;
            }

            ViewState["spareDT"] = spareDT;
            btn_edit.Text = "Edit";
            btn_cancel.Visible = false;
            btn_export.Visible = true;

            btn_display_import.Visible = true;

            //insert or update
            DataTable tbl_insert_Filtered = null;
            DataTable tbl_update_Filtered = null;
            int count = 0;
            try
            {
                tbl_insert_Filtered = spareDT.AsEnumerable().Where(r => r.Field<string>("data_flag") == "insert").CopyToDataTable();
            }
            catch (Exception ex)
            {
                count = 1;
            }
            if (count == 0)
            {
                string[] stud_id_arr = tbl_insert_Filtered.AsEnumerable().Select(r => r.Field<string>("Student_id")).ToArray();
                Int64[] th_arr = tbl_insert_Filtered.AsEnumerable().Select(r => r.Field<Int64>("theory")).ToArray();
                Int64[] inter_arr = tbl_insert_Filtered.AsEnumerable().Select(r => r.Field<Int64>("internal")).ToArray();
                Int64[] pr_arr = tbl_insert_Filtered.AsEnumerable().Select(r => r.Field<Int64>("practical")).ToArray();
                string[] grd_arr = tbl_insert_Filtered.AsEnumerable().Select(r => r.Field<string>("Grade")).ToArray();

                string[] theory_arr = th_arr.Select(x => x.ToString()).ToArray();
                string[] internal_arr = inter_arr.Select(x => x.ToString()).ToArray();
                string[] practical_arr = pr_arr.Select(x => x.ToString()).ToArray();
                string[] grade_arr = grd_arr.Select(x => x.ToString()).ToArray();

                if (tbl_theory.Visible == true)
                {
                    th_type = "Theroy";
                }
                if (tbl_internal.Visible == true)
                {
                    int_type = "Internal";
                }
                if (tbl_practical.Visible == true)
                {
                    pr_type = "Practical";
                }
                if (tbl_grade.Visible == true)
                {
                    grd_type = "Grade";
                }
                on_edit_fill_marks_entry(stud_id_arr, theory_arr, internal_arr, practical_arr,grade_arr,th_type,int_type,pr_type,grd_type);
                notifys("Saved Successfully.", "#198104");
               
            }
            count = 0;

            try
            {
                tbl_update_Filtered = spareDT.AsEnumerable().Where(r => r.Field<string>("data_flag") == "update").CopyToDataTable();
            }
            catch (Exception ex)
            {
                count = 1;
            }
            if (count == 0)
            {
                string[] stud_id_arr = tbl_update_Filtered.AsEnumerable().Select(r => r.Field<string>("Student_id")).ToArray();
                
                Int64[] th_arr = tbl_update_Filtered.AsEnumerable().Select(r => r.Field<Int64>("theory")).ToArray();
                Int64[] inter_arr = tbl_update_Filtered.AsEnumerable().Select(r => r.Field<Int64>("internal")).ToArray();
                Int64[] pr_arr = tbl_update_Filtered.AsEnumerable().Select(r => r.Field<Int64>("practical")).ToArray();
                string[] grd_arr = tbl_update_Filtered.AsEnumerable().Select(r => r.Field<string>("Grade")).ToArray();


                string[] theory_arr = th_arr.Select(x => x.ToString()).ToArray();
                string[] internal_arr = inter_arr.Select(x => x.ToString()).ToArray();
                string[] practical_arr = pr_arr.Select(x => x.ToString()).ToArray();
                string[] grade_arr = grd_arr.Select(x => x.ToString()).ToArray();

                if (tbl_theory.Visible == true)
                {
                    th_type = "Theroy";
                }
                if (tbl_internal.Visible == true)
                {
                    int_type = "Internal";
                }
                if (tbl_practical.Visible == true)
                {
                    pr_type = "Practical";
                }
                if (tbl_grade.Visible == true)
                {
                    grd_type = "Grade";
                }

                on_edit_update_marks_entry(stud_id_arr, theory_arr, internal_arr, practical_arr, grade_arr, th_type, int_type, pr_type, grd_type);
                notifys("Saved Successfully.", "#198104");
            }

            count = 0;
        }


        if (btn_edit.Text == "Update_old")
        {
            string th_type = "", int_type = "", pr_type = "", grd_type = "";
            foreach (GridViewRow row in gv_marks_entry.Rows)
            {
                TextBox txt_theory = row.FindControl("lblgv_theory") as TextBox;
                TextBox txt_internal = row.FindControl("lblgv_internal") as TextBox;
                TextBox txt_practical = row.FindControl("lblgv_practical") as TextBox;
                TextBox txt_grade = row.FindControl("lblgv_grade") as TextBox;
                txt_theory.Enabled = false;
                txt_internal.Enabled = false;
                txt_practical.Enabled = false;
                txt_grade.Enabled = false;

            }

            DataTable dt_temp = new DataTable();
            dt_temp.Columns.Add("Gr. No.");
            dt_temp.Columns.Add("Student_id");
            dt_temp.Columns.Add("student_name");
            dt_temp.Columns.Add("Roll No");
            dt_temp.Columns.Add("theory");
            dt_temp.Columns.Add("internal");
            dt_temp.Columns.Add("practical");
            dt_temp.Columns.Add("grade");

            foreach (GridViewRow row in gv_marks_entry.Rows)
            {
                Label _gr_no = row.FindControl("lblgv_gr_no") as Label;
                Label _std_id = row.FindControl("lblgv_std_id") as Label;
                Label _std_name = row.FindControl("lblgv_std_name") as Label;
                Label _roll_no = row.FindControl("lblgv_roll_no") as Label;
                TextBox _th_marks = row.FindControl("lblgv_theory") as TextBox;
                TextBox _in_marks = row.FindControl("lblgv_internal") as TextBox;
                TextBox _pr_marks = row.FindControl("lblgv_practical") as TextBox;
                TextBox _grd_marks = row.FindControl("lblgv_grade") as TextBox;

                DataRow dr = dt_temp.NewRow();              
                
                    dr["Gr. No."] = _gr_no.Text;
                    dr["Student_id"] = _std_id.Text;
                    dr["student_name"] = _std_name.Text;
                    dr["Roll No"] = _roll_no.Text;
                    dr["theory"] = _th_marks.Text;
                    dr["internal"] = _in_marks.Text;
                    dr["practical"] = _pr_marks.Text;
                    dr["grade"] = _grd_marks.Text; 
                dt_temp.Rows.Add(dr);                               
            }

            string[] stud_id_arr = dt_temp.AsEnumerable().Select(r => r.Field<string>("Student_id")).ToArray();
            string[] theory_arr = dt_temp.AsEnumerable().Select(r => r.Field<string>("theory")).ToArray();
            string[] internal_arr = dt_temp.AsEnumerable().Select(r => r.Field<string>("internal")).ToArray();
            string[] practical_arr = dt_temp.AsEnumerable().Select(r => r.Field<string>("practical")).ToArray();
            string[] grade_arr = dt_temp.AsEnumerable().Select(r => r.Field<string>("grade")).ToArray();

            if (tbl_theory.Visible == true)
            {
                th_type = "Theroy";
            }
            if (tbl_internal.Visible == true)
            {
                int_type = "Internal";
            }
            if (tbl_practical.Visible == true)
            {
                pr_type = "Practical";
            }
            if (tbl_grade.Visible == true)
            {
                grd_type = "Grade";
            }

            on_edit_fill_marks_entry(stud_id_arr, theory_arr, internal_arr, practical_arr,grade_arr,th_type,int_type,pr_type,grd_type);
            btn_edit.Text = "Edit";
            btn_cancel.Visible = false;
            btn_export.Visible = true;
            btn_display_import.Visible = true;
        
        }
    }
  
    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the runtime error "  
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }

    public DataTable get_dt()
    {
        DataTable dtable = new DataTable();
        dtable = ViewState["dt"] as DataTable;

        if (dtable != null || dtable.Rows.Count > 0)
        {
            try
            {
                dtable.Columns.Remove("data_flag");
                dtable.AcceptChanges();
                if (!tbl_theory.Visible == true)
                {
                    dtable.Columns.Remove("theory");
                    dtable.AcceptChanges();
                }
                if (!tbl_internal.Visible == true) 
                {
                    dtable.Columns.Remove("internal");
                    dtable.AcceptChanges();
                }

                if (!tbl_practical.Visible == true)
                {
                    dtable.Columns.Remove("practical");
                    dtable.AcceptChanges();
                }
                if (!tbl_grade.Visible == true)
                {
                    dtable.Columns.Remove("Grade");
                    dtable.AcceptChanges();
                }

            }
            catch (Exception ex)
            { }

            DataTable dtable2 = new DataTable();
            dtable2.Columns.Add("Student ID");
            dtable2.Columns.Add("Student Name");
            dtable2.Columns.Add("Roll No.");
            try
            {
                if (tbl_theory.Visible == true)
                {
                    dtable2.Columns.Add("Theory Marks");
                }

                if (tbl_internal.Visible == true)
                {
                    dtable2.Columns.Add("Internal Marks");
                }

                if (tbl_practical.Visible == true)
                {
                    dtable2.Columns.Add("Practical Marks");
                }
                if (tbl_grade.Visible == true)
                {
                    dtable2.Columns.Add("Grade");
                }
            }
            catch (Exception ex)
            {
                notifys("File export was unsuccessful", "#D44950");
               
            }
            
            dtable2 = dtable.Copy();
            return dtable2;
        }
        else
        {
            DataTable dtable2 = new DataTable();
            return dtable2;
        }
    }

    protected void btn_export_Click(object sender, EventArgs e)
    {      
        DataTable dt = get_dt();
        if (dt != null || dt.Rows.Count > 0)
        {

        }
        else
        {
            notifys("No data available in grid to export in excel.", "#D44950");
            return;
        }
        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(dt);
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string excel_name = ddlstandard.SelectedItem.Text.ToString() + "_" + ddlexam.SelectedItem.Text.ToString() + "_" + ddlsubject.SelectedItem.Text.ToString() + "_" + ddldivision.SelectedItem.Text.ToString();
            Session["excel_name"] = excel_name;
            Response.AddHeader("content-disposition", "attachment;filename=" + excel_name +  ".xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }
    }    
 
    protected void btn_import_Click(object sender, EventArgs e)
    {
        try
        {
          
            string FilePath = "";
         
                try
                {
                    string FileName = FileUpload1.FileName;
                    string excel_name = "";
                    
                    if (ddlstandard.SelectedIndex > 0 && ddlexam.SelectedIndex > 0 && ddlsubject.SelectedIndex > 0 && ddldivision.SelectedIndex > 0)
                    {
                        excel_name = ddlstandard.SelectedItem.Text.ToString() + "_" + ddlexam.SelectedItem.Text.ToString() + "_" + ddlsubject.SelectedItem.Text.ToString() + "_" + ddldivision.SelectedItem.Text.ToString();
                    }
                    else if (Session["excel_name"] != null)
                    {
                        excel_name = Session["excel_name"].ToString();
                    }

                    else
                    {
                        notifys("File name does not match.", "#D44950");
                    }
                    string[] split_string = FileName.Split('.');
                   

                    if (FileName != "")
                    {
                        string Extension = Path.GetExtension(FileUpload1.FileName);
                        string FolderPath = HttpContext.Current.Request.MapPath("~/excel_list/");
                        if (!(Directory.Exists(FolderPath)))
                        {
                            Directory.CreateDirectory(FolderPath);
                        }
                        FilePath = (Path.Combine(FolderPath + FileName));
                        if (Directory.Exists(FileName))
                        {
                            File.Delete(FileName);
                        }

                        FileUpload1.SaveAs(FilePath);

                        string conString = string.Empty;
                        string extension = Path.GetExtension(FileUpload1.FileName);
                        switch (extension)
                        {
                            case ".xls": //Excel 97-03
                                conString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES'";
                                break;
                            case ".xlsx": //Excel 07 or higher
                                conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES'";
                                break;
                        }

                        conString = String.Format(conString, FilePath, Header);

                        using (OleDbConnection conn = new OleDbConnection(conString))
                        {
                            conn.Open();
                            OleDbDataAdapter objDA = new System.Data.OleDb.OleDbDataAdapter
                            (@"select * from [Table1$]", conn);
                            DataSet excelDataSet = new DataSet();
                            objDA.Fill(excelDataSet);

                            DataTable newDT = excelDataSet.Tables[0];
                            try
                            {
                                if (!newDT.Columns.Contains("theory"))
                                {
                                    DataColumn newColumn = new System.Data.DataColumn("theory", typeof(System.String));
                                    newColumn.DefaultValue = "0";
                                    newDT.Columns.Add(newColumn);
                                }

                                if (!newDT.Columns.Contains("internal"))
                                {
                                    DataColumn newColumn = new System.Data.DataColumn("internal", typeof(System.String));
                                    newColumn.DefaultValue = "0";
                                    newDT.Columns.Add(newColumn);
                                }

                                if (!newDT.Columns.Contains("practical"))
                                {
                                    DataColumn newColumn = new System.Data.DataColumn("practical", typeof(System.String));
                                    newColumn.DefaultValue = "0";
                                    newDT.Columns.Add(newColumn);
                                }
                                if (!newDT.Columns.Contains("grade"))
                                {
                                    DataColumn newColumn = new System.Data.DataColumn("grade", typeof(System.String));
                                    newColumn.DefaultValue = "0";
                                    newDT.Columns.Add(newColumn);
                                }
                            }
                            catch (Exception ex)
                            {
                                notifys("Excel file import was unsuccessful.", "#D44950");
                                return;
                            }
                            DataTable spareDT = ViewState["spareDT"] as DataTable;
                            ViewState["previous_spare_dt"] = spareDT;

                            if (newDT.Rows.Count != spareDT.Rows.Count)
                            {
                                notifys("Student data row count does not match with the imported file. Unsuccessful import.", "#D44950");
                            }

                            for (int i = 0; i < spareDT.Rows.Count; i++)
                            {
                                string stud_id_pr = spareDT.Rows[i]["Student_id"].ToString();
                                string theory_pr = spareDT.Rows[i]["theory"].ToString();
                                string inter_pr = spareDT.Rows[i]["internal"].ToString();
                                string practical_pr = spareDT.Rows[i]["practical"].ToString();
                                string grade_pr = spareDT.Rows[i]["Grade"].ToString();

                                string stud_id = newDT.Rows[i]["Student_id"].ToString();
                                string theory = newDT.Rows[i]["theory"].ToString();
                                string inter = newDT.Rows[i]["internal"].ToString();
                                string practical = newDT.Rows[i]["practical"].ToString();
                                string grade = newDT.Rows[i]["Grade"].ToString();

                                if (stud_id == stud_id_pr)
                                {
                                    if (theory != theory_pr || inter != inter_pr || practical != practical_pr || grade != grade_pr)
                                    {
                                        if (theory_pr == "0" && inter_pr == "0" && practical_pr == "0")
                                        {
                                            spareDT.Rows[i]["data_flag"] = "insert";
                                            if (theory.ToString() != "" && txttabto.Text != "" && txttabtp.Text != "" )
                                            {
                                                if ((Convert.ToInt32(theory.ToString()) >= Convert.ToInt32(txttabto.Text.ToString())))
                                                {
                                                    spareDT.Rows[i]["theory"] = "";
                                                    spareDT.Rows[i]["theory"] = System.Drawing.Color.Black;
                                                }
                                                else 
                                                {
                                                    spareDT.Rows[i]["theory"] = Convert.ToInt64(theory);
                                                }
                                            }
                                            if (inter.ToString() != "" && txttabio.Text != "" && txttabip.Text != "")
                                            {
                                                if ((Convert.ToInt32(inter.ToString()) >= Convert.ToInt32(txttabio.Text.ToString())))
                                                {
                                                    spareDT.Rows[i]["internal"] = "";
                                                    spareDT.Rows[i]["internal"] = System.Drawing.Color.Black;
                                                }
                                                else
                                                {
                                                    spareDT.Rows[i]["internal"] = Convert.ToInt64(inter);
                                                }
                                            }
                                            if (practical.ToString() != "" && txttabpo.Text != "" && txttabpp.Text != "")
                                            {
                                                if ((Convert.ToInt32(practical.ToString()) >= Convert.ToInt32(txttabpo.Text.ToString())))
                                                {
                                                    spareDT.Rows[i]["practical"] = "";
                                                    spareDT.Rows[i]["practical"] = System.Drawing.Color.Black;
                                                }
                                                else
                                                {
                                                    spareDT.Rows[i]["practical"] = Convert.ToInt64(practical);
                                                }
                                            }
                                            if (grade.ToString() != "")
                                            {
                                                spareDT.Rows[i]["Grade"] = grade;
                                               
                                              
                                            }
                                            //spareDT.Rows[i]["theory"] = Convert.ToInt64(theory);
                                            //spareDT.Rows[i]["inter"] = Convert.ToInt64(inter);
                                            //spareDT.Rows[i]["practical"] = Convert.ToInt64(practical);
                                        }

                                        else
                                        {
                                            spareDT.Rows[i]["data_flag"] = "update";
                                            //spareDT.Rows[i]["theory"] = Convert.ToInt64(theory);
                                            //spareDT.Rows[i]["internal"] = Convert.ToInt64(inter);
                                            //spareDT.Rows[i]["practical"] = Convert.ToInt64(practical);
                                            if (theory.ToString() != "" && txttabto.Text != "" && txttabtp.Text != "")
                                            {
                                                if ((Convert.ToInt32(theory.ToString()) >= Convert.ToInt32(txttabto.Text.ToString())))
                                                {
                                                    spareDT.Rows[i]["theory"] = "";
                                                    spareDT.Rows[i]["theory"] = System.Drawing.Color.Black;
                                                }
                                                else
                                                {
                                                    spareDT.Rows[i]["theory"] = Convert.ToInt64(theory);
                                                }
                                            }
                                            if (inter.ToString() != "" && txttabio.Text != "" && txttabip.Text != "")
                                            {
                                                if ((Convert.ToInt32(inter.ToString()) >= Convert.ToInt32(txttabio.Text.ToString())))
                                                {
                                                    spareDT.Rows[i]["internal"] = "";
                                                    spareDT.Rows[i]["internal"] = System.Drawing.Color.Black;
                                                }
                                                else
                                                {
                                                    spareDT.Rows[i]["internal"] = Convert.ToInt64(inter);
                                                }
                                            }
                                            if (practical.ToString() != "" && txttabpo.Text != "" && txttabpp.Text != "")
                                            {
                                                if ((Convert.ToInt32(practical.ToString()) >= Convert.ToInt32(txttabpo.Text.ToString())))
                                                {
                                                    spareDT.Rows[i]["practical"] = "";
                                                    spareDT.Rows[i]["practical"] = System.Drawing.Color.Black;
                                                }
                                                else
                                                {
                                                    spareDT.Rows[i]["practical"] = Convert.ToInt64(practical);
                                                }
                                            }
                                            if (grade.ToString() != "")
                                            {
                                                spareDT.Rows[i]["Grade"] = grade;
                                         

                                            }
                                        }
                                    }
                                }
                            }

                           
                            gv_marks_entry.DataSource = spareDT;
                            ViewState["spareDT"] = spareDT;
                            gv_marks_entry.DataBind();
                            notifys("Successfully imported excel file.", "#198104");
                        
                            btn_save.Visible = true;
                            btn_import_cancel.Visible = true;
                            btn_display_import.Visible = false;
                            btn_edit.Visible = false;
                            btn_export.Visible = false;
                          
                        }

                    }
                    else
                    {
                       
                        notifys("First choose file.", "#D44950");
                    }
                }
                catch (Exception ex)
                {
                    notifys("File import was unsuccessful!", "#D44950");
                }
            
        }

        catch (Exception ex)
        {
            notifys("Unsuccessful file import.", "#D44950");
        }
    }

    protected void gv_marks_entry_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
    }

    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        btn_cancel.Visible = false;        
        btn_edit.Text = "Edit";
        foreach (GridViewRow row in gv_marks_entry.Rows)
        {
            TextBox txt_theory = row.FindControl("lblgv_theory") as TextBox;
            TextBox txt_internal = row.FindControl("lblgv_internal") as TextBox;
            TextBox txt_practical = row.FindControl("lblgv_practical") as TextBox;
            TextBox txt_grade = row.FindControl("lblgv_grade") as TextBox;
            txt_theory.Enabled = false;
            txt_internal.Enabled = false;
            txt_practical.Enabled = false;
            txt_grade.Enabled = false;
        }

        if (ViewState["previous_spare_dt"] != null)
        {
            DataTable previous_spare_dt = ViewState["previous_spare_dt"] as DataTable;
            ViewState["spareDT"] = previous_spare_dt;
            gv_marks_entry.DataSource = ViewState["spareDT"] as DataTable;
            gv_marks_entry.DataBind();
        }

        
        btn_export.Visible = true;
        btn_edit.Visible = true;
        btn_display_import.Visible = true;
    }

    public void fill_grid_data()
    {
        if (ddldivision.SelectedIndex > 0 && ddlexam.SelectedIndex > 0 && ddlsubject.SelectedIndex > 0)
        {
            if (Session["acdyear"] != null)
            {
                gv_marks_entry.DataSource = null;
                gv_marks_entry.DataBind();
            }

            else
            {


            }
        }

        else
        {

        }
    }

    //extra for generating blank excel
    public void get_excel()
    {
        System.Data.DataTable dt = new System.Data.DataTable("GridView_Data");
        foreach (TableCell cell in gv_marks_entry.HeaderRow.Cells)
        {
            dt.Columns.Add(cell.Text);
        }

        foreach (GridViewRow row in gv_marks_entry.Rows)
        {
            dt.Rows.Add();

        }
        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(dt);
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=GridView.xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }
    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
        string th_type = "", int_type = "", pr_type = "", grd_type = "";

        DataTable spareDT = ViewState["spareDT"] as DataTable;

        //insert or update
        DataTable tbl_insert_Filtered = null;
        DataTable tbl_update_Filtered = null;
        int count = 0;
        try
        {
            tbl_insert_Filtered = spareDT.AsEnumerable().Where(r => r.Field<string>("data_flag") == "insert").CopyToDataTable();
        }
        catch (Exception ex)
        {
            count = 1;
        }

        if (count == 0)
        {
            string[] stud_id_arr = tbl_insert_Filtered.AsEnumerable().Select(r => r.Field<string>("Student_id")).ToArray();
            Int64[] th_arr = tbl_insert_Filtered.AsEnumerable().Select(r => r.Field<Int64>("theory")).ToArray();
            Int64[] inter_arr = tbl_insert_Filtered.AsEnumerable().Select(r => r.Field<Int64>("internal")).ToArray();
            Int64[] pr_arr = tbl_insert_Filtered.AsEnumerable().Select(r => r.Field<Int64>("practical")).ToArray();
            string[] grd_arr = tbl_insert_Filtered.AsEnumerable().Select(r => r.Field<string>("Grade")).ToArray();

            string[] theory_arr = th_arr.Select(x => x.ToString()).ToArray();
            string[] internal_arr = inter_arr.Select(x => x.ToString()).ToArray();
            string[] practical_arr = pr_arr.Select(x => x.ToString()).ToArray();
            string[] grade_arr = grd_arr.Select(x => x.ToString()).ToArray();

            if (tbl_theory.Visible == true)
            {
                th_type = "Theroy";
            }
            if (tbl_internal.Visible == true)
            {
                int_type = "Internal";
            }
            if (tbl_practical.Visible == true)
            {
                pr_type = "Practical";
            }
            if (tbl_grade.Visible == true)
            {
                grd_type = "Grade";
            }

            on_edit_fill_marks_entry(stud_id_arr, theory_arr, internal_arr, practical_arr, grade_arr, th_type, int_type, pr_type, grd_type);
            notifys("Saved Successfully.", "#198104");
        
            this.btn_display_import.Visible = true;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "make_visible()", true);
     
            this.btn_save.Visible = false;
            this.btn_edit.Visible = true;
            this.btn_export.Visible = true;
            this.btn_import_cancel.Visible = false;

        }
        count = 0;

        try
        {
            tbl_update_Filtered = spareDT.AsEnumerable().Where(r => r.Field<string>("data_flag") == "update").CopyToDataTable();
        }
        catch (Exception ex)
        {
            count = 1;
        }
        if (count == 0)
        {
            string[] stud_id_arr = tbl_update_Filtered.AsEnumerable().Select(r => r.Field<string>("Student_id")).ToArray();

            Int64[] th_arr = tbl_update_Filtered.AsEnumerable().Select(r => r.Field<Int64>("theory")).ToArray();
            Int64[] inter_arr = tbl_update_Filtered.AsEnumerable().Select(r => r.Field<Int64>("internal")).ToArray();
            Int64[] pr_arr = tbl_update_Filtered.AsEnumerable().Select(r => r.Field<Int64>("practical")).ToArray();
            string[] grd_arr = tbl_insert_Filtered.AsEnumerable().Select(r => r.Field<string>("Grade")).ToArray();


            string[] theory_arr = th_arr.Select(x => x.ToString()).ToArray();
            string[] internal_arr = inter_arr.Select(x => x.ToString()).ToArray();
            string[] practical_arr = pr_arr.Select(x => x.ToString()).ToArray();
            string[] grade_arr = grd_arr.Select(x => x.ToString()).ToArray();

            if (tbl_theory.Visible == true)
            {
                th_type = "Theroy";
            }
            if (tbl_internal.Visible == true)
            {
                int_type = "Internal";
            }
            if (tbl_practical.Visible == true)
            {
                pr_type = "Practical";
            }
            if (tbl_grade.Visible == true)
            {
                grd_type = "Grade";
            }

            on_edit_update_marks_entry(stud_id_arr, theory_arr, internal_arr, practical_arr, grade_arr,th_type,int_type,pr_type,grd_type);
            notifys("Saved Successfully.", "#198104");


      
            btn_import.Style.Add("display", "block");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "make_visible()", true);
         
            this.btn_save.Visible = false;
            this.btn_import_cancel.Visible = false;
   
            this.btn_edit.Visible = true;
            this.btn_export.Visible = true;
            this.btn_display_import.Visible = true;
        }

        count = 0;

    }

    protected void btn_import_cancel_Click(object sender, EventArgs e)
    {
        if (ViewState["previous_spare_dt"] != null)
        {
            DataTable previous_spare_dt = ViewState["previous_spare_dt"] as DataTable;
            ViewState["spareDT"] = previous_spare_dt;
            gv_marks_entry.DataSource = ViewState["spareDT"] as DataTable;
            gv_marks_entry.DataBind();
        }

        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "make_visible()", true);
      
        this.btn_save.Visible = false;
        this.btn_import_cancel.Visible = false;
        this.btn_display_import.Disabled = false;
        btn_cancel_Click(btn_cancel, EventArgs.Empty);
    }

    protected void lblgv_theory_TextChanged(object sender, EventArgs e)
        {
        try
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            TextBox txt_therory = (TextBox)row.FindControl("lblgv_theory");
            txt_therory.ForeColor = System.Drawing.Color.Black;
            if (txt_therory.Text != "" && txttabto.Text != "" && txttabtp.Text != "" && txt_therory.Text!="Ab")
            {
                if ((Convert.ToInt32(txt_therory.Text.ToString()) > Convert.ToInt32(txttabto.Text.ToString())))
                {
                    notifys("Marks should be less than " + txttabto.Text.ToString() + ".", "#D44950");
                    txt_therory.Text = "";
                    txt_therory.ForeColor = System.Drawing.Color.Black;
                }
                else if ((Convert.ToInt32(txt_therory.Text.ToString()) < Convert.ToInt32(txttabtp.Text.ToString())))
                {
                    txt_therory.ForeColor = System.Drawing.Color.Red;
                }
            }
           
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void lblgv_internal_TextChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            TextBox txt_internal = (TextBox)row.FindControl("lblgv_internal");
            txt_internal.ForeColor = System.Drawing.Color.Black;
            if (txt_internal.Text != "" && txttabio.Text != "" && txttabip.Text != "" && txt_internal.Text != "Ab")
            {
                if ((Convert.ToInt32(txt_internal.Text.ToString()) > Convert.ToInt32(txttabio.Text.ToString())))
                {
                    notifys("Marks should be less than " + txttabio.Text.ToString() + ".", "#D44950");
                    txt_internal.Text = "";
                    txt_internal.ForeColor = System.Drawing.Color.Black;
                }
                else if ((Convert.ToInt32(txt_internal.Text.ToString()) < Convert.ToInt32(txttabip.Text.ToString())))
                {
                    txt_internal.ForeColor = System.Drawing.Color.Red;
                }
            }

        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void lblgv_practical_TextChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            TextBox txt_prac = (TextBox)row.FindControl("lblgv_practical");
            txt_prac.ForeColor = System.Drawing.Color.Black;
            if (txt_prac.Text != "" && txttabpo.Text != "" && txttabpp.Text != "" && txt_prac.Text != "Ab")
            {
                if ((Convert.ToInt32(txt_prac.Text.ToString()) > Convert.ToInt32(txttabpo.Text.ToString())))
                {
                    notifys("Marks should be less than " + txttabpo.Text.ToString() + ".", "#D44950");
                    txt_prac.Text = "";
                    txt_prac.ForeColor = System.Drawing.Color.Black;
                }
                else if ((Convert.ToInt32(txt_prac.Text.ToString()) < Convert.ToInt32(txttabpp.Text.ToString())))
                {
                    txt_prac.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    public void on_edit_update_marks_entry(string[] std_id, string[] th, string[] intr, string[] pr, string[] grd, string thtype, string inttype, string prtype, string grdtype)
    {
        string html = string.Empty;
        string urlalias = cls.urls();
        string url = @urlalias + "MarksEntry/";
       // string url = @"http://localhost:9199//MarksEntry";
        useless();
        me.student_id = std_id;
        me.theory_marks = th;
        me.internal_marks = intr;
        me.practical_marks = pr;
        me.grade = grd;
        me.th_type = thtype;
        me.int_type = inttype;
        me.pr_type = prtype;
        me.grd_type = grdtype;

        if (Session["acdyear"] == null)
        {
            notifys("Please first select academic year.", "#D44950");
            return;
        }
        me.ayid = Session["acdyear"].ToString();
        me.division_id = ddldivision.SelectedValue.ToString();
        me.exam_id = ddlexam.SelectedValue.ToString();
        me.medium = ddlmedium.SelectedValue.ToString();
        me.standard = ddlstandard.SelectedValue.ToString();
        me.subject_id = ddlsubject.SelectedValue.ToString();
        me.type = "update_marks_entry";

        string jsonString = JsonHelper.JsonSerializer<marks_entry>(me);
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
            ViewState["dt"] = dslist.Tables["Table1"];
        }

        DataColumn newColumn = new System.Data.DataColumn("data_flag", typeof(System.String));
        newColumn.DefaultValue = "old";
        dslist.Tables[0].Columns.Add(newColumn);
        ViewState["spareDT"] = dslist.Tables["Table1"];
    }

    public void on_edit_fill_marks_entry(string[] std_id, string[] th, string[] intr, string[] pr, string[] grd, string thtype, string inttype, string prtype, string grdtype)
    {
        string html = string.Empty;
        string urlalias = cls.urls();
        string url = @urlalias + "MarksEntry/";
       // string url = @"http://localhost:9199//MarksEntry";
        useless();
        me.medium = ddlmedium.SelectedValue.ToString();
        me.standard = ddlstandard.SelectedValue.ToString();
        if (Session["acdyear"] == null)
        {
            notifys("Please first select academic year.", "#D44950");
            return;
        }

        me.ayid = Session["acdyear"].ToString();
        me.exam_id = ddlexam.SelectedValue.ToString();
        me.division_id = ddldivision.SelectedValue.ToString();
        me.subject_id = ddlsubject.SelectedValue.ToString();
        me.type = "on_edit_fill_marks_entry";
        me.student_id = std_id;
        me.theory_marks = th;
        me.internal_marks = intr;
        me.practical_marks = pr;
        me.grade = grd;
        me.th_type = thtype;
        me.int_type = inttype;
        me.pr_type = prtype;
        me.grd_type = grdtype;


        string jsonString = JsonHelper.JsonSerializer<marks_entry>(me);
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

        DataSet ds = new DataSet();
        using (StreamReader sr = new StreamReader(httpWebResponse.GetResponseStream()))
        {
            string result = sr.ReadToEnd();
            ds = JsonConvert.DeserializeObject<DataSet>(result);
        }


        DataColumn newColumn = new System.Data.DataColumn("data_flag", typeof(System.String));
        newColumn.DefaultValue = "old";
        ds.Tables[0].Columns.Add(newColumn);
        ViewState["spareDT"] = ds.Tables[0];
    }

    protected void lblgv_grade_TextChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            TextBox txt_grade = (TextBox)row.FindControl("lblgv_grade");
            txt_grade.ForeColor = System.Drawing.Color.Black;
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }
}