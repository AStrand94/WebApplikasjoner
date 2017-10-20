$(document).ready(function () {
    $("#numbersRow").hide();

    $("#numbersButton").click(function () {
        $("#numbersRow").show(1000);
    });

    $("#closeButton").click(function () {
        $("#numbersRow").hide(1000);
    });
});