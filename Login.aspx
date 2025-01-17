<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="Login_form" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <%--<link rel="shortcut icon" href="image/logo.png" />--%>
   <title>SCHOOL ERP</title>
     <meta charset="UTF-8" />

    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />   
    <meta name="description" content="Free Admin Template Based On Twitter Bootstrap 3.x" />
    <meta name="author" content="" />

    <meta name="msapplication-TileColor" content="#5bc0de" />
    <meta name="msapplication-TileImage" content="public/assets/img/metis-tile.png" />

    <link rel="stylesheet" href="public/assets/lib/bootstrap/css/bootstrap.css" />
    <link rel="stylesheet" href="public/assets/lib/font-awesome/css/font-awesome.css" />
    <link rel="stylesheet" href="public/assets/css/main.css" />

    <!-- metisMenu stylesheet -->
    <link rel="stylesheet" href="public/assets/lib/metismenu/metisMenu.css" />
    <!-- onoffcanvas stylesheet -->

    <link rel="stylesheet" href="public/assets/lib/onoffcanvas/onoffcanvas.css" />
    <!-- animate.css stylesheet -->
    <link rel="stylesheet" href="public/assets/lib/animate.css/animate.css" />
     <script src="jquery/dist/jquery.min.js"></script>
     <style>
         .ErrorControl {
             background-color: #FBE3E4;
             border: solid 1px Red;
         }
     </style>
</head>
<body class="login">
    <form id="Form1" runat="server">
          <asp:ScriptManager ID="script" runat="server"></asp:ScriptManager>
        <div class="form-signin" style="margin-top:180px;">
            <div class="text-center">
                <asp:Image style="width: 130px;" id="logo" runat="server" src="image/school_logo.png"/>
            </div>
            <hr />
          
            <asp:UpdatePanel ID="updt" runat="server" UpdateMode="Always">
                <ContentTemplate>

                    <div class="tab-content">
                        <div id="login" class="tab-pane active">

                            <asp:TextBox ID="txt_user" placeholder="Username" runat="server" onkeypress="return nametxt(event)" CssClass="form-control top" Style="border-radius: 10px;text-transform:uppercase;" MaxLength="8"></asp:TextBox>
                            <br />
                            <asp:TextBox ID="txt_password" placeholder="PASSWORD" runat="server" TextMode="Password" CssClass="form-control bottom" Style="border-radius: 10px;"></asp:TextBox>
                            <div class="text-center" style="font-weight: bold; font-size: 15px;">
                                <asp:Label ID="Label_lbl1" runat="server" ForeColor="Red"></asp:Label>
                                <asp:Label ID="Label_lbl2" runat="server" ForeColor="Red" style="padding-bottom:5px"></asp:Label>
                            </div>
                            <asp:Button ID="btn_login" CssClass="btn btn-lg btn-primary btn-block" runat="server" onclick="btn_login_Click" Text="LOGIN" />
                        </div>
                        <div id="forgot" class="tab-pane">
                             <asp:TextBox ID="txt_email" placeholder="mail@domain.com" runat="server" CssClass="form-control" Style="border-radius: 10px;"></asp:TextBox>
                            <div class="text-center" style="font-weight: bold; font-size: 10px; font-family:Century">
                                
                            </div>
                            <br />
                            <asp:Button ID="btn_submit" runat="server"  Text="Submit" CssClass="btn btn-lg btn-danger       "   /> 
                        </div>                        
                        <hr>
                  

                    </div>
                </ContentTemplate>

            </asp:UpdatePanel>


        </div>

        <script src="public/assets/lib/jquery/jquery.js"></script>  
        <script src="public/assets/lib/bootstrap/js/bootstrap.js"></script>

        <script>
            $(function () {
                $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {

                    localStorage.setItem('lastTab', $(this).attr('href'));
                });

                var lastTab = localStorage.getItem('lastTab');
                if (lastTab) {
                    if (lastTab == '#login' || lastTab == '#forgot') {
                        $('a[href="#current"]').tab('show');
                        $('#Tabs a[href="' + lastTab + '"]').tab('show');
                    }
                    else {
                        $('a[href="' + lastTab + '"]').tab('show');
                    }
                }
            });
        </script>
         <script type="text/javascript">
             $(document).ready(function () {
                 Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
                 function EndRequestHandler(sender, args) {

                     var lastTab = localStorage.getItem('lastTab');
                     if (lastTab) {

                         $('a[href="' + lastTab + '"]').tab('show');

                     }
                 };

             });
         </script>
        <script type="text/javascript">
            (function ($) {
                $(document).ready(function () {
                    $('.list-inline li > a').click(function () {
                        var activeForm = $(this).attr('href') + ' > form';
                        $(activeForm).addClass('animated fadeIn');
                        setTimeout(function () {
                            $(activeForm).removeClass('animated fadeIn');
                        }, 1000);
                    });
                });
            })(jQuery);
        </script>

    </form>
    <script>
        function nametxt(e) {
            isIE = document.all ? 1 : 0
            keyEntry = !isIE ? e.which : event.keyCode;
            if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry >= '48' && keyEntry <= '57'))
                return true;
            else {
                return false;
            }
        }
    </script>
</body>
</html>
