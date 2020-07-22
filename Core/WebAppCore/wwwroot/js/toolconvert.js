
$(function () {
    $("#swap_button").click(function () {
        var fromNum = $("#from_coin .input-coin").val();
        var toNum = $("#to_coin .input-coin").val();
        var from = $("#from_coin > div").clone();
        var to = $("#to_coin > div").clone();
        $("#from_coin").empty();
        $("#to_coin").empty();
        from.appendTo("#to_coin");
        to.appendTo("#from_coin");
        $("#from_coin .input-coin").val(fromNum).trigger("keyup");
        chuyenDoiChu();
    });

    $(document).on("keyup", ".input-coin", function (e) {
        var input = convertKeyCode(e);
        var value = $(this).val() + input;
        $(this).val(value);
        if (value) {
            var rateFrom = parseFloat($("#from_coin input").attr("data-rate"));
            var rateTo = parseFloat($("#to_coin input").attr("data-rate"));
            var out;
            if ($(this).closest(".form-group").is("#from_coin")) {
                out = parseFloat((value / rateFrom) * rateTo);
                $("#to_coin input").val(out);
            } else {
                out = parseFloat((value / rateTo) * rateFrom);
                $("#from_coin input").val(out);
            }
            chuyenDoiChu();
        }
    });

    function convertKeyCode(e) {
        var result = "";
        if (e.keyCode === 45)
            result = 0;
        if (e.keyCode === 35)
            result = 1;
        if (e.keyCode === 40)
            result = 2;
        if (e.keyCode === 34)
            result = 3;
        if (e.keyCode === 37)
            result = 4;
        if (e.keyCode === 12)
            result = 5;
        if (e.keyCode === 39)
            result = 6;
        if (e.keyCode === 36)
            result = 7;
        if (e.keyCode === 38)
            result = 8;
        if (e.keyCode === 33)
            result = 9;
        return result;
    }

    $("input.input-coin").bind({
        keydown: function (e) {
            if (e.shiftKey === true) {
                if (e.which == 9) {
                    return true;
                }
                return false;
            }
            if (e.which > 57) {
                return false;
            }
            if (e.which == 32) {
                return false;
            }
            return true;
        }
    });

    function chuyenDoiChu() {
        $("#convert_text").text("");
        if ($("#from_coin .input-coin").val() && $("#to_coin .input-coin").val()) {
            var coinFr = parseFloat($("#from_coin .input-coin").val());
            var coinTo = parseFloat($("#to_coin .input-coin").val());
            if (coinFr && coinTo) {
                var textFr = $("#from_coin .input-group-addon").text();
                var textTo = $("#to_coin .input-group-addon").text();
                var noidung = coinFr.toLocaleString() + " " + textFr + " = " + coinTo.toLocaleString() + " " + textTo;
                $("#convert_text").text(noidung);
                doiGiaTriBang();
            }
        }
    }
    function doiGiaTriBang() {
        var tienNhap = parseFloat($(".input-coin[data-mo='CAD']").val());
        if (tienNhap) {
            $("#bang_gia .tg_int").each(function () {
                var tgint = $(this).attr("data-int");
                if (tgint) {
                    var tmp_tygia = (tgint * tienNhap).toFixed(2);
                }
            });
        }
    }
    $("#bang_gia").ready(function () {
        for (i = 2; i <= 5; i++) {
            var min = 999999999;
            var max = 0;
            $("#bang_gia tr td:nth-child(" + i + ")").each(function () {
                var gt = $(this).attr("data-int");
                gt = parseFloat(gt).toFixed(2);
                if (gt) {
                    if (gt > 0) {
                        if (gt > max) {
                            max = gt;
                        }
                        if (gt < min) {
                            min = gt;
                        }
                    }
                }
            });
            if (max != min) {
                $("#bang_gia tr td[data-int='" + min + "']:nth-child(" + i + ")").addClass("text-red");
                $("#bang_gia tr td[data-int='" + max + "']:nth-child(" + i + ")").addClass("text-green");
            }
        }
    });
})