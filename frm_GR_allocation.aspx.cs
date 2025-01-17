using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;


public partial class frm_GR_allocation : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ConnectionString);
    Class1 cls = new Class1();
    common sbm = new common();
    string display;
    GRALLO grn = new GRALLO();
    DataTable dt;


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (!Page.IsPostBack)
            {
                filter_panel.Visible = false;
                medium_select();
              
                chkedit.Visible = false;
                sname.Enabled = false;
                txtid.Enabled = false;
                txtgr.Enabled = false;

                btn_export.Visible = false;
                grid_card.Visible = false;
               
                ddlyear.Visible = false;
                rdo1.Enabled = false;
                rdo2.Enabled = false;
                flg = "";
            }
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    public class JsonHelper
    {
        /// <summary>
        /// JSON Serialization
        /// </summary>
        public static string JsonSerializer<T>(T t)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, t);
            string jsonString = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();
            return jsonString;
        }
        /// <summary>
        /// JSON Deserialization
        /// </summary>
        public static T JsonDeserialize<T>(string jsonString)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T obj = (T)ser.ReadObject(ms);
            return obj;
        }
    }

    private void medium_select()
    {
        try
        {
            string type = "ddlfill";
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
                ddlmedium.Items.Insert(0, "--Select--");
                ddlmedium.SelectedIndex = 0;

            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void ddlmedium_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        try
        {
            btn_export.Visible = false;
            grid1.DataSource = null;
            grid1.DataBind();
            chkedit.Visible = false;
            if (ddlmedium.SelectedIndex>0)
            {
                DataSet ds = (DataSet)Session["DSLIST"];
              
                foreach (DataTable table in ds.Tables)
                {
                    if (table.TableName.Equals(ddlmedium.SelectedValue.ToString()))
                    {
                        ddlclass.DataSource = table;
                        ddlclass.DataTextField = "std_name";
                        ddlclass.DataValueField = "std_id";
                        ddlclass.DataBind();
                        ddlclass.Enabled = true;
                        ddlclass.Items.Insert(0, "--SELECT--");
                        ddlclass.SelectedIndex = 0;
                    }
                }
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "loaddate()", true);
            }
            else
            {
                ddlclass.DataSource = "";
                ddlclass.DataBind();
            }
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }

    }

    protected void ddlclass_SelectedIndexChanged(object sender, EventArgs e)
    {


        btn_export.Visible = false;
        class_sel("");
        
       
    }

    public void class_sel(string extsort)
    {
        try
        {
            if (ddlmedium.SelectedIndex > 0 && ddlclass.SelectedIndex > 0)
            {
                string medium = ddlmedium.SelectedValue;
                string cls1 = ddlclass.SelectedValue;
                string ayid = Session["acdyear"].ToString();

                string urlalias = cls.urls();
                string url = @urlalias + "updt/";


                grn.medium = medium;
                grn.class_id = cls1;
                grn.ayid = ayid;
                grn.type_of_query = "str";
                grn.sort = extsort;

                string jsonString = JsonHelper.JsonSerializer<GRALLO>(grn);


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
                    DataSet ds = JsonConvert.DeserializeObject<DataSet>(result);
                    dt = ds.Tables[0];
                    ViewState["dr"] = dt;
                    grid1.DataSource = ViewState["dr"];
                    grid1.DataBind();



                    grid_card.Visible = true;
                    rdo1.Enabled = true;
                    rdo2.Enabled = true;
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        flg = "";
                        chkedit.Visible = true;
                        btn_export.Visible = true;

                    }
                    else
                    {
                        btn_export.Visible = false;
                        chkedit.Visible = false;

                        if (flg != "")
                        {
                            if (flg == "lname")
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify( 'No Student Found of such Surname', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                                return;
                            }
                            else if (flg == "fname")
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify( 'No Student Found of such First Name', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                                return;
                            }
                            else if (flg == "mname")
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify( 'No Student Found of such Middle Name', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                                return;
                            }
                            else if (flg == "grno")
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify( 'No Student Found of such GR No.', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                                return;
                            }
                            else if (flg == "studentid")
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify( 'No Student Found of such Student ID', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                                return;
                            }
                        }
                    }
                    Session["ds1"] = dt;

                }
                if (dt.Rows.Count == 0)
                {
                    btn_export.Visible = false;
                    grid_card.Visible = false;
                    display = "No data found for this Medium and Class ";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify( '" + display + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                    return;
                }
                else if (display == "no")
                {
                    btn_export.Visible = false;
                    grid_card.Visible = false;
                    display = "No data found for this Medium and Class select another";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify( '" + display + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                    return;
                }
            }
            else
            {
                if (ddlmedium.SelectedIndex == 0)
                {
                    btn_export.Visible = false;
                    chkedit.Visible = false;
                    grid1.DataSource = null;
                    grid1.DataBind();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify( 'Select Medium', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                    return;
               

                }
                else if (ddlclass.SelectedIndex == 0)
                {
                    btn_export.Visible = false;
                    chkedit.Visible = false;    
                    grid1.DataSource = null;
                    grid1.DataBind();

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify( 'Select Class', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                    return;
                }
            }

            
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    protected void ddlyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string output3 = "";
            string pre = ddlyear.SelectedItem.Value;
            string confirmValue = Request.Form["confirm_value"];
            string[] words = confirmValue.Split(',');
            Array.Reverse(words);
            if (words[0] == "Yes")
            {
                Boolean chkexists = false;
                

                for (int i = 0; i < grid1.Rows.Count; i++)
                {
                    if (i == grid1.Rows.Count - 1)
                    {

                        output3 = output3 + ((Label)grid1.Rows[i].FindControl("lblid")).Text;
                    }
                    else
                    {

                        output3 = output3 + ((Label)grid1.Rows[i].FindControl("lblid")).Text + ',';
                    }
                }
                output3 = output3.TrimEnd(',');
                string html = string.Empty;

                string urlalias = cls.urls();
                string url = @urlalias + "updt/";
               

                grn.type_of_query = "previ";
                grn.student_id = output3;
                grn.ayid = ddlyear.SelectedItem.Value;
                string jsonString = JsonHelper.JsonSerializer<GRALLO>(grn);
               

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(jsonString);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                JObject results = JObject.Parse(reader.ReadToEnd());
                
                DataSet rs1 = new DataSet();
                rs1.Tables.Add(JsonConvert.DeserializeObject<DataTable>(results["drop3"].ToString()));


                if (rs1.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < grid1.Rows.Count; i++)
                    {

                        TextBox new_gr = (TextBox)grid1.Rows[i].Cells[0].FindControl("lblgr") as TextBox;
                        new_gr.Text = rs1.Tables[0].Rows[i]["msg"].ToString();
                        chkexists = true;
                    }
                }

                if (chkexists == false)
                {
                    display = "NO Previous GR no. Found";

                    chk2.Checked = false;
                    ddlyear.Items.Clear();
                    ddlyear.Visible = false;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify( '" + display + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                    return;
                }
                else
                {
                    string display = "Some Previous GR no. Found";

                  
                    chk2.Checked = false;
                    ddlyear.SelectedIndex = 0;
                    ddlyear.Visible = false;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify( '" + display + "', { color: '#fff', background: '#168029', blur: 0.2, delay: 0 })", true);
                    return;
                }
            }

            else
            {
                chk2.Checked = false;
                ddlyear.Items.Clear();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }
    private void ExportGridToExcel()
    {
        try
        {
           

            GridView gv = new GridView();
            gv.DataSource = ViewState["dr"];
            gv.DataBind();
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=GR Allocation" + DateTime.Now + ".xls");
            Response.ContentType = "application/vnd.ms-excel";

            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            gv.RenderControl(hw);
            Response.Output.Write(sw.ToString());


            Response.End();


        }


        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }

    }
    protected void chkedit_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (chkedit.Checked == true)
            {
                filter_panel.Visible = true;
                sname.Enabled = true;
                txtid.Enabled = true;
                txtgr.Enabled = true;
                class_sel("");
                flg = "";
                ddlmedium.Enabled = false;
                ddlclass.Enabled = false;
            }
            else
            {
                filter_panel.Visible = false;
               
                sname.Text = "";
                txtid.Text = "";
                txtgr.Text = "";
                sname.Enabled = false;
                txtid.Enabled = false;
                txtgr.Enabled = false;
                class_sel("");
                flg = "";
                ddlmedium.Enabled = true;
                ddlclass.Enabled = true;
            }
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void sname_TextChanged(object sender, EventArgs e)
    {
        txtgr.Text = "";
        txtid.Text = "";
        class_updt();
        chkedit.Visible = true;
    }


    string flg="";
    public void class_updt()
    {
        string extquery = "";
        try
        {
            if (sname.Text != "" || txtfname.Text != "" || txtmname.Text != "" || txtid.Text != "" || txtgr.Text != "")
            {

               
                if (sname.Text != "")
                {
                    flg = "lname";
                    extquery = " and asm.stud_L_name like N'" + sname.Text.Trim() + "%'";
                  

                }
                if (txtfname.Text != "")
                {
                    flg = "fname";
                    extquery = extquery + " and asm.stud_F_name like N'" + txtfname.Text.Trim() + "%'";
                   

                }
                if (txtmname.Text != "")
                {
                    flg = "mname";
                    extquery = extquery + " and asm.stud_m_name like N'" + txtmname.Text.Trim() + "%'";
                 

                }
              
                if (txtgr.Text != "")
                {
                    flg="grno";
                    
                    extquery = extquery + " and say.gr_no = N'" + txtgr.Text.Trim() + "'";

                }
                if (txtid.Text != "")
                {
                    flg = "studentid";
                    
                    extquery = extquery + " and asm.Student_id = '" + txtid.Text.Trim() + "'";

                }
                class_sel(extquery);

                
            }
            else
            {
                class_sel(extquery);
                DataTable dt1 = (System.Data.DataTable)Session["ds1"];
                grid1.DataSource = dt1;
                grid1.DataBind();
                
            }
        }
        catch (Exception ex)
        {
            if(flg=="0")
            {
                sname.Text = "";
                display = "Please Enter Proper Student Name";
            }
            else if(flg=="1")
            {
                txtgr.Text = "";
                display = "Please Enter Proper GR No";
            }
            else if (flg == "2")
            {
                txtid.Text = "";
                display = "Please Enter Proper Student Id";
            }
            else
            {
                display = "Please Enter Proper Data";
            }

            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
           
        }
    }
    protected void txtgr_TextChanged(object sender, EventArgs e)
    {
        txtid.Text = "";
        sname.Text = "";
        txtfname.Text = "";
        txtmname.Text = "";
        class_updt();

    }
    protected void txtid_TextChanged(object sender, EventArgs e)
    {
        txtgr.Text = "";
        sname.Text = "";
        txtfname.Text = "";
        txtmname.Text = "";
        class_updt();
    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = grid1.SelectedRow;

            string output = "";
            string output2 = "";


            chk2.Checked = false;
            
                if (grid1.Rows.Count == 0)
                {
                   
                    if (ddlmedium.SelectedIndex ==0)
                    {
                       
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify( 'Select Medium', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                        return;
                       
                    }
                    else if (ddlclass.SelectedIndex == 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify( 'Select Class', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                        return;
                    }
                    else if (grid1.Rows.Count == 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify('No Students found for selected Medium and Standard', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                        return;
                    }

                }
                else{
              
                    for (int i = 0; i < grid1.Rows.Count; i++)
                    {

                       
                            if (i == grid1.Rows.Count - 1)
                            {
                                output = output + ((TextBox)grid1.Rows[i].FindControl("lblgr")).Text;
                                output2 = output2 + ((Label)grid1.Rows[i].FindControl("lblid")).Text;
                            }
                            else
                            {
                                output = output + ((TextBox)grid1.Rows[i].FindControl("lblgr")).Text + ',';
                                output2 = output2 + ((Label)grid1.Rows[i].FindControl("lblid")).Text + ',';
                            }
                       

                    }
                    if (ddlmedium.SelectedItem.Text != "--Select--" && ddlclass.SelectedItem.Text != "")
                    {
                        string html = string.Empty;

                        string urlalias = cls.urls();
                        string url = @urlalias + "updt/";
                       
                        GRALLO gra = new GRALLO();
                        gra.ayid = Session["acdyear"].ToString();
                        gra.gr_no = output;
                        gra.student_id = output2;
                        gra.medium = ddlmedium.SelectedItem.Value.ToString();
                        gra.type_of_query = "setgr";
                        string jsonString = JsonHelper.JsonSerializer<GRALLO>(gra);
                       

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
                            string result = streamReader.ReadToEnd();
                            DataSet ds = JsonConvert.DeserializeObject<DataSet>(result);
                            dt = ds.Tables[0];
                            if (dt.Rows.Count > 0)
                            {
                                display = dt.Rows[0]["msg"].ToString();
                                class_sel("");
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify( '" + display + "', { color: '#fff', background: '#168029', blur: 0.2, delay: 0 })", true);
                                return;
                            }
                            if (dt.Rows.Count == 0)
                            {
                                grid_card.Visible = false;
                                display = "No data found for this Medium and Class ";
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify( '" + display + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                                return;
                            }
                            else if (display == "no")
                            {
                                grid_card.Visible = false;
                                display = "No data found for this Medium and Class select another";
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify( '" + display + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                                return;
                            }

                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify( 'Select Medium and Class', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                        return;
                    }
                }
            
           
          
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }


    protected void btn_clear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
     
    }
  

    protected void chk2_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            string ayid = Session["acdyear"].ToString();
            int med = ddlmedium.SelectedIndex;
            int cls1 = ddlclass.SelectedIndex;

            if (med != 0 & cls1 != 0)
            {

                if (chk2.Checked)
                {
                    ddlyear.Visible = true;
                    grn.type_of_query = "fill";
                    grn.ayid = ayid;
                    string urlalias = cls.urls();
                    string url = @urlalias + "updt/";

                    

                    string jsonString = JsonHelper.JsonSerializer<GRALLO>(grn);


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
                        DataSet ds = JsonConvert.DeserializeObject<DataSet>(result);

                        ddlyear.DataSource = ds;
                        ddlyear.DataTextField = "duration";
                        ddlyear.DataValueField = "ayid";
                        ddlyear.DataBind();
                        ddlyear.Items.Insert(0, "--Select--");
                    }
                }
                else
                {
                    ddlyear.Items.Clear();
                    ddlyear.Visible = false;
                }
            }
            else
            {
                string display = "Select Medium And Class";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "myalert", "alert('" + display + "');", true);
                chk2.Checked = false;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }


    public void Bindgrid()
    {
        grid1.DataSource = ViewState["dr"] as DataTable;
        grid1.DataBind();
    }



    protected void grid1_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            DataTable dt = ViewState["dr"] as DataTable;
            dt.Rows.Clear();
            for (int i = 0; i < grid1.Rows.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = ((Label)grid1.Rows[i].FindControl("lblid")).Text;
                dr[1] = ((Label)grid1.Rows[i].FindControl("lblname")).Text;
                dr[2] = ((TextBox)grid1.Rows[i].FindControl("lblgr")).Text;
                dr[3] = ((Label)grid1.Rows[i].FindControl("lblcate")).Text;
                dr[4] = ((Label)grid1.Rows[i].FindControl("lbldate")).Text;
                dr[5] = ((Label)grid1.Rows[i].FindControl("lblfid")).Text;
               

                dt.Rows.Add(dr);
            }
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToString(ViewState["sort"]).ToUpper() == "ASC")
                {
                    dt.DefaultView.Sort = e.SortExpression + " Desc";
                    ViewState["sort"] = "DESC";
                }
                else
                {
                    dt.DefaultView.Sort = e.SortExpression + " Asc";
                    ViewState["sort"] = "ASC";
                }
            }
            ViewState["dr"] = dt;
            this.Bindgrid();

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
  
    }
    protected void btn_export_Click(object sender, EventArgs e)
    {
        ExportGridToExcel();
    }
    protected void txtfname_TextChanged(object sender, EventArgs e)
    {
        txtgr.Text = "";
        txtid.Text = "";
       
        class_updt();
        chkedit.Visible = true;
    }
   
    protected void txtmname_TextChanged(object sender, EventArgs e)
    {
        txtgr.Text = "";
        txtid.Text = "";
        class_updt();
        chkedit.Visible = true;
    }
}


