<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Bonafide_Certificate.aspx.cs" Inherits="Bonafide_Certificate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="jquery/dist/jquery.min.js"></script>

    <script src="notify-master/js/notify.js"></script>
    <link href="notify-master/css/notify.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function openModal() {
            $('#modal1').modal('show');
        }
        //function opentypemodal() {
        //    $('#printmodal').modal('show');
        //}
        function ValidationForm() {
            var b = $("[id*=txtissue]").val();
            var c = parseInt(b) + 1;
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            confirm_value.value = "";
            var stud_id = $("[id*=txtstud_id]").val();;
            if (confirm("You Have Generated Bonafide " + c + " Time Do You Want To Continue?")) {
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
                    newWin = window.open('Bonafide_report_secondary.aspx');
                }

                else {
                    newWin = window.open('Bonafide_report_secondary_marathi.aspx');
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
    </script>
    <script>
        function ValidateForm() {
            var b = $("[id*=txtissue]").val();
            var c = parseInt(b) + 1;
            if (c > 5) {
                return;
            }
            else {
                if (!newWin || newWin.closed || typeof newWin.closed == 'undefined') {
                    $.notify("it looks like pop-up window has been blocked by browser please allow pop-up window from settings", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                    $("[id*=txtvalue]").val(b);
                    return;
                }
                else {
                    $("[id*=txtvalue]").val(c);
                }
            }
        }
    </script>

    <div class="card">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <div class="card-header text-white" style="background: linear-gradient(to right, #6f03bf 1%, #363695 100%) !important; border-radius: 7px">
                    <div class="row">
                        <div class="col-lg-8">
                            <h3>Bonafide Certificate</h3>
                        </div>
                        <div class="col-lg-2" style="text-align: right; margin-top: 9px">
                            <asp:Label ID="Label1" runat="server" Style="font-weight: bold" Text=" G.R.No/Student ID"></asp:Label>
                        </div>
                        <div class="col-lg-2" style="text-align: right">
                            <asp:TextBox runat="server" CssClass="form-control" Style="float: right; font-size: 15px; text-transform: uppercase;" AutoPostBack="true" ID="txtid" AutoComplete="off" OnTextChanged="txtid_TextChanged" MaxLength="8"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="card-body">
                    <div class="row">
                        <h4 class="three" style="margin-bottom: 12px; margin-top: 12px;"><span style="font-size: 20px;">Personal Details</span></h4>
                    </div>
                    <div class="row">
                        <div class="col-lg-3">
                            Full Name<br />
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtstudame" AutoComplete="off"></asp:TextBox>
                        </div>
                        <div class="col-lg-3">
                            Studied In<br />
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtstandard" AutoComplete="off" MaxLength="2"></asp:TextBox>
                        </div>
                        <div class="col-lg-3">
                            Bonafide no.<br />
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtbonafideno" AutoComplete="off"></asp:TextBox>
                        </div>
                        <div class="col-lg-3">
                            Date Of Birth<br />
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtdob" AutoComplete="off"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-3">
                            Cast<br />
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtcast" AutoComplete="off"></asp:TextBox>
                        </div>
                        <div class="col-lg-3">
                            Sub-Cast<br />
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtsubcast" AutoComplete="off"></asp:TextBox>
                        </div>
                        <div class="col-lg-3">
                            Saral id<br />
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtsaralno" AutoComplete="off"></asp:TextBox>
                        </div>
                        <div class="col-lg-3">
                            Aadhar No<br />
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtAadhar" AutoComplete="off"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-2">
                            <asp:CheckBox runat="server" ID="chkssc" OnCheckedChanged="chkssc_CheckedChanged" AutoPostBack="true" Text=" SSC exam in" />                        
                            <asp:DropDownList runat="server" CssClass="form-control" ID="ddlssc">
                                <asp:ListItem>--Select--</asp:ListItem>
                                <asp:ListItem>January</asp:ListItem>
                                <asp:ListItem>February</asp:ListItem>
                                <asp:ListItem>March</asp:ListItem>
                                <asp:ListItem>April</asp:ListItem>
                                <asp:ListItem>May</asp:ListItem>
                                <asp:ListItem>June</asp:ListItem>
                                <asp:ListItem>July</asp:ListItem>
                                <asp:ListItem>August</asp:ListItem>
                                <asp:ListItem>September</asp:ListItem>
                                <asp:ListItem>October</asp:ListItem>
                                <asp:ListItem>November</asp:ListItem>
                                <asp:ListItem>December</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-2">
                            <br />
                            <asp:DropDownList runat="server" AutoPostBack="true" ID="ddlssc1" CssClass="form-control" style="margin-top: 8px;"></asp:DropDownList>
                        </div>
                        <div class="col-lg-2">
                            <asp:CheckBox runat="server" ID="chkotherstandard" OnCheckedChanged="chkotherstandard_CheckedChanged" AutoPostBack="true" Text=" Other Standard"/>
                          <br />
                            <asp:DropDownList runat="server" AutoPostBack="true" ID="ddlotherstd" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-lg-2">
                            Gender<br />
                            <asp:TextBox runat="server" ID="txtgender" CssClass="form-control" style="margin-top: 8px;"></asp:TextBox>
                        </div>
                          <div class="col-lg-2">
                            Remark<br />
                            <asp:TextBox runat="server" ID="txtremk" CssClass="form-control" style="margin-top: 8px;" Enabled="false"></asp:TextBox>
                        </div>
                        <div class="col-lg-1">
                            <br />
                            <asp:TextBox runat="server" CssClass="form-control" ID="standardid" AutoComplete="off" Visible="false"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row " style="margin-top: 30px">
                         <div class="col-md-3">
                            <input type="text" runat="server" id="txtstud_id" style="display: none" /> 
                        </div>
                        <input type="text" runat="server" id="txtissue" style="display: none" />
                        <div class="col-lg-2">
                            <asp:Button ID="btns" runat="server" Text="Save" CssClass="btn btn-success form-control" OnClick="btns_Click" />
                        </div>
                        <div class="col-lg-2">
                            <asp:Button ID="btnc" runat="server" CssClass="btn btn-success form-control" Text="Cancel" OnClick="btnc_Click" />
                        </div>
                        <div class="col-lg-2">
                            <asp:Button ID="btn_print" runat="server" CssClass="btn btn-success form-control" Text="Bonafide Print"   OnClientClick="return ValidationForm();" OnClick="btnissue_Click"/>
                        </div>
                        <div class="col-lg-3">
                            <input type="text" runat="server" id="txtvalue" style="display: none" />
                        </div>
                    </div>
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
                                    <asp:GridView ID="grid2" runat="server" CssClass="table table-bordered table-hover table-striped" OnRowCommand="grid2_RowCommand" AutoGenerateColumns="False" OnRowCreated="grid2_RowCreated">
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <RowStyle HorizontalAlign="Center"></RowStyle>
                                        <Columns>

                                            <asp:BoundField DataField="student_id" HeaderText="Student ID" />
                                            <asp:BoundField DataField="gr_no" HeaderText="GR Number" />
                                            <asp:BoundField DataField="Stud_Name" HeaderText="Student Name" />
                                            <asp:BoundField DataField="class" HeaderText="Class" />
                                            <asp:BoundField DataField="class_id" HeaderText="Class ID" />
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

<%--        <div class="modal" id="printmodal" role="dialog">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Print Bonafide</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div class="modal-body">

                                <div class="row" style="font-size: 15px; text-align: center">
                                    <div class="col-md-4">
                                        <span><b>Student ID :</b></span><br />
                                        <asp:Label ID="lblstud_id" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <b>Section</b>
                                        <asp:DropDownList CssClass="form-control" ID="ddl_section" runat="server">
                                            <asp:ListItem>PREPRIMARY</asp:ListItem>
                                            <asp:ListItem>PRIMARY</asp:ListItem>
                                            <asp:ListItem>SECONDARY</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-4">
                                        <b></b>
                                        <asp:Button ID="btnissue" runat="server" CssClass="btn btn-success form-control" Text="Bonafide Print" OnClientClick="return ValidateForm();" OnClick="btnissue_Click" />
                                    </div>
                                </div>
                                <br />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>--%>
    </div>
    <script>
        $(document).on('keypress', '#<%= txtid.ClientID %>', function (event) {
            var regex = new RegExp("^[a-zA-Z0-9' ]");
            var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
            if (!regex.test(key)) {
                event.preventDefault();
                return false;
            }
        });
        $(document).on('keypress', '#<%= txtstandard.ClientID %>', function (event) {
            var regex = new RegExp("^[0-9]");
            var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
            if (!regex.test(key)) {
                event.preventDefault();
                return false;
            }
        });
    </script>
</asp:Content>

