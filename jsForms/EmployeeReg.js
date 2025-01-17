$(document).ready(function () {
    localStorage.clear();
    filldepart();
    filldesig();
    filldepartddl();
    filldesigddl();
    fillrole();
    fill_model()
    $("[id*=stud_photo]").attr("src", "image/user.png");
    $("[id*=stud_sign]").attr("src", "image/sign.png");
    if ($("[id*=btnsave]")[0].innerText == 'Save') {
        fillform_new();
    }
});

function filldesigddl() {
    $.ajax({
        type: "POST",
        url: "EmployeeEntry.aspx/filldesignation",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        async: false,
        success: function (r) {
            $("[id*=ddldesig]").empty().append('<option selected="selected" value="0">--Select--</option>');
            $.each(r.d, function () {
                $("[id*=ddldesig]").append($("<option></option>").val(this['Value']).html(this['Text']));
            });
        },
        error: function (xhr, status, error) {
            $.notify(error, { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
        }
    });
}

function fill_model() {
    $.ajax({
        type: "POST",
        url: "EmployeeEntry.aspx/fill_model",
        data: '{ }',
        contentType: "application/json; charset=utf-8",
        success: function (r) {
            $.each(r.d, function () {

                $("[id*=li_name]").append($("<option></option>").val(this['Value']).html(this['Text']));
                $("[id*=li_name]").multiselect("rebuild");


            });
        },
        error: function (xhr, status, error) {
            $.notify(error, { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
        }
    });
}

function fillform() {
    $.ajax({
        type: "POST",
        url: "EmployeeEntry.aspx/fill_form_name",
        data: '{model:"' + $("[id*='li_name']").val() + '" }',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $.each(data.d, function () {

                $("[id*=lst_form]").append($("<option></option>").val(this['Value']).html(this['Text']));
                $("[id*=lst_form]").multiselect("rebuild");


            });
        },
        error: function (xhr, status, error) {
            $.notify(error, { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
        }
    });
}

function fillform_new() {
    $.ajax({
        type: "POST",
        url: "EmployeeEntry.aspx/fill_form_name_new",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        async:false,
        success: function (data) {

            $.each(data.d, function () {

                $("[id*=lst_form]").append($("<option></option>").val(this['Value']).html(this['Text']));
                $("[id*=lst_form]").multiselect("rebuild");


            });
        },
        error: function (xhr, status, error) {
            $.notify(error, { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
        }
    });
}

function fillrole() {
    $.ajax({
        type: "POST",
        url: "EmployeeEntry.aspx/fillrole",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        async: false,
        success: function (r) {
            $("[id*=ddlRoles]").empty().append('<option selected="selected" value="0">--Select--</option>');
            $.each(r.d, function () {
                $("[id*=ddlRoles]").append($("<option></option>").val(this['Value']).html(this['Text']));
            });
        },
        error: function (xhr, status, error) {
            $.notify(error, { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
        }
    });
}

function filldepart() {
    $.ajax({
        type: "POST",
        url: "EmployeeEntry.aspx/filltblgrid",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("[id*=tblgrid]").show();
            $("[id*=tbldepfill]").empty();
            if (data.d.length > 0) {
                $("[id*=tbldepfill]").append("<thead style='background-color:#0078BC;color: white;border: 1px solid black;'><tr ><th style='display:none;border: 1px solid black;'>DepartmentID</th><th style='border: 1px solid black;'><center>SR No.</center></th><th style='border: 1px solid black;'><center>Prefix</center></th><th style='border: 1px solid black;'><center>Department Name</center></th><th style='border: 1px solid black;'><center>Edit</center></th><th style='border: 1px solid black;'><center>Delete</center></th></tr></thead>");
                for (var i = 0; i < data.d.length; i++) {
                    if (i == 0) {
                        $("[id*=tbldepfill]").append("<tbody style='border: 1px solid black;'>");
                    }
                    $("[id*=tbldepfill]").append("<tr><td style='display:none;border: 1px solid black;'>" + data.d[i].deptid + "</td><td style='border: 1px solid black;'><center>" + (i + 1) + "</center></td><td style='border: 1px solid black;'><center>" + data.d[i].prefix + "</center></td><td style='border: 1px solid black;'><center>" + data.d[i].deptname + "</center></td><td style='border: 1px solid black;'><center><a href='#' class='Select'><span class='fa fa-edit fa-fw' style='color: grey;'></span></a></center></td><td style='border: 1px solid black;'><center><a href='#' class='delete'><span class='fas fa-trash' style='color: grey;'></span></a><center></td></tr>");
                    if (i == data.d.length - 1) {
                        $("[id*=tbldepfill]").append("</tbody>");
                    }
                }
            }
        },
        error: function (xhr, status, error) {
            $.notify(error, { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
        }
    });
}

function filldesig() {
    $.ajax({
        type: "POST",
        url: "EmployeeEntry.aspx/fillgesigrid",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("[id*=desigtbl]").show();
            $("[id*=tbldesig]").empty();
            if (data.d.length > 0) {
                $("[id*=tbldesig]").append("<thead style='background-color:#0078BC;color: white;border: 1px solid black;'><tr ><th style='display:none;border: 1px solid black;'>DesignationID</th><th style='border: 1px solid black;'><center>SR No.</center></th><th style='border: 1px solid black;'><center>Designation Name</center></th><th style='border: 1px solid black;'><center>Edit</center></th><th style='border: 1px solid black;'><center>Delete</center></th></tr></thead>");
                for (var i = 0; i < data.d.length; i++) {
                    if (i == 0) {
                        $("[id*=tbldesig]").append("<tbody>");
                    }
                    $("[id*=tbldesig]").append("<tr><td style='display:none;border: 1px solid black;'>" + data.d[i].desid + "</td><td style='border: 1px solid black;'><center>" + (i + 1) + "</center></td><td style='border: 1px solid black;'><center>" + data.d[i].desname + "</center></td><td style='border: 1px solid black;'><center><a href='#' class='Select'><span class='fa fa-edit fa-fw' style='color: grey;'></span></a></center></td><td style='border: 1px solid black;'><center><a href='#' class='delete'><span class='fas fa-trash' style='color: grey;'></span></a></center></td></tr>");
                    if (i == data.d.length - 1) {
                        $("[id*=tbldesig]").append("</tbody>");
                    }
                }
            }
        },
        error: function (xhr, status, error) {
            $.notify(error, { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
        }
    });
}

function filldepartddl() {
    $.ajax({
        type: "POST",
        url: "EmployeeEntry.aspx/filldepartment",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        async: false,
        success: function (r) {
            $("[id*=ddldept]").empty().append('<option selected="selected" value="0">--Select--</option>');
            $.each(r.d, function () {
                $("[id*=ddldept]").append($("<option></option>").val(this['Value']).html(this['Text']));
            });
        },
        error: function (xhr, status, error) {
            $.notify(error, { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
        }
    });
}

$("[id*=btnmodify]").on('click', function () {
    if ($("[id*=btnmodify]")[0].innerText=="Modify") {
        $("[id*=btnsave]")[0].innerText = "Update";
        $("[id*=btnmodify]")[0].innerText = "New";
        $("[id*=txtempID]").removeAttr("disabled", "disabled");
    }
    else if ($("[id*=btnmodify]")[0].innerText == "New") {
        $("[id*=btnsave]")[0].innerText = "Save";
        $("[id*=btnmodify]")[0].innerText = "Modify";
        $("[id*=txtempID]").attr("disabled", "disabled");
    }
    clear();
});

function clear() {
    $("[id*=txtempID]").val("");
    $("[id*=txtpincode]").val("");
    $("[id*=txt_quali]").val("");
    $("[id*='txtfname']").val("");
    $("[id*='txtmname']").val("");
    $("[id*='txtlname']").val("");
    $("[id*='txtmothern']").val("");
    $("[id*='txtmobile']").val("");
    $("[id*='txtemail']").val("");
    $("[id*='dobdate']").val("");
    $("[id*='datedoj']").val("");
    $("[id*='txtphno']").val("");
    $("[id*='txtsalary']").val("");
    $("[id*=ddldept]")[0].selectedIndex = 0;
    $("[id*=ddl_state]")[0].selectedIndex = 0;
    $("[id*=ddldesig]")[0].selectedIndex = 0;
    $("[id*=ddl_gender]")[0].selectedIndex = 0;
    $("[id*=ddlRoles]")[0].selectedIndex = 0;
    $("[id*=li_name]")[0].selectedIndex = 0;
    $("[id*='txtaddress']").val("");
    $("[id*=ddlBloodGroup]")[0].selectedIndex = 0;
    $("[id*=get_sign]").val('');
    $("[id*=get_photo]").val('');
    $("[id*=stud_photo]").attr("src", "image/user.png");
    $("[id*=stud_sign]").attr("src", "image/sign.png");

    $.each($('#li_name > option'), function (i, mod) {
        $(this).prop('disabled', false);
        $("[id*=li_name]").multiselect('deselect', mod.value);
    })
    $("[id*=li_name]").multiselect("refresh");
    $("[id*=li_name]").multiselect("rebuild");

    $.each($('#lst_form > option'), function (i, mod) {
        $(this).prop('disabled', false);
        $("[id*=lst_form]").multiselect('deselect', mod.value);
    })
    $("[id*=lst_form]").empty();
    $("[id*=lst_form]").multiselect("refresh");
    $("[id*=lst_form]").multiselect("rebuild");

    $("[id*='txt_quali']").val("");
    $("[id*='txtpincode']").val("");
    $("[id*=ddl_state]")[0].selectedIndex = 0;
    $("[id*=get_photo]").val('');
    $("[id*=get_sign]").val('');
    $("[id*=stud_photo]").attr("src", "image/user.png");
    $("[id*=stud_sign]").attr("src", "image/sign.png");
    $("[id*=imagediv]").show();
    $("[id*=signdiv]").hide();
    $("[id*=btntoggle]").text('Signature');
    localStorage.clear();
}

$("[id*=btnRefresh]").on('click', function () {
    clear();
    $("[id*=txtempID]").attr("disabled", "disabled");
    $("[id*=btnsave]")[0].innerText = "Save";
    $("[id*=btnmodify]")[0].innerText = "Modify";
});

$("#txtempID").keyup(function (event) {
    if ($("#txtempID")[0].value.length == 8) {
        $("#txtempID").attr("disabled", "disabled");
        changekey();
    }
    else if (event.keyCode == 13) {
        $("#txtempID").attr("disabled", "disabled");
        changekey();
    }


});

function changekey() {
    if ($("[id*='txtempID']").val() != "" && $("[id*='txtempID']").val() != undefined && $("#txtempID")[0].value.length == 8) {
        localStorage.removeItem('defaultmodules');
        localStorage.removeItem('defaultforms');
        localStorage.removeItem('forms');
        $.ajax({
            type: "POST",
            url: "EmployeeEntry.aspx/getemployeedata",
            data: '{empid:"' + $("[id*='txtempID']").val() + '"}',
            contentType: "application/json; charset=utf-8",
            //async: false,
            success: function (data) {
                if (data.d[0].msg == "") {
                    $("[id*='txtfname']").val(data.d[0].fname);
                    $("[id*='txtmname']").val(data.d[0].mname);
                    $("[id*='txtlname']").val(data.d[0].lname);
                    $("[id*='txtmothern']").val(data.d[0].mothername);
                    $("[id*='txtmobile']").val(data.d[0].mobileno);
                    $("[id*='txtemail']").val(data.d[0].emailid);
                    $("[id*='dobdate']").val(data.d[0].dob);
                    $("[id*='datedoj']").val(data.d[0].dojoin);
                    $("[id*='txtphno']").val(data.d[0].mobile2);
                    if (data.d[0].salary == "0") {
                        $("[id*='txtsalary']").val('');
                    }
                    else {
                        $("[id*='txtsalary']").val(data.d[0].salary);
                    }
                    $("[id*=ddldept]").val(data.d[0].deptid);
                    $("[id*=ddldesig]").val(data.d[0].desigid);
                    $("[id*=ddlRoles]").val(data.d[0].role);
                    $("[id*=txtaddress]").val(data.d[0].address);
                    $("[id*=txtpincode]").val(data.d[0].pincode);
                    $("[id*=ddl_state]").val(data.d[0].state);
                    $("[id*=txt_quali]").val(data.d[0].qualification);
                    if (data.d[0].image != "" && data.d[0].image != null) {
                        $("[id*=stud_photo]").attr("src", data.d[0].image);
                        $("[id*=lblphoto]")[0].innerText = data.d[0].image;
                    }
                    else {
                        $("[id*=lblphoto]")[0].innerText = "image/user.png";
                        $("[id*=stud_photo]").attr("src", "image/user.png");
                    }
                    if (data.d[0].sign != "" && data.d[0].sign != null) {
                        $("[id*=stud_sign]").attr("src", data.d[0].sign);
                        $("[id*=lblsign]")[0].innerText = data.d[0].sign;
                    }
                    else {
                        $("[id*=lblsign]")[0].innerText = "image/sign.png";
                        $("[id*=stud_sign]").attr("src", "image/sign.png");
                    }
                    $("[id*=ddl_gender]").val(data.d[0].gender);
                    $("[id*=ddlBloodGroup]").val(data.d[0].bloodgroup);

                    localStorage.setItem('defaultmodules', data.d[0].defaultmodules);
                    localStorage.setItem('defaultforms', data.d[0].defaultforms);
                    localStorage.setItem('forms', data.d[0].form_name);
                    var module = data.d[0].module;
                    var modulearr = [];
                    modulearr = module.split(',');

                    $("[id*=li_name]").val(modulearr);
                    $("[id*=li_name]").multiselect("refresh");

                    $.each(data.d[0].defaultmodules.split(','), function (i, mod) {
                        $.each($('#li_name > option'), function () {
                            var a = $(this).val();
                            if (mod == a) {
                                $(this).prop('disabled', true);
                            }
                        })
                    })
                    $("[id*=li_name]").multiselect("refresh");

                    var form = data.d[0].form_name;
                    var formarr = [];
                    formarr = form.split(',');

                    var list = $("[id*=lst_form]");
                    $.ajax({
                        type: "POST",
                        url: "EmployeeEntry.aspx/fill_form_name",
                        data: '{module_name:"' + module + '" }',
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        success: function (data) {
                            list.empty();
                            $.each(data.d, function () {
                                $("[id*=lst_form]").append($("<option></option>").val(this['Value']).html(this['Text']));
                                $("[id*=lst_form]").multiselect("rebuild");
                            });
                        },
                        error: function (xhr, status, error) {
                            $.notify(error, { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                        }
                    });

                    $("[id*=lst_form]").val(formarr);
                    $("[id*=lst_form]").multiselect("refresh");

                    $.each(data.d[0].defaultforms.split(','), function (i, mod) {
                        $.each($('#lst_form > option'), function () {
                            var a = $(this).val();
                            if (mod == a) {
                                $(this).prop('disabled', true);
                            }
                        })

                    })
                    $("[id*=lst_form]").multiselect("refresh");
                }
                else {
                    $.notify("No Data Found.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                    $("#txtempID").removeAttr("disabled", "disabled");
                    $("#txtempID")[0].value = "";
                }
            },
            error: function (xhr, status, error) {
                $.notify(error, { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
            }
        });
    }
    else {
        $.notify("Invalid Employee Id.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
        $("#txtempID").removeAttr("disabled", "disabled");
    }
}

$("[id*=ddlRoles]").change(function () {

    if ($("#ddlRoles")[0].selectedIndex > 0) {
        localStorage.removeItem('defaultmodules');
        localStorage.removeItem('defaultforms');
        localStorage.removeItem('forms');

        $.each($('#li_name > option'), function (i, mod) {
            $(this).prop('disabled', false);
            $("[id*=li_name]").multiselect('deselect', mod.value);
        })
        $("[id*=li_name]").multiselect("refresh");
        $("[id*=li_name]").multiselect("rebuild");

        $.each($('#lst_form > option'), function (i, mod) {
            $(this).prop('disabled', false);
            $("[id*=lst_form]").multiselect('deselect', mod.value);
        })
        $("[id*=lst_form]").multiselect("refresh");
        $("[id*=lst_form]").multiselect("rebuild");


        $.ajax({
            type: "POST",
            url: "EmployeeEntry.aspx/fill_form_name_load",
            data: '{roleid:"' + $("[id*=ddlRoles]").val() + '" }',
            contentType: "application/json; charset=utf-8",
            async: false,
            success: function (data) {
                localStorage.setItem('defaultmodules', data.d[0].Text);
                localStorage.setItem('defaultforms', data.d[0].Value);
                var module = data.d[0].Text;
                var modulearr = [];
                modulearr = module.split(',');

                $("[id*=li_name]").val(modulearr);
                $("[id*=li_name]").multiselect("refresh");
                $("[id*=li_name]").multiselect("rebuild");

                $.each(modulearr, function (i, mod) {
                    $.each($('#li_name > option'), function () {
                        var a = $(this).val();
                        if (mod == a) {
                            $(this).prop('disabled', true);
                        }
                    })
                });
                $("[id*=li_name]").multiselect("refresh");
                $("[id*=li_name]").multiselect("rebuild");

                var list = $("[id*=lst_form]");
                $.ajax({
                    type: "POST",
                    url: "EmployeeEntry.aspx/fill_form_name",
                    data: '{module_name:"' + module + '" }',
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    success: function (data) {
                        list.empty();
                        if (data.d.length > 0) {
                            $.each(data.d, function () {
                                $("[id*=lst_form]").append($("<option></option>").val(this['Value']).html(this['Text']));
                            });
                            $("[id*=lst_form]").multiselect("refresh");
                        }
                        else {
                            $("[id*=lst_form]").val([]);
                            $("[id*=lst_form]").multiselect("refresh");

                            if ($("[id*=btnsave]")[0].innerText == 'Save') {
                                fillform_new();
                            }
                        }
                    },
                    error: function (xhr, status, error) {
                        $.notify(error, { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                    }
                });
                var form = data.d[0].Value;
                var formarr = [];
                formarr = form.split(',');

                $("[id*=lst_form]").val(formarr);
                $("[id*=lst_form]").multiselect("refresh");

                $.each(formarr, function (i, mod) {
                    $.each($('#lst_form > option'), function () {
                        var a = $(this).val();
                        if (mod == a) {
                            $(this).prop('disabled', true);
                        }
                    })

                })
                $("[id*=lst_form]").multiselect("refresh");
                $("[id*=lst_form]").multiselect("rebuild");
                formlist();
            },
            error: function (xhr, status, error) {
                $.notify(error, { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
            }
        });
    }
    else if ($("#ddlRoles")[0].selectedIndex == 0) {
        $.each($('#li_name > option'), function (i, mod) {
            $(this).prop('disabled', false);
            $("[id*=li_name]").multiselect('deselect', mod.value);
        })
        $("[id*=li_name]").multiselect("refresh");
        $("[id*=li_name]").multiselect("rebuild");

        $.each($('#lst_form > option'), function (i, mod) {
            $(this).prop('disabled', false);
            $("[id*=lst_form]").multiselect('deselect', mod.value);
        })
        $("[id*=lst_form]").multiselect("refresh");
        $("[id*=lst_form]").multiselect("rebuild");
    }
});

$("[id*=li_name]").on('change', function () {
    var module = '';
    $("[id*='li_name'] :selected").each(function (i, selected) {
        if (module == "") {
            module = $(selected).val();
        }
        else {
            module = module + "," + $(selected).val();
        }
    });

    var list = $("[id*=lst_form]");
    $.ajax({
        type: "POST",
        url: "EmployeeEntry.aspx/fill_form_name",
        data: '{module_name:"' + module + '" }',
        contentType: "application/json; charset=utf-8",
        async: false,
        success: function (data) {
            list.empty();
            if (data.d.length > 0) {
                var result = localStorage["forms"];
                var formnames = [];
                formnames = result.split(',');
                var checkedoption;
                $.each(data.d, function () {
                    $("[id*=lst_form]").append($("<option></option>").val(this['Value']).html(this['Text']));
                    $("[id*=lst_form]").multiselect("rebuild");
                });
                if (formnames != "") {
                    $("[id*=lst_form]").val(formnames);
                }
                $("[id*=lst_form]").multiselect("refresh");

                var defaultforms = localStorage["defaultforms"].split(',');
                $.each(defaultforms, function (i, mod) {
                    $.each($('#lst_form > option'), function () {
                        var a = $(this).val();
                        if (mod == a) {
                            $(this).prop('disabled', true);
                        }
                    })

                });
                $("[id*=lst_form]").multiselect("refresh");
                formlist();
            }
            else {
                $("[id*=lst_form]").val([]);
                $("[id*=lst_form]").multiselect("rebuild");
                $("[id*=lst_form]").multiselect("refresh");
                if ($("[id*=btnsave]")[0].innerText == 'Save') {
                    fillform_new();
                }
            }
        },
        error: function (xhr, status, error) {
            $.notify(error, { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
        }
    });
    //---------------------------------------------

});

//----------------- form
$("[id*=lst_form]").on('change', function () {
    formlist();
});

function formlist() {
    localStorage.removeItem("forms");
    form = "";
    $("[id*='lst_form'] :selected").each(function (i, selected) {
        var chkbx = $('input[value="' + $(this).val() + '"]');
        if (form == "") {
            form = $(selected).val();
        }
        else {
            form = form + "," + $(selected).val();
        }
    });
    localStorage.setItem("forms", form);
}
//-----------------------------------------

//--------------department

$("[id*=btndepsave]").on('click', function () {
    var deptid = "";
    var deptname = $("[id*='txtdept']").val().toUpperCase();
    var prefix = $("[id*='txtprefix']").val().toUpperCase();
    var button = $("[id*=btndepsave]")[0].innerText;
    if (prefix.length = 0) {
        $.notify("Enter Prefix .", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
    }
    else {
        if (deptname != "" && deptname != undefined) {
            if (prefix != "") {
                if (button == "Save") {
                    $.ajax({
                        type: "POST",
                        url: "EmployeeEntry.aspx/saverecord",
                        data: '{deptname:"' + deptname + '",prefix:"' + prefix + '",type:"department",empid:"' + employeeId + '"}',
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        success: function (data) {
                            if (data.d[0].msg == "Saved") {
                                $.notify("Data Saved Successfully.", { color: "#fff", background: "#127515", blur: 0.2, delay: 0 });
                                $("[id*=txtdept]").val("");
                                $("[id*=txtprefix]").val("");
                                $("[id*=txtdeparttid]").val("");
                                filldepart();
                                filldepartddl();
                            }
                            else if (data.d[0].msg == "More") {
                                $.notify("More Than One Data Saved.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                            }
                            else if (data.d[0].msg == "Exist") {
                                $.notify("Entered Department Name Already Exist.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                            }
                            else if (data.d[0].msg == "Unsaved") {
                                $.notify("No Data To Save", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                            }
                            else {
                                $.notify("Data Not Saved.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                            }
                        },
                        error: function (xhr, status, error) {
                            $.notify(error, { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                        }
                    });
                }
                else {
                    deptid = $("[id*='txtdeparttid']").val();
                    if (deptid != "" && deptid != undefined) {
                        $.ajax({
                            type: "POST",
                            url: "EmployeeEntry.aspx/updaterecord",
                            data: '{deptname:"' + deptname + '",prefix:"' + prefix + '",type:"department",empid:"' + employeeId + '",deptid:"' + deptid + '"}',
                            contentType: "application/json; charset=utf-8",
                            async: false,
                            success: function (data) {
                                if (data.d[0].msg == "Update") {
                                    $.notify("Data Updated Successfully.", { color: "#fff", background: "#127515", blur: 0.2, delay: 0 });
                                    filldepart();
                                    $("[id*=txtdept]").val("");
                                    $("[id*=txtprefix]").val("");
                                    $("[id*=txtdeparttid]").val("");
                                    $("[id*=btndepsave]")[0].innerText = "Save";
                                    $("[id*=txtprefix]").removeAttr('disabled');
                                    filldepartddl();
                                }
                                else if (data.d[0].msg == "More") {
                                    $.notify("More Than One Data Saved.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                                }
                                else if (data.d[0].msg == "Exist") {
                                    $.notify("Entered Department Name Already Exist.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                                }
                                else if (data.d[0].msg == "Unsaved") {
                                    $.notify("No Data To Save.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                                }
                                else {
                                    $.notify("Data Not Saved.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                                }
                            },
                            error: function (xhr, status, error) {
                                $.notify(error, { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                            }
                        });
                    }
                }
            }
            else {
                $.notify("Enter Prefix Value.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
            }
        }

        else {
            $.notify("Enter Department Name.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
        }

    }
});

$("[id*=tbldepfill]").on('click', 'td a.delete', function () {

    var $td = $(this).closest('tr').children('td');
    var id = $td.eq(0).text();
    var prefix = $td.eq(2).text();
    var name = $td.eq(3).text();

    $.ajax({
        type: "POST",
        url: "EmployeeEntry.aspx/deletedept",
        data: '{id:"' + id + '",type:"department",name:"' + name + '",prefix:"' + prefix + '"}',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.d[0].msg == "deleted") {
                $.notify("Data Deleted Successfully.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                filldepart();
                filldepartddl();
                cleardepartment();
            }
            else if (data.d[0].msg == "assigned") {
                $.notify("Employee Already Assigned To Department.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
            }
            else {
                $.notify("No Data To Delete.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
            }
        },
        error: function (xhr, status, error) {
            $.notify(error, { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
        }
    });
});

$("[id*=btndepref]").on('click', function () {
    cleardepartment();
});

$("[id*=btnadddep]").on('click', function () {
    cleardepartment();
});

function cleardepartment() {
    $("[id*=txtdept]").val("");
    $("[id*=txtprefix]").val("");
    $("[id*=txtdeparttid]").val("");
    $("[id*=btndepsave]")[0].innerText = "Save";
    $("[id*=txtprefix]").removeAttr('disabled');
}

$("[id*=tbldepfill]").on('click', 'td a.Select', function () {
    var $td = $(this).closest('tr').children('td');
    var id = $td.eq(0).text();
    var prefix = $td.eq(2).text();
    var name = $td.eq(3).text();
    $("[id*=btndepsave]")[0].innerText = "Update";
    $("[id*=txtdeparttid]").val("");
    $("[id*=txtdeparttid]").val(id);
    $("[id*=txtdept]").val(name);
    $("[id*=txtprefix]").val(prefix);
    $("[id*=txtprefix]").attr('disabled', 'disabled');
});

//-----------designation 

$("[id*=btndesref]").on('click', function () {
    cleardesignation();
});

$("[id*=btnadddes]").on('click', function () {
    cleardesignation();
});

function cleardesignation() {
    $("[id*=txtdesig]").val("");
    $("[id*=txtdeid]").val("");
    $("[id*=btndesave]")[0].innerText = "Save";
}

$("[id*=tbldesig]").on('click', 'td a.Select', function () {
    var $td = $(this).closest('tr').children('td');
    var id = $td.eq(0).text();
    var name = $td.eq(2).text();
    $("[id*=btndesave]")[0].innerText = "Update";
    $("[id*=txtdeid]").val("");
    $("[id*=txtdeid]").val(id);
    $("[id*=txtdesig]").val(name);
});

$("[id*=tbldesig]").on('click', 'td a.delete', function () {

    var $td = $(this).closest('tr').children('td');
    var id = $td.eq(0).text();
    var name = $td.eq(2).text();

    $.ajax({
        type: "POST",
        url: "EmployeeEntry.aspx/deletedept",
        data: '{id:"' + id + '",type:"designation",name:"' + name + '",prefix:""}',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.d[0].msg == "deleted") {
                $.notify("Data Deleted Successfully.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                filldesig();
                filldesigddl();
                cleardesignation();
            }
            else if (data.d[0].msg == "assigned") {
                $.notify("Employee Already Assigned To Designation.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
            }
            else {
                $.notify("No Data To Delete", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
            }
        },
        error: function (xhr, status, error) {
            $.notify(error, { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
        }
    });
});

$("[id*=btndesave]").on('click', function () {
    var desiid = "";
    var desname = $("[id*='txtdesig']").val().toUpperCase();
    var prefix = "";
    var button = $("[id*=btndesave]")[0].innerText;
    if (desname != "" && desname != undefined) {
        if (button == "Save") {
            $.ajax({
                type: "POST",
                url: "EmployeeEntry.aspx/saverecord",
                data: '{deptname:"' + desname + '",prefix:"' + prefix + '",type:"design",empid:"' + employeeId + '"}',
                contentType: "application/json; charset=utf-8",
                async: false,
                success: function (data) {
                    if (data.d[0].msg == "Saved") {
                        $.notify("Data Saved Successfully.", { color: "#fff", background: "#127515", blur: 0.2, delay: 0 });
                        $("[id*=txtdesig]").val("");
                        $("[id*=txtdeid]").val("");
                        filldesig();
                        filldesigddl();
                    }
                    else if (data.d[0].msg == "More") {
                        $.notify("More Than One Data Saved.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                    }
                    else if (data.d[0].msg == "Exist") {
                        $.notify("Designation Name Already Exist.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                    }
                    else if (data.d[0].msg == "Unsaved") {
                        $.notify("No Data To Save.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                    }
                    else {
                        $.notify("Data Not Saved.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                    }
                },
                error: function (xhr, status, error) {
                    $.notify(error, { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                }
            });
        }
        else {
            desiid = $("[id*='txtdeid']").val();
            if (desiid != "") {
                $.ajax({
                    type: "POST",
                    url: "EmployeeEntry.aspx/updaterecord",
                    data: '{deptname:"' + desname + '",prefix:"' + prefix + '",type:"design",empid:"' + employeeId + '",deptid:"' + desiid + '"}',
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    success: function (data) {
                        if (data.d[0].msg == "Update") {
                            $.notify("Data Updated Successfully.", { color: "#fff", background: "#127515", blur: 0.2, delay: 0 });
                            filldesig();
                            $("[id*=txtdesig]").val("");
                            $("[id*=txtdeid]").val("");
                            $("[id*=btndesave]")[0].innerText = "Save";
                            filldesigddl();
                        }
                        else if (data.d[0].msg == "More") {
                            $.notify("More Than One Data Saved.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                        }
                        else if (data.d[0].msg == "Exist") {
                            $.notify("Designation Name Already Exist.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                        }
                        else if (data.d[0].msg == "Unsaved") {
                            $.notify("No Data To Save.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                        }
                        else {
                            $.notify("Data Not Saved.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                        }
                    },
                    error: function (xhr, status, error) {
                        $.notify(error, { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                    }
                });
            }
        }
    }
    else {
        $.notify("Enter Designation Name.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
    }
});

//-----------------------------

$("[id*=btnsave]").on('click', function () {
    var empid = $("[id*='txtempID']").val();
    var fname = $("[id*='txtfname']").val().toUpperCase();
    fname = fname.replace(/\'/g, "\"");
    var mname = $("[id*='txtmname']").val().toUpperCase();
    mname = mname.replace(/\'/g, "\"");
    var lname = $("[id*='txtlname']").val().toUpperCase();
    lname = lname.replace(/\'/g, "\"");
    var mothernme = $("[id*='txtmothern']").val().toUpperCase();
    mothernme = mothernme.replace(/\'/g, "\"");
    var emailid = $("[id*='txtemail']").val();
    var dobirth = $("#dobdate").val();
    var dojoin = $("#datedoj").val();
    var mobileno = $("[id*='txtmobile']").val();
    var salary = $("[id*='txtsalary']").val();
    var departid = $("[id*='ddldept']").val();
    var desigid = $("[id*='ddldesig']").val();
    var otherno = $("[id*='txtphno']").val();
    var gender = $("[id*='ddl_gender']").val();
    var address = $("[id*='txtaddress']").val();
    var bloodgroup = $("[id*='ddlBloodGroup']").val();
    var photopath = $("[id*=get_photo]").val();
    var signpath = $("[id*=get_sign]").val();
    var pincode = $("[id*='txtpincode']").val();
    var state = $("[id*='ddl_state']").val();
    var emp_quali = $("[id*='txt_quali']").val();

    if (empid == undefined) {
        empid = "";
    }
    var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;

    var role = $("[id*='ddlRoles']").val();
    var form_name = localStorage["forms"];
    var logincat = '';
    var grouplist = "";


    var button = $("[id*=btnsave]")[0].innerText;

    if (fname == "")
    {
        $.notify("Enter First Name", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
    }
    else if (mname=="")
    {
        $.notify("Enter Middle Name", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
    }
    else if (mothernme == "") {
        $.notify("Enter Mother Name", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
    }
    else if (gender == "--Select--") {
        $.notify("Select Gender", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
    }
    else if (dobirth == "") {
        $.notify("Select Date of Birth", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
    }
    else if (dojoin == "") {
        $.notify("Select Date of Joining", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
    }
    else if (mobileno == "") {
        $.notify("Enter Mobile Number", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
    }
    else if (mobileno.length < 10) {
        $.notify("Enter 10 Digit Mobile Number", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
    }
    else if (address=="") {
        $.notify("Enter Address", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
    }
    else if (pincode == "") {
        $.notify("Enter Pincode", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
    }
    else if (pincode.length < 6) {
        $.notify("Enter 6 Digit Pincode", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
    }
    else if (state == "--SELECT--") {
        $.notify("Select State", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
    }
    else if (departid == 0) {
        $.notify("Select Department", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
    }
    else if (desigid ==0) {
        $.notify("Select Designation", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
    }
    else if (role == 0) {
        $.notify("Select Role", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
    }
    else if (form_name == "") {
        $.notify("Select Form", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
    }
    else if (desigid == 0) {
        $.notify("Select Designation", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
    }
    else
    {
    if (fname != "" && mname != "" && mothernme != "" && dobirth != "" && dojoin != "" && mobileno != ""  && departid != "" && desigid != "") {
        if (emailid.match(emailReg)) {
            if (button == "Save") {
                $.ajax({
                    type: "POST",
                    url: "EmployeeEntry.aspx/employeerecord",
                    data: '{emp_id:"",fname:"' + fname.replace('"', '@') + '",mname:"' + mname.replace('"', '@') + '",lname:"' + lname.replace('"', '@') + '",mothernme:"' + mothernme.replace('"', '@') + '",emailid:"' + emailid + '",dobirth:"' + dobirth + '",dojoin:"' + dojoin + '",mobileno:"' + mobileno + '",salary:"' + salary + '",departid:"' + departid + '",desigid:"' + desigid + '",gender:"' + gender + '",mobile2:"' + otherno + '",role:"' + role + '",logincategory:"' + logincat + '",grouplist:"' + grouplist + '",formname:"' + form_name + '",address:"' + address + '",bloodgroup:"' + bloodgroup + '",qualification:"' + emp_quali + '",pincode:"' + pincode + '",state:"' + state + '"}',
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    success: function (data) {
                        if (data.d[0].msg == "Saved") {
                            $.notify("Data Saved Successfully.(" + data.d[0].empid + ")", { color: "#fff", background: "#127515", blur: 0.2, delay: 0 });
                            if (photopath != "") {
                                uploadphoto(data.d[0].empid);
                            }
                            if (signpath != "") {
                                uploadsign(data.d[0].empid);
                            }
                            clear();
                            $("[id*=txtempID]").attr("disabled", "disabled");
                            $("[id*=btnsave]")[0].innerText = "Save";
                            $("[id*=btnmodify]")[0].innerText = "Modify";
                        }
                        else if (data.d[0].msg == "More") {
                            $.notify("More Than One Data Saved.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                        }
                        else {
                            $.notify("Data Not Saved.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                        }
                    },
                    error: function (xhr, status, error) {
                        $.notify(error, { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                    }
                });
            }
            else {
                if (empid != "" && empid != undefined) {
                    $.ajax({
                        type: "POST",
                        url: "EmployeeEntry.aspx/employeerecord",
                        data: '{emp_id:"' + $("[id*='txtempID']").val() + '",fname:"' + fname.replace('"', '@') + '",mname:"' + mname.replace('"', '@') + '",lname:"' + lname.replace('"', '@') + '",mothernme:"' + mothernme.replace('"', '@') + '",emailid:"' + emailid + '",dobirth:"' + dobirth + '",dojoin:"' + dojoin + '",mobileno:"' + mobileno + '",salary:"' + salary + '",departid:"' + departid + '",desigid:"' + desigid + '",gender:"' + gender + '",mobile2:"' + otherno + '",role:"' + role + '",logincategory:"' + logincat + '",grouplist:"' + grouplist + '",formname:"' + form_name + '",address:"' + address + '",bloodgroup:"' + bloodgroup + '",qualification:"' + emp_quali + '",pincode:"' + pincode + '",state:"' + state + '"}',
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        success: function (data) {
                            if (data.d[0].msg == "Saved") {
                                $.notify("Data Updated Successfully.(" + data.d[0].empid + ")", { color: "#fff", background: "#127515", blur: 0.2, delay: 0 });
                                if (photopath != "") {
                                    uploadphoto(data.d[0].empid);
                                }
                                if (signpath != "") {
                                    uploadsign(data.d[0].empid);
                                }
                                clear();
                                $("[id*=txtempID]").attr("disabled", "disabled");
                                $("[id*=btnsave]")[0].innerText = "Save";
                                $("[id*=btnmodify]")[0].innerText = "Modify";
                                Page.Response.Redirect(Page.Request.Url.ToString(), true);
                            }
                            else if (data.d[0].msg == "More") {
                                $.notify("More Than One Data Saved.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                            }
                            else {
                                $.notify("Data Not Saved.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                            }
                        },
                        error: function (xhr, status, error) {
                            $.notify(error, { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                        }
                    });
                }
                else {
                    $.notify("Enter Employee ID.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
                }
            }
        }
        else {
            $.notify("Invalid Email ID.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
        }
    }
    else {
        $.notify("Fill All Details.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
    }
}
});

$("[id*=get_photo]").change(function () {
    var photo_type = "";
    var photo = $("[id*=get_photo]").val();
    if ($("[id*=get_photo]").val() != '') {
        res = $("[id*=get_photo]").get(0).files[0].name.split(".");
        photo_type = '.' + res[res.length - 1];
    }
    if (photo_type == ".jpg" || photo_type == ".png" || photo_type == ".jpeg") {
        if ($("[id*=get_photo]").get(0).files[0].size > 50000) {
            alert("File Size Should be not more than 50Kb");
            photo.value = '';
        }
        else {
            readURL(this, 'stud_photo');
        }
    }
    else {
        alert("Image should be in .JPG or .PNG format");
        $("[id*=get_photo]").val('');
    }
});

$("[id*=get_sign]").change(function () {
    var photo_type = "";
    var sign = $("[id*=get_sign]").val();
    if ($("[id*=get_sign]").val() != '') {
        res = $("[id*=get_sign]").get(0).files[0].name.split(".");
        photo_type = '.' + res[res.length - 1];
    }
    if (photo_type == ".jpg" || photo_type == ".png" || photo_type == ".jpeg") {
        if ($("[id*=get_sign]").get(0).files[0].size > 50000) {
            alert("File Size Should be not more than 50Kb");
            sign.value = '';
        }
        else {
            readURL(this, 'stud_sign');
        }
    }
    else {
        alert("Image should be in .JPG or .PNG format");
        $("[id*=get_sign]").val('');
    }
});

function readURL(input, imgID) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $("[id*=" + imgID + "]").attr('src', e.target.result);
        }

        reader.readAsDataURL(input.files[0]);
    }
}

$("[id*=btntoggle]").click(function () {
    var name = $("[id*=btntoggle]").text();
    if (name == "Signature") {
        $("[id*=imagediv]").hide();
        $("[id*=signdiv]").show();
        $("[id*=btntoggle]").text('Photo');
    }
    else if (name == "Photo") {
        $("[id*=imagediv]").show();
        $("[id*=signdiv]").hide();
        $("[id*=btntoggle]").text('Signature');
    }
});

$("[id*=btnClearPhotos]").click(function () {
    $("[id*=get_photo]").val('');
    $("[id*=get_sign]").val('');
    var id = $("[id*=txtempID]").val();
    if (id != '') {
        $("[id*=stud_photo]").attr("src", $("[id*=lblphoto]")[0].innerHTML);
        $("[id*=stud_sign]").attr("src", $("[id*=lblsign]")[0].innerHTML);

    }
    else {
        $("[id*=stud_photo]").attr("src", "image/user.png");
        $("[id*=stud_sign]").attr("src", "image/sign.png");
        $("[id*=imagediv]").show();
        $("[id*=signdiv]").hide();
        $("[id*=btntoggle]").text('Signature');
    }
});

function uploadphoto(emp_id) {

    var img = $("[id*=get_photo]").val();
    var ext = img.split(';')[0].match(/jpg|jpeg|png|gif/)[0];

    // strip off the data: url prefix to get just the base64-encoded bytes
    var stud_photo = img.replace(/^data:image\/\w+;base64,/, "");


    //to save in folder
    var get_photo = $("[id*=get_photo]").get(0);
    var uploadedfiles = get_photo.files;


    var fromdata = new FormData();
    for (var i = 0; i < uploadedfiles.length; i++) {
        fromdata.append(uploadedfiles[i].name, uploadedfiles[i]);
    }

    if (uploadedfiles.length > 0) {
        $.ajax({
            url: 'Uploader.ashx?ID=' + emp_id + '?Photo',
            type: "POST",
            contentType: false,
            processData: false,
            data: fromdata,
            async: false,
            success: function (result) {
                if (result == 'File Uploaded Successfully!') {
                    //$("[id*=stud_photo]").attr("src", "../utkarsha/image/user.png");

                }
                else {

                }
            },
            error: function (err) {
                //  $.notify("Error ! Error Occured.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
            }
        });
    }
}

function uploadsign(emp_id) {

    var img = $("[id*=get_sign]").val();
    var ext = img.split(';')[0].match(/jpg|jpeg|png|gif/)[0];

    // strip off the data: url prefix to get just the base64-encoded bytes
    var stud_photo = img.replace(/^data:image\/\w+;base64,/, "");


    //to save in folder
    var get_photo = $("[id*=get_sign]").get(0);
    var uploadedfiles = get_photo.files;


    var fromdata = new FormData();
    for (var i = 0; i < uploadedfiles.length; i++) {
        fromdata.append(uploadedfiles[i].name, uploadedfiles[i]);
    }

    if (uploadedfiles.length > 0) {
        $.ajax({
            url: 'Uploader.ashx?ID=' + emp_id + '?Sign',
            type: "POST",
            contentType: false,
            processData: false,
            data: fromdata,
            async: false,
            success: function (result) {
                if (result == 'File Uploaded Successfully!') {
                    //$("[id*=stud_sign]").attr("src", "../utkarsha/image/sign.png");
                }
                else {

                }
            },
            error: function (err) {
                //  $.notify("Error ! Error Occured.", { color: "#fff", background: "#D44950", blur: 0.2, delay: 0 });
            }
        });
    }
}