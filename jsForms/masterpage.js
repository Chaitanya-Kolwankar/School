$(document).ready(function () {

    fill_module();
    fill_form();
})


function fill_module() {
    var emp_id = empId;

    $.ajax({
        type: "POST",
        url: "Login.aspx/fillModule",
        data: '{emp_id:"' + empId + '"}',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.d.length > 0) {
                for (var i=0;i<data.d.length;i++)
                {
                    var module = document.getElementById(data.d[i].module_name);
                    if (module != null) {
                        module.style.display = "block";
                    }
                }
            }
        }
    });

}

function fill_form() {
    var emp_id = empId;

    $.ajax({
        type: "POST",
        url: "Login.aspx/fillform",
        data: '{emp_id:"' + empId + '"}',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.d.length > 0) {
                for (var i = 0; i < data.d.length; i++) {
                    var form = document.getElementById(data.d[i].form_name);
                    if (form != null) {
                        form.style.display = "block";
                    }
                }
               

            }
        }
    });


}