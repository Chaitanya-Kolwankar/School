<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="frm_Standard_Master.aspx.cs" Inherits="frm_Standard_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .label {
            color: black;
        }        
    </style>
        <script type="text/javascript">
            function Confirm() {
                var confirm_value = document.createElement("INPUT");
                confirm_value.type = "hidden";
                confirm_value.name = "confirm_value";
                confirm_value.value = "";
                if (confirm("Do you want to delete the selected Standard ?")) {
                    confirm_value.value = "OK";
                }
                else {
                    confirm_value.value = "Cancel";
                }
                document.forms[0].appendChild(confirm_value);
            }
    </script>
    <script type="text/javascript">
        function CLEAR() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "trigger";
            confirm_value.value = "";
            if (confirm("All the unsaved data will be lost, Do you want to continue ?")) {
                confirm_value.value = "OK";
            }
            else {
                confirm_value.value = "Cancel";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
    <script type="text/javascript">
        function blockSpecialChar(e) {
            var k = e.keyCode;
            return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || (k >= 48 && k <= 57));

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="card-header text-white" style="background: linear-gradient(to right, #6f03bf 1%, #363695 100%) !important; border-radius: 7px">
                <h3>Standard Master</h3>
            </div>
    <div class="card card-body">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-lg-3">
                        Medium :
                        <asp:DropDownList ID="ddlmediumstd" runat="server" AutoPostBack="true" CssClass="form-control" TabIndex="1" OnSelectedIndexChanged="ddlmediumstd_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="col-lg-2" style="padding-top: 20px;">
                        <asp:Button ID="btnsavestd" runat="server" Text="Save" CssClass="btn btn-success form-control"  TabIndex="2"  OnClick="btnsavestd_Click" />
                    </div>
                    <div class="col-lg-2" style="padding-top: 20px;">
                        <asp:Button ID="btnclearstd" OnClientClick="return CLEAR()" runat="server" Text="Clear"  CssClass="btn btn-success form-control" TabIndex="3" OnClick="btnclearstd_Click" />
                    </div>
                </div>
                <div class="row" style="padding-top: 10px;">
                    
                    <div class="container">
                        <asp:GridView ID="gridstd" runat="server" Font-Size="15pt"
                            Style="text-align: center;" AutoGenerateColumns="False" CssClass="table table-bordered table-hover mygrid" HeaderStyle-CssClass="FixedHeader" OnRowEditing="gridstd_RowEditing">
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Size="13pt" />
                            <RowStyle HorizontalAlign="Center"></RowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="RANK">
                                    <ItemTemplate>
                                        <asp:Label ID="lblrank" CssClass="label" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="STANDARD ID" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblstdid" CssClass="label" runat="server" Text='<%# Eval("std_id")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="STANDARD">
                                    <ItemTemplate>
                                        <asp:Label ID="lblstd" runat="server" CssClass="label" Text='<%# Eval("std_name")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtstd" runat="server" CssClass="col-md-4" Text='<%# Eval("std_name")%>' onkeypress="return blockSpecialChar(event)" OnTextChanged="txtstd_TextChanged" autocomplete="off" AutoPostBack="true"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FLAG" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblflag" CssClass="label" runat="server" Text='<%# Eval("flag")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ACTION" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblaction" CssClass="label" runat="server" Text='<%# Eval("action")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ADD">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnadd" runat="server" OnClick="btnaddstd_Click"><i class="fa fa-plus" aria-hidden="true"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DELETE">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btndelete" runat="server" OnClientClick="Confirm()" OnClick="btndeletestd_Click"><i class="fa fa-trash" aria-hidden="true"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle BackColor="#009ACB"  ForeColor="White"  ></HeaderStyle>
                        </asp:GridView>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div class="modal" id="myModal">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">ADD STANDARD</h5>
                </div>
                <div class="modal-body">
                    <div class="Col-lg-12">
                        Which Standard do you want to add ?
                    </div>
                </div>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <div class="modal-footer">
                            <asp:Button ID="btnabovestd" runat="server" Text="Previous" CssClass="btn btn-info col-lg-3" Style="color: white;" OnClick="btnabovestd_Click"></asp:Button>
                            <asp:Button ID="btnbelowstd" runat="server" Text="Next" CssClass="btn btn-info col-lg-3" Style="color: white;" OnClick="btnbelowstd_Click"></asp:Button>
                            <asp:Button ID="btncancelstd" runat="server" Text="Cancel" CssClass="btn btn-info col-lg-3" data-dismiss="modal" Style="color: white;"></asp:Button>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>

