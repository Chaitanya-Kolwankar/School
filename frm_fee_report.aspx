<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frm_fee_report.aspx.cs" Inherits="frm_fee_report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        /*table, td, th {
            border: 1px solid #ddd;
            text-align: center;
        }*/

        table {
            border-collapse: collapse;
            width: 100%;
        }

        th, td {
            padding: 5px;
        }

        th {
            background-color: #0078bc;
            color: white;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid">
        <div class="card">
            <div class="card-header text-white" style="background: linear-gradient(to right, #6f03bf 1%, #363695 100%) !important; border-radius: 7px">
                <h3><b>Fees Report</b></h3>
            </div>
            <div class="card-body">
                <asp:UpdatePanel ID="upd" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-lg-2 col-md-3 col-sm-12 col-xs-12">
                                <b>Medium</b>
                                <asp:DropDownList runat="server" ID="ddl_medium" class="form-control" OnSelectedIndexChanged="ddl_medium_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                            <div class="col-lg-2 col-md-3 col-sm-12 col-xs-12">
                                <b>Class</b>
                                <asp:DropDownList runat="server" ID="ddl_class" class="form-control" OnSelectedIndexChanged="ddl_class_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                            <div class="col-lg-2 col-md-3 col-sm-12 col-xs-12">
                                <b>Division</b>
                                <asp:DropDownList runat="server" ID="ddl_division" class="form-control"></asp:DropDownList>
                            </div>
                            <div class="col-lg-2 col-md-3 col-sm-12 col-xs-12">
                                <b>Report Type</b>
                                <asp:RadioButtonList ID="rdotype" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdotype_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="0" Text="Detailed" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Summaraised"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>

                            <div class="col-lg-4 col-md-3 col-sm-12 col-xs-12" style="    padding-top: 19px;">
                                <div class="row">
                                    <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                        <asp:Button runat="server" class="btn btn-success btn-block" Text="GET DATA" ID="btn_get" OnClick="btn_get_Click" />
                                    </div>
                                   
                                    <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                        <asp:Button runat="server" class="btn btn-success btn-block" Text="CLEAR" ID="btn_clear" OnClick="btn_clear_Click" />
                                    </div>
                                     <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                        <asp:Button runat="server" class="btn btn-success btn-block" Text="GET EXCEL" ID="btn_excel" OnClientClick="exportTableToExcel('tbl_report','Fee Report.xls')" Visible="false" />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row" style="overflow:auto;max-height:450px;margin-top:10px">
                            <asp:Literal ID="tbl_report" runat="server"></asp:Literal>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

    </div>
    <asp:Label runat="server" ID="ermsg"></asp:Label>

    <script>
        function exportTableToExcel(tableID, filename) {
            var downloadLink;
            var dataType = 'application/vnd.ms-excel';
            var tableSelect = document.getElementById(tableID);
            var tableHTML = tableSelect.outerHTML.replace(/ /g, '%20');

            // Specify file name
            filename = filename ? filename : 'excel_data.xls';

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

