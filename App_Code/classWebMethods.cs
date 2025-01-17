using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.IO;
using System.Web.Script.Serialization;
using System.Globalization;



/// <summary>
/// Summary description for classWebMethods
/// </summary>
public class classWebMethods
{
    QueryClass qryCls = new QueryClass();
    Class1 cls1 = new Class1();
    SqlDataReader resultset;
    bool bolinsert;
    int intddno;



    //ADD SHWETA FOR ADMISSION CONFIRM FORM===================================================================================================================


    public List<ListItem> getcourse(string fid)
    {
        string qry = "select Distinct Course_name,course_id from m_crs_course_tbl Where Del_Flag <>1 And Course_name <> '' And faculty_id='" + fid + "'";

        string constr = ConfigurationManager.ConnectionStrings["connect"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(qry))
            {
                List<ListItem> listData = new List<ListItem>();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(sdr[0])))
                        {
                            listData.Add(new ListItem
                            {
                                Value = sdr["course_id"].ToString(),
                                Text = sdr["Course_name"].ToString()
                            });
                        }
                    }
                }
                con.Close();
                return listData;
            }
        }
    }

    public List<ListItem> getfaculty()
    {
        string qry = " select faculty_name,faculty_Id from m_crs_faculty Where Del_Flag <>1 And faculty_name <> ''";

        string constr = ConfigurationManager.ConnectionStrings["connect"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(qry))
            {
                List<ListItem> listData = new List<ListItem>();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(sdr[0])))
                        {
                            listData.Add(new ListItem
                            {
                                Value = sdr["faculty_Id"].ToString(),
                                Text = sdr["faculty_name"].ToString()
                            });
                        }
                    }
                }
                con.Close();
                return listData;
            }
        }
    }
    public List<ListItem> getsubcourse(string course)
    {
        string qry = " select Distinct subcourse_name,subcourse_id from m_crs_subcourse_tbl Where Del_Flag <>1 And subcourse_name <> '' And course_id='" + course + "'";

        string constr = ConfigurationManager.ConnectionStrings["connect"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(qry))
            {
                List<ListItem> listData = new List<ListItem>();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(sdr[0])))
                        {
                            listData.Add(new ListItem
                            {
                                Value = sdr["subcourse_id"].ToString(),
                                Text = sdr["subcourse_name"].ToString()
                            });
                        }
                    }
                }
                con.Close();
                return listData;
            }
        }
    }
    public List<ListItem> getgroup(string subcourse)
    {
        string qry = "select Group_title,Group_id from m_crs_subjectgroup_tbl where subcourse_id='" + subcourse + "'";

        string constr = ConfigurationManager.ConnectionStrings["connect"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(qry))
            {
                List<ListItem> listData = new List<ListItem>();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(sdr[0])))
                        {
                            listData.Add(new ListItem
                            {
                                Value = sdr["Group_id"].ToString(),
                                Text = sdr["Group_title"].ToString()
                            });
                        }
                    }
                }
                con.Close();
                return listData;
            }
        }
    }


    public List<ListItem> getayidadm()
    {
        string qry = "select Duration , AYID from dbo.m_academic ORDER BY Duration DESC";

        string constr = ConfigurationManager.ConnectionStrings["connect"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(qry))
            {
                List<ListItem> listData = new List<ListItem>();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(sdr[0])))
                        {
                            listData.Add(new ListItem
                            {
                                Value = sdr["AYID"].ToString(),
                                Text = sdr["Duration"].ToString()
                            });
                        }
                    }
                }
                con.Close();
                return listData;
            }
        }
    }

    public string JsonConvert { get; set; }

    public List<ListItem> fill_bankuser()
    {
        String qry = "select distinct bankuser_id from bank_fee_confirm";

        string constr = ConfigurationManager.ConnectionStrings["connect"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(qry))
            {
                List<ListItem> course = new List<ListItem>();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        course.Add(new ListItem
                        {
                            Value = sdr["bankuser_id"].ToString(),
                            Text = sdr["bankuser_id"].ToString()
                        });
                    }
                }
                con.Close();
                return course;
            }
        }
    }

    public List<ListItem> filldepartment()
    {
        String qry = "select distinct Dept_id,department_name from m_department where del_flag=0";

        string constr = ConfigurationManager.ConnectionStrings["connect"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(qry))
            {
                List<ListItem> coure = new List<ListItem>();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        coure.Add(new ListItem
                        {
                            Value = sdr["Dept_id"].ToString(),
                            Text = sdr["department_name"].ToString()
                        });
                    }
                }
                con.Close();
                return coure;
            }
        }
    }

    public List<ListItem> filldesignation()
    {
        String qry = "select distinct Designation_Title,Designation_ID from m_designation where del_flag='0'";

        string constr = ConfigurationManager.ConnectionStrings["connect"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(qry))
            {
                List<ListItem> coure = new List<ListItem>();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        coure.Add(new ListItem
                        {
                            Value = sdr["Designation_ID"].ToString(),
                            Text = sdr["Designation_Title"].ToString()
                        });
                    }
                }
                con.Close();
                return coure;
            }
        }
    }


    public Employee[] savepersonaldata(string fname, string lname, string mname, string mothname, string dob, string doj, string gender, string bldgrp, string cat, string national, string marital, string email, string caste, string subcaste, string aadhar, string address1, string city1, string state1, string pincode1, string phoneno1, string telno1, string address2, string city2, string state2, string pincode2, string phoneno2, string telno2, string depart, string desig, string salary, string handicaped, string empid, string type, string pfno, string panno)
    {
        string str = "";
        List<Employee> empdet = new List<Employee>();
        Employee emp = new Employee();

        string constr = ConfigurationManager.ConnectionStrings["connect"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("m_emp_personal", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                if (type == "Insert")
                {
                    cmd.Parameters.Add("@type", type);
                    cmd.Parameters.Add("@emp_id", "");
                    cmd.Parameters.Add("@del_dt", DateTime.Now);
                }
                else
                {
                    cmd.Parameters.Add("@type", type);
                    cmd.Parameters.Add("@emp_id", empid);
                }

                cmd.Parameters.Add("@emp_fname", fname);
                cmd.Parameters.Add("@emp_mname", mname);
                cmd.Parameters.Add("@emp_lname", lname);
                cmd.Parameters.Add("@emp_mother_name", mothname);
                cmd.Parameters.Add("@emp_dob", DateTime.ParseExact(dob, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy"));
                cmd.Parameters.Add("@emp_doj", DateTime.ParseExact(doj, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy"));
                cmd.Parameters.Add("@emp_blood_group", bldgrp);
                cmd.Parameters.Add("@emp_gender", gender);
                cmd.Parameters.Add("@emp_maritial_status", marital);
                cmd.Parameters.Add("@emp_nationality", national);
                cmd.Parameters.Add("@emp_handicaped", handicaped);
                cmd.Parameters.Add("@emp_category", cat);
                cmd.Parameters.Add("@emp_cast", caste);
                cmd.Parameters.Add("@emp_photo", null);
                cmd.Parameters.Add("@emp_sign", null);
                cmd.Parameters.Add("@emp_phone1", telno1);
                cmd.Parameters.Add("@emp_phone2", telno2);

                cmd.Parameters.Add("@emp_mobile1", phoneno1);
                cmd.Parameters.Add("@emp_mobile2", phoneno2);
                cmd.Parameters.Add("@emp_email", email);
                cmd.Parameters.Add("@emp_p_f_num", pfno);
                cmd.Parameters.Add("@emp_pan_card_no", panno);
                cmd.Parameters.Add("@emp_tds_num", null);
                cmd.Parameters.Add("@emp_dricing_lic_no", null);
                cmd.Parameters.Add("@emp_pan_no", null);

                cmd.Parameters.Add("@emp_passport_no", null);
                cmd.Parameters.Add("@emp_address_curr", address1);
                cmd.Parameters.Add("@emp_state_curr", state1);
                cmd.Parameters.Add("@emp_pincode_curr", pincode1);
                cmd.Parameters.Add("@emp_address_per", address2);
                cmd.Parameters.Add("@emp_state_per", state2);

                cmd.Parameters.Add("@emp_pincode_per", pincode2);
                cmd.Parameters.Add("@date_of_leaving", null);
                cmd.Parameters.Add("@dept_id", depart);
                cmd.Parameters.Add("@emp_des_id", desig);

                cmd.Parameters.Add("@emp_from_date", DateTime.ParseExact(doj, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy"));
                cmd.Parameters.Add("@actual_basic_salary", salary);

                cmd.Parameters.Add("@del_flag", false);
                cmd.Parameters.Add("@emp_del_flag", 1);

                cmd.Connection = con;
                con.Open();
                //string message = Convert.ToString(cmd.ExecuteScalar());
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0 && dt.Rows[0][0].ToString().Length == 8)
                {
                    emp.msg = "Saved";
                    if (type == "Update")
                    {
                        emp.empid = empid;
                        string straad = "update m_employee_personal set emp_aadhar_no='" + aadhar + "' where emp_id='" + empid + "'";
                        cls1.DMLqueries(straad);
                    }
                    else
                    {
                        emp.empid = dt.Rows[0][0].ToString();
                        string straad = "update m_employee_personal set emp_aadhar_no='" + aadhar + "' where emp_id='" + dt.Rows[0][0].ToString() + "'";
                        cls1.DMLqueries(straad);
                    }
                }
                else
                {
                    emp.msg = "Not Saved";
                    emp.empid = "";
                }
            }
        }
        empdet.Add(emp);
        return empdet.ToArray();
    }

    public Employee[] getempdata(string inputval)
    {
        string str = "";
        str = "select distinct top 1 e.emp_id,	emp_fname,	emp_mname,	emp_lname,	emp_mother_name,convert(varchar(10),emp_dob,103) emp_dob,convert(varchar(10),emp_doj,103)	emp_doj,	emp_blood_group,	emp_gender,	emp_maritial_status,	emp_nationality,	emp_handicaped,	emp_category,	emp_cast,emp_phone1,emp_phone2,	emp_mobile1,	emp_mobile2,	emp_email,	emp_p_f_num,	emp_pan_card_no,	emp_tds_num,	emp_dricing_lic_no,	emp_pan_no,	emp_passport_no,	emp_address_curr,	emp_state_curr	,emp_cast,	emp_pincode_curr,emp_address_per,emp_state_per,	emp_pincode_per,convert(varchar(10),date_of_leaving,103)	date_of_leaving	,del_flag,d.emp_dept_id,d.emp_des_id,d.actual_basic_salary,d.jobtype	from m_employee_personal e,employee_department_des d where (e.emp_id like '%" + inputval + "%' or emp_fname like '%" + inputval + "%' or emp_lname like '%" + inputval + "%' or emp_mname like '%" + inputval + "%')  and e.emp_id=d.emp_id and del_flag=0 and d.emp_del_flag=1";

        DataTable dt = new DataTable();
        dt = cls1.fillDataTable(str);
        List<Employee> empdet = new List<Employee>();

        if (dt.Rows.Count > 0)
        {
            foreach (DataRow drrow in dt.Rows)
            {
                Employee emp = new Employee();
                emp.countemp = dt.Rows.Count;
                emp.msg = "";
                emp.empid = drrow["emp_id"].ToString();
                emp.fname = drrow["emp_fname"].ToString();
                emp.lname = drrow["emp_lname"].ToString();
                emp.mname = drrow["emp_mname"].ToString();
                emp.mothname = drrow["emp_mother_name"].ToString();
                emp.dob = drrow["emp_dob"].ToString();
                emp.doj = drrow["emp_doj"].ToString();
                emp.gender = drrow["emp_gender"].ToString();
                emp.bldgrp = drrow["emp_blood_group"].ToString();
                emp.cat = drrow["emp_category"].ToString();
                emp.national = drrow["emp_nationality"].ToString();
                emp.marital = drrow["emp_maritial_status"].ToString();
                emp.email = drrow["emp_email"].ToString();
                emp.caste = drrow["emp_cast"].ToString();
                emp.address1 = drrow["emp_address_curr"].ToString();
                //emp.city1 = drrow[""].ToString();
                emp.state1 = drrow["emp_state_curr"].ToString();
                emp.pincode1 = drrow["emp_pincode_curr"].ToString();
                emp.phoneno1 = drrow["emp_mobile1"].ToString();
                emp.telno1 = drrow["emp_phone1"].ToString();
                emp.address2 = drrow["emp_address_per"].ToString();
                //emp.city2 = drrow[""].ToString();
                emp.state2 = drrow["emp_state_per"].ToString();
                emp.pincode2 = drrow["emp_pincode_per"].ToString();
                emp.phoneno2 = drrow["emp_phone2"].ToString();
                emp.telno2 = drrow["emp_mobile2"].ToString();
                emp.depart = drrow["emp_dept_id"].ToString();
                emp.desig = drrow["emp_des_id"].ToString();
                emp.salary = drrow["actual_basic_salary"].ToString();
                emp.handicap = drrow["emp_handicaped"].ToString();
                emp.jobtype = drrow["jobtype"].ToString();
                empdet.Add(emp);
            }
        }
        else
        {
            Employee emp = new Employee();
            emp.countemp = 0;
            emp.msg = "No Data Found";
            empdet.Add(emp);
        }
        return empdet.ToArray();
    }

    public bool saveeducation(string empid, string colgname, string obt, string out1, string board, string degree, string type, string class1, string pursuing, string yearpass, string month, string subject, string dmltype)
    {
        string str = "";

        string constr = ConfigurationManager.ConnectionStrings["connect"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("m_emp_educat", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@type", "Insert_Update");
                cmd.Parameters.Add("@emp_id", empid);
                cmd.Parameters.Add("@emp_coll_name", colgname);
                cmd.Parameters.Add("@emp_unversity_board_name", board);
                cmd.Parameters.Add("@emp_degree_name", degree);
                cmd.Parameters.Add("@emp_degree_type", type);
                cmd.Parameters.Add("@emp_specialization_subj", subject);
                cmd.Parameters.Add("@emp_month_of_passing", month);
                cmd.Parameters.Add("@emp_year_of_passing", yearpass);

                cmd.Parameters.Add("@emp_marks_obtained", obt);
                cmd.Parameters.Add("@emp_total_marks", out1);
                cmd.Parameters.Add("@emp_class_secured", class1);
                cmd.Parameters.Add("@emp_pursuing", pursuing);
                con.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
                if (dt.Rows[0][0].ToString() == "DATA SAVED" || dt.Rows[0][0].ToString() == "SAVED SUCCESSFULLY")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }

    public Employee[] showeducation(string empid)
    {
        string str = "";
        str = "select emp_id,	emp_coll_name,	emp_unversity_board_name,	emp_degree_name,	emp_degree_type,	emp_specialization_subj,	emp_month_of_passing,	emp_year_of_passing,	emp_marks_obtained	,emp_total_marks,	emp_class_secured,case when emp_pursuing=0 then 'Not' else 'Yes' end as emp_pursuing from employee_education_details where emp_id='" + empid + "'";

        DataTable dt = new DataTable();
        dt = cls1.fillDataTable(str);
        List<Employee> empdet = new List<Employee>();

        if (dt.Rows.Count > 0)
        {
            foreach (DataRow drrow in dt.Rows)
            {
                Employee emp = new Employee();
                emp.colgname = drrow["emp_coll_name"].ToString();
                emp.boardname = drrow["emp_unversity_board_name"].ToString();
                emp.degname = drrow["emp_degree_name"].ToString();
                emp.degtype = drrow["emp_degree_type"].ToString();
                emp.class1 = drrow["emp_class_secured"].ToString();
                emp.pursuing = drrow["emp_pursuing"].ToString();
                emp.obtmk = drrow["emp_marks_obtained"].ToString();
                emp.totmrk = drrow["emp_total_marks"].ToString();
                emp.subject = drrow["emp_specialization_subj"].ToString();
                emp.mop = drrow["emp_month_of_passing"].ToString();
                emp.yop = drrow["emp_year_of_passing"].ToString();
                emp.msg = "";
                empdet.Add(emp);
            }
        }
        else
        {
            Employee emp = new Employee();
            emp.msg = "No Data Found";
            empdet.Add(emp);
        }
        return empdet.ToArray();
    }

    public bool savedepart(string empid, string depart, string desig, string salary, string doj, string criteria)
    {
        string constr = ConfigurationManager.ConnectionStrings["connect"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("m_emp_dept_des", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // cmd.Parameters.Add("@type", "Insert_Update");
                cmd.Parameters.Add("@emp_id", empid);
                cmd.Parameters.Add("@emp_dept_id", depart);
                cmd.Parameters.Add("@emp_des_id", desig);
                cmd.Parameters.Add("@emp_from_date", DateTime.ParseExact(doj, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy"));
                cmd.Parameters.Add("@emp_to_date", null);
                cmd.Parameters.Add("@emp_del_flag", 1);
                cmd.Parameters.Add("@actual_basic_salary", salary);
                cmd.Parameters.Add("@type", "Insert_Update");
                cmd.Parameters.Add("@jobtype", criteria);

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
                if (dt.Rows[0][0].ToString() == "DATA SAVED" || dt.Rows[0][0].ToString() == "SAVED SUCCESSFULLY")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }

    public bool saveaccount(string empid, string acntno, string acntype, string bankname, string branch, string isalary)
    {
        string constr = ConfigurationManager.ConnectionStrings["connect"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("m_emp_acc_det", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@type", "Insert_Update");
                cmd.Parameters.Add("@emp_id", empid);
                cmd.Parameters.Add("@emp_bank_acc_no", acntno);
                cmd.Parameters.Add("@bank_flag", isalary);
                cmd.Parameters.Add("@emp_bank_acc_type", acntype);
                cmd.Parameters.Add("@emp_bank_name", bankname);
                cmd.Parameters.Add("@emp_bank_branch", branch);

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
                if (dt.Rows[0][0].ToString().Contains("DATA SAVED") || dt.Rows[0][0].ToString().Contains("SAVED SUCCESSFULLY"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }

    public bool savexperience(string empid, string org, string depart, string desig, string prevsal, string doj, string dol)
    {
        string constr = ConfigurationManager.ConnectionStrings["connect"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("m_emp_experience", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@type", "Insert_Update");
                cmd.Parameters.Add("@emp_id", empid);
                cmd.Parameters.Add("@emp_previous_organization", org);
                cmd.Parameters.Add("@emp_previous_designation", desig);
                cmd.Parameters.Add("@emp_previous_job_dept", depart);
                cmd.Parameters.Add("@emp_previous_salary", prevsal);
                cmd.Parameters.Add("@emp_previous_job_from", DateTime.ParseExact(doj, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy"));
                cmd.Parameters.Add("@emp_previous_job_to", DateTime.ParseExact(dol, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy"));
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
                if (dt.Rows[0][0].ToString().Contains("DATA SAVED") || dt.Rows[0][0].ToString().Contains("SAVED SUCCESSFULLY"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }

    public Employee[] getdepartdata(string empid)
    {
        string str = "";
        str = "select emp_id,	m.Department_name,emp_dept_id,emp_des_id,	d.Designation_Title,convert(varchar(10),emp_from_date,103) as emp_from_date,	emp_to_date,	emp_del_flag,	actual_basic_salary,	jobtype from employee_department_des e,m_department m,m_designation d where e.emp_id='" + empid + "' and m.Dept_id=e.emp_dept_id and d.Designation_ID=e.emp_des_id";

        DataTable dt = new DataTable();
        dt = cls1.fillDataTable(str);
        List<Employee> empdet = new List<Employee>();

        if (dt.Rows.Count > 0)
        {
            foreach (DataRow drrow in dt.Rows)
            {
                Employee emp = new Employee();
                emp.empid = drrow["emp_id"].ToString();
                emp.depart = drrow["Department_name"].ToString();
                emp.desig = drrow["Designation_Title"].ToString();
                emp.deptid = drrow["emp_dept_id"].ToString();
                emp.desid = drrow["emp_des_id"].ToString();
                emp.doj = drrow["emp_from_date"].ToString();
                emp.todt = drrow["emp_to_date"].ToString();
                emp.delflag = drrow["emp_del_flag"].ToString();
                emp.salary = drrow["actual_basic_salary"].ToString();
                emp.jobtype = drrow["jobtype"].ToString();
                emp.msg = "";
                empdet.Add(emp);
            }
        }
        else
        {
            Employee emp = new Employee();
            emp.msg = "No Data Found";
            empdet.Add(emp);
        }
        return empdet.ToArray();
    }

    public Employee[] getaccountdata(string empid)
    {
        string str = "";
        str = "SELECT emp_id,case when bank_flag=0 then 'Not' else 'Yes' end as bank_flag,emp_bank_acc_no,case when emp_bank_acc_type=0 then 'Saving' else 'Current' end as emp_bank_acc_type,emp_bank_name,emp_bank_branch FROM emp_bank_details WHERE emp_id='" + empid + "'";

        DataTable dt = new DataTable();
        dt = cls1.fillDataTable(str);
        List<Employee> empdet = new List<Employee>();

        if (dt.Rows.Count > 0)
        {
            foreach (DataRow drrow in dt.Rows)
            {
                Employee emp = new Employee();
                emp.empid = drrow["emp_id"].ToString();
                emp.isalary = drrow["bank_flag"].ToString();
                emp.accntno = drrow["emp_bank_acc_no"].ToString();
                emp.acntype = drrow["emp_bank_acc_type"].ToString();
                emp.bnkname = drrow["emp_bank_name"].ToString();
                emp.branch = drrow["emp_bank_branch"].ToString();
                emp.msg = "";
                empdet.Add(emp);
            }
        }
        else
        {
            Employee emp = new Employee();
            emp.msg = "No Data Found";
            empdet.Add(emp);
        }
        return empdet.ToArray();
    }

    public Employee[] getexpdata(string empid)
    {
        string str = "";
        str = "select emp_id,	emp_previous_organization,	emp_previous_designation,	emp_previous_job_dept,	emp_previous_salary,	convert(varchar(10),emp_previous_job_from,103) as emp_previous_job_from,convert(varchar(10),emp_previous_job_to,103) as emp_previous_job_to from employee_experience_details where emp_id='" + empid + "'";

        DataTable dt = new DataTable();
        dt = cls1.fillDataTable(str);
        List<Employee> empdet = new List<Employee>();

        if (dt.Rows.Count > 0)
        {
            foreach (DataRow drrow in dt.Rows)
            {
                Employee emp = new Employee();
                emp.empid = drrow["emp_id"].ToString();
                emp.comp = drrow["emp_previous_organization"].ToString();
                emp.desig = drrow["emp_previous_designation"].ToString();
                emp.depart = drrow["emp_previous_job_dept"].ToString();
                emp.prvsal = drrow["emp_previous_salary"].ToString();
                emp.jfdate = drrow["emp_previous_job_from"].ToString();
                emp.jtdate = drrow["emp_previous_job_to"].ToString();
                emp.msg = "";
                empdet.Add(emp);
            }
        }
        else
        {
            Employee emp = new Employee();
            emp.msg = "No Data Found";
            empdet.Add(emp);
        }
        return empdet.ToArray();
    }

    public Employee[] getempPhoto(string empid)
    {
        List<Employee> details = new List<Employee>();
        Employee modal = new Employee();
        if (empid != "")
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Extension");
            dt.Rows.Add(".jpg");
            dt.Rows.Add(".jpeg");
            dt.Rows.Add(".png");
            dt.Rows.Add(".gif");
            dt.Rows.Add(".bmp");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                System.IO.DirectoryInfo dirInfo = new System.IO.DirectoryInfo(System.Web.HttpContext.Current.Server.MapPath("~/EmployeePhoto/photo/" + empid + dt.Rows[i]["Extension"].ToString()));
                System.IO.DirectoryInfo dirsign = new System.IO.DirectoryInfo(System.Web.HttpContext.Current.Server.MapPath("~/EmployeePhoto/sign/" + empid + dt.Rows[i]["Extension"].ToString()));

                string picpath = dirInfo.FullName;
                picpath = picpath.Replace("\\", "\\\\");

                string signpath = dirsign.FullName;
                signpath = signpath.Replace("\\", "\\\\");
                //FileInfo[] listfiles = dirInfo.GetFiles();

                if (File.Exists(picpath) || File.Exists(signpath))
                {
                    if (File.Exists(picpath))
                    {
                        modal.photo = "EmployeePhoto/photo/" + empid + dt.Rows[i]["Extension"].ToString();
                    }

                    if (File.Exists(signpath))
                    {
                        modal.sign = "EmployeePhoto/sign/" + empid + dt.Rows[i]["Extension"].ToString();
                    }
                    modal.msg = "Uploaded";
                    details.Add(modal);
                }
                else
                {
                    if (modal.msg != "")
                    {

                    }
                    else
                    {
                        modal.msg = "";
                    }
                }
            }
            return details.ToArray();
        }
        else
        {
            modal.msg = "Give Stud_id";
            details.Add(modal);
            return details.ToArray();
        }

    }

    public employee[] employeerecord(string emp_id, string fname, string mname, string lname, string mothernme, string emailid, string dobirth, string dojoin, string mobileno, string salary, string departid, string desigid, string gender, string mobile2, string role, string logincategory, string grouplist, string form_name, string address, string bloodgroup, string emp_quali, string pincode, string state)
    {
        List<employee> details = new List<employee>();

            employee retval = new employee();
            string constr = ConfigurationManager.ConnectionStrings["connect"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand("m_emp_personal", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@emp_id", emp_id);
            cmd.Parameters.AddWithValue("@emp_fname", fname.Replace("@","'"));
            cmd.Parameters.AddWithValue("@emp_mname", mname.Replace("@", "'"));
            cmd.Parameters.AddWithValue("@emp_lname", lname.Replace("@", "'"));
            cmd.Parameters.AddWithValue("@emp_mother_name", mothernme.Replace("@", "'"));
            cmd.Parameters.AddWithValue("@emp_dob", DateTime.ParseExact(dobirth, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy"));
            cmd.Parameters.AddWithValue("@emp_trust", DateTime.ParseExact(dojoin, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy"));
            cmd.Parameters.AddWithValue("@emp_gov", DateTime.ParseExact(dojoin, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy"));
            cmd.Parameters.AddWithValue("@emp_blood_group", bloodgroup.ToString());
            cmd.Parameters.AddWithValue("@emp_gender", gender);
            cmd.Parameters.AddWithValue("@emp_maritial_status", null);
            cmd.Parameters.AddWithValue("@emp_nationality", "Indian");
            cmd.Parameters.AddWithValue("@emp_qual", emp_quali);
            cmd.Parameters.AddWithValue("@emp_handicaped", null);
            cmd.Parameters.AddWithValue("@emp_category", "");
            cmd.Parameters.AddWithValue("@emp_cast", "");
            cmd.Parameters.AddWithValue("@emp_photo", null);
            cmd.Parameters.AddWithValue("@emp_sign", null);
            cmd.Parameters.AddWithValue("@emp_phone1", "");
            cmd.Parameters.AddWithValue("@emp_phone2", "");
            cmd.Parameters.AddWithValue("@emp_mobile1", mobileno);
            cmd.Parameters.AddWithValue("@emp_mobile2", mobile2);
            cmd.Parameters.AddWithValue("@emp_email", emailid);
            cmd.Parameters.AddWithValue("@emp_pfno", "");
            cmd.Parameters.AddWithValue("@emp_aadharno", "");
            cmd.Parameters.AddWithValue("@emp_pan", "");
            cmd.Parameters.AddWithValue("@emp_address_curr", address);
            cmd.Parameters.AddWithValue("@emp_state_curr", state);
            cmd.Parameters.AddWithValue("@emp_pincode_curr", pincode);
            cmd.Parameters.AddWithValue("@emp_address_per", address);
            cmd.Parameters.AddWithValue("@emp_state_per", state);
            cmd.Parameters.AddWithValue("@emp_pincode_per", pincode);
            cmd.Parameters.AddWithValue("@date_of_leaving", null);
            cmd.Parameters.AddWithValue("@del_flag", 0);
            cmd.Parameters.AddWithValue("@del_dt", DBNull.Value);
            cmd.Parameters.AddWithValue("@dept_id", departid);
            cmd.Parameters.AddWithValue("@emp_des_id", desigid);
            cmd.Parameters.AddWithValue("@emp_from_date", DateTime.ParseExact(dojoin, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy"));
            cmd.Parameters.AddWithValue("@emp_to_date", DBNull.Value);
            cmd.Parameters.AddWithValue("@emp_del_flag", 1);
            cmd.Parameters.AddWithValue("@actual_basic_salary", salary);

            string query = "";
            if (emp_id != "")
            {
                cmd.Parameters.AddWithValue("@type", "Update");
                query = "update emp_department_des set actual_basic_salary='" + salary + "',emp_dept_id='" + departid + "',emp_des_id='" + desigid + "',emp_from_date='" + DateTime.ParseExact(dojoin, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy") + "' where emp_id='" + emp_id + "'";
            }
            else
            {
                cmd.Parameters.AddWithValue("@type", "Insert");
            }
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            con.Close();
            if (dt.Rows.Count > 0)
            {
                
                string[] arrrole = form_name.Split(','); ;
                string extraformname = "";
                string str1 = "select * from web_tp_roletype where role_id='" + role + "'";
                DataTable dtr = cls1.fillDataTable(str1);
                string[] arr = dtr.Rows[0]["form_name"].ToString().Split(',');
                var myList = new List<string>();
                myList.AddRange(arrrole);
                myList.AddRange(arr);
                var finallist = new List<string>();
                finallist = myList.ToList();

                for (int j = 0; j < arr.Length; j++)
                {
                    for (int i = 0; i < myList.Count; i++)
                    {
                        if (arr[j] == myList[i])
                        {
                            finallist.Remove(arr[j]);
                        }
                    }
                }
                string result = String.Join(",",finallist);
                string qry = "";
                string str = "select * from web_tp_login where emp_id='" + dt.Rows[0][0].ToString().ToUpper() + "'";
                DataTable dtp = cls1.fillDataTable(str);
                if (dtp.Rows.Count > 0)
                {
                    if (dtp.Rows[0]["role_id"].ToString() == role)
                    {
                        qry = "update web_tp_login set role_id='" + role + "',group_ids='" + grouplist + "',col1='" + logincategory + "',col2='" + result + "',mod_date=getdate(),del_flag=0 where  emp_id='" + dt.Rows[0][0].ToString().ToUpper() + "'  ";
                    }
                    else
                    {

                        qry = "update web_tp_login set role_id='" + role + "',group_ids='" + grouplist + "',col1='" + logincategory + "',col2='" + result + "',mod_date=getdate(),del_flag=0  where emp_id='" + dt.Rows[0][0].ToString().ToUpper() + "'";
                    }
                }
                else
                {
                    qry = "insert into web_tp_login values('" + dt.Rows[0][0].ToString().ToUpper() + "','" + role + "','" + dt.Rows[0][0] + "','" + dt.Rows[0][0] + "','" + grouplist + "','" + logincategory + "','" + result + "','','','',getdate(),null,0)";
                }

                if (cls1.DMLqueries(qry))
                {
                    retval.empid = dt.Rows[0][0].ToString().ToUpper();
                    retval.msg = "Saved";
                }

                if (query != "" && qry != "")
                {
                    if (cls1.DMLqueries(query) == true)
                    {
                        retval.empid = dt.Rows[0][0].ToString().ToUpper();
                        retval.msg = "Saved";
                    }

                }
                else
                {
                    retval.empid = dt.Rows[0][0].ToString().ToUpper();
                    retval.msg = "Saved";
                }
                details.Add(retval);
            }
            else
            {
                retval.msg = "No Data Saved";
                details.Add(retval);
            }



        
        return details.ToArray();
    }

    public List<ListItem> fill_medium()
    {
        string qry = "";

        qry = "select distinct med_id, medium from mst_medium_tbl where del_flag = '0'";

        string constr = ConfigurationManager.ConnectionStrings["connect"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(qry))
            {
                List<ListItem> course = new List<ListItem>();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        course.Add(new ListItem
                        {
                            Value = sdr["med_id"].ToString(),
                            Text = sdr["medium"].ToString()
                        });
                    }
                }
                con.Close();
                return course;
            }
        }
    }

    public List<ListItem> fill_standard(string medium)
    {
        string qry = "";

        qry = "select distinct std_id,std_name from mst_standard_tbl where del_flag = '0' and med_id='" + medium + "'";

        string constr = ConfigurationManager.ConnectionStrings["connect"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(qry))
            {
                List<ListItem> course = new List<ListItem>();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        course.Add(new ListItem
                        {
                            Value = sdr["std_id"].ToString(),
                            Text = sdr["std_name"].ToString()
                        });
                    }
                }
                con.Close();
                return course;
            }
        }
    }

    public List<ListItem> fill_form_name(string module_name)
    {
        string qry = "";
        string module = "";

        module = module_name.ToString().Replace(",", "','");

        qry = "select Sr_no,Form_Name from Register_Form  where Portal in ('" + module + "') and [del flag]=0 order by portal ";

        string constr = ConfigurationManager.ConnectionStrings["connect"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(qry))
            {
                List<ListItem> course = new List<ListItem>();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        course.Add(new ListItem
                        {
                            Value = sdr["Sr_no"].ToString().Trim(),
                            Text = sdr["Form_Name"].ToString().Trim()
                        });
                    }
                }
                con.Close();
                return course;
            }
        }
    }

    public List<ListItem> fill_form_name_load(string roleid)
    {
        string qry = "";
        
        qry = "SELECT * FROM   ( select form_name from web_tp_roletype where role_id='" + roleid + "' and del_flag=0) as a,( select distinct stuff((select distinct ','+ portal from Register_Form where Sr_no in (select x.Item as col2 from web_tp_roletype t cross apply (select Item from dbo.SplitString(t.form_name,',') ) x where t.role_id='" + roleid + "') for xml path('')),1,1,'') as module from  Register_Form) as b";

        string constr = ConfigurationManager.ConnectionStrings["connect"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(qry))
            {
                List<ListItem> course = new List<ListItem>();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        course.Add(new ListItem
                        {
                            Value = sdr["Form_Name"].ToString().Trim(),
                            Text = sdr["module"].ToString().Trim()
                        });
                    }
                }
                con.Close();
                return course;
            }
        }
    }

    public List<ListItem> fill_model()
    {
        string qry = "";

        qry = "select distinct sr_no,Module_name from [dbo].[Module_form] where del_flag=0";

        string constr = ConfigurationManager.ConnectionStrings["connect"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(qry))
            {
                List<ListItem> course = new List<ListItem>();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        course.Add(new ListItem
                        {
                            Value = sdr["sr_no"].ToString().Trim(),
                            Text = sdr["Module_name"].ToString().Trim()
                        });
                    }
                }
                con.Close();
                return course;
            }
        }
    }

    public List<ListItem> fill_form_name_new()
    {
        string qry = "";

        qry = "select Sr_no,Form_Name from Register_Form where [del flag]=0 order by portal ";

        string constr = ConfigurationManager.ConnectionStrings["connect"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(qry))
            {
                List<ListItem> course = new List<ListItem>();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        course.Add(new ListItem
                        {
                            Value = sdr["Sr_no"].ToString().Trim(),
                            Text = sdr["Form_Name"].ToString().Trim()
                        });
                    }
                }
                con.Close();
                return course;
            }
        }
    }

    public employee[] getemployeedata(string empid)
    {
        string query = "";

        //query = "select	emp_fname,emp_mname,emp_lname,emp_mother_name,convert (varchar(10),emp_dob,103) as emp_dob,convert (varchar(10),emp_doj,103) as emp_doj,emp_gender,emp_phone1,emp_mobile1,emp_email, e.emp_dept_id,e.emp_des_id,e.actual_basic_salary,w.role_id,w.group_ids,w.col1,w.col2 from m_employee_personal m,employee_department_des e,web_tp_login w where m.emp_id='" + empid + "' and m.emp_id=e.emp_id and m.emp_id=w.emp_id and w.del_flag=0 and m.del_flag=0 ";
        query = "  SELECT * FROM   ( select  emp_fname,emp_mname,emp_lname,emp_mother_name,convert (varchar(10),emp_dob,103) as emp_dob,convert (varchar(10),emp_trust,103) as emp_doj,emp_photo,emp_sign,emp_blood_group,emp_address_curr,emp_pincode_curr,emp_qual,emp_state_curr,emp_gender,emp_mobile2,emp_mobile1,emp_email, e.emp_dept_id,e.emp_des_id,e.actual_basic_salary,w.role_id,w.group_ids,w.col1,w.col2,w.col3,w.col4,rt.form_name as defaultforms,(rt.form_name+','+w.col2) as allforms  from mst_employee_personal m,emp_department_des e,web_tp_login w,web_tp_roletype rt where rt.del_flag=0 and rt.role_id=w.role_id and m.emp_id='" + empid + "' and m.emp_id=e.emp_id and m.emp_id=w.emp_id and w.del_flag=0 and m.del_flag=0 ) AS A,(select distinct stuff((select distinct ','+ portal from Register_Form where [Del Flag]=0 and Sr_no in (select y.Item as col2 from web_tp_roletype r cross apply (select Item from dbo.SplitString(r.form_name,',') ) y where r.role_id in (select role_id from web_tp_login where emp_id='" + empid + "')) for xml path('')),1,1,'') as defaultmodule,stuff((select distinct ','+ portal from Register_Form where [Del Flag]=0 and Sr_no in (select x.Item as col2 from web_tp_login t cross apply (select Item from dbo.SplitString(t.col2,',') ) x where t.emp_id='" + empid + "' union all select y.Item as col2 from web_tp_roletype r cross apply (select Item from dbo.SplitString(r.form_name,',') ) y where r.role_id in (select role_id from web_tp_login where emp_id='" + empid + "')) for xml path('')),1,1,'') as allmodule from  Register_Form) AS B ";
        DataTable dt = new DataTable();
        dt = cls1.fillDataTable(query);
        List<employee> details = new List<employee>();
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                employee sn = new employee();
                sn.deptid = dr["emp_dept_id"].ToString();
                sn.desigid = dr["emp_des_id"].ToString();
                sn.salary = dr["actual_basic_salary"].ToString();
                sn.mothername = dr["emp_mother_name"].ToString();
                sn.lname = dr["emp_lname"].ToString();
                sn.mname = dr["emp_mname"].ToString();
                sn.fname = dr["emp_fname"].ToString();
                sn.mobileno = dr["emp_mobile1"].ToString();
                sn.dob = dr["emp_dob"].ToString();
                sn.address = dr["emp_address_curr"].ToString();
                sn.bloodgroup = dr["emp_blood_group"].ToString();
                sn.dojoin = dr["emp_doj"].ToString();
                sn.emailid = dr["emp_email"].ToString();
                sn.gender = dr["emp_gender"].ToString();
                sn.mobile2 = dr["emp_mobile2"].ToString();
                sn.role = dr["role_id"].ToString();
                sn.grouplist = dr["group_ids"].ToString();
                sn.logincat = dr["col1"].ToString();
                sn.form_name = dr["allforms"].ToString().Trim();
                sn.module = dr["allmodule"].ToString().Trim();
                sn.pincode = dr["emp_pincode_curr"].ToString().Trim();
                sn.state = dr["emp_state_curr"].ToString().Trim();
                sn.state = dr["emp_state_curr"].ToString().Trim();
                sn.qualification = dr["emp_qual"].ToString().Trim();
                sn.medium = dr["col3"].ToString().Trim();
                sn.medium_id = dr["col4"].ToString().Trim();
                sn.image = dr["emp_photo"].ToString();
                sn.sign = dr["emp_sign"].ToString();
                sn.defaultforms= dr["defaultforms"].ToString();
                sn.defaultmodules= dr["defaultmodule"].ToString();

                sn.msg = "";
                details.Add(sn);
            }
        }
        else
        {
            employee sn = new employee();
            sn.msg = "Not Found";
            details.Add(sn);
        }
        return details.ToArray();
    }

    public RoleSave[] saveRole(string rolename, string formname)
    {

        DateTime date = DateTime.Now;

        List<RoleSave> details = new List<RoleSave>();
        RoleSave retval = new RoleSave();
        if (qryCls.checkDuplicatesRolename(rolename) == false)
        {
            if (qryCls.InserRole(rolename, formname) == true)
            {
                retval.msg = "Saved";
                details.Add(retval);
            }
            else
            {
                retval.msg = "No Data Saved";
                details.Add(retval);
            }
        }
        else
        {
            retval.msg = "Exist";
            details.Add(retval);
        }

        return details.ToArray();
    }

    public Employee[] saveForm(string formname, string model)
    {

        DateTime date = DateTime.Now;

        List<Employee> details = new List<Employee>();
        Employee retval = new Employee();
        if (qryCls.checkDuplicatesformname(formname) == false)
        {
            if (qryCls.Inserform(formname, model) == true)
            {
                retval.msg = "Saved";
                details.Add(retval);
            }
            else
            {
                retval.msg = "No Data Saved";
                details.Add(retval);
            }
        }
        else
        {
            retval.msg = "Exist";
            details.Add(retval);
        }

        return details.ToArray();
    }

    public Employee[] deleteForm(string id)
    {
        string qry = "";

        List<Employee> details = new List<Employee>();
        Employee retval = new Employee();

        qry = "delete FROM Register_Form where Sr_no='" + id + "'";
        bool str_flag = cls1.DMLqueries(qry);

        if (str_flag == true)
        {
            retval.msg = "deleted";
            details.Add(retval);
        }
        else
        {
            retval.msg = "Cannot";
            details.Add(retval);
        }
        return details.ToArray();
    }

    public RoleSave[] fillrolegrid()
    {
        List<RoleSave> details = new List<RoleSave>();

        string str = "";
        str = "select * from dbo.Register_Form where [Del Flag]=0";

        DataTable dt = new DataTable();
        dt = cls1.fillDataTable(str);

        foreach (DataRow dr in dt.Rows)
        {
            RoleSave retval = new RoleSave();
            retval.formname = dr["Form_Name"].ToString();
            //retval.deptname = dr["Department_name"].ToString();
            //retval.deptid = dr["Dept_id"].ToString();
            details.Add(retval);
        }

        return details.ToArray();
    }

    public bool saveData(string qry)
    {
        return cls1.DMLqueries(qry);
    }
    public List<ListItem> fillrole()
    {
        string qry = "select distinct role_id,role_name from web_tp_roletype where del_flag=0";

        string constr = ConfigurationManager.ConnectionStrings["connect"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(qry))
            {
                List<ListItem> coure = new List<ListItem>();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        coure.Add(new ListItem

                        {
                            Value = sdr["role_id"].ToString(),
                            Text = sdr["role_name"].ToString()
                        });
                    }
                }
                con.Close();
                return coure;
            }
        }
    }
    public Employee[] fillformgrid()
    {
        List<Employee> confirm = new List<Employee>();
        string qry = "";

        qry = "select * from dbo.Register_Form where [Del Flag]=0";

        string constr = ConfigurationManager.ConnectionStrings["connect"].ConnectionString;
        SqlDataAdapter da1 = new SqlDataAdapter(qry, constr);

        DataTable dt = new DataTable();
        da1.Fill(dt);
        //   DataTable dt = cls1.fillDataTable(str);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            Employee sdb = new Employee();
            sdb.form_id = dt.Rows[i]["Sr_no"].ToString();
            sdb.form_name = dt.Rows[i]["Form_Name"].ToString();

            confirm.Add(sdb);
        }
        return confirm.ToArray();
    }

    //----------------------------------end employee master

    //-------------------fee report student
    public List<ListItem> fillcourse(string courseID)
    {

        //string finalGrp = qryCls.splitGrp(Convert.ToString(HttpContext.Current.Session["group_ids"]));
        String qry = "select subcourse_id,subcourse_name from m_crs_subcourse_tbl where course_id='" + courseID + "'";

        string constr = ConfigurationManager.ConnectionStrings["connect"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(qry))
            {
                List<ListItem> course = new List<ListItem>();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        course.Add(new ListItem
                        {
                            Value = sdr["subcourse_id"].ToString(),
                            Text = sdr["subcourse_name"].ToString()
                        });
                    }
                }
                con.Close();
                return course;
            }
        }
    }

    public List<ListItem> fillsubgroup(string subcourseID)
    {

        //string finalGrp = qryCls.splitGrp(Convert.ToString(HttpContext.Current.Session["group_ids"]));
        String qry = "select group_id,group_title from m_crs_subjectgroup_tbl where subcourse_id='" + subcourseID + "'";

        string constr = ConfigurationManager.ConnectionStrings["connect"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(qry))
            {
                List<ListItem> course = new List<ListItem>();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        course.Add(new ListItem
                        {
                            Value = sdr["group_id"].ToString(),
                            Text = sdr["group_title"].ToString()
                        });
                    }
                }
                con.Close();
                return course;
            }
        }
    }

    public List<ListItem> cd_dept(string connect)
    {
        string qry = "select * from m_crs_course_tbl where del_flag=0";

        string cs = "";
        if (connect.StartsWith("Viva Engg"))
        {
            cs = ConfigurationManager.ConnectionStrings["connect_engg"].ConnectionString;
        }
        else if (connect.StartsWith("MCA")) { cs = ConfigurationManager.ConnectionStrings["connect_mca"].ConnectionString; }
        else if (connect.StartsWith("pharmacy")) { cs = ConfigurationManager.ConnectionStrings["connect_pha"].ConnectionString; }
        else if (connect.StartsWith("Viva IMR")) { cs = ConfigurationManager.ConnectionStrings["connect_imr"].ConnectionString; }


        else if (connect.StartsWith("Viva IMS")) { cs = ConfigurationManager.ConnectionStrings["connect_ims"].ConnectionString; }

        using (SqlConnection con = new SqlConnection(cs))
        {
            using (SqlCommand cmd = new SqlCommand(qry))
            {
                List<ListItem> listData = new List<ListItem>();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(sdr[0])))
                        {
                            listData.Add(new ListItem
                            {
                                Value = sdr["course_id"].ToString(),
                                Text = sdr["course_name"].ToString()
                            });
                        }
                    }
                }
                con.Close();
                return listData;
            }
        }
    }

    public List<ListItem> fillDropdown(string connect)
    {
        string qry = "select * from ll_language_master order by Language asc ";
        //var txtaddlanguage = "";

        //string qry = "if not exists (select * from ll_language_master where language='" + txtaddlanguage + "')begin insert into ll_language_master values('" + txtaddlanguage + "')end";

        string cs = "";// ConfigurationManager.ConnectionStrings["connect"].ConnectionString;

        if (connect.StartsWith("Viva Engg"))
        {
            cs = ConfigurationManager.ConnectionStrings["connect_engg"].ConnectionString;
        }
        else if (connect.StartsWith("MCA")) { cs = ConfigurationManager.ConnectionStrings["connect_mca"].ConnectionString; }
        else if (connect.StartsWith("pharmacy")) { cs = ConfigurationManager.ConnectionStrings["connect_pha"].ConnectionString; }
        else if (connect.StartsWith("Viva IMR")) { cs = ConfigurationManager.ConnectionStrings["connect_imr"].ConnectionString; }
        using (SqlConnection con = new SqlConnection(cs))
        {
            using (SqlCommand cmd = new SqlCommand(qry))
            {
                List<ListItem> listData = new List<ListItem>();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        listData.Add(new ListItem
                        {
                            Value = sdr["id"].ToString(),
                            Text = sdr["language"].ToString()
                        });
                    }
                }
                con.Close();
                return listData;
            }
        }
    }

    public List<ListItem> LoadPrefix(string type, string connect)
    {
        String qry = "";
        //string finalGrp = qryCls.splitGrp(Convert.ToString(HttpContext.Current.Session["group_ids"]));
        if (type == "map")
        {
            qry = "select distinct  substring(SUBSTRING( ACCESSION_NO ,1,case when PATINDEX('%[0-9]%', ACCESSION_NO )='0' then '1' else PATINDEX('%[0-9]%', ACCESSION_NO ) end ), 1,(len(SUBSTRING( ACCESSION_NO ,1,case when PATINDEX('%[0-9]%', ACCESSION_NO )='0' then '1' else PATINDEX('%[0-9]%', ACCESSION_NO ) end )) - 1)) prefix   from lib_MAP_MASTER where ( ACCESSION_NO is not null or  ACCESSION_NO <>'' ) and  substring(SUBSTRING( ACCESSION_NO ,1,case when PATINDEX('%[0-9]%', ACCESSION_NO )='0' then '1' else PATINDEX('%[0-9]%',ACCESSION_NO ) end ), 1,(len(SUBSTRING( ACCESSION_NO ,1,case when PATINDEX('%[0-9]%', ACCESSION_NO )='0' then '1' else PATINDEX('%[0-9]%',ACCESSION_NO ) end )) - 1)) <>''  order by 1";
        }
        else if (type == "cd")
        {
            qry = "select distinct  substring(SUBSTRING( ACCESSION_NO ,1,case when PATINDEX('%[0-9]%', ACCESSION_NO )='0' then '1' else PATINDEX('%[0-9]%', ACCESSION_NO ) end ), 1,(len(SUBSTRING( ACCESSION_NO ,1,case when PATINDEX('%[0-9]%', ACCESSION_NO )='0' then '1' else PATINDEX('%[0-9]%', ACCESSION_NO ) end )) - 1)) prefix  from lib_CD_MASTER where ( ACCESSION_NO is not null or  ACCESSION_NO <>'' ) and  substring(SUBSTRING( ACCESSION_NO ,1,case when PATINDEX('%[0-9]%', ACCESSION_NO )='0' then '1' else PATINDEX('%[0-9]%',ACCESSION_NO ) end ), 1,(len(SUBSTRING( ACCESSION_NO ,1,case when PATINDEX('%[0-9]%', ACCESSION_NO )='0' then '1' else PATINDEX('%[0-9]%',ACCESSION_NO ) end )) - 1)) <>''  order by 1";
        }
        else
        {
            qry = "select distinct   substring(SUBSTRING( ACCESSION_NO ,1,case when PATINDEX('%[0-9]%', ACCESSION_NO )='0' then '1' else PATINDEX('%[0-9]%', ACCESSION_NO ) end ), 1,(len(SUBSTRING( ACCESSION_NO ,1,case when PATINDEX('%[0-9]%', ACCESSION_NO )='0' then '1' else PATINDEX('%[0-9]%', ACCESSION_NO ) end )) - 1)) prefix  from lib_book_MASTER where ( ACCESSION_NO is not null or  ACCESSION_NO <>'' ) and substring(SUBSTRING( ACCESSION_NO ,1,case when PATINDEX('%[0-9]%', ACCESSION_NO )='0' then '1' else PATINDEX('%[0-9]%',ACCESSION_NO ) end ), 1,(len(SUBSTRING( ACCESSION_NO ,1,case when PATINDEX('%[0-9]%', ACCESSION_NO )='0' then '1' else PATINDEX('%[0-9]%',ACCESSION_NO ) end )) - 1)) <>''  order by 1";

        }
        string cs = "";// ConfigurationManager.ConnectionStrings["connect"].ConnectionString;

        if (connect.StartsWith("Viva Engg"))
        {
            cs = ConfigurationManager.ConnectionStrings["connect_engg"].ConnectionString;
        }
        else if (connect.StartsWith("MCA")) { cs = ConfigurationManager.ConnectionStrings["connect_mca"].ConnectionString; }
        else if (connect.StartsWith("pharmacy")) { cs = ConfigurationManager.ConnectionStrings["connect_pha"].ConnectionString; }
        else if (connect.StartsWith("Viva IMR")) { cs = ConfigurationManager.ConnectionStrings["connect_imr"].ConnectionString; }
        using (SqlConnection con = new SqlConnection(cs))
        {
            using (SqlCommand cmd = new SqlCommand(qry))
            {
                List<ListItem> course = new List<ListItem>();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        course.Add(new ListItem
                        {
                            Value = sdr["prefix"].ToString(),
                            Text = sdr["prefix"].ToString()
                        });
                    }
                }
                con.Close();
                return course;
            }
        }
    }

    //----------------Department and Designation---------------
    public deptdes[] saverecord(string deptname, string prefix, string type, string empid)
    {
        string qry = "";
        DateTime date = DateTime.Now;

        List<deptdes> details = new List<deptdes>();
        deptdes retval = new deptdes();

        if (type == "department")
        {
            qry = "exec MDepartment 'D','" + deptname.ToUpper().Replace("'","''") + "','','0','" + prefix.ToUpper() + "','Insert'";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            dt = cls1.fillDataTable(qry);
            if (dt.Rows[0][0].ToString() == "saved")
            {
                retval.msg = "Saved";
                details.Add(retval);
            }
            else if (dt.Rows[0][0].ToString() == "multi")
            {
                retval.msg = "More";
                details.Add(retval);
            }
            else if (dt.Rows[0][0].ToString() == "exist")
            {
                retval.msg = "Exist";
                details.Add(retval);
            }
            else if(dt.Rows[0][0].ToString() == "unsaved")
            {
                retval.msg = "Unsaved";
                details.Add(retval);
            }
            else
            {
                retval.msg = "Error";
                details.Add(retval);
            }
        }
        else
        {
            qry = "exec MDesignation 'A','" + deptname.ToUpper() + "','Admin','0','Insert'";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            dt = cls1.fillDataTable(qry);

            if (dt.Rows[0][0].ToString() == "saved")
            {
                retval.msg = "Saved";
                details.Add(retval);
            }
            else if (dt.Rows[0][0].ToString() == "multi")
            {
                retval.msg = "More";
                details.Add(retval);
            }
            else if (dt.Rows[0][0].ToString() == "exist")
            {
                retval.msg = "Exist";
                details.Add(retval);
            }
            else if (dt.Rows[0][0].ToString() == "unsaved")
            {
                retval.msg = "Unsaved";
                details.Add(retval);
            }
            else
            {
                retval.msg = "Error";
                details.Add(retval);
            }
        }

        return details.ToArray();
    }

    public deptdes[] filltblgrid()
    {
        List<deptdes> details = new List<deptdes>();

        string str = "";
        str = "select Dept_id,Department_name,PREFIX from m_department where del_flag=0 order by dept_id";

        DataTable dt = new DataTable();
        dt = cls1.fillDataTable(str);

        foreach (DataRow dr in dt.Rows)
        {
            deptdes retval = new deptdes();
            retval.prefix = dr["PREFIX"].ToString();
            retval.deptname = dr["Department_name"].ToString();
            retval.deptid = dr["Dept_id"].ToString();
            details.Add(retval);
        }

        return details.ToArray();
    }

    public deptdes[] fillgesigrid()
    {
        List<deptdes> details = new List<deptdes>();

        string str = "";
        str = "select Designation_ID,Designation_Title from m_designation where del_flag=0 order by Designation_ID";

        DataTable dt = new DataTable();
        dt = cls1.fillDataTable(str);

        foreach (DataRow dr in dt.Rows)
        {
            deptdes retval = new deptdes();
            retval.desname = dr["Designation_Title"].ToString();
            retval.desid = dr["Designation_ID"].ToString();
            details.Add(retval);
        }

        return details.ToArray();
    }

    public deptdes[] deletedept(string id, string type, string name, string prefix)
    {
        string qry = "";
        DateTime date = DateTime.Now;

        List<deptdes> details = new List<deptdes>();
        deptdes retval = new deptdes();

        if (type == "department")
        {
            qry = "exec MDepartment '" + id + "','" + name + "','" + date + "','0','" + prefix + "','Delete'";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            dt = cls1.fillDataTable(qry);
            if (dt.Rows[0][0].ToString() == "deleted")
            {
                retval.msg = "deleted";
                details.Add(retval);
            }
            else if (dt.Rows[0][0].ToString() == "assigned")
            {
                retval.msg = "assigned";
                details.Add(retval);
            }
            else
            {
                retval.msg = "Error";
                details.Add(retval);
            }
        }
        else
        {
            qry = "exec MDesignation '" + id + "','" + name + "','Admin','0','Delete'";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            dt = cls1.fillDataTable(qry);

            if (dt.Rows[0][0].ToString() == "deleted")
            {
                retval.msg = "deleted";
                details.Add(retval);
            }
            else if (dt.Rows[0][0].ToString() == "assigned")
            {
                retval.msg = "assigned";
                details.Add(retval);
            }
            else
            {
                retval.msg = "Error";
                details.Add(retval);
            }
        }

        return details.ToArray();
    }

    public deptdes[] updaterecord(string deptname, string prefix, string type, string empid, string deptid)
    {
        string qry = "";
        DateTime date = DateTime.Now;

        List<deptdes> details = new List<deptdes>();
        deptdes retval = new deptdes();

        if (type == "department")
        {
            qry = "exec MDepartment '" + deptid + "','" + deptname + "','" + date + "','0','" + prefix + "','update'";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            dt = cls1.fillDataTable(qry);
            if (dt.Rows[0][0].ToString() == "updated")
            {
                retval.msg = "Update";
                details.Add(retval);
            }
            else if (dt.Rows[0][0].ToString() == "multi")
            {
                retval.msg = "More";
                details.Add(retval);
            }
            else if (dt.Rows[0][0].ToString() == "exist")
            {
                retval.msg = "Exist";
                details.Add(retval);
            }
            else if (dt.Rows[0][0].ToString() == "unsaved")
            {
                retval.msg = "Unsaved";
                details.Add(retval);
            }
            else
            {
                retval.msg = "Error";
                details.Add(retval);
            }
        }
        else
        {
            qry = "exec MDesignation '" + deptid + "','" + deptname + "','Admin','0','update'";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            dt = cls1.fillDataTable(qry);

            if (dt.Rows[0][0].ToString() == "updated")
            {
                retval.msg = "Update";
                details.Add(retval);
            }
            else if (dt.Rows[0][0].ToString() == "multi")
            {
                retval.msg = "More";
                details.Add(retval);
            }
            else if (dt.Rows[0][0].ToString() == "exist")
            {
                retval.msg = "Exist";
                details.Add(retval);
            }
            else if (dt.Rows[0][0].ToString() == "unsaved")
            {
                retval.msg = "Unsaved";
                details.Add(retval);
            }
            else
            {
                retval.msg = "Error";
                details.Add(retval);
            }
        }

        return details.ToArray();
    }

    //-----------------------------

    //---------------overall report

    public List<ListItem> fillselect()
    {
        String qry = "select 'stud_Category' as 'Fieldid','CATEGORY' as 'Text' union all select 'stud_Caste' as 'Fieldid','CASTE' as 'Text' union all select 'stud_SubCaste' as 'Fieldid','SUB-CASTE' as 'Text' union all"
        + " select 'stud_Father_FName' as 'Fieldid','FATHER NAME' as 'Text' union all select 'stud_Father_TelNo' as 'Fieldid','FATHER PHONE NO' as 'Text' union all select 'stud_Mother_FName' as 'Fieldid','MOTHER NAME' as 'Text' union all "
        + " select 'stud_Mother_TelNo' as 'Fieldid','MOTHER PHONE NO' as 'Text' union all select 'stud_NativePhone' as 'Fieldid','NATIVE PHONE NO' as 'Text' union all select 'stud_Gaurd_FName' as 'Fieldid','GAURDIAN NAME' as 'Text' union all "
        + " select 'stud_Gaurd_TelNo' as 'Fieldid','GAURDIAN PHONE NO' as 'Text' union all select 'stud_YearlyIncome' as 'Fieldid','YEARLY INCOME' as 'Text' union all select 'stud_Gender' as 'Fieldid','Gender' as 'Text' union all" //select 'stud_aadhar' as 'Fieldid','AADHAR NO' as 'Text' union all 
        + " select 'stud_dob' as 'Fieldid','DOB' as 'Text' union all select 'stud_PermanentPhone' as 'Fieldid','STUDENT PHONE NO' as 'Text' union all select 'stud_PermanentAdd' as 'Fieldid','STUDENT ADDRESS' as 'Text' " //select 'stud_voterid' as 'Fieldid','VOTER ID NO' as 'Text' union all 
        + "union all select 'ID_No' as 'Fieldid','PRN NO' as 'Text' union all select 'stud_Earning' as 'Fieldid','EARNING' as 'Text' union all select 'stud_NonEarning' as 'Fieldid','NON-EARNING' as 'Text'";

        string constr = ConfigurationManager.ConnectionStrings["connect"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(qry))
            {
                List<ListItem> subgrp = new List<ListItem>();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        subgrp.Add(new ListItem
                        {
                            Value = sdr["Fieldid"].ToString(),
                            Text = sdr["Text"].ToString()
                        });
                    }
                }
                con.Close();
                return subgrp;
            }
        }
    }

    public string graph(string ayid)
    {

        return ayid;
    }

    //-------------------------------Master Page----------------------------------------------------
    public masterpage[] moduleAccess(string emp_id)
    {
        string qry = "";
        DateTime date = DateTime.Now;

        List<masterpage> details = new List<masterpage>();
        string arrlst = "";
        string str = "select distinct stuff((select distinct ','+ b.Module_name from Register_Form as a,Module_form as b where a.Portal=b.sr_no and  a.Sr_no in (select x.Item as col2 from web_tp_login t cross apply (select Item from dbo.SplitString(t.col2,',') ) x where t.emp_id='" + emp_id + "'   union all select y.Item as form_name from web_tp_roletype r cross apply (select Item from dbo.SplitString(r.form_name,',') ) y where r.role_id in (select role_id from web_tp_login where emp_id ='" + emp_id + "' )) for xml path('')),1,1,'') as module from  Register_Form ";
        DataTable dtfill = cls1.fillDataTable(str);
        if (dtfill.Rows.Count > 0)
        {
            string[] arr = dtfill.Rows[0]["module"].ToString().Split(',');

            for (int a = 0; a < arr.Length; a++)
            {
                masterpage retval = new masterpage();
                retval.module_name = arr[a].ToString();


                details.Add(retval);
            }

        }

        return details.ToArray();
    }

    public masterpage[] formAccess(string emp_id)
    {
        string qry = "";
        DateTime date = DateTime.Now;

        List<masterpage> details = new List<masterpage>();
        string str = " select Form_Name as form from Register_Form where Sr_no in (select x.Item as col2 from web_tp_login t cross apply (select Item from dbo.SplitString(t.col2,',') ) x where t.emp_id='" + emp_id + "'  union all select y.Item as form_name from web_tp_roletype r cross apply (select Item from dbo.SplitString(r.form_name,',') ) y where r.role_id in (select role_id from web_tp_login where emp_id ='" + emp_id + "' )) ";
        //string str = "select Form_Name as form from Register_Form where Sr_no in (select x.Item as col2 from web_tp_login t cross apply (select Item from dbo.SplitString(t.col2,',') ) x where t.emp_id='" + emp_id + "')";
        DataTable dtfill = cls1.fillDataTable(str);
        if (dtfill.Rows.Count > 0)
        {
            for (int a = 0; a < dtfill.Rows.Count; a++)
            {
                masterpage retval = new masterpage();
                retval.form_name = dtfill.Rows[a]["form"].ToString();
                details.Add(retval);
            }

        }

        return details.ToArray();
    }

    public void uploadphoto(string path, string id)
    {
        string qry = "update mst_employee_personal set emp_photo='" + path.ToString() + "' where emp_id='" + id + "'";
        cls1.DMLqueries(qry);
    }
    public void uploadsign(string path, string id)
    {
        string qry = "update mst_employee_personal set emp_sign='" + path.ToString() + "' where emp_id='" + id + "'";
        cls1.DMLqueries(qry);
    }
}