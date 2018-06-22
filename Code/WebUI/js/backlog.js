$(function () {
    $("a").each(function () {
        var code = $(this).attr("data-menucode");
        var result;
        if (code != undefined) {
            // 获取权限
            $.ajax({
                url: "../Ajax/Generalize/JudgeMenuRole",
                type: "post",
                data: { "menuCode": code },
                dataType: "json",
                async: false,
                error: function (xml, msg) {
                    alert(msg);
                },
                success: function (data) {
                    result = data;
                }
            });
            if (result == false) {
                $(this).css("display", "none");
            }
            else {
                // 获取待办数量
                var num;
                $.ajax({
                    url: "/Generalize/GetGeneralizeNum",
                    type: "post",
                    data: { "menuCode": code },
                    dataType: "json",
                    async: false,
                    error: function (xml, msg) {
                        alert(msg);
                    },
                    success: function (data) {
                        num = data;
                    }
                });
                $(this).children(0).html(num);
            }
        }
    });
});