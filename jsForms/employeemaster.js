
$(document).ready(function () {
    

    var nowY = new Date().getFullYear(),
    options = "";
    $("[id*=dtpyop]").empty().append('<option selected="selected" value="0">--Select--</option>');
    for(var Y=nowY; Y>=1980; Y--) {
        //options += "<option>"+ Y +"</option>";
        $("[id*=dtpyop]").append($("<option></option>").val(Y).html(Y));
    }

   // $("[id*=tbldepart]").empty();
   // $("[id*=tbldepart]").append("<thead><tr class='alert-info'><th>Department</th><th>Designation</th><th>Date of Joinig</th><th>Basic Salary</th><th>Criteria</th><th>Edit/Add</th></tr></thead>");
   // $("[id*=tbldepart]").append("<tbody><tr><td><select id='ddledudepart' class='form-control' disabled='disabled'></select></td><td><select id='ddledudesig' class='form-control' disabled='disabled'></select></td><td><input type='text' id='txtdatedept' placeholder='Select Date' class='form-control' name='date' disabled='disabled'/></td><td><input type='text' id='txtsalaryedu' class='form-control' onkeypress='return isNumber(event);' disabled='disabled' /></td><td><select id='ddlcriteria' class='form-control' disabled='disabled'><option>--Select--</option><option>PART TIME</option><option>FULL TIME</option></select></td><td><a href='#' disabled='disabled' id='btndetsave' class='ok btn btn-info btn-sm' title='Save'><span class='glyphicon glyphicon-ok'></span></a><a href='#' id='btneditdept' class='edit btn btn-info btn-sm' title='Cancel'><span class='glyphicon glyphicon-edit'></span></a><a class='add btn btn-info'><span class='glyphicon glyphicon-plus'></span></a></td></tr>");

    $.ajax({
        type: "POST",
        url: "EmployeeMaster.aspx/filldepartment",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        async: false,
        success: function (r) {
            $("[id*=ddldepart]").empty().append('<option selected="selected" value="0">--Select--</option>');
            $.each(r.d, function () {
                $("[id*=ddldepart]").append($("<option></option>").val(this['Value']).html(this['Text']));
            });

            $("[id*=ddledudepart]").empty().append('<option selected="selected" value="0">--Select--</option>');
            $.each(r.d, function () {
                $("[id*=ddledudepart]").append($("<option></option>").val(this['Value']).html(this['Text']));
            });
        }
    });

    $("[id*='txtempid']").val("");

    $.ajax({
        type: "POST",
        url: "EmployeeMaster.aspx/filldesignation",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        async: false,
        success: function (r) {
            $("[id*=ddldesig]").empty().append('<option selected="selected" value="0">--Select--</option>');
            $.each(r.d, function () {
                $("[id*=ddldesig]").append($("<option></option>").val(this['Value']).html(this['Text']));
            });

            $("[id*=ddledudesig]").empty().append('<option selected="selected" value="0">--Select--</option>');
            $.each(r.d, function () {
                $("[id*=ddledudesig]").append($("<option></option>").val(this['Value']).html(this['Text']));
            });
        }
    });
   // $("#dtpyop").append( options );
});

$(function () {
    $('input[id="txtdatedept"]').datetimepicker({
        changeMonth: false,
        changeYear: false,
        timepicker: false,
        format: 'd/m/Y',
        viewMode: "months",
        minViewMode: "months"
    }
    );
});

$("[id*='btn_sign']").on('click', function () {

    $("[id*=stud_sign]")[0].style.display = "block";
    $("[id*=btn_edit_sign]")[0].style.display = "block";

    $("[id*=btn_photo]")[0].style.display = "none";
    $("[id*=stud_photo]")[0].style.display = "none";

    //$(this).find('i').toggleClass('glyphicon-arrow-right').toggleClass('glyphicon-arrow-left');
});

$("[id*='btn_pic']").on('click', function () {

    $("[id*=stud_sign]")[0].style.display = "none";
    $("[id*=btn_edit_sign]")[0].style.display = "none";

    $("[id*=btn_photo]")[0].style.display = "block";
    $("[id*=stud_photo]")[0].style.display = "block";

});

$("#txtemail").change(function (e) {
    var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;

    if (reg.test($("[id*='txtemail']").val()) == false) {
        $.notify("Invalid Email ID.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
        $("#txtemail").focus();
        return false;
    }
    return true;
});


$("#get_photo").change(function () {
    readURL(this, 'imgPhoto');
});

$("#upload_sign").change(function () {
    readURL(this, 'signImg');
});

function readURL(input, imgID) {
    if (imgID == "imgPhoto") {
        var fileInput = document.getElementById('get_photo');
    }
    else {
        var fileInput = document.getElementById('upload_sign');
    }
    var filePath = fileInput.value;
    var allowedExtensions = /(\.jpg|\.jpeg|\.png|\.gif|\.bmp)$/i;
    if (!allowedExtensions.exec(filePath)) {
        alert('Please upload Valid image file');
        fileInput.value = '';
        return false;
    } else {
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#' + imgID + '').attr('src', e.target.result);
            }
            reader.readAsDataURL(input.files[0]);
        }
    }
}

var type = "Insert";
var dmltype = "Insert_Update";

