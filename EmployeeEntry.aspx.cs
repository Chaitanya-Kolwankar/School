using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Web.Services;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;

public partial class EmployeeEntry : System.Web.UI.Page
{
    Class1 cls1 = new Class1();
 
    QueryClass queryCls = new QueryClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

        }
        catch(Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    [WebMethod]
    public static List<ListItem> filldepartment()
    {
        classWebMethods clsw = new classWebMethods();
        return clsw.filldepartment();
    }

    [WebMethod]
    public static List<ListItem> filldesignation()
    {
        classWebMethods clsw = new classWebMethods();
        return clsw.filldesignation();
    }

    [WebMethod]
    public static List<ListItem> fillrole()
    {
        classWebMethods clsw = new classWebMethods();
        return clsw.fillrole();
    }

   
    [WebMethod]
    public static List<ListItem> fillmedium()
    {
        classWebMethods clsw = new classWebMethods();
        return clsw.fill_medium();
    }

    [WebMethod]
    public static List<ListItem> fill_form_name_new()
    {
        classWebMethods clsw = new classWebMethods();
        return clsw.fill_form_name_new();
    }

    [WebMethod]
    public static List<ListItem> fill_model()
    {
        classWebMethods clsw = new classWebMethods();
        return clsw.fill_model();
    }

    [WebMethod]
    public static List<ListItem> fill_form_name(string module_name)
    {
        classWebMethods clsw = new classWebMethods();
        return clsw.fill_form_name(module_name);
    }

    [WebMethod]
    public static List<ListItem> fill_standard(string medium)
    {
        classWebMethods clsw = new classWebMethods();
        return clsw.fill_standard(medium);
    }

    [WebMethod]
    public static employee[] employeerecord(string emp_id, string fname, string mname, string lname, string mothernme, string emailid, string dobirth, string dojoin, string mobileno, string salary, string departid, string desigid, string gender, string mobile2, string role, string logincategory, string grouplist, string formname, string address, string bloodgroup, string qualification, string pincode, string state)
    {
        classWebMethods cls = new classWebMethods();
        return cls.employeerecord(emp_id, fname, mname, lname, mothernme, emailid, dobirth, dojoin, mobileno, salary, departid, desigid, gender, mobile2, role, logincategory, grouplist, formname, address, bloodgroup, qualification, pincode, state);
    }

    [WebMethod]
    public static employee[] getemployeedata(string empid)
    {
        classWebMethods cls1 = new classWebMethods();
        return cls1.getemployeedata(empid);
    }

    [WebMethod]
    public static deptdes[] saverecord(string deptname, string prefix, string type, string empid)
    {
        classWebMethods cls = new classWebMethods();
        return cls.saverecord(deptname, prefix, type, empid);
    }

    [WebMethod]
    public static deptdes[] filltblgrid()
    {
        classWebMethods cls = new classWebMethods();
        return cls.filltblgrid();
    }

    [WebMethod]
    public static deptdes[] fillgesigrid()
    {
        classWebMethods cls = new classWebMethods();
        return cls.fillgesigrid();
    }

    [WebMethod]
    public static deptdes[] deletedept(string id, string type, string name, string prefix)
    {
        classWebMethods cls = new classWebMethods();
        return cls.deletedept(id, type, name, prefix);
    }

    [WebMethod]
    public static Employee[] deleteForm(string id)
    {
        classWebMethods cls = new classWebMethods();
        return cls.deleteForm(id);
    }

    [WebMethod]
    public static deptdes[] updaterecord(string deptname, string prefix, string type, string empid, string deptid)
    {
        classWebMethods cls = new classWebMethods();
        return cls.updaterecord(deptname, prefix, type, empid, deptid);
    }


    [WebMethod]
    public static RoleSave[] saveRole(string rolename, string formname)
    {
        classWebMethods cls = new classWebMethods();
        return cls.saveRole(rolename, formname);
    }


    [WebMethod]
    public static Employee[] saveForm(string formname, string model)
    {
        classWebMethods cls = new classWebMethods();
        return cls.saveForm(formname, model);
    }

    [WebMethod]
    public static bool saveData(string qry)
    {
        classWebMethods webCls = new classWebMethods();
        return webCls.saveData(qry);
    }


    protected void btn_getexcel_Click(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeReport.xls");
    }

    private void releaseObject(object obj)
    {
        try
        {
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
            obj = null;
        }
        catch (Exception ex)
        {
            obj = null;
        }
        finally
        {
            GC.Collect();
        }
    }

    [WebMethod]
    public static List<ListItem> fill_form_name_load(string roleid)
    {
        classWebMethods clsw = new classWebMethods();
        return clsw.fill_form_name_load(roleid);
    }

 
}