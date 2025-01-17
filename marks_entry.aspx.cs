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
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml.Wordprocessing;
using Newtonsoft.Json;

public partial class marks_entry : System.Web.UI.Page
{
    Class1 cls = new Class1();
    marksentry mrk = new marksentry();    
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ConnectionString);
    SqlCommand cmd = new SqlCommand();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack == true)
        {
            if (Convert.ToString(Session["emp_id"]) == "")
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                string url = cls.urls();
                Session["url"] = url;
            }
        }
    }

    [WebMethod]
    public static string Fillmedium()
    {
        string result3;
        Class1 cls = new Class1();
         marksentry mrk = new marksentry();
        string urlalias = cls.urls();
        string url3 = @urlalias + "loadmarksentry/";       
        mrk.type = "loadmedium";

        string jsonString3 = JsonHelper.JsonSerializer<marksentry>(mrk);
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
        return  result3;
    }

    [WebMethod]
    public static string Fillclass(string medid)
    {
        string result3;
        Class1 cls = new Class1();
        marksentry mrk = new marksentry();
        string urlalias = cls.urls();
        string url3 = @urlalias + "loadmarksentry/";
        mrk.type = "loadclass";
        mrk.med_id = medid;

        string jsonString3 = JsonHelper.JsonSerializer<marksentry>(mrk);
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
    public static string Filldiv(string medid,string classid,string ayid)
    {
        string result3;
        Class1 cls = new Class1();
        marksentry mrk = new marksentry();
        string urlalias = cls.urls();
        string url3 = @urlalias + "loadmarksentry/";
        mrk.type = "loaddivision";
        mrk.med_id = medid;
        mrk.class_id = classid;
        mrk.ayid = ayid;

        string jsonString3 = JsonHelper.JsonSerializer<marksentry>(mrk);
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
        marksentry mrk = new marksentry();
        string urlalias = cls.urls();
        string url3 = @urlalias + "loadmarksentry/";
        mrk.type = "loadexam";
        mrk.med_id = medid;
        mrk.class_id = classid;
        mrk.ayid = ayid;

        string jsonString3 = JsonHelper.JsonSerializer<marksentry>(mrk);
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
    public static string fillgrp(string medid, string classid, string ayid)
    {
        string result3;
        Class1 cls = new Class1();
        marksentry mrk = new marksentry();
        string urlalias = cls.urls();
        string url3 = @urlalias + "loadmarksentry/";
        mrk.type = "loadgroup";
        mrk.med_id = medid;
        mrk.class_id = classid;
        mrk.ayid = ayid;

        string jsonString3 = JsonHelper.JsonSerializer<marksentry>(mrk);
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
    public static string Fillsub(string medid, string classid, string ayid,string examid,string groupid)
    {
        string result3;
        Class1 cls = new Class1();
        marksentry mrk = new marksentry();
        string urlalias = cls.urls();
        string url3 = @urlalias + "loadmarksentry/";
        mrk.type = "loadsubject";
        mrk.med_id = medid;
        mrk.class_id = classid;
        mrk.ayid = ayid;
        mrk.examid = examid;
        mrk.groupid = groupid;

        string jsonString3 = JsonHelper.JsonSerializer<marksentry>(mrk);
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
    public static string loaddata(string medid, string classid, string ayid,string examid,string subjectid,string division, string groupid)
    {
        string result3;
        Class1 cls = new Class1();
        marksentry mrk = new marksentry();
        string urlalias = cls.urls();
        string url3 = @urlalias + "loadmarksentry/";
        mrk.type = "loaddata";
        mrk.med_id = medid;
        mrk.class_id = classid;
        mrk.ayid = ayid;
        mrk.examid = examid;
        mrk.subject_id = subjectid;
        mrk.division = division;
        mrk.groupid = groupid;

        string jsonString3 = JsonHelper.JsonSerializer<marksentry>(mrk);
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
    public static string checkexcel(string medid, string classid, string ayid, string examid, string subjectid, string division)
    {
        string result3;
        Class1 cls = new Class1();
        marksentry mrk = new marksentry();
        string urlalias = cls.urls();
        string url3 = @urlalias + "loadmarksentry/";
        mrk.type = "checkexcel";
        mrk.med_id = medid;
        mrk.class_id = classid;
        mrk.ayid = ayid;
        mrk.examid = examid;
        mrk.subject_id = subjectid;
        mrk.division = division;

        string jsonString3 = JsonHelper.JsonSerializer<marksentry>(mrk);
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
    public static string savedata(marksentry obj)
    {
        string result3;
        Class1 cls = new Class1();
        marksentry mrk = new marksentry();
        string urlalias = cls.urls();
        string url3 = @urlalias + "loadmarksentry/";
       

        string jsonString3 = JsonHelper.JsonSerializer<marksentry>(obj);
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


