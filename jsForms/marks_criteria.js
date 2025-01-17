

$(document).ready(function () {
    loadmedium();
    var btncount = "";
    var examarray = [];
    $("[id*=ddlmedium]")[0].disabled = false;
    $("[id*=ddlprev]").hide();
})
var prevayid = "";
var chk = false;
var acdyear = "";

$("input[name = 'che']").on("click", function () {
    chk = true;
    $("[id*=calculator]").hide();
    $("[id*=ddlmedium]")[0].disabled = true;
    $("[id*=ddlmedium]")[0].selectedIndex = 0;
    $("[id*=ddlclass]")[0].selectedIndex = 0;
    $("[id*=ddlclass]")[0].disabled = true;
    $("[id*=ddlsub]")[0].selectedIndex = 0;
    $("[id*=ddlsub]")[0].disabled = true;
    $("[id*=ddlexam]")[0].selectedIndex = 0;
    $("[id*=ddlexam]")[0].disabled = true;
    $("[id*=ddlexmtype]")[0].selectedIndex = 0;
    $("[id*=ddlexmtype]")[0].disabled = true;
    $("[id*=tblgrid]").hide();
    $("[id*=tblfill]").empty();
    $("[id*=btnsave]").css("display", "none");
    if ($(this).prop("checked") == true) {
        loadprevyear();
        $("[id*=ddlprev]").show();
        $("[id*=ddlmedium]")[0].disabled = true;
    }
    else {
        chk = false;
        prevayid = "";
        $("[id*=ddlprev]").empty();
        $("[id*=ddlprev]").hide();
        $("[id*=ddlmedium]")[0].disabled = false;
    }

});

$("[id*=ddlprev]").change(function () {
    $("[id*=btnsave]").css("display", "none");
    $("[id*=calculator]").hide();
    $("[id*=ddlmedium]")[0].disabled = true;
    $("[id*=ddlmedium]")[0].selectedIndex = 0;
    $("[id*=ddlclass]")[0].selectedIndex = 0;
    $("[id*=ddlclass]")[0].disabled = true;
    $("[id*=ddlexam]")[0].selectedIndex = 0;
    $("[id*=ddlexam]")[0].disabled = true;
    $("[id*=ddlexmtype]")[0].selectedIndex = 0;
    $("[id*=ddlexmtype]")[0].disabled = true;
    $("[id*=ddlsub]")[0].selectedIndex = 0;
    $("[id*=ddlsub]")[0].disabled = true;

    $("[id*=tblgrid]").hide();
    $("[id*=tblfill]").empty();
    $("[id*=btnsave]").css("display", "none");
    prevayid = $("[id*=ddlprev]").val();
    if ($("[id*=ddlprev] :selected").text() == "--Select--") {
        $("[id*=ddlmedium]")[0].disabled = true;
        prevayid = "";
    }
    else {
        $("[id*=ddlmedium]")[0].disabled = false;
    }

});


