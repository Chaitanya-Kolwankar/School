using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class marks_criteria : System.Web.UI.Page
{
    Class1 cls = new Class1();
    protected void Page_Load(object sender, EventArgs e)
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

    [WebMethod]
    public static string Fillmedium()
    {
        string result3;
        Class1 cls = new Class1();
        marks mrk = new marks();
        string urlalias = cls.urls();
        string url3 = @urlalias + "loadmarks/";
        mrk.type = "loadmedium";

        string jsonString3 = JsonHelper.JsonSerializer<marks>(mrk);
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
        marks mrk = new marks();
        string urlalias = cls.urls();
        string url3 = @urlalias + "loadmarks/";
        mrk.type = "loadclass";
        mrk.med_id = medid;

        string jsonString3 = JsonHelper.JsonSerializer<marks>(mrk);
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
    public static string Fillsub(string medid, string classid, string ayid,string examtype,string examid)
    {
        string result3;
        Class1 cls = new Class1();
        marks mrk = new marks();
        string urlalias = cls.urls();
        string url3 = @urlalias + "loadmarks/";
        mrk.type = "loadsubject";
        mrk.med_id = medid;
        mrk.class_id = classid;
        mrk.ayid = ayid;
        mrk.exam_type = examtype;
        mrk.exam_id = examid;

        string jsonString3 = JsonHelper.JsonSerializer<marks>(mrk);
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
        marks mrk = new marks();
        string urlalias = cls.urls();
        string url3 = @urlalias + "loadmarks/";
        mrk.type = "loadexam";
        mrk.med_id = medid;
        mrk.class_id = classid;
        mrk.ayid = ayid;

        string jsonString3 = JsonHelper.JsonSerializer<marks>(mrk);
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
    public static string checkexam(string medid, string classid, string ayid,string examid,string examtype,string prevayid)
    {
        string result3;
        Class1 cls = new Class1();
        marks mrk = new marks();
        string urlalias = cls.urls();
        string url3 = @urlalias + "loadmarks/";
        mrk.type = "checkexam";
        mrk.med_id = medid;
        mrk.class_id = classid;
        mrk.ayid = ayid;
        mrk.exam_id = examid;
        mrk.exam_type = examtype;
        mrk.prevayid = prevayid;
        string jsonString3 = JsonHelper.JsonSerializer<marks>(mrk);
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
    public static string checksub(string medid, string classid, string ayid, string examid, string examtype, string prevayid,string subjectid)
    {
        string result3;
        Class1 cls = new Class1();
        marks mrk = new marks();
        string urlalias = cls.urls();
        string url3 = @urlalias + "loadmarks/";
        mrk.type = "checksub";
        mrk.med_id = medid;
        mrk.class_id = classid;
        mrk.ayid = ayid;
        mrk.exam_id = examid;
        mrk.exam_type = examtype;
        mrk.prevayid = prevayid;
        mrk.sub_id = subjectid;
        string jsonString3 = JsonHelper.JsonSerializer<marks>(mrk);
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
    public static string checkref(string ayid, string subjectid,string medid,string classid, string refid)
    {
        string result3;
        Class1 cls = new Class1();
        marks mrk = new marks();
        string urlalias = cls.urls();
        string url3 = @urlalias + "loadmarks/";
        mrk.type = "checkref";        
        mrk.ayid = ayid;
        mrk.sub_id = subjectid;
        mrk.med_id = medid;
        mrk.class_id = classid;
        mrk.refid = refid;
        string jsonString3 = JsonHelper.JsonSerializer<marks>(mrk);
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
    public static string fillprevayid( string ayid)
    {
        string result3;
        Class1 cls = new Class1();
        marks mrk = new marks();
        string urlalias = cls.urls();
        string url3 = @urlalias + "loadmarks/";
        mrk.type = "loadprevayid";       
        mrk.ayid = ayid;

        string jsonString3 = JsonHelper.JsonSerializer<marks>(mrk);
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
    public static string Fillexamtype(string medid, string classid, string ayid,string examid)
    {
        string result3;
        Class1 cls = new Class1();
        marks mrk = new marks();
        string urlalias = cls.urls();
        string url3 = @urlalias + "loadmarks/";
        mrk.type = "loadexmtype";
        mrk.med_id = medid;
        mrk.class_id = classid;
        mrk.exam_id=examid;
        mrk.ayid = ayid;

        string jsonString3 = JsonHelper.JsonSerializer<marks>(mrk);
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
    public static string loaddata(string medid, string classid, string ayid,  string subjectid, string examtype, string examid)
    {
        string result3;
        Class1 cls = new Class1();
        marks mrk = new marks();
        string urlalias = cls.urls();
        string url3 = @urlalias + "loadmarks/";
        mrk.type = "loadexam";
        mrk.med_id = medid;
        mrk.class_id = classid;
        mrk.ayid = ayid;
        mrk.sub_id = subjectid;
        mrk.exam_id = examid;
        mrk.exam_type = examtype;

        string jsonString3 = JsonHelper.JsonSerializer<marks>(mrk);
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
    public static string savedata(marks obj)
    {
        string result3;
        Class1 cls = new Class1();
        marks mrk = new marks();
        string urlalias = cls.urls();
        string url3 = @urlalias + "loadmarks/";
       

        string jsonString3 = JsonHelper.JsonSerializer<marks>(obj);
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