
$(document).ready(function () {

    filldepart();
    filldesig();

});
var deptid = "";
var desiid = "";

function filldepart() {
    $.ajax({
        type: "POST",
        url: "DepartDesig.aspx/filltblgrid",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("[id*=tblgrid]").show();
            $("[id*=tbldepfill]").empty();
            if (data.d.length > 0) {
                $("[id*=tbldepfill]").append("<thead><tr class='alert-info'><th style='display:none;'>DepartmentID</th><th><center>SR No.</center></th><th><center>Prefix</center></th><th><center>Department Name</center></th><th><center>Edit</center></th><th><center>Delete</center></th></tr></thead>");
                for (var i = 0; i < data.d.length; i++) {
                    if (i == 0) {
                        $("[id*=tbldepfill]").append("<tbody>");
                    }
                    $("[id*=tbldepfill]").append("<tr><td style='display:none;'>" + data.d[i].deptid + "</td><td><center>" + (i + 1) + "</center></td><td><center>" + data.d[i].prefix + "</center></td><td><center>" + data.d[i].deptname + "</center></td><td><center><a href='#' class='Select'><span class='fa fa-edit fa-fw'></span></a></center></td><td><center><a href='#' class='delete'><span class='fa fa-trash fa-fw'></span></a><center></td></tr>");
                    if (i == data.d.length - 1) {
                        $("[id*=tbldepfill]").append("</tbody>");
                    }
                }
            }
        }
    });
}

function filldesig() {
    $.ajax({
        type: "POST",
        url: "DepartDesig.aspx/fillgesigrid",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("[id*=desigtbl]").show();
            $("[id*=tbldesig]").empty();
            if (data.d.length > 0) {
                $("[id*=tbldesig]").append("<thead><tr class='alert-info'><th style='display:none;'>DesignationID</th><th><center>SR No.</center></th><th><center>Dsignation Name</center></th><th><center>Edit</center></th><th><center>Delete</center></th></tr></thead>");
                for (var i = 0; i < data.d.length; i++) {
                    if (i == 0) {
                        $("[id*=tbldesig]").append("<tbody>");
                    }
                    $("[id*=tbldesig]").append("<tr><td style='display:none;'>" + data.d[i].desid + "</td><td><center>" + (i + 1) + "</center></td><td><center>" + data.d[i].desname + "</center></td><td><center><a href='#' class='Select'><span class='fa fa-edit fa-fw'></span></a></center></td><td><center><a href='#' class='delete'><span class='fa fa-trash fa-fw'></span></a></center></td></tr>");
                    if (i == data.d.length - 1) {
                        $("[id*=tbldesig]").append("</tbody>");
                    }
                }
            }
        }
    });
}

