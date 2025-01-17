<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frm_grade_master.aspx.cs" Inherits="frm_grade_master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="card-header text-white" style="background: linear-gradient(to right, #6f03bf 1%, #363695 100%) !important; border-radius: 7px">
                <h3>Grade Master</h3>
            </div>
            <div class="card card-body">
                <asp:UpdatePanel runat="server" ID="up1">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-md-2">
                                Medium:<asp:DropDownList ID="ddlmedium" runat="server" TabIndex="1" CssClass="form-control" OnSelectedIndexChanged="ddlmedium_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                Class:<asp:DropDownList ID="ddlclass" runat="server" TabIndex="2" CssClass="form-control" OnSelectedIndexChanged="ddlclass_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="col-md-3">
                                        <br />
                                        <asp:Button ID="btn_Add" Text="Add" CssClass="btn btn-success form-control" runat="server" OnClick="btn_Add_Click" TabIndex="4" />
                                    </div>
                                    <div class="col-md-3">
                                        <br />
                                        <asp:Button ID="btn_Save" Text="Save" CssClass="btn btn-success form-control" runat="server" OnClick="btn_Save_Click" TabIndex="4" />
                                    </div>
                                    <div class="col-md-3">
                                        <br />
                                        <asp:Button ID="btnprev" runat="server" Text="Copy as Class" CssClass="btn btn-info form-control" data-target="#myModal" data-toggle="modal" data-backdrop="static" data-keyboard="false" OnClick="btnprev_Click" />
                                    </div>
                                    <div class="col-md-3">
                                        <br />
                                        <asp:Button ID="btnclear" runat="server" Text="Clear" CssClass="btn btn-info form-control" OnClick="btnclear_Click" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-2">
                                <asp:CheckBox ID="chkprev" runat="server" OnCheckedChanged="chkprev_CheckedChanged" Text="As Per Previous Year" AutoPostBack="true" Enabled="false" />
                                <div id="divprev" runat="server" style="display: none">

                                    <asp:DropDownList runat="server" ID="ddlprevyear" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlprevyear_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="row" style="padding-top: 20px">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Always" style="width: 100%; overflow: auto">
                        <ContentTemplate>
                            <asp:GridView ID="grd_grade" runat="server" Font-Size="12pt" Style="text-align: center;" AutoGenerateColumns="False" CssClass="table table-bordered table-hover mygrid" HeaderStyle-CssClass="FixedHeader">
                                <Columns>
                                    <asp:TemplateField HeaderText="Lower (%)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblmin" CssClass="label-default" runat="server" Text='<%# Eval("min")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtmin" CssClass="col-md-5" autocomplete="off" onkeypress="return percentage(event)" MaxLength="8" runat="server" Text='<%# Eval("min")%>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemStyle Width="15%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Higher (%)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblmax" CssClass="label-default" runat="server" Text='<%# Eval("max")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtmax" CssClass="col-md-5" autocomplete="off" onkeypress="return percentage(event)" MaxLength="8" runat="server" Text='<%# Eval("max")%>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemStyle Width="15%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Grade">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgrade" CssClass="label-default" runat="server" Text='<%# Eval("grade")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtgrade" CssClass="col-md-5" autocomplete="off" onkeypress="return grade(event)" MaxLength="3" runat="server" Style="text-transform: uppercase" Text='<%# Eval("grade")%>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemStyle Width="15%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remark">
                                        <ItemTemplate>
                                            <asp:Label ID="lblremark" CssClass="label-default" runat="server" Text='<%# Eval("remark")%>' Style="word-wrap: break-word;"></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtremark" CssClass="col-md-5" autocomplete="off" onkeypress="return remark(event)" runat="server" MaxLength="50" Style="text-transform: uppercase" Text='<%# Eval("remark")%>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemStyle Wrap="true" Width="25%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="EDIT">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnedit" runat="server" OnClick="btnedit_Click" CommandName="Edit" Text="<i class='fas fa-edit' aria-hidden='true'></i>" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:LinkButton ID="btnupdate" Text="<i class='fa fa-save' style='font-size:24px'></i>" CommandName="Update" runat="server" OnClick="btnupdate_Click" />
                                            &nbsp;&nbsp;
                                        <asp:LinkButton ID="btncancel" Text="<i class='fa fa-times-circle' style='font-size:24px'></i>" runat="server" CommandName="Cancel" OnClick="btncancel_Click" />
                                        </EditItemTemplate>
                                        <ItemStyle Width="15%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DELETE">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btndelete" runat="server" OnClientClick="Confirmdiv()" Text="<i class='fas fa-trash' aria-hidden='true'></i>" OnClick="btndelete_Click"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Width="15%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="FLAG" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblflag" CssClass="label-default" runat="server" Text='<%# Eval("flag")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="GradeID" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgradeid" CssClass="label-default" runat="server" Text='<%# Eval("grade_id")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle BackColor="#009ACB" ForeColor="White"></HeaderStyle>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    <div class="modal" id="myModal">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Define Grade</h5>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-6">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                <ContentTemplate>
                                    From Class :
                            <asp:DropDownList ID="ddlfrmclass" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlfrmclass_SelectedIndexChanged"></asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="col-md-6">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    To Class : 
                            <asp:ListBox ID="lstclass" runat="server" SelectionMode="Multiple" CssClass="btn form-control col-md-12" multiple="multiple"></asp:ListBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server" style="overflow: auto">
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
                                            <asp:TemplateField HeaderText="Lower">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbllow" runat="server" Text='<%# Eval("min")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="30%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Higher">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblhigh" runat="server" Text='<%# Eval("max")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="30%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Grade">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrade" runat="server" Text='<%# Eval("grade")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="30%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remark">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblremark" runat="server" Text='<%# Eval("remark")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="30%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="GradeID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrdid" CssClass="label-default" runat="server" Text='<%# Eval("grade_id")%>'></asp:Label>
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
                                    <asp:Button ID="btnsaveprev" runat="server" Text="Save" CssClass="btn btn-success" OnClick="btnsaveprev_Click" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:Button ID="btncancelstd" runat="server" Text="Cancel" CssClass="btn btn-info" data-dismiss="modal" Style="color: white;"></asp:Button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal" id="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Define Grade</h5>
                </div>
                <div class="modal-body">

                    <img src="image/error.gif" style="height: 500px; width: 500px" />
                </div>
            </div>
        </div>
    </div>

    <script>
        function grade(e) {
            isIE = document.all ? 1 : 0
            keyEntry = !isIE ? e.which : event.keyCode;
            if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || ((keyEntry >= '48') && (keyEntry <= '57')) || (keyEntry == '43'))
                return true;
            else {
                return false;
            }
        }
        function remark(e) {
            isIE = document.all ? 1 : 0
            keyEntry = !isIE ? e.which : event.keyCode;
            if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || ((keyEntry >= '48') && (keyEntry <= '57')) || (keyEntry == '44') || (keyEntry == '45') || (keyEntry == '46') || (keyEntry == '32'))
                return true;
            else {
                return false;
            }
        }
        function percentage(e) {
            isIE = document.all ? 1 : 0
            keyEntry = !isIE ? e.which : event.keyCode;
            if (((keyEntry >= '48') && (keyEntry <= '57')) || (keyEntry == '46'))
                return true;
            else {
                return false;
            }
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('[id*=lstclass]').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200,
                buttonWidth: '100%'
            });
        });

        //On UpdatePanel Refresh
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $('[id*=lstclass]').multiselect({
                        includeSelectAllOption: true,
                        maxHeight: 200,
                        buttonWidth: '100%'
                    });
                }
            });
        };
        function multi() {
            $('[id*=lstclass]').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200,
                buttonWidth: '100%'
            });
        }


        function Confirmdiv() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "grade";
            confirm_value.value = "";
            if (confirm("Do you want to delete the selected Grade ?")) {
                confirm_value.value = "OK";
            }
            else {
                confirm_value.value = "Cancel";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
</asp:Content>

