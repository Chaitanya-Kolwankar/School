$(document).ready(function () {
    if (localStorage["result"] != null) {
        var data = JSON.parse(localStorage["result"]);
        var standard = localStorage["standard"];
        var exam = localStorage["Exam"];
        var medium = localStorage["Medium"];
        var checkremark = localStorage["withremark"];
        var tblcount = 0;
        var pattern = localStorage["resultpattern"];
        let mrks_obtn_flag;
        let formula_chk_flag;
        let promoted_flag;

        if (localStorage["promoted"] == "true") {
            promoted_flag = true;
        }
        else {
            promoted_flag = false;
        }

        if (localStorage["DATE"] != "") {
            var datetime = localStorage["DATE"];
        }
        else {
            var datetime = 0;
        }
        if (localStorage["obtained_chk"] == "true") {
            mrks_obtn_flag = true;
        }
        else {
            mrks_obtn_flag = false;
        }

        if (localStorage["formula"] == "true") {
            formula_chk_flag = true;
        }
        else {
            formula_chk_flag = false;
        }
        localStorage.setItem("", true);

        if (pattern == "9") {
            if (data.Table.length > 0) {
                var passing = 0;
                var outof = 0;
                var subpass = 0;
                var flagremark = "PASS";
                var append = "";
                var rowcount = 0;
                var total_marks = 0;
                var total_out = 0;
                var perct = 0;
                var grade = "";
                var subtot = 0;
                var submarks = "";
                var subout = 0;
                var subper = 0;
                var absent = false;
                var gradechar = 0;
                var graderow = "";
                var firstrow = true;
                var spanfor9 = 0;
                var flag = false;

                for (var j = 0; j < data.Table.length; j++) {

                    var colcount = 0;
                       if (j % data.Table1.length == 0) {
                                total_3_sub_count = 0;
                                total_remain_grace_mark = 15;
                            }
                    if (j > 0) {
                        if (data.Table[j - 1]["exam_type"] == "Grade") {
                            flag = true;
                        }
                        else {
                            flag = false;
                        }
                    }
                    if (j == 0) {
                        append = append + "<div style='width: 100%; position: relative;'><table style='border:0px;margin-top:100px' ><thead><tr>";
                        if (medium == 2) {
                            append = append + "<th>PROGRESS CARD</th>";
                        }
                        else {

                            append = append + "<th>प्रगती पत्रक</th>";
                        }


                        if (medium == 1) {
                            append = append + " </tr><tr><th>" + data.Table4[0].duration + "</th></tr><tr><th> विद्यार्थ्यांचे नाव : " + data.Table[j].stud_name + "</th ></tr ></thead ></table > ";
                        }

                        else {
                            append = append + " </tr><tr><th>" + data.Table4[0].duration + "</th></tr><tr><th> NAME : " + data.Table[j].stud_name + "</th ></tr ></thead ></table > ";
                        }


                        //tblcount++;



                        //if (medium == 1)
                        //{
                        //    rollno = data.Table[j].roll_no.toString()
                        //    grno = data.Table[j].stud_gr.toString()

                        //    append = append + "<table style='border:none;margin-top:5px' id='tbl" + tblcount + "t'><tbody><tr> <td style='text-align:center;width:16%;' >रजिस्टर क्रमांक</td><td  style='text-align:center;width:16%;'>इयत्ता</td><td style='text-align:center;width:18%;' >तुकडी</td><td style='text-align:center;width:18%;' >हजेरी क्रमांक</td><td style='text-align:center;width:32%;'  colspan='2'>परीक्षा</td></tr>";
                        //    append = append + "<tr> <td style='text-align:center;' >" + margr(grno) + "</td><td style='text-align:center;' >" + standard + "</td><td style='text-align:center;' >" + data.Table[j].division + "</td><td style='text-align:center;' >" + mardate(rollno) + "</td><td style='text-align:center;' colspan='2' >" + exam + " </td>";
                        //    append = append + "<tr> <td style='text-align:center;height:30px;' colspan='2'>विषय</td><td style='text-align:center;height:30px;' >MARKS OUTOF</td><td style='text-align:center;height:30px;'>MARKS OBTAINED</td><td style='text-align:center;width:15%;'>PERCENTAGE</td><td style='text-align:center;width:15%;' >REMARK</td></tr>";
                        //}

                        //else
                        {
                            append = append + "<table style='border:none;margin-top:5px' id='tbl" + tblcount + "t'><tbody><tr> <td style='text-align:center;width:16%;' >Gr. No.</td><td  style='text-align:center;width:16%;'>Standard</td><td style='text-align:center;width:18%;' >Division</td><td style='text-align:center;width:18%;' >Roll No.</td><td style='text-align:center;width:32%;'  colspan='2'>Exam</td></tr>";
                            append = append + "<tr> <td style='text-align:center;' >" + data.Table[j].stud_gr + "</td><td style='text-align:center;' >" + standard + "</td><td style='text-align:center;' >" + data.Table[j].division + "</td><td style='text-align:center;' >" + data.Table[j].roll_no + "</td><td style='text-align:center;' colspan='2' >" + exam + " </td>";
                            append = append + "<tr> <td style='text-align:center;height:30px;' colspan='2'>SUBJECT</td><td style='text-align:center;height:30px;' >MARKS OUTOF</td><td style='text-align:center;height:30px;'>MARKS OBTAINED</td><td style='text-align:center;width:15%;'>PERCENTAGE</td><td style='text-align:center;width:15%;' >REMARK</td></tr>";
                        }






                        for (var i = 0; i < data.Table1.length; i++) {

                            if (data.Table[j].subject_id == data.Table1[i].subject_id) {
                                if (i == 0) {


                                    append = append + "<tr><td style='height:30px;padding-left: 5px;' colspan='2'>" + data.Table1[i].subject_name + "</td>";

                                    if (data.Table[j]["exam_type"] == "Grade") {
                                        if (absent == true) {
                                            submarks = "AA";
                                        }
                                        else {
                                            submarks = subtot;
                                        }
                                        if (grace_flag == true) {
                                            append = append + "<td style='text-align:center;height:30px;' >" + subout + "</td><td  style='text-align:center;height:30px;' >" + submarks + " <sup style='vertical-align: super;'>  + " + requiredToPass + "</sup> </td>";
                                        }
                                        else {
                                            append = append + "<td style='text-align:center;height:30px;' >" + subout + "</td><td  style='text-align:center;height:30px;' >" + submarks + "</td>";
                                        }
                                        absent = false;
                                        if (firstrow == true) {
                                            append = append + "<td style='text-align:center;height:30px;'  ><label  id='lblper" + tblcount + "' ></label></td><td style='text-align:center;height:30px;'><label  id='lblremark" + tblcount + "'></label></td>";
                                            firstrow = false;
                                            spanfor9 = parseInt(data.Table1.length);
                                        }
                                        else if (parseInt(spanfor9) > 0) {
                                            append = append + "<td rowspan=" + spanfor9 + " colspan=3></td>";
                                            spanfor9 = 0;
                                        }
                                    }
                                    else {
                                        if (data.Table[j]["compare"] != "" && data.Table[j]["compare"] != null && data.Table[j]["compare"] != 'undefined') {
                                            subpass = subpass + parseInt(data.Table[j]["compare"]);
                                            passing = passing + parseInt(data.Table[j]["compare"]);
                                        }
                                        if (data.Table[j]["outof"] != "" && data.Table[j]["outof"] != null && data.Table[j]["outof"] != 'undefined') {
                                            subout = subout + parseInt(data.Table[j]["outof"]);
                                            total_out = total_out + parseInt(data.Table[j]["outof"]);
                                        }

                                        if (formula_chk_flag == true) {
                                            if (data.Table[j]["formula"] != "" && data.Table[j]["formula"] != null && data.Table[j]["formula"] != 'undefined' && data.Table[j]["marks"].toUpperCase() != "AA" && !(data.Table[j]["formula"].includes("AA")) && !(data.Table[j]["formula"].includes("aa"))) {
                                                var myArray = data.Table[j].formula.split("/");
                                                formulamarks = Math.round(eval(eval(myArray[0]) / myArray[1]));
                                                var added_grace_formulamarks
                                                added_grace_formulamarks = formulamarks;
                                                var grace_flag = false;
                                                if (formulamarks < data.Table[j].compare) {
                                                    var requiredToPass = data.Table[j].compare - formulamarks;
                                                    if (total_3_sub_count == undefined) {
                                                        total_3_sub_count = 0
                                                    }

                                                    var total_grace = 15;
                                                    var total_remain_grace_mark;
                                                    var check_gracemark_used = "";
                                                    if (total_remain_grace_mark == undefined) {
                                                        total_remain_grace_mark = 15;
                                                    }
                                                     grace_flag = false;
                                                    if (requiredToPass <= 10 && total_3_sub_count < 3 && requiredToPass <= total_remain_grace_mark) {
                                                        added_grace_formulamarks = formulamarks;
                                                        added_grace_formulamarks += requiredToPass
                                                        total_3_sub_count += 1;
                                                        total_remain_grace_mark = total_grace - requiredToPass;
                                                        grace_flag = true;

                                                    }
                                                    else {

                                                    }
                                                }
                                                subtot = subtot + formulamarks;
                                                total_marks = added_grace_formulamarks;
                                                if (parseInt(added_grace_formulamarks) < parseInt(data.Table[j]["compare"])) {
                                                    flagremark = "FAIL";
                                                }
                                            }

                                            else if (data.Table[j]["formula"] != "" && data.Table[j]["formula"] != null && data.Table[j]["formula"] != 'undefined' && data.Table[j]["formula"].toUpperCase().includes("AA")) {
                                                flagremark = "FAIL";
                                            }
                                        }

                                        else {


                                            if (data.Table[j]["marks"] != "" && data.Table[j]["marks"] != null && data.Table[j]["marks"] != 'undefined' && data.Table[j]["marks"].toUpperCase() != "AA") {
                                                subtot = subtot + parseInt(data.Table[j]["marks"]);
                                                total_marks = total_marks + parseInt(data.Table[j]["marks"]);
                                                if (parseInt(data.Table[j]["marks"]) < parseInt(data.Table[j]["compare"])) {
                                                    flagremark = "FAIL";
                                                }
                                            } //sakshi commented this and below code added for formula
                                        }


                                        if (data.Table[j]["marks"] == "" && data.Table[j]["marks"] == null && data.Table[j]["marks"] == 'undefined' && data.Table[j]["marks"].toUpperCase() == "AA") {
                                            flagremark = "FAIL";
                                        }  // sakshi commented this and below code added for formula

                                        if (subtot == "" && subtot == null && subtot == 'undefined' && data.Table[j]["marks"].toUpperCase() == "AA") {
                                            flagremark = "FAIL";
                                        }
                                        if (data.Table[j]["marks"].toUpperCase() == "AA") {
                                            absent = true;
                                        }

                                    }
                                }
                                else {
                                    if (data.Table[i].subject_id != data.Table[i - 1].subject_id) {
                                        append = append + "<tr><td style='height:30px;padding-left: 5px;' colspan='2'>" + data.Table1[i].subject_name + "</td>";

                                        if (data.Table[j]["exam_type"] == "Grade") {
                                            if (absent == true) {
                                                submarks = "AA";
                                            }
                                            else {
                                                submarks = subtot;
                                            }

                                            if (grace_flag == true)
                                            {
                                                append = append + "<td style='text-align:center;height:30px;' >" + subout + "</td><td  style='text-align:center;height:30px;' >" + submarks + " <sup style='vertical-align: super;'> + " + requiredToPass + "</sup></td>";
                                            }
                                            else {
                                                append = append + "<td style='text-align:center;height:30px;' >" + subout + "</td><td  style='text-align:center;height:30px;' >" + submarks + "</td>";
                                            }
                                            absent = false;
                                            if (firstrow == true) {
                                                append = append + "<td style='text-align:center;height:30px;'  ><label  id='lblper" + tblcount + "' ></label></td><td style='text-align:center;height:30px;'><label  id='lblremark" + tblcount + "'></label></td>";
                                                firstrow = false;
                                                spanfor9 = parseInt(data.Table1.length);
                                            }
                                            else if (parseInt(spanfor9) > 0) {
                                                append = append + "<td rowspan=" + spanfor9 + " colspan=3></td>";
                                                spanfor9 = 0;
                                            }
                                            // append = append + fill_grade(gradechar, medium);
                                        }
                                        else {
                                            if (data.Table[j]["compare"] != "" && data.Table[j]["compare"] != null && data.Table[j]["compare"] != 'undefined') {
                                                subpass = subpass + parseInt(data.Table[j]["compare"]);
                                                passing = passing + parseInt(data.Table[j]["compare"]);
                                            }
                                            if (data.Table[j]["outof"] != "" && data.Table[j]["outof"] != null && data.Table[j]["outof"] != 'undefined') {
                                                subout = subout + parseInt(data.Table[j]["outof"]);
                                                total_out = total_out + parseInt(data.Table[j]["outof"]);
                                            }
                                            if (data.Table[j]["marks"] != "" && data.Table[j]["marks"] != null && data.Table[j]["marks"] != 'undefined' && data.Table[j]["marks"].toUpperCase() != "AA") {
                                                subtot = subtot + parseInt(data.Table[j]["marks"]);
                                                total_marks = total_marks + parseInt(data.Table[j]["marks"]);
                                                if (parseInt(data.Table[j]["marks"]) < parseInt(data.Table[j]["compare"])) {
                                                    flagremark = "FAIL";
                                                }
                                            }
                                            else if (data.Table[j]["marks"] == "" && data.Table[j]["marks"] == null && data.Table[j]["marks"] == 'undefined' && data.Table[j]["marks"].toUpperCase() == "AA") {
                                                flagremark = "FAIL";
                                            }
                                            if (data.Table[j]["marks"].toUpperCase() == "AA") {
                                                absent = true;
                                            }

                                        }
                                    }
                                }
                            }
                        }
                    }
                    else {
                        if (j < data.Table.length - 1) {
                            if (data.Table[j].stud_id == data.Table[j - 1].stud_id) {

                                if (data.Table[j].subject_id == data.Table[j - 1].subject_id) {
                                    if (data.Table[j]["compare"] != "" && data.Table[j]["compare"] != null && data.Table[j]["compare"] != 'undefined') {
                                        subpass = subpass + parseInt(data.Table[j]["compare"]);
                                        passing = passing + parseInt(data.Table[j]["compare"]);
                                    }
                                    if (data.Table[j]["outof"] != "" && data.Table[j]["outof"] != null && data.Table[j]["outof"] != 'undefined') {
                                        subout = subout + parseInt(data.Table[j]["outof"]);
                                        total_out = total_out + parseInt(data.Table[j]["outof"]);
                                    }
                                    if (data.Table[j]["marks"] != "" && data.Table[j]["marks"] != null && data.Table[j]["marks"] != 'undefined' && data.Table[j]["marks"].toUpperCase() != "AA") {
                                        subtot = subtot + parseInt(data.Table[j]["marks"]);
                                        total_marks = total_marks + parseInt(data.Table[j]["marks"]);
                                        if (parseInt(data.Table[j]["marks"]) < parseInt(data.Table[j]["compare"])) {
                                            flagremark = "FAIL";
                                        }
                                    }
                                    else if (data.Table[j]["marks"] == "" && data.Table[j]["marks"] == null && data.Table[j]["marks"] == 'undefined' && data.Table[j]["marks"].toUpperCase() == "AA") {
                                        flagremark = "FAIL";
                                    }
                                    if (data.Table[j]["marks"].toUpperCase() == "AA") {
                                        absent = true;
                                    }

                                }
                                else {


                                    for (var i = 0; i < data.Table1.length; i++) {
                                        if (data.Table[j].subject_id == data.Table1[i].subject_id) {
                                            if (i == 0) {


                                                append = append + "</tr><tr><td style='height:30px;padding-left: 5px;' colspan='2'>" + data.Table1[i].subject_name + "</td>";

                                                if (data.Table[j]["exam_type"] == "Grade") {
                                                    append = append + "<td style='text-align:center;height:30px;' >- -</td><td style='text-align:center;height:30px;' >" + data.Table[j].marks + "</td>";
                                                    if (firstrow == true) {
                                                        append = append + "<td style='text-align:center;height:30px;'  ><label  id='lblper" + tblcount + "' ></label></td><td style='text-align:center;height:30px;'><label  id='lblremark" + tblcount + "'></label></td>";
                                                        firstrow = false;
                                                        spanfor9 = parseInt(data.Table1.length);
                                                    }
                                                    else if (parseInt(spanfor9) > 0) {
                                                        append = append + "<td rowspan=" + spanfor9 + " colspan=3></td>";
                                                        spanfor9 = 0;
                                                    }
                                                    // append = append + fill_grade(gradechar, medium);
                                                }
                                                else {
                                                    if (data.Table[j]["compare"] != "" && data.Table[j]["compare"] != null && data.Table[j]["compare"] != 'undefined') {
                                                        subpass = subpass + parseInt(data.Table[j]["compare"]);
                                                        passing = passing + parseInt(data.Table[j]["compare"]);
                                                    }
                                                    if (data.Table[j]["outof"] != "" && data.Table[j]["outof"] != null && data.Table[j]["outof"] != 'undefined') {
                                                        subout = subout + parseInt(data.Table[j]["outof"]);
                                                        total_out = total_out + parseInt(data.Table[j]["outof"]);
                                                    }
                                                    if (data.Table[j]["marks"] != "" && data.Table[j]["marks"] != null && data.Table[j]["marks"] != 'undefined' && data.Table[j]["marks"].toUpperCase() != "AA") {
                                                        subtot = subtot + parseInt(data.Table[j]["marks"]);
                                                        total_marks = total_marks + parseInt(data.Table[j]["marks"]);
                                                        if (parseInt(data.Table[j]["marks"]) < parseInt(data.Table[j]["compare"])) {
                                                            flagremark = "FAIL";
                                                        }
                                                    }
                                                    else if (data.Table[j]["marks"] == "" && data.Table[j]["marks"] == null && data.Table[j]["marks"] == 'undefined' && data.Table[j]["marks"].toUpperCase() == "AA") {
                                                        flagremark = "FAIL";
                                                    }
                                                    if (data.Table[j]["marks"].toUpperCase() == "AA") {
                                                        absent = true;
                                                    }
                                                }
                                            }
                                            else {
                                                if (data.Table[i].subject_id != data.Table[i - 1].subject_id) {
                                                    append = append + "</tr><tr><td style='height:30px;padding-left: 5px;' colspan='2'>" + data.Table1[i].subject_name + "</td>";

                                                    if (data.Table[j]["exam_type"] == "Grade") {
                                                        append = append + "<td style='text-align:center;height:30px;' >- -</td><td style='text-align:center;height:30px;' >" + data.Table[j].marks + "</td>";
                                                        if (firstrow == true) {
                                                            append = append + "<td style='text-align:center;height:30px;'  ><label  id='lblper" + tblcount + "' ></label></td><td style='text-align:center;height:30px;'><label  id='lblremark" + tblcount + "'></label></td>";
                                                            firstrow = false;
                                                            spanfor9 = parseInt(data.Table1.length);
                                                        }
                                                        else if (parseInt(spanfor9) > 0) {
                                                            append = append + "<td rowspan=" + spanfor9 + " colspan=3></td>";
                                                            spanfor9 = 0;
                                                        }
                                                        //  append = append + fill_grade(gradechar, medium);
                                                    }
                                                    else {
                                                        if (data.Table[j]["compare"] != "" && data.Table[j]["compare"] != null && data.Table[j]["compare"] != 'undefined') {
                                                            subpass = subpass + parseInt(data.Table[j]["compare"]);
                                                            passing = passing + parseInt(data.Table[j]["compare"]);
                                                        }
                                                        if (data.Table[j]["outof"] != "" && data.Table[j]["outof"] != null && data.Table[j]["outof"] != 'undefined') {
                                                            subout = subout + parseInt(data.Table[j]["outof"]);
                                                            total_out = total_out + parseInt(data.Table[j]["outof"]);
                                                        }
                                                        if (formula_chk_flag == false) {
                                                            if (data.Table[j]["marks"] != "" && data.Table[j]["marks"] != null && data.Table[j]["marks"] != 'undefined' && data.Table[j]["marks"].toUpperCase() != "AA") {
                                                                subtot = subtot + parseInt(data.Table[j]["marks"]);
                                                                total_marks = total_marks + parseInt(data.Table[j]["marks"]);
                                                                if (parseInt(data.Table[j]["marks"]) < parseInt(data.Table[j]["compare"])) {
                                                                    flagremark = "FAIL";
                                                                }
                                                            } //commented this and below code added for formula
                                                        }
                                                        else if (formula_chk_flag == true) {
                                                            if (data.Table[j]["formula"] != "" && data.Table[j]["formula"] != null && data.Table[j]["formula"] != 'undefined' && data.Table[j]["marks"].toUpperCase() != "AA" && !(data.Table[j]["formula"].includes("AA")) && !(data.Table[j]["formula"].includes("aa"))) {
                                                                var myArray = data.Table[j].formula.split("/");
                                                                formulamarks = Math.round(eval(eval(myArray[0]) / myArray[1]));

                                                                var added_grace_formulamarks
                                                                added_grace_formulamarks = formulamarks;
                                                                var grace_flag = false;
                                                                if (formulamarks < data.Table[j].compare) {
                                                                    var requiredToPass = data.Table[j].compare - formulamarks;
                                                                    if (total_3_sub_count == undefined) {
                                                                        total_3_sub_count = 0
                                                                    }

                                                                    var total_grace = 15;
                                                                    var total_remain_grace_mark;
                                                                    var check_gracemark_used = "";
                                                                    if (total_remain_grace_mark == undefined) {
                                                                        total_remain_grace_mark = 15;
                                                                    }
                                                                     grace_flag = false;
                                                                    if (requiredToPass <= 10 && total_3_sub_count < 3 && requiredToPass <= total_remain_grace_mark) {
                                                                        added_grace_formulamarks = formulamarks;
                                                                        added_grace_formulamarks += requiredToPass
                                                                        total_3_sub_count += 1;
                                                                        total_remain_grace_mark = total_grace - requiredToPass;
                                                                        grace_flag = true;

                                                                    }
                                                                    else {

                                                                    }
                                                                }
                                                                subtot = subtot + formulamarks;
                                                                total_marks += added_grace_formulamarks;
                                                                if (parseInt(added_grace_formulamarks) < parseInt(data.Table[j]["compare"])) {
                                                                    flagremark = "FAIL";
                                                                }
                                                            }
                                                        }

                                                        else if (data.Table[j]["marks"] == "" && data.Table[j]["marks"] == null && data.Table[j]["marks"] == 'undefined' && data.Table[j]["marks"].toUpperCase() == "AA") {
                                                            flagremark = "FAIL";
                                                        } // commented this and below code added for formula

                                                        else if (subtot == "" && subtot == null && subtot == 'undefined' && data.Table[j]["marks"].toUpperCase() == "AA") {
                                                            flagremark = "FAIL";
                                                        }


                                                        //if (formula_chk_flag == true) {
                                                        //    if ((data.Table[j]["formula"].includes("AA")) || (data.Table[j]["formula"].includes("AA"))) {
                                                        //        absent = true;
                                                        //    }
                                                        //}
                                                        else if (data.Table[j]["marks"].toUpperCase() == "AA" || (data.Table[j]["formula"].includes("AA"))) {
                                                            absent = true;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else {
                                            if (flag == false) {
                                                subper = Math.round((parseFloat(subtot) * 100) / parseFloat(subout));
                                                //if (absent == true) {
                                                //    submarks = "AA";
                                                //}
                                                //else
                                                {
                                                    submarks = subtot;
                                                }

                                                if (grace_flag == true) {
                                                    append = append + "<td style='text-align:center;height:30px;' >" + subout + "</td><td  style='text-align:center;height:30px;' >" + submarks + " <sup style='vertical-align: super;'>  +  " + requiredToPass + "</sup> </td>";
                                                }
                                                else {
                                                    append = append + "<td style='text-align:center;height:30px;' >" + subout + "</td><td  style='text-align:center;height:30px;' >" + submarks + "</td>";
                                                }
                                                absent = false;
                                                //if (firstrow == false) {
                                                //    append = append + "<td colspan=" + parseInt(data.Table1.length)-1 + "></td>"
                                                //}
                                                if (firstrow == true) {
                                                    append = append + "<td style='text-align:center;height:30px;'  ><label  id='lblper" + tblcount + "' ></label></td><td style='text-align:center;height:30px;'><label  id='lblremark" + tblcount + "'></label></td>";
                                                    firstrow = false;
                                                    if (promoted_flag == true) {
                                                        var secondrow = true;
                                                    }

                                                    spanfor9 = parseInt(data.Table1.length);
                                                }
                                                else if (secondrow == true) {
                                                    append = append + " <td style='text-align:center;width:15%;'>Grade/Rank</td><td style='text-align:center;width:15%;' ></td>";

                                                    secondrow = false;
                                                    if (promoted_flag == true) {
                                                        var thirdrow = true;
                                                    }

                                                }
                                                else if (thirdrow == true) {
                                                 //   if (parseFloat(perct) > 35) {
                                                        //append = append + "<td style='text-align:center;width:15%;'  colspan=3> Passed and Promoted to " + Promoted_standard(standard) + "  </td>";
                                                        append = append + "<td style='text-align:center;width:15%;'  colspan=3><label id='prmotd_remark" + tblcount + "'></label></td>";

                                                        thirdrow = false;
                                                  //  }
                                                    //else {
                                                    //    append = append + "<td style='text-align:center;width:15%;'  colspan=3>UnSuccessful</td>";
                                                    //    thirdrow = false;
                                                    //}
                                                }

                                                else if (parseInt(spanfor9) > 0) {
                                                    append = append + "<td rowspan=" + spanfor9 + " colspan=3></td>";
                                                    spanfor9 = 0;
                                                }
                                                //  append = append + fill_grade(gradechar, medium);
                                                //  gradechar++;
                                                subtot = 0;
                                                subpass = 0;
                                                subout = 0;
                                                flag = true;
                                            }
                                        }
                                    }
                                }
                            }
                            else {
                                if (data.Table[j - 1].exam_type != "Grade") {
                                    subper = Math.round((parseFloat(subtot) * 100) / parseFloat(subout));
                                    if (absent == true) {
                                        submarks = "AA";
                                    }
                                    else {
                                        submarks = subtot;
                                    }

                                    if (grace_flag == true) {
                                        append = append + "<td style='text-align:center;height:30px;' >" + subout + "</td><td  style='text-align:center;height:30px;' >" + submarks + " <sup style='vertical-align: super;'>   +  " + requiredToPass + "</sup> </td>";
                                    }
                                    else {
                                        append = append + "<td style='text-align:center;height:30px;' >" + subout + "</td><td  style='text-align:center;height:30px;' >" + submarks + "</td>";
                                    }
                                    absent = false;
                                    if (firstrow == true) {
                                        append = append + "<td style='text-align:center;height:30px;'  ><label  id='lblper" + tblcount + "' ></label></td><td style='text-align:center;height:30px;'><label  id='lblremark" + tblcount + "'></label></td>";
                                        firstrow = false;
                                        spanfor9 = parseInt(data.Table1.length);
                                    }
                                    else if (parseInt(spanfor9) > 0) {
                                        append = append + "<td rowspan=" + spanfor9 + " colspan=3></td>";
                                        spanfor9 = 0;
                                    }
                                    // append = append + fill_grade(gradechar, medium);
                                    // gradechar++;

                                }
                                //for (var h = gradechar; h < 9; h++) {
                                //    append = append + "<tr><td style='text-align:center;height:30px;' colspan=2 ></td><td style='text-align:center;height:30px;'  ></td><td style='text-align:center;height:30px;'  ></td>" + fill_grade(gradechar, medium) + "</tr>";
                                //    gradechar++;
                                //}
                                //gradechar = 0;
                                subtot = 0;
                                subpass = 0;
                                subout = 0;
                                flag = true;

                                //if (medium == 1) {
                                //    append = append + "<tr><td style='text-align:center;width:18%;' colspan='2'>एकूण</td>";
                                //}

                                //else
                                {
                                    append = append + "<tr><td style='text-align:center;width:18%;' colspan='2'>Total</td>";
                                }



                                perct = (parseFloat(total_marks) * 100) / parseFloat(total_out);
                                append = append + "<td style='text-align:center;height:30px;' >" + total_out + "</td><td style='text-align:center;height:30px;'  >" + total_marks + "</td><td style='text-align:center;width:18%;border: none;' colspan='3'></td>"
                                // $("[id*=lblper]").val() = perct;
                                total_marks = 0;
                                total_out = 0;
                                passing = 0;



                                gradechar = 0;
                                firstrow = true;

                                append = append + "</tbody></table></div>";

                                if (datetime == "0") {

                                }
                                else {
                                    append = append + "<div style='width: 100%; position: relative; text-align:left;margin-top:10px;margin-left:40%'> School Will Reopen On &nbsp  " + datetime + "</div>";
                                }

                              /*  append = append + "<div style='display:flex'><div style='width: 40%; position: relative; text-align:left;margin-top:35px;margin-left:15%'><span style='border-top:1px solid black'>CLASS TEACHER SIGN</span></div><div style='width: 60%; position: relative; text-align:right;margin-top:35px;margin-right:15%'><span style='border-top:1px solid black'> HEAD MISTERESS SIGN </span></div></div>";*/
                               
                                $("[id*='div_tbl']").append(append);
                                $("[id*=lblper" + tblcount + "]")[0].innerText = perct.toFixed(2);
                                //  $("[id*=lblgrade" + tblcount + "]")[0].innerText = gradeonly(perct, medium);
                                if (checkremark == "With") {
                                    if (parseFloat(perct) >= 35 && flagremark == "PASS") {
                                        $("[id*=lblremark" + tblcount + "]")[0].innerText = "Pass";
                                        if (promoted_flag == true) {
                                            $("[id*=prmotd_remark" + tblcount + "]")[0].innerText = " Passed and Promoted to  " + Promoted_standard(standard) + "";
                                        }
                                    }
                                    else {
                                        $("[id*=lblremark" + tblcount + "]")[0].innerText = "Fail";
                                        if (promoted_flag == true) {
                                            $("[id*=prmotd_remark" + tblcount + "]")[0].innerText = "UnSuccessful";
                                        }
                                    }
                                }
                                flagremark = "PASS";
                                append = "<div style='width: 100%; position: relative; page-break-before: always;'><table style='border:0px;margin-top:150px'><thead><tr>";
                                if (medium == 2) {
                                    append = append + "<th>PROGRESS CARD</th>";
                                }
                                else {

                                    append = append + "<th>प्रगती पत्रक</th>";
                                }


                                if (medium == 1) {
                                    append = append + "</tr><tr><th>" + data.Table4[0].duration + "</th></tr><tr><th>विद्यार्थ्यांचे नाव : " + data.Table[j].stud_name + "</th></tr ></thead ></table > ";
                                }
                                else {
                                    append = append + "</tr><tr><th>" + data.Table4[0].duration + "</th></tr><tr><th>NAME : " + data.Table[j].stud_name + "</th></tr ></thead ></table > ";
                                }


                                tblcount++;



                                //if (medium == 1)
                                //{
                                //    rollno = data.Table[j].roll_no.toString()
                                //    grno = data.Table[j].stud_gr.toString()
                                //    append = append + "<table style='border:none;margin-top:5px' id='tbl" + tblcount + "t'><tbody><tr> <td style='text-align:center;width:16%;' >रजिस्टर क्रमांक</td><td  style='text-align:center;width:16%;'>इयत्ता</td><td style='text-align:center;width:18%;' >तुकडी</td><td style='text-align:center;width:18%;' >हजेरी क्रमांक</td><td style='text-align:center;width:32%;' colspan='2' >परीक्षा</td></tr>";
                                //    append = append + "<tr> <td style='text-align:center;' >" + margr(grno) + "</td><td style='text-align:center;' >" + standard + "</td><td style='text-align:center;' >" + data.Table[j].division + "</td><td style='text-align:center;' >" + mardate(rollno) + "</td><td style='text-align:center;'  colspan='2'>" + exam + " </td>";
                                //    append = append + "<tr> <td style='text-align:center;height:30px;' colspan='2'>विषय</td><td style='text-align:center;height:30px;' >MARKS OUTOF</td><td style='text-align:center;height:30px;'>MARKS OBTAINED</td><td style='text-align:center;width:15%;'>PERCENTAGE</td><td style='text-align:center;width:15%;' >REMARK</td></tr>";
                                //}

                                //else
                                {
                                    append = append + "<table style='border:none;margin-top:5px' id='tbl" + tblcount + "t'><tbody><tr> <td style='text-align:center;width:16%;' >Gr. No.</td><td  style='text-align:center;width:16%;'>Standard</td><td style='text-align:center;width:18%;' >Division</td><td style='text-align:center;width:18%;' >Roll No.</td><td style='text-align:center;width:32%;' colspan='2' >Exam</td></tr>";
                                    append = append + "<tr> <td style='text-align:center;' >" + data.Table[j].stud_gr + "</td><td style='text-align:center;' >" + standard + "</td><td style='text-align:center;' >" + data.Table[j].division + "</td><td style='text-align:center;' >" + data.Table[j].roll_no + "</td><td style='text-align:center;'  colspan='2'>" + exam + " </td>";
                                    append = append + "<tr> <td style='text-align:center;height:30px;' colspan='2'>SUBJECT</td><td style='text-align:center;height:30px;' >MARKS OUTOF</td><td style='text-align:center;height:30px;'>MARKS OBTAINED</td><td style='text-align:center;width:15%;'>PERCENTAGE</td><td style='text-align:center;width:15%;' >REMARK</td></tr>";
                                }



                                var flag_new = false;
                                for (k = 0; k < data.Table1.length; k++) {
                                    if (data.Table[j].subject_id == data.Table1[k].subject_id) {
                                        if (flag_new == false) {
                                            append = append + "<td style='height:30px;padding-left: 5px;' colspan='2'>" + data.Table1[k].subject_name + "</td>";
                                            flag_new = true;
                                            if (data.Table[j]["compare"] != "" && data.Table[j]["compare"] != null && data.Table[j]["compare"] != 'undefined') {
                                                subpass = subpass + parseInt(data.Table[j]["compare"]);
                                                passing = passing + parseInt(data.Table[j]["compare"]);
                                            }
                                            if (data.Table[j]["outof"] != "" && data.Table[j]["outof"] != null && data.Table[j]["outof"] != 'undefined') {
                                                subout = subout + parseInt(data.Table[j]["outof"]);
                                                total_out = total_out + parseInt(data.Table[j]["outof"]);
                                            }

                                            if (formula_chk_flag == true) {
                                                if (data.Table[j]["formula"] != "" && data.Table[j]["formula"] != null && data.Table[j]["formula"] != 'undefined' && data.Table[j]["marks"].toUpperCase() != "AA" && !(data.Table[j]["formula"].includes("AA")) && !(data.Table[j]["formula"].includes("aa"))) {
                                                    var myArray = data.Table[j].formula.split("/");
                                                    formulamarks = Math.round(eval(eval(myArray[0]) / myArray[1]));
                                                    var added_grace_formulamarks
                                                    added_grace_formulamarks = formulamarks;
                                                    if (formulamarks < data.Table[j].compare) {
                                                        var requiredToPass = data.Table[j].compare - formulamarks;
                                                        if (total_3_sub_count == undefined) {
                                                            total_3_sub_count = 0
                                                        }

                                                        var total_grace = 15;
                                                        var total_remain_grace_mark;
                                                        var check_gracemark_used = "";
                                                        if (total_remain_grace_mark == undefined) {
                                                            total_remain_grace_mark = 15;
                                                        }
                                                        var grace_flag = false;
                                                        if (requiredToPass <= 10 && total_3_sub_count < 3 && requiredToPass <= total_remain_grace_mark) {
                                                            added_grace_formulamarks = formulamarks;
                                                            added_grace_formulamarks += requiredToPass
                                                            total_3_sub_count += 1;
                                                            total_remain_grace_mark = total_grace - requiredToPass;
                                                            grace_flag = true;

                                                        }
                                                        else {

                                                        }
                                                    }
                                                    subtot = subtot + formulamarks
                                                    total_marks = added_grace_formulamarks
                                                    if (parseInt(added_grace_formulamarks) < parseInt(data.Table[j]["compare"])) {
                                                        flagremark = "FAIL";
                                                    }
                                                }
                                            }
                                            else if (data.Table[j]["marks"] != "" && data.Table[j]["marks"] != null && data.Table[j]["marks"] != 'undefined' && data.Table[j]["marks"].toUpperCase() != "AA") {
                                                subtot = subtot + parseInt(data.Table[j]["marks"]);
                                                total_marks = total_marks + parseInt(data.Table[j]["marks"]);
                                                if (parseInt(data.Table[j]["marks"]) < parseInt(data.Table[j]["compare"])) {
                                                    flagremark = "FAIL";
                                                }
                                            }

                                            else if (data.Table[j]["marks"] == "" && data.Table[j]["marks"] == null && data.Table[j]["marks"] == 'undefined' && data.Table[j]["marks"].toUpperCase() == "AA") {
                                                flagremark = "FAIL";
                                            }
                                            if (data.Table[j]["marks"].toUpperCase() == "AA") {
                                                absent = true;
                                            }

                                        }
                                    }
                                }

                            }
                        }
                        else {
                            flag = false;
                            if (data.Table[j].subject_id == data.Table[j - 1].subject_id) {
                                if (data.Table[j]["compare"] != "" && data.Table[j]["compare"] != null && data.Table[j]["compare"] != 'undefined') {
                                    subpass = subpass + parseInt(data.Table[j]["compare"]);
                                    passing = passing + parseInt(data.Table[j]["compare"]);
                                }
                                if (data.Table[j]["outof"] != "" && data.Table[j]["outof"] != null && data.Table[j]["outof"] != 'undefined') {
                                    subout = subout + parseInt(data.Table[j]["outof"]);
                                    total_out = total_out + parseInt(data.Table[j]["outof"]);
                                }
                                if (data.Table[j]["marks"] != "" && data.Table[j]["marks"] != null && data.Table[j]["marks"] != 'undefined' && data.Table[j]["marks"].toUpperCase() != "AA") {
                                    subtot = subtot + parseInt(data.Table[j]["marks"]);
                                    total_marks = total_marks + parseInt(data.Table[j]["marks"]);
                                    if (parseInt(data.Table[j]["marks"]) < parseInt(data.Table[j]["compare"])) {
                                        flagremark = "FAIL";
                                    }
                                }
                                else if (data.Table[j]["marks"] == "" && data.Table[j]["marks"] == null && data.Table[j]["marks"] == 'undefined' && data.Table[j]["marks"].toUpperCase() == "AA") {
                                    flagremark = "FAIL";
                                }
                                if (data.Table[j]["marks"].toUpperCase() == "AA") {
                                    absent = true;
                                }

                            }
                            else {


                                for (var i = 0; i < data.Table1.length; i++) {
                                    if (data.Table[j].subject_id == data.Table1[i].subject_id) {
                                        if (i == 0) {


                                            append = append + "</tr><tr><td style='height:30px;padding-left: 5px;' colspan='2'>" + data.Table1[i].subject_name + "</td>";

                                            if (data.Table[j]["exam_type"] == "Grade") {
                                                append = append + "<td style='text-align:center;height:30px;' >- -</td><td style='text-align:center;height:30px;' >" + data.Table[j].marks + "</td>";
                                                if (firstrow == true) {
                                                    append = append + "<td style='text-align:center;height:30px;'  ><label  id='lblper" + tblcount + "' ></label></td><td style='text-align:center;height:30px;'><label  id='lblremark" + tblcount + "'></label></td>";
                                                    firstrow = false;
                                                    spanfor9 = parseInt(data.Table1.length);
                                                }
                                                else if (parseInt(spanfor9) > 0) {
                                                    append = append + "<td rowspan=" + spanfor9 + " colspan=3></td>";
                                                    spanfor9 = 0;
                                                }
                                                // append = append + fill_grade(gradechar, medium);
                                            }
                                            else {
                                                if (data.Table[j]["compare"] != "" && data.Table[j]["compare"] != null && data.Table[j]["compare"] != 'undefined') {
                                                    subpass = subpass + parseInt(data.Table[j]["compare"]);
                                                    passing = passing + parseInt(data.Table[j]["compare"]);
                                                }
                                                if (data.Table[j]["outof"] != "" && data.Table[j]["outof"] != null && data.Table[j]["outof"] != 'undefined') {
                                                    subout = subout + parseInt(data.Table[j]["outof"]);
                                                    total_out = total_out + parseInt(data.Table[j]["outof"]);
                                                }
                                                if (data.Table[j]["marks"] != "" && data.Table[j]["marks"] != null && data.Table[j]["marks"] != 'undefined' && data.Table[j]["marks"].toUpperCase() != "AA") {
                                                    subtot = subtot + parseInt(data.Table[j]["marks"]);
                                                    total_marks = total_marks + parseInt(data.Table[j]["marks"]);
                                                    if (parseInt(data.Table[j]["marks"]) < parseInt(data.Table[j]["compare"])) {
                                                        flagremark = "FAIL";
                                                    }
                                                }
                                                else if (data.Table[j]["marks"] == "" && data.Table[j]["marks"] == null && data.Table[j]["marks"] == 'undefined' && data.Table[j]["marks"].toUpperCase() == "AA") {
                                                    flagremark = "FAIL";
                                                }
                                                if (data.Table[j]["marks"].toUpperCase() == "AA") {
                                                    absent = true;
                                                }
                                            }
                                        }
                                        else {
                                            if (data.Table[i].subject_id != data.Table[i - 1].subject_id) {
                                                append = append + "</tr><tr><td style='height:30px;padding-left: 5px;' colspan='2'>" + data.Table1[i].subject_name + "</td>";

                                                if (data.Table[j]["exam_type"] == "Grade") {
                                                    append = append + "<td style='text-align:center;height:30px;' >- -</td><td style='text-align:center;height:30px;' >" + data.Table[j].marks + "</td>";
                                                    if (firstrow == true) {
                                                        append = append + "<td style='text-align:center;height:30px;'  ><label  id='lblper" + tblcount + "' ></label></td><td style='text-align:center;height:30px;'><label  id='lblremark" + tblcount + "'></label></td>";
                                                        firstrow = false;
                                                        spanfor9 = parseInt(data.Table1.length);
                                                    }
                                                    else if (parseInt(spanfor9) > 0) {
                                                        append = append + "<td rowspan=" + spanfor9 + " colspan=3></td>";
                                                        spanfor9 = 0;
                                                    }
                                                    //  append = append + fill_grade(gradechar, medium);
                                                }
                                                else {
                                                    if (data.Table[j]["compare"] != "" && data.Table[j]["compare"] != null && data.Table[j]["compare"] != 'undefined') {
                                                        subpass = subpass + parseInt(data.Table[j]["compare"]);
                                                        passing = passing + parseInt(data.Table[j]["compare"]);
                                                    }
                                                    if (data.Table[j]["outof"] != "" && data.Table[j]["outof"] != null && data.Table[j]["outof"] != 'undefined') {
                                                        subout = subout + parseInt(data.Table[j]["outof"]);
                                                        total_out = total_out + parseInt(data.Table[j]["outof"]);
                                                    }
                                                    if (data.Table[j]["marks"] != "" && data.Table[j]["marks"] != null && data.Table[j]["marks"] != 'undefined' && data.Table[j]["marks"].toUpperCase() != "AA") {
                                                        subtot = subtot + parseInt(data.Table[j]["marks"]);
                                                        total_marks = total_marks + parseInt(data.Table[j]["marks"]);
                                                        if (parseInt(data.Table[j]["marks"]) < parseInt(data.Table[j]["compare"])) {
                                                            flagremark = "FAIL";
                                                        }
                                                    }
                                                    else if (data.Table[j]["marks"] == "" && data.Table[j]["marks"] == null && data.Table[j]["marks"] == 'undefined' && data.Table[j]["marks"].toUpperCase() == "AA") {
                                                        flagremark = "FAIL";
                                                    }
                                                    if (data.Table[j]["marks"].toUpperCase() == "AA") {
                                                        absent = true;
                                                    }
                                                    if (absent == true) {
                                                        submarks = "AA";
                                                    }
                                                    else {
                                                        submarks = subtot;
                                                    }
                                                    if (grace_flag == true) {
                                                        append = append + "<td style='text-align:center;height:30px;' >" + subout + "</td><td  style='text-align:center;height:30px;' >" + submarks + " <sup style='vertical-align: super;'>  +  " + requiredToPass + "</sup> </td>";
                                                    }

                                                    else
                                                    {
                                                        append = append + "<td style='text-align:center;height:30px;' >" + subout + "</td><td  style='text-align:center;height:30px;' >" + submarks + "</td>";
                                                        absent = false;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else {
                                        if (flag == false) {
                                            subper = Math.round((parseFloat(subtot) * 100) / parseFloat(subout));
                                            if (absent == true) {
                                                submarks = "AA";
                                            }
                                            else {
                                                submarks = subtot;
                                            }
                                            //  append = append + "<td style='text-align:center;height:30px;' >" + subout + "</td><td  style='text-align:center;height:30px;' >" + submarks + "</td>";
                                            absent = false;
                                            //if (firstrow == false) {
                                            //    append = append + "<td colspan=" + parseInt(data.Table1.length)-1 + "></td>"
                                            //}
                                            if (firstrow == true) {
                                                append = append + "<td style='text-align:center;height:30px;'  ><label  id='lblper" + tblcount + "' ></label></td><td style='text-align:center;height:30px;'><label  id='lblremark" + tblcount + "'></label></td>";
                                                firstrow = false;
                                                spanfor9 = parseInt(data.Table1.length);
                                            }
                                            else if (parseInt(spanfor9) > 0) {
                                                append = append + "<td rowspan=" + spanfor9 + " colspan=3></td>";
                                                spanfor9 = 0;
                                            }
                                            //  append = append + fill_grade(gradechar, medium);
                                            //  gradechar++;
                                            subtot = 0;
                                            subpass = 0;
                                            subout = 0;
                                            flag = true;
                                        }
                                    }
                                }
                            }
                            if (absent == true) {
                                submarks = "AA";
                            }
                            else {
                                submarks = subtot;
                            }
                            //  append = append + "<td style='text-align:center;height:30px;' >" + subout + "</td><td  style='text-align:center;height:30px;' >" + submarks + "</td>";
                            absent = false;
                            if (firstrow == true) {
                                append = append + "<td style='text-align:center;height:30px;'  ><label  id='lblper" + tblcount + "' ></label></td><td style='text-align:center;height:30px;'><label  id='lblremark" + tblcount + "'></label></td>";
                                firstrow = false;
                                spanfor9 = parseInt(data.Table1.length - 1);
                            }
                            else if (parseInt(spanfor9) > 0) {
                                append = append + "<td rowspan=" + spanfor9 + " colspan=3></td>";
                                spanfor9 = 0;
                            }
                            // append = append + fill_grade(gradechar, medium);
                            //gradechar++;
                            //for (var h = gradechar; h < 9; h++) {
                            //    append = append + "<tr><td style='text-align:center;height:30px;' colspan=2 ></td><td style='text-align:center;height:30px;'  ></td><td style='text-align:center;height:30px;'  ></td>" + fill_grade(gradechar, medium) + "</tr>";
                            //    gradechar++;
                            //}
                            subtot = 0;
                            subpass = 0;
                            subout = 0;
                            flag = true;

                            //if (medium == 1) {
                            //    append = append + "<tr><td style='text-align:center;width:18%;' colspan='2'>एकूण</td>";
                            //}

                            //else

                            {
                                append = append + "<tr><td style='text-align:center;width:18%;' colspan='2'>Total</td>";
                            }



                            perct = (parseFloat(total_marks) * 100) / parseFloat(total_out);
                            append = append + "<td style='text-align:center;height:30px;' >" + total_out + "</td><td style='text-align:center;height:30px;'  >" + total_marks + "</td><td style='text-align:center;width:18%;border: none;' colspan='3'></td>";
                            append = append + "</tbody></table></div>";

                            //school will reopwn from
                            if (datetime == "0") {

                            }
                            else {
                                append = append + "<div style='width: 100%; position: relative; text-align:left;margin-top:10px;margin-left:40%'> School Will Reopen On &nbsp  " + datetime + "</div>";
                            }

                          /*  append = append + "<div style='display:flex'><div style='width: 40%; position: relative; text-align:left;margin-top:35px;margin-left:15%'><span style='border-top:1px solid black'>CLASS TEACHER SIGN</span></div><div style='width: 60%; position: relative; text-align:right;margin-top:35px;margin-right:15%'><span style='border-top:1px solid black'> HEAD MISTERESS SIGN </span></div></div>";*/



                            $("[id*='div_tbl']").append(append);
                            $("[id*=lblper" + tblcount + "]")[0].innerText = perct.toFixed(2);
                            if (checkremark == "With") {
                                if (parseFloat(perct) >= 35 && flagremark == "PASS") {
                                    $("[id*=lblremark" + tblcount + "]")[0].innerText = "Pass";
                                    if (promoted_flag == true) {
                                        $("[id*=prmotd_remark" + tblcount + "]")[0].innerText = " Passed and Promoted to  " + Promoted_standard(standard) + "";
                                    }
                                }
                                else {
                                    $("[id*=lblremark" + tblcount + "]")[0].innerText = "Fail";
                                    if (promoted_flag == true) {
                                        $("[id*=prmotd_remark" + tblcount + "]")[0].innerText = "UnSuccessful";
                                    }
                                }
                            }
                            // $("[id*=lblgrade" + tblcount + "]")[0].innerText = gradeonly(perct, medium);
                            // $("[id*=lblremark" + tblcount + "]")[0].innerText = remarkonly(perct, medium, flagremark);
                            flagremark = "PASS";
                            // $("[id*=lblper]").val() = perct;
                            total_marks = 0;
                            total_out = 0;
                            passing = 0;
                            firstrow = true;
                            flagremark = "PASS";
                        }
                    }

                }
                var total_3_sub_count;
                if (j % data.Table1.length == 0) {
                    total_3_sub_count = 0;
                    total_remain_grace_mark = 15;
                }
                // $("[id*='div_tbl']").append(append);

            }
        }
        else {
            if (data.Table.length > 0) {
                var passing = 0;
                var outof = 0;
                var subpass = 0;
                var flagremark = "PASS";
                var append = "";
                var rowcount = 0;
                var total_marks = 0;
                var total_out = 0;
                var perct = 0;
                var grade = "";
                var subtot = 0;
                var subout = 0;
                var subper = 0;
                var gradechar = 0;
                var graderow = "";
                var flag = false;
                //let mrks_obtn_flag = false;

                for (var j = 0; j < data.Table.length; j++) {

                    var colcount = 0;

                    if (j > 0) {
                        if (data.Table[j - 1]["exam_type"] == "Grade") {
                            flag = true;
                        }
                        else {
                            flag = false;
                        }
                    }
                    if (j == 0) {
                        append = append + "<div style='width: 100%; position: relative;'><table style='border:0px;margin-top:150px' id='tbl" + tblcount + "t'><thead><tr>";
                        if (medium == 2) {
                            append = append + "<th>PROGRESS CARD</th>";
                        }
                        else {

                            append = append + "<th>प्रगती पत्रक</th>";
                        }


                        if (medium == 1) {
                            append = append + " </tr><tr><th>" + data.Table4[0].duration + "</th></tr><tr><th> विद्यार्थ्यांचे नाव : " + data.Table[j].stud_name + "</th ></tr ></thead ></table > ";
                        }

                        else {
                            append = append + " </tr><tr><th>" + data.Table4[0].duration + "</th></tr><tr><th> NAME : " + data.Table[j].stud_name + "</th ></tr ></thead ></table > ";
                        }


                        tblcount++;


                        if (medium == 1) {
                            var rollno = data.Table[j].roll_no.toString()
                            grno = data.Table[j].stud_gr.toString()
                            append = append + "<table style='border:none;margin-top:5px' id='tbl" + tblcount + "t'><tbody><tr> <td style='text-align:center;width:16%;' >रजिस्टर क्रमांक</td><td  style='text-align:center;width:16%;'>इयत्ता</td><td style='text-align:center;width:18%;' >तुकडी</td><td style='text-align:center;width:18%;' >हजेरी क्रमांक</td><td style='text-align:center;width:32%;'  colspan='3'>परीक्षा</td></tr>";
                            append = append + "<tr> <td style='text-align:center;' >" + margr(grno) + "</td><td style='text-align:center;' >" + standard + "</td><td style='text-align:center;' >" + data.Table[j].division + "</td><td style='text-align:center;' >" + mardate(rollno) + "</td><td style='text-align:center;' colspan='3' >" + exam + " </td>";
                        }

                        else {
                            append = append + "<table style='border:none;margin-top:5px' id='tbl" + tblcount + "t'><tbody><tr> <td style='text-align:center;width:16%;' >Gr. No.</td><td  style='text-align:center;width:16%;'>Standard</td><td style='text-align:center;width:18%;' >Division</td><td style='text-align:center;width:18%;' >Roll No.</td><td style='text-align:center;width:32%;'  colspan='3'>Exam</td></tr>";
                            append = append + "<tr> <td style='text-align:center;' >" + data.Table[j].stud_gr + "</td><td style='text-align:center;' >" + standard + "</td><td style='text-align:center;' >" + data.Table[j].division + "</td><td style='text-align:center;' >" + data.Table[j].roll_no + "</td><td style='text-align:center;' colspan='3' >" + exam + " </td>";
                        }

                        if (medium == 1) {

                            if (mrks_obtn_flag == false) {
                                append = append + "<tr> <td style='text-align:center;height:30px;' colspan='2'>विषय</td><td style='text-align:center;height:30px;display:none;'>गुण मिळाले</td><td style='text-align:center;height:30px;'>श्रेणी</td><td style='text-align:center;width:25%;'  colspan='2'>श्रेणी स्तर</td><td style='text-align:center;width:32%;' >शेरा</td></tr>";
                            }
                            else {
                                append = append + "<tr> <td style='text-align:center;height:30px;' colspan='2'>विषय</td><td style='text-align:center;height:30px;' >गुण मिळाले</td><td style='text-align:center;height:30px;'>श्रेणी</td><td style='text-align:center;width:21%;'  colspan='2'>श्रेणी स्तर</td><td style='text-align:center;width:32%;' >शेरा</td></tr>";
                            }

                        }

                        else {
                            if (mrks_obtn_flag == false) {
                                append = append + "<tr> <td style='text-align:center;height:30px;' colspan='2'>SUBJECT</td><td style='text-align:center;height:30px;display:none;'>OBTAINED MARKS</td><td style='text-align:center;height:30px;'>Grade</td><td style='text-align:center;width:25%;'  colspan='2'>Grade Key</td><td style='text-align:center;width:32%;' >Remark</td></tr>";
                            }
                            else {
                                append = append + "<tr> <td style='text-align:center;height:30px;' colspan='2'>SUBJECT</td><td style='text-align:center;height:30px;' >OBTAINED MARKS</td><td style='text-align:center;height:30px;'>Grade</td><td style='text-align:center;width:21%;'  colspan='2'>Grade Key</td><td style='text-align:center;width:32%;' >Remark</td></tr>";
                            }

                        }


                        for (var i = 0; i < data.Table1.length; i++) {

                            if (data.Table[j].subject_id == data.Table1[i].subject_id) {
                                if (i == 0) {


                                    append = append + "<tr><td style='height:30px;padding-left: 5px;' colspan='2'>" + data.Table1[i].subject_name + "</td>";

                                    if (data.Table[j]["exam_type"] == "Grade") {
                                        append = append + "<td style='text-align:center;height:30px;' >- -</td><td style='text-align:center;height:30px;' >" + data.Table[j].marks + "</td>";
                                        append = append + fill_grade(gradechar, medium);
                                        gradechar++;

                                    }
                                    else {
                                        if (data.Table[j]["compare"] != "" && data.Table[j]["compare"] != null && data.Table[j]["compare"] != 'undefined') {
                                            subpass = subpass + parseInt(data.Table[j]["compare"]);
                                            passing = passing + parseInt(data.Table[j]["compare"]);
                                        }
                                        if (data.Table[j]["outof"] != "" && data.Table[j]["outof"] != null && data.Table[j]["outof"] != 'undefined') {
                                            subout = subout + parseInt(data.Table[j]["outof"]);
                                            total_out = total_out + parseInt(data.Table[j]["outof"]);
                                        }
                                        if (data.Table[j]["marks"] != "" && data.Table[j]["marks"] != null && data.Table[j]["marks"] != 'undefined') {
                                            subtot = subtot + parseInt(data.Table[j]["marks"]);
                                            total_marks = total_marks + parseInt(data.Table[j]["marks"]);
                                        }

                                    }
                                }
                                else {
                                    if (data.Table[i].subject_id != data.Table[i - 1].subject_id) {
                                        append = append + "<tr><td style='height:30px;padding-left: 5px;' colspan='2'>" + data.Table1[i].subject_name + "</td>";

                                        if (data.Table[j]["exam_type"] == "Grade") {
                                            append = append + "<td style='text-align:center;height:30px;' >- -</td><td style='text-align:center;height:30px;' >" + data.Table[j].marks + "</td>";
                                            append = append + fill_grade(gradechar, medium);
                                            gradechar++;
                                        }
                                        else {
                                            if (data.Table[j]["compare"] != "" && data.Table[j]["compare"] != null && data.Table[j]["compare"] != 'undefined') {
                                                subpass = subpass + parseInt(data.Table[j]["compare"]);
                                                passing = passing + parseInt(data.Table[j]["compare"]);
                                            }
                                            if (data.Table[j]["outof"] != "" && data.Table[j]["outof"] != null && data.Table[j]["outof"] != 'undefined') {
                                                subout = subout + parseInt(data.Table[j]["outof"]);
                                                total_out = total_out + parseInt(data.Table[j]["outof"]);
                                            }
                                            if (data.Table[j]["marks"] != "" && data.Table[j]["marks"] != null && data.Table[j]["marks"] != 'undefined') {
                                                subtot = subtot + parseInt(data.Table[j]["marks"]);
                                                total_marks = total_marks + parseInt(data.Table[j]["marks"]);
                                            }

                                        }
                                    }
                                }
                            }
                        }
                    }
                    else {
                        if (j < data.Table.length - 1) {
                            if (data.Table[j].stud_id == data.Table[j - 1].stud_id) {

                                if (data.Table[j].subject_id == data.Table[j - 1].subject_id) {
                                    if (data.Table[j]["compare"] != "" && data.Table[j]["compare"] != null && data.Table[j]["compare"] != 'undefined') {
                                        subpass = subpass + parseInt(data.Table[j]["compare"]);
                                        passing = passing + parseInt(data.Table[j]["compare"]);
                                    }
                                    if (data.Table[j]["outof"] != "" && data.Table[j]["outof"] != null && data.Table[j]["outof"] != 'undefined') {
                                        subout = subout + parseInt(data.Table[j]["outof"]);
                                        total_out = total_out + parseInt(data.Table[j]["outof"]);
                                    }
                                    if (data.Table[j]["marks"] != "" && data.Table[j]["marks"] != null && data.Table[j]["marks"] != 'undefined') {
                                        subtot = subtot + parseInt(data.Table[j]["marks"]);
                                        total_marks = total_marks + parseInt(data.Table[j]["marks"]);
                                    }

                                }
                                else {


                                    for (var i = 0; i < data.Table1.length; i++) {
                                        if (data.Table[j].subject_id == data.Table1[i].subject_id) {
                                            if (i == 0) {


                                                append = append + "</tr><tr><td style='height:30px;padding-left: 5px;' colspan='2'>" + data.Table1[i].subject_name + "</td>";

                                                if (data.Table[j]["exam_type"] == "Grade") {
                                                    append = append + "<td style='text-align:center;height:30px;' >- -</td><td style='text-align:center;height:30px;' >" + data.Table[j].marks + "</td>";
                                                    append = append + fill_grade(gradechar, medium);
                                                    gradechar++;
                                                }
                                                else {
                                                    if (data.Table[j]["compare"] != "" && data.Table[j]["compare"] != null && data.Table[j]["compare"] != 'undefined') {
                                                        subpass = subpass + parseInt(data.Table[j]["compare"]);
                                                        passing = passing + parseInt(data.Table[j]["compare"]);
                                                    }
                                                    if (data.Table[j]["outof"] != "" && data.Table[j]["outof"] != null && data.Table[j]["outof"] != 'undefined') {
                                                        subout = subout + parseInt(data.Table[j]["outof"]);
                                                        total_out = total_out + parseInt(data.Table[j]["outof"]);
                                                    }
                                                    if (data.Table[j]["marks"] != "" && data.Table[j]["marks"] != null && data.Table[j]["marks"] != 'undefined') {
                                                        subtot = subtot + parseInt(data.Table[j]["marks"]);
                                                        total_marks = total_marks + parseInt(data.Table[j]["marks"]);
                                                    }

                                                }
                                            }
                                            else {
                                                if (data.Table[i].subject_id != data.Table[i - 1].subject_id) {
                                                    append = append + "</tr><tr><td style='height:30px;padding-left: 5px;' colspan='2'>" + data.Table1[i].subject_name + "</td>";

                                                    if (data.Table[j]["exam_type"] == "Grade") {
                                                        if (mrks_obtn_flag == false) {
                                                            append = append + "<td style='text-align:center;height:30px;display:none;' >- -</td><td style='text-align:center;height:30px;' >" + data.Table[j].marks + "</td>";
                                                        }
                                                        else {
                                                            append = append + "<td style='text-align:center;height:30px;' >- -</td><td style='text-align:center;height:30px;' >" + data.Table[j].marks + "</td>";
                                                        }
                                                        append = append + fill_grade(gradechar, medium);
                                                        gradechar++;
                                                    }
                                                    else {
                                                        if (data.Table[j]["compare"] != "" && data.Table[j]["compare"] != null && data.Table[j]["compare"] != 'undefined') {
                                                            subpass = subpass + parseInt(data.Table[j]["compare"]);
                                                            passing = passing + parseInt(data.Table[j]["compare"]);
                                                        }
                                                        if (data.Table[j]["outof"] != "" && data.Table[j]["outof"] != null && data.Table[j]["outof"] != 'undefined') {
                                                            subout = subout + parseInt(data.Table[j]["outof"]);
                                                            total_out = total_out + parseInt(data.Table[j]["outof"]);
                                                        }
                                                        if (data.Table[j]["marks"] != "" && data.Table[j]["marks"] != null && data.Table[j]["marks"] != 'undefined') {
                                                            subtot = subtot + parseInt(data.Table[j]["marks"]);
                                                            total_marks = total_marks + parseInt(data.Table[j]["marks"]);
                                                        }

                                                    }
                                                }
                                            }
                                        }
                                        else {
                                            if (flag == false) {
                                                subper = Math.round((parseFloat(subtot) * 100) / parseFloat(subout));
                                                if (mrks_obtn_flag == false) {
                                                    append = append + "<td style='text-align:center;height:30px;display:none;' >" + subtot + "</td><td style='text-align:center;height:30px;' >" + get_grade(subper, medium) + "</td>";
                                                }
                                                else {
                                                    append = append + "<td style='text-align:center;height:30px;' >" + subtot + "</td><td style='text-align:center;height:30px;' >" + get_grade(subper, medium) + "</td>";
                                                }

                                                append = append + fill_grade(gradechar, medium, standard);
                                                gradechar++;
                                                subtot = 0;
                                                subpass = 0;
                                                subout = 0;
                                                flag = true;
                                            }
                                        }
                                    }
                                }
                            }
                            else {
                                if (data.Table[j - 1].exam_type != "Grade") {
                                    subper = Math.round((parseFloat(subtot) * 100) / parseFloat(subout));
                                    if (mrks_obtn_flag == false) {
                                        append = append + "<td style='text-align:center;height:30px;display:none;'>" + subtot + "</td><td style='text-align:center;height:30px;'  >" + get_grade(subper, medium) + "</td>"
                                    }
                                    else {
                                        append = append + "<td style='text-align:center;height:30px;'>" + subtot + "</td><td style='text-align:center;height:30px;'  >" + get_grade(subper, medium) + "</td>"
                                    }
                                    append = append + fill_grade(gradechar, medium, standard);
                                    gradechar++;

                                }
                                if (medium == 1 && (standard == "छोटा शिशु" || standard == "मोठा शिशु" || standard == "बालवर्ग" || standard == "१ ली" || standard == "२ री" || standard == "३ री" || standard == "४ थी")) {

                                    for (var h = gradechar; h < 7; h++) {
                                        if (mrks_obtn_flag == false) {
                                            append = append + "<tr><td style='text-align:center;height:30px;' colspan=2 ></td><td style='text-align:center;height:30px;display:none;'></td><td style='text-align:center;height:30px;'  ></td>" + fill_grade(gradechar, medium, standard) + "</tr>";
                                        }
                                        else {
                                            append = append + "<tr><td style='text-align:center;height:30px;' colspan=2 ></td><td style='text-align:center;height:30px;'></td><td style='text-align:center;height:30px;'></td>" + fill_grade(gradechar, medium, standard) + "</tr>";
                                        }
                                        gradechar++;
                                    }

                                }

                                else {
                                    for (var h = gradechar; h < 9; h++) {
                                        if (mrks_obtn_flag == false) {
                                            append = append + "<tr><td style='text-align:center;height:30px;' colspan=2 ></td><td style='text-align:center;height:30px;display:none;'></td><td style='text-align:center;height:30px;'  ></td>" + fill_grade(gradechar, medium, standard) + "</tr>";
                                        }
                                        else {
                                            append = append + "<tr><td style='text-align:center;height:30px;' colspan=2 ></td><td style='text-align:center;height:30px;'></td><td style='text-align:center;height:30px;'></td>" + fill_grade(gradechar, medium, standard) + "</tr>";
                                        }
                                        gradechar++;
                                    }

                                }

                                gradechar = 0;
                                subtot = 0;
                                subpass = 0;
                                subout = 0;
                                flag = true;

                                if (medium == 1) {
                                    append = append + "<tr><td style='text-align:center;width:18%;' colspan='2'>एकूण</td>";
                                }

                                else {
                                    append = append + "<tr><td style='text-align:center;width:18%;' colspan='2'>Total</td>";
                                }






                                perct = (parseFloat(total_marks) * 100) / parseFloat(total_out);
                                //if (standard != "9" && standard != "10" && standard != "९ वी" && standard != "१० वी") {
                                //    if (mrks_obtn_flag == false) {
                                //        append = append + "<td style='text-align:center;height:30px;display:none;'>" + total_marks + "</td><td style='text-align:center;height:30px;' >" + get_grade(perct, medium) + "</td><td style='text-align:center;width:18%;' colspan='3'>Passed and Promoted to  " + class_promoted(standard) + "   </td>"
                                //    }
                                //    else {
                                //        append = append + "<td style='text-align:center;height:30px;'  >" + total_marks + "</td><td style='text-align:center;height:30px;' >" + get_grade(perct, medium) + "</td><td style='text-align:center;width:18%;' colspan='3'>Passed and Promoted to  " + class_promoted(standard) + "   </td>"
                                //    }

                                //}

                                /* else {*/
                                if (mrks_obtn_flag == false) {
                                    append = append + "<td style='text-align:center;height:30px;display:none;'>" + total_marks + "</td><td style='text-align:center;height:30px;' >" + get_grade(perct, medium) + "</td>";

                                    //if (promoted_flag = true) {
                                    //    append = append + "   <td style='text-align:center;width:18%;' colspan='3'>Passed and Promoted to " + Promoted_standard(standard) + " </td>";
                                    //}
                                   // else
                                    {
                                        append = append + "   <td style='text-align:center;width:18%;' colspan='3'></td>";
                                    }

                                }

                                
                                else {
                                    append = append + "<td style='text-align:center;height:30px;'  >" + total_marks + "</td><td style='text-align:center;height:30px;' >" + get_grade(perct, medium) + "</td>";

                                    //if (promoted_flag = true) {
                                    //    append = append + "   <td style='text-align:center;width:18%;' colspan='3'>Passed and Promoted to " + Promoted_standard(standard) + " </td>";
                                    //}
                                    //else
                                    {
                                        append = append + "   <td style='text-align:center;width:18%;' colspan='3'></td>";
                                    }

                                }
                                /*   }*/


                                total_marks = 0;
                                total_out = 0;
                                passing = 0;

                                gradechar = 0;
                                append = append + "</tbody></table></div>";

                                if (datetime == "0") {

                                }
                                else {
                                    append = append + "<div style='width: 100%; position: relative; text-align:left;margin-top:10px;margin-left:40%'> School Will Reopen On &nbsp  " + datetime + "</div>";
                                }

                                append = append +  "<div style = 'width: 100%; position: relative; page-break-before: always;' > <table style='border:0px;margin-top:150px' id='tbl" + tblcount + "t'><thead><tr>";
                                if (medium == 2) {
                                    append = append + "<th>PROGRESS CARD</th>";
                                }
                                else {

                                    append = append + "<th>प्रगती पत्रक</th>";
                                }


                                if (medium == 1) {
                                    append = append + "</tr><tr><th>" + data.Table4[0].duration + "</th></tr><tr><th>विद्यार्थ्यांचे नाव : " + data.Table[j].stud_name + "</th></tr ></thead ></table > ";
                                }
                                else {
                                    append = append + "</tr><tr><th>" + data.Table4[0].duration + "</th></tr><tr><th>NAME : " + data.Table[j].stud_name + "</th></tr ></thead ></table > ";
                                }


                                tblcount++;


                                if (medium == 1) {

                                    rollno = data.Table[j].roll_no.toString()
                                    grno = data.Table[j].stud_gr.toString()
                                    append = append + "<table style='border:none;margin-top:5px' id='tbl" + tblcount + "t'><tbody><tr> <td style='text-align:center;width:16%;' >रजिस्टर क्रमांक</td><td  style='text-align:center;width:16%;'>इयत्ता</td><td style='text-align:center;width:18%;' >तुकडी</td><td style='text-align:center;width:18%;' >हजेरी क्रमांक</td><td style='text-align:center;width:32%;' colspan='3' >परीक्षा</td></tr>";
                                    append = append + "<tr> <td style='text-align:center;' >" + margr(grno) + "</td><td style='text-align:center;' >" + standard + "</td><td style='text-align:center;' >" + data.Table[j].division + "</td><td style='text-align:center;' >" + mardate(rollno) + "</td><td style='text-align:center;'  colspan='3'>" + exam + " </td>";

                                }

                                else {
                                    append = append + "<table style='border:none;margin-top:5px' id='tbl" + tblcount + "t'><tbody><tr> <td style='text-align:center;width:16%;' >Gr. No.</td><td  style='text-align:center;width:16%;'>Standard</td><td style='text-align:center;width:18%;' >Division</td><td style='text-align:center;width:18%;' >Roll No.</td><td style='text-align:center;width:32%;' colspan='3' >Exam</td></tr>";
                                    append = append + "<tr> <td style='text-align:center;' >" + data.Table[j].stud_gr + "</td><td style='text-align:center;' >" + standard + "</td><td style='text-align:center;' >" + data.Table[j].division + "</td><td style='text-align:center;' >" + data.Table[j].roll_no + "</td><td style='text-align:center;'  colspan='3'>" + exam + " </td>";
                                }


                                if (medium == 1) {
                                    if (mrks_obtn_flag == false) {
                                        append = append + "<tr> <td style='text-align:center;height:30px;' colspan='2'>विषय</td><td style='text-align:center;height:30px;display:none;'>गुण मिळाले </td><td style='text-align:center;height:30px;'>श्रेणी</td><td style='text-align:center;width:25%;'  colspan='2'>श्रेणी स्तर</td><td style='text-align:center;width:32%;' >शेरा</td></tr>";
                                    }
                                    else {
                                        append = append + "<tr> <td style='text-align:center;height:30px;' colspan='2'>विषय</td><td style='text-align:center;height:30px;' >गुण मिळाले</td><td style='text-align:center;height:30px;'>श्रेणी</td><td style='text-align:center;width:21%;'  colspan='2'>श्रेणी स्तर</td><td style='text-align:center;width:32%;' >शेरा</td></tr>";
                                    }
                                }

                                else {
                                    if (mrks_obtn_flag == false) {
                                        append = append + "<tr> <td style='text-align:center;height:30px;' colspan='2'>SUBJECT</td><td style='text-align:center;height:30px;display:none;'>OBTAINED MARKS</td><td style='text-align:center;height:30px;'>Grade</td><td style='text-align:center;width:25%;'  colspan='2'>Grade Key</td><td style='text-align:center;width:32%;' >Remark</td></tr>";
                                    }
                                    else {
                                        append = append + "<tr> <td style='text-align:center;height:30px;' colspan='2'>SUBJECT</td><td style='text-align:center;height:30px;' >OBTAINED MARKS</td><td style='text-align:center;height:30px;'>Grade</td><td style='text-align:center;width:21%;'  colspan='2'>Grade Key</td><td style='text-align:center;width:32%;' >Remark</td></tr>";
                                    }
                                }




                                var flag_new = false;
                                for (k = 0; k < data.Table1.length; k++) {
                                    if (data.Table[j].subject_id == data.Table1[k].subject_id) {
                                        if (flag_new == false) {
                                            append = append + "<td style='height:30px;padding-left: 5px;' colspan='2'>" + data.Table1[k].subject_name + "</td>";
                                            flag_new = true;
                                            if (data.Table[j]["compare"] != "" && data.Table[j]["compare"] != null && data.Table[j]["compare"] != 'undefined') {
                                                subpass = subpass + parseInt(data.Table[j]["compare"]);
                                                passing = passing + parseInt(data.Table[j]["compare"]);
                                            }
                                            if (data.Table[j]["outof"] != "" && data.Table[j]["outof"] != null && data.Table[j]["outof"] != 'undefined') {
                                                subout = subout + parseInt(data.Table[j]["outof"]);
                                                total_out = total_out + parseInt(data.Table[j]["outof"]);
                                            }
                                            if (data.Table[j]["marks"] != "" && data.Table[j]["marks"] != null && data.Table[j]["marks"] != 'undefined') {
                                                subtot = subtot + parseInt(data.Table[j]["marks"]);
                                                total_marks = total_marks + parseInt(data.Table[j]["marks"]);
                                            }

                                        }
                                    }
                                }

                            }
                        }
                        else {



                            if (data.Table[j].subject_id == data.Table[j - 1].subject_id) {
                                if (data.Table[j]["compare"] != "" && data.Table[j]["compare"] != null && data.Table[j]["compare"] != 'undefined') {
                                    subpass = subpass + parseInt(data.Table[j]["compare"]);
                                    passing = passing + parseInt(data.Table[j]["compare"]);
                                }
                                if (data.Table[j]["outof"] != "" && data.Table[j]["outof"] != null && data.Table[j]["outof"] != 'undefined') {
                                    subout = subout + parseInt(data.Table[j]["outof"]);
                                    total_out = total_out + parseInt(data.Table[j]["outof"]);
                                }
                                if (data.Table[j]["marks"] != "" && data.Table[j]["marks"] != null && data.Table[j]["marks"] != 'undefined') {
                                    subtot = subtot + parseInt(data.Table[j]["marks"]);
                                    total_marks = total_marks + parseInt(data.Table[j]["marks"]);
                                }
                                if (mrks_obtn_flag == false) {
                                    append = append + "<td  style='text-align:center;height:30px;display:none' >" + subtot + "</td><td style='text-align:center;height:30px;' >" + get_grade(subper, medium) + "</td>";
                                }

                                else {
                                    append = append + "<td  style='text-align:center;height:30px;' >" + subtot + "</td><td style='text-align:center;height:30px;' >" + get_grade(subper, medium) + "</td>";
                                }
                                append = append + fill_grade(gradechar, medium);
                                gradechar++;
                                subtot = 0;
                                subpass = 0;
                                subout = 0;
                                flag = true;
                            }
                            else {
                                //append = append + "<td  style='text-align:center;height:30px;' >" + subtot + "</td><td style='text-align:center;height:30px;' >" + get_grade(subper, medium) + "</td>";
                                //append = append + fill_grade(gradechar, medium);
                                //gradechar++;
                                //subtot = 0;
                                //subpass = 0;
                                //subout = 0;
                                //flag = true;
                                for (var i = 0; i < data.Table1.length; i++) {
                                    if (data.Table[j].subject_id == data.Table1[i].subject_id) {
                                        if (i == 0) {


                                            append = append + "</tr><tr><td style='height:30px;padding-left: 5px;' colspan='2'>" + data.Table1[i].subject_name + "</td>";

                                            if (data.Table[j]["exam_type"] == "Grade") {
                                                append = append + "<td style='text-align:center;height:30px;' >- -</td><td style='text-align:center;height:30px;' >" + data.Table[j].marks + "</td>";
                                                append = append + fill_grade(gradechar, medium);
                                                gradechar++;
                                            }
                                            else {
                                                if (data.Table[j]["compare"] != "" && data.Table[j]["compare"] != null && data.Table[j]["compare"] != 'undefined') {
                                                    subpass = subpass + parseInt(data.Table[j]["compare"]);
                                                    passing = passing + parseInt(data.Table[j]["compare"]);
                                                }
                                                if (data.Table[j]["outof"] != "" && data.Table[j]["outof"] != null && data.Table[j]["outof"] != 'undefined') {
                                                    subout = subout + parseInt(data.Table[j]["outof"]);
                                                    total_out = total_out + parseInt(data.Table[j]["outof"]);
                                                }
                                                if (data.Table[j]["marks"] != "" && data.Table[j]["marks"] != null && data.Table[j]["marks"] != 'undefined') {
                                                    subtot = subtot + parseInt(data.Table[j]["marks"]);
                                                    total_marks = total_marks + parseInt(data.Table[j]["marks"]);
                                                }
                                                subper = Math.round((parseFloat(subtot) * 100) / parseFloat(subout));
                                                append = append + "<td  style='text-align:center;height:30px;' >" + subtot + "</td><td style='text-align:center;height:30px;' >" + get_grade(subper, medium) + "</td>";
                                                append = append + fill_grade(gradechar, medium);
                                                gradechar++;
                                                subtot = 0;
                                                subpass = 0;
                                                subout = 0;
                                                flag = true;

                                            }
                                        }
                                        else {
                                            if (data.Table[i].subject_id != data.Table[i - 1].subject_id) {
                                                append = append + "</tr><tr><td style='height:30px;padding-left: 5px;' colspan='2'>" + data.Table1[i].subject_name + "</td>";

                                                if (data.Table[j]["exam_type"] == "Grade") {
                                                    if (mrks_obtn_flag == false) {
                                                        append = append + "<td style='text-align:center;height:30px;display:none;'>- -</td><td style='text-align:center;height:30px;' >" + data.Table[j].marks + "</td>";
                                                    }
                                                    else {
                                                        append = append + "<td style='text-align:center;height:30px;'>- -</td><td style='text-align:center;height:30px;' >" + data.Table[j].marks + "</td>";
                                                    }
                                                    append = append + fill_grade(gradechar, medium);
                                                    gradechar++;
                                                }
                                                else {
                                                    if (data.Table[j]["compare"] != "" && data.Table[j]["compare"] != null && data.Table[j]["compare"] != 'undefined') {
                                                        subpass = subpass + parseInt(data.Table[j]["compare"]);
                                                        passing = passing + parseInt(data.Table[j]["compare"]);
                                                    }
                                                    if (data.Table[j]["outof"] != "" && data.Table[j]["outof"] != null && data.Table[j]["outof"] != 'undefined') {
                                                        subout = subout + parseInt(data.Table[j]["outof"]);
                                                        total_out = total_out + parseInt(data.Table[j]["outof"]);
                                                    }
                                                    if (data.Table[j]["marks"] != "" && data.Table[j]["marks"] != null && data.Table[j]["marks"] != 'undefined') {
                                                        subtot = subtot + parseInt(data.Table[j]["marks"]);
                                                        total_marks = total_marks + parseInt(data.Table[j]["marks"]);
                                                    }
                                                    subper = Math.round((parseFloat(subtot) * 100) / parseFloat(subout));
                                                    if (mrks_obtn_flag == false) {
                                                        append = append + "<td  style='text-align:center;height:30px;display:none;' >" + subtot + "</td><td style='text-align:center;height:30px;' >" + get_grade(subper, medium) + "</td>";
                                                    }
                                                    else {
                                                        append = append + "<td  style='text-align:center;height:30px;' >" + subtot + "</td><td style='text-align:center;height:30px;' >" + get_grade(subper, medium) + "</td>";
                                                    }

                                                    append = append + fill_grade(gradechar, medium);
                                                    gradechar++;
                                                    subtot = 0;
                                                    subpass = 0;
                                                    subout = 0;
                                                    flag = true;

                                                }
                                            }
                                        }
                                        flag = false;
                                    }
                                    else {
                                        if (flag == false) {
                                            subper = Math.round((parseFloat(subtot) * 100) / parseFloat(subout));
                                            if (mrks_obtn_flag == false) {
                                                append = append + "<td  style='text-align:center;height:30px;display:none;'>" + subtot + "</td><td style='text-align:center;height:30px;' >" + get_grade(subper, medium) + "</td>";
                                            }
                                            else {
                                                append = append + "<td  style='text-align:center;height:30px;' >" + subtot + "</td><td style='text-align:center;height:30px;' >" + get_grade(subper, medium) + "</td>";
                                            }

                                            append = append + fill_grade(gradechar, medium);
                                            gradechar++;
                                            subtot = 0;
                                            subpass = 0;
                                            subout = 0;
                                            flag = true;
                                        }
                                    }
                                }
                            }
                            /*   gradechar++;*/ /*sakshi*/


                            if (medium == 1 && (standard == "छोटा शिशु" || standard == "मोठा शिशु" || standard == "बालवर्ग" || standard == "१ ली" || standard == "२ री" || standard == "३ री" || standard == "४ थी")) {
                                for (var h = gradechar; h < 7; h++) {

                                    if (mrks_obtn_flag == false) {
                                        append = append + "<tr><td style='text-align:center;height:30px;' colspan=2 ></td><td style='text-align:center;height:30px;display:none'  ></td><td style='text-align:center;height:30px;'  ></td>" + fill_grade(gradechar, medium) + "</tr>";
                                        gradechar++;
                                    }
                                    else {
                                        append = append + "<tr><td style='text-align:center;height:30px;' colspan=2 ></td><td style='text-align:center;height:30px;'  ></td><td style='text-align:center;height:30px;'  ></td>" + fill_grade(gradechar, medium) + "</tr>";
                                        gradechar++;
                                    }


                                }

                            }

                            else {

                                for (var h = gradechar; h < 9; h++) {

                                    if (mrks_obtn_flag == false) {
                                        append = append + "<tr><td style='text-align:center;height:30px;' colspan=2 ></td><td style='text-align:center;height:30px;display:none'  ></td><td style='text-align:center;height:30px;'  ></td>" + fill_grade(gradechar, medium) + "</tr>";
                                        gradechar++;
                                    }
                                    else {
                                        append = append + "<tr><td style='text-align:center;height:30px;' colspan=2 ></td><td style='text-align:center;height:30px;'  ></td><td style='text-align:center;height:30px;'  ></td>" + fill_grade(gradechar, medium) + "</tr>";
                                        gradechar++;
                                    }


                                }
                            }



                            subtot = 0;
                            subpass = 0;
                            subout = 0;
                            flag = true;

                            if (medium == 1) {
                                append = append + "<tr><td style='text-align:center;width:18%;' colspan='2'>एकूण</td>";
                            }

                            else {
                                append = append + "<tr><td style='text-align:center;width:18%;' colspan='2'>Total</td>";
                            }




                            perct = (parseFloat(total_marks) * 100) / parseFloat(total_out);
                            //if (standard != "9" && standard != "10" && standard != "९ वी" && standard != "१० वी") {
                            //    if (mrks_obtn_flag == false) {
                            //        append = append + "<td style='text-align:center;height:30px;display:none;' >" + total_marks + "</td><td style='text-align:center;height:30px;' >" + get_grade(perct, medium) + "</td><td style='text-align:center;width:18%;' colspan='3'> Passed and Promoted to  " + class_promoted(standard) + "   </td>";
                            //    }
                            //    else {
                            //        append = append + "<td style='text-align:center;height:30px;' >" + total_marks + "</td><td style='text-align:center;height:30px;' >" + get_grade(perct, medium) + "</td><td style='text-align:center;width:18%;' colspan='3'>Passed and Promoted to  " + class_promoted(standard) + "   </td>";
                            //    }
                            //}


                            /* else {*/
                            if (mrks_obtn_flag == false) {
                                append = append + "<td style='text-align:center;height:30px;display:none;' >" + total_marks + "</td><td style='text-align:center;height:30px;' >" + get_grade(perct, medium) + "</td>";
                                //if (promoted_flag = true) {
                                //    append = append + "   <td style='text-align:center;width:18%;' colspan='3'>Passed and Promoted to " + Promoted_standard(standard) + " </td>";
                                //}
                                //else
                                {
                                    append = append + "   <td style='text-align:center;width:18%;' colspan='3'></td>";
                                }

                            }

                            
                            else {
                                append = append + "<td style='text-align:center;height:30px;' >" + total_marks + "</td><td style='text-align:center;height:30px;' >" + get_grade(perct, medium) + "</td>";

                                //if (promoted_flag = true) {
                                //    append = append + "   <td style='text-align:center;width:18%;' colspan='3'>Passed and Promoted to " + Promoted_standard(standard) + " </td>";
                                //}
                                //else
                                {
                                    append = append + "   <td style='text-align:center;width:18%;' colspan='3'></td>";
                                }

                            }

                            /*  }*/

                            total_marks = 0;
                            total_out = 0;
                            passing = 0;
                        }
                    }

                }
                $("[id*='div_tbl']").append(append);

            }
        }
    }
});

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

var test_var = 0;
function fill_grade(count, medium, standard) {
    var graderow = "";
    if (medium == 2) {
        if (parseInt(count) == 0) {
            graderow = "<td style='text-align:center;height:30px;'>91 % to 100 %</td><td style='text-align:center;height:30px;'>A1</td><td style='text-align:center;height:30px;'>Outstanding</td>";
        }
        else if (parseInt(count) == 1) {
            graderow = "<td style='text-align:center;height:30px;'>81 % to 90 %</td><td style='text-align:center;height:30px;'>A2</td><td style='text-align:center;height:30px;'>Exemplary</td>";
        }
        else if (parseInt(count) == 2) {
            graderow = "<td style='text-align:center;height:30px;'>71 % to 80 %</td><td style='text-align:center;height:30px;'>B1</td><td style='text-align:center;height:30px;'>Excellent</td>";
        }
        else if (parseInt(count) == 3) {
            graderow = "<td style='text-align:center;height:30px;'>61 % to 70 %</td><td style='text-align:center;height:30px;'>B2</td><td style='text-align:center;height:30px;'>Very Good</td>";
        }
        else if (parseInt(count) == 4) {
            graderow = "<td style='text-align:center;height:30px;'>51 % to 60 %</td><td style='text-align:center;height:30px;'>C1</td><td style='text-align:center;height:30px;'>Good</td>";
        }
        else if (parseInt(count) == 5) {
            graderow = "<td style='text-align:center;height:30px;'>41 % to 50 %</td><td style='text-align:center;height:30px;'>C2</td><td style='text-align:center;height:30px;'>Above Average</td>";
        }
        else if (parseInt(count) == 6) {
            graderow = "<td style='text-align:center;height:30px;'>31 % to 40 %</td><td style='text-align:center;height:30px;'>D</td><td style='text-align:center;height:30px;'>Pass</td>";
        }
        else if (parseInt(count) == 7) {
            graderow = "<td style='text-align:center;height:30px;'>21 % to 30 %</td><td style='text-align:center;height:30px;'>E1</td><td style='text-align:center;height:30px;'>Not Satisfied</td>";
        }
        else if (parseInt(count) == 8) {
            graderow = "<td style='text-align:center;height:30px;'>21 % Below</td><td style='text-align:center;height:30px;'>E2</td><td style='text-align:center;height:30px;'>Not Satisfied</td>";
        }
        else {
            graderow = "<td style='text-align:center;height:30px;' colspan='3'></td><td style='text-align:center;height:30px;display:none'></td><td style='text-align:center;height:30px;display:none' ></td>";
        }
    }
    else if (medium == 1) {

        if (standard == "छोटा शिशु" || standard == "मोठा शिशु" || standard == "बालवर्ग" || standard == "१ ली" || standard == "२ री" || standard == "३ री" || standard == "४ थी") {
            if (parseInt(count) == 0) {
                graderow = "<td style='text-align:center;height:30px;'>९१  % ते १००  %</td><td style='text-align:center;height:30px;'>अ-१</td><td style='text-align:center;height:30px;'> उत्कृष्ट</td>";
            }
            else if (parseInt(count) == 1) {
                graderow = "<td style='text-align:center;height:30px;'>८१  % ते ९०  %</td><td style='text-align:center;height:30px;'>अ-२</td><td style='text-align:center;height:30px;'>उत्तम </td>";
            }
            else if (parseInt(count) == 2) {
                graderow = "<td style='text-align:center;height:30px;'>७१  % ते ८०  %</td><td style='text-align:center;height:30px;'>ब-१</td><td style='text-align:center;height:30px;'>चांगला </td>";
            }
            else if (parseInt(count) == 3) {
                graderow = "<td style='text-align:center;height:30px;'>६१  % ते ७०  %</td><td style='text-align:center;height:30px;'>ब-२</td><td style='text-align:center;height:30px;'>बरा</td>";
            }
            else if (parseInt(count) == 4) {
                graderow = "<td style='text-align:center;height:30px;'>५१  % ते ६०  %</td><td style='text-align:center;height:30px;'>क-१</td><td style='text-align:center;height:30px;'>समाधानकारक </td>";
            }
            else if (parseInt(count) == 5) {
                graderow = "<td style='text-align:center;height:30px;'>४१  % ते ५०  %</td><td style='text-align:center;height:30px;'>क-२</td><td style='text-align:center;height:30px;'>साधारण </td>";
            }
            else if (parseInt(count) == 6) {
                graderow = "<td style='text-align:center;height:30px;'>३१  % ते ४०  %</td><td style='text-align:center;height:30px;'>ड</td><td style='text-align:center;height:30px;'>असमाधानकारक </td>";
            }
            else {
                graderow = "<td style='text-align:center;height:30px;' colspan='3'></td><td style='text-align:center;height:30px;display:none'></td><td style='text-align:center;height:30px;display:none' ></td>";
            }

        }

        else {
            if (parseInt(count) == 0) {
                graderow = "<td style='text-align:center;height:30px;'>९१  % ते १००  %</td><td style='text-align:center;height:30px;'>अ-१</td><td style='text-align:center;height:30px;'>अति उत्कृष्ट</td>";
            }
            else if (parseInt(count) == 1) {
                graderow = "<td style='text-align:center;height:30px;'>८१  % ते ९०  %</td><td style='text-align:center;height:30px;'>अ-२</td><td style='text-align:center;height:30px;'>अनुकरणीय</td>";
            }
            else if (parseInt(count) == 2) {
                graderow = "<td style='text-align:center;height:30px;'>७१  % ते ८०  %</td><td style='text-align:center;height:30px;'>ब-१</td><td style='text-align:center;height:30px;'>उत्कृष्ट</td>";
            }
            else if (parseInt(count) == 3) {
                graderow = "<td style='text-align:center;height:30px;'>६१  % ते ७०  %</td><td style='text-align:center;height:30px;'>ब-२</td><td style='text-align:center;height:30px;'>उत्तम</td>";
            }
            else if (parseInt(count) == 4) {
                graderow = "<td style='text-align:center;height:30px;'>५१  % ते ६०  %</td><td style='text-align:center;height:30px;'>क-१</td><td style='text-align:center;height:30px;'>चांगला</td>";
            }
            else if (parseInt(count) == 5) {
                graderow = "<td style='text-align:center;height:30px;'>४१  % ते ५०  %</td><td style='text-align:center;height:30px;'>क-२</td><td style='text-align:center;height:30px;'>साधारण </td>";
            }
            else if (parseInt(count) == 6) {
                graderow = "<td style='text-align:center;height:30px;'>३१  % ते ४०  %</td><td style='text-align:center;height:30px;'>ड</td><td style='text-align:center;height:30px;'>उत्तीर्ण</td>";
            }
            else if (parseInt(count) == 7) {
                graderow = "<td style='text-align:center;height:30px;'>२१  % ते ३०  %</td><td style='text-align:center;height:30px;'>ई-१</td><td style='text-align:center;height:30px;'>असमाधानकारक</td>";
            }
            else if (parseInt(count) == 8) {
                graderow = "<td style='text-align:center;height:30px;'>२१  % खाली </td><td style='text-align:center;height:30px;'>ई-२</td><td style='text-align:center;height:30px;'>असमाधानकारक</td>";
            }
            else {
                graderow = "<td style='text-align:center;height:30px;' colspan='3'></td><td style='text-align:center;height:30px;display:none'></td><td style='text-align:center;height:30px;display:none' ></td>";
            }


        }


    }
    /*    test_var++;*/
    return graderow;

};

function gradeonly(total, medium) {
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
        else if (parseFloat(total) > 35 && parseFloat(total) < 41) {
            grade = "D";
        }
        else if (parseFloat(total) > 20 && parseFloat(total) < 35) {
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
        else if (parseFloat(total) > 35 && parseFloat(total) < 41) {
            grade = "ड";
        }
        else if (parseFloat(total) > 20 && parseFloat(total) < 35) {
            grade = "ई-१";
        }
        else {
            grade = "ई-२";
        }
    }
    return grade;
};


function class_promoted(standard) {
    if (standard == "MINIKG") {

        var number1 = "JRKG";
    }

    if (standard == "JRKG") {

        var number1 = "SRKG";
    }

    if (standard == "SRKG") {

        var number1 = "I";
    }

    if (standard == 1) {

        var number1 = "II";
    }

    if (standard == 2) {

        var number1 = "III";
    }

    if (standard == 3) {

        var number1 = "IV";
    }

    if (standard == 4) {

        var number1 = "V";
    }

    if (standard == 5) {

        var number1 = "VI";
    }

    if (standard == 6) {

        var number1 = "VII";
    }

    if (standard == 7) {

        var number1 = "VIII";
    }

    if (standard == 8) {

        var number1 = "IX";
    }


    if (standard == "छोटा शिशु") {

        var number1 = "मोठा शिशु";
    }
    if (standard == "मोठा शिशु") {

        var number1 = "बालवर्ग";
    }
    if (standard == "बालवर्ग") {

        var number1 = "I";
    }

    if (standard == "१ ली") {

        var number1 = "II";
    }
    if (standard == "२ री") {

        var number1 = "III";
    }
    if (standard == "३ री") {

        var number1 = "IV";
    }
    if (standard == "४ थी") {

        var number1 = "V";
    }
    if (standard == "५ वी") {

        var number1 = "VI";
    }
    if (standard == "६ वी") {

        var number1 = "VII";
    }
    if (standard == "७ वी") {

        var number1 = "VIII";
    }
    if (standard == "८ वी") {

        var number1 = "IX";
    }


    return number1;
}

function mardate(rollno) {
    var devanagariDigits = {
        '0': '०',
        '1': '१',
        '2': '२',
        '3': '३',
        '4': '४',
        '5': '५',
        '6': '६',
        '7': '७',
        '8': '८',
        '9': '९'
    };

    var mar_rollno = rollno.toString().replace(/[0123456789]/g, function (s) {
        return devanagariDigits[s];
    });

    return mar_rollno;
}




function margr(grno) {
    var devanagariDigits = {
        '0': '०',
        '1': '१',
        '2': '२',
        '3': '३',
        '4': '४',
        '5': '५',
        '6': '६',
        '7': '७',
        '8': '८',
        '9': '९'
    };

    var mar_rollno = grno.toString().replace(/[0123456789]/g, function (s) {
        return devanagariDigits[s];
    });

    return mar_rollno;
}


function Promoted_standard(standard) {
    if (standard == "९ वी") {
        standard = "१० वी";
    }
    if (standard == "८ वी") {
        standard = "९ वी";
    }
    if (standard == "७ वी") {
        standard = "८ वी";
    }
    if (standard == "६ वी") {
        standard = "७ वी";
    }
    if (standard == "५ वी") {
        standard = "६ वी";
    }
    if (standard == "४ थी") {
        standard = "५ वी";
    }
    if (standard == "३ री") {
        standard = "४ थी";
    }
    if (standard == "२ री") {
        standard = "३ री";
    }
    if (standard == "१ ली") {
        standard = "२ री";
    }
    if (standard == "बालवर्ग") {
        standard = "१ ली";

    }
    if (standard == "मोठा शिशु") {
        standard = "बालवर्ग";
    }
    if (standard == "छोटा शिशु") {
        standard = "मोठा शिशु";
    }

    if (standard == "9") {
        standard = "10";
    }
    if (standard == "8") {
        standard = "9";
    }
    if (standard == "7") {
        standard = "8";
    }
    if (standard == "6") {
        standard = "7";
    }
    if (standard == "5") {
        standard = "4";
    }
    if (standard == "4") {
        standard = "5";
    }
    if (standard == "3") {
        standard = "4";
    }
    if (standard == "2") {
        standard = "3";
    }
    if (standard == "1") {
        standard = "2";
    }
    if (standard == "SRKG") {
        standard = "1";

    }
    if (standard == "JRKG") {
        standard = "SRKG";
    }
    if (standard == "MINIKG") {
        standard = "JRKG";
    }

    return standard;
}

//function remarkonly(total, medium, flagremark) {
//    var remark = "";
//    /*if (medium == 2) {*/
//    if (parseFloat(total) > 90 && parseFloat(total) <= 100 && flagremark == "PASS") {
//        remark = "Outstanding";
//    }
//    else if (parseFloat(total) > 80 && parseFloat(total) < 91 && flagremark == "PASS") {
//        remark = "Exemplary";
//    }
//    else if (parseFloat(total) > 70 && parseFloat(total) < 81 && flagremark == "PASS") {
//        remark = "Excellent";
//    }
//    else if (parseFloat(total) > 60 && parseFloat(total) < 71 && flagremark == "PASS") {
//        remark = "Very Good";
//    }
//    else if (parseFloat(total) > 50 && parseFloat(total) < 61 && flagremark == "PASS") {
//        remark = "Good";
//    }
//    else if (parseFloat(total) > 40 && parseFloat(total) < 51 && flagremark == "PASS") {
//        remark = "Above Average";
//    }
//    else if (parseFloat(total) > 35 && parseFloat(total) < 41 && flagremark == "PASS") {
//        remark = "Pass";
//    }
//    else if (parseFloat(total) > 20 && parseFloat(total) < 35 && flagremark == "PASS") {
//        remark = "Not Satisfied";
//    }
//    else {
//        remark = "Not Satisfied";
//    }

//    return remark;
//};