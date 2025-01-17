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
using Newtonsoft.Json;

public partial class Bonafide_report_secondary_marathi : System.Web.UI.Page
{
    Bonafide bonafide = new Bonafide();
    Class1 cls = new Class1();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ConnectionString);
    SqlCommand cmd = new SqlCommand();

    Array[] rs = new Array[101];
    int count;

    static readonly string[] ones = new string[] { "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" };
    static readonly string[] teens = new string[] { "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
    static readonly string[] tens = new string[] { "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
    static readonly string[] thousandsGroups = { "", " Thousand", " Million", " Billion" };
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Convert.ToString(Session["emp_id"]) == "")
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                //string url = "http://localhost:9199/loadbonafide/";
                if (Session["stud_id"].ToString() != "" || Session["stud_id"].ToString() != null)
                {
                    string urlalias = cls.urls();
                    string url = @urlalias + "loadbonafide/";
                    bonafide.type = "printdata";
                    bonafide.Ayid = Session["acdyear"].ToString();
                    bonafide.sid = Session["stud_id"].ToString();
                    bonafide.standard = Session["standard"].ToString();
                    bonafide.bonafideno = Session["bonafideno"].ToString();

                    string jsonString = JsonHelper.JsonSerializer<Bonafide>(bonafide);
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
                        lbl_name.Text = dataSet.Tables["issue_bonafide"].Rows[0]["Stud_Name"].ToString().ToUpper();
                        DateTime date = DateTime.ParseExact(dataSet.Tables["issue_bonafide"].Rows[0]["dob"].ToString(), "dd/MM/yyyy", null);

                       // lblstandard.Text = dataSet.Tables["issue_bonafide"].Rows[0]["class_name"].ToString().ToUpper();
                        lbl_dt.Text = mardate(dataSet.Tables["issue_bonafide"].Rows[0]["issue_dt"].ToString());

                        lbl_dob.Text = mardate(dataSet.Tables["issue_bonafide"].Rows[0]["dob"].ToString());
                        lbl_doa.Text= mardate(dataSet.Tables["issue_bonafide"].Rows[0]["date_of_admission"].ToString());
                        // DateTime date = DateTime.ParseExact(dataSet.Tables["issue_bonafide"].Rows[0]["dob"].ToString(), "dd-MM-yyyy", null);
                       // lbldobword.Text = convertdatetowrds(date);
                        lbl_saral.Text = mardate(dataSet.Tables["issue_bonafide"].Rows[0]["saral_id"].ToString().ToUpper());
                        //lblreligion.Text = dataSet.Tables["issue_bonafide"].Rows[0]["religion"].ToString().ToUpper();
                        lblcast.Text = dataSet.Tables["issue_bonafide"].Rows[0]["cast_name"].ToString().ToUpper();
                        if (dataSet.Tables["issue_bonafide"].Rows[0]["other_std"].ToString() == "--" || dataSet.Tables["issue_bonafide"].Rows[0]["other_std"].ToString() == null || dataSet.Tables["issue_bonafide"].Rows[0]["other_std"].ToString() == "")
                        {
                            lblstd.Text = standardinwords(dataSet.Tables["issue_bonafide"].Rows[0]["std_name"].ToString().ToUpper());

                        }
                        else
                        {
                            lblstd.Text = standardinwords(dataSet.Tables["issue_bonafide"].Rows[0]["other_std"].ToString());
                        }
                        lbl_grno.Text = mardate(dataSet.Tables["issue_bonafide"].Rows[0]["gr_no"].ToString().ToUpper());
                        if (dataSet.Tables["issue_bonafide"].Rows[0]["student_photo"].ToString() != "")
                        {
                            if ((File.Exists(Server.MapPath("~/" + dataSet.Tables["issue_bonafide"].Rows[0]["student_photo"].ToString().Replace("\\", "/")))) == false)
                            {
                                studentimg.ImageUrl = "~/image/user.png";
                            }
                            else
                            {
                                studentimg.ImageUrl = "~/" + dataSet.Tables["issue_bonafide"].Rows[0]["student_photo"].ToString().Replace("\\", "/");
                            }

                        }
                        else
                        {
                            studentimg.ImageUrl = "~/image/user.png";
                        }

                        string[] arr = (mardate(dataSet.Tables["issue_bonafide"].Rows[0]["duration"].ToString())).Split('-');
                        string[] arrspl1 = arr[0].Split('/');
                        string[] arrspl2 = arr[1].Split('/');

                        string acdemicyr = arrspl1[2].ToString() + " - " + arrspl2[2].ToString();
                        acdemicyear.Text = acdemicyr;


                        if (dataSet.Tables["issue_bonafide"].Rows[0]["gender"].ToString().Contains("Male"))
                        {
                            //lblshe_he5.Text = "he";
                            //lbl_gender2.Text = "His";
                            lbl_gender.Text = "विद्यार्थी";
                            iswas.Text = "होता";
                            lblprespev.Text = "त्याची";
                        }
                        else if (dataSet.Tables["issue_bonafide"].Rows[0]["gender"].ToString().Contains("Female"))
                        {
                            //lblshe_he5.Text = "she";
                            //lbl_gender2.Text = "Her";
                            lbl_gender.Text = "विद्यार्थीनी";
                            iswas.Text = "होती";
                            lblprespev.Text = "तिची";
                        }
                        lblremark.Text = dataSet.Tables["issue_bonafide"].Rows[0]["Remark"].ToString();

                        //if (lblstd.Text.Contains("1"))
                        //{
                        //    lblstd.Text = lblstd.Text + "st";
                        //}
                        //else if (lblstd.Text.Contains("2"))
                        //{
                        //    lblstd.Text = lblstd.Text + "nd";
                        //}
                        //else if (lblstd.Text.Contains("3"))
                        //{
                        //    lblstd.Text = lblstd.Text + "rd";
                        //}
                        //else
                        //{
                        //    lblstd.Text = lblstd.Text + "th";
                        //}


                        // Page.Response.Redirect(Page.Request.Url.ToString(), true);
                    }
                }
                else
                {
                    Response.Redirect("Bonafide_Certificate.aspx", false);
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
    public string convertdatetowrds(DateTime date)
    {
        string[] strDateArray = new string[31];
        String strDate = "";
        strDateArray[0] = "एक";
        strDateArray[1] = "दोन";
        strDateArray[2] = "तीन";
        strDateArray[3] = "चार";
        strDateArray[4] = "पाच";
        strDateArray[5] = "सहा";
        strDateArray[6] = "सात";
        strDateArray[7] = "आठ";
        strDateArray[8] = "नऊ";
        strDateArray[9] = "दहा";
        strDateArray[10] = "अकरा";
        strDateArray[11] = "बारा";
        strDateArray[12] = "तेरा";
        strDateArray[13] = "चौदा";
        strDateArray[14] = "पंधरा";
        strDateArray[15] = "सोळा";
        strDateArray[16] = "सतरा";
        strDateArray[17] = "अठरा";
        strDateArray[18] = "एकोणवीस";
        strDateArray[19] = "वीस";
        strDateArray[20] = "एकवीस";
        strDateArray[21] = "बावीस";
        strDateArray[22] = "तेवीस";
        strDateArray[23] = "चोवीस";
        strDateArray[24] = "पंचवीस";
        strDateArray[25] = "सव्वीस";
        strDateArray[26] = "सत्तावीस";
        strDateArray[27] = "अट्टावीस";
        strDateArray[28] = "एकोणतीस";
        strDateArray[29] = "तीस";
        strDateArray[30] = "एकतीस";


        Int64 intDay = date.Day;
        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        string intMonth = mfi.GetMonthName(date.Month).ToString();
        if (intMonth.ToString() == "January")
        {
            intMonth = "जानेवारी";
        }
        if (intMonth.ToString() == "February")
        {
            intMonth = "फेब्रुवारी";
        }
        if (intMonth.ToString() == "March")
        {
            intMonth = "मार्च";
        }
        if (intMonth.ToString() == "April")
        {
            intMonth = "एप्रिल";
        }
        if (intMonth.ToString() == "May")
        {
            intMonth = "मे";
        }
        if (intMonth.ToString() == "June")
        {
            intMonth = "जून";
        }
        if (intMonth.ToString() == "July")
        {
            intMonth = "जुलै";
        }
        if (intMonth.ToString() == "August")
        {
            intMonth = "ऑगस्ट";
        }
        if (intMonth.ToString() == "September")
        {
            intMonth = "सप्टेंबर्";
        }
        if (intMonth.ToString() == "October")
        {
            intMonth = "ऑक्टोबर";
        }
        if (intMonth.ToString() == "November")
        {
            intMonth = "नोव्हेंबर्";
        }
        if (intMonth.ToString() == "December")
        {
            intMonth = "डिसेंबर";
        }
        Int64 intyear = date.Year;
        strDate = "";
        strDate = strDateArray[intDay - 1];
        strDate = strDate + " " + intMonth;
        if (intyear < 2000)
        {
            strDate = strDate + " " + ConvertNumbertoWords(intyear);
        }
        else
        {

            strDate = strDate + " " + ConvertNumbertoWords(intyear);

        }

        return strDate;
    }

    public string mardate(string engdate)
    {

        string result = new String(engdate.Select(c => c >= '0' && c <= '9' ? (Char)(c - '0' + 0x0966) : c).ToArray());


        return result;
    }

    public string standardinwords(string standard)
    {

        string doastd = standard.ToString();
        string doawordstd = "";
        if (standard.Contains("1") == true || standard.Contains("१") == true)
        {
            doawordstd = "पहिली";
        }
        else if (standard.Contains("2") == true || standard.Contains("२") == true)
        {
            doawordstd = "दुसरी";
        }
        else if (standard.Contains("3") == true || standard.Contains("३") == true)
        {
            doawordstd = "तीसरी";
        }
        else if (standard.Contains("4") == true || standard.Contains("४") == true)
        {
            doawordstd = "चौथी";
        }
        else if (standard.Contains("5") == true || standard.Contains("५") == true)
        {
            doawordstd = "पाचवी";
        }
        else if (standard.Contains("6") == true || standard.Contains("६") == true)
        {
            doawordstd = "सहावी";
        }
        else if (standard.Contains("7") == true || standard.Contains("७") == true)
        {
            doawordstd = "सातवी";
        }
        else if (standard.Contains("8") == true || standard.Contains("८") == true)
        {
            doawordstd = "आठवी";
        }
        else if (standard.Contains("9") == true || standard.Contains("९") == true)
        {
            doawordstd = "नववी";
        }
        else if (standard.Contains("10") == true || standard.Contains("१०") == true)
        {
            doawordstd = "दहावी";
        }
        else
        {
            doawordstd = standard.ToString();
        }
        return doawordstd;
    }

    public string ConvertNumbertoWords(long number)
    {
        if (number == 0) return "Zero";
        if (number < 0) return "minus " + ConvertNumbertoWords(Math.Abs(number));
        string words = "";
        if ((number / 1000000) > 0)
        {
            words += ConvertNumbertoWords(number / 100000) + " लाख";
            number %= 1000000;
        }
        if ((number / 1000) > 0)
        {
            words += ConvertNumbertoWords(number / 1000) + " हजार ";
            number %= 1000;
        }
        if ((number / 100) > 0)
        {
            words += ConvertNumbertoWords(number / 100) + " शंभर  ";
            number %= 100;
        }

        if (number > 0)
        {
            if (words != "") ;
            var unitsMap = new[]
            {
                "शून्य", "एक", "दोन", "तीन", "चार", "पाच", "सहा", "सात", "आठ", "नऊ", "दहा", "अकरा", "बारा", "तेरा", "चौदा", "पंधरा", "सोळा", "सतरा", "अठरा", "एकोणीस"
            };
            var tensMap = new[]
            {
                "शून्य", "दहा", "वीस", "तीस", "चाळीस", "पन्नास", "साठ", "सत्तर", "ऐंशी ", "नव्वद"
            };
            if (number < 20) words += unitsMap[number];
            else
            {
                words += tensMap[number / 10];
                if ((number % 10) > 0) words += " " + unitsMap[number % 10];
            }
        }
        return words;
    }
}