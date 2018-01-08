<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="js/index_html.js"></script>
    <script type="text/javascript" src="js/breakingnews.js"></script>
    <script type="text/javascript">
        if (window != top) {
            top.location.href = location.href;
        }
        document.onkeydown = function (e) {
            var event = e || window.event;
            var code = event.keyCode || event.which || event.charCode;
            if (code == 13) {
                Main.login();
            }
        }
        $(function () {
            Main.clearData();
            //$("input[name='txtUserName']").focus();
            $("#txtUserName", "#txtPassword").keypress(function () { Main.hideErr(); });

        });
        Main = {
            login: function () {
                if ($("input[name='txtUserName']").val().trim() == "" || $("input[name='txtPassword']").val().trim() == ""
                    || $("input[name='txtCode']").val().trim() == "") {
                    $("#showMsg").html("用户名、密码、验证码不能为空！");
                    $("input[name='txtUserName']").focus();

                } else {
                    var userName = $("input[name='txtUserName']");
                    var password = $("input[name='txtPassword']");
                    var str = encMe(password.val().trim(), userName.val().trim());
                    $("input[name='htxtPassword']").val(str);
                    $("#showMsg").html("登录中...");
                    $("#btn_login").attr("disabled", "disabled");
                    $.ajax({
                        type: "POST",
                        url: "Default.aspx",
                        data: $("#loginForm").serialize(),
                        error: function (request) {
                            $("#btn_login").removeAttr("disabled");
                            $("#showMsg").html(request);
                        },
                        success: function (data) {
                            if (data.isSuccess) {
                                document.location = "index.aspx";
                            }
                            else {
                                $("#showMsg").html(data.msg);
                                $("#btn_login").removeAttr("disabled");
                                $("#LoginFrame1_IMG_Identify").attr('src', 'VerifyCode.aspx?rnd=' + Math.random());
                            }
                        }
                    });
                }
            },
            clearData: function () {
                $("input[name='txtUserName']").val('');
                $("input[name='txtPassword']").val('');
                $("input[name='htxtPassword']").val('');
                $("input[name='txtCode']").val('');                
            },
            hideErr: function () {
                $("#showMsg").html('');
            }
        };
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#scrollDiv").Scroll({ line: 1, speed: 500, timer: 5000, up: "but_up", down: "but_down" });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--主体begin-->
    <div class="main">
        <div class="center">
            <div class="adBox">
                <ul class="adpic">
                    <%foreach (var item in picModel)
                        {%>

                    <li>
                        <img src="<%=Page.ResolveUrl("~/")+item%>" alt="" /></li>
                    <%  }%>
                </ul>
                <ul class="hd">
                    <%
                        for (int z = 0; z < picModel.Count; z++)
                        {
                            if (z == 0)
                            { %>
                    <li class=" on"></li>
                    <%    }
                        else
                        { %>
                    <li class=" "></li>
                    <%   }
                        }%>
                </ul>
            </div>
            <script type="text/javascript">
                jQuery(".adBox").slide({ mainCell: ".adpic", effect: "fold", autoPlay: true, delayTime: 800, interTime: 5000 });
                </script>

            <div class="center_left_container">
                <div class="center_left">
                    <div class="content" style="margin: 0 auto">
                        <div class="title" style="background: #fff">
                            <h2><a href="List.aspx?Type=News&PageIndex=1" target="_blank">新闻中心</a></h2>
                            <span><a href="List.aspx?Type=News&PageIndex=1" target="_blank">+更多</a></span>
                        </div>
                        <div class="img_container">
                            <img width="288px" height="288px" src="images/News.jpg" alt="" />
                        </div>
                        <div class="txt_container">
                            <div class="scrollbox">
                                <div id="scrollDiv">
                                    <ul>
                                        <%
                                            foreach (var item in newsModel)
                                            {%>
                                        <li>
                                            <h3><a href="Detail.aspx?Type=News&Id=<%=item.NewID %>" class="linktit"><%=HtmlWorkShop.CutTitle(item.NewTitle,20)%></a></h3>
                                            <div><%=HtmlWorkShop.CutTitle(HttpUtility.UrlDecode(item.NewContent),30)%></div>
                                        </li>
                                        <%  }%>
                                    </ul>
                                </div>
                                <div class="scroltit">
                                    <div class="updown" id="but_down">向上</div>
                                    <div class="updown" id="but_up">向下</div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="center_left_new">
                    <div class="content" style="margin: 0 auto">
                        <div class="title" style="background: #fff">
                            <h2><a href="List.aspx?Type=SupplyDemand&PageIndex=1" target="_blank">供求信息</a></h2>
                            <span><a href="List.aspx?Type=SupplyDemand&PageIndex=1" target="_blank">+更多</a></span>
                        </div>
                        <div class="img_container">
                            <img width="288px" height="288px" src="images/Supply.jpg" alt="" />
                        </div>
                        <div class="txt_container">
                            <div class="sideMenu">
                                <%
                                    var i = 1;
                                    foreach (var item in demandModel)
                                    {%>
                                <h3 class="sideMenu00 <%=(i==1)?"on":""%>">
                                    <a href="Detail.aspx?Type=SupplyDemand&Id=<%=item.ID %>" target="_blank"><%=HtmlWorkShop.CutTitle(item.Title,20)%></a>
                                </h3>

                                <ul>
                                    <p><a href="Detail.aspx?Type=SupplyDemand&Id=<%=item.ID %>" target="_blank"><%=HtmlWorkShop.CutTitle(item.Summary,30)%></a></p>
                                </ul>
                                <%   i++;
                                    }%>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="center_left_new">
                    <div class="content" style="margin: 0 auto">
                        <div class="title" style="background: #fff">
                            <h2><a href="List.aspx?Type=CompanyIntro&PageIndex=1" target="_blank">通航企业</a></h2>
                            <span><a href="List.aspx?Type=CompanyIntro&PageIndex=1" target="_blank">+更多</a></span>
                        </div>
                        <div class="img_container">
                            <img width="288px" height="288px" src="images/Company.jpg" alt="" />
                        </div>
                        <div class="txt_container">
                            <div class="sideMenu">
                                <%
                                    var j = 1;
                                    foreach (var item in companySummaryModel)
                                    {%>
                                <h3 class="sideMenu00 <%=(j==1)?"on":""%>">
                                    <a href="Detail.aspx?Type=CompanyIntro&Id=<%=item.ID %>" target="_blank"><%=HtmlWorkShop.CutTitle(item.Title,20)%></a>
                                </h3>

                                <ul>
                                    <p><a href="Detail.aspx?Type=CompanyIntro&Id=<%=item.ID %>" target="_blank"><%=HtmlWorkShop.CutTitle(item.Summary,30)%></a></p>
                                </ul>
                                <%   j++;
                                    }%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="center_right_container">
                <div class="center_right_top">
                    <div class="title_new">
                        <h2>用户登录</h2>
                    </div>
                    <div class="login_box">
                        <form id="loginForm" name="loginForm" method="post">
                            <ul>
                                <li>
                                    <input name="txtUserName" id="txtUserName" class="login_name ipt" placeholder="请输入用户名" maxlength="40" type="text" /></li>
                                <li>
                                    <input name="txtPassword" id="txtPassword" class="login_pwd ipt" placeholder="请输入密码" type="password" />
                                    <input type="hidden" name="action" value="submit" />
                                    <input type="hidden" name="htxtPassword" />
                                </li>
                                <li style="text-align: left; padding: 0 30px 0 23px; width: 280px;">
                                    <span style="float: right">
                                        <img id="LoginFrame1_IMG_Identify" title="看不清点击刷新" onclick="javascript:this.src='/VerifyCode.aspx?rnd='+Math.random()" src="/VerifyCode.aspx?rnd=<%=rnd %>" style="border-width: 0px;" align="middle" alt="" />
                                    </span>
                                    <input name="txtCode" id="txtCode" class="login_yzm ipt" placeholder="验证码" type="text" /></li>
                                <li id="showMsg" style="margin: 0px; color: red;">&nbsp;</li>
                                <li style="margin-top: 0px;">
                                    <input name="btn_login" value="登录" onclick="Main.login()" id="btn_login" class="login_btn" type="button" /></li>
                            </ul>
                        </form>
                    </div>
                </div>
                <div class="center_right_middle">
                    <div class="title">
                        <h2><a href="#" target="_blank">相关资料</a></h2>
                    </div>
                    <div class="tuijie" style="margin: 0 auto">
                        <ul>
                            <%
                                foreach (var item in resModel)
                                {%>
                            <li>
                                <a href="Handler.ashx?action=download&filepath=<%=item.FilePath %>" target="_blank" class="linktit"><%=HtmlWorkShop.CutTitle(item.Title,20)%></a>
                            </li>
                            <%  }%>
                        </ul>
                    </div>
                </div>
                <div class="center_right_bottom">
                    <div class="title">
                        <h2><a>友情链接</a></h2>
                    </div>
                    <div class="tuijie" style="margin: 0 auto">
                        <ul>
                            <li><a href="http://183.63.187.42/ABCenter/Default.aspx" target="_blank" title="广东省古籍保护中心">广东省古籍保护中心</a></li>
                            <li><a href="http://www.lsgd.org.cn/" target="_blank" title="广东图书馆学会">广东图书馆学会</a></li>
                            <li><a href="http://tsglt.zslib.com.cn/" target="_blank" title="《图书馆论坛》">《图书馆论坛》</a></li>
                            <li><a href="http://ldtsg.zslib.com.cn/" target="_blank" title="广东流动图书馆">广东流动图书馆</a></li>
                            <li><a href="http://govinfo.zslib.com.cn/" target="_blank" title="中国政府公开信息整合服务平台">中国政府公开信息整合服务平台</a></li>
                            <li><a href="http://bmzx.zslib.com.cn/" target="_blank" title="广东省文献编目中心">广东省文献编目中心</a></li>
                            <li><a href="http://www.zslib.com.cn/jingtaiyemian/zwgk/index.html" target="_blank" title="馆务公开">馆务公开</a></li>
                            <li><a href="http://www.zslib.com.cn/TempletPage/GQTBList.aspx" target="_blank" title="《馆情通报》">《馆情通报》</a></li>
                            <li><a href="http://jxjy.gdlink.net/" target="_blank" title="广东省图书情报继续教育网络学习中心">广东省图书情报继续教育网络学习中心</a></li>
                            <li><a href="http://www.gddcn.gov.cn/" target="_blank" title="广东数字文化网">广东数字文化网</a></li>
                        </ul>
                    </div>
                </div>
            </div>
            <div class="kjdh clearfix">
            </div>
        </div>
    </div>
    <!--主体end-->
</asp:Content>
