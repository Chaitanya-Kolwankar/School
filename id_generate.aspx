<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="id_generate.aspx.cs" Inherits="id_generate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <script src="assets/libs/jquery/dist/jquery.min.js"></script>
    <link href="datatablen/css/dataTables.bootstrap4.min.css" rel="stylesheet" />
    <link href="vendors/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="dist/css/icons/font-awesome/css/fontawesome.min.css" rel="stylesheet" />
    <link href="bootstrap-datepicker-1.9.0-dist/css/bootstrap-datepicker.min.css" rel="stylesheet" />  
   <script src="bootstrap-datepicker-1.9.0-dist/js/bootstrap-datepicker.min.js"></script>
    <link href="css/jquery.datetimepicker.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   

    <div class="container-fluid">
        <div class="container-fluid">
            <div class="card" style="width: 100%;">
                <div class="card-header text-white" style="background-color: #0078bc; border-radius: 7px; background: linear-gradient(to right,#0078bc 1%,#00beda 100%, transparent); border-radius: 7px">
                    <div>
                        <h3>Student & Staff ID Generate</h3>
                    </div>
                </div>
                <div class="card-body">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="row container-fluid">
                                <div class="col-md-2">
                                    <asp:DropDownList runat="server" ID="ddl" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddl_SelectedIndexChanged1">
                                        <asp:ListItem Text="None Select" Value="1" />
                                        <asp:ListItem Text="Student" Value="2" />
                                        <asp:ListItem Text="Staff" Value="3" />
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="container-fluid" runat="server" id="hh">
                                <div class="row">
                                    <div class="col-md-3">
                                        Department:
                        <asp:DropDownList ID="ddl_subcrs1" CssClass="form-control" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddl_subcrs1_SelectedIndexChanged"></asp:DropDownList>

                                    </div>
                                    <div class="col-md-3" style="margin-top: 15px">

                                        <asp:CheckBox ID="dup1" CssClass="form-control" runat="server" Text="Duplicate" AutoPostBack="true" OnCheckedChanged="dup1_CheckedChanged" />

                                    </div>
                                    <div class="col-md-3">
                                        Employee ID:

                                        <asp:TextBox ID="txt_stud1" CssClass="form-control email" AutoPostBack="true" runat="server" Enabled="false"></asp:TextBox>

                                    </div>
                                </div>
                                <div class="row" style="margin-top: 5px">
                                    <div class="col-md-2">
                                        <asp:Button runat="server" CssClass="btn btn-success" ID="btn_get" Text="Get Data with Photo And Sign" OnClick="btn_get_Click" />
                                    </div>
                                    <div class="col-md-2">

                                        <asp:Button runat="server" CssClass="btn btn-warning" ID="btn_cnt" Text="Count" data-toggle="modal" data-target="#myModal" OnClick="btn_cnt_Click" />

                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid" runat="server" id="hh1">
                                <div class="row">

                                    <div class="col-md-3">
                                        Medium:
                        <asp:DropDownList ID="ddl_subcrs" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlmedium_SelectedIndexChanged"></asp:DropDownList>
                                    </div>

                                    <div class="col-md-3">
                                        Class:
                        <asp:DropDownList ID="DropDownList1" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"></asp:DropDownList>
                                    </div>

                                    <div class="col-md-1" style="padding-top: 18px">

                                        <asp:CheckBox ID="dup" CssClass="form-control" runat="server" Text="Duplicate" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />

                                    </div>
                                    <div class="col-md-3">
                                        Student ID:

                                        <asp:TextBox ID="txt_stud" CssClass="form-control  email" runat="server" Enabled="false"></asp:TextBox>

                                    </div>
                                </div>
                                <div class="row" style="margin-top: 5px">
                                    <div class="col-md-2">
                                        <asp:Button runat="server" CssClass="btn btn-success" ID="Button1" Text="Get Data with Photo And Sign" OnClick="Button1_Click" />
                                    </div>

                                    <div class="col-md-2">

                                        <asp:Button runat="server" CssClass="btn btn-warning" ID="Button2" Text="Count" data-toggle="modal" data-target="#myModal" OnClick="Button2_Click" />

                                    </div>
                                </div>
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <div class="container-fluid" style="margin-top: 25px">

                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>

                                <asp:GridView runat="server" ID="dd" class="table table-striped table-bordered" Style="width: 100%" AutoGenerateColumns="false" >
                                    <%--<HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />--%>
                                    <Columns>

                                        <asp:TemplateField HeaderText="Student ID">
                                            <ItemTemplate>
                                                <asp:Label ID="labledd" runat="server" Text='<%# Eval("student_id")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Student ID">
                                            <ItemTemplate>
                                                <asp:Label ID="lableddw" runat="server" Text='<%# Eval("name")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Address">
                                            <ItemTemplate>
                                                <asp:Label ID="labledde" runat="server" Text='<%# Eval("address")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="phone no">
                                            <ItemTemplate>
                                                <asp:Label ID="lableddr" runat="server" Text='<%# Eval("phone_no1")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="class">
                                            <ItemTemplate>
                                                <asp:Label ID="lableddt" runat="server" Text='<%# Eval("class_id")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="gr no">
                                            <ItemTemplate>
                                                <asp:Label ID="lableddy" runat="server" Text='<%# Eval("gr_no")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="medium">
                                            <ItemTemplate>
                                                <asp:Label ID="lableddp" runat="server" Text='<%# Eval("medium")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle BackColor="#009ACB"  ForeColor="White"  ></HeaderStyle>
                                </asp:GridView>

                                </div>
                      <div class="container-fluid" style="margin-top: 25px">

                          <asp:GridView runat="server" ID="GridView1" class="table table-striped table-bordered" Style="width: 100%" AutoGenerateColumns="false" >
                              <%--<HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />--%>
                              <Columns>

                                  <asp:TemplateField HeaderText="Employee Id">
                                      <ItemTemplate>
                                          <asp:Label ID="labledd" runat="server" Text='<%# Eval("Employee Id")%>'></asp:Label>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Employee Name">
                                      <ItemTemplate>
                                          <asp:Label ID="lableddw" runat="server" Text='<%# Eval("Employee Name")%>'></asp:Label>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Gender">
                                      <ItemTemplate>
                                          <asp:Label ID="labledde" runat="server" Text='<%# Eval("Gender")%>'></asp:Label>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Date of Birth">
                                      <ItemTemplate>
                                          <asp:Label ID="lableddr" runat="server" Text='<%# Eval("Date of Birth")%>'></asp:Label>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Contact No">
                                      <ItemTemplate>
                                          <asp:Label ID="lableddt" runat="server" Text='<%# Eval("Contact No")%>'></asp:Label>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Blood Group">
                                      <ItemTemplate>
                                          <asp:Label ID="lableddy" runat="server" Text='<%# Eval("Blood Group")%>'></asp:Label>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Address">
                                      <ItemTemplate>
                                          <asp:Label ID="lableddp" runat="server" Text='<%# Eval("Address")%>'></asp:Label>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Trust Joining">
                                      <ItemTemplate>
                                          <asp:Label ID="lableddt" runat="server" Text='<%# Eval("Trust Joining")%>'></asp:Label>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Government Joining">
                                      <ItemTemplate>
                                          <asp:Label ID="lablgovt" runat="server" Text='<%# Eval("Government Joining")%>'></asp:Label>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Designation Name">
                                      <ItemTemplate>
                                          <asp:Label ID="lableddt" runat="server" Text='<%# Eval("Designation_Title")%>'></asp:Label>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                              </Columns>
                              <HeaderStyle BackColor="#009ACB"  ForeColor="White"  ></HeaderStyle>
                          </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </div>
                    <%-- </ContentTemplate>
                    </asp:UpdatePanel>--%>
                </div>
            </div>
        </div>
        <div id="myModal" class="modal fade" role="dialog">
            <div class="modal-dialog" role="document">

                <div class="modal-content">
                    <div class="modal-header ">



                        <div class="container row">
                            <div class="col-md-6">
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtdate" OnTextChanged="txtdate_TextChanged" />
                            </div>
                            <div class="col-md-6">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:Button runat="server" ID="bbb" OnClick="bbb_Click" Text="Get Detail" CssClass="btn btn-success" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <button type="button" id="btn_close" class="btn btn-default" data-dismiss="modal" style="float: right">Close</button>

                    </div>


                    <div class="modal-body" >
                        <div>
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <div style="height: 400px;" id="rr" runat="server">

                                        <asp:GridView runat="server" CssClass="form-group table table-responsive" ID="grid" AutoGenerateColumns="false" Font-Size="12pt"
                                            Style="text-align: center; height: 300px; max-height: 400px;width: 100%;">
                                            
                                            <RowStyle HorizontalAlign="Center"></RowStyle>
                                            <Columns>
                                                <asp:TemplateField HeaderText="Student ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lable" runat="server" Text='<%# Eval("stud_id")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Student Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("name")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Roll No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("roll_no")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle BackColor="#009ACB"  ForeColor="White"  ></HeaderStyle>
                                        </asp:GridView>


                                        <asp:GridView runat="server" CssClass="form-group table table-responsive" ID="GridView2" AutoGenerateColumns="false" Font-Size="12pt"
                                            class="table table-striped table-bordered" Style="width: 100%">
                                            
                                            <RowStyle HorizontalAlign="Center"></RowStyle>
                                            <Columns>
                                                <asp:TemplateField HeaderText="Employee ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lable" runat="server" Text='<%# Eval("stud_id")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Employee Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("name")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Department">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("roll_no")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle BackColor="#009ACB"  ForeColor="White"  ></HeaderStyle>
                                        </asp:GridView>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>

                        <br />


                    </div>

                </div>

            </div>

        </div>
    </div>

   
    <script src="datatablen/js/jquery-3.3.1.js"></script>
    <script src="datatablen/js/jquery.dataTables.min.js"></script>
    <script src="datatablen/js/dataTables.bootstrap4.min.js"></script>
    <script src="bootstrap-datepicker-1.9.0-dist/js/bootstrap-datepicker.min.js"></script>
    <script src="js/jquery.datetimepicker.js"></script>
    <script type="text/javascript">


        $("#btn_close").click(function () {
            $("#<%= txtdate.ClientID %>").val("");
        });
        $('[id*=txtdate]').datetimepicker(
            {
               // minDate: 0,
                timepicker: false,
                format: 'Y-m-d'
            });


        $(document).on('keypress', '.email', function (event) {
            var regex = new RegExp("^[a-zA-z0-9,]");
            var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
            if (!regex.test(key)) {
                event.preventDefault();
                return false;
            }
        });
        // function loaddtt() {
        //     $('#<%= dd.ClientID %>').DataTable({
        //         "responsive": true,
        //         "pageLength": 5,
        //          "lengthChange": false

        //     });
        // }
        // function loaddtd() {
        //     $('#<%=GridView2.ClientID %>').DataTable({
        //         "responsive": true,
        //         "pageLength": 5,
        //         "lengthChange": false
        //     });
        // }
        // function loaddt() {
        //     $('#<%= GridView1.ClientID %>').DataTable({
        //         "responsive": true,
        //         "pageLength": 5,
        //         "lengthChange": false
        //     });
        // }
        // function ldt() {
        //     $('#<%= grid.ClientID %>').DataTable({
        //         "responsive": true,
        //         "pageLength": 5,
        //         "lengthChange": false
        //     });
        // }
    </script>
    <script>
       
    </script>

</asp:Content>

