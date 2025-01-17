using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Configuration;

using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Text;
using System.Net;





/// <summary>
/// Summary description for Class1
/// </summary>
public class Class1
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ConnectionString);
    public string Query;


    SqlCommand cmd=new SqlCommand();
    SqlDataReader sdr;
    SqlDataAdapter da;
    DataSet ds;
    DataTable dt;

    public string institute()
    {
        //return "Jivdani";
        return "KMPD";
    }

    public string urls()
    {
        //return "http://192.168.1.2/schoolapi/";
        return "http://vivacollege.in/School_API/";
    }

    public string imageFolder()
    {
        //specify folder name to store images
        return "Images";
    }

    public Class1()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public class Student_mast
    {
        public string AYID;
        public string date_of_admission;
        public string modified_date;
        public string delflag;
        public string username;
        public string get_form_id;//{get;set;} 
        public string form_id;//{get;set;}
        public string medium;//{get;set;}
        public string gr_no; //{ get; set; }
        public string date; //{ get; set; }
        public string medium1; //{ get; set; }
        public string stud_class; //{ get; set; }
        public string surname; //{ get; set; }
        public string name; //{ get; set; }
        public string father_name; //{ get; set; }
        public string mo_name; //{ get; set; }
        public string gender; //{ get; set; }
        public string address; //{ get; set; }
        public string residence_no; //{ get; set; }
        public string mob_no;//{ get; set; }
        public string co_no; //{ get; set; }
        public string aadhar_no; //{ get; set; }
        public string DOB;//{ get; set; }
        public string nationality;//{ get; set; }
        public string mother_tongue;//{ get; set; }
        public string age;//{ get; set; }
        public string birth_place; //{ get; set; }
        public string category; //{ get; set; }
        public string caste; //{ get; set; }
        public string subcaste;//{ get; set; }
        public string last_schl; //{ get; set; }
        public string last_std;//{ get; set; }
        public string per; //{ get; set; }
        public string grade;//{ get; set; }
        public string vehical_type; //{ get; set; }
        public string vehical_no;//{ get; set; }
        public string driver_mob; //{ get; set; }
        public string b_acc_no; //{ get; set; }
        public string b_name; //{ get; set; }
        public string ifsc_code; //{ get; set; }
        public string branch_name; //{ get; set; }
        public string image;//{ get; set; }
        public string new_admission;//{ get; set; }
        public string saral;//{get;set;}
        public byte[] student_photo;//{ get; set; }
        //  }
    }
    //public SqlDataReader LoadstudDetails(string Form_ID)
    //{
    //    SqlDataReader rstt;
    //    try
    //    {
    //        Query = "select * from admission_student_master where form_id = '" + Form_ID + "'";
    //        rstt = RetriveDataBaseQuery(Query);
    //        return rstt;
    //    }
    //    catch (Exception ex)
    //    {
    //        ex.ToString();
    //        con.Close();
           
          
    //    }
    //}


    public DataTable fid(string query)
    {
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        return dt;
    }

    public void filldropdown(DropDownList ddl, String TABLE_NAME, String CONDITION, String DATA_COLUMN)
    {
        string strquery;
        strquery = "Select distinct " + DATA_COLUMN + " from " + TABLE_NAME + " " + CONDITION;
        con.Open();
        cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = strquery;
        cmd.CommandType = CommandType.Text;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        DataRow drr = ds.Tables[0].NewRow();
        drr[0] = "--SELECT--";
        ds.Tables[0].Rows.InsertAt(drr, 0);
        ddl.DataSource = ds.Tables[0];
        ddl.DataTextField = ds.Tables[0].Columns[0].ColumnName;
        ddl.DataValueField = ds.Tables[0].Columns[0].ColumnName;
        ddl.DataBind();
        ddl.SelectedIndex = 0;
    }
    public void Conn()
    {
        if (con.State == ConnectionState.Open)
        {
            con.Close();
            con.Open();
        }
        else
        {
            con.Open();
        }

    }
   
    public bool CheckNum(string txt)
    {
        Regex reg = new Regex("(^([0-9])+$)");
        return reg.IsMatch(txt);
    }

     public void opencon()
    {
        try
        {

            if (this.con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    public void closecon()
    {
        con.Close();
    }

    //public SqlDataReader RetriveDataBaseQuery(string strQuery)
    //{
    //    try
    //    {
    //        opencon();
    //        cmd.Connection = con;
    //        cmd.CommandText = strQuery;
    //        cmd.CommandType = CommandType.Text;
    //        sdr = cmd.ExecuteReader();
    //        return sdr;
    //    }
    //    catch (Exception ex)
    //    {
    //        ex.ToString();
    //        closecon();
    //        return sdr;
    //    }
    //}

    public SqlDataReader RetriveDataBaseQuery(String strQuery)
    {
        try
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
                con.Open();
            }
            else
            {
                con.Open();
            }
            cmd.Connection = con;
            cmd.CommandText = strQuery;
            cmd.CommandType = CommandType.Text;
            con.Close();

            con.Open();
            SqlDataReader sdr;
            sdr = cmd.ExecuteReader();
            return sdr;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public DataTable fillDataTable(string query)
    {
        cmd = new SqlCommand();
        cmd.CommandText = query;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;
        cmd.CommandTimeout = 1200000;
        Conn();
        da = new SqlDataAdapter(cmd);
        ds = new DataSet();

        da.Fill(ds);

        con.Close();
        return ds.Tables[0];
    }
    //execute query

    public Boolean ExecuteDataBaseQuery(string strQuery)
    {
        try
        {
            opencon();
            cmd.Connection = con;
            cmd.CommandText = strQuery;
            cmd.ExecuteNonQuery();
            return true;
        }
        catch (Exception ex)
        {
            ex.ToString();
            closecon();
            return false;
        }
    }

    public DataSet fillDataset(string query)
    {
        cmd = new SqlCommand();
        cmd.CommandText = query;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;
        Conn();
        da = new SqlDataAdapter(cmd);
        ds = new DataSet();

        da.Fill(ds);

        con.Close();
        return ds;
    }
    public bool fillup(string query)
    {
        con.Open();
        SqlCommand cmd = new SqlCommand(query, con);
        if (Convert.ToBoolean(cmd.ExecuteNonQuery()) == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    //execute query
    public Boolean SingleQuery(string strQuery)
    {
        try
        {
            opencon();
            cmd.Connection = con;
            cmd.CommandText = strQuery;
            cmd.ExecuteScalar();
            return true;
        }
        catch (Exception ex)
        {
            // ex.ToString();
            closecon();
            return false;
        }
    }

    //retrieve query

    public bool DMLqueries(string query)
    {
        try
        {
            cmd = new SqlCommand();
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            if (con.State == ConnectionState.Open)
            {
                con.Close();
                con.Open();
            }
            else
            {
                con.Open();
            }
            if (cmd.ExecuteNonQuery() > 0)
            {
                con.Close();
                return true;

            }
            else
            {
                con.Close();
                return false;

            }
        }
        catch (Exception ex1)
        {
            return false;
        }

    }

    public DataSet RetriveDataBase(string strQuery)
    {
        try
        {
            opencon();


            cmd.Connection = con;
            cmd.CommandText = strQuery;
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            ex.ToString();
            closecon();
            return ds;
        }
    }

    public System.Data.DataTable RetriveDatatable(string strQuery)
    {

        try
        {
            opencon();


            cmd.Connection = con;
            cmd.CommandText = strQuery;
            da = new SqlDataAdapter(cmd);
            dt = new System.Data.DataTable();
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            ex.ToString();
            closecon();
            return dt;
        }
    }

    public System.Data.DataTable filldatatable(string qry)
    {
        //fills datatable
        opencon();
        cmd = new SqlCommand(qry, con);
        cmd.CommandTimeout = 200;
        da = new SqlDataAdapter(cmd);
        dt = new System.Data.DataTable();
        da.Fill(dt);
        con.Close();
        return dt;
    }


    public void chkPassword()
    {
        try
        {
            if (Convert.ToString(HttpContext.Current.ApplicationInstance.Session["username"]) != "")
            {
                if (Convert.ToString(HttpContext.Current.ApplicationInstance.Session["password"]) != "")
                {
                    if (Convert.ToString(HttpContext.Current.ApplicationInstance.Session["username"]) == Convert.ToString(HttpContext.Current.ApplicationInstance.Session["password"]))
                    {
                        HttpContext.Current.ApplicationInstance.Session["passwordchanged"] = "false";
                        HttpContext.Current.Response.Redirect("ChangePasswd.aspx", false);
                    }
                    else
                    {
                        HttpContext.Current.ApplicationInstance.Session["passwordchanged"] = "true";
                    }
                }
                else
                {
                    HttpContext.Current.Response.Redirect("Login.aspx");
                }
            }
            else
            {
                HttpContext.Current.Response.Redirect("Login.aspx");
            }
        }
        catch (Exception ex)
        {
            HttpContext.Current.Response.Redirect("Login.aspx");
        }
    }



    //-----------------role
    public bool checkDuplicatesRolename(string rolename)
    {
         string qry = "select * from dbo.web_tp_roletype where role_name='" + rolename + "' and is_active=1 and del_flag=0";
        DataSet dss = fillDataset(qry);
        if (dss.Tables.Count > 0)
        {
            if (dss.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }
        else
        {
            return false;
        }
    }
    public DataSet fillRole()
    {
        string qry = "select * from dbo.web_tp_roletype where is_active=1 and del_flag=0";
        DataSet dss = fillDataset(qry);
        return dss;
    }
    public bool InserRole(string role_name, string formname)
    {

        bool i = false;
        string insQry = "insert into dbo.web_tp_roletype values ('" + role_name + "','" + formname + "',null,null,1,getDate(),null,0)";

        return i = DMLqueries(insQry);

    }
    public bool UpdtRole(string role_name, string formname)
    {

        bool i = false;
        string updQry = "update dbo.web_tp_roletype set form_name='" + formname + "', mod_date=getdate() where role_name='" + role_name + "'";

        return i = DMLqueries(updQry);

    }

    //--------------------employee-----------------
    public DataSet fill_dataset(string query)
    {
        cmd = new SqlCommand();
        cmd.CommandText = query;
        cmd.CommandType = CommandType.Text;
        cmd.CommandTimeout = 1000000000;
        cmd.Connection = con;
        Conn();
        da = new SqlDataAdapter(cmd);
        ds = new DataSet();

        da.Fill(ds);

        con.Close();
        return ds;
    }

    public object SetdropdownForMember(DropDownList ddl, string TABLE_NAME, string DATA_COLUMN, string VALUE_COLUMN, string CONDITION)
    {
        string Query;
        try
        {

            if (VALUE_COLUMN.Length > 0)
            {
                VALUE_COLUMN = "," + VALUE_COLUMN;
            }
            if (string.IsNullOrEmpty(CONDITION))
            {
                Query = "SELECT " + DATA_COLUMN + VALUE_COLUMN + " FROM " + TABLE_NAME;
            }
            else
            {
                //=============
                Query = "SELECT " + DATA_COLUMN + VALUE_COLUMN + " FROM " + TABLE_NAME + " where " + CONDITION;
                // Query = "SELECT " + DATA_COLUMN + VALUE_COLUMN + " FROM " + TABLE_NAME + " where " + DATA_COLUMN + " not like 'M%' and " + CONDITION;
            }
            //Conn();
            //cmd = new SqlCommand(Query,con);
            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //DataSet ds = new DataSet();
            //da.Fill(ds);
            //con_close();
            ds = fillDataset(Query);

            DataRow drr = ds.Tables[0].NewRow();
            drr[0] = "--SELECT--";
            ds.Tables[0].Rows.InsertAt(drr, 0);
            ddl.DataSource = ds.Tables[0];
            ddl.DataTextField = ds.Tables[0].Columns[0].ColumnName;
            ddl.DataValueField = ds.Tables[0].Columns[1].ColumnName;
            // ddl.Items.Clear();
            ddl.DataSource = null;
            ddl.DataBind();
            ddl.DataSource = ds.Tables[0];
            ddl.DataBind();
            // con.Close();
            //ddl.SelectedIndex = 0;

        }
        catch (Exception ex)
        {
            return null;
        }
        return 0;
    }

    //-------------------------login------------------------------
    public string getClientIp()
    {
        string ipaddress = "";
        try
        {
            IPHostEntry ipEntry = Dns.GetHostEntry(GetCompCode());
            IPAddress[] addr = ipEntry.AddressList;
            ipaddress = addr[2].ToString();
            ipaddress += "," + addr[2].ToString();
        }
        catch (Exception ex2)
        {

        }
        return ipaddress;
    }


    public static string GetCompCode()  // Get Computer Name
    {
        string strHostName = "";
        try
        {
            strHostName = Dns.GetHostName();
        }
        catch (Exception ex1)
        {

        }
        return strHostName;
    }
    //------------------------------profile-------------------------

    public int DMLquerries_1(string qry)
    {
        cmd = new SqlCommand(qry, con);
        cmd.CommandType = CommandType.Text;
        Conn();
        int i;

        i = cmd.ExecuteNonQuery();
        return i;

    }

    public bool DMLqueries3(string query)
    {
        try
        {
            cmd = new SqlCommand();
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            if (con.State == ConnectionState.Open)
            {
                con.Close();
                con.Open();
            }
            else
            {
                con.Open();
            }
            if (cmd.ExecuteNonQuery() > 0)
            {
                con.Close();
                return true;

            }
            else
            {
                con.Close();
                return false;

            }
        }
        catch (Exception ex1)
        {
            return false;
        }

    }

}