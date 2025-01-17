using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for mediumclass
/// </summary>
public class mediumclass
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ConnectionString);

    public int med_id { get; set; }
    public string medium { get; set; }
    public string user_id { get; set; }

    public string curr_date { get; set; }
    public string mod_date { get; set; }

    public string extra1 { get; set; }
    public string extra2 { get; set; }
    public string extra3 { get; set; }
    public int del_flag { get; set; }
    public string form_code { get; set; }

    public DataTable Filldt(string query)
    {
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        return dt;
    }
}