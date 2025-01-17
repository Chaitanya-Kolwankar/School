<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frm_group_master.aspx.cs" Inherits="frm_group_master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }

        function allowAlphaNumericSpace(e) {
            var code = ('charCode' in e) ? e.charCode : e.keyCode;
            if (!(code == 45) &&
                !(code == 32) && // space
              !(code > 47 && code < 58) && // numeric (0-9)
              !(code > 64 && code < 91) && // upper alpha (A-Z)
              !(code > 96 && code < 123)) { // lower alpha (a-z)
                e.preventDefault();
            }
        }
        function Confirm() {
            //if ($("[id*=chkpy]").prop("checked") == false) {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            confirm_value.value = "";
            if (confirm("Do you want to delete the selected Group ?")) {
                confirm_value.value = "OK";
            }
            else {
                confirm_value.value = "Cancel";
            }
            document.forms[0].appendChild(confirm_value);
        }
        //}
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="card">
        <div class="card-header text-white" style="background: linear-gradient(to right, #6f03bf 1%, #363695 100%) !important; border-radius: 7px;">
            <h3>Group Master</h3>
        </div>
        <div class="card-body">
            <asp:UpdatePanel ID="up_row" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-md-8">
                            <div class="row">


                                <div class="col-md-3">
                                    <asp:Label runat="server" ID="lblmedium" Text="Medium"></asp:Label><br />
                                    <asp:DropDownList runat="server" ID="ddlmedium" CssClass="form-control" OnSelectedIndexChanged="ddlmedium_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label runat="server" ID="lblstandard" Text="Standard"></asp:Label><br />
                                    <asp:DropDownList runat="server" ID="ddlstandard" CssClass="form-control" OnSelectedIndexChanged="ddlstandard_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3" style="display: none">
                                    <asp:Label runat="server" ID="lblgroup_id"></asp:Label><br />

                                </div>
                                <div class="col-md-3">
                                    <asp:Label runat="server" ID="Label1" Text="Group Name"></asp:Label><br />
                                    <asp:TextBox runat="server" ID="txtgroupname" CssClass="form-control" AutoPostBack="true" onkeypress="allowAlphaNumericSpace(event)" MaxLength="50">
                                    </asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <asp:CheckBox ID="chkprev" runat="server" OnCheckedChanged="chkprev_CheckedChanged" Text="As Per Previous Year" AutoPostBack="true" />
                                    <div id="divprev" runat="server" style="display: none">

                                        <asp:DropDownList runat="server" ID="ddlprevyear" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlprevyear_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="row">
                                <div class="col-md-4">
                                    <br />
                                    <asp:Button runat="server" ID="btnnew" Text="New" CssClass="btn btn-success form-control" OnClick="btnnew_Click" />
                                </div>
                                <div class="col-md-4">
                                    <br />
                                    <asp:Button runat="server" ID="btn_save" Text="Save" CssClass="btn btn-success form-control" OnClick="btn_save_Click" />
                                </div>
                                <div class="col-md-4">
                                    <br />
                                    <asp:Button runat="server" ID="btn_clear" Text="Clear" CssClass="btn btn-success form-control" OnClick="btn_clear_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <%-- <div class="row">
                        <div class="col-md-3">
                        </div>

                    </div>--%>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="up_grid" runat="server">
                <ContentTemplate>
                    <center>
                        <div class="row" style="padding-top: 30px;">
                     <div id="divgroupmst" runat="server"  style="width: 100%; word-wrap: break-word; max-height:450px;overflow:auto"> 
                        <asp:GridView runat="server" ID="gv_grp_mst" CssClass="table table-bordered table-hover mygrid"  AutoGenerateColumns="false" HeaderStyle-CssClass="FixedHeader" >
                            <Columns>
                                
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lbl_subject" Text='<% #Bind("subject_id")%>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblgroup" runat="server" Text='<% #Bind("group_id")%>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField  HeaderText="Group Name">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblgrpname" Text='<% #Bind("Group_name")%>'  ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle  />
                                </asp:TemplateField>
                                <asp:TemplateField  HeaderText="Subject Name">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblsubname" Text='<% #Bind("Subject_name")%>'  ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle/>
                                </asp:TemplateField>
                                 
                                <asp:TemplateField HeaderText="Edit">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btn_Edit" runat="server"  OnClick="btn_Edit_Click" Text="<i class='fas fa-edit' aria-hidden='true'></i>" Enabled='<%# Eval("Status").ToString() == "A" ? true : false %>'/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Delete">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LBDelete" runat="server"  OnClick="OnDelete" OnClientClick="Confirm()" Enabled='<%# Eval("Status").ToString() == "A" ? true : false %>'><i class='fas fa-trash' aria-hidden='true' ></i></asp:LinkButton>
                                    </ItemTemplate>                                
                                </asp:TemplateField>
                                 </Columns>
                        </asp:GridView>
                    </div>
               </div>
         <div class="row" style="padding-top: 30px;">
                        <div id="divgrpsub" runat="server"  style="width: 100%; word-wrap: break-word; overflow:auto;max-height:450px">
                        <asp:GridView runat="server" ID="grdsub" CssClass="table table-bordered table-hover mygrid"  AutoGenerateColumns="false"  HeaderStyle-CssClass="FixedHeader">
                            <Columns>
                                 <asp:TemplateField  HeaderText="Select">
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="chkgroup"   ></asp:CheckBox>
                                    </ItemTemplate>
                                    <ItemStyle />
                                </asp:TemplateField>
                               
                                <asp:TemplateField  HeaderText="Subject Name">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lbl_subjectname" Text='<% #Bind("Subject_name")%>'  ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle  />
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblsub" Text='<% #Bind("subject_id")%>' Visible="false"></asp:Label>
                                       
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
</asp:Content>

