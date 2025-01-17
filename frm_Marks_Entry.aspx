<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frm_Marks_Entry.aspx.cs" Inherits="frm_Marks_Entry" EnableEventValidation="false" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
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

        function showAlert() {
            if ($("#myAlert").find("div#myAlert2").length == 0) {
                $("#myAlert").append("<div class='alert alert-success alert-dismissable' id='myAlert2'> <button type='button' class='close' data-dismiss='alert'  aria-hidden='true'>&times;</button> Success! message sent successfully.</div>");
            }
            $("#myAlert").css("display", "");
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
    </script>


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
            <h3>Marks Entry</h3>
        </div>
        <div class="card-body">
            <asp:UpdatePanel ID="up_row" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-sm-9 col-md-6 col-md-8">

                            <div class="row">

                                <div class="col-md-4">
                                    <asp:Label runat="server" ID="lblmedium" Text="Medium"></asp:Label><br />
                                    <asp:DropDownList runat="server" ID="ddlmedium" CssClass="form-control" OnSelectedIndexChanged="ddlmedium_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>


                                <div class="col-md-4">
                                    <asp:Label runat="server" ID="lblstandard" Text="Standard"></asp:Label><br />
                                    <asp:DropDownList runat="server" ID="ddlstandard" CssClass="form-control" OnSelectedIndexChanged="ddlstandard_SelectedIndexChanged" AutoPostBack="true" Enabled="false">
                                    </asp:DropDownList>
                                </div>

                                <div class="col-md-4">
                                    <asp:Label runat="server" ID="lblexam" Text="Exam"></asp:Label><br />
                                    <asp:DropDownList runat="server" ID="ddlexam" CssClass="form-control" OnSelectedIndexChanged="ddlexam_SelectedIndexChanged" AutoPostBack="true" Enabled="false">
                                    </asp:DropDownList>
                                </div>

                            </div>

                            <br />

                            <div class="row">

                                <div class="col-md-4">
                                    <asp:Label runat="server" ID="lblsubject" Text="Subject" Visible="true"></asp:Label>&nbsp;
                            <asp:DropDownList runat="server" ID="ddlsubject" CssClass="form-control" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="ddlsubject_SelectedIndexChanged">
                            </asp:DropDownList>
                                </div>

                                <div class="col-md-4">
                                    <asp:Label runat="server" ID="lbldivision" Text="Division" Visible="true"></asp:Label>&nbsp;
                            <asp:DropDownList runat="server" ID="ddldivision" CssClass="form-control" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="ddldivision_SelectedIndexChanged">
                            </asp:DropDownList>
                                </div>

                                <div class="col-md-4">
                                    <br />
                                    <div class="row">

                                        <div class="col-md-4">
                                            

                                        </div>

                                        <div class="col-md-8">
                                            
                                        </div>


                                    </div>



                                </div>

                            </div>

                            <br />


                            <br />
                            <br />
                        </div>
                        
                        <br />
                        <br />

                        <div class="col-sm-3 col-md-6 col-md-4">
                            <div class="row" runat="server" visible="false" id="marks_criteria">

                                <div class="col-md-12" style="padding-left: 8%;">

                                    <div class="row">
                                        <!-- Tabs -->
                                        <div class="col-md-12">
                                            <section id="tabs" style="width: 85%;">
                                                <div class="container">
                                                    <div class="row">
                                                        <div class="col-xs-12 " style="width: 100%;">
                                                            <nav>
                                                                <div class="nav nav-tabs nav-fill" id="nav-tab" role="tablist">
                                                                    <a class="nav-item nav-link active" id="nav-theory-tab" data-toggle="tab" href="#nav-theory" role="tab" aria-controls="nav-theory" aria-selected="true">Theory</a>
                                                                    <a class="nav-item nav-link" id="nav-internal-tab" data-toggle="tab" href="#nav-internal" role="tab" aria-controls="nav-internal" aria-selected="false">Internal</a>
                                                                    <a class="nav-item nav-link" id="nav-practical-tab" data-toggle="tab" href="#nav-practical" role="tab" aria-controls="nav-practical" aria-selected="false">Practical</a>
                                                                </div>
                                                            </nav>
                                                            <div class="tab-content py-3 px-3 px-sm-0" id="nav-tabContent">

                                                                <div class="tab-pane fade show active" id="nav-theory" role="tabpanel" aria-labelledby="nav-theory-tab">
                                                                    <div class="row">
                                                                         <asp:Label runat="server" ID="lbltab_thtemp" Text="Theory marks does not exist." Visible="false" style="padding-left:9%;width:max-content;"></asp:Label>
                                                                        <div class="col-md-6" style="padding-left: 2em;">
                                                                           
                                                                            <asp:Label runat="server" ID="lbltabtp" Text="Passing Marks" Visible="true"></asp:Label><br />
                                                                            <asp:TextBox runat="server" ID="txttabtp" Visible="true" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                                            
                                                                        </div>

                                                                        <div class="col-md-6" style="padding-right: 2em;">
                                                                            <asp:Label runat="server" ID="lbltabto" Text="Out Of Marks" Visible="true"></asp:Label><br />
                                                                            <asp:TextBox runat="server" ID="txttabto" Visible="true" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                                        </div>
                                                                    </div>

                                                                </div>

                                                                <div class="tab-pane fade" id="nav-internal" role="tabpanel" aria-labelledby="nav-internal-tab">
                                                                    <div class="row">
                                                                         <asp:Label runat="server" ID="lbltab_intemp" Text="Internal marks does not exist." Visible="false" style="padding-left:9%;width:max-content;"></asp:Label>
                                                                        <div class="col-md-6" style="padding-left: 2em;">
                                                                            
                                                                            <asp:Label runat="server" ID="lbltabip" Text="Passing Marks" Visible="true"></asp:Label><br />
                                                                            <asp:TextBox runat="server" ID="txttabip" Visible="true" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                                           
                                                                        </div>

                                                                        <div class="col-md-6" style="padding-right: 2em;">
                                                                            <asp:Label runat="server" ID="lbltabio" Text="Out Of Marks" Visible="true"></asp:Label><br />
                                                                            <asp:TextBox runat="server" ID="txttabio" Visible="true" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                                        </div>

                                                                    </div>
                                                                </div>

                                                                <div class="tab-pane fade" id="nav-practical" role="tabpanel" aria-labelledby="nav-practical-tab">
                                                                    <div class="row">
                                                                         <asp:Label runat="server" ID="lbltab_prtemp" Text="Practical marks does not exist." Visible="false" style="padding-left:9%;width:max-content;"></asp:Label>
                                                                        <div class="col-md-6" style="padding-left: 2em;">
                                                                            
                                                                            <asp:Label runat="server" ID="lbltabpp" Text="Passing Marks" Visible="true"></asp:Label><br />
                                                                            <asp:TextBox runat="server" ID="txttabpp" Visible="true" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                                           
                                                                        </div>

                                                                        <div class="col-md-6" style="padding-right: 2em;">
                                                                            <asp:Label runat="server" ID="lbltabpo" Text="Out Of Marks" Visible="true"></asp:Label><br />
                                                                            <asp:TextBox runat="server" ID="txttabpo" Visible="true" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                                
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>
                                            </section>
                                        </div>
                                        <!-- ./Tabs -->


                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btn_export" />
                
                </Triggers>
            </asp:UpdatePanel>

            <br />


            <!-- Gridview -->
            <asp:UpdatePanel ID="up_grid" runat="server">
                <ContentTemplate>

                    <center>
                     <div style="width: 85%; word-wrap: break-word; margin: 0px;">
                        <table visible="true" runat="server" id="tbl_header">
                            <thead>
                                <tr class="alert-info">
                                
                                <th>
                                    Student ID 
                                </th>

                                <th>
                                    Student Name 
                                </th>

                                <th>
                                    Roll No.
                                </th>

                                <th runat="server" id="tbl_theory">
                                    Theory Marks
                                </th>

                                <th runat="server" id="tbl_internal">
                                    Internal Marks
                                </th>

                                <th runat="server" id="tbl_practical">
                                    Practical Marks
                                </th>
                                
                                 <th runat="server" id="tbl_grade">
                                    Grade Marks
                                </th>
                                
                                </tr>
                                </thead>
                        </table>

                        <asp:GridView runat="server" ID="gv_marks_entry" CssClass="table table-bordered table-container" ShowHeader="false" AutoGenerateColumns="false"  OnRowDataBound="gv_marks_entry_RowDataBound" style="max-height:300px;">
                        
                            <RowStyle HorizontalAlign="Center" />
                            <Columns>
                              
                                <asp:TemplateField ShowHeader="false">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblgv_std_id" Text='<% #Bind("student_id")%>' Visible="true"></asp:Label>                                    
                                    </ItemTemplate>                                  
                                </asp:TemplateField>  
                                
                                <asp:TemplateField ShowHeader="false">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblgv_std_name" Text='<% #Bind("student_name")%>' Visible="true"></asp:Label>                                    
                                    </ItemTemplate>                                  
                                </asp:TemplateField>  

                                <asp:TemplateField ShowHeader="false">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblgv_roll_no" Text='<% #Bind("roll_no")%>' Visible="true"></asp:Label>                                    
                                    </ItemTemplate>                                  
                                </asp:TemplateField>  

                                <asp:TemplateField ShowHeader="false">

                                 <ItemTemplate>
                                        <asp:TextBox runat="server" ID="lblgv_theory" Text='<% #Bind("theory")%>' Visible="true" Enabled="false" AutoPostBack="true" CssClass="form-control"  OnTextChanged="lblgv_theory_TextChanged"  onkeypress="return isNumberKey(event)" MaxLength="3" ></asp:TextBox>                                    
                                    </ItemTemplate> 
                                    
                                                  
                                </asp:TemplateField>  

                                <asp:TemplateField ShowHeader="false">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="lblgv_internal" Text='<% #Bind("internal")%>' Visible="true" Enabled="false" AutoPostBack="true" CssClass="form-control" OnTextChanged="lblgv_internal_TextChanged" onkeypress="return isNumberKey(event)" MaxLength="3" ></asp:TextBox>                                    
                                    </ItemTemplate>      
                                                             
                                </asp:TemplateField>  

                                 <asp:TemplateField ShowHeader="false">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="lblgv_practical" Text='<% #Bind("practical")%>' Visible="true" Enabled="false" AutoPostBack="true" CssClass="form-control" OnTextChanged="lblgv_practical_TextChanged" onkeypress="return isNumberKey(event)" MaxLength="3"></asp:TextBox>   
                                     
                                    </ItemTemplate>
                                     
                                                                 
                                </asp:TemplateField>  
                                
                                 <asp:TemplateField ShowHeader="false">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="lblgv_grade" Text='<% #Bind("Grade")%>' Visible="true" Enabled="false" AutoPostBack="true" CssClass="form-control"  OnTextChanged="lblgv_grade_TextChanged" onkeypress="return IsCharacterRestrict(event)" MaxLength="2"></asp:TextBox>   
                                     
                                    </ItemTemplate>
                                     
                                                                 
                                </asp:TemplateField>  

                               

                                 </Columns>
                        </asp:GridView>
                         
                    </div>

                        

                  </center>


                </ContentTemplate>
            </asp:UpdatePanel>
            <br />

            <div class="row">
                <div class="col-md-12" style="padding-left: 8%;">

                    <asp:UpdatePanel runat="server" ID="update_excel" UpdateMode="Always">
                        <ContentTemplate>
                         <asp:Button runat="server" ID="btn_export" Text="Export" CssClass="btn btn-success" OnClick="btn_export_Click" Style="width: 10%;" Visible="false" />&nbsp;&nbsp;
                            <asp:Button runat="server" ID="btn_edit" Text="Edit" CssClass="btn btn-success" OnClick="btn_edit_Click" Style="width: 10%;" Visible="false" />&nbsp;&nbsp;
                            <asp:Button runat="server" ID="btn_cancel" Text="Cancel" CssClass="btn btn-danger" OnClick="btn_cancel_Click" Visible="false" Style="width: 10%;" />&nbsp;&nbsp;
                         
                         <asp:FileUpload ID="FileUpload1" Style="display: none" runat="server" onchange="upload()" />
                         <asp:Button runat="server" ID="btn_import" Text="Import" Style="width: 15%; display: none" OnClick="btn_import_Click"  />
                                <input type="button" runat="server" value="Import Marks" class="btn btn-success " onclick="showBrowseDialog()" id="btn_display_import"  visible="false"/>&nbsp; &nbsp;

                        <asp:Button runat="server" ID="btn_save" Text="Save" CssClass="btn btn-success" OnClick="btn_save_Click" Style="width: 10%;" />&nbsp; &nbsp;
                        <asp:Button runat="server" ID="btn_import_cancel" Text="Cancel" CssClass="btn btn-danger" OnClick="btn_import_cancel_Click" Style="width: 10%;" />
                      
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btn_import" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>

        </div>

    </div>

    <script type="text/javascript">
        function make_visible() {
            document.getElementById('btn_display_import').style.display = "block";
        }
    </script>

    <script type="text/javascript">
        function make_invisible() {
            document.getElementById('btn_display_import').style.display = "none";
        }
    </script>

    <script type="text/javascript">
        var specialKeys = new Array();
        specialKeys.push(8);  //Backspace
        specialKeys.push(9);  //Tab
        specialKeys.push(46); //Delete
        specialKeys.push(36); //Home
        specialKeys.push(35); //End
        specialKeys.push(37); //Left
        specialKeys.push(39); //Right
        function IsCharacterRestrict(e) {
            var keyCode = e.keyCode == 0 ? e.charCode : e.keyCode;
            var ret = ((keyCode >= 65 && keyCode <= 90) || (keyCode >= 97 && keyCode <= 122) ||(specialKeys.indexOf(e.keyCode) != -1 && e.charCode != e.keyCode));
         
            return ret;
        }
    </script>

    <script>
        function showBrowseDialog() {
            var fileuploadctrl = document.getElementById('<%= FileUpload1.ClientID %>');
              fileuploadctrl.click();
          }

          function upload() {
              var btn = document.getElementById('<%= btn_import.ClientID %>');
            btn.click();
          }

       
    </script>


<%--    <script>
        function check_assign_theory(ins) {
            var grid = document.getElementById("<%=gv_marks_entry.ClientID%>");

            var row = ins.parentNode.parentNode.rowIndex;

            var tbl_row = grid.rows[parseInt(row)];

            var theory = tbl_row.cells[4].getElementsByTagName("input")[0].value;

            var t_criteria = document.getElementById('<%= txttabto.ClientID %>').value;

            var it = parseInt(t_criteria);

            var i_theory = parseInt(theory.valueOf());

            if (!isNaN(i_theory)) {
                if (i_theory >= it) {
                    tbl_row.cells[4].getElementsByTagName("input")[0].value = "";
                    alert('Marks entered should be less than or equal to out of marks');
                }
            }
        }
    </script>

    <script>
        function check_assign_internal(ins) {

            var grid = document.getElementById("<%=gv_marks_entry.ClientID%>");

            var row = ins.parentNode.parentNode.rowIndex;

            var tbl_row = grid.rows[parseInt(row)];

            var internal = tbl_row.cells[5].getElementsByTagName("input")[0].value;
            var i_criteria = document.getElementById('<%= txttabio.ClientID %>').value;
            var ii = parseInt(i_criteria);
            var i_internal = parseInt(internal.valueOf());

            if (!isNaN(i_internal)) {
                if (i_internal >= ii) {
                    tbl_row.cells[5].getElementsByTagName("input")[0].value = "";
                    alert('Marks entered should be less than or equal to out of marks');
                }
            }
        }
    </script>

    <script>
        function check_assign_practical(ins) {

            var grid = document.getElementById("<%=gv_marks_entry.ClientID%>");

            var row = ins.parentNode.parentNode.rowIndex;

            var tbl_row = grid.rows[parseInt(row)];

            var practical = tbl_row.cells[6].getElementsByTagName("input")[0].value;
            var ip = parseInt(p_criteria);
            var i_practical = parseInt(practical.valueOf());
            if (!isNaN(i_practical)) {
                if (i_practical >= ip) {
                    tbl_row.cells[6].getElementsByTagName("input")[0].value = "";
                    alert('Marks entered should be less than or equal to out of marks');
                }
            }
        }

    </script>--%>

 <%--  <script>
        function check_assign(ind) {

            var grid = document.getElementById("<%=gv_marks_entry.ClientID%>");

            var row = ind.parentNode.parentNode.rowIndex;

            var tbl_row = grid.rows[parseInt(row)];

            var theory = tbl_row.cells[4].getElementsByTagName("input")[0].value;

            var internal = tbl_row.cells[5].getElementsByTagName("input")[0].value;

            var practical = tbl_row.cells[6].getElementsByTagName("input")[0].value;

            var t_criteria = document.getElementById('<%= txttabto.ClientID %>').value;
            var i_criteria = document.getElementById('<%= txttabio.ClientID %>').value;
            var p_criteria = document.getElementById('<%= txttabpo.ClientID %>').value;

            var it = parseInt(t_criteria);
            var ii = parseInt(i_criteria);
            var ip = parseInt(p_criteria);

            var i_theory = parseInt(theory.valueOf());
            var i_internal = parseInt(internal.valueOf());
            var i_practical = parseInt(practical.valueOf());

            var perc = "";
            if (!isNaN(i_theory) || !isNaN(i_internal) || !isNaN(i_practical)) {
                if (i_theory >= it) {
                    tbl_row.cells[4].getElementsByTagName("input")[0].value = "";
                    alert('Marks entered should be less than or equal to out of marks');



                }
                else if (i_internal >= ii) {
                    tbl_row.cells[5].getElementsByTagName("input")[0].value = "";
                    alert('Marks entered should be less than or equal to out of marks');

                }
                else if (i_practical >= ip) {
                    tbl_row.cells[6].getElementsByTagName("input")[0].value = "";
                    alert('Marks entered should be less than or equal to out of marks');

                }
                else {

                }

            }



        }

    </script>--%>

  <%--  <script>

        function check_criteria(ind) {
            var grid = document.getElementById("<%=gv_marks_entry.ClientID%>");

            var row = ind.parentNode.parentNode.rowIndex;

            var tbl_row = grid.rows[parseInt(row)];

            var theory = tbl_row.cells[4].getElementsByTagName("input")[0].value;

            var internal = tbl_row.cells[5].getElementsByTagName("input")[0].value;

            var practical = tbl_row.cells[6].getElementsByTagName("input")[0].value;

            //tip
            //var t_criteria = document.getElementById("txttabto").valueOf();
            var t_criteria = document.getElementById('<%= txttabto.ClientID %>').value;
            //  var i_criteria = document.getElementById("txttabio").valueOf();
            var i_criteria = document.getElementById('<%= txttabio.ClientID %>').value;

            // var p_criteria = document.getElementById("txttabpo").valueOf();
            var p_criteria = document.getElementById('<%= txttabpo.ClientID %>').value;

            var i_theory = parseInt(theory.valueOf());
            var i_internal = parseInt(internal.valueOf());
            var i_practical = parseInt(practical.valueOf());

            if (i_theory <= t_criteria && i_internal <= i_criteria && i_practical <= p_criteria) {
                tbl_row.cells[6].getElementsByTagName("input")[1].value = "update";
            }
            else {
                alert('Marks entered should be less than Out of marks');
            }
        }
    </script>--%>

</asp:Content>

