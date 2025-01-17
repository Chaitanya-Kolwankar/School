<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="frm_Subject_Master.aspx.cs" Inherits="frm_Subject_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>--%>
    <script src="assets/libs/jquery/dist/jquery.min.js"></script>
    <script src="notify-master/js/notify.js"></script>
    <link href="notify-master/css/notify.css" rel="stylesheet" />
    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }

        function allowAlphaNumericSpace(e) {
            var code = ('charCode' in e) ? e.charCode : e.keyCode;
            if (!(code == 32) && // space
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
                if (confirm("Do you want to delete the selected subject ?")) {
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
            <h3>Subject Master</h3>
        </div>
        <div class="card-body">
            <asp:UpdatePanel ID="up_row" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label runat="server" ID="lblmedium" Text="Medium"></asp:Label><br />
                            <asp:DropDownList runat="server" ID="ddlmedium" CssClass="form-control" OnSelectedIndexChanged="ddlmedium_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <asp:Label runat="server" ID="lblstandard" Text="Standard"></asp:Label><br />
                            <asp:DropDownList runat="server" ID="ddlstandard" CssClass="form-control" OnSelectedIndexChanged="ddlstandard_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-1">
                            <br />
                            <asp:Button runat="server" ID="btnadd" Text="ADD" CssClass="btn btn-success form-control" OnClick="btnadd_Click" Enabled="false"/>
                        </div>
                     
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
            <!-- Gridview -->
            <asp:UpdatePanel ID="up_grid" runat="server">
                <ContentTemplate>
                    <center>
                     <div style="width: 100%; word-wrap: break-word; margin: 0px;max-height:450px;overflow:auto">
                        <asp:GridView runat="server" ID="gv_sub_mst" CssClass="table table-bordered table-hover mygrid"  AutoGenerateColumns="false" HeaderStyle-CssClass="FixedHeader">
                            <Columns>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblgv_subid" Text='<% #Bind("subject_id")%>' Visible="false"></asp:Label>
                                        <asp:Label ID="countflag" runat="server" Text='<% #Bind("count")%>' Visible="false"></asp:Label>
                                         <asp:Label ID="countgroup" runat="server" Text='<% #Bind("count_group")%>' Visible="false"></asp:Label>
                                        <asp:Label ID="updateflag" runat="server" Text='<% #Bind("update")%>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField  HeaderText="Rank">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtrank" Text='<% #Bind("rank")%>' CssClass="form-control" onkeypress="return isNumberKey(event)" MaxLength="2" OnTextChanged="txtrank_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="5%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Subject Name">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblgv_subname" Text='<% #Bind("subject_name")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <center>
                                        <asp:TextBox runat="server" ID="txtgv_subname" Text='<% #Bind("subject_name")%>' CssClass="col-md-4 form-control" onkeypress="allowAlphaNumericSpace(event)"></asp:TextBox>
                                            </center>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Criteria">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_gv_criteria" runat="server" Text='<% #Bind("criteria")%>'></asp:Label>
                                    </ItemTemplate>
                                       <EditItemTemplate>
                                           <center>
                                      <asp:DropDownList ID="ddl_criteria" runat="server" CssClass="col-md-6 form-control">
                                          <asp:ListItem>Marks</asp:ListItem>
                                          <asp:ListItem>Grade</asp:ListItem>
                                      </asp:DropDownList>
                                               </center>
                                        </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Edit">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btn_Edit" runat="server" OnClick="btn_Edit_Click" Text="<i class='fas fa-edit' aria-hidden='true'></i>" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="LBUpdate" runat="server" OnClick="OnUpdate" ><i class='fa fa-save' style='font-size:24px'></i></asp:LinkButton> &nbsp;&nbsp;
                                        <asp:LinkButton ID="LBCancel"  runat="server" OnClick="OnCancel" CommandName="Cancel" ><i class='fa fa-times-circle' style='font-size:24px'></i></asp:LinkButton>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Delete">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LBDelete" runat="server" OnClick="OnDelete" OnClientClick="Confirm()"><i class='fas fa-trash' aria-hidden='true'></i></asp:LinkButton>
                                    </ItemTemplate>                                
                                </asp:TemplateField>
                                 </Columns>
                        </asp:GridView>
                    </div>
                  </center>
                    <!--SUBMIT -->
                    <div class="row">
                        <div class="col-md-5">
                        </div>
                        <div class="col-md-2">
                            <asp:Button runat="server" ID="btnSubmit" CssClass="btn btn-success form-control" Text="Save" OnClick="btnSubmit_Click" Visible="false" />
                        </div>
                        <div class="col-md-5">
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <!-- Modal for Add -->
    <div class="modal fade" id="modalcol" role="dialog">
        <div class="modal-dialog">
            <asp:UpdatePanel ID="up_add_sub" runat="server">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">ADD NEW SUBJECT</h4>
                            <button type="button" id="closecol1" class="close" data-dismiss="modal">&times;</button>
                        </div>
                        <div class="model-body">
                            <div class="panel panel-info">
                                <div class="panel-body" style="padding: 10px">
                                    <div class="row">
                                        <div class="col-md-6">
                                            Subject Name
                                            <asp:TextBox runat="server" ID="txtaddsname" CssClass="form-control" autocomplete="off" onkeypress="allowAlphaNumericSpace(event)" MaxLength="50"></asp:TextBox>
                                        </div>
                                        <div class="col-md-6">
                                            <br />
                                            <asp:RadioButton ID="rd_marks" runat="server" GroupName="criteria" Checked="false" Text="Marks" />
                                            &nbsp;&nbsp;
                                            <asp:RadioButton ID="rd_grade" runat="server" GroupName="criteria" Checked="false" Text="Grade" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <div class="row">
                                    <asp:Button runat="server" ID="btnaddsub" OnClick="btnaddsub_Click" Text="Add Subject" CssClass="btn btn-success" />
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <script type="text/javascript">
        function openModal(name) {
            $("[id*=" + name + "]").modal('show');
            $("[id*=" + name + "]").data('bs.modal')._config.backdrop = 'static';
            $("[id*=" + name + "]").data('bs.modal')._config.keyboard = false;
        }
        function closeModal(name) {
            $("[id*=" + name + "]").modal('hide');
        }
    </script>
</asp:Content>

