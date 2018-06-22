<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BackLog.aspx.cs" Inherits="BackLog" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="css/buttons.css" rel="stylesheet" />
    <script src="js/jquery-1.10.2.min.js"></script>
    <style>
        h3 {
            margin-left:20px;
        }
        a {
            font-size:16px;
            font-weight:bolder;
            width:240px;
            height:180px;
            margin-top:10px;
            margin-left:20px;
            vertical-align:middle;
            line-height:180px;
        }
        /*body {
            overflow-y:hidden;
        }*/
    </style>

    <script>
        $(function () {
            var json = '<%=GetUserDataJson()%>';
            var html = '';
            $.each($.parseJSON(json), function (i, n) {
                switch (n.MenuName) {
                    case "待提交长期计划":
                        html += '<a class="button button-glow button-border button-rounded button-primary" data-menucode="' + n.MenuUrl + '"><span></span>' + n.MenuPlanCount + '条待提交的长期计划</a>';
                        break;
                    case "待审核长期计划":
                        html += '<a class="button button-glow button-border button-rounded button-caution" data-menucode="' + n.MenuUrl + '"><span></span>' + n.MenuPlanCount + '条待审核的长期计划</a>';
                        break;
                    case "待提交飞行计划":
                        html += '<a class="button button-glow button-border button-rounded button-highlight" data-menucode="' + n.MenuUrl + '"><span></span>' + n.MenuPlanCount + '条待提交的飞行计划</a>';
                        break;
                    case "待审核飞行计划":
                        html += '<a class="button button-glow button-border button-rounded button-royal" data-menucode="' + n.MenuUrl + '"><span></span>' + n.MenuPlanCount + '条待审核的飞行计划</a>';
                        break;
                    case "待提交当日起飞申请":
                        html += '<a class="button button-glow button-border button-rounded button-inverse" data-menucode="' + n.MenuUrl + '"><span></span>' + n.MenuPlanCount + '条待提交的当日动态</a>';
                        break;
                    case "待审核当日起飞申请":
                        html += '<a class="button button-glow button-border button-rounded button-action" data-menucode="' + n.MenuUrl + '"><span></span>' + n.MenuPlanCount + '条待审核的当日动态</a>';
                        break;
                }
            });
            $('#backlog').html(html);
            ReSize();
            $(window).resize(function () {
                ReSize();
            });
            $("a").click(function () {
                var code = $(this).attr("data-menucode");
                $(".easyui-accordion1 li a", window.top.document).each(function (i) {
                    var rel = $(this).attr("rel");
                    console.log(rel);
                    if (rel.indexOf(code) > -1) {
                        $(this)[0].click();
                        return;
                    }
                });
            });
        });

        function ReSize() {
            var count = 0;
            $("a").each(function () {
                if ($(this).css("display") != "none")
                    count++;
            });
            var setWidth = (window.innerWidth - 30 * count) / count;
            var setHeight = window.innerHeight - 100;
            if (setWidth < 240) {
                setWidth = 240;
                setHeight = (window.innerHeight - 100) / 2
            }
            $("a").each(function () {
                $(this).css("width", setWidth);
                $(this).css("height", setHeight);
                $(this).css("line-height", setHeight + "px");
            });
        }
    </script>
</head>
<body>
    <h3>待办事项</h3>
    <div id="backlog">
        
    </div>
</body>
</html>
