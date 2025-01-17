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

public partial class Age_report_new : System.Web.UI.Page
{
    agereprt std = new agereprt();
    Class1 cls = new Class1();
    StringBuilder sb = new StringBuilder();
    int add = 0;
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
            if (ddl_medium.SelectedIndex != 0 && dateprint.Text != "")
            {
                agereprt age = new agereprt();
                DataSet dataSet = new DataSet();
                string urlalias = cls.urls();
                string url = @urlalias + "agereport/";

                age.type = "class";
                age.med_id = ddl_medium.SelectedValue;

                string jsonString = JsonHelper.JsonSerializer<agereprt>(age);
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
                    string btotal = "0"; string gtotal = "0";

                    for (int i = 0; i < 20; i++)
                    {
                        int studage = i + 4;

                        if (i == 0)
                        {
                            string th1 = "";
                            string th2 = "";
                            string td1 = "";
                            for (var j = 0; j < dataSet.Tables[0].Rows.Count; j++)
                            {
                                btnGetExcel.Style.Add("display", "block");
                                int studcls = j + 1;


                                DataSet ds = new DataSet();
                                string urlalias1 = cls.urls();
                                string url1 = @urlalias1 + "agereport/";

                                age.type = "age";
                                age.med_id = ddl_medium.SelectedValue;
                                age.class_id = dataSet.Tables[0].Rows[j]["std_id"].ToString();
                                age.Ayid = Session["acdyear"].ToString();
                                age.currdate = dateprint.Text;

                                string jsonString1 = JsonHelper.JsonSerializer<agereprt>(age);
                                var httprequest1 = (HttpWebRequest)WebRequest.Create(url1);
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
                                    ds = JsonConvert.DeserializeObject<DataSet>(result1);
                                    string minb = ""; string ming = ""; string finalgmin = ""; string finalbmin = "";


                                    for (var z = 0; z < ds.Tables[1].Rows.Count; z++)
                                    {

                                        if (Convert.ToInt32(ds.Tables[1].Rows[z]["AgeIntYears"].ToString()) < 5)
                                        {
                                            if (ming == "")
                                            {
                                                ming = ds.Tables[1].Rows[z]["Nooffemale"].ToString();
                                                //$("[id*='txtgc" + std + "min']").val(min);
                                                if (ming == "")
                                                {
                                                    ming = "0";
                                                    // document.getElementById("txtgc" + std + "min").innerText = 0;
                                                }
                                                else
                                                {
                                                    ming = ming;
                                                    // td.InnerText = min;
                                                }
                                                finalgmin = ming;
                                                //gtotal = Convert.ToString(Convert.ToInt32(gtotal) + Convert.ToInt32(finalgmin));
                                            }
                                            else
                                            {
                                                if (ds.Tables[1].Rows[z]["Nooffemale"].ToString() == "")
                                                {
                                                    ming = Convert.ToString(Convert.ToInt32(ming) + 0);
                                                }
                                                else
                                                {
                                                    ming = Convert.ToString(Convert.ToInt32(ming) + Convert.ToInt32(ds.Tables[1].Rows[z]["Nooffemale"].ToString()));
                                                }

                                                /// document.getElementById("txtgc" + std + "min").innerText = min;
                                                finalgmin = ming;
                                                //gtotal = Convert.ToString(Convert.ToInt32(gtotal) + Convert.ToInt32(finalgmin));
                                            }
                                        }
                                    }
                                    for (var y = 0; y < ds.Tables[0].Rows.Count; y++)
                                    {

                                        if (Convert.ToInt32(ds.Tables[0].Rows[y]["AgeIntYears"].ToString()) < 5)
                                        {
                                            if (minb == "")
                                            {
                                                minb = ds.Tables[0].Rows[y]["Noofmale"].ToString();
                                                //$("[id*='txtgc" + std + "min']").val(min);
                                                if (minb == "")
                                                {
                                                    minb = "0";
                                                    // document.getElementById("txtgc" + std + "min").innerText = 0;
                                                }
                                                else
                                                {
                                                    minb = minb;
                                                    // td.InnerText = min;
                                                }
                                                finalbmin = minb;
                                                //btotal = Convert.ToString(Convert.ToInt32(btotal) + Convert.ToInt32(finalbmin));
                                            }
                                            else
                                            {
                                                if (ds.Tables[0].Rows[y]["Noofmale"].ToString() == "")
                                                {
                                                    minb = Convert.ToString(Convert.ToInt32(minb) + 0);
                                                }
                                                else
                                                {
                                                    minb = Convert.ToString(Convert.ToInt32(minb) + Convert.ToInt32(ds.Tables[1].Rows[y]["Noofmale"].ToString()));
                                                }

                                                /// document.getElementById("txtgc" + std + "min").innerText = min;
                                                finalbmin = minb;
                                                // btotal = Convert.ToString(Convert.ToInt32(btotal) + Convert.ToInt32(finalbmin));
                                            }
                                        }


                                    }
                                    if (finalbmin == "")
                                    {
                                        finalbmin = "0";
                                    }
                                    if (finalgmin == "")
                                    {
                                        finalgmin = "0";
                                    }


                                    if (th1 == "" || th2 == "" || td1 == "")
                                    {
                                        th1 = "<th style='width: 90px;font-weight: bold;border:1px solid black' colspan='2' runat='server'><center>Class " + dataSet.Tables[0].Rows[j]["std_name"].ToString() + "</center></th>";
                                        th2 = "<th style='width: 90px;font-weight: bold;border:1px solid black' ><center>Boys</center></th><th style='width: 90px;font-weight: bold;border:1px solid black' ><center>Girls</center></th>";
                                        td1 = "<td style='width: 90px;font-weight: 400;border:1px solid black' id='txtbc" + studcls + "min' runat='server'><center>" + finalbmin + "</center></td><td style='width: 90px;font-weight: 400;border:1px solid black' id='txtgc" + studcls + "min' runat='server'><center>" + finalgmin + "</center></td>";
                                    }
                                    else
                                    {
                                        th1 = th1 + "<th style='width: 90px;font-weight: bold;border:1px solid black' colspan='2' runat='server'><center>Class " + dataSet.Tables[0].Rows[j]["std_name"].ToString() + "</center></th>";
                                        th2 = th2 + "<th style='width: 90px;font-weight: bold;border:1px solid black' ><center>Boys</center></th><th style='width: 90px;font-weight: 400;border:1px solid black;' ><center>Girls</center></th>";
                                        td1 = td1 + "<td style='width: 90px;font-weight: 400;background-color: lightgrey;border:1px solid black' id='txtbc" + studcls + "min' runat='server' ><center></center></td><td style='width: 90px;font-weight: 400;background-color: lightgrey;border:1px solid black' id='txtgc" + studcls + "min' runat='server' ><center></center></td>";
                                    }

                                }
                            }
                            //$("[id*=tblfees]").append("<thead><tr ><th style='width: 90px;font-weight: bold;'><center>Class</center></th>" + th1 + "</tr></thead>");
                            //$("[id*=tblfees]").append("<thead><tr ><th style='width: 90px;font-weight: bold;'><center>Age</center></th>" + th2 + "</tr></thead>");
                            //$("[id*=tblfees]").append("<tbody>");
                            //$("[id*=tblfees]").append("<tr ><td style='width: 90px;font-weight: 400;' id='txtagemin'><center>>5</center></td>" + td1 + "</tr>");
                            sb.Append("<table id='tblfees' style='width:100%;border:2px solid black' class='table table-bordered'>");
                            sb.Append("<thead><tr ><th style='width: 90px;font-weight: bold;border:1px solid black'><center>Class</center></th>" + th1 + "</tr></thead>");
                            sb.Append("<thead><tr ><th style='width: 90px;font-weight: bold;border:1px solid black'><center>Age</center></th>" + th2 + "</tr></thead>");
                            sb.Append("<tbody>");
                            sb.Append("<tr ><td style='width: 90px;font-weight: 400;border:1px solid black' id='txtagemin' runat='server'><center><5</center></td>" + td1 + "</tr>");
                            // sb.Append("</tbody>");
                            //littable.Text = Convert.ToString(sb);
                        }

                        else if (i == 20 - 1)
                        {
                            string td2 = "";
                            string td1 = "";
                            int lastclass = 0;
                            for (int j = 0; j < dataSet.Tables[0].Rows.Count; j++)
                            {
                                int studcls = j + 1;
                                lastclass = j;

                                DataSet ds = new DataSet();
                                string urlalias1 = cls.urls();
                                string url1 = @urlalias1 + "agereport/";

                                age.type = "age";
                                age.med_id = ddl_medium.SelectedValue;
                                age.class_id = dataSet.Tables[0].Rows[j]["std_id"].ToString();
                                age.Ayid = Session["acdyear"].ToString();
                                age.currdate = dateprint.Text;

                                string jsonString1 = JsonHelper.JsonSerializer<agereprt>(age);
                                var httprequest1 = (HttpWebRequest)WebRequest.Create(url1);
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
                                    ds = JsonConvert.DeserializeObject<DataSet>(result1);
                                    string maxb = ""; string maxg = ""; string finalgmax = ""; string finalbmax = "";


                                    for (var z = 0; z < ds.Tables[1].Rows.Count; z++)
                                    {
                                        if (Convert.ToInt32(ds.Tables[1].Rows[z]["AgeIntYears"].ToString()) < 5)
                                        {
                                            if (maxg == "")
                                            {
                                                maxg = ds.Tables[1].Rows[z]["Nooffemale"].ToString();
                                                //$("[id*='txtgc" + std + "max']").val(max);
                                                if (maxg == "")
                                                {
                                                    maxg = "0";
                                                    // document.getElementById("txtgc" + std + "max").innerText = 0;
                                                }
                                                else
                                                {
                                                    maxg = maxg;
                                                    // td.InnerText = max;
                                                }
                                                finalgmax = maxg;
                                                //gtotal = Convert.ToString(Convert.ToInt32(gtotal) + Convert.ToInt32(finalgmax));
                                            }
                                            else
                                            {
                                                if (ds.Tables[1].Rows[z]["Nooffemale"].ToString() == "")
                                                {
                                                    maxg = Convert.ToString(Convert.ToInt32(maxg) + 0);
                                                }
                                                else
                                                {
                                                    maxg = Convert.ToString(Convert.ToInt32(maxg) + Convert.ToInt32(ds.Tables[1].Rows[z]["Nooffemale"].ToString()));
                                                }

                                                /// document.getElementById("txtgc" + std + "max").innerText = max;
                                                finalgmax = maxg;
                                                //gtotal = Convert.ToString(Convert.ToInt32(gtotal) + Convert.ToInt32(finalgmax));
                                            }
                                        }
                                    }
                                    for (var y = 0; y < ds.Tables[0].Rows.Count; y++)
                                    {
                                        if (Convert.ToInt32(ds.Tables[0].Rows[y]["AgeIntYears"].ToString()) < 5)
                                        {
                                            if (maxb == "")
                                            {
                                                maxb = ds.Tables[0].Rows[y]["Noofmale"].ToString();
                                                //$("[id*='txtgc" + std + "max']").val(max);
                                                if (maxb == "")
                                                {
                                                    maxb = "0";
                                                    // document.getElementById("txtgc" + std + "max").innerText = 0;
                                                }
                                                else
                                                {
                                                    maxb = maxb;
                                                    // td.InnerText = max;
                                                }
                                                finalbmax = maxb;
                                                //btotal = Convert.ToString(Convert.ToInt32(btotal) + Convert.ToInt32(finalbmax));
                                            }
                                            else
                                            {
                                                if (ds.Tables[0].Rows[y]["Noofmale"].ToString() == "")
                                                {
                                                    maxb = Convert.ToString(Convert.ToInt32(maxb) + 0);
                                                }
                                                else
                                                {
                                                    maxb = Convert.ToString(Convert.ToInt32(maxb) + Convert.ToInt32(ds.Tables[1].Rows[y]["Noofmale"].ToString()));
                                                }

                                                /// document.getElementById("txtgc" + std + "max").innerText = max;
                                                finalbmax = maxb;
                                                //btotal = Convert.ToString(Convert.ToInt32(btotal) + Convert.ToInt32(finalbmax));
                                            }
                                        }


                                    }

                                    if (finalbmax == "")
                                    {
                                        finalbmax = "0";
                                    }
                                    else
                                    {

                                    }
                                    if (finalgmax == "")
                                    {
                                        finalgmax = "0";
                                    }
                                    else
                                    {

                                    }
                                    if (ds.Tables[3].Rows.Count > 0)
                                    {
                                        if (ds.Tables[3].Rows[0]["TotalMale"].ToString() == "")
                                        {
                                            btotal = "0";
                                        }
                                        else
                                        {
                                            btotal = ds.Tables[3].Rows[0]["TotalMale"].ToString();
                                        }
                                    }
                                    else
                                    {
                                        btotal = "0";
                                    }
                                    if (ds.Tables[2].Rows.Count > 0)
                                    {
                                        if (ds.Tables[2].Rows[0]["TotalFemale"].ToString() == "")
                                        {
                                            gtotal = "0";
                                        }
                                        else
                                        {
                                            gtotal = ds.Tables[2].Rows[0]["TotalFemale"].ToString();
                                        }
                                    }
                                    else
                                    {
                                        gtotal = "0";
                                    }
                                    string cssclass = "";
                                    if (dataSet.Tables[0].Rows.Count == 19)
                                    {
                                        cssclass = "";

                                    }
                                    else
                                    {
                                        cssclass = "background-color:lightgrey";
                                        finalbmax = "";
                                        finalgmax = "";
                                    }

                                    if (td2 == "" || td1 == "")
                                    {
                                        //th1 = "<th style='width: 90px;font-weight: 400;' colspan='2'><center>Class " + r.Table[j].std_name + "</center></th>";
                                        //th2 = "<th style='width: 90px;font-weight: 400;' ><center>Boys</center></th><th style='width: 90px;font-weight: 400;' ><center>Girls</center></th>";
                                        td1 = "<td style='width: 90px;font-weight: 400;border:1px solid black;" + cssclass + "' id='txtbc" + studcls + "max' runat='server'  ><center>" + finalbmax + "</center></td><td style='width: 90px;font-weight: 400;border:1px solid black;" + cssclass + "' id='txtgc" + studcls + "max' runat='server'><center>" + finalgmax + "</center></td>";
                                        td2 = "<td  id='txtbtotc" + studcls + "' runat='server' style='width: 90px;font-weight: 400;background-color:lightgrey;color:black;border:1px solid black'><center>" + btotal + "</center></td><td style='width: 90px;font-weight: 400;background-color:lightgrey;color:black;border:1px solid black' id='txtgtotc" + studcls + "' runat='server'><center>" + gtotal + "</center></td>";
                                    }
                                    else
                                    {

                                        td1 = td1 + "<td style='width: 90px;font-weight: 400;border:1px solid black;" + cssclass + "' id='txtbc" + studcls + "max' runat='server' ><center>" + finalbmax + "</center></td><td style='width: 90px;font-weight: 400;border:1px solid black;" + cssclass + "' id='txtgc" + studcls + "max' runat='server' ><center>" + finalgmax + "</center></td>";
                                        td2 = td2 + "<td style='width: 90px;font-weight: 400;background-color:lightgrey;color:black;border:1px solid black' id='txtbtotc" + studcls + "' runat='server'><center>" + btotal + "</center></td><td style='width: 90px;font-weight: 400;background-color:lightgrey;color:black;border:1px solid black' id='txtgtotc" + studcls + "' runat='server'><center>" + gtotal + "</center></td>";
                                    }

                                }
                            }
                            //$("[id*=tblfees]").append("<tr ><td style='width: 90px;font-weight: 400;' id='txtagemax'><center><22</center></td>" + td1 + "</tr>");
                            //$("[id*=tblfees]").append("<tr ><td style='width: 90px;font-weight: 400;' id='txttot'><center>Total</center></td>" + td2 + "</tr>");
                            //$("[id*=tblfees]").append("</tbody>");

                            sb.Append("<tr ><td style='width: 90px;font-weight: 400;border:1px solid black' id='txtagemax' runat='server'><center>>22</center></td>" + td1 + "</tr>");
                            sb.Append("<tr ><td style='width: 90px;font-weight: 400;border:1px solid black' id='txttot' runat='server'><center>Total</center></td>" + td2 + "</tr>");
                            if (lastclass == (dataSet.Tables[0].Rows.Count - 1))
                            {
                                sb.Append("</tbody></table>");

                                tblfees.Text = Convert.ToString(sb);
                            }
                        }

