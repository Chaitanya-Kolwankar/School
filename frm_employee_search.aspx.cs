using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
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

public partial class frm_employee_search : System.Web.UI.Page
{
    Employee_Search employee = new Employee_Search();
    Class1 cls = new Class1();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ConnectionString);
    SqlCommand cmd = new SqlCommand();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["emp_id"] == null || Session["emp_id"] == "")
        {
            Response.Redirect("Login.aspx?msg=session");
        }
        else
        {
            if (!IsPostBack)
            {
                load();
                ddldepartment.SelectedItem.Text = "--Select Department--";
                ddldesignation.SelectedItem.Text = "--Select Designation--";
                ddldesignation.Enabled = false;
                panel1.Visible = false;
                grid1.Visible = false;
                chkselect.Enabled = true;
            }
        }

    }
    public void clear()
    {
        Page.Response.Redirect(Page.Request.Url.ToString(), true);
    }
    public void load()
    {
        try
        {
            string urlalias = cls.urls();
            string url = @urlalias + "loademp/";


            employee.type = "select";


            string jsonString = JsonHelper.JsonSerializer<Employee_Search>(employee);
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

                ddldepartment.DataSource = dataSet.Tables[0];
                ddldepartment.DataTextField = "Department_name";
                ddldepartment.DataValueField = "Dept_id";
                ddldepartment.DataBind();


                ddldesignation.DataSource = dataSet.Tables[1];
                ddldesignation.DataTextField = "Designation_Title";
                ddldesignation.DataValueField = "Designation_ID";
                ddldesignation.DataBind();

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
    public void notifys(string msg, string color)
    {
        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + msg + "', { color: '#fff', background: '" + color + "', blur: 0.2, delay: 0 });", true);
    }

    public void refresh()
    {
        ddldepartment.SelectedIndex = 0;
        ddldesignation.Enabled = false;
        ddldesignation.SelectedIndex = 0;
    }

    protected void btnget_Click(object sender, EventArgs e)
    {
        try
        {
            string urlalias = cls.urls();
            string url = @urlalias + "loademp/";


            
            employee.type = "get";
            if (ddldepartment.SelectedIndex > 0)
            {
                employee.department = ddldepartment.SelectedValue.ToString();
            }
            if (ddldesignation.SelectedIndex > 0)
            {
                employee.designation = ddldesignation.SelectedValue.ToString();
            }
            if (ddldepartment.SelectedIndex == 0)
            {
                ddldesignation.Enabled = false;
                notifys("Select Proper Department", "#D44950");
                refresh();
                return;
            }
            else if (ddldesignation.SelectedIndex == 0)
            {
                notifys("Select Proper Designation", "#D44950");
                return;
            }

            string jsonString = JsonHelper.JsonSerializer<Employee_Search>(employee);
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

                    grid1.DataSource = dataSet.Tables[0];
                    grid1.DataBind();



                    for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                    {

                        Button btnrow = (Button)grid1.Rows[i].Cells[4].FindControl("btndel");

                        if (dataSet.Tables[0].Rows[i]["del_flag"].ToString().ToUpper() == "FALSE")
                        {

                            btnrow.Text = "Delete";
                            btnrow.BackColor = ColorTranslator.FromHtml("#A60000");
                            btnrow.ForeColor = Color.White;
                        }
                        else if (dataSet.Tables[0].Rows[i]["del_flag"].ToString().ToUpper() == "TRUE")
                        {

                            btnrow.Text = "Recover";
                            btnrow.BackColor = Color.Green;
                            btnrow.ForeColor = Color.White;
                        }
                    }
                    grid1.Visible = true;
                }
                else
                {
                    grid1.Visible = false;
                    notifys("No Employees Found  for provided Department and Designation", "#D44950");
                    chkselect.Enabled = true;
                    chkselect.Checked = false;
                    panel1.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void ddldepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldepartment.SelectedIndex == 0)
        {
            ddldesignation.Enabled = false;
            ddldesignation.SelectedIndex = 0;
            grid1.Visible = false;
        }
        else if (ddldepartment.SelectedIndex > 0)
        {
            ddldesignation.Enabled = true;
        }
    }

    protected void ddldesignation_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkselect.Enabled = true;
        grid1.DataSource = "";
        grid1.DataBind();
    }

    protected void chkselect_CheckedChanged(object sender, EventArgs e)
    {
        if (chkselect.Checked == true)
        {
            ddldesignation.SelectedIndex = 0;
            ddldepartment.SelectedIndex = 0;
            panel1.Visible = true;
            ddldepartment.Enabled = false;
            ddldesignation.Enabled = false;
            btnget.Enabled = false;
            grid1.DataSource = null;
            grid1.DataBind();
            grid1.Visible = false;
            txtfname.Text = "";
            txtmname.Text = "";
            txtlname.Text = "";
        }
        if (chkselect.Checked == false)
        {
            panel1.Visible = false;
            ddldepartment.Enabled = true;
            btnget.Enabled = true;
            grid1.DataSource = null;
            grid1.DataBind();
            grid1.Visible = false;
            txtfname.Text = "";
            txtmname.Text = "";
            txtlname.Text = "";
        }
    }

    protected void btnselect_Click(object sender, EventArgs e)
    {
        try
        {
            string urlalias = cls.urls();
            string url = @urlalias + "loademp/";

            employee.type = "Search";

            if (txtfname.Text == "" && txtmname.Text == "" && txtlname.Text == "")
            {
                notifys("Enter Atleast in One Field", "#D44950");
                return;
            }
            if (txtfname.Text != "")
            {
                employee.fname = txtfname.Text.Trim();
            }
            if (txtmname.Text != "")
            {
                employee.mname = txtmname.Text.Trim();
            }
            if (txtlname.Text != "")
            {
                employee.lname = txtlname.Text.Trim();
            }



            if (ddldepartment.SelectedIndex > 0)
            {
                employee.department = ddldepartment.SelectedValue.ToString();
            }
            if (ddldesignation.SelectedIndex > 0)
            {
                employee.designation = ddldesignation.SelectedValue.ToString();
            }
            string jsonString = JsonHelper.JsonSerializer<Employee_Search>(employee);
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

                    grid1.DataSource = dataSet.Tables[0];
                    grid1.DataBind();

                    for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                    {
                        Button btnrow = (Button)grid1.Rows[i].Cells[4].FindControl("btndel");
                        if (dataSet.Tables[0].Rows[i]["del_flag"].ToString().ToUpper() == "FALSE")
                        {
                            btnrow.Text = "Delete";
                            btnrow.BackColor = ColorTranslator.FromHtml("#A60000");
                            btnrow.ForeColor = Color.White;
                        }
                        else if (dataSet.Tables[0].Rows[i]["del_flag"].ToString().ToUpper() == "TRUE")
                        {
                            btnrow.Text = "Recover";
                            btnrow.BackColor = Color.Green;
                            btnrow.ForeColor = Color.White;
                        }
                    }
                    grid1.Visible = true;
                }
                else
                {
                    grid1.Visible = false;
                    notifys("No Such Employee Found for provided Name", "#D44950");

                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void grid1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            if (e.CommandName == "select")
            {
                int RowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = grid1.Rows[RowIndex];

                Button btnrow = (Button)row.FindControl("btndel");
                if (btnrow.Text == "Delete")
                {
                    btnrow.Enabled = false;
                    btnrow.Text = "Recover";
                    btnrow.BackColor = Color.Green;
                    btnrow.ForeColor = Color.White;

                    employee.eid = row.Cells[2].Text.ToString();
                    string urlalias = cls.urls();
                    string url = @urlalias + "loademp/";

                    employee.type = "Update";


                    string jsonString = JsonHelper.JsonSerializer<Employee_Search>(employee);
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
                    }

                    notifys("Deleted Successfully", "#198104");
                    btnrow.Enabled = true;
                }

                else if (btnrow.Text == "Recover")
                {

                    btnrow.Enabled = false;
                    btnrow.Text = "Delete";
                    btnrow.BackColor = ColorTranslator.FromHtml("#A60000");
                    btnrow.ForeColor = Color.White;

                    employee.eid = row.Cells[2].Text.ToString();
                    string urlalias = cls.urls();
                    string url = @urlalias + "loademp/";


                    employee.type = "set";


                    string jsonString = JsonHelper.JsonSerializer<Employee_Search>(employee);
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

                    }
                    notifys("Recovered Successfully", "#198104");
                    btnrow.Enabled = true;
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void btn_Click(object sender, EventArgs e)
    {
        try
        {
            string urlalias = cls.urls();
            string url = @urlalias + "loademp/";
            if (txtfname.Text == "" && txtmname.Text == "" && txtlname.Text=="")
            {
                notifys("Enter Atleast in One Field", "#D44950");
                return;
            }
            if (txtfname.Text != "")
            {
                employee.fname = txtfname.Text;
            }
            if (txtmname.Text != "")
            {
                employee.mname = txtmname.Text;
            }
            if (txtlname.Text != "")
            {
                employee.lname = txtlname.Text;
            }
            employee.type = "Search";
            

            if (ddldepartment.SelectedIndex == 0)
            {
                notifys("select proper department", "#D44950");
                refresh();
                return;
            }
            else if (ddldesignation.SelectedIndex == 0)
            {
                notifys("Select proper designation", "#D44950");
                return;
            }
            if (ddldepartment.SelectedIndex > 0)
            {
                employee.department = ddldepartment.SelectedValue.ToString();
            }
            if (ddldesignation.SelectedIndex > 0)
            {
                employee.designation = ddldesignation.SelectedValue.ToString();
            }
            string jsonString = JsonHelper.JsonSerializer<Employee_Search>(employee);
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

                    grid1.DataSource = dataSet.Tables[0];
                    grid1.DataBind();

                    for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                    {
                        Button btnrow = (Button)grid1.Rows[i].Cells[4].FindControl("btndel");
                        if (dataSet.Tables[0].Rows[i]["del_flag"].ToString().ToUpper() == "FALSE")
                        {
                            btnrow.Text = "Delete";
                            btnrow.BackColor = ColorTranslator.FromHtml("#A60000");
                            btnrow.ForeColor = Color.White;
                        }
                        else if (dataSet.Tables[0].Rows[i]["del_flag"].ToString().ToUpper() == "TRUE")
                        {
                            btnrow.Text = "Recover";
                            btnrow.BackColor = Color.Green;
                            btnrow.ForeColor = Color.White;
                        }
                        //}

                    }
                    grid1.Visible = true;
                }
                else
                {
                    grid1.Visible = false;
                    notifys("Student Entry Not Done", "#D44950");

                }
            }
        }
        catch (Exception ex)
        {
            //Response.Redirect("Login.aspx");
        }
    }
}