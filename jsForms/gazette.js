

$(document).ready(function () {
    loadmedium();
})

function loadmedium() {
    var ddlmedium = $("[id*=ddlmedium]");

    $.ajax({
        type: "POST",
        url: "gazette.aspx/Fillmedium",
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
    $("[id*=ddldiv]").empty();
    $("[id*=ddlexam]").empty();
    $("[id*=ddlgrp]").empty();
    $("[id*=ddlclass]")[0].selectedIndex = 0;
    $("[id*=ddlclass]")[0].disabled = true;
    $("[id*=ddldiv]")[0].selectedIndex = 0;
    $("[id*=ddldiv]")[0].disabled = true;
    $("[id*=ddlexam]")[0].selectedIndex = 0;
    $("[id*=ddlexam]")[0].disabled = true;
    $("[id*=ddlgrp]")[0].selectedIndex = 0;
    $("[id*=ddlgrp]")[0].disabled = true;
    $("[id*=tblgrid]").hide();
    $("[id*=tblfill]").empty();
    $("[id*=btnget]").css("display", "none");
    $("[id*=btnexcel]").css("display", "none");
    $("[id*=btnresult]").css("display", "none");
    $("[id*=divresult]").css("display", "none");
    if ($("[id*=ddlmedium] :selected").text() == "--Select--") {

    }
    else {
        var ddlclass = $("[id*=ddlclass]");

        $.ajax({
            type: "POST",
            url: "gazette.aspx/Fillclass",
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

$("[id*=ddlgrp]").change(function () {
    loaddiv();
});


function loaddiv() {
    $("[id*=ddldiv]").empty();
    $("[id*=ddldiv]")[0].selectedIndex = 0;
    $("[id*=ddldiv]")[0].disabled = true;
    $("[id*=tblgrid]").hide();
    $("[id*=tblfill]").empty();
    $("[id*=btnget]").css("display", "none");
    $("[id*=btnexcel]").css("display", "none");
    $("[id*=btnresult]").css("display", "none");
    $("[id*=divresult]").css("display", "none");
    if ($("[id*=ddlexam] :selected").text() == "--Select--") {

    }
    else {
        if ($("[id*=ddlgrp]")[0].disabled == false && $("[id*=ddlgrp] :selected").text() == "--Select--") {
            $("[id*=ddldiv]")[0].selectedIndex = 0;
            $("[id*=ddldiv]")[0].disabled = true;
        }
        else {
            $("[id*=btnget]").css("display", "block");
            var ddldiv = $("[id*=ddldiv]");

            $.ajax({
                type: "POST",
                url: "gazette.aspx/Filldiv",
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
}

function loadexam() {
    $("[id*=ddlexam]").empty();
    $("[id*=ddlexam]")[0].selectedIndex = 0;
    $("[id*=ddlexam]")[0].disabled = true;
    $("[id*=ddldiv]").empty();
    $("[id*=ddldiv]")[0].selectedIndex = 0;
    $("[id*=ddldiv]")[0].disabled = true;
    $("[id*=btnget]").css("display", "none");
    $("[id*=btnexcel]").css("display", "none");
    $("[id*=btnresult]").css("display", "none");
    $("[id*=divresult]").css("display", "none");
    $("[id*=ddlgrp]").empty();
    $("[id*=ddlgrp]")[0].selectedIndex = 0;
    $("[id*=ddlgrp]")[0].disabled = true;
    $("[id*=tblgrid]").hide();
    $("[id*=tblfill]").empty();
    if ($("[id*=ddlclass] :selected").text() == "--Select--") {

    }
    else {
        var ddlexam = $("[id*=ddlexam]");

        $.ajax({
            type: "POST",
            url: "gazette.aspx/Fillexam",
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

$("[id*=ddlmedium]").change(function () {
    loadclass();
});

$("[id*=ddlclass]").change(function () {
    loadexam();
    
});

$("[id*=ddldiv]").change(function () {
    $("[id*=btnexcel]").css("display", "none");
    $("[id*=btnresult]").css("display", "none");
    $("[id*=divresult]").css("display", "none");
    $("[id*=tblgrid]").hide();
    $("[id*=tblfill]").empty();
});

$("[id*=btnget]").click(function () {
    loaddata();
});

$("[id*=ddlexam]").change(function () {
    $("[id*=ddldiv]").empty();
    $("[id*=ddldiv]")[0].selectedIndex = 0;
    $("[id*=ddldiv]")[0].disabled = true;
    $("[id*=ddlgrp]").empty();
    $("[id*=ddlgrp]")[0].selectedIndex = 0;
    $("[id*=ddlgrp]")[0].disabled = true;
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
                loaddiv();
            }
        },
        error: function (xhr, status, error) {
            $.notify(error, { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
        }
    });
   
});

function loadgrp() {
  
    $("[id*=ddlsub]").empty();
   
    $("[id*=btnsave]").css("display", "none");
    $("[id*=btnexcel]").css("display", "none");;

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

$("[id*=chkformula]").click(function () {
    $("[id*=div_btnexcel]").css("display", "none");
    $("[id*=tblgrid]").hide();
    $("[id*=tblfill]").empty();
});

function loaddata() {  
    $("[id*=tblgrid]").hide();
    $("[id*=tblfill]").empty();
    $("[id*=btnexcel]").css("display", "none");
    var urlstring = "";
    if ($("[id*=chkformula]")[0].checked == true) {
        urlstring = "loaddata";
    }
    else {
        urlstring = "loaddatawithoutformula";
    }
    if ($("[id*=ddlexam] :selected").text() == "--Select--") {

    }
    else {
        var ddlexam = $("[id*=ddlexam]");
        localStorage.clear();
        $.ajax({
            type: "POST",
            url: "gazette.aspx/" + urlstring + "",
            contentType: "application/json; charset=utf-8",
            data: '{medid:"' + $("[id*=ddlmedium]").val() + '",classid:"' + $("[id*=ddlclass]").val() + '",ayid:"' + ayid + '",examid:"' + $("[id*=ddlexam]").val() + '",division:"' + $("[id*=ddldiv]").val() + '",groupid:"' + $("[id*=ddlgrp]").val() + '"}',
            success: function (r) {
                localStorage.setItem("result", r.d);
                var dn = $("[id*=ddlclass]");
                var selectedTxt = dn.find("option:selected").text();               
                localStorage.setItem("standard", selectedTxt);

                var ds = $("[id*=ddlexam]");
                var selectedexm = ds.find("option:selected").text();
                localStorage.setItem("Exam", selectedexm);

                var dm = $("[id*=ddlmedium]");
                var selectedmed = dm.find("option:selected").val();
                localStorage.setItem("Medium", selectedmed);

              
                var data = [];
                data = JSON.parse(r.d);
                var passing = 0;
                var outof = 0;
                var subpass = 0;
                var flagremark = "PASS";
                var dn = $("[id*=ddlclass]");
                var selectedTxt = dn.find("option:selected").text();
                file = selectedTxt + "_" + $("[id*=ddlexam] :selected").text();

                var appenddata = "";
                var check = 0;
                if (data.Table.length > 0) {

                    for (var i = 0; i < data.Table1.length; i++) {
                        var subject = data.Table1[i]["subject_id"];
                        var colspan = 0;
                        if (data.Table1[i]["exam_type"] != "Grade") {
                            passing = parseFloat(passing) + parseFloat(data.Table1[i]["passing"]);
                            outof = parseFloat(outof) + parseFloat(data.Table1[i]["outof"]);
                        }
                        for (var j = 0; j < data.Table1.length; j++) {
                            if (subject == data.Table1[j]["subject_id"]) {
                                colspan++;

                            }
                        }
                        data.Table1[i]["colspan"] = colspan + 2;                      

                    }
                    check = parseInt(data.Table2.length) * parseInt(data.Table3.length);
                    if (parseInt(check) != parseInt(data.Table.length)) {
                        $.notify('you have not done marks entry for some subjects', { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                        return;
                    }
                    for (var i = 0; i < data.Table1.length; i++) {
                       
                        if (i == 0) {
                            if (data.Table1[i]["colspan"] != "" && data.Table1[i]["exam_type"] != "Grade") {
                                appenddata = appenddata + "<th  class='alert-info' style='text-align:center' colspan=" + data.Table1[i]["colspan"] + ">" + data.Table1[i]["subject_name"] + "</th>";
                            }
                            else {
                                appenddata = appenddata + "<th  class='alert-info' style='text-align:center' >" + data.Table1[i]["subject_name"] + "</th>";
                            }
                        }
                        else {
                            if (data.Table1[i]["subject_id"] == data.Table1[i - 1]["subject_id"]) {
                            }
                            else {
                                if (data.Table1[i]["colspan"] != "" && data.Table1[i]["exam_type"] != "Grade") {
                                    appenddata = appenddata + "<th  class='alert-info' style='text-align:center' colspan=" + data.Table1[i]["colspan"] + ">" + data.Table1[i]["subject_name"] + "</th>";
                                }
                                else {
                                    appenddata = appenddata + "<th  class='alert-info' style='text-align:center'>" + data.Table1[i]["subject_name"] + "</th>";
                                }
                            }
                        }
                    }
                    appenddata = appenddata + "<th  class='alert-info' rowspan='2'>Total Marks</th><th class='alert-info' rowspan='2'>Percentage</th><th class='alert-info' rowspan='2'>Remark</th><th class='alert-info' rowspan='2'>Grade</th></tr><tr>";
                    for (var i = 0; i < data.Table1.length; i++)  {
                        if (i == 0) {
                            if (data.Table1[i]["exam_type"] != "") {
                                appenddata = appenddata + "<th  class='alert-info'>" + data.Table1[i]["exam_type"] + "<br/>(" + data.Table1[i]["passing"] + "/" + data.Table1[i]["outof"] + ")</th>";
                            }
                            if (data.Table1.length > 0 && data.Table1[i]["colspan"] == 3) {
                                appenddata = appenddata + "<th> Total</th><th> Grade</th> ";
                            }
                        }
                        else {
                            if (parseInt(i) == parseInt(data.Table1.length - 1)) {
                                if (data.Table1[i]["exam_type"] != "Grade") {
                                    if (data.Table1[i]["subject_id"] != data.Table1[i - 1]["subject_id"]) {
                                        appenddata = appenddata + "<th  class='alert-info'>" + data.Table1[i]["exam_type"] + "<br/>(" + data.Table1[i]["passing"] + "/" + data.Table1[i]["outof"] + ")</th><th class='alert-info'>Total</th><th> Grade</th> ";
                                    }
                                    else {
                                        appenddata = appenddata + "<th  class='alert-info'>" + data.Table1[i]["exam_type"] + "<br/>(" + data.Table1[i]["passing"] + "/" + data.Table1[i]["outof"] + ")</th></th><th class='alert-info'>Total</th><th> Grade</th> ";
                                    }
                                }
                                else {                                  
                                        appenddata = appenddata + "<th  class='alert-info'>" + data.Table1[i]["exam_type"] + "</th>";                               
                                }
                            }
                            else {
                                if (data.Table1[i]["subject_id"] == data.Table1[i + 1]["subject_id"]) {

                                    if (data.Table1[i]["exam_type"] != "" ) {
                                        appenddata = appenddata + "<th  class='alert-info'>" + data.Table1[i]["exam_type"] + "<br/>(" + data.Table1[i]["passing"] + "/" + data.Table1[i]["outof"] + ")</th>";
                                        }
                                    
                                }
                                else {
                                    if (data.Table1[i]["exam_type"] != "" && data.Table1[i]["exam_type"] != "Grade") {
                                        appenddata = appenddata + "<th  class='alert-info'>" + data.Table1[i]["exam_type"] + "<br/>(" + data.Table1[i]["passing"] + "/" + data.Table1[i]["outof"] + ")</th></th><th class='alert-info'>Total</th><th> Grade</th> ";
                                    }
                                    else {
                                        appenddata = appenddata + "<th  class='alert-info'>" + data.Table1[i]["exam_type"] + "</th>";
                                    }
                                }
                            }
                        }
                        
                    }
                    if (data.Table1.length > 0 && data.Table.length > 0) {

                        appenddata = appenddata + ""
                        $("[id*=tblgrid]").show();
                        $("[id*=tblfill]").empty();
                        $("[id*='tblfill']").append("<thead><tr  class='alert-info th'><th rowspan='2'>GR No</th><th rowspan='2'>Student ID</th><th rowspan='2'>Student Name</th><th rowspan='2'>Roll Number</th>" + appenddata + "</thead><tbody>");
                      
                        var rowcount = 0;
                        var append = "";
                        var total_marks = 0;
                        var total_out = 0;
                        var perct = 0;
                        var grade = "";
                        var subtot = 0;
                        var subout = 0;
                        var subper = 0;
                        
                        var index_len = 0;

                        for (var j = 0; j < data.Table.length; j++) {
                            var total_3_sub_count;
                            var colcount = 0;
                            var formulamarks = 0;
                            //var length = data.Table1.length;
                            //if (j == length) {
                            //    total_3_sub_count = 0;
                            //}
                            //else {
                            //    var length1 = length + length;
                            //    if (length1 == j) {
                            //        total_3_sub_count = 0;
                            //    }
                            //}

                            if (j % data.Table1.length == 0) {
                                total_3_sub_count = 0;
                                var total_grace = 15;
                               var total_remain_grace_mark = 15;
                            }

                                if (j == 0) {
                                    append = append + "<tr><td style='text-align:center;'><label  id='lblstudgr" + rowcount + "R" + colcount + "C'>" + data.Table[j].stud_gr + "</label></td>";
                                    colcount++;
                                    append = append + " <td style='text-align:center;'><label  id='lblstud" + rowcount + "R" + colcount + "C'>" + data.Table[j].stud_id + "</label></td>";
                                    colcount++;
                                    append = append + "<td style='text-align:center;'><label style='width: 300px;' id='lblname" + rowcount + "R" + colcount + "C'>" + data.Table[j].stud_name + "</label></td>";
                                    colcount++;
                                    append = append + "<td style='text-align:center;'><label  id='lblroll" + rowcount + "R" + colcount + "C'>" + data.Table[j].roll_no + "</label></td>";
                                    colcount++;
                                }
                                if (j > 0) {
                                    if (data.Table[j].stud_id == data.Table[j - 1].stud_id) {

                                        if (data.Table[j].subject_id != data.Table[j - 1].subject_id && data.Table[j - 1]["exam_type"] != "Grade") {
                                            subper = (parseFloat(subtot) * 100) / parseFloat(subout);
                                            if (parseFloat(subtot) < parseFloat(subpass)) {
                                                if (subtot - Math.floor(subtot) != 0 && parseFloat(subtot).toString().length > 4) {
                                                    append = append + "<td style = 'color:red;'>" + subtot.toFixed(2) + "</td>";
                                                }
                                                else {
                                                    append = append + "<td style = 'color:red;'>" + subtot + "</td>";
                                                }
                                                flagremark = "FAIL";
                                            }
                                            else {
                                                if (subtot - Math.floor(subtot) != 0 && parseFloat(subtot).toString().length > 4) {
                                                    append = append + "<td>" + subtot.toFixed(2) + "</td>";
                                                }
                                                else {
                                                    append = append + "<td>" + subtot + "</td>";
                                                }
                                            }
                                            append = append + "<td style='text-align:center;'>" + get_grade(subper, selectedmed) + "</td>";

                                            subtot = 0;
                                            subpass = 0;
                                            subout = 0;
                                            colcount++;
                                        }

                                    }
                                    else {

                                        if (data.Table[j - 1]["exam_type"] != "Grade") {
                                            subper = (parseFloat(subtot) * 100) / parseFloat(subout);
                                            if (parseFloat(subtot) < parseFloat(subpass)) {
                                                if (subtot - Math.floor(subtot) != 0 && parseFloat(subtot).toString().length > 4) {
                                                    append = append + "<td style = 'color:red;'>" + subtot.toFixed(2) + "</td>";
                                                }
                                                else {
                                                    append = append + "<td style = 'color:red;'>" + subtot + "</td>";
                                                }
                                                flagremark = "FAIL";
                                            }
                                            else {
                                                if (subtot - Math.floor(subtot) != 0 && subtot.length > 4) {
                                                    append = append + "<td>" + subtot.toFixed(2) + "</td>";
                                                }
                                                else {
                                                    append = append + "<td>" + subtot + "</td>";
                                                }
                                            }
                                            append = append + "<td style='text-align:center;'>" + get_grade(subper, selectedmed) + "</td>";

                                            subtot = 0;
                                            subpass = 0;
                                            subout = 0;
                                            colcount++;
                                        }
                                        perct = (parseFloat(total_marks) * 100) / parseFloat(outof);
                                        append = append + "<td style='text-align:center;'>" + total_marks + "</td><td style='text-align:center;'>" + perct.toFixed(2) + "</td>";
                                        if (parseInt(perct) >= 35 && flagremark == "PASS") {
                                            append = append + " <td style = 'text-align:center;'>Pass</td>";
                                            flagremark = "PASS";
                                        }
                                        else {
                                            append = append + " <td style = 'text-align:center;'>Fail</td>";
                                            flagremark = "PASS";
                                        }

                                        append = append + "<td style='text-align:center;'>" + get_grade(perct, selectedmed) + "</td>";
                                        total_marks = 0;
                                        total_out = 0;
                                        rowcount++;
                                        append = append + "<tr><td style='text-align:center;'><label  id='lblstudgr" + rowcount + "R" + colcount + "C'>" + data.Table[j].stud_gr + "</label></td>";
                                        colcount++;
                                        append = append + " <td style='text-align:center;'><label  id='lblstud" + rowcount + "R" + colcount + "C'>" + data.Table[j].stud_id + "</label></td>";
                                        colcount++;
                                        append = append + "<td style='text-align:center;'><label style='width: 300px;' id='lblname" + rowcount + "R" + colcount + "C'>" + data.Table[j].stud_name + "</label></td>";
                                        colcount++;
                                        append = append + "<td style='text-align:center;'><label  id='lblroll" + rowcount + "R" + colcount + "C'>" + data.Table[j].roll_no + "</label></td>";
                                        colcount++;
                                    }
                                }
                            
                            
                            if (data.Table[j].formula != "" && data.Table[j].formula != null && data.Table[j].formula != 'undefined') {
                                if (!(data.Table[j].formula.includes("AA")) || !(data.Table[j].formula.includes("AA")) ) {
                                    var myArray = data.Table[j].formula.split("/");
                                    formulamarks = Math.round(eval(eval(myArray[0]) / myArray[1]));
                                    var added_grace_formulamarks
                                    added_grace_formulamarks = formulamarks
                                     //sakshi
                                    if (formulamarks < data.Table[j].compare) {
                                        var requiredToPass = data.Table[j].compare - formulamarks;
                                        if (total_3_sub_count == undefined) {
                                            total_3_sub_count = 0
                                        }
                                       
                                        
                                        var check_gracemark_used = "";
                                        
                                        var grace_flag = false;
                                        if (requiredToPass <= 10 && total_3_sub_count < 3 && requiredToPass <= total_remain_grace_mark) {
                                           

                                          /*  formulamarks += requiredToPass;*/
                                            added_grace_formulamarks = formulamarks;
                                            added_grace_formulamarks += requiredToPass
                                            total_3_sub_count += 1;
                                            
                                                total_remain_grace_mark = total_remain_grace_mark - requiredToPass;
                                            
                                           
                                            grace_flag = true;
                                    
                                        }
                                        else {
                                            
                                        }
                                    }
                                }
                               
                                if (data.Table[j].exam_type != 'Grade') {
                                    if ((((formulamarks) < parseFloat(data.Table[j].compare)) || data.Table[j].formula.includes("AA")) && grace_flag == false) {
                                        append = append + "<td style='text-align:center; color:red;' subject_id='" + data.Table[j].subject_id + "' tdtype='" + data.Table[j].exam_type + "' ><label  id='lbl_marks" + rowcount + "R" + colcount + "C'>" + formulamarks + "  </label</td>";
                                        //  flagremark = "FAIL";
                                    }

                                    else if ((((formulamarks) < parseFloat(data.Table[j].compare)) || data.Table[j].formula.includes("AA")) && grace_flag == true) {
                                        append = append + "<td style='text-align:center; color:red;' subject_id='" + data.Table[j].subject_id + "' tdtype='" + data.Table[j].exam_type + "' ><label  id='lbl_marks" + rowcount + "R" + colcount + "C'>" + formulamarks + "<sup style='vertical-align: super;'>  " + " " + requiredToPass + "</sup></label</td>";
                                        //  flagremark = "FAIL";
                                    }
                                    else {
                                        append = append + "<td style='text-align:center;' subject_id='" + data.Table[j].subject_id + "' tdtype='" + data.Table[j].exam_type + "' ><label  id='lbl_marks" + rowcount + "R" + colcount + "C'>" + formulamarks + "</label</td>";
                                    }

                                    if (data.Table[j].formula != "" && data.Table[j].formula != null && data.Table[j].formula != 'undefined') {

                                        if (!(data.Table[j].formula.includes("AA"))) {
                                            subtot = parseFloat(subtot) + parseFloat(added_grace_formulamarks);
                                        }

                                    }
                                   
                                    subpass = parseFloat(subpass) + parseFloat(data.Table[j].compare);
                                    subout = parseFloat(subout) + parseFloat(data.Table[j].outof);
                                    if (data.Table[j].formula != "" && data.Table[j].formula != null && data.Table[j].formula != 'undefined') {
                                        if (!(data.Table[j].formula.includes("AA"))) {
                                            total_marks = parseFloat(total_marks) + parseFloat(added_grace_formulamarks);
                                        }
                                    }
                                }
                                else {
                                    append = append + "<td style='text-align:center;' subject_id='" + data.Table[j].subject_id + "' tdtype='" + data.Table[j].exam_type + "' ><label  id='lbl_marks" + rowcount + "R" + colcount + "C'>" + formulamarks + "</label</td>";
                                }
                            }
                            else {
                                if (data.Table[j].exam_type != 'Grade') {
                                    if ((parseFloat(data.Table[j].marks) < parseFloat(data.Table[j].compare)) || data.Table[j].marks.toUpperCase() == "AA") {
                                        append = append + "<td style='text-align:center;color:red;' subject_id='" + data.Table[j].subject_id + "' tdtype='" + data.Table[j].exam_type + "' ><label  id='lbl_marks" + rowcount + "R" + colcount + "C'>" + data.Table[j].marks + "</label</td>";
                                       // flagremark = "FAIL";
                                    }
                                    else {
                                        append = append + "<td style='text-align:center;' subject_id='" + data.Table[j].subject_id + "' tdtype='" + data.Table[j].exam_type + "' ><label  id='lbl_marks" + rowcount + "R" + colcount + "C'>" + data.Table[j].marks + "</label</td>";
                                    }
                                    if (data.Table[j].marks.toUpperCase() != "AA" && data.Table[j].marks.toUpperCase() != "") {
                                        subtot = parseFloat(subtot) + parseFloat(data.Table[j].marks);
                                    }
                                    subpass = parseFloat(subpass) + parseFloat(data.Table[j].compare);
                                    subout = parseFloat(subout) + parseFloat(data.Table[j].outof);
                                    if (data.Table[j].marks.toUpperCase() != "AA" && data.Table[j].marks.toUpperCase() != "") {
                                        total_marks = parseFloat(total_marks) + parseFloat(data.Table[j].marks);
                                    }
                                }
                                else {
                                    append = append + "<td style='text-align:center;' subject_id='" + data.Table[j].subject_id + "' tdtype='" + data.Table[j].exam_type + "' ><label  id='lbl_marks" + rowcount + "R" + colcount + "C'>" + data.Table[j].marks + "</label</td>";
                                }
                            }
                            if (parseInt(j) == parseInt(data.Table.length - 1)) {
                                if (data.Table[j].exam_type != 'Grade') {
                                    subper = Math.round((parseFloat(subtot) * 100) / parseFloat(subout));
                                    if (parseFloat(subtot) < parseFloat(subpass)) {
                                        if (subtot - Math.floor(subtot) != 0 && parseFloat(subtot).toString().length > 4) {
                                            append = append + "<td style = 'color:red;'>" + subtot.toFixed(2) + "</td>";
                                        }
                                        else {
                                            append = append + "<td style = 'color:red;'>" + subtot + "</td>";
                                        }
                                       // flagremark = "FAIL";
                                    }
                                    else {
                                        if (subtot - Math.floor(subtot) != 0 && parseFloat(subtot).toString().length > 4) {
                                            append = append + "<td>" + subtot.toFixed(2) + "</td>";
                                        }
                                        else {
                                            append = append + "<td>" + subtot + "</td>";
                                        }
                                    }
                                    append = append + "<td style='text-align:center;'>" + get_grade(subper, selectedmed) + "</td>";
                                }
                                subtot = 0;
                                subpass = 0;
                                subout = 0;
                                colcount++;
                            }
                             
                           
                            if (data.Table[j].sub_type == "Marks") {                                
                                total_out = parseFloat(total_out) + parseFloat(data.Table[j].outof_marks);
                            }
                            if (parseInt(j) == parseInt(data.Table.length - 1)) {
                                perct = (parseFloat(total_marks) * 100) / parseFloat(outof);
                                append = append + "<td style='text-align:center;'>" + Math.round(total_marks) + "</td><td style='text-align:center;'>" + perct.toFixed(2) + "</td>";
                                if (parseFloat(perct) >= 35 && flagremark == "PASS") {
                                    append = append + " <td style = 'text-align:center;'>Pass</td>";
                                }
                                else {
                                    append = append + " <td style = 'text-align:center;'>Fail</td>";
                                }                                                               
                                append = append + "<td style='text-align:center;'>" + get_grade(perct, selectedmed) + "</td>";
                                total_marks = 0;
                                total_out = 0;
                            }
                            colcount++;
                        }
                        $("[id*='tblfill']").append(append);
                        $('[id*=tblfill]').fixedTableHeader();
                        $('[id*=tblfill]').arrowTable();
                        $("[id*=tblfill]").show();
                        $("[id*=btnget]").css("display", "block");
                        $("[id*=btnexcel]").css("display", "block");
                        $("[id*=btnresult]").css("display", "block");
                        $("[id*=divresult]").css("display", "block");
                        $("#scroller").scrollTop(0);
                        $("#scroller").scrollLeft(0);
                    }
                }
                else {
                    $.notify('Student are not assigned for this exam', { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                }
            },
            error: function (xhr, status, error) {
                $.notify(error, { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
            }
        });
    }
}

$("[id*='btnresult']").on("click", function () {
    var resultpattern = "";
    var remark = "";
    if ($("[id*=chkresult]")[0].checked == true) {
        resultpattern = "9";
    }
    else {
        resultpattern = "other";
    }
    if ($("[id*=chkremark]")[0].checked == true) {
        remark = "With";
    }
    else {
        remark = "Without";
    }
    chk_obtained_mrks();
    formula_check();
    promoted_check();
    localStorage.setItem("DATE", $('[id*=datepicker]').val());
    localStorage.setItem("resultpattern", resultpattern);
    localStorage.setItem("withremark", remark);
    window.open("result.aspx", "_blank");
});


$("[id*='btnexcel']").on("click", function (e) {
    e.preventDefault();
    exportTableToExcel('tblfill', file);
});

$("[id*='btnrefresh']").on("click", function () {
    localStorage.clear();
    location.reload();
});

function exportTableToExcel(tableID, filename) {
    var downloadLink;
    var dataType = 'application/vnd.ms-excel';
    var style = "<style>.tdstyle {background-color:black;color:white;}</style>";
    var tableSelect = document.getElementById(tableID);
    var tableraw = style + tableSelect.outerHTML;
    var tableHTML = tableraw.replace(/ /g, '%20');

    // Specify file name
    filename = filename ? filename + '.xls' : 'excel_data.xls';

    // Create download link element
    downloadLink = document.createElement("a");

    document.body.appendChild(downloadLink);

    if (navigator.msSaveOrOpenBlob) {
        var blob = new Blob(['\ufeff', tableHTML], {
            type: dataType
        });
        navigator.msSaveOrOpenBlob(blob, filename);
    } else {
        // Create a link to the file
        downloadLink.href = 'data:' + dataType + ', ' + tableHTML;

        // Setting the file name
        downloadLink.download = filename;

        //triggering the function
        downloadLink.click();
    }
}

function get_grade(total, medium) {
    var grade = "";
    if (medium == 2) {
        if (parseFloat(total) > 90 && parseFloat(total) <= 100) {
            grade = "A1";
        }
        else if (parseFloat(total) > 80 && parseFloat(total) < 91) {
            grade = "A2";
        }
        else if (parseFloat(total) > 70 && parseFloat(total) < 81) {
            grade = "B1";
        }
        else if (parseFloat(total) > 60 && parseFloat(total) < 71) {
            grade = "B2";
        }
        else if (parseFloat(total) > 50 && parseFloat(total) < 61) {
            grade = "C1";
        }
        else if (parseFloat(total) > 40 && parseFloat(total) < 51) {
            grade = "C2";
        }
        else if (parseFloat(total) > 33 && parseFloat(total) < 41) {
            grade = "D";
        }
        else if (parseFloat(total) > 20 && parseFloat(total) < 33) {
            grade = "E1";
        }
        else {
            grade = "E2";
        }
    }
    else {
        if (parseFloat(total) > 90 && parseFloat(total) <= 100) {
            grade = "अ-१";
        }
        else if (parseFloat(total) > 80 && parseFloat(total) < 91) {
            grade = "अ-२";
        }
        else if (parseFloat(total) > 70 && parseFloat(total) < 81) {
            grade = "ब-१";
        }
        else if (parseFloat(total) > 60 && parseFloat(total) < 71) {
            grade = "ब-२";
        }
        else if (parseFloat(total) > 50 && parseFloat(total) < 61) {
            grade = "क-१";
        }
        else if (parseFloat(total) > 40 && parseFloat(total) < 51) {
            grade = "क-२";
        }
        else if (parseFloat(total) > 33 && parseFloat(total) < 41) {
            grade = "ड";
        }
        else if (parseFloat(total) > 20 && parseFloat(total) < 33) {
            grade = "ई-१";
        }
        else {
            grade = "ई-२";
        }
    }
    return grade;
};

function chk_obtained_mrks() {
    if ($("#chk_obtn_mrks").is(":checked")) {
        localStorage.setItem("obtained_chk", true);
    } else {
        localStorage.setItem("obtained_chk", false);
    }
}

$("#chk_obtn_mrks").change(function () {
    chk_obtained_mrks();
});


function formula_check() {
    if ($("#chkformula").is(":checked")) {
        localStorage.setItem("formula", true);
    } else {
        localStorage.setItem("formula", false);
    }
}

$("#chkformula").change(function () {
    
    formula_check();
});

function promoted_check() {
    if ($("#promoted").is(":checked")) {
        localStorage.setItem("promoted", true);
    } else {
        localStorage.setItem("promoted", false);
    }
}

$("#promoted").change(function () {
    promoted_check();
});

function loaddate() {
    $('[id*=datepicker]').datepicker({
        minDate: 0,
        timepicker: true,
        format: 'dd- M - yyyy',
        defaultDate: new Date(2023, 5, 15)
    });

    
    $('[id*=datepicker]').change(function () {
        var selectedDate = $(this).val(); 
        localStorage.setItem("DATE", selectedDate); 
    });
}

$(document).ready(function () {
    loaddate();
    localStorage.setItem("DATE", $('[id*=datepicker]').val());
});










