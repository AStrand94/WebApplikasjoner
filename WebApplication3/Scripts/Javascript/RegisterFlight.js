$(document).ready(function () {

    $(".singleToFlight").on('click', function () {
        id1 = $(this).children("input[name='flightId1']").val();
        $('#flightId1').val(id1);
        $('#flightId2').val('');

        var travelDesc, tfromstr, price, totalPrice;

        travelDesc = $(this).children("p[name='travelDescription']").text();
        tfromstr = $(this).children("p[name='travelFromString']").text();
        price = $(this).children("p[name='price']").text();


        price2 = $("#priceReturn").text();
        if (price2 == null || price2 == "") {
            $("#price").text(price);
        } else {
            $("#price").text((parseFloat(price) || 0) + (parseFloat(price2) || 0));
        }

        $("#travelDescription").text(travelDesc);
        $("#travelFromString").text(tfromstr);
        $("#travelToString").text("");
        $('#priceTo').text(price);

        $('#toFlightsDiv').children('a').each(function () {
            $(this).css('background-color', 'white');
        });

        $(this).css('background-color', 'palegreen');

        $('#sumLabel').show();
        $('#sumSpan').show();
    });

    $(".stopoverToFlight").on('click', function () {
        id1 = $(this).children("input[name='flightId1']").val();
        id2 = $(this).children("input[name='flightId2']").val();

        $('#flightId1').val(id1);
        $('#flightId2').val(id2);

        var travelDesc, tfromstr, ttostr, price;

        travelDesc = $(this).children("p[name='travelDescription']").text();
        tfromstr = $(this).children("p[name='travelFromString']").text();
        ttostr = $(this).children("p[name='travelToString']").text();
        price = $(this).children("p[name='price']").text();

        price2 = $("#priceReturn").text();
        $("#price").text((parseFloat(price) || 0) + (parseFloat(price2) || 0));

        $("#travelDescription").text(travelDesc);
        $("#travelFromString").text(tfromstr);
        $("#travelToString").text(ttostr);
        $('#priceTo').text(price);

        $('#toFlightsDiv').children('a').each(function () {
            $(this).css('background-color', 'white');
        });

        $(this).css('background-color', 'palegreen');

        $('#sumLabel').show();
        $('#sumSpan').show();
    });

    $(".singleReturnFlight").on('click', function () {
        id3 = $(this).children("input[name='flightId1']").val();

        $('#flightId3').val(id3);
        $('#flightId4').val('');

        var travelDesc2, tfromstr2, price2;

        travelDesc2 = $(this).children("p[name='travelDescription']").text();
        tfromstr2 = $(this).children("p[name='travelFromString']").text();
        price2 = $(this).children("p[name='price']").text();

        price = $("#priceTo").text();
        $("#price").text((parseFloat(price2) || 0) + (parseFloat(price) || 0));

        $("#travelDescription2").text(travelDesc2);
        $("#travelFromString2").text(tfromstr2);
        $("#travelToString2").text("");
        $('#priceReturn').text(price2);

        $('#returnFlightsDiv').children('a').each(function () {
            $(this).css('background-color', 'white');
        });

        $(this).css('background-color', 'palegreen');

        $('#sumLabel').show();
        $('#sumSpan').show();
    });

    $(".stopoverReturnFlight").on('click', function () {
        id3 = $(this).children("input[name='flightId1']").val();
        id4 = $(this).children("input[name='flightId2']").val();
        //alert(id3 + "\n" + id4);

        $('#flightId3').val(id3);
        $('#flightId4').val(id4);

        var travelDesc2, tfromstr2, ttostr2, price;

        travelDesc2 = $(this).children("p[name='travelDescription']").text();
        tfromstr2 = $(this).children("p[name='travelFromString']").text();
        ttostr2 = $(this).children("p[name='travelToString']").text();
        price2 = $(this).children("p[name='price']").text();

        price = $("#priceTo").text();

        $("#price").text((parseFloat(price2) || 0) + (parseFloat(price) || 0));


        $("#travelDescription2").text(travelDesc2);
        $("#travelFromString2").text(tfromstr2);
        $("#travelToString2").text(ttostr2);
        $('#priceReturn').text(price2);

        $('#returnFlightsDiv').children('a').each(function () {
            $(this).css('background-color', 'white');
        });

        $(this).css('background-color', 'palegreen');

        $('#sumLabel').show();
        $('#sumSpan').show();
    });
});

function checkFlightForm() {
    id1 = document.getElementById('flightId1').value;
    id3 = document.getElementById('flightId3').value;

    if (id1 == "") {
        alert("You must select where you want to travel!");
        return false;
    }
    return true;
}