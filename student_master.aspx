<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="student_master.aspx.cs" Inherits="student_master" EnableEventValidation="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/libs/bootstrap-datepicker/dist/css/bootstrap-datepicker.min.css" rel="stylesheet" />

    <script src="assets/libs/jquery/dist/jquery.min.js"></script>
    <script src="bootstrap-datepicker-1.9.0-dist/js/bootstrap-datepicker.min.js"></script>
    <link href="notify-master/css/notify.css" rel="stylesheet" />
    <script src="notify-master/js/notify.js"></script>
    <link href="css/theme.min.css" rel="stylesheet" />
    <script>
        function loaddate() {
            $('[id*=txtdate_admission]').datepicker(
                {
                    minDate: 0,
                    timepicker: true,
                    format: 'dd/mm/yyyy'
                });

            $('[id*=txtdob]').datepicker(
                {
                    minDate: 0,
                    timepicker: true,
                    format: 'dd/mm/yyyy',
                    orientation: 'bottom'
                });
        }
    </script>
    <script>
        function getAge(dateString) {
            if (dateString.value != null) {
                if (dateString.value.length == 10) {
                    var now = new Date();
                    var today = new Date(now.getDate(), now.getMonth(), now.getYear());

                    var yearNow = now.getFullYear();
                    var monthNow = now.getMonth() + 1;
                    var dateNow = now.getDate();

                    var arr = dateString.value.split('/');

                    var dateDob = arr[0];
                    var monthDob = arr[1];
                    var yearDob = arr[2];

                    var age = {};
                    var ageString = "";
                    var yearString = "";
                    var monthString = "";
                    var dayString = "";


                    yearAge = yearNow - yearDob;

                    if (monthNow >= monthDob)
                        var monthAge = monthNow - monthDob;
                    else {
                        yearAge--;
                        var monthAge = 12 + monthNow - monthDob;
                    }

                    if (dateNow >= dateDob)
                        var dateAge = dateNow - dateDob;
                    else {
                        monthAge--;
                        var dateAge = 31 + dateNow - dateDob;

                        if (monthAge < 0) {
                            monthAge = 11;
                            yearAge--;
                        }
                    }

                    age = {
                        years: yearAge,
                        months: monthAge,
                        days: dateAge
                    };

                    if (age.years > 1) yearString = " Years";
                    else yearString = " Year";
                    if (age.months > 1) monthString = " Months";
                    else monthString = " Month";
                    if (age.days > 1) dayString = " Days";
                    else dayString = " Day";

                    ageString = age.years + yearString + " - " + age.months + monthString;

                    $('[id*=txtage]').val(ageString);
                    return ageString;
                }
            }
        }

    </script>
    <style>
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
    <style type="text/css">
        a {
            color: #0078bc;
        }

            a:hover {
                color: black;
            }

        .custom-file-input {
            color: transparent;
        }

            .custom-file-input::-webkit-file-upload-button {
                visibility: hidden;
            }

            .custom-file-input::before {
                content: 'Choose';
                color: black;
                display: inline-block;
                background: -webkit-linear-gradient(top, #f9f9f9, #e3e3e3);
                border: 1px solid #999;
                border-radius: 3px;
                padding: 5px 5px;
                outline: none;
                white-space: nowrap;
                -webkit-user-select: none;
                cursor: pointer;
                text-shadow: 1px 1px #fff;
                font-weight: 700;
                font-size: 10pt;
            }

            .custom-file-input:hover::before {
                border-color: black;
            }

            .custom-file-input:active {
                outline: 0;
            }

                .custom-file-input:active::before {
                    background: -webkit-linear-gradient(top, #e3e3e3, #f9f9f9);
                }
    </style>
    <style>
        .tooltip-arrow,
        .form-control + .tooltip > .tooltip-inner {
            background-color: #f00;
        }
    </style>
    <style>
        main {
            text-align: center;
            margin: 0 auto;
            max-width: 800px;
        }

        p {
            text-align: left;
            padding: 0 20px;
        }

        code {
            color: firebrick;
        }

        h1 {
            position: relative;
            /*margin-top: 20px;*/
            font-family: "Open Sans Condensed", sans-serif;
            text-align: center;
            position: relative;
        }

        .one {
            margin-top: 0;
        }

            .one:before {
                content: "";
                display: block;
                border-top: solid 1px black;
                width: 100%;
                height: 1px;
                position: absolute;
                top: 50%;
                z-index: 1;
            }

            .one span {
                background: #fff;
                padding: 0 20px;
                position: relative;
                z-index: 5;
            }

        .two span {
            background: #fff;
            padding: 0 10px;
            position: relative;
            z-index: 5;
        }

        .two + p {
            border-top: solid 1px black;
            padding-top: 2.5em;
            margin-top: -2.5em;
        }

        .three {
            background: linear-gradient(#ffffff 0%, #ffffff 49%, #000000 50%, #e4d7d7 51%, #ffffff 52%, #ffffff 100%);
        }

            .three span {
                background: #fff;
                padding: 0 20px;
                position: relative;
                z-index: 5;
            }

        .four {
            margin-top: 0;
        }

            .four:before {
                content: "";
                display: block;
                border-top: solid 1px black;
                width: 100%;
                height: 1px;
                position: absolute;
                top: 50%;
                z-index: 1;
            }

            .four span {
                background: #fff;
                position: relative;
                z-index: 5;
                padding: 0 10px;
            }

        @media (max-width: 480px) {
            .four:before {
                content: "";
                border: none;
            }

            .four span {
                background: inherit;
                position: static;
                padding: 0;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="card">
        <div class="card-header text-white" style="background: linear-gradient(to right,#0078bc 1%,#00beda 100%) !important; border-radius: 6px;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-md-9">
                            <h3>Student Master</h3>
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="lblstudid" runat="server" Visible="false"></asp:Label>
                            <asp:TextBox ID="txtform_id" runat="server" AutoCompleteType="Disabled" AutoPostBack="true" placeholder="Form Id / Student Id" OnTextChanged="txtform_id_TextChanged" CssClass="form-control" Style="float: right; width: 200px; text-transform: uppercase;"></asp:TextBox>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="card-body">
            <div class="row">
                <%-- 1st column start --%>
                <div class="col-md-10">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <%-- 1st row start --%>
                            <div class="row">
                                <div class="col-md-2">
                                    <b>Admission Date<span class="required">*</span></b>
                                    <asp:TextBox ID="txtdate_admission" runat="server" AutoCompleteType="Disabled" CssClass="datepickeradmdate form-control " data-toggle="tooltip" data-placement="Top" title="Date of Admission" TabIndex="1"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <b>Medium<span class="required">*</span></b>
                                    <asp:DropDownList ID="ddlmedium" AutoCompleteType="Disabled" runat="server" AutoPostBack="true" CssClass="form-control" onclientclick="loaddate()" data-toggle="tooltip" data-placement="bottom" title="Select Medium" OnSelectedIndexChanged="ddlmedium_SelectedIndexChanged" TabIndex="2">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <b>Standard<span class="required">*</span></b>
                                    <asp:DropDownList ID="ddlclass" AutoCompleteType="Disabled" runat="server" data-toggle="tooltip" data-placement="bottom" title="Select Standard" CssClass="form-control" TabIndex="3"></asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    <b>Saral No.</b>
                                    <asp:TextBox ID="txtsaral" runat="server" AutoCompleteType="Disabled" onkeypress="CheckNumeric(event)" data-toggle="tooltip" data-placement="bottom" title="Enter 19 Digits Saral No" MaxLength="19" CssClass="form-control" TabIndex="4"></asp:TextBox>
                                </div>
                                <div class="col-md-3" style="margin-top: 3px;">
                                    <br />
                                    <b>Gender<span class="required">*</span>&nbsp;:</b>
                                    <asp:RadioButton runat="server" ID="rbmale" GroupName="radioA" Text="Male" />&nbsp;&nbsp;
                                        <asp:RadioButton runat="server" ID="rbfemale" GroupName="radioA" Text="Female" />&nbsp;&nbsp;
                                        <asp:RadioButton runat="server" ID="rbtrans" GroupName="radioA" Text="Transgender" />
                                </div>
                            </div>
                            <%-- 1st row end --%>
                            <%-- 2nd row start --%>
                            <div class="row" style="padding-top: 3px;">
                                <div class="col-md-12">
                                    <div class="row">
                                        <div class="col-md-3">
                                            <b>Surname<span class="required">*</span></b>
                                            <asp:TextBox ID="txtsurname" AutoCompleteType="Disabled" onkeypress="return surname(event)" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="bottom" Style="text-transform: uppercase;" title="Enter Surname" TabIndex="5" MaxLength="50"></asp:TextBox>
                                        </div>
                                        <div class="col-md-3">
                                            <b>First Name<span class="required">*</span></b>
                                            <asp:TextBox ID="txtname" AutoCompleteType="Disabled" onkeypress="return nametxt(event)" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="bottom" Style="text-transform: uppercase;" title="Enter First Name" TabIndex="6" MaxLength="50"></asp:TextBox>
                                        </div>
                                        <div class="col-md-3">
                                            <b>Father Name<span class="required">*</span></b>
                                            <asp:TextBox ID="txtfname" AutoCompleteType="Disabled" onkeypress="return nametxt(event)" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="bottom" Style="text-transform: uppercase;" title="Enter Father Name" TabIndex="7" MaxLength="50"></asp:TextBox>
                                        </div>
                                        <div class="col-md-3">
                                            <b>Mother Name<span class="required">*</span></b>
                                            <asp:TextBox ID="txtmo_name" AutoCompleteType="Disabled" onkeypress="return nametxt(event)" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="bottom" Style="text-transform: uppercase;" title="Enter Mother Name" TabIndex="8" MaxLength="50"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <%-- 2nd row end --%>

                            <%-- 3rd row start --%>

                            <div class="row" style="padding-top: 3px;">
                                <div class="col-md-12">
                                    <b>Permanant Address<span class="required">*</span></b>
                                    <asp:TextBox ID="txtpersent_addr" AutoCompleteType="Disabled" runat="server" onkeypress="return address(event)" CssClass="form-control" data-toggle="tooltip" Style="text-transform: uppercase;" data-placement="bottom" title="Enter Permanant Address of Student" TabIndex="9"></asp:TextBox>
                                </div>
                            </div>

                            <%-- 3rd row end --%>
                            <%-- 4th row start --%>

                            <div class="row" style="padding-top: 3px;">
                                <div class="col-md-12">
                                    <div class="row">
                                        <div class="col-md-3">
                                            <b>Pin Code<span class="required">*</span></b>
                                            <asp:TextBox ID="txtpincode" AutoCompleteType="Disabled" onkeypress="CheckNumeric(event)" MaxLength="6" runat="server" CssClass="form-control" data-toggle="tooltip " data-placement="bottom" title="Enter Pincode" TabIndex="10"></asp:TextBox>
                                        </div>
                                        <div class="col-md-3">
                                            <b>Taluka<span class="required">*</span></b>
                                            <asp:TextBox ID="txttaluka" runat="server" AutoCompleteType="Disabled" MaxLength="50" onkeypress="return nametxt(event)" CssClass="form-control" data-toggle="tooltip" Style="text-transform: uppercase;" data-placement="bottom" title="Enter Taluka" TabIndex="11"></asp:TextBox>
                                        </div>
                                        <div class="col-md-3">
                                            <b>District<span class="required">*</span></b>
                                            <asp:TextBox ID="txtdistrict" AutoCompleteType="Disabled" MaxLength="50" onkeypress="return nametxt(event)" runat="server" CssClass="form-control" data-toggle="tooltip" Style="text-transform: uppercase;" data-placement="bottom" title="Enter District" TabIndex="12"></asp:TextBox>
                                        </div>
                                        <div class="col-md-3">
                                            <b>State<span class="required">*</span></b>
                                            <asp:DropDownList ID="ddlstate" runat="server" AutoCompleteType="Disabled" MaxLength="50" onkeypress="return nametxt(event)" CssClass="form-control" data-toggle="tooltip" data-placement="bottom" title="Enter State" TabIndex="13">
                                                <asp:ListItem>--State--</asp:ListItem>
                                                <asp:ListItem>ANDAMAN AND NICOBAR ISLANDS</asp:ListItem>
                                                <asp:ListItem>ANDHRA PRADESH</asp:ListItem>
                                                <asp:ListItem>ARUNACHAL PRADESH</asp:ListItem>
                                                <asp:ListItem>ASSAM</asp:ListItem>
                                                <asp:ListItem>BIHAR</asp:ListItem>
                                                <asp:ListItem>CHANDIGARH</asp:ListItem>
                                                <asp:ListItem>CHHATTISGARH</asp:ListItem>
                                                <asp:ListItem>DADRA AND NAGAR HAVELI</asp:ListItem>
                                                <asp:ListItem>DAMAN AND DIU</asp:ListItem>
                                                <asp:ListItem>GOA</asp:ListItem>
                                                <asp:ListItem>GUJARAT</asp:ListItem>
                                                <asp:ListItem>HARYANA</asp:ListItem>
                                                <asp:ListItem>HIMACHAL PRADESH</asp:ListItem>
                                                <asp:ListItem>JAMMU AND KASHMIR</asp:ListItem>
                                                <asp:ListItem>JHARKHAND</asp:ListItem>
                                                <asp:ListItem>KARNATAKA</asp:ListItem>
                                                <asp:ListItem>KERALA</asp:ListItem>
                                                <asp:ListItem>LAKSHADWEEP</asp:ListItem>
                                                <asp:ListItem>MADHYA PRADESH</asp:ListItem>
                                                <asp:ListItem>MAHARASHTRA</asp:ListItem>
                                                <asp:ListItem>MANIPUR</asp:ListItem>
                                                <asp:ListItem>MEGHALAYA</asp:ListItem>
                                                <asp:ListItem>MIZORAM</asp:ListItem>
                                                <asp:ListItem>NAGALAND</asp:ListItem>
                                                <asp:ListItem>NATIONAL CAPITAL TERRITORY OF DELHI</asp:ListItem>
                                                <asp:ListItem>ODISHA</asp:ListItem>
                                                <asp:ListItem>PONDICHERRY</asp:ListItem>
                                                <asp:ListItem>PUNJAB</asp:ListItem>
                                                <asp:ListItem>RAJASTHAN</asp:ListItem>
                                                <asp:ListItem>SIKKIM</asp:ListItem>
                                                <asp:ListItem>TAMIL NADU</asp:ListItem>
                                                <asp:ListItem>TELANGANA</asp:ListItem>
                                                <asp:ListItem>TRIPURA</asp:ListItem>
                                                <asp:ListItem>UTTAR PRADESH</asp:ListItem>
                                                <asp:ListItem>UTTARAKHAND</asp:ListItem>
                                                <asp:ListItem>WEST BENGAL</asp:ListItem>
                                                <asp:ListItem>अंदमान आणि निकोबार बेट</asp:ListItem>
                                                <asp:ListItem>आंध्र प्रदेश</asp:ListItem>
                                                <asp:ListItem>अरुणाचल प्रदेश</asp:ListItem>
                                                <asp:ListItem>आसाम</asp:ListItem>
                                                <asp:ListItem>बिहार</asp:ListItem>
                                                <asp:ListItem>चंदीगड</asp:ListItem>
                                                <asp:ListItem>छत्तीसगड</asp:ListItem>
                                                <asp:ListItem>दादरा आणि नगर हवेली</asp:ListItem>
                                                <asp:ListItem>दमण आणि दिव</asp:ListItem>
                                                <asp:ListItem>गोवा</asp:ListItem>
                                                <asp:ListItem>गुजरात</asp:ListItem>
                                                <asp:ListItem>हरियाणा</asp:ListItem>
                                                <asp:ListItem>हिमाचल प्रदेश</asp:ListItem>
                                                <asp:ListItem>जम्मू आणि काश्मीर</asp:ListItem>
                                                <asp:ListItem>झारखंड</asp:ListItem>
                                                <asp:ListItem>कर्नाटक</asp:ListItem>
                                                <asp:ListItem>केरळ</asp:ListItem>
                                                <asp:ListItem>लक्षद्वीप</asp:ListItem>
                                                <asp:ListItem>मध्य प्रदेश</asp:ListItem>
                                                <asp:ListItem>महाराष्ट्र</asp:ListItem>
                                                <asp:ListItem>मणिपूर</asp:ListItem>
                                                <asp:ListItem>मेघालय	</asp:ListItem>
                                                <asp:ListItem>मिझोराम</asp:ListItem>
                                                <asp:ListItem>नागालँड	</asp:ListItem>
                                                <asp:ListItem>राष्ट्रीय राजधानी प्रदेश दिल्ली	</asp:ListItem>
                                                <asp:ListItem>ओडिशा</asp:ListItem>
                                                <asp:ListItem>पाँडिचेरी</asp:ListItem>
                                                <asp:ListItem>पंजाब</asp:ListItem>
                                                <asp:ListItem>राजस्थान</asp:ListItem>
                                                <asp:ListItem>सिक्कीम</asp:ListItem>
                                                <asp:ListItem>तामिळनाडू</asp:ListItem>
                                                <asp:ListItem>तेलंगाणा</asp:ListItem>
                                                <asp:ListItem>त्रिपुरा</asp:ListItem>
                                                <asp:ListItem>उत्तर प्रदेश</asp:ListItem>
                                                <asp:ListItem>उत्तराखंड</asp:ListItem>
                                                <asp:ListItem>पश्चिम बंगाल</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <%-- 4th row end--%>
                            <%-- 5th row start --%>

                            <div class="row" style="padding-top: 3px;">
                                <div class="col-md-12">
                                    <div class="row">
                                        <div class="col-md-3">
                                            <b>Mobile No.<span class="required">*</span></b>
                                            <asp:TextBox ID="txtmob" AutoCompleteType="Disabled" runat="server" onkeypress="CheckNumeric(event)" MaxLength="10" CssClass="form-control" data-toggle="tooltip" data-placement="bottom" title="Enter Mobile No" TabIndex="14"></asp:TextBox>
                                        </div>
                                        <div class="col-md-3">
                                            <b>Telephone No.</b>
                                            <asp:TextBox ID="txtres_no" AutoCompleteType="Disabled" onkeypress="CheckNumeric(event)" MaxLength="12" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="bottom" title="Enter Telephone No" TabIndex="15"></asp:TextBox>
                                        </div>
                                        <div class="col-md-3">
                                            <b>Date of Birth<span class="required">*</span></b>
                                            <asp:TextBox ID="txtdob" runat="server" AutoCompleteType="Disabled" CssClass="datepickerdob form-control" onchange="getAge(this)" data-toggle="tooltip" data-placement="bottom" title="Enter Date of Birth" TabIndex="16"></asp:TextBox>
                                        </div>
                                        <div class="col-md-3">
                                            <b>Age</b>
                                            <asp:TextBox ID="txtage" AutoCompleteType="Disabled" Enabled="false" AutoPostBack="true" placeholder="(YY-MM)" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="bottom" title=" Age is in (YY-MM) "></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <%-- 5th row end--%>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <%-- 1st column end --%>

                <%-- image start--%>

                <div class="col-md-2">
                    <div class="row" style="text-align: center;">
                        <div class="col-md-12">
                            <div class="panel panel-info" style="padding-left: 5px; padding-right: 5px; padding-top: 10px; padding-bottom: 7px; border: 1px solid; border-top-right-radius: 15px; border-bottom-left-radius: 15px; height: 231px">
                                <div class="col-md-12" id="imagediv" runat="server">
                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                        <ContentTemplate>
                                            <asp:Image runat="server" draggable="false" alt="Photo" class="img-fluid img-circle" ID="stud_photo" Style="height: 160px; width: 150px; margin-bottom: 10px;" />
                                            <asp:Label ID="lblphoto" runat="server" Style="display: none"></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <asp:FileUpload ID="get_photo" runat="server" CssClass="form-control" />
                                </div>
                                <div class="col-md-12" id="signdiv" runat="server" style="display: none;">
                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                        <ContentTemplate>
                                            <asp:Image runat="server" draggable="false" alt="Photo" class="img-fluid img-thumbnail" ID="stud_sign" Style="height: 60px; width: 150px; margin-top: 55px;" />
                                            <asp:Label ID="lblsign" runat="server" Style="display: none"></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <br />
                                    <asp:FileUpload runat="server" ID="get_sign" CssClass="form-control" Style="margin-top: 34px;" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row" style="padding-top: 4px;">
                        <div class="col-md-6"><a id="btntoggle" class="btn btn-info btn-" style="color: white; display: block;"><span>Signature</span></a></div>
                        <div class="col-md-6"><a id="btnClearPhotos" class="btn btn-info form-control" onclick="ClearPhotos()" style="color: white; display: block;"><i class="fas fa-eraser"></i>&nbsp<span>Clear</span></a></div>
                    </div>
                </div>
            </div>
            <%-- image end --%>

            <%-- 6th row start --%>

            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div class="row" style="padding-top: 3px;">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-3">
                                    <b>Alternate No.</b>
                                    <asp:TextBox ID="txtco_mob" MaxLength="10" AutoCompleteType="Disabled" onkeypress="CheckNumeric(event)" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="bottom" title="Enter Alternate Contact No" TabIndex="17"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <b>Aadhar No.</b>
                                    <asp:TextBox ID="txtaadhar" runat="server" AutoCompleteType="Disabled" onkeypress="CheckNumeric(event)" MaxLength="12" CssClass="form-control" data-toggle="tooltip" data-placement="bottom" title="Enter Aadharcard No" TabIndex="18"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <b>Nationality</b>
                                    <asp:TextBox ID="txtnationality" AutoCompleteType="Disabled" onkeypress="return nametxt(event)" MaxLength="20" runat="server" CssClass="form-control" data-toggle="tooltip" Style="text-transform: uppercase;" data-placement="bottom" title="Enter Nationality" TabIndex="19"></asp:TextBox>
                                </div>
                                <%--<div class="col-md-3">
                                    <b>Birth Place<span class="required">*</span></b>
                                    <asp:TextBox ID="txtb_place" AutoCompleteType="Disabled" MaxLength="50" onkeypress="return character(event)" runat="server" CssClass="form-control" data-toggle="tooltip" Style="text-transform: uppercase;" data-placement="bottom" title="Enter Birthplace" TabIndex="20"></asp:TextBox>
                                </div>--%>
                            </div>
                            <div class="row">

                                <div class="col-md-1">
                                    <br />
                                    <b>Birth Place :</b>

                                </div>
                                <%--ID="txt_vilgecity--%>
                                <div class="col-md-2">
                                    <b>Village/City</b>
                                    <asp:TextBox ID="txt_vilgecity" AutoCompleteType="Disabled" MaxLength="50" onkeypress="return villagecitytxtbxx(event)" runat="server" CssClass="form-control" data-toggle="tooltip" Style="text-transform: uppercase;" data-placement="bottom" title="Enter Village/City" TabIndex="20"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <b>Taluka</b>
                                    <asp:TextBox ID="txt_taluka1" AutoCompleteType="Disabled" MaxLength="50" onkeypress="return nametxt(event)" runat="server" CssClass="form-control" data-toggle="tooltip" Style="text-transform: uppercase;" data-placement="bottom" title="Enter Taluka" TabIndex="20"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <b>District</b>
                                    <asp:TextBox ID="txt_distrct1" AutoCompleteType="Disabled" MaxLength="50" onkeypress="return nametxt(event)" runat="server" CssClass="form-control" data-toggle="tooltip" Style="text-transform: uppercase;" data-placement="bottom" title="Enter District" TabIndex="20"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <b>State</b>
                                    <asp:TextBox ID="txt_state1" AutoCompleteType="Disabled" MaxLength="50" onkeypress="return nametxt(event)" runat="server" CssClass="form-control" data-toggle="tooltip" Style="text-transform: uppercase;" data-placement="bottom" title="Enter State" TabIndex="20"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <b>Country</b>
                                    <asp:TextBox ID="txt_country1" AutoCompleteType="Disabled" MaxLength="50" onkeypress="return nametxt(event)" runat="server" CssClass="form-control" data-toggle="tooltip" Style="text-transform: uppercase;" data-placement="bottom" title="Enter Country" TabIndex="20"></asp:TextBox>
                                </div>
                            </div>
                            <%--      </div>--%>
                        </div>
                    </div>

                    <%-- 6th row end --%>
                    <%-- 7th row start --%>

                    <div class="row" style="padding-top: 3px;">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-3">
                                    <b>Mother Tongue</b>
                                    <asp:TextBox ID="txtmother_tongue" AutoCompleteType="Disabled" MaxLength="25" onkeypress="return character(event)" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="bottom" title="Enter Mother Tongue Language" TabIndex="21" Style="text-transform: uppercase"></asp:TextBox>
                                </div>
                                <div class="col-lg-9">
                                    <div class="row">
                                        <div class="col-md-3">
                                            <div class="row">
                                                <div class="col-md-10">
                                                    <b>Religion</b>
                                                    <asp:DropDownList ID="ddlreligion" runat="server" AutoPostBack="true" CssClass="form-control" data-toggle="tooltip" data-placement="bottom" title="Select Religion" TabIndex="22"></asp:DropDownList>
                                                </div>
                                                <div class="col-md-2">
                                                    <br />
                                                    <asp:LinkButton ID="btnreligion" class="btn btn-sm btn-success" runat="server" OnClick="btnreligion_Click" Style="background-color: #35B978; color: white">+</asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="row">
                                                <div class="col-md-10">
                                                    <b>Category</b>
                                                    <asp:DropDownList ID="ddlcategory" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlcategory_SelectedIndexChanged" data-toggle="tooltip" data-placement="bottom" title="Select Category" TabIndex="23"></asp:DropDownList>
                                                </div>
                                                <div class="col-md-2">
                                                    <br />
                                                    <asp:LinkButton ID="btncategory" class="btn btn-sm btn-success" runat="server" OnClick="btncategory_Click" Style="background-color: #35B978; color: white">+</asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="row">
                                                <div class="col-md-10">
                                                    <b>Caste</b>
                                                    <asp:DropDownList ID="ddlcaste" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlcaste_SelectedIndexChanged" CssClass="form-control" data-toggle="tooltip" data-placement="bottom" title="Select Caste" TabIndex="24"></asp:DropDownList>
                                                </div>
                                                <div class="col-md-2">
                                                    <br />
                                                    <asp:LinkButton ID="btncaste" class="btn btn-sm btn-success" runat="server" OnClick="btncaste_Click" Style="background-color: #35B978; color: white">+</asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="row">
                                                <div class="col-md-10">
                                                    <b>Subcaste</b>
                                                    <asp:DropDownList ID="ddlsubcaste" runat="server" AutoPostBack="true" CssClass="form-control" data-toggle="tooltip" placeholder="SubCaste" data-placement="bottom" title="Select Subcaste"></asp:DropDownList>
                                                </div>
                                                <div class="col-md-2">
                                                    <br />
                                                    <asp:LinkButton ID="btnsubcaste" class="btn btn-sm btn-success" runat="server" OnClick="btnsubcaste_Click" Style="background-color: #35B978; color: white">+</asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%-- 7th row end --%>
                </ContentTemplate>
            </asp:UpdatePanel>

            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                    <h1 class="three"><span style="font-size: 20px;">Previous School Details</span></h1>
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-md-3 ">
                                <b>Previous School</b>
                                <asp:TextBox ID="txt_last_schl" AutoCompleteType="Disabled" onkeypress="return character(event)" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="bottom" title="Enter Previous School Name" TabIndex="25"></asp:TextBox>
                            </div>
                            <div class="col-md-3 ">
                                <b>Std In Which Last Studied</b>
                                <asp:TextBox ID="txt_last_std" AutoCompleteType="Disabled" MaxLength="12" onkeypress="return vehicle(event)" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="bottom" title="Enter Previous Standard Studied" TabIndex="26"></asp:TextBox>
                            </div>
                            <div class="col-md-3 ">
                                <b>Grade</b>
                                <asp:TextBox ID="txtgrade" AutoCompleteType="Disabled" onkeypress="return grade(event)" runat="server" MaxLength="1" CssClass="form-control" data-toggle="tooltip" data-placement="bottom" title="Enter Grade" TabIndex="27"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <b>Percentage(%)</b>
                                <asp:TextBox ID="txtper" AutoCompleteType="Disabled" runat="server" onkeypress="CheckNumericdecimal(event)" MaxLength="5" CssClass="form-control" data-toggle="tooltip" data-placement="bottom" title="Enter Percentage" TabIndex="28"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <h1 class="three"><span style="font-size: 20px;">Bank Details</span></h1>
                    <div class="col-md-12">
                        <div class="row ">
                            <div class="col-md-3 ">
                                <b>Bank Name</b>
                                <asp:TextBox ID="txtbank_name" AutoCompleteType="Disabled" MaxLength="50" onkeypress="return bank(event)" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="bottom" title="Enter Bank Name" TabIndex="29"></asp:TextBox>
                            </div>
                            <div class="col-md-3 ">
                                <b>Branch Name</b>
                                <asp:TextBox ID="txt_branch" AutoCompleteType="Disabled" runat="server" MaxLength="50" onkeypress="return branch(event)" CssClass="form-control" data-toggle="tooltip" data-placement="bottom" title="Enter Branch Name" TabIndex="30"></asp:TextBox>
                            </div>
                            <div class="col-md-3 ">
                                <b>IFSC Code</b>
                                <asp:TextBox ID="txt_IFSC" AutoCompleteType="Disabled" runat="server" onkeypress="return vehicle(event)" MaxLength="11" CssClass="form-control" data-toggle="tooltip" data-placement="bottom" title="Enter IFSC Code of Branch" TabIndex="31"></asp:TextBox>
                            </div>
                            <div class="col-md-3 ">
                                <b>Bank A/c No.</b>
                                <asp:TextBox ID="txt_bank_acc" AutoCompleteType="Disabled" MaxLength="15" runat="server" onkeypress="CheckNumeric(event)" CssClass="form-control" data-toggle="tooltip" data-placement="bottom" title="Enter Bank Account No" TabIndex="32"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <h1 class="three"><span style="font-size: 20px;">Vehicle Details</span></h1>
                    <div class="col-md-12">
                        <div class="row ">
                            <div class="col-md-2"></div>
                            <div class="col-md-8">
                                <div class="row ">
                                    <div class="col-md-4 ">
                                        <b>Vehicle Type</b>
                                        <asp:TextBox ID="txtvehical" AutoCompleteType="Disabled" MaxLength="50" runat="server" CssClass="form-control" onkeypress="return nametxt(event)" data-toggle="tooltip" data-placement="bottom" title="Enter Vehicle Type" TabIndex="33"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4 ">
                                        <b>Vehicle Number</b>
                                        <asp:TextBox ID="txtvehical_no" AutoCompleteType="Disabled" runat="server" MaxLength="12" CssClass="form-control" onkeypress="return vehicle(event)" data-toggle="tooltip" data-placement="bottom" title="Enter Vehicle No" TabIndex="34"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4 ">
                                        <b>Driver's Mobile No.</b>
                                        <asp:TextBox ID="txtdriver" runat="server" AutoCompleteType="Disabled" MaxLength="10" onkeypress="CheckNumeric(event)" CssClass="form-control" data-toggle="tooltip" data-placement="bottom" title="Enter Driver No" TabIndex="35"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2"></div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <br />

            <%--buttons--%>
            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                <Triggers>
                    <asp:PostBackTrigger ControlID="save" />
                    <asp:PostBackTrigger ControlID="admission" />
                    <asp:PostBackTrigger ControlID="clear_button" />
                </Triggers>
                <ContentTemplate>
                    <div class="row">
                        <div class="col-md-3">
                        </div>
                        <div class="col-md-6">
                            <div class="row">
                                <div class="col-md-4" style="text-align: center;">
                                    <asp:Button ID="save" runat="server" CssClass="btn btn-primary form-control" OnClick="save_Click" Style="background-color: #2F8AC5; border-radius: 7px; border-color: #2F8AC5" TabIndex="36" UseSubmitBehavior="false" />
                                </div>
                                <div class="col-md-4" style="text-align: center;">
                                    <asp:Button ID="clear_button" runat="server" Text="Clear" CssClass="btn btn-primary form-control" OnClick="clear_button_Click" Style="background-color: #2F8AC5; border-radius: 7px; border-color: #2F8AC5" TabIndex="37" />
                                </div>
                                <div class="col-md-4" style="text-align: center;">
                                    <asp:Button ID="admission" runat="server" Text="" CssClass="btn btn-primary form-control" OnClick="admission_Click" Style="background-color: #2F8AC5; border-radius: 7px; border-color: #2F8AC5" TabIndex="38" />
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <div class="modal" id="modalreligion">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Religion</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtreligion" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                                    <asp:Label ID="lblrelid" runat="server" Text="" Visible="false"></asp:Label>
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="btnadrel" runat="server" Text="Save" CssClass="btn btn-success btn-block" OnClick="btnadrel_Click" />
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="btnclearrel" runat="server" Text="Clear" CssClass="btn btn-success btn-block" OnClick="btnclearrel_Click" />
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12" style="overflow-y: auto; height: 350px">
                                    <asp:GridView ID="grdreligion" runat="server" Font-Size="12pt"
                                        Style="text-align: center;" AutoGenerateColumns="False" CssClass="table table-hover table-bordered" OnSelectedIndexChanged="grdreligion_SelectedIndexChanged" OnRowDeleting="grdreligion_RowDeleting">
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <RowStyle HorizontalAlign="Center"></RowStyle>
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr No.">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                                <ItemStyle Width="5%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Religion">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblname" runat="server" Text='<%# Eval("religion")%>'></asp:Label>
                                                    <asp:Label ID="lblid" runat="server" Text='<%# Eval("religion_id")%>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lblflag" runat="server" Text='<%# Eval("flag")%>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="65%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnedit" runat="server" CommandName="Select" Text="<i class='fas fa-edit' aria-hidden='true'></i>" />
                                                </ItemTemplate>
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btndelete" runat="server" CommandName="Delete" Text="<i class='fas fa-trash' aria-hidden='true'></i>" />
                                                </ItemTemplate>
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <br />
                                    <br />
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <div class="modal" id="modalcategory">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Category</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtcat" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                                    <asp:Label ID="lblcatid" runat="server" Text="" Visible="false"></asp:Label>
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="btnaddcat" runat="server" Text="Save" CssClass="btn btn-success btn-block" OnClick="btnaddcat_Click" />
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="btnclear1" runat="server" Text="Clear" CssClass="btn btn-success btn-block" OnClick="btnclear1_Click" />
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12" style="overflow-y: auto; height: 350px">
                                    <asp:GridView ID="grdcategory" runat="server" Font-Size="12pt"
                                        Style="text-align: center;" AutoGenerateColumns="False" CssClass="table table-hover table-bordered" OnSelectedIndexChanged="grdcategory_SelectedIndexChanged" OnRowDeleting="grdcategory_RowDeleting">
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <RowStyle HorizontalAlign="Center"></RowStyle>
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr No.">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                                <ItemStyle Width="5%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Category">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblname" runat="server" Text='<%# Eval("category_name")%>'></asp:Label>
                                                    <asp:Label ID="lblid" runat="server" Text='<%# Eval("category_id")%>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lblflag" runat="server" Text='<%# Eval("flag")%>' Visible="false"></asp:Label>
                                                    <asp:Label ID="categorydepcount" runat="server" Text='<%# Eval("count")%>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="65%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnedit" runat="server" CommandName="Select" Text="<i class='fas fa-edit' aria-hidden='true'></i>" />
                                                </ItemTemplate>
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btndelete" runat="server" CommandName="Delete" Text="<i class='fas fa-trash' aria-hidden='true'></i>" />
                                                </ItemTemplate>
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <br />
                                    <br />
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <div class="modal" id="modalcaste">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Caste</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-md-4">
                                    <asp:DropDownList ID="ddlcategorymodal" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlcategorymodal_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtcaste" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                                    <asp:Label ID="lblcaste" runat="server" Text="" Visible="false"></asp:Label>
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="btnaddcaste" runat="server" Text="Save" CssClass="btn btn-success btn-block" OnClick="btnaddcaste_Click" />
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="btnclear2" runat="server" Text="Clear" CssClass="btn btn-success btn-block" OnClick="btnclear2_Click" />
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12" style="overflow-y: auto; height: 350px">
                                    <asp:GridView ID="grdcaste" runat="server" Font-Size="12pt"
                                        Style="text-align: center;" AutoGenerateColumns="False" CssClass="table table-hover table-bordered" OnSelectedIndexChanged="grdcaste_SelectedIndexChanged" OnRowDeleting="grdcaste_RowDeleting">
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <RowStyle HorizontalAlign="Center"></RowStyle>
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr No.">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                                <ItemStyle Width="5%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Caste">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblname" runat="server" Text='<%# Eval("cast_name")%>'></asp:Label>
                                                    <asp:Label ID="lblcatidref" runat="server" Text='<%# Eval("category_id")%>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lblid" runat="server" Text='<%# Eval("cast_id")%>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lblflag" runat="server" Text='<%# Eval("flag")%>' Visible="false"></asp:Label>
                                                    <asp:Label ID="castedepcount" runat="server" Text='<%# Eval("count")%>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="65%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnedit" runat="server" CommandName="Select" Text="<i class='fas fa-edit' aria-hidden='true'></i>" />
                                                </ItemTemplate>
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btndelete" runat="server" CommandName="Delete" Text="<i class='fas fa-trash' aria-hidden='true'></i>" />
                                                </ItemTemplate>
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <br />
                                    <br />
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <div class="modal" id="modalsubcaste">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Sub Caste</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-md-4">
                                    <asp:DropDownList ID="ddlcastemodal" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlcastemodal_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtsubcaste" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                                    <asp:Label ID="lblsubcaste" runat="server" Text="" Visible="false"></asp:Label>
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="btnaddsub" runat="server" Text="Save" CssClass="btn btn-success btn-block" OnClick="btnaddsub_Click" />
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="btnclear3" runat="server" Text="Clear" CssClass="btn btn-success btn-block" OnClick="btnclear3_Click" />
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12" style="overflow-y: auto; height: 350px">
                                    <asp:GridView ID="grdsubcaste" runat="server" Font-Size="12pt"
                                        Style="text-align: center;" AutoGenerateColumns="False" CssClass="table table-hover table-bordered" OnSelectedIndexChanged="grdsubcaste_SelectedIndexChanged" OnRowDeleting="grdsubcaste_RowDeleting">
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <RowStyle HorizontalAlign="Center"></RowStyle>
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr No.">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                                <ItemStyle Width="5%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Subcaste">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblname" runat="server" Text='<%# Eval("subcast_name")%>'></asp:Label>
                                                    <asp:Label ID="lblcastidref" runat="server" Text='<%# Eval("cast_id")%>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lblid" runat="server" Text='<%# Eval("subcast_id")%>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lblflag" runat="server" Text='<%# Eval("flag")%>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="65%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnedit" runat="server" CommandName="Select" Text="<i class='fas fa-edit' aria-hidden='true'></i>" />
                                                </ItemTemplate>
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btndelete" runat="server" CommandName="Delete" Text="<i class='fas fa-trash' aria-hidden='true'></i>" />
                                                </ItemTemplate>
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <br />
                                    <br />
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>


    <script src="js/moment/moment.min.js"></script>

    <script>
        $(function () {
            $("#<%=txtper.ClientID %>").keypress(function () {
                var test11 = $("#<%=txtper.ClientID %>").val();
                if (test11.length == 2) {
                    test11 = test11 + ".";
                    $("#<%=txtper.ClientID %>").val(test11);
                }
            });
        });
    </script>
    <script>
        function nametxt(e) {
            isIE = document.all ? 1 : 0
            keyEntry = !isIE ? e.which : event.keyCode;
            if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry == '32'))
                return true;
            else {
                return false;
            }
        }
        function villagecitytxtbxx(e) {
            isIE = document.all ? 1 : 0
            keyEntry = !isIE ? e.which : event.keyCode;
            if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry == '32') || ((keyEntry >= '40') && (keyEntry <= '41')))
                return true;
            else {
                return false;
            }
        }


    </script>
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
    <script lang="javascript" type="text/javascript">
        function CheckNumericdecimal(e) {

            if (window.event) // IE
            {
                if ((e.keyCode < 48 || e.keyCode > 57) & e.keyCode != 8 & e.keyCode != 46) {
                    event.returnValue = false;
                    return false;
                }
            }
            else { // Fire Fox
                if ((e.which < 48 || e.which > 57) & e.which != 8 & e.which != 46) {
                    e.preventDefault();
                    return false;
                }
            }
        }
    </script>
    <script>
        function age(e) {
            isIE = document.all ? 1 : 0
            keyEntry = !isIE ? e.which : event.keyCode;
            if ((keyEntry == '39'))
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
        function surname(e) {
            isIE = document.all ? 1 : 0
            keyEntry = !isIE ? e.which : event.keyCode;
            if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry == '39') || (keyEntry == '45') || (keyEntry == '32'))
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
            if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry == '32'))
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
            if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '48') && (keyEntry <= '57')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry == '39') || (keyEntry == '32') || (keyEntry == '46') || (keyEntry == '44') || (keyEntry == '47') || (keyEntry == '45') || (keyEntry == '40') || (keyEntry == '41'))
                return true;
            else {
                return false;
            }
        }
    </script>
    <script>
        function grade(e) {
            isIE = document.all ? 1 : 0
            keyEntry = !isIE ? e.which : event.keyCode;
            if (((keyEntry >= '65') && (keyEntry <= '69')) || ((keyEntry >= '97') && (keyEntry <= '102')))
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
                    readURL(this, 'stud_photo');
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
                    readURL(this, 'stud_sign');
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
        $("[id*=btntoggle]").click(function () {
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
            var id = $("[id*=txtform_id]").val();
            if (id != '') {
                $("[id*=stud_photo]").attr("src", $("[id*=lblphoto]")[0].innerHTML);
                $("[id*=stud_sign]").attr("src", $("[id*=lblsign]")[0].innerHTML);

            }
            else {
                $("[id*=stud_photo]").attr("src", "image/user.png");
                $("[id*=stud_sign]").attr("src", "image/sign.png");
                $("[id*=imagediv]").show();
                $("[id*=signdiv]").hide();
                $("[id*=btntoggle]").text('Signature');
            }
        }
    </script>
    <script>
        $('.datepickerdob').datepicker({
            format: "dd/mm/yyyy"
        });
    </script>
    <script>
        $('.datepickeradmdate').datepicker({
            format: "dd/mm/yyyy"
        });
    </script>
    <script>
        $(function () {
            $('[data-toggle="tooltip"]').hover();
        });
    </script>
    <script type="text/javascript">
        function openModal(name) {
            $("[id*=" + name + "]").modal('show');
            $("[id*=" + name + "]").data('bs.modal')._config.backdrop = 'static';
            $("[id*=" + name + "]").data('bs.modal')._config.keyboard = false;
        }
    </script>
</asp:Content>


