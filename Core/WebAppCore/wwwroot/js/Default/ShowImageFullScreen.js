var zoom = 40;
var tabindex = 0;
var isShowFullScreen = 0;
$(document).ready(async function () {
    if ($("#timeSleepChangeModel").length > 0) {
        var timeSleep = $("#timeSleepChangeModel").val();//Trường hợp đã đổi model. nhưng đến thời điểm nextpage trước thời điểm đổi model. thời gian còn lại để phải đổi model
        if (parseInt(timeSleep)>0) {
            await timeout(parseInt(timeSleep + "000"));
            $("#isNextPage").val(0);
            $("#currPage").val(1);
            console.log("Is Next Page =" + $("#isNextPage").val());
            //$("#form-display").submit();
            LoadSop();
            //location.reload();
        }   
    }
    $('[tabindex=' + tabindex + ']').focus();
    $(".img").keydown(function (k) {
        tab = $(this).attr("tabindex");
        $('[tabindex=' + tabindex + ']').focus();
        if (k.keyCode === 105) {//Zoom in Phím i
            if (zoom < 150) {
                $(this).css("cursor", "pointer");
                $(this).animate({ width: zoom + "%", height: zoom + "%" }, 'slow');
                zoom = zoom + 10;
            }
        }
        else if (k.keyCode === 79) {//Zoom Out. Phím o
            $('[tabindex=' + 10 + ']').focus();
            //if (zoom > 20) {
            //    $(this).css("cursor", "pointer");
            //    $(this).animate({ width: zoom + "%", height: zoom + "%" }, 'slow');
            //    zoom = zoom - 10;
            //}
        }
        else if (k.keyCode === 13) {//FullScreen. Phím enter
            ShowFullScreen(this, 1);
        }
        else if (k.keyCode === 39) {//Phím mũi tên sang phải. chuyển tabindex sang ảnh tiếp
            tabindex = parseInt(tab) + 1;;
            FocusNextElement();

            //$('[tabindex=' + tabindex + ']').focus();
        }
        else if (k.keyCode === 37) {//Phím mũi tên sang trái. chuyển tabindex sang ảnh trước
            tabindex = parseInt(tab) - 1;
            FocusBackElement();
            //$('[tabindex=' + tabindex + ']').focus();
        }
        //else if (k.keyCode === 53) {
        //    alert("scan");
        //}
    });
    $(".img").dblclick(function () {
        ShowFullScreen(this, 1);
    });
    $("#myModal").dblclick(function (k) {
        var modal = document.getElementById("myModal");
        modal.style.display = "none";
        $('[tabindex=' + tabindex + ']').focus();
        isShowFullScreen = 0;
        autpNextPage();
        return;
    });
    $("#imgfullScreen").keydown(function (k) {
        if (k.keyCode === 13) {//Exit FullScreen. Phím enter
            var modal = document.getElementById("myModal");
            modal.style.display = "none";
            $('[tabindex=' + tabindex + ']').focus();
            isShowFullScreen = 0;
            autpNextPage();
            return;
        }
    });
    $("#myModal").keydown(function (k) {
        if (k.keyCode === 13) {//Exit FullScreen. Phím enter
            var modal = document.getElementById("myModal");
            modal.style.display = "none";
            $('[tabindex=' + tabindex + ']').focus();
            isShowFullScreen = 0;
            autpNextPage();
            return;
        }
    });

    $("#close-modal").click(function (k) {
        var modal = document.getElementById("myModal");
        modal.style.display = "none";
        $('[tabindex=' + tabindex + ']').focus();
    });
    //$(".pagination li a").keydown(function (k) {
    //    debugger;
    //});
    //if (parseInt($("#totalPage").val()) > 1) {
    //    var tab =4;
    //    $(".pagination li a").each(function () {
    //        $(this).attr("tabindex", tab);
    //        tab++;
    //    });
    //}
    //autpNextPage();
});
$(document).ready(function () {
    autpNextPage();
    if ($("#show1Page").val() === "Y") {
        ShowFullScreen($("#img0"), 0);
    } else {
        autoChangeModView();
    }
    
    $("#sample_1_paginate a").keydown(function (k) {
         if (k.keyCode === 39) {//Phím mũi tên sang phải. chuyển tabindex sang ảnh tiếp
             tabindex = parseInt($(this).attr('tabindex')) + 1;;
            FocusNextElement();
         } else if (k.keyCode === 37) {//Phím mũi tên sang trái. chuyển tabindex sang ảnh trước
             tabindex = parseInt($(this).attr('tabindex')) - 1;;
             FocusBackElement();
             //$('[tabindex=' + tabindex + ']').focus();
        }
         else if (k.keyCode === 13) {//Phím Enter
             $(this).click();
            
         }
    });
});
function FocusNextElement() {
    $('#divAll [tabindex]').each(function (a) {
        var tabControl = $(this).attr('tabindex');;
        if (tabControl >= tabindex) {
            tabindex = tabControl;
            $('[tabindex=' + tabindex + ']').focus();
            return false;
        }
    });
}
function FocusBackElement() {
    $($('#divAll [tabindex]').get().reverse()).each(function (a) {
        var tabControl = $(this).attr('tabindex');;
        if (tabControl <= tabindex) {
            tabindex = tabControl;
            $('[tabindex=' + tabindex + ']').focus();
            return false;
        }
    });
}


