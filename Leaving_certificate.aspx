<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Leaving_certificate.aspx.cs" Inherits="Leaving_certificate" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/jquery.datetimepicker.css" rel="stylesheet" />
    <script src="jquery/dist/jquery.min.js"></script>
    <script src="bootstrap-4.0.0-dist/js/bootstrap.min.js"></script>
    <script src="notify-master/js/notify.js"></script>
    <link href="notify-master/css/notify.css" rel="stylesheet" />


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function openModal() {
            $('#modal1').modal('show');

        }
    </script>
    <%--    <script>
        $(document).ready(function () {
            $('#btndelreasn').click(function () {
                BINDGVREASON();
            });
        });
    </script>--%>

    <script type="text/javascript">
        function openModal1() {
            $('#departmodal1').modal('show');


        }
    </script>

    <script type="text/javascript">
        function openModal1() {
            $('#modal_dltRemark').modal('show');


        }
    </script>




    <script>
        function ValidateForm() {
            var b = $("[id*=txtissue]").val();
            var c = parseInt(b) + 1;
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            confirm_value.value = "";
            var stud_id = $("[id*=txtstud_id]").val();;
            if (confirm("You Have Generated Lc " + c + " Time Do You Want To Continue?")) {
                confirm_value.value = "Yes";
            }
            else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
            var abc = confirm_value.value;
            var newWin = "";
            if (confirm_value.value == "Yes") {
                $("[id*=txtvalue]").val(c);
                if (stud_id.includes("E") == true) {
                    newWin = window.open('lc_report.aspx');
                }

                else {
                    newWin = window.open('lc_report_marathi.aspx');
                }


                if (!newWin || newWin.closed || typeof newWin.closed == 'undefined') {
                    $.notify("it looks like pop-up window has been blocked by browser please allow pop-up window from settings", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                    $("[id*=txtvalue]").val(b);
                    return;
                }
                else {

                }
            }
            else {
                $("#btnclose").click;
            }

        }
        function modalclose() {
            var abc = "";
            $("[id*=txtid]").val(abc);
            $("[id*=txtid]")[0].defaultValue = "";
            $("#btnc").click;
        }
    </script>
    <div class="card">
        <asp:UpdatePanel runat="server" UpdateMode="Always">
            <ContentTemplate>
                <div class="card-header text-white" style="background:linear-gradient(to right, #6f03bf 1%, #363695 100%) !important; border-radius: 7px">
                    <div class="row">
                        <div class="col-md-8">
                            <h3>Leaving Certificate</h3>
                        </div>
                        <div class="col-md-2" style="text-align: right; margin-top: 9px">
                            <asp:Label runat="server" Style="font-weight: bold" Text=" G.R.No/Student ID"></asp:Label>
                        </div>
                        <div class="col-md-2" style="text-align: right">
                            <asp:TextBox runat="server" AutoPostBack="true" CssClass="form-control" Style="float: right; font-size: 15px; text-transform: uppercase;" ID="txtid" AutoComplete="off" TabIndex="1" OnTextChanged="txtid_TextChanged" MaxLength="8"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="card-body">
                    <div class="col-md-12" id="div1">
                        <h3 class="three" style="margin-bottom: 12px; margin-top: 12px;"><span style="font-size: 20px;">Personal Details</span></h3>
                        <div class="row">
                            <div class="col-md-2">
                                LC NO<br />
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtlid" AutoComplete="off"> </asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                Surname<br />
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtsname" AutoComplete="off"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                First Name<br />
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtfname" AutoComplete="off"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                Middle Name<br />
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtmname" AutoComplete="off"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                Mother Name<br />
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtmoname" AutoComplete="off"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                Aadhar Number<br />
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtadhar" AutoComplete="off"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                Nationality<br />
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtn" AutoComplete="off"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                Mother Tongue<br />
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtmt" AutoComplete="off"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                Date Of Birth<br />
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtdob" AutoComplete="off"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                Place Of Birth<br />
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtpob" AutoComplete="off"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                Taluka<br />
                                <asp:TextBox runat="server" CssClass="form-control" ID="txttal" AutoComplete="off"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                District<br />
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtdis" AutoComplete="off"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                State<br />
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtstate" AutoComplete="off"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                Saral Number<br />
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtsaral" AutoComplete="off"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                Cast<br />
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtcast" AutoComplete="off"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                Sub Cast<br />
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtscast" AutoComplete="off"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                Seat Number<br />
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtseat" AutoComplete="off" MaxLength="10" TabIndex="2"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div id="div" class="col-md-12">
                        <h3 class="three" style="margin-bottom: 12px; margin-top: 12px;"><span style="font-size: 20px;">School Details</h3>
                        <div class="row">
                            <div class="col-md-2">
                                Last School Name<br />
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtschool" AutoComplete="off" MaxLength="50" Style="text-transform: uppercase;"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                Date Of Admission<br />
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtboa" AutoComplete="off"></asp:TextBox>
                            </div>

                            <div class="col-md-2">
                                Standard<span class="required">*</span><br />
                                <asp:UpdatePanel runat="server" ID="updtpnl11">
                                    <ContentTemplate>
                                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlstandard" AutoPostBack="true" selected="true">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>


                            <div class="col-md-4">
                                Standard in which studing and since when<span class="required">*</span>
                                <div class="row">
                                    <div class="col-md-6">
                                        <asp:TextBox runat="server" CssClass="form-control" AutoPostBack="true" ID="txt1" Style="text-transform: uppercase;" AutoComplete="off" TabIndex="4" placeholder="In Numbers" OnTextChanged="txt1_TextChanged" MaxLength="60"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtstandard" AutoComplete="off" TabIndex="3" Style="text-transform: uppercase;" placeholder="In Words" OnTextChanged="txtstandard_TextChanged" MaxLength="60"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2">
                                Date Of Leaving School<span class="required">*</span><br />
                                <asp:TextBox ID="txtls" runat="server" class="todate form-control" AutoComplete="off" TabIndex="5" MaxLength="10"></asp:TextBox>
                                <input type="text" runat="server" id="txtissue" style="display: none" />
                            </div>

                        </div>
                        <div class="row">

                            <div class="col-md-2">
                                Conduct<span class="required">*</span><br />
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlconduct" AutoPostBack="true">
                                            <asp:ListItem>--Select--</asp:ListItem>
                                            <asp:ListItem>Excellent</asp:ListItem>
                                            <asp:ListItem>Very Good</asp:ListItem>
                                            <asp:ListItem>Good</asp:ListItem>
                                            <asp:ListItem>Satisfactory</asp:ListItem>
                                            <asp:ListItem>उत्तम</asp:ListItem>
                                            <asp:ListItem>चांगली</asp:ListItem>
                                            <asp:ListItem>समाधानकारक</asp:ListItem>
                                            <asp:ListItem>बरी</asp:ListItem>
                                            <asp:ListItem>असमाधानकारक</asp:ListItem>
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-md-2">
                                Progress<span class="required">*</span><br />
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlprog" AutoPostBack="true">
                                            <asp:ListItem>--Select--</asp:ListItem>
                                            <asp:ListItem>Excellent</asp:ListItem>
                                            <asp:ListItem>Very Good</asp:ListItem>
                                            <asp:ListItem>Good</asp:ListItem>
                                            <asp:ListItem>Satisfactory</asp:ListItem>
                                             <asp:ListItem>Unsatisfactory</asp:ListItem>
                                            <asp:ListItem>उत्तम</asp:ListItem>
                                            <asp:ListItem>चांगली</asp:ListItem>
                                            <asp:ListItem>समाधानकारक</asp:ListItem>
                                            <asp:ListItem>बरी</asp:ListItem>
                                            <asp:ListItem>असमाधानकारक</asp:ListItem>
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-md-2">
                                <%--<div class="row">--%>
                                <%--<div class="col-md-6">--%>
                                        Reason<span class="required">*</span><br />

                                <asp:DropDownList runat="server" CssClass="form-control" ID="ddlreason"></asp:DropDownList>

                                <%-- <input type="text" runat="server" id="txtreason" class="form-control"/> --%>
                                <%--<asp:DropDownList runat="server" CssClass="form-control" ID="ddlreason" OnSelectedIndexChanged="ddlreason_SelectedIndexChanged"></asp:DropDownList>--%>
                                <%-- </div>--%>

                                <%-- </div>--%>
                            </div>
                            <div class="col-md-1" runat="server">
                                <div class="row">
                                    <div class="col-md-6" style="display: flex">
                                        <a id="btnadddep" class="btn btn-sm btn-success" style="color: white; margin-top: 23px; padding: 8px;" data-toggle="modal" data-target="#departmodal" onclick="">+</a>
                                    </div>
                                    <div clas="col-md-6">
                                        <button type="button" class="btn btn-sm btn-success" data-toggle="modal" data-target="#departmodal1" id="btndelreasn" style="margin-top: 23px; padding: 8px;">
                                            -
                                        </button>
                                    </div>
                                </div>
                            </div>




                            <div class="col-md-2">
                                Remark<span class="required">*</span><br />
                                <asp:DropDownList runat="server" CssClass="form-control" ID="ddlremk"></asp:DropDownList>
                            </div>
                            <div class="col-md-1">

                                <div class="row">
                                    <div class="col-md-4" runat="server">
                                        <br />
                                        <a id="btn_remark" class="btn btn-sm btn-success" style="color: white; padding: 8px; margin-top: 1px;" data-toggle="modal" data-target="#modal_Remark">+</a>
                                    </div>
                                    <div class="col-md-4" runat="server">
                                        <br />
                                        <a id="deltermk" class="btn btn-sm btn-success" style="color: white; padding: 8px; margin-top: 1px;" data-toggle="modal" data-target="#modal_dltRemark">-</a>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox runat="server" CssClass="form-control" ID="txttablename"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row ">
                        <div class="col-md-3">
                            <input type="text" runat="server" id="txtstud_id" style="display: none" />
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="btns" runat="server" Text="Save" CssClass="btn btn-success form-control" OnClick="btns_Click" UseSubmitBehavior="false" Visible="false" />
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="btnc" runat="server" CssClass="btn btn-success form-control" Text="Cancel" OnClick="btnc_Click" UseSubmitBehavior="false" Visible="false" />
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="btnissue" runat="server" CssClass="btn btn-success form-control" Text="LC Print" Visible="false" OnClick="btnissue_Click" OnClientClick="return ValidateForm();" />
                        </div>
                        <div class="col-md-3">
                            <input type="text" runat="server" id="txtvalue" style="display: none" />
                        </div>
                    </div>
                    <%--ReMark//////////////////////////////////////////////////////////////////////////////////////////////////////--%>

                    <div id="modal_Remark" class="modal fade" data-backdrop="false">
                        <div class="modal-dialog modal-md">
                            <div class="modal-content">
                                <div class="modal-header" style="background-color: #0078BC;">
                                    <h4 class="modal-title" style="color: aliceblue;">Add Remark</h4>
                                </div>
                                <div class="modal-body">
                                    <div class="container">
                                        <br />

                                        <div class="row">
                                            <div class="col-lg-12">
                                                <strong>Remark </strong>
                                                <asp:TextBox runat="server" ID="txt_Reason" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                            </div>

                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-lg-2"></div>
                                            <div class="col-lg-4">
                                                <asp:UpdatePanel runat="server">
                                                    <ContentTemplate>
                                                        <asp:Button ID="btn_addremark" runat="server" OnClick="btn_addremark_Click" CssClass="form-control btn btn-success" Text="Save" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>

                                            </div>
                                            <div class="col-lg-4">
                                                <%--  <button type="button" class="form-control btn btn-info " style="float: right; border-radius: 10px" data-dismiss="modal" >Close</button>--%>

                                                <asp:Button runat="server" CssClass="form-control btn btn-success" ID="modlclosebtn" Style="float: right; border-radius: 10px" data-dismiss="modal" Text="Close" />
                                            </div>
                                            <div class="col-lg-2"></div>
                                        </div>
                                        <br />
                                        <br />
                                    </div>


                                </div>

                            </div>
                        </div>
                    </div>
                    <div id="departmodal" class="modal fade" data-backdrop="false">
                        <div class="modal-dialog modal-md">
                            <div class="modal-content">
                                <div class="modal-header" style="background-color: #0078BC;">
                                    <h4 class="modal-title" style="color: aliceblue;">Add Reason</h4>
                                </div>
                                <div class="modal-body">
                                    <div class="container">
                                        <br />

                                        <div class="row">
                                            <div class="col-lg-12">
                                                <strong>Reason </strong>
                                                <asp:TextBox runat="server" ID="txt_reasn_name" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                            </div>

                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-lg-2"></div>
                                            <div class="col-lg-4">
                                                <asp:Button ID="btn_addreason" runat="server" OnClick="btn_addreason_Click" CssClass="form-control btn btn-success" Text="Save" />
                                            </div>
                                            <div class="col-lg-4">
                                                <button type="button" class="form-control btn btn-info " style="float: right; border-radius: 10px" data-dismiss="modal">Close</button>
                                            </div>
                                            <div class="col-lg-2"></div>
                                        </div>
                                        <br />
                                        <br />
                                    </div>


                                </div>

                            </div>
                        </div>
                    </div>

                    <asp:UpdatePanel runat="server" ID="updtpnl1">
                        <ContentTemplate>
                            <div id="departmodal1" class="modal fade" data-backdrop="false">
                                <div class="modal-dialog modal-md">
                                    <div class="modal-content">
                                        <div class="modal-header" style="background-color: #0078BC;">
                                            <h4 class="modal-title" style="color: aliceblue;">Remove Reason</h4>
                                        </div>
                                        <div class="modal-body">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <div class="container">
                                                        <br />
                                                        <div class="row">

                                                            <div class="col-sm-12" style="height: 300px; overflow-y: auto">
                                                                <asp:UpdatePanel runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:GridView ID="reasongridview" runat="server" CssClass="table table-bordered table-hover table-striped" AutoGenerateColumns="False" Style="width: 100%; text-align: center">
                                                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                                            <RowStyle HorizontalAlign="Center"></RowStyle>
                                                                            <Columns>
                                                                                <asp:TemplateField>
                                                                                    <HeaderTemplate>Select</HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox runat="server" ID="rsnchkbx" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <%--     <asp:BoundField DataField="Reason" HeaderText="Reason" id="gridrsn" />--%>
                                                                                <asp:TemplateField>
                                                                                    <HeaderTemplate>Reason</HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox runat="server" ID="gridrsn" Text='<% #Bind("Reason")%>' ReadOnly="true" Class="form-control"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>

                                                        </div>
                                                        <br />
                                                        <div class="row">
                                                            <div class="col-lg-2"></div>
                                                            <div class="col-lg-4">
                                                                <asp:Button ID="btnrsndlt" runat="server" OnClick="btnrsndlt_Click" CssClass="form-control btn btn-success" Text="Delete" />
                                                            </div>
                                                            <div class="col-lg-4">
                                                                <%--  <button type="button" class="form-control btn btn-info "  >Close</button>--%>
                                                                <asp:UpdatePanel runat="server">
                                                                    <ContentTemplate>
                                                                        <%-- <asp:Button  runat="server" OnClick="close_Click"  ID="close" />--%>

                                                                        <button id="close" runat="server" class="form-control btn btn-success" text="Close" data-dismiss="modal" style="float: right; border-radius: 10px" onclick="javascript:window.location.reload()">Close</button>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                            <div class="col-lg-2"></div>
                                                        </div>
                                                        <br />
                                                        <br />
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>

                                        </div>

                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>



                    <%--deleteremark--%>
                    <asp:UpdatePanel runat="server" ID="UpdatePanel5">
                        <ContentTemplate>
                            <div id="modal_dltRemark" class="modal fade" data-backdrop="false">
                                <div class="modal-dialog modal-md">
                                    <div class="modal-content">
                                        <div class="modal-header" style="background-color: #0078BC;">
                                            <h4 class="modal-title" style="color: aliceblue;">Remove Remark</h4>
                                        </div>
                                        <div class="modal-body">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <div class="container">
                                                        <br />
                                                        <div class="row">

                                                            <div class="col-sm-12" style="height: 300px; overflow-y: auto">
                                                                <asp:UpdatePanel runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:GridView ID="Griddltrmk" runat="server" CssClass="table table-bordered table-hover table-striped" AutoGenerateColumns="False" Style="width: 100%; text-align: center">
                                                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                                            <RowStyle HorizontalAlign="Center"></RowStyle>
                                                                            <Columns>
                                                                                <asp:TemplateField>
                                                                                    <HeaderTemplate>select </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox runat="server" ID="rmkchkbx" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <%--     <asp:BoundField DataField="Reason" HeaderText="Reason" id="gridrsn" />--%>
                                                                                <asp:TemplateField>
                                                                                    <HeaderTemplate>Remark </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox runat="server" ID="gridremark" Text='<% #Bind("Remark")%>' ReadOnly="true" Class="form-control"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>

                                                        </div>
                                                        <br />
                                                        <div class="row">
                                                            <div class="col-lg-2"></div>
                                                            <div class="col-lg-4">
                                                                <asp:Button ID="btndltrmk" runat="server" OnClick="btndltrmk_Click" CssClass="form-control btn btn-success" Text="Delete" />
                                                            </div>
                                                            <div class="col-lg-4">
                                                                <%--  <button type="button" class="form-control btn btn-info "  >Close</button>--%>
                                                                <asp:UpdatePanel runat="server">
                                                                    <ContentTemplate>
                                                                        <%-- <asp:Button  runat="server" OnClick="close_Click"  ID="close" />--%>

                                                                        <button id="btnclsrmk" runat="server" class="form-control btn btn-success" text="Close" data-dismiss="modal" style="float: right; border-radius: 10px" onclick="javascript:window.location.reload()">Close</button>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                            <div class="col-lg-2"></div>
                                                        </div>
                                                        <br />
                                                        <br />
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>

                                        </div>

                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <%--deleteremark--%>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="modal fade" id="modal1" role="dialog" data-backdrop="false">
            <div class="modal-dialog">
                <div class="modal-content" style="width: 704px">
                    <div>
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <asp:LinkButton ID="btnclose" CssClass="fa fa-window-close" OnClick="btnclose_Click" runat="server" Style="float: right; padding: 5px; font-size: 150%;" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <div class="modal-body">
                                <div id="div2" style="max-height: 500px; overflow-y: scroll; overflow: auto">
                                    <asp:GridView ID="grid2" runat="server" CssClass="table table-bordered table-hover table-striped" OnRowCommand="grid2_RowCommand" AutoGenerateColumns="False">
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <RowStyle HorizontalAlign="Center"></RowStyle>
                                        <Columns>
                                            <asp:BoundField DataField="Student_id" HeaderText="Student ID" />
                                            <asp:BoundField DataField="gr_no" HeaderText="GR Number" />
                                            <asp:BoundField DataField="Student_name" HeaderText="Student Name" />
                                            <asp:ButtonField ButtonType="link" runat="server" CommandName="select" ControlStyle-CssClass="fa fa-edit" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
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
        $(document).on('keypress', '#<%= txtstandard.ClientID %>', function (event) {
            var regex = new RegExp("^[a-zA-Z0-9,./- ]");
            var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
            if (!regex.test(key)) {
                event.preventDefault();
                return false;
            }
        });
        $(document).on('keypress', '#<%= txtls.ClientID %>', function (event) {
            var regex = new RegExp("^[0-9]");
            var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
            if (!regex.test(key)) {
                event.preventDefault();
                return false;
            }
        });
        $(document).on('keypress', '#<%= txt1.ClientID %>', function (event) {
            var regex = new RegExp("^[0-9a-zA-Z,.-/ ]");
            var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
            if (!regex.test(key)) {
                event.preventDefault();
                return false;
            }
        });
        $(document).on('keypress', '#<%= txtid.ClientID %>', function (event) {
            var regex = new RegExp("^[a-zA-Z0-9 ]");
            var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
            if (!regex.test(key)) {
                event.preventDefault();
                return false;
            }
        });
        $(document).on('keypress', '#<%= txtseat.ClientID %>', function (event) {
            var regex = new RegExp("^[a-zA-Z0-9 ]");
            var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
            if (!regex.test(key)) {
                event.preventDefault();
                return false;
            }
        });
        $(document).on('keypress', '#<%= txtschool.ClientID %>', function (event) {
            var regex = new RegExp("^[a-zA-Z0-9 ]");
            var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
            if (!regex.test(key)) {
                event.preventDefault();
                return false;
            }
        });
        $(document).on('keypress', '#<%= txtls.ClientID %>', function (event) {
            var regex = new RegExp("^[0-9 ]");
            var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
            if (!regex.test(key)) {
                event.preventDefault();
                return false;
            }
        });
        $(document).on('keypress', '#<%= txt_reasn_name.ClientID %>', function (event) {
            var regex = new RegExp("^[a-zA-Z0-9 ]");
            var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
            if (!regex.test(key)) {
                event.preventDefault();
                return false;
            }
        });
        $(document).on('keypress', '#<%= txt_Reason.ClientID %>', function (event) {
            var regex = new RegExp("^[a-zA-Z0-9 ]");
            var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
            if (!regex.test(key)) {
                event.preventDefault();
                return false;
            }
        });

    </script>
</asp:Content>

