<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ChangePasswd.aspx.cs" Inherits="ChangePasswd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="jquery/dist/jquery.min.js"></script>

    <script src="notify-master/js/notify.js"></script>
    <link href="notify-master/css/notify.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row" style="margin-top: 15px">
        <div class="col-md-4 col-md-4 col-sm-6 col-xs-10">
            <div class="card">
                <div class="card-header text-white" style="background: linear-gradient(to right, #6f03bf 1%, #363695 100%) !important; border-radius: 7px">
                    Change Password
                </div>
                <div class="card-body">
                    <div class="row form-group">
                        <asp:TextBox class="form-control col-md-8" ID="txt_oldpasswd" runat="server" TextMode="Password" placeholder="Old Password" onkeyup="myfunction()"></asp:TextBox>
                        <asp:LinkButton runat="server" ID="btnicon" Style="margin-left: 10px; margin-top: 6px; display: none"><i class='fas fa-check' aria-hidden="true"></i></asp:LinkButton>
                        <asp:LinkButton runat="server" ID="btnclose" Style="margin-left: 20px; margin-top: 6px; display: none"><i class='fas ' aria-hidden="true">&#xf00d;</i></asp:LinkButton>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="row form-group">
                                <asp:TextBox class="form-control col-md-10" ID="txt_newpasswd" runat="server" placeholder="New Password"></asp:TextBox>
                                <asp:LinkButton runat="server" ID="btneye" Style="margin-left: 10px; margin-top: 6px; display: block" OnClick="btneye_Click"><i class='fas fa-eye' aria-hidden="true"></i></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btneyeslash" Style="margin-left: 10px; margin-top: 6px; display: block" OnClick="btneyeslash_Click"><i class='fas fa-eye-slash' aria-hidden="true"></i></asp:LinkButton>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="row form-group">
                        <asp:TextBox class="form-control col-md-10" ID="txt_confrmpasswd" runat="server" Text="password" TextMode="password" placeholder="Confirm Password" onkeyup="getfunction()"></asp:TextBox>
                        <asp:LinkButton runat="server" ID="LinkButton1" Style="margin-left: 10px; margin-top: 6px; display: none"><i class='fas fa-check' aria-hidden='true'></i></asp:LinkButton>
                        <asp:LinkButton runat="server" ID="LinkButton2" Style="margin-left: 20px; margin-top: 6px; display: none"><i class='fas ' aria-hidden="true">&#xf00d;</i></asp:LinkButton>
                    </div>
                    <div class="row form-group">
                        <div class="col-md-4"></div>
                        <div class="col-md-4">
                            <asp:Button ID="btnLogin" runat="server" Text="Save" CssClass="btn btn-block btn-success" OnClick="btnLogin_Click" />
                        </div>
                        <div class="col-md-4"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>
        function myfunction() {
            var b = '<%= Session["password"] %>';
            var n = b.length;
            var x = $("[id*=txt_oldpasswd]").val();
            var c = x.length;
            if (x == b) {
                $("[id*=btnicon]")[0].style.display = "block";
                $("[id*=btnclose]")[0].style.display = "none";
            }
            if (c > n) {
                $("[id*=btnclose]")[0].style.display = "block";
                $("[id*=btnicon]")[0].style.display = "none";

            }
            else if (c < n) {
                $("[id*=btnclose]")[0].style.display = "none";
                $("[id*=btnicon]")[0].style.display = "none";
            }
        }
        function getfunction() {
            var b = '<%= Session["password"] %>';
            var x = $("[id*=txt_newpasswd]").val();
            var z = x.length;
            var y = $("[id*=txt_confrmpasswd]").val();
            var w = y.length;
            if (x == y) {
                $("[id*=LinkButton1]")[0].style.display = "block";
                $("[id*=LinkButton2]")[0].style.display = "none";
            }
            if (w > z) {
                $("[id*=LinkButton2]")[0].style.display = "block";
                $("[id*=LinkButton1]")[0].style.display = "none";

            }
            else if (w < z) {
                $("[id*=LinkButton2]")[0].style.display = "none";
                $("[id*=LinkButton1]")[0].style.display = "none";
            }
        }

        function myFunction1() {
            var x = $('#<%= btneyeslash.ClientID %>');
            $('#<%= btneyeslash.ClientID %>').attr("type", "text");
        }
    </script>
    <script>
        function ChangeToPassField() {
            if (document.getElementById('<%= txt_newpasswd.ClientID %>').type != "password") {
                document.getElementById('<%= txt_newpasswd.ClientID %>').type = "Password";
                document.getElementById('<%= txt_newpasswd.ClientID %>').value = "";
            }
        }
        function ChangeToNormal() {
            if (document.getElementById('<%= txt_newpasswd.ClientID %>').value == "") {
                document.getElementById('<%= txt_newpasswd.ClientID %>').type = "SingleLine";
                document.getElementById('<%= txt_newpasswd.ClientID %>').value = "Password"
            }
        }
    </script>
</asp:Content>

