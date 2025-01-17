using ClosedXML.Excel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class frm_Console_Report : System.Web.UI.Page
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
       // string url = @"http://localhost:9199//Subject_Master";
        useless();
        sm.type = "ddlfill";

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
            ddlstandard.SelectedIndex = 0;
            ddlstandard.Enabled = false;

            ddlexam.SelectedIndex = 0;
            ddlexam.Enabled = false;

            //ddldivision.SelectedIndex = 0;
            ddldivision.Enabled = false;
            btn_console_report.Visible = false;

        }
    }

    protected void ddlstandard_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlstandard.SelectedIndex > 0)
        {
            ddlexam.Enabled = true;            
            fillexam(ddlmedium.SelectedValue.ToString(), ddlstandard.SelectedValue.ToString());            
            // gv_marks_entry.DataSource = null;
            // gv_marks_entry.DataBind();
        }
        else
        {
            ddlexam.SelectedIndex = 0;
            ddlexam.Enabled = false;

            //ddldivision.SelectedIndex = 0;
            ddldivision.Enabled = false;

            btn_console_report.Visible = false;
        }
    }

    protected void ddlexam_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlexam.SelectedIndex > 0)
        {
            if (Session["acdyear"] == null)
            {
                notifys("Please first select academic year.", "#ff0000");
                return;
            }
            else
            {
                filldiv(Session["acdyear"].ToString(), ddlmedium.SelectedValue.ToString(), ddlstandard.SelectedValue.ToString());

            }

            ddldivision.Enabled = true;
        }
        else
        {
            ddldivision.SelectedIndex = 0;
            ddldivision.Enabled = false;

            btn_console_report.Visible = false;

        }
    }
    //----------------------------------------------------------------

    public void filldiv(string ayid, string medium, string std)
    {
        string html = string.Empty;
        string urlalias = cls.urls();
        string url = @urlalias + "MarksEntry/";
       // string url = @"http://localhost:9199/MarksEntry";
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
            notifys("Please select academic year.", "#b00707");
            return;
        }
        else if (Session["acdyear"].ToString() == "----SELECT----")
        {
            notifys("Please select academic year.", "#b00707");
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
                notifys("No exam defined for given medium, class and academic year.", "#c20202");
            }

            ddlexam.DataSource = dt1;
            ddlexam.DataValueField = "exam_id";
            ddlexam.DataTextField = "exam_name";
            ddlexam.DataBind();
            ddlexam.Enabled = true;
            ddlexam.Items.Insert(0, "----SELECT----");
            ddlexam.SelectedIndex = 0;
            if(ddlstandard.SelectedItem.Text.ToString()=="9")
            {
                ddlexam.Items.Insert(1, "AVERAGE");
            }
        }
    }

    //-----------------------------------------------------------------------------
    protected void btn_console_report_Click(object sender, EventArgs e)
    {
        if (ddlmedium.SelectedIndex > 0 && ddlstandard.SelectedIndex > 0 && ddlexam.SelectedIndex > 0 && ddldivision.SelectedIndex > 0)
        {
            generate_excel_data();
            if (Session["error"].ToString() != "1")
            {
                btn_excel_Click(sender, e);
                notifys("Successfully generated final gazette.", "#198104");     
            }
            else
            {
                return;
            }
        }
        else
        {
            notifys("Please give all selection.", "#D44950");
            return;
        }
        //build table
        notifys("Successfully generated final gazette.", "#198104");
    }

    //-----------------------------------------------------------------------------
    public void generate_excel_data()
    {
        DataTable dt = new DataTable();
        string html = string.Empty;

        string urlalias = cls.urls();
        string url = @urlalias + "MarksEntry/";
        //string url = @"http://localhost:9199//MarksEntry";
        useless();

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
        if (ddlstandard.SelectedItem.Text.ToString() == "9" && ddlexam.SelectedItem.Text.ToString() == "AVERAGE")
        {
            me.type = "console_report_nine";
        }
        else
        {
            me.type = "console_report";
        }
     
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
        }

        DataTable dt1 = new DataTable();//subject
        dt1 = dslist.Tables[0];

        DataTable dt2 = new DataTable();//student
        dt2 = dslist.Tables[1];
        if (dt1.Rows.Count > 0)
        {
            if (dt2.Rows.Count > 0)
            {
                if (ddlstandard.SelectedItem.Text.ToString() == "9" &&  ddlexam.SelectedItem.Text.ToString() == "AVERAGE")
                {
                    build_html_table_nine(dt1, dt2);
                }
                else
                {
                    build_html_table(dt1, dt2);
                }
            }
            else
            {
                notifys("Marks Entry is not available.", "#D44950");
                Session["error"] = "1";
                return;
            }
          
        }
        else
        {
            notifys("Subject Not Define .", "#D44950");
            Session["error"] = "1";
            return;
        }
    }

    public void build_html_table(DataTable dt_sub, DataTable dt_stud)
    {
        try
        {
            StringBuilder sb = new StringBuilder();
            //---------------------------------------header--------------------------------------------------------------------------------------------------------
            var distinctIds = dt_sub.AsEnumerable().Select(s => new { id = s.Field<string>("subject_id"), }).Distinct().ToList();
            string no_of_subjects = distinctIds.Count.ToString();
            string no_of_rows = dt_sub.Rows.Count.ToString();
            string no_of_columns = ((Convert.ToInt32(no_of_subjects) * 2) + (Convert.ToInt32(no_of_rows)) + 5).ToString();
            string second_col = "";

                float second_fl = (Convert.ToInt32(no_of_columns) / 4);
                int second_in = (int)Math.Floor(second_fl);
                second_col = second_in.ToString();
            
            sb.Append("<table id='tbl' style = 'border: 1px solid black; width:85%; height:85%;vertical-align:middle;text-align:center' valign='center'>");
            sb.Append("<tr style='height:40px'>");
            sb.Append("<td colspan = " + no_of_columns + " style = 'border: 1px solid black ' align='center'><h1>Utkarsha Vidyalaya</h1></td>");
            sb.Append("</tr>");
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

            //2nd row
            sb.Append("<tr style='height:40px'>");
            sb.Append("<td colspan = " + second_col + " style = 'border: 1px solid black; font-size:18px ;text-align:center;'>Academic Year: " + Session["year"].ToString() + "</td><td colspan = " + second_col + " style = 'border: 1px solid black; font-size:18px;text-align:center;'>Exam: " + ddlexam.SelectedItem.Text.ToString() + "</td><td colspan = " + second_col + " style = 'border: 1px solid black; font-size:18px;text-align:center;'>Division: " + ddldivision.SelectedItem.Text.ToString() + "</td>");
            sb.Append("<td colspan = " + second_col + " style = 'border: 1px solid black; font-size:18px;text-align:center;'>Standard: " + ddlstandard.SelectedItem.Text.ToString() + "</td>");
            sb.Append("</tr>");

            //out of and passing marks
            //3rd row
            sb.Append("<tr style='height:40px'>");
            sb.Append("<td  style = 'border: 1px solid black' align='center' colspan = " + no_of_columns + "><h2>Final Gazette</h2></td>");
            sb.Append("</tr>");


          //-------------------------------------------------------Subject header--------------------------------------------------------------------------------------------------------
            
                sb.Append("<tr>");
                sb.Append("<td colspan = '2' style = 'border: 1px solid black; font-size:18px;text-align:center;'>Subject ID</td>");

                string subject_id="";
                string subject_name = "";
                string exam_type = "";
                string outof = "";//subject out of
                string passing = "";//subject passing
                int h_total = 0;//subject total out of
                int h_totp = 0;//subejct total passing 
                int grand_total = 0;
                int numberOfRecords = 0;

                for (int i = 0; i < dt_sub.Rows.Count; i++)
                {
                
                    if (i == 0)
                    {
                        numberOfRecords = dt_sub.AsEnumerable().Where(x => x["subject_id"].ToString() == dt_sub.Rows[i]["subject_id"].ToString()).ToList().Count + 2;

                        subject_id = "<td colspan =" + numberOfRecords.ToString() + " style = 'border: 1px solid black; font-size:18px;text-align:center;'>" + dt_sub.Rows[i]["subject_id"].ToString() + "</td>";
                        subject_name = "<td colspan =" + numberOfRecords.ToString() + " style = 'border: 1px solid black; font-size:18px;text-align:center;'>" + dt_sub.Rows[i]["subject_name"].ToString() + "</td>";
                    }
                    else if(i > 0)
                    {
                        if (dt_sub.Rows[i]["subject_id"].ToString() != (dt_sub.Rows[i - 1]["subject_id"].ToString()))
                       {
                           if (dt_sub.Rows[i]["exam_type"].ToString() != "3")
                           {
                               grand_total = grand_total + h_total;
                                h_total = 0;//subject total out of
                                h_totp = 0;//subejct total passing 

                               numberOfRecords = dt_sub.AsEnumerable().Where(x => x["subject_id"].ToString() == dt_sub.Rows[i - 1]["subject_id"].ToString()).ToList().Count + 2;

                               subject_id = subject_id + "<td colspan =" + numberOfRecords.ToString() + " style = 'border: 1px solid black; font-size:18px;text-align:center;'>" + dt_sub.Rows[i]["subject_id"].ToString() + "</td>";
                               subject_name = subject_name + "<td colspan =" + numberOfRecords.ToString() + "  style = 'border: 1px solid black; font-size:18px;text-align:center;'>" + dt_sub.Rows[i]["subject_name"].ToString() + "</td>";
                           }
                           else
                           {
                               subject_id = subject_id + "<td style = 'border: 1px solid black; font-size:18px;text-align:center;'>" + dt_sub.Rows[i]["subject_id"].ToString() + "</td>";
                               subject_name = subject_name + "<td style = 'border: 1px solid black; font-size:18px;text-align:center;'>" + dt_sub.Rows[i]["subject_name"].ToString() + "</td>";
                           }
                       }
                     }

                    if (dt_sub.Rows[i]["exam_type"].ToString() == "0") //for theory
                    {
                        exam_type = exam_type+"<td style = 'border: 1px solid black; font-size:18px;text-align:center;'>Theory</td>";
                        outof = outof + "<td style = 'border: 1px solid black; font-size:18px;text-align:center;'>" + dt_sub.Rows[i]["out_of"].ToString()+"</td>";
                        passing = passing + "<td style = 'border: 1px solid black; font-size:18px;text-align:center;'>" + dt_sub.Rows[i]["passing"].ToString()+"</td>";

                        h_total += Convert.ToInt32(dt_sub.Rows[i]["out_of"].ToString());
                        h_totp +=  Convert.ToInt32(dt_sub.Rows[i]["passing"].ToString());
                        if (i < dt_sub.Rows.Count)
                        {
                            if (ddlstandard.SelectedItem.Text.ToString()=="9")
                             {
                                
                                    exam_type = exam_type + "<td style = 'border: 1px solid black; font-size:18px;text-align:center;'>Total</td>";
                                    exam_type = exam_type + "<td style = 'border: 1px solid black; font-size:18px;text-align:center;'>Grade</td>";

                                    outof = outof + "<td style = 'border: 1px solid black; font-size:18px;text-align:center;'>" + h_total + "</td><td style = 'border: 1px solid black; font-size:18px'></td>";
                                    passing = passing + "<td style = 'border: 1px solid black; font-size:18px;text-align:center;'>" + h_totp + "</td><td style = 'border: 1px solid black; font-size:18px'></td>";
                                
                              }
                            else if (dt_sub.Rows[i]["subject_id"].ToString() != (dt_sub.Rows[i + 1]["subject_id"].ToString()))
                            {

                                exam_type = exam_type + "<td style = 'border: 1px solid black; font-size:18px;text-align:center;'>Total</td>";
                                exam_type = exam_type + "<td style = 'border: 1px solid black; font-size:18px;text-align:center;'>Grade</td>";

                                outof = outof + "<td style = 'border: 1px solid black; font-size:18px;text-align:center;'>" + h_total + "</td><td style = 'border: 1px solid black; font-size:18px'></td>";
                                passing = passing + "<td style = 'border: 1px solid black; font-size:18px;text-align:center;'>" + h_totp + "</td><td style = 'border: 1px solid black; font-size:18px'></td>";
                            }
                        }
                        
                        
                    }
                    else if (dt_sub.Rows[i]["exam_type"].ToString() == "1")//for internal
                    {
                        exam_type = exam_type + "<td style = 'border: 1px solid black; font-size:18px;text-align:center;'>Internal</td>";
                        outof = outof + "<td style = 'border: 1px solid black; font-size:18px;text-align:center;'>" + dt_sub.Rows[i]["out_of"].ToString() + "</td>";
                        passing = passing + "<td style = 'border: 1px solid black; font-size:18px;text-align:center;'>" + dt_sub.Rows[i]["passing"].ToString() + "</td>";

                        h_total += Convert.ToInt32(dt_sub.Rows[i]["out_of"].ToString());
                        h_totp += Convert.ToInt32(dt_sub.Rows[i]["passing"].ToString());

                        if (dt_sub.Rows[i]["subject_id"].ToString() != (dt_sub.Rows[i + 1]["subject_id"].ToString()))
                        {
                            exam_type = exam_type + "<td style = 'border: 1px solid black; font-size:18px;text-align:center;'>Total</td>";
                            exam_type = exam_type + "<td style = 'border: 1px solid black; font-size:18px;text-align:center;'>Grade</td>";

                            outof = outof + "<td style = 'border: 1px solid black; font-size:18px;text-align:center;'>" + h_total + "</td><td style = 'border: 1px solid black; font-size:18px'></td>";
                            passing = passing + "<td style = 'border: 1px solid black; font-size:18px;text-align:center;'>" + h_totp + "</td><td style = 'border: 1px solid black; font-size:18px'></td>";
                        }
                    }
                    else if (dt_sub.Rows[i]["exam_type"].ToString() == "2")//for practical
                    {
                        exam_type =exam_type+ "<td style = 'border: 1px solid black; font-size:18px;text-align:center;'>Practical</td>";
                        outof = outof + "<td style = 'border: 1px solid black; font-size:18px;text-align:center;'>" + dt_sub.Rows[i]["out_of"].ToString() + "</td>";
                        passing = passing + "<td style = 'border: 1px solid black; font-size:18px;text-align:center;'>" + dt_sub.Rows[i]["passing"].ToString() + "</td>";
                        h_total += Convert.ToInt32(dt_sub.Rows[i]["out_of"].ToString());
                        h_totp += Convert.ToInt32(dt_sub.Rows[i]["passing"].ToString());
                        if (dt_sub.Rows[i]["subject_id"].ToString() != (dt_sub.Rows[i + 1]["subject_id"].ToString()))
                        {
                            exam_type = exam_type + "<td style = 'border: 1px solid black; font-size:18px;text-align:center;'>Total</td>";
                            exam_type = exam_type + "<td style = 'border: 1px solid black; font-size:18px;text-align:center;'>Grade</td>";

                            outof = outof + "<td style = 'border: 1px solid black; font-size:18px;text-align:center;'>" + h_total + "</td><td style = 'border: 1px solid black; font-size:18px'></td>";
                            passing = passing + "<td style = 'border: 1px solid black; font-size:18px;text-align:center;'>" + h_totp + "</td><td style = 'border: 1px solid black; font-size:18px'></td>";
                        }
                    }
                    else if (dt_sub.Rows[i]["exam_type"].ToString() == "3")//for grade 
                    {
                        exam_type = exam_type+"<td style = 'border: 1px solid black; font-size:18px;text-align:center;'>Grade</td>";
                    }

                }
                grand_total = grand_total + h_total;
                sb.Append(subject_id);
                sb.Append("<td rowspan='5' style = 'border: 1px solid black; font-size:18px;vertical-align:middle;text-align:center;' valign='center'>Grand Total ("+grand_total+")</td>");
                sb.Append("<td rowspan='5' style = 'border: 1px solid black; font-size:18px;vertical-align:middle;text-align:center;' valign='center'>Percentage %</td>");
                sb.Append("<td rowspan='5' style = 'border: 1px solid black; font-size:18px;vertical-align:middle;text-align:center;' valign='center'>Grade</td> </tr>");

                sb.Append("<td colspan = '2' style = 'border: 1px solid black; font-size:18px;text-align:center;'>Subject Name</td>");
                sb.Append(subject_name + "</tr>");

                sb.Append("<tr style='height:40px'>");
                sb.Append("<td valign='center' rowspan = '3' style = 'border: 1px solid black; font-size:18px;vertical-align:middle; text-align:center'>Roll No</td>");
                sb.Append("<td valign = 'center' rowspan = '3' style = 'border: 1px solid black ;font-size:18px;vertical-align:middle; text-align:center'>Name</td>");
                sb.Append(exam_type+"</tr>");
                sb.Append("<tr style='height:40px'>");
                sb.Append(outof + "</tr>");
                sb.Append("<tr style='height:40px'>");
                sb.Append(passing + "</tr>");
            //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                for (int j = 0; j < dt_stud.Rows.Count; j = j + dt_sub.Rows.Count)
                {
                    string marks = "";
                    int sub_tot = 0;
                    int sub_out_of = 0;
                    int gnd_tot = 0;
                    int gnd_out = 0;

                    for (int k = j; k < j + dt_sub.Rows.Count; k++)
                    {
                        string grade = "";
                        if (k == j)
                        {
                            sb.Append("<tr style='height:40px'>");
                            sb.Append("<td style = 'border: 1px solid black; font-size:18px;text-align:center;'>");
                            sb.Append(dt_stud.Rows[k]["Roll_no"].ToString());
                            sb.Append("</td>");

                            sb.Append("<td style = 'border: 1px solid black; font-size:18px; text-align:center'>");
                            sb.Append(dt_stud.Rows[k]["Student_name"].ToString());
                            sb.Append("</td>");
                        }


                        if (dt_stud.Rows[k]["exam_type"].ToString() == "0") //for theory
                        {
                            marks = marks + "<td style = 'border: 1px solid black; font-size:18px;text-align:center;'>" + dt_stud.Rows[k]["marks"].ToString() + "</td>";
                            sub_tot = sub_tot + Convert.ToInt32(dt_stud.Rows[k]["marks"].ToString());
                            sub_out_of = sub_out_of + Convert.ToInt32(dt_stud.Rows[k]["out_of"].ToString());
                            if (ddlstandard.SelectedItem.Text.ToString() == "9")
                            {
                                
                                    gnd_tot = gnd_tot + sub_tot;
                                    gnd_out = gnd_out + sub_out_of;

                                    grade = grade_generator(sub_tot, sub_out_of);
                                    marks = marks + "<td style = 'border: 1px solid black; font-size:18px;text-align:center;'>" + sub_tot + "</td><td style = 'border: 1px solid black; font-size:18px;text-align:center;'>" + grade + "</td>";
                                    sub_tot = 0;
                                    sub_out_of = 0;
                               
                            }
                            else if (dt_stud.Rows[k]["subject_id"].ToString() != (dt_stud.Rows[k + 1]["subject_id"].ToString()))
                            {
                                gnd_tot = gnd_tot + sub_tot;
                                gnd_out = gnd_out + sub_out_of;

                                grade = grade_generator(sub_tot, sub_out_of);
                                marks = marks + "<td style = 'border: 1px solid black; font-size:18px;text-align:center;'>" + sub_tot + "</td><td style = 'border: 1px solid black; font-size:18px;text-align:center;'>" + grade + "</td>";
                                sub_tot = 0;
                                sub_out_of = 0;
                            }
                           
                        }
                        else if (dt_stud.Rows[k]["exam_type"].ToString() == "1")//for internal
                        {
                            

                            marks = marks + "<td style = 'border: 1px solid black; font-size:18px;text-align:center;'>" + dt_stud.Rows[k]["marks"].ToString() + "</td>";
                            sub_tot = sub_tot + Convert.ToInt32(dt_stud.Rows[k]["marks"].ToString());
                            sub_out_of = sub_out_of + Convert.ToInt32(dt_stud.Rows[k]["out_of"].ToString());
                          
                            if (dt_stud.Rows[k]["subject_id"].ToString() != (dt_stud.Rows[k + 1]["subject_id"].ToString()))
                            {
                                gnd_tot = gnd_tot + sub_tot;
                                gnd_out = gnd_out + sub_out_of;

                                grade = grade_generator(sub_tot, sub_out_of);
                                marks = marks + "<td style = 'border: 1px solid black; font-size:18px;text-align:center;'>" + sub_tot + "</td><td style = 'border: 1px solid black; font-size:18px;text-align:center;'>" + grade + "</td>";
                                sub_tot = 0;
                                sub_out_of = 0;
                            }
                        }
                        else if (dt_stud.Rows[k]["exam_type"].ToString() == "2")//for practical
                        {
                           

                            marks = marks + "<td style = 'border: 1px solid black; font-size:18px;text-align:center;'>" + dt_stud.Rows[k]["marks"].ToString() + "</td>";
                            sub_tot = sub_tot + Convert.ToInt32(dt_stud.Rows[k]["marks"].ToString());
                            sub_out_of = sub_out_of + Convert.ToInt32(dt_stud.Rows[k]["out_of"].ToString());
                          
                            if (dt_stud.Rows[k]["subject_id"].ToString() != (dt_stud.Rows[k + 1]["subject_id"].ToString()))
                            {
                                gnd_tot = gnd_tot + sub_tot;
                                gnd_out = gnd_out + sub_out_of;

                                grade = grade_generator(sub_tot, sub_out_of);
                                marks = marks + "<td style = 'border: 1px solid black; font-size:18px;text-align:center;'>" + sub_tot + "</td><td style = 'border: 1px solid black; font-size:18px;text-align:center;'>" + grade + "</td>";
                                sub_tot = 0;
                                sub_out_of = 0;
                            }
                        }
                        else if (dt_stud.Rows[k]["exam_type"].ToString() == "3")//for grade 
                        {
                            marks = marks + "<td style = 'border: 1px solid black; font-size:18px;text-align:center;'>" + dt_stud.Rows[k]["marks"].ToString() + "</td>";

                        }
                    }
                    sb.Append(marks);
                    sb.Append("<td valign='center' style = 'border: 1px solid black; font-size:18px;text-align:center;'>" + gnd_tot + "</td>");
                    sb.Append("<td valign='center' style = 'border: 1px solid black; font-size:18px;text-align:center;'>" + (Math.Round((Convert.ToDouble(gnd_tot * 100)) / Convert.ToDouble(gnd_out), 2)).ToString() + "</td>");
                    sb.Append("<td valign='center' style = 'border: 1px solid black; font-size:18px;text-align:center;'>" + grade_generator(Convert.ToDouble(gnd_tot), Convert.ToDouble(gnd_out)) + "</td>");
                    sb.Append("</tr>");
                }
            //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
         
            sb.Append("</table> ");
            ltable.Text = sb.ToString();
            Session["sb"] = sb.ToString();
            Session["error"] = "0";
            notifys("Successfully generated final gazette.", "#198104");
               
        }

        catch (Exception)
        {
            notifys("Marks Entry is not available for given students.", "#D44950");
            //return;
            Session["error"] = "1";
        }
    }

    //-----------------------------------------------------------------------------

    public void build_html_table_nine(DataTable dt_sub, DataTable dt_stud)
    {
        try
        {
            StringBuilder sb = new StringBuilder();
            //---------------------------------------header--------------------------------------------------------------------------------------------------------
            var distinctIds = dt_sub.AsEnumerable().Select(s => new { id = s.Field<string>("subject_id"), }).Distinct().ToList();
            string no_of_subjects = distinctIds.Count.ToString();
            string no_of_rows = dt_sub.Rows.Count.ToString();
            string no_of_columns = ((Convert.ToInt32(no_of_rows)) +6).ToString();
            string second_col = "";

            float second_fl = (Convert.ToInt32(no_of_columns) / 4);
            int second_in = (int)Math.Floor(second_fl);
            second_col = second_in.ToString();

            sb.Append("<table id='tbl' style = 'border: 1px solid black; width:85%; height:85%;vertical-align:middle;text-align:center' valign='center'>");
            sb.Append("<tr style='height:20px'>");
            sb.Append("<td colspan = " + no_of_columns + " style = 'border: 1px solid black ' align='center'><h4>Late. V.W THAKUR CHARITABLE TRUST'S</h4></td>");
            sb.Append("</tr>");
            sb.Append("<tr style='height:20px'>");
            sb.Append("<td colspan = " + no_of_columns + " style = 'border: 1px solid black ' align='center'><h4>KAI. PANDURANG RAGHUNATH PATIL UTKARSHA MADHYAMIC VIDYALAYA & JR. COLLEGE, VIRAR.</h4></td>");
            sb.Append("</tr>");
            sb.Append("<tr style='height:20px'>");
            sb.Append("<td colspan = " + no_of_columns + " style = 'border: 1px solid black ' align='center'><h4>Academic Year " + Session["year"].ToString() + " Semester:AVERAGE Division:" + ddldivision.SelectedItem.Text.ToString() + "  Standard:" + ddlstandard.SelectedItem.Text.ToString() + "</h4></td>");
            sb.Append("</tr>");
            sb.Append("<tr style='height:30px'>");
            sb.Append("<td colspan = " + no_of_columns + " style = 'border: 1px solid black ' align='center'><h2>Final Gazette</h2></td>");
            sb.Append("</tr>");
            //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            sb.Append("<tr>");
            sb.Append("<td colspan = '2' style = 'border: 1px solid black; font-size:18px;text-align:center;'>Subject Name</td>");
          
            string subject_name = "";
            string outof = "";//subject out of
            int h_total = 0;//subject total out of
            int grand_total = 0;
            for (int i = 0; i < dt_sub.Rows.Count; i++)
            {
                if (i == 0)
                {
                    subject_name = "<td style = 'border: 1px solid black; font-size:18px;text-align:center;'>" + dt_sub.Rows[i]["subject_name"].ToString() + "</td>";
                    h_total += Convert.ToInt32(dt_sub.Rows[i]["out_of"].ToString());
                    outof = "<td style = 'border: 1px solid black; font-size:18px;text-align:center;'>" + dt_sub.Rows[i]["out_of"].ToString() + "</td>";
                }
                else
                {
                    subject_name = subject_name + "<td style = 'border: 1px solid black; font-size:18px;text-align:center;'>" + dt_sub.Rows[i]["subject_name"].ToString() + "</td>";
                    if (dt_sub.Rows[i]["criteria"].ToString() == "Grade")
                    {
                        outof = outof + "<td style = 'border: 1px solid black; font-size:18px;text-align:center;'>Grade</td>";
                    }
                    else
                    {
                        outof = outof + "<td style = 'border: 1px solid black; font-size:18px;text-align:center;'>" + dt_sub.Rows[i]["out_of"].ToString() + "</td>";
                    }
                    h_total += Convert.ToInt32(dt_sub.Rows[i]["out_of"].ToString());
                   
                }
            }

            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            grand_total = grand_total + h_total;
      
            sb.Append(subject_name);
            sb.Append("<td rowspan='2' style = 'border: 1px solid black; font-size:18px;vertical-align:middle;text-align:center;' valign='center'>Grand Total (" + grand_total + ")</td>");
            sb.Append("<td rowspan='2' style = 'border: 1px solid black; font-size:18px;vertical-align:middle;text-align:center;' valign='center'>Percentage %</td>");
            sb.Append("<td rowspan='2' style = 'border: 1px solid black; font-size:18px;vertical-align:middle;text-align:center;' valign='center'>Grade</td>");
            sb.Append("<td rowspan='2' style = 'border: 1px solid black; font-size:18px;vertical-align:middle;text-align:center;' valign='center'>Remark</td> </tr>");
            sb.Append("<tr style='height:30px'>");
            sb.Append("<td valign='center' rowspan = '1' style = 'border: 1px solid black; font-size:18px;vertical-align:middle; text-align:center'>Roll No</td>");
            sb.Append("<td valign = 'center' rowspan = '1' style = 'border: 1px solid black ;font-size:18px;vertical-align:middle; text-align:center'>Name</td>");
            sb.Append(outof + "</tr>");
            
            //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            DataTable filldata = new DataTable();
            filldata.Columns.Add("roll_no");
            filldata.Columns.Add("student_name");
            filldata.Columns.Add("marks");
            filldata.Columns.Add("Total");
            filldata.Columns.Add("Grandtotal");
            filldata.Columns.Add("%");
            filldata.Columns.Add("Grade");
            filldata.Columns.Add("Remark");

            for (int i = 0; i < dt_stud.Rows.Count; i++)
            {
                int count = 0;
                int countm = 0;
                int language = 0;
                int m = 0;
                int total = 0;
                string sub = "";
                int Over = 0;
                int overalltot = 0;
                double percent1 = 0;
                double percentcheck = 0;
                string grade = "";
                string Remark = "";
                double cal = 0;
                double cal1 = 0;
                int check = 0;
                int checkm = 0;
                int ogm = 0;
                int flagremark = 0;
                int flagrace = 0;
                string g = "";
                int grace = 0;
                int passing_new = 0;
                int totalgrace = 0;
                string overr = "";
                string stud_id = dt_stud.Rows[i]["Student_id"].ToString();
                DataTable dtgrace = new DataTable();

                //----------------------------------------------------------------------------
                DataTable dt = new DataTable();
             
                string html = string.Empty;

                string urlalias = cls.urls();
                string url = @urlalias + "MarksEntry/";
                //string url = @"http://localhost:9199//MarksEntry";
                useless();

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
                me.type = "fill_studentdata";
                me.stud_id = stud_id;

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
                }

                dt = dslist.Tables[0];

                dtgrace = dslist.Tables[1];
                
                //-----------------------------------------------------------------------------------------------------------
             
                int mar = 0;
                int lang = 0;
                int maths = 0;
                int p = 0;
                int t = 0;
                int mt = 0;
                int graceLang = 0;
                int graceMaths = 0;
                int mathcheck = 0;
               
                    for (int k = 0; k < dt.Rows.Count; k++)
                    {
                        if (k < 6)
                        {
                            mt = mt + Convert.ToInt32(dt.Rows[k]["Marks"].ToString());
                            mar = Convert.ToInt32(dt.Rows[k]["Marks"].ToString());
                            p = p + Convert.ToInt32(dt.Rows[k]["passing"].ToString());
                            t = t + Convert.ToInt32(dt.Rows[k]["out_of"].ToString());

                            if (k < 3)
                            {
                                if (mar > 24)
                                {
                                    lang = lang + mar;
                                }
                            }
                            if (k <= 4 && k > 2)
                            {
                                if (mar > 24)
                                {
                                    maths = maths + mar;
                                }

                            }
                            if (k == 5)
                            {
                                if ((Convert.ToInt32(dtgrace.Rows[0]["differ"].ToString()) <= 20) && (Convert.ToInt32(dtgrace.Rows[0]["gracing"].ToString()) > 0) && ((Convert.ToInt32(dtgrace.Rows[0]["subcount"].ToString()) == (Convert.ToInt32(dtgrace.Rows[0]["gracing"].ToString()) + Convert.ToInt32(dtgrace.Rows[0]["pass"].ToString())))))
                                {
                                    if (lang <= 105 && lang >= 85)
                                    {
                                        graceLang = 1;
                                    }
                                    if (maths < 71 && maths >= 40)
                                    {
                                        graceMaths = 1;
                                    }
                                }
                            }
                            cal1 = ((Convert.ToDouble(mt)) * 100) / Convert.ToDouble(t);
                            percentcheck = Math.Round(cal1, 2);
                        }
                    }
               
                string  marks = "", oggm = "";
       
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                        sub = dt.Rows[j]["subject_name"].ToString();

                        if (sub.Contains("MARATHI") == true || sub.Contains("HINDI") == true || sub.Contains("ENGLISH") == true || sub.Contains("SANSKRIT") == true || sub.Contains("MATHS") == true || sub.Contains("TEC") == true || sub.Contains("SOC") == true)
                        {
                            ogm = Convert.ToInt32(dt.Rows[j]["Marks"].ToString());
                            m = Convert.ToInt32(dt.Rows[j]["Marks"].ToString());

                            passing_new = Convert.ToInt32(dt.Rows[j]["passing"].ToString());
                            total = Convert.ToInt32(dt.Rows[j]["out_of"].ToString());

                            if (sub.Contains("MARATHI") == true || sub.Contains("HINDI") == true || sub.Contains("ENGLISH") == true || sub.Contains("SANSKRIT") == true)
                            {
                                if ((graceLang == 1) && ((passing_new - m) <= 10) && ((passing_new - m) > 0) && (totalgrace <= 20) && (percentcheck > 34.99) && (Convert.ToInt32(dtgrace.Rows[0]["differ"].ToString()) <= 20))
                                {
                                    grace = (passing_new - m);
                                    m = m + grace;
                                    flagremark = 1;
                                    flagrace = 1;
                                }
                                count++;
                                if (total == 100)
                                {
                                    if (m > 25)
                                    {
                                        language = language + m;

                                    }
                                }
                                else
                                {
                                    if (m > 13)
                                    {
                                        language = language + m;
                                    }
                                }
                                if (language > 105)
                                {
                                    Over = language;

                                }


                            }
                            else if (sub.Contains("MATHS") == true || sub.Contains("TEC") == true)
                            {
                                if ((graceMaths == 1) && ((passing_new - m) <= 10) && ((passing_new - m) > 0) && (totalgrace <= 20) && (percentcheck > 34.99) && (Convert.ToInt32(dtgrace.Rows[0]["differ"].ToString()) <= 20))
                                {
                                    grace = (passing_new - m);
                                    m = m + grace;
                                    flagremark = 1;
                                    flagrace = 1;

                                }
                                countm++;
                                if (m >= 25)
                                {
                                    check = check + m;
                                }

                                if (check >= 70)
                                {
                                    mathcheck = check;
                                    Over = language + mathcheck;
                                }


                            }
                            else if (sub.Contains("SOC") == true)
                            {
                                if (m >= 35)
                                {
                                    Over = Over + m;
                                }
                                if (m < 35 && m >= 25 && ((35 - m) <= (20 - totalgrace)) && (percentcheck > 34.99) && (Convert.ToInt32(dtgrace.Rows[0]["differ"].ToString()) <= 20))
                                {
                                    grace = (passing_new - m);
                                    m = m + grace;
                                    flagremark = 1;
                                    flagrace = 1;
                                    Over = Over + m;
                                }
                            }


                            overalltot = overalltot + total;
                      
                            checkm = checkm + ogm;
                        }
                        else
                        {
                            g = dt.Rows[j]["Marks"].ToString();

                        }

                    
                    if (g == "")
                    {
                        if (flagrace == 1 && flagremark == 1)
                        {
                            totalgrace = totalgrace + grace;
                            oggm = ogm.ToString() + "   +" + grace;
                            flagrace = 0;
                        }
                        else
                        {
                            oggm = ogm.ToString();
                        }

                        marks = marks + oggm.ToString() + ',';
                    }
                    else
                    {
                        marks = marks + g.ToString() + ',';
                    }
                    if (j == dt.Rows.Count - 1)
                    {
                          if (Over == 0 || mathcheck == 0)
                            {
                                Over = checkm;
                            }
                        
                        cal = ((Convert.ToDouble(Over)) * 100) / Convert.ToDouble(overalltot);
                        percent1 = Math.Round(cal, 2);
                        int cchk = 0;

                        if (percent1 >= 35.00 && percent1 <= 50.99)
                        {
                            grade = "C";

                        }
                        else if (percent1 >= 51.00 && percent1 <= 60.99)
                        {
                            grade = "B";

                        }
                        else if (percent1 >= 61.00 && percent1 <= 75.99)
                        {
                            grade = "A";

                        }
                        else if (percent1 >= 76.00)
                        {
                            grade = "A+";

                        }
                        if (percent1 <= 35.00)
                        {

                            grade = "D";
                            Remark = "Re-Exam";
                            overr = checkm.ToString();
                        }
                        else if (((Convert.ToInt32(dtgrace.Rows[0]["subcount"].ToString()) != (Convert.ToInt32(dtgrace.Rows[0]["gracing"].ToString()) + Convert.ToInt32(dtgrace.Rows[0]["pass"].ToString())))) && (graceLang == 0) && (graceMaths == 0))
                        {
                            Remark = "Re-Exam";
                            overr = checkm.ToString();
                        }
                        else if (flagremark == 1)
                        {
                            Remark = "PROMOTED";
                            overr = checkm.ToString() + "  +" + totalgrace;
                        }
                        else
                        {
                            overr = checkm.ToString();
                            Remark = "PASS";
                        }
                    }
                }
                filldata.Rows.Add();
                filldata.Rows[i]["roll_no"] = dt_stud.Rows[i]["roll_no"].ToString();
                filldata.Rows[i]["student_name"] = dt_stud.Rows[i]["student_name"].ToString();
                filldata.Rows[i]["marks"] = marks.TrimEnd(',');
                filldata.Rows[i]["Grandtotal"] = overalltot;
                filldata.Rows[i]["total"] = overr;
                filldata.Rows[i]["%"] = percent1;
                filldata.Rows[i]["grade"] = grade;
                filldata.Rows[i]["Remark"] = Remark;
            }
            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            int c = 8;
            for (int j = 0; j < filldata.Rows.Count;j++)
            {
                sb.Append("<tr style='height:40px'>");
                sb.Append("<td style = 'border: 1px solid black; font-size:18px;text-align:center;'>");
                sb.Append(filldata.Rows[j]["roll_no"].ToString());
                sb.Append("</td>");

                sb.Append("<td style = 'border: 1px solid black; font-size:18px; text-align:center'>");
                sb.Append(filldata.Rows[j]["student_name"].ToString());
                sb.Append("</td>");
                string temp = "";
               
                 string marks = filldata.Rows[j]["marks"].ToString();
                 string[] strarr = marks.Split(',');
                            int chk = 8;
                      
                            for (int l = 0; l < strarr.Length; l++)
                            {
                                temp = temp + "<td style = 'border: 1px solid black; font-size:18px; text-align:center'>";
                                if (strarr[l].Contains("+"))
                                {
                                    string strring = strarr[l];
                                 
                                    string[] arr = strring.Split('+');

                                    string sup = "";
                                    
                                    sup = " <span style='color: rgb(255,0,0);'><sup>+" + arr[1] + "</sup></span>";
                                   
                                    temp = temp + arr[0]+sup;
                                }
                                else
                                {
                                   
                                    temp = temp + strarr[l];
                                }
                                chk++;
                                temp=temp+"</td>";
                            }
                           

                            string at = filldata.Rows[j]["Total"].ToString();
                            temp = temp + "<td style = 'border: 1px solid black; font-size:18px; text-align:center'>";
                            if (at.Contains("+"))
                            {
                                int startidx, lastidx, length;
                                startidx = at.IndexOf("+");
                                lastidx = at.Length;
                                string[] arr = at.Split('+');
                                length = lastidx - startidx;
                                string sup = "";

                                sup = " <span style='color: rgb(255,0,0);'><sup>+" + arr[1] + "</sup></span>";
                                temp = temp + arr[0] + sup + "</td>"; ;
                             
                            }
                            else
                            {
                                temp = temp  + at + "</td>";
                            }
                            temp = temp + "<td style = 'border: 1px solid black; font-size:18px; text-align:center'>" + filldata.Rows[j]["%"].ToString() + "</td>";
                            temp = temp + "<td style = 'border: 1px solid black; font-size:18px; text-align:center'>" + filldata.Rows[j]["Grade"].ToString() + "</td>";
                            temp = temp + "<td style = 'border: 1px solid black; font-size:18px; text-align:center'>" + filldata.Rows[j]["Remark"].ToString() + "</td>";
                            sb.Append(temp);
                            sb.Append("</tr>");
            }
            sb.Append("</table> ");
            //---------------------------------------------------------------------------------------------------------------
            sb.Append("<table>");
         
            sb.Append("<tr style='height:40px'>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td style = ' font-size:16px ;text-align:center; vertical-align:middle;width:20px;' align='center'></td>");
            sb.Append("<td style = ' font-size:16px ;text-align:left; vertical-align:middle;width:20px;' align='center'><b>Total Student -</b>" + filldata.Rows.Count + "</td>");
            sb.Append("</tr>");

            sb.Append("<tr>");
            sb.Append("<td style = ' font-size:16px ;text-align:center; vertical-align:middle;width:20px;' align='center'></td>");
            sb.Append("<td style = ' font-size:16px ;text-align:left; vertical-align:middle;width:20px;' align='center'><b>Present Student -</b>" + filldata.Rows.Count + "</td>");
            sb.Append("</tr>");

            sb.Append("<tr>");
            sb.Append("<td style = ' font-size:16px ;text-align:center; vertical-align:middle;width:20px;' align='center'></td>");
            sb.Append("<td style = ' font-size:16px ;text-align:left; vertical-align:middle;' align='center'><b>Absent Student -</b></td>");
            sb.Append("</tr>");
            sb.Append("<tr style='height:40px'>");
            sb.Append("</tr>");
            sb.Append("</table>");
            //-------------------------------------------------------------------------------------------------
            int passt = 0; int promot = 0; int reexamt = 0;
            int passg = 0; int promog = 0; int reexamg = 0;
            int passb = 0; int promob = 0; int reexamb = 0;
            int totall = 0;
            for (int y = 0; y < filldata.Rows.Count; y++)
            {
                if (filldata.Rows[y]["student_name"].ToString().StartsWith("Miss"))
                {
                    if (filldata.Rows[y]["Remark"].ToString() == "PASS")
                    {
                        passg = passg + 1;
                    }
                    else if (filldata.Rows[y]["Remark"].ToString() == "PROMOTED")
                    {
                        promog = promog + 1;
                    }
                    else
                    {
                        reexamg = reexamg + 1;
                    }

                }
                else
                {
                    if (filldata.Rows[y]["Remark"].ToString() == "PASS")
                    {
                        passb = passb + 1;
                    }
                    else if (filldata.Rows[y]["Remark"].ToString() == "PROMOTED")
                    {
                        promob = promob + 1;
                    }
                    else
                    {
                        reexamb = reexamb + 1;
                    }

                }

            }
            passt = passg + passb;
            promot = promog + promob;
            reexamt = reexamg + reexamb;
            totall = passt + promot + reexamt;
           //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            sb.Append("<table id='tbl' style = 'border: 1px solid black; width:85%; height:85%;vertical-align:middle;text-align:center' valign='center'>");
            sb.Append("<tr>");
         
            sb.Append("<td style = ' font-size:16px ;text-align:center; vertical-align:middle;width:20px;' align='center'></td>");
          
            sb.Append("<td style = 'border: 1px solid black; font-size:18px; text-align:center'><b>Boys</b></td>");
            sb.Append("<td style = 'border: 1px solid black; font-size:18px; text-align:center'><b>Girls</b></td>");
            sb.Append("<td style = 'border: 1px solid black; font-size:18px; text-align:center'><b>Total</b></td>");
            sb.Append("</tr>");

            sb.Append("<tr>");
           
            sb.Append("<td style = 'border: 1px solid black; font-size:18px; text-align:left'><b>Pass Student -</b></td>");
            sb.Append("<td style = 'border: 1px solid black; font-size:18px; text-align:left'><b>"+passb+"</b></td>");
            sb.Append("<td style = 'border: 1px solid black; font-size:18px; text-align:left'><b>"+passg+"</b></td>");
            sb.Append("<td style = 'border: 1px solid black; font-size:18px; text-align:left'><b>" + passt + "</b></td>");
            sb.Append("</tr>");

            sb.Append("<tr>");
           
            sb.Append("<td style = 'border: 1px solid black; font-size:18px; text-align:left'><b>Promoted Student -</b></td>");
            sb.Append("<td style = 'border: 1px solid black; font-size:18px; text-align:left'><b>"+promob+"</b></td>");
            sb.Append("<td style = 'border: 1px solid black; font-size:18px; text-align:left'><b>"+promog+"</b></td>");
            sb.Append("<td style = 'border: 1px solid black; font-size:18px; text-align:left'><b>" + promot + "</b></td>");
            sb.Append("</tr>");

            sb.Append("<tr>");
            
            sb.Append("<td style = 'border: 1px solid black; font-size:18px; text-align:left'><b>Re-Exam  Student -</b></td>");
            sb.Append("<td style = 'border: 1px solid black; font-size:18px; text-align:left'><b>"+reexamb+"</b></td>");
            sb.Append("<td style = 'border: 1px solid black; font-size:18px; text-align:left'><b>"+reexamg+"</b></td>");
            sb.Append("<td style = 'border: 1px solid black; font-size:18px; text-align:left'><b>" + reexamt + "</b></td>");
            sb.Append("</tr>");

            sb.Append("<tr>");
          
            sb.Append("<td style = ' font-size:16px ;text-align:center; vertical-align:middle;width:20px;' align='center'></td>");
            sb.Append("<td style = ' font-size:16px ;text-align:center; vertical-align:middle;width:20px;' align='center'></td>");
            sb.Append("<td style = ' font-size:16px ;text-align:center; vertical-align:middle;width:20px;' align='center'></td>");
            sb.Append("<td style = 'border: 1px solid black; font-size:18px; text-align:left'><b>" + totall + "</b></td>");
            sb.Append("</tr>");

            sb.Append("</table>");
            //--------------------------------------------------------------------------------------------------------------------------------------------
            
            //final row
            sb.Append("</table>");
            sb.Append("<table>");
            sb.Append("<tr style='height:40px'>");
            sb.Append("</tr>");
            sb.Append("<tr style='height:40px'>");
            sb.Append("</tr>");
            sb.Append("<tr style='height:40px'>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td style = ' font-size:16px ;text-align:center; vertical-align:middle;width:20px;' align='center'></td>");
            sb.Append("<td style = ' font-size:16px ;text-align:center; vertical-align:middle;width:20px;' align='center'><b>Class Teacher</b></td>");
            sb.Append("<td style = ' font-size:16px ;text-align:center; vertical-align:middle;width:20px;' align='center'></td>");
            sb.Append("<td style = ' font-size:16px ;text-align:center; vertical-align:middle;width:20px;' align='center'></td>");
            sb.Append("<td style = ' font-size:16px ;text-align:center; vertical-align:middle;width:20px;' align='center'><b>Checked By</b></td>");
            sb.Append("<td style = ' font-size:16px ;text-align:center; vertical-align:middle;width:20px;' align='center'></td>");
            sb.Append("<td style = ' font-size:16px ;text-align:center;vertical-align:middle; ' align='center'></td>");
            sb.Append("<td style = ' font-size:16px ;text-align:center; vertical-align:middle;' align='center'><b>Principal</b></td>");
            sb.Append("</tr>");
            sb.Append("</table>");
            //-----------------------------------------------------------------------------------------------------------------------------------------------
            ltable.Text = sb.ToString();
            Session["sb"] = sb.ToString();
            Session["error"] = "0";
            notifys("Successfully generated final gazette.", "#198104");

        }

        catch (Exception)
        {
            notifys("Marks Entry is not available for given students.", "#D44950");
            //return;
            Session["error"] = "1";
        }
    }

    //---------------------------------------------------------------------------------

    public string grade_generator(double marks_obtained, double out_of)
    {
        string grade = "";
        if ( !double.IsNaN(marks_obtained) && !double.IsNaN(out_of))
        {
            double calculate = (marks_obtained * 100) / out_of;
            int percent = (int)Math.Round(calculate);
           
            if (percent < 20)
                {
                    grade= "E2";

                }
                else if (percent >= 21 && percent < 32.99)
                {
                    grade = "E1";

                }
                else if (percent >= 33 && percent < 40.99)
                {
                    grade = "D";

                }
                else if (percent >= 41 && percent < 50.99)
                {
                    grade = "C2";

                }
                else if (percent >= 51 && percent < 60.99)
                {
                    grade = "C1";

                }
                else if (percent >= 61 && percent < 70.99)
                {
                    grade = "B2";

                }
                else if (percent >= 71 && percent < 80.99)
                {
                    grade= "B1";

                }
                else if (percent >= 81 && percent < 90.99)
                {
                    grade = "A2";

                }
                else if (percent >= 91 && percent < 100)
                {
                    grade = "A1";

                }
        }
        return grade;
    }

    public void notifys(string msg, string color)
    {
        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert12", "$.notify('" + msg + "', { color: '#fff', background: '" + color + "', blur: 0.2, delay: 0 });", true);
    }

    protected void btn_excel_Click(object sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        if (Session["sb"] != null)
        {
            sb.Append(Session["sb"].ToString());
            Session["sb"] = null;
        }

        else
        {
            notifys("No data available for given selection.", "#D44950");
            return;
        }
        notifys("Successfully generated final gazette.", "#198104");
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("content-disposition", "attachment;filename=FinalGazette.xls");
        Response.Charset = "";
        StringWriter sw = new StringWriter();
        
        
        sw.Write(sb);
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        Response.Write(sw.ToString());
        Response.End();
        
    }

}