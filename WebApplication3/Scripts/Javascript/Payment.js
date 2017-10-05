$(document).ready(function () {
    var lastInput = $("#expDate").val();
    $("#expDate").keyup(function () {
        var input = $("#expDate").val();
        if (input.length == 2 && lastInput.length != 3) {
            $('#expDate').val($('#expDate').val() + '/');
        }
        if (input.length < lastInput.length || input.length == 2 && lastInput.length == 2) {
            $('#expDate').val('');
            lastInput = 0;
        } else {
            lastInput = input;
        }
    });
});

var onSubmitForm = function () {

    if ($('#cardNumber').val().length != 16) {
        alert("Cardnumber must be 16 digit!");
        return false;
    }
    if (!$.isNumeric($("#cardNumber").val())) {
        alert("Card Number can only have numbers");
        return false;
    }
    if (!$('#expDate').val()) {
        alert("Expiration Date is required.");
        return false;
    }

    var expReg = /^\d{2}\/\d{2}$/;
    if (!expReg.test($('#expDate').val())) {
        alert('Format on CVC must be xx/xx');
        return false;
    } else {
        var parts = $('#expDate').val().split('/');
        if (parseInt(parts[0]) > 12) {
            alert("Wrong expiration date, month cannot be " + parts[0])
            return false;
        }
    }

    if (!$.isNumeric($("#cvc").val())) {
        alert("CVC can only have numbers");
        return false;
    }
    if ($('#cvc').val().length != 3) {
        alert("CVC must be 3 digit!");
        return false;
    }
    return true;
}