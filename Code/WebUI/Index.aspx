<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Index" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="<%=Page.ResolveUrl("~/")%>Content/JS/easyUI/jquery.min.js" type="text/javascript"></script>
    <script src="<%=Page.ResolveUrl("~/")%>Content/JS/easyUI/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="<%=Page.ResolveUrl("~/")%>Content/JS/easyUI/locale/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <link href="<%=Page.ResolveUrl("~/")%>Content/JS/easyUI/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="<%=Page.ResolveUrl("~/")%>Content/JS/easyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <script src="<%=Page.ResolveUrl("~/")%>Content/JS/wikmenu.js" type="text/javascript"></script>
    <script src="<%=Page.ResolveUrl("~/")%>Content/JS/wikmain.js" type="text/javascript"></script>
    <link href="<%=Page.ResolveUrl("~/")%>Content/css/default.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        //获取左侧导航的图标
        function getIcon(menuid) {
            var icon = 'icon ';
            var json = '<%=GetMenuJson()%>';    
            $.each($.parseJSON(json), function (i, n) {
                $.each(n.menus, function (j, o) {
                    if (o.menuid == menuid) {
                        icon += o.icon;
                    }
                })
            })

            return icon;
        }
        //初始化左侧
        function InitLeftMenu() {
            $(".easyui-accordion1").empty();
            var menulist = "";
            var json = '<%=GetMenuJson()%>';    

            $.each($.parseJSON(json), function (i, n) {
                menulist += '<div title="' + n.menuname + '"  icon="' + n.icon + '" style="overflow:auto;">';
                menulist += '<ul>';
                if (n.menus instanceof Array) {
                    $.each(n.menus, function (j, o) {
                        menulist += '<li><div><a ref="' + o.menuid + '" href="#" rel="' + o.url + '" ><span class="icon ' + o.icon + '" >&nbsp;</span><span class="nav">' + o.menuname + '</span></a></div></li> ';
                    })
                    menulist += '</ul></div>';
                }
            })
              
            $(".easyui-accordion1").append(menulist);

            $('.easyui-accordion1 li a').click(function () {
                var tabTitle = $(this).children('.nav').text();

                var url = $(this).attr("rel");
                var menuid = $(this).attr("ref");
                var icon = getIcon(menuid, icon);

                addTab(tabTitle, url, icon);
                $('.easyui-accordion1 li div').removeClass("selected");
                $(this).parent().addClass("selected");
            }).hover(function () {
                $(this).parent().addClass("hover");
            }, function () {
                $(this).parent().removeClass("hover");
            });

            //导航菜单绑定初始化
            $(".easyui-accordion1").accordion();
        }



        $(function () {
            InitLeftMenu();
            $('#loginOut').click(function () {
                $.messager.confirm('系统提示', '您确定要退出本次登录吗?', function (r) {
                    if (r) {
                        location.href = 'loginout.ashx';
                    }
                });
            })
        });
    </script>
    <title>通航服务站</title>
</head>
<body class="easyui-layout" style="overflow-y: hidden" scroll="no">
    <input type="hidden" id="ipt_UserName" name="ipt_UserName" value="<%=UserName%>" />
    <div region="north" split="true" border="false" style="overflow: hidden; height: 30px;
        background: url(<%=Page.ResolveUrl("~/")%>Content/Img/layout-browser-hd-bg.gif) #7f99be repeat-x center 50%;
        line-height: 20px; color: #fff; font-family: Verdana, 微软雅黑,黑体">
        <span style="float: right; padding-right: 20px;" class="head">欢迎
            <%=UserName%>
    <a href="javascript:void()" id="loginOut" style="cursor: pointer; text-decoration: none;">
                    退出</a></span> <span style="padding-left: 10px; font-size: 16px;">
                        通航服务站</span>
    </div>
    <div region="south" split="true" style="height: 30px; background: #D2E0F2;">
        <div class="footer">
            &nbsp;&nbsp;&nbsp; 通航服务站</div>
    </div>
    <div region="west" split="true" title="导航菜单" style="width: 180px;" id="west">
        <div class="easyui-accordion1" fit="true" border="false">
            <!--  导航内容 -->
        </div>
    </div>
    <div id="mainPanel" region="center" style="background: #eee; overflow-y: hidden">
        <div id="tabs" class="easyui-tabs" fit="true" border="false">
            <div title="欢迎使用" style="padding: 20px; overflow: hidden;" id="home">
                <h1>
                   通航服务站</h1>
            </div>
        </div>
    </div>

    
</body>
</html>
