<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frm_feeDuration.aspx.cs" Inherits="frm_feeDuration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('[id*=lstmonth]').multiselect({
                buttonWidth: '100%',
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="card-header text-white" style="background: linear-gradient(to right, #6f03bf 1%, #363695 100%) !important; border-radius: 7px;">
        <h3>Fee Duration</h3>
    </div>
    <div class="card card-body">

        <div class="row">
            <div class="col-md-3">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        Medium : 
                    <asp:DropDownList ID="ddlmedium" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlmedium_SelectedIndexChanged"></asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-md-3">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        Class : 
                    <asp:DropDownList ID="ddlclass" runat="server" AutoPostBack="true" CssClass="form-control" Enabled="false" OnSelectedIndexChanged="ddlclass_SelectedIndexChanged"></asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-md-3">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        Payment Duration Type :
                        <asp:DropDownList ID="ddltype" runat="server" AutoPostBack="true" CssClass="form-control" Enabled="false" OnSelectedIndexChanged="ddltype_SelectedIndexChanged"></asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <div class="col-md-3">
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <div id="text" runat="server" style="display: none">
                        <asp:Label runat="server" ID="lblname"></asp:Label> Name : 
                    <asp:TextBox ID="txtname" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>

                        <div id="multi" runat="server" style="display: block">
                            Duration : 
                            <asp:ListBox ID="lstmonth" runat="server" multiple="multiple" SelectionMode="Multiple" CssClass="btn form-control col-md-12" Enabled="false" onchange="getselected(this);"></asp:ListBox>
                            
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddltype" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="btnclear" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnsave" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
        <asp:UpdatePanel ID="up2" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-3"></div>
                    <div class="col-md-6">
                        <br />
                        <div class="row">
                            <div class="col-md-6">
                                <asp:Button ID="btnsave" runat="server" CssClass="btn btn-success form-control" Text="Save" OnClick="btnsave_Click" />
                            </div>
                            <div class="col-md-6">
                                <asp:Button ID="btnclear" runat="server" CssClass="btn btn-success form-control" Text="Clear" OnClick="btnclear_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3"></div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <asp:UpdatePanel ID="up3" runat="server">
            <ContentTemplate>
                <div class="col-md-12" style="max-height:350px;overflow-y:auto">
                <asp:GridView ID="grduration" runat="server" Font-Size="12pt"
                    Style="text-align: center;" AutoGenerateColumns="False" CssClass="table table-hover table-striped table-bordered mygrid" HeaderStyle-CssClass="FixedHeader" OnRowDeleting="grduration_RowDeleting">
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <RowStyle HorizontalAlign="Center"></RowStyle>
                    <Columns>
                        <asp:TemplateField HeaderText="Sr No.">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <ItemStyle Width="10%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="DURATION">
                            <ItemTemplate>
                                <asp:Label ID="lblname" runat="server" Text='<%# Eval("duration")%>'></asp:Label>
                                <asp:Label ID="lblflag" runat="server" Text='<%# Eval("Flag")%>' Visible="false"></asp:Label>
                                <asp:Label ID="lblid" runat="server" Text='<%# Eval("duration_id")%>' Visible="false"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="55%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="DELETE">
                            <ItemTemplate>
                                <asp:LinkButton ID="btndelete" runat="server" CommandName="Delete" Text="<i class='fas fa-trash' aria-hidden='true'></i>"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="25%" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle BackColor="#009ACB"  ForeColor="White"  ></HeaderStyle>
                </asp:GridView>
                    </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <script type="text/javascript">
        function multi() {
            $('[id*=lstmonth]').multiselect({
                buttonWidth: '100%',
            });
            sessionStorage.clear();
        }
    </script>
    <script type="text/javascript">
        function getselected(row) {
            var a = new Array();
            var month = "";
            var e = $("[id*=lstmonth]");
            var item = $("[id*=lstmonth]")[0].length;
            var value = $("[id*=ddltype]").val();
            var unchecked = false;
            if (sessionStorage.length != 0 && value == "3") {
                var value1 = sessionStorage.getItem("a");
                var value2 = sessionStorage.getItem("b");
                var value3 = sessionStorage.getItem("c");
            }
            for (var i = 0; i < item ; i++) {

                if (e[0].options[i].selected == true) {
                    if (value == "3") {
                        if (sessionStorage.length == 0) {
                            if (i <= 9) {
                                e[0].options[i + 1].selected = true;
                                e[0].options[i + 2].selected = true;
                                sessionStorage.setItem("a", i);
                                sessionStorage.setItem("b", i + 1);
                                sessionStorage.setItem("c", i + 2);
                                PageMethods.Setsession(e[0].options[i].value + ',' + e[0].options[i + 1].value + ',' + e[0].options[i + 2].value);
                                i = i + 2;
                            }
                            else if (i == 10) {
                                e[0].options[i + 1].selected = true;
                                e[0].options[0].selected = true;
                                sessionStorage.setItem("a", i);
                                sessionStorage.setItem("b", i + 1);
                                sessionStorage.setItem("c", "0");
                                PageMethods.Setsession(e[0].options[i].value + ',' + e[0].options[i + 1].value + ',' + e[0].options[0].value);
                                i = i + 1;
                            }
                            else if (i == 11) {
                                e[0].options[0].selected = true;
                                e[0].options[1].selected = true;
                                sessionStorage.setItem("a", i);
                                sessionStorage.setItem("b", "0");
                                sessionStorage.setItem("c", "1");
                                PageMethods.Setsession(e[0].options[i].value + ',' + e[0].options[0].value + ',' + e[0].options[1].value);
                            }
                        }
                        else {
                            var flag = false;
                            if (i == value1 || i == value2 || i == value3) {
                                flag = true;
                                if (e[0].options[value1].selected == false || e[0].options[value2].selected == false || e[0].options[value3].selected == false) {
                                    unchecked = true;
                                    break;
                                }
                            }
                            if (flag == false) {
                                a.push(i);
                                if (i <= 9) {
                                    a.push(i + 1);
                                    a.push(i + 2);
                                }
                                if (i == 10) {
                                    a.push(i + 1);
                                    a.push(0);
                                }
                                if (i == 11) {
                                    a.push(i + 1);
                                    a.push(0);
                                }
                            }
                        }
                    }
                    if (value == "1") {
                        if (e[0].options[i].selected == true && month == "") {
                            month = e[0].options[i].value;
                        }
                        else if (e[0].options[i].selected == true) {
                            month = month + ',' + e[0].options[i].value;
                        }

                    }
                }
            }
            if (value == "1") {
                PageMethods.Setsession(month);
            }
            if (unchecked == true && value == "3") {
                e[0].options[value1].selected = false;
                e[0].options[value2].selected = false;
                e[0].options[value3].selected = false;
                sessionStorage.clear();
                PageMethods.Setsession("");
            }
            if (sessionStorage.length != 0 && a.length != 0 && unchecked == false && value == "3") {
                sessionStorage.clear();
                sessionStorage.setItem("a", a[0]);
                sessionStorage.setItem("b", a[1]);
                sessionStorage.setItem("c", a[2]);
                e[0].options[value1].selected = false;
                e[0].options[value2].selected = false;
                e[0].options[value3].selected = false;
                e[0].options[a[0]].selected = true;
                e[0].options[a[1]].selected = true;
                e[0].options[a[2]].selected = true;
                PageMethods.Setsession(e[0].options[a[0]].value + ',' + e[0].options[a[1]].value + ',' + e[0].options[a[2]].value);
            }

            $("[id*=lstmonth]").multiselect('rebuild');
            $("[id*=lstmonth]").multiselect('refresh');
            $('[id*=lstmonth]').multiselect({
                buttonWidth: '100%',
            });
            return false;
        }

        $(document).ready(function () {
            $('[id*=lstmonth]').multiselect({
                buttonWidth: '100%',
            });
        });

    </script>
</asp:Content>


