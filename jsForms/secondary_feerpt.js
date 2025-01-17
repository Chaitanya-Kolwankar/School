$(document).ready(function () {
    if (recptno != "" || recptno != null) {

        var updateDetails = {
            type: "printdata",
            receipt_no: recptno,

        }
        $.ajax({
            //type: "POST",

            url: urllink + 'FeeEntry/',
            contentType: 'application/json; charset=utf-8',
            type: 'POST',
            data: JSON.stringify(updateDetails),
            datatype: 'json',
            async: false,
            success: function (data, textStatus, xhr) {

                if (data != "No Data Found") {
                    $("[id*=tblfees]").show();
                    $("[id*=tblfees]").empty();
                    if (data.Table2[0].Type == "Refund") {
                        if (data.Table3.length > 0) {
                            $("[id*=tblfees]").append("<thead><tr ><th style='word-break: break-word;width: 90px;font-weight: 400;'><center>SR No.</center></th><th style='font-weight: 400;'>Fee Particulars</th><th style='word-break: break-word;width: 154px;font-weight: 400;'><center >Rate of Fees Amount Rs.</center></th></tr></thead>");
                            for (var i = 0; i < data.Table3.length; i++) {
                                if (i == 0) {
                                    $("[id*=tblfees]").append("<tbody>");
                                }
                                $("[id*=tblfees]").append("<tr><td style='font-weight:bold'><center>" + (i + 1) + "</center></td><td style='font-weight:bold;text-align: left;'>" + data.Table3[i].refund_details + "</td><td><center>" + data.Table3[i].Amount + "</center></td></tr>");
                                if (i == data.Table3.length - 1) {

                                    $("[id*=tblfees]").append("</tbody>");
                                }
                            }
                        }
                        if (data.Table2.length > 0) {
                            // $("[id*=lbl_date]").Value = Date.now();
                            var dt = new Date();
                            year = dt.getFullYear();
                            month = (dt.getMonth() + 1).toString().padStart(2, "0");
                            day = dt.getDate().toString().padStart(2, "0");
                            document.getElementById("lbl_date").innerText = day + "/" + month + "/" + year;
                            if (data.Table2[0].gender == "Male") {

                                document.getElementById("gender").innerText = "Mast . ";

                            }
                            else if (data.Table2[0].gender == "Female") {
                                document.getElementById("gender").innerText = "Miss . ";
                            }
                            else {
                                document.getElementById("gender").innerText = "Mast / Miss . ";
                            }
                            document.getElementById("recpt").innerText = recptno;
                            document.getElementById("txtname").innerText = data.Table2[0].Student_Name;
                            document.getElementById("lbl_std").innerText = data.Table2[0].standard;
                            document.getElementById("lbl_div").innerText = data.Table2[0].Division;

                            var arr = (data.Table2[0].Year);
                            var splarr = arr.split("-");
                            var fromyear = splarr[0].split("/");
                            var toyear = splarr[1].split("/");

                            document.getElementById("lbl_year").innerText = fromyear[2] + " - " + toyear[2];
                            document.getElementById("frmyer").innerText = fromyear[2];
                            document.getElementById("toyer").innerText = toyear[2];
                            document.getElementById("dop").innerText = data.Table2[0].Pay_date;
                            //document.getElementById("totword").style.display = "none";
                            document.getElementById("totalwords").innerText = convertNumberToWords(data.Table3[0].Amount);
                            if (data.Table2[0].Recpt_mode == "Cash") {
                                document.getElementById("paymentdetails").style.display = "none";
                            }
                            else {
                                document.getElementById("paymentdetails").style.display = "block";
                                document.getElementById("mode").innerText = data.Table2[0].Recpt_mode;
                                document.getElementById("cheqno").innerText = data.Table2[0].Recpt_Chq_No;
                                //document.getElementById("bankname").innerText = data.Table2[0].Recpt_Chq_dt;
                                document.getElementById("bankname").innerText = data.Table2[0].Recpt_Bnk_Name;
                                document.getElementById("bankbranch").innerText = data.Table2[0].Recpt_Bnk_Branch;



                            }

                            //document.getElementById("txtname").innerText = data.Table2[0].Student_Name;


                        }
                    }
                    else {
                        if (data.Table1.length > 0) {
                            $("[id*=tblfees]").append("<thead><tr ><th style='word-break: break-word;width: 90px;font-weight: 400;'><center>SR No.</center></th><th style='font-weight: 400;'>Fee Particulars</th><th style='word-break: break-word;width: 154px;font-weight: 400;'><center >Rate of Fees Amount Rs.</center></th><th style='word-break: break-word;width: 210px;font-weight: 400;'><center >Total Yearly Fee's Amount Rs.</center></th></tr></thead>");
                            for (var i = 0; i < data.Table1.length; i++) {
                                if (i == 0) {
                                    $("[id*=tblfees]").append("<tbody>");
                                }
                                $("[id*=tblfees]").append("<tr><td style='font-weight:bold'><center>" + (i + 1) + "</center></td><td style='font-weight:bold;text-align: left;'>" + data.Table1[i].struct_name + "</td><td><center>" + data.Table1[i].rateamount + "</center></td><td><center>" + data.Table1[i].totalamount + "</center></td></tr>");
                                if (i == data.Table1.length - 1) {
                                    if (data.Table.length > 0) {
                                        $("[id*=tblfees]").append("<tr><td colspan='3' style='font-weight:bold'><center>TOTAL OF YEARLY FEES AMOUNT</center></td><td><center>" + data.Table[0].total + "</center></td></tr>");
                                        $("[id*=tblfees]").append("<tr><td colspan='3' style='font-weight:bold'><center>PAID AMOUNT</center></td><td><center>" + data.Table[0].paid + "</center></td></tr>");
                                        $("[id*=tblfees]").append("<tr><td colspan='3' style='font-weight:bold'><center>BALANCE FEES</center></td><td><center>" + data.Table[0].balance + "</center></td></tr>");
                                    }
                                    else {
                                        $("[id*=tblfees]").append("<tr><td colspan='3' style='font-weight:bold'><center>TOTAL OF YEARLY FEES AMOUNT</center></td><td><center></center></td></tr>");
                                        $("[id*=tblfees]").append("<tr><td colspan='3' style='font-weight:bold'><center>PAID AMOUNT</center></td><td><center></center></td></tr>");
                                        $("[id*=tblfees]").append("<tr><td colspan='3' style='font-weight:bold'><center>BALANCE FEES</center></td><td><center></center></td></tr>");

                                    }
                                    $("[id*=tblfees]").append("</tbody>");
                                }
                            }
                        }


                        if (data.Table2.length > 0) {
                            // $("[id*=lbl_date]").Value = Date.now();
                            var dt = new Date();
                            year = dt.getFullYear();
                            month = (dt.getMonth() + 1).toString().padStart(2, "0");
                            day = dt.getDate().toString().padStart(2, "0");
                            document.getElementById("lbl_date").innerText = day + "/" + month + "/" + year;
                            if (data.Table2[0].gender == "Male") {

                                document.getElementById("gender").innerText = "Mast . ";

                            }
                            else if (data.Table2[0].gender == "Female") {
                                document.getElementById("gender").innerText = "Miss . ";
                            }
                            else {
                                document.getElementById("gender").innerText = "Mast / Miss . ";
                            }
                            document.getElementById("recpt").innerText = recptno;
                            document.getElementById("txtname").innerText = data.Table2[0].Student_Name;
                            document.getElementById("lbl_std").innerText = data.Table2[0].standard;
                            document.getElementById("lbl_div").innerText = data.Table2[0].Division;

                            var arr = (data.Table2[0].Year);
                            var splarr = arr.split("-");
                            var fromyear = splarr[0].split("/");
                            var toyear = splarr[1].split("/");

                            document.getElementById("lbl_year").innerText = fromyear[2] + " - " + toyear[2];
                            document.getElementById("frmyer").innerText = fromyear[2];
                            document.getElementById("toyer").innerText = toyear[2];

                            document.getElementById("totalwords").innerText = convertNumberToWords(data.Table[0].paid);

                            //document.getElementById("txtname").innerText = data.Table2[0].Student_Name;
                            document.getElementById("dop").innerText = data.Table2[0].Pay_date;
                            if (data.Table2[0].Recpt_mode == "Cash") {
                                document.getElementById("paymentdetails").style.display = "none";
                            }
                            else {
                                document.getElementById("paymentdetails").style.display = "block";
                                document.getElementById("mode").innerText = data.Table2[0].Recpt_mode;
                                document.getElementById("cheqno").innerText = data.Table2[0].Recpt_Chq_No;
                                //document.getElementById("bankname").innerText = data.Table2[0].Recpt_Chq_dt;
                                document.getElementById("bankname").innerText = data.Table2[0].Recpt_Bnk_Name;
                                document.getElementById("bankbranch").innerText = data.Table2[0].Recpt_Bnk_Branch;



                            }

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
});


function convertNumberToWords(amount) {
    var words = new Array();
    words[0] = '';
    words[1] = 'One';
    words[2] = 'Two';
    words[3] = 'Three';
    words[4] = 'Four';
    words[5] = 'Five';
    words[6] = 'Six';
    words[7] = 'Seven';
    words[8] = 'Eight';
    words[9] = 'Nine';
    words[10] = 'Ten';
    words[11] = 'Eleven';
    words[12] = 'Twelve';
    words[13] = 'Thirteen';
    words[14] = 'Fourteen';
    words[15] = 'Fifteen';
    words[16] = 'Sixteen';
    words[17] = 'Seventeen';
    words[18] = 'Eighteen';
    words[19] = 'Nineteen';
    words[20] = 'Twenty';
    words[30] = 'Thirty';
    words[40] = 'Forty';
    words[50] = 'Fifty';
    words[60] = 'Sixty';
    words[70] = 'Seventy';
    words[80] = 'Eighty';
    words[90] = 'Ninety';
    amount = amount.toString();
    var atemp = amount.split(".");
    var number = atemp[0].split(",").join("");
    var n_length = number.length;
    var words_string = "";
    if (n_length <= 9) {
        var n_array = new Array(0, 0, 0, 0, 0, 0, 0, 0, 0);
        var received_n_array = new Array();
        for (var i = 0; i < n_length; i++) {
            received_n_array[i] = number.substr(i, 1);
        }
        for (var i = 9 - n_length, j = 0; i < 9; i++, j++) {
            n_array[i] = received_n_array[j];
        }
        for (var i = 0, j = 1; i < 9; i++, j++) {
            if (i == 0 || i == 2 || i == 4 || i == 7) {
                if (n_array[i] == 1) {
                    n_array[j] = 10 + parseInt(n_array[j]);
                    n_array[i] = 0;
                }
            }
        }
        value = "";
        for (var i = 0; i < 9; i++) {
            if (i == 0 || i == 2 || i == 4 || i == 7) {
                value = n_array[i] * 10;
            } else {
                value = n_array[i];
            }
            if (value != 0) {
                words_string += words[value] + " ";
            }
            if ((i == 1 && value != 0) || (i == 0 && value != 0 && n_array[i + 1] == 0)) {
                words_string += "Crores ";
            }
            if ((i == 3 && value != 0) || (i == 2 && value != 0 && n_array[i + 1] == 0)) {
                words_string += "Lakhs ";
            }
            if ((i == 5 && value != 0) || (i == 4 && value != 0 && n_array[i + 1] == 0)) {
                words_string += "Thousand ";
            }
            if (i == 6 && value != 0 && (n_array[i + 1] != 0 && n_array[i + 2] != 0)) {
                words_string += "Hundred and ";
            } else if (i == 6 && value != 0) {
                words_string += "Hundred  ";
            }
        }
        words_string = words_string.split("  ").join(" ");
    }
    return words_string;

}
