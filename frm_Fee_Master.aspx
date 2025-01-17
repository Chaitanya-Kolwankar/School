<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" EnableEventValidation="true" CodeFile="frm_Fee_Master.aspx.cs" Inherits="frm_Fee_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="js/bootstrap-multiselect.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('[id*=lstmonth]').multiselect({
                buttonWidth: '100%',
            });
            $('[id*=lstcaste]').multiselect({
                buttonWidth: '100%',
            });
        });

        function multi() {
            $('[id*=lstmonth]').multiselect({
                buttonWidth: '100%',
            });
            $('[id*=lstcaste]').multiselect({
                buttonWidth: '100%',
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid">
        <div class="card-header text-white" style="background: linear-gradient(to right, #6f03bf 1%, #363695 100%) !important; border-radius: 7px;">
            <h3>Fee Master</h3>
        </div>
        <div class="card card-body">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-md-2">
                            Medium : 
                    <asp:DropDownList ID="ddlmedium" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlmedium_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            Class : 
                    <asp:DropDownList ID="ddlclass" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlclass_SelectedIndexChanged" Enabled="false"></asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            Caste : 
                    <asp:DropDownList ID="ddlcaste" runat="server" AutoPostBack="true" CssClass="form-control" Enabled="false" OnSelectedIndexChanged="ddlcaste_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            Type : 
                    <asp:DropDownList ID="ddltype" runat="server" AutoPostBack="true" CssClass="form-control" Enabled="false" OnSelectedIndexChanged="ddltype_SelectedIndexChanged">
                    </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            Duration :
                    <asp:DropDownList ID="ddlduration" runat="server" AutoPostBack="true" CssClass="form-control" Enabled="false" OnSelectedIndexChanged="ddlduration_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <br />
                            <asp:Button ID="btnclear" runat="server" Text="Clear" OnClick="btnclear_Click" CssClass="btn btn-success form-control" />
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div class="row" runat="server" id="button" visible="false">
                        <div class="col-md-3"></div>
                        <div class="col-md-6">
                            <br />
                            <div class="row">
                                <div class="col-md-3">
                                    <asp:Button ID="btncaste" runat="server" Text="As Per Caste" CssClass="btn btn-info form-control" data-target="#modalcaste" data-toggle="modal" data-backdrop="static" data-keyboard="false" OnClick="btncaste_Click" />
                                </div>
                                <div class="col-md-3">
                                    <asp:Button ID="btnprev" runat="server" Text="As Per Duration" CssClass="btn btn-info form-control" data-target="#myModal" data-toggle="modal" data-backdrop="static" data-keyboard="false" OnClick="btnprev_Click" Enabled="false" />
                                </div>
                                <div class="col-md-3">
                                    <asp:Button ID="btnadd" runat="server" Text="Add" OnClick="btnadd_Click" CssClass="btn btn-success form-control" Enabled="false" />
                                </div>
                                <div class="col-md-3">
                                    <asp:Button ID="btnsave" runat="server" Text="Save" OnClick="btnsave_Click" CssClass="btn btn-success form-control" Enabled="false" />
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3"></div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <br />
                    <div class="row">
                        <div class="col-md-12" style="max-height:350px;overflow-y:auto">
                            <asp:GridView ID="grdfee" runat="server" Font-Size="12pt"
                                Style="text-align: center;" AutoGenerateColumns="False" CssClass="table table-hover table-striped table-bordered mygrid" HeaderStyle-CssClass="FixedHeader" OnRowDataBound="grd_fee_RowDataBound">
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <RowStyle HorizontalAlign="Center"></RowStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr No.">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                        <ItemStyle Width="5%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="STRUCTURE NAME">
                                        <ItemTemplate>
                                            <asp:Label ID="lblname" runat="server" Text='<%# Eval("struct_name")%>'></asp:Label>
                                            <asp:Label ID="lblid" runat="server" Text='<%# Eval("struct_id")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtname" CssClass="form-control" autocomplete="off" runat="server" onkeypress="return nametxt(event)" Text='<%# Eval("struct_name")%>'></asp:TextBox>
                                            <asp:TextBox ID="txtid" CssClass="form-control" autocomplete="off" runat="server" Text='<%# Eval("struct_id")%>' Visible="false" Enabled="false"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemStyle Width="30%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="AMOUNT">
                                        <ItemTemplate>
                                            <asp:Label ID="lblamount" runat="server" Text='<%# Eval("Amount")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtamount" CssClass="form-control" autocomplete="off" runat="server" Text='<%# Eval("Amount")%>' MaxLength="10" onkeypress="return isNumber(event)" Style="text-align: right;"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemStyle Width="20%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="RANK">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtRank" CssClass="form-control" autocomplete="off" runat="server" Text='<%# Eval("Rank")%>' MaxLength="2" onkeypress="return isNumber(event)" Style="text-align: center;" onchange="Calculation(this)"></asp:TextBox>
                                            <asp:Label ID="lblFlag" runat="server" Text='<%# Eval("Flag")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="15%" />
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Is Admission Fees">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="admichk" CssClass="form-control" autocomplete="off" runat="server"  MaxLength="2" onkeypress="return isNumber(event)" Style="text-align: center;" onchange="Calculation(this)"></asp:CheckBox>
                                            <asp:Label ID="lbladmFlag" runat="server" Text='<%# Eval("admflg")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="15%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="EDIT">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnedit" OnClick="btnedit_Click" runat="server" CommandName="Edit" Text="<i class='fas fa-edit' aria-hidden='true'></i>" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:LinkButton ID="btnupdate" Text="<i class='fa fa-save' style='font-size:24px'></i>" runat="server" CommandName="Update" OnClick="btnupdate_Click" />
                                        </EditItemTemplate>
                                        <ItemStyle Width="15%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DELETE">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btndelete" runat="server" CommandName="Delete" Text="<i class='fas fa-trash' aria-hidden='true'></i>" OnClick="btndelete_Click"></asp:LinkButton>
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
    <div class="modal" id="myModal">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Define Fee</h5>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-6">
                            <asp:UpdatePanel runat="server" ID="up1">
                                <ContentTemplate>
                                    From
                        <asp:Label ID="lblref" runat="server"></asp:Label>
                                    :
                            <asp:DropDownList ID="ddlref" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlref_SelectedIndexChanged"></asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="col-md-6">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    To
                            <asp:Label ID="lblto" runat="server"></asp:Label>
                                    : 
                            <asp:ListBox ID="lstmonth" runat="server" SelectionMode="Multiple" CssClass="btn form-control col-md-12" onchange="getselected(this);"></asp:ListBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="grdprev" runat="server" Font-Size="12pt"
                                        Style="text-align: center;" AutoGenerateColumns="False" CssClass="table table-hover table-striped table-bordered table-grdprev">
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <RowStyle HorizontalAlign="Center"></RowStyle>
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr No.">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                                <ItemStyle Width="10%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="STRUCTURE NAME">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblname" runat="server" Text='<%# Eval("struct_name")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="45%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="AMOUNT">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblamount" runat="server" Text='<%# Eval("Amount")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="45%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="RANK" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrank" runat="server" Text='<%# Eval("Rank")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <div class="row">
                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="btnsaveprev" runat="server" Text="Save" CssClass="btn btn-success" OnClientClick="return Confirm()" OnClick="btnsaveprev_Click" />&nbsp;&nbsp;
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:Button ID="btncancelstd" runat="server" Text="Cancel" CssClass="btn btn-info" data-dismiss="modal" Style="color: white;"></asp:Button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal" id="modalcaste">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Define Fee</h5>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-6">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel7">
                                <ContentTemplate>
                                    From Caste:
                            <asp:DropDownList ID="ddlcasteref" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlcasteref_SelectedIndexChanged"></asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="col-md-6">
                            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                <ContentTemplate>
                                    To Caste:
                            <asp:ListBox ID="lstcaste" runat="server" SelectionMode="Multiple" CssClass="btn form-control col-md-12" onchange="getselectedrow(this);"></asp:ListBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="GridView1" runat="server" Font-Size="12pt"
                                        Style="text-align: center;" AutoGenerateColumns="False" CssClass="table table-hover table-striped table-bordered table-grdprev">
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <RowStyle HorizontalAlign="Center"></RowStyle>
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr No.">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                                <ItemStyle Width="10%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="STRUCTURE NAME">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblname" runat="server" Text='<%# Eval("struct_name")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="45%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="AMOUNT">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblamount" runat="server" Text='<%# Eval("Amount")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="45%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="RANK" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrank" runat="server" Text='<%# Eval("Rank")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <div class="row">
                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="btnsavecaste" runat="server" Text="Save" CssClass="btn btn-success" OnClientClick="return Confirmcaste()" OnClick="btnsavecaste_Click" />&nbsp;&nbsp;
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:Button ID="Button2" runat="server" Text="Cancel" CssClass="btn btn-info" data-dismiss="modal" Style="color: white;"></asp:Button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
    </script>
    <script>
        function Calculation(row) {
            var row1 = row.parentNode.parentNode;
            var grid = document.getElementById("<%= grdfee.ClientID%>");
            var id = "ContentPlaceHolder1_grdfee_txtRank_" + (parseInt(row1.rowIndex) - 1).toString();
            var value = document.getElementById(id).value;
            var flag = "";

            if (value != "") {
                if (value != "0") {
                    if (value != "00") {
                        for (var i = 0; i < grid.rows.length - 1; i++) {
                            if (i == (parseInt(row1.rowIndex) - 1)) {

                            }
                            else {
                                var id1 = "ContentPlaceHolder1_grdfee_txtRank_" + (i).toString();
                                var value1 = document.getElementById(id1).value;
                                if (value == value1) {
                                    document.getElementById(id).value = "";
                                    $.notify('Rank cannot be same', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });
                                    flag = "true";
                                    break;
                                }

                            }
                        }
                    }
                    else {
                        document.getElementById(id).value = "";
                    }
                }
                else {
                    document.getElementById(id).value = "";
                }
            }
            else {

            }
        }
    </script>
    <script>
        function Confirm() {
            var btn = $("[id*='btnsaveprev']")[0].defaultValue;
            var drop = $("[id*='ddlref']")[0].selectedIndex;
            var lst = $("[id*=lstmonth]")[0].selectedIndex;
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            confirm_value.value = "";
            if (drop != 0 && lst != -1) {
                if (btn == "Save") {

                    if (confirm("Fees will be copied for Selected Duration. Do you want to proceed ?")) {
                        confirm_value.value = "OK";
                    }
                    else {
                        confirm_value.value = "Cancel";
                    }

                }
                else {
                }
            }
            else if (drop == 0) {
                confirm_value.value = "NO";

                $.notify('Select Reference Structure', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });
            }
            else if (lst == -1) {
                confirm_value.value = "NO";

                $.notify('Select Duration to Save structure', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });
            }
            document.forms[0].appendChild(confirm_value);
        }

        function Confirmcaste() {
            var btn = $("[id*='btnsavecaste']")[0].defaultValue;
            var drop = $("[id*='ddlcasteref']")[0].selectedIndex;
            var lst = $("[id*=lstcaste]")[0].selectedIndex;
            var confirm_value1 = document.createElement("INPUT");
            confirm_value1.type = "hidden";
            confirm_value1.name = "confirm_value1";
            confirm_value1.value = "";
            if (drop != 0 && lst != -1) {
                if (btn == "Save") {

                    if (confirm("Fees will be copied for Selected Caste. Do you want to proceed ?")) {
                        confirm_value1.value = "OK";
                    }
                    else {
                        confirm_value1.value = "Cancel";
                    }

                }
                else {
                }
            }
            else if (drop == 0) {
                confirm_value1.value = "NO";

                $.notify('Select Reference Structure', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });
            }
            else if (lst == -1) {
                confirm_value1.value = "NO";

                $.notify('Select Duration to Save structure', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });
            }
            document.forms[0].appendChild(confirm_value1);
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
    <script type="text/javascript">
        function getselected(row) {
            var a = new Array();
            var month = "";
            var e = $("[id*=lstmonth]");
            var item = $("[id*=lstmonth]")[0].length;
            for (var i = 0; i < item ; i++) {
                if (e[0].options[i].selected == true) {
                    if (e[0].options[i].selected == true && month == "") {
                        month = "'" + e[0].options[i].value + "'";
                    }
                    else if (e[0].options[i].selected == true) {
                        month = month + ",'" + e[0].options[i].value + "'";
                    }
                }
            }
            PageMethods.Setsession(month);
        }
    </script>
    <script type="text/javascript">
        function getselectedrow(row) {
            var a = new Array();
            var month = "";
            var e = $("[id*=lstcaste]");
            var item = $("[id*=lstcaste]")[0].length;
            for (var i = 0; i < item ; i++) {
                if (e[0].options[i].selected == true) {
                    if (e[0].options[i].selected == true && month == "") {
                        month = "'" + e[0].options[i].value + "'";
                    }
                    else if (e[0].options[i].selected == true) {
                        month = month + ",'" + e[0].options[i].value + "'";
                    }
                }
            }
            PageMethods.SetCaste(month);
        }
    </script>
</asp:Content>

