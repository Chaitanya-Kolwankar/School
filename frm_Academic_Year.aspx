<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frm_Academic_Year.aspx.cs" Inherits="frm_Academic_Year" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="notify-master/js/notify.js"></script>
    <link href="notify-master/css/notify.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid">
        <div class="card">
            <div class="card-header text-white" style="background: linear-gradient(to right, #6f03bf 1%, #363695 100%) !important; border-radius: 7px">
                <div>
                    <h3>Academic Year</h3>
                </div>
            </div>
            <div class="card-body">
                <div class="container">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-md-2"></div>
                                <div class="col-md-3">
                                    <label>From</label>
                                    <asp:TextBox ID="txt1" runat="server" CssClass="todate form-control" OnTextChanged="txt1_TextChanged" AutoPostBack="true" MaxLength="10" AutoCompleteType="Disabled"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <label>To</label>
                                    <asp:TextBox ID="txt2" CssClass="fromdt form-control" AutoPostBack="true" Enabled="false" AutoCompleteType="Disabled" MaxLength="10" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="btnAdd" CssClass="btn btn-success form-control" OnClick="btnAdd_Click" runat="server" Text="Add" Style="margin-top: 29px"></asp:Button>&nbsp;&nbsp;    
                                </div>
                                <div class="col-md-2"></div>
                            </div>
                            <div class="row">
                                <div class="col-md-2"></div>
                                <div class="col-md-8" style="height:350px;overflow-y:auto">
                                    <asp:GridView ID="grid" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover mygrid" HeaderStyle-CssClass="FixedHeader" autopostback="true" Width="100%" OnRowDataBound="grid_RowDataBound" Style="text-align: center">
                                        <Columns>
                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblID2" runat="server"
                                                        Text='<%#Eval("AYID")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SR.NO">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblID" runat="server"
                                                        Text='<%#Container.DataItemIndex + 1%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="20%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Duration">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblID1" runat="server"
                                                        Text='<%#Eval("duration")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="60%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="IS CURRENT" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblflag" runat="server"
                                                        Text='<%#Eval("is_current")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Current YEar">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk" runat="server" OnCheckedChanged="chk_CheckedChanged" Checked="false" AutoPostBack="true" Visible="true"></asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div class="col-md-2"></div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-4"></div>
                                <div class="col-md-2">
                                    <asp:Button ID="btnSave" CssClass="btn btn-success form-control" runat="server" Text="Save" OnClick="btnSave_Click"></asp:Button>&nbsp;&nbsp;
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="btnClear" CssClass="btn btn-success form-control" runat="server" Text="Clear" OnClick="btnClear_Click"></asp:Button>
                                </div>
                                <div class="col-md-4"></div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    <script src="assets/libs/jquery/dist/jquery.min.js"></script>
    <script src="bootstrap-datepicker-1.9.0-dist/js/bootstrap-datepicker.min.js"></script>
    <script>
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
            Sys.WebForms.PageRequestManager.getInstance().beginAsyncPostBack();
            function EndRequestHandler(sender, args) {
                $('.todate').datepicker({ format: 'dd/mm/yyyy', autoclose: true });
            }
        });
    </script>
</asp:Content>

