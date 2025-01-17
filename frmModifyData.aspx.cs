using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using Json.Net;


public partial class frmModifyData : System.Web.UI.Page
{
    ModifyData md = new ModifyData();
    Class1 cls = new Class1();
    DataSet ds;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Convert.ToString(Session["emp_id"]) == "")
                {
                    Response.Redirect("Login.aspx");

                }
                else
                {
                    ddlfill();
                    griddiv.Visible = false;
                 
                 
                }

            }
            catch
            {
                Response.Redirect("Login.aspx");

            }

        }   
        

    }

    private void ddlfill()
    {
        try
        {
            string urlalias = cls.urls();
            string url = @urlalias + "Common/";
            string html = string.Empty;
            // string url = @"http://172.16.10.42/Utkarsha_api1/utkarsha_api/Common/";
            useless();
            md.type = "ddlfill";

            string jsonString = JsonHelper.JsonSerializer<ModifyData>(md);
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            using (StreamWriter sw = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                sw.Write(jsonString);
                sw.Flush();
                sw.Close();
            }

            var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (StreamReader sr = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                string result = sr.ReadToEnd();
                DataSet dslist = JsonConvert.DeserializeObject<DataSet>(result);

                //To use it later to assign standard
                Session["DSLIST"] = dslist;

                DataTable dt1 = dslist.Tables[0];
                ddlmedium.DataSource = dt1;
                ddlmedium.DataTextField = "medium";
                ddlmedium.DataValueField = "med_id";
                ddlmedium.Items.Insert(0, "----SELECT----");
                ddlmedium.DataBind();
                ddlmedium.Items.Insert(0, "Select");
                ddlmedium.SelectedIndex = 0;

                DataTable dt3 = dslist.Tables[1];
                ddlclass.DataSource = dt3;
                ddlclass.DataTextField = "class";
                ddlclass.DataValueField = "class_id";
                ddlclass.Items.Insert(0, "----SELECT----");
                ddlclass.DataBind();
                ddlclass.Items.Insert(0, "Select");
                ddlclass.SelectedIndex = 0;

            }
        }
        catch
        {
            Response.Redirect("Login.aspx");

        }

    }

    private void useless()
    {
        md.medium_id = null;
        md.class_id = null;
        md.div = null;
        md.type = null;
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

    protected void ddlmedium_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlmedium.SelectedIndex > 0)
            {
                DataSet ds = (DataSet)Session["DSLIST"];
                foreach (DataTable table in ds.Tables)
                {
                    if (table.TableName.Equals(ddlmedium.SelectedValue.ToString()))
                    {
                        ddlclass.DataSource = table;
                        ddlclass.DataTextField = "std_name";
                        ddlclass.DataValueField = "std_id";
                        ddlclass.DataBind();
                        ddlclass.Enabled = true;
                        ddlclass.Items.Insert(0, "----SELECT----");
                        ddlclass.SelectedIndex = 0;

                        ViewState["dt"] = null;
                        this.BindGrid();
                        grid.DataSource = null;
                        grid.DataBind();

                    }

                }

            }
        }
        catch
        {
            Response.Redirect("Login.aspx");

        }
    }
    protected void ddlclass_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlclass.SelectedIndex > 0)
            {
                if (ddlclass.SelectedIndex > 0)
                {
                    div();

                    grid.DataSource = null;
                    ViewState["dt"] = null;
                    grid.DataBind();
                    this.BindGrid();
                }

            }
        }
        catch
        {
            Response.Redirect("Login.aspx");

        }
    }

    private void div()
    {
        try
        {
            string html = string.Empty;

            string urlalias = cls.urls();
            string url = @urlalias + "Modify/";
            // string url = @"http://localhost:9199//Modify";
            useless();
            md.type = "div";
            md.medium_id = ddlmedium.SelectedValue.ToString();
            md.class_id = ddlclass.SelectedValue.ToString();
            string ayid = Session["acdyear"].ToString();
            md.ayid = Session["acdyear"].ToString();
            string jsonString = JsonHelper.JsonSerializer<ModifyData>(md);
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (StreamWriter sw = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                sw.Write(jsonString);
                sw.Flush();
                sw.Close();
            }
            var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (StreamReader sr = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                string result = sr.ReadToEnd();
                DataTable dt = JsonConvert.DeserializeObject<DataTable>(result);

                if (dt.Rows.Count == 0)
                {
                    notifys("No Division Created", "#D44950");

                }


                ddldiv.DataSource = dt;
                ddldiv.DataTextField = "division_name";
                ddldiv.DataValueField = "division_id";
                ddldiv.Items.Insert(0, "----SELECT----");
                ddldiv.DataBind();
                ddldiv.Items.Insert(0, "Select");
                ddldiv.SelectedIndex = 0;

            }
        }
        catch
        {
            Response.Redirect("Login.aspx");

        }

    }

    public void notifys(string msg, string color)
    {
        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert12", "$.notify('" + msg + "', { color: '#fff', background: '" + color + "', blur: 0.2, delay: 0 });", true);
    }

    protected void ddldiv_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddldiv.SelectedIndex > 0)
            {
                gdvall();
                griddiv.Visible = true;
            }
        }
        catch
        {
            Response.Redirect("Login.aspx");

        }
    }

    private void gdvall()
    {
        try
        {
            string html = string.Empty;

            string urlalias = cls.urls();
            string url = @urlalias + "Modify/";
            // string url = @"http://localhost:9199//Modify";
            useless();
            md.type = "gdvall";
            md.medium_id = ddlmedium.SelectedValue.ToString();
            md.class_id = ddlclass.SelectedValue.ToString();
            md.div = ddldiv.SelectedValue.ToString();
            string ayid = Session["acdyear"].ToString();
            md.ayid = Session["acdyear"].ToString();
            string jsonString = JsonHelper.JsonSerializer<ModifyData>(md);
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (StreamWriter sw = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                sw.Write(jsonString);
                sw.Flush();
                sw.Close();
            }
            var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (StreamReader sr = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                string result = sr.ReadToEnd();


                ds = JsonConvert.DeserializeObject<DataSet>(result);


                DataTable dt1 = new DataTable();
                dt1 = ds.Tables[0];



                DataColumn newColumn = new System.Data.DataColumn("flag", typeof(System.String));
                newColumn.DefaultValue = "old";
                dt1.Columns.Add(newColumn);
                ViewState["dt"] = dt1;
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    if (dt1.Rows[i]["Student_id"].ToString() == "")
                    {
                        grid.EditIndex = -1;
                    }
                }
                if (dt1.Rows.Count == 0)
                {
                    notifys("No Student Allocated", "#D44950");
                    grid.Visible = false;
                    ddlmedium.Enabled = true;
                    ddlclass.Enabled = true;
                }
                else
                {

                    grid.DataSource = dt1;
                    grid.DataBind();
                    ddlclass.Enabled = false;
                    ddlmedium.Enabled = false;
                }

            }
            Session["DSLIST"] = ds;
        }
        catch
        {
            Response.Redirect("Login.aspx");

        }
    }
    protected void grid_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            grid.EditIndex = e.NewEditIndex;
            this.BindGrid();
            fillcast(e.NewEditIndex);
        }
        catch
        {
            Response.Redirect("Login.aspx");

        }
    }

    public void fillcast(int rowIndex)
    {
        try
        {
            DataTable dt = ViewState["dt"] as DataTable;
            DataSet ds_new = new DataSet();
            ds_new = Session["DSLIST"] as DataSet;


            DataTable dt_new = new DataTable();
            dt_new = ds_new.Tables[1];
            int index = rowIndex;

            DropDownList category1 = grid.Rows[index].FindControl("ddlcategory") as DropDownList;
            category1.DataSource = dt_new;
            category1.DataTextField = "category_name";
            category1.DataValueField = "category_id";
            category1.DataBind();
            dt_new = ds_new.Tables[2];

            DropDownList caste1 = grid.Rows[index].FindControl("ddlcaste") as DropDownList;
            caste1.DataSource = dt_new;
            caste1.DataTextField = "cast_name";
            caste1.DataValueField = "cast_id";
            caste1.DataBind();
            dt_new = ds_new.Tables[3];

            DropDownList subcast1 = grid.Rows[index].FindControl("ddlsubcaste") as DropDownList;
            subcast1.DataSource = dt_new;
            subcast1.DataTextField = "subcast_name";
            subcast1.DataValueField = "subcast_id";
            subcast1.DataBind();
        }
        catch
        {
            Response.Redirect("Login.aspx");

        }

    }

    private void BindGrid()
    {
        grid.DataSource = ViewState["dt"] as DataTable;
        grid.DataBind();

    }

    protected void btnclear_Click(object sender, EventArgs e)
    {
        clear();
    }

    private void clear()
    {
        ddlmedium.SelectedIndex = 0;
        ddlclass.SelectedIndex = 0;
        if(ddldiv.SelectedIndex > 0)
        {
            ddldiv.SelectedIndex = 0;
        }
        
        grid.DataSource = null;
        grid.DataBind();
        ddlclass.Enabled = true;
        ddlmedium.Enabled = true;
        ddldiv.Enabled = true;
        griddiv.Visible = false;
        btnsave.Enabled = false;
       
    }

    protected void LBCancel_Click(object sender, EventArgs e)
    {
        grid.EditIndex = -1;
        this.BindGrid();

    }

    protected void LBUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
            Label _stud_id = (Label)row.FindControl("lblstud_id");
            Label _std_name = (Label)row.FindControl("lblstd_name");
            TextBox stud_f_name = (TextBox)row.FindControl("txtstud_f_name") as TextBox;
            TextBox stud_m_name = (TextBox)row.FindControl("txtstud_m_name") as TextBox;
            TextBox stud_l_name = (TextBox)row.FindControl("txtstud_l_name") as TextBox;
            TextBox stud_mo_name = (TextBox)row.FindControl("txtstud_mo_name") as TextBox;
            DropDownList gender = (DropDownList)row.FindControl("ddlgender") as DropDownList;
            TextBox address = (TextBox)row.FindControl("txtaddress") as TextBox;
            TextBox phone_no1 = (TextBox)row.FindControl("txtphone_no1") as TextBox;
            TextBox phone_no2 = (TextBox)row.FindControl("txtphone_no2") as TextBox;
            TextBox co_mobile_no = (TextBox)row.FindControl("txtco_mobile_no") as TextBox;
            //TextBox dob = (TextBox)row.FindControl("txtdob") as TextBox;
            TextBox birth_place = (TextBox)row.FindControl("txtbirth_place") as TextBox;
            TextBox mother_tongue = (TextBox)row.FindControl("txtmother_tongue") as TextBox;
            TextBox nationality = (TextBox)row.FindControl("txtnationality") as TextBox;
            DropDownList category = (DropDownList)row.FindControl("ddlcategory") as DropDownList;
            DropDownList caste = (DropDownList)row.FindControl("ddlcaste") as DropDownList;
            //TextBox caste = (TextBox)row.FindControl("ddlcaste") as TextBox;
            DropDownList sub_caste = (DropDownList)row.FindControl("ddlsubcaste") as DropDownList;
            //TextBox sub_caste = (TextBox)row.FindControl("txtsub_caste") as TextBox;
            TextBox adhar_no = (TextBox)row.FindControl("txtadhar_no") as TextBox;
            TextBox pincode = (TextBox)row.FindControl("txtpincode") as TextBox;
            TextBox dist = (TextBox)row.FindControl("txtdist") as TextBox;
            TextBox Taluka = (TextBox)row.FindControl("txtTaluka") as TextBox;
            TextBox state = (TextBox)row.FindControl("txtstate") as TextBox;
            TextBox vehicle_no = (TextBox)row.FindControl("txtvehicle_no") as TextBox;
            TextBox vehicle_type = (TextBox)row.FindControl("txtvehicle_type") as TextBox;
            TextBox driver_no = (TextBox)row.FindControl("txtdriver_no") as TextBox;
            TextBox saral_id = (TextBox)row.FindControl("txtsaral_id") as TextBox;
            TextBox bank_ac_n = (TextBox)row.FindControl("txtbank_ac_no") as TextBox;
            TextBox bank_name = (TextBox)row.FindControl("txtbank_name") as TextBox;
            TextBox IFSC_code = (TextBox)row.FindControl("txtIFSC_code") as TextBox;
            TextBox Branch_name = (TextBox)row.FindControl("txtBranch_name") as TextBox;
            Label _flag = (Label)row.FindControl("lblflag");
            _flag.Text = "update";
            if (stud_f_name.Text == "" || stud_f_name.Text == null)
            {
                notifys("Student Name cannot be empty", "#D44950");

                btnsave.Enabled = false;
                this.BindGrid();
                fillcast(row.RowIndex);

            }
            else if (address.Text == "" || address.Text == null)
            {
                notifys("Student Address cannot be empty", "#D44950");
                btnsave.Enabled = false;
                this.BindGrid();
                fillcast(row.RowIndex);
            }
            else
            {


                DataTable dt = ViewState["dt"] as DataTable;
                dt.Rows[row.RowIndex]["stud_F_name"] = stud_f_name.Text;
                dt.Rows[row.RowIndex]["stud_m_name"] = stud_m_name.Text;
                dt.Rows[row.RowIndex]["stud_L_name"] = stud_l_name.Text;
                dt.Rows[row.RowIndex]["stud_mo_name"] = stud_mo_name.Text;
                dt.Rows[row.RowIndex]["gender"] = gender.Text;
                dt.Rows[row.RowIndex]["address"] = address.Text;
                dt.Rows[row.RowIndex]["phone_no1"] = phone_no1.Text;
                dt.Rows[row.RowIndex]["phone_no2"] = phone_no2.Text;
                dt.Rows[row.RowIndex]["co_mobile_no"] = co_mobile_no.Text;

                dt.Rows[row.RowIndex]["birth_place"] = birth_place.Text;
                dt.Rows[row.RowIndex]["mother_tongue"] = mother_tongue.Text;
                dt.Rows[row.RowIndex]["nationality"] = nationality.Text;
                dt.Rows[row.RowIndex]["category"] = category.SelectedItem.Text.ToString();
                dt.Rows[row.RowIndex]["caste"] = caste.SelectedItem.Text.ToString();
                dt.Rows[row.RowIndex]["sub_caste"] = sub_caste.SelectedItem.Text.ToString();
                dt.Rows[row.RowIndex]["aadhar_no"] = adhar_no.Text;
                dt.Rows[row.RowIndex]["pincode"] = pincode.Text;
                dt.Rows[row.RowIndex]["dist"] = dist.Text;
                dt.Rows[row.RowIndex]["Taluka"] = Taluka.Text;
                dt.Rows[row.RowIndex]["state"] = state.Text;
                dt.Rows[row.RowIndex]["vehicle_no"] = vehicle_no.Text;
                dt.Rows[row.RowIndex]["vehicle_type"] = vehicle_type.Text;
                dt.Rows[row.RowIndex]["driver_no"] = driver_no.Text;
                dt.Rows[row.RowIndex]["saral_id"] = saral_id.Text;
                dt.Rows[row.RowIndex]["bank_ac_no"] = bank_ac_n.Text;
                dt.Rows[row.RowIndex]["bank_name"] = bank_name.Text;
                dt.Rows[row.RowIndex]["IFSC_code"] = IFSC_code.Text;
                dt.Rows[row.RowIndex]["Branch_name"] = Branch_name.Text;
                dt.Rows[row.RowIndex]["flag"] = _flag.Text;


                ViewState["dt"] = dt;
                btnsave.Enabled = true;
                btnsave.Focus();
                grid.EditIndex = -1;
                this.BindGrid();
            }
        }
        catch
        {
            Response.Redirect("Login.aspx");

        }
    }
    public string DataTableToJSON(DataTable table)
    {
        string JSONString = string.Empty;
        JSONString = JsonConvert.SerializeObject(table);
        return JSONString;
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = ViewState["dt"] as DataTable;
            string json = DataTableToJSON(dt);
            string html = string.Empty;

            string urlalias = cls.urls();
            string url = @urlalias + "Modify/";
            //     string url = @"http://localhost:9199//Modify";


            useless();
            md.type = "save";

            md.data = json;
            string jsonString = JsonHelper.JsonSerializer<ModifyData>(md);
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (StreamWriter sw = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                sw.Write(jsonString);
                sw.Flush();
                sw.Close();
            }
            var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (StreamReader sr = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                string result = sr.ReadToEnd();

                if (ddlmedium.SelectedIndex == 0)
                {
                    notifys("Please select the medium", "#D44950");
                }
                if (ddlclass.SelectedIndex == 0)
                {
                    notifys("Please select the class", "#D44950");
                }
                if (ddldiv.SelectedIndex == 0)
                {
                    notifys("Please select the division", "#D44950");
                }



                notifys("Saved successfully", "#00B200");
                clear();

            }

        }
        catch
        {
            Response.Redirect("Login.aspx");

        }
        
    }


    protected void ddlcategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = ViewState["dt"] as DataTable;
            DataSet ds_new1 = new DataSet();
            ds_new1 = Session["DSLIST"] as DataSet;
            // dt = ds.Tables[1];

            DataTable dt_new1 = new DataTable();
            dt_new1 = ds_new1.Tables[1];
            DropDownList ddlcateg = (DropDownList)sender;
            GridViewRow GridView1 = (GridViewRow)ddlcateg.NamingContainer;
            ddlcateg = (GridView1.FindControl("ddlcategory") as DropDownList);
            DropDownList ddlcast = (DropDownList)sender;
            GridViewRow GridView = (GridViewRow)ddlcast.NamingContainer;
            ddlcast = (GridView.FindControl("ddlcaste") as DropDownList);



            if (ddlcateg.SelectedItem.Text != "Category")
            {
                string ic = ddlcateg.SelectedValue;

                DataSet ds1 = (DataSet)Session["DSLIST"];


                DataView dcast = ds_new1.Tables[2].DefaultView;

                dcast.RowFilter = "category_id = '" + ic + "'";



                ddlcast.DataSource = dcast;
                ddlcast.DataTextField = "cast_name";
                ddlcast.DataValueField = "cast_id";
                ddlcast.DataBind();

                ddlcast.Items.Insert(0, "--select--");
                ddlcast.SelectedIndex = 0;

            }
        }
        catch
        {
            Response.Redirect("Login.aspx");

        }

    }
    protected void ddlcaste_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = ViewState["dt"] as DataTable;
            DataSet ds_new2 = new DataSet();
            ds_new2 = Session["DSLIST"] as DataSet;
            DataTable dt_new2 = new DataTable();
            dt_new2 = ds_new2.Tables[2];
            DropDownList ddlcast = (DropDownList)sender;
            GridViewRow GridView1 = (GridViewRow)ddlcast.NamingContainer;
            ddlcast = (GridView1.FindControl("ddlcaste") as DropDownList);
            DropDownList ddlsubcast = (DropDownList)sender;
            GridViewRow GridView = (GridViewRow)ddlsubcast.NamingContainer;
            ddlsubcast = (GridView.FindControl("ddlsubcaste") as DropDownList);



            if (ddlcast.SelectedItem.Text != "Caste")
            {
                string ic = ddlcast.SelectedValue;

                DataSet ds1 = (DataSet)Session["DSLIST"];


                DataView dcast1 = ds_new2.Tables[3].DefaultView;

                dcast1.RowFilter = "cast_id = '" + ic + "'";



                ddlsubcast.DataSource = dcast1;
                ddlsubcast.DataTextField = "subcast_name";
                ddlsubcast.DataValueField = "subcast_id";
                ddlsubcast.DataBind();

                ddlsubcast.Items.Insert(0, "--select--");
                ddlsubcast.SelectedIndex = 0;



            }
        }
        catch
        {
            Response.Redirect("Login.aspx");

        }
    }

}