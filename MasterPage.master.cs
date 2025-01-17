using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;

public partial class MasterPage : System.Web.UI.MasterPage
{
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da;
    DataSet ds;
    DataSet ds1;
    Class1 c1 = new Class1();
    string s_img;
    String a;
    FileUpload fup_Photo, fuppic;
    DataSet ds_photo_new;
    classWebMethods cls = new classWebMethods();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["emp_id"] == null || Session["emp_id"] == "")
            {
                Response.Redirect("Login.aspx?msg=session");
            }
            else
            {
                if (!IsPostBack)
                {
                    if (Session["acdyear"] == null)
                    {
                        DataSet ds = c1.fillDataset("select * from m_academic order by ayid desc;select ayid from m_academic where is_current=1");
                        ddlyear.DataSource = ds.Tables[0];
                        ddlyear.DataTextField = "Duration";
                        ddlyear.DataValueField = "AYID";
                        ddlyear.DataBind();
                        ListItem selectedListItem = ddlyear.Items.FindByValue(ds.Tables[1].Rows[0]["ayid"].ToString());
                        if (selectedListItem != null)
                        {
                            selectedListItem.Selected = true;
                        }
                        Session["acdyear"] = ddlyear.SelectedValue.ToString();
                        Session["year"] = ddlyear.SelectedItem.Text.ToString();
                        Session["dtyear"] = ds.Tables[0];
                    }
                    else
                    {
                        DataTable dt = c1.fillDataTable("select * from m_academic order by ayid desc");
                        ddlyear.DataSource = dt;
                        ddlyear.DataTextField = "Duration";
                        ddlyear.DataValueField = "AYID";
                        ddlyear.DataBind();
                        ddlyear.SelectedIndex = ddlyear.Items.IndexOf(ddlyear.Items.FindByValue(Session["acdyear"].ToString()));
                        Session["acdyear"] = ddlyear.SelectedValue.ToString();
                        Session["year"] = ddlyear.SelectedItem.Text.ToString();
                        Session["dtyear"] = dt;
                    }
                    string ins = c1.institute();                    
                    if (ins == "Jivdani")
                    {
                        lblschoolname.Text = "School";
                    }
                    else if (ins == "KMPD")
                    {
                        lblschoolname.Text = "School";
                    }
                }
            }
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            Response.Cache.SetNoStore();
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void ddlyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["acdyear"] = ddlyear.SelectedValue.ToString();
        Session["year"] = ddlyear.SelectedItem.Text.ToString();
        Response.Redirect(HttpContext.Current.Request.Url.ToString(), true);
    }

   

}
