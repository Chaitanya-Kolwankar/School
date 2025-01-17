using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

public partial class DepartDesig : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod]
    public static deptdes[] saverecord(string deptname, string prefix, string type,string empid)
    {
        classWebMethods cls = new classWebMethods();
        return cls.saverecord(deptname, prefix, type,empid);
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
        return cls.deletedept(id, type,name,prefix);
    }

    [WebMethod]
    public static deptdes[] updaterecord(string deptname, string prefix, string type, string empid,string deptid)
    {
        classWebMethods cls = new classWebMethods();
        return cls.updaterecord(deptname, prefix, type, empid, deptid);
    }
}