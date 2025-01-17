<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="GR_report.aspx.cs" Inherits="GR_report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="jquery/dist/jquery.min.js"></script>
     <link href="Datatable/datatables.min.css" rel="stylesheet" />
    <script src="Datatable/datatables.min.js"></script>

     <style type="text/css">
        .WordWrap {
            width: 100%;
            word-break: break-all;
        }

        .WordBreak {
            width: 100px;
            OVERFLOW: hidden;
            TEXT-OVERFLOW: ellipsis;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
    
    <div class="card">
        <div class="card-body">

            <div class="col mt-2">
                <div class="card">
                    <div class="card-header bg-primary text-white" style="background: linear-gradient(to right,#0078bc 1%,#00beda 100%, transparent); border-radius: 7px">
                        <h3>GR REPORT</h3>
                    </div>
                    <div class="card-body" style="display: inline; padding-left: 0px; padding-right: 0px; padding-bottom: 0px;">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="col-md-12">

                                    <div class="row ">
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
                                                <asp:DropDownList ID="ddlclass" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlclass_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-2" runat="server"  style="padding-top: 30px;">
                                        </div>
                                        <div class="col-md-2" runat="server" style="padding-top: 30px;">
                                            <asp:Button ID="getexcel" class="btn btn-success form-control" runat="server" OnClick="getexcel_Click" Text="GR Data" />
                                        </div>
                                        <div class="col-md-2" runat="server"  style="padding-top: 30px;">
                                            <asp:Button ID="getidexcel" class="btn btn-success form-control" runat="server" OnClick="getidexcel_Click" Text="ID Card Data" />
                                        </div>
                                        <div class="col-md-2" runat="server"  style="padding-top: 30px;">
                                            <asp:Button ID="clear" class="btn btn-success form-control" OnClick="clear_Click" runat="server" Text="Clear" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="margin-top: 10px">
                                    <%-- GridView --%>
                                    <div class="col-md-12">

                                        <div class="card-panel" runat="server" id="grid_card" style="background-color: white; margin-left: 0px; margin-right: 0px;">
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>
                                                    <div class="well well-lg" runat="server" id="grid_show" >
                                                        <asp:GridView ID="grid1" runat="server"   AutoGenerateColumns="false" CssClass="table table-hover table-striped table-bordered">                                                        
                                                            <Columns>



                                                                <asp:TemplateField HeaderText="Serial No">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="srno" runat="server" Text='<%#Container.DataItemIndex+1%>' AutoPostBack="true"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="STUDENT ID">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblid" runat="server" Text='<%# Eval("Student_id") %>' Style="margin: 9px;"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="STUDENT NAME" ControlStyle-Width="400px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblstud_name" runat="server" Text='<%# Eval("Student_Name") %>' Style="margin: 9px;"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="GR No" >
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgr" runat="server" Text='<%# Eval("gr_no") %>' Style="margin: 9px;"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Category" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblcat" runat="server" Text='<%# Eval("cat") %>' Style="margin: 9px;"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Date of Birth" >
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbldate" runat="server" Text='<%# Eval("Date") %>' Style="margin: 9px;"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Form id" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblfrm" runat="server" Text='<%# Eval("form_id") %>' Style="margin: 9px;"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                           

                                                        </asp:GridView>


                                                    </div>


                                                    <div class="well well-lg" runat="server" visible="false" id="gridcard2" style="">
                                                        <asp:GridView ID="grid2" runat="server"  Style="" AutoGenerateColumns="False" OnRowDataBound="grid2_RowDataBound" CssClass="table table-hover table-striped table-bordered" >
                                                         
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>


                                                                        <tr style="border: 0px">
                                                                            <th style="border: 0px; font-family: Calibri; text-align: center" colspan="8">Report Date:-
                                                                                <asp:Label ID="dte" runat="server"></asp:Label></th>
                                                                        </tr>

                                                                        <tr style="border: 0px">
                                                                            <th style="border: 0px; font-family: Calibri; text-align: center" colspan="8">LATE SHRI VISHNU WAMAN THAKUR CHARITABLE TRUST'S</th>
                                                                        </tr>
                                                                        <tr style="border: 0px">
                                                                            <th style="border: 0px; font-family: Calibri; text-align: center" colspan="8">UTKARSHA VIDYALAYA</th>
                                                                        </tr>
                                                                        <tr style="border: 0px">
                                                                            <th style="border: 0px; font-family: Calibri; text-align: center" colspan="8">Medium : 
                                                                                <asp:Label ID="med" runat="server"></asp:Label>
                                                                                Class : 
                                                                                <asp:Label ID="cls" runat="server"></asp:Label></th>
                                                                        </tr>


                                                                        <tr>
                                                                        </tr>

                                                                        <tr>
                                                                        </tr>

                                                                        <td style="border: 0px"></td>
                                                                        <td style="text-align: center">Sr No</td>

                                                                        <td style="text-align: center">STUDENT ID</td>

                                                                        <td style="text-align: center">STUDENT NAME</td>
                                                                        <td style="text-align: center">GR No</td>
                                                                        <td style="text-align: center">Category</td>

                                                                        <td style="text-align: center">Date of Birth</td>
                                                                        <td style="text-align: center">Form id</td>



                                                                    </HeaderTemplate>

                                                                    <ItemTemplate>

                                                                        <td id="Td11" runat="server">
                                                                            <asp:PlaceHolder ID="PlaceHolder11" runat="server">
                                                                                <asp:Label ID="srno" runat="server" Text='<%#Container.DataItemIndex+1%>' AutoPostBack="true"></asp:Label>
                                                                            </asp:PlaceHolder>
                                                                        </td>

                                                                        <td id="Td1" runat="server">
                                                                            <asp:PlaceHolder ID="PlaceHolder1" runat="server">
                                                                                <asp:Label ID="lblid" runat="server" Text='<%# Eval("Student_id") %>' Style="margin: 9px;"></asp:Label>
                                                                            </asp:PlaceHolder>
                                                                        </td>

                                                                        <td id="Td13" runat="server">
                                                                            <asp:PlaceHolder ID="PlaceHolder13" runat="server">
                                                                                <asp:Label ID="lblstud_name" runat="server" Text='<%# Eval("Student_Name") %>' Style="margin: 9px;"></asp:Label>
                                                                            </asp:PlaceHolder>
                                                                        </td>

                                                                        <td id="Td3" runat="server">
                                                                            <asp:PlaceHolder ID="PlaceHolder3" runat="server">
                                                                                <asp:Label ID="lblgr" runat="server" Text='<%# Eval("gr_no") %>' Style="margin: 9px;"></asp:Label>
                                                                            </asp:PlaceHolder>
                                                                        </td>

                                                                        <td id="Td16" runat="server">
                                                                            <asp:PlaceHolder ID="PlaceHolder16" runat="server">
                                                                                <asp:Label ID="lblcat" runat="server" Text='<%# Eval("cat") %>' Style="margin: 9px;"></asp:Label>
                                                                            </asp:PlaceHolder>
                                                                        </td>

                                                                        <td id="Td17" runat="server">
                                                                            <asp:PlaceHolder ID="PlaceHolder17" runat="server">
                                                                                <asp:Label ID="lbldate" runat="server" Text='<%# Eval("Date") %>' Style="margin: 9px;"></asp:Label>
                                                                            </asp:PlaceHolder>
                                                                        </td>
                                                                        <td id="Td18" runat="server">
                                                                            <asp:PlaceHolder ID="PlaceHolder18" runat="server">
                                                                                <asp:Label ID="lblfrm" runat="server" Text='<%# Eval("form_id") %>' Style="margin: 9px;"></asp:Label>
                                                                            </asp:PlaceHolder>
                                                                        </td>



                                                                    </ItemTemplate>


                                                                </asp:TemplateField>
                                                            </Columns>

                                                        </asp:GridView>
                                                    </div>



                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>


                                        <div class="card-panel" runat="server" id="gridcard3" style="background-color: white; margin-left: 0px; margin-right: 0px;">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <div class="well well-lg" runat="server" id="grid3" style="width:100%">
                                                        <asp:GridView ID="GridView1" runat="server"  Style=" width: 100%" AutoGenerateColumns="false" CssClass="table table-hover table-striped table-bordered">
                                                           

                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Serial No">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="srno" runat="server" Text='<%#Container.DataItemIndex+1%>' AutoPostBack="true"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="NAME OF THE STUDENT" ControlStyle-Width="400px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblid" runat="server" Text='<%# Eval("NAME OF THE STUDENT") %>' Style="margin: 9px;"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="ADDRESS" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblstud_name" runat="server" Text='<%# Eval("ADDRESS") %>' Style="margin: 9px;"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="GR No" >
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgr" runat="server" Text='<%# Eval("GR NO") %>' Style="margin: 9px;"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="ROLL NO">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblcat" runat="server" Text='<%# Eval("ROLL NO") %>' Style="margin: 9px;"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="STANDARD">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbldate" runat="server" Text='<%# Eval("STANDARD") %>' Style="margin: 9px;"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="DIVISION">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblfrm" runat="server" Text='<%# Eval("DIVISION") %>' Style="margin: 9px;"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="CONTACT1" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblfrm" runat="server" Text='<%# Eval("CONTACT 1") %>' Style="margin: 9px;"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="CONTACT2" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblfrm" runat="server" Text='<%# Eval("CONTACT 2") %>' Style="margin: 9px;"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="AADHAR NO" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblfrm" runat="server" Text='<%# Eval("AADHAR NO") %>' Style="margin: 9px;"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="SARAL ID" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblfrm" runat="server" Text='<%# Eval("SARAL ID") %>' Style="margin: 9px;"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="BANK ACCOUNT NO" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblfrm" runat="server" Text='<%# Eval("BANK ACCOUNT NO") %>' Style="margin: 9px;"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="BRANCH NAME" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblfrm" runat="server" Text='<%# Eval("BRANCH NAME") %>' Style="margin: 9px;"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="IFSC NO" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblfrm" runat="server" Text='<%# Eval("IFSC NO") %>' Style="margin: 9px;"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="BANK NAME" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblfrm" runat="server" Text='<%# Eval("BANK NAME") %>' Style="margin: 9px;"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="CATEGORY" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblfrm" runat="server" Text='<%# Eval("CATEGORY") %>' Style="margin: 9px;"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="CASTE" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblfrm" runat="server" Text='<%# Eval("CASTE") %>' Style="margin: 9px;"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="SUBCASTE" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblfrm" runat="server" Text='<%# Eval("SUB CASTE") %>' Style="margin: 9px;"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>


                                                        </asp:GridView>


                                                    </div>

                                                    <div class="WordWrap">
                                                        <div class="well well-lg" runat="server" visible="false" id="gridcard5" style="overflow: scroll; height: auto; WIDTH: 100%; OVERFLOW-X: scroll;">
                                                            <asp:GridView ID="GridView2" runat="server" Font-Size="12pt" Style="text-align: center; border: 2px solid" AutoGenerateColumns="False" OnRowDataBound="GridView2_RowDataBound" CssClass="table table-hover table-striped table-bordered" BorderColor="Black">
                                                                <RowStyle HorizontalAlign="Center"></RowStyle>
                                                                <Columns>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>


                                                                            <tr style="border: 0px">
                                                                                <th style="border: 0px; font-family: Calibri; text-align: center" colspan="17">Report Date:-
                                                                                    <asp:Label ID="dte" runat="server"></asp:Label></th>
                                                                            </tr>

                                                                            <tr style="border: 0px">
                                                                                <th style="border: 0px; font-family: Calibri; text-align: center" colspan="17">LATE SHRI VISHNU WAMAN THAKUR CHARITABLE TRUST'S</th>
                                                                            </tr>
                                                                            <tr style="border: 0px">
                                                                                <th style="border: 0px; font-family: Calibri; text-align: center" colspan="17">UTKARSHA VIDYALAYA</th>
                                                                            </tr>
                                                                            <tr style="border: 0px">
                                                                                <th style="border: 0px; font-family: Calibri; text-align: center" colspan="17">Medium : 
                                                                                    <asp:Label ID="med" runat="server"></asp:Label>
                                                                                    Class : 
                                                                                    <asp:Label ID="cls" runat="server"></asp:Label></th>
                                                                            </tr>


                                                                            <tr>
                                                                            </tr>

                                                                            <tr>
                                                                            </tr>

                                                                            <td style="border: 0px"></td>
                                                                            <td style="text-align: center">Sr No</td>

                                                                            <td style="text-align: center">NAME OF THE STUDENT</td>

                                                                            <td style="text-align: center">ADDRESS</td>
                                                                            <td style="text-align: center">GR No</td>
                                                                            <td style="text-align: center">ROLL NO</td>

                                                                            <td style="text-align: center">STANDARD</td>
                                                                            <td style="text-align: center">DIVISION</td>
                                                                            <td style="text-align: center">CONTACT1</td>

                                                                            <td style="text-align: center">CONTACT2</td>
                                                                            <td style="text-align: center">AADHAR NO</td>

                                                                            <td style="text-align: center">SARAL ID</td>
                                                                            <td style="text-align: center">BANK ACCOUNT NO</td>

                                                                            <td style="text-align: center">BRANCH NAME</td>
                                                                            <td style="text-align: center">CATEGORY</td>
                                                                            <td style="text-align: center">CASTE</td>

                                                                            <td style="text-align: center">SUB CASTE</td>




                                                                        </HeaderTemplate>

                                                                        <ItemTemplate>

                                                                            <td id="Td11" runat="server">
                                                                                <asp:PlaceHolder ID="PlaceHolder11" runat="server">
                                                                                    <asp:Label ID="srno" runat="server" Text='<%#Container.DataItemIndex+1%>' AutoPostBack="true"></asp:Label>
                                                                                </asp:PlaceHolder>
                                                                            </td>

                                                                            <td id="Td1" runat="server">
                                                                                <asp:PlaceHolder ID="PlaceHolder1" runat="server">
                                                                                    <asp:Label ID="lblid" runat="server" Text='<%# Eval("NAME OF THE STUDENT") %>' Style="margin: 9px;"></asp:Label>
                                                                                </asp:PlaceHolder>
                                                                            </td>

                                                                            <td id="Td13" runat="server">
                                                                                <asp:PlaceHolder ID="PlaceHolder13" runat="server">
                                                                                    <asp:Label ID="lblstud_name" runat="server" Text='<%# Eval("ADDRESS") %>' Style="margin: 9px;"></asp:Label>
                                                                                </asp:PlaceHolder>
                                                                            </td>

                                                                            <td id="Td3" runat="server">
                                                                                <asp:PlaceHolder ID="PlaceHolder3" runat="server">
                                                                                    <asp:Label ID="lblgr" runat="server" Text='<%# Eval("GR No") %>' Style="margin: 9px;"></asp:Label>
                                                                                </asp:PlaceHolder>
                                                                            </td>

                                                                            <td id="Td16" runat="server">
                                                                                <asp:PlaceHolder ID="PlaceHolder16" runat="server">
                                                                                    <asp:Label ID="lblcat" runat="server" Text='<%# Eval("ROLL NO") %>' Style="margin: 9px;"></asp:Label>
                                                                                </asp:PlaceHolder>
                                                                            </td>
                                                                            <%--5--%>
                                                                            <td id="Td17" runat="server">
                                                                                <asp:PlaceHolder ID="PlaceHolder17" runat="server">
                                                                                    <asp:Label ID="lbldate" runat="server" Text='<%# Eval("STANDARD") %>' Style="margin: 9px;"></asp:Label>
                                                                                </asp:PlaceHolder>
                                                                            </td>
                                                                            <td id="Td18" runat="server">
                                                                                <asp:PlaceHolder ID="PlaceHolder18" runat="server">
                                                                                    <asp:Label ID="lblfrm" runat="server" Text='<%# Eval("DIVISION") %>' Style="margin: 9px;"></asp:Label>
                                                                                </asp:PlaceHolder>
                                                                            </td>
                                                                            <td id="Td2" runat="server">
                                                                                <asp:PlaceHolder ID="PlaceHolder2" runat="server">
                                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("CONTACT 1") %>' Style="margin: 9px;"></asp:Label>
                                                                                </asp:PlaceHolder>
                                                                            </td>

                                                                            <td id="Td4" runat="server">
                                                                                <asp:PlaceHolder ID="PlaceHolder4" runat="server">
                                                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("CONTACT 2") %>' Style="margin: 9px;"></asp:Label>
                                                                                </asp:PlaceHolder>
                                                                            </td>

                                                                            <td id="Td5" runat="server">
                                                                                <asp:PlaceHolder ID="PlaceHolder5" runat="server">
                                                                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("AADHAR NO") %>' Style="margin: 9px;"></asp:Label>
                                                                                </asp:PlaceHolder>
                                                                            </td>

                                                                            <td id="Td6" runat="server">
                                                                                <asp:PlaceHolder ID="PlaceHolder6" runat="server">
                                                                                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("SARAL ID") %>' Style="margin: 9px;"></asp:Label>
                                                                                </asp:PlaceHolder>
                                                                            </td>

                                                                            <td id="Td7" runat="server">
                                                                                <asp:PlaceHolder ID="PlaceHolder7" runat="server">
                                                                                    <asp:Label ID="Label5" runat="server" Text='<%# Eval("BANK ACCOUNT NO") %>' Style="margin: 9px;"></asp:Label>
                                                                                </asp:PlaceHolder>
                                                                            </td>
                                                                            <td id="Td8" runat="server">
                                                                                <asp:PlaceHolder ID="PlaceHolder8" runat="server">
                                                                                    <asp:Label ID="Label6" runat="server" Text='<%# Eval("BRANCH NAME") %>' Style="margin: 9px;"></asp:Label>
                                                                                </asp:PlaceHolder>
                                                                            </td>
                                                                            <td id="Td9" runat="server">
                                                                                <asp:PlaceHolder ID="PlaceHolder9" runat="server">
                                                                                    <asp:Label ID="Label7" runat="server" Text='<%# Eval("CATEGORY") %>' Style="margin: 9px;"></asp:Label>
                                                                                </asp:PlaceHolder>
                                                                            </td>

                                                                            <td id="Td10" runat="server">
                                                                                <asp:PlaceHolder ID="PlaceHolder10" runat="server">
                                                                                    <asp:Label ID="Label8" runat="server" Text='<%# Eval("CASTE") %>' Style="margin: 9px;"></asp:Label>
                                                                                </asp:PlaceHolder>
                                                                            </td>

                                                                            <td id="Td12" runat="server">
                                                                                <asp:PlaceHolder ID="PlaceHolder12" runat="server">
                                                                                    <asp:Label ID="Label9" runat="server" Text='<%# Eval("SUB CASTE") %>' Style="margin: 9px;"></asp:Label>
                                                                                </asp:PlaceHolder>
                                                                            </td>

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
                            </ContentTemplate>
                        </asp:UpdatePanel>


                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-5 ">
                                </div>


                                <div class="col-md-3" style="margin-top:10px">
                                    <asp:UpdatePanel runat="server" ID="updt4">
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="exportexcel" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <asp:Button ID="exportexcel" OnClick="exportexcel_Click" Text="Export Excel" runat="server" Visible="false" CssClass="btn btn-success form-control" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>



                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
     <script type="text/javascript">
      var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    
                    createDataTable();
                }
            });
         };

     </script>

         <script type="text/javascript">
             var prm = Sys.WebForms.PageRequestManager.getInstance();
             if (prm != null) {
                 prm.add_endRequest(function (sender, e) {
                     if (sender._postBackSettings.panelsToUpdate != null) {

                         createDataTable1();
                     }
                 });
             };

     </script>

       <script type="text/javascript">
           var prm = Sys.WebForms.PageRequestManager.getInstance();
           if (prm != null) {
               prm.add_endRequest(function (sender, e) {
                   if (sender._postBackSettings.panelsToUpdate != null) {

                       createDataTable2();
                   }
               });
           };

       </script>
     

        <script type="text/javascript">
            createDataTable();
            function createDataTable() {
                $('#<%= grid1.ClientID %>').DataTable();
            }
         </script>
    <script type="text/javascript">
        createDataTable1();
        function createDataTable1() {
            $('#<%= grid2.ClientID %>').DataTable();
        }
    </script>
   

      <script type="text/javascript">
          createDataTable1();
          function createDataTable2() {
              $('#<%= GridView1.ClientID %>').DataTable();
          }
      </script>
   
   </asp:Content>

