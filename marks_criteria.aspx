<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="marks_criteria.aspx.cs" Inherits="marks_criteria" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="notify-master/js/notify.js"></script>
    <link href="notify-master/css/notify.css" rel="stylesheet" />
    <script src="jquery/dist/jquery.min.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="card">
        <div class="card-header text-white" style="background: linear-gradient(to right, #6f03bf 1%, #363695 100%) !important; border-radius: 7px;">
            <h3>Exam Criteria</h3>
        </div>
        <div class="card-body">
            <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>--%>
                    <div class="row">
                        <div class="col-md-4">
                            <input type="checkbox" id="chkprev" name="che" />
                            <label id="lblt0" for="chkprev" runat="server">As Per Previous Year</label><br />
                            <asp:DropDownList ID="ddlprev" CssClass="form-control btn-block" TabIndex="2" runat="server">
                            </asp:DropDownList>

                        </div>
                    </div>
                    <div class="row">

                        <div class="col-md-4">
                            <asp:Label runat="server" ID="lblmedium" Text="Medium"></asp:Label><br />
                            <asp:DropDownList runat="server" ID="ddlmedium" CssClass="form-control" >
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-4">
                            <asp:Label runat="server" ID="lblclass" Text="Standard"></asp:Label><br />
                            <asp:DropDownList runat="server" ID="ddlclass" CssClass="form-control"  Enabled="false">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-4">
                            <asp:Label runat="server" ID="lblexam" Text="Exam"></asp:Label><br />
                            <asp:DropDownList runat="server" ID="ddlexam" CssClass="form-control"  Enabled="false">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label runat="server" ID="lblexmtype" Text="Exam Type"></asp:Label><br />
                            <asp:DropDownList runat="server" ID="ddlexmtype" CssClass="form-control"  Enabled="false">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-4">
                            <asp:Label runat="server" ID="lblsub" Text="Subject"></asp:Label><br />
                            <asp:DropDownList runat="server" ID="ddlsub" CssClass="form-control" Enabled="false">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2" style="margin-top: 18px;">
                            <button class="btn btn-success btn-block" type="button" id="btnrefresh">Refresh</button>
                        </div>
                        <div class="col-md-2" style="margin-top: 18px;">
                            <button class="btn btn-success btn-block" type="button" id="btnsave" style="display: none;">Save</button>
                        </div>

                    </div>
                    <br />
                    <%--<div id="div_tbl" runat="server"></div>--%>
                    <div class="row">
                        <div class="col-md-6" id="tblgrid" style="display: none;">


                            <div class="panel">
                                <div class="panel-body">
                                    <div id="scroller" style="overflow-y: auto; height: 450px; width: 100%;">
                                        <table id="tblfill" style="padding-left: 0px; padding-right: 0px; width: 100%">
                                        </table>
                                    </div>
                                </div>
                            </div>



                        </div>

                        <div class="col-lg-6" id="calculator" style="display: none">
                            <div class="container">
                                <div class="row">
                                    <div class="col-lg-2">
                                    </div>
                                    <div class="col-lg-2">
                                        <button class="btn btn-block" type="button" id="btnropen" value="(" style="margin-top: 20px;">(</button>
                                    </div>
                                    <div class="col-lg-2">
                                        <button class="btn btn-block" type="button" id="btnrclose" value=")" style="margin-top: 20px;">)</button>
                                    </div>
                                    <div class="col-lg-2">
                                        <button class="btn btn-block" type="button" id="btnplus" value="+" style="margin-top: 20px;">+</button>
                                    </div>
                                    <div class="col-lg-2">
                                        <button class="btn btn-block" type="button" id="btnminus" value="-" style="margin-top: 20px;">-</button>
                                    </div>
                                    <div class="col-lg-2">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-2">
                                    </div>
                                    <div class="col-lg-2">
                                        <button class="btn btn-block" type="button" id="btnone" value="1" style="margin-top: 20px;">1</button>
                                    </div>
                                    <div class="col-lg-2">
                                        <button class="btn btn-block" type="button" id="btntwo" value="2" style="margin-top: 20px;">2</button>
                                    </div>
                                    <div class="col-lg-2">
                                        <button class="btn btn-block" type="button" id="btnthree" value="3" style="margin-top: 20px;">3</button>
                                    </div>
                                    <div class="col-lg-2">
                                        <button class="btn btn-block" type="button" id="btnmod" value="%" style="margin-top: 20px;">%</button>
                                    </div>
                                    <div class="col-lg-2">
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-lg-2">
                                    </div>
                                    <div class="col-lg-2">
                                        <button class="btn btn-block" type="button" id="btnfour" value="4" style="margin-top: 20px;">4</button>
                                    </div>
                                    <div class="col-lg-2">
                                        <button class="btn btn-block" type="button" id="btnfive" value="5" style="margin-top: 20px;">5</button>
                                    </div>
                                    <div class="col-lg-2">
                                        <button class="btn btn-block" type="button" id="btnsix" value="6" style="margin-top: 20px;">6</button>
                                    </div>
                                    <div class="col-lg-2">
                                        <button class="btn btn-block" type="button" id="btndivide" value="/" style="margin-top: 20px;">/</button>
                                    </div>
                                    <div class="col-lg-2">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-2">
                                    </div>
                                    <div class="col-lg-2">
                                        <button class="btn btn-block" type="button" id="btnseven" value="7" style="margin-top: 20px;">7</button>
                                    </div>
                                    <div class="col-lg-2">
                                        <button class="btn btn-block" type="button" id="btneight" value="8" style="margin-top: 20px;">8</button>
                                    </div>
                                    <div class="col-lg-2">
                                        <button class="btn btn-block" type="button" id="btnnine" value="9" style="margin-top: 20px;">9</button>
                                    </div>
                                    <div class="col-lg-2">
                                        <button class="btn btn-block" type="button" id="btnmultiply" value="*" style="margin-top: 20px;">*</button>
                                    </div>
                                    <div class="col-lg-2">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-2">
                                    </div>
                                    <div class="col-lg-2">
                                        <button class="btn btn-block" type="button" id="btnequal" value="=" style="margin-top: 20px;">=</button>
                                    </div>
                                    <div class="col-lg-2">
                                        <button class="btn btn-block" type="button" id="btnzero" value="0" style="margin-top: 20px;">0</button>
                                    </div>
                                    <div class="col-lg-4">
                                        <button class="btn btn-block" type="button" id="btnclear" style="margin-top: 20px;">Clear</button>
                                    </div>
                                    <div class="col-lg-2">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

               <%-- </ContentTemplate>
            </asp:UpdatePanel>--%>
        </div>
    </div>
    <script src="jsForms/marks_criteria.js"></script>

    <script type="text/javascript">
        var urllink = '<%= Session["url"]%>'
        var ayid = '<%=Session["acdyear"] %>'
    </script>
</asp:Content>

