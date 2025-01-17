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

public partial class Bonafide_Certificate : System.Web.UI.Page
{
    Bonafide bonafide = new Bonafide();
    Class1 cls = new Class1();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ConnectionString);
    SqlCommand cmd = new SqlCommand();
    double id1 = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Convert.ToString(Session["emp_id"]) == "")
                {
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    btns.Text = "Save";
                    load();
                    ddlssc.Enabled = false;
                    ddlssc1.Enabled = false;
                    ddlotherstd.Enabled = false;                    
                    ddlssc1.Items.Add("--Select--");
                    ddlotherstd.Items.Add("--Select--");
                    chkssc.Checked = false;
                    btns.Visible = false;
                    btnc.Visible = false;
                    btn_print.Visible = false;
                    txtstandard.Enabled = false;
                    chkotherstandard.Checked = false;
                    Session["stud_id"] = "";
                    chkssc.Enabled = false;
                    chkotherstandard.Enabled = false;                    
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
    }
    public void clear()
    {
        Page.Response.Redirect(Page.Request.Url.ToString(), true);
    }

    public void load()
    {
        txtstudame.Enabled = false;
        txtbonafideno.Enabled = false;
        txtdob.Enabled = false;
        txtcast.Enabled = false;
        txtsubcast.Enabled = false;
        txtsaralno.Enabled = false;
        txtAadhar.Enabled = false;
        txtgender.Enabled = false;
        txtremk.Enabled = false;
    }
    public void refresh()
    {
        txtid.Text = "";
        txtstudame.Text = "";
        txtbonafideno.Text = "";
        txtstandard.Text = "";
        txtdob.Text = "";
        txtcast.Text = "";
        txtsubcast.Text = "";
        txtsaralno.Text = "";
        txtAadhar.Text = "";
        txtremk.Text = "";
        ddlssc.SelectedIndex = 0;
        ddlssc1.SelectedIndex = 0;       
        ddlotherstd.SelectedIndex = 0;
        ddlotherstd.DataSource = null;
        ddlssc.DataSource = null;
        ddlssc1.DataSource = null;
        ddlotherstd.DataSource = null;
        ddlssc.Enabled = false;
        ddlssc1.Enabled = false;
        ddlotherstd.Enabled = false;
        chkssc.Checked = false;
        chkotherstandard.Checked = false;
        txtgender.Text = "";
        txtid.Enabled = true;
        txtstandard.Enabled = false;
        txtremk.Enabled = false;
    }
    protected void txtid_TextChanged(object sender, EventArgs e)
    {
        string urlalias = cls.urls();
        string url = @urlalias + "loadbonafide/";
        try
        {
                bonafide.type = "select";
                bonafide.Ayid = Session["acdyear"].ToString();
                bonafide.sid = txtid.Text.Trim();
                txtid.Enabled = false;

                string jsonString = JsonHelper.JsonSerializer<Bonafide>(bonafide);
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

                if (dataSet.Tables["Bonafide"].Rows.Count > 1)
                {
                    grid2.DataSource = dataSet.Tables[1];
                    grid2.DataBind();
                    grid2.Visible = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal('modal1');", true);
                }
                else if (dataSet.Tables["Bonafide"].Rows.Count == 1)
                {
                    chkssc.Enabled = true;
                    chkotherstandard.Enabled = true;

                    bonafide.sid = dataSet.Tables["Bonafide"].Rows[0]["student_id"].ToString();
                    bonafide.type = "get";
                    bonafide.Ayid = Session["acdyear"].ToString();
                    bonafide.standard = dataSet.Tables["Bonafide"].Rows[0]["class_id"].ToString();
                    string jsonString1 = JsonHelper.JsonSerializer<Bonafide>(bonafide);
                    var httprequest1 = (HttpWebRequest)WebRequest.Create(url);
                    httprequest1.ContentType = "application/json";
                    httprequest1.Method = "POST";

                    using (var streamWriter1 = new StreamWriter(httprequest1.GetRequestStream()))
                    {
                        streamWriter1.Write(jsonString1);
                        streamWriter1.Flush();
                        streamWriter1.Close();
                    }

                    var httpresponse1 = (HttpWebResponse)httprequest1.GetResponse();
                    using (var streamReader1 = new StreamReader(httpresponse1.GetResponseStream()))
                    {
                        string result1 = streamReader1.ReadToEnd();
                        DataSet dataSet1 = JsonConvert.DeserializeObject<DataSet>(result1);

                        if (dataSet1.Tables[0].Rows.Count > 0)
                        {
                            ddlload();
                            btns.Text = "Update";
                            Session["stud_id"] = dataSet1.Tables["issue_bonafide"].Rows[0]["Stud_Id"].ToString();
                            bonafide.bonafideno = dataSet1.Tables["issue_bonafide"].Rows[0]["Bonafide_No"].ToString(); ;
                            txtstudame.Text = dataSet1.Tables["issue_bonafide"].Rows[0]["Stud_Name"].ToString();
                            txtstandard.Text = dataSet1.Tables["issue_bonafide"].Rows[0]["std_name"].ToString();
                            txtstud_id.Value = dataSet1.Tables["issue_bonafide"].Rows[0]["Stud_Id"].ToString();

                            standardid.Text = dataSet1.Tables["issue_bonafide"].Rows[0]["class_id"].ToString();
                            txtdob.Text = dataSet1.Tables["issue_bonafide"].Rows[0]["dob"].ToString();
                            txtcast.Text = dataSet1.Tables["issue_bonafide"].Rows[0]["cast_name"].ToString();
                            txtsubcast.Text = (dataSet1.Tables["issue_bonafide"].Rows[0]["subcast_name"].ToString());
                            txtsaralno.Text = dataSet1.Tables["issue_bonafide"].Rows[0]["saral_id"].ToString();
                            txtAadhar.Text = dataSet1.Tables["issue_bonafide"].Rows[0]["aadhar_no"].ToString();
                            txtbonafideno.Text = bonafide.bonafideno;
                            txtgender.Text = dataSet1.Tables["issue_bonafide"].Rows[0]["gender"].ToString();
                            txtissue.Value= dataSet1.Tables["issue_bonafide"].Rows[0]["issue"].ToString();
                            Session["standard"] = dataSet1.Tables["issue_bonafide"].Rows[0]["class_id"].ToString();
                            txtremk.Text=dataSet1.Tables["issue_bonafide"].Rows[0]["Remark"].ToString();
                            txtremk.Enabled = true;
                            if (txtstandard.ToString().Contains("10"))
                            {
                                //chkssc.Enabled = true;
                            }

                            if (dataSet1.Tables["issue_bonafide"].Rows[0]["month"].ToString().Contains("--"))
                            {
                                ddlssc.SelectedIndex = 0;
                            }
                            else
                            {
                                chkssc.Checked = true;
                                ddlssc.Enabled = true;
                                ddlssc1.Enabled = true;
                                ddlssc.SelectedValue = dataSet1.Tables["issue_bonafide"].Rows[0]["month"].ToString().Trim();
                                chkssc.Checked = true;
                            }
                            if (dataSet1.Tables["issue_bonafide"].Rows[0]["year"].ToString().Contains("--"))
                            {
                                ddlssc1.SelectedIndex = 0;
                            }
                            else
                            {
                                ddlssc1.SelectedValue = dataSet1.Tables["issue_bonafide"].Rows[0]["year"].ToString().Trim();
                            }
                            if (dataSet1.Tables["issue_bonafide"].Rows[0]["other_std"].ToString().Contains("--"))
                            {

                                ddlotherstd.SelectedItem.Text = "--Select--";
                            }
                            else
                            {
                                chkotherstandard.Checked = true;
                                ddlotherstd.Enabled = true;
                                string otherstd = dataSet1.Tables["issue_bonafide"].Rows[0]["other_std"].ToString();
                                chkotherstandard_CheckedChanged(sender, e);
                                ddlotherstd.SelectedValue = dataSet1.Tables["issue_bonafide"].Rows[0]["other_std"].ToString().Trim();
                            }
                            btns.Visible = true;
                            btnc.Visible = true;
                            btn_print.Visible = true;
                        }
                        else if (dataSet1.Tables[0].Rows.Count == 0)
                        {
                            Session["stud_id"] = dataSet.Tables["Bonafide"].Rows[0]["student_id"].ToString();
                            btns.Text = "Save";
                            bonafide.bonafideno = "";
                            txtstudame.Text = dataSet.Tables["Bonafide"].Rows[0]["Stud_Name"].ToString();
                            txtstandard.Text = dataSet.Tables["Bonafide"].Rows[0]["std_name"].ToString();
                            txtstud_id.Value = dataSet.Tables["Bonafide"].Rows[0]["student_Id"].ToString();
                            standardid.Text = dataSet.Tables["Bonafide"].Rows[0]["class_id"].ToString();
                            txtdob.Text = dataSet.Tables["Bonafide"].Rows[0]["dob"].ToString();
                            txtcast.Text = dataSet.Tables["Bonafide"].Rows[0]["cast_name"].ToString();
                            txtsubcast.Text = (dataSet.Tables["Bonafide"].Rows[0]["subcast_name"].ToString());
                            txtsaralno.Text = dataSet.Tables["Bonafide"].Rows[0]["saral_id"].ToString();
                            txtAadhar.Text = dataSet.Tables["Bonafide"].Rows[0]["aadhar_no"].ToString();
                            txtgender.Text = dataSet.Tables["Bonafide"].Rows[0]["gender"].ToString();
                            //txtissue.Value = dataSet.Tables["Bonafide"].Rows[0]["issue"].ToString();
                            Session["standard"] = dataSet.Tables["Bonafide"].Rows[0]["class_id"].ToString();
                           // txtremk.Text = dataSet.Tables["Bonafide"].Rows[0]["Remark"].ToString();
                            txtremk.Enabled = true;
                            ddlssc.SelectedIndex = 0;
                            ddlssc1.SelectedIndex = 0;
                            ddlotherstd.SelectedIndex = 0;
                            if (txtstandard.ToString().Contains("10"))
                            {
                                chkssc.Checked = true;
                            }
                            btns.Visible = true;
                            btnc.Visible = true;
                        }
                    }
                }
                else
                {
                    notifys("Invalid Student ID or GR Number", "#D44950");
                    txtid.Text = "";
                    txtid.Enabled = true;
                    return;
                }

            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }
    protected void btns_Click(object sender, EventArgs e)
    {
        try
        {
            if (btns.Text == "Save")
            {
                if (txtstandard.Text == "--Select--")
                {
                    notifys("Invalid Standard", "#D44950");
                    return;
                }
                if (txtid.Text == "")
                {
                    notifys("Invalid Student Id", "#D44950");
                    return;
                }
                string urlalias = cls.urls();
                string url3 = @urlalias + "loadbonafide/";

                bonafide.sid = Session["stud_id"].ToString().ToUpper();
                bonafide.Ayid = Session["acdyear"].ToString().ToUpper();
                bonafide.sname = txtstudame.Text.ToUpper();

                bonafide.standard = Session["standard"].ToString().ToUpper();
                bonafide.cast = txtcast.Text.ToUpper();
                bonafide.subcast = txtsubcast.Text.ToUpper();
                bonafide.dob = txtdob.Text;
                bonafide.gender = txtgender.Text.ToUpper();
                bonafide.remark = txtremk.Text.Trim();

                if (chkssc.Checked == true && chkotherstandard.Checked == false)
                {
                    if (ddlssc.SelectedItem.ToString().ToUpper() == "--SELECT--" || ddlssc1.SelectedItem.ToString().ToUpper() == "--SELECT--")
                    {
                        notifys("Select SSC exam month and year", "#D44950");
                        return;
                    }
                    bonafide.examyear = ddlssc1.SelectedItem.ToString();
                    bonafide.exammonth = ddlssc.SelectedItem.ToString();
                    
                    bonafide.otherstandard = "--";
                }
                else if (chkssc.Checked == false && chkotherstandard.Checked == true)
                {
                    if (ddlotherstd.SelectedItem.ToString().ToUpper() == "--SELECT--")
                    {
                        notifys("Select other standard", "#D44950");
                        return;
                    }
                    bonafide.examyear = "--";
                    bonafide.exammonth = "--";
                    bonafide.otherstandard = ddlotherstd.SelectedValue.ToString();
                }
                if (chkotherstandard.Checked == false && chkssc.Checked == false)
                {
                    bonafide.examyear = "--";
                    bonafide.exammonth = "--";
                    bonafide.otherstandard = "--";
                }
                bonafide.saralno = txtsaralno.Text;
                bonafide.aadharno = txtAadhar.Text;

                string remark = "Bonafide_Issued";
                bonafide.bonafideno = "";
                bonafide.userid = Session["emp_id"].ToString();
                bonafide.bonafide_remark = remark;

                bonafide.type = "insert";
                string msg = "";

                string jsonString3 = JsonHelper.JsonSerializer<Bonafide>(bonafide);
                var httprequest3 = (HttpWebRequest)WebRequest.Create(url3);
                httprequest3.ContentType = "application/json";
                httprequest3.Method = "POST";

                using (var streamWriter3 = new StreamWriter(httprequest3.GetRequestStream()))
                {
                    streamWriter3.Write(jsonString3);
                    streamWriter3.Flush();
                    streamWriter3.Close();
                }

                var httpresponse3 = (HttpWebResponse)httprequest3.GetResponse();
                using (var streamReader3 = new StreamReader(httpresponse3.GetResponseStream()))
                {
                    string result3 = streamReader3.ReadToEnd();
                    if (result3.ToString().Contains("true") == true)
                    {
                        notifys("Saved Successfully", "#198104");
                    }
                    else
                    {

                        notifys("Data Not Saved", "#D44950");
                    }
                }
               
                refresh();
                btns.Visible = false;
                btnc.Visible = false;
                ddlotherstd.Enabled = false;
                btn_print.Visible = false;
                chkssc.Enabled = false;
                chkotherstandard.Enabled = false;
                return;

            }
            if (btns.Text == "Update")
            {
                if (txtstandard.Text == "select")
                {
                    txtid.Text = "";
                    notifys("Invalid Standard", "#D44950");
                    return;

                }
                if (txtid.Text == "")
                {

                    notifys("Invalid Student Id", "#D44950");
                    return;
                }
                string urlalias = cls.urls();
                string url1 = @urlalias + "loadbonafide/";


                bonafide.type = "select";
                bonafide.Ayid = Session["acdyear"].ToString().ToUpper();
                bonafide.sid = Session["stud_id"].ToString().ToUpper();


                string jsonString = JsonHelper.JsonSerializer<Bonafide>(bonafide);
                var httprequest = (HttpWebRequest)WebRequest.Create(url1);
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

                    string url3 = @urlalias + "loadbonafide/";
                    bonafide.sid = Session["stud_id"].ToString().ToUpper();
                    bonafide.Ayid = Session["acdyear"].ToString().ToUpper();
                    bonafide.sname = txtstudame.Text.ToUpper();

                    bonafide.standard = Session["standard"].ToString().ToUpper();
                    bonafide.cast = txtcast.Text.ToUpper();
                    bonafide.subcast = txtsubcast.Text.ToUpper();
                    bonafide.dob = txtdob.Text;
                    bonafide.gender = txtgender.Text.ToUpper();
                    bonafide.remark = txtremk.Text.Trim();

                    if (chkssc.Checked == true && chkotherstandard.Checked == false)
                    {
                        if (ddlssc.SelectedItem.ToString().ToUpper() == "--SELECT--" || ddlssc1.SelectedItem.ToString().ToUpper() == "--SELECT--")
                        {
                            notifys("Select SSC exam month and year", "#D44950");
                            return;
                        }
                        bonafide.examyear = ddlssc1.SelectedItem.ToString();
                        bonafide.exammonth = ddlssc.SelectedItem.ToString();

                        bonafide.otherstandard = "--";
                    }
                    else if (chkssc.Checked == false && chkotherstandard.Checked == true)
                    {
                        if (ddlotherstd.SelectedItem.ToString().ToUpper() == "--SELECT--")
                        {
                            notifys("Select other standard", "#D44950");
                            return;
                        }
                        bonafide.examyear = "--";
                        bonafide.exammonth = "--";
                        bonafide.otherstandard = ddlotherstd.SelectedValue.ToString();
                    }
                    if (chkotherstandard.Checked == false && chkssc.Checked == false)
                    {
                        bonafide.examyear = "--";
                        bonafide.exammonth = "--";
                        bonafide.otherstandard = "--";
                    }


                    bonafide.saralno = txtsaralno.Text;
                    bonafide.aadharno = txtAadhar.Text;

                    string remark = "Bonafide_Issued";
                    bonafide.bonafideno = txtbonafideno.Text;
                    bonafide.userid = Session["emp_id"].ToString();
                    bonafide.bonafide_remark = remark;

                    bonafide.type = "Update";
                    string msg = "";

                    string jsonString3 = JsonHelper.JsonSerializer<Bonafide>(bonafide);
                    var httprequest3 = (HttpWebRequest)WebRequest.Create(url3);
                    httprequest3.ContentType = "application/json";
                    httprequest3.Method = "POST";

                    using (var streamWriter3 = new StreamWriter(httprequest3.GetRequestStream()))
                    {
                        streamWriter3.Write(jsonString3);
                        streamWriter3.Flush();
                        streamWriter3.Close();
                    }

                    var httpresponse3 = (HttpWebResponse)httprequest3.GetResponse();
                    using (var streamReader3 = new StreamReader(httpresponse3.GetResponseStream()))
                    {
                        string result3 = streamReader3.ReadToEnd();
                        if (result3.ToString().Contains("true") == true)
                        {
                            notifys("Updated Successfully", "#198104");
                        }
                        else
                        {

                            notifys("Data Not Saved", "#D44950");
                        }
                    }
                   
                    btns.Visible = false;
                    btnc.Visible = false;
                    btn_print.Visible = false;
                    ddlotherstd.Enabled = false;
                    chkssc.Enabled = false;
                    chkotherstandard.Enabled = false;
                    refresh();
                    return;
                }

            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }
    protected void btnc_Click(object sender, EventArgs e)
    {
        clear();
    }
    public void notifys(string msg, string color)
    {
        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + msg + "', { color: '#fff', background: '" + color + "', blur: 0.2, delay: 0 });", true);
    }
    protected void btnissue_Click(object sender, EventArgs e)
    {        
        string urlalias = cls.urls();
        string url = @urlalias + "loadbonafide/";


        bonafide.type = "get";
        bonafide.Ayid = Session["acdyear"].ToString();
        bonafide.sid = Session["stud_id"].ToString().ToUpper();
        bonafide.standard = Session["standard"].ToString();
        string jsonString = JsonHelper.JsonSerializer<Bonafide>(bonafide);
        var httprequest = (HttpWebRequest)WebRequest.Create(url);
        httprequest.ContentType = "application/json";
        httprequest.Method = "POST";
        try
        {
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
                if (dataSet.Tables["issue_bonafide"].Rows.Count > 0)
                {
                    string url1 = @urlalias + "loadbonafide/";

                    bonafide.type = "data";
                    bonafide.Ayid = Session["acdyear"].ToString();
                    bonafide.sid = Session["stud_id"].ToString().ToUpper();
                    bonafide.standard = Session["standard"].ToString();                  
                    Session["bonafideno"] = txtbonafideno.Text;
                    
                    string jsonString1 = JsonHelper.JsonSerializer<Bonafide>(bonafide);
                    var httprequest1 = (HttpWebRequest)WebRequest.Create(url1);
                    httprequest1.ContentType = "application/json";
                    httprequest1.Method = "POST";

                    using (var streamWriter1 = new StreamWriter(httprequest1.GetRequestStream()))
                    {
                        streamWriter1.Write(jsonString1);
                        streamWriter1.Flush();
                        streamWriter1.Close();
                    }

                    var httpresponse1 = (HttpWebResponse)httprequest1.GetResponse();
                    using (var streamReader1 = new StreamReader(httpresponse1.GetResponseStream()))
                    {
                        string result1 = streamReader1.ReadToEnd();
                        DataSet dataSet1 = JsonConvert.DeserializeObject<DataSet>(result1);

                        string value = dataSet1.Tables["issue"].Rows[0]["issue"].ToString();
                        int count = 0;
                        count = Convert.ToInt32(value) + 1;

                        if (count > 50)
                        {
                        }
                        else
                        {
                            string url7 = @urlalias + "loadbonafide/";

                            bonafide.type = "set";
                            bonafide.issue = count.ToString();
                            bonafide.standard = Session["standard"].ToString();
                            bonafide.bonafideno = txtbonafideno.Text;
                            bonafide.userid = Session["emp_id"].ToString();
                            bonafide.remark = txtremk.Text.Trim();

                            string jsonString7 = JsonHelper.JsonSerializer<Bonafide>(bonafide);
                            var httprequest7 = (HttpWebRequest)WebRequest.Create(url7);
                            httprequest7.ContentType = "application/json";
                            httprequest7.Method = "POST";

                            using (var streamWriter7 = new StreamWriter(httprequest7.GetRequestStream()))
                            {
                                streamWriter7.Write(jsonString7);
                                streamWriter7.Flush();
                                streamWriter7.Close();
                            }

                            var httpresponse7 = (HttpWebResponse)httprequest7.GetResponse();
                            using (var streamReader7 = new StreamReader(httpresponse7.GetResponseStream()))
                            {
                                string result7 = streamReader7.ReadToEnd();
                                //if (ddl_section.SelectedItem.Text == "PREPRIMARY")
                                //{
                                //}
                                //else if (ddl_section.SelectedItem.Text == "PRIMARY")
                                //{
                                //    ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "click", @"<script>window.open('Bonafide_Report.aspx','_newtab');</script>", false);
                                //}
                                //else if (ddl_section.SelectedItem.Text == "SECONDARY")
                                //{
                                   // ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "click", @"<script>window.open('Bonafide_report_secondary.aspx','_newtab');</script>", false);
                               // }
                               
                                refresh();
                               // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "disp_confirm", "$('#printmodal').modal('hide');", true);
                            }
                        }
                    }
                    btnc.Visible = false;
                    btns.Visible = false;
                    btn_print.Visible = false;
                    chkssc.Enabled = false;
                    chkotherstandard.Enabled = false;
                }
                else
                {
                    notifys("No data found", "#198104");
                    return;
                }
            }
        }
        catch (Exception ex) {
            Response.Redirect("Login.aspx");
        }
    }

    protected void radio1_SelectedIndexChanged(object sender, EventArgs e)
    {       
    }
    protected void chkssc_CheckedChanged(object sender, EventArgs e)
    {       
        if (chkssc.Checked == true)
        {
            chkotherstandard.Checked = false;           
            ddlssc.Enabled = true;
            ddlssc1.Enabled = true;
            ddlotherstd.Enabled = false;
            ddlotherstd.SelectedIndex = 0;

            try
            {
                string urlalias = cls.urls();
                string url3 = @urlalias + "loadbonafide/";
                bonafide.sid = txtid.Text;
                bonafide.type = "Year";

                string jsonString3 = JsonHelper.JsonSerializer<Bonafide>(bonafide);
                var httprequest3 = (HttpWebRequest)WebRequest.Create(url3);
                httprequest3.ContentType = "application/json";
                httprequest3.Method = "POST";

                using (var streamWriter3 = new StreamWriter(httprequest3.GetRequestStream()))
                {
                    streamWriter3.Write(jsonString3);
                    streamWriter3.Flush();
                    streamWriter3.Close();
                }

                var httpresponse3 = (HttpWebResponse)httprequest3.GetResponse();
                using (var streamReader3 = new StreamReader(httpresponse3.GetResponseStream()))
                {
                    string result3 = streamReader3.ReadToEnd();
                    DataSet dataSet3 = JsonConvert.DeserializeObject<DataSet>(result3);
                    ddlssc1.DataSource = dataSet3.Tables[0];

                    ddlssc1.DataTextField = "duration";
                    ddlssc1.DataValueField = "duration";
                    ddlssc1.DataBind();
                }
            }
            catch (Exception ex) {
                Response.Redirect("Login.aspx");
            }
        }
        else
        {
            chkotherstandard.Checked = false;
            ddlssc.Enabled = false;
            ddlssc.SelectedIndex = 0;
            ddlssc1.SelectedIndex= 0;
            ddlssc1.Enabled = false;
            ddlotherstd.SelectedIndex = 0;
        }
    }


    public void ddlload()
    {
        try
        {
            string urlalias = cls.urls();
            string url3 = @urlalias + "loadbonafide/";
            bonafide.sid = txtid.Text;
            bonafide.type = "Year";

            string jsonString3 = JsonHelper.JsonSerializer<Bonafide>(bonafide);
            var httprequest3 = (HttpWebRequest)WebRequest.Create(url3);
            httprequest3.ContentType = "application/json";
            httprequest3.Method = "POST";

            using (var streamWriter3 = new StreamWriter(httprequest3.GetRequestStream()))
            {
                streamWriter3.Write(jsonString3);
                streamWriter3.Flush();
                streamWriter3.Close();
            }

            var httpresponse3 = (HttpWebResponse)httprequest3.GetResponse();
            using (var streamReader3 = new StreamReader(httpresponse3.GetResponseStream()))
            {
                string result3 = streamReader3.ReadToEnd();
                DataSet dataSet3 = JsonConvert.DeserializeObject<DataSet>(result3);
                ddlssc1.DataSource = dataSet3.Tables[0];

                ddlssc1.DataTextField = "duration";
                ddlssc1.DataValueField = "duration";
                ddlssc1.DataBind();
            }

            bonafide.type = "otherstd";

            string jsonString4 = JsonHelper.JsonSerializer<Bonafide>(bonafide);
            var httprequest4 = (HttpWebRequest)WebRequest.Create(url3);
            httprequest4.ContentType = "application/json";
            httprequest4.Method = "POST";

            using (var streamWriter4 = new StreamWriter(httprequest4.GetRequestStream()))
            {
                streamWriter4.Write(jsonString4);
                streamWriter4.Flush();
                streamWriter4.Close();
            }

            var httpresponse4 = (HttpWebResponse)httprequest4.GetResponse();
            using (var streamReader4 = new StreamReader(httpresponse4.GetResponseStream()))
            {
                string result4 = streamReader4.ReadToEnd();
                DataSet dataSet4 = JsonConvert.DeserializeObject<DataSet>(result4);
                ddlotherstd.DataSource = dataSet4.Tables[0];
                ddlotherstd.DataTextField = "std_name";
                ddlotherstd.DataValueField = "std_id";
                ddlotherstd.DataBind();
                ddlotherstd.SelectedIndex = 0;
            }
        }
        catch (Exception e)
        {
            Response.Redirect("Login.aspx");
        }

    }
    protected void chkotherstandard_CheckedChanged(object sender, EventArgs e)
    {
        if (chkotherstandard.Checked == true)
        {
            chkssc.Checked = false;
            ddlssc.Enabled = false;
            ddlssc1.Enabled = false;
            ddlotherstd.Enabled = true;
            try
            {
                string urlalias = cls.urls();
                string url4 = @urlalias + "loadbonafide/";
                bonafide.sid = txtid.Text;
                bonafide.type = "otherstd";

                string jsonString4 = JsonHelper.JsonSerializer<Bonafide>(bonafide);
                var httprequest4 = (HttpWebRequest)WebRequest.Create(url4);
                httprequest4.ContentType = "application/json";
                httprequest4.Method = "POST";

                using (var streamWriter4 = new StreamWriter(httprequest4.GetRequestStream()))
                {
                    streamWriter4.Write(jsonString4);
                    streamWriter4.Flush();
                    streamWriter4.Close();
                }

                var httpresponse4 = (HttpWebResponse)httprequest4.GetResponse();
                using (var streamReader4 = new StreamReader(httpresponse4.GetResponseStream()))
                {
                    string result4 = streamReader4.ReadToEnd();
                    DataSet dataSet4 = JsonConvert.DeserializeObject<DataSet>(result4);
                    ddlotherstd.DataSource = dataSet4.Tables[0];
                    ddlotherstd.DataTextField = "std_name";
                    ddlotherstd.DataValueField = "std_id";
                    ddlotherstd.DataBind();
                    ddlotherstd.SelectedIndex=0;
                    ddlssc.SelectedIndex = 0;
                    ddlssc1.SelectedIndex = 0;
                }
            }
            catch (Exception ex) {
                Response.Redirect("Login.aspx");
            }
        }
        else
        {
            ddlssc.Enabled = false;
            ddlssc1.Enabled = false;
            ddlotherstd.Enabled = false;
            ddlotherstd.SelectedIndex = 0;
            ddlssc.SelectedIndex = 0;
            ddlssc1.SelectedIndex = 0;
        }
    }


    protected void grid2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "select")
            {
                ddlload();
                int RowIndex = Convert.ToInt32(e.CommandArgument.ToString());
                GridViewRow row = grid2.Rows[RowIndex];
                string stud_id = row.Cells[0].Text.ToString();
                string urlalias = cls.urls();
                string url = @urlalias + "loadbonafide/";

                bonafide.sid = stud_id.ToString();
                Session["stud_id"] = stud_id.ToString();
                bonafide.type = "get";
                bonafide.Ayid = Session["acdyear"].ToString();
                bonafide.standard = row.Cells[4].Text.ToString();
                string jsonString = JsonHelper.JsonSerializer<Bonafide>(bonafide);
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

                    if (dataSet.Tables[0].Rows.Count == 0)
                    {
                        chkssc.Enabled = true;
                        chkotherstandard.Enabled = true;
                        string url1 = @urlalias + "loadbonafide/";
                        bonafide.sid = stud_id.ToString();
                        bonafide.type = "select";

                        string jsonString1 = JsonHelper.JsonSerializer<Bonafide>(bonafide);
                        var httprequest1 = (HttpWebRequest)WebRequest.Create(url1);
                        httprequest1.ContentType = "application/json";
                        httprequest1.Method = "POST";

                        using (var streamWriter1 = new StreamWriter(httprequest1.GetRequestStream()))
                        {
                            streamWriter1.Write(jsonString1);
                            streamWriter1.Flush();
                            streamWriter1.Close();
                        }

                        var httpresponse1 = (HttpWebResponse)httprequest1.GetResponse();
                        using (var streamReader1 = new StreamReader(httpresponse1.GetResponseStream()))
                        {
                            string result1 = streamReader1.ReadToEnd();
                            DataSet dataSet1 = JsonConvert.DeserializeObject<DataSet>(result1);

                            if (dataSet1.Tables[0].Rows.Count > 0)
                            {
                                btns.Text = "Save";
                                txtstudame.Text = dataSet1.Tables["Bonafide"].Rows[0]["Stud_Name"].ToString();
                                txtstandard.Text = dataSet1.Tables["Bonafide"].Rows[0]["std_name"].ToString();
                                txtstud_id.Value = dataSet1.Tables["Bonafide"].Rows[0]["Stud_Id"].ToString();
                                standardid.Text = dataSet1.Tables["Bonafide"].Rows[0]["class_id"].ToString();
                                txtdob.Text = dataSet1.Tables["Bonafide"].Rows[0]["dob"].ToString();
                                txtcast.Text = dataSet1.Tables["Bonafide"].Rows[0]["cast_name"].ToString();
                                txtsubcast.Text = (dataSet1.Tables["Bonafide"].Rows[0]["subcast_name"].ToString());
                                txtsaralno.Text = dataSet1.Tables["Bonafide"].Rows[0]["saral_id"].ToString();
                                txtAadhar.Text = dataSet1.Tables["Bonafide"].Rows[0]["aadhar_no"].ToString();
                                txtbonafideno.Text = "";
                                txtgender.Text = dataSet1.Tables["Bonafide"].Rows[0]["gender"].ToString();
                                txtissue.Value = dataSet1.Tables["Bonafide"].Rows[0]["issue"].ToString();
                                Session["standard"] = dataSet1.Tables["Bonafide"].Rows[0]["class_id"].ToString();                                
                                ddlssc.SelectedIndex = 0;
                                ddlssc1.SelectedIndex = 0;
                                ddlotherstd.SelectedIndex = 0;
                                if (txtstandard.ToString().Contains("10"))
                                {
                                    chkssc.Checked = true;
                                }
                                btns.Visible = true;
                                btnc.Visible = true;
                            }
                        }
                    }

                    else if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        chkssc.Enabled = true;
                        chkotherstandard.Enabled = true;
                        btns.Text = "Update";
                        bonafide.bonafideno = dataSet.Tables["issue_bonafide"].Rows[0]["Bonafide_No"].ToString(); ;
                        txtstudame.Text = dataSet.Tables["issue_bonafide"].Rows[0]["Stud_Name"].ToString();
                        txtstandard.Text = dataSet.Tables["issue_bonafide"].Rows[0]["std_name"].ToString();
                        txtstud_id.Value = dataSet.Tables["issue_bonafide"].Rows[0]["Stud_Id"].ToString();
                        standardid.Text = dataSet.Tables["issue_bonafide"].Rows[0]["class_id"].ToString();
                        txtdob.Text = dataSet.Tables["issue_bonafide"].Rows[0]["dob"].ToString();
                        txtcast.Text = dataSet.Tables["issue_bonafide"].Rows[0]["cast_name"].ToString();
                        txtsubcast.Text = (dataSet.Tables["issue_bonafide"].Rows[0]["subcast_name"].ToString() );
                        txtsaralno.Text = dataSet.Tables["issue_bonafide"].Rows[0]["saral_id"].ToString();
                        txtAadhar.Text = dataSet.Tables["issue_bonafide"].Rows[0]["aadhar_no"].ToString();
                        txtbonafideno.Text = bonafide.bonafideno;
                        txtgender.Text = dataSet.Tables["issue_bonafide"].Rows[0]["gender"].ToString();
                        txtissue.Value = dataSet.Tables["issue_bonafide"].Rows[0]["issue"].ToString();
                        Session["standard"] = dataSet.Tables["issue_bonafide"].Rows[0]["class_id"].ToString();                      
                        if (txtstandard.ToString().Contains("10"))
                        {
                        }                      
                        if (dataSet.Tables["issue_bonafide"].Rows[0]["month"].ToString().Contains("--"))
                        {
                            ddlssc.SelectedIndex = 0;
                        }
                        else
                        {
                            chkssc.Checked = true;
                            ddlssc.Enabled = true;
                            ddlssc1.Enabled = true;
                            ddlssc.SelectedValue = dataSet.Tables["issue_bonafide"].Rows[0]["month"].ToString().Trim();
                        }
                        if (dataSet.Tables["issue_bonafide"].Rows[0]["year"].ToString().Contains("--"))
                        {
                            ddlssc1.SelectedIndex = 0;
                        }
                        else
                        {
                            ddlssc1.SelectedValue = dataSet.Tables["issue_bonafide"].Rows[0]["year"].ToString().Trim();
                        }
                        if (dataSet.Tables["issue_bonafide"].Rows[0]["other_std"].ToString().Contains("--"))
                        {

                            ddlotherstd.SelectedIndex = 0;
                        }
                        else
                        {
                            chkotherstandard.Checked = true;
                            ddlotherstd.Enabled = true;
                            string otherstd = dataSet.Tables["issue_bonafide"].Rows[0]["other_std"].ToString();
                            chkotherstandard_CheckedChanged(sender, e);
                            ddlotherstd.SelectedValue = dataSet.Tables["issue_bonafide"].Rows[0]["other_std"].ToString().Trim();
                            chkotherstandard.Checked = true;
                        }
                        btns.Visible = true;
                        btnc.Visible = true;
                        btn_print.Visible = true;
                    }
                }
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "disp_confirm", "$('#modal1').modal('hide');", true);
            }

        }
        catch (Exception ex) {
            Response.Redirect("Login.aspx");
        }
    }   
    //protected void btn_print_Click(object sender, EventArgs e)
    //{
    //    ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "click", @"<script>window.open('Bonafide_report_secondary.aspx','_newtab');</script>", false);


    //}
    protected void btnclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("Bonafide_Certificate.aspx");
    }
    protected void grid2_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[4].Visible = false;
    }
}