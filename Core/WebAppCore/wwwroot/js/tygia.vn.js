$(function () {
    $(".fb-likes").attr("data-href", window.location.href);
    $(".fb-share-button").attr("data-href", window.location.href);

    $(".main-table").clone(true).appendTo('#table-scroll').addClass('clone');
    $(".main-table_1").clone(true).appendTo('#table-scroll_1').addClass('clone_1');

    $('.sub-menu').removeClass('show');
    $('.sub-menu').addClass('hide');

    var link = location.pathname.split('/')[1];
    if (link.indexOf('gia-vang') !== -1 || link === "/") {
        $('#gia-vang').addClass('show');
    } else if (link.indexOf('ty-gia') !== -1) {
        $('#ty-gia').addClass('show');
    } else if (link.indexOf('ngoai-te') !== -1) {
        $('#ngoai-te').addClass('show');
    } else if (link.indexOf('gia-xang-dau') !== -1) {
        $('#gia-xang-dau').addClass('show');
    } else if (link.indexOf('tien-ao') !== -1) {
        $('#tien-ao').addClass('show');
    } else if (link.indexOf('blog') !== -1) {
        $('#blog').addClass('show');
    } else if (link.indexOf('lai-suat') !== -1) {
        $('#lai-suat').addClass('show');
    }

    $("a[href='#top']").click(function () {
        $("html, body").animate({ scrollTop: 0 }, "slow");
        return false;
    });

    setInterval(time, 500); var dt = new Date().getTime() - new Date().getTime() - parseInt((parseInt(new Date().getTimezoneOffset()) - parseInt(-420)) * 60 * 1000); function time() { var ct = new Date(new Date().getTime() - dt); var m = ct.getMonth() + 1; if (m < 10) { m = '0' + m; } var d = ct.getDate(); if (d < 10) { d = '0' + d; } var h = ct.getHours(); if (h < 10) { h = '0' + h; } var s = ct.getSeconds(); if (s < 10) { s = '0' + s; } var mi = ct.getMinutes(); if (mi < 10) { mi = '0' + mi; } document.getElementById("currentTime").innerHTML = h + ':' + mi + ':' + s + ' | ' + d + '/' + m + '/' + ct.getFullYear(); }

    $(document).on('click', '.bnam-menu, .overlay', function () {
        if ($('#header_nav').hasClass('bmenu')) {
            $('#header_nav').removeClass('bmenu');
            $('.overlay').hide();
            $('.bnam-menu').removeClass('is-open').addClass('is-closed');
        } else {
            $('#header_nav').addClass('bmenu');
            $('.overlay').show();
            $('.bnam-menu').removeClass('is-closed').addClass('is-open');
        }
    });

    $(document).on('click', '.hamb-top', function () {
        $('.overlay').remove();
    });

    $(document).on('click', '.bn-drop', function (e) {
        var lii = $(this).closest('.dropdown');
        if (lii.hasClass('bn-show')) {
            lii.removeClass('bn-show');
        } else {
            $('.bn-show').removeClass('bn-show');
            lii.addClass('bn-show');
        }
        e.preventDefault();
    });

    function resize_images(maxht, maxwt, minht, minwt) {
        var imgs = document.getElementsByTagName('img');

        var resize_image = function (img, newht, newwt) {
            img.height = newht;
            img.width = newwt;
        };

        for (var i = 0; i < imgs.length; i++) {
            var img = imgs[i];
            if (img.height > maxht || img.width > maxwt) {
                // Use Ratios to constraint proportions.
                var old_ratio = img.height / img.width;
                var min_ratio = minht / minwt;
                // If it can scale perfectly.
                if (old_ratio === min_ratio) {
                    resize_image(img, minht, minwt);
                }
                else {
                    var newdim = [img.height, img.width];
                    newdim[0] = minht;  // Sort out the height first
                    // ratio = ht / wt => wt = ht / ratio.
                    newdim[1] = newdim[0] / old_ratio;
                    // Do we still have to sort out the width?
                    if (newdim[1] > maxwt) {
                        newdim[1] = minwt;
                        newdim[0] = newdim[1] * old_ratio;
                    }
                    resize_image(img, newdim[0], newdim[1]);
                }
            }
        }
    }

});
