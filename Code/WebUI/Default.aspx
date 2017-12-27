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
    <form id="form1" runat="server">
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
                                <span><a href="http://www.zslib.com.cn/TempletPage/userService.aspx?dbid=5" target="_blank">+更多</a></span>
                            </div>
                            <div class="img_container">
                                <img width="288px" height="288px" src="images/News.jpg" alt="" />
                            </div>
                            <div class="txt_container">
                                <div class="scrollbox">
                                    <div id="scrollDiv">
                                        <ul>
                                            <li>
                                                <h3><a href="#" class="linktit">移动娱乐业务突飞</a></h3>
                                                <div>为了探索推进公车改革后，新能源汽车分时租赁项目试点工作...</div>
                                            </li>
                                            <li>
                                                <h3><a href="#" class="linktit">不停转动向上滚动可控制向上向下滚动特效</a></h3>
                                                <div>DIV CSS JS自动不断向上一个一个滚动可控制向上向下滚动特效...</div>
                                            </li>
                                            <li>
                                                <h3><a href="#" class="linktit">全国涂料总产量呈现直线下滑态势</a></h3>
                                                <div>我国涂料工业将面临涂料消费税征收全面铺开，环保压力持续增加...</div>
                                            </li>
                                            <li>
                                                <h3><a href="#" class="linktit">图书预约排行榜</a></h3>
                                                <div>想读·就读·共读,是参照我馆闭架图书预约排行榜，统计出...</div>
                                            </li>
                                            <li>
                                                <h3><a href="#" class="linktit">主题创业街亮相</a></h3>
                                                <div>目前已有包括咖啡厅、酒吧、餐厅、瑜伽室在内的8家商铺入驻该火车...</div>
                                            </li>
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
                                <h2><a href="http://www.zslib.com.cn/TempletPage/userService.aspx?dbid=5" target="_blank">供求信息</a></h2>
                                <span><a href="http://www.zslib.com.cn/TempletPage/userService.aspx?dbid=5" target="_blank">+更多</a></span>
                            </div>
                            <div class="img_container">
                                <img width="288px" height="288px" src="images/Supply.jpg" alt="" />
                            </div>
                            <div class="txt_container">
                                <div class="sideMenu">
                                    <h3 class="sideMenu00 on">
                                        <a href="http://www.zslib.com.cn/TempletPage/Detail.aspx?dbid=1&amp;id=2866" target="_blank" title=" 中山讲堂 国事·家事·天下事">中山讲堂 国事·家事·天下事</a></h3>
                                    <ul style="display: block;">
                                        <p><a href="http://www.zslib.com.cn/TempletPage/Detail.aspx?dbid=1&amp;id=2866" target="_blank" title="第180讲 春秋大学问主 题：当代文化现象与核心价值观问题主 ...">第180讲 春秋大学问主 题：当代文化现象与核心价值观问题主 ...</a></p>
                                    </ul>
                                    <h3 class="sideMenu00"><a href="http://www.zslib.com.cn/TempletPage/Detail.aspx?dbid=1&amp;id=2865" target="_blank" title="第179讲 健康系列">第179讲 健康系列</a></h3>
                                    <ul>
                                        <p><a href="http://www.zslib.com.cn/TempletPage/Detail.aspx?dbid=1&amp;id=2865" target="_blank" title="主 题一：小肠生“气”是疝气主讲：陈双，中山大学附属第六医院胃...">主 题一：小肠生“气”是疝气主讲：陈双，中山大学附属第六医院胃...</a></p>
                                    </ul>
                                    <h3 class="sideMenu00"><a href="http://www.zslib.com.cn/TempletPage/Detail.aspx?dbid=1&amp;id=2864" target="_blank" title="想读·就读·共读】第28-29...">想读·就读·共读】第28-29...</a></h3>
                                    <ul style="display: none;">
                                        <p><a href="http://www.zslib.com.cn/TempletPage/Detail.aspx?dbid=1&amp;id=2864" target="_blank" title="简介：“想读·就读·共读”是参照我馆闭架图书预约排行榜，统计出...">简介：“想读·就读·共读”是参照我馆闭架图书预约排行榜，统计出...</a></p>
                                    </ul>
                                    <h3 class="sideMenu00"><a href="http://www.zslib.com.cn/TempletPage/Detail.aspx?dbid=1&amp;id=2863" target="_blank" title="新书推荐第41期｜行走的人生">新书推荐第41期｜行走的人生</a></h3>
                                    <ul style="display: none;">
                                        <p><a href="http://www.zslib.com.cn/TempletPage/Detail.aspx?dbid=1&amp;id=2863" target="_blank" title="当我开始爱自己（节选） 作者：卓别林当我开始真正爱自己，我开始...">当我开始爱自己（节选） 作者：卓别林当我开始真正爱自己，我开始...</a></p>
                                    </ul>
                                    <h3 class="sideMenu00"><a href="http://www.zslib.com.cn/TempletPage/Detail.aspx?dbid=1&amp;id=2862" target="_blank" title="走进中图”第54期——定向挑战...">走进中图”第54期——定向挑战...</a></h3>
                                    <ul>
                                        <p><a href="http://www.zslib.com.cn/TempletPage/Detail.aspx?dbid=1&amp;id=2862" target="_blank" title="活动简介：“走进中图”活动包括带领读者参观我馆主要服务窗口，介...">活动简介：“走进中图”活动包括带领读者参观我馆主要服务窗口，介...</a></p>
                                    </ul>                                    
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="center_left_new">
                        <div class="content" style="margin: 0 auto">
                            <div class="title" style="background: #fff">
                                <h2><a href="http://www.zslib.com.cn/TempletPage/userService.aspx?dbid=5" target="_blank">通航企业</a></h2>
                                <span><a href="http://www.zslib.com.cn/TempletPage/userService.aspx?dbid=5" target="_blank">+更多</a></span>
                            </div>
                            <div class="img_container">
                                <img width="288px" height="288px" src="images/Company.jpg" alt="" />
                            </div>
                            <div class="txt_container">
                                <div class="sideMenu">
                                    <h3 class="sideMenu00 on"><a href="http://www.zslib.com.cn/TempletPage/Detail.aspx?dbid=1&amp;id=2866" target="_blank" title=" 中山讲堂 国事·家事·天下事">中山讲堂 国事·家事·天下事</a></h3>
                                    <ul style="display: block;">
                                        <p><a href="http://www.zslib.com.cn/TempletPage/Detail.aspx?dbid=1&amp;id=2866" target="_blank" title="第180讲 春秋大学问主 题：当代文化现象与核心价值观问题主 ...">第180讲 春秋大学问主 题：当代文化现象与核心价值观问题主 ...</a></p>
                                    </ul>
                                    <h3 class="sideMenu00"><a href="http://www.zslib.com.cn/TempletPage/Detail.aspx?dbid=1&amp;id=2865" target="_blank" title="第179讲 健康系列">第179讲 健康系列</a></h3>
                                    <ul>
                                        <p><a href="http://www.zslib.com.cn/TempletPage/Detail.aspx?dbid=1&amp;id=2865" target="_blank" title="主 题一：小肠生“气”是疝气主讲：陈双，中山大学附属第六医院胃...">主 题一：小肠生“气”是疝气主讲：陈双，中山大学附属第六医院胃...</a></p>
                                    </ul>
                                    <h3 class="sideMenu00"><a href="http://www.zslib.com.cn/TempletPage/Detail.aspx?dbid=1&amp;id=2864" target="_blank" title="想读·就读·共读】第28-29...">想读·就读·共读】第28-29...</a></h3>
                                    <ul style="display: none;">
                                        <p><a href="http://www.zslib.com.cn/TempletPage/Detail.aspx?dbid=1&amp;id=2864" target="_blank" title="简介：“想读·就读·共读”是参照我馆闭架图书预约排行榜，统计出...">简介：“想读·就读·共读”是参照我馆闭架图书预约排行榜，统计出...</a></p>
                                    </ul>
                                    <h3 class="sideMenu00"><a href="http://www.zslib.com.cn/TempletPage/Detail.aspx?dbid=1&amp;id=2863" target="_blank" title="新书推荐第41期｜行走的人生">新书推荐第41期｜行走的人生</a></h3>
                                    <ul style="display: none;">
                                        <p><a href="http://www.zslib.com.cn/TempletPage/Detail.aspx?dbid=1&amp;id=2863" target="_blank" title="当我开始爱自己（节选） 作者：卓别林当我开始真正爱自己，我开始...">当我开始爱自己（节选） 作者：卓别林当我开始真正爱自己，我开始...</a></p>
                                    </ul>
                                    <h3 class="sideMenu00"><a href="http://www.zslib.com.cn/TempletPage/Detail.aspx?dbid=1&amp;id=2862" target="_blank" title="走进中图”第54期——定向挑战...">走进中图”第54期——定向挑战...</a></h3>
                                    <ul>
                                        <p><a href="http://www.zslib.com.cn/TempletPage/Detail.aspx?dbid=1&amp;id=2862" target="_blank" title="活动简介：“走进中图”活动包括带领读者参观我馆主要服务窗口，介...">活动简介：“走进中图”活动包括带领读者参观我馆主要服务窗口，介馆主要服务窗口...</a></p>
                                    </ul>                                    
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
                            <ul>
                                <li>
                                    <input name="LoginFrame1$usrcode" id="LoginFrame1_usrcode" class="login_name ipt" value="用户名" onkeydown="if(event.keyCode==13){document.getElementById('Button1').focus();}" style="color: rgb(173, 173, 173);" type="text" /></li>
                                <li>
                                    <input name="LoginFrame1$password" id="LoginFrame1_password" class="login_pwd ipt" onkeydown="if(event.keyCode==13){document.getElementById('Button1').focus();}" style="color: rgb(173, 173, 173);" type="password" /></li>
                                <li style="text-align: left; padding: 0 30px 0 23px; width: 280px;">
                                    <span style="float: right">
                                        <img id="LoginFrame1_IMG_Identify" title="看不清点击刷新" onclick="javascript:this.src='/VerifyCode.aspx?rnd='+Math.random()" src="/VerifyCode.aspx?rnd=<%=rnd %>" style="border-width: 0px;" align="middle" alt="" />
                                    </span>
                                    <input name="LoginFrame1$icode" id="LoginFrame1_icode" class="login_yzm ipt" value="验证码" onkeydown="if(event.keyCode==13){document.getElementById('Button1').focus();}" style="color: rgb(173, 173, 173);" type="text" /></li>
                                <li>
                                    <input name="LoginFrame1$Button1" value="登录" onclick="javascript: document.getElementById('LoginFrame1_login').style.display = 'none'; document.getElementById('LoginFrame1_logstate').style.display = 'none'; document.getElementById('LoginFrame1_logining').style.display = ''; $('.login_boxb').toggle(500); clearTimeout(settime_2wm);" id="LoginFrame1_Button1" class="login_btn" type="submit" /></li>
                            </ul>
                        </div>
                    </div>
                    <div class="center_right_middle">
                        <div class="title">
                            <h2><a href="http://www.zslib.com.cn/TempletPage/List.aspx?dbid=1" target="_blank">相关资料</a></h2>
                            <span><a href="http://www.zslib.com.cn/TempletPage/List.aspx?dbid=1" target="_blank">+更多</a></span>
                        </div>
                        <div class="tuijie" style="margin: 0 auto">
                            <ul>
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
    </form>
</body>
</html>