async function autoChangeModView() {
    var timeChangeView = $("#timeChangeView").val();
    var i = -1;
    while (true) {
        if (isShowFullScreen == 0) {
            if (i == -1) {
                await timeout(timeChangeView * 1000);
                i++;
            }
            else {
                var img = $("#img" + i);
                if (img.length > 0) {
                    ShowFullScreen(img, 0);
                    await timeout(timeChangeView * 1000);
                    i++;
                }
                else {
                    var modal = document.getElementById("myModal");
                    modal.style.display = "none";
                    i = -1;
                }
            }
        }
        else {
            await timeout(timeChangeView * 1000);
        }
    }
}

function ShowFullScreen(image, fullScreen) {
    isShowFullScreen = fullScreen;
    // Get the modal
    var modal = document.getElementById("myModal");

    // Get the image and insert it inside the modal - use its "alt" text as a caption
    var img = image;
    var modalImg = document.getElementById("imgfullScreen");
    var captionText = "";//document.getElementById("caption");
    modal.style.display = "block";
    if (fullScreen == 0) {//Trường hợp auto show full 1 page
        modalImg.src = img.attr("src");
    }
    else {
        modalImg.src = img.src;
    }

    captionText.innerHTML = this.alt;
    //$('[tabindex=' + 999 + ']').focus();
    $("#imgfullScreen").focus();
    // When the user clicks on <span> (x), close the modal

}

function PagerClick(page) {
    $("#isNextPage").val(1);
    $("#currPage").val(page);
    //$("#form-display").submit();
    LoadSop();
}
function timeout(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
}

//async function sleep(ms) {
//    await timeout(ms);
//    return false;
//}
async function autpNextPage() {
    var timeReload = parseInt($("#timeReload").val());
    await timeout(timeReload * 1000);
    var curPage = parseInt($("#currPage").val());
    var totalPage = parseInt($("#totalPage").val());
    if (curPage == totalPage) {
        curPage = 1;
    }
    else {
        curPage++;
    }
    if (isShowFullScreen == 0 && totalPage>1) {
        PagerClick(curPage);
    }

    return false;
}

function LoadSop()
{
    $.ajax({
        type: 'POST',
        url: $("#urlLoadSop").val(),
        data: jQuery.param({ page: parseInt($("#currPage").val()), sBarcode: $("#barcode").val() }),
        async: false,
        success: function (response) {
            $("#divSop").empty().append(response);
        },
        error: function (error) {
            console.log(error);
            rs = - 1;
        }
    });
    //$.ajax({
    //    url: $("#urlLoadSop").val(),
    //    type: 'POST',
    //    data: jQuery.param({ page: parseInt($("#currPage").val())}),
    //    contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
    //    //data: { page: parseInt($("#currPage").val())},
    //    beforeSend: function () {
    //        //$("#div-loading").show();
    //    },
    //    success: function (result) {
    //        $("#divSop").empty().append(result)
    //        //$("#div-loading").hide();
    //    }
    //});
}