(function ($) {
    $.fn.dwseeTopBottomMenu = function (options) {
        var settings = $.extend({
            'menucontainer': '.TopBottomMenu',
            'boxsize': 40,
            'boxbackground': '#ffffff',
            'position': 'right',
            'openmenusize': 500,
            'topicon': 'images/up_arrow.png',
            'menuicon': 'images/menu_icon.png',
            'bottomicon': 'images/down_arrow.png'
        }, options);    
        $('body').prepend('<div id="top-bottom-menu"><div id="dstbm-top"></div><div id="dstbm-menu"> <div id="dstbmmenu"></div></div><div id="dstbm-bottom"></div></div>');
        $(settings.menucontainer).clone().appendTo('#dstbmmenu');
        $(settings.menucontainer).hide();
        $("#dstbmmenu > " + settings.menucontainer).css("display", "block");
        $("#top-bottom-menu").css("height", settings.boxsize + "px").css("bottom", (settings.boxsize * 2) + 10 + "px").css("line-height", settings.boxsize + "px").css(settings.position, "10px");
        $("#top-bottom-menu #dstbm-top").css("height", settings.boxsize + "px").css("width", settings.boxsize + "px").css(settings.position, "0").css("background", "url('" + settings.topicon + "') center center no-repeat").css("background-color", settings.boxbackground);
        $("#top-bottom-menu #dstbm-bottom").css("height", settings.boxsize + "px").css("width", settings.boxsize + "px").css(settings.position, "0").css("top", (settings.boxsize * 2) + 2 + "px").css("background", "url('" + settings.bottomicon + "') center center no-repeat").css("background-color", settings.boxbackground);
        $("#top-bottom-menu #dstbm-menu").css("height", settings.boxsize + "px").css("width", settings.boxsize + "px").css(settings.position, "0").css("top", (settings.boxsize + 1) + "px").css("background", "url('" + settings.menuicon + "') center center no-repeat").css("background-color", settings.boxbackground);
        $(window).scroll(function () {
            var pos = $(window).height() / 2;
            if ($(window).scrollTop() > 200) {
                $("#dstbm-top").fadeIn("slow")
            } else {
                $("#dstbm-top").fadeOut("slow")
            }
            if (($(window).height() + $(window).scrollTop()) >= $(document).height()) {
                $("#dstbm-bottom").fadeOut("slow")
            } else {
                $("#dstbm-bottom").fadeIn("slow")
            }
        });
        $("#dstbm-top").click(function () {
            $('html, body').animate({
                scrollTop: '0'
            })
        });
        $("#dstbm-bottom").click(function () {
            $('html, body').animate({
                scrollTop: $("body").height()
            })
        });
        $("#dstbm-menu").click(function () {
            if ($(this).width() == settings.boxsize) {
                $("#dstbm-menu #dstbmmenu").css("display", "block");
                $("#dstbm-menu").css("background", settings.boxbackground + " url('') center center no-repeat").css("cursor", "default");
                $(this).animate({
                    width: settings.openmenusize + "px"
                })
            }
        });
        $("#dstbm-menu").hover(function () { }, function () {
            if ($(this).width() == settings.openmenusize) {
                $("#dstbm-menu #dstbmmenu").css("display", "block");
                $("#dstbm-menu").css("background", settings.boxbackground + " url('') center center no-repeat").css("cursor", "pointer");
                $(this).animate({
                    width: settings.boxsize + "px"
                }, function () {
                    $("#dstbm-menu #dstbmmenu").css("display", "none");
                    $("#dstbm-menu").css("background", settings.boxbackground + " url('" + settings.menuicon + "') center center no-repeat")
                })
            }
        })
    }
})(jQuery);