$("[id*=btnSave]").on('click', function () {

    var deptname = $("[id*='txtdept']").val();
    var prefix = $("[id*='txtprefix']").val();
    var button = $("[id*=btnSave]")[0].innerText;
    if (deptname != "" && deptname != undefined) {
        if (prefix != "") {
            if (button == "Save") {
                $.ajax({
                    type: "POST",
                    url: "DepartDesig.aspx/saverecord",
                    data: '{deptname:"' + deptname + '",prefix:"' + prefix + '",type:"department",empid:"' + empId + '"}',
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    success: function (data) {
                        if (data.d[0].msg == "Saved") {
                            $.notify("Data Saved Successfully.", { color: "#fff", background: "#127515", blur: 0.2, delay: 0 });
                            $("[id*=txtdept]").val("");
                            $("[id*=txtprefix]").val("");
                            $("[id*=txtdeparttid]").val("");
                            filldepart();
                        }
                        else if (data.d[0].msg == "More") {
                            $.notify("More Than One Data Saved.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                        }
                        else if (data.d[0].msg == "Exist") {
                            $.notify("Entered Department Already Exist.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                        }
                        else {
                            $.notify("Data Not Saved.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                        }
                    }
                });
            }
            else {
                deptid = $("[id*='txtdeparttid']").val();
                if (deptid != "" && deptid!=undefined) {
                    $.ajax({
                        type: "POST",
                        url: "DepartDesig.aspx/updaterecord",
                        data: '{deptname:"' + deptname + '",prefix:"' + prefix + '",type:"department",empid:"' + empId + '",deptid:"' + deptid + '"}',
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        success: function (data) {
                            if (data.d[0].msg == "Update") {
                                $.notify("Data Updated Successfully.", { color: "#fff", background: "#127515", blur: 0.2, delay: 0 });
                                filldepart();
                                $("[id*=txtdept]").val("");
                                $("[id*=txtprefix]").val("");
                                $("[id*=txtdeparttid]").val("");
                                $("[id*=btnSave]")[0].innerText = "Save";
                            }
                            else if (data.d[0].msg == "More") {
                                $.notify("More Than One Data Saved.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                            }
                            else if (data.d[0].msg == "exist") {
                                $.notify("Cannot update .Department is assigned to employee.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                            }
                            else {
                                $.notify("Data Not Saved.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                            }
                        }
                    });
                }
            }
        }
        else {
            $.notify("Please Give Prefix Value.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
        }
    }
    else {
        $.notify("Please Give Department Name.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
    }
});



$("[id*=btndesave]").on('click', function () {

    var desname = $("[id*='txtdesig']").val();
    var prefix = "";
    var button = $("[id*=btndesave]")[0].innerText;
    if (desname != "" && desname != undefined) {
        if (button == "Save") {
            $.ajax({
                type: "POST",
                url: "DepartDesig.aspx/saverecord",
                data: '{deptname:"' + desname + '",prefix:"' + prefix + '",type:"design",empid:"' + empId + '"}',
                contentType: "application/json; charset=utf-8",
                async: false,
                success: function (data) {
                    if (data.d[0].msg == "Saved") {
                        $.notify("Data Saved Successfully.", { color: "#fff", background: "#127515", blur: 0.2, delay: 0 });
                        $("[id*=txtdesig]").val("");
                        $("[id*=txtdeid]").val("");
                        filldesig();
                    }
                    else if (data.d[0].msg == "More") {
                        $.notify("More Than One Data Saved.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                    }
                    else if (data.d[0].msg == "Exist") {
                        $.notify("Entered Department Already Exist.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                    }
                    else {
                        $.notify("Data Not Saved.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                    }
                }
            });
        }
        else {
            desiid = $("[id*='txtdeid']").val();
            if (desiid != "") {
                $.ajax({
                    type: "POST",
                    url: "DepartDesig.aspx/updaterecord",
                    data: '{deptname:"' + desname + '",prefix:"' + prefix + '",type:"design",empid:"' + empId + '",deptid:"' + desiid + '"}',
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    success: function (data) {
                        if (data.d[0].msg == "Update") {
                            $.notify("Data Updated Successfully.", { color: "#fff", background: "#127515", blur: 0.2, delay: 0 });
                            filldesig();
                            $("[id*=txtdesig]").val("");
                            $("[id*=txtdeid]").val("");
                            $("[id*=btndesave]")[0].innerText = "Save";
                        }
                        else if (data.d[0].msg == "More") {
                            $.notify("More Than One Data Saved.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                        }
                        else if (data.d[0].msg == "exist") {
                            $.notify("Cannot update .Designation is assigned to employee.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                        }
                        else {
                            $.notify("Data Not Saved.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                        }
                    }
                });
            }
        }
    }
    else {
        $.notify("Please Give Designation Name.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
    }
});


$("[id*=tbldepfill]").on('click', 'td a.delete', function () {

    var $td = $(this).closest('tr').children('td');
    var id = $td.eq(0).text();
    var prefix=$td.eq(2).text();
    var name=$td.eq(3).text();

    $.ajax({
        type: "POST",
        url: "DepartDesig.aspx/deletedept",
        data: '{id:"'+id+'",type:"department",name:"'+name+'",prefix:"'+prefix+'"}',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.d[0].msg == "deleted") {
                $.notify("Data Deleted Successfully.", { color: "#fff", background: "#127515", blur: 0.2, delay: 0 });
                filldepart();
            }
            else{
                $.notify("Cannot delete .Department is assigned to employee.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
            }
        }
    });
});

$("[id*=tbldesig]").on('click', 'td a.delete', function () {

    var $td = $(this).closest('tr').children('td');
    var id = $td.eq(0).text();
    //var prefix = $td.eq(2).text();
    var name = $td.eq(2).text();

    $.ajax({
        type: "POST",
        url: "DepartDesig.aspx/deletedept",
        data: '{id:"' + id + '",type:"designation",name:"' + name + '",prefix:""}',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.d[0].msg == "deleted") {
                $.notify("Data Deleted Successfully.", { color: "#fff", background: "#127515", blur: 0.2, delay: 0 });
                filldesig();
            }
            else{
                $.notify("Cannot delete .Designation is assigned to employee.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
            }
        }
    });
});

$("[id*=tbldesig]").on('click', 'td a.Select', function () {
    var $td = $(this).closest('tr').children('td');
    var id = $td.eq(0).text();
    var name = $td.eq(2).text();
    $("[id*=btndesave]")[0].innerText = "Update";
    $("[id*=txtdeid]").val("");
    $("[id*=txtdeid]").val(id);
    $("[id*=txtdesig]").val(name);
});

$("[id*=tbldepfill]").on('click', 'td a.Select', function () {
    var $td = $(this).closest('tr').children('td');
    var id = $td.eq(0).text();
    var prefix = $td.eq(2).text();
    var name = $td.eq(3).text();
    $("[id*=btnSave]")[0].innerText = "Update";
    $("[id*=txtdeparttid]").val("");
    $("[id*=txtdeparttid]").val(id);
    $("[id*=txtdept]").val(name);
    $("[id*=txtprefix]").val(prefix);
});

$("[id*=btnrefresh]").on('click', function () {
    $("[id*=txtdept]").val("");
    $("[id*=txtprefix]").val("");
    $("[id*=txtdeparttid]").val("");
    $("[id*=btnSave]")[0].innerText = "Save";
});

$("[id*=btndesref]").on('click', function () {
    $("[id*=txtdesig]").val("");
    $("[id*=txtdeid]").val("");
    $("[id*=btndesave]")[0].innerText = "Save";
});


