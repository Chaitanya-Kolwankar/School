<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frm_student_acadmic_record.aspx.cs" Inherits="frm_student_acadmic_record" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="bootstrap-4.0.0-dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        table {
            border-collapse: collapse;
            border: 0;
            rules: none;
        }

        .modal-lg {
            max-width: 80% !important;
        }

        td {
            text-align: left;
            border: 0;
            padding: 8px;
            vertical-align: central;
        }

        /*th {
            color:#fff;
            background: #0094ff;
        }*/



        tr:nth-child(even) {
            background-color: #f2f2f2;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid">
        <div class="card" style="width: 100%;">

            <div class="card-header text-white" style="background-color: #0078bc; border-radius: 7px; background: linear-gradient(to right,#0078bc 1%,#00beda 100%, transparent); border-radius: 7px">
                <div>
                    <h3>Student Academic Record</h3>
                </div>
            </div>
            <div class="card-body">
                <div class="row">
                    <div class="col-lg-2">
                        ID
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:TextBox runat="server" ID="id" OnTextChanged="id_TextChanged"  AutoPostBack="TRUE" CssClass="form-control" AutoCompleteType="Disabled" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-lg-3">
                        Name (Surname Firstname)
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <asp:TextBox runat="server" CssClass="form-control" ID="name" AutoPostBack="true" OnTextChanged="name_TextChanged" AutoCompleteType="Disabled"/>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="container-fluid">
                    <div style="margin-top: 40px">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="GridView2" CssClass="form-group table table-bordered" Width="100%" runat="server" OnRowCommand="GridView2_RowCommand" ForeColor="#333333" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:BoundField DataField="student_id" HeaderText="Student id" />
                                        <asp:BoundField DataField="student" HeaderText="Student Name" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button ID="Button1" CssClass="btn btn-success " Text="Select"   runat="server" CommandName="select" CommandArgument="<%# Container.DataItemIndex %>" data-toggle="modal" data-target="#myModal" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle BackColor="#009ACB"  ForeColor="White"  ></HeaderStyle>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div id="myModal" class="modal fade" role="dialog">
                            <div class="modal-dialog modal-lg" role="document">
                                <!-- Modal content-->
                                <div class="modal-content">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <div class="modal-body" style="overflow-y: scroll;">
                                                <div>
                                                    <b>Student Name  :</b>
                                                    <asp:Label runat="server" ID="lbl1"></asp:Label>
                                                    <br />
                                                </div>
                                                <div>
                                                    <b>Student ID    :</b>
                                                    <asp:Label runat="server" ID="Label1"></asp:Label>
                                                    <br />
                                                </div>
                                                <div>
                                                    <b>Academic year      :</b>
                                                    <asp:Label runat="server" ID="Label2"></asp:Label>
                                                </div>
                                                <br />
                                                <br />
                                                <asp:GridView runat="server" CssClass="table table-bordered" ID="grd_report">
                                                </asp:GridView>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                                </div>
                                <div class="modal-footer">
                                    <asp:Button runat="server" CssClass="btn btn-default" data-dismiss="modal" Text="Close" />
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="jquery/dist/jquery.min.js"></script>
  
    <script>
        
        $(document).on('keypress', '#<%= id.ClientID %>', function (event) {
            var regex = new RegExp("^[0-9a-zA-Z]+$");
            var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
            if (!regex.test(key)) {
                event.preventDefault();
                return false;
            }
        });
        $(document).on('keypress', '#<%= name.ClientID %>', function (event) {
            var regex = new RegExp("^[a-zA-Z' ]+$");
            var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
            if (!regex.test(key)) {
                event.preventDefault();
                return false;
            }
            
        });
    </script>
</asp:Content>

