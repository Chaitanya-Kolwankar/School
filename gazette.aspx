<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="gazette.aspx.cs" Inherits="gazette" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="notify-master/js/notify.js"></script>
    <link href="notify-master/css/notify.css" rel="stylesheet" />
    <script src="jquery/dist/jquery.min.js"></script>
    <script src="fix-header-top/jquery.fixedTableHeader.js"></script>
    <script src="table-navigate/dist/arrow-table.js"></script>
    <%--     <script src="assets/libs/jquery/dist/jquery.min.js"></script>
    <script src="bootstrap-datepicker-1.9.0-dist/js/bootstrap-datepicker.min.js"></script>--%>
    <style>
        .table > thead > tr > th {
            background-color: #0078bc;
            color: white;
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="card">
        <div class="card-header text-white" style="background: linear-gradient(to right, #6f03bf 1%, #363695 100%) !important; border-radius: 7px;">
            <h3>Gazette</h3>
        </div>

        <div class="card-body">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-lg-3">
                            <%--  <asp:CheckBox runat="server" ID="chkformula" AutoPostBack="true" />--%>
                            <input type="checkbox" id="chkformula" name="chkbox" />
                            <asp:Label ID="lblformula" runat="server" for="chkformula">Using Formula</asp:Label>
                        </div>
                        <div class="col-lg-3">
                            <input type="checkbox" id="chk_obtn_mrks" name="chkbox" />
                            <label for="chkbox">Obtained Marks</label><br>
                        </div>
                        <div class="col-md-3">
                            <p>Date:
                                <input type="text" id="datepicker" onclientclick=" loaddate()" autocomplete="off"></p>

                        </div>

                        <div class="col-md-3">
                            <input type="checkbox" id="promoted" name="chkbox" />
                            <asp:Label ID="lbl_promoted" runat="server" for="promoted">Promoted</asp:Label>
                        </div>
                    </div>
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
                            <asp:Label runat="server" ID="lblexam" Text="Exam"></asp:Label><br />
                            <asp:DropDownList runat="server" ID="ddlexam" CssClass="form-control" Enabled="false">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-3">
                            <asp:Label runat="server" ID="lblgrp" Text="Group"></asp:Label><br />
                            <asp:DropDownList runat="server" ID="ddlgrp" CssClass="form-control" Enabled="false">
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label runat="server" ID="lbldiv" Text="Division"></asp:Label><br />
                            <asp:DropDownList runat="server" ID="ddldiv" CssClass="form-control" Enabled="false">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <button class="btn btn-success btn-block" type="button" id="btnrefresh" style="margin-top: 20px;">Refresh</button>
                        </div>
                        <div class="col-md-2">
                            <button class="btn btn-success btn-block" type="button" id="btnget" style="display: none; margin-top: 20px;">Get Data</button>
                        </div>
                        <div class="col-lg-2" id="div_btnexcel">
                            <a id="dlink" style="display: none;"></a>
                            <button class="btn btn-success btn-block" type="button" id="btnexcel" style="margin-top: 20px; display: none">Get Excel</button>
                        </div>
                        <div class="col-md-2">
                            <button class="btn btn-success btn-block" type="button" id="btnresult" style="display: none; margin-top: 20px;">Generate Result</button>
                        </div>

                        <div class="col-md-2" style="margin-top: 26px; display: none;" id="divresult">



                            <input type="checkbox" id="chkresult" name="chkbox" runat="server" />


                            <%--   <asp:CheckBox runat="server" ID="chkresult" AutoPostBack="true" />
                            --%>
                            <asp:Label ID="Label1" runat="server" for="chkresult">For 9th and 10th</asp:Label>

                            <%--        <asp:CheckBox runat="server" ID="chkremark" AutoPostBack="true" />--%>

                            <input type="checkbox" id="chkremark" name="chkbox" />

                            <asp:Label ID="Label2" runat="server" for="chkremark">With Remark</asp:Label>
                        </div>
                    </div>

                    <div class="col-lg-12" id="tblgrid" style="display: none; margin-top: 15px;">
                        <div class="panel">
                            <div class="panel-body">
                                <div id="scroller" class="table-responsive" style="overflow-y: auto; height: 480px; width: 100%;">
                                    <table id="tblfill" class="table table-condensed table-bordered" border="2px" style="padding-left: 0px; padding-right: 0px">
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>

                    <%--<div class="row"   margin-top: 15px;">
                         <div class="col-md-4 col-sm-12 col-xs-12"></div>
                          <div class="col-md-4 col-sm-12 col-xs-12"></div>
                          <div class="col-md-4 col-sm-12 col-xs-12"">
                              <p>Date: <input type="text" id="datepicker"></p>
                          </div>
                         </div>--%>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <script src="jsForms/gazette.js"></script>
    <%--<script src="js/xls.js"></script>--%>
    <script type="text/javascript">
        var urllink = '<%= Session["url"]%>'
        var ayid = '<%=Session["acdyear"] %>'
    </script>
</asp:Content>

