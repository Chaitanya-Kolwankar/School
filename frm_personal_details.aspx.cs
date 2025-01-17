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

public partial class frm_personal_details : System.Web.UI.Page
{
    personal_details personal_details = new personal_details();
    Class1 cls = new Class1();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ConnectionString);
    SqlCommand cmd = new SqlCommand();
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
                    if (Session["msgretrived"] == "Updated Successfully")
                    {
                        Session["msgretrived"] = "";
                        notifys("Updated Successfully", "#198104");
                    }
                    lblid.Text = Session["emp_id"].ToString();
                    string urlalias = cls.urls();
                    string url = @urlalias + "loaddetails/";

                    emp_photo.ImageUrl = "~/image/" + Path.GetFileName("user.png");
                    emp_sign.ImageUrl = "~/image/" + Path.GetFileName("sign.png");

                    personal_details.type = "select";
                    personal_details.emp_id = Session["emp_id"].ToString();
                    string jsonString = JsonHelper.JsonSerializer<personal_details>(personal_details);
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
                            if (dataSet.Tables["employee"].Rows[0]["emp_photo"].ToString() != "" && dataSet.Tables["employee"].Rows[0]["emp_photo"].ToString() != null)
                            {
                                emp_photo.ImageUrl = ResolveUrl(String.Format(@"~/{0}", dataSet.Tables["employee"].Rows[0]["emp_photo"].ToString()));
                                lblphoto.Text = String.Format(@"..{0}", emp_photo.ImageUrl);
                            }

                            if (dataSet.Tables["employee"].Rows[0]["emp_sign"].ToString() != "" && dataSet.Tables["employee"].Rows[0]["emp_sign"].ToString() != null)
                            {
                                emp_sign.ImageUrl = ResolveUrl(String.Format(@"~/{0}", dataSet.Tables["employee"].Rows[0]["emp_sign"].ToString()));
                                lblsign.Text = String.Format(@"..{0}", emp_sign.ImageUrl);
                            }
                           
                            txt_last_name.Text = dataSet.Tables["employee"].Rows[0]["emp_lname"].ToString();
                            txt_first_name.Text = dataSet.Tables["employee"].Rows[0]["emp_fname"].ToString();
                            txt_middle_name.Text = dataSet.Tables["employee"].Rows[0]["emp_mname"].ToString();
                            txt_mother.Text = dataSet.Tables["employee"].Rows[0]["emp_mother_name"].ToString();
                            txt_dob.Text = dataSet.Tables["employee"].Rows[0]["emp_dob"].ToString();
                            txtadddress1.Value = dataSet.Tables["employee"].Rows[0]["emp_address_curr"].ToString();
                            txtpincode.Text = dataSet.Tables["employee"].Rows[0]["emp_pincode_curr"].ToString();
                            txt_add1.Value = dataSet.Tables["employee"].Rows[0]["emp_address_per"].ToString();
                            txt_pin.Text = dataSet.Tables["employee"].Rows[0]["emp_pincode_per"].ToString();
                            txt_doj.Text = dataSet.Tables["employee"].Rows[0]["emp_trust"].ToString();
                            txt_department.Text = Session["CURRENT_DEPARTMENT_NAME"].ToString();
                            txt_designation.Text = Session["CURRENT_DESIGNATION"].ToString();
                            txt_mobile_no.Text = dataSet.Tables["employee"].Rows[0]["emp_mobile1"].ToString();
                            txt_mob_no.Text = dataSet.Tables["employee"].Rows[0]["emp_mobile2"].ToString();
                            txt_email.Text = dataSet.Tables["employee"].Rows[0]["emp_email"].ToString();
                            txt_pan.Text = dataSet.Tables["employee"].Rows[0]["emp_pan"].ToString();
                            txt_adhar.Text = dataSet.Tables["employee"].Rows[0]["emp_aadharno"].ToString();
                            ddlState.Items.FindByText(dataSet.Tables["employee"].Rows[0]["emp_state_curr"].ToString()).Selected = true;
                            ddl_state1.Items.FindByText(dataSet.Tables["employee"].Rows[0]["emp_state_per"].ToString()).Selected = true;

                            if (dataSet.Tables["employee"].Rows[0]["emp_gender"].ToString().ToUpper().Contains("FEMALE"))
                            {
                                rdbFemale.Checked = true;
                            }
                            else if (dataSet.Tables["employee"].Rows[0]["emp_gender"].ToString().ToUpper().Contains("MALE"))
                            {
                                rdbMale.Checked = true;
                            }
                            else if (dataSet.Tables["employee"].Rows[0]["emp_gender"].ToString().ToUpper().Contains("TRANSGENDER"))
                            {
                                rdbtrans.Checked = true;
                            }
                            if (dataSet.Tables["employee"].Rows[0]["emp_maritial_status"].ToString().ToUpper().Contains("FALSE"))
                            {
                                rdbUnmarried.Checked = true;
                            }
                            else if (dataSet.Tables["employee"].Rows[0]["emp_maritial_status"].ToString().ToUpper().Contains("TRUE"))
                            {
                                rdbMarried.Checked = true;
                            }

                            ddlBloodGroup.Items.FindByValue(dataSet.Tables["employee"].Rows[0]["emp_blood_group"].ToString()).Selected = true;

                            ddlCategory.DataSource = dataSet.Tables[1];
                            ddlCategory.DataTextField = "category_name";
                            ddlCategory.DataValueField = "category_id";
                            ddlCategory.DataBind();

                            ListItem item = ddlCategory.Items.FindByValue(dataSet.Tables["employee"].Rows[0]["emp_category"].ToString());
                            if (item != null)
                            {
                                ddlCategory.Items.FindByValue(dataSet.Tables["employee"].Rows[0]["emp_category"].ToString()).Selected = true;
                            }
                            else
                            {
                                ddlCategory.SelectedIndex = 0;
                            }


                            string isc = ddlCategory.SelectedValue;

                            DataView dsc = dataSet.Tables[2].DefaultView;

                            if (dsc.Table.Rows.Count > 0)
                            {
                                dsc.RowFilter = "category_id = '" + isc + "'";
                            }

                            ddlCast.DataSource = dsc;
                            ddlCast.DataTextField = "cast_name";
                            ddlCast.DataValueField = "cast_id";
                            ddlCast.DataBind();
                            ddlCast.Items.Insert(0, "--Select--");
                            ddlCast.SelectedIndex = 0;

                            ListItem item1 = ddlCast.Items.FindByValue(dataSet.Tables["employee"].Rows[0]["emp_cast"].ToString());
                            if (item1 != null)
                            {
                                ddlCast.Items.FindByValue(dataSet.Tables["employee"].Rows[0]["emp_cast"].ToString()).Selected = true;
                            }
                            else
                            {
                                ddlCast.SelectedIndex = 0;
                            }
                            Session["cast"] = dataSet.Tables[2];

                        }
                    }
                }
            }
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "loaddate()", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }

    }
    public void notifys(string msg, string color)
    {
        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + msg + "', { color: '#fff', background: '" + color + "', blur: 0.2, delay: 0 });", true);
    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlCategory.SelectedIndex > 0)
            {
                string ic = ddlCategory.SelectedValue;
                DataTable ds = (DataTable)Session["cast"];
                DataView dcast = ds.DefaultView;
                dcast.RowFilter = "category_id = '" + ic + "'";
                ddlCast.DataSource = dcast;
                ddlCast.DataTextField = "cast_name";
                ddlCast.DataValueField = "category_id";
                ddlCast.DataBind();
                ddlCast.Items.Insert(0, "--Select--");
                ddlCast.SelectedIndex = 0;
            }
            else
            {
                ddlCast.Items.Clear();
                ddlCast.Items.Insert(0, "--Select--");
                ddlCast.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }

    }

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        try
        {
            string urlalias = cls.urls();
            string url = @urlalias + "loaddetails/";

            if (txt_first_name.Text == "")
            {
                Session["msgretrived"] = "";
                notifys("Invalid First Name", "#D44950");
                return;
            }
            if (txt_middle_name.Text == "")
            {
                Session["msgretrived"] = "";
                notifys("Invalid Middle Name", "#D44950");
                return;
            }
            if (txt_mother.Text == "")
            {
                Session["msgretrived"] = "";
                notifys("Select Mother Name", "#D44950");
                return;
            }
            if (txt_dob.Text == "")
            {
                Session["msgretrived"] = "";
                notifys("Select Date of Birth", "#D44950");
                return;
            }
            if (rdbMale.Checked == false && rdbFemale.Checked == false && rdbtrans.Checked == false)
            {
                Session["msgretrived"] = "";
                notifys(" Select Gender", "#D44950");
                return;
            }

            if (rdbMarried.Checked == false && rdbUnmarried.Checked == false)
            {
                Session["msgretrived"] = "";
                notifys(" Select Marital Status", "#D44950");
                return;
            }
            if (txtadddress1.Value == "")
            {
                Session["msgretrived"] = "";
                notifys("Enter Address", "#D44950");
                return;
            }
            if (ddlState.SelectedItem.Text.Contains("--Select--"))
            {
                Session["msgretrived"] = "";
                notifys("Enter Current State", "#D44950");
                return;
            }
            if (txtpincode.Text == "" )
            {
                Session["msgretrived"] = "";
                notifys("Enter Current Pincode", "#D44950");
                return;
            }
            if ( txtpincode.Text.Length < 6)
            {
                Session["msgretrived"] = "";
                notifys("Enter 6 Digit Current Pincode", "#D44950");
                return;
            }
            if (txt_add1.Value == "")
            {
                Session["msgretrived"] = "";
                notifys("Enter Permanent Address", "#D44950");
                return;
            }
            if (ddl_state1.SelectedIndex.ToString().Contains("--Select--"))
            {
                Session["msgretrived"] = "";
                notifys("Select Permanent State", "#D44950");
                return;
            }
            if (txt_pin.Text == "" )
            {
                Session["msgretrived"] = "";
                notifys("Enter Permanent Pincode", "#D44950");
                return;
            }
            if ( txt_pin.Text.Length < 6)
            {
                Session["msgretrived"] = "";
                notifys("Enter 6 Digit Permanent Pincode", "#D44950");
                return;
            }
            if (ddlBloodGroup.SelectedIndex == 0)
            {
                Session["msgretrived"] = "";
                notifys("Select Blood Group", "#D44950");
                return;
            }
            if (ddlCategory.SelectedItem.Text.Contains("--Select--"))
            {
                Session["msgretrived"] = "";
                notifys("Select Category", "#D44950");
                return;
            }
            if (ddlCast.Items.Count > 1)
            {
                if (ddlCast.SelectedItem.Text.Contains("--Select--"))
                {
                    Session["msgretrived"] = "";
                    notifys("Select Caste", "#D44950");
                    return;
                }
            }
          
            if (txt_mobile_no.Text == "" )
            {
                Session["msgretrived"] = "";
                notifys("Enter Mobile Number", "#D44950");
                return;
            }
            if ( txt_mobile_no.Text.Length < 10)
            {
                Session["msgretrived"] = "";
                notifys("Enter 10 Digit Mobile Number", "#D44950");
                return;
            }
            if (txt_mob_no.Text != "" )
            {
                if( txt_mob_no.Text.Length < 8)
                {
                    notifys("Enter 8 Digits Telephone Number", "#D44950");
                return;
                }
            }
            if (txt_email.Text == "")
            {
                Session["msgretrived"] = "";
                notifys("Enter Email ID", "#D44950");
                return;
            }
            if (txt_pan.Text != "" )
            {
                if (txt_pan.Text.Length < 10)
                {
                    notifys("Enter 10 Character in Pancard Number", "#D44950");
                    return;
                }
            }
            if (txt_adhar.Text != "" )
            {
                if( txt_adhar.Text.Length < 12)
                {
                    notifys("Enter 12 Digits Aadhar Number", "#D44950");
                return;
                }
            }

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

            string photo = emp_photo.ImageUrl;
            string nameImage = photo.Substring(photo.LastIndexOf('\\') + 1);
            string emp_id = Session["emp_id"].ToString().Trim();
           
            personal_details.type = "get";
            personal_details.emp_id = Session["emp_id"].ToString().ToUpper().Trim();
            personal_details.lname = txt_last_name.Text.Trim().ToUpper();
            personal_details.mname = txt_middle_name.Text.Trim().ToUpper();
            personal_details.fname = txt_first_name.Text.Trim().ToUpper();
            personal_details.moname = txt_mother.Text.Trim().ToUpper();
            personal_details.dob = txt_dob.Text.Trim();
            ///GENDER
            if (rdbMale.Checked == true)
            {
                personal_details.gender = "Male";
            }
            else if (rdbFemale.Checked == true)
            {
                personal_details.gender = "Female";
            }
            else if (rdbtrans.Checked == true)
            {
                personal_details.gender = "Transgender";
            }

            ////MARITAL STATUS
            if (rdbUnmarried.Checked == true)
            {
                personal_details.mstatus = "0";
            }
            else if (rdbMarried.Checked == true)
            {
                personal_details.mstatus = "1";
            }

            personal_details.bloodgroup = ddlBloodGroup.SelectedItem.ToString();


            personal_details.caddress = txtadddress1.Value.Trim();
            personal_details.paddress = txt_add1.Value.Trim();
            personal_details.cstate = ddlState.SelectedItem.Text.Trim();
            personal_details.pstate = ddl_state1.SelectedItem.Text.Trim();
            personal_details.cpin = txtpincode.Text.Trim();
            personal_details.ppin = txt_pin.Text.Trim();
            personal_details.category = ddlCategory.SelectedValue.ToString().Trim();
            if (ddlCast.SelectedIndex > 0)
            {
                personal_details.cast = ddlCast.SelectedValue.ToString().Trim();
            }
            else
            {
                personal_details.cast = null;
            }
            personal_details.mobile1 = txt_mobile_no.Text.Trim();
            personal_details.mobile2 = txt_mob_no.Text.Trim();
            personal_details.email = txt_email.Text.Trim();
            personal_details.panno = txt_pan.Text.Trim().ToUpper();
            personal_details.aadhar = txt_adhar.Text.Trim();

            string jsonString = JsonHelper.JsonSerializer<personal_details>(personal_details);
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
                classWebMethods cls1 = new classWebMethods();
                if (get_photo.HasFile)
                {
                    string img=rootpath + "\\Staff\\" + emp_id.ToString() + "_photo.jpg";
                    get_photo.SaveAs(img);
                    string imgpic = cls.imageFolder() + "/Staff/" + emp_id.ToString() + "_photo.jpg";
                    cls1.uploadphoto(imgpic, emp_id);
                }
                if (get_sign.HasFile)
                {
                    string imgsign = rootpath + "\\Staff\\" + emp_id.ToString() + "_sign.jpg";
                    get_sign.SaveAs(imgsign);
                    string imgsignnew = cls.imageFolder() + "/Staff/" + emp_id.ToString() + "_sign.jpg";
                    cls1.uploadsign(imgsignnew, emp_id);
                }
                Session["msgretrived"] = "Updated Successfully";
                notifys("Updated Successfully", "#198104");
                Page.Response.Redirect(Page.Request.Url.ToString(), false);
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
    protected void chk_same_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (chk_same.Checked == true)
            {
                txt_add1.Value = txtadddress1.Value;
                txt_pin.Text = txtpincode.Text;
                ddl_state1.SelectedValue = ddl_state1.Items.FindByText(ddlState.SelectedItem.Text).Value;
            }
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }
}