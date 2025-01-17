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

public partial class ChangePasswd : System.Web.UI.Page
{
    Class1 cls = new Class1();
    change_password change_password = new change_password();   
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ConnectionString);
    SqlCommand cmd = new SqlCommand();
    string str = "";
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
                btneye.Visible = false;
                txt_newpasswd.TextMode = TextBoxMode.Password;
                try
                {

                    if (Convert.ToString(Session["passwordchanged"]) == "false")
                    {
                        notifys("Please Enter new password to continue.", "#198104");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                }
            }
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            if (txt_oldpasswd.Text == "")
            {
                notifys("Invalid old password", "#198104");
                return;
            }
            else if(txt_newpasswd.Text == "")
            {
                notifys("Invalid new password", "#198104");
                return;
            }
            else if(txt_confrmpasswd.Text == "")
            {
                notifys("Invalid confirm password", "#198104");
                return;
            }
            else
            {
                if (txt_newpasswd.Text.Equals(txt_oldpasswd.Text))
                {
                    notifys("New password cannot be same as your current password", "#198104");
                    return;
                }
                else if (!txt_newpasswd.Text.Equals(txt_confrmpasswd.Text))
                {
                    notifys("Password Does Not Match.", "#198104");
                    return;
                }
                else
                {
                    string password = Session["password"].ToString();
                    if (txt_oldpasswd.Text.Equals(password.ToString()))
                    {
                        string urlalias = cls.urls();
                        string url = @urlalias + "loadpass/";
        
                        change_password.type = "get";

                        change_password.emp_id = Session["emp_id"].ToString();
                        change_password.password = txt_newpasswd.Text;
                        string jsonString1 = JsonHelper.JsonSerializer<change_password>(change_password);
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
                            Session["password"] = txt_newpasswd.Text;
                            notifys("Password Changed Successfully", "#198104");
                            Response.Redirect("Login.aspx?msg=password", false);
                        }
                    }
                    else
                    {
                        notifys("Incorrect Old Password.", "#D44950");
                        return;
                    }
                }
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
    protected void btneye_Click(object sender, EventArgs e)
    {
        try
        {
            txt_newpasswd.TextMode = TextBoxMode.Password;
            string texts = txt_newpasswd.Text;
            btneye.Visible = false;
            btneyeslash.Visible = true;
            txt_newpasswd.Attributes.Add("value", texts);
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void btneyeslash_Click(object sender, EventArgs e)
    {
        try
        {
            string texts = txt_newpasswd.Text;
            txt_newpasswd.TextMode = TextBoxMode.SingleLine;
            btneye.Visible = true;
            btneyeslash.Visible = false;
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void myFunction1(object sender, EventArgs e)
    {
        try
        {
            string texts = txt_newpasswd.Text;
            txt_newpasswd.TextMode = TextBoxMode.SingleLine;
            btneye.Visible = true;
            btneyeslash.Visible = false;
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }
}