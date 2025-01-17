<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lc_report.aspx.cs" Inherits="lc_report" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content=" initial-scale=1" />
    <title></title>
    <link href="bootstrap-4.0.0-dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="bootstrap-4.0.0-dist/js/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">

        <div class="row" style="padding-top: 280px;">
            <div class="col-md-3 col-md-3 col-sm-3 col-xs-3">
            </div>

        </div>



        <div>
            <h2 style="text-align: center; color: gray"><strong></strong></h2>
        </div>
        <div class="container">

            <div class="row">


                <div class="column" style="width: 50%;">
                    <span style="font-size: 22px;  font-family: sans-serif;">General Register No :</span>

                    <asp:Label ID="lblgr" runat="server"  Style="font-family: sans-serif;font-weight: bold; font-size: 22px;margin-left: 10px;"></asp:Label>
                </div>
                <div class="column">
                    <span style="font-size: 22px;  font-family: sans-serif;">LC Sr No.:</span>

                    <asp:Label ID="lblseatno" runat="server" Style="font-family: sans-serif;font-weight: bold; font-size: 22px;margin-left: 10px;"></asp:Label>
                </div>
            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="column" style="width: 50%">
                    <span style="font-size: 22px;  font-family: sans-serif">Student ID : </span>

                    <asp:Label ID="lblsid" runat="server" Style="font-family: sans-serif;font-weight: bold; font-size: 22px;margin-left: 10px;"></asp:Label>

                </div>
                <div class="column">
                    <span style="font-size: 22px;  font-family: sans-serif;">Aadhar No : </span>
                    <asp:Label ID="lbladhar" runat="server" Style="font-family: sans-serif;font-weight: bold; font-size: 22px;margin-left: 10px;"></asp:Label>
                </div>
            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="column" style="width: 30%;">
                <span style="font-size: 22px;  font-family: sans-serif; width: 50%;">1] Name of the Student :
                    <br />
                    &nbsp&nbsp&nbsp&nbsp (Surname First)</span>
               
                    </div>
                <div class="column" style="width: 70%;">
                     <asp:Label ID="lblstudname" runat="server" Style="font-size: 22px;font-weight: bold; font-family: sans-serif"></asp:Label><br />
                    <span style="font-size: 22px;  font-family: sans-serif; margin-left: 50px;">Mother's Name : </span>
                    <asp:Label ID="lblmname" runat="server" Style="font-size: 22px; font-weight: bold;font-family: sans-serif;margin-left: 10px;"></asp:Label>
                </div>

            </div>
        <%--    <div class="row" style="padding-left: 22px;">
                <div class="column" style="width: 10%;">
                </div>
                

            </div>--%>
            <div class="row" style="margin-top: 10px;">
                 <div class="column" style="width: 35%;">
                <span style="font-size: 22px;  font-family: sans-serif; width: 20%;">2] Nationality : </span>
                <asp:Label ID="lblnat" runat="server" Style="font-size: 22px;font-weight: bold; font-family: sans-serif"></asp:Label>

                     </div>

                <div class="column" style="width: 50%;">
                       <span style="font-size: 22px;  font-family: sans-serif; width: 20%;">3] Mother Tongue : </span>
                <asp:Label ID="lblmt" runat="server" Style="font-size: 22px;font-weight: bold; font-family: sans-serif"></asp:Label>

                </div>


            </div>
       

             

            <div class="row" style="margin-top: 10px;">
                <div class="column" style="width: 30%;">
                    <span style="font-size: 22px;font-family: sans-serif; width: 50%;">4] Religion  : </span>
                    <asp:Label ID="lblrelg" runat="server" Style="font-size: 22px;font-weight: bold; font-family: sans-serif;margin-left: 10px;"></asp:Label>
                </div>


                <div class="column" style="width: 30%;">
                    <span style="font-size: 22px;  font-family: sans-serif; width: 50%;">5] Cast   : </span>
                    <asp:Label ID="lblcast" runat="server" Style="font-size: 22px;font-weight: bold; font-family: sans-serif;margin-left: 10px;"></asp:Label>
                </div>


                <div class="column" style="width: 40%;">
                    <span style="font-size: 22px;  font-family: sans-serif; width: 50%;">6] SubCast    : </span>
                    <asp:Label ID="lblsubcast" runat="server" Style="font-size: 22px;font-weight: bold; font-family: sans-serif;margin-left: 10px;"></asp:Label>
                </div>

            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="column" style="width: 50%;">
                    <span style="font-size: 22px;  font-family: sans-serif; width: 50%;">7] Place of Birth : Village/City : </span>
                    <asp:Label ID="lblpob_vlg" runat="server" Style="font-size: 22px;font-weight: bold; font-family: sans-serif;margin-left: 10px;"></asp:Label>
                </div>
                <div class="column" style="width: 50%;">
                    <span style="font-size: 22px;  font-family: sans-serif; width: 50%;">Taluka: </span>
                    <asp:Label ID="lblpob_tal" runat="server" Style="font-size: 22px;font-weight: bold; font-family: sans-serif;margin-left: 10px;"></asp:Label>
                </div>

            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="column" style="width: 30%;">
                    <span style="font-size: 22px;  font-family: sans-serif; width: 20%; margin-left: 2px">District : </span>
                    <asp:Label ID="lblpob_dis" runat="server" Style="font-size: 22px;font-weight: bold; font-family: sans-serif;margin-left: 10px;"></asp:Label>
                </div>
                <div class="column" style="width: 30%;">
                    <span style="font-size: 22px;  font-family: sans-serif; width: 50%;">State : </span>
                    <asp:Label ID="lblpob_sta" runat="server" Style="font-size: 22px;font-weight: bold; font-family: sans-serif;margin-left: 10px;"></asp:Label>
                </div>
                <div class="column" style="width: 40%;">
                    <span style="font-size: 22px;  font-family: sans-serif; width: 20%;">Country :</span>
                    <asp:Label ID="lblpob_con" runat="server" Style="font-size: 22px;font-weight: bold; font-family: sans-serif;margin-left: 10px;"></asp:Label>
                </div>
            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="column" style="width: 50%;">
                    <span style="font-size: 22px; font-family: sans-serif;">8] Date of Birth (month and 
                        <br />
                        &nbsp&nbsp&nbsp year According to
                        <br />
                        &nbsp&nbsp&nbsp  Christian era
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
                    <span style="font-size: 22px; font-family: sans-serif; width: 50%;">9] Previous School attended : </span>
                    <asp:Label ID="lbllschool" runat="server" Style="font-size: 22px;font-weight: bold; font-family: sans-serif;margin-left: 10px;"></asp:Label>

                </div>
                <div class="column" style="width: 30%;">
                    <span style="font-size: 22px;  font-family: sans-serif; width: 50%;">Standard : </span>
                    <asp:Label ID="lbllsstd" runat="server" Style="font-size: 22px;font-weight: bold; font-family: sans-serif;margin-left: 10px;"></asp:Label>
                </div>
            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="column" style="width: 50%;">
                    <span style="font-size: 22px;  font-family: sans-serif;">10] Date of admission    : </span>
                    <asp:Label ID="lbldoa" runat="server" Style="font-size: 22px; font-weight: bold;font-family: sans-serif;margin-left: 10px;"></asp:Label>
                </div>
                <div class="column" style="width: 50%;">
                    <span style="font-size: 22px;  font-family: sans-serif;">Standard    : </span>
                    <asp:Label ID="lblstddoa" runat="server" Style="font-size: 22px;font-weight: bold; font-family: sans-serif;margin-left: 10px;"></asp:Label>
                </div>

            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="column" style="width: 50%;">
                    <span style="font-size: 22px;  font-family: sans-serif; width: 50%;">11] Progress in Studies    : </span>
                    <asp:Label ID="lblprog" runat="server" Style="font-size: 22px;font-weight: bold; font-family: sans-serif;margin-left: 10px;"></asp:Label>

                </div>
                <div class="column" style="width: 50%;">
                    <span style="font-size: 22px;  font-family: sans-serif; width: 50%;">12] Conduct    : </span>
                    <asp:Label ID="lblconduct" runat="server" Style="font-size: 22px;font-weight: bold; font-family: sans-serif;margin-left: 10px;"></asp:Label>
                </div>
            </div>

            <div class="row" style="margin-top: 10px;">
                <span style="font-size: 22px;  font-family: sans-serif; width: 50%;">13] Date of leaving the School    : </span>
                <asp:Label ID="lbldols" runat="server" Style="font-size: 22px;font-weight: bold; font-family: sans-serif"></asp:Label>


            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="column" style="width: 50%;">
                    <span style="font-size: 22px;  font-family: sans-serif; width: 47%;">14] Standard in which studying
                                <br />
                        &nbsp&nbsp&nbsp&nbsp&nbsp and
                                <br />
                        &nbsp&nbsp&nbsp&nbsp&nbsp since when (in words and figures)  </span><span style="float: right; font-size: 50px;  margin-top: -64px; margin-right: 15px;">}</span>
                </div>
                <div class="column" style="width: 50%;">
                    <asp:Label ID="lblstandard" runat="server" Style="font-size: 22px; font-weight: bold;font-family: sans-serif"></asp:Label>
                    <br />
                    <asp:Label ID="lblstand" runat="server" Style="font-size: 22px;font-weight: bold; font-family: sans-serif"></asp:Label>
                </div>

            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="column" style="width: 35%;">
                    <span style="font-size: 22px;  font-family: sans-serif; width: 50%;">15] Reason of leaving the School    : </span>
                </div>
                <div class="column" style="width: 65%;">
                    <asp:Label ID="lblreason" runat="server" Style="font-size: 22px;font-weight: bold; font-family: sans-serif"></asp:Label>
                </div>


            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="column" style="width: 15%;">
                    <span style="font-size: 22px;  font-family: sans-serif; width: 50%;">16] Remarks    : </span>
                </div>
                <div class="column" style="width: 85%;">
                    <asp:Label ID="lblremark" runat="server" Style="font-size: 22px;font-weight: bold; font-family: sans-serif"></asp:Label>
                </div>


            </div>
             <div style="border-bottom: 2px solid black;"></div>
            <div class="row" style="margin-top: 10px;">
                <span style="font-size: 20px; font-weight: bold; font-family: sans-serif; padding-left: 65px;">Certified that the above information is in accordance with the School Register No. 1
                </span>
            </div>
            <div class="row" style="margin-top: 50px;">
                <span style="font-size: 20px; font-weight: bold; font-family: sans-serif; width: 25%; text-align: center">Date:
                </span>
                <span style="font-size: 20px; font-weight: bold; font-family: sans-serif; width: 25%; text-align: center">Class Teacher
                </span>
                <span style="font-size: 20px; font-weight: bold; font-family: sans-serif; width: 25%; text-align: center">Clerk
                </span>
                <span style="font-size: 20px; font-weight: bold; font-family: sans-serif; width: 25%; text-align: center">Head-Mistress
                </span>
            </div>
            <div class="row" style="margin-top: 60px;" >
                <span style="font-size: 20px; font-weight: bold; font-family: sans-serif; ">Note: No Changes in any entry in this certificate shall be made except by the<br />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp authority issuing
                     it and any infringment of this requirement is liable to<br />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp legal action.
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
