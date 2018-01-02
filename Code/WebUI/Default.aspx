<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta name="copyright" content="广州市中南民航空管通信网络科技有限公司" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>河南通航飞行服务站</title>
    <link href="css/base.css?159" rel="stylesheet" type="text/css" />
    <link href="css/index.css?159" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="js/jquery1.42.min.js"></script>
    <script type="text/javascript" src="js/jquery.pack.js"></script>
    <script type="text/javascript" src="js/jquery.SuperSlide.2.1.1.js"></script>
    <script type="text/javascript" src="js/index_html.js"></script>
    <script type="text/javascript" src="js/jq_scroll.js" ></script>
    <script type="text/javascript" src="js/breakingnews.js"></script>
    <script type="text/javascript" src="js/common.js?0410"></script>
    <script src="<%=Page.ResolveUrl("~/Content/JS/GA/base.js")%>" type="text/javascript"></script>      
    <script src="<%=Page.ResolveUrl("~/Content/JS/Des.js")%>" type="text/javascript"></script>
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
            $("input[name='txtUserName']").focus();
            $("#txtUserName", "#txtPassword").keypress(function () { Main.hideErr(); });

        });
        Main = {
            login: function () {
                if ($("input[name='txtUserName']").val().trim() == "" || $("input[name='txtPassword']").val().trim() == "" || $("input[name='txtCode']").val().trim() == "") {
                    $("#showMsg").html("用户名、密码、验证码不能为空！");
                    //  $("input[name='txtUserName']").focus();

                } else {
                    var userName = $("input[name='txtUserName']");
                    var password = $("input[name='txtPassword']");
                    var str = encMe(password.val().trim(), userName.val().trim());
                    $("input[name='htxtPassword']").val(str);
                    $("#showMsg").html("登录中...");
                    $("#btn_login").attr("disabled", "disabled");
                    $.ajax({
                        type: "POST",
                        url:"Login.aspx",
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
                            }

                        }
                    });
                }
            },
            clearData: function () {
                $("input[name='txtUserName']").val('');
                $("input[name='txtPassword']").val('');
                $("input[name='htxtPassword']").val('');
            },
            hideErr: function () {
                $("#showMsg").html('');
            }
        };
    </script>
    <script type="text/javascript">
        if ($.browser.msie && ($.browser.version == "6.0") && !$.support.style) {
            alert("温馨提示:您当前使用的浏览器为ie6内核,可能会影响您浏览此网站,建议使用ie7以上版本浏览器!");
        }
        $(document).ready(function () {
            $("#scrollDiv").Scroll({ line: 1, speed: 500, timer: 5000, up: "but_up", down: "but_down" });
        });
    </script>
