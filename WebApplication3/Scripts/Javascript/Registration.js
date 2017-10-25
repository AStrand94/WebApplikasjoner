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

    $('#regForm').on('submit', function () {

        var message = '';
        $('.firstname').each(function () {
            if (!$(this).val()) {
                message = "Firstname is required.";
                return false;
            }
        });

        if(message === '')
        $('.lastname').each(function () {
            if (!$(this).val()) {
                message = "Lastname is required.";
                return false;
            }
        });

        if (message === '')
        $('.telephone').each(function () {
            if (!$(this).val()) {
                message = "Telephone is required.";
                return false;
            }
        });

        if (message === '')
        $('.telephone').each(function () {
            if (!isPhoneNumber($(this).val())) {
                message = "Not a valid phonenumber.";
                return false;
            }
        });

        if (message === '')
        $('.email').each(function () {
            if (!$(this).val()) {
                message = "Email is required.";
                return false;
            }
        });

        if (message === '')
        $('.email').each(function () {
            if (!isEmail($(this).val())) {
                message = "Not a valid email.";
                return false;
            }
        });

        if (message !== '') {
            alert(message);
            return false;
        }
    });

});