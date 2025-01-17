<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frm_Console_Report.aspx.cs" Inherits="frm_Console_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <script src="assets/libs/jquery/dist/jquery.min.js"></script>
    <script src="notify-master/js/notify.js"></script>
    <link href="notify-master/css/notify.css" rel="stylesheet" />
    <link rel="stylesheet" href="css/bootstrap-multiselect.css" type="text/css" />
    <script type="text/javascript" src="js/bootstrap-multiselect.js"></script>


    <style>
      
        .aspNetDisabled .btn .btn-info {
            background-color: #10c5e4;
            opacity: 0.65;
        }

        .btn:disabled {
            opacity: 0.65;
            color: darkred;
        }

        .table-container {
            height: 400px;
            padding: 0.3em;
        }

        table {
            display: flex;
            flex-flow: column;
            height: 100%;
            width: 100%;
        }

            table thead {
                flex: 0 0 auto;
                width: calc(100% - 0.9em);
            }

            table tbody {
                flex: 1 1 auto;
                display: block;
                overflow-y: scroll;
            }

                table tbody tr {
                    width: 100%;
                }

                table thead,
                table tbody tr {
                    display: table;
                    table-layout: fixed;
                }

            table td, table th {
                padding: 1em;
                text-align: center;
            }

        a.disabled:hover {
            cursor: not-allowed;
        }

        .disabled {
            cursor: not-allowed;
        }

        .aspNetDisabled:hover {
            cursor: not-allowed;
        }

        /* Tabs*/
        section {
            padding: 1px 0;
        }

            section .section-title {
                text-align: center;
                color: #007b5e;
                margin-bottom: 50px;
                text-transform: uppercase;
            }

        #tabs {
            /*background: #007b5e;
            background: #00a17b;*/
            color: #eee;
            background: linear-gradient(to right,#0078bc 1%,#00beda 100%, transparent);
            border-radius: 7px;
        }

            #tabs h6.section-title {
                color: #eee;
            }

            #tabs .nav-tabs .nav-item.show .nav-link, .nav-tabs .nav-link.active {
                color: #f3f3f3;
                background-color: transparent;
                border-color: transparent transparent #f3f3f3;
                border-bottom: 4px solid !important;
                font-size: 20px;
                font-weight: bold;
            }

            #tabs .nav-tabs .nav-link {
                border: 1px solid transparent;
                border-top-left-radius: .25rem;
                border-top-right-radius: .25rem;
                color: #eee;
                font-size: 20px;
            }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="card">

        <div class="card-header text-white" style="background: linear-gradient(to right, #6f03bf 1%, #363695 100%) !important; border-radius: 7px;">
            <h3>Console Report</h3>
        </div>
        <div class="card-body">
            <asp:UpdatePanel ID="up_row" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label runat="server" ID="lbl_ddlmedium" Text="Medium"></asp:Label><br />
                            <asp:DropDownList runat="server" ID="ddlmedium" OnSelectedIndexChanged="ddlmedium_SelectedIndexChanged" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                        </div>

                        <div class="col-md-2">
                            <asp:Label runat="server" ID="lbl_ddlstandard" Text="Standard"></asp:Label><br />
                            <asp:DropDownList runat="server" ID="ddlstandard" OnSelectedIndexChanged="ddlstandard_SelectedIndexChanged" CssClass="form-control" AutoPostBack="true" Enabled="false"></asp:DropDownList>
                        </div>

                        <div class="col-md-2">
                            <asp:Label runat="server" ID="lbl_ddlexam" Text="Exam"></asp:Label><br />
                            <asp:DropDownList runat="server" ID="ddlexam" OnSelectedIndexChanged="ddlexam_SelectedIndexChanged" CssClass="form-control" AutoPostBack="true" Enabled="false"></asp:DropDownList>
                        </div>

                        <div class="col-md-2">
                            <asp:Label runat="server" ID="lbl_ddldivision" Text="Division"></asp:Label><br />
                            <asp:DropDownList runat="server" ID="ddldivision" CssClass="form-control" AutoPostBack="true" Enabled="false"></asp:DropDownList>
                        </div>
                      

                        <div class="col-md-2">
                            <br />
                          
                            
                        </div>

                    </div>

                    <br />
                    <div class="row">
                        <asp:Literal ID="ltable" runat="server" Visible="false"></asp:Literal>
                        <asp:Panel ID="Panel1" runat="server"></asp:Panel>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
              <asp:Button runat="server" ID="btn_console_report" Text="Console Report" OnClick="btn_console_report_Click" CssClass="btn btn-success input-block-level" Visible="true" />
            <asp:Button runat="server" ID="btn_excel" OnClick="btn_excel_Click" Text="Excel" CssClass="btn btn-success input-block-level" Visible="false"/>
            <%--<a id="btn_excel" class="btn btn-block btn-info">Get Excel</a>--%>
        </div>
    </div>

      <script type="text/javascript">
          //On Page Load
          $(document).ready(function () {
              $('[id*=ms_sub_name]').multiselect({
                  includeSelectAllOption: true
              });
          });

          //On UpdatePanel Refresh
          var prm = Sys.WebForms.PageRequestManager.getInstance();
          if (prm != null) {
              prm.add_endRequest(function (sender, e) {
                  if (sender._postBackSettings.panelsToUpdate != null) {
                      $('[id*=ms_sub_name]').multiselect({
                          includeSelectAllOption: true
                      });

                  }
              });
          };
    </script>

    <script>
        $(document).ready(function () {
            $('.multiselect-container').css('height', '250px');
            $('.multiselect').css('width', '100%');
            $('.btn-group').css('width', '100%');
            $('.multiselect-container').addClass('table-responsive');
        });
    </script>
</asp:Content>