</head>
<body>

        <!--head begin-->
        <div class="head">
            <div class="center" style="position: relative">
                <div class="logo"><img src="images/logo.png" alt=""/></div>
                <div class="searchboxdiv">
                    <input type="text" name="search" id="txt_request" class="searchbox ipt" value="请输入搜索关键词" onblur="setkeyword('txt_request')"
                        onkeydown="if(event.keyCode==13){document.getElementById('ibtn_txt_request').focus();}" />
                    <span class="btn_search">
                        <button id="ibtn_txt_request" type="submit" class="btn" onclick="subjs('gc');return false">搜&nbsp;索</button>
                    </span>
                </div>
                <div class="nav-box">
                    <ul>
                        <li class="cur"><a href="/index.aspx" target="_blank">首页<span>Home</span></a></li>
                        <li><a href="/TempletPage/Page.aspx?column=bz" target="_blank">新闻<span>News</span></a></li>
                        <li><a href="/TempletPage/Page.aspx?column=ts" target="_blank">供求<span>Supply-Demand</span></a></li>
                        <li><a href="/TempletPage/Page.aspx?column=qk" target="_blank">企业<span>Company</span></a></li>
                        <li><a href="/TempletPage/Page.aspx?column=qk" target="_blank">资料<span>File</span></a></li>
                        <li><a href="/TempletPage/Page.aspx?column=tc" target="_blank">计划<span>Plan</span></a></li>
                        <li><a href="/TempletPage/Page.aspx?column=tc" target="_blank">气象<span>Weather</span></a></li>
                        <li><a href="/TempletPage/Page.aspx?column=ww" target="_blank">情报<span>Information</span></a></li>
                        <li><a href="/TempletPage/Page.aspx?column=st" target="_blank">监视<span>Surveillance</span></a></li>
                    </ul>
                    <div class="nav-line" style="left: 0px; width: 128px; display: block;"></div>
                </div>
            </div>
        </div>
        <!--head end-->

        <!--主体begin-->
        <div class="main">
            <div class="center">
                 <div class="adBox">
                    <ul class="adpic">
                        <li><img src="images/rollimages/rollimg1.jpg" alt=""/></li>
                        <li><img src="images/rollimages/rollimg2.jpg" alt=""/></li>
                        <li><img src="images/rollimages/rollimg3.jpg" alt=""/></li>
                        <li><img src="images/rollimages/rollimg4.jpg" alt=""/></li>
                        <li><img src="images/rollimages/rollimg5.jpg" alt=""/></li>
                        <li><img src="images/rollimages/rollimg6.jpg" alt=""/></li>                        
                    </ul>
                    <ul class="hd">
                        <li class=" on"></li>
                        <li class=" "></li>
                        <li class=" "></li>
                        <li class=" "></li>
                        <li class="  "></li>
                        <li class=" "></li>
                    </ul>
                </div>
                <script type="text/javascript">
                    jQuery(".adBox").slide({ mainCell: ".adpic", effect: "fold", autoPlay: true, delayTime: 800, interTime: 5000 });
                </script>

                <div class="center_left_container">
                    <div class="center_left">
                        <div class="content" style="margin: 0 auto">
                            <div class="title" style="background: #fff">
                                <h2><a href="http://www.zslib.com.cn/TempletPage/userService.aspx?dbid=5" target="_blank">新闻中心</a></h2>
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
                                                foreach(var item in newsModel)
	                                        {%>
		                                    <li>
                                                <h3><a href="Detail.aspx?Type=News&Id=<%=item.NewID %>" class="linktit"><%=HtmlWorkShop.CutTitle(item.NewTitle,20)%></a></h3>
                                                <div><%=HtmlWorkShop.CutTitle(HttpUtility.UrlDecode(item.NewContent),20)%></div>
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
                                <h2><a href="#" target="_blank">供求信息</a></h2>
                                <span><a href="List.aspx?Type=SupplyDemand&PageIndex=1" target="_blank">+更多</a></span>
                            </div>
                            <%--<div class="img_container">
                                <img width="288px" height="288px" src="images/Supply.jpg" alt="" />
                            </div>
                            <div class="txt_container">
                                <div class="sideMenu">
                                       <%
                                           var i = 1;
                                           foreach (var item in demandModel)
	                                        {%>
                                   <h3 class="sideMenu00 <%=(i==1)?"on":""%>">
                                   
                                     <ul style="<%=(i==1)?"display: block;":"display: none;"%>">
                                        <p><a href="Detail.aspx?Type=News&Id=<%=item.ID %>" target="_blank"><%=HtmlWorkShop.CutTitle(item.Summary,20)%></a></p>
                                    </ul>
                                       <%  
                                                i++;
                                            }%>
              
                                                          
                                </div>
                            </div>--%>
                            <div class="img_container">
                                <img width="288px" height="288px" src="images/Supply.jpg" alt="" />
                            </div>
                            <div class="txt_container">
                                <div class="scrollbox">
                                    <div id="scrollDiv">
                                        <ul>
                                            <%
                                                foreach (var item in demandModel)
	                                        {%>
		                                    <li>
                                                <h3><a href="Detail.aspx?Type=SupplyDemand&Id=<%=item.ID %>" class="linktit"><%=HtmlWorkShop.CutTitle(item.Title,20)%></a></h3>
                                                <div><%=HtmlWorkShop.CutTitle(HttpUtility.UrlDecode(item.Summary),20)%></div>
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
                                <h2><a href="#" target="_blank">通航企业</a></h2>
                                <span><a href="List.aspx?Type=CompanyIntro&PageIndex=1" target="_blank">+更多</a></span>
                            </div>
                            <div class="img_container">
                                <img width="288px" height="288px" src="images/Company.jpg" alt="" />
                            </div>
                           <%-- <div class="txt_container">
                                <div class="sideMenu">
                                   <%
                                           var j = 1;
                                           foreach (var item in companyModel)
	                                        {%>
                                   
                                     <ul style="<%=(j==1)?"display: block;":"display: none;"%>">
                                        <p><a href="#" target="_blank"><%=HtmlWorkShop.CutTitle(item.Summary,20)%></a></p>
                                    </ul>
                                       <%  
                                                i++;
                                            }%>              
                                </div>
                            </div>--%>
                            <div class="txt_container">
                                <div class="scrollbox">
                                    <div id="scrollDiv">
                                        <ul>
                                            <%
                                                foreach (var item in companySummaryModel)
	                                        {%>
		                                    <li>
                                                <h3><a href="Detail.aspx?Type=CompanyIntro&Id=<%=item.ID %>" class="linktit"><%=HtmlWorkShop.CutTitle(item.Title,20)%></a></h3>
                                                <div><%=HtmlWorkShop.CutTitle(HttpUtility.UrlDecode(item.SummaryCode),20)%></div>
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
                </div>

                <div class="center_right_container">
                    <div class="center_right_top">
                        <div class="title_new">
                            <h2>用户登录</h2>
                        </div>
                               <form id="loginForm" method="post">
                        <div class="login_box">
                            <ul>
                                <li>
                                    <input name="txtUserName" id="txtUserName" class="login_name ipt" placeholder="请输入用户名" maxlength="40" style="color: rgb(173, 173, 173);" type="text" /></li>
                                <li>
                                    <input name="txtPassword" id="txtPassword" class="login_pwd ipt" placeholder="请输入密码"  style="color: rgb(173, 173, 173);" type="password" />
                                           <input type="hidden" name="action" value="submit" />
                                        <input type="hidden" name="htxtPassword" />
                                </li>
                                <li style="text-align: left; padding: 0 30px 0 23px; width: 280px;">
                                    <span style="float: right">
                                        <img id="LoginFrame1_IMG_Identify" title="看不清点击刷新" onclick="javascript:this.src='/VerifyCode.aspx?rnd='+Math.random()" src="/VerifyCode.aspx?rnd=<%=rnd %>" style="border-width: 0px;" align="middle" alt="" />
                                    </span>
                                    <input name="txtCode" id="txtCode" class="login_yzm ipt" placeholder="验证码"  style="color: rgb(173, 173, 173);" type="text" /></li>
                                <li id="showMsg" style="margin:0px;color:red;">&nbsp;</li>
                                <li style="margin-top:0px;">
                                    <input name="btn_login" value="登录" onclick="Main.login()" id="btn_login" class="login_btn" type="button" /></li>
                            </ul>
                        </div>
                                   </form>
                    </div>
                    <div class="center_right_middle">
                        <div class="title">
                            <h2><a href="http://www.zslib.com.cn/TempletPage/List.aspx?dbid=1" target="_blank">相关资料</a></h2>
                        </div>
                        <div class="tuijie" style="margin: 0 auto">
                           <%-- <ul>
                                <li><a href="http://183.63.187.42/ABCenter/Default.aspx" target="_blank" title="广东省古籍保护中心">广东省古籍保护中心广</a></li>
                                <li><a href="http://www.lsgd.org.cn/" target="_blank" title="广东图书馆学会">广东图书馆学会</a></li>
                                <li><a href="http://tsglt.zslib.com.cn/" target="_blank" title="《图书馆论坛》">《图书馆论坛》</a></li>
                                <li><a href="http://ldtsg.zslib.com.cn/" target="_blank" title="广东流动图书馆">广东流动图书馆</a></li>
                                <li><a href="http://govinfo.zslib.com.cn/" target="_blank" title="中国政府公开信息整合服务平台">中国政府公开信息整合服务平台</a></li>
                                <li><a href="http://bmzx.zslib.com.cn/" target="_blank" title="广东省文献编目中心">广东省文献编目中心</a></li>
                                <li><a href="http://www.zslib.com.cn/jingtaiyemian/zwgk/index.html" target="_blank" title="馆务公开">馆务公开</a></li>
                                <li><a href="http://www.zslib.com.cn/TempletPage/GQTBList.aspx" target="_blank" title="《馆情通报》">《馆情通报》</a></li>
                                <li><a href="http://jxjy.gdlink.net/" target="_blank" title="广东省图书情报继续教育网络学习中心">广东省图书情报继续教育网络学习中心</a></li>
                                <li><a href="http://www.gddcn.gov.cn/" target="_blank" title="广东数字文化网">广东数字文化网</a></li>                                
                            </ul>--%>
                            <ul>
                                    <%
                                        foreach (var item in resModel)
	                                {%>
		                            <li>
                                        <h3><a href="Handler.ashx?action=download&filepath=<%=item.FilePath %>" class="linktit"><%=HtmlWorkShop.CutTitle(item.Title,20)%></a></h3>
                                    </li>
	                                    <%  }%>                  
                                </ul>
                        </div>
                        <!-- list  End -->
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
                        <!-- list  End -->
                    </div>
                </div>                
                <div class="kjdh clearfix">                    
                </div>               
            </div>
        </div>        
        <!--主体end-->

        <!--foot begin-->
        <%--<div class="foot">
            <div class="foot_center">
                <ul>
                    <li style="width:400px;">
                        <div class="link">
                            <h4>相关链接</h4>
                            <ul>
                                <li><a href="http://www.nlc.cn/" target="_blank" title="国家图书馆">国家图书馆</a></li>
                                <li><a href="http://www.library.sh.cn/" target="_blank" title="上海图书馆">上海图书馆</a></li>
                                <li><a href="http://www.ndcnc.gov.cn/" target="_blank" title="国家数字文化网">国家数字文化网</a></li>
                                <li><a href="http://www.gdlink.net.cn/" target="_blank" title="广东省文献资源共建共享协作网">广东省文献资源共建共享协作网</a></li>
                                <li><a href="http://dlib.gdlink.net.cn/" target="_blank"  title="珠江三角洲数字图书馆联盟">珠江三角洲数字图书馆联盟</a></li>
                                <li><a href="http://www.szlib.org.cn/zgatecgi/zstart" target="_blank"  title="粤港澳图书馆书目检索">粤港澳图书馆书目检索</a></li>

                            </ul>
                        </div>
                    </li>
                    <li style="width:400px;">
                        <div class="link">
                            <h4>联系我们</h4>
                            <ul>
                                <p>
                                    详细地址：河南省郑州市新郑国际机场民航河南空管分局飞行服务室<br/>
                                    邮政编码：451162<br/>
                                    咨询电话：0371-68510544 / 68513195<br/>
                                    传真：0371-68510544<br/>
                                    应急电话：0371-56586167 / 56586168<br/>
                                    技术支持：zhcczpzx@126.com<br />
                                </p>
                            </ul>
                        </div>
                    </li>
                    <li style="width:400px;">
                        <div class="link">
                            <h4>关注我们</h4>
                            <ul>
                                <dl>
                                    <dd>
                                        <img width="94px" height="94px" src="/images/er_wechat.jpg"  alt=""/></dd>
                                    <dd>
                                        <img width="94px" height="94px" src="/images/er_weibo.jpg"  alt=""/></dd>
                                    <dd>
                                        <img width="94px" height="94px" src="/images/er_app.jpg" alt=""/></dd>
                                </dl>
                                <dl>
                                    <dd>官方微信平台</dd>
                                    <dd>官方微博</dd>
                                    <dd>移动客户端(APP)</dd>
                                </dl>
                            </ul>
                        </div>
                    </li>
                </ul>
            </div>
        </div>--%>
        <!--foot end-->
        
        <div class="copyright">
            <div class="cpt_text">
                <div class="cpt_text02">Copyright <% =year %>  &copy;  广州市中南民航空管通信网络科技有限公司</div>
                <%--&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;管理维护：系统开发室&nbsp;&nbsp;&nbsp;&nbsp;<a href="#">粤公网安备44010402000387号</a>
                    <div class="cpt_img">
                    <ul>
                        <li style="padding: 8px 8px 0 0;"><a href="http://www.beian.gov.cn/portal/registerSystemInfo?recordcode=44010402000387" target="_blank">
                            <img src="images/beianlogo.png" alt=""/></a></li>
                        <li style="padding: 12px 2px 0 0;">|</li>
                        <li style="padding: 1px 0px 0 0;"><a href="//bszs.conac.cn/sitename?method=show&id=2AC00CDF5B597751E053022819AC86A8" target="_blank">
                            <img style="width: 40px;" src="images/beianlogo.png" alt=""/></a></li>
                    </ul>
                </div>--%>
            </div>
        </div>
        <%--<script type="text/javascript" src="js/Copyright.js"></script>--%>

</body>
</html>
