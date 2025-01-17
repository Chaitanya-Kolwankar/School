<%@ Page Language="C#" AutoEventWireup="true" CodeFile="secondary_feerpt.aspx.cs" Inherits="secondary_feerpt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Secondary Fee Receipt</title>
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
            <%--<div class="row">
                <div class="col-lg-3">
                    
                        <img src="image/logo.png" style="height: 200px; width: 200px" />
                    
                </div>
                <div class="col-lg-9">
                  

                        <div class="col-lg-12" style="font-weight: bold;">
                            <span style="font-family: 'Times New Roman'; font-size: 25px;">TULINJ EDUCATION TRUST'S</span><br />
                            <span style="font-size: 50px;">PRIMARY SCHOOL</span><br />
                            <span style="font-size: 25px;">(ENGLISH MEDIUM)</span><br />
                            <span style="font-size: 25px;">Reg. No. E / 8461</span><br />
                            <span style="font-size: 25px;">( GOVERNMENT RECOGNISED )</span><br />
                            <span style="font-size: 25px;">Tulinj, Nallasopara (East), Tal. Vasai, Dist. Thane - 401209.</span>
                        </div>
                   
                </div>
            </div>--%>
            <table style="width:100%">
                <tr>
                    <td>  <img src="image/logo.png" style="height: 200px; width: 200px" /></td>
                    <td style="text-align:center" ><center><b>
                          <span style="font-family: 'Times New Roman'; font-size: 30px;">TULINJ EDUCATION TRUST'S</span><br />
                    <span style="font-size: 35px;">Late. Krishna Moru Patil Deshmukh Vidyalay,</span><br />
                    <span style="font-size: 25px;">(ENGLISH MEDIUM)</span><br />
                    <span style="font-size: 20px;">Reg. No. E / 8461</span><br />
                    <span style="font-size: 25px;">( GOVERNMENT RECOGNISED )</span><br />
                    <span style="font-size: 20px;">Tulinj, Nallasopara (East), Tal. Vasai, Dist. Thane - 401209.</span>
                    </td>
                </tr>
                </table>

            <div class="row">
                <table style="width: 100%; font-size: 20px;">
                <tr>
                    <td style="padding-left: 20px;">
                        <span style="float: left;">School Index No : S. 16. 13. 003</span><br />
                        <span style="float: left;">U-DISE : 27361716704</span></td>
                    <td style="padding-right: 20px;">
                        <span style="float: right;">Phone Office : 95250 - 2432256</span><br />
                        <span style="float: right;">95250 - 2432270</span>
                    </td>
                </tr>
            </table>
            </div>
            <div style="border-bottom: 1px solid black;"></div>
            <br />
            <div class="row">
                <div class="col-lg-12" style="font-size: 20px;">
                    Ref. No. : <span id="recpt"></span><span style="float: right;">Date:-
                        <asp:Label ID="lbl_date" runat="server"></asp:Label></span>
                </div>
            </div>
            <div class="row" style="font-weight: bold; font-size: 50px; text-align: center; font-family: 'Times New Roman';">
                <div class="col-lg-12">
                    <span>CERTIFICATE</span>
                </div>
            </div>
            <br />
            <table style="width: 100%; font-size: 29px; font-family: 'Times New Roman'">
              <%--  <tr>
                    <td></td>
                   
                </tr>--%>
                <tr>
                    <td colspan="2"><span>Certified that <span id="gender"></span>: </span> <span id="txtname"></span><span> is a Student of our Educational Institute (School) of Std.
                        <asp:Label ID="lbl_std" runat="server"></asp:Label></span><span> Div.
                        <asp:Label ID="lbl_div" runat="server"></asp:Label>
                         for the Academic Year
                        <asp:Label ID="lbl_year" runat="server"> </asp:Label>
                        (i.e. From June <span id="frmyer"></span>To May  <span id="toyer"></span>).</span></td>
                </tr>
              <%--  <tr>
                    <td colspan="2"></td>
                </tr>--%>
            </table>
            <br />
            <br />
            <div class="row" style="font-size: 29px; font-family: 'Times New Roman';">
                <div class="col-lg-12">
                   <span>From Std. V to Std X. our School Fees ( Admission, Term, Monthly and Computer fees) Details is as follows :-</span>
                </div>
            </div>
            <table id="tblfees" style="width: 100%" class="table table-bordered">
              
            </table>
            <br />
            <div class="row" style="font-size: 29px; font-family: 'Times New Roman';padding-bottom:150px">
                <div class="col-lg-12">
                    <span>(In words Total Yearly Fees Rs  </span><span id="totalwords"></span>)
                </div>
                 <div class="col-lg-12">
                    <div class="row" >
                        <span style="padding-left:50px">Payment Date : <span id="dop"></span></span>
                    </div>
                    <div class="row" id="paymentdetails">
                       <span style="padding-left:50px"> Payment Details <span  id="mode"></span>/<span id="cheqno"></span>/<span id="bankname"></span>/<span id="bankbranch"></span></span>
                    </div>
                </div>
            </div>
           
            <table style="width: 100%; font-size: 25px; font-family: 'Times New Roman'; font-weight: bold">
                <tr>
                    <td style="text-align: left; width: 50%;padding-left: 100px;">Cleak</td>
                    <td style="text-align: right; width: 50%;padding-right: 100px;">Head-Mistress</td>
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
<script src="jsForms/secondary_feerpt.js"></script>

<script type="text/javascript">
    var recptno = '<%=Session["recptno"] %>'
    var urllink = '<%= Session["url"]%>'
</script>
</html>
