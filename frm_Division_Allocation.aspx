<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frm_Division_Allocation.aspx.cs" Inherits="frm_Division_Allocation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="card-header text-white" style="background: linear-gradient(to right, #6f03bf 1%, #363695 100%) !important; border-radius: 7px">
                <h3>Division Allocation</h3>
            </div>
            <div class="card card-body">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-md-2">
                                <b>Medium </b>
                                <br />
                                <asp:DropDownList ID="ddlmedium" CssClass="form-control" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlmedium_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <b>Class </b>
                                <br />
                                <asp:DropDownList ID="ddlclass" CssClass="form-control" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlclass_SelectedIndexChanged" Enabled="false"></asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <b>Division </b>
                                <br />
                                <asp:DropDownList ID="ddldiv" CssClass="form-control" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddldiv_SelectedIndexChanged" Enabled="false"></asp:DropDownList>
                            </div>
                            <div class="col-md-6">
                                <br />
                                <div class="row">
                                    <div class="col-md-4">
                                        <asp:Button ID="btnsave" CssClass="form-control btn btn-success" runat="server" Text="Save" OnClick="btnsave_Click" />
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Button ID="btnclear" CssClass="form-control btn btn-success" runat="server" Text="Clear" OnClick="btnclear_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">

                            <div class="col-md-12" style="max-height: 400px; overflow: auto">
                                <asp:GridView ID="gridstd" runat="server" Font-Size="12pt" Width="100%" CssClass="table  table-bordered mygrid" HeaderStyle-CssClass="FixedHeader"
                                    Style="text-align: center;" AutoGenerateColumns="False" OnRowDataBound="gridstd_RowDataBound" AllowSorting="True" OnSorting="gridstd_Sorting">
                                    <RowStyle HorizontalAlign="Center"></RowStyle>
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkassign" runat="server" onchange="chk_Calculation(this)" />
                                                <asp:TextBox ID="txtdivid" CssClass="form-control col-md-4" Style="display: none" runat="server" Text='<%# Eval("division_id")%>'></asp:TextBox>
                                                <asp:TextBox ID="txtcheck" CssClass="form-control col-md-4" Style="display: none" runat="server" Text='<%# Eval("check")%>'></asp:TextBox>
                                                <asp:TextBox ID="txtflag" CssClass="form-control col-md-4" Style="display: none" runat="server" Text='<%# Eval("flag")%>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FORM ID" SortExpression="form_id">
                                            <ItemTemplate>
                                                <asp:Label ID="lblform" runat="server" Text='<%# Eval("form_id")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="STUDENT ID" SortExpression="Student_id">
                                            <ItemTemplate>
                                                <asp:Label ID="lblstud" runat="server" Text='<%# Eval("Student_id")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="STUDENT NAME" SortExpression="name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblname" runat="server" Text='<%# Eval("name")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ROLL NO" ControlStyle-CssClass="position-static">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtroll" CssClass="form-control col-md-5" MaxLength="8" autocomplete="off" Text='<%# Eval("Roll_no")%>' runat="server" onkeypress="return isNumber(event)" onchange="Calculation(this)"></asp:TextBox><%--OnTextChanged="txtroll_TextChanged"--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle BackColor="#009ACB" ForeColor="White"></HeaderStyle>
                                </asp:GridView>
                            </div>

                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <script src="assets/libs/jquery/dist/jquery.min.js"></script>
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
        function chk_Calculation(row) {
            var row1 = row.parentNode.parentNode;
            var grid = document.getElementById("<%= gridstd.ClientID%>");
            var id = "ContentPlaceHolder1_gridstd_txtroll_" + (parseInt(row1.rowIndex) - 1).toString();
            var id1 = "ContentPlaceHolder1_gridstd_chkassign_" + (parseInt(row1.rowIndex) - 1).toString();
            var id2 = "ContentPlaceHolder1_gridstd_txtflag_" + (parseInt(row1.rowIndex) - 1).toString();
            var id3 = "ContentPlaceHolder1_gridstd_txtdivid_" + (parseInt(row1.rowIndex) - 1).toString();
            var id4 = "ContentPlaceHolder1_gridstd_txtcheck_" + (parseInt(row1.rowIndex) - 1).toString();

            if (document.getElementById(id1).checked == true) {
                document.getElementById(id).disabled = false;
                document.getElementById(id2).value = "1";
                document.getElementById(id4).value = "1";

            }
            else if (document.getElementById(id1).checked == false && document.getElementById(id3).value == "") {
                document.getElementById(id).value = "";
                document.getElementById(id).disabled = true;
                document.getElementById(id2).value = "0";
                document.getElementById(id4).value = "0";
            }
            else {
                document.getElementById(id).value = "";
                document.getElementById(id).disabled = true;
                document.getElementById(id2).value = "1";
                document.getElementById(id4).value = "0";
            }

        }
    </script>
    <script>
        function Calculation(row) {
            var row1 = row.parentNode.parentNode;
            var grid = document.getElementById("<%= gridstd.ClientID%>");
            var id = "ContentPlaceHolder1_gridstd_txtroll_" + (parseInt(row1.rowIndex) - 1).toString();
            var id2 = "ContentPlaceHolder1_gridstd_txtflag_" + (parseInt(row1.rowIndex) - 1).toString();
            var value = document.getElementById(id).value;
            var flag = "";

            if (value != "") {
                if (value != "0") {
                    for (var i = 0; i < grid.rows.length - 1; i++) {
                        if (i == (parseInt(row1.rowIndex) - 1)) {

                        }
                        else {
                            var id1 = "ContentPlaceHolder1_gridstd_txtroll_" + (i).toString();
                            var value1 = document.getElementById(id1).value;
                            if (value == value1) {
                                document.getElementById(id).value = "";
                                $.notify('Roll No cannot be same', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });
                                flag = "true";
                                break;
                            }

                        }
                    }
                    if (flag != "true") {
                        document.getElementById(id2).value = "1";
                    }
                    else {
                        document.getElementById(id2).value = "0";
                    }
                }
                else {
                    document.getElementById(id).value = "";
                }
            }
            else {
                document.getElementById(id2).value = "1";
            }
        }
    </script>
</asp:Content>
