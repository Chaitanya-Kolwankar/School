$(document).ready(function () {

    $("#bcTarget").barcode($("#lbl_barcode").text(), "code39");
    $("#bcTarget1").barcode($("#lbl_barcode").text(), "code39");
});
