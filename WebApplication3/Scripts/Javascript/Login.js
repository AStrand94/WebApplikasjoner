$(document).ready(function () {

    $('#loginButton').on('click', function () {
        var str = $('#loginButton').val();

        
    });

    $('#username').keypress(function (e) {
        if (e.which == 13) {
            
        }
    });

    $('#password').keypress(function (e) {
        if (e.which == 13) {

        }
    });

    $('.closeModal').on('click', function () {
        $('#username').val('');
        $('#password').val('');
    });
});