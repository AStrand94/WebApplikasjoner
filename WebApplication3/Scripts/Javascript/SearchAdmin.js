$(document).ready(function () {

$(".search").on('input', function () {
    var input = $(".search").val().toLowerCase();

    $(".searchItem").each(function () {
        var item = $(this).text().toLowerCase();

        if (!item.includes(input)) {
            $(this).parent().parent().parent().hide();
        } else {
            $(this).parent().parent().parent().show();
        }
    });
});

});