$("[id*='btnsave']").on('click', function () {
    var fname = $("[id*='txt_fname']").val();
    var firstname = fname.replace(/\'/g, "\"");

    var mname = $("[id*='txt_mname']").val();
    var middlename = mname.replace(/\'/g, "\"");

    var lname = $("[id*='txt_lname']").val();
    var lastname = lname.replace(/\'/g, "\"");

    var mothname = $("[id*='txtmothname']").val();
     mothname = mothname.replace(/\'/g, "\"");

    var nationality = "";
    var category = "";
    var bloodgrp = "";
    var dob = $("#dtp_date").val();
    var doj = $("#dtpjoin").val();

    if ($("[id*=ddl_nation] :selected").text() == "--Select--") {
        nationality = "";
    }
    else {
        nationality = $("[id*=ddl_nation] :selected").text();
    }

    if ($("[id*=ddl_cat] :selected").text() == "--Select--") {
        category = "";
    }
    else {
        category = $("[id*=ddl_cat] :selected").text();
    }

    if ($("[id*=ddl_blood] :selected").text() == "--Select--") {
        bloodgrp = 0;
    }
    else {
        bloodgrp = $("[id*=ddl_blood]").val();
    }

    var handicaped;
    if ($("[id*='chkhandi']")[0].checked == true) {
        handicaped = 1;
    }
    else {
        handicaped = 0;
    }

    //$("[id*='ddl_gender']").val()
    var birthplc = $("[id*='txtbirth']").val();
    var marital = $("[id*='ddlmarital']").val();
    var email = $("[id*='txtemail']").val();
    var caste = $("[id*='txt_caste']").val();
    var subcast = $("[id*='txt_sc']").val();
    var aadhar = $("[id*='txtaadhar']").val();
    var address = $("#txt_address").val();
    address = address.replace(/'/g, '\'\'');
    var state = $("[id*='txt_state_a']").val();
    var city = $("[id*='txt_city_a']").val();
    var phoneno = $("[id*='txt_phn']").val();
    var pincode = $("[id*='txt_pin']").val();
    var telno = $("[id*='txttelno1']").val();

    var addressnat = $("#txt_add_nat").val();
    addressnat = addressnat.replace(/'/g, '\'\'');
    var statenat = $("[id*='txt_state_nat']").val();
    var citynat = $("[id*='txt_city_nat']").val();
    var phonenat = $("[id*='txt_phn_nat']").val();
    var pincodenat = $("[id*='txt_pin_nat']").val();
    var telnonat = $("[id*='txtnattel']").val();
    var pfno = $("#txtpfno").val();
    var panno = $("#txtpanno").val();
    var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;

    if (reg.test($("[id*='txtemail']").val()) == true){

        if (fname != "" && mname != "" && mothname != "" && dob != "" && email != "" && $("[id*=ddl_gender]")[0].selectedIndex != 0 && address != "" && state != "" && phoneno != "" && telno != "" && $("[id*=ddldepart]")[0].selectedIndex != 0 && $("[id*=ddldesig]")[0].selectedIndex != 0 && $("[id*='txtsalary']").val() != "" && $("#txtpfno").val() != "" && $("#txtpanno").val() != "" && $("#txtaadhar").val()!="") {

            $.ajax({
                type: "POST",
                url: "EmployeeMaster.aspx/savepersonaldata",
                data: '{fname:"' + fname + '",lname:"' + lname + '", mname:"' + mname + '", mothname:"' + mothname + '", dob:"' + dob + '", doj:"' + doj + '", gender:"' + $("[id*='ddl_gender']").val() + '", bldgrp:"' + bloodgrp + '", cat:"' + category + '", national:"' + nationality + '", marital:"' + marital + '", email:"' + email + '", caste:"' + caste + '", subcaste:"' + subcast + '", aadhar:"' + aadhar + '", address1:"' + address + '", city1:"' + city + '", state1:"' + state + '", pincode1:"' + pincode + '", phoneno1:"' + phoneno + '", telno1:"' + telno + '", address2:"' + addressnat + '", city2:"' + citynat + '", state2:"' + statenat + '", pincode2:"' + pincodenat + '", phoneno2:"' + phonenat + '", telno2:"' + telnonat + '", depart:"' + $("[id*='ddldepart']").val() + '", desig:"' + $("[id*='ddldesig']").val() + '", salary:"' + $("[id*='txtsalary']").val() + '",handicaped:"' + handicaped + '",empid:"' + $("[id*='txtempid']").val() + '",type:"' + type + '",pfno:"'+pfno+'",panno:"'+panno+'"}',
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d[0].msg != "Not Saved") {
                        $("[id*='txtempid']").val(data.d[0].empid);
                        //$("[id*='departdiv']").show();

                        //$("[id*=ddledudepart]").val($("[id*=ddldepart]").val());
                        //$("[id*=ddledudesig]").val($("[id*=ddldesig]").val());
                        //$("[id*='txtdatedept']").val(doj);
                        //$("[id*='txtsalary']").val($("[id*='txtsalary']").val());
                        //$("[id*=ddlcriteria]")[0].selectedIndex = 2;

                        type = "Update";
                        if (data.d[0].empid != "") {
                            var count = "EMP";
                            var img = document.getElementById("imgPhoto").src;
                            var ext = img.split(';')[0].match(/jpg|jpeg|png|gif/)[0];
                            // strip off the data: url prefix to get just the base64-encoded bytes
                            var stud_photo = img.replace(/^data:image\/\w+;base64,/, "");

                            //to save in folder
                            var get_photo = $("#get_photo").get(0);
                            var uploadedfiles = get_photo.files;


                            var fromdata = new FormData();
                            for (var i = 0; i < uploadedfiles.length; i++) {
                                fromdata.append(uploadedfiles[i].name, uploadedfiles[i]);
                            }

                            if (uploadedfiles.length > 0) {
                                $.ajax({
                                    url: 'uploadPhoto.ashx?ID=' + data.d[0].empid + '?pic',
                                    type: "POST",
                                    contentType: false,
                                    processData: false,
                                    data: fromdata,
                                    async: false,
                                    success: function (result) {
                                        if (result == 'File Uploaded Successfully!') {
                                            count = "EMPHOTO";
                                        }
                                        else {
                                            $('#edutab').trigger('click');
                                        }
                                    }
                                });
                            }

                            var img1 = document.getElementById("signImg").src;

                            var ext = img1.split(';')[0].match(/jpg|jpeg|png|gif/)[0];
                            // strip off the data: url prefix to get just the base64-encoded bytes
                            var stud_sign = img1.replace(/^data:image\/\w+;base64,/, "");

                            //to save in folder
                            var get_sign = $("#upload_sign").get(0);
                            var uploadedfiles = get_sign.files;

                            var fromdata = new FormData();
                            for (var i = 0; i < uploadedfiles.length; i++) {
                                fromdata.append(uploadedfiles[i].name, uploadedfiles[i]);
                            }

                            if (uploadedfiles.length > 0) {
                                $.ajax({
                                    url: 'uploadPhoto.ashx?ID=' + data.d[0].empid + '?sign',
                                    type: "POST",
                                    contentType: false,
                                    processData: false,
                                    data: fromdata,
                                    async: false,
                                    success: function (result) {
                                        if (result == 'File Uploaded Successfully!') {
                                            if (count == "EMPHOTO") {
                                                count = "EMPhOTOSIGN";
                                            }
                                            else {
                                                count = "EMPSIGN";
                                            }
                                        }
                                        else {
                                            $('#edutab').trigger('click');
                                        }
                                    }
                                });
                            }
                        }
                        if (count == "EMP") {
                            if (type == "Update") {
                                $.notify("Data Updated Successfully.(" + data.d[0].empid + ")", { color: "#fff", background: "#127515", blur: 0.2, delay: 0 });
                            }
                            else {
                                $.notify("Data Saved Successfully.Employee ID is " + data.d[0].empid + "", { color: "#fff", background: "#127515", blur: 0.2, delay: 0 });
                            }
                        }
                        else if (count == "EMPHOTO") {
                            if (type == "Update") {
                                $.notify("Data and Photo Updated Successfully.(" + data.d[0].empid + ")", { color: "#fff", background: "#127515", blur: 0.2, delay: 0 });
                            }
                            else {
                                $.notify("Data and Photo Saved Successfully.Employee ID is " + data.d[0].empid + "", { color: "#fff", background: "#127515", blur: 0.2, delay: 0 });
                            }
                        }
                        else if (count == "EMPhOTOSIGN") {
                            if (type == "Update") {
                                $.notify("Data and Photo/Sign Updated Successfully.(" + data.d[0].empid + ")", { color: "#fff", background: "#127515", blur: 0.2, delay: 0 });
                            }
                            else {
                                $.notify("Data and Photo/Sign Saved Successfully.Employee ID is " + data.d[0].empid + "", { color: "#fff", background: "#127515", blur: 0.2, delay: 0 });
                            }
                        }
                        else if (count == "EMPSIGN") {
                            if (type == "Update") {
                                $.notify("Data and Sign Updated Successfully.(" + data.d[0].empid + ")", { color: "#fff", background: "#127515", blur: 0.2, delay: 0 });
                            }
                            else {
                                $.notify("Data and Sign Saved Successfully.Employee ID is " + data.d[0].empid + "", { color: "#fff", background: "#127515", blur: 0.2, delay: 0 });
                            }
                        }
                        else {
                        }


                        $.ajax({
                            type: "POST",
                            url: "EmployeeMaster.aspx/showeducation",
                            data: '{empid:"' + $("[id*='txtempid']").val() + '"}',
                            contentType: "application/json; charset=utf-8",
                            async: false,
                            success: function (data) {
                                if (data.d.length > 0 && data.d[0].msg != "No Data Found") {
                                    $("[id*='dgv_load']").empty();
                                    for (var i = 0; i < data.d.length; i++) {
                                        if (i == 0) {
                                            $("[id*='dgv_load']").append("<thead><tr class='alert-info'><th style='display:none;'>ID</th><th>College Name</th><th>Board/University</th><th>Degree Name</th><th>Degree Type</th><th>Specialization Subject</th><th>Month of Passing</th><th>Year of Passing</th><th>Marks Obtained</th><th>Total Marks</th><th>Class</th><th>Pursuing</th><th></th></tr></thead>");
                                            $("[id*='dgv_load']").append("<tbody>");
                                        }
                                        $("[id*='dgv_load']").append("<tr><td style='display:none;'>" + data.d[i].empid + "</td><td>" + data.d[i].colgname + "</td><td>" + data.d[i].boardname + "</td><td>" + data.d[i].degname + "</td><td>" + data.d[i].degtype + "</td><td>" + data.d[i].subject + "</td><td>" + data.d[i].mop + "</td><td>" + data.d[i].yop + "</td><td>" + data.d[i].obtmk + "</td><td>" + data.d[i].totmrk + "</td><td>" + data.d[i].class1 + "</td><td>" + data.d[i].pursuing + "</td><td><a href='#' class='Select'>SELECT</a></td></tr>");
                                        if (i == data.d.length - 1) {
                                            $("[id*='dgv_load']").append("</tbody>");
                                        }
                                    }
                                }
                                else {

                                }
                            }
                        });

                        $("#txtsalaryedu").val("");
                        $.ajax({
                            type: "POST",
                            url: "EmployeeMaster.aspx/getdepartdata",
                            data: '{empid:"' + $("[id*='txtempid']").val() + '"}',
                            contentType: "application/json; charset=utf-8",
                            async: false,
                            success: function (data) {
                                if (data.d.length > 0 && data.d[0].msg != "No Data Found") {
                                    $("[id*=tbldepart]").empty();
                                    for (var i = 0; i < data.d.length; i++) {
                                        if (i == 0) {
                                            $("[id*=tbldepart]").append("<thead><tr class='alert-info'><th style='display:none;'></th><th>Department</th><th>Designation</th><th>Date of Joinig</th><th>Basic Salary</th><th>Criteria</th><th>Edit</th></tr></thead>");
                                            $("[id*='tbldepart']").append("<tbody>");
                                        }
                                        $("[id*='tbldepart']").append("<tr><td style='display:none;'>" + data.d[i].empid + "</td><td>" + data.d[i].depart + "</td><td style='display:none;'>" + data.d[i].deptid + "</td><td>" + data.d[i].desig + "</td><td style='display:none;'>" + data.d[i].desid + "</td><td>" + data.d[i].doj + "</td><td>" + data.d[i].salary + "</td><td>" + data.d[i].jobtype + "</td><td><a href='#' class='Select'>SELECT</a></td></tr>");
                                        if (i == data.d.length - 1) {
                                            $("[id*='tbldepart']").append("</tbody>");
                                        }
                                    }
                                    $("#txtsalaryedu").val("");
                                }
                                else {

                                }
                            }
                        });

                    }
                    else {
                        $.notify("Data Not Saved.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                    }
                }
            });
        }
        else {
            $.notify("Please Fill Required Details.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
        }
    }
    else {
        $.notify("Invalid Email ID.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
    }
});

$("[id*='chk_native']").on("click", function () {
    
    if ($("[id*='chk_native']")[0].checked == true) {
        $("[id*='txt_add_nat']").val($("#txt_address").val());
        $("[id*='txt_state_nat']").val($("[id*='txt_state_a']").val());
        $("[id*='txt_city_nat']").val($("[id*='txt_city_a']").val());
        $("[id*='txt_phn_nat']").val($("[id*='txt_phn']").val());
        $("[id*='txt_pin_nat']").val($("[id*='txt_pin']").val());
        $("[id*='txtnattel']").val($("[id*='txttelno1']").val());
    }
    else {
        $("[id*='txt_add_nat']").val("");
        $("[id*='txt_state_nat']").val("");
        $("[id*='txt_city_nat']").val("");
        $("[id*='txt_phn_nat']").val("");
        $("[id*='txt_pin_nat']").val("");
        $("[id*='txtnattel']").val("");
    }

});
var empid;
$("[id*='btn_upload']").on("click", function () {
    var img = document.getElementById("imgPhoto").src;
    document.getElementById("stud_photo").src = img;
    $('#uplodpic').modal("hide");
});

$("[id*='btn_save_sign']").on("click", function () {
    var img = document.getElementById("signImg").src;
    document.getElementById("stud_sign").src = img;
    $('#sign_modal').modal("hide");
});
var employeeid = "";

$("#txtSearch").on('keydown', function (e) {
    if (e.which == 13) {
        $("#btn_ok").trigger('click');
        //document.getElementById("btn_ok").click();
        return false;
    }
});

$("[id*='btn_ok']").on("click", function () {
    $("[id*=ddldepart]").prop("disabled","disabled");
    $("[id*=ddldesig]").prop("disabled", "disabled");

    $("#txtsalaryedu").val("");
    if ($("#txtSearch").val() != "") {
        $.ajax({
            type: "POST",
            url: "EmployeeMaster.aspx/getempdata",
            data: '{inputval:"' + $("#txtSearch").val() + '"}',
            contentType: "application/json; charset=utf-8",
            async: false,
            success: function (data) {
                if (data.d.length > 0 && data.d[0].msg != "No Data Found") {
                    $('#employee').trigger('click');
                   // $("[id*='departdiv']").hide();
                    if (data.d[0].countemp > 1) {
                        $("[id*=tblempsearch]").empty();
                        $("[id*=tblempsearch]").append("<tr class='alert-info'><th style='display:none;'>ID</th><th>First Name</th><th>Last Name</th><th>Father Name</th><th>Mother Name</th><th></th></tr>");
                        for (var i = 0; i < data.d.length; i++) {
                            if (i == 0) {
                                $("[id*=tblempsearch]").append("<tbody>");
                            }
                            $("[id*=tblempsearch]").append("<tr><td style='display:none;'>" + data.d[i].empid + "</td><td>" + data.d[i].fname + "</td><td>" + data.d[i].lname + "</td><td>" + data.d[i].mname + "</td><td>" + data.d[i].mothname + "</td><td><a href='#' class='Select'>SELECT</a></td></tr>");
                            if (i == data.d.length) {
                                $("[id*=tblempsearch]").append("</tbody>");
                            }
                        }
                        $('#searchmodal').modal("show");
                    }
                    else {
                        //$("[id*='departdiv']").show();

                        $("#txtSearch").val("");
                        $("[id*='btnsave']")[0].innerText = "Update";
                        type = "Update";
                        $('#searchmodal').modal("hide");
                        $("[id*='txt_add_nat']").val(data.d[0].address2);
                        $("[id*='txt_state_nat']").val(data.d[0].state2);
                        // $("[id*='txt_city_nat']").val(data.d[0].);
                        $("[id*='txt_phn_nat']").val(data.d[0].phoneno2);
                        $("[id*='txt_pin_nat']").val(data.d[0].pincode2);
                        $("[id*='txtnattel']").val(data.d[0].telno2);

                        $("[id*='txt_address']").val(data.d[0].address1);
                        $("[id*='txt_state_a']").val(data.d[0].state1);
                        // $("[id*='txt_city_a']").val(data.d[0].);
                        $("[id*='txt_phn']").val(data.d[0].phoneno1);
                        $("[id*='txt_pin']").val(data.d[0].pincode1);
                        $("[id*='txttelno1']").val(data.d[0].telno1);

                        $("[id*='txt_fname']").val(data.d[0].fname);
                        $("[id*='txt_mname']").val(data.d[0].mname);
                        $("[id*='txt_lname']").val(data.d[0].lname);

                        $("[id*='txtmothname']").val(data.d[0].mothname);
                        $("[id*='txtemail']").val(data.d[0].email);
                        $("[id*='txt_caste']").val(data.d[0].caste);
                        //$("[id*='txt_sc']").val(data.d[0].);
                        $("[id*='txtaadhar']").val(data.d[0].aadhar);
                        $("[id*='txtpanno']").val(data.d[0].panno);
                        $("[id*='txtpfno']").val(data.d[0].pfno);
                        $("[id*='txtempid']").val(data.d[0].empid);

                        if (data.d[0].gender == "False") {
                            $("[id*=ddl_gender]")[0].selectedIndex = 1;
                        }
                        else {
                            $("[id*=ddl_gender]")[0].selectedIndex = 2;
                        }

                        if (data.d[0].marital == "False") {
                            $("[id*=ddlmarital]")[0].selectedIndex = 1;
                        }
                        else {
                            $("[id*=ddlmarital]")[0].selectedIndex = 2;
                        }

                        // $("[id*=ddlcriteria]").val(data.d[0].jobtype);
                        // $("[id*=ddledudepart]").val(data.d[0].depart);
                        //  $("[id*=ddledudesig]").val(data.d[0].desig);
                        //  $("[id*='txtdatedept']").val(data.d[0].doj);

                        $("[id*=ddldepart]").val(data.d[0].depart);
                        $("[id*=ddldesig]").val(data.d[0].desig);

                        $("[id*='dtp_date']").val(data.d[0].dob);
                        $("[id*='dtpjoin']").val(data.d[0].doj);
                        if (data.d[0].national != "") {
                            $("[id*=ddl_nation]").val(data.d[0].national);
                        }
                        else {
                            $("[id*=ddl_nation]")[0].selectedIndex = 0;
                        }

                        if (data.d[0].cat == "") {
                            $("[id*=ddl_cat]")[0].selectedIndex = 0;
                        }
                        else {
                            $("[id*=ddl_cat]").val(data.d[0].cat);
                        }

                        if (data.d[0].bldgrp == "-1") {
                            $("[id*=ddl_blood]")[0].selectedIndex = 0;
                        }
                        else {
                            $("[id*=ddl_blood]").val(data.d[0].bldgrp);
                        }

                        if (data.d[0].handicap == true) {
                            $("[id*='chkhandi']")[0].checked == true;
                        }
                        else {
                            $("[id*='chkhandi']")[0].checked == false;
                        }
                        $("[id*='txtsalary']").val(data.d[0].salary);

                        $.ajax({
                            type: "POST",
                            url: "EmployeeMaster.aspx/showeducation",
                            data: '{empid:"' + $("[id*='txtempid']").val() + '"}',
                            contentType: "application/json; charset=utf-8",
                            async: false,
                            success: function (data) {
                                if (data.d.length > 0 && data.d[0].msg != "No Data Found") {
                                    $("[id*='dgv_load']").empty();
                                    for (var i = 0; i < data.d.length; i++) {
                                        if (i == 0) {
                                            $("[id*='dgv_load']").append("<thead><tr class='alert-info'><th style='display:none;'>ID</th><th>College Name</th><th>Board/University</th><th>Degree Name</th><th>Degree Type</th><th>Specialization Subject</th><th>Month of Passing</th><th>Year of Passing</th><th>Marks Obtained</th><th>Total Marks</th><th>Class</th><th>Pursuing</th><th></th></tr></thead>");
                                            $("[id*='dgv_load']").append("<tbody>");
                                        }
                                        $("[id*='dgv_load']").append("<tr><td style='display:none;'>" + data.d[i].empid + "</td><td>" + data.d[i].colgname + "</td><td>" + data.d[i].boardname + "</td><td>" + data.d[i].degname + "</td><td>" + data.d[i].degtype + "</td><td>" + data.d[i].subject + "</td><td>" + data.d[i].mop + "</td><td>" + data.d[i].yop + "</td><td>" + data.d[i].obtmk + "</td><td>" + data.d[i].totmrk + "</td><td>" + data.d[i].class1 + "</td><td>" + data.d[i].pursuing + "</td><td><a href='#' class='Select'>SELECT</a></td></tr>");
                                        if (i == data.d.length - 1) {
                                            $("[id*='dgv_load']").append("</tbody>");
                                        }
                                    }
                                }
                                else {

                                }
                            }
                        });

                        $.ajax({
                            type: "POST",
                            url: "EmployeeMaster.aspx/getdepartdata",
                            data: '{empid:"' + $("[id*='txtempid']").val() + '"}',
                            contentType: "application/json; charset=utf-8",
                            async: false,
                            success: function (data) {
                                if (data.d.length > 0 && data.d[0].msg != "No Data Found") {
                                    $("[id*=tbldepart]").empty();
                                    for (var i = 0; i < data.d.length; i++) {
                                        if (i == 0) {
                                            $("[id*=tbldepart]").append("<thead><tr class='alert-info'><th style='display:none;'></th><th>Department</th><th>Designation</th><th>Date of Joinig</th><th>Basic Salary</th><th>Criteria</th><th>Edit</th></tr></thead>");
                                            $("[id*='tbldepart']").append("<tbody>");
                                        }
                                        $("[id*='tbldepart']").append("<tr><td style='display:none;'>" + data.d[i].empid + "</td><td>" + data.d[i].depart + "</td><td style='display:none;'>" + data.d[i].deptid + "</td><td>" + data.d[i].desig + "</td><td style='display:none;'>" + data.d[i].desid + "</td><td>" + data.d[i].doj + "</td><td>" + data.d[i].salary + "</td><td>" + data.d[i].jobtype + "</td><td><a href='#' class='Select'>SELECT</a></td></tr>");
                                        if (i == data.d.length - 1) {
                                            $("[id*='tbldepart']").append("</tbody>");
                                        }
                                    }
                                }
                                else {

                                }
                            }
                        });

                        $.ajax({
                            type: "POST",
                            url: "EmployeeMaster.aspx/getaccountdata",
                            data: '{empid:"' + $("[id*='txtempid']").val() + '"}',
                            contentType: "application/json; charset=utf-8",
                            async: false,
                            success: function (data) {
                                if (data.d.length > 0 && data.d[0].msg != "No Data Found") {
                                    $("[id*=dgvaccount]").empty();
                                    for (var i = 0; i < data.d.length; i++) {
                                        if (i == 0) {
                                            $("[id*=dgvaccount]").append("<thead><tr class='alert-info'><th style='display:none;'></th><th>Account No.</th><th>Account Type</th><th>Bank Name</th><th>Branch Name</th><th>Salary Account</th><th>Edit</th></tr></thead>");
                                            $("[id*='dgvaccount']").append("<tbody>");
                                        }
                                        $("[id*='dgvaccount']").append("<tr><td style='display:none;'>" + data.d[i].empid + "</td><td>" + data.d[i].accntno + "</td><td>" + data.d[i].acntype + "</td><td>" + data.d[i].bnkname + "</td><td>" + data.d[i].branch + "</td><td>" + data.d[i].isalary + "</td><td><a href='#' class='Select'>SELECT</a></td></tr>");
                                        if (i == data.d.length - 1) {
                                            $("[id*='dgvaccount']").append("</tbody>");
                                        }
                                    }
                                }
                                else {

                                }
                            }
                        });

                        $.ajax({
                            type: "POST",
                            url: "EmployeeMaster.aspx/getexpdata",
                            data: '{empid:"' + $("[id*='txtempid']").val() + '"}',
                            contentType: "application/json; charset=utf-8",
                            async: false,
                            success: function (data) {
                                if (data.d.length > 0 && data.d[0].msg != "No Data Found") {
                                    $("[id*=tblexp]").empty();
                                    for (var i = 0; i < data.d.length; i++) {
                                        if (i == 0) {
                                            $("[id*=tblexp]").append("<thead><tr class='alert-info'><th style='display:none;'></th><th>Name of Company</th><th>Department</th><th>Designation</th><th>Previous Salary</th><th>Job From Date</th><th>Job To Date</th><th>Edit</th></tr></thead>");
                                            $("[id*='tblexp']").append("<tbody>");
                                        }
                                        $("[id*='tblexp']").append("<tr><td style='display:none;'>" + data.d[i].empid + "</td><td>" + data.d[i].comp + "</td><td>" + data.d[i].depart + "</td><td>" + data.d[i].desig + "</td><td>" + data.d[i].prvsal + "</td><td>" + data.d[i].jfdate + "</td><td>" + data.d[i].jtdate + "</td><td><a href='#' class='Select'>SELECT</a></td></tr>");
                                        if (i == data.d.length - 1) {
                                            $("[id*='tblexp']").append("</tbody>");
                                        }
                                    }
                                }
                                else {

                                }
                            }
                        });

                        $.ajax({
                            type: "POST",
                            url: "EmployeeMaster.aspx/getempPhoto",
                            data: '{empid:"' + $("[id*='txtempid']").val() + '"}',
                            async: false,
                            contentType: "application/json; charset=utf-8",
                            success: function (data) {
                                // for (var i = 0; i < data.length; i++) {
                                if (data.d[0].msg == "Uploaded") {
                                    if (data.d[0].photo != "") {
                                        document.getElementById("stud_photo").src = data.d[0].photo;
                                    }
                                    else {
                                        $.notify("Photo Not Found..!!", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                                    }
                                    if (data.d[0].sign != "") {
                                        document.getElementById("stud_sign").src = data.d[0].sign;
                                    }
                                    else {
                                        $.notify("Sign Not Found..!!", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                                    }
                                    //document.getElementById("stud_photo").setAttribute("src", path);
                                }
                                else {
                                    //$.notify("Photo Not Found..!!", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                                }
                                //}
                            }
                        });
                    }
                }
                else {
                    $.notify("No Data Found.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                }
            }
        });
    }
    else {
        $.notify("Please Provide Input To Search.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
    }
});

$("[id*='new_std']").on('click', function () {
    if ($("[id*='txt_fname']").val() != "" && $("[id*='txt_lname']").val() != "" && $("[id*='txtemail']").val() != "") {
        $('#newemp').modal("show");
    }
    else {
        clearall();
    }
});

$("[id*='btnyes']").on('click', function () {
    clearall();
    $('#employee').trigger('click');
});

function clearall() {
    type = "Insert";
    //$("[id*='departdiv']").hide();
    $("[id*=ddledudepart]")[0].selectedIndex = 0;
    $("[id*=ddledudesig]")[0].selectedIndex = 0;
    $("#txtsalaryedu").val("");
    $("#txtdatedept").val("");
    $("#txtcurrdep").val("");
    $("[id*=ddlcriteria]")[0].selectedIndex = 0;
    $("[id*=tbldepart]").empty();
    $("[id*='btnsave']")[0].innerText = "Save";
    $("[id*=ddldepart]").removeAttr("disabled");
    $("[id*=ddldesig]").removeAttr("disabled");
    $('#stud_photo').attr('src', "images/user.png");
    $('#stud_sign').attr('src', "images/sign.jpg");
    $("[id*='txt_add_nat']").val("");
    $("[id*='txt_state_nat']").val("");
    $("[id*='txt_city_nat']").val("");
    $("[id*='txt_phn_nat']").val("");
    $("[id*='txt_pin_nat']").val("");
    $("[id*='txtnattel']").val("");
    $("[id*=ddlmarital]")[0].selectedIndex = 0;
    $("[id*='txt_address']").val("");
    $("[id*='txt_state_a']").val("");
    $("[id*='txt_city_a']").val("");
    $("[id*='txt_phn']").val("");
    $("[id*='txt_pin']").val("");
    $("[id*='txttelno1']").val("");
    $("[id*='txtaadhar']").val("");
    $("[id*='txtpanno']").val("");
    $("[id*='txtpfno']").val("");
    $("[id*='txt_fname']").val("");
    $("[id*='txt_mname']").val("");
    $("[id*='txt_lname']").val("");

    $("[id*='txtmothname']").val("");
    $("[id*='txtemail']").val("");
    $("[id*='txt_caste']").val("");
    $("[id*='txt_sc']").val("");
    $("[id*='txtaadhar']").val("");
    $("[id*='txtempid']").val("");

    $("[id*=ddl_gender]")[0].selectedIndex = 0 ;
    $("[id*=ddldepart]")[0].selectedIndex = 0; 
    $("[id*=ddldesig]")[0].selectedIndex = 0;

    $("[id*='dtp_date']").val("");
    $("[id*='dtpjoin']").val("");

    $("[id*=ddl_nation]")[0].selectedIndex = 0;
    $("[id*=ddl_cat]")[0].selectedIndex = 0;
    $("[id*=ddl_blood]")[0].selectedIndex = 0;
    $("[id*=ddl_gender]")[0].selectedIndex = 0;
    $("[id*='chkhandi']")[0].checked == false;
    $("[id*='chk_native']")[0].checked == false;
    $("[id*='txtsalary']").val("");

    $("[id*=ddledudepart]")[0].selectedIndex = 0;
    $("[id*=ddledudesig]")[0].selectedIndex = 0;
    $("#txtsalaryedu").val("");
    $("#txtdatedept").val("");
    $("#txtcurrdep").val("");
    $("[id*=ddlcriteria]")[0].selectedIndex = 0;

    $("[id*=ddledudepart]").removeAttr("disabled");
    $("[id*=ddledudesig]").removeAttr("disabled");
    $("#txtcurrdep").removeAttr("disabled");

    $("#txtcolgname").val("");
    $("#txtobt").val("");
    $("#txtmarks").val("");
    $("[id*='chkpur']")[0].checked == false;
    $("#txtboard").val("");
    $("#txtdegree").val("");
    $("#txtdegty").val("");
    $("[id*='txtdegty']").removeAttr("disabled");
    $("#txtsubject").val("");
    $("[id*='ddlclass']")[0].selectedIndex = 0;
    $("[id*='dtpyop']")[0].selectedIndex = 0;
    $("[id*='ddlmonth']")[0].selectedIndex = 0;

    $("[id*='chkisalary']")[0].checked = false;
    $("[id*='txtempid']").val("");
    $("[id*='txtaccno']").val("");
    $("[id*='txtbnkname']").val("");
    $("[id*=ddlacctype]")[0].selectedIndex == 0;
    $("#txtbranch").val("");
    $("#txtaccno").removeAttr("disabled");
    $("[id*='txtorg']").val("");
    $("[id*='txtexpdept']").val("");
    $("[id*='txtexpdes']").val("");
    $("[id*='txtpresal']").val("");
    $("[id*='txtexpdoj']").val("");
    $("[id*='txtexplev']").val("");
    $("#txtorg").removeAttr("disabled");
    $("#txtexpdoj").removeAttr("disabled");
    $("#txtexplev").removeAttr("disabled");
    $("[id*=tbldepart]").empty();
    $("[id*=dgv_load]").empty();
    $("[id*=dgvaccount]").empty();
    $("[id*=tblexp]").empty();
}

$("[id*='btnclear']").on('click', function () {
    clearall();
});

$("[id*=tblempsearch]").on('click', 'td a.Select', function () {
    
    $("[id*='btnsave']")[0].innerText = "Update";
    type = "Update";
    $("#txtSearch").val("");
    var $td = $(this).closest('tr').children('td');
    var id = $td.eq(0).text();

    $.ajax({
        type: "POST",
        url: "EmployeeMaster.aspx/getempdata",
        data: '{inputval:"' + id + '"}',
        contentType: "application/json; charset=utf-8",
        async: false,
        success: function (data) {
            if (data.d.length > 0 && data.d[0].msg == "") {
                $("[id*=ddlcriteria]").val(data.d[0].jobtype);
                $('#searchmodal').modal("hide");
                $("[id*='txt_add_nat']").val(data.d[0].address2);
                $("[id*='txt_state_nat']").val(data.d[0].state2);
                // $("[id*='txt_city_nat']").val(data.d[0].);
                $("[id*='txt_phn_nat']").val(data.d[0].phoneno2);
                $("[id*='txt_pin_nat']").val(data.d[0].pincode2);
                $("[id*='txtnattel']").val(data.d[0].telno2);

                $("[id*='txt_address']").val(data.d[0].address1);
                $("[id*='txt_state_a']").val(data.d[0].state1);
                // $("[id*='txt_city_a']").val(data.d[0].);
                $("[id*='txt_phn']").val(data.d[0].phoneno1);
                $("[id*='txt_pin']").val(data.d[0].pincode1);
                $("[id*='txttelno1']").val(data.d[0].telno1);

                $("[id*='txt_fname']").val(data.d[0].fname);
                $("[id*='txt_mname']").val(data.d[0].mname);
                $("[id*='txt_lname']").val(data.d[0].lname);

                $("[id*='txtmothname']").val(data.d[0].mothname);
                $("[id*='txtemail']").val(data.d[0].email);
                $("[id*='txt_caste']").val(data.d[0].caste);
                //$("[id*='txt_sc']").val(data.d[0].);
                //$("[id*='txtaadhar']").val(data.d[0].);
                $("[id*='txtaadhar']").val(data.d[0].aadhar);
                $("[id*='txtpanno']").val(data.d[0].panno);
                $("[id*='txtpfno']").val(data.d[0].pfno);
                $("[id*='txtempid']").val(data.d[0].empid);
                if (data.d[0].marital == "False") {
                    $("[id*=ddlmarital]")[0].selectedIndex = 1;
                }
                else {
                    $("[id*=ddlmarital]")[0].selectedIndex = 2;
                }
                if (data.d[0].gender == "False") {
                    $("[id*=ddl_gender]")[0].selectedIndex=1;
                }
                else {
                    $("[id*=ddl_gender]")[0].selectedIndex = 2;
                }
                $("[id*=ddldepart]").val(data.d[0].depart);
                $("[id*=ddldesig]").val(data.d[0].desig);

                $("[id*='dtp_date']").val(data.d[0].dob);
                $("[id*='dtpjoin']").val(data.d[0].doj);
                if (data.d[0].national != "") {
                    $("[id*=ddl_nation]").val(data.d[0].national);
                }
                else {
                    $("[id*=ddl_nation]")[0].selectedIndex = 0;
                }

                //$("[id*='departdiv']").show();

                //$("[id*=ddledudepart]").val(data.d[0].depart);
                //$("[id*=ddledudesig]").val(data.d[0].desig);
                //$("[id*='txtdatedept']").val(data.d[0].doj);
                //$("[id*='txtsalary']").val(data.d[0].salary);
                //$("[id*=ddlcriteria]")[0].selectedIndex = 2;
                if (data.d[0].cat == "") {
                    $("[id*=ddl_cat]")[0].selectedIndex = 0;
                }
                else {
                    $("[id*=ddl_cat]").val(data.d[0].cat);
                }
                //$("[id*=ddl_blood]").val(data.d[0].bldgrp);
                if (data.d[0].bldgrp == "-1") {
                    $("[id*=ddl_blood]")[0].selectedIndex = 0;
                }
                else {
                    $("[id*=ddl_blood]").val(data.d[0].bldgrp);
                }

                if (data.d[0].handicap != "False") {
                    $("[id*='chkhandi']")[0].checked == true;
                }
                else {
                    $("[id*='chkhandi']")[0].checked == false;
                }

                $("[id*='txtsalary']").val(data.d[0].salary);

                $.ajax({
                    type: "POST",
                    url: "EmployeeMaster.aspx/showeducation",
                    data: '{empid:"' + $("[id*='txtempid']").val() + '"}',
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    success: function (data) {
                        if (data.d.length > 0 && data.d[0].msg != "No Data Found") {
                            $("[id*='dgv_load']").empty();
                            for (var i = 0; i < data.d.length; i++) {
                                if (i == 0) {
                                    $("[id*='dgv_load']").append("<thead><tr class='alert-info'><th style='display:none;'>ID</th><th>College Name</th><th>Board/University</th><th>Degree Name</th><th>Degree Type</th><th>Specialization Subject</th><th>Month of Passing</th><th>Year of Passing</th><th>Marks Obtained</th><th>Total Marks</th><th>Class</th><th>Pursuing</th><th></th></tr></thead>");
                                    $("[id*='dgv_load']").append("<tbody>");
                                }
                                $("[id*='dgv_load']").append("<tr><td style='display:none;'>" + data.d[i].empid + "</td><td>" + data.d[i].colgname + "</td><td>" + data.d[i].boardname + "</td><td>" + data.d[i].degname + "</td><td>" + data.d[i].degtype + "</td><td>" + data.d[i].subject + "</td><td>" + data.d[i].mop + "</td><td>" + data.d[i].yop + "</td><td>" + data.d[i].obtmk + "</td><td>" + data.d[i].totmrk + "</td><td>" + data.d[i].class1 + "</td><td>" + data.d[i].pursuing + "</td><td><a href='#' class='Select'>SELECT</a></td></tr>");
                                if (i == data.d.length - 1) {
                                    $("[id*='dgv_load']").append("</tbody>");
                                }
                            }
                        }
                        else {

                        }
                    }
                });

                $.ajax({
                    type: "POST",
                    url: "EmployeeMaster.aspx/getdepartdata",
                    data: '{empid:"' + $("[id*='txtempid']").val() + '"}',
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    success: function (data) {
                        if (data.d.length > 0 && data.d[0].msg != "No Data Found") {
                            $("[id*=tbldepart]").empty();
                            for (var i = 0; i < data.d.length; i++) {
                                if (i == 0) {
                                    $("[id*=tbldepart]").append("<thead><tr class='alert-info'><th style='display:none;'></th><th>Department</th><th>Designation</th><th>Date of Joinig</th><th>Basic Salary</th><th>Criteria</th><th>Edit</th></tr></thead>");
                                    $("[id*='tbldepart']").append("<tbody>");
                                }
                                $("[id*='tbldepart']").append("<tr><td style='display:none;'>" + data.d[i].empid + "</td><td>" + data.d[i].depart + "</td><td style='display:none;'>" + data.d[i].deptid + "</td><td>" + data.d[i].desig + "</td><td style='display:none;'>" + data.d[i].desid + "</td><td>" + data.d[i].doj + "</td><td>" + data.d[i].salary + "</td><td>" + data.d[i].jobtype + "</td><td><a href='#' class='Select'>SELECT</a></td></tr>");
                                if (i == data.d.length - 1) {
                                    $("[id*='tbldepart']").append("</tbody>");
                                }
                            }
                        }
                        else {

                        }
                    }
                });

                $.ajax({
                    type: "POST",
                    url: "EmployeeMaster.aspx/getaccountdata",
                    data: '{empid:"' + $("[id*='txtempid']").val() + '"}',
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    success: function (data) {
                        if (data.d.length > 0 && data.d[0].msg != "No Data Found") {
                            $("[id*=dgvaccount]").empty();
                            for (var i = 0; i < data.d.length; i++) {
                                if (i == 0) {
                                    $("[id*=dgvaccount]").append("<thead><tr class='alert-info'><th style='display:none;'></th><th>Account No.</th><th>Account Type</th><th>Bank Name</th><th>Branch Name</th><th>Salary Account</th><th>Edit</th></tr></thead>");
                                    $("[id*='dgvaccount']").append("<tbody>");
                                }
                                $("[id*='dgvaccount']").append("<tr><td style='display:none;'>" + data.d[i].empid + "</td><td>" + data.d[i].accntno + "</td><td>" + data.d[i].acntype + "</td><td>" + data.d[i].bnkname + "</td><td>" + data.d[i].branch + "</td><td>" + data.d[i].isalary + "</td><td><a href='#' class='Select'>SELECT</a></td></tr>");
                                if (i == data.d.length - 1) {
                                    $("[id*='dgvaccount']").append("</tbody>");
                                }
                            }
                        }
                        else {

                        }
                    }
                });

                $.ajax({
                    type: "POST",
                    url: "EmployeeMaster.aspx/getexpdata",
                    data: '{empid:"' + $("[id*='txtempid']").val() + '"}',
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    success: function (data) {
                        if (data.d.length > 0 && data.d[0].msg != "No Data Found") {
                            $("[id*=tblexp]").empty();
                            for (var i = 0; i < data.d.length; i++) {
                                if (i == 0) {
                                    $("[id*=tblexp]").append("<thead><tr class='alert-info'><th style='display:none;'></th><th>Name of Company</th><th>Department</th><th>Designation</th><th>Previous Salary</th><th>Job From Date</th><th>Job To Date</th><th>Edit</th></tr></thead>");
                                    $("[id*='tblexp']").append("<tbody>");
                                }
                                $("[id*='tblexp']").append("<tr><td style='display:none;'>" + data.d[i].empid + "</td><td>" + data.d[i].comp + "</td><td>" + data.d[i].depart + "</td><td>" + data.d[i].desig + "</td><td>" + data.d[i].prvsal + "</td><td>" + data.d[i].jfdate + "</td><td>" + data.d[i].jtdate + "</td><td><a href='#' class='Select'>SELECT</a></td></tr>");
                                if (i == data.d.length - 1) {
                                    $("[id*='tblexp']").append("</tbody>");
                                }
                            }
                        }
                        else {

                        }
                    }
                });

                $.ajax({
                    type: "POST",
                    url: "EmployeeMaster.aspx/getempPhoto",
                    data: '{empid:"' + $("[id*='txtempid']").val() + '"}',
                    async: false,
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        // for (var i = 0; i < data.length; i++) {
                        if (data.d[0].msg == "Uploaded") {
                            if (data.d[0].photo != "") {
                                document.getElementById("stud_photo").src = data.d[0].photo;
                            }
                            else {
                                $.notify("Photo Not Found..!!", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                            }
                            if (data.d[0].sign != "") {
                                document.getElementById("stud_sign").src = data.d[0].sign;
                            }
                            else {
                                $.notify("Sign Not Found..!!", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                            }
                            //document.getElementById("stud_photo").setAttribute("src", path);
                        }
                        else {
                            //$.notify("Photo Not Found..!!", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                        }
                        //}
                    }
                });
            }
        }
    });
});


$("[id*='btnaddedu']").on('click', function () {
    var colgname = $("#txtcolgname").val();
    var colgname = colgname.replace(/\'/g, "\"");

    var obtmarks = $("#txtobt").val();
    var outmarks = $("#txtmarks").val();
    $("[id*='txtdegree']").removeAttr("disabled");
    var pursuing;
    if ($("[id*='chkpur']")[0].checked == true) {
        pursuing = 1;
    }
    else {
        pursuing = 0;
    }
   
    if (parseInt(outmarks) < parseInt(obtmarks)) {
        $.notify("Obtained Marks Should be Less than or equal to Out of Marks.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
    }
    else {
        if (colgname != "" && obtmarks != "" && outmarks != "" && $("[id*=txtboard]").val() != "" && $("[id*=txtdegree]").val() != "" && $("[id*=txtdegty]").val() != "" && $("[id*='ddlclass']")[0].selectedIndex != 0) {
            if ($("[id*='txtempid']").val() != "") {
                $.ajax({
                    type: "POST",
                    url: "EmployeeMaster.aspx/saveeducation",
                    data: '{empid:"' + $("[id*='txtempid']").val() + '",colgname:"' + colgname + '",obt:"' + obtmarks + '",out1:"' + outmarks + '",board:"' + $("[id*=txtboard]").val() + '",degree:"' + $("[id*=txtdegree]").val() + '",type:"' + $("[id*=txtdegty]").val() + '",class1:"' + $("[id*='ddlclass']").val() + '",pursuing:"' + pursuing + '",yearpass:"' + $("[id*='dtpyop']").val() + '",month:"' + $("[id*='ddlmonth']").val() + '",subject:"'+$("[id*='txtsubject']").val()+'",dmltype:"' + dmltype + '"}',
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    success: function (r) {
                        if (r.d == true) {
                            $.notify("Data Added Successfully.", { color: "#fff", background: "#127515", blur: 0.2, delay: 0 });
                            $("#txtcolgname").val("");
                            $("#txtobt").val("");
                            $("#txtmarks").val("");
                            $("[id*='chkpur']")[0].checked = false;
                            $("#txtboard").val("");
                            $("#txtdegree").val("");
                            $("#txtdegty").val("");
                            $("#txtsubject").val("");
                            $("[id*='ddlclass']")[0].selectedIndex=0;
                            $("[id*='dtpyop']")[0].selectedIndex = 0;
                            $("[id*='ddlmonth']")[0].selectedIndex = 0;

                            $.ajax({
                                type: "POST",
                                url: "EmployeeMaster.aspx/showeducation",
                                data: '{empid:"' + $("[id*='txtempid']").val() + '"}',
                                contentType: "application/json; charset=utf-8",
                                async: false,
                                success: function (data) {
                                    if (data.d.length > 0 && data.d[0].msg != "No Data Found") {
                                        $("[id*='dgv_load']").empty();
                                        for (var i = 0; i < data.d.length; i++) {
                                            if (i == 0) {
                                                $("[id*='dgv_load']").append("<thead><tr class='alert-info'><th style='display:none;'>ID</th><th>College Name</th><th>Board/University</th><th>Degree Name</th><th>Degree Type</th><th>Specialization Subject</th><th>Month of Passing</th><th>Year of Passing</th><th>Marks Obtained</th><th>Total Marks</th><th>Class</th><th>Pursuing</th><th></th></tr></thead>");
                                                $("[id*='dgv_load']").append("<tbody>");                                                
                                            }
                                            $("[id*='dgv_load']").append("<tr><td style='display:none;'>" + $("[id*='txtempid']").val() + "</td><td>" + data.d[i].colgname + "</td><td>" + data.d[i].boardname + "</td><td>" + data.d[i].degname + "</td><td>" + data.d[i].degtype + "</td><td>" + data.d[i].subject + "</td><td>" + data.d[i].mop + "</td><td>" + data.d[i].yop + "</td><td>" + data.d[i].obtmk + "</td><td>" + data.d[i].totmrk + "</td><td>" + data.d[i].class1 + "</td><td>" + data.d[i].pursuing + "</td><td><a href='#' class='Select'>SELECT</a></td></tr>");
                                            if (i == data.d.length-1) {
                                                $("[id*='dgv_load']").append("</tbody>");
                                            }
                                        }
                                    }
                                    else {

                                    }
                                }
                            });
                        }
                        else {

                        }
                    }
                });
            }
            else {
                $.notify("Employee ID Not Found.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
            }
        }
        else {
            $.notify("Please fill all fields.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
        }
    }
});

$("[id*='btn_reset']").on('click', function () {
    $("#txtcolgname").val("");
    $("#txtobt").val("");
    $("#txtmarks").val("");
    $("[id*='chkpur']")[0].checked == false;
    $("#txtboard").val("");
    $("#txtdegree").val("");
    $("#txtdegty").val("");
    $("[id*='txtdegree']").removeAttr("disabled");
    $("#txtsubject").val("");
    $("[id*='ddlclass']")[0].selectedIndex = 0;
    $("[id*='dtpyop']")[0].selectedIndex = 0;
    $("[id*='ddlmonth']")[0].selectedIndex = 0;
});

$("[id*=dgv_load]").on('click', 'td a.Select', function () {
    $("[id*='txtdegree']").prop("disabled","disabled");
    var $td = $(this).closest('tr').children('td');
    var id = $td.eq(0).text();

    $("#txtcolgname").val($td.eq(1).text());
    $("#txtobt").val($td.eq(8).text());
    $("#txtmarks").val($td.eq(9).text());
    if ($td.eq(11).text() != "Not") {
        $("[id*='chkpur']")[0].checked == true;
    }
    else {
        $("[id*='chkpur']")[0].checked == false;
    }
    $("#txtboard").val($td.eq(2).text());
    $("#txtdegree").val($td.eq(3).text());
    $("#txtdegty").val($td.eq(4).text());
    $("#txtsubject").val($td.eq(5).text());
    $("[id*='ddlclass']").val($td.eq(10).text());
    $("[id*='dtpyop']").val($td.eq(7).text());
    $("[id*='ddlmonth']").val($td.eq(6).text());
});

$("[id*=tbldepart]").on('click', 'td a.Select', function () {
   
    var $td = $(this).closest('tr').children('td');
    var id = $td.eq(0).text();
    $("#txtsalaryedu").val($td.eq(6).text());
    $("#txtdatedept").val($td.eq(5).text());
    $("#txtcurrdep").val($td.eq(1).text());
    $("[id*='txtcurrdep']").prop("disabled", "disabled");
    $("[id*='ddlcriteria']").val($td.eq(7).text());
    $("[id*='ddledudepart']").prop("disabled", "disabled");
    $("[id*='ddledudesig']").prop("disabled", "disabled");
    $("[id*='ddledudepart']").val($td.eq(2).text());
    $("[id*='ddledudesig']").val($td.eq(4).text());
   
});


$("[id*=btneduadd]").on('click', function () {
    $("[id*=ddledudepart]").removeAttr("disabled");
    $("[id*=ddledudesig]").removeAttr("disabled");
    $("#txtcurrdep").removeAttr("disabled");
    if ($("[id*=ddledudepart]")[0].selectedIndex != 0 && $("[id*=ddledudesig]")[0].selectedIndex != 0 && $("[id*=ddlcriteria]")[0].selectedIndex != 0 && $("[id*='txtdatedept']").val() != "" && $("[id*='txtsalaryedu']").val() != "") {
        if ($("[id*='txtempid']").val() != "") {
            $.ajax({
                type: "POST",
                url: "EmployeeMaster.aspx/savedepart",
                data: '{empid:"' + $("[id*='txtempid']").val() + '",depart:"' + $("[id*='ddledudepart']").val() + '", desig:"' + $("[id*='ddledudesig']").val() + '", salary:"' + $("[id*='txtsalaryedu']").val() + '",doj:"' + $("#txtdatedept").val() + '",criteria:"' + $("[id*='ddlcriteria']").val() + '"}',
                contentType: "application/json; charset=utf-8",
                async: false,
                success: function (r) {
                    if (r.d == true) {
                        $.notify("Data Updated Successfully.", { color: "#fff", background: "#127515", blur: 0.2, delay: 0 });
                        $("[id*=ddledudepart]")[0].selectedIndex=0;
                        $("[id*=ddledudesig]")[0].selectedIndex = 0;
                        $("#txtsalaryedu").val("");
                        $("#txtdatedept").val("");
                        $("#txtcurrdep").val("");
                        $("[id*=ddlcriteria]")[0].selectedIndex = 0;

                        $.ajax({
                            type: "POST",
                            url: "EmployeeMaster.aspx/getdepartdata",
                            data: '{empid:"' + $("[id*='txtempid']").val() + '"}',
                            contentType: "application/json; charset=utf-8",
                            async: false,
                            success: function (data) {
                                if (data.d.length > 0 && data.d[0].msg != "No Data Found") {
                                    $("[id*=tbldepart]").empty();
                                    for (var i = 0; i < data.d.length; i++) {
                                        if (i == 0) {
                                            $("[id*=tbldepart]").append("<thead><tr class='alert-info'><th style='display:none;'></th><th>Department</th><th>Designation</th><th>Date of Joinig</th><th>Basic Salary</th><th>Criteria</th><th>Edit</th></tr></thead>");
                                            $("[id*='tbldepart']").append("<tbody>");
                                        }
                                        $("[id*='tbldepart']").append("<tr><td style='display:none;'>" + data.d[i].empid + "</td><td>" + data.d[i].depart + "</td><td style='display:none;'>" + data.d[i].deptid + "</td><td>" + data.d[i].desig + "</td><td style='display:none;'>" + data.d[i].desid + "</td><td>" + data.d[i].doj + "</td><td>" + data.d[i].salary + "</td><td>" + data.d[i].jobtype + "</td><td><a href='#' class='Select'>SELECT</a></td></tr>");
                                        if (i == data.d.length - 1) {
                                            $("[id*='tbldepart']").append("</tbody>");
                                        }
                                    }
                                }
                                else {

                                }
                            }
                        });
                    }
                    else {
                        $.notify("Data Not Updated.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                    }
                }
            });
        }
        else {
            $.notify("Emplopyee ID not found to update.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
        }
    }
    else {
        $.notify("Please select all fields.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
    }
});


$("[id*=btneduclear]").on('click', function () {
    $("[id*=ddledudepart]")[0].selectedIndex = 0;
    $("[id*=ddledudesig]")[0].selectedIndex = 0;
    $("#txtsalaryedu").val("");
    $("#txtdatedept").val("");
    $("#txtcurrdep").val("");
    $("[id*=ddlcriteria]")[0].selectedIndex = 0;

    $("[id*=ddledudepart]").removeAttr("disabled");
    $("[id*=ddledudesig]").removeAttr("disabled");
    $("#txtcurrdep").removeAttr("disabled");
});

var issalary;
var acntype;

$("[id*='btnaccadd']").on('click', function () {
    $("#txtaccno").removeAttr("disabled");
    if ($("[id*='chkisalary']")[0].checked == true) {
        issalary=1;
    }
    else
    {
        issalary=0;
    }

    if ($("[id*=ddlacctype] :selected").text() == "--Select--") {
        $.notify("Please choose Account Type.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
    }
    else {
        if ($("[id*=ddlacctype]")[0].selectedIndex == 1) {
            acntype = 0;
        }
        else {
            acntype = 1;
        }
    }
    // && $("[id*='txtbnkname']").val() != "" && $("[id*='txtbranch']").val() != "" && acntype != ""
    if ($("[id*='txtaccno']").val() != "" && $("[id*='ddlacctype']")[0].selectedIndex!=0) {
        $.ajax({
            type: "POST",
            url: "EmployeeMaster.aspx/saveaccount",
            data: '{empid:"' + $("[id*='txtempid']").val() + '",acntno:"' + $("[id*='txtaccno']").val() + '", acntype:"' + acntype + '", bankname:"' + $("[id*='txtbnkname']").val() + '",branch:"' + $("#txtbranch").val() + '",isalary:"' + issalary + '"}',
            contentType: "application/json; charset=utf-8",
            async: false,
            success: function (r) {
                if (r.d == true) {
                    $.notify("Data Saved Successfully.", { color: "#fff", background: "#127515", blur: 0.2, delay: 0 });
                    $("[id*='chkisalary']")[0].checked = false;
                    $("[id*='txtaccno']").val("");
                    $("[id*='txtbnkname']").val("");
                    $("[id*='ddlacctype']")[0].selectedIndex=0;
                    $("#txtbranch").val("");

                    $.ajax({
                        type: "POST",
                        url: "EmployeeMaster.aspx/getaccountdata",
                        data: '{empid:"' + $("[id*='txtempid']").val() + '"}',
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        success: function (data) {
                            if (data.d.length > 0 && data.d[0].msg != "No Data Found") {
                                $("[id*=dgvaccount]").empty();
                                for (var i = 0; i < data.d.length; i++) {
                                    if (i == 0) {
                                        $("[id*=dgvaccount]").append("<thead><tr class='alert-info'><th style='display:none;'></th><th>Account No.</th><th>Account Type</th><th>Bank Name</th><th>Branch Name</th><th>Salary Account</th><th>Edit</th></tr></thead>");
                                        $("[id*='dgvaccount']").append("<tbody>");
                                    }
                                    $("[id*='dgvaccount']").append("<tr><td style='display:none;'>" + data.d[i].empid + "</td><td>" + data.d[i].accntno + "</td><td>" + data.d[i].acntype + "</td><td>" + data.d[i].bnkname + "</td><td>" + data.d[i].branch + "</td><td>" + data.d[i].isalary + "</td><td><a href='#' class='Select'>SELECT</a></td></tr>");
                                    if (i == data.d.length - 1) {
                                        $("[id*='dgvaccount']").append("</tbody>");
                                    }
                                }
                            }
                            else {

                            }
                        }
                    });
                }
                else {
                    $.notify("Data Not Saved.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                }
            }
        });
    }
    else {
        $.notify("Please fill all details.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
    }
});

$("[id*=dgvaccount]").on('click', 'td a.Select', function () {

    var $td = $(this).closest('tr').children('td');
    var id = $td.eq(0).text();
    $("#txtaccno").val($td.eq(1).text());
    $("#txtaccno").prop("disabled","disabled");
    $("#txtbnkname").val($td.eq(3).text());
    $("#txtbranch").val($td.eq(4).text());
    if ($td.eq(2).text() == "Saving") {
        $("[id*='ddlacctype']")[0].selectedIndex=1;
    }
    else if ($td.eq(2).text() == "Current") {
        $("[id*='ddlacctype']")[0].selectedIndex = 2;
    }
    else {
        $("[id*='ddlacctype']")[0].selectedIndex = 0;
    }

    if ($td.eq(5).text() == "Not") {
        $("[id*='chkisalary']")[0].checked = false;
    }
    else {
        $("[id*='chkisalary']")[0].checked = true;
    }

});

$("[id*=btnacclear]").on('click', function () {
    $("[id*='chkisalary']")[0].checked = false;
   // $("[id*='txtempid']").val("");
    $("[id*='txtaccno']").val("");
    $("[id*='txtbnkname']").val("");
    $("[id*='ddlacctype']")[0].selectedIndex = 0;
    $("#txtbranch").val("");
    $("#txtaccno").removeAttr("disabled");
});

$("[id*='btnexpadd']").on('click', function () {
    $("#txtorg").removeAttr("disabled");
    $("#txtexpdoj").removeAttr("disabled");
    $("#txtexplev").removeAttr("disabled");

    if ($("[id*='txtempid']").val() != "") {
        if ($("#txtorg").val() != "" && $("#txtexpdept").val() != "" && $("#txtexpdes").val() != "" && $("#txtpresal").val() != "" && $("#txtexpdoj").val() != "" && $("#txtexplev").val() != "") {
            $.ajax({
                type: "POST",
                url: "EmployeeMaster.aspx/savexperience",
                data: '{empid:"' + $("[id*='txtempid']").val() + '",org:"' + $("#txtorg").val() + '", depart:"' + $("#txtexpdept").val() + '",desig:"' + $("#txtexpdes").val() + '", prevsal:"' + $("#txtpresal").val() + '",doj:"' + $("#txtexpdoj").val() + '",dol:"' + $("#txtexplev").val() + '"}',
                contentType: "application/json; charset=utf-8",
                async: false,
                success: function (r) {
                    if (r.d == true) {
                        $.notify("Data Saved Successfully.", { color: "#fff", background: "#127515", blur: 0.2, delay: 0 });
                        $("[id*='txtorg']").val("");
                        $("[id*='txtexpdept']").val("");
                        $("[id*='txtexpdes']").val("");
                        $("[id*='txtpresal']").val("");
                        $("[id*='txtexpdoj']").val("");
                        $("[id*='txtexplev']").val("");

                        $.ajax({
                            type: "POST",
                            url: "EmployeeMaster.aspx/getexpdata",
                            data: '{empid:"' + $("[id*='txtempid']").val() + '"}',
                            contentType: "application/json; charset=utf-8",
                            async: false,
                            success: function (data) {
                                if (data.d.length > 0 && data.d[0].msg != "No Data Found") {
                                    $("[id*=tblexp]").empty();
                                    for (var i = 0; i < data.d.length; i++) {
                                        if (i == 0) {
                                            $("[id*=tblexp]").append("<thead><tr class='alert-info'><th style='display:none;'></th><th>Name of Company</th><th>Department</th><th>Designation</th><th>Previous Salary</th><th>Job From Date</th><th>Job To Date</th><th>Edit</th></tr></thead>");
                                            $("[id*='tblexp']").append("<tbody>");
                                        }
                                        $("[id*='tblexp']").append("<tr><td style='display:none;'>" + data.d[i].empid + "</td><td>" + data.d[i].comp + "</td><td>" + data.d[i].depart + "</td><td>" + data.d[i].desig + "</td><td>" + data.d[i].prvsal + "</td><td>" + data.d[i].jfdate + "</td><td>" + data.d[i].jtdate + "</td><td><a href='#' class='Select'>SELECT</a></td></tr>");
                                        if (i == data.d.length - 1) {
                                            $("[id*='tblexp']").append("</tbody>");
                                        }
                                    }
                                }
                                else {

                                }
                            }
                        });
                    }
                }
            });
        }
        else {
            $.notify("Please fill all fields.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
        }
    }
    else {
        $.notify("Emplopyee ID not found to Add.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
    }
});

$("[id*=tblexp]").on('click', 'td a.Select', function () {
    var $td = $(this).closest('tr').children('td');
    var id = $td.eq(0).text();
    $("#txtorg").prop("disabled", "disabled");
    $("#txtexpdoj").prop("disabled", "disabled");
    $("#txtexplev").prop("disabled", "disabled");

    $("[id*='txtorg']").val($td.eq(1).text());
    $("[id*='txtexpdept']").val($td.eq(2).text());
    $("[id*='txtexpdes']").val($td.eq(3).text());
    $("[id*='txtpresal']").val($td.eq(4).text());
    $("[id*='txtexpdoj']").val($td.eq(5).text());
    $("[id*='txtexplev']").val($td.eq(6).text());
});

$("[id*=btnexpclr]").on('click', function () {
    $("[id*='txtorg']").val("");
    $("[id*='txtexpdept']").val("");
    $("[id*='txtexpdes']").val("");
    $("[id*='txtpresal']").val("");
    $("[id*='txtexpdoj']").val("");
    $("[id*='txtexplev']").val("");
    $("#txtorg").removeAttr("disabled");
    $("#txtexpdoj").removeAttr("disabled");
    $("#txtexplev").removeAttr("disabled");
});


