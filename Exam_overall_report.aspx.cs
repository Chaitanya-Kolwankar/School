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


public partial class Exam_overall_report : System.Web.UI.Page
{
    Class1 cls = new Class1();
    exam_reports er = new exam_reports();

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
            Response.Redirect("Login.aspx");

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
        er.exam_id = null;
        er.exam_name = null;
        er.medium_id = null;
        er.class_id = null;
        er.type = null;
        er.subject_id = null;
    }

    //fill the medium
    public void ddlfill()
    {
        string html = string.Empty;

        string urlalias = cls.urls();
        string url = @urlalias + "ExamReport/";
     
        useless();
        er.type = "ddlfill";

        string jsonString = JsonHelper.JsonSerializer<exam_reports>(er);
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

                    lst_standard.DataSource = table;
                    lst_standard.DataTextField = "std_name";
                    lst_standard.DataValueField = "std_id";
                    lst_standard.DataBind();
                    lst_standard.Enabled = true;
                   
                }
            }
        }
        else
        {
            ddlstandard.SelectedIndex = 0;
            ddlstandard.Enabled = false;

            ddlexam.SelectedIndex = 0;
            ddlexam.Enabled = false;

        }

    }

    protected void ddlstandard_SelectedIndexChanged(object sender, EventArgs e)
    {
        

        if (ddlstandard.SelectedIndex > 0)
        {
            ddlexam.Enabled = true;
            fillexam(ddlmedium.SelectedValue.ToString(), ddlstandard.SelectedValue.ToString());
           
        }
      
        else
        {
            ddlexam.SelectedIndex = 0;
            ddlexam.Enabled = false;
        }
    }

    public void fillexam(string med_id, string class_id)
    {
        string html = string.Empty;

        string urlalias = cls.urls();
        string url = @urlalias + "ExamReport/";
        useless();
        er.type = "fillexam";
        er.medium_id = med_id;
        er.class_id = class_id;

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
            er.ayid = Session["acdyear"].ToString();
        }

        string jsonString = JsonHelper.JsonSerializer<exam_reports>(er);
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
            if (ddlstandard.SelectedItem.Text.ToString() == "9")
            {
                ddlexam.Items.Insert(1, "AVERAGE");
            }
        }
    }

    protected void rd_report1_CheckedChanged(object sender, EventArgs e)//Class wise grade Report
    {
        lbl_ddlmedium.Visible = true;
        ddlmedium.Visible = true;

        lbl_ddlstandard.Visible = true;
        ddlstandard.Visible = true;

      
        lst_standard.Visible = false;

        lbl_ddlexam.Visible = true;
        ddlexam.Visible = true;
        ddl_exam_new.Visible = false;
         
        if (ddlmedium.SelectedIndex > 0)
        {
    
            ddlmedium.SelectedIndex = 0;
        }

        if (ddlexam.SelectedIndex > 0)
        {
            ddlexam.SelectedIndex = 0;
        }
   
         if (ddlstandard.SelectedIndex > 0)
        {
            ddlstandard.SelectedIndex = 0;
        }
      
    }

    protected void rd_report2_CheckedChanged(object sender, EventArgs e)//Overall Class Wise Report
    {
        lbl_ddlmedium.Visible = true;
        ddlmedium.Visible = true;

      
        lst_standard.Visible = true;


        lbl_ddlstandard.Visible = true;
        ddlstandard.Visible = false;

        lbl_ddlexam.Visible = true;
        ddlexam.Visible = false;
        ddl_exam_new.Visible = true;

        ddl_exam_new.Enabled = true;

        if (ddlmedium.SelectedIndex > 0)
        {
            ddlmedium.SelectedIndex = 0;
        }

        if (ddlexam.SelectedIndex > 0)
        {
            ddlexam.SelectedIndex = 0;
        }

        if (ddl_exam_new.SelectedIndex > 0)
        {
            ddl_exam_new.SelectedIndex = 0;
        }
     
        foreach (ListItem item in lst_standard.Items)
        {
            if (item.Selected == true)
            {
                item.Selected = false;
            }
        }
    
    }

    protected void rd_report3_CheckedChanged(object sender, EventArgs e)//Class wise with All subject Grade
    {
        lbl_ddlmedium.Visible = true;
        ddlmedium.Visible = true;

        lst_standard.Visible = true;

        lbl_ddlstandard.Visible = true;
        ddlstandard.Visible = false;

        lbl_ddlexam.Visible = true;
        ddlexam.Visible = false;
        ddl_exam_new.Visible = true;

        ddl_exam_new.Enabled = true;

        foreach (ListItem item in lst_standard.Items)
        {
            if (item.Selected == true)
            {
                item.Selected = false;
            }
        }
        if (ddlmedium.SelectedIndex > 0)
        {
            ddlmedium.SelectedIndex = 0;
        }

        if (ddlexam.SelectedIndex > 0)
        {
            ddlexam.SelectedIndex = 0;
        }

        if (ddl_exam_new.SelectedIndex > 0)
        {
            ddl_exam_new.SelectedIndex = 0;
        }

    }

    public void notifys(string msg, string color)
    {
        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert12", "$.notify('" + msg + "', { color: '#fff', background: '" + color + "', blur: 0.2, delay: 0 });", true);
    }

    public void clear()
    {

        if (ddlmedium.SelectedIndex > 0)
        {
            ddlmedium.SelectedIndex = 0;
        }

        if (ddlexam.SelectedIndex > 0)
        {
            ddlexam.SelectedIndex = 0;
        }

        if (ddlstandard.SelectedIndex > 0)
        {
            ddlstandard.SelectedIndex = 0;
        }
   
        ddlstandard.Enabled = false;
        ddlexam.Enabled = false;
        foreach (ListItem item in lst_standard.Items)
        {
            if (item.Selected == true)
            {
                item.Selected = false;
            }
        }
    
     
      
    }

    protected void ddlexam_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rd_report1.Checked == true)
        {

        }

        if (Session["sb"] != null)
        {
            btnexcel.Visible = true;
        }
        else
        {
            notifys("Excel Not generated.", "#D44950");
            return;
        }
    }

    public void build_html_table_Class_wise_with_All_subject_and_Grade()
    {
        try
        {
            StringBuilder sb = new StringBuilder();
            //---------------------------------------header--------------------------------------------------------------------------------------------------------
            sb.Append("<table id='tbl' style = 'text-align:center' valign='center'>");
            sb.Append("<tr>");
            sb.Append("<td colspan ='20' align='center'><b>LATE SHRI V.W.Thakur Charitable Trust's</b></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td colspan ='20'  align='center'><b>Kai.P.R. Patil Utkarsha Madhyamik Vidyalaya & Jr.College,Virar</b></td>");
            sb.Append("</tr>");
         
            if (ddl_exam_new.SelectedItem.Text == "Term I")
            {
                sb.Append("<tr>");
                sb.Append("<td colspan ='20' align='center'><b>I  Semester Continuous and Comprehensive Evaluation</b></td>");
                sb.Append("</tr>");
            }
            else if (ddl_exam_new.SelectedItem.Text == "Term II")
            {
                sb.Append("<tr>");
                sb.Append("<td colspan ='20' align='center'><b>II  Semester  Continuous and Comprehensive Evaluation</b></td>");
                sb.Append("</tr>");
            }
            String str = Session["year"].ToString();
            String[] separator = { " - " };
            Int32 count = 2;
            String[] strlist = str.Split(separator, count, StringSplitOptions.RemoveEmptyEntries);
            sb.Append("<tr>");
            sb.Append("<td colspan ='20' align='center'><b>" + strlist[0].Substring(strlist[0].Length - 4) + " - " + strlist[1].Substring(strlist[1].Length - 4) + "</b></td>");
            sb.Append("</tr>");

            sb.Append("<tr>");
            sb.Append("<td></td>");
            sb.Append("<td  colspan ='5' align='left'>School-Kai.P.R.Patil Utkarsha Madhyamik Vidyalaya,Virar.</td>");
            sb.Append("<td></td>");
            sb.Append("<td></td>");
            sb.Append("<td></td>");
            sb.Append("<td></td>");
            sb.Append("<td></td>");
            sb.Append("<td align='center'>Cluster-Virar</td>");
            sb.Append("<td></td>");
            sb.Append("<td></td>");
            sb.Append("<td align='center'>Taluka-Vasai</td>");
            sb.Append("<td></td>");
            sb.Append("<td></td>");
            sb.Append("<td align='center'>District-Palghar</td>");
            sb.Append("</tr>");

            sb.Append("<tr>");
            sb.Append("<td></td>");
            sb.Append("<td colspan ='3' align='left'>U Dise No:-27361711007</td>");
            sb.Append("</tr>");
         
            //------------------------------------------------------------------------------------------
            string class_id = "";
            string std = "";
            foreach (ListItem item in lst_standard.Items)
            {
                if (item.Selected)
                {
                    class_id += item.Value.ToString() + ",";
                }
            }
            foreach (ListItem item in lst_standard.Items)
            {
                if (item.Selected)
                {
                    std += item.Text.ToString() + ",";
                }
            }
            if (class_id != "")
            {
                class_id = class_id.Remove(class_id.Length - 1);
            }
            if (std != "")
            {
                std = std.Remove(std.Length - 1);
            }
            string[] arry = class_id.Split(',');
            string[] std_name = std.Split(',');

            for (int m = 0; m < arry.Length; m++)
            {
                DataTable dt_grade = new DataTable();
                dt_grade.Clear();
                dt_grade.Columns.Add("Grade");

                DataRow one = dt_grade.NewRow();
                one["Grade"] = "A-1";
                dt_grade.Rows.Add(one);

                DataRow two = dt_grade.NewRow();
                two["Grade"] = "A-2";
                dt_grade.Rows.Add(two);

                DataRow three = dt_grade.NewRow();
                three["Grade"] = "B-1";
                dt_grade.Rows.Add(three);

                DataRow four = dt_grade.NewRow();
                four["Grade"] = "B-2";
                dt_grade.Rows.Add(four);

                DataRow five = dt_grade.NewRow();
                five["Grade"] = "C-1";
                dt_grade.Rows.Add(five);

                DataRow six = dt_grade.NewRow();
                six["Grade"] = "C-2";
                dt_grade.Rows.Add(six);

                DataRow seven = dt_grade.NewRow();
                seven["Grade"] = "D";
                dt_grade.Rows.Add(seven);

                DataRow eight = dt_grade.NewRow();
                eight["Grade"] = "E-1";
                dt_grade.Rows.Add(eight);

                DataRow nine = dt_grade.NewRow();
                nine["Grade"] = "E-2";
                dt_grade.Rows.Add(nine); 

                sb.Append("<br><br><br><br>");
                sb.Append("<tr style='height:40px'>");
                sb.Append("</tr>");
             
                sb.Append("<tr>");
                sb.Append("<td></td>");
                sb.Append("<td align='left'>Std:" + std_name[m] + "</td>");
                sb.Append("</tr>");

                //-----------------------------------------
                DataTable dtstud = new DataTable();
                DataTable dtsub = new DataTable();
                string html = string.Empty;

                string urlalias = cls.urls();
                string url = @urlalias + "ExamReport/";
             
                useless();

                if (Session["acdyear"] == null)
                {
                    notifys("Please first select academic year.", "#D44950");
                    return;
                }

                er.type = "filldata";
                er.medium_id = ddlmedium.SelectedValue.ToString();
                er.class_id = arry[m].ToString();
                er.ayid = Session["acdyear"].ToString();
                er.exam_name=ddl_exam_new.SelectedItem.Text.ToString();

                string jsonString = JsonHelper.JsonSerializer<exam_reports>(er);
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
                dtstud = dslist.Tables["Table1"];
                dtsub = dslist.Tables["Table2"];
                if (dtstud.Rows.Count == 0)
                {
                    sb.Append("</tr>");
                    sb.Append("<td align='center' colspan='3'> Marks Entry Not Found................... </td>");
                    sb.Append("</tr>");
                }
                if (dtsub.Rows.Count== 0)
                {
                    sb.Append("</tr>");
                    sb.Append("<td align='center' colspan='3'> Subject Not Defined..................</td>");
                    sb.Append("</tr>");
                }
                if(dtstud.Rows.Count>0 && dtsub.Rows.Count>0)
                {
                    string subject_name = "";
                    string student_type = "";
                    for (int n = 0; n < dtsub.Rows.Count; n++)
                    {
                      subject_name=subject_name+"<td colspan='3' style = 'border: 1px solid black; font-size:14px;text-align:center;'>" + dtsub.Rows[n]["subject_name"].ToString() + "</td>";
                      student_type = student_type + "<td style = 'border: 1px solid black; font-size:14px;text-align:center;'>Boys</td><td style = 'border: 1px solid black; font-size:14px;text-align:center;'>Girls</td><td style = 'border: 1px solid black; font-size:14px;text-align:center;'>Total</td>";

                    }//subject loop end

                    sb.Append("<tr style='height:20px'>");
                    sb.Append("<td valign='center' rowspan = '2' style = 'border: 1px solid black; font-size:14px;vertical-align:middle; text-align:center'>Grade</td>");
                    sb.Append(subject_name + "</tr>");
                    sb.Append("<tr style='height:20px'>");
                    sb.Append(student_type + "</tr>");



                    for (int y= 0; y < dtsub.Rows.Count; y++)
                    {

                        dt_grade.Columns.Add(dtsub.Rows[y]["subject_name"].ToString() + "_Boy");
                        dt_grade.Columns.Add(dtsub.Rows[y]["subject_name"].ToString() + "_Girl");
                        dt_grade.Columns.Add(dtsub.Rows[y]["subject_name"].ToString() + "_Total");

                        DataTable dt_count = new DataTable();
                        html = string.Empty;

                        urlalias = cls.urls();
                        url = @urlalias + "ExamReport/";

                        useless();

                        if (Session["acdyear"] == null)
                        {
                            notifys("Please first select academic year.", "#D44950");
                            return;
                        }

                        er.type = "fill_marks";
                        er.medium_id = ddlmedium.SelectedValue.ToString();
                        er.class_id = arry[m].ToString();
                        er.subject_id = dtsub.Rows[y]["subject_id"].ToString();
                        er.ayid = Session["acdyear"].ToString();
                        er.exam_name = ddl_exam_new.SelectedItem.Text.ToString();

                        jsonString = JsonHelper.JsonSerializer<exam_reports>(er);
                        httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                        httpWebRequest.ContentType = "application/json";
                        httpWebRequest.Method = "POST";

                        using (StreamWriter sw = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            sw.Write(jsonString);
                            sw.Flush();
                            sw.Close();
                        }

                        httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                        dslist = new DataSet();

                        using (StreamReader sr = new StreamReader(httpWebResponse.GetResponseStream()))
                        {
                            string result = sr.ReadToEnd();
                            dslist = JsonConvert.DeserializeObject<DataSet>(result);
                        }

                        dt_count = dslist.Tables[0];

                        for (int l = 0; l < dt_count.Rows.Count; l++)
                        {
                            dt_grade.Rows[l][dtsub.Rows[y]["subject_name"] + "_Boy"] = dt_count.Rows[l]["boys"];
                            dt_grade.Rows[l][dtsub.Rows[y]["subject_name"] + "_Girl"] = dt_count.Rows[l]["girls"];
                            dt_grade.Rows[l][dtsub.Rows[y]["subject_name"] + "_Total"] = dt_count.Rows[l]["tot"];
                             
                          
                        }
                     

                    }//subject loop end

                    for (int i = 0; i < dt_grade.Rows.Count; i++)
                    {
                        sb.Append("<tr style='height:20px'>");
                        sb.Append("<td valign='center' style = 'border: 1px solid black; font-size:14px;vertical-align:middle; text-align:center'>" + dt_grade.Rows[i]["Grade"] + "</td>");
                        for (int j = 1; j < dt_grade.Columns.Count; j++)
                        {
                        
                            sb.Append("<td valign='center' style = 'border: 1px solid black; font-size:14px;vertical-align:middle; text-align:center'>" + dt_grade.Rows[i][j] + "</td>");
                            j++;
                            sb.Append("<td valign='center' style = 'border: 1px solid black; font-size:14px;vertical-align:middle; text-align:center'>" + dt_grade.Rows[i][j] + "</td>");
                            j++;
                            sb.Append("<td valign='center' style = 'border: 1px solid black; font-size:14px;vertical-align:middle; text-align:center'>" + dt_grade.Rows[i][j] + "</td>");
                           
                           
                        }
                        sb.Append("</tr>");
                    }

                   

                }
                //---------------------------------------------

            }// standard loop end for header
            //---------------------------------------------------------------------------------------

            sb.Append("</table> ");
            
            Session["sb"] = sb.ToString();
            Session["error"] = "0";
           
        }

        catch (Exception)
        {
            notifys("Marks Entry is not available for given students.", "#D44950");
            //return;
            Session["error"] = "1";
        }
    }

    protected void btnexcel_Click(object sender, EventArgs e)
    {
        if (rd_report2.Checked == true)
        {

        }
        else if (rd_report3.Checked == true)
        {
            build_html_table_Class_wise_with_All_subject_and_Grade();

            StringBuilder sb = new StringBuilder();
            if (Session["error"].ToString() != "1")
            {
                sb.Append(Session["sb"].ToString());
                Session["sb"] = null;
                notifys("Successfully generated final Report.", "#198104");
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("content-disposition", "attachment;filename=Report.xls");
                Response.Charset = "";
                StringWriter sw = new StringWriter();


                sw.Write(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                notifys("No data available for given selection.", "#D44950");
                return;
            }

        }
        else
        {
            notifys("Select Report Type.", "#D44950");
            return;
        }

      
       
    }
}