                        else
                        {
                            string td1 = "";
                            int stepstart1 = 0;
                            for (int j = 0; j < dataSet.Tables[0].Rows.Count; j++)
                            {
                                int studcls = j + 1;
                                if (i > 9)
                                {
                                    if (add == 0)
                                    {
                                        add = 1;
                                    }
                                    else
                                    {
                                        add = add + 1;
                                    }

                                    stepstart1 = add + 1;


                                }

                                DataSet ds = new DataSet();
                                string urlalias1 = cls.urls();
                                string url1 = @urlalias1 + "agereport/";

                                age.type = "age";
                                age.med_id = ddl_medium.SelectedValue;
                                age.class_id = dataSet.Tables[0].Rows[j]["std_id"].ToString();
                                age.Ayid = Session["acdyear"].ToString();
                                age.currdate = dateprint.Text;

                                string jsonString1 = JsonHelper.JsonSerializer<agereprt>(age);
                                var httprequest1 = (HttpWebRequest)WebRequest.Create(url1);
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
                                    ds = JsonConvert.DeserializeObject<DataSet>(result1);
                                    string b = ""; string g = ""; string finalg = "0"; string finalb = "0";


                                    for (var z = 0; z < ds.Tables[1].Rows.Count; z++)
                                    {
                                        if (studage == Convert.ToInt32(ds.Tables[1].Rows[z]["AgeIntYears"].ToString()))
                                        {
                                            // $("[id*='txtbc" + std + "" + lblage + "']").val(data.Table[z].Noofmale);
                                            if (ds.Tables[1].Rows[z]["Nooffemale"].ToString() == "")
                                            {
                                                //document.getElementById("txtbc" + std + "" + lblage + "").innerText = 0;
                                                finalg = "0";
                                            }
                                            else
                                            {
                                                //document.getElementById("txtbc" + std + "" + lblage + "").innerText = data.Table[z].Noofmale;
                                                finalg = ds.Tables[1].Rows[z]["Nooffemale"].ToString();
                                                //gtotal = Convert.ToString(Convert.ToInt32(gtotal) + Convert.ToInt32(finalg));
                                            }
                                        }
                                        else
                                        {
                                            finalg = "0";
                                        }
                                    }
                                    for (var y = 0; y < ds.Tables[0].Rows.Count; y++)
                                    {
                                        if (studage == Convert.ToInt32(ds.Tables[0].Rows[y]["AgeIntYears"].ToString()))
                                        {
                                            // $("[id*='txtbc" + std + "" + lblage + "']").val(data.Table[z].Noofmale);
                                            if (ds.Tables[0].Rows[y]["Noofmale"].ToString() == "")
                                            {
                                                //document.getElementById("txtbc" + std + "" + lblage + "").innerText = 0;
                                                finalb = "0";
                                            }
                                            else
                                            {
                                                //document.getElementById("txtbc" + std + "" + lblage + "").innerText = data.Table[z].Noofmale;
                                                finalb = ds.Tables[0].Rows[y]["Noofmale"].ToString();
                                                //btotal = Convert.ToString(Convert.ToInt32(btotal) + Convert.ToInt32(finalb));
                                            }
                                        }
                                        else
                                        {
                                            finalb = "0";
                                        }
                                    }
                                    string csscls = "";





                                    if (i > 0)
                                    {
                                        int stepstart = i + 1;
                                        if (stepstart > j)
                                        {
                                            csscls = "";

                                        }
                                        else
                                        {
                                            csscls = "background-color:lightgrey";
                                            finalb = "";
                                            finalg = "";
                                        }
                                    }


                                    if (i > 8)
                                    {
                                        int stepstart = (i - 1) - dataSet.Tables[0].Rows.Count;
                                        if (stepstart < j)
                                        {
                                            csscls = "";


                                        }
                                        else
                                        {
                                            csscls = "background-color:lightgrey";
                                            finalb = "";
                                            finalg = "";

                                        }

                                    }

                                    


                                    if (td1 == "")
                                    {

                                        td1 = "<td style='width: 90px;font-weight: 400;border:1px solid black;" + csscls + "' id='txtbc" + studcls + "" + studage + "' runat='server'><center>" + finalb + "</center></td><td style='width: 90px;font-weight: 400;border:1px solid black;" + csscls + "' id='txtgc" + studcls + "" + studage + "' runat='server' ><center>" + finalg + "</center></td>";
                                    }
                                    else
                                    {

                                        td1 = td1 + "<td style='width: 90px;font-weight: 400;border:1px solid black;" + csscls + "' id='txtbc" + studcls + "" + studage + "' runat='server' ><center>" + finalb + "</center></td><td style='width: 90px;font-weight: 400;border:1px solid black;" + csscls + "' id='txtgc" + studcls + "" + studage + "' runat='server' ><center>" + finalg + "</center></td>";
                                    }
                                }

                            }
                            //$("[id*=tblfees]").append("<tr ><td style='width: 90px;font-weight: 400;' id='txtstudage" + studage + "'><center>" + studage + "</center>" + td1 + "</td></tr>");

                            sb.Append("<tr ><td style='width: 90px;font-weight: 400;border:1px solid black;' id='txtstudage" + studage + "' runat='server'><center>" + studage + "</center></td>" + td1 + "</tr>");

                            //littable.Text = Convert.ToString(sb);
                        }
                    }

                }
            }
            else
            {
                string display = "";
                if (ddl_medium.SelectedIndex ==0)
                {
                    
                    display = "Please Select Appropriate Medium";
                }
                else if (dateprint.Text == "")
                {

                    display = "Please sselect Date";
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
            //Response.Redirect("Login.aspx");
        }
    }

    protected void btnGetExcel_Click(object sender, EventArgs e)
    {

    }
}