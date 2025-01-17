<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="Form_insert.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        table {
            width: 100%;
            border-collapse: collapse;
            border: 0;
            rules: none;
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

        .hiddencol {
            display: none;
        }

        .FixedHeader {
            position: absolute;
            font-weight: bold;
        }
    </style>
    <script src="jquery/dist/jquery.min.js"></script>

    <script src="notify-master/js/notify.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="card">
        <div id="Div2">
            <div class="card-header text-white" style="background: linear-gradient(to right, #6f03bf 1%, #363695 100%) !important; border-radius: 7px">
                <h3>Form Insert</h3>
            </div>
            <div class="card-body">
                <div class="container row">
                    <div class="col-md-2">
                        Module Form
                        <asp:UpdatePanel runat="server" ID="UpdatePanel3">
                            <ContentTemplate>
                                <asp:DropDownList CssClass="form-control" ID="li_name" runat="server" OnSelectedIndexChanged="li_name_SelectedIndexChanged" AutoPostBack="TRUE">
                                    <asp:ListItem>  
      
                                    </asp:ListItem>
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-md-1">
                        <input type="button" data-toggle="modal" data-target="#myModal" value="+" class="btn btn-success" style="margin-top: 20px" />
                    </div>
                    <div class="col-md-2">
                        Register Form
                        <asp:UpdatePanel runat="server" ID="UpdatePanel4">
                            <ContentTemplate>
                                <asp:TextBox CssClass="form-control" ID="form_name" runat="server" ToolTip="Enter Form Name" Enabled="false"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-md-2">
                        <asp:UpdatePanel runat="server" ID="UpdatePanel5">
                            <ContentTemplate>
                                <asp:Button runat="server" Text="Add" AutoPostBack="false" OnClick="add_form_Click" ID="add_form" class="btn btn-success" Style="margin-top: 20px" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12" style="overflow-y: scroll; height: 500px">
                        <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="always">
                            <ContentTemplate>
                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover mygrid" OnRowCancelingEdit="GridView2_RowCancelingEdit" OnRowEditing="GridView2_RowEditing" OnRowUpdating="GridView2_RowUpdating" OnRowDeleting="GridView2_RowDeleting">
                                    <Columns>
                                        <asp:TemplateField HeaderText="ID">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_ID" runat="server" Text='<%#Eval("SR_NO") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="20%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Name" runat="server" Text='<%#Eval("form_name") %>'></asp:Label><asp:Label ID="Label1" runat="server" Text='<%#Eval("form_name1") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_Name" runat="server" Text='<%#Eval("form_name") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle Width="50%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edit">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btn_Edit" runat="server" Text="<i class='fas fa-edit' aria-hidden='true'></i>" CommandName="Edit" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="btn_Update" runat="server" Text="<i class='fa fa-save' aria-hidden='true' ></i>" CommandName="Update" />
                                                <asp:LinkButton ID="btn_Cancel" runat="server" Text="<i class='fa fa-times-circle'></i>" CommandName="Cancel" />
                                            </EditItemTemplate>
                                            <ItemStyle Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btn_delete" OnClientClick="Confirm()" runat="server" Text="<i class='fa fa-trash' aria-hidden='true'></i>" CommandName="Delete" />
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="myModal" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5>Add Module</h5>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel6">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-lg-6">
                                    <asp:TextBox CssClass="form-control" ID="TextBox1" runat="server" ToolTip="Enter Form Name"></asp:TextBox>
                                </div>
                                <div class="col-lg-6">
                                    <asp:Button ID="inse" Text="Add" runat="server" class="btn btn-success" OnClick="inse_Click" />
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <br />
                    <div style="width: 100%; overflow-y: auto; height: 300px">
                        <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="always">
                            <ContentTemplate>
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" OnRowEditing="OnRowEditing" OnRowDeleting="GridView1_RowDeleting" OnRowCommand="GridView1_RowCommand" CssClass="table table-bordered table-hover mygrid">
                                    <Columns>
                                        <asp:BoundField DataField="SR_NO" HeaderText="sr no" ItemStyle-Width="150" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
                                        <asp:BoundField DataField="MODULE_NAME" HeaderText="Name" ItemStyle-Width="150" />
                                        <asp:BoundField DataField="FLAG" HeaderText="FLAG" ItemStyle-Width="150" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" CommandName="Edit" Text="<i class='fas fa-edit' aria-hidden='true'></i>" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:LinkButton Text="<i class='fa fa-save' aria-hidden='true' ></i>" runat="server" OnClick="OnUpdate" />
                                                <asp:LinkButton Text="<i class='fa fa-times-circle'></i>" runat="server" OnClick="OnCancel" />
                                            </EditItemTemplate>
                                            <ItemStyle Width="20%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton Text="Delete" runat="server" CommandName="Delete" OnClientClick="Confirm()" OnClick="onDelete"><i class='fa fa-trash' aria-hidden='true'></i></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Width="20%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="modal-footer">
                </div>
            </div>
        </div>
    </div>
    <script src="bootstrap-datepicker-1.9.0-dist/js/bootstrap-datepicker.min.js"></script>
    <script>
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
        function alertMessage() {
            alert('Form Are Already Assign to This Module');
        }
    </script>
</asp:Content>

