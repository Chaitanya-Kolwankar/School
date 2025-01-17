$(document).ready(function () {   
    loadmedium();

    $("[id*=exceldata]").empty();
})

function loadmedium() {
    var ddlmedium = $("[id*=ddlmedium]");

    $.ajax({
        type: "POST",
        url: "marks_entry.aspx/Fillmedium",
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
    $("[id*=ddlclass]").empty();
    $("[id*=ddlsub]").empty();
    $("[id*=ddldiv]").empty();
    $("[id*=ddlexam]").empty();
    $("[id*=ddlclass]")[0].selectedIndex = 0;
    $("[id*=ddlclass]")[0].disabled = true;
    $("[id*=ddlsub]")[0].selectedIndex = 0;
    $("[id*=ddlsub]")[0].disabled = true;
    $("[id*=ddldiv]")[0].selectedIndex = 0;
    $("[id*=ddldiv]")[0].disabled = true;
    $("[id*=ddlgrp]")[0].selectedIndex = 0;
    $("[id*=ddlgrp]")[0].disabled = true;
    $("[id*=ddlexam]")[0].selectedIndex = 0;
    $("[id*=ddlexam]")[0].disabled = true;
    $("[id*=tblgrid]").hide();
    $("[id*=tblfill]").empty();
    $("[id*=btnsave]").css("display", "none");
    $("[id*=btnexcel]").css("display", "none");
    $("[id*=btnimport]").css("display", "none");
    $("[id*=excfile]").css("display", "none");
    document.getElementById('excfile').value = "";
    if ($("[id*=ddlmedium] :selected").text() == "--Select--") {

    }
    else {
        var ddlclass = $("[id*=ddlclass]");

        $.ajax({
            type: "POST",
            url: "marks_entry.aspx/Fillclass",
            contentType: "application/json; charset=utf-8",
            data: '{medid:"'+$("[id*=ddlmedium]").val()+'"}',
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

function loaddiv() {
    $("[id*=ddlsub]").empty();
    $("[id*=ddldiv]").empty();
    $("[id*=ddlexam]").empty();
    $("[id*=ddlsub]")[0].selectedIndex = 0;
    $("[id*=ddlsub]")[0].disabled = true;
    $("[id*=ddldiv]")[0].selectedIndex = 0;
    $("[id*=ddldiv]")[0].disabled = true;
    $("[id*=ddlgrp]")[0].selectedIndex = 0;
    $("[id*=ddlgrp]")[0].disabled = true;
    $("[id*=ddlexam]")[0].selectedIndex = 0;
    $("[id*=ddlexam]")[0].disabled = true;
    $("[id*=tblgrid]").hide();
    $("[id*=tblfill]").empty();
    $("[id*=btnsave]").css("display", "none");
    $("[id*=btnexcel]").css("display", "none");
    $("[id*=btnimport]").css("display", "none");
    $("[id*=excfile]").css("display", "none");
    document.getElementById('excfile').value = "";
    if ($("[id*=ddlclass] :selected").text() == "--Select--") {

    }
    else {
        var ddldiv = $("[id*=ddldiv]");

        $.ajax({
            type: "POST",
            url: "marks_entry.aspx/Filldiv",
            contentType: "application/json; charset=utf-8",
            data: '{medid:"' + $("[id*=ddlmedium]").val() + '",classid:"' + $("[id*=ddlclass]").val() + '",ayid:"' + ayid + '"}',
            success: function (r) {

                ddldiv.empty().append('<option selected="selected" value="0">--Select--</option>');
                var div = [];
                div = JSON.parse(r.d);
                $.each(div.Table, function () {
                    ddldiv.append($("<option></option>").val(this['division_id']).html(this['division_name']));
                });
                $("[id*=ddldiv]")[0].disabled = false;
            },
            error: function (xhr, status, error) {
                $.notify(error, { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
            }
        });
    }
}

function loadexam() {
    $("[id*=ddlexam]").empty();
    $("[id*=ddlsub]").empty();
    $("[id*=ddlexam]")[0].selectedIndex = 0;
    $("[id*=ddlexam]")[0].disabled = true;
    $("[id*=ddlsub]")[0].selectedIndex = 0;
    $("[id*=ddlsub]")[0].disabled = true;
    $("[id*=btnsave]").css("display", "none");
    $("[id*=btnexcel]").css("display", "none");
    $("[id*=btnimport]").css("display", "none");
    $("[id*=excfile]").css("display", "none");
    document.getElementById('excfile').value = "";

    $("[id*=tblgrid]").hide();
    $("[id*=tblfill]").empty();
    if ($("[id*=ddldiv] :selected").text() == "--Select--") {
        $("[id*=ddlexam]")[0].selectedIndex = 0;
        $("[id*=ddlexam]")[0].disabled = true;
    }
    else {
        if ($("[id*=ddlgrp]")[0].disabled == false && $("[id*=ddlgrp] :selected").text() == "--Select--") {
            $("[id*=ddlexam]")[0].selectedIndex = 0;
            $("[id*=ddlexam]")[0].disabled = true;
        }
        else {
            var ddlexam = $("[id*=ddlexam]");

            $.ajax({
                type: "POST",
                url: "marks_entry.aspx/Fillexam",
                contentType: "application/json; charset=utf-8",
                data: '{medid:"' + $("[id*=ddlmedium]").val() + '",classid:"' + $("[id*=ddlclass]").val() + '",ayid:"' + ayid + '"}',
                success: function (r) {

                    ddlexam.empty().append('<option selected="selected" value="0">--Select--</option>');
                    var exam = [];
                    exam = JSON.parse(r.d);
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
}

function loadgrp() {
    $("[id*=ddlexam]").empty();
    $("[id*=ddlsub]").empty();
    $("[id*=ddlexam]")[0].selectedIndex = 0;
    $("[id*=ddlexam]")[0].disabled = true;
    $("[id*=ddlsub]")[0].selectedIndex = 0;
    $("[id*=ddlsub]")[0].disabled = true;
    $("[id*=btnsave]").css("display", "none");
    $("[id*=btnexcel]").css("display", "none");
    $("[id*=btnimport]").css("display", "none");
    $("[id*=excfile]").css("display", "none");
    document.getElementById('excfile').value = "";

    $("[id*=tblgrid]").hide();
    $("[id*=tblfill]").empty();
    if ($("[id*=ddldiv] :selected").text() == "--Select--") {

    }
    else {
        var ddlgrp = $("[id*=ddlgrp]");

        $.ajax({
            type: "POST",
            url: "marks_entry.aspx/fillgrp",
            contentType: "application/json; charset=utf-8",
            data: '{medid:"' + $("[id*=ddlmedium]").val() + '",classid:"' + $("[id*=ddlclass]").val() + '",ayid:"' + ayid + '"}',
            success: function (r) {

                ddlgrp.empty().append('<option selected="selected" value="0">--Select--</option>');
                var group = [];
                group = JSON.parse(r.d);
                $.each(group.Table, function () {
                    ddlgrp.append($("<option></option>").val(this['group_id']).html(this['group_name']));
                });
                $("[id*=ddlgrp]")[0].disabled = false;
            },
            error: function (xhr, status, error) {
                $.notify(error, { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
            }
        });
    }
}

function loadsub() {
    $("[id*=ddlsub]").empty();
    $("[id*=ddlsub]")[0].selectedIndex = 0;
    $("[id*=ddlsub]")[0].disabled = true;
    $("[id*=tblgrid]").hide();
    $("[id*=tblfill]").empty();
    $("[id*=btnsave]").css("display", "none");
    $("[id*=btnexcel]").css("display", "none");
    $("[id*=btnimport]").css("display", "none");
    $("[id*=excfile]").css("display", "none");
    document.getElementById('excfile').value = "";
    if ($("[id*=ddlexam] :selected").text() == "--Select--") {

    }
    else {
        var ddlsub = $("[id*=ddlsub]");

        $.ajax({
            type: "POST",
            url: "marks_entry.aspx/Fillsub",
            contentType: "application/json; charset=utf-8",
            data: '{medid:"' + $("[id*=ddlmedium]").val() + '",classid:"' + $("[id*=ddlclass]").val() + '",ayid:"' + ayid + '",examid:"' + $("[id*=ddlexam]").val() + '",groupid:"' + $("[id*=ddlgrp]").val() + '"}',
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

function loaddata() {
    $("[id*=btnsave]").css("display", "none");
    $("[id*=tblgrid]").hide();
    $("[id*=tblfill]").empty();
    $("[id*=btnexcel]").css("display", "none");
    $("[id*=btnimport]").css("display", "none");
    $("[id*=excfile]").css("display", "none");
    $("[id*=excfile]").empty();
    document.getElementById('excfile').value = "";
    if ($("[id*=ddlsub] :selected").text() == "--Select--") {

    }
    else {
        var ddlsub = $("[id*=ddlsub]");

        $.ajax({
            type: "POST",
            url: "marks_entry.aspx/loaddata",
            contentType: "application/json; charset=utf-8",
            data: '{medid:"' + $("[id*=ddlmedium]").val() + '",classid:"' + $("[id*=ddlclass]").val() + '",ayid:"' + ayid + '",examid:"' + $("[id*=ddlexam]").val() + '",subjectid:"' + $("[id*=ddlsub]").val() + '",division:"' + $("[id*=ddldiv]").val() + '",groupid:"' + $("[id*=ddlgrp]").val() + '"}',
            success: function (r) {               
                var data = [];
                data = JSON.parse(r.d);
                if (data.Table.length > 0) {
                    for (var i = 0; i < data.Table1.length; i++) {
                        if (data.Table1[i]["exam_type"] == "Grade") {
                            var appenddata = appenddata + "<th>" + data.Table1[i]["exam_type"] + "</th>";
                        }
                        else {
                            var appenddata = appenddata + "<th>" + data.Table1[i]["exam_type"] + "<br/>(" + data.Table1[i]["passing_marks"] + "/" + data.Table1[i]["out_of_marks"] + ")</th></th>";
                        }
                    }

                    if (data.Table1.length > 0 && data.Table.length > 0) {

                        appenddata = appenddata + ""
                        $("[id*=tblgrid]").show();
                        $("[id*=tblfill]").empty();
                        $("[id*='tblfill']").append("<thead><tr  class='alert-info th'><th>Student ID</th> <th>Student Name</th><th >Roll Number</th>" + appenddata + "</tr></thead><tbody>");
                        var row = 0;
                        var col = 0;
                        var append = "";
                        for (var j = 0; j < data.Table.length; j++) {
                            if (j == 0) {
                                row = 1;
                                col = 4;
                                append = append + "<tr><td exmtype=" + data.Table[j]["exam_type"] + " >" + data.Table[j]["Student_id"] + "</td><td>" + data.Table[j]["student_name"] + "</td><td>" + data.Table[j]["Roll_no"] + "</td><td><input type='text' autocomplete='off' id='obt" + row + "R" + col + "C'  class='form-control' value='" + data.Table[j]["marks"] + "' exmtype=" + data.Table[j]["exam_type"] + "  ";

                                if (data.Table[j]["exam_type"] == "Grade") {
                                    append = append + "style='color:black; text-transform: uppercase;' MaxLength='2' onchange = 'validate(this)' onkeypress = 'return numberAlpha(this, event, true)'  onpaste = 'return false; '</td > ";
                                }
                                else {
                                    if (parseInt(data.Table[j]["marks"]) < parseInt(data.Table[j]["passing_marks"])) {
                                        append = append + " style = 'color:red;text-transform: uppercase;'";
                                    }
                                    else if (data.Table[j]["marks"].toUpperCase() == "AA")
                                    {
                                        append = append + " style = 'color:red;text-transform: uppercase;'";
                                    }
                                    else {
                                        append = append + " style = 'color:black;text-transform: uppercase;'";
                                    }
                                    append = append + " MaxLength='5' passing=" + data.Table[j]["passing_marks"] + " outof=" + data.Table[j]["out_of_marks"] +" onkeypress = 'return numberAlpha(this, event, false)' onchange = 'maxmarks(this, " + data.Table[j]["out_of_marks"] + ", " + data.Table[j]["passing_marks"] + ")'  onpaste = 'return false; '</td > ";
                                }
                                col++;
                            }
                            if (j > 0) {
                                if (data.Table[j]["Student_id"] == data.Table[j - 1]["Student_id"]) {
                                    append = append + "<td exmtype=" + data.Table[j]["exam_type"] + " ><input type='text' autocomplete='off' id='obt" + row + "R" + col + "C'  class='form-control' value='" + data.Table[j]["marks"] + "'";

                                    if (data.Table[j]["exam_type"] == "Grade") {
                                        append = append + "style='color:black; text-transform: uppercase;' MaxLength='2' onchange = 'validate(this)' onkeypress = 'return numberAlpha(this, event, true)'  exmtype = " + data.Table[j]["exam_type"] + "    onpaste = 'return false; ' /> </td > ";
                                    }
                                    else {
                                        if (parseInt(data.Table[j]["marks"]) < parseInt(data.Table[j]["passing_marks"])) {
                                            append = append + " style = 'color:red;text-transform: uppercase;'"
                                        }
                                        else if (data.Table[j]["marks"].toUpperCase() == "AA") {
                                            append = append + " style = 'color:red;text-transform: uppercase;'";
                                        }
                                        else {
                                            append = append + " style = 'color:black;text-transform: uppercase;'"
                                        }
                                        append = append + " MaxLength='5' passing=" + data.Table[j]["passing_marks"] + " outof=" + data.Table[j]["out_of_marks"] +"  onkeypress = 'return numberAlpha(this, event, false)' onchange = 'maxmarks(this, " + data.Table[j]["out_of_marks"] + ", " + data.Table[j]["passing_marks"] + ")' exmtype = " + data.Table[j]["exam_type"] + "   onpaste = 'return false; ' /> </td > ";
                                    }
                                    col++;
                                }
                                else {
                                    append = append + "<tr><td exmtype=" + data.Table[j]["exam_type"] + "  >" + data.Table[j]["Student_id"] + "</td><td>" + data.Table[j]["student_name"] + "</td><td>" + data.Table[j]["Roll_no"] + "</td><td><input autocomplete='off' type='text' id='obt" + row + "R" + col + "C'  class='form-control' value='" + data.Table[j]["marks"] + "' exmtype=" + data.Table[j]["exam_type"] + " ";

                                    if (data.Table[j]["exam_type"] == "Grade") {
                                        append = append + " style='color:black; text-transform: uppercase;' onchange = 'validate(this)'  MaxLength='2' onkeypress = 'return numberAlpha(this, event, true)'   onpaste = 'return false; ' /> </td > ";
                                    }
                                    else {
                                        if (parseInt(data.Table[j]["marks"]) < parseInt(data.Table[j]["passing_marks"])) {
                                            append = append + " style = 'color:red;text-transform: uppercase;'"
                                        }
                                        else if (data.Table[j]["marks"].toUpperCase() == "AA") {
                                            append = append + " style = 'color:red;text-transform: uppercase;'";
                                        }
                                        else {
                                            append = append + " style = 'color:black;text-transform: uppercase;'"
                                        }
                                        append = append + " MaxLength='5' passing=" + data.Table[j]["passing_marks"] + " outof=" + data.Table[j]["out_of_marks"] +"  onkeypress = 'return numberAlpha(this, event, false)' onchange = 'maxmarks(this, " + data.Table[j]["out_of_marks"] + ", " + data.Table[j]["passing_marks"] + ")'  onpaste = 'return false; ' /> </td > ";
                                    }
                                    row++;
                                    col++;
                                }
                            }
                        }
                        $("[id*='tblfill']").append(append);
                        $('[id*=tblfill]').fixedTableHeader();
                        $('[id*=tblfill]').arrowTable();
                        $("[id*=tblfill]").show();
                        $("[id*=btnsave]").css("display", "block");
                        $("[id*=btnexcel]").css("display", "block");
                        $("[id*=btnimport]").css("display", "block");
                        $("[id*=excfile]").css("display", "block");
                        $("#scroller").scrollTop(0);
                        $("#scroller").scrollLeft(0);
                    }
                }
                else {
                    $.notify('No data found', { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                }
            },
            error: function (xhr, status, error) {
                $.notify(error, { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
            }
        });
    }
}



$("[id*=ddlmedium]").change(function () {
    loadclass();
});

$("[id*=ddlclass]").change(function () {
    loaddiv();    
});

$("[id*=ddlgrp]").change(function () {
    loadexam();
});

$("[id*=ddldiv]").change(function () {
    $("[id*=ddlgrp]")[0].selectedIndex = 0;
    $("[id*=ddlgrp]")[0].disabled = true;
    $("[id*=ddlexam]")[0].selectedIndex = 0;
    $("[id*=ddlexam]")[0].disabled = true;
    $.ajax({
        type: "POST",
        url: "marks_entry.aspx/fillgrp",
        contentType: "application/json; charset=utf-8",
        data: '{medid:"' + $("[id*=ddlmedium]").val() + '",classid:"' + $("[id*=ddlclass]").val() + '",ayid:"' + ayid + '"}',
        success: function (r) {
            var data = [];
            data = JSON.parse(r.d);
            if (data.Table.length > 0) {
                loadgrp();
            }
            else {
                loadexam();
            }
        },
        error: function (xhr, status, error) {
            $.notify(error, { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
        }
    });

});

$("[id*=ddlsub]").change(function () {

    loaddata();

});

$("[id*=ddlexam]").change(function () {
    loadsub();
});


function alphabet(e, id) {

    isIE = document.all ? 1 : 0
    keyEntry = !isIE ? e.which : event.keyCode;

    if (((keyEntry == '65') || (keyEntry == '97')) || ((keyEntry >= '48') && (keyEntry <= '57'))) {
        return true;
    }
    else {
        return false;
    }
}

function validate(abc) {

    var id = abc.id;
    if ($("[id*=" + abc.id + "]").val() == "+") {
        $.notify("Invalid input", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
        $("[id*=" + abc.id + "]").val('');
        $("[id*=" + abc.id + "]").css("border-color", "red");
    }

}

function maxmarks(abc, val,pass) {
    var id = abc.id;
    var marks = abc.value;
    var maxmarks = val;
    var passmrk = pass;
    if (marks == "AA" || marks == "aa") {
        $("[id*=" + abc.id + "]").css("color", "red");
        $("[id*=" + abc.id + "]").css("border-color", "");
    }
    else {
        if ($("[id*=" + abc.id + "]").val().endsWith(".") == true) {
            $.notify("Invalid input", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
            $("[id*=" + abc.id + "]").val('');
            $("[id*=" + abc.id + "]").css("border-color", "red");
        }
        else {
            if (parseFloat(marks) > parseFloat(maxmarks)) {
                $.notify("Marks should be less than " + val + "", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                $("[id*=" + abc.id + "]").val('');
                $("[id*=" + abc.id + "]").css("border-color", "red");
            }
            else if (parseFloat(marks) < parseFloat(passmrk)) {

                $("[id*=" + abc.id + "]").css("color", "red");
                $("[id*=" + abc.id + "]").css("border-color", "");
            }
            //else if (marks.includes('A') == true || marks.includes('a') == true) {
            //    $.notify("Invalid input", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
            //    $("[id*=" + abc.id + "]").val('');
            //    $("[id*=" + abc.id + "]").css("border-color", "red");
            //}
            else {
                $("[id*=" + abc.id + "]").css("border-color", "");
                $("[id*=" + abc.id + "]").css("color", "black");
            }
        }
    }
}

$("[id*='btnrefresh']").on("click", function () {
    location.reload();
});

$("[id*='btnsave']").on("click", function () {
    
    var obj = {};
    var details = [];
    $("[id*=tblfill] tr").each(function (r) {
        if (r > 0) {
            var stud_id = '', marks = '', exam_type = '';
            var tcols = $(this).find('td').length;
            $(this).find('td').each(function (c) {
                if (c == 0) {
                    stud_id = $(this)[0].innerText;
                }
                if (c > 2) {
                    $(this).find('input').each(function (c) {
                        if ($(this)[0].hasAttribute('exmtype')) {
                            exam_type = $(this).attr('exmtype');
                        }
                        if ($(this).attr('id').indexOf("obt") != -1) {
                            marks = $(this).val();
                        }
                        marks = $(this).val();
                        

                    });
                    details.push({
                        'stud_id': stud_id, 'marks': marks, 'exam_type': exam_type
                    })
                }
            });

        }
    });


    obj.ayid = ayid;
    obj.subject_id = $("[id*=ddlsub]").val();
    obj.user_id = empId;
    obj.mrkdata = details;
    obj.examid = $("[id*=ddlexam]").val();
    obj.division = $("[id*=ddldiv]").val();
    obj.class_id = $("[id*=ddlclass]").val();
    obj.med_id = $("[id*=ddlmedium]").val();
    obj.type = "insert";
    

    $.ajax({
        type: "POST",
        url: "marks_entry.aspx/savedata",
        contentType: "application/json; charset=utf-8",
        data: '{obj:' + JSON.stringify(obj) + '}',
        success: function (r) {
            if (r.d=="true") {
                $.notify("Data Saved Successfully", { color: "#fff", background: "#127515", blur: 0.2, delay: 0 });
                $("[id*=btnsave]").css("display", "none");
                $("[id*=ddlsub]")[0].selectedIndex = 0;
                $("[id*=tblgrid]").hide();
                $("[id*=tblfill]").empty();
                $("[id*=btnexcel]").css("display", "none");
                $("[id*=btnimport]").css("display", "none");
                $("[id*=excfile]").css("display", "none");
            }
           
        },
        error: function (xhr, status, error) {
            $.notify(error, { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
        }
    });


});

$("[id*='btnexcel']").on("click", function (e) {
    $("[id*='exceldata']").empty();
    var tblappend = "";
    $("[id*=tblfill] thead ").each(function (r) {
        tblappend = tblappend + "<tr>";
        $(this).find('th').each(function (c) {           
                tblappend = tblappend + "<th>" + $(this)[0].innerText + "</th>";         
        });
    });
    
    $("[id*=tblfill] tbody tr").each(function (r) {
        tblappend = tblappend + "<tr>";
        $(this).find('td').each(function (c) {
            if (c < 3) {
                tblappend = tblappend + "<td>" + $(this)[0].innerText + "</td>";
            }
            if (c > 2) {               
                $(this).find('input').each(function (c) {
                    tblappend = tblappend + "<td>" + $(this).val() + "</td>";
                });
            }
        });        
    });
    $("[id*='exceldata']").append(tblappend);
    tableToExcel('exceldata', 'Marksentry', 'Marksentry.xls');
});

$("[id*=excfile]").change(function () {
    var photo_type = "";
    if ($("[id*=excfile]").val() != '') {
        res = $("[id*=excfile]").get(0).files[0].name.split(".");
        photo_type = '.' + res[res.length - 1];
    }
    if (photo_type.toLowerCase().indexOf('xls') == -1 ) {
        alert("Only Excel Format Allowed");
        document.getElementById('excfile').value = "";
    }
});

$("[id*='btnimport']").on("click", function () {
    var files = document.getElementById('excfile').files;
    if (files.length == 0) {
        $.notify("Choose Valid Excel File.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
        document.getElementById('excfile').value = "";
        return;
    }
    var filename = files[0].name;
    var extension = filename.substring(filename.lastIndexOf(".")).toUpperCase();
    if (extension == '.XLS' || extension == '.XLSX') {

        excelFileToJSON(files[0]);

    } else {
        $.notify("Select Valid Excel File.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
        document.getElementById('excfile').value = "";
    }

});

function numberAlpha(t, e, flag) {
    if (flag == false) {
        isIE = document.all ? 1 : 0
        keyEntry = !isIE ? e.which : event.keyCode;
        //if ($("[id*=" + t.id + "]").val().endsWith(".") == true) {
        //    $("[id*=" + t.id + "]").val('');
        //    $("[id*=" + t.id + "]").css("border-color", "red");
        //}
        //if ($("[id*=" + t.id + "]").val() == "" && keyEntry >= '43') {
        //    return false;
        //}
        if ((((keyEntry >= '48') && (keyEntry <= '57')) && (keyEntry <= '65')) || (keyEntry == '46') || (keyEntry == '97')) {
            if ($("[id*=" + t.id + "]").val() == "" && keyEntry == "46") {
                return false;
            }
            if ($("[id*=" + t.id + "]").val().toUpperCase().includes(".") == true && keyEntry == "46") {
                return false;
            }
            if ($("[id*=" + t.id + "]").val().toUpperCase().includes("A") == true && keyEntry != "97") {
                return false;
            }
            if (keyEntry == "97" && $("[id*=" + t.id + "]").val() != "") {
                if ($("[id*=" + t.id + "]").val().toUpperCase() == "A") {
                    return true;
                }
                else {
                    return false;
                }
            }
            if (keyEntry == "97" && $("[id*=" + t.id + "]").val().toUpperCase() == "AA") {
                return false;
            }
            if ($("[id*=" + t.id + "]").val().endsWith(".") == true && $("[id*=" + t.id + "]").val().includes(".") == true) {
                return true;
            }
            else if ($("[id*=" + t.id + "]").val().includes(".") == false) {
                return true;
            }
            else {

                return false;
            }
        }
        else {
            return false;
        }
    }
    else if (flag == true) {
        isIE = document.all ? 1 : 0
        keyEntry = !isIE ? e.which : event.keyCode;

        if ((keyEntry >= '65') && (keyEntry <= '70') || (keyEntry >= '97') && (keyEntry <= '102') || (keyEntry == '43')) {
            if ($("[id*=" + t.id + "]").val() != "" && keyEntry != '97' && keyEntry != '43') {
                return false;
            }
            if ($("[id*=" + t.id + "]").val() == "" && keyEntry == '43') {
                return false;
            }
            else if ($("[id*=" + t.id + "]").val().endsWith("+") == true)
            {
                return false;
            }          
            else {
               
                $("[id*=" + t.id + "]").css("border-color", "");
                $("[id*=" + t.id + "]").css("color", "black");
                return true;
            }
        }
        else {
            return false;
        }
    }
}

function invaliddata() {
    $("[id*=btnsave]").css("display", "none");
    $("[id*=tblgrid]").hide();
    $("[id*=tblfill]").empty();
    $("[id*=btnexcel]").css("display", "none");
    $("[id*=btnimport]").css("display", "none");
    $("[id*=excfile]").css("display", "none");
    $("[id*=ddlsub]")[0].selectedIndex = 0;
}

function onlyNumbers(str) {
    return /^[A-Za-z]*$/.test(str);
}

function excelFileToJSON(file) {
    event.preventDefault();
    try {
        var fdata = {};
        var reader = new FileReader();
        reader.readAsBinaryString(file);
        reader.onload = function (e) {

            var data1 = e.target.result;
            var workbook = XLSX.read(data1, {
                type: 'binary'
            });
            var result = {};
            workbook.SheetNames.forEach(function (sheetName) {
                var roa = XLSX.utils.sheet_to_row_object_array(workbook.Sheets[sheetName]);
                if (roa.length > 0) {
                    result = roa;
                }
            });

            if (result.length > 0) {
                $.ajax({
                    type: "POST",
                    url: "marks_entry.aspx/checkexcel",
                    contentType: "application/json; charset=utf-8",
                    data: '{medid:"' + $("[id*=ddlmedium]").val() + '",classid:"' + $("[id*=ddlclass]").val() + '",ayid:"' + ayid + '",examid:"' + $("[id*=ddlexam]").val() + '",subjectid:"' + $("[id*=ddlsub]").val() + '",division:"' + $("[id*=ddldiv]").val() + '"}',
                    success: function (r) {
                        var data = [];
                        data = JSON.parse(r.d);
                        var dataflag = false;
                        var check = false;
                        $("[id*=tblfill] tr").each(function (r) {                            
                            if (r > 0) {
                                var theory = "";
                                var practical = "";
                                var internal = "";
                                var grade = "";
                                var rstud = "";

                                for (var key in result[r - 1]) {
                                    if (key.indexOf("Student ID") != -1) {
                                        rstud = result[r - 1][key];
                                    }
                                    else if (key.indexOf("Theory") != -1) {
                                        theory = result[r - 1][key];
                                    } else if (key.indexOf("Practical") != -1) {
                                        practical = result[r - 1][key];
                                    } else if (key.indexOf("Internal") != -1) {
                                        internal = result[r - 1][key];
                                    } else if (key.indexOf("Grade") != -1) {
                                        grade = result[r - 1][key];
                                    }
                                }
                                var stud_id = ''
                              
                               
                                $(this).find('td').each(function (c) {
                                    if (c == 0) {
                                        stud_id = $(this)[0].innerText;
                                        if (stud_id != rstud) {
                                            $.notify("Excel data is not matched with table data", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                                            invaliddata();
                                            return;
                                        }
                                    }
                                    if (c > 2) {
                                        var outof = "";
                                        var pass = "";
                                        var spChars = /[!@#$%^&*()_+\-=\[\]{};':"\\|,<>\/?]+/;
                                        $(this).find('input').each(function (c) {
                                            if ($(this).attr('exmtype').indexOf("Theory") != -1) {
                                                $(this)[0].value = theory;
                                                outof = $(this).attr('outof');
                                                pass = $(this).attr('passing');
                                                //var regex = "[^a-zA-Z0-9]+";
                                                //Pattern p = Pattern.compile(regex);
                                                //Matcher m = p.matcher(theory);
                                                if (spChars.test(theory) || (onlyNumbers(theory) == true && theory != "AA")) {
                                                    $(this)[0].value = "";
                                                    $("[id*=" + $(this)[0].id + "]").css("border-color", "red");
                                                    dataflag = true;
                                                }
                                                else {
                                                    if (parseFloat(theory).toString().length > 5) {
                                                        $(this)[0].value = "";
                                                        $("[id*=" + $(this)[0].id + "]").css("border-color", "red");
                                                        dataflag = true;
                                                    }
                                                    if (parseFloat(theory) > parseFloat(outof)) {
                                                        $(this)[0].value = "";
                                                        $("[id*=" + $(this)[0].id + "]").css("border-color", "red");
                                                        dataflag = true;
                                                    }
                                                    if (parseFloat(theory) < parseFloat(pass)) {
                                                        $("[id*=" + $(this)[0].id + "]").css("color", "red");
                                                    }
                                                    else {
                                                        $("[id*=" + $(this)[0].id + "]").css("color", "black");
                                                        $("[id*=" + $(this)[0].id + "]").css("border-color", "");
                                                    }
                                                }
                                                if (theory == "AA") {
                                                    $("[id*=" + $(this)[0].id + "]").css("color", "red");
                                                    $("[id*=" + $(this)[0].id + "]").css("border-color", "");
                                                }
                                                if (theory != "") {

                                                    check = true;
                                                }

                                            }
                                            if ($(this).attr('exmtype').indexOf("Practical") != -1) {
                                                $(this)[0].value = practical;
                                                outof = $(this).attr('outof');
                                                pass = $(this).attr('passing');
                                                if (spChars.test(practical) || (onlyNumbers(practical) == true && practical != "AA")) {
                                                    $(this)[0].value = "";
                                                    $("[id*=" + $(this)[0].id + "]").css("border-color", "red");
                                                    dataflag = true;
                                                }
                                                else {
                                                    if (parseFloat(practical).toString().length > 5) {
                                                        $(this)[0].value = "";
                                                        $("[id*=" + $(this)[0].id + "]").css("border-color", "red");
                                                        dataflag = true;
                                                    }
                                                    if (parseInt(practical) > parseInt(outof)) {
                                                        $(this)[0].value = "";
                                                        $("[id*=" + $(this)[0].id + "]").css("border-color", "red");
                                                        dataflag = true;
                                                    }
                                                    if (parseInt(practical) < parseInt(pass)) {
                                                        $("[id*=" + $(this)[0].id + "]").css("color", "red");
                                                    }
                                                    else {
                                                        $("[id*=" + $(this)[0].id + "]").css("color", "black");
                                                        $("[id*=" + $(this)[0].id + "]").css("border-color", "");
                                                    }
                                                }
                                                if (practical == "AA") {
                                                    $("[id*=" + $(this)[0].id + "]").css("color", "red");
                                                    $("[id*=" + $(this)[0].id + "]").css("border-color", "");
                                                }
                                                if (practical != "") {

                                                    check = true;
                                                }
                                            }
                                            if ($(this).attr('exmtype').indexOf("Internal") != -1) {
                                                $(this)[0].value = internal;
                                                outof = $(this).attr('outof');
                                                pass = $(this).attr('passing');
                                                if (spChars.test(internal) || (onlyNumbers(internal) == true && internal != "AA")) {
                                                    $(this)[0].value = "";
                                                    $("[id*=" + $(this)[0].id + "]").css("border-color", "red");
                                                    dataflag = true;
                                                }
                                                else {
                                                    if (parseFloat(internal).toString().length > 5) {
                                                        $(this)[0].value = "";
                                                        $("[id*=" + $(this)[0].id + "]").css("border-color", "red");
                                                        dataflag = true;
                                                    }
                                                    if (parseInt(internal) > parseInt(outof)) {
                                                        $(this)[0].value = "";
                                                        $("[id*=" + $(this)[0].id + "]").css("border-color", "red");
                                                        dataflag = true;
                                                    }
                                                    if (parseInt(internal) < parseInt(pass)) {
                                                        $("[id*=" + $(this)[0].id + "]").css("color", "red");
                                                    }
                                                    else {
                                                        $("[id*=" + $(this)[0].id + "]").css("color", "black");
                                                        $("[id*=" + $(this)[0].id + "]").css("border-color", "");
                                                    }
                                                }
                                                if (internal == "AA") {
                                                    $("[id*=" + $(this)[0].id + "]").css("color", "red");
                                                    $("[id*=" + $(this)[0].id + "]").css("border-color", "");
                                                }
                                                if (internal != "") {

                                                    check = true;
                                                }
                                            }
                                            if ($(this).attr('exmtype').indexOf("Grade") != -1) {
                                                $(this)[0].value = grade;
                                                if ($(this)[0].value.indexOf("+") != -1) {
                                                    grade = grade.substring(0, grade.length - 1);
                                                }
                                                if (grade == "A" || grade == "B" || grade == "C" || grade == "D" || grade == "E" || grade == "F" || grade == "") {
                                                    $("[id*=" + $(this)[0].id + "]").css("border-color", "");
                                                }
                                                else {
                                                    $(this)[0].value = "";
                                                    $("[id*=" + $(this)[0].id + "]").css("border-color", "red");
                                                    dataflag = true;
                                                }
                                                if (grade != "") {

                                                    check = true;
                                                }
                                            }
                                        });
                                    }
                                });

                            }
                        });
                        $("#scroller").scrollTop(0);
                        $("#scroller").scrollLeft(0);
                        if (dataflag == true) {
                            $.notify("You have entered some invalid data in excel file.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                            
                        }
                        if (check == false) {
                            $.notify("Select valid excel file", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                        }
                        document.getElementById('excfile').value = "";
                    }
                });
            }            
        else {
                $.notify("Import valid excel file", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                return;
            }
        }

    }
    
    catch (e) {
        console.error(e);
    }
}


var tableToExcel = (function () {
    var uri ='data:application/vnd.ms-excel;base64,'
        , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/html401"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>'
        , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
        , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }
    return function (table, name, filename) {
        if (!table.nodeType) table = document.getElementById(table)
        var ctx = { worksheet: name || 'Worksheet', table: table.innerHTML }

        document.getElementById("dlink").href = uri + base64(format(template, ctx));
        document.getElementById("dlink").download = filename;
        document.getElementById("dlink").click();

    }
})()
