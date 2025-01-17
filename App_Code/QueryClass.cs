using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Text;
using System.Collections.Generic;
using Microsoft.SqlServer.Server;

/// <summary>
/// Summary description for QueryClass
/// </summary>
public class QueryClass
{
    Class1 cls = new Class1();
    SqlCommand cmd1 = new SqlCommand();
    SqlDataAdapter da1;
    DataSet dss = new DataSet();

	public QueryClass()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    public bool checkDuplicatesRolename(string rolename)
    {
        string qry = "select * from dbo.web_tp_roletype where role_name='" + rolename + "' and is_active=1 and del_flag=0";
        DataSet dss = cls.fillDataset(qry);
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

    public bool checkDuplicatesformname(string formname)
    {
        string qry = "select * from dbo.Register_Form where Form_Name='" + formname + "'  and [Del Flag]=0";
        DataSet dss = cls.fillDataset(qry);
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


    public bool UpdtRole(string role_name, string formname)
    {

        bool i = false;
        string updQry = "update dbo.web_tp_roletype set form_name='" + formname + "' where role_name='" + role_name + "'";

        return i = cls.DMLqueries(updQry);

    }

    public bool InserRole(string role_name, string formname)
    {

        bool i = false;
        string insQry = "insert into dbo.web_tp_roletype values ('" + role_name + "','" + formname + "','','',1,getDate(),0)";

        return i = cls.DMLqueries(insQry);

    }

    public bool Inserform(string formname, string model)
    {

        bool i = false;

        string insQry = "insert into dbo.Register_Form values('" + formname.ToString().Trim() + "', '" + model.ToString() + "' , Null, Null, Null, 0, getdate(), getdate())";
        return i = cls.DMLqueries(insQry);

    }

    public void insertLog(string empid, string ipAddrs, string machineName, bool isLogin)
    {
        try
        {
            string insqry = "";
            if (isLogin)
            {
                insqry = "insert into dbo.web_tp_loginlog values('" + empid + "','" + ipAddrs + "','" + machineName + "',getDate(),null)";
            }
            else
            {
                insqry = "insert into dbo.web_tp_loginlog values('" + empid + "','" + ipAddrs + "','" + machineName + "',null,getDate())";
            }
            cls.DMLqueries(insqry);
        }
        catch (Exception ex2)
        {

        }
    }

}

public class attendLogClass
{
    QueryClass qryObj = new QueryClass();
    Class1 cls = new Class1();

}

public class accessClass 
{
    public string empid { get; set; }
    public string ayid { get; set; }
    public string groupid { get; set; }
    public string divBatch { get; set; }
    public string subid { get; set; }
    public string semid { get; set; }
    public string dmlType { get; set; }
}
//--------------------------------dept &desg
public class deptdes
{
    public string msg { get; set; }
    public string prefix { get; set; }
    public string deptname { get; set; }
    public string desname { get; set; }
    public string deptid { get; set; }
    public string desid { get; set; }
}
public class Employee
{
    public string photo { get; set; }
    public string sign { get; set; }
    public int countemp { get; set; }
    public string empid { get; set; }
    public string fname { get; set; }
    public string lname { get; set; }
    public string mname { get; set; }
    public string mothname { get; set; }
    public string dob { get; set; }
    public string doj { get; set; }
    public string gender { get; set; }
    public string bldgrp { get; set; }
    public string cat { get; set; }
    public string national { get; set; }
    public string marital { get; set; }
    public string email { get; set; }
    public string caste { get; set; }
    public string subcaste { get; set; }
    public string aadhar { get; set; }
    public string address1 { get; set; }
    public string city1 { get; set; }
    public string state1 { get; set; }
    public string pincode1 { get; set; }
    public string phoneno1 { get; set; }
    public string telno1 { get; set; }
    public string address2 { get; set; }
    public string city2 { get; set; }
    public string state2 { get; set; }
    public string pincode2 { get; set; }
    public string phoneno2 { get; set; }
    public string telno2 { get; set; }
    public string depart { get; set; }
    public string desig { get; set; }
    public string salary { get; set; }
    public string msg { get; set; }
    public string handicap { get; set; }
    public string pfno { get; set; }
    public string panno { get; set; }

    public string boardname { get; set; }
    public string colgname { get; set; }
    public string degname { get; set; }
    public string degtype { get; set; }
    public string subject { get; set; }
    public string mop { get; set; }
    public string yop { get; set; }
    public string obtmk { get; set; }
    public string totmrk { get; set; }
    public string class1 { get; set; }
    public string pursuing { get; set; }
    public string jobtype { get; set; }

    public string todt { get; set; }
    public string delflag { get; set; }
    public string deptid { get; set; }
    public string desid { get; set; }

    public string accntno { get; set; }
    public string acntype { get; set; }
    public string bnkname { get; set; }
    public string branch { get; set; }
    public string isalary { get; set; }

    public string comp { get; set; }
    public string prvsal { get; set; }
    public string jfdate { get; set; }
    public string jtdate { get; set; }

    public string logincat { get; set; }
    public string role { get; set; }
    public string grouplist { get; set; }
    public string form_name { get; set; }
    public string form_id { get; set; }
}

public class employee
{
    public string msg { get; set; }
    public string fname { get; set; }
    public string mname { get; set; }
    public string lname { get; set; }
    public string mothername { get; set; }
    public string salary { get; set; }
    public string emailid { get; set; }
    public string deptid { get; set; }
    public string desigid { get; set; }
    public string mobileno { get; set; }
    public string empid { get; set; }
    public string mobile2 { get; set; }
    public string gender { get; set; }
    public string dob { get; set; }
    public string dojoin { get; set; }
    public string address { get; set; }
    public string bloodgroup { get; set; }
    public string marrst { get; set; }
    public string category { get; set; }
    public string caste { get; set; }
    public string phone { get; set; }
    public string pfno { get; set; }
    public string ration { get; set; }
    public string election { get; set; }
    public string driving { get; set; }
    public string panno { get; set; }
    public string religion { get; set; }
    public string desig { get; set; }
    public string nativadd { get; set; }
    public string nativst { get; set; }
    public string state { get; set; }
    public string pincode { get; set; }
    public string degree { get; set; }
    public string eclass { get; set; }
    public string educnt { get; set; }
    public string course { get; set; }
    public string aadharno { get; set; }
    public string bank_accno { get; set; }
    public string branch { get; set; }
    public string council { get; set; }
    public string validity { get; set; }
    public string semcount { get; set; }
    public string PAPER { get; set; }
    public string SEMINAR { get; set; }
    public string logincat { get; set; }
    public string role { get; set; }
    public string grouplist { get; set; }
    public string form_name { get; set; }
    public string form_id { get; set; }
    public string module { get; set; }
    public string qualification { get; set; }
    public string medium { get; set; }
    public string medium_id { get; set; }
    public string image { get; set; }
    public string sign { get; set; }
    public string defaultforms { get; set; }
    public string defaultmodules { get; set; }
}

public class RoleSave
{
    public string msg { get; set; }
    public string rolename { get; set; }
    public string formname { get; set; }
    public string roleid { get; set; }

}

//----------------------masterpage
public class masterpage
{
    public string emp_id { get; set; }
    public string module_name { get; set; }
    public string form_name { get; set; }
    public string module_temp { get; set; }

}



