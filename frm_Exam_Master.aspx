<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frm_Exam_Master.aspx.cs" Inherits="frm_Exam_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link href="css/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="assets/libs/jquery/dist/jquery.min.js"></script>
    <link href="notify-master/css/notify.css" rel="stylesheet" />
    <link rel="stylesheet" href="css/bootstrap-multiselect.css" type="text/css" />
    <script type="text/javascript" src="js/bootstrap-multiselect.js"></script>
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

        $(function () {
            $('[id*=ms_sub_name]').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200,
                buttonWidth: '100%'
            });
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="card">
        <div class="card-header text-white" style="background: linear-gradient(to right, #6f03bf 1%, #363695 100%) !important; border-radius: 7px;">
            <h3>Exam Master</h3>
        </div>
        <div class="card-body">
            <asp:UpdatePanel ID="up_row" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:CheckBox ID="chkprevious" runat="server" Text="As Per Previous Year" AutoPostBack="true" OnCheckedChanged="chkprevious_CheckedChanged" TabInde="1"/>
                        </div>
                        <div class="col-md-2">
                            <asp:DropDownList runat="server" ID="ddlprevyear" CssClass="form-control" AutoPostBack="true" Visible="false" OnSelectedIndexChanged="ddlprevyear_SelectedIndexChanged" TabIndex="2">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            Medium<span class="required">*</span>
                            <asp:DropDownList runat="server" ID="ddlmedium" CssClass="form-control" OnSelectedIndexChanged="ddlmedium_SelectedIndexChanged" AutoPostBack="true" Enabled="true" TabIndex="3">
                            </asp:DropDownList>
                        </div>

                        <div class="col-md-2">
                            Standard<span class="required">*</span>
                            <asp:DropDownList runat="server" ID="ddlstandard" CssClass="form-control" OnSelectedIndexChanged="ddlstandard_SelectedIndexChanged" AutoPostBack="true" Enabled="false" TabIndex="4">
                            </asp:DropDownList>
                        </div>

                        <div class="col-md-2">
                            Exam
                            <asp:DropDownList runat="server" ID="ddlexam_name" CssClass="form-control" OnSelectedIndexChanged="ddlexam_name_SelectedIndexChanged" AutoPostBack="true" Enabled="false" TabIndex="5">
                            </asp:DropDownList>
                            <asp:Label runat="server" ID="lblrefid" Visible="false"></asp:Label>
                        </div>

                        <div class="col-md-2">
                            Exam Type<span class="required">*</span>
                            <asp:DropDownList runat="server" ID="ddlexamtype" CssClass="form-control" OnSelectedIndexChanged="ddlexamtype_SelectedIndexChanged" Enabled="false" AutoPostBack="true" TabIndex="6">
                                <asp:ListItem Text="--Select--" Value="select"></asp:ListItem>
                                <asp:ListItem Text="Theory"></asp:ListItem>
                                <asp:ListItem Text="Practical"></asp:ListItem>
                                <asp:ListItem Text="Internal"></asp:ListItem>
                                <asp:ListItem Text="Grade"></asp:ListItem>
                            </asp:DropDownList>
                        </div>

                        <div class="col-md-2">
                            Subject<span class="required">*</span>
                            <asp:ListBox ID="ms_sub_name" runat="server" SelectionMode="Multiple" multiple="multiple" CssClass="btn form-control col-md-12" TabIndex="7"></asp:ListBox>
                        </div>

                        <div class="col-md-2">
                            <br />
                            <asp:Button runat="server" ID="btncreate_test" Text="Create New Test" CssClass="btn btn-success form-control" Width="100%" OnClick="btncreate_test_Click" Enabled="false" TabIndex="8"/>
                        </div>

                    </div>
                    <br />

                    <div class="row" runat="server" id="savepanel" visible="false">
                        <div class="col-md-2" runat="server" id="testname">
                            Test Name<span class="required">*</span>
                            <asp:TextBox runat="server" ID="txt_test_name" CssClass="form-control" onkeypress="allowAlphaNumericSpace(event)" MaxLength="15" TabIndex="9" AutoPostBack="true" OnTextChanged="txt_test_name_TextChanged"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            Out of Marks<span class="required">*</span>
                            <asp:TextBox runat="server" ID="txt_out_of" CssClass="form-control" MaxLength="3" onkeypress="return isNumberKey(event)" AutoPostBack="true" OnTextChanged="txt_out_of_TextChanged" TabIndex="10"></asp:TextBox>
                        </div>

                        <div class="col-md-2">
                            Passing Marks<span class="required">*</span>
                            <asp:TextBox runat="server" ID="txt_passing" CssClass="form-control" MaxLength="3" onkeypress="return isNumberKey(event)" AutoPostBack="true" OnTextChanged="txt_passing_TextChanged" TabIndex="11"></asp:TextBox>
                        </div>

                        <div class="col-md-2">
                            Criteria(Percentage)
                            <asp:TextBox runat="server" ID="txt_criteria" CssClass="form-control" MaxLength="3" onkeypress="return isNumberKey(event)"  Enabled="false"></asp:TextBox>
                        </div>

                        <div class="col-md-2">
                            <br />
                            <asp:Button runat="server" ID="btn_save_test" Text="Save" TabIndex="12" CssClass="btn btn-success form-control" Width="100%" OnClick="btn_save_test_Click" OnClientClick="if (!confirm('Passing and Out of Marks will be given to selected list of subjects. Are you sure you want to save?')) return false;" />
                        </div>

                        <div class="col-md-2">
                            <br />
                            <asp:Button runat="server" ID="btn_clear" Text="Clear" CssClass="btn btn-success form-control" Width="100%" OnClick="btn_clear_Click" TabIndex="13"/>
                        </div>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
            <br />

            <!-- Gridview -->
            <asp:UpdatePanel ID="up_exam_grid" runat="server">
                <ContentTemplate>
                    <center>
                     <div style="width: 100%; max-height:500px;word-wrap: break-word; margin: 0px;overflow-y:auto">
                        <asp:GridView runat="server" ID="gv_exam_mst" CssClass="table table-bordered table-hover mygrid" HeaderStyle-CssClass="FixedHeader" AutoGenerateColumns="false" OnRowEditing="gv_exam_mst_RowEditing" OnRowDataBound="gv_exam_mst_RowDataBound">
                        
                            <RowStyle HorizontalAlign="Center" />
                            <Columns>
                             <asp:TemplateField HeaderText="exam" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblcount" Text='<% #Bind("count")%>' Visible="false"></asp:Label>
                                        <asp:Label runat="server" ID="lblgv_examid" Text='<% #Bind("exam_id")%>' Visible="false"></asp:Label>
                                        <asp:Label runat="server" ID="lblgv_examname" Text='<% #Bind("exam_name")%>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                               <asp:TemplateField HeaderText="Subject name">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblgv_subid" Text='<% #Bind("subject_id")%>' Visible="false"></asp:Label>
                                        <asp:Label runat="server" ID="lblgv_subname" Text='<% #Bind("subject_name")%>'></asp:Label>
                                    </ItemTemplate>
                             
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Exam type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgv_exam_type" runat="server" Text='<%# Bind("exam_type") %>'></asp:Label>
                                    </ItemTemplate>
                             
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Out of">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgv_out_of" runat="server" Text='<%# Bind("out_of_marks") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgv_out_of" runat="server" Text='<%# Bind("out_of_marks") %>' MaxLength="3" onkeypress="return isNumberKey(event)" CssClass="col-md-2" AutoPostBack="true" OnTextChanged="txtgv_out_of_TextChanged" onkeyup="CalFun(this)"></asp:TextBox>
                                    </EditItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="passing">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgv_passing" runat="server" Text='<%# Bind("passing_marks") %>'></asp:Label>
                                    </ItemTemplate>

                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgv_passing" runat="server" Text='<%# Bind("passing_marks") %>' MaxLength="3" onkeypress="return isNumberKey(event)" CssClass="col-md-2" AutoPostBack="true"  OnTextChanged="txtgv_passing_TextChanged" onkeyup="CalFun(this)"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="edit">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btn_Edit" runat="server" CommandName="Edit" Text="<i class='fas fa-edit' aria-hidden='true'></i>" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="LBUpdate"  CommandName="Update" runat="server" OnClick="LBUpdate_Click" ><i class='fa fa-save' style='font-size:24px'></i></asp:LinkButton> &nbsp;&nbsp;
                                        <asp:LinkButton ID="LBCancel"  runat="server" OnClick="LBCancel_Click" CommandName="Cancel" ><i class='fa fa-times-circle' style='font-size:24px'></i></asp:LinkButton>
                                    </EditItemTemplate>                                
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="delete">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LBDelete" runat="server" CommandName="Delete" OnClick="LBDelete_Click" OnClientClick="if (!confirm('Are you sure you want delete?')) return false;"><i class='fas fa-trash' aria-hidden='true'></i></asp:LinkButton>
                                    </ItemTemplate>                                
                                </asp:TemplateField>    

                            </Columns>
                        </asp:GridView>                         
                    </div>
                  </center>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <script>
        function CalFun(ind) {

            var grid = document.getElementById("<%=gv_exam_mst.ClientID%>");

            var row = ind.parentNode.parentNode.rowIndex;

            var tbl_row = grid.rows[parseInt(row)];

            var out_of = tbl_row.cells[3].getElementsByTagName("input")[0].value;

            var passing = tbl_row.cells[4].getElementsByTagName("input")[0].value;

            var ipass = parseInt(passing.valueOf());
            var iout = parseInt(out_of.valueOf());
            var perc = "";

            if (isNaN(ipass) || isNaN(iout) || ipass == iout) {
                perc = " ";
            }

            else {
                perc = ((ipass / iout) * 100).toFixed(3);
                perc = Math.round(perc);
                tbl_row.cells[5].getElementsByTagName("input")[0].value = perc;
            }
        }

    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('[id*=ms_sub_name]').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200,
                buttonWidth: '100%'
            });
        });

        //On UpdatePanel Refresh
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $('[id*=ms_sub_name]').multiselect({
                        includeSelectAllOption: true,
                        maxHeight: 200,
                        buttonWidth: '100%'
                    });
                }
            });
        };
        function multi() {
            $('[id*=ms_sub_name]').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200,
                buttonWidth: '100%'
            });
        }
    </script>
</asp:Content>

