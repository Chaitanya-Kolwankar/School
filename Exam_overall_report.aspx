<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Exam_overall_report.aspx.cs" Inherits="Exam_overall_report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link href="css/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="assets/libs/jquery/dist/jquery.min.js"></script>
    <link href="notify-master/css/notify.css" rel="stylesheet" />

    <link rel="stylesheet" href="css/bootstrap-multiselect.css" type="text/css" />
    <script type="text/javascript" src="js/bootstrap-multiselect.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <div class="container-fluid">
            <div class="col-md-12">
                <div class="card-header" style="background: linear-gradient(to right,#0078bc 1%,#00beda 100%) !important;">
                    <span style="color: white; font-size: 20px;">Exam Reports 
                    </span>
                </div>

                <div class="card card-body">

                    <asp:UpdatePanel ID="up_row" runat="server">
                        <ContentTemplate>

                            <div class="row">
                                <div class="col-md-2">
                                    <asp:RadioButton ID="rd_report1" runat="server" AutoPostBack="true" GroupName="report" OnCheckedChanged="rd_report1_CheckedChanged" Checked="false" Text="Class wise grade Report" />
                                </div>
                                <div class="col-md-2">
                                    <asp:RadioButton ID="rd_report2" runat="server" AutoPostBack="true" GroupName="report" OnCheckedChanged="rd_report2_CheckedChanged" Checked="false" Text="Overall Class Wise Report" />
                                </div>
                                <div class="col-md-3">
                                    <asp:RadioButton ID="rd_report3" runat="server" AutoPostBack="true" GroupName="report" OnCheckedChanged="rd_report3_CheckedChanged" Checked="false" Text="Class wise with All subject and Grade" />
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label runat="server" ID="lbl_ddlmedium" Text="Medium" Visible="false"></asp:Label><br />
                                    <asp:DropDownList runat="server" ID="ddlmedium" OnSelectedIndexChanged="ddlmedium_SelectedIndexChanged" CssClass="form-control" AutoPostBack="true" Visible="false"></asp:DropDownList>
                                </div>

                                <div class="col-md-2">
                                    <asp:Label runat="server" ID="lbl_ddlstandard" Text="Standard" Visible="false"></asp:Label><br />
                                    <asp:DropDownList runat="server" ID="ddlstandard" OnSelectedIndexChanged="ddlstandard_SelectedIndexChanged" CssClass="form-control" AutoPostBack="true" Enabled="false" Visible="false"></asp:DropDownList>


                                    <asp:ListBox ID="lst_standard" runat="server" SelectionMode="Multiple" multiple="multiple" CssClass="btn form-control col-md-12" Visible="false"></asp:ListBox>
                                </div>

                                <div class="col-md-2">
                                    <asp:Label runat="server" ID="lbl_ddlexam" Text="Exam" Visible="false"></asp:Label><br />
                                    <asp:DropDownList runat="server" ID="ddlexam" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlexam_SelectedIndexChanged" Enabled="false" Visible="false"></asp:DropDownList>
                                    <asp:DropDownList runat="server" ID="ddl_exam_new" CssClass="form-control" AutoPostBack="true" Enabled="false" Visible="false">
                                        <asp:ListItem>----SELECT----</asp:ListItem>
                                        <asp:ListItem>Term I</asp:ListItem>
                                        <asp:ListItem>Term II</asp:ListItem>
                                    </asp:DropDownList>
                                </div>


                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <br />
                    <div class="row">
                       
                        <div class="col-md-3">
                            <asp:Button runat="server" ID="btnexcel" Text="GET EXCEL" OnClick="btnexcel_Click" CssClass="btn btn-success" Width="70%" />
                        </div>
                        <div class="col-md-3">
                            <asp:Button ID="btnclear" runat="server" Text="CLEAR" CssClass="btn btn-success" Autopostback="true" Width="70%" />
                        </div>
                        <div class="col-md-3"></div>
                         <div class="col-md-3"></div>
                    </div>
                </div>

            </div>
        </div>
    </div>
    <%-----------------------------------------------------------------------------------------------------------------------------%>
    <script type="text/javascript">
        $(function () {
            $('[id*=lst_standard]').multiselect({
                includeSelectAllOption: true,
                enableCaseInsensitiveFiltering: true,
                enableFiltering: true,
                maxHeight: 200
            });
        });
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('[id*=lst_standard]').multiselect({
                includeSelectAllOption: true
            });
        });

        //On UpdatePanel Refresh
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $('[id*=lst_standard]').multiselect({
                        includeSelectAllOption: true
                    });
                }
            });
        };
    </script>
</asp:Content>

