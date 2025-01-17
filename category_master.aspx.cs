using Newtonsoft.Json;
using System;
using System.Collections;
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

public partial class category_master : System.Web.UI.Page
{
    DataTable dt4;
    category cs = new category();
    Class1 cls = new Class1();
    protected void Page_Load(object sender, EventArgs e)
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
                    gridload();
                    hide();
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("Login.aspx");
            }
        }

    }
    public void gridload()
    {
        cs.type1 = "modsubcast";


        string urlalias = cls.urls();
        string url = @urlalias + "category/";
        // string url = "http://localhost:9199/category/";
        string jsonString = JsonHelper.JsonSerializer<category>(cs);
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

            DataTable dt1 = dataSet.Tables[0];
            DataTable dt2 = dataSet.Tables[1];
            DataTable dt3 = dataSet.Tables[2];
            dt4 = dataSet.Tables[3];


            List<string> List = new List<string>();
            List<string> List9 = new List<string>();
            List<string> List8 = new List<string>();


            foreach (DataRow row in dt4.Rows)
            {
                List.Add((string)row["category"]);
                List9.Add((string)row["caste"]);
                List8.Add((string)row["sub_caste"]);
            }

            ddlmedium.DataSource = dt1;
            ddlmedium.DataTextField = "category_name";
            ddlmedium.DataValueField = "category_id";
            ddlmedium.DataBind();
            ddlmedium.Items.Insert(0, "--Select--");
            ddlmedium.SelectedIndex = 0;
            grd2.DataSource = dt2;
            ddl2.DataSource = dt2;
            ddl2.DataTextField = "cast_name";
            ddl2.DataValueField = "cast_id";
            ddl2.DataBind();
            ddl2.Items.Insert(0, "--Select--");
            ddl2.SelectedIndex = 0;
            grd2.DataBind();
            grid1.DataSource = dt1;
            grid1.DataBind();
            gridsubcast.DataSource = dt3;
            gridsubcast.DataBind();
            foreach (GridViewRow row in grid1.Rows)
            {
                Label check11 = row.FindControl("lblgroup") as Label;
                Label check1 = row.FindControl("ll") as Label;
                if (check1.Text == "0" || List.Contains(check11.Text))
                {
                    ((LinkButton)row.Cells[4].FindControl("delet")).Enabled = false;
                    ((LinkButton)row.Cells[4].FindControl("delet")).OnClientClick = "";


                }
            }
            foreach (GridViewRow row in grd2.Rows)
            {
                Label check = row.FindControl("cst_name") as Label;
                Label check91 = row.FindControl("l1") as Label;
                if (check91.Text == "0" || List9.Contains(check.Text))
                {
                    ((LinkButton)row.Cells[4].FindControl("delet")).Enabled = false;
                    ((LinkButton)row.Cells[4].FindControl("delet")).OnClientClick = "";
                }
            }
            foreach (GridViewRow row in gridsubcast.Rows)
            {
                Label check01 = row.FindControl("lblo") as Label;
                if (check01.Text == "0")
                {
                    ((LinkButton)row.Cells[4].FindControl("delet3")).Enabled = false;
                    ((LinkButton)row.Cells[4].FindControl("delet3")).OnClientClick = "";
                }
            }
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
    protected void Unnamed_Click(object sender, EventArgs e)
    {
        if (categoryt.Text == "")
        {
            notifys("Please Enter Category", "#D9534F");
        }
        else
        {
            try
            {
                cs.type1 = "inserting";
                cs.cat = categoryt.Text.ToUpper();

                string urlalias = cls.urls();
                string url = @urlalias + "category/";
                // string url = "http://localhost:9199/category/";
                string jsonString = JsonHelper.JsonSerializer<category>(cs);
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
                    string result1 = result.Replace('"', ' ');
                    notifys(result1, "#28b779");

                } gridload();
                categoryt.Text = "";
            }
            catch (Exception ex)
            {
                Response.Redirect("Login.aspx");
            }
        }

    }

    protected void delet_Click(object sender, EventArgs e)
    {
        string confirmvalue = Request.Form["confirm_value"];
        string[] words = confirmvalue.Split(',');
        Array.Reverse(words);
        try
        {
            if (words[0] == "Yes")
            {
                LinkButton btn = (LinkButton)sender;
                GridViewRow GridView1 = (GridViewRow)btn.NamingContainer;
                //    ///////////////////////////////////////////////////////////////////

                string check = (GridView1.FindControl("lblgroup") as Label).Text;

                cs.type1 = "deleting";
                cs.delet = check;
                string urlalias = cls.urls();
                string url1 = @urlalias + "category/";

                // string url1 = "http://localhost:9199/category/";
                string jsonString1 = JsonHelper.JsonSerializer<category>(cs);
                var httprequest1 = (HttpWebRequest)WebRequest.Create(url1);
                httprequest1.ContentType = "application/json";
                httprequest1.Method = "POST";
                using (var streamWriter = new StreamWriter(httprequest1.GetRequestStream()))
                {
                    streamWriter.Write(jsonString1);
                    streamWriter.Flush();
                    streamWriter.Close();
                }


                var httpresponse1 = (HttpWebResponse)httprequest1.GetResponse();
                using (var streamReader1 = new StreamReader(httpresponse1.GetResponseStream()))
                {
                    string result11 = streamReader1.ReadToEnd();
                    string result1 = result11.Replace('"', ' ');
                    notifys(result1, "#28b779");

                }
                gridload();
            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked NO!')", true);
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }
    protected void btn_Edit_Click(object sender, EventArgs e)
    {
        gridload();
    }
    protected void grid1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grid1.EditIndex = e.NewEditIndex;


        gridload();

    }
    protected void OnUpdate(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;

            string name = (row.FindControl("hi") as TextBox).Text;
            string name1 = (row.FindControl("lbl1") as Label).Text;
            cs.type1 = "updating";
            cs.delet = name1;
            cs.cat = name.ToUpper();

            string urlalias = cls.urls();
            string url = @urlalias + "category/";


            // string url = "http://localhost:9199/category/";
            string jsonString = JsonHelper.JsonSerializer<category>(cs);
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
                string result1 = result.Replace('"', ' ');
                notifys(result1, "#28b779");

            }
            grid1.EditIndex = -1;
            gridload();
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void OnCancel(object sender, EventArgs e)
    {
        grid1.EditIndex = -1;
        gridload();
    }

    protected void delet_Click1(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow GridView1 = (GridViewRow)btn.NamingContainer;
            ///////////////////////////////////////////////////////////////////
            cs.type1 = "checkdata";

            string urlalias = cls.urls();
            string url = @urlalias + "category/";

            // string url = "http://localhost:9199/category/";
            string jsonString = JsonHelper.JsonSerializer<category>(cs);
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
                DataTable dt1 = dataSet.Tables[0];
                List<string> myList = new List<string>();
                foreach (DataRow row in dt1.Rows)
                {
                    myList.Add((string)row[0]);
                }
                string check = (GridView1.FindControl("cst_name") as Label).Text;

                if (myList.Contains(check))
                {
                    notifys("This Category is already asssigned to student", "#28b779");
                }
                else
                {
                    cs.type1 = "deleting2";

                    cs.delet = (GridView1.FindControl("Label1") as Label).Text;

                    string url1 = @urlalias + "category/";

                    // string url1 = "http://localhost:9199/category/";
                    string jsonString1 = JsonHelper.JsonSerializer<category>(cs);
                    var httprequest1 = (HttpWebRequest)WebRequest.Create(url1);
                    httprequest1.ContentType = "application/json";
                    httprequest1.Method = "POST";
                    using (var streamWriter = new StreamWriter(httprequest1.GetRequestStream()))
                    {
                        streamWriter.Write(jsonString1);
                        streamWriter.Flush();
                        streamWriter.Close();
                    }
                    string confirmValue = Request.Form["confirm_value"];
                    string[] words = confirmValue.Split(',');
                    Array.Reverse(words);
                    if (words[0] == "Yes")
                    {
                        var httpresponse1 = (HttpWebResponse)httprequest1.GetResponse();
                        using (var streamReader1 = new StreamReader(httpresponse1.GetResponseStream()))
                        {
                            string result1 = streamReader1.ReadToEnd();
                            string result12 = result1.Replace('"', ' ');
                            notifys(result12, "#28b779");

                        }
                        gridload();

                    }
                    else
                    {
                        Session.Remove(confirmValue);
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked NO!')", true);
                    }

                }
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void delet_Click2(object sender, EventArgs e)
    {
        try
        {
            string confirmValue = Request.Form["confirm_value"];
            string[] words = confirmValue.Split(',');
            Array.Reverse(words);
            if (words[0] == "Yes")
            {
                cs.type1 = "deleting3";
                LinkButton btn = (LinkButton)sender;
                GridViewRow GridView1 = (GridViewRow)btn.NamingContainer;
                cs.delet = (GridView1.FindControl("lblcastid") as Label).Text;

                string urlalias = cls.urls();
                string url = @urlalias + "category/";

                // string url = "http://localhost:9199/category/";
                string jsonString = JsonHelper.JsonSerializer<category>(cs);
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
                    string result1 = result.Replace('"', ' ');
                    notifys(result1, "#28b779");

                }
                gridload();
            }
            else
            {
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }
    protected void grd2_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grd2.EditIndex = e.NewEditIndex;
        gridload();

    }
    protected void Link2_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;

            string name = (row.FindControl("cst_name1") as TextBox).Text;
            string name1 = (row.FindControl("Label1") as Label).Text;
            cs.type1 = "updating1";
            cs.delet = name1;
            cs.cat = name.ToString().ToUpper();

            string urlalias = cls.urls();
            string url = @urlalias + "category/";

            //string url = "http://localhost:9199/category/";
            string jsonString = JsonHelper.JsonSerializer<category>(cs);
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
                string result1 = result.Replace('"', ' ');
                notifys(result1, "#28b779");

            }
            grd2.EditIndex = -1;
            gridload();
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }
    protected void Link3_Click(object sender, EventArgs e)
    {
        grd2.EditIndex = -1;
        gridload();
    }
    protected void gridsubcast_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gridsubcast.EditIndex = e.NewEditIndex;
        gridload();
    }
    protected void L3_Click(object sender, EventArgs e)
    {
        gridsubcast.EditIndex = -1;
        gridload();
    }
    protected void L2_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
            string name = (row.FindControl("TextBox1") as TextBox).Text;
            string name1 = (row.FindControl("lblcastid") as Label).Text;
            cs.type1 = "updating2";
            cs.delet = name1;
            cs.cat = name.ToString().ToUpper();

            string urlalias = cls.urls();
            string url = @urlalias + "category/";

            //  string url = "http://localhost:9199/category/";
            string jsonString = JsonHelper.JsonSerializer<category>(cs);
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
                string result1 = result.Replace('"', ' ');
                notifys(result1, "#28b779");
            }
            gridsubcast.EditIndex = -1;
            gridload();
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }
    protected void btnsubcast_Click(object sender, EventArgs e)
    {
        if (ddl2.SelectedIndex == 0) { notifys("Please Select Proper Cast", "#D9534F"); }
        else if (textb.Text == "")
        {
            notifys("Please Enter Subcast", "#D9534F");
        }

        else
        {
            try
            {
                cs.delet = ddl2.SelectedValue;
                cs.cat = textb.Text.ToUpper();
                cs.type1 = "inserting2";

                string urlalias = cls.urls();
                string url = @urlalias + "category/";

                //  string url = "http://localhost:9199/category/";
                string jsonString = JsonHelper.JsonSerializer<category>(cs);
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
                    string result1 = result.Replace('"', ' ');
                    notifys(result1, "#28b779");

                } gridload();
                textb.Text = "";
            }
            catch (Exception ex) {
                Response.Redirect("Login.aspx");
            }
        }
    }
    protected void castbtn_Click(object sender, EventArgs e)
    {
        if (ddlmedium.SelectedIndex == 0) { notifys("Please Select Proper cast", "#D9534F"); }
        else if (cast.Text == "")
        {
            notifys("Please Enter Cast", "#D9534F");
        }
        else
        {
            try
            {
                cs.delet = ddlmedium.SelectedValue;
                cs.cat = cast.Text.ToUpper();
                cs.type1 = "inserting1";

                string urlalias = cls.urls();
                string url = @urlalias + "category/";

                //  string url = "http://localhost:9199/category/";
                string jsonString = JsonHelper.JsonSerializer<category>(cs);
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
                    string result1 = result.Replace('"', ' ');
                    notifys(result1, "#28b779");

                } gridload();
                cast.Text = "";
            }
            catch (Exception ex) {
                Response.Redirect("Login.aspx");
            }
        }
    }
    public void hide()
    {
        hi1.Visible = false;
        h2.Visible = false;
        ddl2.Visible = false;
        textb.Visible = false;
        btnsubcast.Visible = false;
        addd.Visible = false;
        adddd.Visible = false;
        dd1.Visible = false;
        ddlmedium.Visible = false;
        dd2.Visible = false;
        cast.Visible = false;
        castbtn.Visible = false;
    }
    protected void Unnamed_Click1(object sender, EventArgs e)
    {

        if (btnad.Text != "cancel adding")
        {
            btnad.Text = "cancel adding";
            hi1.Visible = true;
            categoryt.Text = "";
            h2.Visible = true;
        }
        else
        {
            btnad.Text = "ADD";
            hi1.Visible = false;
            h2.Visible = false;
        }

    }
    protected void subhide_Click(object sender, EventArgs e)
    {

        if (subhide.Text != "cancel adding")
        {
            subhide.Text = "cancel adding";
            ddl2.Visible = true;
            textb.Visible = true;
            btnsubcast.Visible = true;
            addd.Visible = true;
            adddd.Visible = true;
            ddl2.SelectedIndex = 0;
            textb.Text = "";
        }
        else
        {
            subhide.Text = "ADD";
            ddl2.Visible = false;
            textb.Visible = false;
            btnsubcast.Visible = false;
            addd.Visible = false;
            adddd.Visible = false;
        }
    }
    protected void dd_Click(object sender, EventArgs e)
    {
        if (dd.Text != "cancel adding")
        {
            dd.Text = "cancel adding";
            ddlmedium.Visible = true;
            dd1.Visible = true;
            dd2.Visible = true;
            cast.Visible = true;
            castbtn.Visible = true;
            cast.Text = "";
            ddlmedium.SelectedIndex = 0;
        }
        else
        {
            dd.Text = "ADD";
            ddlmedium.Visible = false;
            dd1.Visible = false;
            dd2.Visible = false;
            cast.Visible = false;
            castbtn.Visible = false;
        }
    }
}