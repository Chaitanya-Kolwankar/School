using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frm_grade_master : System.Web.UI.Page
{
    grd_master gm = new grd_master();
    Class1 cls = new Class1();
    common cm = new common();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["emp_id"].ToString() == null || Session["emp_id"] == "")
            {
                Response.Redirect("Login.aspx?msg=session");
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
        public static T JsonDeserialize<T>(string jsonString)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T obj = (T)ser.ReadObject(ms);
            return obj;
        }
    }

    public string DataTableToJSON(DataTable table)
    {
        string JSONString = string.Empty;
        JSONString = JsonConvert.SerializeObject(table);
        return JSONString;
    }

    public void useless()
    {
        gm.medium_id = null;
        gm.class_id = null;
        gm.username = null;
        gm.copy_class_id = null;
        gm.grade_id = null;
        gm.type = null;
        gm.ayid = null;
        gm.table = null;



    }
    public void fillclass()
    {
        try
        {

            useless();
            string urlalias = cls.urls();
            string url = @urlalias + "Common/";
            cm.type = "ddlfill";
            cm.year = Session["acdyear"].ToString();

            string jsonString = JsonHelper.JsonSerializer<common>(cm);
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
                Session["DSLIST"] = dslist;

            }

        }
        catch (Exception ex)
        {

        }
    }
    public void ddlfill()
    {
        try
        {

            btnprev.Enabled = false;
            btn_Add.Enabled = false;
            lstclass.Enabled = false;
            useless();
            string urlalias = cls.urls();
            string url = @urlalias + "Common/";
            cm.type = "ddlfill";
            cm.year = Session["acdyear"].ToString();

            string jsonString = JsonHelper.JsonSerializer<common>(cm);
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
                Session["DSLIST"] = dslist;

                DataTable dt1 = dslist.Tables[0];
                ddlmedium.DataSource = dt1;
                ddlmedium.DataTextField = "medium";
                ddlmedium.DataValueField = "med_id";
                ddlmedium.DataBind();
                ddlmedium.Items.Insert(0, "--Select--");
                ddlmedium.SelectedIndex = 0;

                DataTable dt2 = dslist.Tables[1];
                ddlprevyear.DataSource = dt2;
                ddlprevyear.DataTextField = "duration";
                ddlprevyear.DataValueField = "AYID";
                ddlprevyear.DataBind();
                ddlprevyear.Items.Insert(0, "--Select--");
                ddlprevyear.SelectedIndex = 0;
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
            btnprev.Enabled = false;
            btn_Add.Enabled = false;

            grd_grade.DataSource = "";
            grd_grade.DataBind();

            fillclass();
            if (ddlmedium.SelectedIndex > 0)
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
                        ddlclass.Items.Insert(0, "--Select--");
                        ddlclass.SelectedIndex = 0;


                    }
                }


            }
            else if (ddlmedium.SelectedIndex == 0)
            {

                lstclass.Enabled = false;
                ViewState["dt"] = null;
                ddlclass.DataSource = "";
                ddlclass.DataBind();
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }
    protected void BindGrid()
    {
        try
        {
            DataTable dt = ViewState["dt"] as DataTable;

            if (dt.Rows.Count > 0)
            {

                grd_grade.DataSource = dt;
                grd_grade.DataBind();
                chkprev.Enabled = false;
                if (chkprev.Checked == true)
                {
                    chkprev.Enabled = true;
                    btn_Add.Enabled = false;
                    btnprev.Enabled = false;
                }
            }
            else
            {
                grd_grade.DataSource = null;
                grd_grade.DataBind();
                chkprev.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void btn_Add_Click(object sender, EventArgs e)
    {
        string ddclass = "";
        try
        {
            if (ddlmedium.SelectedIndex > 0 && ddlclass.SelectedIndex > 0)
            {
                int i = 0;
                bool check = false;
                DataTable dt = ViewState["dt"] as DataTable;
                if (dt.Rows.Count > 0)
                {
                    for (i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["min"].ToString() == "")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Lower  cannot be empty', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                            ViewState["dt"] = dt;
                            grd_grade.EditIndex = i;
                            this.BindGrid();

                            ((TextBox)grd_grade.Rows[i].FindControl("txtmin")).Focus();

                            check = true;

                        }
                        else if (dt.Rows[i]["max"].ToString() == "")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Higher  cannot be empty', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                            ViewState["dt"] = dt;
                            grd_grade.EditIndex = i;
                            this.BindGrid();


                            ((TextBox)grd_grade.Rows[i].FindControl("txtmax")).Focus();

                            check = true;

                        }
                        else if (dt.Rows[i]["grade"].ToString() == "")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Grade cannot be empty', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                            ViewState["dt"] = dt;
                            grd_grade.EditIndex = i;
                            this.BindGrid();


                            ((TextBox)grd_grade.Rows[i].FindControl("txtgrade")).Focus();

                            check = true;

                        }
                        else if (dt.Rows[i]["remark"].ToString() == "")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Remark cannot be empty', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                            ViewState["dt"] = dt;
                            grd_grade.EditIndex = i;
                            this.BindGrid();


                            ((TextBox)grd_grade.Rows[i].FindControl("txtremark")).Focus();
                            check = true;

                        }
                    }
                    if (check == false)
                    {
                        dt.Rows.Add("");
                        //dt.Rows[i]["min"] = "";
                        //dt.Rows[i]["max"] = "";
                        dt.Rows[i]["flag"] = "0";
                        ViewState["dt"] = dt;
                        grd_grade.EditIndex = i;
                        this.BindGrid();
                        ((TextBox)grd_grade.Rows[i].FindControl("txtmin")).Focus();
                        ((TextBox)grd_grade.Rows[i].FindControl("txtmax")).Focus();
                        ((TextBox)grd_grade.Rows[i].FindControl("txtgrade")).Focus();
                        ((TextBox)grd_grade.Rows[i].FindControl("txtremark")).Focus();
                        btn_Save.Enabled = false;
                        btn_Add.Enabled = false;
                        btnprev.Enabled = false;
                        //foreach (GridViewRow row in grd_grade.Rows)
                        //{
                        //    if (row.RowType == DataControlRowType.DataRow)
                        //    {
                        //        if (row.RowIndex != i)
                        //        {
                        //            LinkButton lnkedit = (row.FindControl("btnedit") as LinkButton);
                        //            lnkedit.Enabled = false;
                        //            LinkButton lnkrow = (row.FindControl("btndelete") as LinkButton);
                        //            lnkrow.Enabled = false;
                        //            lnkrow.OnClientClick = null;
                        //        }
                        //    }
                        //}
                    }
                }
                else
                {
                    dt.Rows.Add("");
                    //dt.Rows[0]["min"] = "";
                    //dt.Rows[0]["max"] = "";
                    dt.Rows[0]["flag"] = "0";
                    ViewState["dt"] = dt;
                    grd_grade.EditIndex = i;
                    this.BindGrid();
                    ((TextBox)grd_grade.Rows[i].FindControl("txtmin")).Focus();
                    ((TextBox)grd_grade.Rows[i].FindControl("txtmax")).Focus();
                    ((TextBox)grd_grade.Rows[i].FindControl("txtgrade")).Focus();
                    ((TextBox)grd_grade.Rows[i].FindControl("txtremark")).Focus();
                    btn_Save.Enabled = false;
                    btn_Add.Enabled = false;

                    btnprev.Enabled = false;
                    //foreach (GridViewRow row in grd_grade.Rows)
                    //{
                    //    if (row.RowType == DataControlRowType.DataRow)
                    //    {
                    //        if (row.RowIndex != i)
                    //        {
                    //            LinkButton lnkedit = (row.FindControl("btnedit") as LinkButton);
                    //            lnkedit.Enabled = false;
                    //            LinkButton lnkrow = (row.FindControl("btndelete") as LinkButton);
                    //            lnkrow.Enabled = false;
                    //            lnkrow.OnClientClick = null;
                    //        }
                    //    }
                    //}

                }
            }
            else
            {
                if (ddlmedium.SelectedIndex == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Select the Medium', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                }
                if (ddclass == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Select atleast one Standard', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                }
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }


    protected void btnupdate_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
            DataTable dt = ViewState["dt"] as DataTable;
            double min;
            double max;
            if (((TextBox)grd_grade.Rows[row.RowIndex].FindControl("txtmin")).Text == "")
            {
                min = 0;
            }
            else
            {
                min = Convert.ToDouble(((TextBox)grd_grade.Rows[row.RowIndex].FindControl("txtmin")).Text);
            }

            if (((TextBox)grd_grade.Rows[row.RowIndex].FindControl("txtmax")).Text == "")
            {
                max = 0;
            }
            else
            {
                max = Convert.ToDouble(((TextBox)grd_grade.Rows[row.RowIndex].FindControl("txtmax")).Text);
            }


            string grade = ((TextBox)grd_grade.Rows[row.RowIndex].FindControl("txtgrade")).Text;
            string remark = ((TextBox)grd_grade.Rows[row.RowIndex].FindControl("txtremark")).Text;
            if (min != 0 && max != 0 && grade.ToString() != "" && remark.ToString() != "")
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    double mindt;
                    double maxdt;
                    if (dt.Rows[i]["min"].ToString() == "")
                    {
                        mindt = 0;
                    }
                    else
                    {
                        mindt = Convert.ToDouble(dt.Rows[i]["min"].ToString().ToUpper());
                    }
                    if (dt.Rows[i]["min"].ToString() == "")
                    {
                        maxdt = 0;
                    }
                    else
                    {
                        maxdt = Convert.ToDouble(dt.Rows[i]["max"].ToString().ToUpper());
                    }

                    string gradedt = dt.Rows[i]["grade"].ToString().ToUpper();
                    string remarkdt = dt.Rows[i]["remark"].ToString().ToUpper();
                    if (min >= max)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Lower percentage cannot be greater or Equal to higher percentage', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
                        ((TextBox)grd_grade.Rows[row.RowIndex].FindControl("txtmin")).Text = "";
                        grd_grade.EditIndex = row.RowIndex;
                        this.BindGrid();
                        return;
                    }
                    if (min > 100)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Lower percentage cannot be greater than 100 percentage', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
                        ((TextBox)grd_grade.Rows[row.RowIndex].FindControl("txtmax")).Text = "";
                        grd_grade.EditIndex = row.RowIndex;
                        this.BindGrid();
                        return;
                    }
                    if (max > 100)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Higher percentage cannot be greater than 100 percentage', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
                        ((TextBox)grd_grade.Rows[row.RowIndex].FindControl("txtmax")).Text = "";
                        grd_grade.EditIndex = row.RowIndex;
                        this.BindGrid();
                        return;
                    }
                    else if (min >= mindt && min <= maxdt && dt.Rows.Count > 1 && i != row.RowIndex)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Lower percentage is already assigned to grade', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
                        ((TextBox)grd_grade.Rows[row.RowIndex].FindControl("txtmin")).Text = "";
                        grd_grade.EditIndex = row.RowIndex;
                        this.BindGrid();
                        return;
                    }
                    else if (max >= mindt && max <= maxdt && dt.Rows.Count > 1 && i != row.RowIndex)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Higher percentage is already assigned to grade', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
                        ((TextBox)grd_grade.Rows[row.RowIndex].FindControl("txtmax")).Text = "";
                        grd_grade.EditIndex = row.RowIndex;
                        this.BindGrid();
                        return;
                    }
                    else if (grade.ToString().ToUpper() == gradedt.ToString() && dt.Rows.Count > 1 && i != row.RowIndex)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Grade cannot be same', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
                        ((TextBox)grd_grade.Rows[row.RowIndex].FindControl("txtgrade")).Text = "";
                        grd_grade.EditIndex = row.RowIndex;
                        this.BindGrid();
                        return;
                    }

                }
                dt.Rows[row.RowIndex]["min"] = ((TextBox)grd_grade.Rows[row.RowIndex].FindControl("txtmin")).Text;
                dt.Rows[row.RowIndex]["max"] = ((TextBox)grd_grade.Rows[row.RowIndex].FindControl("txtmax")).Text;
                dt.Rows[row.RowIndex]["grade"] = ((TextBox)grd_grade.Rows[row.RowIndex].FindControl("txtgrade")).Text.ToUpper();
                dt.Rows[row.RowIndex]["remark"] = ((TextBox)grd_grade.Rows[row.RowIndex].FindControl("txtremark")).Text.ToUpper();
                dt.Rows[row.RowIndex]["flag"] = "1";
                ViewState["dt"] = dt;
                grd_grade.EditIndex = -1;
                this.BindGrid();
                btn_Save.Enabled = true;
                btn_Add.Enabled = true;
                btnprev.Enabled = true;
            }
            else
            {
                if (min == 0 && max == 0 && grade.ToString() == "" && remark.ToString() == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Fill all fields', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                    return;
                }
                else if (min == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Minimum cannot be empty', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                    return;
                }
                else if (max == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Maximum cannot be empty', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                    return;
                }
                else if (grade.ToString() == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Grade cannot be empty', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                    return;
                }
                else if (remark.ToString() == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Remark cannot be empty', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                    return;
                }

                ViewState["dt"] = dt;
                grd_grade.EditIndex = row.RowIndex;
                this.BindGrid();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
            DataTable dt = ViewState["dt"] as DataTable;
            if (((TextBox)grd_grade.Rows[row.RowIndex].FindControl("txtmin")).Text == "" && ((TextBox)grd_grade.Rows[row.RowIndex].FindControl("txtmax")).Text == "" && ((TextBox)grd_grade.Rows[row.RowIndex].FindControl("txtgrade")).Text == "" && ((TextBox)grd_grade.Rows[row.RowIndex].FindControl("txtremark")).Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Fill all fields', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                ViewState["dt"] = dt;
                grd_grade.EditIndex = row.RowIndex;
                this.BindGrid();
            }
            else if (((TextBox)grd_grade.Rows[row.RowIndex].FindControl("txtmin")).Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Lower cannot be empty', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                ViewState["dt"] = dt;
                grd_grade.EditIndex = row.RowIndex;
                this.BindGrid();
            }

            else if (((TextBox)grd_grade.Rows[row.RowIndex].FindControl("txtmax")).Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Higher cannot be empty', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                ViewState["dt"] = dt;
                grd_grade.EditIndex = row.RowIndex;
                this.BindGrid();
            }
            else if (((TextBox)grd_grade.Rows[row.RowIndex].FindControl("txtgrade")).Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Grade cannot be empty', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                ViewState["dt"] = dt;
                grd_grade.EditIndex = row.RowIndex;
                this.BindGrid();
            }
            else if (((TextBox)grd_grade.Rows[row.RowIndex].FindControl("txtremark")).Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Remark cannot be empty', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                ViewState["dt"] = dt;
                grd_grade.EditIndex = row.RowIndex;
                this.BindGrid();
            }
            else
            {
                if (((Label)grd_grade.Rows[row.RowIndex].FindControl("lblflag")).Text == "1" || ((Label)grd_grade.Rows[row.RowIndex].FindControl("lblflag")).Text == "2")
                {
                    grd_grade.EditIndex = -1;
                    this.BindGrid();
                    btn_Save.Enabled = true;
                    btn_Add.Enabled = true;
                    btnprev.Enabled = true;
                }
                else
                {
                    ((TextBox)grd_grade.Rows[row.RowIndex].FindControl("txtmin")).Text = "";
                    ((TextBox)grd_grade.Rows[row.RowIndex].FindControl("txtmax")).Text = "";
                    ((TextBox)grd_grade.Rows[row.RowIndex].FindControl("txtgrade")).Text = "";
                    ((TextBox)grd_grade.Rows[row.RowIndex].FindControl("txtremark")).Text = "";
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }
    protected void btndelete_Click(object sender, EventArgs e)
    {

        string confirmValue = Request.Form["grade"];
        if (confirmValue.Contains(","))
        {
            confirmValue = confirmValue.Split(',').Last();
        }
        if (confirmValue.ToString() == "OK")
        {
            GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;

            bool process = false;
            DataTable dt = ViewState["dt"] as DataTable;
            int i = grd_grade.EditIndex;
            string min, max, grd, rem;

            if (i != -1)
            {
                try
                {
                    min = ((TextBox)grd_grade.Rows[i].FindControl("txtmin")).Text;
                    max = ((TextBox)grd_grade.Rows[i].FindControl("txtmax")).Text;
                    grd = ((TextBox)grd_grade.Rows[i].FindControl("txtgrade")).Text;
                    rem = ((TextBox)grd_grade.Rows[i].FindControl("txtremark")).Text;
                }
                catch (Exception ex)
                {
                    min = ((Label)grd_grade.Rows[i].FindControl("lblmin")).Text;
                    max = ((Label)grd_grade.Rows[i].FindControl("lblmax")).Text;
                    grd = ((Label)grd_grade.Rows[i].FindControl("lblgrade")).Text;
                    rem = ((Label)grd_grade.Rows[i].FindControl("lblremark")).Text;
                }
                if (min.ToString() != "" || max.ToString() != "" || grd.ToString() != "" || rem.ToString() != "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Save the current changes', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);

                }
                else
                {


                    if (dt.Rows[row.RowIndex]["grade_id"].ToString() == "")
                    {
                        dt.Rows.RemoveAt(row.RowIndex);
                    }
                    else
                    {
                        useless();
                        string urlalias = cls.urls();
                        string url = @urlalias + "GradeMaster/";
                        gm.type = "Remove";
                        gm.medium_id = ddlmedium.SelectedValue;
                        gm.class_id = ddlclass.SelectedValue;
                        gm.grade_id = dt.Rows[row.RowIndex]["grade_id"].ToString();
                        gm.ayid = Session["acdyear"].ToString();
                        string jsonString = JsonHelper.JsonSerializer<grd_master>(gm);
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
                            if (result.ToString().Contains("Deleted") == true)
                            {
                                dt.Rows.RemoveAt(i);
                                dt.Rows.RemoveAt(row.RowIndex);
                                process = true;
                            }
                        }
                    }
                    ViewState["dt"] = dt;
                    this.BindGrid();
                    btn_Save.Enabled = true;
                    btn_Add.Enabled = true;
                    btnprev.Enabled = true;

                    if (process == true)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Grade Deleted ', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                    }

                }
            }
            else
            {
                if (dt.Rows[row.RowIndex]["grade_id"].ToString() == "")
                {
                    dt.Rows.RemoveAt(row.RowIndex);
                }
                else
                {
                    useless();
                    string urlalias = cls.urls();
                    string url = @urlalias + "GradeMaster/";
                    gm.type = "Remove";
                    gm.medium_id = ddlmedium.SelectedValue;
                    gm.class_id = ddlclass.SelectedValue;
                    gm.grade_id = dt.Rows[row.RowIndex]["grade_id"].ToString();
                    gm.ayid = Session["acdyear"].ToString();
                    string jsonString = JsonHelper.JsonSerializer<grd_master>(gm);
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
                        if (result.ToString().Contains("Deleted") == true)
                        {
                            dt.Rows.RemoveAt(row.RowIndex);
                            process = true;
                        }
                    }
                }
                ViewState["dt"] = dt;
                this.BindGrid();
                btn_Save.Enabled = true;
                btn_Add.Enabled = true;
                btnprev.Enabled = true;

                if (process == true)
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Grade Deleted Successfully', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                }
            }
        }

    }

    public void fillgrid(string ayid)
    {
        try
        {
            ViewState["dt"] = null;
            useless();
            //btnprev.Enabled = true;
            btn_Add.Enabled = true;
            string urlalias = cls.urls();
            string url = @urlalias + "GradeMaster/";
            gm.type = "fillgrd";
            gm.medium_id = ddlmedium.SelectedValue.ToString();
            gm.class_id = ddlclass.SelectedValue.ToString();
            gm.ayid = ayid;


            string jsonString = JsonHelper.JsonSerializer<grd_master>(gm);
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
                    ViewState["dt"] = dataSet.Tables[0];

                    grd_grade.EditIndex = -1;
                    this.BindGrid();
                    if (chkprev.Checked == true)
                    {
                        foreach (GridViewRow row in grd_grade.Rows)
                        {
                            if (row.RowType == DataControlRowType.DataRow)
                            {
                                LinkButton lnkedit = (row.FindControl("btnedit") as LinkButton);
                                lnkedit.Enabled = false;
                                LinkButton lnkrow = (row.FindControl("btndelete") as LinkButton);
                                lnkrow.Enabled = false;
                                lnkrow.OnClientClick = null;
                            }
                        }
                    }
                }
                else
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("min");
                    dt.Columns.Add("max");
                    dt.Columns.Add("grade");
                    dt.Columns.Add("remark");
                    dt.Columns.Add("flag");
                    dt.Columns.Add("grade_id");
                    ViewState["dt"] = dt;
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('No grade defined', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                    this.BindGrid();


                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void ddlclass_SelectedIndexChanged(object sender, EventArgs e)
    {
        string ddclass = "";
        try
        {
            btnprev.Enabled = false;
            btn_Add.Enabled = false;
            if (ddlclass.SelectedIndex > 0)
            {
                fillgrid(Session["acdyear"].ToString());
                frmclass();
            }
            else
            {
                grd_grade.DataSource = null;
                grd_grade.DataBind();
                ViewState["dt"] = null;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        try
        {
            bool chkup = false, chkins = false;
            string json = "";
            DataTable dt = new DataTable();
            dt.Columns.Add("min");
            dt.Columns.Add("max");
            dt.Columns.Add("grade");
            dt.Columns.Add("remark");
            dt.Columns.Add("action");
            dt.Columns.Add("grade_id");

            if (ddlmedium.SelectedIndex == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Select the Medium', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
                ddlmedium.Focus();
                return;
            }
            if (ddlclass.SelectedIndex == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Select the Class', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
                ddlclass.Focus();
                return;
            }
            else
            {
                foreach (GridViewRow row in grd_grade.Rows)
                {
                    if (chkprev.Checked == true)
                    {
                        DataRow dr = dt.NewRow();
                        dr[0] = ((Label)row.FindControl("lblmin")).Text;
                        dr[1] = ((Label)row.FindControl("lblmax")).Text;
                        dr[2] = ((Label)row.FindControl("lblgrade")).Text;
                        dr[3] = ((Label)row.FindControl("lblremark")).Text;
                        dr[4] = "insert";
                        chkins = true;
                        //dr[5] = ((Label)row.FindControl("lblgradeid")).Text;
                        dt.Rows.Add(dr);
                    }
                    else
                    {
                        if (((Label)row.FindControl("lblflag")).Text != "2")
                        {
                            DataRow dr = dt.NewRow();
                            dr[0] = ((Label)row.FindControl("lblmin")).Text;
                            dr[1] = ((Label)row.FindControl("lblmax")).Text;
                            dr[2] = ((Label)row.FindControl("lblgrade")).Text;
                            dr[3] = ((Label)row.FindControl("lblremark")).Text;


                            if (((Label)row.FindControl("lblgradeid")).Text == "" || ((Label)row.FindControl("lblgradeid")).Text == null)
                            {
                                dr[4] = "insert";
                                chkins = true;
                            }
                            else
                            {
                                dr[4] = "update";
                                chkup = true;
                            }
                            dr[5] = ((Label)row.FindControl("lblgradeid")).Text;
                            dt.Rows.Add(dr);
                        }
                    }
                }
                if (dt.Rows.Count > 0)
                {

                    json = DataTableToJSON(dt);


                    string urlalias = cls.urls();
                    string url = @urlalias + "GradeMaster/";

                    gm.type = "Insert";
                    gm.table = json.ToString();
                    gm.class_id = ddlclass.SelectedValue.ToString();
                    gm.ayid = Session["acdyear"].ToString();
                    gm.medium_id = ddlmedium.SelectedValue.ToString();
                    gm.username = Session["emp_id"].ToString();

                    string jsonString = JsonHelper.JsonSerializer<grd_master>(gm);
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
                        if (result.ToString().Contains("Saved") == true)
                        {

                            if (chkprev.Checked == true)
                            {
                                ddlmedium.Enabled = true;
                                ddlclass.Enabled = true;
                                ddlprevyear.SelectedIndex = 0;
                                divprev.Attributes.Add("style", "display:none");
                            }
                            chkprev.Checked = false;
                            chkprev.Enabled = false;
                            fillgrid(Session["acdyear"].ToString());


                            if (chkup == false && chkins == true)
                            {
                                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Saved Successfully', { color: '#fff', background: '#198104', blur: 0.2, delay: 0 });", true);

                            }
                            else if (chkup == true && chkins == false)
                            {
                                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Updated Successfully', { color: '#fff', background: '#198104', blur: 0.2, delay: 0 });", true);

                            }
                            else if (chkup == true && chkins == true)
                            {
                                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Saved and Updated Successfully', { color: '#fff', background: '#198104', blur: 0.2, delay: 0 });", true);

                            }

                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('No Changes Made', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('No Changes to Save or Update', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
                }
            }





        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }
    protected void ddlfrmclass_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            fillclass();
         
            if (ddlmedium.SelectedIndex > 0 && ddlfrmclass.SelectedIndex > 0)
            {
                DataSet ds = (DataSet)Session["DSLIST"];


                foreach (DataTable table in ds.Tables)
                {
                    if (table.TableName.Equals(ddlmedium.SelectedValue.ToString()))
                    {
                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            DataRow row = table.Rows[i];
                            if (row["std_id"].ToString() == ddlfrmclass.SelectedValue)
                            {
                                row.Delete();


                            }
                        }

                        lstclass.DataSource = table;
                        lstclass.DataTextField = "std_name";
                        lstclass.DataValueField = "std_id";
                        lstclass.DataBind();
                        lstclass.Enabled = true;

                    }
                }


                useless();

                string urlalias = cls.urls();
                string url = @urlalias + "GradeMaster/";
                gm.type = "fillgrd";
                gm.medium_id = ddlmedium.SelectedValue.ToString();
                gm.class_id = ddlfrmclass.SelectedValue.ToString();
                gm.ayid = Session["acdyear"].ToString();


                string jsonString = JsonHelper.JsonSerializer<grd_master>(gm);
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

                        grdprev.DataSource = dataSet.Tables[0];
                        grdprev.DataBind();
                    }
                    else
                    {

                        grdprev.DataSource = null;
                        grdprev.DataBind();
                    }
                }

            }
            else
            {
                grdprev.DataSource = null;
                grdprev.DataBind();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    public void frmclass()
    {
        try
        {
            if (ddlmedium.SelectedIndex > 0)
            {
                lstclass.DataSource = "";
                lstclass.DataBind();
                grdprev.DataSource = null;
                grdprev.DataBind();
                btnprev.Enabled = false;

                useless();
               
                btn_Add.Enabled = true;
                string urlalias = cls.urls();
                string url = @urlalias + "GradeMaster/";
                gm.type = "fillfromclass";
                gm.medium_id = ddlmedium.SelectedValue.ToString();
                gm.ayid = Session["acdyear"].ToString();


                string jsonString = JsonHelper.JsonSerializer<grd_master>(gm);
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

                        btnprev.Enabled = true;
                        ddlfrmclass.DataSource = dataSet.Tables[0];
                        ddlfrmclass.DataTextField = "std_name";
                        ddlfrmclass.DataValueField = "std_id";
                        ddlfrmclass.DataBind();
                        ddlfrmclass.Enabled = true;
                        ddlfrmclass.Items.Insert(0, "--Select--");
                        ddlfrmclass.SelectedIndex = 0;
                    }
                    
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Pop", "$('#myModal').modal('hide');", true);
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Select Medium', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
            }
        }
        catch(Exception ex)
        {

        }
    }

    protected void btnprev_Click(object sender, EventArgs e)
    {
        try
        {

            if (ddlmedium.SelectedIndex > 0)
            {
                frmclass();

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Pop", "$('#myModal').modal('hide');", true);
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Select Medium', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }
    protected void btnsaveprev_Click(object sender, EventArgs e)
    {
        try
        {
            bool chkup = false, chkins = false; string class_ids = "";
            string json = "";
            DataTable dt = new DataTable();
            dt.Columns.Add("min");
            dt.Columns.Add("max");
            dt.Columns.Add("grade");
            dt.Columns.Add("remark");
            dt.Columns.Add("action");

            foreach (ListItem item in lstclass.Items)
            {
                if (item.Selected == true)
                {
                    if (class_ids == "")
                    {
                        class_ids = item.Value;
                    }
                    else
                    {
                        class_ids = class_ids + "," + item.Value;
                    }
                }
            }

            if (ddlmedium.SelectedIndex == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Select the Medium', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
                ddlmedium.Focus();
                return;
            }
            else if (ddlfrmclass.SelectedIndex == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Select the Class', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
                ddlfrmclass.Focus();
                return;
            }
            else if (class_ids == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Select atleast one class to copy', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
                lstclass.Focus();
                return;
            }
            else
            {
                foreach (GridViewRow row in grdprev.Rows)
                {

                    DataRow dr = dt.NewRow();
                    dr[0] = ((Label)row.FindControl("lbllow")).Text;
                    dr[1] = ((Label)row.FindControl("lblhigh")).Text;
                    dr[2] = ((Label)row.FindControl("lblgrade")).Text;
                    dr[3] = ((Label)row.FindControl("lblremark")).Text;
                    dr[4] = "insert";
                    chkins = true;

                    dt.Rows.Add(dr);

                }
                if (dt.Rows.Count > 0)
                {

                    json = DataTableToJSON(dt);
                    string urlalias = cls.urls();
                    string url = @urlalias + "GradeMaster/";

                    gm.type = "copyclsinsert";
                    gm.table = json.ToString();
                    gm.class_id = ddlfrmclass.SelectedValue.ToString();
                    gm.ayid = Session["acdyear"].ToString();
                    gm.copy_class_id = class_ids;
                    gm.medium_id = ddlmedium.SelectedValue.ToString();
                    gm.username = Session["emp_id"].ToString();

                    string jsonString = JsonHelper.JsonSerializer<grd_master>(gm);
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
                        if (result.ToString().Contains("Saved") == true)
                        {
                            frmclass();

                            if (chkup == false && chkins == true)
                            {
                                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Saved Successfully', { color: '#fff', background: '#198104', blur: 0.2, delay: 0 });", true);

                            }
                            else if (chkup == true && chkins == false)
                            {
                                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Updated Successfully', { color: '#fff', background: '#198104', blur: 0.2, delay: 0 });", true);

                            }
                            else if (chkup == true && chkins == true)
                            {
                                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Saved and Updated Successfully', { color: '#fff', background: '#198104', blur: 0.2, delay: 0 });", true);

                            }

                        }
                        else if (result.ToString().Contains("Grade Already Exist") == true)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + result.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('No Changes Made', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('No Grade to Assign', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }
    protected void chkprev_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (chkprev.Checked == true)
            {
                divprev.Attributes.Add("style", "display:block");
                ddlmedium.Enabled = false;
                ddlclass.Enabled = false;
                grd_grade.DataSource = null;
                grd_grade.DataBind();
                btn_Add.Enabled = false;
               // btnprev.Enabled = false;
            }
            else
            {
                //btnprev.Enabled = true;
                btn_Add.Enabled = true;
                divprev.Attributes.Add("style", "display:none");
                ddlmedium.Enabled = true;
                ddlclass.Enabled = true;
                grd_grade.DataSource = null;
                grd_grade.DataBind();
                ddlprevyear.SelectedIndex = 0;

            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }
    protected void ddlprevyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlprevyear.SelectedIndex > 0)
            {
                fillgrid(ddlprevyear.SelectedValue);
            }
            else
            {
                grd_grade.DataSource = null;
                grd_grade.DataBind();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }



    protected void btnedit_Click(object sender, EventArgs e)
    {
        btn_Save.Enabled = false;
        btn_Add.Enabled = false;
        btnprev.Enabled = false;
        LinkButton btn = sender as LinkButton;
        GridViewRow gvrow = btn.NamingContainer as GridViewRow;
        GridViewRow row1 = grd_grade.Rows[gvrow.RowIndex];
        DataTable dt = ViewState["dt"] as DataTable;
        int i = grd_grade.EditIndex;
        string min, max, grd, rem;
        try
        {
            if (i != -1)
            {
                try
                {
                    min = ((TextBox)grd_grade.Rows[i].FindControl("txtmin")).Text;
                    max = ((TextBox)grd_grade.Rows[i].FindControl("txtmax")).Text;
                    grd = ((TextBox)grd_grade.Rows[i].FindControl("txtgrade")).Text;
                    rem = ((TextBox)grd_grade.Rows[i].FindControl("txtremark")).Text;
                }
                catch (Exception ex)
                {
                    min = ((Label)grd_grade.Rows[i].FindControl("lblmin")).Text;
                    max = ((Label)grd_grade.Rows[i].FindControl("lblmax")).Text;
                    grd = ((Label)grd_grade.Rows[i].FindControl("lblgrade")).Text;
                    rem = ((Label)grd_grade.Rows[i].FindControl("lblremark")).Text;
                }
                if (min.ToString() != "" || max.ToString() != "" || grd.ToString() != "" || rem.ToString() != "")
                {
                    ViewState["dt"] = dt;

                    grd_grade.EditIndex = row1.RowIndex;

                }
                else
                {
                    ViewState["dt"] = dt;
                    grd_grade.EditIndex = i;
                    this.BindGrid();
                }
            }
            else
            {
                ViewState["dt"] = dt;
                grd_grade.EditIndex = row1.RowIndex;
                this.BindGrid();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }

    }
    protected void btnclear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }
}