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
using utkarsha_api.App_Start;

public partial class frm_fee_report : System.Web.UI.Page
{
    Class1 cls = new Class1();
    common cm = new common();
    Allocation ac = new Allocation();
    fee_rpt fr = new fee_rpt();
    StringBuilder sb = new StringBuilder();
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        //scriptManager.RegisterPostBackControl(this.btn_excel);
        if (!IsPostBack)
        {

            medium_select();

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
            //grdstudentdetails.DataSource = "";
            //grdstudentdetails.DataBind();
            string type = "ddlfill";

            string urlalias = cls.urls();
            string url = @urlalias + "Common/";
            // string url = @"http://203.192.254.34/Utkarsha_api1/utkarsha_api/Common/";
            cm.type = type.ToString();

            string jsonString = JsonHelper.JsonSerializer<common>(cm);


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
                ddl_medium.DataSource = dt1;
                ddl_medium.DataTextField = "medium";
                ddl_medium.DataValueField = "med_id";
                ddl_medium.DataBind();
                ddl_medium.Items.Insert(0, "--Select--");
                ddl_medium.SelectedIndex = 0;
                sb.Append("");
                tbl_report.Text = sb.ToString();
                btn_excel.Visible = false;
                ddl_class.DataSource = "";
                ddl_class.DataBind();
                ddl_division.DataSource = "";
                ddl_division.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }




    protected void ddl_medium_SelectedIndexChanged(object sender, EventArgs e)
    {
      

        try
        {
            sb.Append("");
            tbl_report.Text = sb.ToString();
            btn_excel.Visible = false;
            
            ddl_division.DataSource = "";
            ddl_division.DataBind();
            if (ddl_medium.SelectedIndex > 0)
            {
                DataSet ds = (DataSet)Session["DSLIST"];

                foreach (DataTable table in ds.Tables)
                {
                    if (table.TableName.Equals(ddl_medium.SelectedValue.ToString()))
                    {
                        ddl_class.DataSource = table;
                        ddl_class.DataTextField = "std_name";
                        ddl_class.DataValueField = "std_id";
                        ddl_class.DataBind();
                       
                        ddl_class.Items.Insert(0, "--Select--");
                        ddl_class.SelectedIndex = 0;
                        Session["tab"] = table;
                    }
                }
            }
            else
            {
                ddl_class.DataSource = "";
                ddl_class.DataBind();
            }
        }
        catch (Exception ex)
        {
            //Response.Redirect("Login.aspx");
        }
    }
    protected void ddl_class_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            sb.Append("");
            tbl_report.Text = sb.ToString();
            btn_excel.Visible = false;
            
