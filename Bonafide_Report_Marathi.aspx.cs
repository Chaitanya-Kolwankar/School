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

public partial class Bonafide_Report_Marathi : System.Web.UI.Page
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

                        lblstandard.Text = dataSet.Tables["issue_bonafide"].Rows[0]["class_name"].ToString().ToUpper();
                        lbl_dt.Text = dataSet.Tables["issue_bonafide"].Rows[0]["issue_dt"].ToString();

                        lbl_dob.Text = dataSet.Tables["issue_bonafide"].Rows[0]["dob"].ToString();
                        lbl_saral.Text = dataSet.Tables["issue_bonafide"].Rows[0]["saral_id"].ToString().ToUpper();
                        lblcast.Text = dataSet.Tables["issue_bonafide"].Rows[0]["cast_name"].ToString().ToUpper();
                        if (dataSet.Tables["issue_bonafide"].Rows[0]["other_std"].ToString() == "--" || dataSet.Tables["issue_bonafide"].Rows[0]["other_std"].ToString() == null || dataSet.Tables["issue_bonafide"].Rows[0]["other_std"].ToString() == "")
                        {
                            lblstd.Text = dataSet.Tables["issue_bonafide"].Rows[0]["std_name"].ToString().ToUpper();

                        }
                        else
                        {
                            lblstd.Text = dataSet.Tables["issue_bonafide"].Rows[0]["other_std"].ToString();
                        }
                        lbl_grno.Text = dataSet.Tables["issue_bonafide"].Rows[0]["gr_no"].ToString().ToUpper();
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

                        string[] arr = (dataSet.Tables["issue_bonafide"].Rows[0]["duration"].ToString()).Split('-');
                        string[] arrspl1 = arr[0].Split('/');
                        string[] arrspl2 = arr[1].Split('/');

                        string acdemicyr = arrspl1[2].ToString() + " - " + arrspl2[2].ToString();
                        acdemicyear.Text = acdemicyr;


                        if (dataSet.Tables["issue_bonafide"].Rows[0]["gender"].ToString().Contains("Male"))
                        {
                            lblshe_he5.Text = "he";
                            lbl_gender2.Text = "His";
                            lbl_gender.Text = "Master";
                        }
                        else if (dataSet.Tables["issue_bonafide"].Rows[0]["gender"].ToString().Contains("Female"))
                        {
                            lblshe_he5.Text = "she";
                            lbl_gender2.Text = "Her";
                            lbl_gender.Text = "Miss";
                        }
                        if (dataSet.Tables["ayid"].Rows[0]["is_current"].ToString().Trim().ToUpper() == "TRUE")
                        {
                            iswas.Text = "is";
                            lblprespev.Text = " present ";
                        }
                        else
                        {
                            iswas.Text = "was";
                            lblprespev.Text = " previously ";
                        }
                        if (lblstd.Text.Contains("1"))
                        {
                            lblstandard.Text = "st";
                        }
                        else if (lblstd.Text.Contains("2"))
                        {
                            lblstandard.Text = "nd";
                        }
                        else if (lblstd.Text.Contains("3"))
                        {
                            lblstandard.Text = "rd";
                        }
                        else
                        {
                            lblstandard.Text = "th";
                        }

                        if (dataSet.Tables["issue_bonafide"].Rows[0]["class_id"].ToString().Contains("10"))
                        {

                        }
                        else
                        {

                        }
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
            if (words != "") words += "And ";
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