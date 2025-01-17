<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bonafide_Report_Marathi.aspx.cs" Inherits="Bonafide_Report_Marathi" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>Bonafide Report</title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <script src="jquery/dist/jquery.min.js"></script>
    <link href="bootstrap-4.0.0-dist/css/bootstrap.min.css" rel="stylesheet" />
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
</head>
<body>
    <form id="form1" runat="server">
  <div class="container-fluid" style="width: 100%;">       
            <div style="border-bottom: 2px solid black;margin-top: 200px"></div>
            <div class="row" style="font-size: 50px; text-align: center; font-family: 'Constantia'; margin-top: 30px">
                <div class="col-lg-12">
                    <span><b>BONAFIDE CERTIFICATE</b></span>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12" style="font-size: 27px;margin-top:20px;font-family: 'Constantia';"">
                   <span> <b>Saral ID: </b></span><span>
                        <asp:Label ID="lbl_saral" Style="font-family:'Times New Roman'" runat="server" ></asp:Label></span>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-lg-12" style="font-size: 27px;font-family: 'Constantia';">
                   <span><b> G. R. No.: </b></span><span ><asp:Label ID="lbl_grno" runat="server" Style="font-family:'Times New Roman'"></asp:Label></span>   <span style="float: right;"><b>Date: </b><asp:Label ID="lbl_dt" Style="font-family:'Times New Roman'" runat="server" ></asp:Label></span>
                </div>
            </div>

            <table style="font-size: 30px; margin-top: 100px;margin-left: 60px;margin-right: 30px; font-family: 'Constantia';">
               
                <tr style="padding-bottom:5px;">
                    <td colspan="2"><span>This is to certify that <asp:Label ID="lbl_gender" runat="server"></asp:Label>  <asp:Label ID="lbl_name" runat="server"></asp:Label> </span><asp:Label ID="iswas" runat="server"></asp:Label> a bonafide Student of this institution, at <asp:Label ID="lblprespev" runat="server"></asp:Label> studying in standard 
                       <asp:Label ID="lblstd" runat="server" Style="font-family:'Times New Roman'"></asp:Label>
                         <asp:Label ID="lblstandard" runat="server"></asp:Label>
                         for the academic year 
                        <asp:Label ID="acdemicyear"  runat="server" style="font-family:'Times New Roman'"></asp:Label>.</td>
                </tr>

                <tr style="padding-bottom:5px; padding-top:5px">
                    <td> <asp:Label ID="lbl_gender2"  runat="server"></asp:Label> date of birth as recorded in our General Register is 
                       <asp:Label ID="lbl_dob"  runat="server" style="font-family:'Times New Roman'"></asp:Label> and caste is
                        <asp:Label ID="lblcast"  runat="server" Text="BAUDHA"></asp:Label>.
                    </td>
                </tr>
                <tr>
                        <td style="padding-top: 75px;">
                    <span >To the best of our Knowledge <asp:Label ID="lblshe_he5" runat="server"></asp:Label> bears a good moral character.</span>
               </td>
                </tr>
             
            </table>
  
            <table style="width: 100%; font-size: 30px; font-family: 'Constantia';  margin-top: 200px;">
                <tr>
                    <td style="text-align: center; width: 30%;padding-top: 157px;">Clerk</td>
                    <td style="text-align: center; width: 30%;"><asp:Image ID="studentimg"  runat="server" style="height:200px;width:200px" /></td>
                    <td style="text-align: center; width: 30%;padding-top: 157px;">Head-Mistress</td>
                </tr>
            </table>
           
        </div>
    </form>
     <script>
        $(document).ready(function () {
            window.print();
        });
     </script>
</body>
</html>
