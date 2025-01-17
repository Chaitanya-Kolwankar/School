<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Grant_Access.aspx.cs" Inherits="Grant_Access" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/../netdna.bootstrapcdn.com/twitter-bootstrap/2.3.2/css/bootstrap-combined.min.css" rel="stylesheet" />
    <link href="bower_components/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet" />
    <%--<link href="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/css/bootstrap-multiselect.css" rel="stylesheet" type="text/css" />--%>
    <script src="jquery/dist/jquery.min.js"></script>
    <style type="text/css">
        .WordWrap {
            width: 100%;
            word-break: break-all;
        }

        .WordBreak {
            width: 100px;
            overflow: hidden;
            text-overflow: ellipsis;
        }

        table {
            width: 100%;
            border-collapse: collapse;
            border: 0;
            rules: none;
        }

        td {
            text-align: center;
            border: 0;
            padding: 8px;
            vertical-align: central;
        }

        th {
            text-align: center;
            color: #fff;
            width: inherit;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid">
        <div class="card">
            <div class="card-header text-white" style="background: linear-gradient(to right, #6f03bf 1%, #363695 100%) !important; border-radius: 7px">
                <div>
                    <h3>Role Creation</h3>
                </div>
            </div>
            <div class="card-body">
                <br />
                <div class="card card-body">
                    <div class="tab-content" id="pills-tabContent">
                        <div class="row">
                            <span style="font-size: medium;" id="msg" runat="server"></span>
                        </div>
                        <div class="tab-pane fade show active" id="role" role="tabpanel" aria-labelledby="pills-role">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-md-2"></div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="lbl_roles_name" runat="server" Text="Roles Name :"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:TextBox ID="txt_roles_name" runat="server" Height="35 " CssClass="form-control" AutoPostBack="true" placeholder="Roles Name" onkeypress="return nametxt(event)" style="text-transform:uppercase"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2"></div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label1" runat="server" Text="Select Roles :"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlRoles" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlRoles_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                            <asp:Button ID="btnupdate" CssClass="btn btn-success form-control" runat="server" Text="Update" OnClick="btnupdate_Click" />
                                        </div>
                                        <div class="col-md-1">
                                            <asp:Button ID="Button3" CssClass="btn btn-danger form-control" OnClientClick="Confirm()" runat="server" Text="Delete" OnClick="Button3_Click" />
                                        </div>
                                        <div class="col-md-1">
                                            <asp:Button ID="Button2" CssClass="btn btn-danger" runat="server" Text="X" OnClick="Button2_Click" />
                                        </div>
                                    </div>
                                    <div class="form-group" style="overflow: auto; height: 400PX">
                                            <asp:GridView ID="gvRoles" runat="server" Width="100%" CssClass="table table-bordered table-hover mygrid" HeaderStyle-CssClass="FixedHeader" AutoGenerateColumns="false">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="checkAll" runat="server" AutoPostBack="true" OnCheckedChanged="checkAll_CheckedChanged" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkSelect" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Forms" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="sr" runat="server" Text='<%# Eval("sr_no") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Forms">
                                                        <ItemTemplate>
                                                            <asp:Label ID="forms" runat="server" Text='<%# Eval("form_name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-5"></div>
                                        <div class="col-md-2">
                                            <asp:Button ID="Button1" runat="server" CssClass="btn btn-success form-control" Text="Save" OnClick="Button1_Click"></asp:Button>

                                        </div>
                                        <div class="col-md-5"></div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>
        function nametxt(e) {
            isIE = document.all ? 1 : 0
            keyEntry = !isIE ? e.which : event.keyCode;
            if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) ||  (keyEntry >='48'  && keyEntry <='57' ))
                return true;
            else {
                return false;
            }
        }
    </script>
    <script lang="javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to delete this?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
</asp:Content>

