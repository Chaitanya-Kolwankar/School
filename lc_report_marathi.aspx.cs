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

public partial class lc_report_marathi : System.Web.UI.Page
{
    LC lc = new LC();
    Class1 cls = new Class1();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //string url8 = "http://localhost:9199/load/";
            string urlalias = cls.urls();
            string url8 = @urlalias + "load/";
            lc.type = "gettype";
            lc.Ayid = Session["acdyear"].ToString();
            lc.studid = Session["stud_id"].ToString();
            lc.lcno = Session["lcno"].ToString();

            string jsonString8 = JsonHelper.JsonSerializer<LC>(lc);
            var httprequest8 = (HttpWebRequest)WebRequest.Create(url8);
            httprequest8.ContentType = "application/json";
            httprequest8.Method = "POST";

            using (var streamWriter8 = new StreamWriter(httprequest8.GetRequestStream()))
            {
                streamWriter8.Write(jsonString8);
                streamWriter8.Flush();
                streamWriter8.Close();
            }

            var httpresponse8 = (HttpWebResponse)httprequest8.GetResponse();
            using (var streamReader8 = new StreamReader(httpresponse8.GetResponseStream()))
            {

                string result8 = streamReader8.ReadToEnd();
                DataSet dataSet8 = JsonConvert.DeserializeObject<DataSet>(result8);
                lblgr.Text = mardate(dataSet8.Tables["issue"].Rows[0]["gr_no"].ToString().ToUpper());
                lblsid.Text = mardate(dataSet8.Tables["issue"].Rows[0]["saral_id"].ToString().ToUpper());
                DateTime date = DateTime.ParseExact(dataSet8.Tables["issue"].Rows[0]["dob"].ToString(), "dd-MM-yyyy", null);


                lbladhar.Text = mardate(dataSet8.Tables["issue"].Rows[0]["aadhar_no"].ToString().ToUpper());
                lblstudname.Text = dataSet8.Tables["issue"].Rows[0]["stud_name"].ToString().ToUpper();
                lblmname.Text = dataSet8.Tables["issue"].Rows[0]["stud_mo_name"].ToString().ToUpper();
                lblmt.Text = dataSet8.Tables["issue"].Rows[0]["mother_tongue"].ToString().ToUpper();
                lblnat.Text = dataSet8.Tables["issue"].Rows[0]["nationality"].ToString().ToUpper();
                lblrelg.Text = dataSet8.Tables["issue"].Rows[0]["religion"].ToString().ToUpper();
                lblcast.Text = dataSet8.Tables["issue"].Rows[0]["caste"].ToString().ToUpper();
                lblsubcast.Text = dataSet8.Tables["issue"].Rows[0]["subcast_name"].ToString().ToUpper();
                string[] place_birth = dataSet8.Tables["issue"].Rows[0]["birth_place"].ToString().ToUpper().Split(',');
                if (place_birth.Length == 1)
                {
                    lblpob_vlg.Text = place_birth[0];

                }
                else if (place_birth.Length == 2)
                {
                    lblpob_vlg.Text = place_birth[0];
                    lblpob_tal.Text = place_birth[1];

                }
                else if (place_birth.Length == 3)
                {
                    lblpob_vlg.Text = place_birth[0];
                    lblpob_tal.Text = place_birth[1];
                    lblpob_dis.Text = place_birth[2];
                }
                else if (place_birth.Length == 4)
                {
                    lblpob_vlg.Text = place_birth[0];
                    lblpob_tal.Text = place_birth[1];
                    lblpob_dis.Text = place_birth[2];
                    lblpob_sta.Text = place_birth[3];
                }
                else if (place_birth.Length == 5)
                {
                    lblpob_vlg.Text = place_birth[0];
                    lblpob_tal.Text = place_birth[1];
                    lblpob_dis.Text = place_birth[2];
                    lblpob_sta.Text = place_birth[3];
                    lblpob_con.Text = place_birth[4];
                }
                //lbldob.Text = dataSet8.Tables["issue"].Rows[0]["dob"].ToString().ToUpper();
                lbldob.Text = mardate(dataSet8.Tables["issue"].Rows[0]["dob"].ToString().ToUpper());
                lbldobinwords.Text = convertdatetowrds(date).ToUpper();
                lbllschool.Text = dataSet8.Tables["issue"].Rows[0]["last_school_name"].ToString().ToUpper();
                lbllsstd.Text= dataSet8.Tables["issue"].Rows[0]["last_studied_std"].ToString().ToUpper();
               
                //lbldoa.Text = dataSet8.Tables["issue"].Rows[0]["date_of_admission"].ToString().ToUpper();
                lbldoa.Text = mardate(dataSet8.Tables["issue"].Rows[0]["date_of_admission"].ToString().ToUpper());
                lblstddoa.Text = standardinwords(dataSet8.Tables["issue"].Rows[0]["doa_std"].ToString().ToUpper());
                lblprog.Text = dataSet8.Tables["issue"].Rows[0]["progress"].ToString().ToUpper();
                lblconduct.Text = dataSet8.Tables["issue"].Rows[0]["Conduct"].ToString().ToUpper();
                //lbldols.Text = dataSet8.Tables["issue"].Rows[0]["Date_of_leaving"].ToString().ToUpper();
                lbldols.Text = mardate(dataSet8.Tables["issue"].Rows[0]["Date_of_leaving"].ToString().ToUpper());
                lblstandard.Text = dataSet8.Tables["issue"].Rows[0]["standard_in_which"].ToString().ToUpper();
                lblstand.Text = dataSet8.Tables["issue"].Rows[0]["standard_in_which_in_numbers"].ToString().ToUpper();
                lblreason.Text = dataSet8.Tables["issue"].Rows[0]["Reason"].ToString().ToUpper();
                lblremark.Text = dataSet8.Tables["issue"].Rows[0]["Remark"].ToString().ToUpper();
                lblseatno.Text = mardate(dataSet8.Tables["issue"].Rows[0]["Lc_No"].ToString().ToUpper());
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
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



    public string standardinwords(string standard)
    {

        string doastd = standard.ToString();
        string doawordstd = "";
        if ((standard.Contains("1") == true || standard.Contains("१") == true) && (standard.Contains("10") == false || standard.Contains("१०") == false))
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
        else if (standard.Contains("9") == true  || standard.Contains("९") == true)
        {
            doawordstd = "नववी";
        }
        else if (standard.Contains("10") == true  || standard.Contains("१०") == true)
        {
            doawordstd = "दहावी";
        }
        else
        {
            doawordstd = standard.ToString();
        }
        return doawordstd;
    }
    public string mardate(string engdate)
    {
        
        string result = new String(engdate.Select(c => c >= '0' && c <= '9' ? (Char)(c - '0' + 0x0966) : c).ToArray());


        return result;
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