            if (ddl_class.SelectedIndex > 0)
            {
                DataSet ds = new DataSet();
                string urlalias = cls.urls();
                string url = @urlalias + "all/";

                ac.type = "divload";
                ac.medium = ddl_medium.SelectedValue.ToString();
                ac.classid = ddl_class.SelectedValue.ToString();
                ac.ayid = Session["acdyear"].ToString();

                string jsonString = JsonHelper.JsonSerializer<Allocation>(ac);
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
                    ds = JsonConvert.DeserializeObject<DataSet>(result);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddl_division.DataSource = ds.Tables[0];
                        ddl_division.DataTextField = "division_name";
                        ddl_division.DataValueField = "division_id";
                        ddl_division.DataBind();
                        ddl_division.Items.Insert(0, "--Select--");
                        ddl_division.SelectedIndex = 0;

                        
                        //grdstudentdetails.DataSource = "";
                        //grdstudentdetails.DataBind();
                        //grid_card.Attributes.Add("style", "display:none");
                    }
                    else
                    {
                        
                       
                        ddl_division.DataSource = "";
                        ddl_division.DataBind();
                        ddl_division.Items.Insert(0, "");
                        ddl_division.SelectedIndex = 0;
                        //grid_card.Attributes.Add("style", "display:none");
                        //grdstudentdetails.DataSource = "";
                        //grdstudentdetails.DataBind();
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('No Division Assigned for Current Academic Year', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                    }
                }

            }
            else
            {
                ddl_division.DataSource = "";
               
                ddl_division.DataBind();


            }
        }
        catch (Exception ex)
        {
            //Response.Redirect("Login.aspx");
        }
    }

    protected void btn_get_Click(object sender, EventArgs e)
    {

        

        
        try
        {
            if (rdotype.SelectedValue == "1")
            {
                string medium_id = "", class_id = "", div_id = ""; string table = "";
                string type = "summarised";
                string ayid = Session["acdyear"].ToString();
                if (ddl_medium.SelectedIndex > 0)
                {
                    medium_id = ddl_medium.SelectedValue;
                }
                else
                {
                    medium_id = "";
                }
                if (ddl_class.SelectedIndex > 0)
                {
                    class_id = ddl_class.SelectedValue;
                }
                else
                {
                    class_id = "";
                }
                if (ddl_division.SelectedIndex > 0)
                {
                    div_id = ddl_division.SelectedValue;
                }
                else
                {
                    div_id = "";
                }

                string urlalias = cls.urls();
                string url = @urlalias + "feereport/";
                //string url = @"http://localhost:9199/statisticalrpt/";
                fr.type = type.ToString();
                fr.med_id = medium_id;
                fr.class_id = class_id;
                fr.div_id = div_id;
                fr.ayid = ayid;
                string jsonString = JsonHelper.JsonSerializer<fee_rpt>(fr);

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
                    DataSet datewise = JsonConvert.DeserializeObject<DataSet>(result);

                    DataTable dt1 = datewise.Tables[0];
                    DataTable dt2 = datewise.Tables[1];

                    string std_id = "", med = ""; int Struct_Amount = 0, bal = 0, refundable = 0, totfees = 0, rowcount = 1 ;
                    if (dt1.Rows.Count > 0)
                    {

                        
                        table = "<table id='tbl_report' style='border:1px solid black ;text-align: center;'>";
                        table = table + "<thead style='border:1px solid black ;text-align: center;'><tr><th style='border:1px solid black ;text-align: center;;text-align: center;'>Sr No</th><th style='border:1px solid black ;text-align: center;'>Student ID</th><th style='border:1px solid black ;text-align: center;'>Roll No.</th><th style='border:1px solid black ;text-align: center;'>GR No.</th><th style='border:1px solid black ;text-align: center;'>Division</th><th style='border:1px solid black ;text-align: center;'>Class</th><th style='border:1px solid black ;text-align: center;'>Medium</th><th style='border:1px solid black ;text-align: center;'>Name</th><th style='border:1px solid black ;text-align: center;'>Gender</th><th style='border:1px solid black ;text-align: center;'>Total Fees</th><th style='border:1px solid black ;text-align: center;'>Paid Fees</th><th style='border:1px solid black ;text-align: center;'>Balance Fees</th><th style='border:1px solid black ;text-align: center;'>Refundable Fees</th></tr></thead>";
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            if (i == 0)
                            {
                                table = table+ "<tbody>";
                            }

                            if (med == dt1.Rows[i]["medium_id"].ToString() && std_id == dt1.Rows[i]["class_id"].ToString())
                            {
                                Struct_Amount = totfees;
                            }
                            else
                            {
                                foreach (DataRow dtr in dt2.Rows)
                                {
                                    if (dtr["med_id"].ToString() == dt1.Rows[i]["medium_id"].ToString())
                                    {
                                        if (dtr["class_id"].ToString() == dt1.Rows[i]["class_id"].ToString())
                                        {
                                            Struct_Amount = Convert.ToInt32(dtr["Totfees"].ToString());
                                            totfees = Convert.ToInt32(dtr["Totfees"].ToString());
                                            med = dtr["med_id"].ToString();
                                            std_id = dtr["class_id"].ToString();
                                        }
                                    }
                                }
                            }

                           

                            bal = 0;refundable=0;
                            if ((Struct_Amount - Convert.ToInt32(dt1.Rows[i]["Amount"].ToString())) >=0)
                            {
                                bal=Struct_Amount - Convert.ToInt32(dt1.Rows[i]["Amount"].ToString());
                            }
                            else
                            {
                                string refund="";
                                refund =Convert.ToString(Struct_Amount - Convert.ToInt32(dt1.Rows[i]["Amount"].ToString()));
                                refundable = Convert.ToInt32(refund.Replace("-",""));
                            }

                            table = table + "<tr><td style='border:1px solid black ;text-align: center;'>" + rowcount++ + "</td><td style='border:1px solid black ;text-align: center;'>" + dt1.Rows[i]["student_id"].ToString() + "</td><td style='border:1px solid black ;text-align: center;'>" + dt1.Rows[i]["Roll_no"].ToString() + "</td><td style='border:1px solid black ;text-align: center;'>" + dt1.Rows[i]["gr_no"].ToString() + "</td><td style='border:1px solid black ;text-align: center;'>" + dt1.Rows[i]["division_name"].ToString() + "</td><td style='border:1px solid black ;text-align: center;'>" + dt1.Rows[i]["std_name"].ToString() + "</td><td style='border:1px solid black ;text-align: center;'>" + dt1.Rows[i]["medium"].ToString() + "</td><td style='border:1px solid black ;text-align: left;'>" + dt1.Rows[i]["Name"].ToString() + "</td><td style='border:1px solid black ;text-align: center;'>" + dt1.Rows[i]["gender"].ToString() + "</td><td style='border:1px solid black ;text-align: right;'>" + Struct_Amount + "</td><td style='border:1px solid black ;text-align: right;'>" + dt1.Rows[i]["Amount"].ToString() + "</td><td style='border:1px solid black ;text-align: right;'>" + bal + "</td><td style='border:1px solid black ;text-align: right;'>" + refundable + "</td></tr>";
                            Struct_Amount = 0;

                            if (i == dt1.Rows.Count -1)
                            {
                                table = table+ "</tbody></table>";
                            }

                        }

                        sb.Append(table);
                        tbl_report.Text = sb.ToString();

                        btn_excel.Visible = true;
                    }
                    else
                    {
                        sb.Append("");
                        tbl_report.Text = sb.ToString();
                        btn_excel.Visible = false;
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify(' Fees not paid ', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                    }
                }
            
            }
            else
            {


                string medium_id = "", class_id = "", div_id = "";
                string type = "feereport";
                string ayid = Session["acdyear"].ToString();
                if (ddl_medium.SelectedIndex > 0)
                {
                    medium_id = ddl_medium.SelectedValue;
                }
                else
                {
                    medium_id = "";
                }
                if (ddl_class.SelectedIndex > 0)
                {
                    class_id = ddl_class.SelectedValue;
                }
                else
                {
                    class_id = "";
                }
                if (ddl_division.SelectedIndex > 0)
                {
                    div_id = ddl_division.SelectedValue;
                }
                else
                {
                    div_id = "";
                }

                string urlalias = cls.urls();
                string url = @urlalias + "feereport/";
                //string url = @"http://localhost:9199/statisticalrpt/";
                fr.type = type.ToString();
                fr.med_id = medium_id;
                fr.class_id = class_id;
                fr.div_id = div_id;
                fr.ayid = ayid;
                string jsonString = JsonHelper.JsonSerializer<fee_rpt>(fr);

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
                    DataSet datewise = JsonConvert.DeserializeObject<DataSet>(result);

                    DataTable dt1 = datewise.Tables[0];
                    if (dt1.Rows.Count > 0)
                    {

                        string table = "";


                        table = "<table id='tbl_report' style='border:1px solid black ;text-align: center;'>";
                        table = table + "<thead style='border:1px solid black ;text-align: center;'><tr><th style='border:1px solid black ;text-align: center;'>Sr No</th><th style='border:1px solid black ;text-align: center;'>Student ID</th><th style='border:1px solid black ;text-align: center;'>Roll No.</th><th style='border:1px solid black ;text-align: center;'>GR No.</th><th style='border:1px solid black ;text-align: center;'>Division</th><th style='border:1px solid black ;text-align: center;'>Class</th><th style='border:1px solid black ;text-align: center;'>Medium</th><th style='border:1px solid black ;text-align: center;'>Name</th><th style='border:1px solid black ;text-align: center;'>Gender</th><th style='border:1px solid black ;text-align: center;'>Type</th><th style='border:1px solid black ;text-align: center;'>Duration Type</th><th style='border:1px solid black ;text-align: center;'>Particular Name</th><th style='border:1px solid black ;text-align: center;'>Total Fees</th><th style='border:1px solid black ;text-align: center;'>Paid Fees</th><th style='border:1px solid black ;text-align: center;'>Balance Fees</th><th style='border:1px solid black ;text-align: center;'>Installment</th><th style='border:1px solid black ;text-align: center;'>Payment Date</th><th style='border:1px solid black ;text-align: center;'>Remark</th></tr></thead>";
                        string dura_id = "", struct_id = "", currstud_id = ""; int rowstudcount = 0; bool durflg = true; int totalfees = 0, totalpaid = 0, totalbalance = 0, rowdura = 0, inst = 0, totrowfees = 0, totpaidfees = 0, totrowbal = 0, rowstruct = 0;
                        //Building the Data rows.
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            if (i == 0)
                            {
                                table = table + "<tbody>";
                            }
                            currstud_id = dt1.Rows[i]["student_id"].ToString();
                            dura_id = dt1.Rows[i]["duration_id"].ToString();
                            struct_id = dt1.Rows[i]["struct_id"].ToString();
                            if (dt1.Rows.Count > (i + 1))
                            {
                                if (currstud_id == dt1.Rows[i + 1]["student_id"].ToString())
                                {

                                    if (dura_id == dt1.Rows[i + 1]["duration_id"].ToString())
                                    {
                                        //for loop for row count


                                        if (durflg == true)
                                        {
                                            durflg = false;
                                            inst = 1;
                                            for (int k = 0; k < dt1.Rows.Count; k++)
                                            {
                                                if (currstud_id == dt1.Rows[k]["student_id"].ToString())
                                                {
                                                    if (dura_id == dt1.Rows[k]["duration_id"].ToString())
                                                    {
                                                        if (dt1.Rows.Count > (k + 1))
                                                        {
                                                            if (struct_id == dt1.Rows[k + 1]["struct_id"].ToString())
                                                            {
                                                                if (dura_id != dt1.Rows[k]["duration_id"].ToString())
                                                                {
                                                                    totrowfees = totrowfees + Convert.ToInt32(dt1.Rows[k]["struct_fees"].ToString());
                                                                    totalfees = totalfees + Convert.ToInt32(dt1.Rows[k]["struct_fees"].ToString());

                                                                }
                                                                //rowstruct = rowstruct + 1;
                                                            }
                                                            else
                                                            {

                                                                totrowfees = totrowfees + Convert.ToInt32(dt1.Rows[k]["struct_fees"].ToString());
                                                                totalfees = totalfees + Convert.ToInt32(dt1.Rows[k]["struct_fees"].ToString());

                                                            }
                                                            if (struct_id == dt1.Rows[k]["struct_id"].ToString())
                                                            {
                                                                if (rowstruct == 0)
                                                                {
                                                                    rowstruct = 1;
                                                                }
                                                                else
                                                                {

                                                                    rowstruct = rowstruct + 1;
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (struct_id == dt1.Rows[k]["struct_id"].ToString())
                                                            {
                                                                if (rowstruct == 0)
                                                                {
                                                                    rowstruct = 1;
                                                                }
                                                                else
                                                                {

                                                                    rowstruct = rowstruct + 1;
                                                                }
                                                            }
                                                            totrowfees = totrowfees + Convert.ToInt32(dt1.Rows[k]["struct_fees"].ToString());
                                                            totalfees = totalfees + Convert.ToInt32(dt1.Rows[k]["struct_fees"].ToString());


                                                        }

                                                        if (rowdura == 0)
                                                        {
                                                            rowdura = 1;
                                                            totalpaid = totalpaid + Convert.ToInt32(dt1.Rows[k]["Amount"].ToString());
                                                            totpaidfees = totpaidfees + Convert.ToInt32(dt1.Rows[k]["Amount"].ToString());
                                                        }
                                                        else
                                                        {
                                                            rowdura = rowdura + 1;
                                                            totalpaid = totalpaid + Convert.ToInt32(dt1.Rows[k]["Amount"].ToString());
                                                            totpaidfees = totpaidfees + Convert.ToInt32(dt1.Rows[k]["Amount"].ToString());
                                                        }



                                                    }
                                                }
                                            }
                                            rowstudcount = rowstudcount + 1;
                                            totalbalance = totalfees - totalpaid;
                                            totrowbal = totrowfees - totpaidfees;
                                            table = table + "<tr><td style='border:1px solid black ;text-align: center;'  rowspan=" + rowdura + ">" + rowstudcount + "</td><td style='border:1px solid black ;text-align: center;'  rowspan=" + rowdura + " >" + dt1.Rows[i]["student_id"].ToString() + "</td><td style='border:1px solid black ;text-align: center;'  rowspan=" + rowdura + ">" + dt1.Rows[i]["Roll_no"].ToString() + "</td><td style='border:1px solid black ;text-align: center;'  rowspan=" + rowdura + ">" + dt1.Rows[i]["gr_no"].ToString() + "</td><td style='border:1px solid black ;text-align: center;'  rowspan=" + rowdura + ">" + dt1.Rows[i]["division_name"].ToString() + "</td><td style='border:1px solid black ;text-align: center;'  rowspan=" + rowdura + ">" + dt1.Rows[i]["std_name"].ToString() + "</td><td style='border:1px solid black ;text-align: center;'  rowspan=" + rowdura + ">" + dt1.Rows[i]["medium"].ToString() + "</td><td style='border:1px solid black ;text-align: left;'  rowspan=" + rowdura + ">" + dt1.Rows[i]["Name"].ToString() + "</td><td style='border:1px solid black ;text-align: center;'  rowspan=" + rowdura + ">" + dt1.Rows[i]["gender"].ToString() + "</td><td style='border:1px solid black ;text-align: center;'  rowspan=" + rowdura + ">" + dt1.Rows[i]["type_name"].ToString() + "</td><td style='border:1px solid black ;text-align: center;'  rowspan=" + rowdura + ">" + dt1.Rows[i]["type_description"].ToString() + "</td><td style='border:1px solid black ;text-align: center;'  rowspan=" + rowstruct + ">" + dt1.Rows[i]["struct_name"].ToString() + "</td><td style='border:1px solid black ;text-align: right;'  >" + dt1.Rows[i]["struct_fees"].ToString() + "</td><td style='border:1px solid black ;text-align: right;'  >" + dt1.Rows[i]["Amount"].ToString() + "</td><td style='border:1px solid black ;text-align: right;'  rowspan=" + rowdura + ">" + totrowbal + "</td><td style='border:1px solid black ;text-align: center;'  >" + inst++ + "</td><td style='border:1px solid black ;text-align: center;'  >" + dt1.Rows[i]["Pay_date"].ToString() + "</td><td style='border:1px solid black ;text-align: center;'  >" + dt1.Rows[i]["Remark"].ToString() + "</td></tr>";
                                            rowdura = 0; totrowfees = 0; totrowbal = 0; totpaidfees = 0;

                                        }
                                        else
                                        {
                                            if (rowstruct == 1)
                                            {
                                                rowstruct = 0;
                                                for (int k = 0; k < dt1.Rows.Count; k++)
                                                {
                                                    if (currstud_id == dt1.Rows[k]["student_id"].ToString())
                                                    {
                                                        if (dura_id == dt1.Rows[k]["duration_id"].ToString())
                                                        {
                                                            if (struct_id == dt1.Rows[k]["struct_id"].ToString())
                                                            {
                                                                if (rowstruct == 0)
                                                                {
                                                                    rowstruct = 1;
                                                                }
                                                                else
                                                                {

                                                                    rowstruct = rowstruct + 1;
                                                                }
                                                            }
                                                        }

                                                    }

                                                }



                                                table = table + "<tr><td style='border:1px solid black ;text-align: center;'  rowspan=" + rowstruct + ">" + dt1.Rows[i]["struct_name"].ToString() + "</td><td style='border:1px solid black ;text-align: right;'  >" + dt1.Rows[i]["struct_fees"].ToString() + "</td><td style='border:1px solid black ;text-align: right;' >" + dt1.Rows[i]["Amount"].ToString() + "</td><td style='border:1px solid black ;text-align: center;'  >" + inst++ + "</td><td style='border:1px solid black ;text-align: center;'  >" + dt1.Rows[i]["Pay_date"].ToString() + "</td><td style='border:1px solid black ;text-align: center;'  >" + dt1.Rows[i]["Remark"].ToString() + "</td></tr>";

                                            }
                                            else
                                            {

                                                table = table + "<tr><td style='border:1px solid black ;text-align: right;'  >" + dt1.Rows[i]["struct_fees"].ToString() + "</td><td style='border:1px solid black ;text-align: right;' >" + dt1.Rows[i]["Amount"].ToString() + "</td><td style='border:1px solid black ;text-align: center;'  >" + inst++ + "</td><td style='border:1px solid black ;text-align: center;'  >" + dt1.Rows[i]["Pay_date"].ToString() + "</td><td style='border:1px solid black ;text-align: center;'  >" + dt1.Rows[i]["Remark"].ToString() + "</td></tr>";
                                                rowstruct--;
                                            }

                                        }
                                    }
                                    else
                                    {
                                        if (dura_id != dt1.Rows[i - 1]["duration_id"].ToString())
                                        {

                                            durflg = true;
                                            inst = 1;
                                            for (int k = 0; k < dt1.Rows.Count; k++)
                                            {
                                                if (currstud_id == dt1.Rows[k]["student_id"].ToString())
                                                {
                                                    if (dura_id == dt1.Rows[k]["duration_id"].ToString())
                                                    {
                                                        if (dt1.Rows.Count > (k + 1))
                                                        {
                                                            if (struct_id == dt1.Rows[k + 1]["struct_id"].ToString())
                                                            {
                                                                if (dura_id != dt1.Rows[k]["duration_id"].ToString())
                                                                {
                                                                    totrowfees = totrowfees + Convert.ToInt32(dt1.Rows[k]["struct_fees"].ToString());
                                                                    totalfees = totalfees + Convert.ToInt32(dt1.Rows[k]["struct_fees"].ToString());

                                                                }
                                                                //rowstruct = rowstruct + 1;

                                                            }
                                                            else
                                                            {

                                                                totrowfees = totrowfees + Convert.ToInt32(dt1.Rows[k]["struct_fees"].ToString());
                                                                totalfees = totalfees + Convert.ToInt32(dt1.Rows[k]["struct_fees"].ToString());

                                                            }
                                                            if (struct_id == dt1.Rows[k]["struct_id"].ToString())
                                                            {
                                                                if (rowstruct == 0)
                                                                {
                                                                    rowstruct = 1;
                                                                }
                                                                else
                                                                {

                                                                    rowstruct = rowstruct + 1;
                                                                }
                                                            }

                                                        }
                                                        else
                                                        {
                                                            if (struct_id == dt1.Rows[k]["struct_id"].ToString())
                                                            {
                                                                if (rowstruct == 0)
                                                                {
                                                                    rowstruct = 1;
                                                                }
                                                                else
                                                                {

                                                                    rowstruct = rowstruct + 1;
                                                                } rowstruct = rowstruct + 1;

                                                            }
                                                            totrowfees = totrowfees + Convert.ToInt32(dt1.Rows[k]["struct_fees"].ToString());
                                                            totalfees = totalfees + Convert.ToInt32(dt1.Rows[k]["struct_fees"].ToString());


                                                        }

                                                        if (rowdura == 0)
                                                        {
                                                            rowdura = 1;
                                                            totalpaid = totalpaid + Convert.ToInt32(dt1.Rows[k]["Amount"].ToString());
                                                            totpaidfees = totpaidfees + Convert.ToInt32(dt1.Rows[k]["Amount"].ToString());
                                                        }
                                                        else
                                                        {
                                                            rowdura = rowdura + 1;
                                                            totalpaid = totalpaid + Convert.ToInt32(dt1.Rows[k]["Amount"].ToString());
                                                            totpaidfees = totpaidfees + Convert.ToInt32(dt1.Rows[k]["Amount"].ToString());
                                                        }


                                                    }
                                                }

                                            }

                                            rowstudcount = rowstudcount + 1;
                                            totalbalance = totalfees - totalpaid;
                                            totrowbal = totrowfees - totpaidfees;
                                            table = table + "<tr><td style='border:1px solid black ;text-align: center;'  rowspan=" + rowdura + ">" + rowstudcount + "</td><td style='border:1px solid black ;text-align: center;'  rowspan=" + rowdura + " >" + dt1.Rows[i]["student_id"].ToString() + "</td><td style='border:1px solid black ;text-align: center;'  rowspan=" + rowdura + ">" + dt1.Rows[i]["Roll_no"].ToString() + "</td><td style='border:1px solid black ;text-align: center;'  rowspan=" + rowdura + ">" + dt1.Rows[i]["gr_no"].ToString() + "</td><td style='border:1px solid black ;text-align: center;'  rowspan=" + rowdura + ">" + dt1.Rows[i]["division_name"].ToString() + "</td><td style='border:1px solid black ;text-align: center;'  rowspan=" + rowdura + ">" + dt1.Rows[i]["std_name"].ToString() + "</td><td style='border:1px solid black ;text-align: center;'  rowspan=" + rowdura + ">" + dt1.Rows[i]["medium"].ToString() + "</td><td style='border:1px solid black ;text-align: left;'  rowspan=" + rowdura + ">" + dt1.Rows[i]["Name"].ToString() + "</td><td style='border:1px solid black ;text-align: center;'  rowspan=" + rowdura + ">" + dt1.Rows[i]["gender"].ToString() + "</td><td style='border:1px solid black ;text-align: center;'  rowspan=" + rowdura + ">" + dt1.Rows[i]["type_name"].ToString() + "</td><td style='border:1px solid black ;text-align: center;'  rowspan=" + rowdura + ">" + dt1.Rows[i]["type_description"].ToString() + "</td><td style='border:1px solid black ;text-align: center;'  >" + dt1.Rows[i]["struct_name"].ToString() + "</td><td style='border:1px solid black ;text-align: right;'  >" + dt1.Rows[i]["struct_fees"].ToString() + "</td><td style='border:1px solid black ;text-align: right;'  >" + dt1.Rows[i]["Amount"].ToString() + "</td><td style='border:1px solid black ;text-align: right;'  rowspan=" + rowdura + ">" + totrowbal + "</td><td style='border:1px solid black ;text-align: center;'  >" + inst++ + "</td><td style='border:1px solid black ;text-align: center;'  >" + dt1.Rows[i]["Pay_date"].ToString() + "</td><td style='border:1px solid black ;text-align: center;'  >" + dt1.Rows[i]["Remark"].ToString() + "</td></tr>";
                                            rowdura = 0; totrowfees = 0; totrowbal = 0; totpaidfees = 0; rowstruct = 0;




                                        }
                                        else
                                        {
                                            if (rowstruct == 1)
                                            {
                                                rowstruct = 0;
                                                for (int k = 0; k < dt1.Rows.Count; k++)
                                                {
                                                    if (currstud_id == dt1.Rows[k]["student_id"].ToString())
                                                    {
                                                        if (dura_id == dt1.Rows[k]["duration_id"].ToString())
                                                        {
                                                            if (struct_id == dt1.Rows[k]["struct_id"].ToString())
                                                            {
                                                                if (rowstruct == 0)
                                                                {
                                                                    rowstruct = 1;
                                                                }
                                                                else
                                                                {

                                                                    rowstruct = rowstruct + 1;
                                                                }
                                                            }
                                                        }

                                                    }

                                                }



                                                table = table + "<tr><td style='border:1px solid black ;text-align: center;'  rowspan=" + rowstruct + ">" + dt1.Rows[i]["struct_name"].ToString() + "</td><td style='border:1px solid black ;text-align: right;'  >" + dt1.Rows[i]["struct_fees"].ToString() + "</td><td style='border:1px solid black ;text-align: right;' >" + dt1.Rows[i]["Amount"].ToString() + "</td><td style='border:1px solid black ;text-align: center;'  >" + inst++ + "</td><td style='border:1px solid black ;text-align: center;'  >" + dt1.Rows[i]["Pay_date"].ToString() + "</td><td style='border:1px solid black ;text-align: center;'  >" + dt1.Rows[i]["Remark"].ToString() + "</td></tr>";
                                                rowstruct = 0;
                                            }
                                            else
                                            {

                                                table = table + "<tr><td style='border:1px solid black ;text-align: right;'  >" + dt1.Rows[i]["struct_fees"].ToString() + "</td><td style='border:1px solid black ;text-align: right;' >" + dt1.Rows[i]["Amount"].ToString() + "</td><td style='border:1px solid black ;text-align: center;'  >" + inst++ + "</td><td style='border:1px solid black ;text-align: center;'  >" + dt1.Rows[i]["Pay_date"].ToString() + "</td><td style='border:1px solid black ;text-align: center;'  >" + dt1.Rows[i]["Remark"].ToString() + "</td></tr>";
                                                rowstruct--;
                                            }


                                            durflg = true;
                                        }

                                    }

                                }
                                else
                                {
                                    if (rowstruct == 1)
                                    {
                                        rowstruct = 0;
                                        for (int k = 0; k < dt1.Rows.Count; k++)
                                        {
                                            if (currstud_id == dt1.Rows[k]["student_id"].ToString())
                                            {
                                                if (dura_id == dt1.Rows[k]["duration_id"].ToString())
                                                {
                                                    if (struct_id == dt1.Rows[k]["struct_id"].ToString())
                                                    {
                                                        if (rowstruct == 0)
                                                        {
                                                            rowstruct = 1;
                                                        }
                                                        else
                                                        {

                                                            rowstruct = rowstruct + 1;
                                                        }
                                                    }
                                                }

                                            }

                                        }



                                        table = table + "<tr><td style='border:1px solid black ;text-align: center;'  rowspan=" + rowstruct + ">" + dt1.Rows[i]["struct_name"].ToString() + "</td><td style='border:1px solid black ;text-align: right;'  >" + dt1.Rows[i]["struct_fees"].ToString() + "</td><td style='border:1px solid black ;text-align: right;' >" + dt1.Rows[i]["Amount"].ToString() + "</td><td style='border:1px solid black ;text-align: center;'  >" + inst++ + "</td><td style='border:1px solid black ;text-align: center;'  >" + dt1.Rows[i]["Pay_date"].ToString() + "</td><td style='border:1px solid black ;text-align: center;'  >" + dt1.Rows[i]["Remark"].ToString() + "</td></tr>";
                                        rowstruct = 0;
                                    }
                                    else
                                    {

                                        table = table + "<tr><td style='border:1px solid black ;text-align: right;'  >" + dt1.Rows[i]["struct_fees"].ToString() + "</td><td style='border:1px solid black ;text-align: right;' >" + dt1.Rows[i]["Amount"].ToString() + "</td><td style='border:1px solid black ;text-align: center;'  >" + inst++ + "</td><td style='border:1px solid black ;text-align: center;'  >" + dt1.Rows[i]["Pay_date"].ToString() + "</td><td style='border:1px solid black ;text-align: center;'  >" + dt1.Rows[i]["Remark"].ToString() + "</td></tr>";
                                        rowstruct--;
                                    }



                                    totalbalance = totalfees - totalpaid;
                                    totrowbal = totrowfees - totpaidfees;
                                    table = table + "<tr><td style='border:1px solid black ;text-align: center;'  colspan='11' ></td><td style='border:1px solid black ;text-align: center;'  >Total Fees</td><td style='border:1px solid black ;text-align: right;'  >" + totalfees + "</td><td style='border:1px solid black ;text-align: right;'  >" + totalpaid + "</td><td style='border:1px solid black ;text-align: right;'  >" + totalbalance + "</td><td style='border:1px solid black ;text-align: center;'  colspan='3' ></td></tr>";
                                    totalfees = 0; totalpaid = 0; totalbalance = 0;
                                    durflg = true;
                                }
                            }
                            else
                            {
                                if (durflg == true)
                                {
                                    durflg = false;
                                    inst = 1;
                                    for (int k = 0; k < dt1.Rows.Count; k++)
                                    {
                                        if (currstud_id == dt1.Rows[k]["student_id"].ToString())
                                        {
                                            if (dura_id == dt1.Rows[k]["duration_id"].ToString())
                                            {
                                                if (dt1.Rows.Count > (k + 1))
                                                {
                                                    if (struct_id == dt1.Rows[k + 1]["struct_id"].ToString())
                                                    {
                                                        if (dura_id != dt1.Rows[k]["duration_id"].ToString())
                                                        {
                                                            totrowfees = totrowfees + Convert.ToInt32(dt1.Rows[k]["struct_fees"].ToString());
                                                            totalfees = totalfees + Convert.ToInt32(dt1.Rows[k]["struct_fees"].ToString());

                                                        }


                                                    }
                                                    else
                                                    {

                                                        totrowfees = totrowfees + Convert.ToInt32(dt1.Rows[k]["struct_fees"].ToString());
                                                        totalfees = totalfees + Convert.ToInt32(dt1.Rows[k]["struct_fees"].ToString());

                                                    }
                                                    if (struct_id == dt1.Rows[k]["struct_id"].ToString())
                                                    {
                                                        if (rowstruct == 0)
                                                        {
                                                            rowstruct = 1;
                                                        }
                                                        else
                                                        {

                                                            rowstruct = rowstruct + 1;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (struct_id == dt1.Rows[k]["struct_id"].ToString())
                                                    {
                                                        if (rowstruct == 0)
                                                        {
                                                            rowstruct = 1;
                                                        }
                                                        else
                                                        {

                                                            rowstruct = rowstruct + 1;
                                                        }
                                                    }

                                                    totrowfees = totrowfees + Convert.ToInt32(dt1.Rows[k]["struct_fees"].ToString());
                                                    totalfees = totalfees + Convert.ToInt32(dt1.Rows[k]["struct_fees"].ToString());


                                                }

                                                if (rowdura == 0)
                                                {
                                                    rowdura = 1;
                                                    totalpaid = totalpaid + Convert.ToInt32(dt1.Rows[k]["Amount"].ToString());
                                                    totpaidfees = totpaidfees + Convert.ToInt32(dt1.Rows[k]["Amount"].ToString());
                                                }
                                                else
                                                {
                                                    rowdura = rowdura + 1;
                                                    totalpaid = totalpaid + Convert.ToInt32(dt1.Rows[k]["Amount"].ToString());
                                                    totpaidfees = totpaidfees + Convert.ToInt32(dt1.Rows[k]["Amount"].ToString());
                                                }


                                            }
                                        }

                                    }

                                    rowstudcount = rowstudcount + 1;
                                    totalbalance = totalfees - totalpaid;
                                    totrowbal = totrowfees - totpaidfees;
                                    table = table + "<tr><td style='border:1px solid black ;text-align: center;'  rowspan=" + rowdura + ">" + rowstudcount + "</td><td style='border:1px solid black ;text-align: center;'  rowspan=" + rowdura + " >" + dt1.Rows[i]["student_id"].ToString() + "</td><td style='border:1px solid black ;text-align: center;'  rowspan=" + rowdura + ">" + dt1.Rows[i]["Roll_no"].ToString() + "</td><td style='border:1px solid black ;text-align: center;'  rowspan=" + rowdura + ">" + dt1.Rows[i]["gr_no"].ToString() + "</td><td style='border:1px solid black ;text-align: center;'  rowspan=" + rowdura + ">" + dt1.Rows[i]["division_name"].ToString() + "</td><td style='border:1px solid black ;text-align: center;'  rowspan=" + rowdura + ">" + dt1.Rows[i]["std_name"].ToString() + "</td><td style='border:1px solid black ;text-align: center;'  rowspan=" + rowdura + ">" + dt1.Rows[i]["medium"].ToString() + "</td><td style='border:1px solid black ;text-align: left;'  rowspan=" + rowdura + ">" + dt1.Rows[i]["Name"].ToString() + "</td><td style='border:1px solid black ;text-align: center;'  rowspan=" + rowdura + ">" + dt1.Rows[i]["gender"].ToString() + "</td><td style='border:1px solid black ;text-align: center;'  rowspan=" + rowdura + ">" + dt1.Rows[i]["type_name"].ToString() + "</td><td style='border:1px solid black ;text-align: center;'  rowspan=" + rowdura + ">" + dt1.Rows[i]["type_description"].ToString() + "</td><td style='border:1px solid black ;text-align: center;'  rowspan=" + rowstruct + "  >" + dt1.Rows[i]["struct_name"].ToString() + "</td><td style='border:1px solid black ;text-align: right;'   >" + dt1.Rows[i]["struct_fees"].ToString() + "</td><td style='border:1px solid black ;text-align: right;'  >" + dt1.Rows[i]["Amount"].ToString() + "</td><td style='border:1px solid black ;text-align: right;'  rowspan=" + rowdura + ">" + totrowbal + "</td><td style='border:1px solid black ;text-align: center;'  >" + inst++ + "</td><td style='border:1px solid black ;text-align: center;'  >" + dt1.Rows[i]["Pay_date"].ToString() + "</td><td style='border:1px solid black ;text-align: center;'  >" + dt1.Rows[i]["Remark"].ToString() + "</td></tr>";
                                    rowdura = 0; totrowfees = 0; totrowbal = 0; totpaidfees = 0; rowstruct = 0;


                                    table = table + "<tr><td style='border:1px solid black ;text-align: center;'  colspan='10' ></td><td style='border:1px solid black ;text-align: center;'  >Total Fees</td><td style='border:1px solid black ;text-align: right;'  >" + totalfees + "</td><td style='border:1px solid black ;text-align: right;'  >" + totalpaid + "</td><td style='border:1px solid black ;text-align: right;'  >" + totalbalance + "</td><td style='border:1px solid black ;text-align: center;'  ></td></tr>";



                                }
                                else
                                {
                                    table = table + "<tr><td style='border:1px solid black ;text-align: center;'  >" + dt1.Rows[i]["struct_name"].ToString() + "</td><td style='border:1px solid black ;text-align: right;' >" + dt1.Rows[i]["struct_fees"].ToString() + "</td><td style='border:1px solid black ;text-align: right;' >" + dt1.Rows[i]["Amount"].ToString() + "</td><td style='border:1px solid black ;text-align: center;'  >" + inst++ + "</td><td style='border:1px solid black ;text-align: center;'  >" + dt1.Rows[i]["Pay_date"].ToString() + "</td><td style='border:1px solid black ;text-align: center;'  >" + dt1.Rows[i]["Remark"].ToString() + "</td></tr>";
                                    totalbalance = totalfees - totalpaid;
                                    table = table + "<tr><td style='border:1px solid black ;text-align: center;'  colspan='11' ></td><td style='border:1px solid black ;text-align: center;'  >Total Fees</td><td style='border:1px solid black ;text-align: right;'  >" + totalfees + "</td><td style='border:1px solid black ;text-align: right;'  >" + totalpaid + "</td><td style='border:1px solid black ;text-align: right;'  >" + totalbalance + "</td><td style='border:1px solid black ;text-align: center;'  colspan='3' ></td></tr>";
                                    totalfees = 0; totalpaid = 0; totalbalance = 0;
                                    durflg = true;
                                }
                            }


                        }
                        table = table + "</tbody></table>";

                        sb.Append(table);

                        tbl_report.Text = Convert.ToString(sb);


                        btn_excel.Visible = true;

                    }
                    else
                    {
                        sb.Append("");

                        tbl_report.Text = Convert.ToString(sb);


                        btn_excel.Visible = false;
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify(' Fees not paid ', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);

                    }
                }
            }
        }
        catch (Exception ex)
        {
            ermsg.Text = ex.ToString();
        }
    }
    protected void btn_clear_Click(object sender, EventArgs e)
    {
        sb.Append("");
        tbl_report.Text = sb.ToString();
        btn_excel.Visible = false;
        rdotype.SelectedIndex = 0;
        ddl_medium.SelectedIndex = 0;
        ddl_class.DataSource = "";
        ddl_class.DataBind();
        ddl_division.DataSource = "";
        ddl_division.DataBind();
    }




    protected void rdotype_SelectedIndexChanged(object sender, EventArgs e)
    {
        sb.Append("");
        tbl_report.Text = sb.ToString();
        btn_excel.Visible = false;
    }
}