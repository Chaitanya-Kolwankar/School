<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DepartDesig.aspx.cs" Inherits="DepartDesig" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="notify-master/css/notify.css" rel="stylesheet" />
    <link href="dist/css/icons/font-awesome/css/fontawesome.min.css" rel="stylesheet" />
    <script src="jquery/dist/jquery.min.js"></script>
    <link href="css/jquery-ui-1.10.4.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .table-container {
            height: 300px;
            padding: 0.3em;
        }

        table {
            display: flex;
            flex-flow: column;
            height: 100%;
            width: 100%;
        }

            table thead {
                flex: 0 0 auto;
                width: calc(100% - 0.9em);
            }

            table tbody {
                flex: 1 1 auto;
                display: block;
                overflow-y: scroll;
            }

                table tbody tr {
                    width: 100%;
                }

                table thead,
                table tbody tr {
                    display: table;
                    table-layout: fixed;
                }

            table td, table th {
                padding: 0.3em;
            }
    </style>
    <div class="container-fluid">
        <div class="card">
            <div id="Div1">
                <div class="card-header text-white" style="background: linear-gradient(to right, #6f03bf 1%, #363695 100%) !important; border-radius: 7px">
                    <div>
                        <h3>Department & Designation</h3>
                    </div>
                </div>

                <div class="card-body">
                    <div class="card">
                        <div class="card-header text-white" style="background: linear-gradient(to right, #6f03bf 1%, #363695 100%) !important; border-radius: 7px; padding-top: 0px; padding-bottom: 0px;" id="headingOne">
                            <h5 class="mb-0">
                                <a class="btn btn-link" data-toggle="collapse" data-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne">Department Master
                                </a>
                            </h5>
                        </div>

                        <div id="collapseOne" class="collapse" aria-labelledby="headingOne" data-parent="#Div1">
                            <div class="card-body">
                                    <div class="row">
                                        <div class="col-md-3 col-sm-6 col-xs-12">
                                            <b>Department Name</b>
                                            <input type="text" id="txtdept" class="form-control" />
                                        </div>
                                        <div class="col-md-2 col-sm-6 col-xs-12">
                                            <b>Department Prefix</b>
                                            <input type="text" id="txtprefix" class="form-control" maxlength="4" />
                                        </div>
                                        <div class="col-md-2 col-sm-6 col-xs-6">
                                            <br />
                                            <a href="#" id="btnSave" class="btn btn-block btn-success">Save</a>
                                        </div>
                                        <div class="col-md-2 col-sm-6 col-xs-6">
                                            <br />
                                            <a href="#" id="btnrefresh" class="btn btn-block btn-success">Refresh</a>
                                        </div>
                                        <div class="col-md-1" style="display: none;">
                                            <input type="text" id="txtdeparttid" class="form-control" />
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row" id="tblgrid" style="display: none; width: 100%; word-wrap: break-word; margin: 0px;">
                                        <div>
                                            <table id="tbldepfill" class="table table-container table-bordered">
                                            </table>
                                        </div>
                                    </div>
                            </div>
                        </div>
                    </div>

                    <div class="card">
                        <div class="card-header text-white" style="background: linear-gradient(to right, #6f03bf 1%, #363695 100%) !important; border-radius: 7px; padding-top: 0px; padding-bottom: 0px;" id="Div2">
                            <h5 class="mb-0">
                                <a class="btn btn-link" data-toggle="collapse" data-target="#collapsetwo" aria-expanded="true" aria-controls="collapsetwo">Designation Master
                                </a>
                            </h5>
                        </div>
                        <div id="collapsetwo" class="collapse" aria-labelledby="headingOne" data-parent="#Div1">
                            <div class="card-body">
                                    <div class="row">
                                        <div class="col-md-2 col-sm-4 col-xs-12">
                                            <b>Designation Name</b>
                                            <input type="text" id="txtdesig" class="form-control" />
                                        </div>
                                        <div class="col-md-2 col-sm-4 col-xs-6">
                                            <br />
                                            <a href="#" id="btndesave" class="btn btn-block btn-success">Save</a>
                                        </div>
                                        <div class="col-md-2 col-sm-4 col-xs-6">
                                            <br />
                                            <a href="#" id="btndesref" class="btn btn-block btn-success">Refresh</a>
                                        </div>
                                        <div class="col-md-2" style="display: none;">
                                            <input type="text" id="txtdeid" class="form-control" />
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row" id="desigtbl" style="display: none; width: 100%; word-wrap: break-word; margin: 0px;">
                                        <div>
                                            <table id="tbldesig" class="table table-container table-bordered">
                                            </table>
                                        </div>
                                    </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>
    <script src="jsForms/desigdept.js"></script>
    <script src="notify-master/js/notify.js"></script>
    <script type="text/javascript">
        var empId = '<%=Session["emp_id"] %>'
    </script>
</asp:Content>

