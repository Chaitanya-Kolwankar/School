using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

public partial class Profie : System.Web.UI.Page
{
    //string con = ConfigurationManager.ConnectionStrings["connect"].ConnectionString;
    Class1 obj = new Class1();
    DataSet ds = new DataSet();


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
                    //obj.chkPassword();
                    //retrieve();
                    //disableFields();
                }
            }
            catch (Exception w)
            {
                Response.Redirect("Login.aspx");
            }
        }
    }

    public void retrieve()
    {
        try
        {
            txt_first_name.Text = Session["NAME"].ToString();

            txt_middle_name.Text = Session["FATHER"].ToString();

            txt_last_name.Text = Session["SURNAME"].ToString();

            if(Session["DOB"].ToString()!="")
            {
            txt_dob.Text = Convert.ToDateTime(Session["DOB"]).ToShortDateString();
            }

            txt_address.Text = Session["CURRENT_ADDRESS"].ToString();

            txt_mobile_no.Text = Session["MOBILE1"].ToString();

            txt_caste.Text = Session["CASTE"].ToString();
            if(Session["DOJ"].ToString()!="")
            {

            txt_doj.Text = Convert.ToDateTime(Session["DOJ"]).ToShortDateString();
            }

            txt_category.Text = Session["CATEGORY"].ToString();

            txt_email.Text = Session["EMAIL_ADDRESS"].ToString();

            if (Session["BLOOD_GROUP"].ToString() == "A +ve")
            {
                ddlBloodGroup.SelectedIndex = 1;
            }
            else if (Session["BLOOD_GROUP"].ToString() == "B +ve")
            {
                ddlBloodGroup.SelectedIndex = 2;
            }
            else if (Session["BLOOD_GROUP"].ToString() == "O +ve")
            {
                ddlBloodGroup.SelectedIndex = 3;
            }
            else if (Session["BLOOD_GROUP"].ToString() == "A -ve")
            {
                ddlBloodGroup.SelectedIndex = 0;
            }
            else if (Session["BLOOD_GROUP"].ToString() == "B -ve")
            {
                ddlBloodGroup.SelectedIndex = 5;
            }
            else if (Session["BLOOD_GROUP"].ToString() == "O -ve")
            {
                ddlBloodGroup.SelectedIndex = 6;
            }
            else if (Session["BLOOD_GROUP"].ToString() == "AB +ve")
            {
                ddlBloodGroup.SelectedIndex = 7;
            }
            else if (Session["BLOOD_GROUP"].ToString() == "AB -ve")
            {
                ddlBloodGroup.SelectedIndex = 8;
            }
            else
            {
                ddlBloodGroup.SelectedIndex = -1;
            }

            if (Session["MARITIAL_STATUS"].ToString() == "MARRIED")
            {
                rdbMarried.Checked = true;
            }
            else
            {
                rdbUnmarried.Checked = true;
            }

            if (Session["GENDER"].ToString() == "MALE")
            {
                rdbMale.Checked = true;
            }
            else
            {
                rdbFemale.Checked = true;
            }

            Label3.Text = Session["emp_id"].ToString();

            txt_department.Text = Session["CURRENT_DEPARTMENT_NAME"].ToString();

            if (Session["CURRENT_DESIGNATION"] != null)
            {
                txt_designation.Text = Session["CURRENT_DESIGNATION"].ToString();
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void btnGetdata_Click(object sender, EventArgs e)
    {
        retrieve();
    }

    public void disableFields()
    {
        // txt_address.Enabled = false;
        txt_caste.Enabled = false;
        txt_department.Enabled = false;
        txt_designation.Enabled = false;
        txt_dob.Enabled = false;
        txt_doj.Enabled = false;
        txt_first_name.Enabled = false;
        txt_last_name.Enabled = false;
        txt_middle_name.Enabled = false;
        txt_mobile_no.Enabled = false;
        rdbFemale.Enabled = false;
        rdbMale.Enabled = false;
        rdbMarried.Enabled = false;
        rdbUnmarried.Enabled = false;
        ddlBloodGroup.Enabled = false;
        txt_email.Enabled = false;
        txt_category.Enabled = false;

    }
}