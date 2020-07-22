$(document).ready(function () {
    if ($(".datepicker").length>0) {
        $(".datepicker").datepicker(
            { format: 'dd-mm-yyyy' }
        );
    }
    if ($(".select2").length > 0) {
        $(".select2").select2();
    }
    $(".chk-all").click(function () {
        $("#tbody input:checkbox").not(this).prop('checked', this.checked);
    });
    $("#btnBTN_DELETE").click(function () {        
        if ($(".chk:checked").length==0) {
            bootbox.alert("Bạn chưa chọn dữ liệu muốn xóa");
            return;
        }
        var keyDel = "";
        var arr = [];
        
        $(".chk:checked").each(function () {
            keyDel = $(this).closest("tr").find(".key-Del").val();
            arr.push(keyDel);
        });
        Delete(arr);
    });
    $(".action-del").click(function () {
        var keyDel = $(this).closest('td').find('.key-Del').val();
        var arr = [];
        arr.push(keyDel);
        Delete(keyDel);
    });
    $(".custom-file-input").change(function (e) {
        var names = "";
        for (var i = 0; i < $(this).get(0).files.length; ++i) {
            names += $(this).get(0).files[i].name+";";
        }
        $(this).closest(".custom-file").find(".custom-file-label").html(names);
        //for (var i = 0; i< $(this).length; i++) {
        //    var file=$(this).val();
        //    alert($(this).val().replace(/C:\\fakepath\\/i, ''));
        //}        
    });
    if ($("#txtError").length>0) {
        if ($("#txtError").val().length > 0) {
            bootbox.alert("" + $("#txtError").val());
        }
    }
    function toggleIcon(e) {
        $(e.target)
            .prev('.panel-heading')
            .find(".more-less")
            .toggleClass('ik-plus-circle ik-minus-circle');
    }
    $('.panel-group').on('hidden.bs.collapse1', toggleIcon);
    $('.panel-group').on('shown.bs.collapse1', toggleIcon);
});

function Delete(keyDel1) {
    bootbox.confirm("Bạn chắc chắn muốn xóa dữ liệu này?", function (result) {
        if (result) {
            $.ajax({
                type: 'POST',
                url: $("#urlDel").val(),
                data: jQuery.param({ modId: $("#txtModDel").val(), subModId: $("#txtSubModDel").val(), keyDels: keyDel1 }),
                async: false,
                success: function (response) {
                    if (response.resultCode== undefined) {
                        bootbox.alert("Bạn không có quyền thực hiện chức năng này");
                        return;
                    }
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
    });
}

$("#btnAddCondition").click(function () {
    $.ajax({
        type: 'POST',
        url: $("#urlLoadModSearchControl").val(),
        data: jQuery.param({ modId: $("#txtModId").val(), valueControl: $("#controlName").val(), index: $("#control-modsearch").length }),
        async: false,
        success: function (response) {
            $("#divSearchCondition").append(response);
            var i = 0;
            $("#divSearchCondition").find(".control-modsearch").each(function () {
                $(this).find(".control").find(':input').attr('id', 'Conditions[' + i + '].ConditionID');
                $(this).find(".control").find(':input').attr('name', 'Conditions[' + i + '].ConditionID');

                $(this).find(".condition").find(':input').attr('id', 'Conditions[' + i + '].Value');
                $(this).find(".condition").find(':input').attr('name', 'Conditions[' + i + '].Value');
                i++;
            });
            
        },
        error: function (error) {
            console.log(error);
            rs = - 1;
        }
    });
});
//function AutoRefesh() {
//    $.ajax({
//        type: 'POST',
//        url: $("#urlAutoRefesh").val(),
//        data: jQuery.param({ modId: $("#txtModDel").val(), subModId: $("#txtSubModDel").val(), keyDels: keyDel }),
//        processData: false,
//        async: false,
//        success: function (response) {
//            if (parseInt(response.resultCode) > 0) {
//                if (response.data == "success") {
//                    bootbox.alert("Xóa dữ liệuthành công", function () {
//                        location.reload();
//                    });
//                }
//                else {
//                    bootbox.alert(response.data);
//                }
//            }
//            else {
//                alert(response.messeage);
//            }
//        },
//        error: function (error) {
//            console.log(errror);
//            rs = - 1;
//        }
//    });
//}