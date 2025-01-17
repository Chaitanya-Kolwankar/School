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
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class social_category : System.Web.UI.Page
{
    soc_cat socat = new soc_cat();
    Class1 cls = new Class1();
    StringBuilder sb = new StringBuilder();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["emp_id"]) == "")
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    ddlfillstd();
                }
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


        public static T JsonDeserialize<T>(string jsonString)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T obj = (T)ser.ReadObject(ms);
            return obj;
        }
    }

    public void ddlfillstd()
    {
        try
        {
            common cm = new common();
            DataSet dataSet = new DataSet();
            string urlalias = cls.urls();
            string url = @urlalias + "Common/";

            cm.type = "ddlfill";
            cm.year = Session["acdyear"].ToString();

            string jsonString = JsonHelper.JsonSerializer<common>(cm);
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
                dataSet = JsonConvert.DeserializeObject<DataSet>(result);
                Session["DSLIST"] = dataSet;
                ddl_medium.DataSource = dataSet.Tables[0];
                ddl_medium.DataTextField = "medium";
                ddl_medium.DataValueField = "med_id";
                ddl_medium.Items.Insert(0, "--Select--");
                ddl_medium.DataBind();
                ddl_medium.Items.Insert(0, "--Select--");
                ddl_medium.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            //Response.Redirect("Login.aspx");
        }
    }//done

    protected void btn_view_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddl_medium.SelectedIndex != 0)
            {
                soc_cat socat = new soc_cat();

                DataSet dst1 = new DataSet();
                string urlalias = cls.urls();
                string url = @urlalias + "socialcategory/";

                socat.type = "religioncategory";
                socat.med_id = ddl_medium.SelectedValue;

                string jsonString = JsonHelper.JsonSerializer<soc_cat>(socat);

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
                    dst1 = JsonConvert.DeserializeObject<DataSet>(result);
                    {
                        if (dst1.Tables[0].Rows.Count > 0)
                        {
                            //category
                            DataSet dst2 = new DataSet();
                            string urlalias2 = cls.urls();
                            string url2 = @urlalias2 + "socialcategory/";

                            socat.type = "class";
                            socat.med_id = ddl_medium.SelectedValue;

                            string jsonString2 = JsonHelper.JsonSerializer<soc_cat>(socat);

                            var httprequest2 = (HttpWebRequest)WebRequest.Create(url2);
                            httprequest2.ContentType = "application/json";
                            httprequest2.Method = "POST";

                            using (var streamWriter2 = new StreamWriter(httprequest2.GetRequestStream()))
                            {
                                streamWriter2.Write(jsonString2);
                                streamWriter2.Flush();
                                streamWriter2.Close();
                            }

                            var httpresponse2 = (HttpWebResponse)httprequest2.GetResponse();
                            using (var streamReader2 = new StreamReader(httpresponse2.GetResponseStream()))
                            {
                                string result2 = streamReader2.ReadToEnd();
                                dst2 = JsonConvert.DeserializeObject<DataSet>(result2);
                                {
                                    if (dst2.Tables[0].Rows.Count > 0)
                                    {
                                        for (int i = 0; i < dst1.Tables[0].Rows.Count; i++)
                                        {
                                            string th1 = "";
                                            string th2 = "";
                                            string td1 = "";
                                            string stud_cat = dst1.Tables[0].Rows[i]["category_id"].ToString();
                                            if (i == 0)
                                            {

                                                string finalg = ""; string finalb = "";
                                                int firstrow = 1 + (dst2.Tables[0].Rows.Count * 2);
                                                string category = dst1.Tables[0].Rows[i]["category_name"].ToString();
                                                for (int j = 0; j < dst2.Tables[0].Rows.Count; j++)
                                                {
                                                    DataSet dst3 = new DataSet();
                                                    string urlalias3 = cls.urls();
                                                    string url3 = @urlalias3 + "socialcategory/";

                                                    socat.type = "category";
                                                    socat.med_id = ddl_medium.SelectedValue;
                                                    socat.class_id = dst2.Tables[0].Rows[j]["std_id"].ToString();
                                                    socat.Ayid = Session["acdyear"].ToString();
                                                    socat.category_id = dst1.Tables[0].Rows[i]["category_id"].ToString();
                                                    //socat.currdate = dateprint.Text;

                                                    string jsonString3 = JsonHelper.JsonSerializer<soc_cat>(socat);
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
                                                        string result3 = streamReader3.ReadToEnd();
                                                        dst3 = JsonConvert.DeserializeObject<DataSet>(result3);

                                                        if (dst3.Tables[0].Rows.Count > 0)
                                                        {
                                                            if (stud_cat == dst3.Tables[0].Rows[0]["category_id"].ToString())
                                                            {
                                                                // $("[id*='txtbc" + std + "" + lblage + "']").val(data.Table[z].Noofmale);
                                                                if (dst3.Tables[0].Rows[0]["countf"].ToString() == "")
                                                                {
                                                                    //document.getElementById("txtbc" + std + "" + lblage + "").innerText = 0;
                                                                    finalg = "0";
                                                                }
                                                                else
                                                                {
                                                                    //document.getElementById("txtbc" + std + "" + lblage + "").innerText = data.Table[z].Noofmale;
                                                                    finalg = dst3.Tables[0].Rows[0]["countf"].ToString();
                                                                    //gtotal = Convert.ToString(Convert.ToInt32(gtotal) + Convert.ToInt32(finalg));
                                                                }
                                                            }
                                                            else
                                                            {
                                                                finalg = "0";
                                                            }
                                                        }
                                                        else
                                                        {
                                                            finalg = "0";
                                                        }

                                                        if (dst3.Tables[1].Rows.Count > 0)
                                                        {
                                                            if (stud_cat == dst3.Tables[1].Rows[0]["category_id"].ToString())
                                                            {
                                                                // $("[id*='txtbc" + std + "" + lblage + "']").val(data.Table[z].Noofmale);
                                                                if (dst3.Tables[1].Rows[0]["countm"].ToString() == "")
                                                                {
                                                                    //document.getElementById("txtbc" + std + "" + lblage + "").innerText = 0;
                                                                    finalb = "0";
                                                                }
                                                                else
                                                                {
                                                                    //document.getElementById("txtbc" + std + "" + lblage + "").innerText = data.Table[z].Noofmale;
                                                                    finalb = dst3.Tables[1].Rows[0]["countm"].ToString();
                                                                    //btotal = Convert.ToString(Convert.ToInt32(btotal) + Convert.ToInt32(finalb));
                                                                }
                                                            }
                                                            else
                                                            {
                                                                finalb = "0";
                                                            }
                                                        }
                                                        else
                                                        {
                                                            finalb = "0";
                                                        }



                                                        if (th1 == "" || th2 == "" || td1 == "")
                                                        {
                                                            th1 = "<th style='width: 90px;font-weight: bold;border:1px solid black' colspan='2' runat='server'><center>Class " + dst2.Tables[0].Rows[j]["std_name"].ToString() + "</center></th>";
                                                            th2 = "<th style='width: 90px;font-weight: bold;border:1px solid black' ><center>Boys</center></th><th style='width: 90px;font-weight: bold;border:1px solid black' ><center>Girls</center></th>";
                                                            td1 = "<td style='width: 90px;font-weight: 400;border:1px solid black'  runat='server'><center>" + finalb + "</center></td><td style='width: 90px;font-weight: 400;border:1px solid black' runat='server'><center>" + finalg + "</center></td>";
                                                        }
                                                        else
                                                        {
                                                            th1 = th1 + "<th style='width: 90px;font-weight: bold;border:1px solid black' colspan='2' runat='server'><center>Class " + dst2.Tables[0].Rows[j]["std_name"].ToString() + "</center></th>";
                                                            th2 = th2 + "<th style='width: 90px;font-weight: bold;border:1px solid black' ><center>Boys</center></th><th style='width: 90px;font-weight: 400;border:1px solid black;' ><center>Girls</center></th>";
                                                            td1 = td1 + "<td style='width: 90px;font-weight: 400;border:1px solid black'  runat='server' ><center>" + finalb + "</center></td><td style='width: 90px;font-weight: 400;border:1px solid black' runat='server' ><center>" + finalg + "</center></td>";
                                                        }
                                                    }
                                                    //sb.Append("<tr ><td style='width: 90px;font-weight: 400;border:1px solid black' id='txtagemin' runat='server'><center><5</center></td>" + td1 + "</tr>");

                                                }
                                                sb.Append("<table id='tblsocial' style='width:100%;border:2px solid black' class='table table-bordered'>");
                                                sb.Append("<thead><tr ><th style='width: 90px;font-weight: bold;border:1px solid black' colspan='" + firstrow + "'><center>(a) By Social Category</center></th></tr></thead>");
                                                sb.Append("<thead><tr ><th style='width: 90px;font-weight: bold;border:1px solid black'><center>Class</center></th>" + th1 + "</tr></thead>");
                                                sb.Append("<thead><tr ><th style='width: 90px;font-weight: bold;border:1px solid black'><center></center></th>" + th2 + "</tr></thead>");
                                                sb.Append("<tbody>");
                                                sb.Append("<tr ><td style='width: 90px;font-weight: 400;border:1px solid black'  runat='server'><center>" + category + "</center></td>" + td1 + "</tr>");

                                            }

                                            else if (i == dst1.Tables[0].Rows.Count - 1)
                                            {
                                                string td2 = ""; string btotal = ""; string gtotal = "";
                                                for (int j = 0; j < dst2.Tables[0].Rows.Count; j++)
                                                {

                                                    DataSet dst3 = new DataSet();
                                                    string urlalias3 = cls.urls();
                                                    string url3 = @urlalias3 + "socialcategory/";

                                                    socat.type = "category";
                                                    socat.med_id = ddl_medium.SelectedValue;
                                                    socat.class_id = dst2.Tables[0].Rows[j]["std_id"].ToString();
                                                    socat.Ayid = Session["acdyear"].ToString();
                                                    socat.category_id = dst1.Tables[0].Rows[i]["category_id"].ToString();
                                                    //socat.currdate = dateprint.Text;

                                                    string jsonString3 = JsonHelper.JsonSerializer<soc_cat>(socat);
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
                                                        string result3 = streamReader3.ReadToEnd();
                                                        dst3 = JsonConvert.DeserializeObject<DataSet>(result3);



                                                        if (dst3.Tables[3].Rows.Count > 0)
                                                        {
                                                            if (dst3.Tables[3].Rows[0]["countmtotal"].ToString() == "")
                                                            {
                                                                btotal = "0";
                                                            }
                                                            else
                                                            {
                                                                btotal = dst3.Tables[3].Rows[0]["countmtotal"].ToString();
                                                            }
                                                        }
                                                        else
                                                        {
                                                            btotal = "0";
                                                        }
                                                        if (dst3.Tables[2].Rows.Count > 0)
                                                        {
                                                            if (dst3.Tables[2].Rows[0]["countftotal"].ToString() == "")
                                                            {
                                                                gtotal = "0";
                                                            }
                                                            else
                                                            {
                                                                gtotal = dst3.Tables[2].Rows[0]["countftotal"].ToString();
                                                            }
                                                        }
                                                        else
                                                        {
                                                            gtotal = "0";
                                                        }


                                                        if (td2 == "")
                                                        {
                                                            //th1 = "<th style='width: 90px;font-weight: 400;' colspan='2'><center>Class " + r.Table[j].std_name + "</center></th>";
                                                            //th2 = "<th style='width: 90px;font-weight: 400;' ><center>Boys</center></th><th style='width: 90px;font-weight: 400;' ><center>Girls</center></th>";
                                                            //td1 = "<td style='width: 90px;font-weight: 400;border:1px solid black' id='txtbc" + studcls + "max' runat='server' class='tdbg' ><center>" + finalbmax + "</center></td><td style='width: 90px;font-weight: 400;border:1px solid black' id='txtgc" + studcls + "max' runat='server' class='tdbg'><center>" + finalgmax + "</center></td>";
                                                            td2 = "<td   runat='server' style='width: 90px;font-weight: 400;border:1px solid black'><center>" + btotal + "</center></td><td style='width: 90px;font-weight: 400;border:1px solid black'  runat='server'><center>" + gtotal + "</center></td>";
                                                        }
                                                        else
                                                        {

                                                            //td1 = td1 + "<td style='width: 90px;font-weight: 400;border:1px solid black' id='txtbc" + studcls + "max' runat='server' class='tdbg'><center>" + finalbmax + "</center></td><td style='width: 90px;font-weight: 400;border:1px solid black' id='txtgc" + studcls + "max' runat='server' class='tdbg'><center>" + finalgmax + "</center></td>";
                                                            td2 = td2 + "<td style='width: 90px;font-weight: 400;border:1px solid black'  runat='server'><center>" + btotal + "</center></td><td style='width: 90px;font-weight: 400;border:1px solid black' runat='server'><center>" + gtotal + "</center></td>";
                                                        }
                                                    }
                                                    // sb.Append("<tr ><td style='width: 90px;font-weight: 400;border:1px solid black' id='txtagemax' runat='server'><center>>22</center></td>" + td1 + "</tr>");

                                                }
                                                sb.Append("<tr ><td style='width: 90px;font-weight: 400;border:1px solid black' id='txttot' runat='server'><center>Total</center></td>" + td2 + "</tr>");

                                            }
                                            else
                                            {
                                                string td3 = "";
                                                string category = dst1.Tables[0].Rows[i]["category_name"].ToString();
                                                for (int j = 0; j < dst2.Tables[0].Rows.Count; j++)
                                                {
                                                    DataSet dst3 = new DataSet();
                                                    string urlalias3 = cls.urls();
                                                    string url3 = @urlalias3 + "socialcategory/";

                                                    socat.type = "category";
                                                    socat.med_id = ddl_medium.SelectedValue;
                                                    socat.class_id = dst2.Tables[0].Rows[j]["std_id"].ToString();
                                                    socat.Ayid = Session["acdyear"].ToString();
                                                    socat.category_id = dst1.Tables[0].Rows[i]["category_id"].ToString();
                                                    //socat.currdate = dateprint.Text;

                                                    string jsonString3 = JsonHelper.JsonSerializer<soc_cat>(socat);
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
                                                        string result3 = streamReader3.ReadToEnd();
                                                        dst3 = JsonConvert.DeserializeObject<DataSet>(result3);
                                                        string finalg = ""; string finalb = "";
                                                        //for (var z = 0; z < dst3.Tables[1].Rows.Count; z++)
                                                        //{
                                                        if (dst3.Tables[0].Rows.Count > 0)
                                                        {
                                                            if (stud_cat == dst3.Tables[0].Rows[0]["category_id"].ToString())
                                                            {
                                                                // $("[id*='txtbc" + std + "" + lblage + "']").val(data.Table[z].Noofmale);
                                                                if (dst3.Tables[0].Rows[0]["countf"].ToString() == "")
                                                                {
                                                                    //document.getElementById("txtbc" + std + "" + lblage + "").innerText = 0;
                                                                    finalg = "0";
                                                                }
                                                                else
                                                                {
                                                                    //document.getElementById("txtbc" + std + "" + lblage + "").innerText = data.Table[z].Noofmale;
                                                                    finalg = dst3.Tables[0].Rows[0]["countf"].ToString();
                                                                    //gtotal = Convert.ToString(Convert.ToInt32(gtotal) + Convert.ToInt32(finalg));
                                                                }
                                                            }
                                                            else
                                                            {
                                                                finalg = "0";
                                                            }

                                                        }
                                                        else
                                                        {
                                                            finalg = "0";
                                                        }

                                                        //}
                                                        //for (var y = 0; y < dst3.Tables[0].Rows.Count; y++)
                                                        //{
                                                        if (dst3.Tables[1].Rows.Count > 0)
                                                        {
                                                            if (stud_cat == dst3.Tables[1].Rows[0]["category_id"].ToString())
                                                            {
                                                                // $("[id*='txtbc" + std + "" + lblage + "']").val(data.Table[z].Noofmale);
                                                                if (dst3.Tables[1].Rows[0]["countm"].ToString() == "")
                                                                {
                                                                    //document.getElementById("txtbc" + std + "" + lblage + "").innerText = 0;
                                                                    finalb = "0";
                                                                }
                                                                else
                                                                {
                                                                    //document.getElementById("txtbc" + std + "" + lblage + "").innerText = data.Table[z].Noofmale;
                                                                    finalb = dst3.Tables[1].Rows[0]["countm"].ToString();
                                                                    //btotal = Convert.ToString(Convert.ToInt32(btotal) + Convert.ToInt32(finalb));
                                                                }
                                                            }
                                                            else
                                                            {
                                                                finalb = "0";
                                                            }
                                                        }
                                                        else
                                                        {
                                                            finalb = "0";
                                                        }
                                                        //}
                                                        if (td3 == "")
                                                        {

                                                            td3 = "<td style='width: 90px;font-weight: 400;border:1px solid black;'  runat='server' ><center>" + finalb + "</center></td><td style='width: 90px;font-weight: 400;border:1px solid black'  runat='server' ><center>" + finalg + "</center></td>";
                                                        }
                                                        else
                                                        {

                                                            td3 = td3 + "<td style='width: 90px;font-weight: 400;border:1px solid black;' runat='server' ><center>" + finalb + "</center></td><td style='width: 90px;font-weight: 400;border:1px solid black;' runat='server' ><center>" + finalg + "</center></td>";
                                                        }
                                                    }

                                                }
                                                sb.Append("<tr ><td style='width: 90px;font-weight: 400;border:1px solid black;' runat='server'><center>" + category + "</center></td>" + td3 + "</tr>");
                                            }

                                        }

                                    }
                                }
                            }
                        }


                        if (dst1.Tables[1].Rows.Count > 0)
                        {
                            DataSet dst2 = new DataSet();
                            string urlalias2 = cls.urls();
                            string url2 = @urlalias2 + "socialcategory/";

                            socat.type = "class";
                            socat.med_id = ddl_medium.SelectedValue;

                            string jsonString2 = JsonHelper.JsonSerializer<soc_cat>(socat);

                            var httprequest2 = (HttpWebRequest)WebRequest.Create(url2);
                            httprequest2.ContentType = "application/json";
                            httprequest2.Method = "POST";

                            using (var streamWriter2 = new StreamWriter(httprequest2.GetRequestStream()))
                            {
                                streamWriter2.Write(jsonString2);
                                streamWriter2.Flush();
                                streamWriter2.Close();
                            }

                            var httpresponse2 = (HttpWebResponse)httprequest2.GetResponse();
                            using (var streamReader2 = new StreamReader(httpresponse2.GetResponseStream()))
                            {
                                string result2 = streamReader2.ReadToEnd();
                                dst2 = JsonConvert.DeserializeObject<DataSet>(result2);
                                {
                                    if (dst2.Tables[0].Rows.Count > 0)
                                    {

                                        for (int i = 0; i < dst1.Tables[1].Rows.Count; i++)
                                        {

                                            string td1 = "";
                                            string stud_rel = dst1.Tables[1].Rows[i]["religion_id"].ToString();
                                            if (i == 0)
                                            {
                                                string finalg = ""; string finalb = "";
                                                int firstrow = 1 + (dst2.Tables[0].Rows.Count * 2);
                                                string religion = dst1.Tables[1].Rows[i]["religion"].ToString();
                                                for (int j = 0; j < dst2.Tables[0].Rows.Count; j++)
                                                {
                                                    DataSet dst3 = new DataSet();
                                                    string urlalias3 = cls.urls();
                                                    string url3 = @urlalias3 + "socialcategory/";

                                                    socat.type = "religion";
                                                    socat.med_id = ddl_medium.SelectedValue;
                                                    socat.class_id = dst2.Tables[0].Rows[j]["std_id"].ToString();
                                                    socat.Ayid = Session["acdyear"].ToString();
                                                    socat.religion_id = dst1.Tables[1].Rows[i]["religion_id"].ToString();
                                                    //socat.currdate = dateprint.Text;

                                                    string jsonString3 = JsonHelper.JsonSerializer<soc_cat>(socat);
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
                                                        string result3 = streamReader3.ReadToEnd();
                                                        dst3 = JsonConvert.DeserializeObject<DataSet>(result3);

                                                        if (dst3.Tables[0].Rows.Count > 0)
                                                        {
                                                            if (stud_rel == dst3.Tables[0].Rows[0]["religion_id"].ToString())
                                                            {
                                                                // $("[id*='txtbc" + std + "" + lblage + "']").val(data.Table[z].Noofmale);
                                                                if (dst3.Tables[0].Rows[0]["countf"].ToString() == "")
                                                                {
                                                                    //document.getElementById("txtbc" + std + "" + lblage + "").innerText = 0;
                                                                    finalg = "0";
                                                                }
                                                                else
                                                                {
                                                                    //document.getElementById("txtbc" + std + "" + lblage + "").innerText = data.Table[z].Noofmale;
                                                                    finalg = dst3.Tables[0].Rows[0]["countf"].ToString();
                                                                    //gtotal = Convert.ToString(Convert.ToInt32(gtotal) + Convert.ToInt32(finalg));
                                                                }
                                                            }
                                                            else
                                                            {
                                                                finalg = "0";
                                                            }
                                                        }
                                                        else
                                                        {
                                                            finalg = "0";
                                                        }

                                                        if (dst3.Tables[1].Rows.Count > 0)
                                                        {
                                                            if (stud_rel == dst3.Tables[1].Rows[0]["religion_id"].ToString())
                                                            {
                                                                // $("[id*='txtbc" + std + "" + lblage + "']").val(data.Table[z].Noofmale);
                                                                if (dst3.Tables[1].Rows[0]["countm"].ToString() == "")
                                                                {
                                                                    //document.getElementById("txtbc" + std + "" + lblage + "").innerText = 0;
                                                                    finalb = "0";
                                                                }
                                                                else
                                                                {
                                                                    //document.getElementById("txtbc" + std + "" + lblage + "").innerText = data.Table[z].Noofmale;
                                                                    finalb = dst3.Tables[1].Rows[0]["countm"].ToString();
                                                                    //btotal = Convert.ToString(Convert.ToInt32(btotal) + Convert.ToInt32(finalb));
                                                                }
                                                            }
                                                            else
                                                            {
                                                                finalb = "0";
                                                            }
                                                        }
                                                        else
                                                        {
                                                            finalb = "0";
                                                        }



                                                        if (td1 == "")
                                                        {
                                                            //th1 = "<th style='width: 90px;font-weight: bold;border:1px solid black' colspan='2' runat='server'><center>Class " + dst2.Tables[0].Rows[j]["std_name"].ToString() + "</center></th>";
                                                            //th2 = "<th style='width: 90px;font-weight: bold;border:1px solid black' ><center>Boys</center></th><th style='width: 90px;font-weight: bold;border:1px solid black' ><center>Girls</center></th>";
                                                            td1 = "<td style='width: 90px;font-weight: 400;border:1px solid black'  runat='server'><center>" + finalb + "</center></td><td style='width: 90px;font-weight: 400;border:1px solid black' runat='server'><center>" + finalg + "</center></td>";
                                                        }
                                                        else
                                                        {
                                                            //th1 = th1 + "<th style='width: 90px;font-weight: bold;border:1px solid black' colspan='2' runat='server'><center>Class " + dst2.Tables[0].Rows[j]["std_name"].ToString() + "</center></th>";
                                                            //th2 = th2 + "<th style='width: 90px;font-weight: bold;border:1px solid black' ><center>Boys</center></th><th style='width: 90px;font-weight: 400;border:1px solid black;' ><center>Girls</center></th>";
                                                            td1 = td1 + "<td style='width: 90px;font-weight: 400;border:1px solid black'  runat='server' ><center>" + finalb + "</center></td><td style='width: 90px;font-weight: 400;border:1px solid black' runat='server' ><center>" + finalg + "</center></td>";
                                                        }
                                                    }
                                                    //sb.Append("<tr ><td style='width: 90px;font-weight: 400;border:1px solid black' id='txtagemin' runat='server'><center><5</center></td>" + td1 + "</tr>");

                                                }
                                                //sb.Append("<table id='tblsocial' style='width:100%;border:2px solid black' class='table table-bordered'>");
                                                sb.Append("<thead><tr ><th style='width: 90px;font-weight: bold;border:1px solid black' colspan='" + firstrow + "'><center>(b) <b>Out of the total enrollment</b> provide details of enrolment belonging to following Minority groups*</center></th></tr></thead>");
                                                //sb.Append("<thead><tr ><th style='width: 90px;font-weight: bold;border:1px solid black'><center>Class</center></th>" + th1 + "</tr></thead>");
                                                //sb.Append("<thead><tr ><th style='width: 90px;font-weight: bold;border:1px solid black'><center></center></th>" + th2 + "</tr></thead>");
                                                //sb.Append("<tbody>");
                                                sb.Append("<tr ><td style='width: 90px;font-weight: 400;border:1px solid black'  runat='server'><center>" + religion + "</center></td>" + td1 + "</tr>");

                                            }
                                            else
                                            {
                                                string td3 = "";
                                                string religion = dst1.Tables[1].Rows[i]["religion"].ToString();
                                                for (int j = 0; j < dst2.Tables[0].Rows.Count; j++)
                                                {
                                                    DataSet dst3 = new DataSet();
                                                    string urlalias3 = cls.urls();
                                                    string url3 = @urlalias3 + "socialcategory/";

                                                    socat.type = "religion";
                                                    socat.med_id = ddl_medium.SelectedValue;
                                                    socat.class_id = dst2.Tables[0].Rows[j]["std_id"].ToString();
                                                    socat.Ayid = Session["acdyear"].ToString();
                                                    socat.religion_id = dst1.Tables[1].Rows[i]["religion_id"].ToString();
                                                    //socat.currdate = dateprint.Text;

                                                    string jsonString3 = JsonHelper.JsonSerializer<soc_cat>(socat);
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
                                                        string result3 = streamReader3.ReadToEnd();
                                                        dst3 = JsonConvert.DeserializeObject<DataSet>(result3);
                                                        string finalg = ""; string finalb = "";
                                                        //for (var z = 0; z < dst3.Tables[1].Rows.Count; z++)
                                                        //{
                                                        if (dst3.Tables[0].Rows.Count > 0)
                                                        {
                                                            if (stud_rel == dst3.Tables[0].Rows[0]["religion_id"].ToString())
                                                            {
                                                                // $("[id*='txtbc" + std + "" + lblage + "']").val(data.Table[z].Noofmale);
                                                                if (dst3.Tables[0].Rows[0]["countf"].ToString() == "")
                                                                {
                                                                    //document.getElementById("txtbc" + std + "" + lblage + "").innerText = 0;
                                                                    finalg = "0";
                                                                }
                                                                else
                                                                {
                                                                    //document.getElementById("txtbc" + std + "" + lblage + "").innerText = data.Table[z].Noofmale;
                                                                    finalg = dst3.Tables[0].Rows[0]["countf"].ToString();
                                                                    //gtotal = Convert.ToString(Convert.ToInt32(gtotal) + Convert.ToInt32(finalg));
                                                                }
                                                            }
                                                            else
                                                            {
                                                                finalg = "0";
                                                            }

                                                        }
                                                        else
                                                        {
                                                            finalg = "0";
                                                        }

                                                        //}
                                                        //for (var y = 0; y < dst3.Tables[0].Rows.Count; y++)
                                                        //{
                                                        if (dst3.Tables[1].Rows.Count > 0)
                                                        {
                                                            if (stud_rel == dst3.Tables[1].Rows[0]["religion_id"].ToString())
                                                            {
                                                                // $("[id*='txtbc" + std + "" + lblage + "']").val(data.Table[z].Noofmale);
                                                                if (dst3.Tables[1].Rows[0]["countm"].ToString() == "")
                                                                {
                                                                    //document.getElementById("txtbc" + std + "" + lblage + "").innerText = 0;
                                                                    finalb = "0";
                                                                }
                                                                else
                                                                {
                                                                    //document.getElementById("txtbc" + std + "" + lblage + "").innerText = data.Table[z].Noofmale;
                                                                    finalb = dst3.Tables[1].Rows[0]["countm"].ToString();
                                                                    //btotal = Convert.ToString(Convert.ToInt32(btotal) + Convert.ToInt32(finalb));
                                                                }
                                                            }
                                                            else
                                                            {
                                                                finalb = "0";
                                                            }
                                                        }
                                                        else
                                                        {
                                                            finalb = "0";
                                                        }
                                                        //}
                                                        if (td3 == "")
                                                        {

                                                            td3 = "<td style='width: 90px;font-weight: 400;border:1px solid black;'  runat='server' ><center>" + finalb + "</center></td><td style='width: 90px;font-weight: 400;border:1px solid black'  runat='server' ><center>" + finalg + "</center></td>";
                                                        }
                                                        else
                                                        {

                                                            td3 = td3 + "<td style='width: 90px;font-weight: 400;border:1px solid black;' runat='server' ><center>" + finalb + "</center></td><td style='width: 90px;font-weight: 400;border:1px solid black;' runat='server' ><center>" + finalg + "</center></td>";
                                                        }
                                                    }

                                                }
                                                sb.Append("<tr ><td style='width: 90px;font-weight: 400;border:1px solid black;' runat='server'><center>" + religion + "</center></td> " + td3 + "</tr>");

                                            }
                                        }

                                    }
                                }

                            }

                        }

                    }
                }


                DataSet dst4 = new DataSet();
                string urlalias4 = cls.urls();
                string url4 = @urlalias4 + "socialcategory/";

                socat.type = "class";
                socat.med_id = ddl_medium.SelectedValue;

                string jsonString4 = JsonHelper.JsonSerializer<soc_cat>(socat);

                var httprequest4 = (HttpWebRequest)WebRequest.Create(url4);
                httprequest4.ContentType = "application/json";
                httprequest4.Method = "POST";

                using (var streamWriter4 = new StreamWriter(httprequest4.GetRequestStream()))
                {
                    streamWriter4.Write(jsonString4);
                    streamWriter4.Flush();
                    streamWriter4.Close();
                }

                var httpresponse4 = (HttpWebResponse)httprequest4.GetResponse();
                using (var streamReader4 = new StreamReader(httpresponse4.GetResponseStream()))
                {
                    string result4 = streamReader4.ReadToEnd();
                    dst4 = JsonConvert.DeserializeObject<DataSet>(result4);
                    {
                        if (dst4.Tables[0].Rows.Count > 0)
                        {
                            string td1 = ""; string td2 = "";
                            int firstrow = 1 + (dst4.Tables[0].Rows.Count * 2);
                            for (int j = 0; j < dst4.Tables[0].Rows.Count; j++)
                            {
                                DataSet dst3 = new DataSet();
                                string urlalias3 = cls.urls();
                                string url3 = @urlalias3 + "socialcategory/";

                                socat.type = "aadharbpl";
                                socat.med_id = ddl_medium.SelectedValue;
                                socat.class_id = dst4.Tables[0].Rows[j]["std_id"].ToString();
                                socat.Ayid = Session["acdyear"].ToString();
                                //socat.religion_id = dst1.Tables[1].Rows[i]["religion_id"].ToString();
                                //socat.currdate = dateprint.Text;

                                string jsonString3 = JsonHelper.JsonSerializer<soc_cat>(socat);
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
                                    string result3 = streamReader3.ReadToEnd();
                                    dst3 = JsonConvert.DeserializeObject<DataSet>(result3);
                                    string finalg = ""; string finalb = "";
                                    //for (var z = 0; z < dst3.Tables[1].Rows.Count; z++)
                                    //{
                                    if (dst3.Tables[0].Rows.Count > 0)
                                    {

                                        // $("[id*='txtbc" + std + "" + lblage + "']").val(data.Table[z].Noofmale);
                                        if (dst3.Tables[0].Rows[0]["countf"].ToString() == "")
                                        {
                                            //document.getElementById("txtbc" + std + "" + lblage + "").innerText = 0;
                                            finalg = "0";
                                        }
                                        else
                                        {
                                            //document.getElementById("txtbc" + std + "" + lblage + "").innerText = data.Table[z].Noofmale;
                                            finalg = dst3.Tables[0].Rows[0]["countf"].ToString();
                                            //gtotal = Convert.ToString(Convert.ToInt32(gtotal) + Convert.ToInt32(finalg));
                                        }


                                    }
                                    else
                                    {
                                        finalg = "0";
                                    }
                                    if (dst3.Tables[1].Rows.Count > 0)
                                    {

                                        // $("[id*='txtbc" + std + "" + lblage + "']").val(data.Table[z].Noofmale);
                                        if (dst3.Tables[1].Rows[0]["countm"].ToString() == "")
                                        {
                                            //document.getElementById("txtbc" + std + "" + lblage + "").innerText = 0;
                                            finalb = "0";
                                        }
                                        else
                                        {
                                            //document.getElementById("txtbc" + std + "" + lblage + "").innerText = data.Table[z].Noofmale;
                                            finalb = dst3.Tables[1].Rows[0]["countm"].ToString();
                                            //btotal = Convert.ToString(Convert.ToInt32(btotal) + Convert.ToInt32(finalb));
                                        }

                                    }
                                    else
                                    {
                                        finalb = "0";
                                    }
                                    //}
                                    if (td1 == "" || td2 == "")
                                    {

                                        td1 = "<td style='width: 90px;font-weight: 400;border:1px solid black;'  runat='server' ><center>" + finalb + "</center></td><td style='width: 90px;font-weight: 400;border:1px solid black'  runat='server' ><center>" + finalg + "</center></td>";
                                        td2 = "<td style='width: 90px;font-weight: 400;border:1px solid black;'  runat='server' ><center></center></td><td style='width: 90px;font-weight: 400;border:1px solid black'  runat='server' ><center></center></td>";
                                    }
                                    else
                                    {

                                        td1 = td1 + "<td style='width: 90px;font-weight: 400;border:1px solid black;' runat='server' ><center>" + finalb + "</center></td><td style='width: 90px;font-weight: 400;border:1px solid black;' runat='server' ><center>" + finalg + "</center></td>";
                                        td2 = td2 + "<td style='width: 90px;font-weight: 400;border:1px solid black;' runat='server' ><center></center></td><td style='width: 90px;font-weight: 400;border:1px solid black;' runat='server' ><center></center></td>";
                                    }
                                }

                            }
                            sb.Append("<thead><tr ><th style='width: 90px;font-weight: bold;border:1px solid black' colspan='" + firstrow + "'><center>(c) <b>Out of the total enrollment provide number of students </b></center></th></tr></thead>");
                            sb.Append("<tr ><td style='width: 90px;font-weight: 400;border:1px solid black;' runat='server'><center>Having Aadhar</center></td>" + td1 + "</tr>");
                            sb.Append("<tr ><td style='width: 90px;font-weight: 400;border:1px solid black;' runat='server'><center>BPL</center></td>" + td2 + "</tr>");

                        }
                    }
                }

                //transgender

                DataSet dst5 = new DataSet();
                string urlalias5 = cls.urls();
                string url5 = @urlalias5 + "socialcategory/";

                socat.type = "class";
                socat.med_id = ddl_medium.SelectedValue;

                string jsonString5 = JsonHelper.JsonSerializer<soc_cat>(socat);

                var httprequest5 = (HttpWebRequest)WebRequest.Create(url5);
                httprequest5.ContentType = "application/json";
                httprequest5.Method = "POST";

                using (var streamWriter5 = new StreamWriter(httprequest5.GetRequestStream()))
                {
                    streamWriter5.Write(jsonString5);
                    streamWriter5.Flush();
                    streamWriter5.Close();
                }

                var httpresponse5 = (HttpWebResponse)httprequest5.GetResponse();
                using (var streamReader5 = new StreamReader(httpresponse5.GetResponseStream()))
                {
                    string result5 = streamReader5.ReadToEnd();
                    dst5 = JsonConvert.DeserializeObject<DataSet>(result5);
                    {
                        if (dst5.Tables[0].Rows.Count > 0)
                        {
                            string td1 = ""; string td2 = "";
                            int firstrow = 1 + (dst5.Tables[0].Rows.Count * 2);
                            for (int j = 0; j < dst5.Tables[0].Rows.Count; j++)
                            {
                                DataSet dst3 = new DataSet();
                                string urlalias3 = cls.urls();
                                string url3 = @urlalias3 + "socialcategory/";

                                socat.type = "transgender";
                                socat.med_id = ddl_medium.SelectedValue;
                                socat.class_id = dst5.Tables[0].Rows[j]["std_id"].ToString();
                                socat.Ayid = Session["acdyear"].ToString();
                                //socat.religion_id = dst1.Tables[1].Rows[i]["religion_id"].ToString();
                                //socat.currdate = dateprint.Text;

                                string jsonString3 = JsonHelper.JsonSerializer<soc_cat>(socat);
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
                                    string result3 = streamReader3.ReadToEnd();
                                    dst3 = JsonConvert.DeserializeObject<DataSet>(result3);
                                    string finalt = "";

                                    if (dst3.Tables[0].Rows.Count > 0)
                                    {

                                        // $("[id*='txtbc" + std + "" + lblage + "']").val(data.Table[z].Noofmale);
                                        if (dst3.Tables[0].Rows[0]["countt"].ToString() == "")
                                        {
                                            //document.getElementById("txtbc" + std + "" + lblage + "").innerText = 0;
                                            finalt = "0";
                                        }
                                        else
                                        {
                                            //document.getElementById("txtbc" + std + "" + lblage + "").innerText = data.Table[z].Noofmale;
                                            finalt = dst3.Tables[0].Rows[0]["countt"].ToString();
                                            //gtotal = Convert.ToString(Convert.ToInt32(gtotal) + Convert.ToInt32(finalg));
                                        }


                                    }
                                    else
                                    {
                                        finalt = "0";
                                    }


                                    if (td1 == "" || td2 == "")
                                    {

                                        td1 = "<td style='width: 90px;font-weight: 400;border:1px solid black;' colspan='2'  runat='server' ><center>Class " + dst5.Tables[0].Rows[j]["std_name"].ToString() + "</center></td>";
                                        td2 = "<td style='width: 90px;font-weight: 400;border:1px solid black;' colspan='2'   runat='server' ><center>" + finalt + "</center></td>";
                                    }
                                    else
                                    {

                                        td1 = td1 + "<td style='width: 90px;font-weight: 400;border:1px solid black;' colspan='2'  runat='server' ><center>Class " + dst5.Tables[0].Rows[j]["std_name"].ToString() + "</center></td>";
                                        td2 = td2 + "<td style='width: 90px;font-weight: 400;border:1px solid black;' colspan='2'  runat='server' ><center>" + finalt + "</center></td>";
                                    }
                                }

                            }
                            sb.Append("<thead><tr ><th style='width: 90px;font-weight: bold;border:1px solid black' colspan='" + firstrow + "'><center>(d) <b>Out of the total enrollment provide number of Transgender students </b></center></th></tr></thead>");
                            sb.Append("<tr ><td style='width: 90px;font-weight: 400;border:1px solid black;' runat='server'><center></center></td>" + td1 + "</tr>");
                            sb.Append("<tr ><td style='width: 90px;font-weight: 400;border:1px solid black;' runat='server'><center>Transgender</center></td>" + td2 + "</tr>");
                            sb.Append("</tbody></table>");

                            tblsocial.Text = Convert.ToString(sb);
                            btnGetExcel.Style.Add("display", "block");
                        }
                    }
                }
            }
            else
            {
                string display = "";
                if (ddl_medium.SelectedIndex == 0)
                {

                    display = "Please Select Appropriate Medium";
                }
               
                else
                {

                }



                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify( '" + display + "', { color: '#fff', background: 'red', blur: 0.2, delay: 0 })", true);
                return;
            }
        }

        catch (Exception ex)
        {

        }
    }
}