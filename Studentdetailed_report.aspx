<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Studentdetailed_report.aspx.cs" Inherits="Studentdetailed_report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="jquery/dist/jquery.min.js"></script>

    <script src="notify-master/js/notify.js"></script>
    <link href="notify-master/css/notify.css" rel="stylesheet" />

    <link rel="stylesheet" href="css/bootstrap-multiselect.css" type="text/css" />
    <%--<link  href="datatablen/css/dataTables.bootstrap4.min.css" rel="stylesheet"/>
    <script type="text/javascript" src="datatablen/js/dataTables.bootstrap4.min.js"></script>--%>

    <link href="Datatable/datatables.min.css" rel="stylesheet" />
    <script src="Datatable/datatables.min.js"></script>

    <script type="text/javascript" src="js/bootstrap-multiselect.js"></script>
    <style>
        .btn-group, .multiselect {
            width: 100%
        }

        .row {
            padding: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="card">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <div class="card-header text-white" style="background: linear-gradient(to right, #6f03bf 1%, #363695 100%) !important; border-radius: 7px">
                    <div class="row">
                        <div class="col-md-8">
                            <h3>Student Detailed Rport</h3>
                        </div>

                    </div>
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-3">
                            <label for="ddlmedium" style="color: black; float: left">Medium</label>
                            <div class="input-field">
                                <asp:DropDownList ID="ddlmedium" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlmedium_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <label for="ddlclass" style="color: black; float: left">Class</label>
                            <div class="input-field">
                                <asp:DropDownList ID="ddlclass" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlclass_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <label for="ddldiv" style="color: black; float: left">Division</label>
                            <div class="input-field">
                                <asp:DropDownList ID="ddldiv" CssClass="form-control" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddldiv_SelectedIndexChanged"></asp:DropDownList>

                            </div>
                        </div>
                        <div class="col-md-3">
                            <label for="ddlfield" style="color: black; float: left">Details</label>
                            <div class="input-field">
                                <asp:ListBox ID="ddlfield" runat="server" SelectionMode="Multiple" AutoPostBack="false" CssClass="form-control">
                                    <asp:ListItem Value="[Mother Name]">Mother Name</asp:ListItem>
                                    <asp:ListItem Value="Gender">Gender  </asp:ListItem>
                                    <asp:ListItem Value="[Roll No.]">Roll No.</asp:ListItem>
                                    <asp:ListItem Value="[GR No.]">GR No.</asp:ListItem>
                                    <asp:ListItem Value="[Date of Admission]">Date of Admission</asp:ListItem>
                                    <asp:ListItem Value="[Address]">Address</asp:ListItem>
                                    <asp:ListItem Value="[Mob 1]">Mob 1</asp:ListItem>
                                    <asp:ListItem Value="[Mob 2]">Mob 2</asp:ListItem>
                                    <asp:ListItem Value="[Date of Birth]">Date of Birth</asp:ListItem>
                                    <asp:ListItem Value="[Birth Place]">Birth Place</asp:ListItem>
                                    <asp:ListItem Value="[Mother Tongue]">Mother Tongue</asp:ListItem>
                                    <asp:ListItem Value="[Nationality]">Nationality</asp:ListItem>
                                    <asp:ListItem Value="[Last School Name]">Last School Name</asp:ListItem>
                                    <asp:ListItem Value="[Last studied Name]">Last studied Name</asp:ListItem>
                                    <asp:ListItem Value="Percentage">Percentage</asp:ListItem>
                                    <asp:ListItem Value="Grade">Grade</asp:ListItem>
                                    <asp:ListItem Value="Religion">Religion</asp:ListItem>
                                    <asp:ListItem Value="Category">Category</asp:ListItem>
                                    <asp:ListItem Value="Caste">Caste</asp:ListItem>
                                    <asp:ListItem Value="Subcaste">Subcaste</asp:ListItem>
                                    <asp:ListItem Value="[Aadhar Card]">Aadhar Card</asp:ListItem>
                                    <asp:ListItem Value="[Vehicle Type]">Vehicle Type</asp:ListItem>
                                    <asp:ListItem Value="[Vehicle No]">Vehicle No</asp:ListItem>
                                    <asp:ListItem Value="[Driver Number]">Driver Number</asp:ListItem>
                                    <asp:ListItem Value="[Bank Account No]">Bank Account No</asp:ListItem>
                                    <asp:ListItem Value="[Bank Name]">Bank Name</asp:ListItem>
                                    <asp:ListItem Value="[IFSC Code]">IFSC Code</asp:ListItem>
                                    <asp:ListItem Value="[Branch Name]">Branch Name</asp:ListItem>
                                    <asp:ListItem Value="[Pincode]">Pincode</asp:ListItem>
                                    <asp:ListItem Value="[District]">District</asp:ListItem>
                                    <asp:ListItem Value="[Taluka]">Taluka</asp:ListItem>
                                    <asp:ListItem Value="[State]">State</asp:ListItem>
                                    <asp:ListItem Value="[Saral ID]">Saral ID</asp:ListItem>
                                </asp:ListBox>
                            </div>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-3"></div>
                        <div class="col-md-2">
                            <div class="input-field">
                                <asp:Button ID="btn_get" CssClass="btn btn-success btn-block" runat="server" Text="Get Data" OnClick="btn_get_Click" />
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="input-field">
                                <asp:Button ID="btn_clear" CssClass="btn btn-success btn-block" runat="server" Text="Clear" OnClick="btn_clear_Click" />
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="input-field">
                                <asp:Button ID="btn_excel" CssClass="btn btn-success btn-block" runat="server" Text="Export to Excel" Enabled="false" OnClick="btn_excel_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="card-panel" runat="server" id="grid_card" style="background-color: white; margin-left: 0px; margin-right: 0px; width: 100%">
                            <div class="well well-lg" runat="server" id="grid_show" style="width: 100%;">
                                <asp:GridView ID="grid1" runat="server" Style="" AutoGenerateColumns="true" CssClass="table table-hover table-striped table-bordered mygrid"></asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <script type="text/javascript">
        $(function () {
            $('[id*=ddlfield]').multiselect({
                includeSelectAllOption: true,
                enableCaseInsensitiveFiltering: true,
                enableFiltering: true,
                maxHeight: 200
            });
        });
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('[id*=ddlfield]').multiselect({
                includeSelectAllOption: true,
                enableCaseInsensitiveFiltering: true,
                enableFiltering: true,
                maxHeight: 200
            });
        });

        //On UpdatePanel Refresh
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $('[id*=ddlfield]').multiselect({
                        includeSelectAllOption: true,
                        enableCaseInsensitiveFiltering: true,
                        enableFiltering: true,
                        maxHeight: 200
                    });

                    createDataTable();
                }
            });
        };
    </script>

    <script type="text/javascript">
        createDataTable();
        function createDataTable() {
            $('#<%= grid1.ClientID %>').DataTable();
        }
    </script>


</asp:Content>



