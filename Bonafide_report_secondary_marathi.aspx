<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bonafide_report_secondary_marathi.aspx.cs" Inherits="Bonafide_report_secondary_marathi" %>

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
            /*margin-bottom: -20px;*/
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
            <div style="margin-top: 260px"></div>
            <div class="row" style="font-size: 40px; text-align: center; font-family: 'Constantia'; margin-top: 10px">
                <div class="col-lg-12">
                    <span><b>दाखला</b></span>
                </div>
            </div>

            <%--     <div class="row">
                <div class="col-lg-3">
                
                <div class="" style="font-size: 27px; font-family: 'Constantia';">
                    <span><b>विद्यार्थी क्रमांक : </b></span>
                   <span><asp:Label ID="lbl_saral" Style="font-family: 'Times New Roman'" runat="server"></asp:Label></span> 
                </div></div>
                <div class="col-lg-3">
                <div class="" style="font-size: 27px; font-family: 'Constantia';">
                    <span style="float:right;"><b>दिनांक : </b></span>
                  <span>  <asp:Label ID="lbl_dt" Style="font-family: 'Times New Roman'" runat="server"></asp:Label></span>
                </div></div>
            </div>--%>
            <div class="row">
                <div class="col-lg-12" style="font-size: 27px; font-family: 'Constantia';margin-top: 20px;margin-left: 60px;">
                    <span><b>विद्यार्थी क्रमांक : </b></span><span>
                        <asp:Label ID="lbl_saral" runat="server" Style="font-family: 'Times New Roman'"></asp:Label></span>   <span style="float: right;"><b>दिनांक : </b>
                            <asp:Label ID="lbl_dt" Style="font-family: 'Times New Roman'" runat="server"></asp:Label></span>
                </div>
            </div>
            <br />


            <table style="font-size: 27px; margin-top: 10px; margin-left: 60px; margin-right: 30px; font-family: 'Constantia';">

                <tr style="padding-bottom: 50px;">
                    <td colspan="2"><span>दाखला देण्यात येतो की, सदर
                    <asp:Label ID="lbl_gender" runat="server"></asp:Label>
                        वरील शाळेत शैक्षणिक वर्ष
                    <asp:Label ID="acdemicyear" runat="server" Style="font-family: 'Times New Roman'"></asp:Label>
                        मध्ये शिकत  </span>
                        <asp:Label ID="iswas" runat="server"></asp:Label>
                        / असून 
                    <asp:Label ID="lblprespev" runat="server"></asp:Label>
                        माहिती जनरल रजि. क्रमांक
                    <asp:Label ID="lbl_grno" runat="server" Style="font-family: 'Times New Roman'"></asp:Label>
                        नुसार खालील प्रमाणे :-
                      
                    </td>
                </tr>
            </table>
            <table style="font-size: 27px; margin-top: 20px; margin-left: 60px; margin-right: 30px; font-family: 'Constantia';">
                <tr style="margin-top: 40px;">


                    <td style="margin-top: 40px;">
                        <span style="font-size: 27px; font-family: sans-serif;">०१] विद्यार्थ्याचे नाव :
                        </span>
                    </td>
                    <td style="margin-top: 40px;">
                        <asp:Label ID="lbl_name" runat="server" Style="font-size: 27px; font-family: sans-serif;margin-left:120px;"></asp:Label>
                    </td>
                </tr>

                <tr>


                    <td>
                        <span style="font-size: 27px; font-family: sans-serif;">०२] जन्मतारीख : 
                        <%--<br />
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp व 
                        <br />
                            &nbsp&nbsp&nbsp&nbsp&nbsp  जन्म दिनांक अक्षरी
                        </span><span style="float: right; font-size: 50px; margin-top: -61px; margin-right: 15px;">}</span>--%>
                    </td>
                    <td>
                        <asp:Label ID="lbl_dob" runat="server" Style="font-family: 'Times New Roman'; font-size: 27px;margin-left:120px;"></asp:Label>
                       <%-- <br />
                        <asp:Label ID="lbldobword" runat="server" Style="font-family: 'Times New Roman'; font-size: 30px;"></asp:Label>--%>
                    </td>
                </tr>
                <tr>


                    <td>
                        <span style="font-size: 27px; font-family: sans-serif; width: 50%;">०३] जात   : </span>
                    </td>
                    <td>
                        <asp:Label ID="lblcast" runat="server" Style="font-family: 'Times New Roman'; font-size: 27px;margin-left:120px;"></asp:Label>
                    </td>
                </tr>
                <tr>


                    <td>
                        <span style="font-size: 27px; font-family: sans-serif;">०४] शाळेत दाखल झाल्याची तारीख  : </span>
                    </td>
                    <td>
                        <asp:Label ID="lbl_doa" runat="server" Style="font-family: 'Times New Roman'; font-size: 27px;margin-left:120px;"></asp:Label>
                    </td>
                </tr>
                <tr>


                    <td>
                        <span style="font-size: 27px; font-family: sans-serif;">०५] सध्या शिकत असलेली इयत्ता :</span>
                    </td>
                    <td>
                        <asp:Label ID="lblstd" runat="server" Style="font-family: 'Times New Roman'; font-size: 27px;margin-left:120px;"></asp:Label>
                    </td>
                </tr>

                <tr>


                    <td>
                        <span style="font-size: 27px; font-family: sans-serif;">०६] शेरा :</span>
                    </td>
                    <td>
                        <asp:Label ID="lblremark" runat="server" Style="font-family: 'Times New Roman'; font-size: 27px;margin-left:120px;"></asp:Label>
                    </td>
                </tr>



            </table>
            <table style="width: 100%; font-size: 27px; font-family: 'Constantia'; margin-top: 60px;">
                <tr>
                    <td>
                        <span style="font-size: 27px; font-family: sans-serif;padding-left:55px;">वरील माहिती जनरल रजिस्टर वरून तपासली,माहिती खरी आहे.</span>
                    </td>
                </tr>
                </table>
             <table style="width: 100%; font-size: 27px; font-family: 'Constantia'; margin-top: 60px;">
                <tr>
                    <td style="text-align: center; width: 30%; padding-top: 157px;">शाळेचा शिक्का</td>
                    <td style="text-align: center; width: 30%;">
                        <asp:Image ID="studentimg" runat="server" Style="height: 200px; width: 200px" /></td>
                    <td style="text-align: center; width: 30%; padding-top: 157px;">मुख्याध्यापिका</td>
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
