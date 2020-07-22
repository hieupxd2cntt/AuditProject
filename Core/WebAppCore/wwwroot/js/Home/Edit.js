$(".enter-go-mod").keypress(function (k) {
    if (k.keyCode === 13) {//FullScreen. Phím enter
        var modId = $(this).attr("mod-enter");
        var fieldName = $(this).attr("name");
        var value = $(this).val();
        var key=$("#txtKeyEdit").val();
        var url = $("#host-address").val() + "/Home/GoToMod?modId=" + modId + "&fieldName=" + fieldName + "&parr=" + value + "&key=" + key;

        window.location.href = url;
    }
});
$("#btnGoToMod").click(function () {
    var text = $(this).closest("div").find(".enter-go-mod");
    var modId = text.attr("mod-enter");
    var fieldName = text.attr("name");
    var value = text.val();
    var key = $("#txtKeyEdit").val();
    var url = $("#host-address").val() + "/Home/GoToMod?modId=" + modId + "&fieldName=" + fieldName + "&parr=" + value + "&key=" + key;
    window.location.href = url;
});

$(".btn-add-grid").click(function () {
    debugger;
    var input = $('#trInput').find(':input');
    var fields = new Array();
    for (var i = 0; i < input.length; i++) {
        if (input[i].type=="button") {
            continue;
        }
        var field = new Object();
        field.FieldID = input[i].value;
        field.FieldName = input[i].name;
        fields.push(field);
    }
    CallAddGrid(fields);
});
function CallAddGrid(fields) {
    $.ajax({
        type: 'POST',
        url: $("#urlAddRowGidModMaintain").val(),
        data: jQuery.param({ modId: $("#txtModId").val(), fieldInfos: fields }),
        async: false,
        success: function (response) {
            debugger;
        },
        error: function (error) {
            console.log(error);
            rs = - 1;
        }
    });
}