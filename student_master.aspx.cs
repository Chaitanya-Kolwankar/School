using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Drawing;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Configuration;
using System.Globalization;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Web.Services;

public partial class student_master : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ConnectionString);
    SqlCommand cmd = new SqlCommand();
    Class1 cls = new Class1();
    Student_mast stdms = new Student_mast();
    common sbm = new common();

    string msg;


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
        stdms.AYID = null;
        stdms.date_of_admission = null;
        stdms.modified_date = null;
        stdms.delflag = null;
        stdms.username = null;
        stdms.get_form_id = null;
        stdms.form_id_new = null;
        stdms.medium = null;
        stdms.gr_no = null;
        stdms.date = null;
        stdms.medium1 = null;
        stdms.stud_class = null;
        stdms.surname = null;
        stdms.name = null;
        stdms.father_name = null;
        stdms.mo_name = null;
        stdms.gender = null;
        stdms.address = null;
        stdms.residence_no = null;
        stdms.mob_no = null;
        stdms.co_no = null;
        stdms.aadhar_no = null;
        stdms.DOB = null;
        stdms.nationality = null;
        stdms.mother_tongue = null;
        stdms.age = null;
        stdms.birth_place = null;
        stdms.category = null;
        stdms.religion = null;
        stdms.caste = null;
        stdms.subcaste = null;
        stdms.last_schl = null;
        stdms.last_std = null;
        stdms.per = null;
        stdms.grade = null;
        stdms.vehical_type = null;
        stdms.vehical_no = null;
        stdms.driver_mob = null;
        stdms.b_acc_no = null;
        stdms.b_name = null;
        stdms.ifsc_code = null;
        stdms.branch_name = null;
        stdms.image = null;
        stdms.new_admission = null;
        stdms.saral = null;
        stdms.student_photo = null;
        stdms.type_of_query = null;
        stdms.classs = null;
        stdms.categories = null;
        stdms.cast = null;
        stdms.subcast = null;
        stdms.msg = null;
        stdms.cancel_remark = null;
        stdms.admission_recover = null;
        stdms.student_id = null;
        stdms.form_no = null;
        stdms.medium_name = null;
        stdms.state = null;
        stdms.district = null;
        stdms.taluka = null;
        stdms.pincode = null;
        stdms.student_sign = null;
    }

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
                try
                {

                    if (Convert.ToString(Session["emp_id"]) == "")
                    {
                        Response.Redirect("Login.aspx");
                    }
                    else
                    {
                        stud_photo.ImageUrl = "~/image/" + Path.GetFileName("user.png");
                        stud_sign.ImageUrl = "~/image/" + Path.GetFileName("sign.png");


                        string type = "ddlfill";

                        ddlclass.Enabled = false;
                        save.Text = "Save";
                        admission.Visible = false;

                        string urlalias = cls.urls();
                        string url = @urlalias + "Common/";

                        sbm.type = type.ToString();
                        string jsonString = JsonHelper.JsonSerializer<common>(sbm);


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
                            ddlmedium.Items.Insert(0, "--Medium--");
                            ddlmedium.SelectedIndex = 0;
                            //-- to load category, caste, subcaste
                            loaddata();

                            Page.Form.Attributes.Add("enctype", "multipart/form-data");
                        }
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                }
            }
        }
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "loaddate()", true);
    }
    
    protected void ddlmedium_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlmedium.SelectedIndex>0)
            {
                DataSet ds = (DataSet)Session["DSLIST"];

                foreach (DataTable table in ds.Tables)
                {
                    if (table.TableName.Equals(ddlmedium.SelectedValue))
                    {
                        ddlclass.DataSource = table;
                        ddlclass.DataTextField = "std_name";
                        ddlclass.DataValueField = "std_id";
                        ddlclass.DataBind();
                        ddlclass.Enabled = true;
                        ddlclass.Items.Insert(0, "--Standard--");
                        ddlclass.SelectedIndex = 0;
                    }
                }
            }
            else
            {
                ddlclass.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void save_Click(object sender, EventArgs e)//chinmay
    {
        try
        {
            string rootpath = Server.MapPath(cls.imageFolder());
            string subPath = cls.imageFolder();
            bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));
            if (!exists)
            {
                System.IO.Directory.CreateDirectory(Server.MapPath(subPath));
                System.IO.Directory.CreateDirectory(Server.MapPath(subPath + "\\Admission"));
                System.IO.Directory.CreateDirectory(Server.MapPath(subPath + "\\Student"));
                System.IO.Directory.CreateDirectory(Server.MapPath(subPath + "\\Staff"));
            }

            txtform_id.Enabled = true;
            string str = validate();
            if (str.ToString() == "")
            {
                string urlalias = cls.urls();
                string url = @urlalias + "stud_mast/";
                useless();
                if (save.Text == "Save")
                {
                    stdms.type_of_query = "save";
                    stdms.medium_name = ddlmedium.SelectedItem.Text.Trim().Substring(0, 1);
                }
                else if (save.Text == "Update" && txtform_id.Text.ToString().Trim() != "")
                {
                    string name = "";
                    string path = "";
                    stdms.type_of_query = "update";
                    if (lblstudid.Text != "" && lblstudid.Text != null && lblstudid.Text.ToString().Trim().Length == 8)
                    {
                        name = lblstudid.Text.ToString().Trim();
                        path = "Student";
                    }
                    else
                    {
                        name = txtform_id.Text.ToString().Trim();
                        path = "Admission";
                    }
                    if (txtform_id.Text.ToString().Trim().Length == 7)
                    {
                        stdms.form_no = txtform_id.Text.ToString().Trim();
                    }
                    else
                    {
                        stdms.form_no = "";
                    }
                    if (txtform_id.Text.ToString().Trim().Length == 8)
                    {
                        stdms.student_id = txtform_id.Text.ToString().Trim();
                    }
                    else
                    {
                        stdms.student_id = "";
                    }

                    string photo = stud_photo.ImageUrl;
                    string nameImage = photo.Substring(photo.LastIndexOf('\\') + 1);

                    if (get_photo.HasFile)
                    {
                        get_photo.SaveAs(rootpath + "\\" + path.ToString() + "\\" + name.ToString() + "_photo.jpg");
                        stdms.student_photo = cls.imageFolder() + "/" + path.ToString() + "/" + name.ToString() + "_photo.jpg";
                    }
                    if (get_sign.HasFile)
                    {
                        get_sign.SaveAs(rootpath + "\\" + path.ToString() + "\\" + name.ToString() + "_sign.jpg");
                        stdms.student_sign = cls.imageFolder() + "/" + path.ToString() + "/" + name.ToString() + "_sign.jpg";
                    }
                }

                stdms.new_admission = "1";//
                stdms.AYID = Session["acdyear"].ToString();//
                stdms.username = Session["emp_id"].ToString();//
                stdms.date_of_admission = txtdate_admission.Text.Trim();//
                stdms.medium = ddlmedium.SelectedValue.ToString();//
                stdms.stud_class = ddlclass.SelectedValue.ToString();//
                if (txtsaral.Text.ToString().Trim() != "")
                {
                    stdms.saral = txtsaral.Text.Trim();//
                }
                if (rbfemale.Checked == true)//
                {
                    stdms.gender = "Female";
                }
                else if (rbmale.Checked == true)//
                {
                    stdms.gender = "Male";
                }
                else if (rbtrans.Checked == true)//
                {
                    stdms.gender = "Transgender";
                }

                stdms.surname = txtsurname.Text.Trim().ToUpper();//
                stdms.name = txtname.Text.Trim().ToUpper();//
                stdms.father_name = txtfname.Text.Trim().ToUpper();//
                stdms.mo_name = txtmo_name.Text.Trim().ToUpper();//
                stdms.address = txtpersent_addr.Text.Trim().ToUpper();//
                stdms.pincode = txtpincode.Text.Trim();//
                stdms.taluka = txttaluka.Text.Trim().ToUpper();//
                stdms.district = txtdistrict.Text.Trim().ToUpper();//
                stdms.state = ddlstate.Text.Trim().ToUpper();//
                stdms.mob_no = txtmob.Text.Trim();//
                stdms.residence_no = txtres_no.Text.Trim();//
                stdms.DOB = txtdob.Text.Trim();//
                stdms.co_no = txtco_mob.Text.Trim();//
                stdms.aadhar_no = txtaadhar.Text.Trim();//
                stdms.nationality = txtnationality.Text.Trim().ToUpper();//
                                                                         //stdms.birth_place = txtb_place.Text.Trim().ToUpper();//
                string birthplaceconcat;
                birthplaceconcat = txt_vilgecity.Text.Trim().ToUpper() + ',' + txt_taluka1.Text.Trim().ToUpper() + ',' + txt_distrct1.Text.Trim().ToUpper() + ',' + txt_state1.Text.Trim().ToUpper() + ',' + txt_country1.Text.Trim().ToUpper();
                stdms.birth_place = birthplaceconcat;
                stdms.mother_tongue = txtmother_tongue.Text.Trim().ToUpper();//
                if (ddlreligion.SelectedIndex > 0)//
                {
                    stdms.religion = ddlreligion.SelectedValue.ToString();//
                }
                if (ddlcategory.SelectedIndex > 0)//
                {
                    stdms.category = ddlcategory.SelectedValue.ToString();//
                }
                if (ddlcaste.SelectedIndex > 0)//
                {
                    stdms.caste = ddlcaste.SelectedValue.ToString();//
                }
                if (ddlsubcaste.SelectedIndex > 0)//
                {
                    stdms.subcaste = ddlsubcaste.SelectedValue.ToString();//
                }
                if (txt_last_schl.Text.ToString() != "")
                {
                    stdms.last_schl = txt_last_schl.Text.Trim().ToUpper();//
                }
                if (txt_last_std.Text.ToString() != "")
                {
                    stdms.last_std = txt_last_std.Text.Trim().ToUpper();//
                }
                if (txtgrade.Text.ToString() != "")
                {
                    stdms.grade = txtgrade.Text.Trim().ToUpper();//
                }
                if (txtper.Text.ToString() != "")
                {
                    stdms.per = txtper.Text.Trim();//
                }
                if (txtbank_name.Text.ToString() != "")
                {
                    stdms.b_name = txtbank_name.Text.Trim().ToUpper();//
                }
                if (txt_branch.Text.ToString() != "")
                {
                    stdms.branch_name = txt_branch.Text.Trim().ToUpper();//
                }
                if (txt_IFSC.Text.ToString() != "")
                {
                    stdms.ifsc_code = txt_IFSC.Text.Trim();//
                }
                if (txt_bank_acc.Text.ToString() != "")
                {
                    stdms.b_acc_no = txt_bank_acc.Text.Trim();//
                }
                if (txtvehical.Text.ToString() != "")
                {
                    stdms.vehical_type = txtvehical.Text.Trim().ToUpper();//
                }
                if (txtvehical_no.Text.ToString() != "")
                {
                    stdms.vehical_no = txtvehical_no.Text.Trim().ToUpper();//
                }
                if (txtdriver.Text.ToString() != "")
                {
                    stdms.driver_mob = txtdriver.Text.Trim();//
                }

                string jsonString = JsonHelper.JsonSerializer<Student_mast>(stdms);

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
                    var result = streamReader.ReadToEnd();
                    DataSet ds = JsonConvert.DeserializeObject<DataSet>(result);

                    msg = ds.Tables[0].Rows[0]["msg"].ToString();
                    if (msg.ToString().Contains("No") != true && save.Text == "Save")
                    {
                        useless();
                        bool start = false;
                        if (get_photo.HasFile)
                        {
                            get_photo.SaveAs(rootpath + "\\Admission\\" + msg.ToString() + "_photo.jpg");
                            stdms.student_photo = cls.imageFolder() + "/Admission/" + msg.ToString() + "_photo.jpg";
                            start = true;
                        }
                        if (get_sign.HasFile)
                        {
                            get_sign.SaveAs(rootpath + "\\Admission\\" + msg.ToString() + "_sign.jpg");
                            stdms.student_sign = cls.imageFolder() + "/Admission/" + msg.ToString() + "_sign.jpg";
                            start = true;
                        }
                        if (start == true)
                        {
                            string url1 = @urlalias + "stud_mast/";
                            stdms.type_of_query = "updateimagepath";
                            stdms.form_no = msg.ToString();

                            string jsonString1 = JsonHelper.JsonSerializer<Student_mast>(stdms);
                            var httpWebRequest1 = (HttpWebRequest)WebRequest.Create(url1);
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
                            }
                        }
                        msg = "Form Id is " + msg.ToString();

                    }
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify('" + msg + "', { color: '#fff', background: '#198104', blur: 0.2, delay: 0 })", true);
                    clearpage();

                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify('" + str + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
            }

        }

        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void clear_button_Click(object sender, EventArgs e)
    {
        clearpage();
    }

    public void clearpage()
    {
        save.Enabled = true;
        txtform_id.Enabled = true;
        ddlmedium.Enabled = true;
        txtdate_admission.Text = "";
        txtsaral.Text = "";
        txtsurname.Text = "";
        txtname.Text = "";
        txtfname.Text = "";
        txtmo_name.Text = "";
        txtpersent_addr.Text = "";
        txtres_no.Text = "";
        txtmob.Text = "";
        txtco_mob.Text = "";
        txtaadhar.Text = "";
        rbmale.Checked = false;
        rbfemale.Checked = false;
        rbtrans.Checked = false;
        txtdob.Text = "";
        txtage.Text = "";
        // txtb_place.Text = "";
        txt_vilgecity.Text = "";
        txt_taluka1.Text = "";
        txt_distrct1.Text = "";
        txt_state1.Text = "";
        txt_country1.Text = "";
        txtmother_tongue.Text = "";
        txtnationality.Text = "";
        txtbank_name.Text = "";
        txt_branch.Text = "";
        txt_bank_acc.Text = "";
        txt_IFSC.Text = "";
        txt_last_schl.Text = "";
        txt_last_std.Text = "";
        txtper.Text = "";
        txtgrade.Text = "";
        txtvehical.Text = "";
        txtvehical_no.Text = "";
        txtdriver.Text = "";
        ddlmedium.SelectedIndex = 0;
        if (ddlclass.SelectedIndex > 0)
        {
            ddlclass.SelectedIndex = 0;
            ddlclass.DataSource = null;
            ddlclass.DataBind();
        }
        ddlstate.SelectedIndex = 0;
        ddlcategory.SelectedIndex = 0;
        ddlreligion.SelectedIndex = 0;
        ddlclass.Enabled = false;
        if (ddlcaste.SelectedIndex > 0)
        {
            ddlcaste.SelectedIndex = 0;
            ddlcaste.DataSource = null;
            ddlcaste.DataBind();
        }
        if (ddlsubcaste.SelectedIndex > 0)
        {
            ddlsubcaste.SelectedIndex = 0;
            ddlsubcaste.DataSource = null;
            ddlsubcaste.DataBind();
        }
        txtform_id.Text = "";
        txtpincode.Text = "";
        txttaluka.Text = "";
        txtdistrict.Text = "";
        ddlstate.Text = "";
        stud_photo.ImageUrl = "~/image/" + Path.GetFileName("user.png");
        stud_sign.ImageUrl = "~/image/" + Path.GetFileName("sign.png");
        save.Text = "Save";
        admission.Visible = false;
        Session["STUD"] = "";
        Session["LOAD"] = "";
        lblstudid.Text = "";
    }

    public string CalculateYourAge(DateTime Dob)
    {
        DateTime Now = DateTime.Now;
        DateTime currentDate = DateTime.Now;
        DateTime compareDate = Convert.ToDateTime(Dob, new CultureInfo("en-GB"));
        int Months = 0;
        int Years = 0;
     
            if (currentDate > compareDate)
            {
                Years = new DateTime(DateTime.Now.Subtract(Dob).Ticks).Year - 1;
                DateTime PastYearDate = Dob.AddYears(Years);

                for (int i = 1; i <= 12; i++)
                {
                    if (PastYearDate.AddMonths(i) == Now)
                    {
                        Months = i;
                        break;
                    }
                    else if (PastYearDate.AddMonths(i) >= Now)
                    {
                        Months = i - 1;
                        break;
                    }
                }
            }
        return String.Format(" {0} Years - {1} Months ", Years, Months);
    }

    protected void ddlcategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlcategory.SelectedItem.Text != "Category")
            {
                string ic = ddlcategory.SelectedValue;

                DataSet ds = (DataSet)Session["DDLALL"];


                DataView dcast = ds.Tables[1].DefaultView;

                if (dcast.Table.Rows.Count > 0)
                {
                    dcast.RowFilter = "category_id = '" + ic + "'";
                }

                ddlcaste.DataSource = dcast;
                ddlcaste.DataTextField = "cast_name";
                ddlcaste.DataValueField = "cast_id";
                ddlcaste.DataBind();

                ddlcaste.Items.Insert(0, "Caste");
                ddlcaste.SelectedIndex = 0;

                if (ddlsubcaste.SelectedIndex > 0)
                {
                    ddlsubcaste.SelectedIndex = 0;
                    ddlsubcaste.DataSource = null;
                    ddlsubcaste.DataBind();
                    ddlsubcaste.Enabled = false;
                }

                if (txtform_id.Text.Trim() != "" && save.Text=="Update")
                {
                    DataTable dt1 = ViewState["dtcategory"] as DataTable;
                    dt1.Rows[0]["category"] = ddlcategory.SelectedValue.ToString();
                    ViewState["dtcategory"] = dt1;
                }
            }

            else
            {

            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void ddlcaste_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlcaste.SelectedItem.Text != "Caste")
            {
                ddlsubcaste.Enabled = true;
                string ic = ddlcaste.SelectedValue;

                DataSet ds = (DataSet)Session["DDLALL"];


                DataView dsubcast = ds.Tables[2].DefaultView;

                if (dsubcast.Table.Rows.Count > 0)
                {
                    dsubcast.RowFilter = "cast_id = '" + ic + "'";
                }

                ddlsubcaste.DataSource = dsubcast;
                ddlsubcaste.DataTextField = "subcast_name";
                ddlsubcaste.DataValueField = "subcast_id";
                ddlsubcaste.DataBind();

                ddlsubcaste.Items.Insert(0, "Subcaste");
                ddlsubcaste.SelectedIndex = 0;

                if (txtform_id.Text.Trim() != "" && save.Text == "Update")
                {
                    DataTable dt1 = ViewState["dtcategory"] as DataTable;
                    dt1.Rows[0]["caste"] = ddlcaste.SelectedValue.ToString();
                    ViewState["dtcategory"] = dt1;
                }
            }

            else
            {
                ddlsubcaste.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }

    }

    protected void txtform_id_TextChanged(object sender, EventArgs e)
    {
        if (txtform_id.Enabled == true)
        {
            try
            {
                ViewState["dtcategory"] = null;
                useless();
                string stud_id = "";
                string frm_id = "";
                string urlalias = cls.urls();

                if (txtform_id.Text.Trim().Length == 7)
                {
                    frm_id = txtform_id.Text.Trim();
                    txtform_id.Enabled = false;
                    ddlmedium.Enabled = false;
                    ddlclass.Enabled = false;
                }
                else if (txtform_id.Text.Trim().Length == 8)
                {
                    stud_id = txtform_id.Text.Trim();
                    txtform_id.Enabled = false;
                    ddlmedium.Enabled = false;
                    ddlclass.Enabled = false;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify(' Enter Proper ID', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                    return;
                }

                string url1 = @urlalias + "stud_mast/";

                stdms.type_of_query = "retrive";
                stdms.form_no = frm_id.ToString();
                stdms.student_id = stud_id.ToString();

                string jsonString1 = JsonHelper.JsonSerializer<Student_mast>(stdms);
                var httpWebRequest1 = (HttpWebRequest)WebRequest.Create(url1);
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
                    string ck;
                    string result1 = sr1.ReadToEnd();
                    DataSet ds1 = JsonConvert.DeserializeObject<DataSet>(result1);
                    DataTable dtnew = ds1.Tables["load"];
                    Session["LOAD"] = ds1;

                    if (dtnew.Rows.Count > 0)
                    {
                        stud_id = dtnew.Rows[0]["Student_id"].ToString().ToUpper();
                        if (stud_id.ToString() != "" && stud_id.ToString() != null)
                        {
                            lblstudid.Text = dtnew.Rows[0]["Student_id"].ToString().ToUpper();
                        }
                        txtdate_admission.Text = Convert.ToDateTime(dtnew.Rows[0]["date_of_admission"]).ToString("dd/MM/yyyy").Replace("-", "/");
                        txtfname.Text = dtnew.Rows[0]["stud_m_name"].ToString().ToUpper();
                        txtname.Text = dtnew.Rows[0]["stud_F_name"].ToString().ToUpper();
                        txtmo_name.Text = dtnew.Rows[0]["stud_mo_name"].ToString().ToUpper();
                        txtsurname.Text = dtnew.Rows[0]["stud_L_name"].ToString().ToUpper();
                        txtpersent_addr.Text = dtnew.Rows[0]["address"].ToString().ToUpper();
                        txtmob.Text = dtnew.Rows[0]["phone_no1"].ToString();
                        txtdob.Text = Convert.ToDateTime(dtnew.Rows[0]["dob"]).ToString("dd/MM/yyyy").Replace("-", "/");
                        ck = dtnew.Rows[0]["new_admission"].ToString();
                        // txtb_place.Text = dtnew.Rows[0]["birth_place"].ToString().ToUpper().ToUpper();
                        string[] array = dtnew.Rows[0]["birth_place"].ToString().Split(',');

                        if (array.Length == 5)
                        {

                            txt_vilgecity.Text = array[0];
                            txt_taluka1.Text = array[1];
                            txt_distrct1.Text = array[2];
                            txt_state1.Text = array[3];
                            txt_country1.Text = array[4];
                        }
                        else if (array.Length == 4)
                        {
                            txt_vilgecity.Text = array[0];
                            txt_taluka1.Text = array[1];
                            txt_distrct1.Text = array[2];
                            txt_state1.Text = array[3];


                        }
                        else if (array.Length == 3)
                        {
                            txt_vilgecity.Text = array[0];
                            txt_taluka1.Text = array[1];
                            txt_distrct1.Text = array[2];
                        }
                        else if (array.Length == 2)
                        {
                            txt_vilgecity.Text = array[0];
                            txt_taluka1.Text = array[1];
                        }
                        else if (array.Length == 1)
                        {
                            txt_vilgecity.Text = array[0];
                        }

                        txtmother_tongue.Text = dtnew.Rows[0]["mother_tongue"].ToString().ToUpper();
                        txtnationality.Text = dtnew.Rows[0]["nationality"].ToString().ToUpper();
                        txt_last_schl.Text = dtnew.Rows[0]["last_school_name"].ToString().ToUpper();
                        txt_last_std.Text = dtnew.Rows[0]["last_studied_std"].ToString().ToUpper();
                        txtper.Text = dtnew.Rows[0]["percentage"].ToString();
                        txtgrade.Text = dtnew.Rows[0]["grade"].ToString().ToUpper();
                        txtco_mob.Text = dtnew.Rows[0]["co_mobile_no"].ToString();
                        txtaadhar.Text = dtnew.Rows[0]["aadhar_no"].ToString();
                        txtvehical.Text = dtnew.Rows[0]["vehicle_type"].ToString().ToUpper();
                        txtvehical_no.Text = dtnew.Rows[0]["vehicle_no"].ToString().ToUpper();
                        txtdriver.Text = dtnew.Rows[0]["driver_no"].ToString();
                        txtsaral.Text = dtnew.Rows[0]["saral_id"].ToString();
                        txt_bank_acc.Text = dtnew.Rows[0]["bank_ac_no"].ToString();
                        txtbank_name.Text = dtnew.Rows[0]["bank_name"].ToString().ToUpper();
                        txt_IFSC.Text = dtnew.Rows[0]["IFSC_code"].ToString();
                        txt_branch.Text = dtnew.Rows[0]["Branch_name"].ToString().ToUpper();
                        txtres_no.Text = dtnew.Rows[0]["phone_no2"].ToString();
                        txttaluka.Text = dtnew.Rows[0]["Taluka"].ToString().ToUpper();
                        ddlstate.SelectedIndex = ddlstate.Items.IndexOf(ddlstate.Items.FindByText(dtnew.Rows[0]["state"].ToString().ToUpper()));
                        txtdistrict.Text = dtnew.Rows[0]["dist"].ToString().ToUpper();
                        txtpincode.Text = dtnew.Rows[0]["pincode"].ToString().ToUpper();

                        if (txtdob.Text != null)
                        {
                            string agecal;
                            DateTime now;
                            int result;
                            now = DateTime.Now;
                            agecal = txtdob.Text.ToString().Split('/')[2];
                            result = Convert.ToInt32(now.Year) - Convert.ToInt32(agecal);
                            txtage.Text = CalculateYourAge(DateTime.ParseExact(txtdob.Text, "d/M/yyyy", CultureInfo.InvariantCulture));
                        }

                        DataSet ds2 = (DataSet)Session["DSLIST"];

                        DataTable dt1 = ds2.Tables[0];
                        ddlmedium.DataSource = dt1;
                        ddlmedium.DataTextField = "medium";
                        ddlmedium.DataValueField = "med_id";
                        ddlmedium.DataBind();
                        ddlmedium.Items.Insert(0, "--Select--");
                        ddlmedium.Items.FindByText(ddlmedium.SelectedValue = dtnew.Rows[0]["medium_id"].ToString());


                        foreach (DataTable table in ds2.Tables)
                        {
                            if (table.TableName.Equals(ddlmedium.SelectedValue))
                            {
                                ddlclass.DataSource = table;
                                ddlclass.DataTextField = "std_name";
                                ddlclass.DataValueField = "std_id";
                                ddlclass.DataBind();
                                ddlclass.Enabled = true;

                                ddlclass.Items.Insert(0, "--Select--");
                                ddlclass.Items.Equals(ddlclass.SelectedValue = dtnew.Rows[0]["class_id"].ToString());
                            }
                        }

                        DataSet ds = (DataSet)Session["DDLALL"];
                        DataTable dtcategory = new DataTable();
                        dtcategory.Columns.Add("category");
                        dtcategory.Columns.Add("caste");
                        dtcategory.Columns.Add("sub_caste");
                        dtcategory.Columns.Add("religion");

                        DataRow dr = dtcategory.NewRow();

                        //religion
                        ddlreligion.DataSource = ds.Tables[3];
                        ddlreligion.DataTextField = "religion";
                        ddlreligion.DataValueField = "religion_id";
                        ddlreligion.DataBind();
                        ddlreligion.Items.Insert(0, "Religion");

                        dr[3] = dtnew.Rows[0]["religion"].ToString();

                        ListItem itemR = ddlreligion.Items.FindByValue(dtnew.Rows[0]["religion"].ToString());
                        if (itemR != null)
                        {
                            ddlreligion.Items.FindByValue(dtnew.Rows[0]["religion"].ToString()).Selected = true;
                        }
                        else
                        {
                            ddlreligion.SelectedIndex = 0;
                        }

                        //category
                        DataTable dt = ds.Tables[0];

                        ddlcategory.DataSource = dt;
                        ddlcategory.DataTextField = "category_name";
                        ddlcategory.DataValueField = "category_id";
                        ddlcategory.DataBind();
                        ddlcategory.Items.Insert(0, "Category");


                        dr[0] = dtnew.Rows[0]["category"].ToString();

                        ListItem item = ddlcategory.Items.FindByValue(dtnew.Rows[0]["category"].ToString());
                        if (item != null)
                        {
                            ddlcategory.Items.FindByValue(dtnew.Rows[0]["category"].ToString()).Selected = true;
                        }
                        else
                        {
                            ddlcategory.SelectedIndex = 0;
                        }

                        //ddlcaste
                        DataView dc = ds.Tables[1].DefaultView;
                        string ic = ddlcategory.SelectedValue.ToString();

                        if (dc.Table.Rows.Count > 0)
                        {
                            if (ic == "Category")
                            {
                                ddlcategory.SelectedIndex = 0;
                            }

                            else
                            {
                                dc.RowFilter = "category_id = '" + ic + "'";
                            }

                           
                        }

                        ddlcaste.DataSource = dc;
                        ddlcaste.DataTextField = "cast_name";
                        ddlcaste.DataValueField = "cast_id";
                        ddlcaste.DataBind();
                        ddlcaste.Items.Insert(0, "Caste");
                        dr[1] = dtnew.Rows[0]["caste"].ToString();
                        ListItem item1 = ddlcaste.Items.FindByValue(dtnew.Rows[0]["caste"].ToString());
                        if (item1 != null)
                        {
                            ddlcaste.Items.FindByValue(dtnew.Rows[0]["caste"].ToString()).Selected = true;
                        }
                        else
                        {
                            ddlcaste.SelectedIndex = 0;
                        }

                        string isc = ddlcaste.SelectedValue;

                        DataView dsc = ds.Tables[2].DefaultView;

                        if (dsc.Table.Rows.Count > 0)
                        {
                            if (isc == "Caste")

                            {
                                ddlcaste.SelectedIndex = 0;
                            }
                            else
                            {
                                dsc.RowFilter = "cast_id = '" + isc + "'";
                            }

                           
                        }

                        ddlsubcaste.DataSource = dsc;
                        ddlsubcaste.DataTextField = "subcast_name";
                        ddlsubcaste.DataValueField = "subcast_id";
                        ddlsubcaste.DataBind();
                        ddlsubcaste.Items.Insert(0, "Subcaste");
                        dr[2] = dtnew.Rows[0]["sub_caste"].ToString();
                        ListItem item3 = ddlsubcaste.Items.FindByValue(dtnew.Rows[0]["sub_caste"].ToString());
                        if (item3 != null)
                        {
                            ddlsubcaste.Items.FindByValue(dtnew.Rows[0]["sub_caste"].ToString()).Selected = true;
                        }
                        else
                        {
                            ddlsubcaste.SelectedIndex = 0;
                        }

                        dtcategory.Rows.Add(dr);
                        ViewState["dtcategory"] = dtcategory;

                        if (dtnew.Rows[0]["gender"].ToString().ToUpper() == "MALE")
                        {
                            rbmale.Checked = true;
                            rbfemale.Checked = false;
                            rbtrans.Checked = false;
                        }
                        else if (dtnew.Rows[0]["gender"].ToString().ToUpper() == "FEMALE")
                        {
                            rbfemale.Checked = true;
                            rbmale.Checked = false;
                            rbtrans.Checked = false;
                        }
                        else if (dtnew.Rows[0]["gender"].ToString().ToUpper() == "TRANSGENDER")
                        {
                            rbfemale.Checked = false;
                            rbmale.Checked = false;
                            rbtrans.Checked = true;
                        }

                        if (dtnew.Rows[0]["student_photo"].ToString() != "" && dtnew.Rows[0]["student_photo"].ToString() != null)
                        {
                            stud_photo.ImageUrl = ResolveUrl(String.Format(@"~/{0}", dtnew.Rows[0]["student_photo"].ToString()));
                            lblphoto.Text = String.Format(@"..{0}", stud_photo.ImageUrl);
                        }
                        if (dtnew.Rows[0]["student_sign"].ToString() != "" && dtnew.Rows[0]["student_sign"].ToString() != null)
                        {
                            stud_sign.ImageUrl = ResolveUrl(String.Format(@"~/{0}", dtnew.Rows[0]["student_sign"].ToString()));
                            lblsign.Text = String.Format(@"..{0}", stud_sign.ImageUrl);
                        }

                        save.Text = "Update";
                        admission.Visible = true;

                        if (stud_id != "" && dtnew.Rows[0]["del_flag"].ToString() == "False")
                        {
                            admission.Text = "Admission Cancel";
                            ddlclass.Enabled = false;
                        }
                        else if (stud_id != "" && dtnew.Rows[0]["del_flag"].ToString() == "True")
                        {
                            ddlclass.Enabled = false;
                            save.Enabled = false;
                            admission.Text = "Admission Recover";
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify('Student admission is Cancelled', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                        }
                        if (stud_id == "")
                        {
                            ddlclass.Enabled = true;
                            admission.Text = "Admission Confirm";
                        }
                    }
                    else
                    {
                        ddlmedium.Enabled = true;
                        ddlclass.Enabled = true;
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify(' No data found', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                        txtform_id.Enabled = true;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
            }
        }
    }

    public string validate()
    {
        string msg="";
        if (txtdate_admission.Text == "")
        {
            msg = "Select Date of Admission";
            return msg;
        }
        else if (ddlmedium.SelectedIndex == 0)
        {
            msg = "Select the Medium";
            return msg;
        }
        else if (ddlclass.SelectedIndex == 0)
        {
            msg = "Select the Class";
            return msg;
        }
        else if (txtsaral.Text != "" && txtsaral.Text.Length != 19)
        {
            msg = "Enter Valid Saral Number(19 Digits)";
            return msg;
        }
        else if (rbmale.Checked == false && rbfemale.Checked == false && rbtrans.Checked == false)
        {
            msg = "Select Student Gender";
            return msg;
        }
        else if (txtsurname.Text == "")
        {
            msg = "Enter Surname";
            return msg;
        }
        else if (txtname.Text == "")
        {
            msg = "Enter First Name";
            return msg;
        }
        else if (txtfname.Text == "")
        {
            msg = "Enter Father Name";
            return msg;
        }
        else if (txtmo_name.Text == "")
        {
            msg = "Enter Mother Name";
            return msg;
        }
        else if (txtpersent_addr.Text == "")
        {
            msg = "Enter Address";
            return msg;
        }
        else if (txtpincode.Text == "")
        {
            msg = "Enter Pincode";
            return msg;
        }
        else if (txtpincode.Text.Length != 6)
        {
            msg = "Enter Valid Pincode";
            return msg;
        }
        else if (txttaluka.Text == "")
        {
            msg = "Enter Taluka";
            return msg;
        }
        else if (txtdistrict.Text == "")
        {
            msg = "Enter District";
            return msg;
        }
        else if (ddlstate.SelectedIndex == 0)
        {
            msg = "Select State";
            return msg;
        }
        else if (txtmob.Text == "" || txtmob.Text.Length < 10)
        {
            msg = "Enter Mobile No";
            return msg;
        }
        else if (txtres_no.Text != "" && txtres_no.Text.Length < 6)
        {
            msg = "Invalid Telephone no";
            return msg;
        }
        else if (txtdob.Text == "")
        {
            msg = "Select Date of Birth";
            return msg;
        }
        else if (txtco_mob.Text != "" && txtco_mob.Text.Length != 10)
        {
            msg = "Invalid Alternate Contact no";
            return msg;
        }
        //samiya
        //else if (txtaadhar.Text == "")
        //{
        //    msg = "Enter Aadhar Card No";
        //    return msg;
        //}
        else if (txtaadhar.Text != "" && txtaadhar.Text.Length != 12)
        {
            msg = "Invalid Aadhar Card No";
            return msg;
        }
        //samiya
        //else if (txtnationality.Text == "")
        //{
        //    msg = "Enter Nationality";
        //    return msg;
        //}
        //else if (txtb_place.Text == "")
        //{
        //    msg = "Enter Birth Place";
        //    return msg;
        //}
        //samiya
        //else if (txt_vilgecity.Text == "")
        //{
        //    msg = "Enter Village/City in Birth Place";
        //}
        //samiya
        //else if (txtmother_tongue.Text == "")
        //{
        //    msg = "Enter Mother tonuge";
        //    return msg;
        //}
        //samiya
        //else if (ddlreligion.SelectedIndex == 0)
        //{
        //    msg = "Select the Religion";
        //    return msg;
        //}
        //samiya
        //else if (ddlcategory.SelectedIndex == 0)
        //{
        //    msg = "Select the Category";
        //    return msg;
        //}
        //samiya
        //else if (ddlcategory.SelectedIndex > 0 && ddlcaste.Enabled == true && ddlcaste.SelectedIndex == 0)
        //{
        //    msg = "Select the Caste";
        //    return msg;
        //}

        if (txt_last_schl.Text != "")
        {
            //|| txtgrade.Text == "" || txtper.Text == ""
            if (txt_last_std.Text == "" )
            {
                msg = "Enter Std in Previous school details";
                return msg;
            }
        }
        if (txt_last_std.Text != "")
        {
            //|| txtgrade.Text == ""   || txtper.Text == ""
            if (txt_last_schl.Text == "")
            {
                msg = "Enter Std in Previous school details";
                return msg;
            }
        }
        //samiya
        //if (txtgrade.Text != "")
        //{
        //    if (txt_last_schl.Text == "" || txt_last_std.Text == "" || txtper.Text == "")
        //    {
        //        msg = "Enter All the Previous school details";
        //        return msg;
        //    }
        //}
        //if (txtper.Text != "")
        //{
        //    //|| txtgrade.Text == ""
        //    if (txt_last_schl.Text == "" || txt_last_std.Text == "")
        //    {
        //        msg = "Enter All the Previous school details";
        //        return msg;
        //    }
        //}
        if (txtbank_name.Text != "")
        {
            if (txt_branch.Text == "" || txt_IFSC.Text == "" || txt_bank_acc.Text == "")
            {
                msg = "Enter All the Bank details";
                return msg;
            }
        }
        if (txt_branch.Text != "")
        {
            if (txtbank_name.Text == "" || txt_IFSC.Text == "" || txt_bank_acc.Text == "")
            {
                msg = "Enter All the Bank details";
                return msg;
            }
        }
        if (txt_IFSC.Text != "")
        {
            if (txtbank_name.Text == "" || txt_branch.Text == "" || txt_bank_acc.Text == "")
            {
                msg = "Enter All the Bank details";
                return msg;
            }
        }
        if (txt_bank_acc.Text != "")
        {
            if (txtbank_name.Text == "" || txt_branch.Text == "" || txt_IFSC.Text == "")
            {
                msg = "Enter All the Bank details";
                return msg;
            }
        }
        if (txtvehical.Text != "")
        {
            if (txtvehical_no.Text == "" || txtdriver.Text == "")
            {
                msg = "Enter All the Vehicle details";
                return msg;
            }
        }
        if (txtvehical_no.Text != "")
        {
            if (txtvehical.Text == "" || txtdriver.Text == "")
            {
                msg = "Enter All the Vehicle details";
                return msg;
            }
        }
        if (txtdriver.Text != "")
        {
            if (txtvehical.Text == "" || txtvehical_no.Text == "")
            {
                msg = "Enter All the Vehicle details";
                return msg;
            }
        }
        
        return msg;
    }

    protected void admission_Click(object sender, EventArgs e)
    {
        try
        {
            string urlalias = cls.urls();
            string url = @urlalias + "stud_mast/";
            useless();
            if (admission.Text == "Admission Confirm")
            {
                stdms.type_of_query = "stud_id";
                stdms.form_no = txtform_id.Text.ToString().Trim();
                stdms.medium_name = ddlmedium.SelectedItem.Text;
                stdms.stud_class = ddlclass.SelectedValue.ToString();
                stdms.AYID = Session["acdyear"].ToString();
                stdms.medium = ddlmedium.SelectedValue.ToString();
            }
            else if (admission.Text == "Admission Cancel")
            {
                stdms.type_of_query = "cancel";
                if (txtform_id.Text.ToString().Trim().Length == 7)
                {
                    stdms.form_no = txtform_id.Text.ToString().Trim();
                }
                else
                {
                    stdms.student_id = txtform_id.Text.ToString().Trim();
                }
                stdms.AYID = Session["acdyear"].ToString();
            }
            else if (admission.Text == "Admission Recover")
            {
                stdms.type_of_query = "recover";
                if (txtform_id.Text.ToString().Trim().Length == 7)
                {
                    stdms.form_no = txtform_id.Text.ToString().Trim();
                }
                else
                {
                    stdms.student_id = txtform_id.Text.ToString().Trim();
                }
                stdms.AYID = Session["acdyear"].ToString();
            }

            string jsonString = JsonHelper.JsonSerializer<Student_mast>(stdms);

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
                var result = streamReader.ReadToEnd();
                DataSet ds = JsonConvert.DeserializeObject<DataSet>(result);
                msg = ds.Tables[0].Rows[0]["msg"].ToString();
                if (admission.Text == "Admission Confirm" && msg.ToString().Contains("No") != true)
                {
                    string rootpath = Server.MapPath(cls.imageFolder());
                    useless();
                    bool start = false;

                    string photo = rootpath.Replace("\\", "/") + "/admission/" + txtform_id.Text + "_photo.jpg";
                    string sign = rootpath.Replace("\\", "/") + "/admission/" + txtform_id.Text + "_sign.jpg";
                    if (File.Exists(photo))
                    {
                        File.Copy(photo, rootpath.Replace("\\", "/") + "/student/" + msg.ToString() + "_photo.jpg");
                        stdms.student_photo = cls.imageFolder() + "/student/" + msg.ToString() + "_photo.jpg";
                        start = true;
                    }
                    if (File.Exists(sign))
                    {
                        File.Copy(sign, rootpath.Replace("\\", "/") + "/student/" + msg.ToString() + "_sign.jpg");
                        stdms.student_sign = cls.imageFolder() + "/student/" + msg.ToString() + "_sign.jpg";
                        start = true;
                    }
                    if (start == true)
                    {
                        string url1 = @urlalias + "stud_mast/";
                        stdms.type_of_query = "updateimagepath";
                        stdms.student_id = msg.ToString();

                        string jsonString1 = JsonHelper.JsonSerializer<Student_mast>(stdms);
                        var httpWebRequest1 = (HttpWebRequest)WebRequest.Create(url1);
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
                            //DataSet ds1 = JsonConvert.DeserializeObject<DataSet>(result1);
                        }
                    }
                    msg = "Student Id is " + msg.ToString();
                }

            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify('" + msg + "', { color: '#fff', background: '#198104', blur: 0.2, delay: 0 })", true);
            clearpage();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    //-------------------category-----------------------------------
    protected void btncategory_Click(object sender, EventArgs e)
    {
        try
        {
            string urlalias = cls.urls();
            string url = @urlalias + "stud_mast/";

            stdms.type_of_query = "categorymodal";
            string jsonString = JsonHelper.JsonSerializer<Student_mast>(stdms);
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
                DataSet dscategory = JsonConvert.DeserializeObject<DataSet>(result);
                if (dscategory.Tables[0].Rows.Count > 0)
                {
                    grdcategory.DataSource = dscategory.Tables[0];
                    grdcategory.DataBind();
                }
            }
            txtcat.Text = "";
            btnaddcat.Text = "Save";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal('modalcategory');", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void grdcategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow gvrow = grdcategory.SelectedRow;

            txtcat.Text = ((Label)gvrow.FindControl("lblname")).Text;
            lblcatid.Text = ((Label)gvrow.FindControl("lblid")).Text;
            btnaddcat.Text = "Update";
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void btnaddcat_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtcat.Text.Trim() != "")
            {
                useless();
                string urlalias = cls.urls();
                string url = @urlalias + "stud_mast/";
                if (btnaddcat.Text == "Save" && lblcatid.Text == "")
                {
                    stdms.categories = "savecategory";
                    stdms.type_of_query = "savecategory";
                    stdms.category = txtcat.Text;
                }
                else if (btnaddcat.Text == "Update" && lblcatid.Text != "")
                {
                    stdms.categories = lblcatid.Text;
                    stdms.category = txtcat.Text;
                    stdms.type_of_query = "savecategory";
                }
                string jsonString = JsonHelper.JsonSerializer<Student_mast>(stdms);
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
                    DataSet dscategory = JsonConvert.DeserializeObject<DataSet>(result);
                    if (dscategory.Tables[1].Rows.Count > 0)
                    {
                        if (dscategory.Tables[1].Rows[0][0].ToString() == "Same Category Found")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify('" + dscategory.Tables[1].Rows[0][0].ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                        }
                        else if (dscategory.Tables[1].Rows[0][0].ToString() == "Saved Successfully" || dscategory.Tables[1].Rows[0][0].ToString() == "Updated Successfully")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify('" + dscategory.Tables[1].Rows[0][0].ToString() + "', { color: '#fff', background: '#198104', blur: 0.2, delay: 0 })", true);
                            txtcat.Text = "";
                            lblcatid.Text = "";
                            btnaddcat.Text = "Save";
                            grdcategory.DataSource = dscategory.Tables[0];
                            grdcategory.DataBind();
                            loaddata();
                        }
                    }
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify('Add category to save', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void btnclear1_Click(object sender, EventArgs e)
    {
        try
        {
            txtcat.Text = "";
            lblcatid.Text = "";
            btnaddcat.Text = "Save";
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void grdcategory_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            GridViewRow gr = (GridViewRow)grdcategory.Rows[e.RowIndex];
            string count = ((Label)gr.FindControl("lblflag")).Text;
            string depcount = ((Label)gr.FindControl("categorydepcount")).Text;
            if (count == "1")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify('Category already assigned to students', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
            }
            else if (depcount=="1")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify('Caste are defined under this category', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
            }
            else
            {
                string urlalias = cls.urls();
                string url = @urlalias + "stud_mast/";

                stdms.type_of_query = "updatedeletecategory";
                stdms.category = ((Label)gr.FindControl("lblid")).Text;

                string jsonString = JsonHelper.JsonSerializer<Student_mast>(stdms);
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
                    DataSet dscategory = JsonConvert.DeserializeObject<DataSet>(result);
                    grdcategory.DataSource = dscategory.Tables[0];
                    grdcategory.DataBind();
                    loaddata();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify('" + dscategory.Tables[1].Rows[0][0].ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true); 
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    //------------------caste-----------------------------------
    protected void btncaste_Click(object sender, EventArgs e)
    {
        try
        {
            string urlalias = cls.urls();
            string url = @urlalias + "stud_mast/";

            stdms.type_of_query = "castemodal";
            string jsonString = JsonHelper.JsonSerializer<Student_mast>(stdms);
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
                DataSet dscaste = JsonConvert.DeserializeObject<DataSet>(result);
                grdcaste.DataSource = dscaste.Tables[0];
                grdcaste.DataBind();
                ddlcategorymodal.DataSource = dscaste.Tables[1];
                ddlcategorymodal.DataValueField = "category_id";
                ddlcategorymodal.DataTextField = "category_name";
                ddlcategorymodal.DataBind();
                ddlcategorymodal.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            txtcaste.Text = "";
            btnaddcaste.Text = "Save";
            lblcaste.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal('modalcaste');", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void btnaddcaste_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtcaste.Text.Trim() != "")
            {
                useless();
                string urlalias = cls.urls();
                string url = @urlalias + "stud_mast/";
                stdms.type_of_query = "savecaste";
                if (txtcaste.Text != "" && btnaddcaste.Text == "Save" && ddlcategorymodal.SelectedIndex > 0)
                {
                    stdms.caste = txtcaste.Text;
                    stdms.category = ddlcategorymodal.SelectedValue.ToString();
                    stdms.categories = "savecaste";
                }
                else if (txtcaste.Text != "" && btnaddcaste.Text == "Update" && ddlcategorymodal.SelectedIndex > 0)
                {
                    stdms.caste = txtcaste.Text;
                    stdms.category = ddlcategorymodal.SelectedValue.ToString();
                    stdms.categories = "updatecaste";
                    stdms.cast = lblcaste.Text;
                }
                string jsonString = JsonHelper.JsonSerializer<Student_mast>(stdms);
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
                    DataSet dscaste = JsonConvert.DeserializeObject<DataSet>(result);
                    if (dscaste.Tables[2].Rows[0][0].ToString() == "Same Caste Found")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify('" + dscaste.Tables[2].Rows[0][0].ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                    }
                    else if (dscaste.Tables[2].Rows[0][0].ToString() == "Saved Successfully" || dscaste.Tables[2].Rows[0][0].ToString() == "Updated Successfully")
                    {
                        grdcaste.DataSource = dscaste.Tables[0];
                        grdcaste.DataBind();
                        ddlcategorymodal.DataSource = dscaste.Tables[1];
                        ddlcategorymodal.DataValueField = "category_id";
                        ddlcategorymodal.DataTextField = "category_name";
                        ddlcategorymodal.DataBind();
                        ddlcategorymodal.Items.Insert(0, new ListItem("--Select--", "0"));
                        loaddata();
                        txtcaste.Text = "";
                        btnaddcaste.Text = "Save";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify('" + dscaste.Tables[2].Rows[0][0].ToString() + "', { color: '#fff', background: '#198104', blur: 0.2, delay: 0 })", true);
                    }
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify('Add caste to save', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void btnclear2_Click(object sender, EventArgs e)
    {
        try
        {
            txtcaste.Text = "";
            lblcaste.Text = "";
            btnaddcaste.Text = "Save";

            string urlalias = cls.urls();
            string url = @urlalias + "stud_mast/";

            stdms.type_of_query = "castemodal";
            string jsonString = JsonHelper.JsonSerializer<Student_mast>(stdms);
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
                DataSet dscaste = JsonConvert.DeserializeObject<DataSet>(result);
                grdcaste.DataSource = dscaste.Tables[0];
                grdcaste.DataBind();
                ddlcategorymodal.DataSource = dscaste.Tables[1];
                ddlcategorymodal.DataValueField = "category_id";
                ddlcategorymodal.DataTextField = "category_name";
                ddlcategorymodal.DataBind();
                ddlcategorymodal.Items.Insert(0, new ListItem("--Select--", "0"));
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void grdcaste_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow gvrow = grdcaste.SelectedRow;
            txtcaste.Text = ((Label)gvrow.FindControl("lblname")).Text;
            lblcaste.Text = ((Label)gvrow.FindControl("lblid")).Text;
            ddlcategorymodal.SelectedValue = ((Label)gvrow.FindControl("lblcatidref")).Text;
            btnaddcaste.Text = "Update";
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void grdcaste_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {

            GridViewRow gr = (GridViewRow)grdcaste.Rows[e.RowIndex];
            string count = ((Label)gr.FindControl("lblflag")).Text;
            string depcount = ((Label)gr.FindControl("castedepcount")).Text;
            if (count == "1")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify('Category already assigned to students', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
            }
            else if (depcount == "1")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify('Sub Caste are defined under this category', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
            }
            else
            {
                string urlalias = cls.urls();
                string url = @urlalias + "stud_mast/";

                stdms.type_of_query = "deletecaste";
                stdms.category = ((Label)gr.FindControl("lblcatidref")).Text;
                stdms.caste = ((Label)gr.FindControl("lblid")).Text;
                string jsonString = JsonHelper.JsonSerializer<Student_mast>(stdms);
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
                    DataSet dscaste = JsonConvert.DeserializeObject<DataSet>(result);
                    grdcaste.DataSource = dscaste.Tables[0];
                    grdcaste.DataBind();
                    ddlcategorymodal.DataSource = dscaste.Tables[1];
                    ddlcategorymodal.DataValueField = "category_id";
                    ddlcategorymodal.DataTextField = "category_name";
                    ddlcategorymodal.DataBind();
                    ddlcategorymodal.Items.Insert(0, new ListItem("--Select--", "0"));
                    loaddata();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify('" + dscaste.Tables[2].Rows[0][0].ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                }
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void ddlcategorymodal_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            useless();
            txtcaste.Text = "";
            btnaddcaste.Text = "Save";
            lblcaste.Text = "";
            string urlalias = cls.urls();
            string url = @urlalias + "stud_mast/";

            if (ddlcategorymodal.SelectedIndex > 0)
            {
                stdms.category = ddlcategorymodal.SelectedValue.ToString();
            }
            stdms.type_of_query = "castemodal";

            string jsonString = JsonHelper.JsonSerializer<Student_mast>(stdms);
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
                DataSet dscaste = JsonConvert.DeserializeObject<DataSet>(result);
                if (dscaste.Tables[0].Rows.Count > 0)
                {
                    grdcaste.DataSource = dscaste.Tables[0];
                    grdcaste.DataBind();
                }
                else
                {
                    grdcaste.DataSource = null;
                    grdcaste.DataBind();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify('No data found', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    //-------------------subcaste----------------------------------
    protected void btnsubcaste_Click(object sender, EventArgs e)
    {
        try
        {
            string urlalias = cls.urls();
            string url = @urlalias + "stud_mast/";

            stdms.type_of_query = "subcastemodal";
            string jsonString = JsonHelper.JsonSerializer<Student_mast>(stdms);
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
                DataSet dssubcaste = JsonConvert.DeserializeObject<DataSet>(result);
                grdsubcaste.DataSource = dssubcaste.Tables[0];
                grdsubcaste.DataBind();
                ddlcastemodal.DataSource = dssubcaste.Tables[1];
                ddlcastemodal.DataValueField = "cast_id";
                ddlcastemodal.DataTextField = "cast_name";
                ddlcastemodal.DataBind();
                ddlcastemodal.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            txtsubcaste.Text = "";
            lblsubcaste.Text = "";
            btnaddsub.Text = "Save";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal('modalsubcaste');", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void ddlcastemodal_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            useless();
            txtsubcaste.Text = "";
            btnaddsub.Text = "Save";
            lblsubcaste.Text = "";
            string urlalias = cls.urls();
            string url = @urlalias + "stud_mast/";

            if (ddlcastemodal.SelectedIndex > 0)
            {
                stdms.caste = ddlcastemodal.SelectedValue.ToString();
            }
            stdms.type_of_query = "subcastemodal";

            string jsonString = JsonHelper.JsonSerializer<Student_mast>(stdms);
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
                DataSet dssubcaste = JsonConvert.DeserializeObject<DataSet>(result);
                if (dssubcaste.Tables[0].Rows.Count > 0)
                {
                    grdsubcaste.DataSource = dssubcaste.Tables[0];
                    grdsubcaste.DataBind();
                }
                else
                {
                    grdsubcaste.DataSource = null;
                    grdsubcaste.DataBind();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify('No data found', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void grdsubcaste_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow gvrow = grdsubcaste.SelectedRow;
            txtsubcaste.Text = ((Label)gvrow.FindControl("lblname")).Text;
            lblsubcaste.Text = ((Label)gvrow.FindControl("lblid")).Text;
            ddlcastemodal.SelectedValue = ((Label)gvrow.FindControl("lblcastidref")).Text;
            btnaddsub.Text = "Update";
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void grdsubcaste_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            GridViewRow gr = (GridViewRow)grdsubcaste.Rows[e.RowIndex];
            string count = ((Label)gr.FindControl("lblflag")).Text;
            if (count == "1")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify('Category already assigned to students', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
            }
            else
            {
                string urlalias = cls.urls();
                string url = @urlalias + "stud_mast/";

                stdms.type_of_query = "deletesubcaste";
                stdms.caste = ((Label)gr.FindControl("lblcastidref")).Text;
                stdms.subcast = ((Label)gr.FindControl("lblid")).Text;
                string jsonString = JsonHelper.JsonSerializer<Student_mast>(stdms);
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
                    DataSet dssubcaste = JsonConvert.DeserializeObject<DataSet>(result);
                    grdsubcaste.DataSource = dssubcaste.Tables[0];
                    grdsubcaste.DataBind();
                    ddlcastemodal.DataSource = dssubcaste.Tables[1];
                    ddlcastemodal.DataValueField = "cast_id";
                    ddlcastemodal.DataTextField = "cast_name";
                    ddlcastemodal.DataBind();
                    ddlcastemodal.Items.Insert(0, new ListItem("--Select--", "0"));
                    loaddata();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify('" + dssubcaste.Tables[2].Rows[0][0].ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                }
            }
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
            if (txtsubcaste.Text.Trim() != "")
            {
                useless();
                string urlalias = cls.urls();
                string url = @urlalias + "stud_mast/";
                stdms.type_of_query = "savesubcaste";
                if (txtsubcaste.Text != "" && btnaddsub.Text == "Save" && ddlcastemodal.SelectedIndex > 0)
                {
                    stdms.subcaste = txtsubcaste.Text;
                    stdms.caste = ddlcastemodal.SelectedValue.ToString();
                    stdms.categories = "savesubcaste";
                }
                else if (txtsubcaste.Text != "" && btnaddsub.Text == "Update" && ddlcastemodal.SelectedIndex > 0)
                {
                    stdms.subcaste = txtsubcaste.Text;
                    stdms.caste = ddlcastemodal.SelectedValue.ToString();
                    stdms.categories = "updatesubcaste";
                    stdms.subcast = lblsubcaste.Text;
                }
                string jsonString = JsonHelper.JsonSerializer<Student_mast>(stdms);
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
                    DataSet dssubcaste = JsonConvert.DeserializeObject<DataSet>(result);
                    if (dssubcaste.Tables[2].Rows[0][0].ToString() == "Same Subcaste Found")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify('" + dssubcaste.Tables[2].Rows[0][0].ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                    }
                    else if (dssubcaste.Tables[2].Rows[0][0].ToString() == "Saved Successfully" || dssubcaste.Tables[2].Rows[0][0].ToString() == "Updated Successfully")
                    {
                        grdsubcaste.DataSource = dssubcaste.Tables[0];
                        grdsubcaste.DataBind();
                        ddlcastemodal.DataSource = dssubcaste.Tables[1];
                        ddlcastemodal.DataValueField = "cast_id";
                        ddlcastemodal.DataTextField = "cast_name";
                        ddlcastemodal.DataBind();
                        ddlcastemodal.Items.Insert(0, new ListItem("--Select--", "0"));
                        loaddata();
                        txtsubcaste.Text = "";
                        btnaddsub.Text = "Save";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify('" + dssubcaste.Tables[2].Rows[0][0].ToString() + "', { color: '#fff', background: '#198104', blur: 0.2, delay: 0 })", true);
                    }
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify('Add caste to save', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void btnclear3_Click(object sender, EventArgs e)
    {
        try
        {
            string urlalias = cls.urls();
            string url = @urlalias + "stud_mast/";

            stdms.type_of_query = "subcastemodal";
            string jsonString = JsonHelper.JsonSerializer<Student_mast>(stdms);
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
                DataSet dssubcaste = JsonConvert.DeserializeObject<DataSet>(result);
                grdsubcaste.DataSource = dssubcaste.Tables[0];
                grdsubcaste.DataBind();
                ddlcastemodal.DataSource = dssubcaste.Tables[1];
                ddlcastemodal.DataValueField = "cast_id";
                ddlcastemodal.DataTextField = "cast_name";
                ddlcastemodal.DataBind();
                ddlcastemodal.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            txtsubcaste.Text = "";
            lblsubcaste.Text = "";
            btnaddsub.Text = "Save";
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    //----------------------------religion---------------------------------
    protected void grdreligion_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow gvrow = grdreligion.SelectedRow;

            txtreligion.Text = ((Label)gvrow.FindControl("lblname")).Text;
            lblrelid.Text = ((Label)gvrow.FindControl("lblid")).Text;
            btnadrel.Text = "Update";
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void grdreligion_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            GridViewRow gr = (GridViewRow)grdreligion.Rows[e.RowIndex];
            string count = ((Label)gr.FindControl("lblflag")).Text;
            if (count == "1")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify('Religion already assigned to students', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
            }
            else
            {
                string urlalias = cls.urls();
                string url = @urlalias + "stud_mast/";

                stdms.type_of_query = "updatedeletereligion";
                stdms.category = ((Label)gr.FindControl("lblid")).Text;

                string jsonString = JsonHelper.JsonSerializer<Student_mast>(stdms);
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
                    DataSet dsreligion = JsonConvert.DeserializeObject<DataSet>(result);
                    grdreligion.DataSource = dsreligion.Tables[0];
                    grdreligion.DataBind();
                    loaddata();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify('" + dsreligion.Tables[1].Rows[0][0].ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void btnclearrel_Click(object sender, EventArgs e)
    {
        try
        {
            txtreligion.Text = "";
            lblrelid.Text = "";
            btnadrel.Text = "Save";
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void btnadrel_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtreligion.Text.Trim() != "")
            {
                useless();
                string urlalias = cls.urls();
                string url = @urlalias + "stud_mast/";
                if (btnadrel.Text == "Save" && lblrelid.Text == "")
                {
                    stdms.categories = "savereligion";
                    stdms.type_of_query = "savereligion";
                    stdms.category = txtreligion.Text;
                }
                else if (btnadrel.Text == "Update" && lblrelid.Text != "")
                {
                    stdms.categories = lblrelid.Text;
                    stdms.category = txtreligion.Text;
                    stdms.type_of_query = "savereligion";
                }
                string jsonString = JsonHelper.JsonSerializer<Student_mast>(stdms);
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
                    DataSet dsreligion = JsonConvert.DeserializeObject<DataSet>(result);
                    if (dsreligion.Tables[1].Rows.Count > 0)
                    {
                        if (dsreligion.Tables[1].Rows[0][0].ToString() == "Same Religion Found")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify('" + dsreligion.Tables[1].Rows[0][0].ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                        }
                        else if (dsreligion.Tables[1].Rows[0][0].ToString() == "Saved Successfully" || dsreligion.Tables[1].Rows[0][0].ToString() == "Updated Successfully")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify('" + dsreligion.Tables[1].Rows[0][0].ToString() + "', { color: '#fff', background: '#198104', blur: 0.2, delay: 0 })", true);
                            txtreligion.Text = "";
                            lblrelid.Text = "";
                            btnadrel.Text = "Save";
                            grdreligion.DataSource = dsreligion.Tables[0];
                            grdreligion.DataBind();
                            loaddata();
                        }
                    }
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify('Add category to save', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void btnreligion_Click(object sender, EventArgs e)
    {
        try
        {
            string urlalias = cls.urls();
            string url = @urlalias + "stud_mast/";

            stdms.type_of_query = "religionmodel";
            string jsonString = JsonHelper.JsonSerializer<Student_mast>(stdms);
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
                DataSet dsreligion = JsonConvert.DeserializeObject<DataSet>(result);
                if (dsreligion.Tables[0].Rows.Count > 0)
                {
                    grdreligion.DataSource = dsreligion.Tables[0];
                    grdreligion.DataBind();
                }
            }
            txtreligion.Text = "";
            btnadrel.Text = "Save";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal('modalreligion');", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }
    //-----------------------------------------------------------------------
    public void loaddata()
    {
        DataSet ddlall = new DataSet();
        string urlalias = cls.urls();
        string url1 = @urlalias + "stud_mast/";

        stdms.type_of_query = "catcastsubcast";

        string jsonString1 = JsonHelper.JsonSerializer<Student_mast>(stdms);
        var httpWebRequest1 = (HttpWebRequest)WebRequest.Create(url1);
        httpWebRequest1.ContentType = "application/json";
        httpWebRequest1.Method = "POST";
        using (var streamWriter = new StreamWriter(httpWebRequest1.GetRequestStream()))
        {

            streamWriter.Write(jsonString1);
            streamWriter.Flush();
            streamWriter.Close();
        }

        var httpResponse1 = (HttpWebResponse)httpWebRequest1.GetResponse();

        using (StreamReader sr1 = new StreamReader(httpResponse1.GetResponseStream()))
        {
            string result1 = sr1.ReadToEnd();
            ddlall = JsonConvert.DeserializeObject<DataSet>(result1);
            Session["DDLALL"] = ddlall;
            DataTable dt = ddlall.Tables[0];

            ddlcategory.DataSource = dt;
            ddlcategory.DataValueField = "category_id";
            ddlcategory.DataTextField = "category_name";
            ddlcategory.DataBind();
            ddlcategory.Items.Insert(0, new ListItem("Category", "0"));

            ddlreligion.DataSource = ddlall.Tables[3];
            ddlreligion.DataValueField = "religion_id";
            ddlreligion.DataTextField = "religion";
            ddlreligion.DataBind();
            ddlreligion.Items.Insert(0, new ListItem("Religion", "0"));
        }

        if (txtform_id.Text.Trim() != "" && save.Text == "Update")
        {
            DataTable dt1 = ViewState["dtcategory"] as DataTable;
            //religion
            ListItem itemR = ddlreligion.Items.FindByValue(dt1.Rows[0]["religion"].ToString());
            if (itemR != null)
            {
                ddlcategory.Items.FindByValue(dt1.Rows[0]["religion"].ToString()).Selected = true;
            }
            else
            {
                ddlcategory.SelectedIndex = 0;
            }
            
            //---category
            ListItem item = ddlcategory.Items.FindByValue(dt1.Rows[0]["category"].ToString());
            if (item != null)
            {
                ddlcategory.Items.FindByValue(dt1.Rows[0]["category"].ToString()).Selected = true;
            }
            else
            {
                ddlcategory.SelectedIndex = 0;
            }

            //--caste
            DataView dc = ddlall.Tables[1].DefaultView;
            if (dc.Table.Rows.Count > 0)
            {
                dc.RowFilter = "category_id = '" + dt1.Rows[0]["category"].ToString() + "'";
            }
            ddlcaste.DataSource = dc;
            ddlcaste.DataTextField = "cast_name";
            ddlcaste.DataValueField = "cast_id";
            ddlcaste.DataBind();
            ddlcaste.Items.Insert(0, "Caste");

            ListItem item1 = ddlcaste.Items.FindByValue(dt1.Rows[0]["caste"].ToString());
            if (item1 != null)
            {
                ddlcaste.Items.FindByValue(dt1.Rows[0]["caste"].ToString()).Selected = true;
            }
            else
            {
                ddlcaste.SelectedIndex = 0;
            }

            //-- subcaste
            DataView dsc = ddlall.Tables[2].DefaultView;
            if (dsc.Table.Rows.Count > 0)
            {
                dsc.RowFilter = "cast_id = '" + dt1.Rows[0]["caste"].ToString() + "'";
            }
            ddlsubcaste.DataSource = dsc;
            ddlsubcaste.DataTextField = "subcast_name";
            ddlsubcaste.DataValueField = "subcast_id";
            ddlsubcaste.DataBind();
            ddlsubcaste.Items.Insert(0, "Subcaste");

            ListItem item3 = ddlsubcaste.Items.FindByValue(dt1.Rows[0]["sub_caste"].ToString());
            if (item3 != null)
            {
                ddlsubcaste.Items.FindByValue(dt1.Rows[0]["sub_caste"].ToString()).Selected = true;
            }
            else
            {
                ddlsubcaste.SelectedIndex = 0;
            }
        }
    }

}