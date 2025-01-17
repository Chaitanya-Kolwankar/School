<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Profile.aspx.cs" Inherits="Profie" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
        <div class="row" style="margin-top: 10px">

        <div class="col-md-12">
            <div class="row" style="margin-top: 10px">
                <div class="col-md-12">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <span style="font-family: Verdana; font-size: 18pt"><strong>Personal Details</strong></span>
                            <div class="pull-right">
                                <div class="form-group">
                                    <asp:Label ID="Label2" runat="server" Font-Italic="true" Text="Teacher Id:"></asp:Label>
                                    <asp:Label ID="Label3" runat="server" Font-Bold="true" ForeColor="Yellow"></asp:Label>
                                </div>
                            </div>
                        </div>

                        <div class="panel-body">
                            <form role="form">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="well well-lg">

                                            <div class="panel panel-info">
                                                <div class="panel-heading">
                                                    <div>Basic Details</div>
                                                </div>
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <asp:Label ID="lbl_first_name" runat="server" Text="First Name :"></asp:Label>
                                                                <asp:TextBox ID="txt_first_name" runat="server" CssClass="form-control" placeholder="First Name"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <asp:Label ID="lbl_middle_name" runat="server" Text="Middle Name :"></asp:Label>
                                                                <asp:TextBox ID="txt_middle_name" runat="server" CssClass="form-control" placeholder="Middle Name"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <asp:Label ID="lbl_last_name" runat="server" Text="Last Name :"></asp:Label>
                                                                <asp:TextBox ID="txt_last_name" runat="server" CssClass="form-control" placeholder="First Name"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <asp:Label ID="lbl_dob" runat="server" Text="D.O.B :"></asp:Label>
                                                                <asp:TextBox ID="txt_dob" runat="server" CssClass="form-control" placeholder="Date of Birth"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <asp:Label ID="lbl_gender" runat="server" Text="Gender :"></asp:Label>
                                                                <div class="form-control">
                                                                    <asp:RadioButton runat="server" ID="rdbMale" Text="Male" />
                                                                    <asp:RadioButton runat="server" ID="rdbFemale" Text="Female" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <asp:Label ID="lbl_marital_status" runat="server" Text="Marital Status :"></asp:Label>
                                                                <div class="form-control">
                                                                    <asp:RadioButton runat="server" ID="rdbMarried" Text="Married" />
                                                                    <asp:RadioButton runat="server" ID="rdbUnmarried" Text="Unmarried" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="form-group">
                                                                <asp:Label ID="lbl_address" runat="server" Text="Address :"></asp:Label>
                                                                <asp:TextBox ID="txt_address" runat="server" class="form-control" placeholder="Address" TextMode="MultiLine" Rows="3" disabled></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-md-12">
                                        <div class="well well-lg">
                                            <div class="panel panel-info">
                                                <div class="panel-heading">
                                                    <div>Other Details</div>
                                                </div>
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <asp:Label ID="lbl_doj" runat="server" Text="D.O.J :"></asp:Label>
                                                                <asp:TextBox ID="txt_doj" runat="server" CssClass="form-control" placeholder="Date of Joining"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <asp:Label ID="lbl_caste" runat="server" Text="Caste :"></asp:Label>
                                                                <asp:TextBox ID="txt_caste" runat="server" CssClass="form-control" placeholder="Caste"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <asp:Label ID="lbl_category" runat="server" Text="Category :"></asp:Label>
                                                                <asp:TextBox ID="txt_category" runat="server" CssClass="form-control" placeholder="Category"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <asp:Label ID="lbl_mobile_no" runat="server" Text="Mobile No :"></asp:Label>
                                                                <asp:TextBox ID="txt_mobile_no" runat="server" CssClass="form-control" placeholder="Mobile No"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <asp:Label ID="lbl_email" runat="server" Text="Email ID :"></asp:Label>
                                                                <asp:TextBox ID="txt_email" runat="server" CssClass="form-control" placeholder="Email ID"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <asp:Label ID="Label_blood_group" runat="server" Text="Blood Group :"></asp:Label>
                                                                <asp:DropDownList ID="ddlBloodGroup" CssClass="form-control" runat="server">
                                                                    <asp:ListItem>--Select--</asp:ListItem>
                                                                    <asp:ListItem>A+</asp:ListItem>
                                                                    <asp:ListItem>B+</asp:ListItem>
                                                                    <asp:ListItem>O+</asp:ListItem>
                                                                    <asp:ListItem>A-</asp:ListItem>
                                                                    <asp:ListItem>B-</asp:ListItem>
                                                                    <asp:ListItem>O-</asp:ListItem>
                                                                    <asp:ListItem>AB+</asp:ListItem>
                                                                    <asp:ListItem>AB-</asp:ListItem>

                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <asp:Label ID="lbl_department" runat="server" Text="Department:"></asp:Label>
                                                                <asp:TextBox ID="txt_department" runat="server" CssClass="form-control" placeholder="Department"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <asp:Label ID="lbl_designation" runat="server" Text="Designation:"></asp:Label>
                                                                <asp:TextBox ID="txt_designation" runat="server" CssClass="form-control" placeholder="Designation"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>



                                </div>
                            </form>
                        </div>

                        <%--<div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
                            aria-hidden="true">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span></button>
                                        <h4 class="modal-title" id="myModalLabel">Required New Password</h4>
                                    </div>
                                    <div class="modal-body">
                                        <div class="form-group">
                                            <asp:TextBox class="form-control" ID="txt_oldpasswd" runat="server" type="password" placeholder="Old Password" autofocus></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <asp:TextBox class="form-control" ID="txt_newpasswd" runat="server" type="password" placeholder="New Password"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <asp:TextBox class="form-control" ID="txt_confrmpasswd" runat="server" type="password" placeholder="Confirm Password"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-default" data-dismiss="modal">
                                            Close</button>
                                        <button type="button"  class="btn btn-primary">
                                        Save changes</button>
                                        <asp:Button ID="btnChange" Text="Save" OnClick="changepassword" runat="server" /><br />
                                        <div runat="server" id="errorMessage" visible="false" class="row topMargin alert alert-danger"></div>
                                    </div>
                                </div>
                            </div>
                        </div>--%>
                    </div>


                </div>

            </div>
        </div>
    </div>

</asp:Content>

