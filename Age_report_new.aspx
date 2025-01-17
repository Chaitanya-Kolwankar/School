<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Age_report_new.aspx.cs" Inherits="Age_report_new" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/jquery.datetimepicker.css" rel="stylesheet" />
    <script src="jquery/dist/jquery.min.js"></script>

    <script src="notify-master/js/notify.js"></script>
    <link href="notify-master/css/notify.css" rel="stylesheet" />
    <style>
        .tdbg {
            background-color: lightgrey;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid">
        <div class="card-header text-white" style="background: linear-gradient(to right, #6f03bf 1%, #363695 100%) !important; border-radius: 7px;">
            <h3>Age Report</h3>
        </div>
        <div class="card card-body">
            <div class="row">
                <div class="col-lg-6">
                    <div class="row">
                        <div class="col-lg-6">
                            <span>Medium</span>
                            <asp:DropDownList runat="server" ID="ddl_medium" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-lg-6">
                            <span>From Date</span>
                            <asp:TextBox runat="server" ID="dateprint" CssClass="datepickerdob form-control" />
                        </div>
                    </div>
                </div>
                <div class="col-lg-6">
                    <asp:UpdatePanel ID="up" runat="server" class="col-lg-6" style="padding-top: 20px">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-lg-6">
                                    <asp:Button runat="server" ID="btn_view" Text="View" OnClick="btn_view_Click" CssClass="btn btn-success form-control" autopostback="true " />
                                </div>
                                <div class="col-lg-6">
                                    <asp:Button runat="server" ID="btnGetExcel" Style="display: none" OnClientClick="exportTableToExcel('tblfees','Agereport.xls')" Text="Export" CssClass="btn btn-success form-control" Autopostback="true" />
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>


                </div>
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>


                        <div class="row" style="padding-top: 100px">
                            <asp:Literal ID="tblfees" runat="server">
               
                            </asp:Literal>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <script src="bootstrap-datepicker-1.9.0-dist/js/bootstrap-datepicker.min.js"></script>
    <%--<script src="jsForms/agereport.js"></script>--%>
    <script>

        $('.datepickerdob').datepicker({
            format: "dd/mm/yyyy",
            orientation: "bottom"
        });


    </script>
    <script>
        function exportTableToExcel(tableID, filename) {
            var downloadLink;
            var dataType = 'application/vnd.ms-excel';
            var tableSelect = document.getElementById(tableID);
            var tableHTML = tableSelect.outerHTML.replace(/ /g, '%20');

            // Specify file name
            filename = filename ? filename + '.xls' : 'excel_data.xls';

            // Create download link element
            downloadLink = document.createElement("a");

            document.body.appendChild(downloadLink);

            if (navigator.msSaveOrOpenBlob) {
                var blob = new Blob(['\ufeff', tableHTML], {
                    type: dataType
                });
                navigator.msSaveOrOpenBlob(blob, filename);
            } else {
                // Create a link to the file
                downloadLink.href = 'data:' + dataType + ', ' + tableHTML;

                // Setting the file name
                downloadLink.download = filename;

                //triggering the function
                downloadLink.click();
            }
        }
    </script>
</asp:Content>

