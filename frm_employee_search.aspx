<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frm_employee_search.aspx.cs" Inherits="frm_employee_search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="jquery/dist/jquery.min.js"></script>
    <script src="notify-master/js/notify.js"></script>
    <link href="notify-master/css/notify.css" rel="stylesheet" />
    <link href="bootstrap-toggle-master/css/bootstrap-toggle.min.css" rel="stylesheet" />
    <script src="bootstrap-toggle-master/js/bootstrap-toggle.min.js"></script>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <style>
        .switch {
            position: relative;
            display: inline-block;
            width: 60px;
            height: 34px;
        }

            .switch input {
                opacity: 0;
                width: 0;
                height: 0;
            }

        .slider {
            position: absolute;
            cursor: pointer;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background-color: #ccc;
            -webkit-transition: .4s;
            transition: .4s;
        }

            .slider:before {
                position: absolute;
                content: "";
                height: 26px;
                width: 26px;
                left: 4px;
                bottom: 4px;
                background-color: white;
                -webkit-transition: .4s;
                transition: .4s;
            }

        input:checked + .slider {
            background-color: #2196F3;
        }

        input:focus + .slider {
            box-shadow: 0 0 1px #2196F3;
        }

        input:checked + .slider:before {
            -webkit-transform: translateX(26px);
            -ms-transform: translateX(26px);
            transform: translateX(26px);
        }

        /* Rounded sliders */
        .slider.round {
            border-radius: 34px;
        }

            .slider.round:before {
                border-radius: 50%;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="card">
        <div class="card-header text-white" style="background: linear-gradient(to right, #6f03bf 1%, #363695 100%) !important; border-radius: 7px">
            <div class="row">
                <div class="col-lg-8">
                    <h3>Employee Search</h3>
                </div>
            </div>
        </div>
        <div class="card-body">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-lg-2  col-md-2 col-sm-3">
                            <b>Department</b>
                            <asp:DropDownList CssClass="form-control" ID="ddldepartment" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddldepartment_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-3">

                            <b>Designation</b>
                            <asp:DropDownList CssClass="form-control" ID="ddldesignation" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddldesignation_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-3" style="margin-top: 20px">
                            <asp:Button ID="btnget" runat="server" Text="Get Data" CssClass="btn btn-success btn-block" OnClick="btnget_Click" />
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-3" style="padding-left: 10px; margin-top: 28px">

                            <asp:CheckBox runat="server" ID="chkselect" OnCheckedChanged="chkselect_CheckedChanged" AutoPostBack="true" Style="margin: 2px;" />
                            <asp:Label runat="server" ID="lblsearch" Text="Search by Name" Style="font-weight: bold"></asp:Label>

                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />

            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <div class="row" id="panel1" style="margin: 10px" runat="server">

                        <div class="col-lg-2">
                            First name<br />
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtfname" AutoComplete="off"></asp:TextBox>
                        </div>
                        <div class="col-lg-2">
                            Middle name<br />
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtmname" AutoComplete="off"> </asp:TextBox>
                        </div>
                        <div class="col-lg-2">
                            Last Name<br />
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtlname" AutoComplete="off"></asp:TextBox>
                        </div>

                        <div class="col-lg-2 col-md-2 col-sm-2" style="margin-top: 18px">
                            <asp:Button ID="btnselect" runat="server" Text="Search" CssClass="btn btn-success btn-block" OnClick="btnselect_Click" />

                        </div>

                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <br />


            <div>
                <asp:UpdatePanel ID="updatepanel2" runat="server">
                    <ContentTemplate>
                        <div id="div1" style="max-height: 400px; overflow-y: auto" class="container">
                            <asp:GridView ID="grid1" runat="server" CssClass="table table-bordered table-hover mygrid" HeaderStyle-CssClass="FixedHeader" autopostback="true" OnRowCommand="grid1_RowCommand" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField DataField="emp_id" HeaderText="Employee Id" Visible="false" />
                                    <asp:BoundField DataField="employee_Name" HeaderText="Employee Name" />
                                    <asp:BoundField DataField="username" HeaderText="Username/Employee ID" />
                                    <asp:BoundField DataField="password" HeaderText="Password" />

                                    <asp:TemplateField HeaderText="Delete/Recover">
                                        <ItemTemplate>
                                            <asp:Button ID="btndel" runat="server" CssClass="form-control  btn-block" CommandName="select" Style="max-width: 133px;" CommandArgument="<%# Container.DataItemIndex %>" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle BackColor="#009ACB" ForeColor="White"></HeaderStyle>
                            </asp:GridView>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

    </div>
    <script>


        $(document).on('keypress', '#<%= txtfname.ClientID %>', function (event) {
            var regex = new RegExp("^[a-zA-Z' ]");
            var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
            if (!regex.test(key)) {
                event.preventDefault();
                return false;
            }
        });
        $(document).on('keypress', '#<%= txtmname.ClientID %>', function (event) {
            var regex = new RegExp("^[a-zA-Z']");
            var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
            if (!regex.test(key)) {
                event.preventDefault();
                return false;
            }
        });
        $(document).on('keypress', '#<%= txtlname.ClientID %>', function (event) {
            var regex = new RegExp("^[a-zA-Z']");
            var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
            if (!regex.test(key)) {
                event.preventDefault();
                return false;
            }
        });
    </script>
</asp:Content>

