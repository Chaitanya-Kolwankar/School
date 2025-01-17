using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ProfileDashboard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnprsnprofile_Click(object sender, EventArgs e)
    {
        Response.Redirect("frm_personal_details.aspx");
    }
    protected void btnEdu_Click(object sender, EventArgs e)
    {
        Response.Redirect("Qualification_Details.aspx");
    }
    protected void btnProf_Click(object sender, EventArgs e)
    {
        Response.Redirect("Professional_Experience.aspx");

    }
    protected void btnWrk_Click(object sender, EventArgs e)
    {
        Response.Redirect("Workload.aspx");

    }
    protected void btnExtra_Click(object sender, EventArgs e)
    {
        Response.Redirect("Extra_Activity.aspx");
    }
    protected void btndoc_Click(object sender, EventArgs e)
    {
        Response.Redirect("Documents_Upload.aspx");

    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        Response.Redirect("FormPrint.aspx");
    }
}