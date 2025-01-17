<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="frm_Divison_Master.aspx.cs" Inherits="frm_Divison_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .label {
            color: black;
        }
    </style>
    <script type="text/javascript">
        function Confirmdiv() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "division";
            confirm_value.value = "";
            if (confirm("Do you want to delete the selected Division ?")) {
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
    <div class="row">
        <div class="col-md-12">
            <div class="card-header text-white" style="background: linear-gradient(to right, #6f03bf 1%, #363695 100%) !important; border-radius: 7px">
                <h3>Division Master</h3>
            </div>
            <div class="card card-body">
                <asp:UpdatePanel runat="server" ID="up1" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-md-2">
                                Medium:<asp:DropDownList ID="ddlmedium" runat="server" TabIndex="1" CssClass="form-control" OnSelectedIndexChanged="ddlmedium_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                Class:<asp:DropDownList ID="ddlclass" runat="server" CssClass="form-control" TabIndex="2" AutoPostBack="true" OnSelectedIndexChanged="ddlclass_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <div class="row">
                                    <div class="col-md-5">
                                        <br />
                                        <asp:CheckBox runat="server" ID="chkprev" Text="As Per Previous Year" TabIndex="3" OnCheckedChanged="chkprev_CheckedChanged" AutoPostBack="true" />
                                    </div>
                                    <div class="col-md-7" id="Prevyear" runat="server">
                                        Year
                                   <asp:DropDownList ID="ddlprev" runat="server" Enabled="false" CssClass="form-control" TabIndex="4" AutoPostBack="true" OnSelectedIndexChanged="ddlprev_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-5">
                                <div class="row">
                                    <div class="col-md-4">
                                        <br />
                                        <asp:Button ID="btnadd" runat="server" Text="Add" TabIndex="5" OnClick="btnadd_Click" CssClass="btn btn-success form-control" />
                                    </div>
                                    <div class="col-md-4">
                                        <br />
                                        <asp:Button ID="btnsave" runat="server" Text="Save" TabIndex="6" OnClick="btnsave_Click" CssClass="btn btn-success form-control" />
                                    </div>
                                    <div class="col-md-4">
                                        <br />
                                        <asp:Button ID="btnclear" runat="server" Text="Clear" TabIndex="7" CssClass="btn btn-success form-control" OnClick="btnclear_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <div class="row" runat="server" id="grid_show" style="padding-top: 15px;">
                    <div class="col-md-2"></div>
                    <div class="col-md-8">
                        <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Always">
                            <ContentTemplate>
                                <asp:GridView ID="grd_divison" runat="server" Font-Size="12pt"
                                    Style="text-align: center;" AutoGenerateColumns="False" CssClass="table table-bordered table-hover mygrid" OnRowDataBound="grd_divison_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="DIVISION ID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldivid" CssClass="label-default" runat="server" Text='<%# Eval("division_id")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DIVISION">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldivname" CssClass="label-default" runat="server" Text='<%# Eval("division_name")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtdivname" CssClass="col-md-5" autocomplete="off" runat="server" Text='<%# Eval("division_name")%>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle Width="60%" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="EDIT">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnedit" runat="server" CommandName="Edit" Text="<i class='fas fa-edit' aria-hidden='true'></i>" OnClick="btnedit_Click"/>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="btnupdate" Text="<i class='fa fa-save' style='font-size:24px'></i>" CommandName="Update" runat="server" OnClick="btnupdate_Click" />
                                                &nbsp;&nbsp;
                                        <asp:LinkButton ID="btncancel" Text="<i class='fa fa-times-circle' style='font-size:24px'></i>" runat="server" CommandName="Cancel" OnClick="btncancel_Click" />
                                            </EditItemTemplate>
                                            <ItemStyle Width="20%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DELETE">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btndelete" runat="server" OnClientClick="Confirmdiv()" Text="<i class='fas fa-trash' aria-hidden='true'></i>" OnClick="btndelete_Click1"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Width="20%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FLAG" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblflag" CssClass="label-default" runat="server" Text='<%# Eval("flag")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle BackColor="#009ACB" ForeColor="White"></HeaderStyle>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-md-2"></div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

