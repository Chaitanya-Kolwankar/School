<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bonafide_Report.aspx.cs" Inherits="Bonafide_Report" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
     <script src="jquery/dist/jquery.min.js"></script>
    <link href="bootstrap-4.0.0-dist/css/bootstrap.min.css" rel="stylesheet" />
    <%--<link href="bootstrap-4.0.0-dist/css/bootstrap.min.css" rel="stylesheet" />--%>
    <style>
        .table > thead > tr > th, .table > tbody > tr > th, .table > tfoot > tr > th, .table > thead > tr > td, .table > tbody > tr > td, .table > tfoot > tr > td {
            padding: 0.156em;
            border: 3px solid black;
            margin-bottom: -20px;
            text-align: center;
            font-size: 25px;
        }

        .itemCss {
            margin-left: 5px;
            font-size: 10px;
        }

        .table {
            border-collapse: collapse;
            margin-bottom: 10px;
        }

        @page {
            size: A4;
        }
    </style>
    <%--<link href="vendors/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet" />--%>
</head>
<body style="margin: 0px;">
    <form id="form1" runat="server">
        <%--<asp:ScriptManager runat="server" ID="scr"></asp:ScriptManager>--%>
         <div class="container-fluid" style="width: 100%;">
            <table style="width: 100%; margin-top: 80px">
                <tr>
                    <td style="width: 5%">
                        <img src="image/logo.png" style="height: 200px; width: 230px" />
                    </td>
                    <td style="font-weight: bold; text-align: center;">
                        <span style="font-family: 'Times New Roman'; font-size: 25px;">TULINJ EDUCATION TRUST'S</span><br />
                        <span style="font-size: 60px;">PRIMARY SCHOOL</span><br />
                        <span style="font-size: 18px;">ENGLISH MEDIUM</span><br />
                        <span style="font-size: 18px;">Tulinj, Nallasopara (E).</span><br />
                        <span style="font-size: 18px;">Recognition No. : Prim Ed. 719/6810</span><br />
                        <span style="font-size: 18px;">Z. P. Thane Dated 03.05.1993</span><br />
                        <span style="font-size: 18px;">( Recognised by Govt. of Maharashtra State India )</span><br />
                        <span style="font-size: 25px;">U-DISE:27361716703</span>
                    </td>
                </tr>
            </table>
             <div style="border-bottom: 1px solid black;"></div>
            <div class="row" style="font-weight: bold; font-size: 50px; text-align: center; font-family: 'Times New Roman'; margin-top: 30px">
                <div class="col-lg-12">
                    <span>BONAFIDE CERTIFICATE</span>
                </div>
            </div>

            <div class="row">
                <div class="col-lg-12" style="font-size: 20px;">
                    G. R. No.<span ><asp:Label ID="lbl_grno" runat="server" ></asp:Label></span>   <span style="float: right;">DATE:-<asp:Label ID="lbl_dt"  runat="server" ></asp:Label></span>
                </div>
            </div>

            <table style="width: 100%; font-size: 29px; margin-top: 50px">
                <tr>
                    <td colspan="2"><span style="padding-left: 15%;">This is to Certify that <asp:Label ID="lbl_gender" runat="server"></asp:Label>:  <asp:Label ID="lbl_name" runat="server"></asp:Label></span>
                        <asp:Label ID="iswas" runat="server"></asp:Label> a bonafide Student of this institution, at  <asp:Label ID="lblprespev" runat="server"></asp:Label>  studying in
                        Std. <asp:Label ID="lblstd" runat="server"></asp:Label>
                         <asp:Label ID="lblstandard" runat="server"></asp:Label>
                        for the academic year
                       <asp:Label ID="acdemicyear" runat="server"> </asp:Label>.
                    </td>
                    
                </tr>
               <%-- <tr>
                    <td colspan="2"></td>
                </tr>
                <tr>
                    <td colspan="2"> </td>
                </tr>--%>
               
                                                 
                                                    
                                                    
                <tr>
                    <td colspan="2"> <asp:Label style="padding-left:200px" ID="lbl_gender2"  runat="server"></asp:Label> date of birth as recorded in our General Register is
                         <asp:Label ID="lbl_dob" runat="server"></asp:Label>. 
                        in words:    <asp:Label ID="lbl_dob_wrds" runat="server"></asp:Label>
                        and Religion & Caste is
                       <asp:Label ID="lblcast" runat="server"></asp:Label>.
                    </td>
                </tr>
               <%-- <tr>
                    <td colspan="2">
                    </td>
                </tr>--%>
            </table>
            <div class="row" style="font-size: 29px; margin-top: 40px;">
                <div class="col-lg-12">
                    <span style="padding-left: 18px">To the best of our Knowledge  <asp:Label ID="lblshe_he5" runat="server"></asp:Label> bears a good moral character.</span>
                </div>
            </div>

            <table style="width: 100%; margin-top: 200px; text-align: center;font-size:20px">
                <tr>
                    <td>
                      <%-- <asp:UpdatePanel runat="server">
                           <ContentTemplate>--%>
                                 <asp:Image ID="studentimg"  runat="server" style="height:200px;width:200px" />
                          <%-- </ContentTemplate>
                       </asp:UpdatePanel>--%>
                      </td>
                    <td style="text-align:center;padding-top:75px">
                        <span style="float: right">Head - Mistress</span><br />
                        <span style="float: right">Tulinj Education Trust's</span><br />
                        <span style="float: right">PRIMARY SCHOOL</span><br />
                        <span style="float: right">English Medium</span><br />
                        <span style="float: right">Tulinj, Nallasopara (E).</span>
                    </td>
                </tr>
            </table>
        </div>
       
      
    </form>
    <script type="text/javascript">
        window.onload = function () {
            window.print()

        }
    </script>
</body>
</html>
