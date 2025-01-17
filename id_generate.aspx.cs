using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class id_generate : System.Web.UI.Page
{
    id_gen id = new id_gen();
    common cm=new common();
    DataSet ds = new DataSet();
    Class1 cls = new Class1();
    protected void Page_Load(object sender, EventArgs e)
    {
        try{
            if (Convert.ToString(Session["emp_id"]) == "")
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    rr.Visible = false;
                    department();

                    medium_select();
                    hh.Visible = false;
                    hh1.Visible = false;

                }
            }
        }
        catch(Exception ex) {
            Response.Redirect("Login.aspx");
        }
        
    }

    protected void ddl_SelectedIndexChanged(object sender, EventArgs e)
    {
      
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
    public void department() {
        try
        {
            DataSet ds = new DataSet();
            id.type = "employee";


            string urlalias = cls.urls();
            string url = @urlalias + "id_gen/";

            // string url = "http://localhost:9199/id_gen/";
            string jsonString = JsonHelper.JsonSerializer<id_gen>(id);
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
                Session["DSLIST"] = ds;
                DataTable dt1 = ds.Tables[0];
                ddl_subcrs1.DataSource = dt1;
                ddl_subcrs1.DataTextField = "Department_name";
                ddl_subcrs1.DataValueField = "dept_id";
                ddl_subcrs1.DataBind();
                ddl_subcrs1.Items.Insert(0, "--Select--");
                ddl_subcrs1.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }

    }

    private void medium_select()
    {
        try
        {
            string type = "ddlfill";


            string urlalias = cls.urls();
            string url = @urlalias + "Common/";
            //string url = @"http://203.192.254.34/Utkarsha_api1/utkarsha_api/Common/";
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
                ddl_subcrs.DataSource = dt1;
                ddl_subcrs.DataTextField = "medium";
                ddl_subcrs.DataValueField = "med_id";
                ddl_subcrs.DataBind();
                ddl_subcrs.Items.Insert(0, "--Select--");
                ddl_subcrs.SelectedIndex = 0;


            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void ddlmedium_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddl_subcrs.SelectedItem.Text != "--Select--")
            {
                DataSet ds = (DataSet)Session["DSLIST"];

                foreach (DataTable table in ds.Tables)
                {
                    if (table.TableName.Equals(ddl_subcrs.SelectedValue.ToString()))
                    {
                        DropDownList1.DataSource = table;
                        DropDownList1.DataTextField = "std_name";
                        DropDownList1.DataValueField = "std_id";
                        DropDownList1.DataBind();
                        DropDownList1.Enabled = true;
                        DropDownList1.Items.Insert(0, "--SELECT--");
                        DropDownList1.SelectedIndex = 0;
                        Session["tab"] = table;
                    }
                }

            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
                DataSet ds = new DataSet();
            string str_schol_na = "";
            string pri = "";
            if ((ddl_subcrs.SelectedIndex != 0 && DropDownList1.SelectedIndex != 0) || txt_stud.Text != "")
            {
                DataTable tt = new DataTable();
                tt.Columns.Add("Stud_id");
                tt.Columns.Add("Name");
                tt.Columns.Add("roll_no");
                tt.Columns.Add("ayid");
                tt.Columns.Add("Emp_id");

                string str = "";
                if (txt_stud.Text != "")
                {
                    string stud_id = txt_stud.Text;
                    id.classid = stud_id.Replace(",", "','");
                    id.type = "duplicate1";

                    string urlalias = cls.urls();
                    string url = @urlalias + "id_gen/";

                  //  string url = "http://localhost:9199/id_gen/";
                    string jsonString = JsonHelper.JsonSerializer<id_gen>(id);
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


                    }
                 
                }
                else
                {
                    id.type = "select";
                    id.classid = DropDownList1.SelectedValue;
                    id.mediumid = ddl_subcrs.SelectedValue;


                    id.ayid = Session["acdyear"].ToString();
                    //id.ayid=Session["ddlyesr"]

                    string urlalias = cls.urls();
                    string url = @urlalias + "id_gen/";

                   // string url = "http://localhost:9199/id_gen/";
                    string jsonString = JsonHelper.JsonSerializer<id_gen>(id);
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
                    }
                }

             
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    path = Server.MapPath("ID_card");
                    //StreamWriter table12 = new StreamWriter("E:\\website\\staff\\ID_card_html_files\\" + ddl_subcrs.SelectedItem.Text.ToString() + "_ID_card.html");
                    StreamWriter table12 = new StreamWriter(path + "\\ID_card.html");

                    int k = 0;
                    StringBuilder sb = new StringBuilder();
                    table12.WriteLine("<head><style>"
                           + "@media print {"
                                        + "@page {"
                                                + "margin-top: 0.5cm;"
                                                + "margin-bottom: 0.5cm;"
                                              + "}"
                                         + "}"
                          + "</style></head>");
                    table12.WriteLine("<body style='margin:0px'>");
                    table12.WriteLine("<center>");
                    string va = "";

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                                //if (ds.Tables[0].Rows[i]["student_photo"].ToString() != "" && ds.Tables[0].Rows[i]["emp_photo"].ToString() != "null" && ds.Tables[0].Rows[i]["emp_photo"].ToString() != null)
                                //{
                                //    if (ds.Tables[0].Rows[i]["emp_sign"].ToString() != "" && ds.Tables[0].Rows[i]["emp_sign"].ToString() != "null" && ds.Tables[0].Rows[i]["emp_sign"].ToString() != null)
                                //    {
                        tt.Rows.Add(ds.Tables[0].Rows[i]["student_id"].ToString(), ds.Tables[0].Rows[i]["name"].ToString(), ds.Tables[0].Rows[i]["roll_no"].ToString(), Session["acdyear"].ToString(), Session["Emp_id"].ToString());
                        if (ds.Tables[0].Rows[i]["class_id"].ToString() == "7" || ds.Tables[0].Rows[i]["class_id"].ToString() == "8" || ds.Tables[0].Rows[i]["class_id"].ToString() == "11" || ds.Tables[0].Rows[i]["class_id"].ToString() == "12" || ds.Tables[0].Rows[i]["class_id"].ToString() == "9" || ds.Tables[0].Rows[i]["class_id"].ToString() == "10") { va = "green"; }
                        else { va = "lightgreen"; }
                        if (ddl_subcrs.SelectedItem.Text.StartsWith("M"))
                        {
                            str_schol_na = "Late Shri. Vishnu Waman Thakur Charitable Trust's <br/><b>Kai. P.R. Patil Utkarsha Madhyamik Vidyalaya & <br/>Junior College, Virar(W.) </b><br/><span >VIVA College (Old Building),</span><br/><span >VIVA College Road, Virar (West),</span><br/><span > Mob: 7498311472</span></p>";
                            pri = "http://203.192.254.35/staff/images/jr_sign.gif";
                        }
                        else
                        {
                            str_schol_na = "Late Shri. Vishnu Waman Thakur Charitable Trust's <br/><b>UTHKARSHA VIDYALAYA, VIRAR </b><br/><span >VIVA College (Old Building),M.B. ESTATE,</span><br/><span >Ram Mandir Road Virar (W), </span><br/><span >Tal. - Vasai,Dist. - Palghar Pin -401303</span><br/><span >Tel. :02505999/2515276</span></p>";

                            pri = "http://203.192.254.35/staff/images/jr_sign_eng.png";
                        }


                        if (i < ds.Tables[0].Rows.Count)
                        {

                            string s_img;
                            string s_img1;
                            string bb = "";
                            string barCode = ds.Tables[0].Rows[i]["student_id"].ToString();
                            System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
                            using (Bitmap bitMap = new Bitmap(barCode.Length * 40, 80))
                            {
                                using (Graphics graphics = Graphics.FromImage(bitMap))
                                {
                                    System.Drawing.Font oFont = new System.Drawing.Font("IDAutomationHC39M", 16);
                                    PointF point = new PointF(2f, 2f);
                                    SolidBrush blackBrush = new SolidBrush(Color.Black);
                                    SolidBrush whiteBrush = new SolidBrush(Color.White);
                                    graphics.FillRectangle(whiteBrush, 0, 0, bitMap.Width, bitMap.Height);
                                    graphics.DrawString("*" + barCode + "*", oFont, blackBrush, point);
                                }
                                using (MemoryStream ms = new MemoryStream())
                                {
                                    bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                                    byte[] byteImage = ms.ToArray();

                                    bb = Convert.ToBase64String(byteImage);

                                }
                            }

                            int l = 0;

                            l = 11;
                            if (k == 5)
                            {
                                k = 1;

                                table12.WriteLine("<table style='page-break-after:always;'></table>"
                                   + "<table style='border-spacing:0px;border-collapse: collapse;font-family:arial;'>"
                                    + "<tr><td style='width: 90mm; height: 57mm; border: 1px solid black;padding:0px;background-size:contain'>"
                                     + "<center><table><tr>"
                                     + "<td style='width: 86mm; height: 54mm;border-radius:12px;padding:0px;border:1px solid black;'>"
                                     + "<center><table><tr>"
                                     + "<td style='width:81mm;height:50mm;padding:0px;vertical-align: top;'>"
                                     + "<center style='height:19mm;margin: 0px;margin-right: -10px;margin-left: -9px;margin-top: -2px;border-top-right-radius: 11px;border-top-left-radius: 11px;background-color: " + va + ";'><table style='border-spacing:0px;width:100%'><tr><td>"
                                     + "<img src='http://www.utkarshavidyalaya.org/images/englishUtkarshaLogo.gif' class='img-rounded' style='margin-left:5px' alt='Cinque Terre' width='45' height='45' />"
                                     + "</td><td>"
                                     + "<p style='font-size: 10px;color:black;text-align: center;margin: 0px;'>"
                                    + str_schol_na
                                     + "</td></tr></table></center>"
                                     + "<center style='margin-top: 4px;'><table style='border-spacing:0px;'><tr><td>"
                                     + "<p style='font-size:9px;color:white;text-align:center;margin:0px;color:black;font-weight:bold'>"
                                     + "</p>"
                                     + "</td></tr></table></center>"
                                     + "<table style='width:100%;height: 27mm;'><tr>"

                                     + "<td>"

                                     + "<b style='font-size: 11px;font-family:arial'><div style='margin-bottom:5px'>Name: " + ds.Tables[0].Rows[i]["NAME"].ToString() + "</div>"
                                     + "<div style='margin-bottom:5px'>Stream " + (Convert.ToInt32(ds.Tables[0].Rows[i]["class_id"].ToString()) - 2) + " &nbsp&nbsp Div :   &nbsp &nbsp Roll No: "
                                     + "<div style='margin-bottom:5px'>GR No. :" + ds.Tables[0].Rows[i]["gr_no"].ToString() + "</div>"
                                     + "<div style='margin-bottom:5px'>D.O.B. : " + Convert.ToDateTime(ds.Tables[0].Rows[i]["DOB"]).Date.ToString("dd-MM-yyyy") + "</div>"
                                     + "</td>"

                                     + "<td style='float: right;margin-top: 0px;'>"

                                     + "<img   alt='' width='65' height='65' style='border: 1px solid black;' src='data:image/png;base64," + ds.Tables[0].Rows[i]["student_photo"] + "'>"
                                     + "<br>"


                                     + "</td>"

                                     + "</tr></table>"


                                     + "<img src='" + pri + "' alt='' width='80' height='30' style='float:right;margin-top: -25px;;margin-right: 4px;'>"
                                     + "<table style='width:100%;height:4mm;text-align:center;margin-top: 5px;'><tr><td>"
                                     + "<label style='color:black;font-size:11px;font-weight:bold' id='lblstud_id_" + i + "' >" + ds.Tables[0].Rows[i]["student_id"].ToString() + "</label>"
                                     + "</td>"
                                     + "<td>"
                                     + "<b style='color:black;font-size:11px;float:right;margin-left: 8px;'></b>"
                                     + "</td>"
                                     + "<td>"
                                     + "<b style='color:black;font-size:11px;float:right;'>Principal Sign</b>"
                                     + "</td>"
                                     + "</tr></table>"


                                     + "</td></tr></table></center></td></tr></table></center></td>"

                                     + "<td style='width: 90mm; height: 57mm; border: 1px solid black;padding:0px;'>"
                                     + "<center><table><tr>"
                                     + "<td style='width: 86mm; height: 54mm;border-radius:12px;padding:0px'>"
                                     + "<center style='margin-left: 5px;'><table><tr>"
                                     + "<td style='width:81mm;height:50mm;padding:0px'>"
                                     + "<table><tr style='font-size:10px;'><td style='    width: 17%;'>"
                                        + "<b>Address :</b></td><td>" + ds.Tables[0].Rows[i]["address"].ToString() + "</td>"
                                        + "</td></tr></table>"
                                        + "<table><tr><td>"
                                        + "<p style='font-size:10px;'><b>Phone No:</b>" + ds.Tables[0].Rows[i]["phone_no2"].ToString() + ""
                                        + "</td></tr></table>"
                                          + "<table><tr><td>"
                                        + "<p style='font-size:10px;'><b>Aadhar Card No:</b>" + ds.Tables[0].Rows[i]["aadhar_no"].ToString() + ""
                                        + "</td></tr></table>"
                                     + "<table><tr><td>"
                                     + "<b style='font-size:12px'>Instructions :</b>"
                                     + "</td></tr></table>"
                                     + "<table><tr><td>"
                                     + "<b style='font-size:8px'>1) This card is not transferable and must be produced whenever demanded.</b>"
                                     + "</td></tr></table>"
                                     + "<table><tr><td>"
                                     + "<b style='font-size:8px'>2) In the event of its loss the holder of the card must intimate to the Principal <span style='margin-left:9px'>immediately in writing.</span></b>"
                                     + "</td></tr></table>"
                                     + "<table><tr><td>"
                                     + "<b style='font-size:8px'>3) Any one who finds this card is requested to send it to the Principal.</b>"
                                     + "</td></tr></table>"
                                     + "<table><tr>"
                                     + "<td></td>"
                                     + "<td>"
                                     + "<center><img src='data:image/png;base64," + bb + "' alt='' width='245' height='35' style='margin-left: 50px;'></center>"
                                     + "</td>"
                                     + "<td></td>"
                                     + "</tr></table>"
                                     + "</td></tr></table></center></td></tr></table></center></td>"

                                    );




                            }
                            else
                            {
                                //string o = ds.Tables[0].Rows[i]["NAME"].ToString();
                                string u = ds.Tables[0].Rows[i]["class_id"].ToString();
                                //string h=ds.Tables[0].Rows[i]["class_id"].ToString();
                                table12.WriteLine(
                                    "<table style='border-spacing:0px;border-collapse: collapse;font-family:arial;'>"
                                    + "<tr><td style='width: 90mm; height: 57mm; border: 1px solid black;padding:0px;background-size:contain'>"
                                     + "<center><table><tr>"
                                     + "<td style='width: 86mm; height: 54mm;border-radius:12px;padding:0px;border:1px solid black;'>"
                                     + "<center><table><tr>"
                                     + "<td style='width:81mm;height:50mm;padding:0px;vertical-align: top;'>"
                                     + "<center style='height:19mm;margin: 0px;margin-right: -10px;margin-left: -9px;margin-top: -2px;border-top-right-radius: 11px;border-top-left-radius: 11px;background-color: " + va + ";'><table style='border-spacing:0px;width:100%'><tr><td>"
                                     + "<img src='http://www.utkarshavidyalaya.org/images/englishUtkarshaLogo.gif' class='img-rounded' style='margin-left:5px' alt='Cinque Terre' width='45' height='45' />"
                                     + "</td><td>"
                                     + "<p style='font-size: 10px;color:black;text-align: center;margin: 0px;'>"

                                    + str_schol_na


                                     + "</td></tr></table></center>"
                                     + "<center style='margin-top: 4px;'><table style='border-spacing:0px;'><tr><td>"
                                     + "<p style='font-size:9px;color:white;text-align:center;margin:0px;color:black;font-weight:bold'>"
                                     + "</p>"
                                     + "</td></tr></table></center>"
                                     + "<table style='width:100%;height: 27mm;'><tr>"

                                     + "<td>"

                                     + "<b style='font-size: 11px;font-family:arial'><div style='margin-bottom:5px'>Name: " + ds.Tables[0].Rows[i]["NAME"].ToString() + "</div>"
                                   + "<div style='margin-bottom:5px'>Stream " + ds.Tables[0].Rows[i]["class_id"].ToString() + " &nbsp&nbsp Div :   &nbsp &nbsp Roll No: "
                                     + "<div style='margin-bottom:5px'>GR No. :" + ds.Tables[0].Rows[i]["gr_no"].ToString() + "</div>"
                                     + "<div style='margin-bottom:5px'>D.O.B. : " + Convert.ToDateTime(ds.Tables[0].Rows[i]["DOB"]).Date.ToString("dd-MM-yyyy") + "</div>"
                                     + "</td>"

                                     + "<td style='float: right;margin-top: 0px;'>"

                                    // + "<img   alt='' width='65' height='65' style='border: 1px solid black;'>"//src='data:image/png;base64," + s_img + "'
                                     + "<img   alt='' width='65' height='65' style='border: 1px solid black;' src='data:image/png;base64," + ds.Tables[0].Rows[i]["student_photo"] + "'>"

                                     + "<br>"


                                     + "</td>"

                                     + "</tr></table>"


                                    + "<img src='" + pri + "' alt='' width='80' height='30' style='float:right;margin-top: -25px;;margin-right: 4px;'>"
                                     + "<table style='width:100%;height:4mm;text-align:center;margin-top: 5px;'><tr><td>"
                                     + "<label style='color:black;font-size:11px;font-weight:bold' id='lblstud_id_" + i + "' >" + ds.Tables[0].Rows[i]["student_id"].ToString() + "</label>"
                                     + "</td>"
                                     + "<td>"
                                     + "<b style='color:black;font-size:11px;float:right;margin-left: 8px;'></b>"
                                     + "</td>"
                                     + "<td>"
                                     + "<b style='color:black;font-size:11px;float:right;'>Principal Sign</b>"
                                     + "</td>"
                                     + "</tr></table>"



                                     + "</td></tr></table></center></td></tr></table></center></td>"

                                     + "<td style='width: 90mm; height: 57mm; border: 1px solid black;padding:0px;'>"
                                     + "<center><table><tr>"
                                     + "<td style='width: 86mm; height: 54mm;border-radius:12px;padding:0px'>"
                                     + "<center style='margin-left: 5px;'><table><tr>"
                                     + "<td style='width:81mm;height:50mm;padding:0px'>"
                                     + "<table><tr style='font-size:10px;'><td style='    width: 17%;'>"
                                        + "<b>Address :</b></td><td>" + ds.Tables[0].Rows[i]["address"].ToString() + "</td>"
                                        + "</td></tr></table>"
                                        + "<table><tr><td>"
                                        + "<p style='font-size:10px;'><b>Phone No:</b>" + ds.Tables[0].Rows[i]["phone_no2"].ToString() + ""
                                        + "</td></tr></table>"
                                          + "<table><tr><td>"
                                        + "<p style='font-size:10px;'><b>Aadhar Card No:</b>" + ds.Tables[0].Rows[i]["aadhar_no"].ToString() + ""
                                        + "</td></tr></table>"
                                     + "<table><tr><td>"
                                     + "<b style='font-size:12px'>Instructions :</b>"
                                     + "</td></tr></table>"
                                     + "<table><tr><td>"
                                     + "<b style='font-size:8px'>1) This card is not transferable and must be produced whenever demanded.</b>"
                                     + "</td></tr></table>"
                                     + "<table><tr><td>"
                                     + "<b style='font-size:8px'>2) In the event of its loss the holder of the card must intimate to the Principal <span style='margin-left:9px'>immediately in writing.</span></b>"
                                     + "</td></tr></table>"
                                     + "<table><tr><td>"
                                     + "<b style='font-size:8px'>3) Any one who finds this card is requested to send it to the Principal.</b>"
                                     + "</td></tr></table>"
                                     + "<table><tr>"
                                     + "<td></td>"
                                     + "<td>"
                                     + "<center><img src='data:image/png;base64," + bb + "' alt='' width='245' height='35' style='margin-left: 50px;'></center>"
                                     + "</td>"
                                     + "<td></td>"
                                     + "</tr></table>"
                                     + "</td></tr></table></center></td></tr></table></center></td>"


                                    );

                                k++;
                            }

                        }


                    }

                    id.type = "insert";
                    //id.insert = tt.ToString();
                    id.insert = DataTableToJSON(tt);
                    id.ayid = Session["acdyear"].ToString();
                    //id.ayid=Session["ddlyesr"]

                    string urlalias = cls.urls();
                    string url = @urlalias + "id_gen/";

                   // string url1 = "http://localhost:9199/id_gen/";
                    string jsonString1 = JsonHelper.JsonSerializer<id_gen>(id);
                    var httprequest1 = (HttpWebRequest)WebRequest.Create(url);
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
                        string result1 = streamReader1.ReadToEnd();
                        //DataSet ds11 = JsonConvert.DeserializeObject<DataSet>(result1);
                    }

                    string bb1 = "";
                    string barCode1 = "123456";
                    System.Web.UI.WebControls.Image imgBarCode1 = new System.Web.UI.WebControls.Image();
                    using (Bitmap bitMap = new Bitmap(barCode1.Length * 40, 80))
                    {
                        using (Graphics graphics = Graphics.FromImage(bitMap))
                        {
                            System.Drawing.Font oFont = new System.Drawing.Font("IDAutomationHC39M", 16);
                            PointF point = new PointF(2f, 2f);
                            SolidBrush blackBrush = new SolidBrush(Color.Black);
                            SolidBrush whiteBrush = new SolidBrush(Color.White);
                            graphics.FillRectangle(whiteBrush, 0, 0, bitMap.Width, bitMap.Height);
                            graphics.DrawString("*" + barCode1 + "*", oFont, blackBrush, point);
                        }
                        using (MemoryStream ms = new MemoryStream())
                        {
                            bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                            byte[] byteImage = ms.ToArray();

                            bb1 = Convert.ToBase64String(byteImage);

                        }
                    }
                    table12.WriteLine("</center>");
                    table12.WriteLine("</body>");
                    table12.Close();
                    //con.Close();http://localhost/Utkarsha/id_card/ID_card.html
                    string url2 = "http://203.192.254.34/Utkarsha/ID_card/ID_card.html";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "window.open('" + url2 + "','_blank')", true);
                    //dd.HeaderRow.TableSection = TableRowSection.TableHeader;
                    //ScriptManager.RegisterStartupScript(Page, GetType(), "loaddtt", "loaddtt();", true);
                }
                else { notifys("There Is No Data", "#D9534F"); }
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data Downloaded')", true);
                //Response.Redirect("ID_card/ID_card.html");

            }
            else
            {
                if (txt_stud.Text == "" && dup.Checked)
                {
                    notifys("Please Enter ID", "#D9534F");
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('please enter id')", true);
                }

                else
                {
                    notifys("Please Select Medium and Class", "#D9534F");
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('please select Medium and class')", true);
                }

            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    public void notifys(string msg, string color)
    {
        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + msg + "', { color: '#fff', background: '" + color + "', blur: 0.2, delay: 0 });", true);
    }

    public string DataTableToJSON(DataTable table)
    {
        string JSONString = string.Empty;
        JSONString = JsonConvert.SerializeObject(table);
        return JSONString;
    }

    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (dup.Checked == true)
            {
                dd.DataSource = null;
                dd.DataBind();
                txt_stud.Text = "";
                txt_stud.Enabled = true;
                ddl_subcrs.SelectedIndex = 0;
                ddl_subcrs.Enabled = false;
                if (dd.Rows.Count != 0)
                {
                    dd.HeaderRow.TableSection = TableRowSection.TableHeader;
                    ScriptManager.RegisterStartupScript(Page, GetType(), "loaddtt", "loaddtt();", true);
                }

                if (DropDownList1.Items.Count == 0)
                {
                    DropDownList1.Enabled = false;
                }
                else
                {
                    DropDownList1.Enabled = false;
                    DropDownList1.SelectedIndex = 0;
                }


            }
            else
            {
                txt_stud.Text = "";
                txt_stud.Enabled = false;
                ddl_subcrs.SelectedIndex = 0;
                ddl_subcrs.Enabled = true;
                dd.DataSource = null;
                dd.DataBind();
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void btn_get_Click(object sender, EventArgs e)
    {
        
        DataTable dtt = new DataTable();
        dtt.Columns.Add("Stud_id");
        dtt.Columns.Add("Name");
        dtt.Columns.Add("roll_no");
        dtt.Columns.Add("ayid");
        dtt.Columns.Add("Emp_id");
        try
        {
            if (ddl_subcrs1.SelectedIndex != 0 || txt_stud1.Text != "")
            {
                
                string str = "";
                if (txt_stud1.Text != "")
                {
                    string stud_id = txt_stud1.Text;
                    stud_id = stud_id.Replace(",", "','");
                    id.type = "duplicate";
                    id.classid = stud_id;
                    id.ayid = Session["acdyear"].ToString();
                    //id.ayid=Session["ddlyesr"]
                    string urlalias = cls.urls();
                    string url = @urlalias + "id_gen/";
                    //string url = "http://localhost:9199/id_gen/";
                    string jsonString = JsonHelper.JsonSerializer<id_gen>(id);
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
                    }
                  
                }
                else
                {

                    id.type = "employee1";
                     id.classid = ddl_subcrs1.SelectedValue;
                    


                    id.ayid = Session["acdyear"].ToString();
                    //id.ayid=Session["ddlyesr"]

                    string urlalias = cls.urls();
                    string url = @urlalias + "id_gen/";

                  //  string url = "http://localhost:9199/id_gen/";
                    string jsonString = JsonHelper.JsonSerializer<id_gen>(id);
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
                    }
                    
                }
            
                string FilePath = "";

                if (ds.Tables[0].Rows.Count > 0)
                {
                    string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                    //StreamWriter table12 = new StreamWriter("E:\\website\\staff\\ID_card_html_files\\" + ddl_subcrs.SelectedItem.Text.ToString() + "_ID_card.html");
                    path = Server.MapPath("");
                    StreamWriter table12 = new StreamWriter(path + "\\id_card\\_ID_card.html");

                    int k = 0;
                    StringBuilder sb = new StringBuilder();
                    table12.WriteLine("<head><style>"
                           + "@media print {"
                                        + "@page {"
                                                + "margin-top: 0.5cm;"
                                                + "margin-bottom: 0.5cm;"
                                              + "}"
                                         + "}"
                          + "</style></head>");
                    table12.WriteLine("<body style='margin:0px' onload='window.print()'>");
                    table12.WriteLine("<center>");
                    string va = "";

                    for (int i = 0; i <= ds.Tables[0].Rows.Count; i++)
                    {
                        if (i < ds.Tables[0].Rows.Count)
                        {

                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                dtt.Rows.Add(ds.Tables[0].Rows[i]["emp_id"].ToString(), ds.Tables[0].Rows[i]["name"].ToString(), ds.Tables[0].Rows[i]["dept_id"].ToString(), Session["acdyear"].ToString(), Session["Emp_id"].ToString());

                                if (ds.Tables[0].Rows[i]["emp_photo"].ToString() != "" && ds.Tables[0].Rows[i]["emp_photo"].ToString() != "null" && ds.Tables[0].Rows[i]["emp_photo"].ToString() != null)
                                {
                                    if (ds.Tables[0].Rows[i]["emp_sign"].ToString() != "" && ds.Tables[0].Rows[i]["emp_sign"].ToString() != "null" && ds.Tables[0].Rows[i]["emp_sign"].ToString() != null)
                                    {
                                        string class_val = "";

                                        string bb = "";
                                        string barCode = ds.Tables[0].Rows[i]["emp_id"].ToString();
                                        System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
                                        using (Bitmap bitMap = new Bitmap(barCode.Length * 40, 80))
                                        {
                                            using (Graphics graphics = Graphics.FromImage(bitMap))
                                            {
                                                System.Drawing.Font oFont = new System.Drawing.Font("IDAutomationHC39M", 16);
                                                PointF point = new PointF(2f, 2f);
                                                SolidBrush blackBrush = new SolidBrush(Color.Black);
                                                SolidBrush whiteBrush = new SolidBrush(Color.White);
                                                graphics.FillRectangle(whiteBrush, 0, 0, bitMap.Width, bitMap.Height);
                                                graphics.DrawString("*" + barCode + "*", oFont, blackBrush, point);
                                            }
                                            using (MemoryStream ms = new MemoryStream())
                                            {
                                                bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                                                byte[] byteImage = ms.ToArray();

                                                bb = Convert.ToBase64String(byteImage);

                                            }
                                        }
                                        //--------------------------------------
                                        int l = 0;

                                        l = 11;
                                        if (k == 3)
                                        {
                                            k = 1;

                                            table12.WriteLine("<table style='page-break-after:always;'></table>"
                                                 + "<table style='border-spacing:0px;border-collapse: collapse;font-family:arial;'>"
                                                //  + "<tr><td style='width: 57mm; height: 90mm; border: 1px solid black;padding:0px; background-image: url(http://203.192.254.35/staff/images/" + va + ".jpg);background-size:contain'>"
                                                + "<tr><td style='width: 57mm; height: 90mm; border: 1px solid black;padding:0px; '>"

                                                + "<center><table><tr>"
                                                + "<td style='width: 54mm; height: 86mm;border-radius:12px;padding:0px;border:1px solid white;margin-left;background-image: url(http://localhost:14094/staff_engg_lib_changes_done_IssueRet_original/images/id-bg.png);background-size:cover'>"
                                                + "<center><table><tr>"
                                                + "<td style='width:50mm;height:81mm;padding-top: 5px;'>"
                                                + "<center><table style='width: 100%; height: 20mm;border-spacing:0px;'><tr><td><center>"
                                                + "<img src='http://203.192.254.34/staff_lib_new/images/vivalogo.png' class='img-rounded' alt='Cinque Terre' width='35' height='35' />"
                                                + "<center></td></tr><tr><td>"
                                                + "<p style='font-size:8px;color:black;text-align:center;margin:0px;    color: blue;'>"
                                                + "Late Shri. Vishnu Waman Thakur Charitable Trust's"
                                                + "<br/><b style='font-size: 11px;color:purple'>VIVA INSTITUTE OF TECHNOLOGY</b>"
                                                + "<br/>Shirgoan, Virar (E),Pin - 401 305;Tel : 0250-6990999</p>"
                                                + "</td></tr></table></center>"
                                                 + "<center style='margin-top: 4px;'><table style='border-spacing:0px;'>"
                                                //+"<tr><td>"
                                                //+ "<p style='font-size:9px;color:white;text-align:center;margin:0px;color:black;font-weight:bold'>"
                                                //+ "2018 - 2019 </p>"
                                                //+ "</td></tr>"
                                                + "</table></center>"
                                                + "<table style='width: 100%; height: 42mm;'><tr><td colspan='2'>"

                                                + "<center><img src='data:image/png;base64," + ds.Tables[0].Rows[i]["emp_photo"] + "'  alt='' width='65' height='65' style='border: 1px solid black;'></center>"

                                                + "</td></tr><tr>"
                                                + "<td colspan='2' style='padding-left: 11px;font-size: 11px;font-family:arial'>"

                                            //    + "<center><b style='font-size: 11px;font-family:arial'>NAME:</b> " + ds.Tables[0].Rows[i]["NAME"].ToString().ToUpper() + "</center>"
                                                + "<center> " + ds.Tables[0].Rows[i]["NAME"].ToString().ToUpper() + "</center>"
                                                + "<br><b style='font-size: 11px;font-family:arial'>DEPARTMENT:</b> " + ds.Tables[0].Rows[i]["Department_name"].ToString().ToUpper() + ""
                                                //  + "<br>Roll No: " + ds.Tables[0].Rows[i]["ROLL NO"].ToString() + "<br>"
                                                + "<br><b style='font-size: 11px;font-family:arial'>DESIGNATION. :</b>" + ds.Tables[0].Rows[i]["Designation_title"].ToString().ToUpper() + ""//Convert.ToDateTime(ds.Tables[0].Rows[i]["DOB"]).Date.ToString("dd/MM/yyyy")
                                                + "<br><b style='font-size: 11px;font-family:arial'>DATE OF JOINING. :</b>" + Convert.ToDateTime(ds.Tables[0].Rows[i]["emp_gov"]).Date.ToString("dd/MM/yyyy") + ""
                                                + "<br><b style='font-size: 11px;font-family:arial'>GENDER. :</b>" + ds.Tables[0].Rows[i]["emp_gender"].ToString() + ""
                                                + "</td></tr></table>"
                                                + "<table style='width: 100%; height: 12mm;text-align: center;'><tr>"
                                                     + "<td>"
                                                     + "<img src='data:image/png;base64," + ds.Tables[0].Rows[i]["emp_sign"] + "' alt='' width='65' height='25' style='PADDING-TOP: 2PX;border: 1px solid black;margin-top: 2px;'><br/>"
                                                + "<label style='color:black;font-size:11px;font-weight:bold' id='lblstud_id_" + i + "' >Employee Sign</label></td>"

                                                + "<td style='vertical-align: bottom;text-align: center;padding-right: 10px;'>"
                                                + "<img src='http://103.31.144.152/staff/images/PRI_sing_engg.png' alt='' width='50' height='20' ><br/>"
                                                + "<label style='color:black;font-size:11px;font-weight:bold' id='lblstud_id_" + i + "' >Principal Sign</label>"
                                                + "</td>"

                                                + "</tr></table>"
                                                + "<table style='width:100%;height:5mm;text-align:center;margin-top:10px'><tr><td>"

                                                + "<b style='color:white;font-size:10px;float:left;margin-left: 8px;'>" + ds.Tables[0].Rows[i]["emp_id"].ToString().ToUpper() + "</b>"
                                                + "</td>"
                                                + "<td>"
                                                + "<b style='color:white;font-size:10px;float:right;'>www.viva-technology.org</b>"
                                                + "</td>"
                                                + "</tr></table>"


                                                + "</td></tr></table></center></td></tr></table></center></td>"

                                             + "<td style='width: 57mm; height: 90mm; border: 1px solid black;padding:0px;'>"
                                                + "<center><table><tr>"
                                                + "<td style='width: 54mm; height: 86mm;border-radius:12px;padding:0px'>"
                                                + "<center style='margin-left: 5px;'><table><tr>"
                                                + "<td style='width:50mm;height:81mm;padding:0px'>"
                                                + "<table><tr><td>"
                                                + "<p style='font-size:11px;'><b>ADDRESS :</b>" + ds.Tables[0].Rows[i]["emp_address_curr"].ToString() + "</p>"
                                                + "</td></tr></table>"
                                                + "<table><tr><td>"
                                                + "<p style='font-size:11px;'><b>CONTACT NO :</b>" + ds.Tables[0].Rows[i]["emp_phone1"].ToString() + "</p>"
                                                + "</td></tr></table>"
                                                 + "<table><tr><td>"
                                                + "<p style='font-size:11px;'><b>DATE OF BIRTH :</b>" + Convert.ToDateTime(ds.Tables[0].Rows[i]["emp_dob"]).Date.ToString("dd/MM/yyyy") + ""
                                                + "</td></tr></table>"
                                                 + "<table><tr><td>"
                                                + "<p style='font-size:11px;'><b>BLOOD GROUP :</b>" + ds.Tables[0].Rows[i]["emp_blood_group"].ToString() + "</p>"
                                                + "</td></tr></table>"
                                                + "<table><tr><td>"
                                                + "<b style='font-size:14px'>Instructions :</b>"
                                                + "</td></tr></table>"
                                                + "<table><tr><td>"
                                                + "<b style='font-size:10px'>1) Staff Must Wear This Card All Time In The College.</b>"
                                                + "</td></tr></table>"
                                                + "<table><tr><td>"
                                                + "<b style='font-size:10px'>2)Tis Card Is Not Transferable And Must Be Produced On Demand.</span></b>"
                                                + "</td></tr></table>"
                                                + "<table><tr><td>"
                                                + "<b style='font-size:10px'>3)Lost, Theft Or Damage Of This card Should Be Produced.</b>"
                                                + "</td></tr></table>"
                                                + "<table><tr><td>"
                                                + "<b style='font-size:10px'>4)Must Be Worm During All Function, Celebration, Pinic And Events.</b>"
                                                + "</td></tr></table>"

                                                + "</td></tr></table></center></td></tr></table></center></td>"


                                                );
                                        }
                                        else
                                        {

                                            table12.WriteLine("<table style='border-spacing:0px;border-collapse: collapse;font-family:arial;'>"

                                                + "<tr><td style='width: 57mm; height: 90mm; border: 1px solid black;padding:0px; '>"

                                                + "<center><table><tr>"
                                                + "<td style='width: 54mm; height: 86mm;border-radius:12px;padding:0px;border:1px solid white;margin-left;background-image: url(http://localhost:14094/staff_engg_lib_changes_done_IssueRet_original/images/id-bg.png);background-size:cover'>"
                                                + "<center><table><tr>"
                                                + "<td style='width:50mm;height:81mm;padding-top: 5px;'>"
                                                + "<center><table style='width: 100%; height: 20mm;border-spacing:0px;'><tr><td><center>"
                                                + "<img src='http://203.192.254.34/staff_lib_new/images/vivalogo.png' class='img-rounded' alt='Cinque Terre' width='35' height='35' />"
                                                + "<center></td></tr><tr><td>"
                                                + "<p style='font-size:8px;color:black;text-align:center;margin:0px;    color: blue;'>"
                                                + "Late Shri. Vishnu Waman Thakur Charitable Trust's"
                                                + "<br/><b style='font-size: 11px;color:purple'>VIVA INSTITUTE OF TECHNOLOGY</b>"
                                                + "<br/>Shirgoan, Virar (E),Pin - 401 305;Tel : 0250-6990999</p>"
                                                + "</td></tr></table></center>"
                                                 + "<center style='margin-top: 4px;'><table style='border-spacing:0px;'>"

                                                + "</table></center>"
                                                + "<table style='width: 100%; height: 42mm;'><tr><td colspan='2'>"

                                                + "<center><img src='data:image/png;base64," + ds.Tables[0].Rows[i]["emp_photo"] + "'  alt='' width='65' height='65' style='border: 1px solid black;'></center>"

                                                + "</td></tr><tr>"
                                                + "<td colspan='2' style='padding-left: 11px;font-size: 11px;font-family:arial'>"

                                              + "<center> <b>" + ds.Tables[0].Rows[i]["NAME"].ToString().ToUpper() + "</b></center>"
                                                + "<br><b style='font-size: 10px;font-family:arial'>DEPARTMENT:</b> " + ds.Tables[0].Rows[i]["Department_name"].ToString().ToUpper() + ""

                                                + "<br><b style='font-size: 10px;font-family:arial'>DESIGNATION. :</b>" + ds.Tables[0].Rows[i]["Designation_title"].ToString().ToUpper() + ""//Convert.ToDateTime(ds.Tables[0].Rows[i]["DOB"]).Date.ToString("dd/MM/yyyy")
                                                + "<br><b style='font-size: 10px;font-family:arial'>DATE OF JOINING. :</b>" + Convert.ToDateTime(ds.Tables[0].Rows[i]["emp_gov"]).Date.ToString("dd/MM/yyyy") + ""
                                                + "<br><b style='font-size: 10px;font-family:arial'>GENDER. :</b>" + ds.Tables[0].Rows[i]["emp_gender"] + ""
                                                + "</td></tr></table>"
                                                + "<table style='width: 100%; height: 12mm;text-align: center;'><tr>"
                                                     + "<td>"
                                                     + "<img src='data:image/png;base64," + ds.Tables[0].Rows[i]["emp_sign"] + "' alt='' width='65' height='25' style='PADDING-TOP: 2PX;border: 1px solid black;margin-top: 2px;'><br/>"
                                                + "<label style='color:black;font-size:10px;font-weight:bold' id='lblstud_id_" + i + "' >Employee Sign</label></td>"

                                                + "<td style='vertical-align: bottom;text-align: center;padding-right: 10px;'>"
                                                + "<img src='http://103.31.144.152/staff/images/PRI_sing_engg.png' alt='' width='50' height='20' ><br/>"
                                                + "<label style='color:black;font-size:10px;font-weight:bold' id='lblstud_id_" + i + "' >Principal Sign</label>"
                                                + "</td>"

                                                + "</tr></table>"
                                                + "<table style='width:100%;height:5mm;text-align:center;margin-top:10px'><tr><td>"

                                                + "<b style='color:white;font-size:10px;float:left;margin-left: 8px;'>" + ds.Tables[0].Rows[i]["emp_id"].ToString().ToUpper() + "</b>"
                                                + "</td>"
                                                + "<td>"
                                                + "<b style='color:white;font-size:10px;float:right;'>www.viva-technology.org</b>"
                                                + "</td>"
                                                + "</tr></table>"


                                                + "</td></tr></table></center></td></tr></table></center></td>"

                                             + "<td style='width: 57mm; height: 90mm; border: 1px solid black;padding:0px;'>"
                                                + "<center><table><tr>"
                                                + "<td style='width: 54mm; height: 86mm;border-radius:12px;padding:0px'>"
                                                + "<center style='margin-left: 5px;'><table><tr>"
                                                + "<td style='width:50mm;height:81mm;padding:0px'>"
                                                + "<table><tr><td>"
                                                + "<p style='font-size:10px;'><b>ADDRESS :</b>" + ds.Tables[0].Rows[i]["emp_address_curr"].ToString() + "</p>"
                                                + "</td></tr></table>"
                                                + "<table><tr><td>"
                                                + "<p style='font-size:10px;'><b>CONTACT NO :</b>" + ds.Tables[0].Rows[i]["emp_phone1"].ToString() + "</p>"
                                                + "</td></tr></table>"
                                                 + "<table><tr><td>"
                                                + "<p style='font-size:10px;'><b>DATE OF BIRTH :</b>" + Convert.ToDateTime(ds.Tables[0].Rows[i]["emp_dob"]).Date.ToString("dd/MM/yyyy") + ""
                                                + "</td></tr></table>"
                                                 + "<table><tr><td>"
                                                + "<p style='font-size:10px;'><b>BLOOD GROUP :</b>" + ds.Tables[0].Rows[i]["emp_blood_group"].ToString() + "</p>"
                                                + "</td></tr></table>"
                                                + "<table><tr><td>"
                                                + "<b style='font-size:14px'>Instructions :</b>"
                                                + "</td></tr></table>"
                                                + "<table><tr><td>"
                                                + "<b style='font-size:10px'>1) Staff Must Wear This Card All Time In The College.</b>"
                                                + "</td></tr></table>"
                                                + "<table><tr><td>"
                                                + "<b style='font-size:10px'>2)Tis Card Is Not Transferable And Must Be Produced On Demand.</span></b>"
                                                + "</td></tr></table>"
                                                + "<table><tr><td>"
                                                + "<b style='font-size:10px'>3)Lost, Theft Or Damage Of This card Should Be Produced.</b>"
                                                + "</td></tr></table>"
                                                + "<table><tr><td>"
                                                + "<b style='font-size:10px'>4)Must Be Worm During All Function, Celebration, Pinic And Events.</b>"
                                                + "</td></tr></table>"

                                                + "</td></tr></table></center></td></tr></table></center></td>"

                                                );

                                            k++;
                                        }

                                    }
                                }
                            }

                        }

                        else
                        {

                            string bb = "";
                            string barCode = "123456";
                            System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
                            using (Bitmap bitMap = new Bitmap(barCode.Length * 40, 80))
                            {
                                using (Graphics graphics = Graphics.FromImage(bitMap))
                                {
                                    System.Drawing.Font oFont = new System.Drawing.Font("IDAutomationHC39M", 16);
                                    PointF point = new PointF(2f, 2f);
                                    SolidBrush blackBrush = new SolidBrush(Color.Black);
                                    SolidBrush whiteBrush = new SolidBrush(Color.White);
                                    graphics.FillRectangle(whiteBrush, 0, 0, bitMap.Width, bitMap.Height);
                                    graphics.DrawString("*" + barCode + "*", oFont, blackBrush, point);
                                }
                                using (MemoryStream ms = new MemoryStream())
                                {
                                    bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                                    byte[] byteImage = ms.ToArray();

                                    bb = Convert.ToBase64String(byteImage);

                                }
                            }

                            int l = 0;

                            l = 11;
                            if (k == 5)
                            {
                                k = 1;

                                table12.WriteLine("<table style='page-break-after:always;'></table>"
                                    + "<table><tr>"
                                    + "<td></td>"
                                    + "<td>"
                                    + "<center><img src='data:image/png;base64," + bb + "' alt='' width='245' height='35' style='margin-left: 50px;display:none'></center>"
                                    + "</td>"
                                    + "<td></td>"
                                    + "</tr></table>"
                                    + "</td></tr></table>"


                                    );




                            }
                            else
                            {
                                table12.WriteLine("<table></table>"
                                   + "<table><tr>"
                                   + "<td></td>"
                                   + "<td>"
                                   + "<center><img src='data:image/png;base64," + bb + "' alt='' width='245' height='35' style='margin-left: 50px;display:none'></center>"
                                   + "</td>"
                                   + "<td></td>"
                                   + "</tr></table>"
                                   + "</td></tr></table>"


                                   );

                                k++;
                            }

                        }

                    }
                    //string strr= Session["emp_id"].ToString();

                    id.type = "employee1insert";
                    id.insert = DataTableToJSON(dtt);
                    id.ayid = Session["acdyear"].ToString();

                    string urlalias = cls.urls();
                    string url = @urlalias + "id_gen/";

                   // string url = "http://localhost:9199/id_gen/";
                    string jsonString = JsonHelper.JsonSerializer<id_gen>(id);
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
                        // ds = JsonConvert.DeserializeObject<DataSet>(result);
                    }
                    string bb1 = "";
                    string barCode1 = "123456";
                    System.Web.UI.WebControls.Image imgBarCode1 = new System.Web.UI.WebControls.Image();
                    using (Bitmap bitMap = new Bitmap(barCode1.Length * 40, 80))
                    {
                        using (Graphics graphics = Graphics.FromImage(bitMap))
                        {
                            System.Drawing.Font oFont = new System.Drawing.Font("IDAutomationHC39M", 16);
                            PointF point = new PointF(2f, 2f);
                            SolidBrush blackBrush = new SolidBrush(Color.Black);
                            SolidBrush whiteBrush = new SolidBrush(Color.White);
                            graphics.FillRectangle(whiteBrush, 0, 0, bitMap.Width, bitMap.Height);
                            graphics.DrawString("*" + barCode1 + "*", oFont, blackBrush, point);
                        }
                        using (MemoryStream ms = new MemoryStream())
                        {
                            bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                            byte[] byteImage = ms.ToArray();

                            bb1 = Convert.ToBase64String(byteImage);

                        }
                    }
                    table12.WriteLine("<table style='page-break-after:always;'></table>"
                                   + "<table><tr>"
                                   + "<td></td>"
                                   + "<td>"
                                   + "<center><img src='data:image/png;base64," + bb1 + "' alt='' width='245' height='35' style='margin-left: 50px;display:none'></center>"
                                   + "</td>"
                                   + "<td></td>"
                                   + "</tr></table>"
                                   + "</td></tr></table>");



                    table12.WriteLine("</center>");

                    table12.WriteLine("</body>");
                    table12.Close();
                 
                    string url2 = "http://203.192.254.34/Utkarsha/ID_card/_ID_card.html";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "window.open('" + url2 + "','_blank')", true);
                  
                    

                }
                else { notifys("There Is No Data", "#D9534F"); }

            }
            else
            {
                if (txt_stud1.Text == "" && dup1.Checked)
                {
                    notifys("Please Enter Employee ID", "#D9534F");
                }
                else
                {
                    notifys("Please Select Department", "#D9534F");
                   
                }
                

            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void ddl_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {

            if (ddl.SelectedIndex == 1)
            {
                hh1.Visible = true;
                hh.Visible = false;
                dd.DataSource = null;
                dd.DataBind();
                GridView1.DataSource = null;
                GridView1.DataBind();
                ddl_subcrs.SelectedIndex = 0;
                if (DropDownList1.Items.Count != 0)
                {
                    DropDownList1.SelectedIndex = 0;
                }

            }
            else if (ddl.SelectedIndex == 2)
            {
                hh.Visible = true;
                hh1.Visible = false;
                dd.DataSource = null;
                dd.DataBind();
                GridView1.DataSource = null;
                GridView1.DataBind();
                ddl_subcrs1.SelectedIndex = 0;
            }
            else if (ddl.SelectedIndex == 0)
            {
                hh1.Visible = false;
                hh.Visible = false;
                dd.DataSource = null;
                dd.DataBind();
                GridView1.DataSource = null;
                GridView1.DataBind();
                //ddl_subcrs1.SelectedIndex = 0;
            }
            else { }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void dup1_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (dup1.Checked == true)
            {
                ddl_subcrs1.SelectedIndex = 0;
                ddl_subcrs1.Enabled = false;
                txt_stud1.Enabled = true;
                GridView1.DataSource = null;
                GridView1.DataBind();
                if (GridView1.Rows.Count != 0)
                {
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    ScriptManager.RegisterStartupScript(Page, GetType(), "loaddt", "loaddt();", true);
                }

            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                txt_stud1.Text = "";
                ddl_subcrs1.Enabled = true;
                txt_stud1.Enabled = false;
                if (GridView1.Rows.Count != 0)
                {
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    ScriptManager.RegisterStartupScript(Page, GetType(), "loaddt", "loaddt();", true);
                }
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void txtdate_TextChanged(object sender, EventArgs e)
    {
        string str = txtdate.ToString();
        
    }

    protected void bbb_Click(object sender, EventArgs e)
    {
        try
        {
            string str = txtdate.Text;
            int sr1 = ddl.SelectedIndex;
            if (sr1 == 1)
            {
                id.type = "w";
                id.classid = str;

                string urlalias = cls.urls();
                string url = @urlalias + "id_gen/";

                //   string url = "http://localhost:9199/id_gen/";
                string jsonString = JsonHelper.JsonSerializer<id_gen>(id);
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
                    DataSet ds = JsonConvert.DeserializeObject<DataSet>(result);
                    grid.DataSource = ds;


                    grid.DataBind();
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        notifys("There Is No Student Id generated On Selected Date", "#D9534F");

                    }
                    else
                    {
                        rr.Visible = true;
                        grid.HeaderRow.TableSection = TableRowSection.TableHeader;
                        ScriptManager.RegisterStartupScript(Page, GetType(), "ldt", "ldt();", true);
                    }

                }

            }
            else
            {

                id.type = "w1";
                id.classid = str;

                string urlalias = cls.urls();
                string url = @urlalias + "id_gen/";

                //  string url = "http://localhost:9199/id_gen/";
                string jsonString = JsonHelper.JsonSerializer<id_gen>(id);
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
                    DataSet ds = JsonConvert.DeserializeObject<DataSet>(result);
                    GridView2.DataSource = ds;

                    GridView2.DataBind();
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        notifys("There Is No Staff Id generated On Selected Date", "#D9534F");
                    }
                    else
                    {
                        rr.Visible = true;
                        GridView2.HeaderRow.TableSection = TableRowSection.TableHeader;
                        ScriptManager.RegisterStartupScript(Page, GetType(), "loaddtd", "loaddtd();", true);
                    }

                }
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void btn_cnt_Click(object sender, EventArgs e)
    {
        try
        {
            grid.DataSource = null;
            txtdate.Text = "";

            grid.DataBind();
            GridView2.DataBind();
            rr.Visible = false;

            if (GridView1.Rows.Count != 0)
            {
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                ScriptManager.RegisterStartupScript(Page, GetType(), "loaddt", "loaddt();", true);
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
        
        
    }

    protected void ddl_subcrs1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            id.type = "employee1";
            id.classid = ddl_subcrs1.SelectedValue;



            id.ayid = Session["acdyear"].ToString();
            //id.ayid=Session["ddlyesr"]

            string urlalias = cls.urls();
            string url = @urlalias + "id_gen/";

            // string url = "http://localhost:9199/id_gen/";
            string jsonString = JsonHelper.JsonSerializer<id_gen>(id);
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
                if (ds.Tables[0].Rows.Count == 0)
                {
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                    notifys("There Is No Data", "#D9534F");
                }
                else
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();

                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    ScriptManager.RegisterStartupScript(Page, GetType(), "loaddt", "loaddt();", true);
                }
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            id.type = "select";
            id.classid = DropDownList1.SelectedValue;
            id.mediumid = ddl_subcrs.SelectedValue;


            id.ayid = Session["acdyear"].ToString();
            //id.ayid=Session["ddlyesr"]

            string urlalias = cls.urls();
            string url = @urlalias + "id_gen/";

            //  string url = "http://localhost:9199/id_gen/";
            string jsonString = JsonHelper.JsonSerializer<id_gen>(id);
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
                if (ds.Tables[0].Rows.Count == 0)
                {
                    notifys("There Is No Student For This Standard", "#D9534F");
                    dd.DataSource = null;
                    dd.DataBind();
                    //dd.HeaderRow.TableSection = TableRowSection.TableHeader;
                    //ScriptManager.RegisterStartupScript(Page, GetType(), "loaddtt", "loaddtt();", true);
                }
                else
                {
                    dd.DataSource = ds;
                    dd.DataBind();
                    dd.HeaderRow.TableSection = TableRowSection.TableHeader;
                    ScriptManager.RegisterStartupScript(Page, GetType(), "loaddtt", "loaddtt();", true);
                }
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void btn_get_Click1(object sender, EventArgs e)
    {

    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            grid.DataSource = null;
            txtdate.Text = "";
            grid.DataBind();
            GridView2.DataBind();
            rr.Visible = false;
            if (dd.Rows.Count != 0)
            {
                dd.HeaderRow.TableSection = TableRowSection.TableHeader;
                ScriptManager.RegisterStartupScript(Page, GetType(), "loaddtt", "loaddtt();", true);
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }
}