using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class Grant_Access : System.Web.UI.Page
{
    SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ConnectionString);
    Class1 c1 = new Class1();
    DataTable dt;
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

                    btnupdate.Visible = false;
                    Button2.Visible = false;
                    Button3.Visible = false;

                    fillGrid();
                    fillRoles();

                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    //--------------------Role-----

    protected void ddlRoles_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ((CheckBox)gvRoles.HeaderRow.FindControl("checkAll")).Checked = false;
            if (ddlRoles.SelectedItem.Text == "--SELECT--")
            {
                btnupdate.Visible = false;
                Button2.Visible = false;
                Button3.Visible = false;
                foreach (GridViewRow gvr in gvRoles.Rows)
                {
                    CheckBox chk = (CheckBox)gvr.FindControl("chkSelect");
                    chk.Checked = false;
                }
                txt_roles_name.Enabled = true;
                Button1.Visible = true;
            }
            else
            {
                Button1.Visible = false;
                txt_roles_name.Enabled = false;
                txt_roles_name.Text = "";
                btnupdate.Visible = true;
                Button2.Visible = true;
                Button3.Visible = true;
                string qry1 = "select role_name,form_name from web_tp_roletype where role_id=" + ddlRoles.SelectedValue.ToString() + " and del_flag=0";

                DataSet ds1 = c1.fillDataset(qry1);
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    string form = ds1.Tables[0].Rows[0]["form_name"].ToString();
                    string[] form1 = form.Split(',');

                    for (int i = 0; i <= form1.Length - 1; i++)
                    {
                        for (int h = 0; h <= gvRoles.Rows.Count - 1; h++)
                        {
                            string words;
                            Label l = (Label)gvRoles.Rows[h].FindControl("sr");
                            words = l.Text;
                            if (form1.Contains(words))
                            {
                                CheckBox c = (CheckBox)gvRoles.Rows[h].FindControl("chkSelect");
                                c.Checked = true;
                            }
                            else
                            {
                                CheckBox c = (CheckBox)gvRoles.Rows[h].FindControl("chkSelect");
                                c.Checked = false;
                            }
                        }
                    }
                }

            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    void fillGrid()
    {
        try
        {
            DataTable dt = new DataTable();
            string str = "select sr_no,a.Form_name+'('+(select module_name from module_form where sr_no=a.portal)+')' as form_name from dbo.Register_Form as a where a.[del flag]=0";
            dt = c1.fillDataTable(str);

            gvRoles.DataSource = dt;
            gvRoles.DataBind();
            for (int i = 0; i <= gvRoles.Rows.Count - 1; i++)
            {
                CheckBox c = (CheckBox)gvRoles.Rows[i].FindControl("chkSelect");
                c.Checked = false;
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    void fillRoles()
    {
        try
        {
            DataSet dsNew = c1.fillRole();
            ddlRoles.Items.Clear();
            ddlRoles.Items.Add(new ListItem("--SELECT--", "0"));
            for (int i = 0; i < dsNew.Tables[0].Rows.Count; i++)
            {
                ddlRoles.Items.Add(new ListItem(dsNew.Tables[0].Rows[i]["role_name"].ToString(), dsNew.Tables[0].Rows[i]["role_id"].ToString()));

            }
            ddlRoles.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (txt_roles_name.Text.Trim() == "" && ddlRoles.SelectedIndex == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify('Enter Role Name', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
            }
            else if (txt_roles_name.Text.Trim() != string.Empty)
            {
                if (c1.checkDuplicatesRolename(txt_roles_name.Text.Trim()) == false)
                {
                    string formName = "", form = ""; bool flag = false;
                    int j = 0;
                    for (int i = 0; i < gvRoles.Rows.Count; i++)
                    {

                        CheckBox chk2 = (CheckBox)gvRoles.Rows[i].Cells[0].FindControl("chkSelect");
                        form = (gvRoles.Rows[i].Cells[1].FindControl("sr") as Label).Text;

                        if (chk2.Checked == true)
                        {
                            flag = true;
                            if (j == 0)
                            {
                                formName += form;
                            }
                            else
                            {
                                formName += "," + form;
                            }
                            j++;
                        }
                    }

                    if (flag == true)
                    {
                        if (c1.InserRole(txt_roles_name.Text.Trim().ToUpper(), formName) == true)
                        {
                            txt_roles_name.Text = "";

                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Role Added Successfully', {color: '#fff', background: '#198104', blur: 0.2, delay: 0  });", true);
                            fillRoles();
                            fillGrid();
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Data not saved ', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);

                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Select Forms', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Roll Name already Exist', {color: '#fff', background: '#D44950', blur: 0.2, delay: 0  });", true);

                }
            }
            else
            {
                string formName = "", form = ""; bool flag = false;
                for (int i = 0; i < gvRoles.Rows.Count; i++)
                {

                    CheckBox chk2 = (CheckBox)gvRoles.Rows[i].Cells[0].FindControl("chkSelect");
                    form = (gvRoles.Rows[i].Cells[1].FindControl("sr") as Label).Text;

                    if (chk2.Checked == true)
                    {
                        flag = true;
                        if (i == 0)
                        {
                            formName += form;
                        }
                        else
                        {
                            formName += "," + form;
                        }
                        i++;
                    }
                }

                if (flag == true)
                {
                    if (formName != string.Empty)
                    {
                        if (c1.UpdtRole(ddlRoles.SelectedItem.Text, formName) == true)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Role Updated  Successfully', {color: '#fff', background: '#198104', blur: 0.2, delay: 0  });", true);

                            ddlRoles.SelectedIndex = 0;
                            fillGrid();

                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Role not saved ', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);


                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Select Forms', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    //--------------------Edit Login------------------------

    protected void btnupdate_Click(object sender, EventArgs e)
    {
        string formName = "", form = ""; bool flag = false;
        int j = 0;
        for (int i = 0; i < gvRoles.Rows.Count; i++)
        {
            CheckBox chk2 = (CheckBox)gvRoles.Rows[i].Cells[0].FindControl("chkSelect");
            form = (gvRoles.Rows[i].Cells[1].FindControl("sr") as Label).Text;
            String[] words;
            words = form.Split(new string[] { "(" }, StringSplitOptions.None);

            if (chk2.Checked == true)
            {
                flag = true;
                if (j == 0)
                {
                    formName += form;
                }
                else
                {
                    formName += "," + form;
                }
                j++;
            }
        }

        if (flag == true)
        {
            if (c1.UpdtRole(ddlRoles.SelectedItem.Text, formName) == true)
            {
                txt_roles_name.Text = "";

                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Role Updated Successfully', {color: '#fff', background: '#198104', blur: 0.2, delay: 0  });", true);
                txt_roles_name.Enabled = true;
                Button1.Visible = true;
                fillRoles();
                fillGrid();
                btnupdate.Visible = false;
                Button2.Visible = false;
                Button3.Visible = false;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Data not saved ', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Select Forms', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }

    }

    string checking = "";

    protected void checkAll_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox ChkBoxHeader = (CheckBox)gvRoles.HeaderRow.FindControl("checkAll");
            foreach (GridViewRow row in gvRoles.Rows)
            {
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkSelect");
                if (ChkBoxHeader.Checked == true)
                {
                    ChkBoxRows.Checked = true;
                }
                else
                {
                    ChkBoxRows.Checked = false;
                }
            }
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
        
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            btnupdate.Visible = false;
            Button2.Visible = false;
            Button3.Visible = false;
            foreach (GridViewRow gvr in gvRoles.Rows)
            {
                CheckBox chk = (CheckBox)gvr.FindControl("chkSelect");
                chk.Checked = false;

            }
            txt_roles_name.Enabled = true;
            Button1.Visible = true;
            ddlRoles.SelectedIndex = 0;
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        try
        {
            string confirmValue = Request.Form["confirm_value"];
            string[] words = confirmValue.Split(',');
            Array.Reverse(words);
            if (words[0] == "Yes")
            {
                DataTable dt = new DataTable();
                string str = "select role_id from web_tp_login where role_id='" + ddlRoles.SelectedValue.ToString() + "'";
                dt = c1.fillDataTable(str);
                if (dt.Rows.Count > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Role is Assigned to Employee ', {color: '#fff', background: '#D44950', blur: 0.2, delay: 0  });", true);

                }
                else
                {
                    string sql = "update web_tp_roletype set del_flag=1 where role_id='" + ddlRoles.SelectedValue.ToString() + "'";
                    cnn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    SqlCommand command;
                    command = new SqlCommand(sql, cnn);
                    adapter.DeleteCommand = new SqlCommand(sql, cnn);
                    adapter.DeleteCommand.ExecuteNonQuery();
                    command.Dispose();
                    cnn.Close();
                    fillGrid();
                    fillRoles();
                    btnupdate.Visible = false;
                    Button2.Visible = false;
                    Button3.Visible = false;
                    txt_roles_name.Enabled = true;
                    Button1.Visible = true;
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Role Deleted Succesfully', {color: '#fff', background: '#198104', blur: 0.2, delay: 0  });", true);
                }
            }
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }
}