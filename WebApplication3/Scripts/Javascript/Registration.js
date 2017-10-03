function isEmail(email) {
    var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    return regex.test(email);
}

function isPhoneNumber(phonenumber) {
    var regex = /^[0-9]{6,9}$/;
    return regex.test(phonenumber);
}

$(document).ready(function () {

    $('#firstname').keyup(function () {
        if ($.isNumeric($(this).val())) {
            alert("First Name can not be numeric");
        }
    });

    $('#lastname').keyup(function () {
        if ($.isNumeric($(this).val())) {
            alert("Last Name can not be numeric");
        }
    });

});

var onSubmitForm = function () {
    if (!$('#firstname').val()) {
        alert("First Name is required.");
        return false;
    }
    if (!$('#lastname').val()) {
        alert("Last Name is required.");
        return false;
    }
    if (!$('#telephone').val()) {
        alert("Telephone is required.");
        return false;
    }
    if (!isPhoneNumber($('#telephone').val())) {
        alert("Not a valid Telephone Number");
        return false;
    }
    if (!$('#email').val()) {
        alert("Email is required.");
        return false;
    }
    if (!isEmail($('#email').val())) {
        alert("Not a valid Email");
        return false;
    }

    return true;
}