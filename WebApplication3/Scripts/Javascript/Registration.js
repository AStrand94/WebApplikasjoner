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

    $('#country').keyup(function () {
        if ($.isNumeric($(this).val())) {
            alert("Country can not be numeric");
        }
    });

    $('#city').keyup(function () {
        if ($.isNumeric($(this).val())) {
            alert("City can not be numeric");
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
    if (!$('#address').val()) {
        alert("Address is required.");
        return false;
    }
    if (!$('#zipcode').val()) {
        alert("Zip Code is required.");
        return false;
    }
    if (!$('#city').val()) {
        alert("City is required.");
        return false;
    }
    if (!$('#country').val()) {
        alert("Country is required.");
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