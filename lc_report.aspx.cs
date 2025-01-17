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

public partial class lc_report : System.Web.UI.Page
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
                lblgr.Text = dataSet8.Tables["issue"].Rows[0]["gr_no"].ToString().ToUpper();
                lblsid.Text = dataSet8.Tables["issue"].Rows[0]["saral_id"].ToString().ToUpper();
                DateTime date = DateTime.ParseExact(dataSet8.Tables["issue"].Rows[0]["dob"].ToString(), "dd-MM-yyyy", null);


                lbladhar.Text = dataSet8.Tables["issue"].Rows[0]["aadhar_no"].ToString().ToUpper();
                lblstudname.Text = dataSet8.Tables["issue"].Rows[0]["stud_name"].ToString().ToUpper();
                lblmname.Text = dataSet8.Tables["issue"].Rows[0]["stud_mo_name"].ToString().ToUpper();
                lblmt.Text = dataSet8.Tables["issue"].Rows[0]["mother_tongue"].ToString().ToUpper();
                lblnat.Text = dataSet8.Tables["issue"].Rows[0]["nationality"].ToString().ToUpper();
                lblrelg.Text = dataSet8.Tables["issue"].Rows[0]["religion"].ToString().ToUpper();
                lblcast.Text = dataSet8.Tables["issue"].Rows[0]["caste"].ToString().ToUpper();
                lblsubcast.Text = dataSet8.Tables["issue"].Rows[0]["subcast_name"].ToString().ToUpper();
                string[] place_birth = dataSet8.Tables["issue"].Rows[0]["birth_place"].ToString().ToUpper().Split(',') ;
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
              else  if (place_birth.Length ==4)
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
                lbldob.Text = dataSet8.Tables["issue"].Rows[0]["dob"].ToString().ToUpper();
                lbldobinwords.Text = convertdatetowrds(date).ToUpper();
                lbllschool.Text = dataSet8.Tables["issue"].Rows[0]["last_school_name"].ToString().ToUpper();
                lbllsstd.Text = dataSet8.Tables["issue"].Rows[0]["last_studied_std"].ToString().ToUpper();

                lbldoa.Text = dataSet8.Tables["issue"].Rows[0]["date_of_admission"].ToString().ToUpper();
                lblstddoa.Text= standardinwords( dataSet8.Tables["issue"].Rows[0]["doa_std"].ToString().ToUpper());
                lblprog.Text = dataSet8.Tables["issue"].Rows[0]["progress"].ToString().ToUpper();
                lblconduct.Text = dataSet8.Tables["issue"].Rows[0]["Conduct"].ToString().ToUpper();
                lbldols.Text = dataSet8.Tables["issue"].Rows[0]["Date_of_leaving"].ToString().ToUpper();
                lblstandard.Text = dataSet8.Tables["issue"].Rows[0]["standard_in_which"].ToString().ToUpper();
                lblstand.Text = dataSet8.Tables["issue"].Rows[0]["standard_in_which_in_numbers"].ToString().ToUpper();
                lblreason.Text = dataSet8.Tables["issue"].Rows[0]["Reason"].ToString().ToUpper();
                lblremark.Text = dataSet8.Tables["issue"].Rows[0]["Remark"].ToString().ToUpper();
                lblseatno.Text = dataSet8.Tables["issue"].Rows[0]["Lc_No"].ToString().ToUpper();
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }

    }

    public string standardinwords(string standard)
    {

        string doastd = standard.ToString();
        string doawordstd = "";
        if (standard.Contains("1") == true && standard.Contains("10") == false)
        {
            doawordstd = "FIRST";
        }
        else if (standard.Contains("2") == true)
        {
            doawordstd = "SECOND";
        }
        else if(standard.Contains("3") == true)
        {
            doawordstd = "THIRD";
        }
        else if(standard.Contains("4") == true)
        {
            doawordstd = "FOURTH";
        }
        else if(standard.Contains("5") == true)
        {
            doawordstd = "FIFTH";
        }
        else if(standard.Contains("6") == true)
        {
            doawordstd = "SIXTH";
        }
        else if(standard.Contains("7") == true)
        {
            doawordstd = "SEVENTH";
        }
        else if(standard.Contains("8") == true)
        {
            doawordstd = "EIGHTH";
        }
        else if(standard.Contains("9") == true)
        {
            doawordstd = "NINETH";
        }
        else if(standard.Contains("10") == true)
        {
            doawordstd = "TENTH";
        }
        else
        {
            doawordstd = standard.ToString();
        }
        return doawordstd;
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
        strDateArray[0] = "First";
        strDateArray[1] = "Second";
        strDateArray[2] = "Third";
        strDateArray[3] = "Fourth";
        strDateArray[4] = "Fifth";
        strDateArray[5] = "Sixth";
        strDateArray[6] = "Seventh";
        strDateArray[7] = "Eighth";
        strDateArray[8] = "Ninth";
        strDateArray[9] = "Tenth";
        strDateArray[10] = "Eleventh";
        strDateArray[11] = "Twelfth";
        strDateArray[12] = "Thirteenth";
        strDateArray[13] = "Fourteenth";
        strDateArray[14] = "Fifteenth";
        strDateArray[15] = "Sixteenth";
        strDateArray[16] = "Seventeenth";
        strDateArray[17] = "Eighteenth";
        strDateArray[18] = "Nineteenth";
        strDateArray[19] = "Twentieth";
        strDateArray[20] = "Twenty-First";
        strDateArray[21] = "Twenty-Second";
        strDateArray[22] = "Twenty-Third";
        strDateArray[23] = "Twenty-Fourth";
        strDateArray[24] = "Twenty-Fifth";
        strDateArray[25] = "Twenty-Sixth";
        strDateArray[26] = "Twenty-Seventh";
        strDateArray[27] = "Twenty-Eighth";
        strDateArray[28] = "Twenty-Ninth";
        strDateArray[29] = "Thirtieth";
        strDateArray[30] = "Thirty-First";


        Int64 intDay = date.Day;
        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        string intMonth = mfi.GetMonthName(date.Month).ToString();
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
            words += ConvertNumbertoWords(number / 100000) + " Lakhs ";
            number %= 1000000;
        }
        if ((number / 1000) > 0)
        {
            words += ConvertNumbertoWords(number / 1000) + " Thousand ";
            number %= 1000;
        }
        if ((number / 100) > 0)
        {
            words += ConvertNumbertoWords(number / 100) + " Hundred ";
            number %= 100;
        }

        if (number > 0)
        {
            if (words != "") words += "";
            var unitsMap = new[]   
            {  
                "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "TEN", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Ninteen"  
            };
            var tensMap = new[]   
            {  
                "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety"  
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