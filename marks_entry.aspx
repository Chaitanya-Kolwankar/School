<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="marks_entry.aspx.cs" Inherits="marks_entry" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   
    <link href="notify-master/css/notify.css" rel="stylesheet" />
    <script src="jquery/dist/jquery.min.js"></script>
    <script src="fix-header-top/jquery.fixedTableHeader.js"></script>
    <script src="table-navigate/dist/arrow-table.js"></script>
    <style>
        .table > thead > tr > th {
            background-color: #0078bc;
            color: white
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="card">
        <div class="card-header text-white" style="background: linear-gradient(to right, #6f03bf 1%, #363695 100%) !important; border-radius: 7px;">
            <h3>Marks Entry</h3>
        </div>
        <div class="card-body">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="row">

                        <div class="col-md-3">
                            <asp:Label runat="server" ID="lblmedium" Text="Medium"></asp:Label><br />
                            <asp:DropDownList runat="server" ID="ddlmedium" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-3">
                            <asp:Label runat="server" ID="lblclass" Text="Standard"></asp:Label><br />
                            <asp:DropDownList runat="server" ID="ddlclass" CssClass="form-control" Enabled="false">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-3">
                            <asp:Label runat="server" ID="lbldiv" Text="Division"></asp:Label><br />
                            <asp:DropDownList runat="server" ID="ddldiv" CssClass="form-control"  Enabled="false">
                            </asp:DropDownList>
                        </div>
                             <div class="col-md-3">
                            <asp:Label runat="server" ID="lblgrp" Text="Group"></asp:Label><br />
                            <asp:DropDownList runat="server" ID="ddlgrp" CssClass="form-control"  Enabled="false">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <asp:Label runat="server" ID="lblexam" Text="Exam"></asp:Label><br />
                            <asp:DropDownList runat="server" ID="ddlexam" CssClass="form-control" Enabled="false">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-3">
                            <asp:Label runat="server" ID="lblsub" Text="Subject"></asp:Label><br />
                            <asp:DropDownList runat="server" ID="ddlsub" CssClass="form-control"  Enabled="false">
                            </asp:DropDownList>
                        </div>
                         <div class="col-md-2">
                            <button class="btn btn-success btn-block" type="button" id="btnrefresh" style="margin-top: 20px;">Refresh</button>
                        </div>
                        <div class="col-md-2">
                            <button class="btn btn-success btn-block" type="button" id="btnsave" style="display: none; margin-top: 20px;">Save</button>
                        </div>

                    </div>

                    <div class="col-lg-12" id="tblgrid" style="display: none; margin-top: 15px;">
                        <div class="panel">
                            <div class="panel-body">
                                <div id="scroller" class="table-responsive" style="overflow-y: auto; height: 450px; width: 100%;">
                                    <table id="tblfill" class="table table-condensed table-bordered" border="2px" style="padding-left: 0px; padding-right: 0px">
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-3">
                            <div class="table-responsive" style="height: 550px; width: 100%; display: none;">
                                <table id="exceldata" class="table table-condensed table-bordered" border="2px">
                                </table>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                          <div class="col-lg-3"></div>
                        <div class="col-lg-2">
                            <a id="dlink" style="display: none;"></a>
                            <button class="btn btn-success btn-block" type="button" id="btnexcel" style="margin-top: 20px; display: none">Get Excel</button>
                        </div>
                        <div class="col-lg-2">
                            <button class="btn btn-success btn-block" type="button" id="btnimport" style="margin-top: 20px; display: none">Import</button>
                        </div>
                        <div class="col-lg-3">
                           <%-- &nbsp  Select Excel File--%>
                                    <input type="file"  id="excfile" class="form-control btn-block" style="margin-top: 20px;display: none" />
                        </div>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
     <script src="notify-master/js/notify.js"></script>
    <script src="jsForms/marks_entry.js"></script>
    <script src="js/xls.js"></script>
    <script type="text/javascript">
        var urllink = '<%= Session["url"]%>'
        var ayid = '<%=Session["acdyear"] %>'
    </script>
</asp:Content>

