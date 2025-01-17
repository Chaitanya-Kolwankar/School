<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bonafide_report_secondary.aspx.cs" Inherits="Bonafide_report_secondary" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Bonafide Report</title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <script src="jquery/dist/jquery.min.js"></script>
    <link href="bootstrap-4.0.0-dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="dist/css/icons/font-awesome/css/fontawesome.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.2.1/css/all.min.css" integrity="sha512-MV7K8+y+gLIBoVD59lQIYicR65iaqukzvf/nwasF0nqhPay5w/9lJmVM2hMDcnK1OnMGCdVK+iQrJ7lzPJQd1w==" crossorigin="anonymous" referrerpolicy="no-referrer" />
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
            <div style=" margin-top: 240px"></div>
            <div class="row" style="font-size: 40px; text-align: center; font-family: 'Constantia'; margin-top: 30px">
                <div class="col-lg-12">
                    <span><b><i class="fa-solid fa-asterisk"></i>  BONAFIDE CERTIFICATE  <i class="fa-solid fa-asterisk"></i></b></span>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12" style="font-size: 27px; margin-top: 20px; font-family: 'Constantia';">
                    <span><b>Saral ID: </b></span><span>
                        <asp:Label ID="lbl_saral" Style="font-family: 'Times New Roman'" runat="server"></asp:Label></span>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-lg-12" style="font-size: 27px; font-family: 'Constantia';">
                    <span><b>G. R. No.: </b></span><span>
                        <asp:Label ID="lbl_grno" runat="server" Style="font-family: 'Times New Roman'"></asp:Label></span>   <span style="float: right;"><b>Date: </b>
                            <asp:Label ID="lbl_dt" Style="font-family: 'Times New Roman'" runat="server"></asp:Label></span>
                </div>
            </div>

            <table style="font-size: 30px; margin-top: 100px; margin-left: 60px; margin-right: 30px; font-family: 'Constantia';">

                <tr style="padding-bottom: 5px;">
                    <td colspan="2"><span style="margin-left:60px;"> This is to certify that
                        <asp:Label ID="lbl_gender" runat="server"></asp:Label>
                        <asp:Label ID="lbl_name" runat="server"></asp:Label>
                    </span>
                        <asp:Label ID="iswas" runat="server"></asp:Label>
                        a bonafide Student of this institution, at
                        <asp:Label ID="lblprespev" runat="server"></asp:Label>
                        studying in standard 
                       <asp:Label ID="lblstd" runat="server" Style="font-family: 'Times New Roman'"></asp:Label>
                        <asp:Label ID="lblstandard" runat="server"></asp:Label>
                        for the academic year 
                        <asp:Label ID="acdemicyear" runat="server" Style="font-family: 'Times New Roman'"></asp:Label>.</td>
                </tr>

                <tr style="padding-bottom: 5px; padding-top: 5px">
                    <td>
                        <asp:Label ID="lbl_gender2" runat="server" style="margin-left:60px;"></asp:Label>
                        date of birth as recorded in our General Register is 
                       <asp:Label ID="lbl_dob" runat="server" Style="font-family: 'Times New Roman'"></asp:Label>
                        in words
                        <asp:Label ID="lbldobword" runat="server" Style="font-family: 'Times New Roman'"></asp:Label>
                        and Religion is
                        <asp:Label ID="lblreligion" runat="server"></asp:Label>
                        and Caste is
                        <asp:Label ID="lblcast" runat="server"></asp:Label>.
                    </td>
                </tr>
                <tr>
                    <td style="padding-top: 30px;">
                        <span style="margin-left:60px;">To the best of our Knowledge
                            <asp:Label ID="lblshe_he5" runat="server"></asp:Label>
                            bears a good moral character.</span>
                    </td>
                </tr>

            </table>

            <table style="width: 100%; font-size: 30px; font-family: 'Constantia'; margin-top: 80px;">
                <tr>
                    <td style="text-align: center; width: 30%; padding-top: 157px;"></td>
                    <td style="text-align: center; width: 30%;">
                        <asp:Image ID="studentimg" runat="server" Style="height: 200px; width: 200px" /></td>
                     <td style="text-align: center; width: 30%; padding-top: 157px;"></td>
                </tr>
                <tr>
                    <td style="text-align: center; width: 30%; padding-top: 157px;"></td>
                    <td style="text-align: center; width: 30%;"></td>
                       
                    <td style="text-align: center; width: 30%; padding-top: 40px;">Head-Mistress</td>
                </tr>
            </table>
            <div style="margin-top:40px;margin-left: 55px;">
                <span style="width: 100%; font-size: 30px; font-family: 'Constantia';">
                   Note: All information given as per school's<br />  &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp general register.
                </span>
            </div>
        </div>
    </form>
    <script>
        $(document).ready(function () {
            window.print();
        });
    </script>
</body>
</html>

