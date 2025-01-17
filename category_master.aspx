<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="category_master.aspx.cs" Inherits="category_master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
            text-align: center;
            border: 0;
            padding: 8px;
            vertical-align: central;
        }

        th {
            text-align: center;
            color: #fff;
        }



        tr:nth-child(even) {
            background-color: #f2f2f2;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid">
        <div class="card-header text-white" style="background: linear-gradient(to right, #6f03bf 1%, #363695 100%) !important; border-radius: 7px;">
            <div>
                <h3>Category Master</h3>
            </div>
        </div>

        <div class="card card-body">
            <div id="Div1">
                <div class="card">
                    <div class="card-header text-white" style="background: linear-gradient(to right, #6f03bf 1%, #363695 100%) !important; border-radius: 7px; padding-top: 0px; padding-bottom: 0px;" id="headingOne">
                        <h5 class="mb-0">
                            <a class="btn btn-link" data-toggle="collapse" data-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne">Category
                            </a>
                        </h5>
                    </div>

                    <div id="collapseOne" class="collapse" aria-labelledby="headingOne" data-parent="#Div1">
                        <div class="card-body">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-md-3" style="margin-top: 22px">
                                            <asp:Button runat="server" ID="btnad" Text="ADD" CssClass="btn btn-success " OnClick="Unnamed_Click1" />
                                        </div>

                                        <div class="col-md-3" id="hi1" runat="server">
                                            Add Category
                                            <asp:TextBox runat="server" CssClass="form-control"  style="text-transform: uppercase;border: 1px solid;" ID="categoryt" AutoCompleteType="Disabled" />
                                        </div>
                                        <div class="col-md-3" style="margin-top: 19px;" id="h2" runat="server">
                                            <asp:Button runat="server" Text="Save" OnClick="Unnamed_Click" CssClass="btn btn-success form-control" />
                                        </div>
                                    </div>

                                    <div class="container" style="margin-top: 10px">
                                        <asp:GridView runat="server" ID="grid1" Width="100%" CssClass="form-group" AutoGenerateColumns="false" OnRowEditing="grid1_RowEditing">
                                            <Columns>
                                                <%--<asp:BoundField DataField="category_name" HeaderText="category" />--%>
                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ll" runat="server" Text='<%# Eval("flag")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl1" runat="server" Text='<%# Eval("category_id")%>' Style="margin: 9px;"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="category">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgroup" runat="server" Text='<%# Eval("category_name")%>' Style="margin: 9px;"></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox runat="server"  onkeypress="return bank(event)" ID="hi" Text='<%# Eval("category_name")%>' />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton1" Text="<i class='fas fa-edit' aria-hidden='true'></i>" runat="server" CommandName="Edit" Font-Size="17px" />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:LinkButton ID="LinkButton2" Text="<i class='fa fa-save' aria-hidden='true' ></i>" runat="server" OnClick="OnUpdate" Font-Size="17px" />
                                                        <asp:LinkButton ID="LinkButton3" Text="<i class='fa fa-times-circle'></i>" runat="server" OnClick="OnCancel" Font-Size="17px" />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="delet" runat="server" CommandName="delet" OnClientClick="Confirm()" OnClick="delet_Click"  Font-Size="17px"><i class='fa fa-trash' aria-hidden='true'></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>

                <div class="card">
                    <div class="card-header text-white" style="background: linear-gradient(to right, #6f03bf 1%, #363695 100%) !important; border-radius: 7px; padding-top: 0px; padding-bottom: 0px;" id="headingTwo">
                        <h5 class="mb-0">
                            <a class="btn btn-link collapsed" data-toggle="collapse" data-target="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">Cast
                            </a>
                        </h5>
                    </div>
                    <div id="collapseTwo" class="collapse" aria-labelledby="headingTwo" data-parent="#Div1">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div class="card card-body">
                                    <div class="row">
                                        <div class="col-md-2" style="margin-top: 19px">
                                            <asp:Button runat="server" ID="dd" CssClass="btn btn-success" Text="ADD" OnClick="dd_Click" />
                                        </div>
                                        <div class="col-md-2">
                                            <asp:Label runat="server" ID="dd1" Text="Category" />
                                            <asp:DropDownList ID="ddlmedium" CssClass="form-control" AutoPostBack="true" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:Label runat="server" ID="dd2" Text="Cast" />
                                            <asp:TextBox runat="server" CssClass="form-control" ID="cast" style="text-transform: uppercase;border: 1px solid;" AutoCompleteType="Disabled" />
                                        </div>
                                        <div class="col-md-2" style="margin-top: 19px;">
                                            <asp:Button runat="server" ID="castbtn" CssClass="btn btn-success form-control" Text="save" OnClick="castbtn_Click" />
                                        </div>
                                    </div>
                                    <div class="container" style="margin-top: 20px">
                                        <asp:GridView runat="server" ID="grd2" Width="100%" CssClass="form-group" AutoGenerateColumns="false" OnRowEditing="grd2_RowEditing">
                                            <Columns>
                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="l1" runat="server" Text='<%# Eval("flag")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cast Name">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="cst_name" Text='<%# Eval("cast_name")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox runat="server" ID="cst_name1"  onkeypress="return bank(event)" Text='<%# Eval("cast_name")%>' />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cast id" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("cast_id")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="Link1" Text="<i class='fas fa-edit' aria-hidden='true'></i>" runat="server" CommandName="Edit" Font-Size="17px" />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:LinkButton ID="Link2" Text="<i class='fa fa-save' aria-hidden='true' ></i>" runat="server" OnClick="Link2_Click" Font-Size="17px" />
                                                        <asp:LinkButton ID="Link3" Text="<i class='fa fa-times-circle'></i>" runat="server" OnClick="Link3_Click" Font-Size="17px" />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="delet" runat="server" CommandName="delet" OnClientClick="Confirm()" OnClick="delet_Click1" Font-Size="17px"><i class='fa fa-trash' aria-hidden='true'></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </div>
                </div>

                <div class="card">
                    <div class="card-header text-white" style="background: linear-gradient(to right,#0078bc 1%,#00beda 100%, transparent); border-radius: 7px; padding-top: 0px; padding-bottom: 0px;" id="headingThree">
                        <h5 class="mb-0">
                            <a class="btn btn-link collapsed" data-toggle="collapse" data-target="#collapseThree" aria-expanded="false" aria-controls="collapseThree">SubCast
                            </a>
                        </h5>
                    </div>
                    <div id="collapseThree" class="collapse" aria-labelledby="headingThree" data-parent="#Div1">
                        <div class="card-body">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>

                                    <div class="well well-lg" runat="server" id="grid_show" style="WIDTH: 100%; padding-top: 15px;">
                                        <div class="row">
                                            <div class="col-md-2" style="margin-top: 19px">
                                                <asp:Button runat="server" ID="subhide" CssClass="btn btn-success" Text="ADD" OnClick="subhide_Click" />
                                            </div>
                                            <div class="col-md-2">
                                                <asp:Label runat="server" ID="adddd" Text="Cast" />
                                                <asp:DropDownList ID="ddl2" CssClass="form-control" AutoPostBack="true" runat="server">
                                                </asp:DropDownList>
                                            </div>

                                            <div class="col-md-2">
                                                <asp:Label ID="addd" Text="Add SubCast" runat="server"></asp:Label>
                                                <asp:TextBox runat="server" ID="textb" style="text-transform: uppercase;border: 1px solid;" CssClass="form-control" AutoCompleteType="Disabled" />
                                            </div>
                                            <div class="col-md-2" style="margin-top: 19px">
                                                <asp:Button runat="server" ID="btnsubcast" Text="save" CssClass="btn btn-success" OnClick="btnsubcast_Click" />
                                            </div>
                                        </div>
                                        <div class="container" style="margin-top: 20px">
                                            <asp:GridView runat="server" AutoGenerateColumns="false" ID="gridsubcast" Width="100%" OnRowEditing="gridsubcast_RowEditing">
                                                <Columns>
                                                    <asp:TemplateField Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblo" Text='<%# Eval("flag")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sub Cast">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblcast" Text='<%# Eval("subcast_name")%>' />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox1" runat="server" onkeypress="return bank(event)" Text='<%# Eval("subcast_name")%>' />
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblcastid" Text='<%# Eval("subcast_id")%>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="L1" Text="<i class='fas fa-edit' aria-hidden='true'></i>" runat="server" CommandName="edit" Font-Size="17px" />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:LinkButton ID="L2" Text="<i class='fa fa-save' aria-hidden='true' ></i>" runat="server" OnClick="L2_Click" Font-Size="17px" />
                                                            <asp:LinkButton ID="L3" Text="<i class='fa fa-times-circle'></i>" runat="server" OnClick="L3_Click" Font-Size="17px" />
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="delet3" runat="server" CommandName="delet" OnClientClick="Confirm()" OnClick="delet_Click2" Font-Size="17px"><i class='fa fa-trash' aria-hidden='true'></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>

                                            </asp:GridView>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>

                    </div>
                </div>
            </div>

        </div>
        
    </div>
    <script>
        $(document).on('keypress', '#<%= categoryt.ClientID %>', function (event) {
            var regex = new RegExp("^[a-zA-Z0-9-.' ]+$");
            var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
            if (!regex.test(key)) {
                event.preventDefault();
                return false;
            }

        }); $(document).on('keypress', '#<%=textb.ClientID%>', function (event) {
            var regex = new RegExp("^[a-zA-Z0-9-.' ]+$");
            var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
            if (!regex.test(key)) {
                event.preventDefault();
                return false;
            }

        });
        $(document).on('keypress', '#<%=cast.ClientID%>', function (event) {
            var regex = new RegExp("^[a-zA-Z0-9-.' ]+$");
            var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
            if (!regex.test(key)) {
                event.preventDefault();
                return false;
            }

        });

    </script>
    <script>
        function bank(e) {
            isIE = document.all ? 1 : 0
            keyEntry = !isIE ? e.which : event.keyCode;
            if (((keyEntry >= '48') && (keyEntry <= '57')) || ((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry == '45') || (keyEntry == '46') || (keyEntry == '32'))
                return true;
            else {
                return false;
            }
        }
    </script>

     <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to delete this?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }


    </script>
</asp:Content>

