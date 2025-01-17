<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="frm_GR_allocation.aspx.cs" Inherits="frm_GR_allocation" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>GR Allocation</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

 


   
    <script type="text/javascript">
        $(document).ready(function () {
            $(".datepicker").pickadate({
                closeOnSelect: true,
                format: "dd/mm/yyyy"
            });
        });
        (function ($) {
            $(function () {

                $('.button-collapse').sideNav();
                $('select').material_select();
                $('.dropdown-button').dropdown();
            });
        })(jQuery);
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



    <div class="col-md-12">

        <div class="card">

            <div class="card-header bg-primary text-white;" style="background: linear-gradient(to right,#0078bc 1%,#00beda 100%, transparent);color:white; border-radius: 7px">
                <h3><b>GR Allocation </b></h3>
            </div>
            <div class="card-body form-group" style="display: inline; text-align: center;">

                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="row">

                            <div class="col-md-2">
                                <label for="ddlmedium" style="color: black; float: left">Medium</label>
                                <div class="input-field">
                                    <asp:DropDownList ID="ddlmedium" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlmedium_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>


                            <div class="col-md-2">
                                <label for="ddlclass" style="color: black; float: left">Class</label>
                                <div class="input-field">
                                    <asp:DropDownList ID="ddlclass" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlclass_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-2" style="display:none">
                                <div class="row">
                                    <h6 style="font-weight: bold; padding-top: 0px">Sort BY</h6>
                                </div>
                                <div class="row">
                                    <div class="col-md-4" style="padding-left: 0px; padding-right: 0px; margin-top: 8px;">
                                        <label>
                                            <asp:RadioButton ID="rdo1" runat="server" GroupName="radio" AutoPostBack="true" /> 
                                            <span style="font-size: medium; color: black;">Form No</span>
                                        </label>
                                    </div>
                                    <div class="col-md-6" style="padding-left: 0px; padding-right: 0px; margin-top: 8px;">
                                        <label>
                                            <asp:RadioButton ID="rdo2" runat="server" GroupName="radio" AutoPostBack="true" />
                                            <span style="font-size: medium; color: black;">Student Name</span>
                                        </label>
                                    </div>

                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="row">
                                    <div class="col-md-12" >
                                        <asp:CheckBox ID="chk2" runat="server" OnCheckedChanged="chk2_CheckedChanged" AutoPostBack="true" Text=" Apply Previous Year Gr No." Style="font-weight:bold" />
                                       
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="input-field">
                                            <asp:DropDownList ID="ddlyear" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlyear_SelectedIndexChanged" onChange="Confirm()">
                                                <asp:ListItem>--Select Year--</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>

                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-12" style="width: 115px; margin-top: 10px">
                                    <asp:CheckBox ID="chkedit" Style="float: left;font-weight:bold;" runat="server" OnCheckedChanged="chkedit_CheckedChanged" Checked="false" AutoPostBack="true" Text=" Filter By" ></asp:CheckBox>
                                </div>

                            </div>

                            <div class="row" id="filter_panel" runat="server">

                                <div class="col-md-2" style="width: 292.5px;">
                                    
                                        <label style="float: left">Surname</label>
                                        <asp:TextBox ID="sname" runat="server" onkeypress="return txtnam(event)" MaxLength="50" AutoPostBack="true" CssClass="form-control" OnTextChanged="sname_TextChanged" Style="text-transform: uppercase"></asp:TextBox>
                                  
                                </div>
                                <div class="col-md-2" style="width: 292.5px;">
                                    
                                        <label style="float: left">First Name</label>
                                        <asp:TextBox ID="txtfname" runat="server" onkeypress="return txtnam(event)" MaxLength="50" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtfname_TextChanged" Style="text-transform: uppercase"></asp:TextBox>
                                  
                                </div>
                                <div class="col-md-2" style="width: 292.5px;">
                                    
                                        <label style="float: left">Middle Name</label>
                                        <asp:TextBox ID="txtmname" runat="server" onkeypress="return txtnam(event)" MaxLength="50" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtmname_TextChanged" Style="text-transform: uppercase"></asp:TextBox>
                                  
                                </div>
                                <div class="col-md-2">
                                    <div class="input-field">
                                        <label for="txtgr" style="float: left">GR No.:</label>
                                        <asp:TextBox ID="txtgr" runat="server"  onkeypress="return grno(event)" MaxLength="20" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtgr_TextChanged"></asp:TextBox>
                                    </div>
                                </div>


                                <div class="col-md-2">
                                    <div class="input-field">
                                        <label for="txtid" style="float: left">Student ID:</label>
                                        <asp:TextBox ID="txtid" runat="server" onkeypress="return studid(event)" MaxLength="9" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtid_TextChanged"></asp:TextBox>
                                    </div>
                                </div>


                            </div>
                        </div>




                        <div class="row" style="margin-top: 10px">
                           
                            <div class="col-md-12">

                                <div class="card-panel" runat="server" id="grid_card" style="background-color: white; margin-left: 0px; margin-right: 0px;">
                                   
                                            <div class="well well-lg" runat="server" id="grid_show" style="  width: 100%; overflow-y: auto;max-height:450px">
                                                <asp:GridView ID="grid1" runat="server"  Style="text-align: center; border: 1px solid; border-color: black; width: 100%" AutoGenerateColumns="False" AllowSorting="True"   CssClass="table table-hover table-striped table-bordered mygrid" HeaderStyle-CssClass="FixedHeader" OnSorting="grid1_Sorting">
                                                
                                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                    <RowStyle HorizontalAlign="Center"></RowStyle>

                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Serial No" >
                                                            <ItemTemplate>
                                                                <asp:Label ID="srno" runat="server" Text='<%#Container.DataItemIndex+1%>' AutoPostBack="true"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Student ID" SortExpression="Student id">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblid" runat="server" Text='<%# Eval("Student id")%>' Style="margin: 9px;"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Student Name" SortExpression="Student Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblname" runat="server" Text='<%# Eval("Student Name") %>' Style="margin: 9px;" ></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="GR No.">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="lblgr" onkeypress="return grno(event)"  MaxLength="20" runat="server" Text='<%# Eval("GR No") %>' Style="margin: 9px;text-transform:uppercase"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Category" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcate" runat="server" Text='<%# Eval("Category") %>' Style="margin: 9px;"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="D.O.B" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldate" runat="server" Text='<%# Eval("Date of Birth") %>' Style="margin: 9px;"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Form ID" SortExpression="Form id">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblfid" runat="server" Text='<%# Eval("Form id") %>' Style="margin: 9px;"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                     
                                                    </Columns>
                                                    <HeaderStyle BackColor="#009ACB"  ForeColor="White"  ></HeaderStyle>
                                                </asp:GridView>
                                                <br />
                                            </div>
                                     
                                </div>
                            </div>
                        </div>


                          <div class="col-md-12">
                                <div class="row">
                                    <div class="col-md-3">

                                    </div>
                                    <div class="col-md-2">
                                        <br />
                                        <asp:Button runat="server" ID="btn_save" class="btn btn-success form-control" Text="Save"  OnClick="btn_save_Click" />
                                    </div>
                                    <div class="col-md-2">
                                        <br />
                                        <asp:Button runat="server" ID="btn_clear" class="btn btn-success form-control" Text="Clear" OnClick="btn_clear_Click" />

                                    </div>
                                    <div class="col-md-2">
                                        <br />
                                        <asp:Button runat="server" ID="btn_export" class="btn btn-success form-control" Text="Export to Excel"  OnClick="btn_export_Click" />

                                    </div>
                                </div>


                            </div>






                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btn_export" />

                    </Triggers>
                </asp:UpdatePanel>


            </div>
        </div>

    </div>









    <script>
        function grno(e) {
            
            isIE = document.all ? 1 : 0
            keyEntry = !isIE ? e.which : event.keyCode;
            if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '48') && (keyEntry <= '57')) || ((keyEntry >= '97') && (keyEntry <= '122')))
                return true;
            else {
                return false;
            }
           
         
        }
    </script>
    <script>
        function studid(e) {
           
            isIE = document.all ? 1 : 0
            keyEntry = !isIE ? e.which : event.keyCode;
            if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '48') && (keyEntry <= '57')) || ((keyEntry >= '97') && (keyEntry <= '122')))
                return true;
            else {
                return false;
            }
           
        }
    </script>

       <script>
           function txtnam(e) {

               isIE = document.all ? 1 : 0
               keyEntry = !isIE ? e.which : event.keyCode;
               if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry == '32'))
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
            if (confirm("Do you want to give the same gr_no of previous year ?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
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
            var ret = ((keyCode >= 48 && keyCode <= 57) || (keyCode >= 65 && keyCode <= 90) || (keyCode >= 97 && keyCode <= 122) || (specialKeys.indexOf(e.keyCode) != -1 && e.charCode != e.keyCode));
            return ret;
        }
    </script>
    <script src="assets/libs/jquery/dist/jquery.min.js"></script>
    <script src="assets/libs/bootstrap/dist/js/bootstrap.min.js"></script>
</asp:Content>

