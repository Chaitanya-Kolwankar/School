<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" EnableEventValidation="true" CodeFile="employee_detail_report.aspx.cs" Inherits="employee_detail_report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        th {
            color: #fff;
        }
    </style>
    <%--<link href="css/bootstrap-multiselect.css" rel="stylesheet" />--%>
    <%--<script src="assets/libs/jquery/dist/jquery.min.js"></script>--%>
    <%--<link href="vendors/bootstrap/bootstrap-multiselect.css" rel="stylesheet" />--%>
    <%--<script src="js/bootstrap-multiselect.js"></script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="card">
        <div class="card-header bg-primary text-white" style="background: linear-gradient(to right,#0078bc 1%,#00beda 100%, transparent); border-radius: 7px">
            <h3><b>Employee Report</b></h3>
        </div>
        <div class="card-body">




            <div class="card-body" style="display: inline; padding-left: 0px; padding-right: 0px; padding-bottom: 0px;">


                <div class="col-md-12">
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                        <ContentTemplate>
                            <div class="row">

                                <div class="col-md-2">
                                    <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="Always" runat="server">
                                        <ContentTemplate>
                                            <asp:Label ID="lblemptype" runat="server">Employee Type</asp:Label>
                                            <asp:DropDownList ID="emptype" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="emptype_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </ContentTemplate>

                                    </asp:UpdatePanel>
                                </div>

                                <div class="col-md-2">
                                    <%--                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                <ContentTemplate>--%>
                                    <asp:Label ID="Label1" runat="server">Select Fields</asp:Label><br />
                                    <asp:ListBox ID="ddlselfield" runat="server" SelectionMode="Multiple" AutoPostBack="false" CssClass="form-control" OnTextChanged="ddlselfield_SelectedIndexChanged" OnSelectedIndexChanged="ddlselfield_SelectedIndexChanged"></asp:ListBox>
                                    <%--</ContentTemplate>
                                            </asp:UpdatePanel>--%>
                                </div>
                                <div class="col-md-2">
                                </div>

                                <div class="col-md-2 " style="padding-top: 20px">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:Button runat="server" OnClick="getdata_Click" ID="getdata" CssClass="btn btn-success form-control" Text="GET DATA" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>



                                <div class="col-md-2 " style="padding-top: 20px">
                                    <asp:Button runat="server" ID="clear" OnClick="clear_Click" CssClass="btn btn-success form-control" Text="CLEAR" />
                                </div>

                                <div class="col-md-2" style="padding-top: 20px">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="excel" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <asp:Button runat="server" OnClick="excel_Click" ID="excel" CssClass="btn btn-success form-control" Text="GET EXCEL" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>

                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <br />
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>


                            <div id="gcard" runat="server" class="table-responsive table-hover" style="margin-top: 20px;">
                                <div class="table-responsive" style="width: 100%; height: 400px; overflow: scroll">
                                    <asp:GridView ID="gvdata" runat="server" CssClass="table table-bordered" AutoGenerateColumns="true" HeaderStyle-BackColor="#009ACB" Style="text-align: center" HeaderStyle-CssClass="FixedHeader">
                                    </asp:GridView>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

            </div>
        </div>
    </div>
    

  
    <script src="jquery/dist/jquery.min.js"></script>

    <script src="js/bootstrap-multiselect.js"></script>



    <script>
        $(document).ready(function () {
            $('.multiselect').css('width', '100%');
            $('.btn-group').css('width', '100%');
            $('.multiselect-container').addClass('table-responsive');
        });
        //$("#ddlselfield").change(function (e) {

    </script>

    <script>

        ddlselmul();
        $('[id*=<%= ddlselfield.ClientID %>]').change(function () {
            ddlselmul();
        });

        function ddlselmul() {
            $('[id*=<%= ddlselfield.ClientID %>]').multiselect({
                includeSelectAllOption: true,
                enableCaseInsensitiveFiltering: true,
                enableFiltering: true,
                maxHeight: 200

            });
            $('.btn-group').css('width', '100%');
            $('.multiselect').css('width', '100%');
        }
        $('[id*=<%= emptype.ClientID %>]').change(function () {
            $('#<%= ddlselfield.ClientID %>').multiselect("clearSelection");
        });








    </script>
    <script>
       
       
    </script>


</asp:Content>

