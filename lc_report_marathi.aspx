<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lc_report_marathi.aspx.cs" Inherits="lc_report_marathi" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
     <link href="bootstrap-4.0.0-dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="bootstrap-4.0.0-dist/js/bootstrap.min.js"></script>
</head>
<body>
     <form id="form1" runat="server">

 <div class="row" style="padding-top: 330px;">
            <div class="col-md-3 col-md-3 col-sm-3 col-xs-3">
            </div>

        </div>



        <div>
            <h2 style="text-align: center; color: gray"><strong></strong></h2>
        </div>
        <div class="container">

            <div class="row">


                <div class="column" style="width: 50%;">
                    <span style="font-size: 22px;  font-family: sans-serif;">जनरल रजिस्टर नं. :</span>

                    <asp:Label ID="lblgr" runat="server" Style="font-family: sans-serif; font-weight: bold;font-size: 22px;"></asp:Label>
                </div>
                <div class="column">
                    <span style="font-size: 22px; font-family: sans-serif;">अनु क्रमांक:</span>

                    <asp:Label ID="lblseatno" runat="server" Style="font-family: sans-serif;font-weight: bold; font-size: 22px;"></asp:Label>
                </div>
            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="column" style="width: 50%">
                    <span style="font-size: 22px;  font-family: sans-serif">विद्यार्थी क्रमांक : </span>

                    <asp:Label ID="lblsid" runat="server" Style="font-family: sans-serif;font-weight: bold; font-size: 22px;"></asp:Label>

                </div>
                <div class="column">
                    <span style="font-size: 22px;  font-family: sans-serif;">आधार कार्ड क्रमांक : </span>
                    <asp:Label ID="lbladhar" runat="server" Style="font-family: sans-serif;font-weight: bold; font-size: 22px;"></asp:Label>
                </div>
            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="column" style="width: 30%;">
                <span style="font-size: 22px;  font-family: sans-serif; width: 50%;">०१] विद्यार्थ्याचे संपूर्ण नाव :
                    </span>
               
                    </div>
                <div class="column" style="width: 70%;">
                     <asp:Label ID="lblstudname" runat="server" Style="font-size: 22px;font-weight: bold; font-family: sans-serif"></asp:Label><br />
                 
                   <span style="font-size: 22px;  font-family: sans-serif; width: 50%;"> आडनाव &nbsp&nbsp  नाव &nbsp&nbsp वडिलांचे नाव</span>
                </div>

            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="column" style="width: 20%;">
                 <span style="font-size: 22px;  font-family: sans-serif; ">०२] आईचे नाव : </span>
                    </div>
                <div class="column" style="width: 80%;">
                    <asp:Label ID="lblmname" runat="server" Style="font-size: 22px;font-weight: bold; font-family: sans-serif"></asp:Label>
                    </div>
                

            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="column" style="width: 50%;">
                <span style="font-size: 22px;  font-family: sans-serif; width: 50%;">०३] राष्ट्रीयत्व : </span>
                <asp:Label ID="lblnat" runat="server" Style="font-size: 22px; font-weight: bold;font-family: sans-serif"></asp:Label>
                    </div>
                <div class="column" style="width: 50%;">
                    <span style="font-size: 22px;  font-family: sans-serif; width: 50%;">  मातृभाषा : </span>
                <asp:Label ID="lblmt" runat="server" Style="font-size: 22px;font-weight: bold; font-family: sans-serif"></asp:Label>
                </div>

            </div>
        
            <div class="row" style="margin-top: 10px;">
                <div class="column" style="width: 30%;">
                    <span style="font-size: 22px;  font-family: sans-serif; width: 50%;">०४] धर्म  : </span>
                    <asp:Label ID="lblrelg" runat="server" Style="font-size: 22px;font-weight: bold; font-family: sans-serif"></asp:Label>
                </div>


                <div class="column" style="width: 30%;">
                    <span style="font-size: 22px;  font-family: sans-serif; width: 50%;"> जात   : </span>
                    <asp:Label ID="lblcast" runat="server" Style="font-size: 22px;font-weight: bold; font-family: sans-serif"></asp:Label>
                </div>


                <div class="column" style="width: 40%;">
                    <span style="font-size: 22px;  font-family: sans-serif; width: 50%;"> पोटजात    : </span>
                    <asp:Label ID="lblsubcast" runat="server" Style="font-size: 22px; font-weight: bold;font-family: sans-serif"></asp:Label>
                </div>

            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="column" style="width: 50%;">
                    <span style="font-size: 22px;  font-family: sans-serif; width: 50%;">०५] जन्मस्थळ : गाव/शहर : </span>
                    <asp:Label ID="lblpob_vlg" runat="server" Style="font-size: 22px;font-weight: bold; font-family: sans-serif"></asp:Label>
                </div>
                <div class="column" style="width: 50%;">
                    <span style="font-size: 22px; font-family: sans-serif; width: 50%;">तालुका: </span>
                    <asp:Label ID="lblpob_tal" runat="server" Style="font-size: 22px;font-weight: bold; font-family: sans-serif"></asp:Label>
                </div>

            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="column" style="width: 40%;">
                    <span style="font-size: 22px;  font-family: sans-serif; width: 50%; margin-left: 25px">जिल्हा : </span>
                    <asp:Label ID="lblpob_dis" runat="server" Style="font-size: 22px;font-weight: bold; font-family: sans-serif"></asp:Label>
                </div>
                <div class="column" style="width: 30%;">
                    <span style="font-size: 22px;  font-family: sans-serif; width: 50%;">राज्य : </span>
                    <asp:Label ID="lblpob_sta" runat="server" Style="font-size: 22px;font-weight: bold; font-family: sans-serif"></asp:Label>
                </div>
                <div class="column" style="width: 30%;">
                    <span style="font-size: 22px;  font-family: sans-serif; width: 50%;">देश :</span>
                    <asp:Label ID="lblpob_con" runat="server" Style="font-size: 22px;font-weight: bold; font-family: sans-serif"></asp:Label>
                </div>
            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="column" style="width: 50%;">
                    <span style="font-size: 22px;  font-family: sans-serif;">०६] ई. सनाप्रमाणे जन्म दिनांक 
                        <br />
                        &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp व 
                        <br />
                        &nbsp&nbsp&nbsp&nbsp&nbsp  जन्म दिनांक अक्षरी
                    </span><span style="float: right; font-size: 50px;  margin-top: -61px; margin-right: 15px;">}</span>
                </div>
                <div class="column" style="width: 50%;">
                    <asp:Label ID="lbldob" runat="server" Style="font-size: 22px;font-weight: bold; font-family: sans-serif"></asp:Label>
                    <br />
                    <asp:Label ID="lbldobinwords" runat="server" Style="font-size: 22px;font-weight: bold; font-family: sans-serif"></asp:Label>
                </div>



            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="column" style="width: 70%;">
                    <span style="font-size: 22px; font-family: sans-serif; width: 50%;">०७] या पूर्वीची शाळा : </span>
                    <asp:Label ID="lbllschool" runat="server" Style="font-size: 22px;font-weight: bold; font-family: sans-serif"></asp:Label>

                </div>
                <div class="column" style="width: 30%;">
                    <span style="font-size: 22px;  font-family: sans-serif; width: 50%;">इयत्ता : </span>
                    <asp:Label ID="lbllsstd" runat="server" Style="font-size: 22px;font-weight: bold; font-family: sans-serif"></asp:Label>
                </div>
            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="column" style="width: 50%;">
                    <span style="font-size: 22px;  font-family: sans-serif;">०८] या शाळेत प्रवेश घेतल्याची दिनांक    : </span>
                    <asp:Label ID="lbldoa" runat="server" Style="font-size: 22px;font-weight: bold; font-family: sans-serif"></asp:Label>
                </div>
                <div class="column" style="width: 50%;">
                    <span style="font-size: 22px;  font-family: sans-serif;">इयत्ता : </span>
                    <asp:Label ID="lblstddoa" runat="server" Style="font-size: 22px;font-weight: bold; font-family: sans-serif"></asp:Label>
                </div>

            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="column" style="width: 50%;">
                    <span style="font-size: 22px;  font-family: sans-serif; width: 50%;">०९] अभ्यासातील प्रगती    : </span>
                    <asp:Label ID="lblprog" runat="server" Style="font-size: 22px;font-weight: bold; font-family: sans-serif"></asp:Label>

                </div>
                <div class="column" style="width: 50%;">
                    <span style="font-size: 22px;  font-family: sans-serif; width: 50%;"> वर्तणूक        : </span>
                    <asp:Label ID="lblconduct" runat="server" Style="font-size: 22px;font-weight: bold; font-family: sans-serif"></asp:Label>
                </div>
            </div>

            <div class="row" style="margin-top: 10px;">
                <span style="font-size: 22px;  font-family: sans-serif; width: 50%;">१०] शाळा सोडल्याची दिनांक : </span>
                <asp:Label ID="lbldols" runat="server" Style="font-size: 22px;font-weight: bold; font-family: sans-serif"></asp:Label>


            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="column" style="width: 50%;">
                    <span style="font-size: 22px;  font-family: sans-serif; width: 47%;">११] कोणत्या इयत्तेत शिकत होता
                                <br />
                        &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp व 
                                <br />
                        &nbsp&nbsp&nbsp&nbsp&nbsp केव्हापासून (अक्षरी व अंकी)  </span><span style="float: right; font-size: 50px;  margin-top: -64px; margin-right: 15px;">}</span>
                </div>
                <div class="column" style="width: 50%;">
                    <asp:Label ID="lblstandard" runat="server" Style="font-size: 22px;font-weight: bold; font-family: sans-serif"></asp:Label>
                    <br />
                    <asp:Label ID="lblstand" runat="server" Style="font-size: 22px;font-weight: bold; font-family: sans-serif"></asp:Label>
                </div>

            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="column" style="width: 27%;">
                    <span style="font-size: 22px;  font-family: sans-serif; width: 50%;">१२] शाळा सोडण्याचे कारण    : </span>
                </div>
                <div class="column" style="width: 73%;">
                    <asp:Label ID="lblreason" runat="server" Style="font-size: 22px;font-weight: bold; font-family: sans-serif"></asp:Label>
                </div>


            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="column" style="width: 10%;">
                    <span style="font-size: 22px;  font-family: sans-serif; width: 50%;">१३] शेरा    : </span>
                </div>
                <div class="column" style="width: 90%;">
                    <asp:Label ID="lblremark" runat="server" Style="font-size: 22px;font-weight: bold; font-family: sans-serif"></asp:Label>
                </div>


            </div>
             <div style="border-bottom: 2px solid black;margin-top: 20px"></div>
            <div class="row" style="margin-top: 20px;">
                <span style="font-size: 20px; font-weight: bold; font-family: sans-serif; padding-left: 65px;">दाखला देण्यात येतो की, वरील माहिती शाळेतील जनरल रजिस्टर नं. १ प्रमाणे आहे.
                </span>
            </div>
            <div class="row" style="margin-top: 20px;">
                <span style="width:5%">

                </span>
                <span style="font-size: 20px; font-weight: bold; font-family: sans-serif;width:30%;text-align:center">
                    तारीख:
                </span>
                <span style="font-size: 20px; font-weight: bold; font-family: sans-serif;width:30%;text-align:center">
                    माहे:
                </span>
                <span style="font-size: 20px; font-weight: bold; font-family: sans-serif;width:30%;text-align:center">
                   सन:
                </span>
                 <span style="width:5%">

                </span>
            </div>
                <div class="row" style="margin-top: 50px;">
                <span style="width:5%">

                </span>
                <span style="font-size: 20px; font-weight: bold; font-family: sans-serif;width:30%;text-align:center">
                   वर्गशिक्षक 
                </span>
                <span style="font-size: 20px; font-weight: bold; font-family: sans-serif;width:30%;text-align:center">
                    लेखनिक
                </span>
                <span style="font-size: 20px; font-weight: bold; font-family: sans-serif;width:30%;text-align:center">
                   मुख्याध्यापिका
                </span>
                 <span style="width:5%">

                </span>
            </div>
            <div class="row" style="margin-top: 100px;" >
                <span style="font-size: 20px; font-weight: bold; font-family: sans-serif; padding-left: 55px;">टीप : शाळा सोडल्याचे दाखल्यामध्ये अनधिकृतरीत्या बदल केल्यास संबंधितांवर कायदेशीर कारवाई करण्यात येईल.
                </span>
            </div>
        </div>
    </form>
    <script type="text/javascript">
        window.onload = function () {
            window.print()
        }
    </script>

</body>
    <script>
        window.print();
    </script>
</html>
