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


public partial class _Default : System.Web.UI.Page
{
    SqlDataAdapter adapter;
    string con = ConfigurationManager.ConnectionStrings["connect"].ConnectionString;
    DataTable dt;
    SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                gridupdta();
                modalgridview();
            }
            catch (Exception ex)
            {

            }
        }
    }

    public void modalgridview() {
        try
        {
            DataTable dt1 = new DataTable();
            adapter = new SqlDataAdapter("select Form_Name,'('+(SELECT MODULE_NAME FROM MODULE_FORM WHERE SR_NO=PORTAL)+')'  AS Form_name1,SR_NO from Register_Form  where Portal in (select SR_NO from module_form ) and[del flag] = 0 order by portal ", cnn);
            adapter.Fill(dt1);
            GridView2.DataSource = dt1;
            GridView2.DataBind();
        }
        catch (Exception ex)
        {

        }
    }

    public void gridupdta()
    {
        try
        {
            dt = new DataTable();
            adapter = new SqlDataAdapter("select sr_no,module_name,(SELECT COUNT(*) FROM REGISTER_FORM WHERE PORTAL=B.sr_no AND [DEL FLAG]=0) AS FLAG from module_form AS B WHERE B.DEL_FLAG='0'", cnn);
            adapter.Fill(dt);

            li_name.DataSource = dt;
            li_name.DataTextField = "module_name";
            li_name.DataValueField = "sr_no";
            li_name.DataBind();
            li_name.Items.Insert(0, new ListItem("ALL FORMS", "0"));
            cnn.Close();
            ViewState["dt"] = dt;
            this.BindGrid();
        }
        catch (Exception ex)
        {

        }
    }

    public void gridupdtaforadding()
    {
        try
        {
            dt = new DataTable();
            adapter = new SqlDataAdapter("select sr_no,module_name,(SELECT COUNT(*) FROM REGISTER_FORM WHERE PORTAL=B.sr_no and [del flag]=0) AS FLAG from module_form AS B WHERE B.DEL_FLAG='0'", cnn);
            adapter.Fill(dt);
            cnn.Close();
            ViewState["dt"] = dt;
            this.BindGrid();
        }
        catch (Exception ex)
        {

        }
    }

    protected void inse_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection cnn = new SqlConnection(con);
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dt2 = new DataTable();

            adapter = new SqlDataAdapter("select Module_name from module_form  WHERE module_name='" + TextBox1.Text + "' and del_flag=0", cnn);
            adapter.Fill(dt2);
            if (dt2.Rows.Count > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify(  'Module Name Already Exists', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
            }
            else
            {
                if (TextBox1.Text != "")
                {
                    string sql = "INSERT INTO Module_form (Module_name,curr_dt,del_flag) values('" + TextBox1.Text + "',GETDATE(),0);";
                    cnn.Open();
                    SqlCommand command;
                    command = new SqlCommand(sql, cnn);
                    adapter.InsertCommand = new SqlCommand(sql, cnn);
                    adapter.InsertCommand.ExecuteNonQuery();
                    command.Dispose();
                    cnn.Close();
                    gridupdta();
                    gridupdtaforadding();
                    TextBox1.Text = "";
                    OnCancel(this, EventArgs.Empty);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify(  'Module Saved', { color: '#fff', background: '#198104', blur: 0.2, delay: 0 })", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify(  'Please Enter Module Name', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void add_form_Click(object sender, EventArgs e)
    {
        try
        {
            if (li_name.SelectedValue != "0")
            {
                string con = ConfigurationManager.ConnectionStrings["connect"].ConnectionString;
                SqlConnection cnn = new SqlConnection(con);
                DataTable dt2 = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter = new SqlDataAdapter("select FORM_NAME from register_form  WHERE FORM_NAME='" + form_name.Text + "' AND [DEL FLAG]=0", cnn);
                adapter.Fill(dt2);
                if (dt2.Rows.Count > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify(  'Form Name Already Exists', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                }
                else
                {
                    if (form_name.Text != "")
                    {
                        string DiagResult = li_name.SelectedValue;
                        string sql = "INSERT INTO register_form (FORM_NAME,PORTAL,[DEL FLAG],currdt)values ('" + form_name.Text + "','" + li_name.SelectedItem.Value + "',0,getdate())";
                        cnn.Open();
                        SqlCommand command;
                        command = new SqlCommand(sql, cnn);
                        adapter.InsertCommand = new SqlCommand(sql, cnn);
                        adapter.InsertCommand.ExecuteNonQuery();
                        command.Dispose();
                        cnn.Close();
                        DataTable dt1 = new DataTable();
                        adapter = new SqlDataAdapter("select Form_Name,SR_NO,'' as form_name1 from Register_Form where Portal in (SELECT SR_NO FROM module_form WHERE MODULE_NAME IN('" + li_name.SelectedItem.Text + "')) and[del flag] = 0 order by portal", cnn);
                        adapter.Fill(dt1);
                        GridView2.DataSource = "";
                        GridView2.DataSource = dt1;
                        GridView2.DataBind();
                        form_name.Text = "";
                        gridupdtaforadding();
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify(  'Form Inserted', { color: '#fff', background: '#198104', blur: 0.2, delay: 0 })", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify(  'Add Form Name', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                    }
                }
            }
            else
            {

            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void OnRowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            TextBox1.Text = "";
            GridView1.EditIndex = e.NewEditIndex;
            this.BindGrid();
        }
        catch (Exception ex)
        {

        }
    }

    protected void BindGrid()
    {
        try
        {
            GridView1.DataSource = "";
            GridView1.DataSource = ViewState["dt"] as DataTable;
            GridView1.DataBind();
        }
        catch (Exception ex)
        {

        }
    }

    protected void onDelete(object sender, EventArgs e)
    {
        try
        {
            TextBox1.Text = "";
            GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
            string id = row.Cells[0].Text;//id
            string FLAG = row.Cells[2].Text;//flag
            if (FLAG == "0")
            {
                string confirmValue = Request.Form["confirm_value"];
                string[] words = confirmValue.Split(',');
                Array.Reverse(words);
                if (words[0] == "Yes")
                {
                    string sql = "update module_form set del_flag='1' where sr_no='" + id + "'";
                    SqlConnection cnn = new SqlConnection(con);
                    cnn.Open();
                    SqlCommand command;
                    command = new SqlCommand(sql, cnn);
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.UpdateCommand = new SqlCommand(sql, cnn);
                    adapter.UpdateCommand.ExecuteNonQuery();
                    command.Dispose();
                    cnn.Close();
                    gridupdta();
                    OnCancel(this, EventArgs.Empty);
                    gridupdta();
                    form_name.Enabled = false;
                    modalgridview();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify(  'Module Deleted', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);

                }
                else
                {
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify(  'Forms Are Aready Assign To This Module', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
            }
        }
        catch (Exception ex)
        {

        }

    }

    protected void OnUpdate(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
            string id = (row.Cells[0].Controls[0] as TextBox).Text;
            string form_name = (row.Cells[1].Controls[0] as TextBox).Text;
            SqlConnection cnn = new SqlConnection(con);
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dt2 = new DataTable();

            adapter = new SqlDataAdapter("select Module_name from module_form  WHERE module_name='" + form_name + "' and del_flag=0", cnn);
            adapter.Fill(dt2);
            if (dt2.Rows.Count > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify(  'Module Name Already Exists', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
            }
            else
            {

                string FLAG = (row.Cells[1].Controls[0] as TextBox).Text;
                if (form_name != "")
                {
                    DataTable dt = ViewState["dt"] as DataTable;
                    dt.Rows[row.RowIndex]["sr_no"] = id;
                    dt.Rows[row.RowIndex]["module_name"] = form_name;
                    ViewState["dt"] = dt;
                    GridView1.EditIndex = -1;
                    this.BindGrid();

                    string sql = "update module_form set module_name = '" + form_name + "', mod_dt = getdate() where sr_no = '" + id + "'";
                    cnn.Open();
                    SqlCommand command;
                    command = new SqlCommand(sql, cnn);
                    adapter.UpdateCommand = new SqlCommand(sql, cnn);
                    adapter.UpdateCommand.ExecuteNonQuery();
                    command.Dispose();
                    cnn.Close();
                    gridupdta();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify(  'Module Name Updated', { color: '#fff', background: '#198104', blur: 0.2, delay: 0 })", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify(  'Please Enter Module Name', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                }
            }
        }
        catch (Exception ex)
        {

        }

    }

    protected void OnCancel(object sender, EventArgs e)
    {
        try
        {
            GridView1.EditIndex = -1;
            this.BindGrid();
        }
        catch (Exception ex)
        {

        }
    }

    protected void li_name_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridView2.EditIndex = -1;
            ShowData();
            string modulle_name = li_name.SelectedItem.Text;
            if (modulle_name != "ALL FORMS")
            {
                DataTable dt1 = new DataTable();
                SqlConnection cnn = new SqlConnection(con);
                adapter = new SqlDataAdapter("select Form_Name,SR_NO,'' AS Form_Name1 from Register_Form where Portal in (SELECT SR_NO FROM module_form WHERE MODULE_NAME IN('" + modulle_name + "')) and[del flag] = 0 order by portal", cnn);
                adapter.Fill(dt1);
                GridView2.DataSource = "";
                GridView2.DataSource = dt1;
                GridView2.DataBind();
                form_name.Enabled = true;

            }
            else
            {
                form_name.Enabled = false;
                form_name.Text = "";
                DataTable dt1 = new DataTable();
                SqlConnection cnn = new SqlConnection(con);
                adapter = new SqlDataAdapter("select Form_Name,'('+(SELECT MODULE_NAME FROM MODULE_FORM WHERE SR_NO=PORTAL)+')'  AS Form_name1,SR_NO from Register_Form  where Portal in (select SR_NO from module_form ) and[del flag] = 0 order by portal ", cnn);
                adapter.Fill(dt1);
                GridView2.DataSource = "";
                GridView2.DataSource = dt1;
                GridView2.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void GridView2_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
    {
        try
        {
            GridView2.EditIndex = e.NewEditIndex;
            ShowData();
        }
        catch (Exception ex)
        {

        }
    }

    protected void GridView2_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
    {
        try
        {
            GridView2.EditIndex = -1;
            ShowData();
        }
        catch (Exception ex)
        {

        }
    }

    public void ShowData()
    {
        try
        {
            DataTable dt1 = new DataTable();
            SqlConnection cnn = new SqlConnection(con);
            if (li_name.SelectedItem.Text != "ALL FORMS")
            {
                adapter = new SqlDataAdapter("select Form_Name,SR_NO,'' FORM_NAME1 from Register_Form where Portal in (SELECT SR_NO FROM module_form WHERE MODULE_NAME IN('" + li_name.SelectedItem.Text + "')) and[del flag] = 0 order by portal", cnn);
            }
            else
            {
                adapter = new SqlDataAdapter("select Form_Name,'('+(SELECT MODULE_NAME FROM MODULE_FORM WHERE SR_NO=PORTAL)+')'  AS Form_name1,SR_NO from Register_Form  where Portal in (select SR_NO from module_form ) and[del flag] = 0 order by portal ", cnn);
            }
            adapter.Fill(dt1);
            GridView2.DataSource = "";
            GridView2.DataSource = dt1;
            GridView2.DataBind();
        }
        catch (Exception ex)
        {

        }
    }

    protected void GridView2_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
    {
        try
        {
            SqlConnection cnn = new SqlConnection(con);
            DataTable dt2 = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            TextBox name = GridView2.Rows[e.RowIndex].FindControl("txt_Name") as TextBox;
            adapter = new SqlDataAdapter("select FORM_NAME from register_form  WHERE FORM_NAME='" + name.Text + "' and [Del Flag]=0", cnn);
            adapter.Fill(dt2);
            if (dt2.Rows.Count > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify(  'Form Name Already Exists', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
            }
            else
            {
                if (name.Text != "")
                {
                    Label id = GridView2.Rows[e.RowIndex].FindControl("lbl_ID") as Label;

                    string sql = "update REGISTER_FORM set FORM_name = '" + name.Text + "', moddt = getdate() where sr_no = '" + id.Text + "'";

                    cnn.Open();
                    SqlCommand command;
                    command = new SqlCommand(sql, cnn);

                    adapter.UpdateCommand = new SqlCommand(sql, cnn);
                    adapter.UpdateCommand.ExecuteNonQuery();
                    command.Dispose();
                    cnn.Close();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify(  'Form Name Updated', { color: '#fff', background: '#198104', blur: 0.2, delay: 0 })", true);
                    DataTable dt1 = new DataTable();
                    if (li_name.SelectedItem.Text != "ALL FORMS")
                    {
                        adapter = new SqlDataAdapter("select Form_Name,SR_NO,'' FORM_NAME1 from Register_Form where Portal in (SELECT SR_NO FROM module_form WHERE MODULE_NAME IN('" + li_name.SelectedItem.Text + "')) and[del flag] = 0 order by portal", cnn);
                    }
                    else
                    {
                        adapter = new SqlDataAdapter("select Form_Name,'('+(SELECT MODULE_NAME FROM MODULE_FORM WHERE SR_NO=PORTAL)+')'  AS Form_name1,SR_NO from Register_Form  where Portal in (select SR_NO from module_form ) and[del flag] = 0 order by portal ", cnn);
                    }
                    adapter.Fill(dt1);
                    GridView2.DataSource = "";
                    GridView2.DataSource = dt1;
                    GridView2.DataBind();
                    GridView2.EditIndex = -1;
                    ShowData();

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify(  'Please Enter Some Value', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string confirmValue = Request.Form["confirm_value"];
            string[] words = confirmValue.Split(',');
            Array.Reverse(words);

            if (words[0] == "Yes")
            {
                Label name = GridView2.Rows[e.RowIndex].FindControl("lbl_ID") as Label;
                SqlConnection cnn = new SqlConnection(con);
                DataTable dt2 = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string strwithcoma = "";
                adapter = new SqlDataAdapter("select col2 from web_tp_login where del_flag=0", cnn);
                adapter.Fill(dt2);
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    if (strwithcoma == "")
                    {
                        strwithcoma = dt2.Rows[i][0].ToString();
                    }
                    else
                    {
                        strwithcoma = strwithcoma + "," + dt2.Rows[i][0].ToString();
                    }
                }
                if (strwithcoma.Contains("," + name.Text + ","))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify(  'Form Already Assign', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                }
                else
                {
                    string sql = "update REGISTER_FORM set [del flag]='1' where sr_no='" + name.Text + "'";

                    cnn.Open();
                    SqlCommand command;
                    command = new SqlCommand(sql, cnn);

                    adapter.UpdateCommand = new SqlCommand(sql, cnn);
                    adapter.UpdateCommand.ExecuteNonQuery();
                    command.Dispose();
                    cnn.Close();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify(  'Form Deleted', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                    DataTable dt1 = new DataTable();

                    if (li_name.SelectedItem.Text != "ALL FORMS")
                    {
                        adapter = new SqlDataAdapter("select Form_Name,SR_NO,'' AS FORM_NAME1 from Register_Form where Portal in (SELECT SR_NO FROM module_form WHERE MODULE_NAME IN('" + li_name.SelectedItem.Text + "')) and[del flag] = 0 order by portal", cnn);
                    }
                    else
                    {
                        adapter = new SqlDataAdapter("select Form_Name,'('+(SELECT MODULE_NAME FROM MODULE_FORM WHERE SR_NO=PORTAL)+')'  AS Form_name1,SR_NO from Register_Form  where Portal in (select SR_NO from module_form ) and[del flag] = 0 order by portal ", cnn);
                    }
                    adapter.Fill(dt1);
                    GridView2.DataSource = "";
                    GridView2.DataSource = dt1;
                    GridView2.DataBind();
                    GridView2.EditIndex = -1;
                    ShowData();
                    gridupdtaforadding();
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {

        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {

        }
    }
}