<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmModifyData.aspx.cs"  EnableEventValidation="true" Inherits="frmModifyData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="assets/libs/jquery/dist/jquery.min.js"></script>
    <link href="notify-master/css/notify.css" rel="stylesheet" />
    <script src="notify-master/js/notify.js"></script>
    <style>
        table {
            border-collapse: collapse;
            border: 0;
        }

        .modal-lg {
            max-width: 80% !important;
        }

        td {
            text-align: center;
            border: 0;
            padding: 8px;
            vertical-align: central;
        }

        th {
            text-align: center;
            color: #fff;
        }



        tr:nth-child(even) {
            background-color: #f2f2f2;
        }
    </style>

 

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Always">
        <ContentTemplate>
    <div>

        <div class="container-fluid">
            <div class="col-md-12">
                <div class="card-header" style="background: linear-gradient(to right,#0078bc 1%,#00beda 100%) !important;">
                    <span style="color: white; font-size: 20px;">Modify Master 
                    </span>
                </div>
                <div class="card card-body">

                    <br />
                   <%-- <asp:UpdatePanel runat="server" ID="up1" UpdateMode="Conditional">
                        <ContentTemplate>--%>
                            <div class="row">
                                <%--<div class="col-md-1"></div>--%>
                                <div class="col-md-3">
                                    Medium:<asp:DropDownList ID="ddlmedium" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlmedium_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    Class:<asp:DropDownList ID="ddlclass" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlclass_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    Division:<asp:DropDownList ID="ddldiv" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddldiv_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>

                            </div>
                       <%-- </ContentTemplate>
                    </asp:UpdatePanel>--%>

                    <br />
                    <br />
                   <%-- <asp:UpdatePanel runat="server" ID="up2" UpdateMode="Always">
                        <ContentTemplate>--%>
                            <div id="griddiv" runat="server" style="overflow-x: scroll; overflow-y: scroll; height: 500px">
                                <div class="row">
                                    <div class="col-md-12">


                                        <asp:GridView ID="grid" runat="server" CssClass="table table-bordered table-container" ShowHeader = "true" AutoGenerateColumns="false" Style="overflow: scroll; Height: 100%; background-color: white; Width: 100%" OnRowEditing="grid_RowEditing">
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btn_Edit" runat="server" CommandName="Edit"  Text="<i class='fas fa-edit' aria-hidden='true' ></i>" />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <%--<asp:LinkButton ID="LBUpdate"  CommandName="Update" runat="server" OnClick="LBUpdate_Click" ><i class='fa fa-save' style='font-size:24px'></i></asp:LinkButton> &nbsp;&nbsp;--%>
                                                        <asp:LinkButton ID="LBUpdate" CommandName="Update" runat="server" OnClick="LBUpdate_Click"><i class='fa fa-save' style='font-size:24px' ></i></asp:LinkButton>
                                                        &nbsp;&nbsp;
                                        <asp:LinkButton ID="LBCancel" runat="server" OnClick="LBCancel_Click" CommandName="Cancel"><i class='fa fa-times-circle' style='font-size:24px'></i></asp:LinkButton>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="STUDENT ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblstud_id" CssClass="label-default" style="width:100px" runat="server" Text='<%# Eval("Student_id")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="STANDARD NAME">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblstd_name" CssClass="label-default" runat="server" style="width:100px" Text='<%# Eval("std_name")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="STUD_F_NAME">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblstud_f_name" CssClass="label-default" runat="server" style="width:100px" Text='<%# Eval("stud_F_name")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtstud_f_name" CssClass="form-control" autocomplete="off" runat="server" style="width:100px" onkeypress="return nametxt(event)" Text='<%# Eval("stud_F_name")%>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="STUDENT MIDDLE NAME">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblstud_m_name" CssClass="label-default" runat="server" style="width:100px" Text='<%# Eval("stud_m_name")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtstud_m_name" CssClass="form-control" autocomplete="off" runat="server" style="width:100px" onkeypress="return nametxt(event)" Text='<%# Eval("stud_m_name")%>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="STUDENT LAST NAME">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblstud_l_name" CssClass="label-default" runat="server" style="width:100px" Text='<%# Eval("stud_L_name")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtstud_l_name" CssClass="form-control" autocomplete="off" runat="server" style="width:100px" onkeypress="return surname(event)" Text='<%# Eval("stud_L_name")%>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="STUDENT MOTHER NAME">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblstud_mo_name" CssClass="label-default" runat="server" style="width:100px" Text='<%# Eval("stud_mo_name")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtstud_mo_name" CssClass="form-control" autocomplete="off" runat="server" style="width:100px" onkeypress="return nametxt(event)" Text='<%# Eval("stud_mo_name")%>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ADDRESS">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbladdress" CssClass="label-default" runat="server" style="width:100px" Text='<%# Eval("address")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtaddress" CssClass="form-control" autocomplete="off" runat="server" style="width:100px" onkeypress="return address(event)" Text='<%# Eval("address")%>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="GENDER">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgender" CssClass="label-default" runat="server" style="width:100px" Text='<%# Eval("gender")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <%-- <asp:TextBox ID="txtgender" CssClass="form-control" autocomplete="off" runat="server" Text='<%# Eval("gender")%>'></asp:TextBox>--%>
                                                        <asp:DropDownList ID="ddlgender" CssClass="form-control" style="width:100px" runat="server">
                                                            <asp:ListItem>Male</asp:ListItem>
                                                            <asp:ListItem>Female</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="STUDENT CATEGORY">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblcategory" CssClass="label-default" runat="server" style="width:100px" Text='<%# Eval("category")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="ddlcategory" CssClass="form-control" AutoPostBack="true" style="width:100px" OnSelectedIndexChanged="ddlcategory_SelectedIndexChanged" runat="server"></asp:DropDownList>
                                                        <%-- <asp:TextBox ID="txtcategory" CssClass="form-control" autocomplete="off" runat="server" Text='<%# Eval("category")%>'></asp:TextBox>--%>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="STUDENT CASTE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblcaste" CssClass="label-default" runat="server" style="width:100px" Text='<%# Eval("caste")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="ddlcaste" CssClass="form-control" AutoPostBack="true" style="width:100px" OnSelectedIndexChanged="ddlcaste_SelectedIndexChanged" runat="server"></asp:DropDownList>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="STUDENT SUB-CASTE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsub_caste" CssClass="label-default" runat="server" style="width:100px" Text='<%# Eval("sub_caste")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="ddlsubcaste" CssClass="form-control" style="width:100px" runat="server"></asp:DropDownList>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PHONE NO. 1">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblphone_no1" CssClass="label-default" runat="server" style="width:100px" Text='<%# Eval("phone_no1")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtphone_no1" CssClass="form-control" autocomplete="off" style="width:100px" onkeypress="CheckNumeric(event)" runat="server" MaxLength="10" Text='<%# Eval("phone_no1")%>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PHONE NO. 2">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblphone_no2" CssClass="label-default" runat="server" style="width:100px" Text='<%# Eval("phone_no2")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtphone_no2" CssClass="form-control" autocomplete="off" style="width:100px" runat="server" onkeypress="CheckNumeric(event)" MaxLength="10" Text='<%# Eval("phone_no2")%>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CO-PHONE NO.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblco_mobile_no" CssClass="label-default" runat="server" style="width:100px" Text='<%# Eval("co_mobile_no")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtco_mobile_no" CssClass="form-control" autocomplete="off" style="width:100px" runat="server" onkeypress="CheckNumeric(event)" MaxLength="10" Text='<%# Eval("co_mobile_no")%>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <%-- <asp:TemplateField HeaderText="DOB" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldob" CssClass="label-default" runat="server" Text='<%# Eval("dob")%>'></asp:Label>
                                                </ItemTemplate>
                                               <EditItemTemplate>
                                                    <asp:TextBox ID="txtdob" CssClass="form-control" autocomplete="off" runat="server" Text='<%# Eval("dob")%>'></asp:TextBox>
                                                </EditItemTemplate>
                                 </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="STUDENT BIRTH PLACE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblbirth_place" CssClass="label-default" runat="server" style="width:100px" Text='<%# Eval("birth_place")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtbirth_place" CssClass="form-control" autocomplete="off" style="width:100px" runat="server" onkeypress=" return character(event)" Text='<%# Eval("birth_place")%>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="STUDENT MOTHER TONGUE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblmother_tongue" CssClass="label-default" runat="server" style="width:100px" Text='<%# Eval("mother_tongue")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtmother_tongue" CssClass="form-control" autocomplete="off" style="width:100px" runat="server" onkeypress="return character(event)" Text='<%# Eval("mother_tongue")%>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="STUDENT NATIONALITY">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblnationality" CssClass="label-default" runat="server" style="width:100px" Text='<%# Eval("nationality")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtnationality" CssClass="form-control" autocomplete="off" style="width:100px" runat="server" onkeypress=" return nametxt(event)" Text='<%# Eval("nationality")%>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="STUDENT AADHAR NO.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbladhar_no" CssClass="label-default" runat="server" style="width:100px" Text='<%# Eval("aadhar_no")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtadhar_no" CssClass="form-control" autocomplete="off" runat="server"  style="width:100px" onkeypress="CheckNumeric(event)" MaxLength="12" Text='<%# Eval("aadhar_no")%>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PINCODE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblpincode" CssClass="label-default" runat="server" style="width:100px" Text='<%# Eval("pincode")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtpincode" CssClass="form-control" autocomplete="off" runat="server" style="width:100px" onkeypress="CheckNumeric(event)" MaxLength="6" Text='<%# Eval("pincode")%>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DISTRICT">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldist" CssClass="label-default" runat="server" style="width:100px" Text='<%# Eval("dist")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtdist" CssClass="form-control" autocomplete="off" runat="server" style="width:100px" onkeypress="return nametxt(event)" Text='<%# Eval("dist")%>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="TALUKA">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTaluka" CssClass="label-default" runat="server" style="width:100px" Text='<%# Eval("Taluka")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtTaluka" CssClass="form-control" autocomplete="off" runat="server" style="width:100px" onkeypress="return nametxt(event)" Text='<%# Eval("Taluka")%>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="STATE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblstate" CssClass="label-default" runat="server" style="width:100px" Text='<%# Eval("state")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtstate" CssClass="form-control" autocomplete="off" runat="server" style="width:100px" onkeypress="return  nametxt(event)" Text='<%# Eval("state")%>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="VEHICLE NO">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblvehicle_no" CssClass="label-default" runat="server" style="width:100px" Text='<%# Eval("vehicle_no")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtvehicle_no" CssClass="form-control" autocomplete="off" runat="server" style="width:100px" onkeypress="return  vehicle(event)" MaxLength="12" Text='<%# Eval("vehicle_no")%>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="VEHICLE TYPE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblvehicle_type" CssClass="label-default" runat="server" style="width:100px" Text='<%# Eval("vehicle_type")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtvehicle_type" CssClass="form-control" autocomplete="off" runat="server" style="width:100px" onkeypress="return nametxt(event)" Text='<%# Eval("vehicle_type")%>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DRIVER NO">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldriver_no" CssClass="label-default" runat="server" style="width:100px" Text='<%# Eval("driver_no")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtdriver_no" CssClass="form-control" autocomplete="off" runat="server" style="width:100px" onkeypress="CheckNumeric(event)" MaxLength="10" Text='<%# Eval("driver_no")%>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SARAL ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsaral_id" CssClass="label-default" runat="server" Text='<%# Eval("saral_id")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtsaral_id" CssClass="form-control" autocomplete="off" runat="server" style="width:100px" onkeypress="CheckNumeric(event)" MaxLength="20" Text='<%# Eval("saral_id")%>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="BANK A/C NO">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblbank_ac_no" CssClass="label-default" runat="server" style="width:100px" Text='<%# Eval("bank_ac_no")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtbank_ac_no" CssClass="form-control" autocomplete="off" runat="server" style="width:100px" onkeypress="CheckNumeric(event)" MaxLength="15" Text='<%# Eval("bank_ac_no")%>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="BANK NAME">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblbank_name" CssClass="label-default" runat="server" style="width:100px" Text='<%# Eval("bank_name")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtbank_name" CssClass="form-control" autocomplete="off" runat="server" style="width:100px" onkeypress="return bank(event)" Text='<%# Eval("bank_name")%>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="IFSC CODE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIFSC_code" CssClass="label-default" runat="server" style="width:100px" Text='<%# Eval("IFSC_code")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtIFSC_code" CssClass="form-control" autocomplete="off" runat="server" style="width:100px" onkeypress="return  vehicle(event)" MaxLength="11" Text='<%# Eval("IFSC_code")%>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="BRANCH NAME">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBranch_name" CssClass="label-default" runat="server" style="width:100px" Text='<%# Eval("Branch_name")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtBranch_name" CssClass="form-control" autocomplete="off" runat="server" style="width:100px" onkeypress="return branch(event)" Text='<%# Eval("Branch_name")%>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="FLAG" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblflag" CssClass="label-default" runat="server"  Text='<%# Eval("flag")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                            </Columns>
                                        </asp:GridView>





                                    </div>
                                </div>
                            </div>
                        <%--</ContentTemplate>
                    </asp:UpdatePanel>--%>
                    <br />
                    <br />


                    <div class="row">
                        <div class="col-md-3"></div>
                        <div class="col-md-3">
                            <br />
                          
                                   <%-- <input type="button"  runat="server" ID="btnsave" Text="Save"  CssClass="btn btn-success"  Width="100%" onclick="btnsave_Click" />--%>
                                    <asp:Button runat="server" ID="btnsave" Text="Save" OnClick="btnsave_Click" CssClass="btn btn-success" Enabled="false" Width="100%" />
                          
                        </div>
                        <div class="col-md-3">
                            <br />
                            <asp:Button ID="btnclear" runat="server" Text="Clear" CssClass="btn btn-success" Autopostback="true" OnClick="btnclear_Click" Width="100%" />
                        </div>
                        <div class="col-md-3"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    <script lang="javascript" type="text/javascript">
        function CheckNumeric(e) {

            if (window.event) // IE
            {
                if ((e.keyCode < 48 || e.keyCode > 57) & e.keyCode != 8) {
                    event.returnValue = false;
                    return false;

                }
            }
            else { // Fire Fox
                if ((e.which < 48 || e.which > 57) & e.which != 8) {
                    e.preventDefault();
                    return false;

                }
            }
        }

    </script>
    <script>
        function branch(e) {
            isIE = document.all ? 1 : 0
            keyEntry = !isIE ? e.which : event.keyCode;
            if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry == '32') || (keyEntry == '40') || (keyEntry == '41'))
                return true;
            else {
                return false;
            }
        }
    </script>
    <script>
        function bank(e) {
            isIE = document.all ? 1 : 0
            keyEntry = !isIE ? e.which : event.keyCode;
            if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry == '39') || (keyEntry == '32'))
                return true;
            else {
                return false;
            }
        }
    </script>
    <script>
        function address(e) {
            isIE = document.all ? 1 : 0
            keyEntry = !isIE ? e.which : event.keyCode;
            if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '48') && (keyEntry <= '57')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry == '39') || (keyEntry == '32') || (keyEntry == '46') || (keyEntry == '44') || (keyEntry == '47') || (keyEntry == '45'))
                return true;
            else {
                return false;
            }
        }
    </script>
    <script>
        function nametxt(e) {
            isIE = document.all ? 1 : 0
            keyEntry = !isIE ? e.which : event.keyCode;
            if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')))
                return true;
            else {
                return false;
            }
        }
    </script>
    <script>
        function surname(e) {
            isIE = document.all ? 1 : 0
            keyEntry = !isIE ? e.which : event.keyCode;
            if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry == '39') || (keyEntry == '45'))
                return true;
            else {
                return false;
            }
        }
    </script>
    <script>
        function character(e) {
            isIE = document.all ? 1 : 0
            keyEntry = !isIE ? e.which : event.keyCode;
            if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry == '39') || (keyEntry == '32') || (keyEntry == '46') || (keyEntry == '44'))
                return true;
            else {
                return false;
            }
        }
    </script>
    <script>
        function vehicle(e) {
            isIE = document.all ? 1 : 0
            keyEntry = !isIE ? e.which : event.keyCode;
            if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '48') && (keyEntry <= '57')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry == '32'))
                return true;
            else {
                return false;
            }
        }
    </script>



    <script src="jquery/jquery.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>

</asp:Content>

