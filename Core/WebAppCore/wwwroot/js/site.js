$(function () {
    if ($("img.lazy").length > 0) {
        $("img.lazy").lazyload({
            effect: "fadeIn",
            effectspeed: 300
        });
    }
    $("#coin_search input").keyup(function (e) {
        var coin_key = $(this).val();
        if (coin_key) {
            coin_key = coin_key.toLowerCase();
            $("#coin_table .coin-main .coin-name").each(function () {
                var litit = $(this).attr("data-search");
                litit = litit.toLowerCase();
                var coin_tr = $(this).closest("tr");
                if (litit.indexOf(coin_key) != -1) {
                    coin_tr.show();
                } else {
                    coin_tr.hide();
                }
            });
        } else {
            $("#coin_table .coin-main tr").show();
        }
        $(window).trigger("scroll");
    });
});