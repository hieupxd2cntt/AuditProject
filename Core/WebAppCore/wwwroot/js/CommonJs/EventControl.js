$(document).ready(function () {
    $(".call-back").change(function () {
        var value = $(this).val();
        var id = $(this).attr("id");
        var modId = $(this).closest('form').find("#txtModId").val();
        CallBack(value, id,modId);
    });
});

function CallBack(value,id,mod) {
    $.ajax({
        type: 'POST',
        url: $("#urlCallBack").val(),
        data: jQuery.param({ valueControl: value, idControl: id, modId: mod}),
        async: false,

        success: function (response) {
            debugger;
            var t = Object.keys(response[0]);
            if (parseInt(response.resultCode) > 0) {
                if (response.data == "success") {
                    bootbox.alert("Xóa dữ liệu thành công", function () {
                        location.reload();
                    });
                }
                else {
                    bootbox.alert(response.data);
                }
            }
            else {
                alert(response.messeage);
            }
        },
        error: function (error) {
            console.log(error);
            rs = - 1;
        }
    });
}