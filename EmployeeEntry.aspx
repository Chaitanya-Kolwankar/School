<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EmployeeEntry.aspx.cs" Inherits="EmployeeEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/jquery.datetimepicker.css" rel="stylesheet" />
    <script src="jquery/dist/jquery.min.js"></script>

    <script src="notify-master/js/notify.js"></script>


    <link rel="stylesheet" href="css/bootstrap-multiselect.css" type="text/css" />
    <script type="text/javascript" src="js/bootstrap-multiselect.js"></script>
    <link href="notify-master/css/notify.css" rel="stylesheet" />
    <style>
        .img-circle {
            border-radius: 50%;
        }
        .required {
            color: red;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<div class="container-fluid">--%>
    <div class="card">
        <div class="card-header text-white" style="background: linear-gradient(to right, #6f03bf 1%, #363695 100%) !important; border-radius: 7px">
            <h3><b>Employee Master</b></h3>
        </div>
        <div class="card-body">
            <div class="row">
                <%--col 1 start--%>
                <div class="col-md-10">
                    <%--1 st row start--%>
                    <div class="row">
                        <div class="col-md-2 ">
                            <b>Employee ID</b>
                            <input type="text" id="txtempID" class="form-control" disabled="disabled" style="text-transform:uppercase"/>
                        </div>
                        <div class="col-md-2">
                            <b>Last Name</b>
                            <input type="text" id="txtlname" class="form-control" onkeypress="return alphaname(event);" style="text-transform:uppercase"  />
                        </div>
                        <div class="col-md-2">
                            <b>First Name<span class="required">*</span></b>
                            <input type="text" id="txtfname" class="form-control" onkeypress="return alphaname(event);"  style="text-transform:uppercase" />
                        </div>
                        <div class="col-md-2">
                            <b>Middle Name<span class="required">*</span></b>
                            <input type="text" id="txtmname" class="form-control" onkeypress="return alphaname(event);" style="text-transform:uppercase"  />
                        </div>
                        <div class="col-md-2 ">
                            <b>Mother Name<span class="required">*</span></b>
                            <input type="text" id="txtmothern" class="form-control" onkeypress="return alphaname(event);" style="text-transform:uppercase"  />
                        </div>
                        <div class="col-md-2">
                            <b>Gender <span class="required">*</span></b>
                            <asp:DropDownList ID="ddl_gender" runat="server" CssClass="form-control">
                                <asp:ListItem>--Select--</asp:ListItem>
                                <asp:ListItem>Male</asp:ListItem>
                                <asp:ListItem>Female</asp:ListItem>
                                <asp:ListItem>Transgender</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <%--1 st row end--%>
                    <%--2 nd row start--%>
                    <div class="row">
                        <br />
                        <div class="col-md-2 ">
                            <b>Date Of Birth<span class="required">*</span></b>
                            <input type="text" id="dobdate" placeholder="dd/mm/yyyy" autocomplete="off" class="form-control" name="date" readonly="true" />
                        </div>
                        <div class="col-md-2 ">
                            <b>Date Of Joining<span class="required">*</span></b>
                            <input type="text" id="datedoj" placeholder="dd/mm/yyyy" autocomplete="off" class="form-control" name="date" readonly="true" />
                        </div>


                        <div class="col-md-2 ">
                            <b>Email ID</b>
                            <input type="text" id="txtemail" class="form-control" onchange="validateEmail(this);"  maxlength="50"/>
                        </div>

                        <div class="col-md-2 ">
                            <b>Mobile No.<span class="required">*</span></b>
                            <input type="text" id="txtmobile" class="form-control" onkeypress="return isNumber(event)" maxlength="10" />
                        </div>
                        <div class="col-md-2 ">
                            <b>Phone No.</b>
                            <input type="text" id="txtphno" class="form-control" onkeypress="return isNumber(event)" maxlength="15" />
                        </div>

                        <div class="col-md-2">
                            <b>Annual Salary</b>
                            <input type="text" id="txtsalary" class="form-control" onkeypress="return isNumber(event)" maxlength="7"/>
                        </div>

                    </div>
                    <%--2nd row end--%>
                    <%--3rd row start--%>
                    <div class="row">
                        <br />
                        <div class="col-md-4">
                            <b>Address <span class="required">*</span></b>
                            <textarea id="txtaddress" class="form-control" onkeypress="return address(event)"></textarea>
                        </div>
                        <div class="col-md-2">
                            <b>Pin Code <span class="required">*</span></b>
                            <input type="text" id="txtpincode" class="form-control" onkeypress="return isNumber(event)" maxlength="6" />
                        </div>
                        <div class="col-md-2">
                            <b>State <span class="required">*</span></b>
                            <asp:DropDownList ID="ddl_state" CssClass="form-control" runat="server">
                                <asp:ListItem>--SELECT--</asp:ListItem>
                                <asp:ListItem>MAHARASHTRA</asp:ListItem>
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

                        <div class="col-md-2">
                            <b>Blood Group</b>
                            <asp:DropDownList ID="ddlBloodGroup" CssClass="form-control" runat="server">
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
                        <div class="col-md-2">
                            <b>Qualification</b>
                            <input type="text" id="txt_quali" class="form-control" maxlength="50"/>
                        </div>
                    </div>
                    <%--3rd row end--%>

                    <%--<div class="card-body">--%>
                    <%--4th row start--%>
                    <div class="row">
                        <div class="col-md-5">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="row">
                                        <div class="col-md-10">
                                            <b>Department<span class="required">*</span></b>
                                            <select id="ddldept" class="form-control"></select>
                                        </div>
                                        <div class="col-md-2">
                                            <br />
                                            <a id="btnadddep" class="btn btn-sm btn-success" style="background-color: #35B978; color: white" data-toggle="modal" data-target="#departmodal">+</a>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <div class="row">
                                        <div class="col-md-10">
                                            <b>Designation<span class="required">*</span></b>
                                            <select id="ddldesig" class="form-control"></select>
                                        </div>
                                        <div class="col-md-2">
                                            <br />
                                            <a id="btnadddes" class="btn btn-sm btn-success" style="background-color: #35B978; color: white" data-toggle="modal" data-target="#desigmodal">+</a>
                                        </div>
                                    </div>
                                </div>


                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="row">
                                <div class="col-md-12">
                                    <b>Role<span class="required">*</span></b>
                                    <select id="ddlRoles" class="form-control"></select>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-5">
                            <div class="row">
                                <div class="col-md-6">
                                    <b>Module Name</b><br />
                                    <select class="select2_multiple form-control" id="li_name" style="width: 100%" multiple="multiple"></select>
                                </div>

                                <div class="col-md-6">
                                    <b>Form Name</b>
                                    <br />
                                    <select class="select2_multiple form-control" id="lst_form" style="height: 172px;" multiple="multiple"></select>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%--4 th row end--%>
                </div>
                <%--col 1 end--%>
                <%--col 2 start--%>
                <div class="col-md-2">
                    <div class="row" style="text-align: center;">
                        <div class="col-md-12">
                            <div class="panel panel-info" style="padding-left: 5px; padding-right: 5px; padding-top: 10px; padding-bottom: 7px; border: 1px solid; border-top-right-radius: 15px; border-bottom-left-radius: 15px; height: 244px">
                                <div class="row" style="height: 180px">
                                    <div class="col-md-12" id="imagediv" runat="server">
                                        <asp:Image runat="server" draggable="false" alt="Photo" class="img-fluid img-circle" ID="stud_photo" Style="height: 130px; width: 130px; margin-bottom: 7px;" />
                                        <asp:Label ID="lblphoto" runat="server" Style="display: none"></asp:Label>
                                        <%--<asp:FileUpload ID="get_photo" runat="server" CssClass="form-control" />--%>
                                        <input type="file" id="get_photo" runat="server" class="form-control" />
                                    </div>
                                    <div class="col-md-12" id="signdiv" runat="server" style="display: none;">
                                        <asp:Image runat="server" draggable="false" alt="Photo" class="img-fluid img-thumbnail" ID="stud_sign" Style="height: 55px; width: 130px; margin-top: 50px;" />
                                        <asp:Label ID="lblsign" runat="server" Style="display: none"></asp:Label>
                                        <br />
                                        <%--<asp:FileUpload runat="server" ID="get_sign" CssClass="form-control" Style="margin-top: 32px;" />--%>
                                        <input type="file" id="get_sign" runat="server" class="form-control" style="margin-top: 32px;" />
                                    </div>
                                </div>
                                <div class="row" style="padding-top: 4px;">
                                    <div class="col-md-6"><a id="btntoggle" class="btn btn-fluid btn-info" style="color: white; display: block;"><span>Signature</span></a></div>
                                    <div class="col-md-6"><a id="btnClearPhotos" class="btn btn-info form-control" style="color: white; display: block;"><i class="fas fa-eraser"></i>&nbsp<span>Clear</span></a></div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                </div>
                <%--col 2 end--%>
            </div>
            <%--</div>--%>
            <%--5 th row start--%>
            <div class="row" style="padding-top: 50px;">
                <br />
                <div class="col-md-3">
                </div>
                <div class="col-md-2">
                    <a id="btnmodify" class="btn btn-block btn-success" style="background-color: #35B978; color: white">Modify</a>
                </div>
                <div class="col-md-2">
                    <a id="btnsave" class="btn btn-block btn-success" style="background-color: #35B978; color: white">Save</a>
                </div>
                <div class="col-md-2">
                    <a id="btnRefresh" class="btn btn-block btn-success" style="background-color: #35B978; color: white">Refresh</a>
                </div>
                <div class="col-md-3">
                </div>

            </div>
            <%--5 th row end--%>
        </div>
        <br />
    </div>
    <%--</div>--%>

    <div id="departmodal" class="modal fade" data-backdrop="static">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header" style="background-color: #0078BC;">
                    <h4 class="modal-title" style="color: aliceblue;">Add Department</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-4">
                            <b>Department Name</b>
                            <input type="text" id="txtdept" class="form-control" onkeypress="return address(event)"  style="text-transform:uppercase" maxlength="30"/>
                            <input type="text" id="txtdeparttid" class="form-control" style="display:none"/>
                        </div>
                        <div class="col-md-4">
                            <b>Department Prefix</b>
                            <input type="text" id="txtprefix" class="form-control" maxlength="4"  onkeypress="return alpha(event)" style="text-transform:uppercase" />
                        </div>
                        <div class="col-md-2" style="padding-top: 20px;">
                            <a href="#" id="btndepsave" class="btn btn-block btn-success form-control">Save</a>
                        </div>
                        <div class="col-md-2" style="padding-top: 20px;">
                            <a href="#" id="btndepref" class="btn btn-block btn-success  form-control">Refresh</a>
                        </div>
                    </div>
                    <div class="row" id="tblgrid" style="padding-top:15px;display:none;">
                        <div class="col-md-12">
                            <table id="tbldepfill" class="table table-container table-bordered">
                            </table>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>

    <div id="desigmodal" class="modal fade" data-backdrop="static">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header" style="background-color: #0078BC;">
                    <h4 class="modal-title" style="color: aliceblue;">Add Designation</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-4">
                            <b>Designation Name</b>
                            <input type="text" id="txtdesig" class="form-control" onkeypress="return address(event)"  style="text-transform:uppercase" maxlength="30"/>
                            <input type="text" id="txtdeid" class="form-control" style="display:none" />
                        </div>
                        <div class="col-md-2" style="padding-top:20px">
                       
                            <a href="#" id="btndesave" class="btn btn-block btn-success" >Save</a>
                        </div>
                        <div class="col-md-2" style="padding-top:20px">
                            <a href="#" id="btndesref" class="btn btn-block btn-success" >Refresh</a>
                        </div>
                    </div>
                    <div class="row" id="desigtbl" style="padding-top: 15px;display:none;">
                        <div class="col-md-12">
                            <table id="tbldesig" class="table table-container table-bordered">
                            </table>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>

    <script src="bootstrap-datepicker-1.9.0-dist/js/bootstrap-datepicker.min.js"></script>
       <script src="js/jquery.datetimepicker.js"></script>

    <script src="jsForms/EmployeeReg.js"></script>
    <script type="text/javascript">
        var employeeId = '<%=Session["emp_id"] %>';
    </script>

    <script type="text/javascript">
        //On Page Load
        $(document).ready(function () {
            $('[id*=lst_form]').multiselect({
                includeSelectAllOption: true
            });
            $('[id*=li_name]').multiselect({
                includeSelectAllOption: true
            });
        });



        //On UpdatePanel Refresh
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $('[id*=lst_form]').multiselect({
                        includeSelectAllOption: true
                    });

                }
            });
        };
    </script>



    <script>
        $(document).ready(function () {
            $('.multiselect-container').css('height', '250px');
            $('.multiselect').css('width', '100%');
            $('.btn-group').css('width', '100%');
            $('.multiselect-container').addClass('table-responsive');
        });


    </script>

    <script>
         $(function () {
            $('input[name="date"]').datetimepicker({
                changeMonth: false,
                changeYear: false,
                timepicker: false,
                format: 'd/m/Y',
                viewMode: "months",
                minViewMode: "months"
            });
        });
        </script>

    <script>
        function ValidateAlpha(evt) {
            var keyCode = (evt.which) ? evt.which : evt.keyCode
            if ((keyCode < 65 || keyCode > 90) && (keyCode < 97 || keyCode > 122) &&  keyCode != 34) {
                return false;
            } else {
                return true;
            }
        }
        function address(evt) {
            var keyCode = (evt.which) ? evt.which : evt.keyCode
            if ((keyCode < 65 || keyCode > 90) && (keyCode < 97 || keyCode > 122) &&  (keyCode < 48 || keyCode > 57) && (keyCode != 32) && (keyCode != 45) && (keyCode != 47) && (keyCode != 44)) {
                return false;
            } else {
                return true;
            }
        }
        function alpha(evt) {
            var keyCode = (evt.which) ? evt.which : evt.keyCode
            if ((keyCode < 65 || keyCode > 90) && (keyCode < 97 || keyCode > 122)) {
                return false;
            } else {
                return true;
            }
        }
        function alphaname(evt) {
            var keyCode = (evt.which) ? evt.which : evt.keyCode
            if ((keyCode < 65 || keyCode > 90) && (keyCode < 97 || keyCode > 122) && (keyCode!=39)) {
                return false;
            } else {
                return true;
            }
        }
        function validateEmail(emailField) {
            var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;

            if (reg.test(emailField.value) == false && emailField.value!="") {
                $.notify("Please Enter a Valid Email ID.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                return false;
            }

            return true;
        }
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }

            return true;
        }
    </script>
</asp:Content>

