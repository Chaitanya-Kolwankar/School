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

public partial class Leaving_certificate : System.Web.UI.Page
{
    LC lc = new LC();
    Class1 cls = new Class1();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ConnectionString);
    SqlCommand cmd = new SqlCommand();
    DataTable dt = new DataTable();
    string tablename;
    string syear, syear1;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "loaddate()", true);
            if (!IsPostBack)
            {
                if (Convert.ToString(Session["emp_id"]) == "")
                {
                    Response.Redirect("Login.aspx");

                }
                else
                {

                }
                BINDGVRemark();
                BINDGVREASON();
                load();
                reason_load();
                remarkload();
                txttablename.Visible = false;
                txtid.Text = "";
                string year = (Session["year"].ToString());
                syear = year.Substring(year.Length - 4);
                syear1 = year.Substring(6, 5);
                Session["syear"] = syear.ToString().Trim();
                Session["syear1"] = syear1.ToString().Trim();
                ddlstandard.Items.Add("--Select--");
                //ddlyear.Items.Add("--select--");
                //ddlyear.Items.Add(syear1.ToString().Trim());
                //ddlyear.Items.Add(syear.ToString().Trim());
                disable();
            }

        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }


    }

    public void reason_load()
    {
        string urlalias = cls.urls();
        string url111 = @urlalias + "load/";
        lc.type = "Reason";
        string jsonString111 = JsonHelper.JsonSerializer<LC>(lc);
        var httprequest111 = (HttpWebRequest)WebRequest.Create(url111);
        httprequest111.ContentType = "application/json";
        httprequest111.Method = "POST";


        using (var streamWriter111 = new StreamWriter(httprequest111.GetRequestStream()))
        {
            streamWriter111.Write(jsonString111);
            streamWriter111.Flush();
            streamWriter111.Close();
        }
        var httpresponse111 = (HttpWebResponse)httprequest111.GetResponse();
        using (var streamReader111 = new StreamReader(httpresponse111.GetResponseStream()))
        {
            string result111 = streamReader111.ReadToEnd();
            DataSet dataSet111 = JsonConvert.DeserializeObject<DataSet>(result111);
            ddlreason.DataSource = dataSet111.Tables[0];
            ddlreason.DataTextField = "Reason";
            ddlreason.DataValueField = "Reason";
            ddlreason.DataBind();
            ddlreason.Items.Insert(0, new ListItem("--select--", "--select--"));

        }

    }


    public void remarkload()
    {
        string urlalias = cls.urls();
        string url111 = @urlalias + "load/";
        lc.type = "Remark";

        string jsonString111 = JsonHelper.JsonSerializer<LC>(lc);
        var httprequest111 = (HttpWebRequest)WebRequest.Create(url111);
        httprequest111.ContentType = "application/json";
        httprequest111.Method = "POST";


        using (var streamWriter111 = new StreamWriter(httprequest111.GetRequestStream()))
        {
            streamWriter111.Write(jsonString111);
            streamWriter111.Flush();
            streamWriter111.Close();
        }
        var httpresponse111 = (HttpWebResponse)httprequest111.GetResponse();
        using (var streamReader111 = new StreamReader(httpresponse111.GetResponseStream()))
        {
            string result111 = streamReader111.ReadToEnd();
            DataSet dataSet111 = JsonConvert.DeserializeObject<DataSet>(result111);
            ddlremk.DataSource = dataSet111.Tables[0];
            ddlremk.DataTextField = "Remark";
            ddlremk.DataValueField = "Remark";
            ddlremk.DataBind();
            ddlremk.Items.Insert(0, new ListItem("--select--", "--select--"));

        }
    }
    //public void ddlload()
    //{
    //    try
    //    {
    //        string urlalias = cls.urls();
    //        string url9 = @urlalias + "load/";
    //        lc.type = "lcreason";
    //        lc.Ayid = Session["acdyear"].ToString();

    //        string jsonString9 = JsonHelper.JsonSerializer<LC>(lc);
    //        var httprequest9 = (HttpWebRequest)WebRequest.Create(url9);
    //        httprequest9.ContentType = "application/json";
    //        httprequest9.Method = "POST";

    //        using (var streamWriter9 = new StreamWriter(httprequest9.GetRequestStream()))
    //        {
    //            streamWriter9.Write(jsonString9);
    //            streamWriter9.Flush();
    //            streamWriter9.Close();
    //        }

    //        var httpresponse9 = (HttpWebResponse)httprequest9.GetResponse();
    //        using (var streamReader9 = new StreamReader(httpresponse9.GetResponseStream()))
    //        {
    //            string result9 = streamReader9.ReadToEnd();
    //            DataSet dataSet9 = JsonConvert.DeserializeObject<DataSet>(result9);
    //            ddlreason.DataSource = dataSet9.Tables[0];
    //            ddlreason.DataTextField = "Reason";
    //            ddlreason.DataBind();

    //            ddlremark.DataSource = dataSet9.Tables[1];
    //            ddlremark.DataTextField = "Remark";
    //            ddlremark.DataBind();

    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        Response.Redirect("Login.aspx");
    //    }
    //}

    public void load()
    {
        txtsname.Enabled = false;
        txtfname.Enabled = false;
        txtmname.Enabled = false;
        txtmoname.Enabled = false;
        txtadhar.Enabled = false;
        txtn.Enabled = false;
        txtmt.Enabled = false;
        txtdob.Enabled = false;
        txtpob.Enabled = false;
        txttal.Enabled = false;
        txtdis.Enabled = false;
        txtstate.Enabled = false;
        txtsaral.Enabled = false;
        txtcast.Enabled = false;
        txtscast.Enabled = false;
        txtlid.Enabled = false;
        txtschool.Enabled = false;
        txtboa.Enabled = false;
    }

    public void load_std()
    {
        string urlalias = cls.urls();
        string url111 = @urlalias + "load/";
        lc.type = "standard";

        string jsonString111 = JsonHelper.JsonSerializer<LC>(lc);
        var httprequest111 = (HttpWebRequest)WebRequest.Create(url111);
        httprequest111.ContentType = "application/json";
        httprequest111.Method = "POST";


        using (var streamWriter111 = new StreamWriter(httprequest111.GetRequestStream()))
        {
            streamWriter111.Write(jsonString111);
            streamWriter111.Flush();
            streamWriter111.Close();
        }


        var httpresponse111 = (HttpWebResponse)httprequest111.GetResponse();
        using (var streamReader111 = new StreamReader(httpresponse111.GetResponseStream()))
        {
            string result111 = streamReader111.ReadToEnd();
            DataSet dataSet111 = JsonConvert.DeserializeObject<DataSet>(result111);
            ddlstandard.DataSource = dataSet111.Tables[0];
            ddlstandard.DataTextField = "std_name";
            ddlstandard.DataValueField = "std_id";
            ddlstandard.DataBind();
            ddlstandard.Items.Insert(0, new ListItem("--select--", "--select--"));

        }
    }

    public void refresh()
    {
        txtsname.Text = "";
        txtfname.Text = "";
        txtmname.Text = "";
        txtmoname.Text = "";
        txtadhar.Text = "";
        txtn.Text = "";
        txtmt.Text = "";
        txtdob.Text = "";
        txtpob.Text = "";
        txttal.Text = "";
        txtdis.Text = "";
        txtstate.Text = "";
        txtsaral.Text = "";
        txtcast.Text = "";
        txtscast.Text = "";
        txtlid.Text = "";
        txtschool.Text = "";
        txtboa.Text = "";
        txtid.Text = "";
        txtstandard.Text = "";
        txt1.Text = "";
        txtls.Text = "";
        txtseat.Text = "";
        txtstud_id.Value = "";
        ddlremk.SelectedValue = "--select--";
        ddlreason.SelectedValue = "--select--";
        //txtreason.Text = "";
        //txtremark.Text = "";      
        ddlconduct.SelectedIndex = 0;
        ddlprog.SelectedIndex = 0;
        // ddlyear.SelectedIndex = 0;
        txtid.Enabled = true;
        ddlstandard.SelectedIndex = 0;
    }

    public void disable()
    {
        txtseat.Enabled = false;
        txtstandard.Enabled = false;
        txt1.Enabled = false;
        txtls.Attributes.Add("disabled", "disabled");
        ddlconduct.Enabled = false;
        ddlprog.Enabled = false;
        //txtreason.Enabled = false;
        //txtremark.Enabled = false;
        ddlreason.Enabled = true;
        ddlremk.Enabled = true;

        // ddlyear.Enabled = false;
        txtschool.Enabled = false;
        ddlstandard.Enabled = false;
    }

    public void enable()
    {
        txtseat.Enabled = true;
        txtstandard.Enabled = true;
        txt1.Enabled = true;
        txtls.Attributes.Remove("disabled");
        ddlconduct.Enabled = true;
        ddlprog.Enabled = true;
        //txtreason.Enabled = true;
        //txtremark.Enabled = true;
        ddlremk.Enabled = true;
        ddlreason.Enabled = true;
        // ddlyear.Enabled = true;
        txtschool.Enabled = true;
        ddlstandard.Enabled = true;
    }

    public void clear()
    {
        Page.Response.Redirect(Page.Request.Url.ToString(), true);
    }

    protected void btns_Click(object sender, EventArgs e)
    {
        try
        {
            if (btns.Text == "Save")
            {
                lc.Ayid = Session["acdyear"].ToString();
                string urlalias = cls.urls();
                string url3 = @urlalias + "load/";
                lc.sid = Session["stud_id"].ToString().Trim();
                lc.lcno = "";
                lc.dol = txtls.Text;
                lc.conduct = ddlconduct.SelectedItem.ToString().Trim();
                lc.progress = ddlprog.SelectedItem.ToString().Trim();
                //lc.Reason = txtreason.Text.ToUpper().Trim();
                //lc.Remark = txtremark.Text.ToUpper().Trim();
                lc.standard = ddlstandard.SelectedValue;
                lc.Reason = ddlreason.SelectedValue;
                lc.Remark = ddlremk.SelectedValue;
                //if (ddlreason.SelectedItem.ToString().ToUpper().Contains("MARCH"))
                //{
                //    lc.Reason = ddlreason.SelectedItem.ToString().Trim() + ' ' + ddlyear.SelectedItem.ToString();
                //}
                //else
                //{
                //    lc.Reason = ddlreason.SelectedItem.ToString().Trim();
                //}
                //if (ddlremark.SelectedItem.ToString().ToUpper().Contains("MARCH"))
                //{
                //    lc.Remark = ddlremark.SelectedItem.ToString().Trim() + ' ' + ddlyear.SelectedItem.ToString();
                //}
                //else
                //{
                //    lc.Remark = ddlremark.SelectedItem.ToString().Trim();
                //}
                lc.siw = txtstandard.Text.ToUpper().Trim();
                lc.siw2 = txt1.Text.Trim();
                lc.seat_no = txtseat.Text.Trim();
                string remark = "LC_Issued";
                lc.lcremark = remark;
                lc.tablename = txttablename.Text;
                lc.userid = Session["emp_id"].ToString();
                lc.lastschool = txtschool.Text.Trim();
                lc.type = "insert";
                string msg = "";

                if (txtseat.Text.Length < 10 && txtseat.Text != "")
                {
                    notifys("Invalid Seat Number", "#D44950");
                    return;
                }
                if (txtstandard.Text == "")
                {
                    notifys("Enter Standard", "#D44950");
                    return;
                }
                if (ddlstandard.SelectedIndex == 0)
                {
                    notifys("Select standard", "#D44950");
                    return;
                }
                if (txt1.Text == "")
                {
                    notifys("Enter Standard", "#D44950");
                    return;
                }
                if (txtls.Text == "")
                {
                    notifys("Select proper date of leaving", "#D44950");
                    return;
                }
                if (ddlconduct.SelectedItem.ToString().Contains("--Select--"))
                {
                    notifys("Select Conduct", "#D44950");
                    return;
                }
                if (ddlprog.SelectedItem.ToString().Contains("--Select--"))
                {
                    notifys("Select Progress", "#D44950");
                    return;
                }
                //if (txtreason.ToString().Trim()=="")
                //{
                //    notifys("Enter Reason", "#D44950");
                //    return;
                //}
                //if (txtremark.ToString().Trim() == "")
                //{
                //    notifys("Enter Remark", "#D44950");
                //    return;
                //}
                //if (ddlyear.SelectedItem.ToString().Contains("--select--") && (ddlreason.SelectedItem.ToString().ToUpper().Contains("MARCH") || ddlremark.SelectedItem.ToString().ToUpper().Contains("MARCH")))
                //{
                //    notifys("Select Year", "#D44950");
                //    return;
                //}
                string jsonString3 = JsonHelper.JsonSerializer<LC>(lc);
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
                btns.Visible = false;
                btnc.Visible = false;
                refresh();
                disable();
                return;
            }
            else if (btns.Text == "Update")
            {
                if (txtseat.Text.Length < 10 && txtseat.Text != "")
                {
                    notifys("invalid Seat no", "#D44950");
                    return;
                }
                if (ddlstandard.SelectedIndex == 0)
                {
                    notifys("Select standard", "#D44950");
                    return;
                }
                if (txtstandard.Text == "")
                {
                    notifys("Enter Standard", "#D44950");
                    return;
                }
                if (txt1.Text == "")
                {
                    notifys("Enter Standard", "#D44950");
                    return;
                }
                if (txtls.Text == "")
                {
                    notifys("Select proper date of leaving", "#D44950");
                    return;
                }
                if (ddlconduct.SelectedItem.ToString().Contains("--Select--"))
                {
                    notifys("Select Conduct", "#D44950");
                    return;
                }
                if (ddlprog.SelectedItem.ToString().Contains("--Select--"))
                {
                    notifys("Select Progress", "#D44950");
                    return;
                }
                // txt changed to dropdown field reason and remark

                //if (txtreason.ToString().Trim() == "")
                //{
                //    notifys("Enter Reason", "#D44950");
                //    return;
                //}
                //if (txtremark.ToString().Trim() == "")
                //{
                //    notifys("Enter Remark", "#D44950");
                //    return;
                //}

                if (ddlreason.SelectedValue == "--select--")
                {
                    notifys("Enter Reason", "#D44950");
                    return;
                }

                if (ddlremk.SelectedValue == "--select--")
                {
                    notifys("Enter Remark", "#D44950");
                    return;
                }


                //if (ddlyear.SelectedItem.ToString().Contains("--select--") && (ddlreason.SelectedItem.ToString().ToUpper().Contains("MARCH") || ddlremark.SelectedItem.ToString().ToUpper().Contains("MARCH")))
                //{
                //    notifys("Select Year", "#D44950");
                //    return;
                //}
                lc.Ayid = Session["acdyear"].ToString();
                string urlalias = cls.urls();
                string url4 = @urlalias + "load/";
                lc.sid = Session["stud_id"].ToString().Trim();
                lc.lcno = txtlid.Text;
                lc.dol = txtls.Text;
                lc.conduct = ddlconduct.SelectedItem.ToString().Trim();
                lc.progress = ddlprog.SelectedItem.ToString().Trim();
                lc.standard = ddlstandard.SelectedValue.Trim();
                lc.Reason = ddlreason.SelectedValue.Trim();
                //if (ddlreason.SelectedItem.ToString().ToUpper().Contains("MARCH"))
                //{
                //    lc.Reason = ddlreason.SelectedItem.ToString().Trim() + ' ' + ddlyear.SelectedItem.ToString();
                //}
                //else
                //{
                //    lc.Reason = ddlreason.SelectedItem.ToString().Trim();
                //}
                //if (ddlremark.SelectedItem.ToString().ToUpper().Contains("MARCH"))
                //{
                //    lc.Remark = ddlremark.SelectedItem.ToString().Trim() + ' ' + ddlyear.SelectedItem.ToString();
                //}
                //else
                //{
                //    lc.Remark = ddlremark.SelectedItem.ToString().Trim();
                //}
                //lc.Reason = txtreason.Text.Trim().ToUpper();
                //lc.Remark = txtremark.Text.Trim().ToUpper();

                lc.Reason = ddlreason.SelectedValue.Trim();
                lc.Remark = ddlremk.SelectedValue.Trim();
                lc.siw = txtstandard.Text.ToUpper().Trim();
                lc.siw2 = txt1.Text.Trim();
                lc.seat_no = txtseat.Text.Trim();
                lc.userid = Session["emp_id"].ToString();
                lc.lastschool = txtschool.Text.Trim();
                lc.type = "Update";
                string msg = "";


                string jsonString4 = JsonHelper.JsonSerializer<LC>(lc);
                var httprequest4 = (HttpWebRequest)WebRequest.Create(url4);
                httprequest4.ContentType = "application/json";
                httprequest4.Method = "POST";
                using (var streamWriter3 = new StreamWriter(httprequest4.GetRequestStream()))
                {
                    streamWriter3.Write(jsonString4);
                    streamWriter3.Flush();
                    streamWriter3.Close();
                }
                var httpresponse4 = (HttpWebResponse)httprequest4.GetResponse();
                using (var streamReader4 = new StreamReader(httpresponse4.GetResponseStream()))
                {
                    string result4 = streamReader4.ReadToEnd();
                    if (result4.ToString().Contains("true") == true)
                    {
                        notifys("Updated Successfully", "#198104");
                    }
                    else
                    {

                        notifys("Data Not Saved", "#D44950");
                    }
                }
                notifys("Updated Successfully", "#198104");
                btns.Text = "Save";
                refresh();
                disable();
                btnissue.Visible = false;
                btns.Visible = false;
                btnc.Visible = false;
                return;
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

    protected void txtstandard_TextChanged(object sender, EventArgs e)
    {

    }

    protected void txt1_TextChanged(object sender, EventArgs e)
    {
        if (txt1.Text != "")
        {
            if (txt1.Text == "1")
            {
                txtstandard.Text = "FIRST";
            }
            if (txt1.Text == "2")
            {
                txtstandard.Text = "SECOND";
            }
            if (txt1.Text == "3")
            {
                txtstandard.Text = "THIRD";
            }
            if (txt1.Text == "4")
            {
                txtstandard.Text = "FOURTH";
            }
            if (txt1.Text == "5")
            {
                txtstandard.Text = "FIFTH";
            }
            if (txt1.Text == "6")
            {
                txtstandard.Text = "SIXTH";
            }
            if (txt1.Text == "7")
            {
                txtstandard.Text = "SEVENTH";
            }
            if (txt1.Text == "8")
            {
                txtstandard.Text = "EIGHTH";
            }
            if (txt1.Text == "9")
            {
                txtstandard.Text = "NINETH";
            }
            if (txt1.Text == "10")
            {
                txtstandard.Text = "TENTH";
            }
        }
    }

    protected void ddlreason_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlremark_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void txtid_TextChanged(object sender, EventArgs e)
    {
        try
        {

            if (txtid.Text.Trim() == "")
            {
            }
            else
            {
                string urlalias = cls.urls();

                lc.Ayid = Session["acdyear"].ToString();
                string url = @urlalias + "load/";
                lc.sid = txtid.Text.Trim();
                lc.type = "select";
                lc.tablename = Session["emp_id"].ToString();

                string jsonString = JsonHelper.JsonSerializer<LC>(lc);
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
                    if (dataSet.Tables[0].Rows.Count > 1)
                    {
                        //ddlload();
                        grid2.DataSource = dataSet.Tables[1];
                        grid2.DataBind();
                        grid2.Visible = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal('modal1');", true);
                    }
                    else if (dataSet.Tables[0].Rows.Count == 1)
                    {
                        // ddlload();
                        load_std();
                        string url1 = @urlalias + "load/";
                        lc.sid = dataSet.Tables[0].Rows[0]["Student_id"].ToString().Trim();
                        Session["stud_id"] = dataSet.Tables[0].Rows[0]["Student_id"].ToString().Trim();
                        lc.type = "get";
                        lc.tablename = Session["emp_id"].ToString();

                        string jsonString1 = JsonHelper.JsonSerializer<LC>(lc);
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
                                string url2 = @urlalias + "load/";
                                lc.sid = dataSet.Tables[0].Rows[0]["Student_id"].ToString().Trim();
                                lc.type = "modify";
                                string jsonString2 = JsonHelper.JsonSerializer<LC>(lc);
                                var httprequest2 = (HttpWebRequest)WebRequest.Create(url2);
                                httprequest2.ContentType = "application/json";
                                httprequest2.Method = "POST";

                                using (var streamWriter2 = new StreamWriter(httprequest2.GetRequestStream()))
                                {
                                    streamWriter2.Write(jsonString2);
                                    streamWriter2.Flush();
                                    streamWriter2.Close();
                                }

                                var httpresponse2 = (HttpWebResponse)httprequest2.GetResponse();
                                using (var streamReader2 = new StreamReader(httpresponse2.GetResponseStream()))
                                {
                                    string result2 = streamReader2.ReadToEnd();
                                    DataSet dataSet2 = JsonConvert.DeserializeObject<DataSet>(result2);
                                    if (dataSet2.Tables[0].Rows.Count > 0)
                                    {
                                        enable();
                                        btns.Text = "Update";
                                        btnissue.Visible = true;
                                        txtstud_id.Value = dataSet.Tables[0].Rows[0]["Student_id"].ToString().Trim();
                                        txtsname.Text = dataSet2.Tables["abc"].Rows[0]["stud_L_name"].ToString();
                                        txtfname.Text = dataSet2.Tables["abc"].Rows[0]["stud_F_name"].ToString();
                                        txtmname.Text = dataSet2.Tables["abc"].Rows[0]["stud_m_name"].ToString();
                                        txtmoname.Text = dataSet2.Tables["abc"].Rows[0]["stud_mo_name"].ToString();
                                        txtadhar.Text = dataSet2.Tables["abc"].Rows[0]["aadhar_no"].ToString();
                                        txtn.Text = dataSet2.Tables["abc"].Rows[0]["nationality"].ToString();
                                        txtmt.Text = dataSet2.Tables["abc"].Rows[0]["mother_tongue"].ToString();
                                        txtdob.Text = dataSet2.Tables["abc"].Rows[0]["dob"].ToString();





                                        //txtpob.Text = dataSet2.Tables["abc"].Rows[0]["birth_place"].ToString();
                                        //txttal.Text = dataSet2.Tables["abc"].Rows[0]["Taluka"].ToString();
                                        //txtdis.Text = dataSet2.Tables["abc"].Rows[0]["dist"].ToString();
                                        //txtstate.Text = dataSet2.Tables["abc"].Rows[0]["state"].ToString();
                                        string[] array = dataSet2.Tables["abc"].Rows[0]["birth_place"].ToString().Split(',');
                                        if (array.Length == 5)
                                        {

                                            txtpob.Text = array[0];
                                            txttal.Text = array[1];
                                            txtdis.Text = array[2];
                                            txtstate.Text = array[3];
                                            txtstate.Text = array[4];
                                        }
                                        else if (array.Length == 4)
                                        {
                                            txtpob.Text = array[0];
                                            txttal.Text = array[1];
                                            txtdis.Text = array[2];
                                            txtstate.Text = array[3];
                                            //txt_vilgecity.Text = array[0];
                                            //txt_taluka1.Text = array[1];
                                            //txt_distrct1.Text = array[2];
                                            //txt_state1.Text = array[3];


                                        }
                                        else if (array.Length == 3)
                                        {
                                            //    txt_vilgecity.Text = array[0];
                                            //    txt_taluka1.Text = array[1];
                                            //    txt_distrct1.Text = array[2];
                                            txtpob.Text = array[0];
                                            txttal.Text = array[1];
                                            txtdis.Text = array[2];
                                        }
                                        else if (array.Length == 2)
                                        {
                                            txtpob.Text = array[0];
                                            txttal.Text = array[1];
                                            //txt_vilgecity.Text = array[0];
                                            //    txt_taluka1.Text = array[1];
                                        }
                                        else if (array.Length == 1)
                                        {
                                            txtpob.Text = array[0];
                                        }




                                        txtsaral.Text = dataSet2.Tables["abc"].Rows[0]["saral_id"].ToString();
                                        txtcast.Text = dataSet2.Tables["abc"].Rows[0]["caste"].ToString();
                                        txtscast.Text = dataSet2.Tables["abc"].Rows[0]["subcast_name"].ToString();
                                        txtlid.Text = dataSet2.Tables["abc"].Rows[0]["Lc_No"].ToString();
                                        txtschool.Text = dataSet2.Tables["abc"].Rows[0]["last_school_name"].ToString();
                                        txtboa.Text = dataSet2.Tables["abc"].Rows[0]["date_of_admission"].ToString();
                                        txtstandard.Text = dataSet2.Tables["abc"].Rows[0]["standard_in_which"].ToString();
                                        txt1.Text = dataSet2.Tables["abc"].Rows[0]["standard_in_which_in_numbers"].ToString();
                                        txtseat.Text = dataSet2.Tables["abc"].Rows[0]["seat_no"].ToString();
                                        txtls.Text = Convert.ToDateTime(dataSet2.Tables["abc"].Rows[0]["Date_of_leaving"]).ToString("dd/MM/yyyy");
                                        ddlconduct.Text = dataSet2.Tables["abc"].Rows[0]["Conduct"].ToString();

                                        txtissue.Value = dataSet2.Tables["abc"].Rows[0]["issue"].ToString();
                                        Session["lcno"] = dataSet2.Tables["abc"].Rows[0]["Lc_No"].ToString();
                                        // string reason = 
                                        // changed to txt to ddl field reason and remark
                                        //ddlprog.Text = dataSet2.Tables["abc"].Rows[0]["progress"].ToString();
                                        //txtreason.Text= dataSet2.Tables["abc"].Rows[0]["Reason"].ToString();
                                        //txtremark.Text= dataSet2.Tables["abc"].Rows[0]["Remark"].ToString();
                                        //ddlreason.SelectedValue= dataSet2.Tables["abc"].Rows[0]["Reason"].ToString();
                                        //ddlremk.SelectedValue= dataSet2.Tables["abc"].Rows[0]["Remark"].ToString();
                                        //ddlstandard.SelectedValue = dataSet2.Tables["abc"].Rows[0]["stdid"].ToString();


                                        if (dataSet2.Tables["abc"].Rows[0]["Reason"].ToString() == "")
                                        {
                                            ddlreason.SelectedValue = "--select--";
                                        }
                                        else
                                        {
                                            ddlreason.SelectedValue = dataSet2.Tables["abc"].Rows[0]["Reason"].ToString();
                                        }

                                        if (dataSet2.Tables["abc"].Rows[0]["Remark"].ToString() == "")
                                        {
                                            ddlremk.SelectedValue = "--select--";
                                        }
                                        else
                                        {
                                            ddlremk.SelectedValue = dataSet2.Tables["abc"].Rows[0]["Remark"].ToString();
                                        }


                                        ddlstandard.SelectedValue = dataSet2.Tables["abc"].Rows[0]["stdid"].ToString();

                                        if (dataSet2.Tables["abc"].Rows[0]["Conduct"].ToString() == "")
                                        {
                                            ddlconduct.Text = "--Select--";
                                        }
                                        else
                                        {
                                            ddlconduct.Text = dataSet2.Tables["abc"].Rows[0]["Conduct"].ToString();
                                        }
                                        if (dataSet2.Tables["abc"].Rows[0]["progress"].ToString() == "")
                                        {
                                            ddlprog.Text = "--Select--";
                                        }
                                        else
                                        {
                                            ddlprog.Text = dataSet2.Tables["abc"].Rows[0]["progress"].ToString();
                                        }

                                        //if (reason.ToString().Contains(Session["syear"].ToString()) || reason.ToString().Contains(Session["syear1"].ToString()))
                                        //{
                                        //    string reason1 = reason.Remove(reason.Length - 5);
                                        //    ddlreason.SelectedValue = reason1.ToString();
                                        //}
                                        //else
                                        //{
                                        //    ddlreason.Text = dataSet2.Tables["abc"].Rows[0]["Reason"].ToString();
                                        //}
                                        //string remark = dataSet2.Tables["abc"].Rows[0]["Remark"].ToString();
                                        //if (remark.ToString().Contains(Session["syear"].ToString()) == true || remark.ToString().Contains(Session["syear1"].ToString()) == true)
                                        //{
                                        //    string remark1 = remark.Remove(remark.Length - 5);
                                        //    ddlremark.SelectedValue = remark1;
                                        //}
                                        //else
                                        //{
                                        //    ddlremark.Text = dataSet2.Tables["abc"].Rows[0]["Remark"].ToString();
                                        //}
                                        //if (remark.ToString().Contains(Session["syear"].ToString()) == true || reason.ToString().Contains(Session["syear"].ToString()) == true)
                                        //{
                                        //    ddlyear.SelectedValue = Session["syear"].ToString();
                                        //}
                                        //else if (remark.ToString().Contains(Session["syear1"].ToString()) == true || reason.ToString().Contains(Session["syear1"].ToString()) == true)
                                        //{
                                        //    ddlyear.SelectedValue = Session["syear1"].ToString();
                                        //}
                                        txtid.Enabled = false;

                                    }
                                }
                            }

                            else
                            {
                                if (dataSet.Tables[0].Rows.Count > 0)
                                {
                                    enable();
                                    txtstud_id.Value = dataSet.Tables[0].Rows[0]["Student_id"].ToString().Trim();
                                    lc.grno = dataSet.Tables["leaving"].Rows[0]["gr_no"].ToString();
                                    txtsname.Text = dataSet.Tables["leaving"].Rows[0]["stud_L_name"].ToString();
                                    txtfname.Text = dataSet.Tables["leaving"].Rows[0]["stud_F_name"].ToString();
                                    txtmname.Text = dataSet.Tables["leaving"].Rows[0]["stud_m_name"].ToString();
                                    txtmoname.Text = dataSet.Tables["leaving"].Rows[0]["stud_mo_name"].ToString();
                                    txtadhar.Text = dataSet.Tables["leaving"].Rows[0]["aadhar_no"].ToString();
                                    txtn.Text = dataSet.Tables["leaving"].Rows[0]["nationality"].ToString();
                                    txtmt.Text = dataSet.Tables["leaving"].Rows[0]["mother_tongue"].ToString();
                                    txtdob.Text = dataSet.Tables["leaving"].Rows[0]["dob"].ToString();

                                    string[] array = dataSet.Tables["leaving"].Rows[0]["birth_place"].ToString().Split(',');
                                    if (array.Length == 5)
                                    {

                                        txtpob.Text = array[0];
                                        txttal.Text = array[1];
                                        txtdis.Text = array[2];
                                        txtstate.Text = array[3];
                                        //txtstate.Text = array[4];
                                    }
                                    else if (array.Length == 4)
                                    {
                                        txtpob.Text = array[0];
                                        txttal.Text = array[1];
                                        txtdis.Text = array[2];
                                        txtstate.Text = array[3];
                                        //txt_vilgecity.Text = array[0];
                                        //txt_taluka1.Text = array[1];
                                        //txt_distrct1.Text = array[2];
                                        //txt_state1.Text = array[3];


                                    }
                                    else if (array.Length == 3)
                                    {
                                        //    txt_vilgecity.Text = array[0];
                                        //    txt_taluka1.Text = array[1];
                                        //    txt_distrct1.Text = array[2];
                                        txtpob.Text = array[0];
                                        txttal.Text = array[1];
                                        txtdis.Text = array[2];
                                    }
                                    else if (array.Length == 2)
                                    {
                                        txtpob.Text = array[0];
                                        txttal.Text = array[1];
                                        //txt_vilgecity.Text = array[0];
                                        //    txt_taluka1.Text = array[1];
                                    }
                                    else if (array.Length == 1)
                                    {
                                        txtpob.Text = array[0];
                                    }
                                    txtpob.Text = dataSet.Tables["leaving"].Rows[0]["birth_place"].ToString();
                                    txttal.Text = dataSet.Tables["leaving"].Rows[0]["Taluka"].ToString();
                                    txtdis.Text = dataSet.Tables["leaving"].Rows[0]["dist"].ToString();
                                    txtstate.Text = dataSet.Tables["leaving"].Rows[0]["state"].ToString();

                                    txtsaral.Text = dataSet.Tables["leaving"].Rows[0]["saral_id"].ToString();
                                    txtcast.Text = dataSet.Tables["leaving"].Rows[0]["caste"].ToString();
                                    txtscast.Text = dataSet.Tables["leaving"].Rows[0]["subcast_name"].ToString();
                                    txtlid.Text = "";
                                    txtschool.Text = dataSet.Tables["leaving"].Rows[0]["last_school_name"].ToString();
                                    txtboa.Text = dataSet.Tables["leaving"].Rows[0]["date_of_admission"].ToString();
                                    ddlstandard.SelectedValue = dataSet.Tables["leaving"].Rows[0]["stdid"].ToString();
                                    txtstandard.Text = "";
                                    txt1.Text = "";
                                    txtls.Text = "";
                                    txtseat.Text = "";
                                    if (txtstud_id.Value.Contains("E") == true)
                                    {
                                        ddlconduct.Text = "Good";
                                    }
                                    else if (txtstud_id.Value.Contains("M") == true)
                                    {
                                        ddlconduct.Text = "चांगली";
                                    }
                                    load();
                                    txtid.Enabled = false;
                                }
                            }
                        }
                        btns.Visible = true;
                        btnc.Visible = true;
                    }
                    else
                    {
                        txtid.Text = "";
                        notifys("Enter Valid Student ID Or GR Number", "#D44950");
                        return;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }


    protected void btnissue_Click(object sender, EventArgs e)
    {
        try
        {
            string urlalias = cls.urls();

            string url6 = @urlalias + "load/";
            lc.sid = txtid.Text.Trim();
            lc.type = "gettype";
            lc.Ayid = Session["acdyear"].ToString();
            Session["stud_id"] = txtid.Text;
            lc.studid = Session["stud_id"].ToString();

            lc.lcno = Session["lcno"].ToString();

            string jsonString6 = JsonHelper.JsonSerializer<LC>(lc);
            var httprequest6 = (HttpWebRequest)WebRequest.Create(url6);
            httprequest6.ContentType = "application/json";
            httprequest6.Method = "POST";

            using (var streamWriter6 = new StreamWriter(httprequest6.GetRequestStream()))
            {
                streamWriter6.Write(jsonString6);
                streamWriter6.Flush();
                streamWriter6.Close();
            }

            var httpresponse6 = (HttpWebResponse)httprequest6.GetResponse();
            using (var streamReader6 = new StreamReader(httpresponse6.GetResponseStream()))
            {
                string result6 = streamReader6.ReadToEnd();
                DataSet dataSet6 = JsonConvert.DeserializeObject<DataSet>(result6);

                string value = dataSet6.Tables["issue"].Rows[0]["issue"].ToString();
                int count = 0;
                count = Convert.ToInt32(value) + 1;

                string url7 = @urlalias + "load/";
                lc.sid = dataSet6.Tables["issue"].Rows[0]["Stud_id"].ToString().Trim();
                lc.lcno = Session["lcno"].ToString();
                lc.dol = txtls.Text;
                lc.conduct = ddlconduct.SelectedItem.ToString();
                lc.progress = ddlprog.SelectedItem.ToString();
                lc.Reason = ddlreason.SelectedValue.Trim();
                lc.Remark = ddlremk.SelectedValue.Trim();
                //lc.Reason = txtreason.Text.ToUpper().Trim();
                //lc.Remark = txtremark.Text.ToUpper().Trim();
                lc.standard = ddlstandard.SelectedValue;
                lc.siw = txtstandard.Text.ToUpper().Trim();
                lc.siw2 = txt1.Text;
                lc.seat_no = txtseat.Text;
                lc.issue = txtvalue.Value;
                lc.lastschool = txtschool.Text.Trim();
                lc.type = "set";

                string jsonString7 = JsonHelper.JsonSerializer<LC>(lc);
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
                    refresh();
                    //  ddlload();
                    disable();
                    btns.Visible = false;
                    btnc.Visible = false;
                    btnissue.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void ddlyear_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void grid2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "select")
            {
                int RowIndex = Convert.ToInt32(e.CommandArgument.ToString());
                GridViewRow row = grid2.Rows[RowIndex];
                string stud_id = row.Cells[0].Text.ToString();
                Session["stud_id"] = stud_id.ToString();
                string urlalias = cls.urls();
                string url1 = @urlalias + "load/";

                lc.sid = stud_id.ToString().Trim();
                lc.type = "get";
                lc.Ayid = Session["acdyear"].ToString();

                string jsonString1 = JsonHelper.JsonSerializer<LC>(lc);
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

                    if (dataSet1.Tables[0].Rows.Count == 0)
                    {
                        btns.Text = "Save";

                        string url = @urlalias + "load/";
                        lc.sid = stud_id.ToString().Trim();
                        load_std();
                        lc.type = "select";

                        string jsonString = JsonHelper.JsonSerializer<LC>(lc);
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
                                enable();
                                txtstud_id.Value = dataSet.Tables["leaving"].Rows[0]["Student_id"].ToString().Trim();
                                lc.grno = dataSet.Tables["leaving"].Rows[0]["gr_no"].ToString();
                                txtsname.Text = dataSet.Tables["leaving"].Rows[0]["stud_L_name"].ToString();
                                txtfname.Text = dataSet.Tables["leaving"].Rows[0]["stud_F_name"].ToString();
                                txtmname.Text = dataSet.Tables["leaving"].Rows[0]["stud_m_name"].ToString();
                                txtmoname.Text = dataSet.Tables["leaving"].Rows[0]["stud_mo_name"].ToString();
                                txtadhar.Text = dataSet.Tables["leaving"].Rows[0]["aadhar_no"].ToString();
                                txtn.Text = dataSet.Tables["leaving"].Rows[0]["nationality"].ToString();
                                txtmt.Text = dataSet.Tables["leaving"].Rows[0]["mother_tongue"].ToString();
                                txtdob.Text = dataSet.Tables["leaving"].Rows[0]["dob"].ToString();
                                txtpob.Text = dataSet.Tables["leaving"].Rows[0]["birth_place"].ToString();
                                txttal.Text = dataSet.Tables["leaving"].Rows[0]["Taluka"].ToString();
                                txtdis.Text = dataSet.Tables["leaving"].Rows[0]["dist"].ToString();
                                txtstate.Text = dataSet.Tables["leaving"].Rows[0]["state"].ToString();
                                txtsaral.Text = dataSet.Tables["leaving"].Rows[0]["saral_id"].ToString();
                                txtcast.Text = dataSet.Tables["leaving"].Rows[0]["caste"].ToString();
                                txtscast.Text = dataSet.Tables["leaving"].Rows[0]["subcast_name"].ToString();
                                txtlid.Text = "";
                                txtschool.Text = dataSet.Tables["leaving"].Rows[0]["last_school_name"].ToString();
                                ddlstandard.SelectedValue = dataSet.Tables["leaving"].Rows[0]["stdid"].ToString();
                                txtboa.Text = dataSet.Tables["leaving"].Rows[0]["date_of_admission"].ToString();
                                txtstandard.Text = "";
                                txt1.Text = "";
                                txtls.Text = "";
                                txtseat.Text = "";
                                if (txtstud_id.Value.Contains("E") == true)
                                {
                                    ddlconduct.Text = "Good";
                                }
                                else if (txtstud_id.Value.Contains("M") == true)
                                {
                                    ddlconduct.Text = "चांगली";
                                }
                                load();
                                txtid.Enabled = false;
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "disp_confirm", "$('#modal1').modal('hide');", true);
                                btns.Visible = true;
                                btnc.Visible = true;
                            }

                            else
                            {
                                notifys("No data found", "#D44950");
                                return;
                            }
                        }
                    }
                    else if (dataSet1.Tables[0].Rows.Count > 0)
                    {
                        btns.Text = "Update";
                        btnissue.Visible = true;
                        string url2 = @urlalias + "load/";
                        lc.sid = stud_id.ToString().Trim();
                        load_std();
                        lc.type = "modify";

                        btnissue.Visible = true;
                        string jsonString2 = JsonHelper.JsonSerializer<LC>(lc);
                        var httprequest2 = (HttpWebRequest)WebRequest.Create(url2);
                        httprequest2.ContentType = "application/json";
                        httprequest2.Method = "POST";

                        using (var streamWriter2 = new StreamWriter(httprequest2.GetRequestStream()))
                        {
                            streamWriter2.Write(jsonString2);
                            streamWriter2.Flush();
                            streamWriter2.Close();
                        }
                        var httpresponse2 = (HttpWebResponse)httprequest2.GetResponse();
                        using (var streamReader2 = new StreamReader(httpresponse2.GetResponseStream()))
                        {
                            string result2 = streamReader2.ReadToEnd();
                            DataSet dataSet2 = JsonConvert.DeserializeObject<DataSet>(result2);
                            if (dataSet2.Tables[0].Rows.Count > 0)
                            {
                                enable();
                                txtstud_id.Value = dataSet2.Tables["abc"].Rows[0]["Student_id"].ToString().Trim();
                                txtsname.Text = dataSet2.Tables["abc"].Rows[0]["stud_L_name"].ToString();
                                txtfname.Text = dataSet2.Tables["abc"].Rows[0]["stud_F_name"].ToString();
                                txtmname.Text = dataSet2.Tables["abc"].Rows[0]["stud_m_name"].ToString();
                                txtmoname.Text = dataSet2.Tables["abc"].Rows[0]["stud_mo_name"].ToString();
                                txtadhar.Text = dataSet2.Tables["abc"].Rows[0]["aadhar_no"].ToString();
                                txtn.Text = dataSet2.Tables["abc"].Rows[0]["nationality"].ToString();
                                txtmt.Text = dataSet2.Tables["abc"].Rows[0]["mother_tongue"].ToString();
                                txtdob.Text = dataSet2.Tables["abc"].Rows[0]["dob"].ToString();



                                txtpob.Text = dataSet2.Tables["abc"].Rows[0]["birth_place"].ToString();
                                txttal.Text = dataSet2.Tables["abc"].Rows[0]["Taluka"].ToString();
                                txtdis.Text = dataSet2.Tables["abc"].Rows[0]["dist"].ToString();
                                txtstate.Text = dataSet2.Tables["abc"].Rows[0]["state"].ToString();
                                txtsaral.Text = dataSet2.Tables["abc"].Rows[0]["saral_id"].ToString();
                                txtcast.Text = dataSet2.Tables["abc"].Rows[0]["caste"].ToString();
                                txtscast.Text = dataSet2.Tables["abc"].Rows[0]["subcast_name"].ToString();
                                txtlid.Text = dataSet2.Tables["abc"].Rows[0]["Lc_No"].ToString();
                                txtschool.Text = dataSet2.Tables["abc"].Rows[0]["last_school_name"].ToString();
                                txtboa.Text = dataSet2.Tables["abc"].Rows[0]["date_of_admission"].ToString();
                                txtstandard.Text = dataSet2.Tables["abc"].Rows[0]["standard_in_which"].ToString();
                                txt1.Text = dataSet2.Tables["abc"].Rows[0]["standard_in_which_in_numbers"].ToString();
                                txtseat.Text = dataSet2.Tables["abc"].Rows[0]["seat_no"].ToString();
                                txtls.Text = Convert.ToDateTime(dataSet2.Tables["abc"].Rows[0]["Date_of_leaving"]).ToString("dd/MM/yyyy");
                                ddlconduct.Text = dataSet2.Tables["abc"].Rows[0]["Conduct"].ToString();
                                ddlprog.Text = dataSet2.Tables["abc"].Rows[0]["progress"].ToString();
                                ddlstandard.SelectedValue = dataSet2.Tables["abc"].Rows[0]["stdid"].ToString();
                                txtissue.Value = dataSet2.Tables["abc"].Rows[0]["issue"].ToString();
                                //txtreason.Text= dataSet2.Tables["abc"].Rows[0]["Reason"].ToString();
                                //txtremark.Text= dataSet2.Tables["abc"].Rows[0]["Remark"].ToString();
                                //ddlreason.SelectedValue= dataSet2.Tables["abc"].Rows[0]["Reason"].ToString();
                                //ddlremk.SelectedValue= dataSet2.Tables["abc"].Rows[0]["Remark"].ToString();

                                if (dataSet2.Tables["abc"].Rows[0]["Reason"].ToString() == "")
                                {
                                    ddlreason.SelectedValue = "--select--";
                                }
                                else
                                {
                                    ddlreason.SelectedValue = dataSet2.Tables["abc"].Rows[0]["Reason"].ToString();
                                }

                                if (dataSet2.Tables["abc"].Rows[0]["Remark"].ToString() == "")
                                {
                                    ddlremk.SelectedValue = "--select--";
                                }
                                else
                                {
                                    ddlremk.SelectedValue = dataSet2.Tables["abc"].Rows[0]["Remark"].ToString();
                                }



                                //string reason = dataSet2.Tables["abc"].Rows[0]["Reason"].ToString();
                                //if (reason.ToString().Contains(Session["syear"].ToString()) || reason.ToString().Contains(Session["syear1"].ToString()))
                                //{
                                //    string reason1 = reason.Remove(reason.Length - 5);
                                //    ddlreason.SelectedValue = reason1.ToString();
                                //}
                                //else
                                //{
                                //    ddlreason.Text = dataSet2.Tables["abc"].Rows[0]["Reason"].ToString();
                                //}
                                //string remark = dataSet2.Tables["abc"].Rows[0]["Remark"].ToString();
                                //if (remark.ToString().Contains(Session["syear"].ToString()) == true || remark.ToString().Contains(Session["syear1"].ToString()) == true)
                                //{
                                //    string remark1 = remark.Remove(remark.Length - 5);
                                //    ddlremark.SelectedValue = remark1;
                                //}
                                //else
                                //{
                                //    ddlremark.Text = dataSet2.Tables["abc"].Rows[0]["Remark"].ToString();
                                //}
                                //if (remark.ToString().Contains(Session["syear"].ToString()) == true || reason.ToString().Contains(Session["syear"].ToString()) == true)
                                //{
                                //    ddlyear.SelectedValue = Session["syear"].ToString();
                                //}
                                //else if (remark.ToString().Contains(Session["syear1"].ToString()) == true || reason.ToString().Contains(Session["syear1"].ToString()) == true)
                                //{
                                //    ddlyear.SelectedValue = Session["syear1"].ToString();
                                //}
                                txtid.Enabled = false;
                                Session["lcno"] = txtlid.Text;
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "disp_confirm", "$('#modal1').modal('hide');", true);
                                btns.Visible = true;
                                btnc.Visible = true;
                            }
                            else
                            {
                                notifys("No data found", "#D44950");
                                return;

                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void btnclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("Leaving_certificate.aspx");
    }

    //protected void addreasn_Click(object sender, EventArgs e)
    //{

    //}

    protected void addremk_Click(object sender, EventArgs e)
    {

    }

    protected void btn_addreason_Click(object sender, EventArgs e)
    {
        if (txt_reasn_name.Text.Trim() == "")
        {
            notifys("Enter Reason", "#198104");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "departmodal", "$('#departmodal').modal();", true);


        }
        else
        {

            lc.type = "addReason";
            lc.reason_text = txt_reasn_name.Text.Trim();
            string urlalias = cls.urls();
            string url3 = @urlalias + "load/";
            string jsonString3 = JsonHelper.JsonSerializer<LC>(lc);
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
                    notifys("Reason Added Successfully", "#198104");
                    txt_reasn_name.Text = "";


                }
                else
                {

                    notifys("Reason Not Saved", "#D44950");
                }
                BINDGVREASON();
                reason_load();



            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "departmodal", "$('#departmodal').modal();", true);
        }


    }

    protected void btn_addremark_Click(object sender, EventArgs e)
    {

        if (txt_Reason.Text.Trim() == "")
        {
            notifys("Enter Remark", "#D44950");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal_Remark", "$('#modal_Remark').modal();", true);
        }
        else
        {

            lc.type = "addRemark";
            lc.remark_text = txt_Reason.Text.Trim();
            string urlalias = cls.urls();
            string url3 = @urlalias + "load/";
            string jsonString3 = JsonHelper.JsonSerializer<LC>(lc);
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
                    notifys("Remark  Added Successfully", "#198104");
                    txt_Reason.Text = "";

                    remarkload();
                    BINDGVRemark();


                }
                else
                {

                    notifys("Remark Not Saved", "#D44950");
                }
                //reason_load();



            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal_Remark", "$('#modal_Remark').modal();", true);
        }

    }


    //protected void Unnamed_Click(object sender, EventArgs e)
    //{

    //}





    protected void BINDGVREASON()
    {
        string urlalias = cls.urls();
        string url111 = @urlalias + "load/";
        lc.type = "Reason";
        string jsonString111 = JsonHelper.JsonSerializer<LC>(lc);
        var httprequest111 = (HttpWebRequest)WebRequest.Create(url111);
        httprequest111.ContentType = "application/json";
        httprequest111.Method = "POST";


        using (var streamWriter111 = new StreamWriter(httprequest111.GetRequestStream()))
        {
            streamWriter111.Write(jsonString111);
            streamWriter111.Flush();
            streamWriter111.Close();
        }
        var httpresponse111 = (HttpWebResponse)httprequest111.GetResponse();
        using (var streamReader111 = new StreamReader(httpresponse111.GetResponseStream()))
        {
            string result111 = streamReader111.ReadToEnd();
            DataSet dataSet111 = JsonConvert.DeserializeObject<DataSet>(result111);
            reasongridview.DataSource = dataSet111.Tables[0];

            reasongridview.DataBind();

        }
    }





    protected void BINDGVRemark()
    {
        string urlalias = cls.urls();
        string url111 = @urlalias + "load/";
        lc.type = "Remark";
        string jsonString111 = JsonHelper.JsonSerializer<LC>(lc);
        var httprequest111 = (HttpWebRequest)WebRequest.Create(url111);
        httprequest111.ContentType = "application/json";
        httprequest111.Method = "POST";


        using (var streamWriter111 = new StreamWriter(httprequest111.GetRequestStream()))
        {
            streamWriter111.Write(jsonString111);
            streamWriter111.Flush();
            streamWriter111.Close();
        }
        var httpresponse111 = (HttpWebResponse)httprequest111.GetResponse();
        using (var streamReader111 = new StreamReader(httpresponse111.GetResponseStream()))
        {
            string result111 = streamReader111.ReadToEnd();
            DataSet dataSet111 = JsonConvert.DeserializeObject<DataSet>(result111);
            Griddltrmk.DataSource = dataSet111.Tables[0];

            Griddltrmk.DataBind();

        }
    }




    protected void btnrsndlt_Click(object sender, EventArgs e)
    {
        string reason = "";

        foreach (GridViewRow row in reasongridview.Rows)
        {

            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = (row.FindControl("rsnchkbx") as CheckBox);
               
                    if (chkRow.Checked == true)
                    {
                        if (reason == "")
                        {
                            reason = (row.FindControl("gridrsn") as TextBox).Text;
                            lc.Reason = reason;
                        }
                        else
                        {
                            reason = reason + "/" + (row.FindControl("gridrsn") as TextBox).Text;
                            lc.Reason = reason;
                        }
                    }                           
            }
        }
        if (reason == "")
        {           
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Select atleast one reason', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);          
        }
        else
        {





            string[] arr = reason.Split('/');
            for (int i = 0; i < arr.Length; i++)

            {
                string deletecheckdata = "Select * from Leaving_tbl where Reason =N'" + (arr[i]).Replace("'", "''") + "' and del_flag=0";

                DataTable dt = new DataTable(deletecheckdata);

                dt = cls.filldatatable(deletecheckdata);

                if (dt.Rows.Count > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Reason cannot be deleted ', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
                    BINDGVREASON();
                   
                }

                else

                {
                    string urlalias = cls.urls();
                    string url111 = @urlalias + "load/";
                    lc.type = "deletereason";
                    lc.Reason = reason;
                    string jsonString111 = JsonHelper.JsonSerializer<LC>(lc);
                    var httprequest111 = (HttpWebRequest)WebRequest.Create(url111);
                    httprequest111.ContentType = "application/json";
                    httprequest111.Method = "POST";


                    using (var streamWriter111 = new StreamWriter(httprequest111.GetRequestStream()))
                    {
                        streamWriter111.Write(jsonString111);
                        streamWriter111.Flush();
                        streamWriter111.Close();
                    }
                    var httpresponse111 = (HttpWebResponse)httprequest111.GetResponse();
                    using (var streamReader111 = new StreamReader(httpresponse111.GetResponseStream()))
                    {
                        string result = streamReader111.ReadToEnd();

                        if (result.ToString().Contains("Saved") == true)
                        {

                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Reason deleted successfully', { color: '#fff', background: '#198104', blur: 0.2, delay: 0 });", true);

                            BINDGVREASON();
                            reason_load();
                        }
                        else
                        {

                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + result.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
                            BINDGVREASON();
                            reason_load();
                        }


                    }

                }


            }






          
        }




    }
    DataTable GetDataTable(GridView dtg)
    {
        DataTable rsndt = new DataTable();

        // add the columns to the datatable            
        rsndt.Columns.Add("Reason", typeof(string));


        foreach (GridViewRow row in dtg.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                string Reason = (row.FindControl("gridrsn") as TextBox).Text;
                string gridchkbx = (row.FindControl("rsnchkbx") as CheckBox).Text;
               
                CheckBox gridrsnchk = (CheckBox)row.FindControl("rsnchkbx");
                if (gridrsnchk.Checked == true)
                {
                    DataRow dr1;
                    dr1 = rsndt.NewRow();
                    dr1[0] = Reason.ToString();
                    //dr1[1] = gridchkbx.ToString();
                    //dt.Rows.Add(dr1);
                }
            }
        }
        return rsndt;
    }

    protected void close_Click(object sender, EventArgs e)
    {
        reason_load();


    }

    protected void btndltrmk_Click(object sender, EventArgs e)
    {
        string remark = "";

        foreach (GridViewRow row in Griddltrmk.Rows)
        {

            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = (row.FindControl("rmkchkbx") as CheckBox);

                if (chkRow.Checked == true)
                {
                    if (remark == "")
                    {
                        remark = (row.FindControl("gridremark") as TextBox).Text;
                        lc.Remark = remark;
                    }
                    else
                    {
                        remark = remark + "/" + (row.FindControl("gridremark") as TextBox).Text;
                        lc.Remark = remark;
                    }
                }
            }
        }
        if (remark == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Select atleast one remark', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
        else
        {




            string[] arr = remark.Split('/');
            for (int i = 0; i < arr.Length; i++)

            {
                string deletecheckdata = "Select * from Leaving_tbl where Remark =N'" + (arr[i]).Replace("'", "''") + "' and del_flag=0";

                DataTable dt = new DataTable(deletecheckdata);

                dt = cls.filldatatable(deletecheckdata);

                if (dt.Rows.Count > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Remark cannot be deleted ', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
                    BINDGVRemark();
                   
                }


                else
                {


                    string urlalias = cls.urls();
                    string url111 = @urlalias + "load/";
                    lc.type = "deleteremark";
                    lc.Reason = remark;
                    string jsonString111 = JsonHelper.JsonSerializer<LC>(lc);
                    var httprequest111 = (HttpWebRequest)WebRequest.Create(url111);
                    httprequest111.ContentType = "application/json";
                    httprequest111.Method = "POST";


                    using (var streamWriter111 = new StreamWriter(httprequest111.GetRequestStream()))
                    {
                        streamWriter111.Write(jsonString111);
                        streamWriter111.Flush();
                        streamWriter111.Close();
                    }
                    var httpresponse111 = (HttpWebResponse)httprequest111.GetResponse();
                    using (var streamReader111 = new StreamReader(httpresponse111.GetResponseStream()))
                    {
                        string result = streamReader111.ReadToEnd();

                        if (result.ToString().Contains("Saved") == true)
                        {

                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Remark deleted successfully', { color: '#fff', background: '#198104', blur: 0.2, delay: 0 });", true);
                            BINDGVRemark();
                            remarkload();
                        }
                        else
                        {

                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + result.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
                            BINDGVRemark();
                            remarkload();
                        }


                    }
                }


            }




        }


    }
}
