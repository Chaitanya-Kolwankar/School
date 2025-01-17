<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frm_Medium_Master.aspx.cs" Inherits="frm_Medium_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-lg-12">
            <div class="card-header text-white" style="background: linear-gradient(to right, #6f03bf 1%, #363695 100%) !important; border-radius: 7px">
                <h3>Medium Master</h3>
            </div>
            <div class="card card-body">
                <asp:UpdatePanel ID="updt" runat="server">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-lg-3">
                                <asp:TextBox ID="txtName" runat="server" placeholder="Medium Name" CssClass="form-control" Style="text-transform:uppercase" MaxLength="50" AutoCompleteType="Disabled"></asp:TextBox>
                            </div>
                            <div class="col-lg-2">
                                <asp:Button ID="btn_Save" AutoPostBack="true" class="btn btn-success btn3d" runat="server" Text="Save" OnClick="btn_Save_Click" />
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <br />

                <asp:UpdatePanel ID="updt_edit" runat="server">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-lg-12 col-md-6 col-sm-12">
                                <div class="table-responsive">
                                    <center>
                                        <div>
                          <asp:GridView ID="GridView1" CssClass="table table-bordered table-hover mygrid"  runat="server" AutoGenerateColumns="False" OnRowEditing="GridView1_RowEditing" OnRowCommand="GridView1_RowCommand" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowUpdating="GridView1_RowUpdating" OnRowDeleting="GridView1_RowDeleting">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Medium Name" ItemStyle-Width="300px" ItemStyle-Height="10px" >
                                            <ItemTemplate>
                                                <asp:HiddenField ID="lbl_id" runat="server" Value='<%#Eval("med_id")%>' />                                            
                                                  <asp:Label ID="lbl_med" runat="server" Text='<%#Eval("medium")%>'></asp:Label>
                                                  <asp:TextBox ID="txt_med_name"  Style="text-transform:uppercase"  runat="server" Enabled="false" OnTextChanged="txt_med_name_TextChanged" onkeypress="return alphabet(event,this)" 
                                                    Text='<%#Eval("medium")%>' MaxLength="50" Visible="false"></asp:TextBox>
                                                <asp:Label ID="lblflag" runat="server" Text='<%#Eval("newcolumn")%>' Visible="false" ></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edit" ItemStyle-Width="300px" ItemStyle-Height="10px">
                                            <ItemTemplate>
                                             <asp:LinkButton ID="edit_btn" Text="<i class='fas fa-edit' aria-hidden='true'></i>" CssClass="btn btn-link" runat="server" CommandName="Edit" CommandArgument="<%# Container.DataItemIndex %>" />
                                                <asp:LinkButton ID="updt_btn" Text="<i class='fa fa-save' style='font-size:24px'></i>" CssClass="btn btn-link" runat="server" CommandName="Update" CommandArgument="<%# Container.DataItemIndex %>" visible="false"/>
                                                  <asp:LinkButton ID="btn_cancel" Text="<i class='fa fa-times-circle' style='font-size:24px'></i>" CssClass="btn btn-link" runat="server" CommandName="Cancel" CommandArgument="<%# Container.DataItemIndex %>" visible="false"/>
                                                 </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" ItemStyle-Width="300px" ItemStyle-Height="10px" >
                                            <ItemTemplate>
                                                 <asp:LinkButton ID="del_btn" Text="<i class='fas fa-trash'></i>"  OnClick="del_btn_Click" OnClientClick="Confirm()" runat="server" CssClass="btn" CommandName="DELETE" CommandArgument="<%# Container.DataItemIndex %>" ></asp:LinkButton>
                                                   </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                                </div>
                                </center>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
       <script>
           $(document).on('keypress', '#<%= txtName.ClientID %>', function (event) {
            var regex = new RegExp("^[a-zA-Z' ]");
            var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
            if (!regex.test(key)) {
                event.preventDefault();
                return false;
            }
           });

           function alphabet(e) {

               isIE = document.all ? 1 : 0
               keyEntry = !isIE ? e.which : event.keyCode;

               if ((keyEntry >= '65') && (keyEntry <= '90') || (keyEntry >= '97') && (keyEntry <= '122')) {
                   return true;
               }
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
             confirm_value.value = "";
             if (confirm("Do you want to delete the selected Medium ?")) {
                 confirm_value.value = "Yes";
             }
             else {
                 confirm_value.value = "No";
             }
             document.forms[0].appendChild(confirm_value);
         }
    </script>
</asp:Content>

