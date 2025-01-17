<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frm_personal_details.aspx.cs" Inherits="frm_personal_details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/jquery.datetimepicker.css" rel="stylesheet" />
    <script src="jquery/dist/jquery.min.js"></script>

    <script src="notify-master/js/notify.js"></script>
    <link href="notify-master/css/notify.css" rel="stylesheet" />
    <style>
        fieldset.scheduler-border {
            border: 1px groove #ddd !important;
            padding: 0 1.4em 1.4em 1.4em !important;
            margin: 0 0 1.5em 0 !important;
            -webkit-box-shadow: 0px 0px 0px 0px #000;
            box-shadow: 0px 0px 0px 0px #000;
        }

        legend.scheduler-border {
            width: inherit; /* Or auto */
            padding: 0 10px; /* To give a bit of padding on the left and right */
            border-bottom: none;
        }
    </style>
    <style>
        .required {
            color: red;
        }

        #placeholder {
            color: red;
        }

        .img-circle {
            border-radius: 50%;
        }

        .img-circle1 {
            border-radius: 5%;
        }

        div.box {
            border: 1px solid #f4f4f4;
            background-color: #f4f4f4;
        }

        .modal-header-info {
            color: #fff;
            padding: 9px 15px;
            border-bottom: 1px solid #eee;
            background-color: #5bc0de;
            -webkit-border-top-left-radius: 5px;
            -webkit-border-top-right-radius: 5px;
            -moz-border-radius-topleft: 5px;
            -moz-border-radius-topright: 5px;
            border-top-left-radius: 5px;
            border-top-right-radius: 5px;
        }

        .image-preview-input {
            position: relative;
            overflow: hidden;
            margin: 0px;
            color: #333;
            background-color: #fff;
            border-color: #ccc;
        }

            .image-preview-input input[type=file] {
                position: absolute;
                top: 0;
                right: 0;
                margin: 0;
                padding: 0;
                font-size: 20px;
                cursor: pointer;
                opacity: 0;
                filter: alpha(opacity=0);
            }

        .image-preview-input-title {
            margin-left: 2px;
        }

        .vertical-alignment-helper {
            display: table;
            height: 100%;
            width: 100%;
        }

        .vertical-align-center {
            display: table-cell;
            vertical-align: middle;
        }

        .modal-content {
            width: inherit;
            height: inherit;
            margin: 0 auto;
        }

        .modal-header-primary {
            color: #fff;
            padding: 9px 15px;
            border-bottom: 1px solid #eee;
            background-color: #428bca;
            -webkit-border-top-left-radius: 5px;
            -webkit-border-top-right-radius: 5px;
            -moz-border-radius-topleft: 5px;
            -moz-border-radius-topright: 5px;
            border-top-left-radius: 5px;
            border-top-right-radius: 5px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="card">
        <div class="card-header text-white" style="background: linear-gradient(to right, #6f03bf 1%, #363695 100%) !important; border-radius: 7px">
            <div class="row">
                <div class="col-md-9">
                    <h3>My Profile</h3>
                </div>
                <div class="col-md-3" >
                     <asp:Label ID="lblid" runat="server" Font-Size="16px" ForeColor="Yellow" style="float: right"></asp:Label>
                    <asp:Label ID="lblempid" runat="server" Font-Size="16px" Text="Employee Id:" style="float: right"></asp:Label>
                   
                </div>
            </div>
        </div>
        <div class="card-body">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btn_submit" />
                </Triggers>
                <ContentTemplate>
                    <div class="row">
                        <div class="col-md-10">

                            <fieldset class="scheduler-border">
                                <legend class="scheduler-border" style="margin-bottom: 0px; font-weight: bold; font-family: 'Times New Roman'">[Basic Details]</legend>

                                <div class="row">
                                    <div class="col-md-3 col-sm-3 col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="lbl_first_name" runat="server"></asp:Label><b>First Name</b><span class="required">*</span>
                                            <asp:TextBox ID="txt_first_name" runat="server" AutoCompleteType="Disabled" CssClass="form-control" MaxLength="50" placeholder="First Name" Style="text-transform: uppercase"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3 col-sm-3 col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="lbl_middle_name" runat="server"><b>Middle Name</b></asp:Label><span class="required">*</span>
                                            <asp:TextBox ID="txt_middle_name" runat="server" AutoCompleteType="Disabled" CssClass="form-control" MaxLength="50" placeholder="Middle Name" Style="text-transform: uppercase"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3 col-sm-2 col-md-2">
                                        <div class="form-group">
                                            <asp:Label ID="lbl_last_name" runat="server"><b>Last Name</b></asp:Label><span class="required">*</span>
                                            <asp:TextBox ID="txt_last_name" runat="server" AutoCompleteType="Disabled" CssClass="form-control" MaxLength="50" placeholder="Last Name" Style="text-transform: uppercase"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3 col-sm-2 col-md-2">
                                        <asp:Label ID="lbl_mother" runat="server"><b>Mother Name</b></asp:Label><span class="required">*</span>
                                        <asp:TextBox ID="txt_mother" runat="server" CssClass="form-control" AutoCompleteType="Disabled" MaxLength="50" placeholder="Mother Name" Style="text-transform: uppercase"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-3 col-sm-4 col-md-4">
                                        <div class="form-group">
                                            <asp:Label ID="lbl_dob" runat="server"><b>Date Of Birth</b></asp:Label><span class="required">*</span>
                                            <asp:TextBox ID="txt_dob" CssClass="todate form-control" AutoCompleteType="Disabled" MaxLength="10" runat="server" placeholder="Date of Birth"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3 col-sm-4 col-md-4">
                                        <div class="form-group">
                                            <asp:Label ID="lbl_gender" runat="server"><b>Gender</b></asp:Label><span class="required">*</span>
                                            <div class="form-control">
                                                <asp:RadioButton runat="server" ID="rdbMale" GroupName="gender" AutoPostBack="true" Text="Male" />
                                                <asp:RadioButton runat="server" ID="rdbFemale" GroupName="gender" AutoPostBack="true" Text="Female" />
                                                <asp:RadioButton runat="server" ID="rdbtrans" GroupName="gender" AutoPostBack="true" Text="Transgender" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3 col-sm-4 col-md-4">
                                        <div class="form-group">
                                            <asp:Label ID="lbl_marital_status" runat="server"><b>Marital Status</b></asp:Label><span class="required">*</span>
                                            <div class="form-control">
                                                <asp:RadioButton runat="server" ID="rdbMarried" Text="Married" AutoPostBack="true" GroupName="marital" />
                                                <asp:RadioButton runat="server" ID="rdbUnmarried" Text="Unmarried" AutoPostBack="true" GroupName="marital" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>

                        </div>
                        <div class="col-md-2">

                            <div class="row" style="text-align: center;">
                                <div class="col-md-12">
                                    <div class="panel panel-info" style="padding-left: 5px; padding-right: 5px; margin-top: 17px; padding-top: 7px; border: 1px solid; border-top-right-radius: 15px; border-bottom-left-radius: 15px; height: 190px">
                                        <div class="col-md-12" id="imagediv" runat="server">
                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                <ContentTemplate>
                                                    <asp:Image runat="server" draggable="false" alt="Photo" class="img-fluid img-circle" ID="emp_photo" Style="height: 127px; width: 125px; margin-bottom: 10px;" />
                                                    <asp:Label ID="lblphoto" runat="server" Style="display: none"></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <asp:FileUpload ID="get_photo" runat="server" CssClass="form-control" />
                                        </div>
                                        <div class="col-md-12" id="signdiv" runat="server" style="display: none;">
                                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                <ContentTemplate>
                                                    <asp:Image runat="server" draggable="false" alt="Photo" class="img-fluid img-thumbnail" ID="emp_sign" Style="height: 60px; width: 150px; margin-top: 34px;" />
                                                    <asp:Label ID="lblsign" runat="server" Style="display: none"></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <br />
                                            <asp:FileUpload runat="server" ID="get_sign" CssClass="form-control" Style="margin-top: 22px;" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row" style="padding-top: 4px;">
                                <div class="col-md-6"><a id="btntoggle" class="btn btn-info" style="color: white; display: block;" >Signature</a></div>
                                <div class="col-md-6"><a id="btnClearPhotos" class="btn btn-info form-control" onclick="ClearPhotos()" style="color: white; display: block;"><i class="fas fa-eraser"></i>&nbsp<span>Clear</span></a></div>
                            </div>

                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-6">
                            <fieldset class="scheduler-border">
                                <legend class="scheduler-border" style="margin-bottom: 0px; font-weight: bold; font-family: 'Times New Roman'">[Current Address]</legend>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <b>Address</b> <span class="required">*</span>
                                            <textarea runat="server" id="txtadddress1" autocompletetype="Disabled" class="form-control" placeholder="Flat no./bldg"></textarea>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <span style="FONT-SIZE: 10pt; FONT-FAMILY: Verdana"><b>State</b></span><span class="required">*</span>
                                            <asp:DropDownList onblur="OnBlur(this);" ID="ddlState" AutoPostBack="true" onfocus="OnFocus(this);" CssClass="form-control" runat="server" ToolTip="STATE">
                                                <asp:ListItem>--Select--</asp:ListItem>

                                                <asp:ListItem>ANDHRA PRADESH</asp:ListItem>
                                                <asp:ListItem>ARUNACHAL PRADESH</asp:ListItem>
                                                <asp:ListItem>ASSAM</asp:ListItem>
                                                <asp:ListItem>BIHAR</asp:ListItem>
                                                <asp:ListItem>CHHATTISGARH</asp:ListItem>
                                                <asp:ListItem>GOA</asp:ListItem>
                                                <asp:ListItem>GUJARAT</asp:ListItem>
                                                <asp:ListItem>HARYANA</asp:ListItem>
                                                <asp:ListItem>HIMACHAL PRADESH</asp:ListItem>
                                                <asp:ListItem>JAMMU AND KASHMIR</asp:ListItem>
                                                <asp:ListItem>JHARKHAND</asp:ListItem>
                                                <asp:ListItem>KARNATAKA</asp:ListItem>
                                                <asp:ListItem>KERALA</asp:ListItem>
                                                <asp:ListItem>MADHYA PRADESH</asp:ListItem>
                                                <asp:ListItem>MAHARASHTRA</asp:ListItem>
                                                <asp:ListItem>MANIPUR</asp:ListItem>
                                                <asp:ListItem>MEGHALAYA</asp:ListItem>
                                                <asp:ListItem>MIZORAM</asp:ListItem>
                                                <asp:ListItem>NAGALAND</asp:ListItem>
                                                <asp:ListItem>ORISSA</asp:ListItem>
                                                <asp:ListItem>PUNJAB</asp:ListItem>
                                                <asp:ListItem>RAJASTHAN</asp:ListItem>
                                                <asp:ListItem>SIKKIM</asp:ListItem>
                                                <asp:ListItem>TAMIL NADU</asp:ListItem>
                                                <asp:ListItem>TRIPURA</asp:ListItem>
                                                <asp:ListItem>UTTAR PRADESH</asp:ListItem>
                                                <asp:ListItem>UTTARAKHAND</asp:ListItem>
                                                <asp:ListItem>WEST BENGAL</asp:ListItem>
                                                <asp:ListItem>ANDAMAN AND NICOBAR ISLANDS</asp:ListItem>
                                                <asp:ListItem>CHANDIGARH</asp:ListItem>
                                                <asp:ListItem>DADRA AND NAGAR HAVELI</asp:ListItem>
                                                <asp:ListItem>DAMAN AND DIU</asp:ListItem>
                                                <asp:ListItem>LAKSHADWEEP</asp:ListItem>
                                                <asp:ListItem>NATIONAL CAPITAL TERRITORY OF DELHI</asp:ListItem>
                                                <asp:ListItem>PUDUCHERRY</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <b>Pin Code</b><span class="required">*</span>
                                            <asp:TextBox runat="server" ID="txtpincode" class="form-control topMargin" AutoCompleteType="Disabled" placeholder="Pincode" MaxLength="6"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                        <div class="col-md-6">
                            <fieldset class="scheduler-border">
                                <legend class="scheduler-border" style="margin-bottom: 0px; font-weight: bold; font-family: 'Times New Roman'">[Permanent Address]<span style="font-size: 15px">
                                    <asp:CheckBox runat="server" ID="chk_same" CausesValidation="False" OnCheckedChanged="chk_same_CheckedChanged" Text="Same as Current Address" AutoPostBack="true" />
                                </span></legend>

                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <b>Address</b> <span class="required">*</span>
                                            <textarea runat="server" id="txt_add1" autocompletetype="Disabled" class="form-control" placeholder="Flat no./bldg"></textarea>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <span style="FONT-SIZE: 10pt; FONT-FAMILY: Verdana"><b>State</b></span><span class="required">*</span>
                                            <asp:DropDownList onblur="OnBlur(this);" ID="ddl_state1" AutoPostBack="true" onfocus="OnFocus(this);" CssClass="form-control" runat="server" ToolTip="STATE">
                                                <asp:ListItem>--Select--</asp:ListItem>
                                                <asp:ListItem>ANDHRA PRADESH</asp:ListItem>
                                                <asp:ListItem>ARUNACHAL PRADESH</asp:ListItem>
                                                <asp:ListItem>ASSAM</asp:ListItem>
                                                <asp:ListItem>BIHAR</asp:ListItem>
                                                <asp:ListItem>CHHATTISGARH</asp:ListItem>
                                                <asp:ListItem>GOA</asp:ListItem>
                                                <asp:ListItem>GUJARAT</asp:ListItem>
                                                <asp:ListItem>HARYANA</asp:ListItem>
                                                <asp:ListItem>HIMACHAL PRADESH</asp:ListItem>
                                                <asp:ListItem>JAMMU AND KASHMIR</asp:ListItem>
                                                <asp:ListItem>JHARKHAND</asp:ListItem>
                                                <asp:ListItem>KARNATAKA</asp:ListItem>
                                                <asp:ListItem>KERALA</asp:ListItem>
                                                <asp:ListItem>MADHYA PRADESH</asp:ListItem>
                                                <asp:ListItem>MAHARASHTRA</asp:ListItem>
                                                <asp:ListItem>MANIPUR</asp:ListItem>
                                                <asp:ListItem>MEGHALAYA</asp:ListItem>
                                                <asp:ListItem>MIZORAM</asp:ListItem>
                                                <asp:ListItem>NAGALAND</asp:ListItem>
                                                <asp:ListItem>ORISSA</asp:ListItem>
                                                <asp:ListItem>PUNJAB</asp:ListItem>
                                                <asp:ListItem>RAJASTHAN</asp:ListItem>
                                                <asp:ListItem>SIKKIM</asp:ListItem>
                                                <asp:ListItem>TAMIL NADU</asp:ListItem>
                                                <asp:ListItem>TRIPURA</asp:ListItem>
                                                <asp:ListItem>UTTAR PRADESH</asp:ListItem>
                                                <asp:ListItem>UTTARAKHAND</asp:ListItem>
                                                <asp:ListItem>WEST BENGAL</asp:ListItem>
                                                <asp:ListItem>ANDAMAN AND NICOBAR ISLANDS</asp:ListItem>
                                                <asp:ListItem>CHANDIGARH</asp:ListItem>
                                                <asp:ListItem>DADRA AND NAGAR HAVELI</asp:ListItem>
                                                <asp:ListItem>DAMAN AND DIU</asp:ListItem>
                                                <asp:ListItem>LAKSHADWEEP</asp:ListItem>
                                                <asp:ListItem>NATIONAL CAPITAL TERRITORY OF DELHI</asp:ListItem>
                                                <asp:ListItem>PUDUCHERRY</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <b>Pin Code</b><span class="required">*</span>
                                            <asp:TextBox runat="server" ID="txt_pin" AutoCompleteType="Disabled" class="form-control topMargin" placeholder="Pincode" MaxLength="6"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                            </fieldset>
                        </div>

                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <fieldset class="scheduler-border">
                                <legend class="scheduler-border" style="margin-bottom: 0px; font-weight: bold; font-family: 'Times New Roman'">[Other Details]</legend>
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="lbl_doj" runat="server"><b>D.O.J </b></asp:Label><span class="required">*</span>
                                            <asp:TextBox ID="txt_doj" runat="server" AutoCompleteType="Disabled" CssClass="form-control" placeholder="Date of Joining" disabled="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="lbl_department" runat="server"><b>Department</b></asp:Label><span class="required">*</span>
                                            <asp:TextBox ID="txt_department" runat="server" AutoCompleteType="Disabled" CssClass="form-control" placeholder="Department" disabled="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="lbl_designation" runat="server"><b>Designation</b></asp:Label><span class="required">*</span>
                                            <asp:TextBox ID="txt_designation" runat="server" AutoCompleteType="Disabled" CssClass="form-control" placeholder="Designation" disabled="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label_blood_group" runat="server"><b>Blood Group</b></asp:Label><span class="required">*</span>
                                            <asp:DropDownList ID="ddlBloodGroup" AutoPostBack="true" CssClass="form-control" runat="server">
                                                <asp:ListItem>--Select--</asp:ListItem>
                                                <asp:ListItem>A+</asp:ListItem>
                                                <asp:ListItem>B+</asp:ListItem>
                                                <asp:ListItem>O+</asp:ListItem>
                                                <asp:ListItem>A-</asp:ListItem>
                                                <asp:ListItem>B-</asp:ListItem>
                                                <asp:ListItem>O-</asp:ListItem>
                                                <asp:ListItem>AB+</asp:ListItem>
                                                <asp:ListItem>AB-</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <b>Category</b><span class="required">*</span>
                                            <asp:DropDownList onblur="OnBlur(this);" ID="ddlCategory" onfocus="OnFocus(this);"
                                                runat="server" CssClass="uppercase form-control" Font-Names="Verdana" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"
                                                AutoPostBack="True">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <b>Caste</b><span class="required">*</span>
                                            <asp:DropDownList onblur="OnBlur(this);" ID="ddlCast" onfocus="OnFocus(this);"
                                                runat="server" ToolTip="Caste" CssClass="uppercase form-control" Font-Names="Verdana">
                                                
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="lbl_mobile_no" runat="server"><b>Mobile No</b></asp:Label><span class="required">*</span>
                                            <asp:TextBox ID="txt_mobile_no" runat="server" AutoCompleteType="Disabled" CssClass="form-control" placeholder="Mobile No" MaxLength="10"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="lbl_mob_no" runat="server"><b>Telephone No</b></asp:Label>
                                            <asp:TextBox ID="txt_mob_no" runat="server" AutoCompleteType="Disabled" CssClass="form-control" placeholder="Telephone No" MaxLength="10"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-3 col-sm-4 col-md-4">
                                        <div class="form-group">
                                            <asp:Label ID="lbl_email" runat="server"><b>Email ID</b></asp:Label><span class="required">*</span>
                                            <asp:TextBox ID="txt_email" runat="server" AutoCompleteType="Disabled" CssClass="form-control" placeholder="Email ID"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="invalid email_id" ControlToValidate="txt_email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                    <div class="col-md-3 col-sm-4 col-md-4">
                                        <div class="form-group">
                                            <asp:Label ID="lbl_pan" runat="server"><b>Pancard No.</b></asp:Label>
                                            <asp:TextBox ID="txt_pan" runat="server" AutoCompleteType="Disabled" CssClass="form-control" placeholder="Pancard No" MaxLength="10"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3 col-sm-4 col-md-4">
                                        <div class="form-group">
                                            <asp:Label ID="lbl_adhar" runat="server"><b>Aadhar No.</b></asp:Label>
                                            <asp:TextBox ID="txt_adhar" runat="server" AutoCompleteType="Disabled" CssClass="form-control" placeholder="Aadhar No" MaxLength="12"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </div>



                    <div class="row">

                        <div class="col-md-4 "></div>
                        <div class="col-md-4">
                            <asp:Button ID="btn_submit" runat="server" Text="Submit" CssClass="btn btn-block btn-success" OnClick="btn_submit_Click" />
                        </div>
                        <div class="col-md-4"></div>

                    </div>
                </ContentTemplate>

            </asp:UpdatePanel>
        </div>
    </div>

    <script src="bootstrap-datepicker-1.9.0-dist/js/bootstrap-datepicker.min.js"></script>
    <script>
        function loaddate() {
            $('.todate').datepicker({
                format: "dd/mm/yyyy",
                orientation: "bottom"

            });
        }
        $(document).on('keypress', '#<%= txt_dob.ClientID %>', function (event) {
            var regex = new RegExp("^[0-9' ]");
            var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
            if (!regex.test(key)) {
                event.preventDefault();
                return false;
            }
        });
        $(document).on('keypress', '#<%= txt_first_name.ClientID %>', function (event) {
            var regex = new RegExp("^[a-zA-Z' ]");
            var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
            if (!regex.test(key)) {
                event.preventDefault();
                return false;
            }
        });
        $(document).on('keypress', '#<%= txt_middle_name.ClientID %>', function (event) {
            var regex = new RegExp("^[a-zA-Z' ]");
            var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
            if (!regex.test(key)) {
                event.preventDefault();
                return false;
            }
        });
        $(document).on('keypress', '#<%= txt_last_name.ClientID %>', function (event) {
            var regex = new RegExp("^[a-zA-Z' ]");
            var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
            if (!regex.test(key)) {
                event.preventDefault();
                return false;
            }
        });
        $(document).on('keypress', '#<%= txt_mother.ClientID %>', function (event) {
            var regex = new RegExp("^[a-zA-Z' ]");
            var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
            if (!regex.test(key)) {
                event.preventDefault();
                return false;
            }
        })
        $(document).on('keypress', '#<%= txt_pin.ClientID %>', function (event) {
            var regex = new RegExp("^[0-9]");
            var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
            if (!regex.test(key)) {
                event.preventDefault();
                return false;
            }
        });
        $(document).on('keypress', '#<%= txtpincode.ClientID %>', function (event) {
            var regex = new RegExp("^[0-9]");
            var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
            if (!regex.test(key)) {
                event.preventDefault();
                return false;
            }
        });
        $(document).on('keypress', '#<%= txt_mob_no.ClientID %>', function (event) {
            var regex = new RegExp("^[0-9]");
            var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
            if (!regex.test(key)) {
                event.preventDefault();
                return false;
            }
        });
        $(document).on('keypress', '#<%= txt_mobile_no.ClientID %>', function (event) {
            var regex = new RegExp("^[0-9]");
            var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
            if (!regex.test(key)) {
                event.preventDefault();
                return false;
            }
        });
        $(document).on('keypress', '#<%= txt_pan.ClientID %>', function (event) {
            var regex = new RegExp("^[a-zA-Z0-9' ]");
            var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
            if (!regex.test(key)) {
                event.preventDefault();
                return false;
            }
        });
        $(document).on('keypress', '#<%= txt_adhar.ClientID %>', function (event) {
            var regex = new RegExp("^[0-9]");
            var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
            if (!regex.test(key)) {
                event.preventDefault();
                return false;
            }
        });



    </script>
    <script>

        $("[id*=get_photo]").change(function () {
            var photo_type = "";
            var photo = document.getElementById("<%=get_photo.ClientID %>");
            if ($("[id*=get_photo]").val() != '') {
                res = $("[id*=get_photo]").get(0).files[0].name.split(".");
                photo_type = '.' + res[res.length - 1];
            }
            if (photo_type == ".jpg" || photo_type == ".png" || photo_type == ".jpeg") {
                if ($("[id*=get_photo]").get(0).files[0].size > 50000) {
                    alert("File Size Should be not more than 50Kb");
                    photo.value = '';
                }
                else {
                    readURL(this, 'emp_photo');
                }
            }
            else {
                alert("Image should be in .JPG or .PNG format");
                photo.value = '';
            }
        });


        $("[id*=get_sign]").change(function () {
            var photo_type = "";
            var sign = document.getElementById("<%=get_sign.ClientID %>");
             if ($("[id*=get_sign]").val() != '') {
                 res = $("[id*=get_sign]").get(0).files[0].name.split(".");
                 photo_type = '.' + res[res.length - 1];
             }
             if (photo_type == ".jpg" || photo_type == ".png" || photo_type == ".jpeg") {
                 if ($("[id*=get_sign]").get(0).files[0].size > 50000) {
                     alert("File Size Should be not more than 50Kb");
                     sign.value = '';
                 }
                 else {
                     readURL(this, 'emp_sign');
                 }
             }
             else {
                 alert("Image should be in .JPG or .PNG format");
                 sign.value = '';
             }
         });

         function readURL(input, imgID) {
             if (input.files && input.files[0]) {
                 var reader = new FileReader();

                 reader.onload = function (e) {
                     $("[id*=" + imgID + "]").attr('src', e.target.result);
                 }

                 reader.readAsDataURL(input.files[0]);
             }
         }
    </script>

    <script>
        $("body").on("click", "#btntoggle", function () {
            var name = $("[id*=btntoggle]").text();
            if (name == "Signature") {
                $("[id*=imagediv]").hide();
                $("[id*=signdiv]").show();
                $("[id*=btntoggle]").text('Photo');
            }
            else if (name == "Photo") {
                $("[id*=imagediv]").show();
                $("[id*=signdiv]").hide();
                $("[id*=btntoggle]").text('Signature');
            }
        });
    </script>
    <script type="text/javascript">
        function ClearPhotos() {
            var photo = document.getElementById("<%=get_photo.ClientID %>");
            photo.value = '';
            var sign = document.getElementById("<%=get_sign.ClientID %>");
            sign.value = '';
            if ($("[id*=lblphoto]")[0].innerHTML != '' || $("[id*=lblsign]")[0].innerHTML != '') {
                $("[id*=emp_photo]").attr("src", $("[id*=lblphoto]")[0].innerHTML);
                $("[id*=emp_sign]").attr("src", $("[id*=lblsign]")[0].innerHTML);

            }
            else {
                $("[id*=emp_photo]").attr("src", "../utkarsha/image/user.png");
                $("[id*=emp_sign]").attr("src", "../utkarsha/image/sign.png");
                $("[id*=imagediv]").show();
                $("[id*=signdiv]").hide();
                $("[id*=btntoggle]").text('Signature');
            }
        }
    </script>
</asp:Content>





