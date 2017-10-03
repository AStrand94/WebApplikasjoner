$(document).ready(function () {

    $('#searchButton').on('click', function () {
        var str = $('#searchText').val();

        $.ajax({
            data: { Reference: str },
            type: "post",
            url: "/home/_OrderView",
            success: function (response) {
                $('#searchResponse').html(response);
            }
        });
    });

    $('#searchText').keypress(function (e) {
        if (e.which == 13) {

            var str = $('#searchText').val();

            $.ajax({
                data: { Reference: str },
                type: "post",
                url: "/home/_OrderView",
                success: function (response) {
                    $('#searchResponse').html(response);
                }
            });
        }
    });
});