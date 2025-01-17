using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Drawing;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Configuration;
using System.Globalization;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Web.Services;

public partial class frm_feeEntry : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ConnectionString);
    SqlCommand cmd = new SqlCommand();
    Class1 cls = new Class1();
    FeeEntry fee = new FeeEntry();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["emp_id"] == null || Session["emp_id"] == "")
            {
                Response.Redirect("Login.aspx?msg=session");
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "loaddate()", true);
    }

    public class JsonHelper
    {
        public static string JsonSerializer<T>(T t)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, t);
            string jsonString = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();
            return jsonString;
        }
        public static T JsonDeserialize<T>(string jsonString)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T obj = (T)ser.ReadObject(ms);
            return obj;
        }
    }

    public string DataTableToJSON(DataTable table)
    {
        string JSONString = string.Empty;
        JSONString = JsonConvert.SerializeObject(table);
        return JSONString;
    }

    public void useless()
    {
        try
        {
            fee.type = null;
            fee.stud_id = null;
            fee.medium_id = null;
            fee.class_id = null;
            fee.division = null;
            fee.roll_no = null;
            fee.caste = null;
            fee.ayid = null;
            fee.duration = null;
            fee.dutype = null;
            fee.table = null;
            fee.user = null;
            fee.duration_id = null;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void lnksearch_Click(object sender, EventArgs e)
    {
        try
        {
            //div visibility
            if (txtstud_id.Text.Trim().Length == 8)
            {
                useless();
                string urlalias = cls.urls();
                string url = @urlalias + "FeeEntry/";

                fee.type = "yearlydata";
                fee.stud_id = txtstud_id.Text;

                string jsonString = JsonHelper.JsonSerializer<FeeEntry>(fee);
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(jsonString);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (StreamReader sr = new StreamReader(httpResponse.GetResponseStream()))
                {
                    string result = sr.ReadToEnd();
                    DataSet ds = JsonConvert.DeserializeObject<DataSet>(result);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        grdyear.DataSource = ds.Tables[0];
                        grdyear.DataBind();
                        lblmodalid.Text = ds.Tables[0].Rows[0]["Student_id"].ToString();
                        lblmodalname.Text = ds.Tables[0].Rows[0]["Student_Name"].ToString();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal('modalyear');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify('Student Id not found', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                    }
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify('Invalid Student Id', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
            }
            
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void grdyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtstud_id.Enabled = false;
            cal.Visible = false;
            details.Visible = false;
            status.Visible = false;
            btn.Visible = false;
            feetable.Visible = false;
            refund.Visible = false;
            GridViewRow gvrow = grdyear.SelectedRow;
            useless();
            string urlalias = cls.urls();
            string url = @urlalias + "FeeEntry/";

            fee.type = "feesdata";
            fee.stud_id = txtstud_id.Text;
            fee.ayid = ((Label)gvrow.FindControl("ayid")).Text;
            fee.class_id = ((Label)gvrow.FindControl("classid")).Text;
            fee.medium_id = ((Label)gvrow.FindControl("medium")).Text;

            string jsonString = JsonHelper.JsonSerializer<FeeEntry>(fee);
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(jsonString);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            using (StreamReader sr = new StreamReader(httpResponse.GetResponseStream()))
            {
                string result = sr.ReadToEnd();
                DataSet ds = JsonConvert.DeserializeObject<DataSet>(result);
                if (ds.Tables[1].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[1].NewRow();
                    dr[0] = "--Select--";
                    ds.Tables[1].Rows.InsertAt(dr, 0);
                    ddltype.DataSource = ds.Tables[1];
                    ddltype.DataTextField = "type";
                    ddltype.DataValueField = "value";
                    ddltype.DataBind();

                    txtstud_id.Text = ds.Tables[0].Rows[0]["Student_id"].ToString();
                    lblname.Text = ds.Tables[0].Rows[0]["Student_Name"].ToString();
                    lblyear.Text = ds.Tables[0].Rows[0]["Year"].ToString();
                    lblcategory.Text = ds.Tables[0].Rows[0]["categoryname"].ToString();
                    lblgr.Text = ds.Tables[0].Rows[0]["gr_no"].ToString();
                    lblmedium.Text = ds.Tables[0].Rows[0]["medium"].ToString();
                    lblclass.Text = ds.Tables[0].Rows[0]["standard"].ToString();
                    lbldivision.Text = ds.Tables[0].Rows[0]["Division"].ToString();
                    lblroll.Text = ds.Tables[0].Rows[0]["roll_no"].ToString();

                    // visible false values
                    glbayid.Text = ds.Tables[0].Rows[0]["AYID"].ToString();
                    glbcat.Text = ds.Tables[0].Rows[0]["category"].ToString();
                    glbclass.Text = ds.Tables[0].Rows[0]["class_id"].ToString();
                    glbdiv.Text = ds.Tables[0].Rows[0]["division_id"].ToString();
                    glbmed.Text = ds.Tables[0].Rows[0]["medium_id"].ToString();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModal('modalyear');", true);
                    feepanel.Visible = true;
                    load_grid(txtstud_id.Text, glbayid.Text);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify('Fees not defined', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                    ddltype.Enabled = false;
                    feepanel.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            cal.Visible = false;
            feetable.Visible = false;
            btn.Visible = false;
            ddlmode.SelectedIndex = 0;
            ddlmode.Enabled = false;
            details.Visible = false;
            status.Visible = false;
            txtpaydate.Text = "";
            if (ddltype.SelectedIndex > 0)
            {
                useless();
                string urlalias = cls.urls();
                string url = @urlalias + "FeeEntry/";

                fee.type = "duration";
                fee.stud_id = txtstud_id.Text;
                fee.ayid = glbayid.Text;
                fee.class_id = glbclass.Text;
                fee.medium_id = glbmed.Text;
                fee.dutype = ddltype.SelectedItem.ToString();

                string jsonString = JsonHelper.JsonSerializer<FeeEntry>(fee);
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(jsonString);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (StreamReader sr = new StreamReader(httpResponse.GetResponseStream()))
                {
                    string result = sr.ReadToEnd();
                    DataSet ds = JsonConvert.DeserializeObject<DataSet>(result);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlduration.Enabled = true;
                        DataRow dr = ds.Tables[0].NewRow();
                        dr[1] = "--Select--";
                        ds.Tables[0].Rows.InsertAt(dr, 0);
                        ddlduration.DataSource = ds.Tables[0];
                        ddlduration.DataTextField = "duration";
                        ddlduration.DataValueField = "duration_id";
                        ddlduration.DataBind();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify('Fees payment duration not defined', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                    }
                }
            }
            else
            {
                if (ddlduration.SelectedIndex > 0)
                {
                    ddlduration.SelectedIndex = 0;
                }
                ddlduration.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void ddlduration_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlmode.SelectedIndex = 0;
            details.Visible = false;
            status.Visible = false;
            txtpaydate.Text = "";
            feetable.Visible = false;
            btn.Visible = false;
            if (ddlduration.SelectedIndex > 0)
            {
                if (ddlpay.SelectedItem.ToString() == "All Fees")
                {
                    useless();
                    string urlalias = cls.urls();
                    string url = @urlalias + "FeeEntry/";

                    fee.type = "gridfee";
                    fee.stud_id = txtstud_id.Text;
                    fee.ayid = glbayid.Text;
                    fee.class_id = glbclass.Text;
                    fee.medium_id = glbmed.Text;
                    fee.dutype = ddltype.SelectedItem.ToString();
                    fee.caste = glbcat.Text;
                    fee.duration = ddlduration.SelectedValue.ToString();

                    string jsonString = JsonHelper.JsonSerializer<FeeEntry>(fee);
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                    httpWebRequest.ContentType = "application/json";
                    httpWebRequest.Method = "POST";

                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        streamWriter.Write(jsonString);
                        streamWriter.Flush();
                        streamWriter.Close();
                    }

                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                    using (StreamReader sr = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        string result = sr.ReadToEnd();
                        DataSet ds = JsonConvert.DeserializeObject<DataSet>(result);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            grdfees.DataSource = ds.Tables[0];
                            grdfees.DataBind();
                            if (ds.Tables[1].Rows.Count > 0)
                            {
                                lblall.Text = ds.Tables[1].Rows[0]["total"].ToString();
                                lblpaid.Text = ds.Tables[1].Rows[0]["paid"].ToString();
                                lblbalance.Text = ds.Tables[1].Rows[0]["balance"].ToString();
                                lblrefundable.Text=ds.Tables[1].Rows[0]["refundable"].ToString();
                                lblrefunded.Text = ds.Tables[1].Rows[0]["refunded"].ToString();
                                cal.Visible = true;
                                feetable.Visible = true;
                                btn.Visible = true;
                                ddlmode.Enabled = true;
                                if (ds.Tables[1].Rows[0]["balance"].ToString() == "0" && btnsave.Text == "Save")
                                {
                                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('No pending fees', { color: '#fff', background: '#198104', blur: 0.2, delay: 0 });", true);
                                }
                            }
                        }
                    }
                }
                else if (ddlpay.SelectedItem.ToString() == "Refund Fees")
                {
                    cal.Visible = false;
                    feetable.Visible = false;
                    btn.Visible = true;
                    ddlmode.Enabled = true;
                    refundable();
                }
            }
            else
            {
                ddlmode.Enabled = false;
                cal.Visible = false;
                btn.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void btnrefresh_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect(HttpContext.Current.Request.Url.ToString(), true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void chkselect_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = (sender as CheckBox).NamingContainer as GridViewRow;
            CheckBox cb=((CheckBox)grdfees.Rows[row.RowIndex].FindControl("chkselect"));
            if (cb.Checked == true)
            {
                string amount = ((Label)grdfees.Rows[row.RowIndex].FindControl("lblpending")).Text;
                ((TextBox)grdfees.Rows[row.RowIndex].FindControl("txtpay")).Enabled = true;
                ((TextBox)grdfees.Rows[row.RowIndex].FindControl("txtpay")).Text = amount;
            }
            else
            {
                ((TextBox)grdfees.Rows[row.RowIndex].FindControl("txtpay")).Enabled = false;
                ((TextBox)grdfees.Rows[row.RowIndex].FindControl("txtpay")).Text = "";
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void grdfees_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (btnsave.Text == "Save")
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (Convert.ToInt32(((Label)e.Row.FindControl("lblpending")).Text) <= 0)
                    {
                        ((TextBox)e.Row.FindControl("txtpay")).Enabled = false;
                        ((CheckBox)e.Row.FindControl("chkselect")).Enabled = false;
                        e.Row.Enabled = false;
                        e.Row.ToolTip = "Already Paid";
                        e.Row.ForeColor = Color.Green;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void chkall_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox cb = ((CheckBox)grdfees.HeaderRow.FindControl("chkall"));
            if (cb.Checked == true)
            {
                foreach (GridViewRow row in grdfees.Rows)
                {
                    if (row.Enabled == true && ((CheckBox)row.FindControl("chkselect")).Enabled == true)
                    {
                        ((CheckBox)row.FindControl("chkselect")).Checked = true;
                        string amount = ((Label)row.FindControl("lblpending")).Text;
                        ((TextBox)row.FindControl("txtpay")).Enabled = true;
                        ((TextBox)row.FindControl("txtpay")).Text = amount;
                    }
                }
            }
            else
            {
                foreach (GridViewRow row in grdfees.Rows)
                {
                    if (row.Enabled == true && ((CheckBox)row.FindControl("chkselect")).Enabled == true)
                    {
                        ((CheckBox)row.FindControl("chkselect")).Checked = false;
                        ((TextBox)row.FindControl("txtpay")).Enabled = false;
                        ((TextBox)row.FindControl("txtpay")).Text = "";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void txtpay_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (btnsave.Text == "Save")
            {
                GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;
                if (((TextBox)grdfees.Rows[row.RowIndex].FindControl("txtpay")).Text != "")
                {
                    int actual = Convert.ToInt32(((Label)grdfees.Rows[row.RowIndex].FindControl("lblpending")).Text);
                    int latest = Convert.ToInt32(((TextBox)grdfees.Rows[row.RowIndex].FindControl("txtpay")).Text);
                    if (latest > actual)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify('Amount Exceed', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                        ((TextBox)grdfees.Rows[row.RowIndex].FindControl("txtpay")).Text = actual.ToString();
                        ((TextBox)grdfees.Rows[row.RowIndex].FindControl("txtpay")).Focus();
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify('Amount cannot be blank', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("struct_id");
            dt.Columns.Add("Amount");
            dt.Columns.Add("Flag");

            string msg = validate();
            if (msg.ToString() == "")
            {
                if (btnsave.Text == "Save")
                {
                    if (ddlpay.SelectedItem.ToString() == "All Fees")
                    {
                        foreach (GridViewRow row in grdfees.Rows)
                        {
                            if (row.Enabled == true)
                            {
                                DataRow dr = dt.NewRow();
                                if (((CheckBox)row.FindControl("chkselect")).Checked == true)
                                {
                                    dr[0] = ((Label)row.FindControl("lblstructid")).Text;
                                    dr[1] = ((TextBox)row.FindControl("txtpay")).Text;
                                    dr[2] = "insert";
                                    dt.Rows.Add(dr);
                                }
                            }
                        }
                        if (dt.Rows.Count > 0)
                        {
                            string json = DataTableToJSON(dt);

                            useless();
                            string urlalias = cls.urls();
                            string url = @urlalias + "FeeEntry/";

                            fee.type = "savetran";
                            fee.stud_id = txtstud_id.Text;
                            fee.table = json.ToString();
                            fee.duration_id = ddlduration.SelectedValue.ToString();
                            fee.ayid = glbayid.Text.ToString();
                            fee.paydate = txtpaydate.Text;
                            fee.paymode = ddlmode.SelectedItem.ToString();
                            if (ddlmode.SelectedItem.ToString() == "Cheque" || ddlmode.SelectedItem.ToString() == "NEFT")
                            {
                                fee.bankname = txtbnkname.Text.ToString();
                                fee.branchname = txtbranch.Text.ToString();
                                fee.chdate = txtchdate.Text;
                                fee.chno = txtchno.Text.ToString();
                                fee.chstatus = ddlstatus.SelectedItem.ToString();
                            }
                            else if (ddlmode.SelectedItem.ToString() == "DD")
                            {
                                fee.bankname = txtbnkname.Text.ToString();
                                fee.branchname = txtbranch.Text.ToString();
                                fee.chdate = txtchdate.Text;
                                fee.chno = txtchno.Text.ToString();
                                fee.chstatus = "Clear";
                            }
                            else if (ddlmode.SelectedItem.ToString() == "Cash")
                            {
                                fee.chstatus = "Clear";
                            }

                            fee.fees_type = "Fees";
                            fee.user = Session["emp_id"].ToString();

                            string jsonString = JsonHelper.JsonSerializer<FeeEntry>(fee);
                            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                            httpWebRequest.ContentType = "application/json";
                            httpWebRequest.Method = "POST";

                            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                            {
                                streamWriter.Write(jsonString);
                                streamWriter.Flush();
                                streamWriter.Close();
                            }

                            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                            using (StreamReader sr = new StreamReader(httpResponse.GetResponseStream()))
                            {
                                string result = sr.ReadToEnd();
                                if (result.ToString().Contains("success") == true)
                                {
                                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Saved Successfully', { color: '#fff', background: '#198104', blur: 0.2, delay: 0 });", true);
                                }
                                else if (result.ToString().Contains("fail") == true)
                                {
                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify('Data Not Saved', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                                }
                                else if (result.ToString().Contains("No changes") == true)
                                {
                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify('No data to save', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                                }
                                btncancel_Click(sender, e);
                                load_grid(txtstud_id.Text, glbayid.Text);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify('Select fee structure to pay', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                        }
                    }
                    else if (ddlpay.SelectedItem.ToString() == "Refund Fees")
                    {
                        if (Convert.ToDouble(lblfinalrefund.Text) < Convert.ToDouble(txtrefund.Text))
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify('Refundable amount is "+ lblfinalrefund.Text + " ', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                        }
                        else if(txtrefund.Text=="0")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify('Refund cannot be 0', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                        }
                        else
                        {
                            useless();
                            string urlalias = cls.urls();
                            string url = @urlalias + "FeeEntry/";

                            fee.type = "savetranrefund";
                            fee.stud_id = txtstud_id.Text;
                            fee.table = txtrefund.Text;
                            fee.duration = txtref_reason.Text;
                            fee.duration_id = ddlduration.SelectedValue.ToString();
                            fee.ayid = glbayid.Text.ToString();
                            fee.paydate = txtpaydate.Text;
                            fee.paymode = ddlmode.SelectedItem.ToString();
                            if (ddlmode.SelectedItem.ToString() == "Cheque" || ddlmode.SelectedItem.ToString() == "NEFT")
                            {
                                fee.bankname = txtbnkname.Text.ToString();
                                fee.branchname = txtbranch.Text.ToString();
                                fee.chdate = txtchdate.Text;
                                fee.chno = txtchno.Text.ToString();
                                fee.chstatus = ddlstatus.SelectedItem.ToString();
                            }
                            else if (ddlmode.SelectedItem.ToString() == "DD")
                            {
                                fee.bankname = txtbnkname.Text.ToString();
                                fee.branchname = txtbranch.Text.ToString();
                                fee.chdate = txtchdate.Text;
                                fee.chno = txtchno.Text.ToString();
                                fee.chstatus = "Clear";
                            }
                            else if (ddlmode.SelectedItem.ToString() == "Cash")
                            {
                                fee.chstatus = "Clear";
                            }

                            fee.fees_type = "Refund";
                            fee.user = Session["emp_id"].ToString();

                            string jsonString = JsonHelper.JsonSerializer<FeeEntry>(fee);
                            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                            httpWebRequest.ContentType = "application/json";
                            httpWebRequest.Method = "POST";

                            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                            {
                                streamWriter.Write(jsonString);
                                streamWriter.Flush();
                                streamWriter.Close();
                            }

                            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                            using (StreamReader sr = new StreamReader(httpResponse.GetResponseStream()))
                            {
                                string result = sr.ReadToEnd();
                                if (result.ToString().Contains("success") == true)
                                {
                                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Saved Successfully', { color: '#fff', background: '#198104', blur: 0.2, delay: 0 });", true);
                                }
                                else if (result.ToString().Contains("fail") == true)
                                {
                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify('Data Not Saved', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                                }
                                else if (result.ToString().Contains("No changes") == true)
                                {
                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify('No data to save', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                                }
                                btncancel_Click(sender, e);
                                load_grid(txtstud_id.Text, glbayid.Text);
                            }
                        }
                    }
                }
                else if (btnsave.Text == "Update")
                {
                    if (ddlpay.SelectedItem.ToString() == "All Fees")
                    {
                        foreach (GridViewRow row in grdfees.Rows)
                        {
                            if (row.Enabled == true)
                            {
                                DataRow dr = dt.NewRow();
                                if (((CheckBox)row.FindControl("chkselect")).Checked == true)
                                {
                                    dr[0] = ((Label)row.FindControl("lblstructid")).Text;
                                    dr[1] = ((TextBox)row.FindControl("txtpay")).Text;
                                    if (((CheckBox)row.FindControl("chkselect")).Enabled == false)
                                    {
                                        dr[2] = "updatefee";
                                    }
                                    else
                                    {
                                        dr[2] = "insert";
                                    }
                                    dt.Rows.Add(dr);
                                }
                            }
                        }
                        if (dt.Rows.Count > 0)
                        {
                            string json = DataTableToJSON(dt);

                            useless();
                            string urlalias = cls.urls();
                            string url = @urlalias + "FeeEntry/";

                            fee.type = "updatetran";
                            fee.receipt_no = txtreceiptno.Text;
                            fee.stud_id = txtstud_id.Text;
                            fee.table = json.ToString();
                            fee.duration_id = ddlduration.SelectedValue.ToString();
                            fee.ayid = glbayid.Text.ToString();
                            fee.paydate = txtpaydate.Text;
                            fee.paymode = ddlmode.SelectedItem.ToString();
                            if (ddlmode.SelectedItem.ToString() == "Cheque" || ddlmode.SelectedItem.ToString() == "NEFT")
                            {
                                fee.bankname = txtbnkname.Text.ToString();
                                fee.branchname = txtbranch.Text.ToString();
                                fee.chdate = txtchdate.Text;
                                fee.chno = txtchno.Text.ToString();
                                fee.chstatus = ddlstatus.SelectedItem.ToString();
                            }
                            else if (ddlmode.SelectedItem.ToString() == "DD")
                            {
                                fee.bankname = txtbnkname.Text.ToString();
                                fee.branchname = txtbranch.Text.ToString();
                                fee.chdate = txtchdate.Text;
                                fee.chno = txtchno.Text.ToString();
                                fee.chstatus = "Clear";
                            }
                            else if (ddlmode.SelectedItem.ToString() == "Cash")
                            {
                                fee.chstatus = "Clear";
                            }

                            fee.fees_type = "Fees";
                            fee.user = Session["emp_id"].ToString();

                            string jsonString = JsonHelper.JsonSerializer<FeeEntry>(fee);
                            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                            httpWebRequest.ContentType = "application/json";
                            httpWebRequest.Method = "POST";

                            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                            {
                                streamWriter.Write(jsonString);
                                streamWriter.Flush();
                                streamWriter.Close();
                            }

                            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                            using (StreamReader sr = new StreamReader(httpResponse.GetResponseStream()))
                            {
                                string result = sr.ReadToEnd();
                                if (result.ToString().Contains("success") == true)
                                {
                                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Saved Successfully', { color: '#fff', background: '#198104', blur: 0.2, delay: 0 });", true);
                                }
                                else if (result.ToString().Contains("fail") == true)
                                {
                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify('Data Not Saved', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                                }
                                else if (result.ToString().Contains("No changes") == true)
                                {
                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify('No data to save', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                                }
                                btncancel_Click(sender, e);
                                load_grid(txtstud_id.Text, glbayid.Text);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify('Select fee structure to pay', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                        }
                    }
                    else if (ddlpay.SelectedItem.ToString() == "Refund Fees")
                    {
                        useless();
                        string urlalias = cls.urls();
                        string url = @urlalias + "FeeEntry/";

                        fee.type = "updatetranrefund";
                        fee.receipt_no = txtreceiptno.Text;
                        fee.stud_id = txtstud_id.Text;
                        fee.table = txtrefund.Text;
                        fee.duration = txtref_reason.Text;
                        fee.duration_id = ddlduration.SelectedValue.ToString();
                        fee.ayid = glbayid.Text.ToString();
                        fee.paydate = txtpaydate.Text;
                        fee.paymode = ddlmode.SelectedItem.ToString();
                        if (ddlmode.SelectedItem.ToString() == "Cheque" || ddlmode.SelectedItem.ToString() == "NEFT")
                        {
                            fee.bankname = txtbnkname.Text.ToString();
                            fee.branchname = txtbranch.Text.ToString();
                            fee.chdate = txtchdate.Text;
                            fee.chno = txtchno.Text.ToString();
                            fee.chstatus = ddlstatus.SelectedItem.ToString();
                        }
                        else if (ddlmode.SelectedItem.ToString() == "DD")
                        {
                            fee.bankname = txtbnkname.Text.ToString();
                            fee.branchname = txtbranch.Text.ToString();
                            fee.chdate = txtchdate.Text;
                            fee.chno = txtchno.Text.ToString();
                            fee.chstatus = "Clear";
                        }
                        else if (ddlmode.SelectedItem.ToString() == "Cash")
                        {
                            fee.chstatus = "Clear";
                        }

                        fee.fees_type = "Refund";
                        fee.user = Session["emp_id"].ToString();

                        string jsonString = JsonHelper.JsonSerializer<FeeEntry>(fee);
                        var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                        httpWebRequest.ContentType = "application/json";
                        httpWebRequest.Method = "POST";

                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            streamWriter.Write(jsonString);
                            streamWriter.Flush();
                            streamWriter.Close();
                        }

                        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                        using (StreamReader sr = new StreamReader(httpResponse.GetResponseStream()))
                        {
                            string result = sr.ReadToEnd();
                            if (result.ToString().Contains("success") == true)
                            {
                                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Saved Successfully', { color: '#fff', background: '#198104', blur: 0.2, delay: 0 });", true);
                            }
                            else if (result.ToString().Contains("fail") == true)
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify('Data Not Saved', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                            }
                            else if (result.ToString().Contains("No changes") == true)
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify('No data to save', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                            }
                            btncancel_Click(sender, e);
                            load_grid(txtstud_id.Text, glbayid.Text);
                        }
                    }
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify('" + msg + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    public string validate()
    {
        string msg="";
        if (ddlpay.SelectedIndex == 0)
        {
            msg = "Select fees type";
        }
        else if (ddltype.SelectedIndex == 0)
        {
            msg = "Select payment type";
        }
        else if (ddlduration.SelectedIndex == 0)
        {
            msg = "Select payment duration";
        }
        else if (ddlmode.SelectedIndex == 0)
        {
            msg = "Select payment mode";
        }
        else if (txtpaydate.Text=="")
        {
            msg = "Select payment date";
        }
        else if(ddlmode.SelectedIndex>0)
        {
            if (ddlmode.SelectedItem.ToString() == "Cheque" || ddlmode.SelectedItem.ToString() == "NEFT")
            {
                if (txtbnkname.Text == "")
                {
                    msg = "Add bank name";
                }
                else if (txtbranch.Text == "")
                {
                    msg = "Add branch name";
                }
                else if (txtchdate.Text == "")
                {
                    msg = "Select Cheque/DD/NEFT date";
                }
                else if (txtchno.Text == "")
                {
                    msg = "Add Cheque/DD/NEFT No.";
                }
                else if (ddlstatus.SelectedIndex == 0)
                {
                    msg = "Select Cheque/NEFT status";
                }    
            }
            else if (ddlmode.SelectedItem.ToString() == "DD")
            {
                if (txtbnkname.Text == "")
                {
                    msg = "Add bank name";
                }
                else if (txtbranch.Text == "")
                {
                    msg = "Add branch name";
                }
                else if (txtchdate.Text == "")
                {
                    msg = "Select Cheque/DD/NEFT date";
                }
                else if (txtchno.Text == "")
                {
                    msg = "Add Cheque/DD/NEFT No.";
                }
            }
        }
        if (ddlpay.SelectedItem.ToString() == "Refund Fees")
        {
            if (txtrefund.Text == "")
            {
                msg = "Add refund amount";
            }
            else if(txtref_reason.Text=="")
            {
                msg = "Add refund remark";
            }
        }
        return msg;
    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        try
        {
            ddlpay.SelectedIndex = 0;
            ddlpay.Enabled = true;
            btnsave.Text = "Save";
            recptno.Visible = false;
            ddlpay_SelectedIndexChanged(sender, e);
            grdfees.DataSource = null;
            grdfees.DataBind();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void ddlpay_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlduration.SelectedIndex > 0)
            {
                ddlduration.SelectedIndex = 0;
            }
            ddlduration.Enabled = false;
            cal.Visible = false;
            details.Visible = false;
            status.Visible = false;
            feetable.Visible = false;
            btn.Visible = false;
            ddlmode.SelectedIndex = 0;
            ddlmode.Enabled = false;
            txtpaydate.Text = ""; 
            refund.Visible = false;
            if (ddltype.SelectedIndex > 0)
            {
                ddltype.SelectedIndex = 0;
            }
            if (ddlpay.SelectedIndex == 0)
            {
                ddltype.Enabled = false;
            }
            else
            {
                ddltype.Enabled = true;
            }
            if (ddlpay.SelectedItem.ToString() == "All Fees")
            {
                refund.Visible = false;
            }
            else if (ddlpay.SelectedItem.ToString() == "Refund Fees")
            {
                refund.Visible = true;
                txtref_reason.Text = "";
                txtrefund.Text = "";
                lblfinalrefund.Text = "";
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    public void refundable()
    {
        useless();
        string urlalias = cls.urls();
        string url = @urlalias + "FeeEntry/";

        fee.type = "refundable";
        fee.stud_id = txtstud_id.Text;
        fee.ayid = glbayid.Text;
        fee.class_id = glbclass.Text;
        fee.medium_id = glbmed.Text;
        fee.dutype = ddltype.SelectedItem.ToString();
        fee.caste = glbcat.Text;
        fee.duration = ddlduration.SelectedValue.ToString();

        string jsonString = JsonHelper.JsonSerializer<FeeEntry>(fee);
        var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "POST";

        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {
            streamWriter.Write(jsonString);
            streamWriter.Flush();
            streamWriter.Close();
        }

        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

        using (StreamReader sr = new StreamReader(httpResponse.GetResponseStream()))
        {
            string result = sr.ReadToEnd();
            DataSet ds = JsonConvert.DeserializeObject<DataSet>(result);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtrefund.Text=ds.Tables[0].Rows[0]["refundable"].ToString();
                lblfinalrefund.Text = ds.Tables[0].Rows[0]["refundable"].ToString();
            }
        }
    }

    protected void ddlmode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtbnkname.Text = "";
            txtbranch.Text = "";
            txtchdate.Text = "";
            txtchno.Text = "";
            ddlstatus.SelectedIndex = 0;
            if (ddlmode.SelectedItem.ToString() == "Cheque" || ddlmode.SelectedItem.ToString() == "NEFT")
            {
                details.Visible = true;
                status.Visible = true;
            }
            else if (ddlmode.SelectedItem.ToString() == "DD")
            {
                details.Visible = true;
                status.Visible = false;
            }
            else
            {
                details.Visible = false;
                status.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    public void load_grid(string stud_id, string ayid)
    {
        try
        {
            useless();
            string urlalias = cls.urls();
            string url = @urlalias + "FeeEntry/";

            fee.type = "loadgrid";
            fee.stud_id = txtstud_id.Text;
            fee.ayid = glbayid.Text.ToString();

            string jsonString = JsonHelper.JsonSerializer<FeeEntry>(fee);
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(jsonString);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            using (StreamReader sr = new StreamReader(httpResponse.GetResponseStream()))
            {
                string result = sr.ReadToEnd();
                DataSet ds = JsonConvert.DeserializeObject<DataSet>(result);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdedit.DataSource = ds.Tables[0];
                    grdedit.DataBind();
                }
                else
                {
                    grdedit.DataSource = null;
                    grdedit.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void grdedit_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow gvrow = grdedit.SelectedRow;
            string urlalias = cls.urls();
            string url = @urlalias + "FeeEntry/";

            fee.type = "editdata";
            fee.stud_id = txtstud_id.Text;
            fee.ayid = glbayid.Text;
            fee.receipt_no = ((Label)gvrow.FindControl("lblrecptno")).Text;

            string jsonString = JsonHelper.JsonSerializer<FeeEntry>(fee);
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(jsonString);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            using (StreamReader sr = new StreamReader(httpResponse.GetResponseStream()))
            {
                string result = sr.ReadToEnd();
                DataSet ds = JsonConvert.DeserializeObject<DataSet>(result);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    btnsave.Text = "Update";
                    recptno.Visible = true;
                    if(ds.Tables[0].Rows[0]["type"].ToString()=="Fees")
                    {
                        ddlpay.SelectedValue = "All Fees";
                    }
                    else if(ds.Tables[0].Rows[0]["type"].ToString()=="Refund")
                    {
                        ddlpay.SelectedValue = "Refund Fees";
                    }
                    ddlpay_SelectedIndexChanged(sender, e);
                    ddltype.SelectedValue = ds.Tables[1].Rows[0]["value"].ToString();
                    ddltype_SelectedIndexChanged(sender, e);
                    ddlduration.SelectedValue = ds.Tables[0].Rows[0]["duration_id"].ToString();
                    ddlduration_SelectedIndexChanged(sender, e);
                    ddlpay.Enabled = false;
                    ddltype.Enabled = false;
                    ddlduration.Enabled = false;
                    ddlmode.SelectedValue = ds.Tables[0].Rows[0]["Recpt_mode"].ToString();
                    ddlmode_SelectedIndexChanged(sender, e);
                    txtpaydate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["Pay_Date"]).ToString("dd/MM/yyyy").Replace("-", "/");
                    txtreceiptno.Text = ds.Tables[0].Rows[0]["Recpt_no"].ToString();
                    if (ds.Tables[0].Rows[0]["Recpt_mode"].ToString() == "Cheque" || ds.Tables[0].Rows[0]["Recpt_mode"].ToString() == "NEFT")
                    {
                        txtbnkname.Text = ds.Tables[0].Rows[0]["Recpt_Bnk_Name"].ToString();
                        txtbranch.Text = ds.Tables[0].Rows[0]["Recpt_Bnk_Branch"].ToString();
                        txtchdate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["Recpt_Chq_dt"]).ToString("dd/MM/yyyy").Replace("-", "/");
                        txtchno.Text = ds.Tables[0].Rows[0]["Recpt_Chq_No"].ToString();
                        ddlstatus.SelectedValue = ds.Tables[0].Rows[0]["Chq_status"].ToString();
                    }
                    else if (ds.Tables[0].Rows[0]["Recpt_mode"].ToString() == "DD")
                    {
                        txtbnkname.Text = ds.Tables[0].Rows[0]["Recpt_Bnk_Name"].ToString();
                        txtbranch.Text = ds.Tables[0].Rows[0]["Recpt_Bnk_Branch"].ToString();
                        txtchdate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["Recpt_Chq_dt"]).ToString("dd/MM/yyyy").Replace("-", "/");
                        txtchno.Text = ds.Tables[0].Rows[0]["Recpt_Chq_No"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["type"].ToString() == "Fees")
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            foreach (GridViewRow row in grdfees.Rows)
                            {
                                string structid = ((Label)row.FindControl("lblstructid")).Text;
                                if (structid == ds.Tables[0].Rows[i]["struct_id"].ToString())
                                {
                                    ((CheckBox)row.FindControl("chkselect")).Checked = true;
                                    ((CheckBox)row.FindControl("chkselect")).Enabled = false;
                                    ((TextBox)row.FindControl("txtpay")).Enabled = true;
                                    ((TextBox)row.FindControl("txtpay")).Text = ds.Tables[0].Rows[i]["amount"].ToString();
                                }
                            }
                        }
                    }
                    else if (ds.Tables[0].Rows[0]["type"].ToString() == "Refund")
                    {
                        txtrefund.Text = ds.Tables[0].Rows[0]["amount"].ToString();
                        txtref_reason.Text = ds.Tables[0].Rows[0]["refund_details"].ToString();
                    }
                }
            }
            
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void grdedit_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue.Contains(","))
            {
                confirmValue = confirmValue.Split(',').Last();
            }
            if (confirmValue.ToString() == "OK")
            {
                GridViewRow gvrow = (GridViewRow)grdedit.Rows[e.RowIndex];
                string urlalias = cls.urls();
                string url = @urlalias + "FeeEntry/";

                fee.type = "deletefee";
                fee.stud_id = txtstud_id.Text;
                fee.ayid = glbayid.Text;
                fee.receipt_no = ((Label)gvrow.FindControl("lblrecptno")).Text;

                string jsonString = JsonHelper.JsonSerializer<FeeEntry>(fee);
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(jsonString);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (StreamReader sr = new StreamReader(httpResponse.GetResponseStream()))
                {
                    string result = sr.ReadToEnd();
                    if (result.ToString().Contains("success") == true)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Data Deleted', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
                    }
                    else if (result.ToString().Contains("fail") == true)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify('Data Not Deleted', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                    }
                    btncancel_Click(sender, e);
                    load_grid(txtstud_id.Text, glbayid.Text);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void grdedit_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if(e.CommandName=="printmodal")
        {
            int RowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grdedit.Rows[RowIndex];

            Label lblrecptno = (Label)row.FindControl("lblrecptno");
           
            lblrecptnumber.Text = lblrecptno.Text;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal('printmodal');", true);
        }
    }

    protected void btn_print_Click(object sender, EventArgs e)
    {
        if (ddl_section.SelectedItem.Text == "PREPRIMARY")
        {
            Session["recptno"] = lblrecptnumber.Text;
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "redirect('secondary_feerpt.aspx');", true);
        }
        else if (ddl_section.SelectedItem.Text == "PRIMARY")
        {
            Session["recptno"] = lblrecptnumber.Text;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "redirect('primary_feerpt.aspx');", true);
        }
        else if (ddl_section.SelectedItem.Text == "SECONDARY")
        {
            Session["recptno"] = lblrecptnumber.Text;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "redirect('secondary_feerpt.aspx');", true);

        }
    }

    protected void grdedit_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if(((Label)e.Row.FindControl("lbltype")).Text == "Refund")
                {
                    ((LinkButton)e.Row.FindControl("btnedit")).Enabled = false;
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }
}