function loadprevyear() {
    var ddlprev = $("[id*=ddlprev]");
    $.ajax({
        type: "POST",
        url: "marks_criteria.aspx/fillprevayid",
        contentType: "application/json; charset=utf-8",
        data: '{ayid:"' + ayid + '"}',
        success: function (r) {

            ddlprev.empty().append('<option selected="selected" value="0">--Select--</option>');
            var preayid = [];
            preayid = JSON.parse(r.d);
            $.each(preayid.Table, function () {
                ddlprev.append($("<option></option>").val(this['AYID']).html(this['Duration']));
            });
        },
        error: function (xhr, status, error) {
            $.notify(error, { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
        }
    });
}

function loadmedium() {
    $("[id*=btnsave]").css("display", "none");
    $("[id*=calculator]").hide();
    $("[id*=ddlmedium]")[0].disabled = true;
    $("[id*=ddlmedium]")[0].selectedIndex = 0;
    $("[id*=ddlclass]")[0].selectedIndex = 0;
    $("[id*=ddlclass]")[0].disabled = true;
    $("[id*=ddlexam]")[0].selectedIndex = 0;
    $("[id*=ddlexam]")[0].disabled = true;
    $("[id*=ddlexmtype]")[0].selectedIndex = 0;
    $("[id*=ddlexmtype]")[0].disabled = true;
    $("[id*=ddlsub]")[0].selectedIndex = 0;
    $("[id*=ddlsub]")[0].disabled = true;

    var ddlmedium = $("[id*=ddlmedium]");
    $("[id*=calculator]").hide();
    $.ajax({
        type: "POST",
        url: "marks_criteria.aspx/Fillmedium",
        contentType: "application/json; charset=utf-8",

        success: function (r) {

            ddlmedium.empty().append('<option selected="selected" value="0">--Select--</option>');
            var medium = [];
            medium = JSON.parse(r.d);
            $.each(medium.Table, function () {
                ddlmedium.append($("<option></option>").val(this['med_id']).html(this['medium']));
            });
        },
        error: function (xhr, status, error) {
            $.notify(error, { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
        }
    });

}

function loadclass() {
    $("[id*=calculator]").hide();
    $("[id*=ddlclass]")[0].selectedIndex = 0;
    $("[id*=ddlclass]")[0].disabled = true;
    $("[id*=ddlexam]")[0].selectedIndex = 0;
    $("[id*=ddlexam]")[0].disabled = true;
    $("[id*=ddlexmtype]")[0].selectedIndex = 0;
    $("[id*=ddlexmtype]")[0].disabled = true;
    $("[id*=ddlsub]")[0].selectedIndex = 0;
    $("[id*=ddlsub]")[0].disabled = true;

    $("[id*=tblgrid]").hide();
    $("[id*=tblfill]").empty();
    $("[id*=btnsave]").css("display", "none");
    if ($("[id*=ddlmedium] :selected").text() == "--Select--") {

    }
    else {
        var ddlclass = $("[id*=ddlclass]");

        $.ajax({
            type: "POST",
            url: "marks_criteria.aspx/Fillclass",
            contentType: "application/json; charset=utf-8",
            data: '{medid:"' + $("[id*=ddlmedium]").val() + '"}',
            success: function (r) {

                ddlclass.empty().append('<option selected="selected" value="0">--Select--</option>');
                var mclass = [];
                mclass = JSON.parse(r.d);
                $.each(mclass.Table, function () {
                    ddlclass.append($("<option></option>").val(this['std_id']).html(this['std_name']));
                });
                $("[id*=ddlclass]")[0].disabled = false;
            },
            error: function (xhr, status, error) {
                $.notify(error, { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
            }
        });
    }
}
$("[id*='btnrefresh']").on("click", function () {
    location.reload();
});



function loadexam(acdyear) {
    $("[id*=ddlexam]")[0].selectedIndex = 0;
    $("[id*=ddlexam]")[0].disabled = true;
    $("[id*=ddlexmtype]")[0].selectedIndex = 0;
    $("[id*=ddlexmtype]")[0].disabled = true;
    $("[id*=ddlsub]")[0].selectedIndex = 0;
    $("[id*=ddlsub]")[0].disabled = true;
    $("[id*=btnsave]").css("display", "none");
    $("[id*=calculator]").hide();
    $("[id*=tblgrid]").hide();
    $("[id*=tblfill]").empty();
    if ($("[id*=ddlclass] :selected").text() == "--Select--") {

    }
    else {
        var ddlexam = $("[id*=ddlexam]");

        $.ajax({
            type: "POST",
            url: "marks_entry.aspx/Fillexam",
            contentType: "application/json; charset=utf-8",
            data: '{medid:"' + $("[id*=ddlmedium]").val() + '",classid:"' + $("[id*=ddlclass]").val() + '",ayid:"' + acdyear + '"}',
            success: function (r) {

                ddlexam.empty().append('<option selected="selected" value="0">--Select--</option>');
                var exam = [];
                exam = JSON.parse(r.d);
                examarray = [];
                examarray = JSON.parse(r.d);
                $.each(exam.Table, function () {
                    ddlexam.append($("<option></option>").val(this['exam_id']).html(this['exam_name']));
                });
                $("[id*=ddlexam]")[0].disabled = false;
            },
            error: function (xhr, status, error) {
                $.notify(error, { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
            }
        });
    }
}

function loadexmtype(acdyear) {
    $("[id*=ddlexmtype]")[0].selectedIndex = 0;
    $("[id*=ddlexmtype]")[0].disabled = true;
    $("[id*=ddlsub]")[0].selectedIndex = 0;
    $("[id*=ddlsub]")[0].disabled = true;
    $("[id*=btnsave]").css("display", "none");
    $("[id*=calculator]").hide();
    $("[id*=tblgrid]").hide();
    $("[id*=tblfill]").empty();
    if ($("[id*=ddlexam] :selected").text() == "--Select--") {

    }
    else {
        var ddlexamtype = $("[id*=ddlexmtype]");
        $.ajax({
            type: "POST",
            url: "marks_criteria.aspx/Fillexamtype",
            contentType: "application/json; charset=utf-8",
            data: '{medid:"' + $("[id*=ddlmedium]").val() + '",classid:"' + $("[id*=ddlclass]").val() + '",ayid:"' + acdyear + '",examid:"' + $("[id*=ddlexam]").val() + '"}',
            success: function (r) {

                ddlexamtype.empty().append('<option selected="selected" value="0">--Select--</option>');
                var examtype = [];
                examtype = JSON.parse(r.d);
                $.each(examtype.Table, function () {
                    ddlexamtype.append($("<option></option>").val(this['exam_type']).html(this['exam_type']));
                });
                $("[id*=ddlexmtype]")[0].disabled = false;
            },
            error: function (xhr, status, error) {
                $.notify(error, { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
            }
        });
    }
}

function loadsub(acdyear) {
    $("[id*=ddlsub]")[0].selectedIndex = 0;
    $("[id*=ddlsub]")[0].disabled = true;
    $("[id*=tblgrid]").hide();
    $("[id*=tblfill]").empty();
    $("[id*=btnsave]").css("display", "none");
    $("[id*=calculator]").hide();
    $("[id*=btnsave]").css("display", "none");
    $("[id*=calculator]").hide();
    if ($("[id*=ddlexmtype] :selected").text() == "--Select--") {

    }
    else {
        var ddlsub = $("[id*=ddlsub]");
        var chkexam = [];
        if (prevayid != "") {
            $.ajax({
                type: "POST",
                url: "marks_criteria.aspx/checkexam",
                contentType: "application/json; charset=utf-8",
                data: '{medid:"' + $("[id*=ddlmedium]").val() + '",classid:"' + $("[id*=ddlclass]").val() + '",ayid:"' + ayid + '",examid:"' + $("[id*=ddlexam]").val() + '",examtype:"' + $("[id*=ddlexmtype]").val() + '",prevayid:"' + acdyear + '"}',
                success: function (r) {

                    chkexam = JSON.parse(r.d);
                    if (chkexam.Table.length > 0 && prevayid != "") {
                        $.ajax({
                            type: "POST",
                            url: "marks_criteria.aspx/Fillsub",
                            contentType: "application/json; charset=utf-8",
                            data: '{medid:"' + $("[id*=ddlmedium]").val() + '",classid:"' + $("[id*=ddlclass]").val() + '",ayid:"' + acdyear + '",examtype:"' + $("[id*=ddlexmtype]").val() + '",examid:"' + $("[id*=ddlexam]").val() + '"}',
                            success: function (r) {

                                ddlsub.empty().append('<option selected="selected" value="0">--Select--</option>');
                                var sub = [];
                                sub = JSON.parse(r.d);
                                $.each(sub.Table, function () {
                                    ddlsub.append($("<option></option>").val(this['subject_id']).html(this['subject_name']));
                                });
                                $("[id*=ddlsub]")[0].disabled = false;
                            },
                            error: function (xhr, status, error) {
                                $.notify(error, { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                            }
                        });
                    }
                    else {
                        $.notify('This exam is not  defined in current year ', { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                    }
                },
                error: function (xhr, status, error) {
                    $.notify(error, { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                }
            });
        }
        else {
            $.ajax({
                type: "POST",
                url: "marks_criteria.aspx/Fillsub",
                contentType: "application/json; charset=utf-8",
                data: '{medid:"' + $("[id*=ddlmedium]").val() + '",classid:"' + $("[id*=ddlclass]").val() + '",ayid:"' + acdyear + '",examtype:"' + $("[id*=ddlexmtype]").val() + '",examid:"' + $("[id*=ddlexam]").val() + '"}',
                success: function (r) {

                    ddlsub.empty().append('<option selected="selected" value="0">--Select--</option>');
                    var sub = [];
                    sub = JSON.parse(r.d);
                    $.each(sub.Table, function () {
                        ddlsub.append($("<option></option>").val(this['subject_id']).html(this['subject_name']));
                    });
                    $("[id*=ddlsub]")[0].disabled = false;
                },
                error: function (xhr, status, error) {
                    $.notify(error, { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                }
            });
        }
    }
}
//function splitMulti(str, tokens) {
//    var tempChar = tokens[0]; // We can use the first token as a temporary join character
//    for (var i = 1; i < tokens.length; i++) {
//        str = str.split(tokens[i]).join(tempChar);
//    }
//    str = str.split(tempChar);
//    return str;
//}

function loaddata(acdyear) {
    $("[id*=btnsave]").css("display", "none");
    $("[id*=tblgrid]").hide();
    $("[id*=tblfill]").empty();
    $("[id*=calculator]").hide();
    if ($("[id*=ddlsub] :selected").text() == "--Select--") {

    }
    else {
        var checksub = [];
        if (prevayid != "") {
            $.ajax({
                type: "POST",
                url: "marks_criteria.aspx/checksub",
                contentType: "application/json; charset=utf-8",
                data: '{medid:"' + $("[id*=ddlmedium]").val() + '",classid:"' + $("[id*=ddlclass]").val() + '",ayid:"' + ayid + '",examid:"' + $("[id*=ddlexam]").val() + '",examtype:"' + $("[id*=ddlexmtype]").val() + '",prevayid:"' + acdyear + '",subjectid:"' + $("[id*=ddlsub]").val() + '"}',
                success: function (r) {

                    chksub = JSON.parse(r.d);
                    if (chksub.Table.length > 0 && prevayid != "") {
                        if (chksub.Table[0]["formula"] == "" || chksub.Table[0]["formula"] == null) {
                            $.ajax({

                                type: "POST",
                                url: "marks_criteria.aspx/loaddata",
                                contentType: "application/json; charset=utf-8",
                                data: '{medid:"' + $("[id*=ddlmedium]").val() + '",classid:"' + $("[id*=ddlclass]").val() + '",ayid:"' + acdyear + '",subjectid:"' + $("[id*=ddlsub]").val() + '",examtype:"' + $("[id*=ddlexmtype]").val() + '",examid:"' + $("[id*=ddlexam]").val() + '"}',
                                async: false,
                                success: function (r) {
                                    var data = [];
                                    data = JSON.parse(r.d);

                                    if (data.Table1.length > 0) {
                                        if (data.Table.length > 0) {
                                            var newformula = data.Table1[0]["ref_formula"];
                                            var refformula = data.Table1[0]["ref_formula"];
                                            // var arrfor = [];
                                            var checkref = "";
                                            var refcount = 0;
                                            var append = "";
                                            $("[id*=tblgrid]").show();
                                            $("[id*=tblfill]").empty();

                                            for (var j = 0; j < data.Table.length; j++) {
                                                if (newformula.includes(data.Table[j]["ref_id"]) == true) {
                                                    newformula = newformula.replace(data.Table[j]["ref_id"], data.Table[j]["exam_name"]);
                                                    // append = append + " disabled = 'disabled'";
                                                    if (checkref == "") {
                                                        checkref = checkref + data.Table[j]["ref_id"];
                                                    }
                                                    else {
                                                        checkref = checkref + "','" + data.Table[j]["ref_id"];
                                                    }
                                                    refcount++;
                                                }
                                            }
                                            if (checkref != "") {
                                                $.ajax({
                                                    type: "POST",
                                                    url: "marks_criteria.aspx/checkref",
                                                    contentType: "application/json; charset=utf-8",
                                                    data: '{ayid:"' + ayid + '",subjectid:"' + $("[id*=ddlsub]").val() + '",medid:"' + $("[id*=ddlmedium]").val() + '",classid:"' + $("[id*=ddlclass]").val() + '",refid:"' + checkref + '"}',
                                                    success: function (r) {
                                                        var datanew = [];
                                                        datanew = JSON.parse(r.d);
                                                        if (datanew.Table1.length == refcount && datanew.Table.length>0) {
                                                            for (var i = 0; i < datanew.Table.length; i++) {
                                                                var count = i + 1;

                                                                if (i == 0) {
                                                                    append = append + "<tr>";
                                                                }

                                                                append = append + "<td class='col-md-2'> <input type='button' style='display: none;'  id='btn_" + i + "' testid=" + datanew.Table[i]["exam_id"] + " refid=" + datanew.Table[i]["ref_id"] + " runat='server' value='" + datanew.Table[i]["exam_name"] + "' onclick='addtest(this,btn_" + i + ")'";

                                                                //   arrfor[i] = data.Table[i]["ref_id"]);
                                                                if (newformula.includes(datanew.Table[i]["ref_id"]) == true) {
                                                                    newformula = newformula.replace(datanew.Table[i]["ref_id"], datanew.Table[i]["exam_name"]);
                                                                    append = append + " disabled = 'disabled'";
                                                                    //if (checkref == "") {
                                                                    //    checkref = checkref + data.Table[i]["ref_id"];
                                                                    //}
                                                                    //else {
                                                                    //    checkref = checkref + "," + data.Table[i]["ref_id"];
                                                                    //}
                                                                    //refcount++;
                                                                }
                                                                if (refformula.includes(datanew.Table[i]["ref_id"]) == true) {
                                                                    refformula = refformula.replace(datanew.Table[i]["ref_id"], datanew.Table[i]["exam_id"]);

                                                                }

                                                                append = append + "/></td>";
                                                                if (count % 3 == 0 && count > 1) {
                                                                    append = append + " </tr><tr style='margin-top:2px'>";
                                                                }

                                                            }


                                                            append = append + "<br /></tr><tr><td > Formula</td></tr><br /><td colspan='4'><input class='form-control' disabled='disabled' type='text' id='txtformula' style='width:100%' ReadOnly='true'";
                                                            if (data.Table1.length > 0) {
                                                                append = append + " value = '" + newformula + "'";
                                                            }
                                                            append = append + "runat = 'server' /></td ></tr ><tr><td><input type='text' ";
                                                            if (data.Table1.length > 0) {
                                                                append = append + "value = '" + refformula + "' ";
                                                            }
                                                            append = append + "id = 'txtidformula' style='display: none;width:100%' ReadOnly = 'true' class='form-control' disabled='disabled' runat = 'server' /></td ></tr > <tr><td><input type='text'  ";
                                                            if (data.Table1.length > 0) {
                                                                append = append + "value = '" + data.Table1[0]["ref_formula"] + "'";
                                                            }
                                                            append = append + "id = 'txtrefformula' style='display: none;width:100%' class='form-control' disabled='disabled' runat = 'server' ReadOnly = 'true' /></td ></tr > ";
                                                            $("[id*='tblfill']").append(append);
                                                            var input = document.getElementById('txtformula');
                                                            input.onkeydown = function () {
                                                                var key = event.keyCode || event.charCode;
                                                                if (key == 8) {
                                                                    $("[id*=txtformula]").val("");
                                                                    $("[id*=txtidformula]").val("");
                                                                    $("[id*=txtrefformula]").val("");
                                                                    for (var j = 0; j < data.Table.length; j++) {
                                                                        $("[id*=btn_" + j + "]").attr("disabled", false);
                                                                    }
                                                                }
                                                            };
                                                            btncount = data.Table.length;
                                                            var btninput = document.getElementById('btnclear');
                                                            btninput.onkeypress = function () {
                                                                var key = event.keyCode || event.charCode;
                                                                if (key == 8) {
                                                                    $("[id*=txtformula]").val("");
                                                                    $("[id*=txtidformula]").val("");
                                                                    $("[id*=txtrefformula]").val("");

                                                                }
                                                            };
                                                            $("[id*=btnsave]").css("display", "block");
                                                        }
                                                        else {
                                                            $.notify('All exams from previous year are not forwarded to this year', { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                                                            return;
                                                        }
                                                    }
                                                });
                                               
                                            }
                                            else {
                                                $.notify('Formula not defined for this subject in previous academic year', { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                                                return;
                                            }
                                        }                                       
                                       
                                    }
                                    else {
                                        $.notify('Formula not defined for this subject in previous academic year', { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                                        return;
                                    }
                                },
                                error: function (error) {

                                }
                            });
                        }
                        else {
                            $.notify('Formula already defined for this subject', { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                        }
                    }
                    else {
                        $.notify('This subject is not defined in current academic year', { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });

                    }
                }
            });
        }
        else {
            $.ajax({

                type: "POST",
                url: "marks_criteria.aspx/loaddata",
                contentType: "application/json; charset=utf-8",
                data: '{medid:"' + $("[id*=ddlmedium]").val() + '",classid:"' + $("[id*=ddlclass]").val() + '",ayid:"' + acdyear + '",subjectid:"' + $("[id*=ddlsub]").val() + '",examtype:"' + $("[id*=ddlexmtype]").val() + '",examid:"' + $("[id*=ddlexam]").val() + '"}',
                async: false,
                success: function (r) {
                    var data = [];
                    data = JSON.parse(r.d);


                    if (data.Table.length > 0) {                        
                        if (data.Table1.length > 0) {
                            var newformula = data.Table1[0]["ref_formula"];
                        }


                        var append = "";
                        $("[id*=tblgrid]").show();
                        $("[id*=calculator]").show();
                        $("[id*=tblfill]").empty();
                        for (var i = 0; i < data.Table.length ; i++) {
                            var count = i + 1;
                            if (i == 0) {
                                append = append + "<tr>";
                            }

                            append = append + "<td class='col-md-3'> <input type='button' class='btn btn-info' style='width:100%' id='btn_" + i + "' testid=" + data.Table[i]["exam_id"] + " refid=" + data.Table[i]["ref_id"] + " runat='server' value='" + data.Table[i]["exam_name"] + "' onclick='addtest(this,btn_" + i + ")'";
                            if (data.Table1.length > 0) {
                                if (newformula.includes(data.Table[i]["ref_id"]) == true) {
                                    newformula = newformula.replace(data.Table[i]["ref_id"], data.Table[i]["exam_name"]);
                                    append = append + " disabled = 'disabled'";
                                }
                            }
                            append = append + "/></td>";
                            if (count % 3 == 0 && count > 1) {
                                append = append + " </tr><tr style='margin-top:2px'>";
                            }
                        }


                        append = append + "<br /></tr><tr><td > Formula</td></tr><br /><td colspan='4'><input class='form-control' disabled='disabled' type='text' id='txtformula' style='width:100%' ReadOnly='true'";
                        if (data.Table1.length > 0) {
                            append = append + " value = '" + newformula + "'";
                        }
                        append = append + "runat = 'server' /></td ></tr ><tr><td><input type='text' ";
                        if (data.Table1.length > 0) {
                            append = append + "value = '" + data.Table1[0]["formula"] + "' ";
                        }
                        append = append + "id = 'txtidformula' style='display: none;width:100%' ReadOnly = 'true' class='form-control' disabled='disabled' runat = 'server' /></td ></tr > <tr><td><input type='text'  ";
                        if (data.Table1.length > 0) {
                            append = append + "value = '" + data.Table1[0]["ref_formula"] + "'";
                         
                        }
                        append = append + "id = 'txtrefformula' style='display: none;width:100%' class='form-control' disabled='disabled' runat = 'server' ReadOnly = 'true' /></td ></tr > ";
                    }
                    $("[id*='tblfill']").append(append);


                    var input = document.getElementById('txtformula');
                    input.onkeydown = function () {
                        var key = event.keyCode || event.charCode;
                        if (key == 8) {
                            $("[id*=txtformula]").val("");
                            $("[id*=txtidformula]").val("");
                            $("[id*=txtrefformula]").val("");
                            for (var j = 0; j < data.Table.length; j++) {
                                $("[id*=btn_" + j + "]").attr("disabled", false);
                            }
                        }
                    };
                    btncount = data.Table.length;
                    var btninput = document.getElementById('btnclear');
                    btninput.onkeypress = function () {
                        var key = event.keyCode || event.charCode;
                        if (key == 8) {
                            $("[id*=txtformula]").val("");
                            $("[id*=txtidformula]").val("");
                            $("[id*=txtrefformula]").val("");

                        }
                    };
                    $("[id*=btnsave]").css("display", "block");
                },
                error: function (error) {

                }
            });
        }
    }
}

document.addEventListener('dragstart', function (event) {
    event.dataTransfer.setData('Text', event.target.innerHTML);
});


function addtest(e, id) {
    var text = "";
    var id = e.id;
    var check = "";
    var value = $("[id*=txtformula]").val();
    var idval = $("[id*=txtidformula]").val();
    var refval = $("[id*=txtrefformula]").val();
    if ($("[id*=txtformula]").val().includes("=") == true ) {
        return;
    }
    if ($("[id*=txtformula]").val() != "") {
        check = value.substring(value.length - 1, value.length);
        if (check == "+" || check == "-" || check == "*" || check == "/" || check == "=" || check == "(" ) {
            value = value + " " + e.value;
            idval = idval + " " + e.attributes.testid.value + "";
            refval = refval + " " + e.attributes.refid.value + "";
        }
        else {
            value = value + " + " + e.value;
            idval = idval + " + " + e.attributes.testid.value + "";
            refval = refval + " + " + e.attributes.refid.value + "";
        }
    }
    else {
        value = $("[id*=txtformula]").val() + " " + e.value;
        idval = $("[id*=txtidformula]").val() + " " + e.attributes.testid.value + "";
        refval = $("[id*=txtrefformula]").val() + " " + e.attributes.refid.value + "";
    }

    $("[id*=" + id + "]").attr("disabled", true);
    $("[id*=txtformula]").val(value);
    $("[id*=txtidformula]").val(idval);
    $("[id*=txtrefformula]").val(refval);
}


function addbtn(e, id) {
    var text = "";
    var id = e.id;
    var check = "";
    var value = $("[id*=txtformula]").val();
    var idval = $("[id*=txtidformula]").val();
    var refval = $("[id*=txtrefformula]").val();
   
    if ($("[id*=txtformula]").val() != "" || e.value == "(") {
        check = value.substring(value.length - 1, value.length);
        if ($("[id*=txtformula]").val() != "") {
            if (e.value == "(" && (check != "+" && check != "-" && check != "*" && check != "/" && check != "%" && check != "=")) {
                return;
            }
        }
        if (e.value == ")" && (check == "+" || check == "-" || check == "*" || check == "/" || check == "%" || check == "(" || check == "=")) {
            return;
        }
        if (e.value == "+" && (check == "+" || check == "-" || check == "*" || check == "/" || check == "%" || check == "(" || check == "=")) {
            return;
        }
        if (e.value == "-" && (check == "+" || check == "-" || check == "*" || check == "/" || check == "%" || check == "(" || check == "=")) {
            return;
        }
        if (e.value == "*" && (check == "+" || check == "-" || check == "*" || check == "/" || check == "%" || check == "(" || check == "=")) {
            return;
        }
        if (e.value == "/" && (check == "+" || check == "-" || check == "*" || check == "/" || check == "%" || check == "(" || check == "=")) {
            return;
        }
        if (e.value == "%" && (check == "+" || check == "-" || check == "*" || check == "/" || check == "%" || check == "(" || check == "=")) {
            return;
        }
        
        var openbracket = 0;
        var closebracket = 0;
        var formulabracket = $("[id*=txtformula]").val();
        for (let i = 0; i < formulabracket.length; i++) {

            // check if the character is at that position
            if (formulabracket.charAt(i) == "(") {
                openbracket++;
            }
        }
        for (let i = 0; i < formulabracket.length; i++) {

            // check if the character is at that position
            if (formulabracket.charAt(i) == ")") {
                closebracket++;
            }
        }
        if (e.value==")" && (parseInt(openbracket) == parseInt(closebracket))) {
            return;
        }
        if (e.value == "=" && (check == "+" || check == "-" || check == "*" || check == "/" || check == "%" || check == "(" || check == "=") && (parseInt(openbracket) != parseInt(closebracket))) {
            return;
        }

        for (var i = 0; i < examarray.Table.length; i++) {
            if ($("[id*=txtformula]").val().endsWith(examarray.Table[i]["exam_name"]) == true && (e.value == "1" || e.value == "2" || e.value == "3" || e.value == "4" || e.value == "5" || e.value == "6" || e.value == "7" || e.value == "8" || e.value == "9" || e.value == "0")) {
                return;
            }

        }

        if (check == "+" || check == "-" || check == "*" || check == "/" || check == "=" || check == "(" || check == "%") {
            if ($("[id*=txtformula]").val().includes("=") == true) {
                if (e.value == "+" || e.value == "-" || e.value == "*" || e.value == "/" || e.value == "=" || e.value == ")" || e.value == "(" || e.value == "%") {
                    return;
                }
                else {
                    value = value + " " + e.value;
                    idval = idval + " " + e.value;
                    refval = refval + " " + e.value;
                }
            }
            else {
                value = value + " " + e.value;
                idval = idval + " " + e.value;
                refval = refval + " " + e.value;
            }
        }
        else {
            if (e.value == "+" || e.value == "-" || e.value == "*" || e.value == "/" || e.value == "=" || e.value == "(" || e.value == ")" || e.value == "%" || e.value == "=") {
                if ($("[id*=txtformula]").val().includes("=") == true) {
                    if (e.value == "+" || e.value == "-" || e.value == "*" || e.value == "/" || e.value == "=" || e.value == ")" || e.value == "(" || e.value == "%") {
                        return;
                    }
                    else {
                        value = value + " " + e.value;
                        idval = idval + " " + e.value;
                        refval = refval + " " + e.value;
                    }
                }
                else {
                    value = value + " " + e.value;
                    idval = idval + " " + e.value;
                    refval = refval + " " + e.value;
                }
            }

            else {
                value = value + e.value;
                idval = idval + e.value;
                refval = refval + e.value;
            }
        }        
        $("[id*=txtformula]").val(value);
        $("[id*=txtidformula]").val(idval);
        $("[id*=txtrefformula]").val(refval);
    }
    else {
        $.notify("First add exam!", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
    }
}



$("[id*=ddlmedium]").change(function () {
    loadclass();
});

$("[id*=ddlclass]").change(function () {
    if (prevayid != "") {
        loadexam(prevayid);
    }
    else {
        loadexam(ayid);
    }
});

$("[id*=ddlexam]").change(function () {
    if (prevayid != "") {
        loadexmtype(prevayid);
    }
    else {
        loadexmtype(ayid);
    }

});

$("[id*=ddlexmtype]").change(function () {
    if (prevayid != "") {
        loadsub(prevayid);
    }
    else {
        loadsub(ayid);
    }
});

$("[id*=ddlsub]").change(function () {
    if (prevayid != "") {
        loaddata(prevayid);
    }
    else {
        loaddata(ayid);
    }
});

$("[id*='btnclear']").on("click", function () {
    $("[id*=txtformula]").val("");
    $("[id*=txtidformula]").val("");
    $("[id*=txtrefformula]").val("");
    for (var j = 0; j < btncount; j++) {
        $("[id*=btn_" + j + "]").attr("disabled", false);
    }
    $("[id*=btnplus]").attr("disabled", false);
    $("[id*=btnminus]").attr("disabled", false);
    $("[id*=btndivide]").attr("disabled", false);
    $("[id*=btnmultiply]").attr("disabled", false);
    $("[id*=btnequal]").attr("disabled", false);
    $("[id*=btnropen]").attr("disabled", false);
    $("[id*=btnrclose]").attr("disabled", false);
    $("[id*=btnmod]").attr("disabled", false);
});

$("[id*='btnplus']").on("click", function () {
    addbtn(this, btnplus)

});

$("[id*='btnminus']").on("click", function () {
    addbtn(this, btnminus)

});

$("[id*='btndivide']").on("click", function () {
    addbtn(this, btndivide)

});

$("[id*='btnmultiply']").on("click", function () {
    addbtn(this, btnmultiply)

});

$("[id*='btnequal']").on("click", function () {
    addbtn(this, btnequal)

});

$("[id*='btnropen']").on("click", function () {
    addbtn(this, btnropen)    
});

$("[id*='btnrclose']").on("click", function () {
    addbtn(this, btnrclose)    
});

$("[id*='btnmod']").on("click", function () {
    addbtn(this, btnmod)

});

$("[id*='btnone']").on("click", function () {
    addbtn(this, btnone)

});
$("[id*='btntwo']").on("click", function () {
    addbtn(this, btntwo)

});
$("[id*='btnthree']").on("click", function () {
    addbtn(this, btnthree)

});
$("[id*='btnfour']").on("click", function () {
    addbtn(this, btnfour)

});
$("[id*='btnfive']").on("click", function () {
    addbtn(this, btnfive)

});
$("[id*='btnsix']").on("click", function () {
    addbtn(this, btnsix)

});
$("[id*='btnseven']").on("click", function () {
    addbtn(this, btnseven)

});
$("[id*='btneight']").on("click", function () {
    addbtn(this, btneight)

});
$("[id*='btnnine']").on("click", function () {
    addbtn(this, btnnine)

});
$("[id*='btnzero']").on("click", function () {
    addbtn(this, btnzero)

});

$("[id*='btnsave']").on("click", function () {    
    var obj = {};
 //   var chk = substring($("[id*=txtidformula]").val().indexOf("="), $("[id*=txtidformula]").val().length);
    if ($("[id*=txtidformula]").val().includes("=") == false || $("[id*=txtidformula]").val().substring($("[id*=txtidformula]").val().indexOf("=") + 1, $("[id*=txtidformula]").val().length) == "") {
        $.notify('Define valid formula', { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
        return;
    }
    var openbracket = 0;
    var closebracket = 0;
    var formulacheck = $("[id*=txtidformula]").val();
    for (let i = 0; i < formulacheck.length; i++) {

        // check if the character is at that position
        if (formulacheck.charAt(i) == "(") {
            openbracket++;
        }
    }
    for (let i = 0; i < formulacheck.length; i++) {

        // check if the character is at that position
        if (formulacheck.charAt(i) == ")") {
            closebracket++;
        }
    }
    if (parseInt(openbracket) != parseInt(closebracket)) {
        $.notify('Define proper formula', { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
        return;
    }
    
    
    if (prevayid == "") {
        obj.sptype = 'insert';
    }
    else {
        obj.sptype = 'update';
    }
    obj.ayid = ayid;
    obj.sub_id = $("[id*=ddlsub]").val();
    obj.exam_id = $("[id*=ddlexam]").val();
    obj.exam_type = $("[id*=ddlexmtype]").val();
    obj.class_id = $("[id*=ddlclass]").val();
    obj.med_id = $("[id*=ddlmedium]").val();
    obj.type = "insert";
    obj.formula = $("[id*=txtidformula]").val();
    obj.refformula = $("[id*=txtrefformula]").val();
    obj.prevayid = prevayid;

    $.ajax({
        type: "POST",
        url: "marks_criteria.aspx/savedata",
        contentType: "application/json; charset=utf-8",
        data: '{obj:' + JSON.stringify(obj) + '}',
        success: function (r) {
            if (r.d == "true") {
                $.notify("Data Saved Successfully", { color: "#fff", background: "#127515", blur: 0.2, delay: 0 });
                $("[id*=btnsave]").css("display", "none");
                $("[id*=ddlsub]")[0].selectedIndex = 0;
                $("[id*=tblgrid]").hide();
                $("[id*=tblfill]").empty();
                $("[id*=calculator]").hide();
            }

        },
        error: function (xhr, status, error) {
            $.notify(error, { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
        }
    });


});