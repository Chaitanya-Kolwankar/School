using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class gazette : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Class1 cls = new Class1();
        gazettedata gzt = new gazettedata();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        if (!IsPostBack)
        {
            if (Convert.ToString(Session["emp_id"]) == "")
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                string url = cls.urls();
                //Session["url"] = url;
                //url = (Session["year"].ToString());
                Session["url"] = url;
            }
        }
    }

    [WebMethod]
    public static string Fillmedium()
    {
        string result3;
        Class1 cls = new Class1();
        gazettedata gzt = new gazettedata();
        string urlalias = cls.urls();
        string url3 = @urlalias + "loadgazette/";
        gzt.type = "loadmedium";

        string jsonString3 = JsonHelper.JsonSerializer<gazettedata>(gzt);
        var httprequest3 = (HttpWebRequest)WebRequest.Create(url3);
        httprequest3.ContentType = "application/json";
        httprequest3.Method = "POST";

        using (var streamWriter3 = new StreamWriter(httprequest3.GetRequestStream()))
        {
            streamWriter3.Write(jsonString3);
            streamWriter3.Flush();
            streamWriter3.Close();
        }

        var httpresponse3 = (HttpWebResponse)httprequest3.GetResponse();
        using (var streamReader3 = new StreamReader(httpresponse3.GetResponseStream()))
        {
            result3 = streamReader3.ReadToEnd();
        }
        return result3;
    }

    [WebMethod]
    public static string Fillclass(string medid)
    {
        string result3;
        Class1 cls = new Class1();
        gazettedata gzt = new gazettedata();
        string urlalias = cls.urls();
        string url3 = @urlalias + "loadgazette/";
        gzt.type = "loadclass";
        gzt.med_id = medid;

        string jsonString3 = JsonHelper.JsonSerializer<gazettedata>(gzt);
        var httprequest3 = (HttpWebRequest)WebRequest.Create(url3);
        httprequest3.ContentType = "application/json";
        httprequest3.Method = "POST";

        using (var streamWriter3 = new StreamWriter(httprequest3.GetRequestStream()))
        {
            streamWriter3.Write(jsonString3);
            streamWriter3.Flush();
            streamWriter3.Close();
        }

        var httpresponse3 = (HttpWebResponse)httprequest3.GetResponse();
        using (var streamReader3 = new StreamReader(httpresponse3.GetResponseStream()))
        {
            result3 = streamReader3.ReadToEnd();
        }

        return result3;


    }

    [WebMethod]
    public static string Filldiv(string medid, string classid, string ayid)
    {
        string result3;
        Class1 cls = new Class1();
        gazettedata gzt = new gazettedata();
        string urlalias = cls.urls();
        string url3 = @urlalias + "loadgazette/";
        gzt.type = "loaddivision";
        gzt.med_id = medid;
        gzt.class_id = classid;
        gzt.ayid = ayid;

        string jsonString3 = JsonHelper.JsonSerializer<gazettedata>(gzt);
        var httprequest3 = (HttpWebRequest)WebRequest.Create(url3);
        httprequest3.ContentType = "application/json";
        httprequest3.Method = "POST";

        using (var streamWriter3 = new StreamWriter(httprequest3.GetRequestStream()))
        {
            streamWriter3.Write(jsonString3);
            streamWriter3.Flush();
            streamWriter3.Close();
        }

        var httpresponse3 = (HttpWebResponse)httprequest3.GetResponse();
        using (var streamReader3 = new StreamReader(httpresponse3.GetResponseStream()))
        {
            result3 = streamReader3.ReadToEnd();
        }

        return result3;


    }

    [WebMethod]
    public static string Fillexam(string medid, string classid, string ayid)
    {
        string result3;
        Class1 cls = new Class1();
        gazettedata gzt = new gazettedata();
        string urlalias = cls.urls();
        string url3 = @urlalias + "loadgazette/";
        gzt.type = "loadexam";
        gzt.med_id = medid;
        gzt.class_id = classid;
        gzt.ayid = ayid;

        string jsonString3 = JsonHelper.JsonSerializer<gazettedata>(gzt);
        var httprequest3 = (HttpWebRequest)WebRequest.Create(url3);
        httprequest3.ContentType = "application/json";
        httprequest3.Method = "POST";

        using (var streamWriter3 = new StreamWriter(httprequest3.GetRequestStream()))
        {
            streamWriter3.Write(jsonString3);
            streamWriter3.Flush();
            streamWriter3.Close();
        }

        var httpresponse3 = (HttpWebResponse)httprequest3.GetResponse();
        using (var streamReader3 = new StreamReader(httpresponse3.GetResponseStream()))
        {
            result3 = streamReader3.ReadToEnd();
        }

        return result3;


    }

    [WebMethod]
    public static string loaddata(string medid, string classid, string ayid, string examid, string division,string groupid)
    {
        string result3;
        Class1 cls = new Class1();
        gazettedata gzt = new gazettedata();
        string urlalias = cls.urls();
        string url3 = @urlalias + "loadgazette/";
        gzt.type = "loaddata";
        gzt.med_id = medid;
        gzt.class_id = classid;
        gzt.ayid = ayid;
        gzt.examid = examid;       
        gzt.division = division;
        gzt.groupid = groupid;

        string jsonString3 = JsonHelper.JsonSerializer<gazettedata>(gzt);
        var httprequest3 = (HttpWebRequest)WebRequest.Create(url3);
        httprequest3.ContentType = "application/json";
        httprequest3.Method = "POST";

        using (var streamWriter3 = new StreamWriter(httprequest3.GetRequestStream()))
        {
            streamWriter3.Write(jsonString3);
            streamWriter3.Flush();
            streamWriter3.Close();
        }

        var httpresponse3 = (HttpWebResponse)httprequest3.GetResponse();
        using (var streamReader3 = new StreamReader(httpresponse3.GetResponseStream()))
        {
            result3 = streamReader3.ReadToEnd();
        }

        return result3;


    }

    [WebMethod]
    public static string loaddatawithoutformula(string medid, string classid, string ayid, string examid, string division,string groupid)
    {
        string result3;
        Class1 cls = new Class1();
        gazettedata gzt = new gazettedata();
        string urlalias = cls.urls();
        string url3 = @urlalias + "loadgazette/";
        gzt.type = "loaddatawithoutformula";
        gzt.med_id = medid;
        gzt.class_id = classid;
        gzt.ayid = ayid;
        gzt.examid = examid;
        gzt.division = division;
        gzt.groupid = groupid;
       

        string jsonString3 = JsonHelper.JsonSerializer<gazettedata>(gzt);
        var httprequest3 = (HttpWebRequest)WebRequest.Create(url3);
        httprequest3.ContentType = "application/json";
        httprequest3.Method = "POST";

        using (var streamWriter3 = new StreamWriter(httprequest3.GetRequestStream()))
        {
            streamWriter3.Write(jsonString3);
            streamWriter3.Flush();
            streamWriter3.Close();
        }

        var httpresponse3 = (HttpWebResponse)httprequest3.GetResponse();
        using (var streamReader3 = new StreamReader(httpresponse3.GetResponseStream()))
        {
            result3 = streamReader3.ReadToEnd();
        }

        return result3;


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
}