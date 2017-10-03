$(document).ready(function () {
    $('#returnDateDiv').empty();
    $('.datepicker').datepicker({
        format: 'mm/dd/yyyy',
        startDate: '+1d'
    });

    $('.date').datepicker().on('changeDate', function (ev) {
        $('.date').datepicker('hide');
        var date = $('#date').val();
        if (!isValidDate(date)) {
            $('#errorLabel').text("Wrong date");
        }
    });

    $('#toDate').datepicker().on('changeDate', function (ev) {
        $('#toDate').datepicker('hide');
    });

    $('#singleTrip').click(function () {
        $("#returnDateDiv").empty();
    });

    $('#roundtrip').click(function () {
        if ($("#returnDateDiv").is(':empty')) {
            $("#returnDateDiv").prepend('<label for="returnDate">Return date:</label ><div class="input-group date" data-provide="datepicker" style="width:285px;" id="datepicker2"><input id="returnDate" name="returnDate" type="text" class="form-control"><div class="input-group-addon"><span class="glyphicon glyphicon-th"></span></div></div>');
        }
    });

    $('#searchForm').submit(function (e) {
        e.preventDefault();

        if (!checkForm(this)) return false;

        $.ajax({
            data: $(this).serialize(),
            type: $(this).attr('method'),
            url: $(this).attr('action'),
            success: function (response) {
                $('#created').html(response).hide().fadeIn();
            }
        });
        return false; // cancel original event to prevent form submitting
    });
});

function isValidDate(dateString) {
    if (!/^\d{1,2}\/\d{1,2}\/\d{4}$/.test(dateString))
        return false;

    var parts = dateString.split("/");
    var day = parseInt(parts[1], 10);
    var month = parseInt(parts[0], 10);
    var year = parseInt(parts[2], 10);

    if (year < 1000 || year > 3000 || month == 0 || month > 12)
        return false;

    var monthLength = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];

    if (year % 400 == 0 || (year % 100 != 0 && year % 4 == 0))
        monthLength[1] = 29;

    return day > 0 && day <= monthLength[month - 1];
};

function returnDateIsLater(date1, date2) {

    var parts = date1.split("/");
    var day = parseInt(parts[1], 10);
    var month = parseInt(parts[0], 10);
    var year = parseInt(parts[2], 10);

    var parts2 = date2.split("/");
    var day2 = parseInt(parts2[1], 10);
    var month2 = parseInt(parts2[0], 10);
    var year2 = parseInt(parts2[2], 10);

    if (year < year2) return true;
    else if (year > year2) return false;
    else if (month > month2) return false;
    else if (day >= day2) return false;
    return true;
}

function checkForm(formVals) {
    var from = formVals.fromAirportId.value;
    var to = formVals.toAirportId.value;
    var date = formVals.date.value;
    var date2 = null;

    if (from == null || to == null || from == "" || to == "") {
        document.getElementById('errorLabel').innerHTML = 'Specify where you want to travel!';
        return false;
    }

    if (document.getElementById('returnDate')) {
        if (isValidDate(document.getElementById('returnDate').value)) {
            date2 = document.getElementById('returnDate').value;
        } else {
            document.getElementById('errorLabel').innerHTML = 'Specify returndate!';
            return false;
        }
    }

    if (date2 != null && !returnDateIsLater(date, date2)) {
        document.getElementById('errorLabel').innerHTML = 'Return-date must be after travel-date!';
        return false;
    }

    if (from === to || !from || !to) {
        document.getElementById('errorLabel').innerHTML = 'You cannot travel to the same airport!';
        return false;
    } else if (!isValidDate(date)) {
        document.getElementById('errorLabel').innerHTML = 'Wrong format for the date';
        return false;
    }
    return true;
}