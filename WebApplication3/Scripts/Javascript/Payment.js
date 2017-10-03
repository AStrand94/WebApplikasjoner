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


    var onSubmitForm = function () {
        cardNumber = $("#cardNumber");

        if (!cardNumber.val()) {
            alert("Card Number is required");
            return false;
        }
        if (!$.isNumeric(cardNumber.val())) {
            alert("Card Number can only have numbers");
            return false;
        }
        if (!$('#expDate').val()) {
            alert("Expiration Date is required.");
            return false;
        }
        if (!$('#cvc').val()) {
            alert("CVC is required.");
            return false;
        }
    }
});