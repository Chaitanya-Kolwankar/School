<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ProfileDashboard.aspx.cs" Inherits="ProfileDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="card-header" style="background: linear-gradient(to right,#0078bc 1%,#00beda 100%) !important;">
                <span style="color: white; font-size: 20px;">Profile Dashboard</span>
            </div>
            <div class="card card-body">
                <div class="row" style="padding-bottom: 10px">
                    <div class="col-md-4">

                        <asp:LinkButton ID="btnprsnprofile" runat="server" class="btn btn-block btn-lg btn-success" OnClick="btnprsnprofile_Click"><i class="fa fa-user">  Personal Profile</i></asp:LinkButton>
                    </div>
                    <div class="col-md-4">
                        <asp:LinkButton ID="btnEdu" runat="server" class="btn btn-block btn-lg  btn-success" OnClick="btnEdu_Click"><i class="fa fa-book"  >  Education Details</i></asp:LinkButton>
                    </div>
                    <div class="col-md-4">
                        <asp:LinkButton ID="btnProf" runat="server" class="btn btn-block btn-lg  btn-success" OnClick="btnProf_Click"><i class="fa fa-briefcase">  Experience Details</i></asp:LinkButton>
                    </div>

                </div>

               
            </div>
        </div>
    </div>

</asp:Content>

