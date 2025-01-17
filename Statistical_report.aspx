<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Statistical_report.aspx.cs" Inherits="Statistical_report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

        <script src="graphjs/jquery.jqChart.min.js"></script>  
     <script src="graphjs/jquery-1.11.1.min.js"></script>
   <link href="graphjs/jquery.jqChart.css" rel="stylesheet" />
 
    <script src="graphjs/jquery.jqRangeSlider.min.js"></script>
    <script src="graphjs/jquery.mousewheel.js"></script>
    <script src="graphjs/excanvas.js"></script>
    <style>
        @import url(https://fonts.googleapis.com/css?family=Open+Sans:400,700);

        @keyframes bake-pie {
            from {
                transform: rotate(0deg) translate3d(0,0,0);
            }
        }

        body {
            font-family: "Open Sans", Arial;
            background: #EEE;
        }

        main {
            width: 400px;
            margin: 30px auto;
        }

        section {
            margin-top: 30px;
        }

        .pieID {
            display: inline-block;
            vertical-align: top;
        }

        .pie {
            height: 200px;
            width: 200px;
            position: relative;
            margin: 0 30px 30px 0;
        }

            .pie::before {
                content: "";
                display: block;
                position: absolute;
                z-index: 1;
                width: 100px;
                height: 100px;
                background: #EEE;
                border-radius: 50%;
                top: 50px;
                left: 50px;
            }

            .pie::after {
                content: "";
                display: block;
                width: 120px;
                height: 2px;
                background: rgba(0,0,0,0.1);
                border-radius: 50%;
                box-shadow: 0 0 3px 4px rgba(0,0,0,0.1);
                margin: 220px auto;
            }

        .slice {
            position: absolute;
            width: 200px;
            height: 200px;
            clip: rect(0px, 200px, 200px, 100px);
            animation: bake-pie 1s;
        }

            .slice span {
                display: block;
                position: absolute;
                top: 0;
                left: 0;
                background-color: black;
                width: 200px;
                height: 200px;
                border-radius: 50%;
                clip: rect(0px, 200px, 200px, 100px);
            }

        .legend {
            list-style-type: none;
            padding: 0;
            margin: 0;
            background: #FFF;
            padding: 15px;
            font-size: 13px;
            box-shadow: 1px 1px 0 #DDD, 2px 2px 0 #BBB;
        }

            .legend li {
                width: 110px;
                height: 1.25em;
                margin-bottom: 0.7em;
                padding-left: 0.5em;
                border-left: 1.25em solid black;
            }

            .legend em {
                font-style: normal;
            }

            .legend span {
                float: right;
            }

        /* footer {
            position: fixed;
            bottom: 0;
            right: 0;
            font-size: 13px;
            background: #DDD;
            padding: 5px 10px;
            margin: 5px;
        } */
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row" style="margin-top: 10px">
        <div class="col-md-12">
            <div class="card">
                <div class="card-header text-white" style="background: linear-gradient(to right, #6f03bf 1%, #363695 100%) !important;">
                    <h3><b>Report</b></h3>
                    
                </div>
                <div class="card-body">
                    <div style="margin-top: 25px">


                        <div class="card-body">
                            <div>
                                <ul class="nav nav-pills mb-3" id="pills-tab" role="tablist">
                                    <li class="nav-item">
                                        <a class="nav-link active" id="pills-home-tab" data-toggle="pill" href="#report1" role="tab" aria-controls="pills-home" aria-selected="true">Date Wise</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" id="pills-profile-tab" data-toggle="pill" href="#subdata" role="tab" aria-controls="pills-profile" aria-selected="false">Group Wise</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" id="pills-contact-tab" data-toggle="pill" href="#cancel" role="tab" aria-controls="pills-contact" aria-selected="false">Date wise Cancel Admissions</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" id="pills-graph1-tab" data-toggle="pill" href="#graph1" role="tab" aria-controls="pills-graph1" aria-selected="false">Graph</a>
                                    </li>
                                </ul>


                                <div class="tab-content" id="pills-tabContent">
                                    <div id="report1" class="tab-pane fade show active" role="tabpanel" aria-labelledby="report1-tab">
                                        <div id="dropdown" class="row">
                                            <asp:UpdatePanel ID="UpdatePanel1" class="col-md-6" runat="server">
                                                <ContentTemplate>
                                                    <div class="col-md-6 ">
                                                        <br />
                                                        <asp:DropDownList ID="ddlmedium" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlmedium_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                    <div id="label" class="col-md-12 " style="float: right;">
                                                        <br />
                                                        <div class="row">
                                                            <div class="col-md-6 ">
                                                                <span style="color: red">Total No. of Applicant :
                                                                    <asp:Label ID="lblapplicant" runat="server"></asp:Label></span>
                                                            </div>
                                                            <div class="col-md-6 ">
                                                                <span style="color: red">Total No. of Admissions :
                                                                    <asp:Label ID="lbltotal" runat="server"></asp:Label>
                                                                </span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>

                                        <div id="datecard" runat="server" class="table-responsive table-hover" style="margin-top: 20px;">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <div class="table-responsive" style="width: 100%; overflow: scroll">

                                                        <asp:GridView ID="Grd_datewise" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false">
                                                            <Columns>

                                                                <asp:BoundField DataField="Class" HeaderText="Class" HeaderStyle-CssClass="alignment" FooterStyle-VerticalAlign="Middle" />
                                                                <asp:BoundField DataField="Date" HeaderText="Date" />
                                                                <asp:BoundField DataField="No. of Admissions" HeaderText="No. of Admissions" />


                                                            </Columns>
                                                            <HeaderStyle BackColor="#009ACB"  ForeColor="White"  ></HeaderStyle>
                                                        </asp:GridView>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>

                                    </div>

                                    <div id="subdata" class="tab-pane fade" role="tabpanel" aria-labelledby="subdata-tab">
                                        <div id="dropdown2" class="row">
                                            <asp:UpdatePanel ID="UpdatePanel3" class="col-md-6" runat="server">
                                                <ContentTemplate>
                                                    <div class="col-md-6">
                                                        <br />
                                                        <asp:DropDownList ID="ddlmedium1" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlmedium1_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div id="lbl2" class="col-md-12 " style="float: right;">
                                                        <br />
                                                        <div class="row">
                                                            <div class="col-md-6 ">
                                                                <span style="color: red">Total No. of Applicant:
                                                    <asp:Label ID="lblgrpapp" runat="server"></asp:Label></span>
                                                            </div>
                                                            <div class="col-md-6 ">
                                                                <span style="color: red">Total No. of Admissions:
                                                    <asp:Label ID="lblgrptotal" runat="server"></asp:Label>
                                                                </span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>

                                        <div id="grpcard" runat="server" class="table table-responsive table-hover" style="width: 100%; overflow: scroll">
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>
                                                    <br />
                                                    <asp:GridView ID="Grd_grpwise" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false">

                                                        <Columns>

                                                            <asp:BoundField DataField="Class" HeaderText="Class" HeaderStyle-CssClass="alignment" FooterStyle-VerticalAlign="Middle" />

                                                            <asp:BoundField DataField="No. of Admissions" HeaderText="No. of Admissions" />

                                                        </Columns>
                                                        <HeaderStyle BackColor="#009ACB"  ForeColor="White"  ></HeaderStyle>
                                                    </asp:GridView>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>

                                    </div>

                                    <div id="cancel" class="tab-pane fade" role="tabpanel" aria-labelledby="cancel-tab">
                                        <div id="dropdown3" class="row">
                                            <asp:UpdatePanel ID="UpdatePanel5" class="col-md-6" runat="server">
                                                <ContentTemplate>
                                                    <div class="col-md-6">
                                                        <br />
                                                        <asp:DropDownList ID="ddlmedium2" OnSelectedIndexChanged="ddlmedium2_SelectedIndexChanged" runat="server" AutoPostBack="true" CssClass="form-control">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div id="Div5" class="col-md-12 " style="float: right;">
                                                        <br />
                                                        <span style="color: red">Total No. of Admission Cancelled:
                                                    <asp:Label ID="lblcancel" runat="server"></asp:Label>
                                                        </span>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>

                                        <div id="cancard" runat="server" class="table table-responsive table-hover" style="width: 100%; overflow: scroll">
                                            <br />
                                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                <ContentTemplate>

                                                    <asp:GridView ID="gridcancel" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false">
                                                        <Columns>
                                                            <asp:BoundField DataField="Class" HeaderText="Class" HeaderStyle-CssClass="alignment" FooterStyle-VerticalAlign="Middle" />
                                                            <asp:BoundField DataField="Date" HeaderText="Date" />
                                                            <asp:BoundField DataField="No. of Admissions Cancelled" HeaderText="No. of Admissions Cancelled" />
                                                        </Columns>
                                                        <HeaderStyle BackColor="#009ACB"  ForeColor="White"  ></HeaderStyle>
                                                    </asp:GridView>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>

                                    </div>


                                    <div id="graph1" class="tab-pane fade" role="tabpanel" aria-labelledby="cancel-tab">
                                      
                                        <div id="dropdown4" class="row">
                                          <%--  <asp:UpdatePanel ID="UpdatePanel7" class="col-md-6" runat="server">
                                                <ContentTemplate>--%>
                                                    <div class="col-md-3">
                                                        <br />
                                                        <asp:DropDownList ID="ddlmedium3" runat="server" CssClass="form-control">
                                                        </asp:DropDownList>
                                                    </div>
                                               <%-- </ContentTemplate>
                                            </asp:UpdatePanel>--%>
                                        </div>
                                        <div class="row" style="margin-top:20px">
                                        <div id="jqChart" class="col-md-6" style="width: 500px; height: 500px;">
                                        </div>


                                        <div class="col-md-6" style="display:none" id="piechartdiv">
                                            
                                            <section>
                                                <div class="pieID pie">
                                                </div>
                                                <ul class="pieID legend " id="piechrt" >
                                                </ul>
                                            </section>
                                           

                                        </div>

                                        </div>
                                    </div>
                                  
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
     
   <%-- <script src="graphjs/jquery-1.11.1.min.js"></script>
   
    <script src="graphjs/jquery.jqChart.min.js"></script>
    <script src="graphjs/jquery.jqRangeSlider.min.js"></script>
    <script src="graphjs/jquery.mousewheel.js"></script>
    <script src="graphjs/excanvas.js"></script>--%>
      <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.4.1/core.js"></script>
    <%--<script src="graphjs/core.js"></script>--%>
     <script type="text/javascript">
         var $j = $.noConflict(true);
    </script>
    <script type="text/javascript">
       
        $('[id*=ddlmedium3]').change(function () {
            var med = $(this).val();
            var ay = '<%=Session["acdyear"]%>';

            var data_arr = [];
            var value_arr = [];

            var updateDetails = {

                medium: med,
                standard: "null",
                ayid: ay,
                type: "graph",
                date: "null"
            }

            $.ajax({
                type: 'POST',
                url: 'http://203.192.254.34/utkarsha_api1/utkarsha_api/statisticalreport/',
               // url: 'http://localhost:9199/statisticalrpt/',
                dataType: 'json',
                data: JSON.stringify(updateDetails),
                processData: false,
                async: false,
                contentType: 'application/json; charset=utf-8',
               
                success: function (response, textStatus, xhr) {

                    if (response.Table.length > 0) {
                        for (var i = 0; i < response.Table.length; i++) {
                            data_arr.push(response.Table[i].Class);
                            value_arr.push(response.Table[i].Admission);


                        }
                        var ar = data_arr;
                        var va = value_arr;


                        var ulval = document.getElementById('piechrt');
                        var ulvalue = ulval.childNodes.length;

                        for (i = 0; i < ulvalue; i++) {

                            ulval.removeChild(ulval.firstChild);

                        }

                        var list = "";
                        for (var i = 0; i < ar.length; i++) {
                            list += '<li style="width:250px"><em>' + ar[i] + '</em> <span>' + va[i] + '</span> </li>';
                        }
                        $('#piechrt').append(list);
                     


                        $('#jqChart').jqChart({
                            title: { text: 'Stacked Bar Chart' },
                            legend: { location: 'top' },
                            animation: { duration: 1 },
                            shadows: {
                                enabled: true
                            },
                            axes: [
                                {
                                    type: 'category',
                                    location: 'left',
                                    categories: data_arr,
                                },
                                {
                                    type: 'linear',
                                    location: 'bottom',
                                    interval: 120
                                }
                            ],
                            series: [
                                {
                                    type: 'stackedBar',
                                    title: 'Admission Count',
                                    data: value_arr,
                                    labels: {
                                      
                                        font: '12px sans-serif',
                                      

                                    },
                                },
                            ]
                        });


                        function sliceSize(dataNum, dataTotal) {
                            return (dataNum / dataTotal) * 360;
                        }
                        function addSlice(sliceSize, pieElement, offset, sliceID, color) {
                            $(pieElement).append("<div class='slice " + sliceID + "'><span></span></div>");
                            var offset = offset - 1;
                            var sizeRotation = -179 + sliceSize;
                            $("." + sliceID).css({
                                "transform": "rotate(" + offset + "deg) translate3d(0,0,0)"
                            });
                            $("." + sliceID + " span").css({
                                "transform": "rotate(" + sizeRotation + "deg) translate3d(0,0,0)",
                                "background-color": color
                            });
                        }
                        function iterateSlices(sliceSize, pieElement, offset, dataCount, sliceCount, color) {
                            var sliceID = "s" + dataCount + "-" + sliceCount;
                            var maxSize = 179;
                            if (sliceSize <= maxSize) {
                                addSlice(sliceSize, pieElement, offset, sliceID, color);
                            } else {
                                addSlice(maxSize, pieElement, offset, sliceID, color);
                                iterateSlices(sliceSize - maxSize, pieElement, offset + maxSize, dataCount, sliceCount + 1, color);
                            }
                        }
                        function createPie(dataElement, pieElement) {
                            var listData = [];
                            $(dataElement + " span").each(function () {
                                listData.push(Number($(this).html()));
                            });
                            var listTotal = 0;
                            for (var i = 0; i < listData.length; i++) {
                                listTotal += listData[i];
                            }
                            var offset = 0;
                            var color = [
                              "cornflowerblue",
                              "olivedrab",
                              "orange",
                              "tomato",
                              "crimson",
                              "purple",
                              "turquoise",
                              "forestgreen",
                              "navy",
                              "gray",
                               "red",
                               "green",
                               "brown"
                            ];
                            for (var i = 0; i < listData.length; i++) {
                                var size = sliceSize(listData[i], listTotal);
                                iterateSlices(size, pieElement, offset, i, 0, color[i]);
                                $(dataElement + " li:nth-child(" + (i + 1) + ")").css("border-color", color[i]);
                                offset += size;
                            }
                        }

                        createPie(".pieID.legend", ".pieID.pie");
                        $("#piechartdiv").css('border','solid black 1px');
                        $("#piechartdiv").css('border-radius', '14px');
                        $("#piechartdiv").css('display', 'block');
                       
                    }
                }
            });
           
        });

    </script>

</asp:Content>

