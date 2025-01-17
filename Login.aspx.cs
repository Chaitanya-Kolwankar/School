using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using System.Web.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization.Json;
using System.Text;



public partial class Login_form : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ConnectionString);
    Class1 c1 = new Class1();
    DataSet ds = new DataSet();
    DataSet ds_new = new DataSet();
    QueryClass qrycls = new QueryClass();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Session.Clear();
                string msg = Request.QueryString["msg"];
                if (msg == "session")
                {
                    Label_lbl2.Text = "Session Timeout...Re-login";
                }
                if(msg == "password")
                {
                    Label_lbl2.Text = "Password Changed...Re-login";
                }
                string ins = c1.institute();
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
    }//done

    protected void btn_login_Click(object sender, EventArgs e)
        {
        try
        {
            if (txt_user.Text == "" || txt_password.Text == "")
            {
                if (txt_user.Text == "")
                {
                    Label_lbl2.Text = "Login Failed";
                }
                else
                {
                    Label_lbl2.Text = "Login Failed";
                }
            }
            else
            {
                login cm = new login();
                DataSet dataSet = new DataSet();

                string urlalias = c1.urls();
                string url = @urlalias + "login/";
                cm.type = "Web";
                cm.username = txt_user.Text;
                cm.passwd = txt_password.Text;

                string jsonString = JsonHelper.JsonSerializer<login>(cm);
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
                    try
                    {
                        dataSet = JsonConvert.DeserializeObject<DataSet>(result);
                    }
                    catch (Exception ex)
                    {
                        if (result.ToString().Contains("password") == true)
                        {
                            Label_lbl2.Text = "Invalid Password";
                            Label_lbl2.Visible = true;
                        }
                        else
                        {
                            Label_lbl2.Text = "Invalid Username";
                            Label_lbl2.Visible = true;
                        }
                    }
                }

                if (dataSet.Tables.Count != 0)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {

                        Session["emp_role"] = dataSet.Tables[0].Rows[0]["emp_role"].ToString();
                        Session["emp_id"] = txt_user.Text;
                        Session["username"] = txt_user.Text;
                        Session["password"] = txt_password.Text;
                        Label_lbl2.Visible = false;
                        if (dataSet.Tables[0].Rows[0]["emp_id"].ToString() != "")
                        {
                            Session["emp_id"] = dataSet.Tables[0].Rows[0]["emp_id"].ToString();
                        }
                        else
                        {
                            Session["NAME"] = "";
                        }
                        if (dataSet.Tables[0].Rows[0]["NAME"].ToString() != "")
                        {
                            Session["NAME"] = dataSet.Tables[0].Rows[0]["NAME"].ToString() + " " + dataSet.Tables[0].Rows[0]["SURNAME"].ToString();
                        }
                        else
                        {
                            Session["NAME"] = "";
                        }
                        if (dataSet.Tables[0].Rows[0]["FATHER"].ToString() != "")
                        {
                            Session["FATHER"] = dataSet.Tables[0].Rows[0]["FATHER"].ToString();
                        }
                        else
                        {
                            Session["FATHER"] = "";
                        }

                        if (dataSet.Tables[0].Rows[0]["SURNAME"].ToString() != "")
                        {
                            Session["SURNAME"] = dataSet.Tables[0].Rows[0]["SURNAME"].ToString();
                        }
                        else
                        {
                            Session["SURNAME"] = "";
                        }
                        if (dataSet.Tables[0].Rows[0]["MOTHER"].ToString() != "")
                        {
                            Session["MOTHER"] = dataSet.Tables[0].Rows[0]["MOTHER"].ToString();
                        }
                        else
                        {
                            Session["MOTHER"] = "";
                        }
                        if (dataSet.Tables[0].Rows[0]["DOB"].ToString() != "")
                        {
                            Session["DOB"] = dataSet.Tables[0].Rows[0]["DOB"].ToString();
                        }
                        else
                        {
                            Session["DOB"] = "";
                        }

                        if (dataSet.Tables[0].Rows[0]["DOJ_gov"].ToString() != "")
                        {
                            Session["DOJ"] = dataSet.Tables[0].Rows[0]["DOJ_gov"].ToString();
                        }
                        else
                        {
                            Session["DOJ"] = "";
                        }
                        if (dataSet.Tables[0].Rows[0]["PAN_NO"].ToString() != "")
                        {
                            Session["PAN_NO"] = dataSet.Tables[0].Rows[0]["PAN_NO"].ToString();
                        }
                        else
                        {
                            Session["PAN_NO"] = "";
                        }
                        if (dataSet.Tables[0].Rows[0]["BLOOD_GROUP"].ToString() != "")
                        {
                            Session["BLOOD_GROUP"] = dataSet.Tables[0].Rows[0]["BLOOD_GROUP"].ToString();
                        }
                        else
                        {
                            Session["BLOOD_GROUP"] = "";
                        }

                        if (dataSet.Tables[0].Rows[0]["GENDER"].ToString() != "")
                        {
                            Session["GENDER"] = dataSet.Tables[0].Rows[0]["GENDER"].ToString();
                        }
                        else
                        {
                            Session["GENDER"] = "";
                        }

                        if (dataSet.Tables[0].Rows[0]["CASTE"].ToString() != "")
                        {
                            Session["CASTE"] = dataSet.Tables[0].Rows[0]["CASTE"].ToString();
                        }
                        else
                        {
                            Session["CASTE"] = "";
                        }

                        if (dataSet.Tables[0].Rows[0]["MOBILE1"].ToString() != "")
                        {
                            Session["MOBILE1"] = dataSet.Tables[0].Rows[0]["MOBILE1"].ToString();
                        }
                        else
                        {
                            Session["MOBILE1"] = "";
                        }
                        if (dataSet.Tables[0].Rows[0]["MOBILE2"].ToString() != "")
                        {
                            Session["MOBILE2"] = dataSet.Tables[0].Rows[0]["MOBILE2"].ToString();
                        }
                        else
                        {
                            Session["MOBILE2"] = "";
                        }
                        if (dataSet.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString() != "")
                        {
                            Session["EMAIL_ADDRESS"] = dataSet.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                        }
                        else
                        {
                            Session["EMAIL_ADDRESS"] = "";
                        }

                        if (dataSet.Tables[0].Rows[0]["CURRENT_ADDRESS"].ToString() != "")
                        {
                            Session["CURRENT_ADDRESS"] = dataSet.Tables[0].Rows[0]["CURRENT_ADDRESS"].ToString();
                        }
                        else
                        {
                            Session["CURRENT_ADDRESS"] = "";
                        }

                        if (dataSet.Tables[0].Rows[0]["CURRENT_DEPARTMENT_NAME"].ToString() != "")
                        {
                            Session["CURRENT_DEPARTMENT_NAME"] = dataSet.Tables[0].Rows[0]["CURRENT_DEPARTMENT_NAME"].ToString();
                        }
                        else
                        {
                            Session["CURRENT_DEPARTMENT_NAME"] = "";
                        }

                        if (dataSet.Tables[0].Rows[0]["CURRENT_DESIGNATION"].ToString() != "")
                        {
                            Session["CURRENT_DESIGNATION"] = dataSet.Tables[0].Rows[0]["CURRENT_DESIGNATION"].ToString();
                        }
                        else
                        {
                            Session["CURRENT_DEPARTMENT_NAME"] = "";
                        }

                        if (dataSet.Tables[0].Rows[0]["MARITIAL_STATUS"].ToString() != "")
                        {
                            Session["MARITIAL_STATUS"] = dataSet.Tables[0].Rows[0]["MARITIAL_STATUS"].ToString();
                        }
                        else
                        {
                            Session["MARITIAL_STATUS"] = "";
                        }

                        if (dataSet.Tables[0].Rows[0]["group_name"].ToString() != "")
                        {
                            Session["group_ids"] = dataSet.Tables[0].Rows[0]["group_name"].ToString();
                        }
                        else
                        {
                            Session["group_ids"] = "";
                        }

                        if (dataSet.Tables[0].Rows[0]["form_name"].ToString() != "")
                        {
                            Session["frm"] = "";
                            Session["form_name"] = dataSet.Tables[0].Rows[0]["form_name"].ToString();
                            string form1 = (string)Session["form_name"];
                            Session["col2"] = dataSet.Tables[0].Rows[0]["col2"].ToString();
                            form1 += "," + (string)Session["col2"];
                            Session["frm"] = form1;

                        }
                        else
                        {
                            Session["form_name"] = "";
                        }

                        if (dataSet.Tables[0].Rows[0]["CATEGORY"].ToString() != "")
                        {
                            Session["CATEGORY"] = dataSet.Tables[0].Rows[0]["CATEGORY"].ToString();
                        }
                        else
                        {
                            Session["CATEGORY"] = "";
                        }

                        if (dataSet.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString() != "")
                        {
                            Session["EMAIL_ADDRESS"] = dataSet.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                        }
                        else
                        {
                            Session["EMAIL_ADDRESS"] = "";
                        }

                       qrycls.insertLog(Session["emp_id"].ToString(), c1.getClientIp(), "", true);
                        Response.Redirect("Profile_page.aspx",false);
                    }
                    else
                    {
                        //Label_lbl2.Text = "Invalid Login";
                        //Label_lbl2.Visible = true;
                    }
                }
                else
                {
                    //Label_lbl2.Text = "Invalid Login";
                    //Label_lbl2.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    [WebMethod]
    public static masterpage[] fillModule(string emp_id)
    {
        classWebMethods cls = new classWebMethods();
        return cls.moduleAccess(emp_id);
    }


    [WebMethod]
    public static masterpage[] fillform(string emp_id)
    {
        classWebMethods cls = new classWebMethods();
        return cls.formAccess(emp_id);
    }
    
}