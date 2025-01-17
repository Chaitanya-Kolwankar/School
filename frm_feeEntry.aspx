<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frm_feeEntry.aspx.cs" Inherits="frm_feeEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function loaddate() {
            $('[id*=txtpaydate]').datepicker(
                {
                    todayHighlight: true,
                    minDate: 0,
                    timepicker: true,
                    format: 'dd/mm/yyyy',
                    orientation: 'bottom'
                });
            $('[id*=txtchdate]').datepicker(
                {
                    todayHighlight: true,
                    minDate: 0,
                    timepicker: true,
                    format: 'dd/mm/yyyy',
                    orientation: 'bottom'
                });
        }
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            confirm_value.value = "";
            if (confirm("Do you want to delete the selected fee entry ?")) {
                confirm_value.value = "OK";
            }
            else {
                confirm_value.value = "Cancel";
            }
            document.forms[0].appendChild(confirm_value);
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid">
        <div class="card-header text-white" style="background: linear-gradient(to right, #6f03bf 1%, #363695 100%) !important; border-radius: 7px;">
            <h3>Fee Entry</h3>
        </div>
        <div class="card card-body">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-md-3">
                            <asp:TextBox ID="txtstud_id" runat="server" CssClass="form-control" placeholder="Student Id" style="text-transform:uppercase"></asp:TextBox>
                        </div>
                        <div class="col-md-1">
                            <asp:LinkButton ID="lnksearch" runat="server" CssClass="btn btn-success" OnClick="lnksearch_Click"><span class="fas fa-search"></span></asp:LinkButton>
                        </div>
                        <div class="col-md-1">
                            <asp:Button ID="btnrefresh" runat="server" Text="Refresh" CssClass="btn btn-success" OnClick="btnrefresh_Click" />
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="card card-body" style="border: 1px solid; border-radius: 7px; border-color: #0078bc;" runat="server" id="feepanel" visible="false">
                            <div class="row">
                                <div class="col-md-3">
                                    <b>NAME : </b>
                                    <asp:Label ID="lblname" runat="server"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <b>ACADEMIC YEAR : </b>
                                    <asp:Label ID="lblyear" runat="server"></asp:Label>
                                    <asp:Label ID="glbayid" runat="server" Visible="false"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <b>CATEGORY : </b>
                                    <asp:Label ID="lblcategory" runat="server"></asp:Label>
                                    <asp:Label ID="glbcat" runat="server" Visible="false"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <b>GR NO. : </b>
                                    <asp:Label ID="lblgr" runat="server"></asp:Label>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-3">
                                    <b>MEDIUM : </b>
                                    <asp:Label ID="lblmedium" runat="server"></asp:Label>
                                    <asp:Label ID="glbmed" runat="server" Visible="false"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <b>CLASS : </b>
                                    <asp:Label ID="lblclass" runat="server"></asp:Label>
                                    <asp:Label ID="glbclass" runat="server" Visible="false"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <b>DIVISION : </b>
                                    <asp:Label ID="lbldivision" runat="server"></asp:Label>
                                    <asp:Label ID="glbdiv" runat="server" Visible="false"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <b>ROLL NO. : </b>
                                    <asp:Label ID="lblroll" runat="server"></asp:Label>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-2">
                                    Fees Type
                    <asp:DropDownList ID="ddlpay" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlpay_SelectedIndexChanged">
                        <asp:ListItem>--Select--</asp:ListItem>
                        <asp:ListItem>All Fees</asp:ListItem>
                        <asp:ListItem>Refund Fees</asp:ListItem>
                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    Payment Type
                    <asp:DropDownList ID="ddltype" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddltype_SelectedIndexChanged" Enabled="false">
                    </asp:DropDownList>
                                </div>

                                <div class="col-md-2">
                                    Duration
                    <asp:DropDownList ID="ddlduration" runat="server" AutoPostBack="true" CssClass="form-control" Enabled="false" OnSelectedIndexChanged="ddlduration_SelectedIndexChanged">
                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    Payment Mode
                    <asp:DropDownList ID="ddlmode" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlmode_SelectedIndexChanged">
                        <asp:ListItem>--Select--</asp:ListItem>
                        <asp:ListItem>Cash</asp:ListItem>
                        <asp:ListItem>Cheque</asp:ListItem>
                        <asp:ListItem>NEFT</asp:ListItem>
                        <asp:ListItem>DD</asp:ListItem>
                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    Pay Date (dd-mm-yyyy)
                                        <asp:TextBox ID="txtpaydate" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2" runat="server" id="recptno" visible="false">
                                    Receipt No.
                                        <asp:TextBox ID="txtreceiptno" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <br />
                            <div class="row" runat="server" id="details" visible="false">
                                <div class="col-md-3">
                                    Bank Name
                                    <asp:TextBox ID="txtbnkname" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    Branch Name
                                    <asp:TextBox ID="txtbranch" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <div class="row">
                                        <div class="col-md-4">
                                            Cheque/DD/NEFT Date (dd-mm-yyyy)
                                            <asp:TextBox ID="txtchdate" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col-md-4">
                                            Cheque/DD/NEFT No.
                                            <asp:TextBox ID="txtchno" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col-md-4" runat="server" id="status" visible="false">
                                            Cheque/NEFT Status
                                            <asp:DropDownList ID="ddlstatus" CssClass="form-control" runat="server">
                                                <asp:ListItem>--Select--</asp:ListItem>
                                                <asp:ListItem>Clear</asp:ListItem>
                                                <asp:ListItem>Pending</asp:ListItem>
                                                <asp:ListItem>Bounce</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row" runat="server" id="cal" style="padding-top: 15px; text-align: center;" visible="false">
                                <div class="col-md-1"></div>
                                <div class="col-md-2">
                                    <b>TOTAL FEES : </b>
                                    <asp:Label ID="lblall" runat="server"></asp:Label>
                                </div>
                                <div class="col-md-2">
                                    <b>FEES PAID : </b>
                                    <asp:Label ID="lblpaid" runat="server" Style="color: green"></asp:Label>
                                </div>
                                <div class="col-md-2">
                                    <b>BALANCE AMOUNT : </b>
                                    <asp:Label ID="lblbalance" runat="server" Style="color: red"></asp:Label>
                                </div>
                                <div class="col-md-2">
                                    <b>REFUNDED AMOUNT : </b>
                                    <asp:Label ID="lblrefunded" runat="server"></asp:Label>
                                </div>
                                <div class="col-md-2">
                                    <b>REFUNDABLE AMOUNT : </b>
                                    <asp:Label ID="lblrefundable" runat="server"></asp:Label>
                                </div>
                                <div class="col-md-1"></div>
                            </div>
                            <div class="row" runat="server" id="refund" style="padding-top: 15px;" visible="false">
                                <div class="col-md-2"></div>
                                <div class="col-md-8">
                                    <div class="row">
                                        <div class="col-md-4">
                                            Refund Amount
                                            <asp:TextBox ID="txtrefund" CssClass="form-control" onkeypress="return isNumber(event)" runat="server"></asp:TextBox>
                                            <asp:Label ID="lblfinalrefund" runat="server" Visible="false"></asp:Label>
                                        </div>
                                        <div class="col-md-8">
                                            Refund Remark
                                            <asp:TextBox ID="txtref_reason" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2"></div>
                            </div>
                            <br />
                            <div class="row" runat="server" id="feetable" visible="false">
                                <div class="col-md-12" style="overflow: auto">
                                    <asp:GridView ID="grdfees" runat="server" Font-Size="12pt"
                                        Style="text-align: center;" AutoGenerateColumns="False" CssClass="table table-bordered table-condensed table-hover mygrid" OnRowDataBound="grdfees_RowDataBound">
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <RowStyle HorizontalAlign="Center" Height="10px"></RowStyle>
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkall" runat="server" AutoPostBack="true" OnCheckedChanged="chkall_CheckedChanged" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkselect" runat="server" AutoPostBack="true" OnCheckedChanged="chkselect_CheckedChanged" />
                                                </ItemTemplate>
                                                <ItemStyle Width="10%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="STRUCTURE">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblstructname" runat="server" Text='<%# Eval("struct_name")%>'></asp:Label>
                                                    <asp:Label ID="lblstructid" runat="server" Text='<%# Eval("struct_id")%>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="25%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TOTAL FEE">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblamount" runat="server" Text='<%# Eval("Amount")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PAID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpaid" runat="server" Text='<%# Eval("paid")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="BALANCE">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpending" runat="server" Text='<%# Eval("balance")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PAY">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtpay" runat="server" onkeypress="return isNumber(event)" Enabled="false" AutoPostBack="true" OnTextChanged="txtpay_TextChanged" MaxLength="10"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle Width="20%" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            <div class="row" runat="server" id="btn" visible="false">
                                <div class="col-md-4"></div>
                                <div class="col-md-2">
                                    <asp:Button ID="btnsave" runat="server" CssClass="btn btn-success form-control" Text="Save" OnClick="btnsave_Click" />
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="btncancel" runat="server" CssClass="btn btn-success form-control" Text="Cancel" OnClick="btncancel_Click" />
                                </div>
                                <div class="col-md-4"></div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12" style="overflow: auto">
                                    <asp:GridView ID="grdedit" runat="server" Font-Size="12pt"
                                        Style="text-align: center;" AutoGenerateColumns="False" CssClass="table table-bordered table-hover mygrid" OnSelectedIndexChanged="grdedit_SelectedIndexChanged" OnRowDeleting="grdedit_RowDeleting" OnRowCommand="grdedit_RowCommand" OnRowDataBound="grdedit_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="RECEIPT NO">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrecptno" runat="server" Text='<%# Eval("Recpt_no")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="AMOUNT">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblamount" runat="server" Text='<%# Eval("amount")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="12%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PAYMENT MODE">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblmode" runat="server" Text='<%# Eval("Recpt_mode")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="13%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DURATION">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblduration" runat="server" Text='<%# Eval("duration")%>'></asp:Label>
                                                    <asp:Label ID="lbldurid" runat="server" Text='<%# Eval("duration_id")%>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="16%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PAY DATE">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldate" runat="server" Text='<%# Eval("date")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="10%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="FEES TYPE">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbltype" runat="server" Text='<%# Eval("type")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="10%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PRINT">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnprint" runat="server" CommandName="printmodal" CommandArgument="<%# Container.DataItemIndex %>" Text="<i class='fas fa-print' aria-hidden='true'></i>" />
                                                </ItemTemplate>
                                                <ItemStyle Width="7%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="EDIT">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnedit" runat="server" CommandName="Select" Text="<i class='fas fa-edit' aria-hidden='true'></i>" />
                                                </ItemTemplate>
                                                <ItemStyle Width="7%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DELETE">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btndelete" runat="server" OnClientClick="Confirm()" CommandName="Delete" Text="<i class='fas fa-trash' aria-hidden='true'></i>" />
                                                </ItemTemplate>
                                                <ItemStyle Width="7%" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <%--Modal--%>
    <div class="modal" id="modalyear">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Student Details</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div class="row" style="font-size: 15px; text-align: center">
                                <div class="col-md-4">
                                    <b>STUDENT ID :</b>
                                    <asp:Label ID="lblmodalid" runat="server"></asp:Label>
                                </div>
                                <div class="col-md-8">
                                    <b>NAME :</b>
                                    <asp:Label ID="lblmodalname" runat="server"></asp:Label>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12" style="height: 300px; overflow: auto">
                                    <asp:GridView ID="grdyear" runat="server" Font-Size="12pt"
                                        Style="text-align: center;" AutoGenerateColumns="False" CssClass="table table-bordered table-condensed table-hover" OnSelectedIndexChanged="grdyear_SelectedIndexChanged">
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <RowStyle HorizontalAlign="Center"></RowStyle>
                                        <Columns>
                                            <asp:TemplateField HeaderText="Academic Year">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblyear" runat="server" Text='<%# Eval("Year")%>'></asp:Label>
                                                    <asp:Label ID="ayid" runat="server" Text='<%# Eval("AYID")%>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Standard">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblstdname" runat="server" Text='<%# Eval("standard")%>'></asp:Label>
                                                    <asp:Label ID="classid" runat="server" Text='<%# Eval("class_id")%>' Visible="false"></asp:Label>
                                                    <asp:Label ID="medium" runat="server" Text='<%# Eval("medium_id")%>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Division">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldivname" runat="server" Text='<%# Eval("Division")%>'></asp:Label>
                                                    <asp:Label ID="divid" runat="server" Text='<%# Eval("division_id")%>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Roll No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrollno" runat="server" Text='<%# Eval("roll_no")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="View">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnedit" runat="server" CommandName="Select" Text="<i class='fas fa-edit' aria-hidden='true'></i>" />
                                                </ItemTemplate>
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
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

    <div class="modal" id="printmodal">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Print Fee Reciept</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <div class="row" style="font-size: 15px; text-align: center">
                                <div class="col-md-4">
                                    <span><b>Receipt no :</b></span><br />
                                    <asp:Label ID="lblrecptnumber" runat="server"></asp:Label>
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
                                    <br />
                                    <asp:Button ID="btn_print" CssClass="form-control btn btn-success" runat="server" Text="Print" OnClick="btn_print_Click" OnClientClick="target ='_blank';"></asp:Button>
                                </div>
                            </div>
                            <br />

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function openModal(name) {
            $("[id*=" + name + "]").modal('show');
            $("[id*=" + name + "]").data('bs.modal')._config.backdrop = 'static';
            $("[id*=" + name + "]").data('bs.modal')._config.keyboard = false;
        }
        function closeModal(name) {
            $("[id*=" + name + "]").modal('hide');
        }
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
        function redirect(name) {
            window.open(name, '_blank');
        }
    </script>
</asp:Content>

