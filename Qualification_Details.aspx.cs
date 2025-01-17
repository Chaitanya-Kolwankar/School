using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Configuration;
using System.Text.RegularExpressions;
using System.IO;
using System.Drawing;
using Newtonsoft.Json;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;

public partial class Qualification_Details : System.Web.UI.Page
{
    QueryClass qryCls = new QueryClass();
    Class1 c1 = new Class1();
 
    DataSet ds, ds_year, grd_ds, ds_gen;
    string emp_id_new = "", req_no = "", qry = "", phn_no = "", subject = "";
    int flagrid=0;
    Class1 cls = new Class1();
    emp_qualification q = new emp_qualification();
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
                    Grid();
                    LoadDocGrid();
                    changeColor();
                    Hidetabs();
                    for (int i = 2017; i >= 1950; i--)
                    {
                        string s = i.ToString();
                        ddl_ssc_year.Items.Add(s);
                        ddl_hsc_year.Items.Add(s);
                        ddl_degree_year.Items.Add(s);
                        ddl_pg_year.Items.Add(s);
                        ddl_mphill_year.Items.Add(s);
                        ddl_phd_year.Items.Add(s);
                        ddl_others_year.Items.Add(s);
                        ddl_diploma_year.Items.Add(s);
                        ddl_net_year.Items.Add(s);
                        ddl_set_year.Items.Add(s);
                    }


                }
            }
            catch (Exception ex)
            {
                Response.Redirect("Login.aspx");
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

    void Hidetabs()
    {
        tabssc.Visible = false;
        tabhsc.Visible = false;
        tabdegree.Visible = false;
        tabpg.Visible = false;
        tabmphil.Visible = false;
        tabphd.Visible = false;
        tabnet.Visible = false;
        tabset.Visible = false;
        tabothers.Visible = false;
        tabdiploma.Visible = false;
    }

    protected void btn_hsc_Click(object sender, EventArgs e)
    {
        tabdiploma.Visible = false;
        tabssc.Visible = false;
        tabhsc.Visible = true;
        tabdegree.Visible = false;
        tabpg.Visible = false;
        tabmphil.Visible = false;
        tabphd.Visible = false;
        tabnet.Visible = false;
        tabset.Visible = false;
        tabothers.Visible = false;
        grd_ssc.Visible = false;
        grd_doc.Visible = false;
    }

    protected void btn_ssc_Click(object sender, EventArgs e)
    {
        tabdiploma.Visible = false;
        tabssc.Visible = true;
        tabhsc.Visible = false;
        tabdegree.Visible = false;
        tabpg.Visible = false;
        tabmphil.Visible = false;
        tabphd.Visible = false;
        tabnet.Visible = false;
        tabset.Visible = false;
        tabothers.Visible = false;
        grd_ssc.Visible = true;
        grd_doc.Visible = true;
    }

    protected void btn_degree_Click(object sender, EventArgs e)
    {
        tabdiploma.Visible = false;
        tabssc.Visible = false;
        tabhsc.Visible = false;
        tabdegree.Visible = true;
        tabpg.Visible = false;
        tabmphil.Visible = false;
        tabphd.Visible = false;
        tabnet.Visible = false;
        tabset.Visible = false;
        tabothers.Visible = false;
        grd_ssc.Visible = false;
        grd_doc.Visible = false;
    }

    protected void btn_pg_Click(object sender, EventArgs e)
    {
        tabdiploma.Visible = false;
        tabssc.Visible = false;
        tabhsc.Visible = false;
        tabdegree.Visible = false;
        tabpg.Visible = true;
        tabmphil.Visible = false;
        tabphd.Visible = false;
        tabnet.Visible = false;
        tabset.Visible = false;
        tabothers.Visible = false;
        grd_ssc.Visible = false;
        grd_doc.Visible = false;
    }

    protected void btn_mphill_Click(object sender, EventArgs e)
    {
        tabdiploma.Visible = false;
        tabssc.Visible = false;
        tabhsc.Visible = false;
        tabdegree.Visible = false;
        tabpg.Visible = false;
        tabmphil.Visible = true;
        tabphd.Visible = false;
        tabnet.Visible = false;
        tabset.Visible = false;
        tabothers.Visible = false;
        grd_ssc.Visible = false;
        grd_doc.Visible = false;
    }

    protected void btn_phd_Click(object sender, EventArgs e)
    {
        tabdiploma.Visible = false;
        tabssc.Visible = false;
        tabhsc.Visible = false;
        tabdegree.Visible = false;
        tabpg.Visible = false;
        tabmphil.Visible = false;
        tabphd.Visible = true;
        tabnet.Visible = false;
        tabset.Visible = false;
        tabothers.Visible = false;
        grd_ssc.Visible = false;
        grd_doc.Visible = false;
    }

    protected void btn_net_Click(object sender, EventArgs e)
    {
        tabdiploma.Visible = false;
        tabssc.Visible = false;
        tabhsc.Visible = false;
        tabdegree.Visible = false;
        tabpg.Visible = false;
        tabmphil.Visible = false;
        tabphd.Visible = false;
        tabnet.Visible = true;
        tabset.Visible = false;
        tabothers.Visible = false;
        grd_ssc.Visible = false;
        grd_doc.Visible = false;
    }

    protected void btn_set_Click(object sender, EventArgs e)
    {
        tabdiploma.Visible = false;
        tabssc.Visible = false;
        tabhsc.Visible = false;
        tabdegree.Visible = false;
        tabpg.Visible = false;
        tabmphil.Visible = false;
        tabphd.Visible = false;
        tabnet.Visible = false;
        tabset.Visible = true;
        tabothers.Visible = false;
        grd_ssc.Visible = false;
        grd_doc.Visible = false;
    }

    protected void btn_others_Click(object sender, EventArgs e)
    {
        tabdiploma.Visible = false;
        tabssc.Visible = false;
        tabhsc.Visible = false;
        tabdegree.Visible = false;
        tabpg.Visible = false;
        tabmphil.Visible = false;
        tabphd.Visible = false;
        tabnet.Visible = false;
        tabset.Visible = false;
        tabothers.Visible = true;
        grd_ssc.Visible = false;
        grd_doc.Visible = false;
    }

    protected void btn_diploma_Click(object sender, EventArgs e)
    {
        tabdiploma.Visible = true;
        tabssc.Visible = false;
        tabhsc.Visible = false;
        tabdegree.Visible = false;
        tabpg.Visible = false;
        tabmphil.Visible = false;
        tabphd.Visible = false;
        tabnet.Visible = false;
        tabset.Visible = false;
        tabothers.Visible = false;
        grd_ssc.Visible = false;
        grd_doc.Visible = false;
    }

    public void changeColor()
    {
        btn_ssc.Attributes.Remove("BackColor");
        btn_hsc.Attributes.Remove("BackColor");
        btn_diploma.Attributes.Remove("BackColor");
        btn_degree.Attributes.Remove("BackColor");
        btn_phd.Attributes.Remove("BackColor");

        btn_net.Attributes.Remove("BackColor");
        btn_set.Attributes.Remove("BackColor");
        btn_pg.Attributes.Remove("BackColor");
        btn_others.Attributes.Remove("BackColor");



        btn_ssc.CssClass = "btn btn-block btn-success background:#26B99A";
        btn_ssc.BackColor = Color.FromArgb(38, 185, 154);

        btn_hsc.CssClass = "btn btn-block btn-success background:#26B99A";
        btn_hsc.BackColor = Color.FromArgb(38, 185, 154);

        btn_diploma.CssClass = "btn btn-block btn-success background:#26B99A";
        btn_diploma.BackColor = Color.FromArgb(38, 185, 154);

        btn_degree.CssClass = "btn btn-block btn-success background:#26B99A";
        btn_degree.BackColor = Color.FromArgb(38, 185, 154);

        btn_phd.CssClass = "btn btn-block btn-success background:#26B99A";
        btn_phd.BackColor = Color.FromArgb(38, 185, 154);

        btn_mphill.CssClass = "btn btn-block btn-success background:#26B99A";
        btn_mphill.BackColor = Color.FromArgb(38, 185, 154);

        btn_net.CssClass = "btn btn-block btn-success background:#26B99A";
        btn_net.BackColor = Color.FromArgb(38, 185, 154);

        btn_set.CssClass = "btn btn-block btn-success background:#26B99A";
        btn_set.BackColor = Color.FromArgb(38, 185, 154);

        btn_pg.CssClass = "btn btn-block btn-success background:#26B99A";
        btn_pg.BackColor = Color.FromArgb(38, 185, 154);

        btn_others.CssClass = "btn btn-block btn-success background:#26B99A";
        btn_others.BackColor = Color.FromArgb(38, 185, 154);


        for (int i = 0; i <= grd_ssc.Rows.Count - 1;i++ )
        {
            if (grd_ssc.Rows[i].Cells[1].Text == "S.S.C")
            {
                btn_ssc.BackColor = System.Drawing.ColorTranslator.FromHtml("#D44950");
            }
            else if (grd_ssc.Rows[i].Cells[1].Text == "H.S.C")
            {
                btn_hsc.BackColor = System.Drawing.ColorTranslator.FromHtml("#D44950");
            }
            else if (grd_ssc.Rows[i].Cells[1].Text == "Diploma")
            {
                btn_diploma.BackColor = System.Drawing.ColorTranslator.FromHtml("#D44950");
            }
            else if (grd_ssc.Rows[i].Cells[1].Text == "Degree")
            {
                btn_degree.BackColor = System.Drawing.ColorTranslator.FromHtml("#D44950");
            }
            else if (grd_ssc.Rows[i].Cells[1].Text == "PG")
            {
                btn_pg.BackColor = System.Drawing.ColorTranslator.FromHtml("#D44950");
            }
            else if (grd_ssc.Rows[i].Cells[1].Text == "M-Phill")
            {
                btn_mphill.BackColor = System.Drawing.ColorTranslator.FromHtml("#D44950");
            }
            else if (grd_ssc.Rows[i].Cells[1].Text == "Ph.D")
            {
                btn_phd.BackColor = System.Drawing.ColorTranslator.FromHtml("#D44950");
            }
            else if (grd_ssc.Rows[i].Cells[1].Text == "NET")
            {
                btn_net.BackColor = System.Drawing.ColorTranslator.FromHtml("#D44950");
            }
            else if (grd_ssc.Rows[i].Cells[1].Text == "SET")
            {
                btn_set.BackColor = System.Drawing.ColorTranslator.FromHtml("#D44950");
            }
            else if (grd_ssc.Rows[i].Cells[1].Text == "Course / Certification")
            {
                btn_others.BackColor = System.Drawing.ColorTranslator.FromHtml("#D44950");
            }
            else if (grd_ssc.Rows[i].Cells[1].Text == "Others")
            {
                btn_others.BackColor = System.Drawing.ColorTranslator.FromHtml("#D44950");
            }
        }
    }

    public string aapostrophe(string s)
    {

        string replacestr = s.Replace("'", "''");
        return replacestr;
    }

    public bool savedoc(FileUpload f, string exam)
    {

        if (exam.Contains("."))
        {
           exam= exam.Replace('.', '_');
        }
        if (f.HasFiles)
        {
            if (Directory.Exists(Server.MapPath("~/StaffDocuments/") + Session["emp_id"].ToString()))
            {
                if (Directory.GetFiles(Server.MapPath("~/StaffDocuments/" + Session["emp_id"].ToString()), Session["emp_id"].ToString() + "_" + exam + "*").Length > 0)
                {
                    string[] ff = Directory.GetFiles(Server.MapPath("~/StaffDocuments/" + Session["emp_id"].ToString()), Session["emp_id"].ToString() + "_" + exam + "*");

                    foreach (string s in ff)
                    {
                        File.Delete(s);
                    }
                    int cnt = 0;
                    foreach (HttpPostedFile uploadedFile in f.PostedFiles)
                    {
                        FileInfo fi = new FileInfo(uploadedFile.FileName);
                        string ext = fi.Extension;
                        cnt++;
                        uploadedFile.SaveAs(Server.MapPath("~/StaffDocuments/" + Session["emp_id"].ToString() + "/") + Session["emp_id"].ToString() + "_" + exam + "_" + cnt + ext);


                    }
                    return true;

                    //   Response.Write("<script>alert('" + exam + " Documents Already Exists.Please Delete and Reupload the Same')</script>");

                }
                else
                {

                    int cnt = 0;
                    foreach (HttpPostedFile uploadedFile in f.PostedFiles)
                    {
                        FileInfo fi = new FileInfo(uploadedFile.FileName);
                        string ext = fi.Extension;
                        cnt++;
                        uploadedFile.SaveAs(Server.MapPath("~/StaffDocuments/" + Session["emp_id"].ToString() + "/") + Session["emp_id"].ToString() + "_" + exam + "_" + cnt + ext);


                    }
                    return true;
                }
            }

            else
            {

                Directory.CreateDirectory(Server.MapPath("~/StaffDocuments/") + Session["emp_id"].ToString());
                int cnt = 0;
                foreach (HttpPostedFile uploadedFile in f.PostedFiles)
                {
                    FileInfo fi = new FileInfo(uploadedFile.FileName);
                    string ext = fi.Extension;
                    cnt++;
                    uploadedFile.SaveAs(Server.MapPath("~/StaffDocuments/" + Session["emp_id"].ToString() + "/") + Session["emp_id"].ToString() + "_" + exam + "_" + cnt + ext);


                }
                return true;
            }

        }
        else
        {
            return false;
        }
    }

    public bool deldoc( string exam)
    {
        if (exam.Contains("."))
        {
            exam = exam.Replace('.', '_');
        }
       
            if (Directory.Exists(Server.MapPath("~/StaffDocuments/") + Session["emp_id"].ToString()))
            {
               
                    string[] ff = Directory.GetFiles(Server.MapPath("~/StaffDocuments/" + Session["emp_id"].ToString()), Session["emp_id"].ToString() + "_" + exam + "*");

                    foreach (string s in ff)
                    {
                        File.Delete(s);
                    }

                    return true;

                 
            }

            return true;
           
        
    }

    public void ClearAll()
    {

        //ssc
        ddl_ssc_state.SelectedIndex = 0;
        ddl_ssc_board.SelectedIndex = 0;
        txt_ssc_insti_name.Text = "";
        txt_ssc_insti_place.Text = "";
        ddl_ssc_year.SelectedIndex = 0;
        ddl_ssc_month.SelectedIndex = 0;
        txt_ssc_mks_obtnd.Text = "";
        txt_ssc_nout_mks.Text = "";
        txt_ssc_grd.Text = "";
        txt_ssc_seat_no.Text = "";

        //hsc
        ddl_hsc_state.SelectedIndex = 0;
        ddl_hsc_board.SelectedIndex = 0;
        txt_hsc_insti_name.Text = "";
        txt_hsc_insti_place.Text = "";
        ddl_hsc_year.SelectedIndex = 0;
        ddl_hsc_month.SelectedIndex = 0;
        txt_hsc_mks_obt.Text = "";
        txt_hsc_total_mks.Text = "";
        txt_hsc_grade.Text = "";
        txt_hsc_seat.Text = "";

        //diploma
        ddl_diploma_state.SelectedIndex = 0;
        ddl_diploma_board.SelectedIndex = 0;
        txt_diploma_insti_name.Text = "";
        txt_diploma_insti_place.Text = "";
        ddl_diploma_year.SelectedIndex = 0;
        ddl_diploma_month.SelectedIndex = 0;
        txt_diploma_mks_obt.Text = "";
        txt_diploma_total_mks.Text = "";
        txt_diploma_grade.Text = "";
        txt_diploma_seat_no.Text = "";

        //degree
        ddl_degree_state.SelectedIndex = 0;
        ddl_degree_board.SelectedIndex = 0;
        txt_degree_insti_name.Text = "";
        txt_degree_insti_place.Text = "";
        ddl_degree_year.SelectedIndex = 0;
        ddl_degree_month.SelectedIndex = 0;
        txt_degree_mks_obtnd.Text = "";
        txt_degree_total_mks.Text = "";
        txt_degree_grade.Text = "";
        txt_degree_seat_no.Text = "";
        txt_degree_specilize_subject.Text = "";

        //PG
        ddl_pg_state.SelectedIndex = 0;
        ddl_pg_board.SelectedIndex = 0;
        txt_pg_insti_name.Text = "";
        txt_pg_insti_place.Text = "";
        ddl_pg_year.SelectedIndex = 0;
        ddl_pg_month.SelectedIndex = 0;
        txt_pg_mks_obt.Text = "";
        txt_pg_total_mks.Text = "";
        txt_pg_grade.Text = "";
        txt_pg_seat_no.Text = "";
        txt_pg_specilize_sub.Text = "";

        //mphil
        ddl_mphill_state.SelectedIndex = 0;
        ddl_mphill_board.SelectedIndex = 0;
        txt_mphill_insti_name.Text = "";
        txt_mphill_insti_place.Text = "";
        ddl_mphill_year.SelectedIndex = 0;
        ddl_mphill_month.SelectedIndex = 0;
        txt_mphill_mks_obt.Text = "";
        txt_mphill_total_mks.Text = "";
        txt_mphill_grade.Text = "";
        txt_mphill_seat_no.Text = "";
        txt_mphill_specilize_sub.Text = "";
        //ph.d
        ddl_phd_state.SelectedIndex = 0;
        ddl_phd_board.SelectedIndex = 0;
        txt_phd_insti_name.Text = "";
        txt_phd_insti_place.Text = "";
        ddl_phd_year.SelectedIndex = 0;
        ddl_phd_month.SelectedIndex = 0;
        txt_phd_mks_obt.Text = "";
        txt_Phd_total_mks.Text = "";
        txt_phd_grade.Text = "";
        txt_phd_seat_no.Text = "";
        txt_phd_specilize_Sub.Text = "";

        //net
        ddl_net.SelectedIndex = 0;
        ddl_net_year.SelectedIndex = 0;

        ddl_net_month.SelectedIndex = 0;

        //set
        ddl_set.SelectedIndex = 0;
        ddl_set_year.SelectedIndex = 0;

        ddl_set_month.SelectedIndex = 0;


        //others

        ddl_others_state.SelectedIndex = 0;
        ddl_others_board.SelectedIndex = 0;

        ddl_others_year.SelectedIndex = 0;
        ddl_others_month.SelectedIndex = 0;
        txt_others_mks_obt.Text = "";
        txt_others_total_mks.Text = "";
        txt_others_grade.Text = "";
        txt_others_seat_no.Text = "";
        txt_others_specilize_sub.Text = "";
    }

    //------------------------------------SSC---------------------------------------------------

    protected void ddl_ssc_state_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet state_board_name = c1.fill_dataset("select  child from dbo.State_category_details where Main = 'State' and parent = '" + ddl_ssc_state.Text.ToString().Trim() + "'");
            ddl_ssc_board.Items.Clear();
            ddl_ssc_board.DataSource = state_board_name.Tables[0];

            ddl_ssc_board.DataTextField = "child";
            ddl_ssc_board.DataBind();
            ddl_ssc_board.Items.Insert(0, new ListItem("-- Select --", "0"));
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void btn_ssc_submit_Click(object sender, EventArgs e)
    {
        try
        {
            if (validate_ssc() == true && ssc_file_uplaod.HasFiles == true)
            {
                savedoc(ssc_file_uplaod, "S.S.C");

                DataSet ds = ((DataSet)Session["emp_data"]);
                string qry1 = "";
                string qry = "";
                qry1 = "delete from emp_education_details where emp_id='" + Session["emp_id"] + "' and emp_degree_name='S.S.C'";
                int l = c1.DMLquerries_1(qry1);
                // string str1 = tabssc.InnerText;
                string inst_name = txt_ssc_insti_name.Text;
                inst_name = inst_name.Replace("'", "''");

                string urlalias = cls.urls();
                string url = @urlalias + "EmpQualification/";

                //string url = "http://localhost:9199/EmpQualification/";

                q.emp_id = Session["emp_id"].ToString();
                q.emp_coll_name = aapostrophe(txt_ssc_insti_name.Text);
                q.emp_uni_board_name = ddl_ssc_board.SelectedItem.Text;
                q.emp_deg_name = "S.S.C";
                q.emp_deg_type = "ssc";
                q.emp_spec_subject = null;
                q.emp_month_of_passing = ddl_ssc_month.SelectedItem.Text;
                q.emp_year_of_passing = ddl_ssc_year.SelectedItem.Text;
                q.emp_mrk_obt = aapostrophe(txt_ssc_mks_obtnd.Text);
                q.emp_tot_mrk = aapostrophe(txt_ssc_nout_mks.Text);
                q.emp_class_sec = aapostrophe(txt_ssc_grd.Text);
                q.emp_pursuing = null;
                q.emp_exm_order = null;
                q.emp_exm_netset_remrk = null;
                q.emp_coll_state = ddl_ssc_state.SelectedItem.Text;
                q.emp_coll_place = aapostrophe(txt_ssc_insti_place.Text);
                q.emp_seat_no = aapostrophe(txt_ssc_seat_no.Text);
                q.type = "insert";

                string jsonString = JsonHelper.JsonSerializer<emp_qualification>(q);
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
                    if (result.ToString().Contains("Insert") == true)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Saved Successfully', { color: '#fff', background: '#198104', blur: 0.2, delay: 0 });", true);
                        btn_ssc.BackColor = System.Drawing.ColorTranslator.FromHtml("#D44950");

                        ClearAll();
                        Hidetabs();
                        grd_ssc.Visible = true;
                        LoadDocGrid();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Connection Error', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);

                    }
                }



                Grid();
                changeColor();


            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Please fill all the details', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);

            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    public void Grid()
    {
        try
        {
            string urlalias = cls.urls();
            string url = @urlalias + "EmpQualification/";

            //string url = "http://localhost:9199/EmpQualification/";

            q.emp_id = Session["emp_id"].ToString();
            q.type = "bindgrid_ssc";

            string jsonString = JsonHelper.JsonSerializer<emp_qualification>(q);
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
                    grd_ssc.DataSource = dataSet.Tables[0];
                    grd_ssc.DataBind();
                    if (Convert.ToString(Session["profile_complete"]) == "YES")
                    {
                        foreach (GridViewRow gr in grd_ssc.Rows)
                        {
                            gr.Cells[14].Enabled = false;
                        }
                    }
                    else
                    {
                        foreach (GridViewRow gr in grd_ssc.Rows)
                        {
                            gr.Cells[14].Enabled = true;
                        }
                    }
                }
                else
                {
                    grd_ssc.DataSource = null;
                    grd_ssc.DataBind();
                }

            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
        
    }

    protected void grd_ssc_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grd_ssc.EditIndex = -1;
        Grid();
    }

    public bool validate_ssc()
    {

        if (ddl_ssc_state.SelectedIndex <= 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Select State');", true);
            return false;
        }
       
        if (txt_ssc_insti_name.Text.Trim().Equals("") || txt_ssc_insti_name.Text.Trim().Length == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Enter Institute Name');", true);
            return false;
        }
        if (txt_ssc_insti_place.Text.Trim().Equals("") || txt_ssc_insti_place.Text.Trim().Length == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Enter Institute Place');", true);
            return false;
        }
        if (ddl_ssc_year.SelectedIndex <= 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Select passing year');", true);
            return false;
        }
        if (ddl_ssc_month.SelectedIndex <= 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Select passing month');", true);
            return false;
        }
        if (txt_ssc_mks_obtnd.Text.Trim().Equals("") || txt_ssc_mks_obtnd.Text.Trim().Length == 0)
        {
            if (txt_ssc_grd.Text.Trim().Equals("") || txt_ssc_grd.Text.Trim().Length == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Enter Grade');", true);
                return false;
            }
        }
        else
        {
            if (txt_ssc_nout_mks.Text.Trim().Equals("") || txt_ssc_nout_mks.Text.Trim().Length == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Enter Out Of Marks');", true);
                return false;
            }
        }
        return true;

    }

  //-------------------------------------HSC---------------------------------------------------------
    
    protected void ddl_hsc_state_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet state_board_name = c1.fill_dataset("select  child from dbo.State_category_details where Main = 'State' and parent = '" + ddl_hsc_state.Text.ToString().Trim() + "'");
            ddl_hsc_board.Items.Clear();
            ddl_hsc_board.DataSource = state_board_name.Tables[0];

            ddl_hsc_board.DataTextField = "child";
            ddl_hsc_board.DataBind();
            ddl_hsc_board.Items.Insert(0, new ListItem("-- Select --", "0"));
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void btn_hsc_submit_Click(object sender, EventArgs e)
    {
        try
        {
            if (validate_hsc() == true && hsc_file_upload.HasFiles == true)
            {
                savedoc(hsc_file_upload, "H.S.C");

                //err.Visible = false;

                DataSet ds = ((DataSet)Session["emp_data"]);
                string qry2 = "";
                string qry = "";
                qry2 = "delete from emp_education_details where emp_id='" + Session["emp_id"] + "' and emp_degree_name='H.S.C'";
                int l = c1.DMLquerries_1(qry2);

                string urlalias = cls.urls();
                string url = @urlalias + "EmpQualification/";

             //   string url = "http://localhost:9199/EmpQualification/";

                q.emp_id = Session["emp_id"].ToString();
                q.emp_coll_name = aapostrophe(txt_hsc_insti_name.Text);
                q.emp_uni_board_name = ddl_hsc_board.SelectedItem.Text;
                q.emp_deg_name = "H.S.C";
                q.emp_deg_type = "HSC";
                q.emp_spec_subject = null;
                q.emp_month_of_passing = ddl_hsc_month.SelectedItem.Text;
                q.emp_year_of_passing = ddl_hsc_year.SelectedItem.Text;
                q.emp_mrk_obt = aapostrophe(txt_hsc_mks_obt.Text);
                q.emp_tot_mrk = aapostrophe(txt_hsc_total_mks.Text);
                q.emp_class_sec = aapostrophe(txt_hsc_grade.Text);
                q.emp_pursuing = null;
                q.emp_exm_order = null;
                q.emp_exm_netset_remrk = null;
                q.emp_coll_state = ddl_hsc_state.SelectedItem.Text;
                q.emp_coll_place = aapostrophe(txt_hsc_insti_name.Text);
                q.emp_seat_no = aapostrophe(txt_hsc_seat.Text);
                q.type = "insert";


                string jsonString = JsonHelper.JsonSerializer<emp_qualification>(q);
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
                    if (result.ToString().Contains("Insert") == true)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Saved Successfully', { color: '#fff', background: '#198104', blur: 0.2, delay: 0 });", true);
                        btn_hsc.BackColor = System.Drawing.ColorTranslator.FromHtml("#D44950");

                        ClearAll();
                        Hidetabs();
                        grd_ssc.Visible = true;
                        LoadDocGrid();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Connection Error', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);

                    }
                }


                Grid();
                changeColor();
                LoadDocGrid();

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please fill all the details');", true);
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void grd_hsc_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grd_hsc.EditIndex = -1;
        
    }

    public bool validate_hsc()
    {

        if (ddl_hsc_state.SelectedIndex <= 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Select State');", true);
            return false;
        }

        if (txt_hsc_insti_name.Text.Trim().Equals("") || txt_hsc_insti_name.Text.Trim().Length == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Enter Institute Name');", true);
            return false;
        }
        if (txt_hsc_insti_place.Text.Trim().Equals("") || txt_hsc_insti_place.Text.Trim().Length == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Enter Institute Place');", true);
            return false;
        }
        if (ddl_hsc_year.SelectedIndex <= 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Select passing year');", true);
            return false;
        }
        if (ddl_hsc_month.SelectedIndex <= 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Select passing month');", true);
            return false;
        }
        if (txt_hsc_mks_obt.Text.Trim().Equals("") || txt_hsc_mks_obt.Text.Trim().Length == 0)
        {
            if (txt_hsc_grade.Text.Trim().Equals("") || txt_hsc_grade.Text.Trim().Length == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Enter Grade');", true);
                return false;
            }
        }
        else
        {
            if (txt_hsc_total_mks.Text.Trim().Equals("") || txt_hsc_total_mks.Text.Trim().Length == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Enter Out Of Marks');", true);
                return false;
            }
        }
        return true;

    }
    //------------------------------------diploma---------------------------------------------------

    protected void ddl_diploma_state_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet state_board_name = c1.fill_dataset("select  child from dbo.State_category_details where Main = 'State' and parent = '" + ddl_diploma_state.Text.ToString().Trim() + "'");
            ddl_diploma_board.Items.Clear();
            ddl_diploma_board.DataSource = state_board_name.Tables[0];

            ddl_diploma_board.DataTextField = "child";
            ddl_diploma_board.DataBind();
            ddl_diploma_board.Items.Insert(0, new ListItem("-- Select --", "0"));
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void btn_diploma_submit_Click(object sender, EventArgs e)
    {
        try
        {
            if (validate_diploma() == true && diploma_file_upload.HasFiles == true)
            {
                savedoc(diploma_file_upload, "diploma");

                //err.Visible = false;

                DataSet ds = ((DataSet)Session["emp_data"]);
                string qry6 = "";
                string qry = "";
                qry6 = "delete from emp_education_details where emp_id='" + Session["emp_id"] + "' and emp_degree_name='Course / Certification'";
                int l = c1.DMLquerries_1(qry6);

                string urlalias = cls.urls();
                string url = @urlalias + "EmpQualification/";

                //string url = "http://localhost:9199/EmpQualification/";

                q.emp_id = Session["emp_id"].ToString();
                q.emp_coll_name = aapostrophe(txt_diploma_insti_name.Text);
                q.emp_uni_board_name = ddl_diploma_board.SelectedItem.Text;
                q.emp_deg_name = "Diploma";
                q.emp_deg_type = "Diploma";
                q.emp_spec_subject = null;
                q.emp_month_of_passing = ddl_diploma_month.SelectedItem.Text;
                q.emp_year_of_passing = ddl_diploma_year.SelectedItem.Text;
                q.emp_mrk_obt = aapostrophe(txt_diploma_mks_obt.Text);
                q.emp_tot_mrk = aapostrophe(txt_diploma_total_mks.Text);
                q.emp_class_sec = aapostrophe(txt_diploma_grade.Text);
                q.emp_pursuing = null;
                q.emp_exm_order = null;
                q.emp_exm_netset_remrk = null;
                q.emp_coll_state = ddl_diploma_state.SelectedItem.Text;
                q.emp_coll_place = aapostrophe(txt_diploma_insti_name.Text);
                q.emp_seat_no = aapostrophe(txt_diploma_seat_no.Text);
                q.type = "insert";


                string jsonString = JsonHelper.JsonSerializer<emp_qualification>(q);
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
                    if (result.ToString().Contains("Insert") == true)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Saved Successfully', { color: '#fff', background: '#198104', blur: 0.2, delay: 0 });", true);
                        btn_diploma.BackColor = System.Drawing.ColorTranslator.FromHtml("#D44950");

                        ClearAll();
                        Hidetabs();
                        grd_ssc.Visible = true;

                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Connection Error', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);

                    }
                }

                Grid();
                changeColor();
                LoadDocGrid();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please fill all the details');", true);
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    public bool validate_diploma()
    {

        if (ddl_diploma_state.SelectedIndex <= 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Select State');", true);
            return false;
        }

        if (txt_diploma_insti_name.Text.Trim().Equals("") || txt_diploma_insti_name.Text.Trim().Length == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Enter Institute Name');", true);
            return false;
        }
        if (txt_diploma_insti_place.Text.Trim().Equals("") || txt_diploma_insti_place.Text.Trim().Length == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Enter Institute Place');", true);
            return false;
        }

        if (ddl_diploma_year.SelectedIndex <= 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Select passing year');", true);
            return false;
        }
        if (ddl_diploma_month.SelectedIndex <= 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Select passing month');", true);
            return false;
        }
        if (txt_diploma_mks_obt.Text.Trim().Equals("") || txt_diploma_mks_obt.Text.Trim().Length == 0)
        {
            if (txt_diploma_grade.Text.Trim().Equals("") || txt_diploma_grade.Text.Trim().Length == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Enter Grade');", true);
                return false;
            }
        }
        else
        {
            if (txt_diploma_total_mks.Text.Trim().Equals("") || txt_diploma_total_mks.Text.Trim().Length == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Enter Out Of Marks');", true);
                return false;
            }
        }
        return true;

    }
    //------------------------------------Degree------------------------------------------------------
    protected void ddl_degree_state_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet state_board_name = c1.fill_dataset("select  child from dbo.State_category_details where Main = 'State' and parent = '" + ddl_degree_state.Text.ToString().Trim() + "'");
            ddl_degree_board.Items.Clear();
            ddl_degree_board.DataSource = state_board_name.Tables[0];

            ddl_degree_board.DataTextField = "child";
            ddl_degree_board.DataBind();
            ddl_degree_board.Items.Insert(0, new ListItem("-- Select --", "0"));
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void grd_degree_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grd_degree.EditIndex = -1;
    }

    protected void btn_degree_submit_Click(object sender, EventArgs e)
    {
        try
        {
            if (validate_degree() == true && degree_file_upload.HasFiles == true)
            {
                DataSet ds = ((DataSet)Session["emp_data"]);
                string qry3 = "";
                string qry = "";

                if (flagrid == 0)
                {
                    Session["subject"] = "";

                }

                if (Session["subject"].ToString() != string.Empty)
                {

                    savedoc(degree_file_upload, "Degree_" + Session["subject"].ToString());
                    qry3 = "delete from emp_education_details where emp_id='" + Session["emp_id"] + "' and emp_degree_name='Degree' and emp_specialization_subj='" + Session["subject"].ToString() + "'";

                }
                else
                {
                    savedoc(degree_file_upload, "Degree_" + txt_degree_specilize_subject.Text);
                    qry3 = "delete from emp_education_details where emp_id='" + Session["emp_id"] + "' and emp_degree_name='Degree' and emp_specialization_subj='" + txt_degree_specilize_subject.Text + "'";

                }
                int l = c1.DMLquerries_1(qry3);

                string urlalias = cls.urls();
                string url = @urlalias + "EmpQualification/";

                //string url = "http://localhost:9199/EmpQualification/";

                q.emp_id = Session["emp_id"].ToString();
                q.emp_coll_name = aapostrophe(txt_degree_insti_name.Text);
                q.emp_uni_board_name = ddl_degree_board.SelectedItem.Text;
                q.emp_deg_name = "Degree";
                q.emp_deg_type = "Degree";
                q.emp_spec_subject = aapostrophe(txt_degree_specilize_subject.Text);
                q.emp_month_of_passing = ddl_degree_month.SelectedItem.Text;
                q.emp_year_of_passing = ddl_degree_year.SelectedItem.Text;
                q.emp_mrk_obt = aapostrophe(txt_degree_mks_obtnd.Text);
                q.emp_tot_mrk = aapostrophe(txt_degree_total_mks.Text);
                q.emp_class_sec = aapostrophe(txt_degree_grade.Text);
                q.emp_pursuing = null;
                q.emp_exm_order = null;
                q.emp_exm_netset_remrk = null;
                q.emp_coll_state = ddl_degree_state.SelectedItem.Text;
                q.emp_coll_place = aapostrophe(txt_degree_insti_place.Text);
                q.emp_seat_no = aapostrophe(txt_degree_seat_no.Text);
                q.type = "insert";



                string jsonString = JsonHelper.JsonSerializer<emp_qualification>(q);
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
                    if (result.ToString().Contains("Insert") == true)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Saved Successfully', { color: '#fff', background: '#198104', blur: 0.2, delay: 0 });", true);
                        btn_degree.BackColor = System.Drawing.ColorTranslator.FromHtml("#D44950");

                        ClearAll();
                        Hidetabs();
                        grd_ssc.Visible = true;

                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Connection Error', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);

                    }
                }




                Grid();
                changeColor();
                LoadDocGrid();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please fill all the details');", true);
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    public bool validate_degree()
    {

        if (ddl_degree_state.SelectedIndex <= 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Select State');", true);
            return false;
        }

        if (txt_degree_insti_name.Text.Trim().Equals("") || txt_degree_insti_name.Text.Trim().Length == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Enter Institute Name');", true);
            return false;
        }
        if (txt_degree_insti_place.Text.Trim().Equals("") || txt_degree_insti_place.Text.Trim().Length == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Enter Institute Place');", true);
            return false;
        }
        if (ddl_degree_year.SelectedIndex <= 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Select passing year');", true);
            return false;
        }
        if (ddl_degree_month.SelectedIndex <= 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Select passing month');", true);
            return false;
        }
        if (txt_degree_mks_obtnd.Text.Trim().Equals("") || txt_degree_mks_obtnd.Text.Trim().Length == 0)
        {
            if (txt_degree_grade.Text.Trim().Equals("") || txt_degree_grade.Text.Trim().Length == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Enter Grade');", true);
                return false;
            }
        }
        else
        {
            if (txt_degree_total_mks.Text.Trim().Equals("") || txt_degree_total_mks.Text.Trim().Length == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Enter Out Of Marks');", true);
                return false;
            }
        }
        return true;

    }
    //-----------------------PG----------------------------------------------------------
 
    protected void ddl_pg_state_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet state_board_name = c1.fill_dataset("select  child from dbo.State_category_details where Main = 'State' and parent = '" + ddl_pg_state.Text.ToString().Trim() + "'");
            ddl_pg_board.Items.Clear();
            ddl_pg_board.DataSource = state_board_name.Tables[0];

            ddl_pg_board.DataTextField = "child";
            ddl_pg_board.DataBind();
            ddl_pg_board.Items.Insert(0, new ListItem("-- Select --", "0"));
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void btn_pg_submit_Click(object sender, EventArgs e)
    {
        try
        {
            if (validate_pg() == true && pg_file_upload.HasFiles == true)
            {
                DataSet ds = ((DataSet)Session["emp_data"]);
                string qry4 = "";
                string qry = "";
                if (flagrid == 0)
                {
                    Session["subject"] = "";

                }
                if (Session["subject"].ToString() != string.Empty)
                {
                    savedoc(pg_file_upload, "PG_" + Session["subject"].ToString());
                    qry4 = "delete from emp_education_details where emp_id='" + Session["emp_id"] + "' and emp_degree_name='PG' and emp_specialization_subj='" + Session["subject"].ToString() + "'";
                }
                else
                {
                    savedoc(pg_file_upload, "PG_" + txt_pg_specilize_sub.Text);
                    qry4 = "delete from emp_education_details where emp_id='" + Session["emp_id"] + "' and emp_degree_name='PG' and emp_specialization_subj='" + txt_pg_specilize_sub.Text + "'";


                }
                int l = c1.DMLquerries_1(qry4);

                string urlalias = cls.urls();
                string url = @urlalias + "EmpQualification/";

               // string url = "http://localhost:9199/EmpQualification/";

                q.emp_id = Session["emp_id"].ToString();
                q.emp_coll_name = aapostrophe(txt_pg_insti_name.Text);
                q.emp_uni_board_name = ddl_pg_board.SelectedItem.Text;
                q.emp_deg_name = "PG";
                q.emp_deg_type = "PG";
                q.emp_spec_subject = aapostrophe(txt_pg_specilize_sub.Text);
                q.emp_month_of_passing = ddl_pg_month.SelectedItem.Text;
                q.emp_year_of_passing = ddl_pg_year.SelectedItem.Text;
                q.emp_mrk_obt = aapostrophe(txt_pg_mks_obt.Text);
                q.emp_tot_mrk = aapostrophe(txt_pg_total_mks.Text);
                q.emp_class_sec = aapostrophe(txt_pg_grade.Text);
                q.emp_pursuing = null;
                q.emp_exm_order = null;
                q.emp_exm_netset_remrk = null;
                q.emp_coll_state = ddl_pg_state.SelectedItem.Text;
                q.emp_coll_place = aapostrophe(txt_pg_insti_place.Text);
                q.emp_seat_no = aapostrophe(txt_pg_seat_no.Text);
                q.type = "insert";


                string jsonString = JsonHelper.JsonSerializer<emp_qualification>(q);
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
                    if (result.ToString().Contains("Insert") == true)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Saved Successfully', { color: '#fff', background: '#198104', blur: 0.2, delay: 0 });", true);
                        btn_pg.BackColor = System.Drawing.ColorTranslator.FromHtml("#D44950");

                        ClearAll();
                        Hidetabs();
                        grd_ssc.Visible = true;
                        LoadDocGrid();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Connection Error', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);

                    }
                }


                Grid();
                changeColor();

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please fill all the details');", true);
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void grd_pg_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grd_pg.EditIndex = -1;
        
    }

    public bool validate_pg()
    {

        if (ddl_pg_state.SelectedIndex <= 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Select State');", true);
            return false;
        }

        if (txt_pg_insti_name.Text.Trim().Equals("") || txt_pg_insti_name.Text.Trim().Length == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Enter Institute Name');", true);
            return false;
        }
        if (txt_pg_insti_place.Text.Trim().Equals("") || txt_pg_insti_place.Text.Trim().Length == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Enter Institute Place');", true);
            return false;
        }
        if (ddl_pg_year.SelectedIndex <= 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Select passing year');", true);
            return false;
        }
        if (ddl_pg_month.SelectedIndex <= 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Select passing month');", true);
            return false;
        }
        if (txt_pg_mks_obt.Text.Trim().Equals("") || txt_pg_mks_obt.Text.Trim().Length == 0)
        {
            if (txt_pg_grade.Text.Trim().Equals("") || txt_pg_grade.Text.Trim().Length == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Enter Grade');", true);
                return false;
            }
        }
        else
        {
            if (txt_pg_total_mks.Text.Trim().Equals("") || txt_pg_total_mks.Text.Trim().Length == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Enter Out Of Marks');", true);
                return false;
            }
        }
        return true;

    }
    //-------------------------------M-PHILL--------------------------------------------------------

    protected void ddl_mphill_state_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet state_board_name = c1.fill_dataset("select  child from dbo.State_category_details where Main = 'State' and parent = '" + ddl_mphill_state.Text.ToString().Trim() + "'");
            ddl_mphill_board.Items.Clear();
            ddl_mphill_board.DataSource = state_board_name.Tables[0];

            ddl_mphill_board.DataTextField = "child";
            ddl_mphill_board.DataBind();
            ddl_mphill_board.Items.Insert(0, new ListItem("-- Select --", "0"));
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void btn_mphill_submit_Click(object sender, EventArgs e)
    {
        try
        {
            if (validate_mphill() == true && mphill_file_upload.HasFiles == true)
            {
                DataSet ds = ((DataSet)Session["emp_data"]);
                string qry5 = "";
                string qry = "";
                if (flagrid == 0)
                {
                    Session["subject"] = "";

                }
                if (Session["subject"].ToString() != string.Empty)
                {

                    savedoc(mphill_file_upload, "M-Phill_" + Session["subject"].ToString());
                    qry5 = "delete from emp_education_details where emp_id='" + Session["emp_id"] + "' and emp_degree_name='M-Phill' and emp_specialization_subj='" + Session["subject"].ToString() + "'";
                }
                else
                {
                    savedoc(mphill_file_upload, "M-Phill_" + txt_mphill_specilize_sub.Text);
                    qry5 = "delete from emp_education_details where emp_id='" + Session["emp_id"] + "' and emp_degree_name='M-Phill' and emp_specialization_subj='" + txt_mphill_specilize_sub.Text + "'";

                }
                int l = c1.DMLquerries_1(qry5);

                string urlalias = cls.urls();
                string url = @urlalias + "EmpQualification/";

                //string url = "http://localhost:9199/EmpQualification/";

                q.emp_id = Session["emp_id"].ToString();
                q.emp_coll_name = aapostrophe(txt_mphill_insti_name.Text);
                q.emp_uni_board_name = ddl_mphill_board.SelectedItem.Text;
                q.emp_deg_name = "M-Phill";
                q.emp_deg_type = "M.Phill";
                q.emp_spec_subject = aapostrophe(txt_mphill_specilize_sub.Text);
                q.emp_month_of_passing = ddl_mphill_month.SelectedItem.Text;
                q.emp_year_of_passing = ddl_mphill_year.SelectedItem.Text;
                q.emp_mrk_obt = aapostrophe(txt_mphill_mks_obt.Text);
                q.emp_tot_mrk = aapostrophe(txt_mphill_total_mks.Text);
                q.emp_class_sec = aapostrophe(txt_mphill_grade.Text);
                q.emp_pursuing = null;
                q.emp_exm_order = null;
                q.emp_exm_netset_remrk = null;
                q.emp_coll_state = ddl_mphill_state.SelectedItem.Text;
                q.emp_coll_place = aapostrophe(txt_mphill_insti_place.Text);
                q.emp_seat_no = aapostrophe(txt_mphill_seat_no.Text);
                q.type = "insert";

                string jsonString = JsonHelper.JsonSerializer<emp_qualification>(q);
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
                    if (result.ToString().Contains("Insert") == true)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Saved Successfully', { color: '#fff', background: '#198104', blur: 0.2, delay: 0 });", true);
                        btn_mphill.BackColor = System.Drawing.ColorTranslator.FromHtml("#D44950");

                        ClearAll();
                        Hidetabs();
                        grd_ssc.Visible = true;
                        LoadDocGrid();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Connection Error', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);

                    }
                }

                Grid();
                changeColor();
                LoadDocGrid();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please fill all the details');", true);
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
        
    }

    protected void grd_mphill_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grd_mphill.EditIndex = -1;

    }

    public bool validate_mphill()
    {

        if (ddl_mphill_state.SelectedIndex <= 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Select State');", true);
            return false;
        }

        if (txt_mphill_insti_name.Text.Trim().Equals("") || txt_mphill_insti_name.Text.Trim().Length == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Enter Institute Name');", true);
            return false;
        }
        if (txt_mphill_insti_place.Text.Trim().Equals("") || txt_mphill_insti_place.Text.Trim().Length == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Enter Institute Place');", true);
            return false;
        }
        if (ddl_mphill_year.SelectedIndex <= 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Select passing year');", true);
            return false;
        }
        if (ddl_mphill_month.SelectedIndex <= 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Select passing month');", true);
            return false;
        }
        if (txt_mphill_mks_obt.Text.Trim().Equals("") || txt_mphill_mks_obt.Text.Trim().Length == 0)
        {
            if (txt_mphill_grade.Text.Trim().Equals("") || txt_mphill_grade.Text.Trim().Length == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Enter Grade');", true);
                return false;
            }
        }
        else
        {
            if (txt_mphill_total_mks.Text.Trim().Equals("") || txt_mphill_total_mks.Text.Trim().Length == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Enter Out Of Marks');", true);
                return false;
            }
        }
        return true;

    }
    //-----------------------------------------PHD----------------------------------------------------
    protected void ddl_phd_state_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet state_board_name = c1.fill_dataset("select  child from dbo.State_category_details where Main = 'State' and parent = '" + ddl_phd_state.Text.ToString().Trim() + "'");
            ddl_phd_board.Items.Clear();
            ddl_phd_board.DataSource = state_board_name.Tables[0];

            ddl_phd_board.DataTextField = "child";
            ddl_phd_board.DataBind();
            ddl_phd_board.Items.Insert(0, new ListItem("-- Select --", "0"));
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void grd_phd_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grd_phd.EditIndex = -1;
    }

    protected void btn_phd_submit_Click(object sender, EventArgs e)
    {
        try
        {

            if (validate_phd() == true && phd_file_upload.HasFiles == true)
            {
                DataSet ds = ((DataSet)Session["emp_data"]);
                string qry6 = "";
                string qry = "";

                if (flagrid == 0)
                {
                    Session["subject"] = "";

                }
                if (Session["subject"].ToString() != string.Empty)
                {
                    savedoc(phd_file_upload, "Ph-D_" + Session["subject"].ToString());
                    qry6 = "delete from emp_education_details where emp_id='" + Session["emp_id"] + "' and emp_degree_name='Ph.D' and emp_specialization_subj='" + Session["subject"].ToString() + "'";
                }
                else
                {
                    savedoc(phd_file_upload, "Ph-D_" + txt_phd_specilize_Sub.Text);
                    qry6 = "delete from emp_education_details where emp_id='" + Session["emp_id"] + "' and emp_degree_name='Ph.D' and emp_specialization_subj='" + txt_phd_specilize_Sub.Text + "'";
                }

                int l = c1.DMLquerries_1(qry6);

                string urlalias = cls.urls();
                string url = @urlalias + "EmpQualification/";

               // string url = "http://localhost:9199/EmpQualification/";

                q.emp_id = Session["emp_id"].ToString();
                q.emp_coll_name = aapostrophe(txt_phd_insti_name.Text);
                q.emp_uni_board_name = ddl_phd_board.SelectedItem.Text;
                q.emp_deg_name = "Ph.D";
                q.emp_deg_type = "Ph.D";
                q.emp_spec_subject = aapostrophe(txt_phd_specilize_Sub.Text);
                q.emp_month_of_passing = ddl_phd_month.SelectedItem.Text;
                q.emp_year_of_passing = ddl_phd_year.SelectedItem.Text;
                q.emp_mrk_obt = aapostrophe(txt_phd_mks_obt.Text);
                q.emp_tot_mrk = aapostrophe(txt_Phd_total_mks.Text);
                q.emp_class_sec = aapostrophe(txt_phd_grade.Text);
                q.emp_pursuing = null;
                q.emp_exm_order = null;
                q.emp_exm_netset_remrk = null;
                q.emp_coll_state = ddl_phd_state.SelectedItem.Text;
                q.emp_coll_place = aapostrophe(txt_phd_insti_place.Text);
                q.emp_seat_no = aapostrophe(txt_phd_seat_no.Text);
                q.type = "insert";

                string jsonString = JsonHelper.JsonSerializer<emp_qualification>(q);
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
                    if (result.ToString().Contains("Insert") == true)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Saved Successfully', { color: '#fff', background: '#198104', blur: 0.2, delay: 0 });", true);
                        btn_phd.BackColor = System.Drawing.ColorTranslator.FromHtml("#D44950");

                        ClearAll();
                        Hidetabs();
                        grd_ssc.Visible = true;
                        LoadDocGrid();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Connection Error', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);

                    }
                }


                Grid();
                changeColor();
                LoadDocGrid();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please fill all the details');", true);
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    public bool validate_phd()
    {

        if (ddl_phd_state.SelectedIndex <= 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Select State');", true);
            return false;
        }

        if (txt_phd_insti_name.Text.Trim().Equals("") || txt_phd_insti_name.Text.Trim().Length == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Enter Institute Name');", true);
            return false;
        }
        if (txt_phd_insti_place.Text.Trim().Equals("") || txt_phd_insti_place.Text.Trim().Length == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Enter Institute Place');", true);
            return false;
        }
        if (ddl_phd_year.SelectedIndex <= 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Select passing year');", true);
            return false;
        }
        if (ddl_phd_month.SelectedIndex <= 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Select passing month');", true);
            return false;
        }
        if (txt_phd_mks_obt.Text.Trim().Equals("") || txt_phd_mks_obt.Text.Trim().Length == 0)
        {
            if (txt_phd_grade.Text.Trim().Equals("") || txt_phd_grade.Text.Trim().Length == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Enter Grade');", true);
                return false;
            }
        }
        else
        {
            if (txt_Phd_total_mks.Text.Trim().Equals("") || txt_Phd_total_mks.Text.Trim().Length == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Enter Out Of Marks');", true);
                return false;
            }
        }
        return true;

    }
    //----------------------------------------net--------------------------------------------------------

    protected void grd_net_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grd_phd.EditIndex = -1;
    }

    protected void btn_net_submit_Click(object sender, EventArgs e)
    {
        try
        {
            if (validate_net() == true)
            {
                savedoc(net_file_upload, "NET");

                //err.Visible = false;

                DataSet ds = ((DataSet)Session["emp_data"]);
                string qry7 = "";

                qry7 = "delete from emp_education_details where emp_id='" + Session["emp_id"] + "' and emp_degree_name='NET'";
                int l = c1.DMLquerries_1(qry7);

                string urlalias = cls.urls();
                string url = @urlalias + "EmpQualification/";

              //  string url = "http://localhost:9199/EmpQualification/";

                q.emp_id = Session["emp_id"].ToString();
                q.emp_coll_name = null;
                q.emp_uni_board_name = null;
                q.emp_deg_name = "NET";
                q.emp_deg_type = "NET";
                q.emp_spec_subject = null;
                q.emp_month_of_passing = ddl_net_month.SelectedItem.Text;
                q.emp_year_of_passing = ddl_net_year.SelectedItem.Text;
                q.emp_mrk_obt = null;
                q.emp_tot_mrk = null;
                q.emp_class_sec = null;
                q.emp_pursuing = null;
                q.emp_exm_order = null;
                q.emp_exm_netset_remrk = ddl_net.SelectedItem.Text;
                q.emp_coll_state = null;
                q.emp_coll_place = null;
                q.emp_seat_no = null;
                q.type = "insert";

                string jsonString = JsonHelper.JsonSerializer<emp_qualification>(q);
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
                    if (result.ToString().Contains("Insert") == true)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Saved Successfully', { color: '#fff', background: '#198104', blur: 0.2, delay: 0 });", true);
                        btn_net.BackColor = System.Drawing.ColorTranslator.FromHtml("#D44950");

                        ClearAll();
                        Hidetabs();
                        grd_ssc.Visible = true;
                        LoadDocGrid();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Connection Error', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);

                    }
                }



                Grid();
                changeColor();
                LoadDocGrid();

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please fill all the details');", true);
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    public bool validate_net()
    {

        if (ddl_net.SelectedIndex <= 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Select Qualified/Not Qualified');", true);
            return false;
        }
        else {
            if (ddl_net.SelectedIndex == 1 && net_file_upload.HasFiles==false)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Select file to upload');", true);
                return false;
            }
        }
        if (ddl_net_year.SelectedIndex <= 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Select year');", true);
            return false;
        }
        if (ddl_net_month.SelectedIndex <= 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Select month');", true);
            return false;
        }
      
        return true;

    }

    //----------------------------------------other-------------------------------------------------------------
    protected void ddl_others_state_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet state_board_name = c1.fill_dataset("select  child from dbo.State_category_details where Main = 'State' and parent = '" + ddl_others_state.Text.ToString().Trim() + "'");
            ddl_others_board.Items.Clear();
            ddl_others_board.DataSource = state_board_name.Tables[0];

            ddl_others_board.DataTextField = "child";
            ddl_others_board.DataBind();
            ddl_others_board.Items.Insert(0, new ListItem("-- Select --", "0"));
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void btn_others_submit_Click(object sender, EventArgs e)
    {
        try
        {
            if (validate_others() == true && others_file_upload.HasFiles == true)
            {
                DataSet ds = ((DataSet)Session["emp_data"]);
                string qry6 = "";
                string qry = "";
                if (Session["subject"] != null)
                {

                    if (Session["subject"].ToString() != string.Empty)
                    {
                        savedoc(others_file_upload, "Others_" + Session["subject"].ToString());
                        qry6 = "delete from emp_education_details where emp_id='" + Session["emp_id"] + "' and emp_specialization_subj='" + Session["subject"].ToString() + "'";
                        int l = c1.DMLquerries_1(qry6);
                    }

                }
                else
                {

                    savedoc(others_file_upload, "Others_" + txt_others_specilize_sub.Text);
                    qry6 = "delete from emp_education_details where emp_id='" + Session["emp_id"] + "' and emp_specialization_subj='" + txt_others_specilize_sub.Text + "'";
                    int l = c1.DMLquerries_1(qry6);
                }

                string urlalias = cls.urls();
                string url = @urlalias + "EmpQualification/";

                //string url = "http://localhost:9199/EmpQualification/";

                q.emp_id = Session["emp_id"].ToString();
                q.emp_coll_name = null;
                q.emp_uni_board_name = null;
                q.emp_deg_name = "NET";
                q.emp_deg_type = "NET";
                q.emp_spec_subject = null;
                q.emp_month_of_passing = ddl_net_month.SelectedItem.Text;
                q.emp_year_of_passing = ddl_net_year.SelectedItem.Text;
                q.emp_mrk_obt = null;
                q.emp_tot_mrk = null;
                q.emp_class_sec = null;
                q.emp_pursuing = null;
                q.emp_exm_order = null;
                q.emp_exm_netset_remrk = ddl_net.SelectedItem.Text;
                q.emp_coll_state = null;
                q.emp_coll_place = null;
                q.emp_seat_no = null;
                q.type = "insert";

                

                string jsonString = JsonHelper.JsonSerializer<emp_qualification>(q);
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
                    if (result.ToString().Contains("Insert") == true)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Saved Successfully', { color: '#fff', background: '#198104', blur: 0.2, delay: 0 });", true);
                        btn_others.BackColor = System.Drawing.ColorTranslator.FromHtml("#D44950");

                        ClearAll();
                        Hidetabs();
                        grd_ssc.Visible = true;
                        LoadDocGrid();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Connection Error', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);

                    }
                }


                Grid();
                changeColor();

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please fill all the details');", true);
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    public bool validate_others()
    {

        if (ddl_others_state.SelectedIndex <= 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Select State');", true);
            return false;
        }


        if (ddl_others_year.SelectedIndex <= 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Select passing year');", true);
            return false;
        }
        if (ddl_others_month.SelectedIndex <= 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Select passing month');", true);
            return false;
        }
        if (txt_others_mks_obt.Text.Trim().Equals("") || txt_others_mks_obt.Text.Trim().Length == 0)
        {
            if (txt_others_grade.Text.Trim().Equals("") || txt_others_grade.Text.Trim().Length == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Enter Grade');", true);
                return false;
            }
        }
        else
        {
            if (txt_others_total_mks.Text.Trim().Equals("") || txt_others_total_mks.Text.Trim().Length == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Enter Out Of Marks');", true);
                return false;
            }
        }
        return true;

    }
    //----------------------------------------set------------------------------------------------------------
    public bool validate_set()
    {

        if (ddl_set.SelectedIndex <= 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Select Qualified/Not Qualified');", true);
            return false;
        }
        else
        {
            if (ddl_set.SelectedIndex == 1 && set_file_upload.HasFiles == false)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Select file to upload');", true);
                return false;
            }
        }
        if (ddl_set_year.SelectedIndex <= 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Select year');", true);
            return false;
        }
        if (ddl_set_month.SelectedIndex <= 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Select month');", true);
            return false;
        }

        return true;

    }
    protected void btn_set_submit_Click(object sender, EventArgs e)
    {
        try
        {
            if (validate_set() == true)
            {
                savedoc(set_file_upload, "SET");

                // err.Visible = false;

                DataSet ds = ((DataSet)Session["emp_data"]);
                string qry8 = "";
                string qry = "";
                qry8 = "delete from emp_education_details where emp_id='" + Session["emp_id"] + "' and emp_degree_name='SET'";
                int l = c1.DMLquerries_1(qry8);

                string urlalias = cls.urls();
                string url = @urlalias + "EmpQualification/";

               // string url = "http://localhost:9199/EmpQualification/";

                q.emp_id = Session["emp_id"].ToString();
                q.emp_coll_name = null;
                q.emp_uni_board_name = null;
                q.emp_deg_name = "SET";
                q.emp_deg_type = "SET";
                q.emp_spec_subject = null;
                q.emp_month_of_passing = ddl_set_month.SelectedItem.Text;
                q.emp_year_of_passing = ddl_set_year.SelectedItem.Text;
                q.emp_mrk_obt = null;
                q.emp_tot_mrk = null;
                q.emp_class_sec = null;
                q.emp_pursuing = null;
                q.emp_exm_order = null;
                q.emp_exm_netset_remrk = ddl_set.SelectedItem.Text;
                q.emp_coll_state = null;
                q.emp_coll_place = null;
                q.emp_seat_no = null;
                q.type = "insert";

                string jsonString = JsonHelper.JsonSerializer<emp_qualification>(q);
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
                    if (result.ToString().Contains("Insert") == true)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Saved Successfully', { color: '#fff', background: '#198104', blur: 0.2, delay: 0 });", true);
                        btn_set.BackColor = System.Drawing.ColorTranslator.FromHtml("#D44950");

                        ClearAll();
                        Hidetabs();
                        grd_ssc.Visible = true;
                        LoadDocGrid();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Connection Error', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);

                    }
                }


                Grid();
                changeColor();

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please fill all the details');", true);
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }
  
    protected void grd_set_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grd_phd.EditIndex = -1;
     
    }

    //----------------------------------------------------------------------------------------------------------------

    protected void grd_ssc_SelectedIndexChanged(object sender, EventArgs e)
    {
        flagrid = 1;

        try
        {
            GridView gv = (GridView)(sender);


            if (gv.Rows[gv.SelectedIndex].Cells[1].Text == "S.S.C")
            {


                tabhsc.Visible = false;
                tabssc.Visible = true;
                tabdegree.Visible = false;
                tabpg.Visible = false;
                tabmphil.Visible = false;
                tabphd.Visible = false;



                string board = gv.Rows[gv.SelectedIndex].Cells[4].Text;
                ddl_ssc_board.SelectedItem.Text = board;

                ddl_ssc_state.SelectedIndex = ddl_ssc_state.Items.IndexOf(ddl_ssc_state.Items.FindByText(gv.Rows[gv.SelectedIndex].Cells[3].Text));
                txt_ssc_insti_name.Text = gv.Rows[gv.SelectedIndex].Cells[2].Text;
                txt_ssc_insti_place.Text = gv.Rows[gv.SelectedIndex].Cells[5].Text;
                ddl_ssc_year.SelectedIndex = ddl_ssc_year.Items.IndexOf(ddl_ssc_year.Items.FindByText(gv.Rows[gv.SelectedIndex].Cells[7].Text));
                ddl_ssc_month.SelectedIndex = ddl_ssc_month.Items.IndexOf(ddl_ssc_month.Items.FindByText(gv.Rows[gv.SelectedIndex].Cells[6].Text));
                txt_ssc_mks_obtnd.Text = gv.Rows[gv.SelectedIndex].Cells[8].Text;
                txt_ssc_nout_mks.Text = gv.Rows[gv.SelectedIndex].Cells[9].Text;
                txt_ssc_grd.Text = gv.Rows[gv.SelectedIndex].Cells[10].Text;
                txt_ssc_seat_no.Text = gv.Rows[gv.SelectedIndex].Cells[11].Text;
                //txt_ssc_specialize_subject.Text = gv.Rows[gv.SelectedIndex].Cells[12].Text;
            }
            else if (gv.Rows[gv.SelectedIndex].Cells[1].Text == "H.S.C")
            {
                tabhsc.Visible = true;
                tabssc.Visible = false;
                tabdegree.Visible = false;
                tabpg.Visible = false;
                tabmphil.Visible = false;
                tabphd.Visible = false;
                string board = gv.Rows[gv.SelectedIndex].Cells[4].Text;
                ddl_hsc_board.SelectedItem.Text = board;
                ddl_hsc_state.SelectedIndex = ddl_hsc_state.Items.IndexOf(ddl_hsc_state.Items.FindByText(gv.Rows[gv.SelectedIndex].Cells[3].Text));
                // ddl_hsc_board.SelectedIndex = ddl_hsc_board.Items.IndexOf(ddl_hsc_board.Items.FindByText(gv.Rows[gv.SelectedIndex].Cells[2].Text));
                txt_hsc_insti_name.Text = gv.Rows[gv.SelectedIndex].Cells[2].Text;
                txt_hsc_insti_place.Text = gv.Rows[gv.SelectedIndex].Cells[5].Text;
                ddl_hsc_year.SelectedIndex = ddl_ssc_year.Items.IndexOf(ddl_ssc_year.Items.FindByText(gv.Rows[gv.SelectedIndex].Cells[7].Text));
                ddl_hsc_month.SelectedIndex = ddl_ssc_month.Items.IndexOf(ddl_ssc_month.Items.FindByText(gv.Rows[gv.SelectedIndex].Cells[6].Text));
                txt_hsc_mks_obt.Text = gv.Rows[gv.SelectedIndex].Cells[8].Text;
                txt_hsc_total_mks.Text = gv.Rows[gv.SelectedIndex].Cells[9].Text;
                txt_hsc_grade.Text = gv.Rows[gv.SelectedIndex].Cells[10].Text;
                txt_hsc_seat.Text = gv.Rows[gv.SelectedIndex].Cells[11].Text;
                // txt_hsc_specilize_sub.Text = gv.Rows[gv.SelectedIndex].Cells[12].Text;
            }
            else if (gv.Rows[gv.SelectedIndex].Cells[1].Text == "Degree")
            {
                tabhsc.Visible = false;
                tabssc.Visible = false;
                tabdegree.Visible = true;
                tabpg.Visible = false;
                tabmphil.Visible = false;
                tabphd.Visible = false;
                string board = gv.Rows[gv.SelectedIndex].Cells[4].Text;
                ddl_degree_board.SelectedItem.Text = board;

                ddl_degree_state.SelectedIndex = ddl_degree_state.Items.IndexOf(ddl_degree_state.Items.FindByText(gv.Rows[gv.SelectedIndex].Cells[3].Text));
                //ddl_degree_board.SelectedIndex = ddl_degree_board.Items.IndexOf(ddl_degree_board.Items.FindByText(gv.Rows[gv.SelectedIndex].Cells[2].Text));
                txt_degree_insti_name.Text = gv.Rows[gv.SelectedIndex].Cells[2].Text;
                txt_degree_insti_place.Text = gv.Rows[gv.SelectedIndex].Cells[5].Text;
                ddl_degree_year.SelectedIndex = ddl_degree_year.Items.IndexOf(ddl_degree_year.Items.FindByText(gv.Rows[gv.SelectedIndex].Cells[7].Text));
                ddl_degree_month.SelectedIndex = ddl_degree_month.Items.IndexOf(ddl_degree_month.Items.FindByText(gv.Rows[gv.SelectedIndex].Cells[6].Text));
                txt_degree_mks_obtnd.Text = gv.Rows[gv.SelectedIndex].Cells[8].Text;
                txt_degree_total_mks.Text = gv.Rows[gv.SelectedIndex].Cells[9].Text;
                txt_degree_grade.Text = gv.Rows[gv.SelectedIndex].Cells[10].Text;
                txt_degree_seat_no.Text = gv.Rows[gv.SelectedIndex].Cells[11].Text;
                Session["subject"] = gv.Rows[gv.SelectedIndex].Cells[12].Text;
                txt_degree_specilize_subject.Text = gv.Rows[gv.SelectedIndex].Cells[12].Text;
            }
            else if (gv.Rows[gv.SelectedIndex].Cells[1].Text == "PG")
            {
                tabhsc.Visible = false;
                tabssc.Visible = false;
                tabdegree.Visible = false;
                tabpg.Visible = true;
                tabmphil.Visible = false;
                tabphd.Visible = false;

                string board = gv.Rows[gv.SelectedIndex].Cells[4].Text;
                ddl_pg_board.SelectedItem.Text = board;

                ddl_pg_state.SelectedIndex = ddl_pg_state.Items.IndexOf(ddl_pg_state.Items.FindByText(gv.Rows[gv.SelectedIndex].Cells[3].Text));
                // ddl_pg_board.SelectedIndex = ddl_pg_board.Items.IndexOf(ddl_pg_board.Items.FindByText(gv.Rows[gv.SelectedIndex].Cells[2].Text));
                txt_pg_insti_name.Text = gv.Rows[gv.SelectedIndex].Cells[2].Text;
                txt_pg_insti_place.Text = gv.Rows[gv.SelectedIndex].Cells[5].Text;
                ddl_pg_year.SelectedIndex = ddl_pg_year.Items.IndexOf(ddl_pg_year.Items.FindByText(gv.Rows[gv.SelectedIndex].Cells[7].Text));
                ddl_pg_month.SelectedIndex = ddl_pg_month.Items.IndexOf(ddl_pg_month.Items.FindByText(gv.Rows[gv.SelectedIndex].Cells[6].Text));
                txt_pg_mks_obt.Text = gv.Rows[gv.SelectedIndex].Cells[8].Text;
                txt_pg_total_mks.Text = gv.Rows[gv.SelectedIndex].Cells[9].Text;
                txt_pg_grade.Text = gv.Rows[gv.SelectedIndex].Cells[10].Text;
                txt_pg_seat_no.Text = gv.Rows[gv.SelectedIndex].Cells[11].Text;
                Session["subject"] = gv.Rows[gv.SelectedIndex].Cells[12].Text;
                txt_pg_specilize_sub.Text = gv.Rows[gv.SelectedIndex].Cells[12].Text;
            }
            else if (gv.Rows[gv.SelectedIndex].Cells[1].Text == "M-Phill")
            {
                tabhsc.Visible = false;
                tabssc.Visible = false;
                tabdegree.Visible = false;
                tabpg.Visible = false;
                tabmphil.Visible = true;
                tabphd.Visible = false;


                string board = gv.Rows[gv.SelectedIndex].Cells[4].Text;
                ddl_mphill_board.SelectedItem.Text = board;

                ddl_mphill_state.SelectedIndex = ddl_mphill_state.Items.IndexOf(ddl_mphill_state.Items.FindByText(gv.Rows[gv.SelectedIndex].Cells[3].Text));
                //ddl_mphill_board.SelectedIndex = ddl_mphill_board.Items.IndexOf(ddl_mphill_board.Items.FindByText(gv.Rows[gv.SelectedIndex].Cells[2].Text));
                txt_mphill_insti_name.Text = gv.Rows[gv.SelectedIndex].Cells[2].Text;
                txt_mphill_insti_place.Text = gv.Rows[gv.SelectedIndex].Cells[5].Text;
                ddl_mphill_year.SelectedIndex = ddl_mphill_year.Items.IndexOf(ddl_mphill_year.Items.FindByText(gv.Rows[gv.SelectedIndex].Cells[7].Text));
                ddl_mphill_month.SelectedIndex = ddl_mphill_month.Items.IndexOf(ddl_mphill_month.Items.FindByText(gv.Rows[gv.SelectedIndex].Cells[6].Text));
                txt_mphill_mks_obt.Text = gv.Rows[gv.SelectedIndex].Cells[8].Text;
                txt_mphill_total_mks.Text = gv.Rows[gv.SelectedIndex].Cells[9].Text;
                txt_mphill_grade.Text = gv.Rows[gv.SelectedIndex].Cells[10].Text;
                txt_mphill_seat_no.Text = gv.Rows[gv.SelectedIndex].Cells[11].Text;
                Session["subject"] = gv.Rows[gv.SelectedIndex].Cells[12].Text;
                txt_mphill_specilize_sub.Text = gv.Rows[gv.SelectedIndex].Cells[12].Text;
            }
            else if (gv.Rows[gv.SelectedIndex].Cells[1].Text == "Ph.D")
            {
                tabhsc.Visible = false;
                tabssc.Visible = false;
                tabdegree.Visible = false;
                tabpg.Visible = false;
                tabmphil.Visible = false;
                tabphd.Visible = true;

                string board = gv.Rows[gv.SelectedIndex].Cells[4].Text;
                ddl_phd_board.SelectedItem.Text = board;

                ddl_phd_state.SelectedIndex = ddl_phd_state.Items.IndexOf(ddl_phd_state.Items.FindByText(gv.Rows[gv.SelectedIndex].Cells[3].Text));
                //ddl_phd_board.SelectedIndex = ddl_phd_board.Items.IndexOf(ddl_phd_board.Items.FindByText(gv.Rows[gv.SelectedIndex].Cells[2].Text));
                txt_phd_insti_name.Text = gv.Rows[gv.SelectedIndex].Cells[2].Text;
                txt_phd_insti_place.Text = gv.Rows[gv.SelectedIndex].Cells[5].Text;
                ddl_phd_year.SelectedIndex = ddl_phd_year.Items.IndexOf(ddl_phd_year.Items.FindByText(gv.Rows[gv.SelectedIndex].Cells[7].Text));
                ddl_phd_month.SelectedIndex = ddl_phd_month.Items.IndexOf(ddl_phd_month.Items.FindByText(gv.Rows[gv.SelectedIndex].Cells[6].Text));
                txt_phd_mks_obt.Text = gv.Rows[gv.SelectedIndex].Cells[8].Text;
                txt_Phd_total_mks.Text = gv.Rows[gv.SelectedIndex].Cells[9].Text;
                txt_phd_grade.Text = gv.Rows[gv.SelectedIndex].Cells[10].Text;
                txt_phd_seat_no.Text = gv.Rows[gv.SelectedIndex].Cells[11].Text;
                Session["subject"] = gv.Rows[gv.SelectedIndex].Cells[12].Text;
                txt_phd_specilize_Sub.Text = gv.Rows[gv.SelectedIndex].Cells[12].Text;
            }
            else if (gv.Rows[gv.SelectedIndex].Cells[1].Text == "Diploma")
            {
                tabhsc.Visible = false;
                tabssc.Visible = false;
                tabdegree.Visible = false;
                tabpg.Visible = false;
                tabmphil.Visible = false;
                tabphd.Visible = false;
                tabdiploma.Visible = true;

                string board = gv.Rows[gv.SelectedIndex].Cells[4].Text;
                ddl_diploma_board.SelectedItem.Text = board;

                ddl_diploma_state.SelectedIndex = ddl_diploma_state.Items.IndexOf(ddl_diploma_state.Items.FindByText(gv.Rows[gv.SelectedIndex].Cells[3].Text));
                //ddl_phd_board.SelectedIndex = ddl_phd_board.Items.IndexOf(ddl_phd_board.Items.FindByText(gv.Rows[gv.SelectedIndex].Cells[2].Text));
                txt_diploma_insti_name.Text = gv.Rows[gv.SelectedIndex].Cells[2].Text;
                txt_diploma_insti_place.Text = gv.Rows[gv.SelectedIndex].Cells[5].Text;
                ddl_diploma_year.SelectedIndex = ddl_phd_year.Items.IndexOf(ddl_diploma_year.Items.FindByText(gv.Rows[gv.SelectedIndex].Cells[7].Text));
                ddl_diploma_month.SelectedIndex = ddl_phd_month.Items.IndexOf(ddl_diploma_month.Items.FindByText(gv.Rows[gv.SelectedIndex].Cells[6].Text));
                txt_diploma_mks_obt.Text = gv.Rows[gv.SelectedIndex].Cells[8].Text;
                txt_diploma_total_mks.Text = gv.Rows[gv.SelectedIndex].Cells[9].Text;
                txt_diploma_grade.Text = gv.Rows[gv.SelectedIndex].Cells[10].Text;
                txt_diploma_seat_no.Text = gv.Rows[gv.SelectedIndex].Cells[11].Text;
                Session["subject"] = gv.Rows[gv.SelectedIndex].Cells[12].Text;
                //txt_diploma_specilize_Sub.Text = gv.Rows[gv.SelectedIndex].Cells[12].Text;
            }
            else if (gv.Rows[gv.SelectedIndex].Cells[1].Text == "NET")
            {
                Hidetabs();
                tabnet.Visible = true;

                // string board = gv.Rows[gv.SelectedIndex].Cells[13].Text;
                //ddl_net.SelectedItem.Text = board;
                ddl_net.SelectedIndex = ddl_net.Items.IndexOf(ddl_net.Items.FindByText(gv.Rows[gv.SelectedIndex].Cells[13].Text));

                ddl_net_year.SelectedIndex = ddl_net_year.Items.IndexOf(ddl_net_year.Items.FindByText(gv.Rows[gv.SelectedIndex].Cells[7].Text));
                ddl_net_month.SelectedIndex = ddl_net_month.Items.IndexOf(ddl_net_month.Items.FindByText(gv.Rows[gv.SelectedIndex].Cells[6].Text));

            }
            else if (gv.Rows[gv.SelectedIndex].Cells[1].Text == "SET")
            {
                Hidetabs();
                tabset.Visible = true;

                // string board = gv.Rows[gv.SelectedIndex].Cells[13].Text;
                //ddl_net.SelectedItem.Text = board;
                ddl_set.SelectedIndex = ddl_net.Items.IndexOf(ddl_net.Items.FindByText(gv.Rows[gv.SelectedIndex].Cells[13].Text));

                ddl_set_year.SelectedIndex = ddl_net_year.Items.IndexOf(ddl_net_year.Items.FindByText(gv.Rows[gv.SelectedIndex].Cells[7].Text));
                ddl_set_month.SelectedIndex = ddl_net_month.Items.IndexOf(ddl_net_month.Items.FindByText(gv.Rows[gv.SelectedIndex].Cells[6].Text));

            }
            else if (gv.Rows[gv.SelectedIndex].Cells[1].Text == "Others")
            {
                Hidetabs();
                tabothers.Visible = true;


                ddl_others_board.SelectedIndex = ddl_others_board.Items.IndexOf(ddl_others_board.Items.FindByText(gv.Rows[gv.SelectedIndex].Cells[4].Text));


                ddl_others_state.SelectedIndex = ddl_others_state.Items.IndexOf(ddl_others_state.Items.FindByText(gv.Rows[gv.SelectedIndex].Cells[3].Text));
                ddl_others_year.SelectedIndex = ddl_others_year.Items.IndexOf(ddl_others_year.Items.FindByText(gv.Rows[gv.SelectedIndex].Cells[7].Text));
                ddl_others_month.SelectedIndex = ddl_others_month.Items.IndexOf(ddl_others_month.Items.FindByText(gv.Rows[gv.SelectedIndex].Cells[6].Text));



                txt_others_mks_obt.Text = gv.Rows[gv.SelectedIndex].Cells[8].Text;


                txt_others_total_mks.Text = gv.Rows[gv.SelectedIndex].Cells[9].Text;

                txt_others_grade.Text = gv.Rows[gv.SelectedIndex].Cells[10].Text;

                txt_others_seat_no.Text = gv.Rows[gv.SelectedIndex].Cells[11].Text;

                // Session["subject"] = gv.Rows[gv.SelectedIndex].Cells[12].Text;

                txt_others_specilize_sub.Text = gv.Rows[gv.SelectedIndex].Cells[12].Text;
                Session["subject"] = gv.Rows[gv.SelectedIndex].Cells[12].Text;

            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    public bool checkEdcDoc(string filename)
    {
        try
        {
            if (filename.Contains("S.S.C") || filename.Contains("H.S.C") || filename.Contains("diploma") || filename.Contains("Degree") || filename.Contains("PG") || filename.Contains("Ph-D") || filename.Contains("M-Phill") || filename.Contains("NET") || filename.Contains("SET") || filename.Contains("Others"))
            {
                return true;

            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
            return false;
        }
     }

    public void LoadDocGrid()
    {
        try
        {
            if (Directory.Exists(Server.MapPath("~/StaffDocuments/") + Session["emp_id"].ToString()))
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("doc");
                dt.Columns.Add("path");

                string[] f = Directory.GetFiles(Server.MapPath("~/StaffDocuments/" + Session["emp_id"].ToString()));
                foreach (string s in f)
                {
                    string[] filename = s.Split('\\');
                    if (checkEdcDoc(filename[filename.Length - 1]))
                    {
                        dt.Rows.Add();
                        dt.Rows[dt.Rows.Count - 1][0] = filename[filename.Length - 1];
                        dt.Rows[dt.Rows.Count - 1][1] = "StaffDocuments\\" + Session["emp_id"].ToString() + "\\" + filename[filename.Length - 1].ToString();
                    }

                }

                grd_doc.DataSource = dt;
                grd_doc.DataBind();

                grd_doc.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void back_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProfileDashboard.aspx");
    }

    protected void grd_ssc_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            GridView gv = (GridView)(sender);
            string del_str = "";
            string urlalias = cls.urls();
            string url = @urlalias + "EmpQualification/";

           // string url = "http://localhost:9199/EmpQualification/";

            if (gv.Rows[e.RowIndex].Cells[1].Text == "S.S.C")
            {
                q.emp_id = Session["emp_id"].ToString();
                q.emp_deg_name = "S.S.C";
                q.emp_spec_subject = null;
                q.type = "delete";

                deldoc("S.S.C");
            }
            else if (gv.Rows[e.RowIndex].Cells[1].Text == "H.S.C")
            {
                q.emp_id = Session["emp_id"].ToString();
                q.emp_deg_name = "H.S.C";
                q.emp_spec_subject = null;
                q.type = "delete";

                deldoc("H.S.C");
            }
            else if (gv.Rows[e.RowIndex].Cells[1].Text == "Degree")
            {
                q.emp_id = Session["emp_id"].ToString();
                q.emp_deg_name = "Degree";
                q.emp_spec_subject = gv.Rows[e.RowIndex].Cells[12].Text;
                q.type = "delete_with_sub";

                deldoc("Degree_" + gv.Rows[e.RowIndex].Cells[12].Text);
            }
            else if (gv.Rows[e.RowIndex].Cells[1].Text == "PG")
            {
                q.emp_id = Session["emp_id"].ToString();
                q.emp_deg_name = "PG";
                q.emp_spec_subject = gv.Rows[e.RowIndex].Cells[12].Text;
                q.type = "delete_with_sub";

                deldoc("PG_" + gv.Rows[e.RowIndex].Cells[12].Text);
            }
            else if (gv.Rows[e.RowIndex].Cells[1].Text == "M-Phill")
            {
                q.emp_id = Session["emp_id"].ToString();
                q.emp_deg_name = "M-Phill";
                q.emp_spec_subject = gv.Rows[e.RowIndex].Cells[12].Text;
                q.type = "delete_with_sub";

                deldoc("M-Phill_" + gv.Rows[e.RowIndex].Cells[12].Text);

            }
            else if (gv.Rows[e.RowIndex].Cells[1].Text == "Ph.D")
            {
                q.emp_id = Session["emp_id"].ToString();
                q.emp_deg_name = "Ph.D";
                q.emp_spec_subject = gv.Rows[e.RowIndex].Cells[12].Text;
                q.type = "delete_with_sub";

                deldoc("Ph-D_" + gv.Rows[e.RowIndex].Cells[12].Text);

            }
            else if (gv.Rows[e.RowIndex].Cells[1].Text == "Diploma")
            {
                q.emp_id = Session["emp_id"].ToString();
                q.emp_deg_name = "Diploma";
                q.emp_spec_subject = gv.Rows[e.RowIndex].Cells[12].Text;
                q.type = "delete";

                deldoc("diploma");

            }
            else if (gv.Rows[e.RowIndex].Cells[1].Text == "NET")
            {
                q.emp_id = Session["emp_id"].ToString();
                q.emp_deg_name = "NET";
                q.emp_spec_subject = gv.Rows[e.RowIndex].Cells[12].Text;
                q.type = "delete";

                deldoc("NET");

            }
            else if (gv.Rows[e.RowIndex].Cells[1].Text == "SET")
            {
                q.emp_id = Session["emp_id"].ToString();
                q.emp_deg_name = "SET";
                q.emp_spec_subject = gv.Rows[e.RowIndex].Cells[12].Text;
                q.type = "delete";

                deldoc("SET");

            }
            else if (gv.Rows[e.RowIndex].Cells[1].Text == "Others")
            {
                if (gv.Rows[e.RowIndex].Cells[12].Text == "--")
                {
                    q.emp_id = Session["emp_id"].ToString();
                    q.emp_deg_name = "Others";
                    q.emp_spec_subject = null;
                    q.type = "delete";
                }
                else
                {
                    q.emp_id = Session["emp_id"].ToString();
                    q.emp_deg_name = "Others";
                    q.emp_spec_subject = gv.Rows[e.RowIndex].Cells[12].Text;
                    q.type = "delete_with_sub";
                }
                deldoc("Others_" + gv.Rows[e.RowIndex].Cells[12].Text);


            }
            string jsonString = JsonHelper.JsonSerializer<emp_qualification>(q);
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
                if (result.ToString().Contains("Delete") == true)
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Deleted Successfully', { color: '#fff', background: '#198104', blur: 0.2, delay: 0 });", true);


                    Hidetabs();
                    Grid();
                    LoadDocGrid();
                    changeColor();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Connection Error', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);

                }
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
       
    }


   

   
}
