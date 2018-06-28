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
    <link href="<%=Page.ResolveUrl("~/")%>css/indexstatics.css" rel="stylesheet" type="text/css" />
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
        $(document).on("click", ".panelIndex", function () {
            var url = $(this).attr("data_val");
            $(".easyui-accordion1 li a").each(function (i) {
                var rel = $(this).attr("rel");
                console.log(rel);
                if (rel.indexOf(url) > -1)
                {
                    $(this).click();
                    return;
                }
            })
            //  $('.easyui-accordion1 li a')
        })
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

        <%--function GetUserData() {
            var json = '<%=GetUserDataJson()%>';
            var html = "";
            $.each($.parseJSON(json), function (i,n) {
                html += '<div class="col-lg-3 col-md-6" style="width:20%;float:left;">\
                <div class="bpanel panel-primary">\
                    <div class="panel-heading">\
                        <div class="row">\
                            <div class="huge" style="display: inline;">' + n.MenuPlanCount + '</div>\
                            <div class="text-right" style="table-cell; float: right; margin-right: 20px;margin-top:-4px;">\
                             <img src="Content/Img/' + n.MenuImgUrl + '" style="width: 50px; height: 40px;display:none" />\
                            </div>\
                        </div>\
                    </div>\
                    <a href="#" class="panelIndex" data_val=' + n.MenuUrl + '>\
                        <div class="panel-footer">\
                            <div class="pull-left" style="font-size: 20px; display: inline;">' + n.MenuName + '</div>\
                            <div class="pull-right" style="display: inline;">\
                                <i class="fa fa-arrow-circle-right">\
                                    <img src="Content/Img/rightarrow.png" style="width: 20px; height: 20px; margin-top: 10px;" /></i>\
                            </div>\
                            <div class="clearfix"></div>\
                        </div>\
                    </a>\
                </div>\
                </div>';
                //json.MenuName
                //json.MenuUrl
                //json.MenuPlanCount
                //json.MenuImgUrl
            });
            $(".row").html(html);
        }--%>

        $(function () {
            InitLeftMenu();
            //GetUserData();
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

    <div region="north" split="true" border="false" style="overflow: hidden; height: 60px; background: url(<%=Page.ResolveUrl("~/")%>images/icon06.png) repeat-x; line-height: 60px; color: #fff; font-family: Verdana, 微软雅黑,黑体">
        <span style="float: right; padding-right: 20px;" class="head">
            <%--         <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-tip" plain="true" onclick="Main.Export()">消息</a>--%>
            欢迎
            <%=UserName%>&nbsp;&nbsp;
            <a href="/Default.aspx" style="cursor: pointer; text-decoration: none;">主页</a>&nbsp;&nbsp;
            <a href="javascript:void()" id="loginOut" style="cursor: pointer; text-decoration: none;">退出</a>
        </span>
        <img style="padding-left:50px;height:60px;vertical-align:middle;" src="Content/Img/index_logo.png" />
    </div>
    <div region="south" split="true" style="height: 30px; background: #D2E0F2;">
        <div class="footer">
            &nbsp;&nbsp;&nbsp; 通航服务站
        </div>
    </div>
    <div region="west" split="true" title="导航菜单" style="width: 180px;" id="west">
        <div class="easyui-accordion1" fit="true" border="false">
            <!--  导航内容 -->
        </div>
    </div>
    <div id="mainPanel" region="center" style="background: #eee; overflow-y: hidden">
        <div id="tabs" class="easyui-tabs" fit="true" border="false">
            <div title="欢迎使用" style="padding: 20px; overflow: hidden;" id="home">
                <%-- <h1>通航服务站</h1>--%>
                <div style="width:100%;text-align:center;float:left">
                    <iframe id="generalize" class="NFine_iframe" style="width:49%;float:left;" src="BackLog.aspx" frameborder="0" data-menucode="MyUnSubmitRepetPlanCheck"></iframe>
                    <iframe id="generalize-chart" class="NFine_iframe" style="width:49%;margin-left:20px;margin-top:20px;float:right;" src="/Charts/Generalize.aspx" frameborder="0"></iframe>
                </div>
                <div style="width:100%;">
                    <iframe id="flynum-chart" class="NFine_iframe" style="width:49%;margin-left:20px;margin-top:20px;float:left;" src="/Charts/FlyNum.aspx" frameborder="0"></iframe>
                    <iframe id="flytime-chart" class="NFine_iframe" style="width:49%;margin-top:20px;float:right;" src="/Charts/FlyTime.aspx" frameborder="0"></iframe>
                </div>
            </div>
        </div>

    </div>
    <script type="text/javascript">
        $(function () {
            ReSize();
            $(window).resize(function () {
                ReSize();
            });
        });

        function ReSize() {
            $("#generalize").css("height", (window.innerHeight - 150) / 2);
            $("#generalize-chart").css("height", (window.innerHeight - 150) / 2);
            $("#flynum-chart").css("height", (window.innerHeight - 150) / 2);
            $("#flytime-chart").css("height", (window.innerHeight - 150) / 2);
        }
    </script>
</body>
</html>
