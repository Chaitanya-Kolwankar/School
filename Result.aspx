<%@ Page Language="C#" AutoEventWireup="true" CodeFile="result.aspx.cs" Inherits="result" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<script src="jquery/dist/jquery.min.js"></script>
<head runat="server">
    <title></title>
    <style>
        table, td {
            border: 1px solid black;
            border-collapse: collapse;
        }

        table {
            width: 80%;
            margin-right: 10%;
            margin-left: 10%;
        }

        .column {
            float: left;
            width: 33.33%;
        }
    </style>
</head>
<body>
    <div id="div_tbl" runat="server"></div>
    <script src="jsForms/result.js"></script>
    <script type="text/javascript">
        var urllink = '<%= Session["url"]%>'
        var ayid = '<%=Session["acdyear"] %>'
    </script>
</body>
</html>
