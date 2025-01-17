<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Qualification_Details.aspx.cs" Inherits="Qualification_Details"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
      <script src="assets/libs/jquery/dist/jquery.min.js"></script>
       <link href="notify-master/css/notify.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12">
            <div class="card-header" style="background: linear-gradient(to right,#0078bc 1%,#00beda 100%) !important;">
                <div class="col-md-12">
                    <div class="col-md-10">
                        <span style="color: white; font-size: 20px;">Qualification Details
                        </span>
                    </div>
                    <div class="col-md-2">
                        <asp:Button CssClass="btn btn-warning btn-block" ID="Button1" Text="Go To Profile Dashboard" runat="server" OnClick="back_Click" />
                    </div>
                </div>
            </div>


            <div class="card card-body">
             

                <div class="row" style="margin-left: 15px; margin-right: 15px">
                    <div class="col-md-3">
                        <asp:Button ID="btn_ssc" runat="server" class="btn btn-block btn-success" Text="S.S.C" OnClick="btn_ssc_Click" />
                    </div>
                    <div class="col-md-3">
                        <asp:Button ID="btn_hsc" runat="server" class="btn btn-block btn-success" Text="H.S.C" OnClick="btn_hsc_Click" />
                    </div>
                    <div class="col-md-3">
                        <asp:Button ID="btn_diploma" runat="server" class="btn btn-block btn-success" Text="Diploma" OnClick="btn_diploma_Click" />
                    </div>
                    <div class="col-md-3">
                        <asp:Button ID="btn_degree" runat="server" class="btn btn-block btn-success" Text="Degree" OnClick="btn_degree_Click" />
                    </div>
                </div>
                <br />
                <div class="row" style="margin-left: 15px; margin-right: 15px">
                    <div class="col-md-3">
                        <asp:Button ID="btn_pg" runat="server" class="btn btn-block btn-success" Text="PG" OnClick="btn_pg_Click" />
                    </div>
                    <div class="col-md-3">
                        <asp:Button ID="btn_mphill" runat="server" class="btn btn-block btn-success" Text="M.Phill" OnClick="btn_mphill_Click" />
                    </div>
                    <div class="col-md-3">
                        <asp:Button ID="btn_phd" runat="server" class="btn btn-block btn-success" Text="Ph.D" OnClick="btn_phd_Click" />
                    </div>
                    <div class="col-md-3">
                        <asp:Button ID="btn_net" runat="server" class="btn btn-block btn-success" Text="Net" OnClick="btn_net_Click" />
                    </div>
                </div>
                <br />
                <div class="row" style="margin-left: 15px; margin-right: 15px">
                    <div class="col-md-3">
                        <asp:Button ID="btn_set" runat="server" class="btn btn-block btn-success" Text="Set" OnClick="btn_set_Click" />
                    </div>
                    <div class="col-md-3">
                        <asp:Button ID="btn_others" runat="server" class="btn btn-block btn-success" Text="Others" OnClick="btn_others_Click" />
                    </div>
                </div>
             
                <br />
                <div class="panel panel-body">
                    <div>

                        <div class="row">
                            <div class="col-md-12">

                                <%-- ssc--%>
                                <div id="tabssc" class="row" title="" runat="server">
                                    <div class="col-md-12">
                                        <div class="panel panel-default">
                                            <div id="ssc" class="panel-heading">
                                                S.S.C
                                            </div>
                                            <div class="panel-body">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">State</span>
                                                        <br />
                                                        <asp:DropDownList ID="ddl_ssc_state" TabIndex="1" runat="server" ToolTip="State" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddl_ssc_state_SelectedIndexChanged">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            <asp:ListItem>Maharashtra </asp:ListItem>
                                                            <asp:ListItem>Andaman and Nicobar</asp:ListItem>
                                                            <asp:ListItem>Andhra Pradesh</asp:ListItem>
                                                            <asp:ListItem>Arunachal Pradesh</asp:ListItem>
                                                            <asp:ListItem>Assam</asp:ListItem>
                                                            <asp:ListItem>Bihar</asp:ListItem>
                                                            <asp:ListItem>Chandigarh</asp:ListItem>
                                                            <asp:ListItem>Chhattisgarh</asp:ListItem>
                                                            <asp:ListItem>Dadra and Nagar Haveli</asp:ListItem>
                                                            <asp:ListItem>Daman and Diu</asp:ListItem>
                                                            <asp:ListItem>Delhi</asp:ListItem>
                                                            <asp:ListItem>Goa</asp:ListItem>
                                                            <asp:ListItem>Gujarat</asp:ListItem>
                                                            <asp:ListItem>Haryana</asp:ListItem>
                                                            <asp:ListItem>Himachal Pradesh</asp:ListItem>
                                                            <asp:ListItem>Jammu and Kashmir</asp:ListItem>
                                                            <asp:ListItem>Jharkhand</asp:ListItem>
                                                            <asp:ListItem>Karnataka</asp:ListItem>
                                                            <asp:ListItem>Kerala</asp:ListItem>
                                                            <asp:ListItem>Lakshadweep</asp:ListItem>
                                                            <asp:ListItem>Madhya Pradesh</asp:ListItem>
                                                            <asp:ListItem>Manipur</asp:ListItem>
                                                            <asp:ListItem>Meghalaya</asp:ListItem>
                                                            <asp:ListItem>Mizoram</asp:ListItem>
                                                            <asp:ListItem>Nagaland</asp:ListItem>
                                                            <asp:ListItem>Orissa</asp:ListItem>
                                                            <asp:ListItem>Pondicherry</asp:ListItem>
                                                            <asp:ListItem>Punjab</asp:ListItem>
                                                            <asp:ListItem>Rajasthan</asp:ListItem>
                                                            <asp:ListItem>Sikkim</asp:ListItem>
                                                            <asp:ListItem>Tamil Nadu</asp:ListItem>
                                                            <asp:ListItem>Tripura</asp:ListItem>
                                                            <asp:ListItem>Uttar Pradesh</asp:ListItem>
                                                            <asp:ListItem>Uttarakhand</asp:ListItem>
                                                            <asp:ListItem>West Bengal</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Board  </span>
                                                        <br />
                                                        <asp:DropDownList ID="ddl_ssc_board" TabIndex="2" CssClass="form-control" runat="server" ToolTip="Select Board">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row topMargin">
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Institute Name</span>
                                                        <br />
                                                        <asp:TextBox runat="server" type="text" name="txt_ssc_insti_name" ID="txt_ssc_insti_name" class="form-control" placeholder="Institute Name" TabIndex="3"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Institute place</span>
                                                        <br />
                                                        <asp:TextBox runat="server" type="text" name="txt10instituteplace" ID="txt_ssc_insti_place" class="form-control" placeholder="Institute place" TabIndex="4"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row topMargin">
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Passing Year </span>
                                                        <br />
                                                        <asp:DropDownList onblur="OnBlur(this);" ID="ddl_ssc_year" CssClass="form-control" onfocus="OnFocus(this);" TabIndex="5" runat="server" ToolTip="Year">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Passing Month </span>
                                                        <br />
                                                        <asp:DropDownList onblur="OnBlur(this);" ID="ddl_ssc_month" CssClass=" form-control" onfocus="OnFocus(this);" TabIndex="6" runat="server" ToolTip="Month">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            <asp:ListItem>Jan</asp:ListItem>
                                                            <asp:ListItem>Feb</asp:ListItem>
                                                            <asp:ListItem>Mar</asp:ListItem>
                                                            <asp:ListItem>Apr</asp:ListItem>
                                                            <asp:ListItem>May</asp:ListItem>
                                                            <asp:ListItem>Jun</asp:ListItem>
                                                            <asp:ListItem>Jul</asp:ListItem>
                                                            <asp:ListItem>Aug</asp:ListItem>
                                                            <asp:ListItem>Sept</asp:ListItem>
                                                            <asp:ListItem>Oct</asp:ListItem>
                                                            <asp:ListItem>Nov</asp:ListItem>
                                                            <asp:ListItem>Dec</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row topMargin">
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Total Marks Obtained </span>
                                                        <br />
                                                        <asp:TextBox runat="server" type="text" name="txt_ssc_mks_obtnd" ID="txt_ssc_mks_obtnd" class="uppercase form-control" onkeypress="CheckNumeric(event);" placeholder="Total Marks Obtained" MaxLength="4" TabIndex="7"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Out of Marks </span>
                                                        <br />
                                                        <asp:TextBox runat="server" type="text" onblur="OnBlur(this);" name="txt_ssc_nout_mks" ID="txt_ssc_nout_mks" class="uppercase form-control" onkeypress="CheckNumeric(event);" placeholder="Out of Marks " MaxLength="4" TabIndex="8"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row topMargin">
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Grade Obtained</span>
                                                        <br />
                                                        <asp:TextBox runat="server" type="text" onblur="OnBlur(this);" name="txt_ssc_grd" ID="txt_ssc_grd" class="uppercase form-control" placeholder="Grade Obtained" TabIndex="9"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Seat No</span>
                                                        <br />
                                                        <asp:TextBox runat="server" type="text" onblur="OnBlur(this);" name="txt_ssc_seat_no" ID="txt_ssc_seat_no" class="uppercase form-control" MaxLength="10" placeholder="Seat No" TabIndex="10"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row topMargin">
                                                    <%--<div class="col-md-6">
                                                <span style="FONT-FAMILY: Verdana">Specialize Subject</span>
                                                <br />
                                                <asp:TextBox runat="server" type="text" onblur="OnBlur(this);" name="txt_ssc_specialize_subject" ID="txt_ssc_specialize_subject" class="uppercase form-control" placeholder="Specialize Subject" TabIndex="11"></asp:TextBox>
                                            </div>--%>
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Document Upload</span>
                                                        <br />
                                                        <asp:FileUpload ID="ssc_file_uplaod" runat="server" AllowMultiple="true" class="uppercase form-control" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" style="padding: 10pt;">
                                                <div class="col-md-6"></div>
                                                <div class="col-md-6 col-xs-12 col-md-12">
                                                    <asp:Button ID="btn_ssc_submit" runat="server" Text="Submit" class="btn btn-success btn-block topMargin" TabIndex="13" data-toggle="panel" data-target="#tab" OnClick="btn_ssc_submit_Click" />
                                                </div>
                                            </div>


                                            <%--<div class="row">
                                        <div class="col-md-12 col-sm-12">
                                            <div runat="server" id="errorMessage" visible="false" class="row topMargin alert alert-danger"></div>
                                        </div>
                                    </div>--%>
                                        </div>

                                    </div>
                                </div>

                                <%--hsc--%>
                                <div id="tabhsc" class="row" runat="server">
                                    <div class="col-md-12">
                                        <div class="panel panel-default">
                                            <div class="panel-heading">
                                                H.S.C
                                            </div>
                                            <div class="panel-body">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">State </span>
                                                        <br />
                                                        <asp:DropDownList ID="ddl_hsc_state" TabIndex="1" runat="server" ToolTip="State" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddl_hsc_state_SelectedIndexChanged">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            <asp:ListItem>Maharashtra </asp:ListItem>
                                                            <asp:ListItem>Andaman and Nicobar</asp:ListItem>
                                                            <asp:ListItem>Andhra Pradesh</asp:ListItem>
                                                            <asp:ListItem>Arunachal Pradesh</asp:ListItem>
                                                            <asp:ListItem>Assam</asp:ListItem>
                                                            <asp:ListItem>Bihar</asp:ListItem>
                                                            <asp:ListItem>Chandigarh</asp:ListItem>
                                                            <asp:ListItem>Chhattisgarh</asp:ListItem>
                                                            <asp:ListItem>Dadra and Nagar Haveli</asp:ListItem>
                                                            <asp:ListItem>Daman and Diu</asp:ListItem>
                                                            <asp:ListItem>Delhi</asp:ListItem>
                                                            <asp:ListItem>Goa</asp:ListItem>
                                                            <asp:ListItem>Gujarat</asp:ListItem>
                                                            <asp:ListItem>Haryana</asp:ListItem>
                                                            <asp:ListItem>Himachal Pradesh</asp:ListItem>
                                                            <asp:ListItem>Jammu and Kashmir</asp:ListItem>
                                                            <asp:ListItem>Jharkhand</asp:ListItem>
                                                            <asp:ListItem>Karnataka</asp:ListItem>
                                                            <asp:ListItem>Kerala</asp:ListItem>
                                                            <asp:ListItem>Lakshadweep</asp:ListItem>
                                                            <asp:ListItem>Madhya Pradesh</asp:ListItem>
                                                            <asp:ListItem>Manipur</asp:ListItem>
                                                            <asp:ListItem>Meghalaya</asp:ListItem>
                                                            <asp:ListItem>Mizoram</asp:ListItem>
                                                            <asp:ListItem>Nagaland</asp:ListItem>
                                                            <asp:ListItem>Orissa</asp:ListItem>
                                                            <asp:ListItem>Pondicherry</asp:ListItem>
                                                            <asp:ListItem>Punjab</asp:ListItem>
                                                            <asp:ListItem>Rajasthan</asp:ListItem>
                                                            <asp:ListItem>Sikkim</asp:ListItem>
                                                            <asp:ListItem>Tamil Nadu</asp:ListItem>
                                                            <asp:ListItem>Tripura</asp:ListItem>
                                                            <asp:ListItem>Uttar Pradesh</asp:ListItem>
                                                            <asp:ListItem>Uttarakhand</asp:ListItem>
                                                            <asp:ListItem>West Bengal</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Board  </span>
                                                        <br />
                                                        <asp:DropDownList ID="ddl_hsc_board" TabIndex="2" CssClass="form-control" runat="server" ToolTip="Select Board">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row topMargin">
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Institute Name</span>
                                                        <br />
                                                        <asp:TextBox runat="server" type="text" name="txt_hsc_insti_name" ID="txt_hsc_insti_name" class="form-control" placeholder="Institute Name" TabIndex="3"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Institute place</span>
                                                        <br />
                                                        <asp:TextBox runat="server" type="text" name="txt_hsc_insti_place" ID="txt_hsc_insti_place" class="form-control" placeholder="Institute place" TabIndex="4"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row topMargin">
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Passing Year </span>
                                                        <br />
                                                        <asp:DropDownList onblur="OnBlur(this);" ID="ddl_hsc_year" CssClass="form-control" onfocus="OnFocus(this);" TabIndex="5" runat="server" ToolTip="Year">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Passing Month </span>
                                                        <br />
                                                        <asp:DropDownList onblur="OnBlur(this);" ID="ddl_hsc_month" CssClass=" form-control" onfocus="OnFocus(this);" TabIndex="6" runat="server" ToolTip="Month">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            <asp:ListItem>Jan</asp:ListItem>
                                                            <asp:ListItem>Feb</asp:ListItem>
                                                            <asp:ListItem>Mar</asp:ListItem>
                                                            <asp:ListItem>Apr</asp:ListItem>
                                                            <asp:ListItem>May</asp:ListItem>
                                                            <asp:ListItem>Jun</asp:ListItem>
                                                            <asp:ListItem>Jul</asp:ListItem>
                                                            <asp:ListItem>Aug</asp:ListItem>
                                                            <asp:ListItem>Sept</asp:ListItem>
                                                            <asp:ListItem>Oct</asp:ListItem>
                                                            <asp:ListItem>Nov</asp:ListItem>
                                                            <asp:ListItem>Dec</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row topMargin">
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Total Marks Obtained </span>
                                                        <br />
                                                        <asp:TextBox runat="server" type="text" name="txt_hsc_mks_obt" ID="txt_hsc_mks_obt" class="uppercase form-control" onkeypress="CheckNumeric(event);" placeholder="Total Marks Obtained" MaxLength="4" TabIndex="7"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Out of Marks </span>
                                                        <br />
                                                        <asp:TextBox runat="server" type="text" onblur="OnBlur(this);" name="txt_hsc_total_mks" ID="txt_hsc_total_mks" class="uppercase form-control" onkeypress="CheckNumeric(event);" placeholder="Out of Marks " MaxLength="4" TabIndex="8"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row topMargin">
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Grade Obtained</span>
                                                        <br />
                                                        <asp:TextBox runat="server" type="text" onblur="OnBlur(this);" name="txt_hsc_grade" ID="txt_hsc_grade" class="uppercase form-control" placeholder="Grade Obtained" TabIndex="9"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Seat No</span>
                                                        <br />
                                                        <asp:TextBox runat="server" type="text" onblur="OnBlur(this);" name="txt_hsc_seat" ID="txt_hsc_seat" class="uppercase form-control" MaxLength="10" placeholder="Seat No" TabIndex="10"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row topMargin">
                                                    <%--<div class="col-md-6">
                                                <span style="FONT-FAMILY: Verdana">Specialize Subject</span>
                                                <br />
                                                <asp:TextBox runat="server" type="text" onblur="OnBlur(this);" name="txt_hsc_specilize_sub" ID="txt_hsc_specilize_sub" class="uppercase form-control" placeholder="Specialize Subject" TabIndex="11"></asp:TextBox>
                                            </div>--%>
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Document Upload</span>
                                                        <br />
                                                        <asp:FileUpload ID="hsc_file_upload" runat="server" AllowMultiple="true" class="uppercase form-control" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" style="padding: 10pt;">
                                                <div class="col-md-6"></div>
                                                <div class="col-md-6 col-xs-12 col-md-12">
                                                    <asp:Button ID="btn_hsc_submit" runat="server" Text="Submit" class="btn btn-success btn-block topMargin" TabIndex="13" data-toggle="panel" data-target="#tab" OnClick="btn_hsc_submit_Click" />
                                                </div>
                                            </div>
                                        
                                            <div class="row">
                                                <asp:GridView ID="grd_hsc" runat="server" AutoGenerateColumns="False" OnRowCancelingEdit="grd_hsc_RowCancelingEdit"
                                                    Style="width: 100%;" GridLines="None" CellPadding="4" ForeColor="#333333">
                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                    <Columns>
                                                        <asp:BoundField DataField="emp_coll_name" HeaderText="School/College Name" />
                                                        <asp:BoundField DataField="emp_unversity_board_name" HeaderText="University/ Board Name" />
                                                        <asp:BoundField DataField="emp_degree_name" HeaderText="Exam" HeaderStyle-Height="30px" />
                                                        <asp:BoundField DataField="emp_specialization_subj" HeaderText="Specialize Subject" />
                                                        <asp:BoundField DataField="emp_month_of_passing" HeaderText="Passing Month" />
                                                        <asp:BoundField DataField="emp_year_of_passing" HeaderText="Passing Year" />
                                                        <asp:BoundField DataField="emp_marks_obtained" HeaderText="Marks Obtained" />
                                                        <asp:BoundField DataField="emp_total_marks" HeaderText="Total Marks" />
                                                        <asp:BoundField DataField="emp_class_secured" HeaderText="Grade" />
                                                        <asp:BoundField DataField="emp_col_state" HeaderText="State" />
                                                        <asp:BoundField DataField="emp_col_place" HeaderText="Place" />
                                                        <asp:BoundField DataField="emp_seatno" HeaderText="Seat no" />
                                                    </Columns>
                                                    <EditRowStyle BackColor="#999999" />
                                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Height="20px" />
                                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <%--Diploma--%>
                                <div id="tabdiploma" class="row" runat="server">
                                    <div class="col-md-12">
                                        <div class="panel panel-default">
                                            <div class="panel-heading">
                                                Diploma
                                            </div>
                                            <div class="panel-body">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">State </span>
                                                        <br />
                                                        <asp:DropDownList ID="ddl_diploma_state" TabIndex="1" runat="server" ToolTip="State" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddl_diploma_state_SelectedIndexChanged">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            <asp:ListItem>Maharashtra </asp:ListItem>
                                                            <asp:ListItem>Andaman and Nicobar</asp:ListItem>
                                                            <asp:ListItem>Andhra Pradesh</asp:ListItem>
                                                            <asp:ListItem>Arunachal Pradesh</asp:ListItem>
                                                            <asp:ListItem>Assam</asp:ListItem>
                                                            <asp:ListItem>Bihar</asp:ListItem>
                                                            <asp:ListItem>Chandigarh</asp:ListItem>
                                                            <asp:ListItem>Chhattisgarh</asp:ListItem>
                                                            <asp:ListItem>Dadra and Nagar Haveli</asp:ListItem>
                                                            <asp:ListItem>Daman and Diu</asp:ListItem>
                                                            <asp:ListItem>Delhi</asp:ListItem>
                                                            <asp:ListItem>Goa</asp:ListItem>
                                                            <asp:ListItem>Gujarat</asp:ListItem>
                                                            <asp:ListItem>Haryana</asp:ListItem>
                                                            <asp:ListItem>Himachal Pradesh</asp:ListItem>
                                                            <asp:ListItem>Jammu and Kashmir</asp:ListItem>
                                                            <asp:ListItem>Jharkhand</asp:ListItem>
                                                            <asp:ListItem>Karnataka</asp:ListItem>
                                                            <asp:ListItem>Kerala</asp:ListItem>
                                                            <asp:ListItem>Lakshadweep</asp:ListItem>
                                                            <asp:ListItem>Madhya Pradesh</asp:ListItem>
                                                            <asp:ListItem>Manipur</asp:ListItem>
                                                            <asp:ListItem>Meghalaya</asp:ListItem>
                                                            <asp:ListItem>Mizoram</asp:ListItem>
                                                            <asp:ListItem>Nagaland</asp:ListItem>
                                                            <asp:ListItem>Orissa</asp:ListItem>
                                                            <asp:ListItem>Pondicherry</asp:ListItem>
                                                            <asp:ListItem>Punjab</asp:ListItem>
                                                            <asp:ListItem>Rajasthan</asp:ListItem>
                                                            <asp:ListItem>Sikkim</asp:ListItem>
                                                            <asp:ListItem>Tamil Nadu</asp:ListItem>
                                                            <asp:ListItem>Tripura</asp:ListItem>
                                                            <asp:ListItem>Uttar Pradesh</asp:ListItem>
                                                            <asp:ListItem>Uttarakhand</asp:ListItem>
                                                            <asp:ListItem>West Bengal</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Board  </span>
                                                        <br />
                                                        <asp:DropDownList ID="ddl_diploma_board" TabIndex="2" CssClass="form-control" runat="server" ToolTip="Select Board">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row topMargin">
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Institute Name</span>
                                                        <br />
                                                        <asp:TextBox runat="server" type="text" name="txt_diploma_insti_name" ID="txt_diploma_insti_name" class="form-control" placeholder="Institute Name" TabIndex="3"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Institute place</span>
                                                        <br />
                                                        <asp:TextBox runat="server" type="text" name="txt_diploma_insti_place" ID="txt_diploma_insti_place" class="form-control" placeholder="Institute place" TabIndex="4"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row topMargin">
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Passing Year </span>
                                                        <br />
                                                        <asp:DropDownList onblur="OnBlur(this);" ID="ddl_diploma_year" CssClass="form-control" onfocus="OnFocus(this);" TabIndex="5" runat="server" ToolTip="Year">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Passing Month </span>
                                                        <br />
                                                        <asp:DropDownList onblur="OnBlur(this);" ID="ddl_diploma_month" CssClass=" form-control" onfocus="OnFocus(this);" TabIndex="6" runat="server" ToolTip="Month">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            <asp:ListItem>Jan</asp:ListItem>
                                                            <asp:ListItem>Feb</asp:ListItem>
                                                            <asp:ListItem>Mar</asp:ListItem>
                                                            <asp:ListItem>Apr</asp:ListItem>
                                                            <asp:ListItem>May</asp:ListItem>
                                                            <asp:ListItem>Jun</asp:ListItem>
                                                            <asp:ListItem>Jul</asp:ListItem>
                                                            <asp:ListItem>Aug</asp:ListItem>
                                                            <asp:ListItem>Sept</asp:ListItem>
                                                            <asp:ListItem>Oct</asp:ListItem>
                                                            <asp:ListItem>Nov</asp:ListItem>
                                                            <asp:ListItem>Dec</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row topMargin">
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Total Marks Obtained </span>
                                                        <br />
                                                        <asp:TextBox runat="server" type="text" name="txt_diploma_mks_obt" ID="txt_diploma_mks_obt" class="uppercase form-control" onkeypress="CheckNumeric(event);" placeholder="Total Marks Obtained" MaxLength="4" TabIndex="7"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Out of Marks </span>
                                                        <br />
                                                        <asp:TextBox runat="server" type="text" onblur="OnBlur(this);" name="txt_diploma_total_mks" ID="txt_diploma_total_mks" class="uppercase form-control" onkeypress="CheckNumeric(event);" placeholder="Out of Marks " MaxLength="4" TabIndex="8"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row topMargin">
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Grade Obtained</span>
                                                        <br />
                                                        <asp:TextBox runat="server" type="text" onblur="OnBlur(this);" name="txt_diploma_grade" ID="txt_diploma_grade" class="uppercase form-control" placeholder="Grade Obtained" TabIndex="9"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Seat No</span>
                                                        <br />
                                                        <asp:TextBox runat="server" type="text" onblur="OnBlur(this);" name="txt_diploma_seat_no" ID="txt_diploma_seat_no" class="uppercase form-control" placeholder="Seat No" TabIndex="10"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row topMargin">
                                                    <%--<div class="col-md-6">
                                                <span style="FONT-FAMILY: Verdana">Specialize Subject</span>
                                                <br />
                                                <asp:TextBox runat="server" type="text" onblur="OnBlur(this);" name="txt_hsc_specilize_sub" ID="txt_hsc_specilize_sub" class="uppercase form-control" placeholder="Specialize Subject" TabIndex="11"></asp:TextBox>
                                            </div>--%>
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Document Upload</span>
                                                        <br />
                                                        <asp:FileUpload ID="diploma_file_upload" runat="server" AllowMultiple="true" class="uppercase form-control" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" style="padding: 10pt;">
                                                <div class="col-md-6"></div>
                                                <div class="col-md-6 col-xs-12 col-md-12">
                                                    <asp:Button ID="btn_diploma_submit" runat="server" Text="Submit" class="btn btn-success btn-block topMargin" TabIndex="13" data-toggle="panel" data-target="#tab" OnClick="btn_diploma_submit_Click" />
                                                </div>
                                            </div>
                                            <%--<div class="row">
                                    <div class="col-md-12 col-sm-12">
                                        <div runat="server" id="err" visible="false" class="row topMargin alert alert-danger"></div>
                                    </div>
                                </div>--%>
                                            <div class="row">
                                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" OnRowCancelingEdit="grd_hsc_RowCancelingEdit"
                                                    Style="width: 100%;" GridLines="None" CellPadding="4" ForeColor="#333333">
                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                    <Columns>
                                                        <asp:BoundField DataField="emp_coll_name" HeaderText="School/College Name" />
                                                        <asp:BoundField DataField="emp_unversity_board_name" HeaderText="University/ Board Name" />
                                                        <asp:BoundField DataField="emp_degree_name" HeaderText="Exam" HeaderStyle-Height="30px" />
                                                        <asp:BoundField DataField="emp_specialization_subj" HeaderText="Specialize Subject" />
                                                        <asp:BoundField DataField="emp_month_of_passing" HeaderText="Passing Month" />
                                                        <asp:BoundField DataField="emp_year_of_passing" HeaderText="Passing Year" />
                                                        <asp:BoundField DataField="emp_marks_obtained" HeaderText="Marks Obtained" />
                                                        <asp:BoundField DataField="emp_total_marks" HeaderText="Total Marks" />
                                                        <asp:BoundField DataField="emp_class_secured" HeaderText="Grade" />
                                                        <asp:BoundField DataField="emp_col_state" HeaderText="State" />
                                                        <asp:BoundField DataField="emp_col_place" HeaderText="Place" />
                                                        <asp:BoundField DataField="emp_seatno" HeaderText="Seat no" />
                                                    </Columns>
                                                    <EditRowStyle BackColor="#999999" />
                                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Height="20px" />
                                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <%--degree--%>
                                <div id="tabdegree" class="row" runat="server">
                                    <div class="col-md-12">
                                        <div class="panel panel-default">
                                            <div class="panel-heading">
                                                Degree
                                            </div>
                                            <div class="panel-body">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">State </span>
                                                        <br />
                                                        <asp:DropDownList ID="ddl_degree_state" TabIndex="1" runat="server" ToolTip="State" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddl_degree_state_SelectedIndexChanged">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            <asp:ListItem>Maharashtra </asp:ListItem>
                                                            <asp:ListItem>Andaman and Nicobar</asp:ListItem>
                                                            <asp:ListItem>Andhra Pradesh</asp:ListItem>
                                                            <asp:ListItem>Arunachal Pradesh</asp:ListItem>
                                                            <asp:ListItem>Assam</asp:ListItem>
                                                            <asp:ListItem>Bihar</asp:ListItem>
                                                            <asp:ListItem>Chandigarh</asp:ListItem>
                                                            <asp:ListItem>Chhattisgarh</asp:ListItem>
                                                            <asp:ListItem>Dadra and Nagar Haveli</asp:ListItem>
                                                            <asp:ListItem>Daman and Diu</asp:ListItem>
                                                            <asp:ListItem>Delhi</asp:ListItem>
                                                            <asp:ListItem>Goa</asp:ListItem>
                                                            <asp:ListItem>Gujarat</asp:ListItem>
                                                            <asp:ListItem>Haryana</asp:ListItem>
                                                            <asp:ListItem>Himachal Pradesh</asp:ListItem>
                                                            <asp:ListItem>Jammu and Kashmir</asp:ListItem>
                                                            <asp:ListItem>Jharkhand</asp:ListItem>
                                                            <asp:ListItem>Karnataka</asp:ListItem>
                                                            <asp:ListItem>Kerala</asp:ListItem>
                                                            <asp:ListItem>Lakshadweep</asp:ListItem>
                                                            <asp:ListItem>Madhya Pradesh</asp:ListItem>
                                                            <asp:ListItem>Manipur</asp:ListItem>
                                                            <asp:ListItem>Meghalaya</asp:ListItem>
                                                            <asp:ListItem>Mizoram</asp:ListItem>
                                                            <asp:ListItem>Nagaland</asp:ListItem>
                                                            <asp:ListItem>Orissa</asp:ListItem>
                                                            <asp:ListItem>Pondicherry</asp:ListItem>
                                                            <asp:ListItem>Punjab</asp:ListItem>
                                                            <asp:ListItem>Rajasthan</asp:ListItem>
                                                            <asp:ListItem>Sikkim</asp:ListItem>
                                                            <asp:ListItem>Tamil Nadu</asp:ListItem>
                                                            <asp:ListItem>Tripura</asp:ListItem>
                                                            <asp:ListItem>Uttar Pradesh</asp:ListItem>
                                                            <asp:ListItem>Uttarakhand</asp:ListItem>
                                                            <asp:ListItem>West Bengal</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Board  </span>
                                                        <br />
                                                        <asp:DropDownList ID="ddl_degree_board" TabIndex="2" CssClass="form-control" runat="server" ToolTip="Select Board">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row topMargin">
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Institute Name</span>
                                                        <br />
                                                        <asp:TextBox runat="server" type="text" name="txt_degree_insti_name" ID="txt_degree_insti_name" class="form-control" placeholder="Institute Name" TabIndex="3"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Institute place</span>
                                                        <br />
                                                        <asp:TextBox runat="server" type="text" name="txt_degree_insti_place" ID="txt_degree_insti_place" class="form-control" placeholder="Institute place" TabIndex="4"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row topMargin">
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Passing Year </span>
                                                        <br />
                                                        <asp:DropDownList onblur="OnBlur(this);" ID="ddl_degree_year" CssClass="form-control" onfocus="OnFocus(this);" TabIndex="5" runat="server" ToolTip="Year">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Passing Month </span>
                                                        <br />
                                                        <asp:DropDownList onblur="OnBlur(this);" ID="ddl_degree_month" CssClass=" form-control" onfocus="OnFocus(this);" TabIndex="6" runat="server" ToolTip="Month">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            <asp:ListItem>Jan</asp:ListItem>
                                                            <asp:ListItem>Feb</asp:ListItem>
                                                            <asp:ListItem>Mar</asp:ListItem>
                                                            <asp:ListItem>Apr</asp:ListItem>
                                                            <asp:ListItem>May</asp:ListItem>
                                                            <asp:ListItem>Jun</asp:ListItem>
                                                            <asp:ListItem>Jul</asp:ListItem>
                                                            <asp:ListItem>Aug</asp:ListItem>
                                                            <asp:ListItem>Sept</asp:ListItem>
                                                            <asp:ListItem>Oct</asp:ListItem>
                                                            <asp:ListItem>Nov</asp:ListItem>
                                                            <asp:ListItem>Dec</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row topMargin">
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Total Marks Obtained </span>
                                                        <br />
                                                        <asp:TextBox runat="server" type="text" name="txt_degree_mks_obtnd" ID="txt_degree_mks_obtnd" class="uppercase form-control" onkeypress="CheckNumeric(event);" placeholder="Total Marks Obtained" MaxLength="4" TabIndex="7"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Out of Marks </span>
                                                        <br />
                                                        <asp:TextBox runat="server" type="text" onblur="OnBlur(this);" name="txt_degree_total_mks" ID="txt_degree_total_mks" class="uppercase form-control" onkeypress="CheckNumeric(event);" placeholder="Out of Marks " MaxLength="4" TabIndex="8"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row topMargin">
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Grade Obtained</span>
                                                        <br />
                                                        <asp:TextBox runat="server" type="text" onblur="OnBlur(this);" name="txt_degree_grade" ID="txt_degree_grade" class="uppercase form-control" placeholder="Grade Obtained" TabIndex="9"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Seat No</span>
                                                        <br />
                                                        <asp:TextBox runat="server" type="text" onblur="OnBlur(this);" name="txt_degree_seat_no" ID="txt_degree_seat_no" class="uppercase form-control" placeholder="Seat No" TabIndex="10"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row topMargin">
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Course / Subject Name</span>
                                                        <br />
                                                        <asp:TextBox runat="server" type="text" onblur="OnBlur(this);" name="txt_degree_specilize_subject" ID="txt_degree_specilize_subject" class="uppercase form-control" placeholder="Course / Specialize Subject" TabIndex="11"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Document Upload</span>
                                                        <br />
                                                        <asp:FileUpload ID="degree_file_upload" runat="server" AllowMultiple="true" class="uppercase form-control" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" style="padding: 10pt;">
                                                <div class="col-md-6"></div>
                                                <div class="col-md-6 col-xs-12 col-md-12">
                                                    <asp:Button ID="btn_degree_submit" runat="server" Text="Submit" class="btn btn-success btn-block topMargin" TabIndex="13" data-toggle="panel" data-target="#tab" OnClick="btn_degree_submit_Click" />
                                                </div>
                                            </div>
                                     
                                            <div class="row">
                                                <asp:GridView ID="grd_degree" runat="server" AutoGenerateColumns="False" OnRowCancelingEdit="grd_degree_RowCancelingEdit"
                                                    Style="width: 100%;" GridLines="None" CellPadding="4" ForeColor="#333333">
                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                    <Columns>
                                                        <asp:BoundField DataField="emp_coll_name" HeaderText="School/College Name" />
                                                        <asp:BoundField DataField="emp_unversity_board_name" HeaderText="University/ Board Name" />
                                                        <asp:BoundField DataField="emp_degree_name" HeaderText="Exam" HeaderStyle-Height="30px" />
                                                        <asp:BoundField DataField="emp_specialization_subj" HeaderText="Specialize Subject" />
                                                        <asp:BoundField DataField="emp_month_of_passing" HeaderText="Passing Month" />
                                                        <asp:BoundField DataField="emp_year_of_passing" HeaderText="Passing Year" />
                                                        <asp:BoundField DataField="emp_marks_obtained" HeaderText="Marks Obtained" />
                                                        <asp:BoundField DataField="emp_total_marks" HeaderText="Total Marks" />
                                                        <asp:BoundField DataField="emp_class_secured" HeaderText="Grade" />
                                                        <asp:BoundField DataField="emp_col_state" HeaderText="State" />
                                                        <asp:BoundField DataField="emp_col_place" HeaderText="Place" />
                                                        <asp:BoundField DataField="emp_seatno" HeaderText="Seat no" />
                                                    </Columns>
                                                    <EditRowStyle BackColor="#999999" />
                                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Height="20px" />
                                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <%--pg--%>
                                <div id="tabpg" class="row" runat="server">
                                    <div class="col-md-12">
                                        <div class="panel panel-default">
                                            <div class="panel-heading">
                                                Post Graduation
                                            </div>
                                            <div class="panel-body">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">State </span>
                                                        <br />
                                                        <asp:DropDownList ID="ddl_pg_state" TabIndex="1" runat="server" ToolTip="State" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddl_pg_state_SelectedIndexChanged">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            <asp:ListItem>Maharashtra </asp:ListItem>
                                                            <asp:ListItem>Andaman and Nicobar</asp:ListItem>
                                                            <asp:ListItem>Andhra Pradesh</asp:ListItem>
                                                            <asp:ListItem>Arunachal Pradesh</asp:ListItem>
                                                            <asp:ListItem>Assam</asp:ListItem>
                                                            <asp:ListItem>Bihar</asp:ListItem>
                                                            <asp:ListItem>Chandigarh</asp:ListItem>
                                                            <asp:ListItem>Chhattisgarh</asp:ListItem>
                                                            <asp:ListItem>Dadra and Nagar Haveli</asp:ListItem>
                                                            <asp:ListItem>Daman and Diu</asp:ListItem>
                                                            <asp:ListItem>Delhi</asp:ListItem>
                                                            <asp:ListItem>Goa</asp:ListItem>
                                                            <asp:ListItem>Gujarat</asp:ListItem>
                                                            <asp:ListItem>Haryana</asp:ListItem>
                                                            <asp:ListItem>Himachal Pradesh</asp:ListItem>
                                                            <asp:ListItem>Jammu and Kashmir</asp:ListItem>
                                                            <asp:ListItem>Jharkhand</asp:ListItem>
                                                            <asp:ListItem>Karnataka</asp:ListItem>
                                                            <asp:ListItem>Kerala</asp:ListItem>
                                                            <asp:ListItem>Lakshadweep</asp:ListItem>
                                                            <asp:ListItem>Madhya Pradesh</asp:ListItem>
                                                            <asp:ListItem>Manipur</asp:ListItem>
                                                            <asp:ListItem>Meghalaya</asp:ListItem>
                                                            <asp:ListItem>Mizoram</asp:ListItem>
                                                            <asp:ListItem>Nagaland</asp:ListItem>
                                                            <asp:ListItem>Orissa</asp:ListItem>
                                                            <asp:ListItem>Pondicherry</asp:ListItem>
                                                            <asp:ListItem>Punjab</asp:ListItem>
                                                            <asp:ListItem>Rajasthan</asp:ListItem>
                                                            <asp:ListItem>Sikkim</asp:ListItem>
                                                            <asp:ListItem>Tamil Nadu</asp:ListItem>
                                                            <asp:ListItem>Tripura</asp:ListItem>
                                                            <asp:ListItem>Uttar Pradesh</asp:ListItem>
                                                            <asp:ListItem>Uttarakhand</asp:ListItem>
                                                            <asp:ListItem>West Bengal</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Board  </span>
                                                        <br />
                                                        <asp:DropDownList ID="ddl_pg_board" TabIndex="2" CssClass="form-control" runat="server" ToolTip="Select Board">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row topMargin">
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Institute Name</span>
                                                        <br />
                                                        <asp:TextBox runat="server" type="text" name="txt_pg_insti_name" ID="txt_pg_insti_name" class="form-control" placeholder="Institute Name" TabIndex="3"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Institute place</span>
                                                        <br />
                                                        <asp:TextBox runat="server" type="text" name="txt_pg_insti_place" ID="txt_pg_insti_place" class="form-control" placeholder="Institute place" TabIndex="4"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row topMargin">
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Passing Year </span>
                                                        <br />
                                                        <asp:DropDownList onblur="OnBlur(this);" ID="ddl_pg_year" CssClass="form-control" onfocus="OnFocus(this);" TabIndex="5" runat="server" ToolTip="Year">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Passing Month </span>
                                                        <br />
                                                        <asp:DropDownList onblur="OnBlur(this);" ID="ddl_pg_month" CssClass=" form-control" onfocus="OnFocus(this);" TabIndex="6" runat="server" ToolTip="Month">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            <asp:ListItem>Jan</asp:ListItem>
                                                            <asp:ListItem>Feb</asp:ListItem>
                                                            <asp:ListItem>Mar</asp:ListItem>
                                                            <asp:ListItem>Apr</asp:ListItem>
                                                            <asp:ListItem>May</asp:ListItem>
                                                            <asp:ListItem>Jun</asp:ListItem>
                                                            <asp:ListItem>Jul</asp:ListItem>
                                                            <asp:ListItem>Aug</asp:ListItem>
                                                            <asp:ListItem>Sept</asp:ListItem>
                                                            <asp:ListItem>Oct</asp:ListItem>
                                                            <asp:ListItem>Nov</asp:ListItem>
                                                            <asp:ListItem>Dec</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row topMargin">
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Total Marks Obtained </span>
                                                        <br />
                                                        <asp:TextBox runat="server" type="text" name="txt_pg_mks_obt" ID="txt_pg_mks_obt" class="uppercase form-control" onkeypress="CheckNumeric(event);" placeholder="Total Marks Obtained" MaxLength="4" TabIndex="7"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Out of Marks </span>
                                                        <br />
                                                        <asp:TextBox runat="server" type="text" onblur="OnBlur(this);" name="txt_pg_total_mks" ID="txt_pg_total_mks" class="uppercase form-control" onkeypress="CheckNumeric(event);" placeholder="Out of Marks " MaxLength="4" TabIndex="8"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row topMargin">
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Grade Obtained</span>
                                                        <br />
                                                        <asp:TextBox runat="server" type="text" onblur="OnBlur(this);" name="txt_pg_grade" ID="txt_pg_grade" class="uppercase form-control" placeholder="Grade Obtained" TabIndex="9"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Seat No</span>
                                                        <br />
                                                        <asp:TextBox runat="server" type="text" onblur="OnBlur(this);" name="txt_pg_seat_no" ID="txt_pg_seat_no" class="uppercase form-control" placeholder="Seat No" TabIndex="10"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row topMargin">
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Course / Specialize Subject</span>
                                                        <br />
                                                        <asp:TextBox runat="server" type="text" onblur="OnBlur(this);" name="txt_pg_specilize_sub" ID="txt_pg_specilize_sub" class="uppercase form-control" placeholder="Course / Specialize Subject" TabIndex="11"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Document Upload</span>
                                                        <br />
                                                        <asp:FileUpload ID="pg_file_upload" runat="server" AllowMultiple="true" class="uppercase form-control" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" style="padding: 10pt;">
                                                <div class="col-md-6"></div>
                                                <div class="col-md-6 col-xs-12 col-md-12">
                                                    <asp:Button ID="btn_pg_submit" runat="server" Text="Submit" class="btn btn-success btn-block topMargin" TabIndex="13" data-toggle="panel" data-target="#tab" OnClick="btn_pg_submit_Click" />
                                                </div>
                                            </div>
                              
                                            <div class="row">
                                                <asp:GridView ID="grd_pg" runat="server" AutoGenerateColumns="False" OnRowCancelingEdit="grd_pg_RowCancelingEdit"
                                                    Style="width: 100%;" GridLines="None" CellPadding="4" ForeColor="#333333">
                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                    <Columns>
                                                        <asp:BoundField DataField="emp_coll_name" HeaderText="School/College Name" />
                                                        <asp:BoundField DataField="emp_unversity_board_name" HeaderText="University/ Board Name" />
                                                        <asp:BoundField DataField="emp_degree_name" HeaderText="Exam" HeaderStyle-Height="30px" />
                                                        <asp:BoundField DataField="emp_specialization_subj" HeaderText="Specialize Subject" />
                                                        <asp:BoundField DataField="emp_month_of_passing" HeaderText="Passing Month" />
                                                        <asp:BoundField DataField="emp_year_of_passing" HeaderText="Passing Year" />
                                                        <asp:BoundField DataField="emp_marks_obtained" HeaderText="Marks Obtained" />
                                                        <asp:BoundField DataField="emp_total_marks" HeaderText="Total Marks" />
                                                        <asp:BoundField DataField="emp_class_secured" HeaderText="Grade" />
                                                        <asp:BoundField DataField="emp_col_state" HeaderText="State" />
                                                        <asp:BoundField DataField="emp_col_place" HeaderText="Place" />
                                                        <asp:BoundField DataField="emp_seatno" HeaderText="Seat no" />
                                                    </Columns>
                                                    <EditRowStyle BackColor="#999999" />
                                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Height="20px" />
                                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <%--mphill--%>
                                <div id="tabmphil" class="row" runat="server">
                                    <div class="col-md-12">
                                        <div class="panel panel-default">
                                            <div class="panel-heading">
                                                M.Phill
                                            </div>
                                            <div class="panel-body">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">State  </span>
                                                        <br />
                                                        <asp:DropDownList ID="ddl_mphill_state" TabIndex="1" runat="server" ToolTip="State" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddl_mphill_state_SelectedIndexChanged">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            <asp:ListItem>Maharashtra </asp:ListItem>
                                                            <asp:ListItem>Andaman and Nicobar</asp:ListItem>
                                                            <asp:ListItem>Andhra Pradesh</asp:ListItem>
                                                            <asp:ListItem>Arunachal Pradesh</asp:ListItem>
                                                            <asp:ListItem>Assam</asp:ListItem>
                                                            <asp:ListItem>Bihar</asp:ListItem>
                                                            <asp:ListItem>Chandigarh</asp:ListItem>
                                                            <asp:ListItem>Chhattisgarh</asp:ListItem>
                                                            <asp:ListItem>Dadra and Nagar Haveli</asp:ListItem>
                                                            <asp:ListItem>Daman and Diu</asp:ListItem>
                                                            <asp:ListItem>Delhi</asp:ListItem>
                                                            <asp:ListItem>Goa</asp:ListItem>
                                                            <asp:ListItem>Gujarat</asp:ListItem>
                                                            <asp:ListItem>Haryana</asp:ListItem>
                                                            <asp:ListItem>Himachal Pradesh</asp:ListItem>
                                                            <asp:ListItem>Jammu and Kashmir</asp:ListItem>
                                                            <asp:ListItem>Jharkhand</asp:ListItem>
                                                            <asp:ListItem>Karnataka</asp:ListItem>
                                                            <asp:ListItem>Kerala</asp:ListItem>
                                                            <asp:ListItem>Lakshadweep</asp:ListItem>
                                                            <asp:ListItem>Madhya Pradesh</asp:ListItem>
                                                            <asp:ListItem>Manipur</asp:ListItem>
                                                            <asp:ListItem>Meghalaya</asp:ListItem>
                                                            <asp:ListItem>Mizoram</asp:ListItem>
                                                            <asp:ListItem>Nagaland</asp:ListItem>
                                                            <asp:ListItem>Orissa</asp:ListItem>
                                                            <asp:ListItem>Pondicherry</asp:ListItem>
                                                            <asp:ListItem>Punjab</asp:ListItem>
                                                            <asp:ListItem>Rajasthan</asp:ListItem>
                                                            <asp:ListItem>Sikkim</asp:ListItem>
                                                            <asp:ListItem>Tamil Nadu</asp:ListItem>
                                                            <asp:ListItem>Tripura</asp:ListItem>
                                                            <asp:ListItem>Uttar Pradesh</asp:ListItem>
                                                            <asp:ListItem>Uttarakhand</asp:ListItem>
                                                            <asp:ListItem>West Bengal</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Board  </span>
                                                        <br />
                                                        <asp:DropDownList ID="ddl_mphill_board" TabIndex="2" CssClass="form-control" runat="server" ToolTip="Select Board">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row topMargin">
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Institute Name</span>
                                                        <br />
                                                        <asp:TextBox runat="server" type="text" name="txt_mphill_insti_name" ID="txt_mphill_insti_name" class="form-control" placeholder="Institute Name" TabIndex="3"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Institute place</span>
                                                        <br />
                                                        <asp:TextBox runat="server" type="text" name="txt_mphill_insti_place" ID="txt_mphill_insti_place" class="form-control" placeholder="Institute place" TabIndex="4"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row topMargin">
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Passing Year </span>
                                                        <br />
                                                        <asp:DropDownList onblur="OnBlur(this);" ID="ddl_mphill_year" CssClass="form-control" onfocus="OnFocus(this);" TabIndex="5" runat="server" ToolTip="Year">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Passing Month</span>
                                                        <br />
                                                        <asp:DropDownList onblur="OnBlur(this);" ID="ddl_mphill_month" CssClass=" form-control" onfocus="OnFocus(this);" TabIndex="6" runat="server" ToolTip="Month">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            <asp:ListItem>Jan</asp:ListItem>
                                                            <asp:ListItem>Feb</asp:ListItem>
                                                            <asp:ListItem>Mar</asp:ListItem>
                                                            <asp:ListItem>Apr</asp:ListItem>
                                                            <asp:ListItem>May</asp:ListItem>
                                                            <asp:ListItem>Jun</asp:ListItem>
                                                            <asp:ListItem>Jul</asp:ListItem>
                                                            <asp:ListItem>Aug</asp:ListItem>
                                                            <asp:ListItem>Sept</asp:ListItem>
                                                            <asp:ListItem>Oct</asp:ListItem>
                                                            <asp:ListItem>Nov</asp:ListItem>
                                                            <asp:ListItem>Dec</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row topMargin">
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Total Marks Obtained </span>
                                                        <br />
                                                        <asp:TextBox runat="server" type="text" name="txt_mphill_mks_obt" ID="txt_mphill_mks_obt" class="uppercase form-control" onkeypress="CheckNumeric(event);" placeholder="Total Marks Obtained" MaxLength="4" TabIndex="7"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Out of Marks </span>
                                                        <br />
                                                        <asp:TextBox runat="server" type="text" onblur="OnBlur(this);" name="txt_mphill_total_mks" ID="txt_mphill_total_mks" class="uppercase form-control" onkeypress="CheckNumeric(event);" placeholder="Out of Marks " MaxLength="4" TabIndex="8"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row topMargin">
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Grade Obtained</span>
                                                        <br />
                                                        <asp:TextBox runat="server" type="text" onblur="OnBlur(this);" name="txt_mphill_grade" ID="txt_mphill_grade" class="uppercase form-control" placeholder="Grade Obtained" TabIndex="9"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Seat No</span>
                                                        <br />
                                                        <asp:TextBox runat="server" type="text" onblur="OnBlur(this);" name="txt_mphill_seat_no" ID="txt_mphill_seat_no" class="uppercase form-control" placeholder="Seat No" TabIndex="10"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row topMargin">
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Course / Specialize Subject</span>
                                                        <br />
                                                        <asp:TextBox runat="server" type="text" onblur="OnBlur(this);" name="txt_mphill_specilize_sub" ID="txt_mphill_specilize_sub" class="uppercase form-control" placeholder="Course / Specialize Subject" TabIndex="11"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Document Upload</span>
                                                        <br />
                                                        <asp:FileUpload ID="mphill_file_upload" runat="server" AllowMultiple="true" class="uppercase form-control" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" style="padding: 10pt;">
                                                <div class="col-md-6"></div>
                                                <div class="col-md-6 col-xs-12 col-md-12">
                                                    <asp:Button ID="btn_mphill_submit" runat="server" Text="Submit" class="btn btn-success btn-block topMargin" TabIndex="13" data-toggle="panel" data-target="#tab" OnClick="btn_mphill_submit_Click" />
                                                </div>
                                            </div>
                                          
                                            <div class="row">
                                                <asp:GridView ID="grd_mphill" runat="server" AutoGenerateColumns="False" OnRowCancelingEdit="grd_mphill_RowCancelingEdit"
                                                    Style="width: 100%;" GridLines="None" CellPadding="4" ForeColor="#333333">
                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                    <Columns>
                                                        <asp:BoundField DataField="emp_coll_name" HeaderText="School/College Name" />
                                                        <asp:BoundField DataField="emp_unversity_board_name" HeaderText="University/ Board Name" />
                                                        <asp:BoundField DataField="emp_degree_name" HeaderText="Exam" HeaderStyle-Height="30px" />
                                                        <asp:BoundField DataField="emp_specialization_subj" HeaderText="Specialize Subject" />
                                                        <asp:BoundField DataField="emp_month_of_passing" HeaderText="Passing Month" />
                                                        <asp:BoundField DataField="emp_year_of_passing" HeaderText="Passing Year" />
                                                        <asp:BoundField DataField="emp_marks_obtained" HeaderText="Marks Obtained" />
                                                        <asp:BoundField DataField="emp_total_marks" HeaderText="Total Marks" />
                                                        <asp:BoundField DataField="emp_class_secured" HeaderText="Grade" />
                                                        <asp:BoundField DataField="emp_col_state" HeaderText="State" />
                                                        <asp:BoundField DataField="emp_col_place" HeaderText="Place" />
                                                        <asp:BoundField DataField="emp_seatno" HeaderText="Seat no" />
                                                    </Columns>
                                                    <EditRowStyle BackColor="#999999" />
                                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Height="20px" />
                                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <%--phd--%>
                                <div id="tabphd" class="row" runat="server">
                                    <div class="col-md-12">
                                        <div class="panel panel-default">
                                            <div class="panel-heading">
                                                Ph.D
                                            </div>
                                            <div class="panel-body">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">State</span>
                                                        <br />
                                                        <asp:DropDownList ID="ddl_phd_state" TabIndex="1" runat="server" ToolTip="State" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddl_phd_state_SelectedIndexChanged">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            <asp:ListItem>Maharashtra </asp:ListItem>
                                                            <asp:ListItem>Andaman and Nicobar</asp:ListItem>
                                                            <asp:ListItem>Andhra Pradesh</asp:ListItem>
                                                            <asp:ListItem>Arunachal Pradesh</asp:ListItem>
                                                            <asp:ListItem>Assam</asp:ListItem>
                                                            <asp:ListItem>Bihar</asp:ListItem>
                                                            <asp:ListItem>Chandigarh</asp:ListItem>
                                                            <asp:ListItem>Chhattisgarh</asp:ListItem>
                                                            <asp:ListItem>Dadra and Nagar Haveli</asp:ListItem>
                                                            <asp:ListItem>Daman and Diu</asp:ListItem>
                                                            <asp:ListItem>Delhi</asp:ListItem>
                                                            <asp:ListItem>Goa</asp:ListItem>
                                                            <asp:ListItem>Gujarat</asp:ListItem>
                                                            <asp:ListItem>Haryana</asp:ListItem>
                                                            <asp:ListItem>Himachal Pradesh</asp:ListItem>
                                                            <asp:ListItem>Jammu and Kashmir</asp:ListItem>
                                                            <asp:ListItem>Jharkhand</asp:ListItem>
                                                            <asp:ListItem>Karnataka</asp:ListItem>
                                                            <asp:ListItem>Kerala</asp:ListItem>
                                                            <asp:ListItem>Lakshadweep</asp:ListItem>
                                                            <asp:ListItem>Madhya Pradesh</asp:ListItem>
                                                            <asp:ListItem>Manipur</asp:ListItem>
                                                            <asp:ListItem>Meghalaya</asp:ListItem>
                                                            <asp:ListItem>Mizoram</asp:ListItem>
                                                            <asp:ListItem>Nagaland</asp:ListItem>
                                                            <asp:ListItem>Orissa</asp:ListItem>
                                                            <asp:ListItem>Pondicherry</asp:ListItem>
                                                            <asp:ListItem>Punjab</asp:ListItem>
                                                            <asp:ListItem>Rajasthan</asp:ListItem>
                                                            <asp:ListItem>Sikkim</asp:ListItem>
                                                            <asp:ListItem>Tamil Nadu</asp:ListItem>
                                                            <asp:ListItem>Tripura</asp:ListItem>
                                                            <asp:ListItem>Uttar Pradesh</asp:ListItem>
                                                            <asp:ListItem>Uttarakhand</asp:ListItem>
                                                            <asp:ListItem>West Bengal</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Board </span>
                                                        <br />
                                                        <asp:DropDownList ID="ddl_phd_board" TabIndex="2" CssClass="form-control" runat="server" ToolTip="Select Board">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row topMargin">
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Institute Name</span>
                                                        <br />
                                                        <asp:TextBox runat="server" type="text" name="txt_phd_insti_name" ID="txt_phd_insti_name" class="form-control" placeholder="Institute Name" TabIndex="3"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Institute place</span>
                                                        <br />
                                                        <asp:TextBox runat="server" type="text" name="txt_phd_insti_place" ID="txt_phd_insti_place" class="form-control" placeholder="Institute place" TabIndex="4"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row topMargin">
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Passing Year</span>
                                                        <br />
                                                        <asp:DropDownList onblur="OnBlur(this);" ID="ddl_phd_year" CssClass="form-control" onfocus="OnFocus(this);" TabIndex="5" runat="server" ToolTip="Year">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Passing Month </span>
                                                        <br />
                                                        <asp:DropDownList onblur="OnBlur(this);" ID="ddl_phd_month" CssClass=" form-control" onfocus="OnFocus(this);" TabIndex="6" runat="server" ToolTip="Month">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            <asp:ListItem>Jan</asp:ListItem>
                                                            <asp:ListItem>Feb</asp:ListItem>
                                                            <asp:ListItem>Mar</asp:ListItem>
                                                            <asp:ListItem>Apr</asp:ListItem>
                                                            <asp:ListItem>May</asp:ListItem>
                                                            <asp:ListItem>Jun</asp:ListItem>
                                                            <asp:ListItem>Jul</asp:ListItem>
                                                            <asp:ListItem>Aug</asp:ListItem>
                                                            <asp:ListItem>Sept</asp:ListItem>
                                                            <asp:ListItem>Oct</asp:ListItem>
                                                            <asp:ListItem>Nov</asp:ListItem>
                                                            <asp:ListItem>Dec</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row topMargin">
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Total Marks Obtained </span>
                                                        <br />
                                                        <asp:TextBox runat="server" type="text" name="txt_phd_mks_obt" ID="txt_phd_mks_obt" class="uppercase form-control" onkeypress="CheckNumeric(event);" placeholder="Total Marks Obtained" MaxLength="4" TabIndex="7"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Out of Marks </span>
                                                        <br />
                                                        <asp:TextBox runat="server" type="text" onblur="OnBlur(this);" name="txt_Phd_total_mks" ID="txt_Phd_total_mks" class="uppercase form-control" onkeypress="CheckNumeric(event);" placeholder="Out of Marks " MaxLength="4" TabIndex="8"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row topMargin">
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Grade Obtained</span>
                                                        <br />
                                                        <asp:TextBox runat="server" type="text" onblur="OnBlur(this);" name="txt_phd_grade" ID="txt_phd_grade" class="uppercase form-control" placeholder="Grade Obtained" TabIndex="9"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Seat No</span>
                                                        <br />
                                                        <asp:TextBox runat="server" type="text" onblur="OnBlur(this);" name="txt_phd_seat_no" ID="txt_phd_seat_no" class="uppercase form-control" placeholder="Seat No" TabIndex="10"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row topMargin">
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Specialize Subject</span>
                                                        <br />
                                                        <asp:TextBox runat="server" type="text" onblur="OnBlur(this);" name="txt_phd_specilize_Sub" ID="txt_phd_specilize_Sub" class="uppercase form-control" placeholder="Specialize Subject" TabIndex="11"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Document Upload</span>
                                                        <br />
                                                        <asp:FileUpload ID="phd_file_upload" runat="server" AllowMultiple="true" class="uppercase form-control" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" style="padding: 10pt;">
                                                <div class="col-md-6"></div>
                                                <div class="col-md-6 col-xs-12 col-md-12">
                                                    <asp:Button ID="btn_phd_submit" runat="server" Text="Submit" class="btn btn-success btn-block topMargin" TabIndex="13" data-toggle="panel" data-target="#tab" OnClick="btn_phd_submit_Click" />
                                                </div>
                                            </div>
                                        
                                            <div class="row">
                                                <asp:GridView ID="grd_phd" runat="server" AutoGenerateColumns="False" OnRowCancelingEdit="grd_phd_RowCancelingEdit"
                                                    Style="width: 100%;" GridLines="None" CellPadding="4" ForeColor="#333333">
                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                    <Columns>
                                                        <asp:BoundField DataField="emp_coll_name" HeaderText="School/College Name" />
                                                        <asp:BoundField DataField="emp_unversity_board_name" HeaderText="University/ Board Name" />
                                                        <asp:BoundField DataField="emp_degree_name" HeaderText="Exam" HeaderStyle-Height="30px" />
                                                        <asp:BoundField DataField="emp_specialization_subj" HeaderText="Specialize Subject" />
                                                        <asp:BoundField DataField="emp_month_of_passing" HeaderText="Passing Month" />
                                                        <asp:BoundField DataField="emp_year_of_passing" HeaderText="Passing Year" />
                                                        <asp:BoundField DataField="emp_marks_obtained" HeaderText="Marks Obtained" />
                                                        <asp:BoundField DataField="emp_total_marks" HeaderText="Total Marks" />
                                                        <asp:BoundField DataField="emp_class_secured" HeaderText="Grade" />
                                                        <asp:BoundField DataField="emp_col_state" HeaderText="State" />
                                                        <asp:BoundField DataField="emp_col_place" HeaderText="Place" />
                                                        <asp:BoundField DataField="emp_seatno" HeaderText="Seat no" />
                                                    </Columns>
                                                    <EditRowStyle BackColor="#999999" />
                                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Height="20px" />
                                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <%--net--%>
                                <div id="tabnet" class="row" runat="server">
                                    <div class="col-md-12">
                                        <div class="panel panel-default">
                                            <div class="panel-heading">
                                                Net
                                            </div>
                                            <div class="panel-body">
                                                <div class="row topMargin">
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Net Exam </span>
                                                        <br />
                                                        <asp:DropDownList onblur="OnBlur(this);" ID="ddl_net" CssClass=" form-control" onfocus="OnFocus(this);" TabIndex="1" runat="server" ToolTip="Month">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            <asp:ListItem>Qualified</asp:ListItem>
                                                            <asp:ListItem>Not Qualified</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Document Upload</span>
                                                        <br />
                                                        <asp:FileUpload ID="net_file_upload" runat="server" AllowMultiple="true" class="uppercase form-control" />
                                                    </div>
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Year</span>
                                                        <br />
                                                        <asp:DropDownList onblur="OnBlur(this);" ID="ddl_net_year" CssClass="form-control" onfocus="OnFocus(this);" TabIndex="5" runat="server" ToolTip="Year">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Month</span>
                                                        <br />
                                                        <asp:DropDownList onblur="OnBlur(this);" ID="ddl_net_month" CssClass=" form-control" onfocus="OnFocus(this);" TabIndex="6" runat="server" ToolTip="Month">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            <asp:ListItem>Jan</asp:ListItem>
                                                            <asp:ListItem>Feb</asp:ListItem>
                                                            <asp:ListItem>Mar</asp:ListItem>
                                                            <asp:ListItem>Apr</asp:ListItem>
                                                            <asp:ListItem>May</asp:ListItem>
                                                            <asp:ListItem>Jun</asp:ListItem>
                                                            <asp:ListItem>Jul</asp:ListItem>
                                                            <asp:ListItem>Aug</asp:ListItem>
                                                            <asp:ListItem>Sept</asp:ListItem>
                                                            <asp:ListItem>Oct</asp:ListItem>
                                                            <asp:ListItem>Nov</asp:ListItem>
                                                            <asp:ListItem>Dec</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row" style="padding: 10pt;">
                                                    <div class="col-md-6"></div>
                                                    <div class="col-md-6 col-xs-12 col-md-12">
                                                        <asp:Button ID="btn_net_submit" runat="server" Text="Submit" class="btn btn-success btn-block topMargin" TabIndex="2" data-toggle="panel" data-target="#tab" OnClick="btn_net_submit_Click" />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <asp:GridView ID="grd_net" runat="server" AutoGenerateColumns="False" OnRowCancelingEdit="grd_net_RowCancelingEdit"
                                                        Style="width: 100%;" GridLines="None" CellPadding="4" ForeColor="#333333">
                                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                        <Columns>

                                                            <asp:BoundField DataField="emp_netset_remark" HeaderText="Exam " />

                                                        </Columns>
                                                        <EditRowStyle BackColor="#999999" />
                                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Height="20px" />
                                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <%--set--%>
                                <div id="tabset" class="row" runat="server">
                                    <div class="col-md-12">
                                        <div class="panel panel-default">
                                            <div class="panel-heading">
                                                Set
                                            </div>
                                            <div class="panel-body">
                                                <div class="row topMargin">
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Set Exam </span>
                                                        <br />
                                                        <asp:DropDownList onblur="OnBlur(this);" ID="ddl_set" CssClass=" form-control" onfocus="OnFocus(this);" TabIndex="1" runat="server" ToolTip="Month">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            <asp:ListItem>Qualified</asp:ListItem>
                                                            <asp:ListItem>Not Qualified</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Document Upload</span>
                                                        <br />
                                                        <asp:FileUpload ID="set_file_upload" runat="server" AllowMultiple="true" class="uppercase form-control" />
                                                    </div>
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Year</span>
                                                        <br />
                                                        <asp:DropDownList onblur="OnBlur(this);" ID="ddl_set_year" CssClass="form-control" onfocus="OnFocus(this);" TabIndex="5" runat="server" ToolTip="Year">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Month</span>
                                                        <br />
                                                        <asp:DropDownList onblur="OnBlur(this);" ID="ddl_set_month" CssClass=" form-control" onfocus="OnFocus(this);" TabIndex="6" runat="server" ToolTip="Month">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            <asp:ListItem>Jan</asp:ListItem>
                                                            <asp:ListItem>Feb</asp:ListItem>
                                                            <asp:ListItem>Mar</asp:ListItem>
                                                            <asp:ListItem>Apr</asp:ListItem>
                                                            <asp:ListItem>May</asp:ListItem>
                                                            <asp:ListItem>Jun</asp:ListItem>
                                                            <asp:ListItem>Jul</asp:ListItem>
                                                            <asp:ListItem>Aug</asp:ListItem>
                                                            <asp:ListItem>Sept</asp:ListItem>
                                                            <asp:ListItem>Oct</asp:ListItem>
                                                            <asp:ListItem>Nov</asp:ListItem>
                                                            <asp:ListItem>Dec</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row" style="padding: 10pt;">
                                                    <div class="col-md-6"></div>
                                                    <div class="col-md-6 col-xs-12 col-md-12">
                                                        <asp:Button ID="btn_set_submit" runat="server" Text="Submit" class="btn btn-success btn-block topMargin" TabIndex="2" data-toggle="panel" data-target="#tab" OnClick="btn_set_submit_Click" />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <asp:GridView ID="grd_set" runat="server" AutoGenerateColumns="False" OnRowCancelingEdit="grd_set_RowCancelingEdit"
                                                        Style="width: 100%;" GridLines="None" CellPadding="4" ForeColor="#333333">
                                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                        <Columns>

                                                            <asp:BoundField DataField="emp_netset_remark" HeaderText="Exam " />

                                                        </Columns>
                                                        <EditRowStyle BackColor="#999999" />
                                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Height="20px" />
                                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>


                                <%--Others--%>
                                <div id="tabothers" class="row" runat="server">
                                    <div class="col-md-12">
                                        <div class="panel panel-default">
                                            <div class="panel-heading">
                                                Other Course / Certification
                                            </div>
                                            <div class="panel-body">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">State</span>
                                                        <br />
                                                        <asp:DropDownList ID="ddl_others_state" TabIndex="1" runat="server" ToolTip="State" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddl_others_state_SelectedIndexChanged">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            <asp:ListItem>Maharashtra </asp:ListItem>
                                                            <asp:ListItem>Andaman and Nicobar</asp:ListItem>
                                                            <asp:ListItem>Andhra Pradesh</asp:ListItem>
                                                            <asp:ListItem>Arunachal Pradesh</asp:ListItem>
                                                            <asp:ListItem>Assam</asp:ListItem>
                                                            <asp:ListItem>Bihar</asp:ListItem>
                                                            <asp:ListItem>Chandigarh</asp:ListItem>
                                                            <asp:ListItem>Chhattisgarh</asp:ListItem>
                                                            <asp:ListItem>Dadra and Nagar Haveli</asp:ListItem>
                                                            <asp:ListItem>Daman and Diu</asp:ListItem>
                                                            <asp:ListItem>Delhi</asp:ListItem>
                                                            <asp:ListItem>Goa</asp:ListItem>
                                                            <asp:ListItem>Gujarat</asp:ListItem>
                                                            <asp:ListItem>Haryana</asp:ListItem>
                                                            <asp:ListItem>Himachal Pradesh</asp:ListItem>
                                                            <asp:ListItem>Jammu and Kashmir</asp:ListItem>
                                                            <asp:ListItem>Jharkhand</asp:ListItem>
                                                            <asp:ListItem>Karnataka</asp:ListItem>
                                                            <asp:ListItem>Kerala</asp:ListItem>
                                                            <asp:ListItem>Lakshadweep</asp:ListItem>
                                                            <asp:ListItem>Madhya Pradesh</asp:ListItem>
                                                            <asp:ListItem>Manipur</asp:ListItem>
                                                            <asp:ListItem>Meghalaya</asp:ListItem>
                                                            <asp:ListItem>Mizoram</asp:ListItem>
                                                            <asp:ListItem>Nagaland</asp:ListItem>
                                                            <asp:ListItem>Orissa</asp:ListItem>
                                                            <asp:ListItem>Pondicherry</asp:ListItem>
                                                            <asp:ListItem>Punjab</asp:ListItem>
                                                            <asp:ListItem>Rajasthan</asp:ListItem>
                                                            <asp:ListItem>Sikkim</asp:ListItem>
                                                            <asp:ListItem>Tamil Nadu</asp:ListItem>
                                                            <asp:ListItem>Tripura</asp:ListItem>
                                                            <asp:ListItem>Uttar Pradesh</asp:ListItem>
                                                            <asp:ListItem>Uttarakhand</asp:ListItem>
                                                            <asp:ListItem>West Bengal</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Board </span>
                                                        <br />
                                                        <asp:DropDownList ID="ddl_others_board" TabIndex="2" CssClass="form-control" runat="server" ToolTip="Select Board">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <%-- <div class="row topMargin">
                                            <div class="col-md-6">
                                                <span style="FONT-FAMILY: Verdana">Institute Name</span>
                                                <br />
                                                <asp:TextBox runat="server" type="text" name="txt_others_inst_name" ID="txt_others_inst_name" class="form-control" placeholder="Institute Name" TabIndex="3"></asp:TextBox>
                                            </div>
                                            <div class="col-md-6">
                                                <span style="FONT-FAMILY: Verdana">Institute place</span>
                                                <br />
                                                <asp:TextBox runat="server" type="text" name="txt_others_inst_place" ID="txt_others_inst_place" class="form-control" placeholder="Institute place" TabIndex="4"></asp:TextBox>
                                            </div>
                                        </div>--%>
                                                <div class="row topMargin">
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Passing Year</span>
                                                        <br />
                                                        <asp:DropDownList onblur="OnBlur(this);" ID="ddl_others_year" CssClass="form-control" onfocus="OnFocus(this);" TabIndex="5" runat="server" ToolTip="Year">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Passing Month </span>
                                                        <br />
                                                        <asp:DropDownList onblur="OnBlur(this);" ID="ddl_others_month" CssClass=" form-control" onfocus="OnFocus(this);" TabIndex="6" runat="server" ToolTip="Month">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            <asp:ListItem>Jan</asp:ListItem>
                                                            <asp:ListItem>Feb</asp:ListItem>
                                                            <asp:ListItem>Mar</asp:ListItem>
                                                            <asp:ListItem>Apr</asp:ListItem>
                                                            <asp:ListItem>May</asp:ListItem>
                                                            <asp:ListItem>Jun</asp:ListItem>
                                                            <asp:ListItem>Jul</asp:ListItem>
                                                            <asp:ListItem>Aug</asp:ListItem>
                                                            <asp:ListItem>Sept</asp:ListItem>
                                                            <asp:ListItem>Oct</asp:ListItem>
                                                            <asp:ListItem>Nov</asp:ListItem>
                                                            <asp:ListItem>Dec</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row topMargin">
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Total Marks Obtained </span>
                                                        <br />
                                                        <asp:TextBox runat="server" type="text" name="txt_others_mks_obt" ID="txt_others_mks_obt" class="uppercase form-control" onkeypress="CheckNumeric(event);" placeholder="Total Marks Obtained" MaxLength="4" TabIndex="7"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Out of Marks </span>
                                                        <br />
                                                        <asp:TextBox runat="server" type="text" onblur="OnBlur(this);" name="txt_others_total_mks" ID="txt_others_total_mks" class="uppercase form-control" onkeypress="CheckNumeric(event);" placeholder="Out of Marks " MaxLength="4" TabIndex="8"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row topMargin">
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Grade Obtained</span>
                                                        <br />
                                                        <asp:TextBox runat="server" type="text" onblur="OnBlur(this);" name="txt_others_grade" ID="txt_others_grade" class="uppercase form-control" placeholder="Grade Obtained" TabIndex="9"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Seat No</span>
                                                        <br />
                                                        <asp:TextBox runat="server" type="text" onblur="OnBlur(this);" name="txt_others_seat_no" ID="txt_others_seat_no" class="uppercase form-control" placeholder="Seat No" TabIndex="10"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row topMargin">
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Course/ Certication Name</span>
                                                        <br />
                                                        <asp:TextBox runat="server" type="text" onblur="OnBlur(this);" name="txt_others_specilize_sub" ID="txt_others_specilize_sub" class="uppercase form-control" placeholder="Course/ Certication Name" TabIndex="11"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <span style="FONT-FAMILY: Verdana">Document Upload</span>
                                                        <br />
                                                        <asp:FileUpload ID="others_file_upload" runat="server" AllowMultiple="true" class="uppercase form-control" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" style="padding: 10pt;">
                                                <div class="col-md-6"></div>
                                                <div class="col-md-6 col-xs-12 col-md-12">
                                                    <asp:Button ID="btn_others_submit" runat="server" Text="Submit" class="btn btn-success btn-block topMargin" TabIndex="13" data-toggle="panel" data-target="#tab" OnClick="btn_others_submit_Click" />
                                                </div>
                                            </div>
                                            <%--<div class="row">
                                    <div class="col-md-12 col-sm-12">
                                        <div runat="server" id="err" visible="false" class="row topMargin alert alert-danger"></div>
                                    </div>
                                </div>--%>
                                            <div class="row">
                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCancelingEdit="grd_phd_RowCancelingEdit"
                                                    Style="width: 100%;" GridLines="None" CellPadding="4" ForeColor="#333333">
                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                    <Columns>
                                                        <asp:BoundField DataField="emp_coll_name" HeaderText="School/College Name" />
                                                        <asp:BoundField DataField="emp_unversity_board_name" HeaderText="University/ Board Name" />
                                                        <asp:BoundField DataField="emp_degree_name" HeaderText="Exam" HeaderStyle-Height="30px" />
                                                        <asp:BoundField DataField="emp_specialization_subj" HeaderText="Specialize Subject" />
                                                        <asp:BoundField DataField="emp_month_of_passing" HeaderText="Passing Month" />
                                                        <asp:BoundField DataField="emp_year_of_passing" HeaderText="Passing Year" />
                                                        <asp:BoundField DataField="emp_marks_obtained" HeaderText="Marks Obtained" />
                                                        <asp:BoundField DataField="emp_total_marks" HeaderText="Total Marks" />
                                                        <asp:BoundField DataField="emp_class_secured" HeaderText="Grade" />
                                                        <asp:BoundField DataField="emp_col_state" HeaderText="State" />
                                                        <asp:BoundField DataField="emp_col_place" HeaderText="Place" />
                                                        <asp:BoundField DataField="emp_seatno" HeaderText="Seat no" />
                                                    </Columns>
                                                    <EditRowStyle BackColor="#999999" />
                                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Height="20px" />
                                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>


                                <div class="row">
                                    <asp:GridView ID="grd_ssc" runat="server" CssClass="table table-reponsive table-striped" AutoGenerateColumns="False" OnRowCancelingEdit="grd_ssc_RowCancelingEdit" OnSelectedIndexChanged="grd_ssc_SelectedIndexChanged" OnRowDeleting="grd_ssc_RowDeleting"
                                        GridLines="None" CellPadding="4" ForeColor="#333333">
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <Columns>
                                            <asp:CommandField ShowSelectButton="true" ButtonType="Link" />

                                            <asp:BoundField DataField="emp_degree_name" HeaderText="Exam" HeaderStyle-Height="30px" />
                                            <asp:BoundField DataField="emp_coll_name" HeaderText="School/College Name" />
                                            <asp:BoundField DataField="emp_col_state" HeaderText="State" />
                                            <asp:BoundField DataField="emp_unversity_board_name" HeaderText="University/ Board Name" />
                                            <asp:BoundField DataField="emp_col_place" HeaderText="Place" />
                                            <asp:BoundField DataField="emp_month_of_passing" HeaderText="Passing Month" />
                                            <asp:BoundField DataField="emp_year_of_passing" HeaderText="Passing Year" />
                                            <asp:BoundField DataField="emp_marks_obtained" HeaderText="Marks Obtained" />
                                            <asp:BoundField DataField="emp_total_marks" HeaderText="Total Marks" />
                                            <asp:BoundField DataField="emp_class_secured" HeaderText="Grade" />
                                            <asp:BoundField DataField="emp_seatno" HeaderText="Seat no" />
                                            <asp:BoundField DataField="emp_specialization_subj" HeaderText="Course / Subject Name " />
                                            <asp:BoundField DataField="emp_netset_remark" HeaderText="NET/SET Remark" />
                                            <asp:CommandField ShowDeleteButton="true" ButtonType="Link" />
                                               

                                         
                                        </Columns>
                                        <EditRowStyle BackColor="#999999" />
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Height="20px" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                    </asp:GridView>
                                </div>

                                <div class="row">
                                    <asp:GridView ID="grd_doc" runat="server" CssClass="table table-reponsive table-striped" AutoGenerateColumns="False"
                                        Style="width: 100%;" GridLines="None" CellPadding="4" ForeColor="#333333">
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <Columns>
                                            <asp:BoundField DataField="doc" HeaderText="Document Name" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <a href='<%# Eval("path") %>' target="_blank">View</a>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                        <EditRowStyle BackColor="#999999" />
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Height="20px" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>


            </div>
        </div>
    </div>
    <script type="text/javascript">
        var empId = '<%=Session["emp_id"] %>'
        var emp_role = '<%=Session["emp_role"] %>'
    </script>
     <script src="notify-master/js/notify.js"></script>
    <%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.6.2/jquery.min.js"></script>--%>
    
    
  
    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

